using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHelper : MonoBehaviour
{

    private static SceneHelper _instance;


    // public SceneId previousScene;
    public static SceneHelper instance {
        get {

            if (_instance == null) {
                _instance = FindObjectOfType<SceneHelper>();

                if (!_instance)
                {

                    var gameObject = Resources.Load("Scenes/SceneHelper") as GameObject;
                    gameObject = Instantiate(gameObject, Vector3.zero, Quaternion.identity);
                    _instance = gameObject.GetComponent<SceneHelper>();
                }
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    public SceneId GetCurrentSceneId()
    {
        Enum.TryParse(SceneManager.GetActiveScene().name, out SceneId sceneId);
        return sceneId;
    }
    public void ReloadScene()
    {
        Enum.TryParse(SceneManager.GetActiveScene().name, out  SceneId sceneId);
        StartCoroutine(_LoadScene(sceneId));
    }
    public void LoadScene(string sceneNameString)
    {
        Enum.TryParse(sceneNameString, out SceneId sceneId);

        LoadScene(sceneId);
    }
    public void LoadScene(SceneId sceneId) {

        StartCoroutine(_LoadScene( sceneId));
    }

    private IEnumerator _LoadScene(SceneId sceneId)
    {
    
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneId.ToString());
     
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
       



    }
}
