using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class AutoDestroyEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ParticleSystem partcleSystem = GetComponent<ParticleSystem>();
        
        Destroy(gameObject, (float)partcleSystem.main.duration);
    }
}
