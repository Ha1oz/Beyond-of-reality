using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacteristics : MonoBehaviour
{
    //public PlayerConfiguration config;
    
    public static float bonusOfJump, balance, superJump,currentO2,range;
    public static int O2Cylenders, Crystals;
    
    public static int UPCounter, UPSaver, UOCounter, UOSaver;
    //public float range;
    public LayerMask whatIsLoot,isItArgon;
    public Transform camera;

    private bool isActiveSuperJump,isActiveShop,isFull,isDead,IsWin;//isComplete

    private float startTime;

    void Start()
    {     /*
        Crystals=config.CrystalsOfArgon;
        balance = config.balance;//временно
        superJump = config.superJumpBar;
        O2Cylenders=config.O2Cylenders;
        bonusOfJump = config.bonusOfSuperJump;
        currentO2 = config.playerO2Health;
        */
        UPCounter = 0;
        UOCounter = 0;
        startTime = Time.time;
        //DCounter = 0;

        isDead = false;
        isFull =true;
        isActiveShop =false;
        IsWin = false;
        //isComplete = false;
        isActiveSuperJump = false;
        
        GameManager.Instance.UpdateScore(balance);
        GameManager.Instance.UpdateArgons(Crystals);
        GameManager.Instance.UpdateHealthBar(currentO2);
        GameManager.Instance.UpdateCylenders(O2Cylenders);
    }


    private void Update()
    {
        float t = Time.time - startTime;
        string min = ((int)t/60).ToString();
        string sec = (t%60).ToString("F2");
        //timerText.text = min+","+sec;

        currentO2 -= 1f*Time.deltaTime;

        if(!isActiveSuperJump){
            superJump += 0.01f*Time.deltaTime;
        }else{
            superJump -= 0.05f*Time.deltaTime;
        }

        GameManager.Instance.UpdateSuperJumpBar(superJump,isActiveSuperJump);
        GameManager.Instance.UpdateHealthBar(currentO2);

        if(currentO2<=0 && isFull){
            isFull = false;
            isDead= true;
            GameManager.Instance.StopTimerAndWriteTime(min,sec);
            Dead();
            
        }
        if(Crystals>=3&&!IsWin){// =
            IsWin = true;
            GameManager.Instance.DeadOrWinEffect(true);
            GameManager.Instance.StopTimerAndWriteTime(min,sec);
                    //isComplete=true;
        }

        if (currentO2>100){
             currentO2=100;   
        }


        if(superJump>1){
            superJump = 1;
        }else if(superJump<0){
            superJump=0;
            PlayerMovement.jumpForce /= bonusOfJump;
            isActiveSuperJump = false;           
        }

        if(!isDead&&gameObject.transform.position.y<=-100){
            isDead =true;
            GameManager.Instance.StopTimerAndWriteTime(min,sec);
            Dead();           
        }

        if(Input.GetKeyDown(KeyCode.Alpha1) && superJump == 1&&!isActiveShop){
            isActiveSuperJump = true;

            UPCounter++;     
            UPSaver = PlayerPrefs.GetInt("StatisticsOfUsingPower");
            UPSaver++;

            PlayerPrefs.SetInt("StatisticsOfUsingPower", UPSaver);

            //balance+=30;
            
            PlayerMovement.jumpForce *= bonusOfJump;
            //GameManager.Instance.PlusCredits(30f); //Поощрение за использование
            //GameManager.Instance.UpdateScore(balance);           
        }



        if(Input.GetKeyDown(KeyCode.H)&& currentO2!=0 && O2Cylenders>0 &&!isActiveShop&&!isActiveSuperJump&&!isDead){
            O2Cylenders--;
            currentO2 += 30;

            UOCounter++;     
            UOSaver = PlayerPrefs.GetInt("StatisticsOfUsingO2");
            UOSaver++;

            PlayerPrefs.SetInt("StatisticsOfUsingO2", UOSaver);
            /*
            balance+=10;
            GameManager.Instance.PlusCredits(10f);
            GameManager.Instance.UpdateScore(balance); // Поощрение за использование
            */
            GameManager.Instance.UpdateHealthBar(currentO2);
            GameManager.Instance.AddO2Effect();
        }


        if(Input.GetKeyDown(KeyCode.B)&&!isActiveShop&&!isActiveSuperJump){
            GameManager.Instance.OnShop();
            //gameObject.GetComponent<RayCastShootMainWeapon>().enabled = false;
            //gameObject.GetComponent<RayCastShootSecondWeapon>().enabled = false;
            isActiveShop = true;
        }else if(Input.GetKeyDown(KeyCode.B)&&isActiveShop){
            GameManager.Instance.OffShop();
            //gameObject.GetComponent<RayCastShootMainWeapon>().enabled = true;
            //gameObject.GetComponent<RayCastShootSecondWeapon>().enabled = true;
            isActiveShop = false;
        }

        
        if(Input.GetKeyDown(KeyCode.E)){
            
            RaycastHit hitN;
 
            if (Physics.Raycast(camera.position, camera.forward, out hitN, range,whatIsLoot)){
                GameObject hitObjectN = hitN.transform.gameObject;
                int rndBonus = Random.RandomRange(25,60);
                balance+=(float)rndBonus;
                GameManager.Instance.PlusCredits((float)rndBonus);
                GameManager.Instance.UpdateScore(balance);
                Destroy(hitObjectN);

            } else if(Physics.Raycast(camera.position, camera.forward, out hitN, range,isItArgon)){
                GameObject hitObjectN = hitN.transform.gameObject;
                Crystals++;

                GameManager.Instance.PlusQuestItem(1);
                GameManager.Instance.UpdateArgons(Crystals);
                Destroy(hitObjectN);

                
            }
            else{
                GameManager.Instance.ChangeScopeColor();
            }
        }
        /*if(Input.GetKeyDown(KeyCode.F)&&isComplete){
            GameManager.Instance.OnMessageAboutFinish();
        }*/
    }
    
    public void Dead(){
        //gameObject.GetComponent<PlayerMovement>().enabled = false;//
        //camera.GetComponent<CameraRotate>().enabled = false;// Плохо, что используется не в Manager?       
        GameManager.Instance.DeadOrWinEffect(false);       
    }

}
