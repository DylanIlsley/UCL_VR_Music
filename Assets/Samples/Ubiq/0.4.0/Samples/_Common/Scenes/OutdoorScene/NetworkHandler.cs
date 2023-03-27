using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Messaging;
using Ubiq.Spawning;
using System;
using System.Threading;

using AudioMessage;
using Ubiq.Extensions;
using UnityEngine.UI;


public class NetworkHandler : MonoBehaviour
{
    public void TriggerSound(AudioSource audioSource, float fStartTime)
    {
        Debug.Log("Triggering sound..." + audioSource.name);
        // Only play a sound and add it to the loop if it is in edit mode
        if (!m_bEdit)
            return;

        // If it is not playing, then it should not be allowed to be apart of the loop either
        if (!m_bPlay)
            return;

        float fLoopTime = fStartTime - m_LoopStartTime;

        // Update recording for future playing


        float[] samples = new float[audioSource.clip.samples * audioSource.clip.channels];
        audioSource.clip.GetData(samples, 0);

        float[] currentSamples = new float[audioSource.clip.samples * m_nextAudioSource.clip.channels];
        m_nextAudioSource.clip.GetData(currentSamples, (int)(fLoopTime * m_iSampleRate_Hz));

        // Adding current track with the new track
        float[] newSamples = new float[audioSource.clip.samples * m_nextAudioSource.clip.channels];
        for (int i=0; i<audioSource.clip.samples; i++)
        {
            newSamples[i] = currentSamples[i] + samples[i];
        }

        // TODO: Need to update one for the next cycle to prevent the sound playing twice
        m_nextAudioSource.clip.SetData(newSamples, (int)(fLoopTime * m_iSampleRate_Hz));

        // Sending off so that it can be used and updated on the other side
        int iAudioIndex = m_listAudioSources.IndexOf(audioSource);
        if (iAudioIndex != -1)
            SendAudioTrackUpdate(iAudioIndex, fLoopTime);
        else
            Debug.LogError("Trigger audio with name " + audioSource.name + "with start time [" + fStartTime.ToString() + "] could not be found");


    }

    public void SetEditMode(bool bEnableEdit)
    {

        Debug.Log("Changing edit mode to " + (bEnableEdit ? "true" : "false"));
        m_bEdit = bEnableEdit;
    }

    public void StartPlaying()
    {
        if (m_bPlay)
            Debug.LogWarning("Start playing called whilst already started");
        else
            Debug.Log("Start playing called");
        m_bPlay = true;
    }

    public void StopPlaying()
    {
        if (m_bPlay)
            Debug.LogWarning("Stop playing called whilst already stopped");
        else
            Debug.Log("Stop playing called");
        m_bPlay = false;
        m_currentAudioSource.Stop();
    }

    public void ClearRecording()
    {
        Debug.Log("Clearing current recording");

        m_currentAudioSource.clip = AudioClip.Create("LoopedMusic", m_iSampleRate_Hz * (int)m_LoopDuration_s, 1, m_iSampleRate_Hz, false);
        
        SendAudioTrackClear();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_dictRegisteredUnits = new Dictionary<Tuple<uint, uint>, Action<ReferenceCountedSceneGraphMessage>>();
        m_currentAudioSource = gameObject.AddComponent<AudioSource>();
        m_currentAudioSource.volume = 1.0f;
        m_currentAudioSource.clip = AudioClip.Create("LoopedMusic", m_iSampleRate_Hz * (int)m_LoopDuration_s, 1, m_iSampleRate_Hz, false);
        m_currentAudioSource.spatialBlend = 0;
        m_nextAudioSource = m_currentAudioSource;
        RegisterUnitMessages();
        context = NetworkScene.Register(this);
        Debug.Log(NetworkId);
       // m_currentAudioSource = GetComponent<AudioSource>();
        Debug.Log(m_currentAudioSource.ToString());
        OnStart();

    }

    void OnStart()
    {
        SendAudioTrackRequest();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Add master control information here when needed
        // Will need to add further controls at a later point
        if (!m_currentAudioSource.isPlaying)
        {
            m_LoopStartTime = Time.time;

            m_currentAudioSource = m_nextAudioSource;

            if (m_bPlay)
                m_currentAudioSource.Play();

            //SendAudioTrackRequest();

            // TODO: Master control will need to be checked here and exchanged at this point if master
            /*
            if(m_bMaster){
                m_bMaster=false;
                SendMasterControlRelease();
                SendAudioStatusResponse();
            }
            */
        }
        else if (Time.time - m_LoopStartTime > m_LoopDuration_s)
            m_currentAudioSource.Stop();
    }

