using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


public interface ISceneStateReporter {
  void  OnLoadNewLevel();
    void OnStartLoadNewLevel();
}
public class SceneHelper : MonoBehaviour
{

    private static SceneHelper _instance;
    private List<ISceneStateReporter> observers=new List<ISceneStateReporter>();


    public void AddNewObserver(ISceneStateReporter observer) {
        observers.Add(observer);
    }
    public void DeleteObserver(ISceneStateReporter observer) {
        observers.Remove(observer);
    }

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
        ReportOnStartLoadNewLevel();

        yield return LoadingScreen.instance.ShowScreen();
        yield return new WaitForSeconds(0.4f);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        yield return LoadingScreen.instance.HideScreen();

        ReportOnNewLevelLoaded();



    }
    void ReportOnStartLoadNewLevel()
    {
        foreach (var observer in observers)
        {
            if(observer!=null)
            observer.OnStartLoadNewLevel();
        }
    }
    void ReportOnNewLevelLoaded() {
        foreach (var observer in observers) {
            if (observer != null)
                observer.OnLoadNewLevel();
        }
    }
}
