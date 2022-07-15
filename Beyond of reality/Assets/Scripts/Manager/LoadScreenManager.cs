using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScreenManager : MonoBehaviour
{
    public static int idLoadScene;
    public Image loadImg;
    private bool cont;

    private void Start()
    {
        StartCoroutine(AsyncLoad());
        
    }



    IEnumerator AsyncLoad()
    {
        //yield return new WaitForSeconds(2f); need to see LoadScreen
        AsyncOperation operation = SceneManager.LoadSceneAsync(idLoadScene);


        while(!operation.isDone)
        {
            float progress = operation.progress / 0.9f;
            loadImg.fillAmount = progress;
            yield return null;
        }
    }
}

