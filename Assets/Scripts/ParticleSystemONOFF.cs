using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemONOFF : MonoBehaviour
{
    public ParticleSystem PS;

    private void OnEnable()
    {
        PS.Play();
    }
    private void OnDisable()
    {
        PS.Stop();
    }
}
