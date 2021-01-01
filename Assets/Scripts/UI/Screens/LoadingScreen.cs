using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{


    [SerializeField] CanvasFade canvasFade;

    private static LoadingScreen _instance;

    public static LoadingScreen instance
    {

        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<LoadingScreen>();
                if (!_instance)
                {

                    var gameObject = Resources.Load("UI/Screens/LoadingScreen") as GameObject;
                    gameObject = Instantiate(gameObject, Vector3.zero, Quaternion.identity);
                    _instance = gameObject.GetComponent<LoadingScreen>();
                }


                DontDestroyOnLoad(_instance.gameObject);
            }


            return _instance;


        }


    }


    public IEnumerator ShowScreen() {
      yield return   canvasFade._FadeIn();
    }


    public IEnumerator HideScreen()
    {
        yield return canvasFade._FadeOut();

        Destroy(this.gameObject);
    }


}