    void RegisterUnitMessages()
    {

        // GROUP 1 - Audio Track Messages
        RegisterUnitMessage(new AudioTrackRequest(), OnAudioTrackRequest);
        RegisterUnitMessage(new AudioTrackResponse(), OnAudioTrackResponse);
        RegisterUnitMessage(new AudioTrackUpdate(), OnAudioTrackUpdate);

        RegisterUnitMessage(new AudioTrackClearRequest(), OnAudioTrackClear);


        // GROUP 1 - Master messages
        // RegisterUnitMessage(new MasterControlStatusRequestMessage(), OnMasterControlStatusRequest);
        // RegisterUnitMessage(new MasterControlStatusResponseMessage(), OnMasterControlStatusResponse);
        //RegisterUnitMessage(new MasterControlReleaseMessage(), OnMasterControlRelease);

        // Group 2 - Audio track messages
        //RegisterUnitMessage(new AudioStatusRequestMessage(), OnAudioStatusRequest);
        //RegisterUnitMessage(new AudioStatusResponseMessage(), OnAudioStatusResponse);
    }

    void RegisterUnitMessage(MessageInterface m, Action<ReferenceCountedSceneGraphMessage> OnMessageFunction)
    {
        Debug.Log(OnMessageFunction);

        m_dictRegisteredUnits[Tuple.Create(m.GetGroupID(), m.GetUnitID())] = OnMessageFunction;
    }

    public void SendUpdate()
    {
        

        //context.SendJson();
    }


    public void ProcessMessage(ReferenceCountedSceneGraphMessage m)
    {
        Debug.Log(m.ToString());
        var message = m.FromJson<MessageInterface>();
        uint GroupID = message.GetGroupID();
        uint UnitID = message.GetUnitID();
        Debug.Log("Message received with ID["+GroupID +", "+UnitID+"]");
        // TODO: Find in list of message types
        Action<ReferenceCountedSceneGraphMessage> messageFunc;
        m_dictRegisteredUnits.TryGetValue(Tuple.Create(GroupID, UnitID), out messageFunc);

        if (messageFunc != null)
            messageFunc(m);
        else
            Debug.LogError("Message [" + GroupID.ToString() + "," + UnitID.ToString() + "] received but not supported");
    }

    public void SendAudioTrackRequest()
    {
        context.SendJson(new AudioTrackRequest()
        {
        });
    }

    public void SendAudioTrackResponse()
    {
        AudioTrackResponse ATR = new AudioTrackResponse();
        ATR.m_uAudioID = m_listAudioSources.IndexOf(m_currentAudioSource);
        //ATR.m_Audio = m_currentAudioSource;
        // Debug.Log(ATR.m_Audio.ToString());
        context.SendJson(ATR);
    }

    public void SendAudioTrackUpdate(int iAudioID, float fStartTime)
    {
        Debug.Log("AudioID [" + iAudioID.ToString() + "] sending AudioTrackUpdate with start time " + fStartTime.ToString());
        context.SendJson(new AudioTrackUpdate()
        { 
            m_iAudioID = iAudioID,
            m_fStartTime = fStartTime
        });
    }

    public void SendAudioTrackClear()
    {
        Debug.Log("Sending audio track clear request");
        context.SendJson(new AudioTrackClearRequest()
        {
        });
    }

    private void OnAudioTrackRequest(ReferenceCountedSceneGraphMessage m)
    {
        Debug.Log("OnAudioTrackRequest received");
        SendAudioTrackResponse();
    }

    private void OnAudioTrackResponse(ReferenceCountedSceneGraphMessage m)
    {
        Debug.Log("OnAudioTrackResponse received");
        var message = m.FromJson<AudioTrackResponse>();
    }

    private void OnAudioTrackUpdate(ReferenceCountedSceneGraphMessage m)
    {
        
        var message = m.FromJson<AudioTrackUpdate>();
        Debug.Log("OnAudioTrackUpdate received with AudioID[" + message.m_iAudioID.ToString() + "] and start time ["+ message.m_fStartTime.ToString() + "]");

        // Updating audio based off this
        AudioSource audioSource = m_listAudioSources[message.m_iAudioID];

        // if the start time is close enough to the current time, then play it straight away
        float fLoopTime = Time.time - m_LoopStartTime;
        if (message.m_fStartTime - fLoopTime < 0.1f && m_bPlay)
            audioSource.Play();

        float[] samples = new float[audioSource.clip.samples * audioSource.clip.channels];
        audioSource.clip.GetData(samples, 0);

        float[] currentSamples = new float[audioSource.clip.samples * m_nextAudioSource.clip.channels];
        m_nextAudioSource.clip.GetData(currentSamples, (int)(message.m_fStartTime * m_iSampleRate_Hz));

        // Adding current track with the new track
        float[] newSamples = new float[audioSource.clip.samples * m_nextAudioSource.clip.channels];
        for (int i = 0; i < audioSource.clip.samples; i++)
        {
            newSamples[i] = currentSamples[i] + samples[i];
        }

        // UPdate sound here by adding it as well
        m_currentAudioSource.clip.SetData(newSamples, (int)(message.m_fStartTime * m_iSampleRate_Hz));
    }

