using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

[Serializable]
public class IntermittentData {
    public float activationDelay;
    public float activeDuration;

}

public class IntermittentPlatforms : MonoBehaviour
{
    [SerializeField] List<IntermittentData> intermittents;

    private int index = 0;

    private bool finishRutine = false;


    private void Awake()
    {
        StartCoroutine(HandleIntermittents());
    }

    IEnumerator HandleIntermittents() {
        finishRutine = false;
        var intermitentData = intermittents[index];
        var child = this.transform.GetChild(index);
        foreach (Transform c in this.transform) {
            c.gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(intermitentData.activationDelay);

        child.gameObject.SetActive(true);
        yield return new WaitForSeconds(intermitentData.activeDuration);
        finishRutine = true;
        index++;
        if (index >= this.transform.childCount) {
            index = 0;
        }
    }

    private void Update()
    {
        if (finishRutine) {
            StartCoroutine(HandleIntermittents());
        }
    }
}
