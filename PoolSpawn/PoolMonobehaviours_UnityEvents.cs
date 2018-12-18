using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PoolMonobehaviours_UnityEvents : MonoBehaviour {
    public UnityEvent OnSpawn = new UnityEvent(), OnDespawn = new UnityEvent();
    private PoolMonoBehaviour poolMono;

    private void Awake() {
        poolMono = GetComponent<PoolMonoBehaviour>();
    }

    private void OnEnable() {
        poolMono.OnSpawn += PoolMono_OnSpawn;;
        poolMono.OnDespawn += PoolMono_OnDespawn;;
    }

    private void OnDisable() {
        poolMono.OnSpawn -= PoolMono_OnSpawn;
        poolMono.OnDespawn -= PoolMono_OnDespawn;
    }

    private void PoolMono_OnDespawn() {
        OnSpawn.Invoke();
    }


    private void PoolMono_OnSpawn() {
        OnDespawn.Invoke();
    }


}
