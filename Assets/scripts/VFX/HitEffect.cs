using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem splatter;
    [SerializeField] private ParticleSystem hit;
    [SerializeField] private float damageScaling;
    private float damageAmount;

    void Init(float damageAmount)
    {
        
    }
}
