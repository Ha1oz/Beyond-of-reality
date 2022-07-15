using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitMessageManager : MonoBehaviour
{
    public void ExitButton(){
        GameManager.Instance.SceneLoad(3);
    }

    public void ContinueButton(){
        //GameManager.Instance.OffMessageAboutFinish();
    }
}
