
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
public class ObjectsPool<T> : Pool<T> where T : PoolMonoBehaviour {

    protected T PoolMonoBehaviour {
        get {
            return PoolMonoBehaviours[Random.Range( 0, PoolMonoBehaviours.Length )];
        }
    }

    [Tooltip( "Crea la misma cantidad de objetos por tipo." )]
    public bool evenlyCreate;

    protected virtual void Awake() {
        InstantiateObjects();
    }

    /// <summary>
    /// Creates all the objects to be used in the pool.
    /// </summary>
    protected override void InstantiateObjects () {
        poolQueue = new Queue<T>();
        if ( evenlyCreate ) {
            pool = new T[poolSize * PoolMonoBehaviours.Length];
            for ( int i = 0; i < PoolMonoBehaviours.Length; i++ ) {
                for ( int j = 0; j < poolSize; j++ ) {
                    var index = i * poolSize + j;
                    pool[index] = Instantiate( PoolMonoBehaviours[i] );
                    pool[index].Available = true;
                    pool[index].OnPoolReturnRequest = ReturnToQueue;
                    poolQueue.Enqueue( pool[index] );
                }
            }
        }
        else {
            pool = new T[poolSize];
            for ( int i = 0; i < poolSize; i++ ) {
                pool[i] = Instantiate( PoolMonoBehaviour );
                pool[i].Available = true;
                pool[i].OnPoolReturnRequest = ReturnToQueue;
                poolQueue.Enqueue( pool[i] );
            }
        }
    }
}


