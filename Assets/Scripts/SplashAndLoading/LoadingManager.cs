/*============================================================================== 
 * Copyright (c) 2012-2014 Qualcomm Connected Experiences, Inc. All Rights Reserved. 
 * Confidential and Proprietary – Qualcomm Connected Experiences, Inc. 
 * ==============================================================================*/

using UnityEngine;
using System.Collections;

/// <summary>
/// Loading manager.
/// 
/// This Script handles the loading of the Main scene in background
/// displaying a loading animation while the scene is being loaded
/// </summary>
public class LoadingManager : MonoBehaviour
{
    #region UNITY_MONOBEHAVIOUR_METHODS
    public float loadingDelay = 5.0F;
    void Start()
    {
        StartCoroutine(LoadNextSceneAfter(loadingDelay));
    }

    void Update()
    {
    }

    #endregion UNITY_MONOBEHAVIOUR_METHODS

    #region PRIVATE_METHODS
    private IEnumerator LoadNextSceneAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Application.LoadLevel("Vuforia-4-Scan");
    }
    #endregion PRIVATE_METHODS
}
