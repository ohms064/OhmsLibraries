using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoadToText : MonoBehaviour {

    [SerializeField]SceneLoaderScriptable sceneLoader;

	// Use this for initialization
	void Start () {
        GetComponent<Text>().text = sceneLoader.data.levelName;
	}

}
