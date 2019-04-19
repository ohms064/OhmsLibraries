using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PoolMonobehaviours_UnityEvents : MonoBehaviour {
    public UnitySpawnEvent OnSpawn, OnDespawn ;
    private PoolMonoBehaviour poolMono;

    private void Awake() {
        poolMono = GetComponent<PoolMonoBehaviour>();
    }

    private void OnEnable() {
        poolMono.OnSpawn += OnSpawn.Invoke;
        poolMono.OnDespawn += OnDespawn.Invoke;
    }

    private void OnDisable() {
        poolMono.OnSpawn -= OnSpawn.Invoke;
        poolMono.OnDespawn -= OnDespawn.Invoke;
    }
}

[System.Serializable]
public class UnitySpawnEvent : UnityEvent<PoolMonoBehaviour> { }
