using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour
{

    
    public void WaitButton(){

        GameManager.Instance.OffMessage();

    }

    public void GoButton(){
        GameManager.Instance.OffMessage();
        GameManager.Instance.SceneLoad(0);
    }
    
}
