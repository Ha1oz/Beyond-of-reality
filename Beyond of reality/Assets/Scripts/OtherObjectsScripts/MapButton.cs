using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapButton : MonoBehaviour
{
    public Animator animat;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        animat = GetComponent<Animator>();
        animat.SetBool("isTriggered",true);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider collider){
        Collider col = player.GetComponent<Collider>();
        if(collider == col){
            animat.SetBool("isTriggered",false);
            
        }
    }
    private void OnTriggerExit(Collider collider){
        Collider col = player.GetComponent<Collider>();
        if(collider == col){
            animat.SetBool("isTriggered",true);
        }
    }
}
