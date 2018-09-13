using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class Util : MonoBehaviour {

    public static Util instance;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SceneChange(string sceneName)
    {
        EditorSceneManager.LoadScene(sceneName);
    }
}
