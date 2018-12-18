using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Text;

public abstract class Pool<T> : MonoBehaviour where T : PoolMonoBehaviour {
#if ODIN_INSPECTOR
    [ValidateInput( "ValidatePoolSize" )]
    public int poolSize = 1;
    [AssetsOnly]
    public T[] PoolMonoBehaviours;
#else
    public int poolSize = 1;
    public T[] PoolMonoBehaviours;
#endif
    [ShowInInspector, HideInEditorMode, DisableInPlayMode]
    protected T[] pool;
    private int iterator = 0;

#if UNITY_EDITOR && ODIN_INSPECTOR
    private bool ValidatePoolSize ( int i ) {
        return i > 0;
    }
#endif

    public override string ToString() {
        StringBuilder builder = new StringBuilder();
        for ( int i = 0; i < PoolMonoBehaviours.Length;  i++){
            builder.AppendFormat( "{0} ", PoolMonoBehaviours[i].name );
        }
        return builder.ToString();
    }

    public IEnumerable<T> GetUnavailableObjects () {
        for ( int i = 0; i < pool.Length; i++ ) {
            if ( !pool[i].Available )
                yield return pool[i];
        }
    }

    public IEnumerable<T> GetAvailableObjects () {
        for ( int i = 0; i < pool.Length; i++ ) {
            if ( pool[i].Available )
                yield return pool[i];
        }
    }

    public virtual bool RequestPoolMonoBehaviour ( out T PoolMonoBehaviour ) {
        if ( pool == null ) {
            PoolMonoBehaviour = null;
            return false;
        }
        if ( iterator >= pool.Length ) {
            iterator = 0;
        }
        DebugManager.LogFormat( "Getting object {0} from pool", iterator );
        PoolMonoBehaviour = pool[iterator];
        iterator++;
        return PoolMonoBehaviour.Available;
    }

    protected abstract void InstantiateObjects ();

    public void RegisterOnSpawn(System.Action OnSpawn){
        for ( int i = 0; i < pool.Length;  i++){
            pool[i].OnSpawn += OnSpawn;
        }
    }

    public void UnregisterOnSpawn( System.Action OnSpawn ) {
        for ( int i = 0; i < pool.Length; i++ ) {
            pool[i].OnSpawn -= OnSpawn;
        }
    }

    public void RegisterOnDespawn( System.Action OnDespawn ) {
        Debug.Log( "Registrando evento" );
        for ( int i = 0; i < pool.Length; i++ ) {
            pool[i].OnDespawn += OnDespawn;
        }
    }

    public void UnregisterOnDespawn( System.Action OnDespawn ) {
        for ( int i = 0; i < pool.Length; i++ ) {
            pool[i].OnDespawn -= OnDespawn;
        }
    }
}

public abstract class PoolMonoBehaviour : MonoBehaviour {
    public event System.Action OnSpawn, OnDespawn;
    //Disponible para el pool
    public virtual bool Available {
        get {
            return !gameObject.activeSelf;
        }
        set {
            gameObject.SetActive( !value );
        }
    }

    public virtual void Spawn ( Vector3 position ) {
        Available = false;
        transform.position = position;
        CallOnSpawnEvent();
    }

    public virtual void Despawn () {
        CallOnDespawnEvent();
        Available = true;
    }

    protected void CallOnDespawnEvent () {
        OnDespawn?.Invoke();
    }

    protected void CallOnSpawnEvent () {
        OnSpawn?.Invoke();
    }
}
