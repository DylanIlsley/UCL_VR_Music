using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioMessage
{
    class MessageInterface
    {
        public uint m_uGroupID = 0;
        public uint m_uUnitID = 0;
        // uint uMessageGroupID;
        virtual public uint GetGroupID() { return m_uGroupID; }
        virtual public uint GetUnitID() { return m_uUnitID; }
    }


    class AudioTrackRequest : MessageInterface
    {
        public AudioTrackRequest()
        {
            m_uGroupID = 1;
            m_uUnitID = 1;
        }
    }

    class AudioTrackResponse : MessageInterface
    {
        public AudioTrackResponse()
        {
            m_uGroupID = 1;
            m_uUnitID = 2;
        }

        //public AudioSource m_Audio;
        public int m_uAudioID;
        //List<int> m_AudioIDs;
        //List<int> m_AudioStartTimes;
    }

    class AudioTrackUpdate : MessageInterface
    {

        public AudioTrackUpdate()
        {
            m_uGroupID = 1;
            m_uUnitID = 3;
        }


        //public AudioSource m_NewAudio;
        public int m_iAudioID = -1;
        //int m_newAudioID;
        public float m_fStartTime = -1;
    }

   
    
    class AudioTrackClearRequest : MessageInterface
    {
        public AudioTrackClearRequest()
        {
            m_uGroupID = 2;
            m_uUnitID = 1;
        }
    }

    class EffectUpdate : MessageInterface
    {
        public EffectUpdate()
        {
            m_uGroupID = 3;
            m_uUnitID = 1;
        }

        public int m_iEffectID = -1;
        public float m_fStartTime = -1;
    }
}
    /*
    class MasterControlStatusRequestMessage: MessageInterface
    {
        public override uint GetGroupID()  { return 1; }
        public override uint GetUnitID() { return 1; }
        
    }

    class MasterControlStatusResponseMessage: MessageInterface
    {
        // Need to add the relevant user ID
        // TODO: Find out how to get the user ID
        public string m_strUserID; // NOTE: Placeholder for ID

        public override uint GetGroupID() { return 1; }
        public override uint GetUnitID() { return 2; }
    }

    class MasterControlReleaseMessage : MessageInterface
    {
        // Need to add the relevant user ID
        // TODO: Find out how to get the user ID
        string userID; // NOTE: Placeholder for ID


        public override uint GetGroupID() { return 1; }
        public override uint GetUnitID() { return 3; }
    }

    class AudioStatusRequestMessage : MessageInterface
    {
        // Requests the current status of the record
        public override uint GetGroupID() { return 2; }
        public override uint GetUnitID() { return 1; }
    }


    /// <summary>
    ///  Needs to send the list of audio sources used, all of their volumes and start times
    ///  Could possibly just send the audio source since it's serialized
    /// </summary>
    class AudioStatusResponseMessage : MessageInterface
    {
        bool bPrompted; ///< Whether the response has been requested or not
        // NOTE: WE cannot use the actual audio sources for this. We will probably either need to create a custom audio source type OR some other way to identify it
        // TODO: Change from actual audio sources
        public List<AudioSource> listAudioSourceFiles; ///< List of all the audio sources that are used in the track
        public List<uint> listAudioStartTimes;
        public List<uint> listAudioVolumes;

        public override uint GetGroupID() { return 2; }
        public override uint GetUnitID() { return 2; }
    }
    */


