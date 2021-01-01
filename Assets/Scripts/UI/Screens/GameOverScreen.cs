using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{

    [SerializeField] CanvasFade canvasFade;

    private static GameOverScreen _instance;

    public static GameOverScreen instance
    {

        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameOverScreen>();
                if (!_instance)
                {

                    var gameObject = Resources.Load("UI/Screens/GameOverScreen") as GameObject;
                    gameObject = Instantiate(gameObject, Vector3.zero, Quaternion.identity);
                    _instance = gameObject.GetComponent<GameOverScreen>();
                }


                DontDestroyOnLoad(_instance.gameObject);
            }


            return _instance;


        }


    }


    public void ShowScreen()
    {
        canvasFade.FadeIn();
        AudioManager.instance.ChangePitchMusic();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            AudioManager.instance.RestorePitchMusic();
            SceneHelper.instance.LoadScene(SceneHelper.instance.GetCurrentSceneId());
            Destroy(this.gameObject);
        }
    }


}
