using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public float rotateSpeed;
    public float rotLock;
    //private Camera cam;
    private float rotX;
    private void Start(){
        Camera.main.fieldOfView = PlayerPrefs.GetFloat("FOV");
        //gameObject.GetComponent<Camera>().      
    }
    void FixedUpdate()
    {
        rotX -= Input.GetAxis("Mouse Y") * rotateSpeed*PlayerPrefs.GetFloat("Sensitivity");
        rotX = Mathf.Clamp(rotX, -rotLock, rotLock);

        transform.localEulerAngles = new Vector3(rotX,
            transform.localEulerAngles.y, transform.localEulerAngles.z);
    }
    
}