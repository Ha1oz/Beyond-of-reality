using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RayCastShootSecondWeapon : MonoBehaviour
{

    public float damage, range, hitForce, fireRate;
    private float forceOfShoot;
    private bool isPowerReady;
    public static int GGCounter, GGSaver;

    [Space(10)]
    public Transform camera;

    //
    //public Image forceOfShootImage;
    //

    [Header("Effects")]
    public ParticleSystem shootEffect,shootEffect2;
    //public GameObject impactEffect;

    [Space(20)]

   // public Camera fpsCam;

    private float nextTimeToShoot = 0f;
    //private float forceOfShoot;




    private float startFOV;


    void Start()
    {
        isPowerReady = false;
        forceOfShoot = 0;
        GameManager.Instance.UpdateForceOfWeapon(forceOfShoot); 
    }


    void Update()
    {
        

        if(Input.GetMouseButton(0)){
            isPowerReady=true;
            forceOfShoot += 1f*Time.deltaTime;
            GameManager.Instance.UpdateForceOfWeapon(forceOfShoot); 
            
            if(forceOfShoot>1){
                forceOfShoot=1;
            }
            
        }
        if(Input.GetMouseButtonUp(0))
        {
            Shoot(forceOfShoot);
            isPowerReady=false;
            forceOfShoot=0;  
            GameManager.Instance.UpdateForceOfWeapon(forceOfShoot); 
        }

        if (Input.GetMouseButtonDown(1)&& Time.time >= nextTimeToShoot && isPowerReady==false){

            RaycastHit hitN;

            

            if (Physics.Raycast(camera.position,camera.forward, out hitN,range))
            {
                GameObject hitObjectN = hitN.transform.gameObject;


                Rigidbody rb = hitObjectN.GetComponent<Rigidbody>();
                
                if(rb != null){
                    shootEffect2.Play();
                    GameManager.Instance.GravZero(rb,hitObjectN);

                    GGCounter++;     
                    GGSaver = PlayerPrefs.GetInt("StatisticsOfUsingGG");
                    GGSaver++;

                    PlayerPrefs.SetInt("StatisticsOfUsingGG", GGSaver);
                }
        
            }
            else{
                GameManager.Instance.ChangeScopeColor();
            }
        }

    }


    private void Shoot(float bonusOfForce) {

        shootEffect.Play();

        RaycastHit hit;

        

        if (Physics.Raycast(camera.position,camera.forward, out hit, range)) {
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (rb != null && rb.constraints == RigidbodyConstraints.None) {
                rb.AddForce(-hit.normal * hitForce*bonusOfForce, ForceMode.Impulse);
            }

            //EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();

        }
        Debug.Log("Force Of Shoot = "+ bonusOfForce);

        //GameObject hitEffect = Instantiate(impactEffect,hit.point, Quaternion.LookRotation(hit.normal));

        //Destroy(hitEffect, 2f);

    } 
}
