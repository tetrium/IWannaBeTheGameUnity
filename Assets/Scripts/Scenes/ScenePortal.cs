using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PortalType { 
    ReturnPortal=0,
    NextLevelPortal=1
}
public class ScenePortal : MonoBehaviour
{


    [SerializeField] PortalType portalType;



    public PortalType GetPortalType() {
        return portalType;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player")) {
            StartLoadLevel();
        }
    }
    void StartLoadLevel() {
        var currentScene = (int)SceneHelper.instance.GetCurrentSceneId();

        var sceneNameToLoad = "";
        if (portalType == PortalType.ReturnPortal) {
            sceneNameToLoad = "Level" + (currentScene - 1);
        }
        if (portalType == PortalType.NextLevelPortal)
        {
            sceneNameToLoad = "Level" + (currentScene + 1);
        }

        SceneHelper.instance.LoadScene(sceneNameToLoad);



    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (portalType == PortalType.ReturnPortal)
        {
            Gizmos.color = Color.yellow;
        }
        else {
            Gizmos.color = Color.cyan;

        }

        for (int i = 0; i < 5; i++) {
            Gizmos.DrawWireCube(this.transform.position, new Vector3(0.3f, 0.3f, 0.3f) * i);
        }
    }
#endif
}
