using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class TorchLight : MonoBehaviour
{
    [SerializeField] private GameObject SpotLight;

    private void OnEnable()
    {
        InputManager.OnAim += CheckIfAiming;
    }

    private void OnDisable()
    {
        InputManager.OnAim -= CheckIfAiming;
    }

    private void CheckIfAiming(bool aim)
    {
        StartCoroutine(LightDelay(aim));
    }

    private IEnumerator LightDelay(bool aim)
    {
        yield return new WaitForSeconds(0.4f);
        SpotLight.SetActive(!aim);
    }
}
