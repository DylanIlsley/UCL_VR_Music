using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Parent class responsible for extracting beats from..
/// ..spectrum value given by AudioSpectrum.cs
/// </summary>
public class AudioSyncer : MonoBehaviour {

	/// <summary>
	/// Inherit this to cause some behavior on each beat
	/// </summary>
	public virtual void OnBeat()
	{
		Debug.Log("beat");
		m_timer = 0;
		m_isBeat = true;
	}

	/// <summary>
	/// Inherit this to do whatever you want in Unity's update function
	/// Typically, this is used to arrive at some rest state..
	/// ..defined by the child class
	/// </summary>
	public virtual void OnUpdate()
	{ 
		m_timer += Time.deltaTime;
	}

	public void OnTrigger()
	{
		// if minimum beat interval is reached
		if (m_timer > timeStep)
			OnBeat();
	}


	private void Update()
	{
		OnUpdate();
	}

	public float bias;
	public float timeStep;
	public float timeToBeat;
	public float restSmoothTime;

	private float m_timer;

	protected bool m_isBeat;
}
