using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstScene : MonoBehaviour {
    public SceneDataScriptable scene;

    private void Awake() {
        scene.sceneManager.data = scene;    
    }
}