    private void OnAudioTrackClear(ReferenceCountedSceneGraphMessage m)
    {
        var message = m.FromJson<AudioTrackUpdate>();
        Debug.Log("OnAudioTrackClear received");

        m_currentAudioSource.clip = AudioClip.Create("LoopedMusic", m_iSampleRate_Hz * (int)m_LoopDuration_s, 1, m_iSampleRate_Hz, false);
    }

    public int GetCurrentLoopTime()
    {
        return (int)(Time.time - m_LoopStartTime);
    }



    /*
    public void SendMasterControlStatusRequest()
    {
        context.SendJson(new MasterControlStatusRequestMessage()
        {
        });
    }
   
    public void SendMasterControlRelease()
    {
        context.SendJson(new MasterControlReleaseMessage()
        {
        });
    }


    public void SendMasterControlStatusResponse()
    {
        context.SendJson(new MasterControlStatusResponseMessage()
        {

        });
    }

    public void SendAudioStatusRequest()
    {
        context.SendJson(new AudioStatusRequestMessage()
        {
        });
    }

    public void SendAudioStatusResponse(List<AudioSource> listAudioSource, bool bPrompted)
    {

        context.SendJson(new AudioStatusResponseMessage()
        {
            listAudioSourceFiles = listAudioSource
        });

    }

    private void OnAudioStatusRequest(ReferenceCountedSceneGraphMessage m)
    {
        // Do not need to actually need to convert anything
        Debug.Log("OnAudioStatusRequest received");


        // TODO: Use a seperate thread for this
        List<AudioSource> audioSourceList = new List<AudioSource>();
        audioSourceList.Add(m_currentAudioSource);

        SendAudioStatusResponse(audioSourceList, true);
    }


    private void OnAudioStatusResponse(ReferenceCountedSceneGraphMessage m)
    {
        Debug.Log("OnAudioStatusResponse received");

        var message = m.FromJson<AudioStatusResponseMessage>();

        // TODO: Use a loop builder. For now just save the firstfile and then continue
        m_mxAudioSourceLock.WaitOne();
        m_nextAudioSource = message.listAudioSourceFiles[0];
        m_mxAudioSourceLock.ReleaseMutex();
        // Used for debugging
        m_nextAudioSource.Play();
    }

    private void OnMasterControlRelease(ReferenceCountedSceneGraphMessage m)
    {
        Debug.Log("OnMasterControlRelease received");
        var message = m.FromJson<MasterControlReleaseMessage>();


        // Set master state if they have released master status. This will be used to flag certain events
        m_mxMasterLock.WaitOne();
        m_bMaster = true;
        m_mxMasterLock.ReleaseMutex();
        SendMasterControlStatusResponse();
    }

    private void OnMasterControlStatusRequest(ReferenceCountedSceneGraphMessage m)
    {
        var message = m.FromJson<MasterControlStatusRequestMessage>();

        SendMasterControlStatusResponse();
    }

    private void OnMasterControlStatusResponse(ReferenceCountedSceneGraphMessage m)
    {
        var message = m.FromJson<MasterControlStatusResponseMessage>();

        // Check to see for various thing
        m_mxMasterLock.WaitOne();
        if (m_bMaster && message.m_strUserID != "")
            Debug.LogWarning("MasterControlStatusResponse message received with user ID, clashing master control....");
        m_mxMasterLock.ReleaseMutex();
    }
    */

    /// --------------------------------------------------------
    ///  MEMBER VARIABLES
    /// --------------------------------------------------------
    private Dictionary<Tuple<uint, uint>, Action<ReferenceCountedSceneGraphMessage>> m_dictRegisteredUnits;
    public NetworkId NetworkId { get; set; }

    private NetworkContext context;

    public bool m_bPlay = true;
    bool m_bEdit = true;
    private AudioSource m_nextAudioSource;
    public AudioSource m_currentAudioSource;

    public List<AudioSource> m_listAudioSources;
    public float m_LoopStartTime = 0;
    private float m_LoopDuration_s = 10;

    private int m_iSampleRate_Hz = 44100;
}
