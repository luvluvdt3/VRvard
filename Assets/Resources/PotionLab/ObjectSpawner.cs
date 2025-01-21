using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

/// <summary>
/// Will spawn the given prefab, either when the Spawn function is called or if a MinInstance is setup, until that
/// amount exist in the scene. If Spawn is called when there is already MaxInstance number of object in the scene,
/// it will destroy the oldest one to make room for the newest.
/// </summary>
public class ObjectSpawner : MonoBehaviour
{
    public GameObject Prefab;
    public VisualEffect SpawnEffect;
    public Transform SpawnPoint;
    

    private void Start()
    {
        Prefab = null;
    }

    public void Spawn()
    {
        var newInst = Instantiate(Prefab, SpawnPoint.position, SpawnPoint.rotation);
        
        SpawnEffect.SendEvent("SingleBurst");
    }
    
}
