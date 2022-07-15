using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using DG.Tweening;


public class PlayerInLobb : MonoBehaviour
{

    //private SaveSystem sav = new SaveSystem();

    //public PlayerConfiguration config;
    public Transform camera;
    public LayerMask isItComputerToCore,isItComputerToPortal,isItComputerToMenu,isItLevelChanger,level1,level2,level3;//isItComputerToTest
    public ParticleSystem portalParticle,portalParticle2,mapManagerParticleON,mapManagerParticleOFF;
    public ParticleSystem[] coreParticles;
    public GameObject pointLightForPortal,triggerForPortal, planets, portalForPortal;
    //public Image testImage;
    private Collider col;
    private Color mapOFFParticleColor;
    private int changerLevelValue;
    private bool isCoreUse,isPortalUse,isSetUpWay,isLevelChangerUse;

    // Start is called before the first frame update
    void Start()
    {
        isCoreUse = false;
        isPortalUse = false;
        isSetUpWay = false;
        isLevelChangerUse = false;

        mapOFFParticleColor = mapManagerParticleOFF.startColor;
        //testImage.DOFade(255f,2f).From();
        if(PlayerPrefs.GetInt("FirstGame")==1){
            
        }
        //TestLocationMessageManager.yesOrNo =false;
         
    }

    // Update is called once per frame
    void Update()
    {

        /* <--------------------------------------------------------------------------SAVE/LOAD
        if(Input.GetKeyDown(KeyCode.P)){
            sav.SavePosition(this);
            Debug.Log("SAVE DATA");
        }
        if(Input.GetKeyDown(KeyCode.L)&&PlayerPrefs.HasKey("PosX")&&PlayerPrefs.HasKey("PosY")&&PlayerPrefs.HasKey("PosZ")){
            transform.position = sav.LoadPosition();
            Debug.Log("LOAD DATA");
        }
        */



        /*
        RaycastHit hitN;
        if (Physics.Raycast(camera.position, camera.forward, out hitN, config.range)){
            if(isPortalUse) CheckerOnLayerMask(hitN,isItComputerToPortal);
            CheckerOnLayerMask(hitN,isItLevelChanger);
            CheckerOnLayerMask(hitN,level1);
            CheckerOnLayerMask(hitN,level2);
            CheckerOnLayerMask(hitN,level3);
        }
        else{
            GameManager.Instance.OFFObject(eToUse);
        }
        */
        if(Input.GetKeyDown(KeyCode.E)){

            //isSetUpWay = TestLocationMessageManager.yesOrNo;
            RaycastHit hitN;
            /*
            if (Physics.Raycast(camera.position, camera.forward, out hitN, config.range,isItComputerToCore)&&!isCoreUse){
                GameManager.Instance.ONParticleSystem(coreParticle);
                isCoreUse = true;
            }
            else if(Physics.Raycast(camera.position, camera.forward, out hitN, config.range,isItComputerToCore)&&isCoreUse){
                GameManager.Instance.OFFParticleSystem(coreParticle);
                isCoreUse =false;
            }else */
            if (Physics.Raycast(camera.position, camera.forward, out hitN, PlayerCharacteristics.range,isItComputerToMenu)&&!isPortalUse){
                GameManager.Instance.OnMessage();
            }
            if (Physics.Raycast(camera.position, camera.forward, out hitN, PlayerCharacteristics.range,isItComputerToPortal)&&!isPortalUse&&isSetUpWay){
                
                GameManager.Instance.ONParticleSystem(portalParticle);
                GameManager.Instance.ONParticleSystem(portalParticle2);
                GameManager.Instance.ONObject(pointLightForPortal);
                GameManager.Instance.ONObject(triggerForPortal);
                //GameManager.Instance.OFFObject(eToUse);

                col = triggerForPortal.GetComponent<Collider>();

                isPortalUse = true;
            }
            else if(Physics.Raycast(camera.position, camera.forward, out hitN, PlayerCharacteristics.range,isItComputerToPortal)&&isPortalUse){
                GameManager.Instance.OFFParticleSystem(portalParticle);
                GameManager.Instance.OFFParticleSystem(portalParticle2);
                GameManager.Instance.OFFObject(pointLightForPortal);
                GameManager.Instance.OFFObject(triggerForPortal);
                //GameManager.Instance.OFFObject(eToUse);
                isPortalUse =false;
            }else if (Physics.Raycast(camera.position, camera.forward, out hitN, PlayerCharacteristics.range,isItLevelChanger)&&!isLevelChangerUse){
                
                GameManager.Instance.ONObject(planets);
                mapManagerParticleON.Play();
                //GameManager.Instance.OFFObject(eToUse);

                isLevelChangerUse = true;
            }
            else if(Physics.Raycast(camera.position, camera.forward, out hitN, PlayerCharacteristics.range,isItLevelChanger)&&isLevelChangerUse){
                
                GameManager.Instance.OFFObject(planets);
                mapManagerParticleOFF.startColor = mapOFFParticleColor;
                mapManagerParticleOFF.Play();
                
                //GameManager.Instance.OFFObject(eToUse);

                isLevelChangerUse = false;
            }
            else if((Physics.Raycast(camera.position, camera.forward, out hitN, PlayerCharacteristics.range,level1))){
                
                changerLevelValue = 4;//временно
                portalForPortal.tag = "ItCanUse";
                PortalReactiveOnMapManager(hitN);              

            }else if((Physics.Raycast(camera.position, camera.forward, out hitN, PlayerCharacteristics.range,level2))){
                changerLevelValue = 2;//временно
                portalForPortal.tag = "ItCanUse";
                PortalReactiveOnMapManager(hitN);

            }else if((Physics.Raycast(camera.position, camera.forward, out hitN, PlayerCharacteristics.range,level3))){
                changerLevelValue = 3;//временно
                portalForPortal.tag = "ItCanUse";
                PortalReactiveOnMapManager(hitN);
            }
            else{
                GameManager.Instance.ChangeScopeColor();
            }
        }

    }

    private void OnTriggerEnter(Collider other){
        if(other == col){
            GameManager.Instance.SceneLoad(changerLevelValue);
        }
    }

    private void PortalReactiveOnMapManager(RaycastHit hit){
        GameObject hitObject = hit.transform.gameObject;
        Color col = hitObject.GetComponent<Renderer> ().material.color;
        portalParticle.startColor = col;
        portalParticle2.startColor = col;
        mapManagerParticleOFF.startColor = col;
        pointLightForPortal.GetComponent<Light>().color = col;


        for(int i=0; i<coreParticles.Length;i++){
            coreParticles[i].startColor = col; 
            coreParticles[i].Play();

        }

        GameManager.Instance.OnPortIsReadyText();
        GameManager.Instance.OFFObject(planets);
        mapManagerParticleOFF.Play();
        //GameManager.Instance.OFFObject(eToUse);

        isLevelChangerUse = false;
        isSetUpWay = true;
    }
    /*
    private void CheckerOnLayerMask(RaycastHit hit,LayerMask lm){
        if (Physics.Raycast(camera.position, camera.forward, out hit, PlayerCharacteristics.range,lm)){
            GameManager.Instance.ONObject(eToUse);
        }
    }*/
}

/*
else if (Physics.Raycast(camera.position, camera.forward, out hitN, config.range,isItComputerToTest)&&!isSetUpWay){
                GameManager.Instance.OnMessageAboutGoToTestLocation();
                
            }
*/