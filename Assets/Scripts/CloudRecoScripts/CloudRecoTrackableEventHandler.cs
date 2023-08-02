/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Qualcomm Connected Experiences, Inc.
==============================================================================*/

using UnityEngine;
using Vuforia;
using System.Xml;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A custom handler that implements the ITrackableEventHandler interface.
/// </summary>
public class CloudRecoTrackableEventHandler : MonoBehaviour,
                                            ITrackableEventHandler
{
    #region PRIVATE_MEMBER_VARIABLES

    public GameObject back;
	public GameObject closeBtn;
	public delegate void FoundMarker();
	public static event FoundMarker EventFoundMarker;
	public delegate void OnTrackingChanged(bool bFound);
	public static event OnTrackingChanged EventTrackingChanged;
    private TrackableBehaviour mTrackableBehaviour;
	private bool bFoundMarker = false;

	#endregion // PRIVATE_MEMBER_VARIABLES
    #region UNTIY_MONOBEHAVIOUR_METHODS
    
    void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
		GUIManager.EventFindingMarker += OnFindingMarker;
    }

	void Update()
	{
	}

	void OnDestroy()
	{
		GUIManager.EventFindingMarker -= OnFindingMarker;
	}

	#endregion // UNTIY_MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS
	public TrackableBehaviour.Status GetCurrentStatus()
	{
		return mTrackableBehaviour.CurrentStatus;
	}
    /// <summary>
    /// Implementation of the ITrackableEventHandler function called when the
    /// tracking state changes.
    /// </summary>

	public void OnTrackableStateChanged(
                                    TrackableBehaviour.Status previousStatus,
                                    TrackableBehaviour.Status newStatus)
    {
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
			newStatus == TrackableBehaviour.Status.TRACKED ||
			newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED) {
			if (mTrackableBehaviour.Trackable != null)

			if (!bFoundMarker) {
				OnTrackingFound ();
				bFoundMarker = true;
			}
		}
    }

    #endregion // PUBLIC_METHODS

    #region PRIVATE_METHODS


    private void OnTrackingFound()
    {
		EventFoundMarker ();
        back.SetActive(true);
		closeBtn.SetActive (true);
		// Stop finder since we have now a result, finder will be restarted again when we lose track of the result
		ObjectTracker objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
		if (objectTracker != null)
		{
			objectTracker.TargetFinder.Stop();
		}

		Global.ARCampos = new Vector3 (0.0f, this.transform.localScale.y * 1.6f, 0.0f);

        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");	
    }

    private void OnTrackingLost()
    {
        back.SetActive(false);
		closeBtn.SetActive (false);
    }

	private void OnFindingMarker()
	{
		// Start finder again if we lost the current trackable
		Debug.Log("asdf");
		ObjectTracker imageTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
		if(imageTracker != null)
		{
			imageTracker.TargetFinder.ClearTrackables(false);
			imageTracker.TargetFinder.StartRecognition();
		}
		bFoundMarker = false;
		if(Screen.orientation != ScreenOrientation.Landscape)
			Screen.orientation = ScreenOrientation.Landscape;
	}

    #endregion // PRIVATE_METHODS
}
