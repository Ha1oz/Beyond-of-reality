using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastShootMainWeapon : MonoBehaviour
{
    public float range;

    [Space(20)]
    public LayerMask whatIsGrappleable;
    public Transform gunTip, camera, player;
    private GameObject other;
    private LineRenderer lr;
    private Vector3 grapplePoint;
    private float maxDistance=35f;
    private SpringJoint joint;
    private RaycastHit hit;
    private bool isStopped;

    public static int PBCounter,UGCounter,PBSaver,UGSaver;
    


    


    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }




    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(camera.position,camera.forward,out hit,range))
            {
                GameObject hitObject = hit.transform.gameObject;

                GameManager.Instance.SphereCreate(hit.point,hitObject);

                PBCounter++;     
                PBSaver = PlayerPrefs.GetInt("StatisticsOfPlacedGB");
                PBSaver++;

                PlayerPrefs.SetInt("StatisticsOfPlacedGB", PBSaver);
                
            }
            else{
                GameManager.Instance.ChangeScopeColor();
            }

        }

        
        //------Grapple Mode--------
        
        if (Input.GetMouseButtonDown(1)){
            StartGrapple();
            
        }
        if(other!=null&&!isStopped){
            grapplePoint= other.transform.position;
            joint.connectedAnchor = grapplePoint;
        }else{
            StopGrapple();
        }
        if(Input.GetMouseButtonUp(1)){
            StopGrapple();

            UGCounter++;     
            UGSaver = PlayerPrefs.GetInt("StatisticsOfUsingGrappler");
            UGSaver++;

            PlayerPrefs.SetInt("StatisticsOfUsingGrappler", UGSaver);
        }

        
    }

    void LateUpdate(){
        DrawRope();
    }

    void StartGrapple(){

        if(Physics.Raycast(camera.position,camera.forward,out hit,maxDistance,whatIsGrappleable)){
            other = hit.transform.gameObject;
            hit.transform.parent = other.transform;

            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            
            isStopped=false;

            float distanceFromPoint = Vector3.Distance(player.position,grapplePoint);

            //The distance grapple will try to keep from grapple point!
            joint.maxDistance = distanceFromPoint*0.75f;
            joint.minDistance = distanceFromPoint*0.25f;

            //can change
            joint.spring = 10f;//4.5f
            joint.damper = 0f;//7
            joint.massScale = 2f;//4.5f

            lr.positionCount = 2;

        }else{
            GameManager.Instance.ChangeScopeColor();
        }
    }


    void DrawRope(){
        //dont draw, if not grappling
        if(!joint) return;
        lr.SetPosition(0,gunTip.position);
        lr.SetPosition(1,grapplePoint);
    }

    void StopGrapple(){
        isStopped=true;
        lr.positionCount=0;
        Destroy(joint);
    }
    

}
