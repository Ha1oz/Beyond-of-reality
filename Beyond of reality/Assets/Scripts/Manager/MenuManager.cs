using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{
    //public PlayerConfiguration config;
    //public PlayerConfiguration status;
    public GameObject instructionOfPlay,settingsMenu,insParticle;
    public Slider sensSlider, fovSlider;
    public Text sensTxt, fovTxt, MaxSpeedtxt, ColvoJumpstxt, ColvoGBtxt, ColvoUGtxt, ColvoUPtxt, ColvoGGtxt,ColvoO2txt,ColvoFDtxt,ColvoDtxt,ColvoSBtxt;
    
    /*private void Awake(){
        config = Resources.Load("PlayerConfig") as PlayerConfiguration;
    }*/

    private void Start(){
        
        /*
        status.balance = config.balance;
        status.superJumpBar = config.superJumpBar;
        status.range = config.range;
        status.bonusOfSuperJump = config.bonusOfSuperJump;
        status.jumpFrc = config.jumpFrc;
        status.speed = config.speed;
        status.playerO2Health = config.playerO2Health;
        status.rotateSpeedHor = config.rotateSpeedHor;
        status.O2Cylenders = config.O2Cylenders;
        status.CrystalsOfArgon = config.CrystalsOfArgon;
        */
        //testImage.color = new Color(255f,255f,255f,Mathf.Lerp(255f,0f,0.5f));
        //float lerp = Mathf.PingPong(Time.time, 0.5f) / 0.5f; //textColor.a = Mathf.Lerp(0.0f, 1.0f,0.1f);
        //textColor.a = Mathf.Lerp(0.0f, 1.0f,0.1f);
        //testImage.color = textColor;


        //PlayerPrefs.SetFloat("Sensitivity", sensSlider);
        if(!PlayerPrefs.HasKey("FirstGame")){
            PlayerPrefs.SetInt("FirstGame", 1);//1 - yes, 0 - no
        }
        
        if(!PlayerPrefs.HasKey("Sensitivity")){
            PlayerPrefs.SetFloat("Sensitivity", 1);
        }
        if(!PlayerPrefs.HasKey("FOV")){
            PlayerPrefs.SetFloat("FOV", 60);
        }
        if(!PlayerPrefs.HasKey("StatisticsOfMaxSpeed")){
            PlayerPrefs.SetInt("StatisticsOfMaxSpeed", 0);
        }
        if(!PlayerPrefs.HasKey("StatisticsOfJumps")){
            PlayerPrefs.SetInt("StatisticsOfJumps", 0);
        }
        if(!PlayerPrefs.HasKey("StatisticsOfPlacedGB")){
            PlayerPrefs.SetInt("StatisticsOfPlacedGB", 0);
        }
        if(!PlayerPrefs.HasKey("StatisticsOfUsingGrappler")){
            PlayerPrefs.SetInt("StatisticsOfUsingGrappler", 0);
        }
        if(!PlayerPrefs.HasKey("StatisticsOfUsingPower")){
            PlayerPrefs.SetInt("StatisticsOfUsingPower", 0);
        }
        if(!PlayerPrefs.HasKey("StatisticsOfUsingGG")){
            PlayerPrefs.SetInt("StatisticsOfUsingGG", 0);
        }
        if(!PlayerPrefs.HasKey("StatisticsOfUsingO2")){
            PlayerPrefs.SetInt("StatisticsOfUsingO2", 0);
        }
        if(!PlayerPrefs.HasKey("StatisticsOfTakingFallDamage")){
            PlayerPrefs.SetInt("StatisticsOfTakingFallDamage", 0);
        }
        if(!PlayerPrefs.HasKey("StatisticsOfDeath")){
            PlayerPrefs.SetInt("StatisticsOfDeath", 0);
        }
        if(!PlayerPrefs.HasKey("StatisticsOfSpentBalance")){
            PlayerPrefs.SetInt("StatisticsOfSpentBalance", 0);
        }
        



        sensSlider.value = PlayerPrefs.GetFloat("Sensitivity");
        sensTxt.text = PlayerPrefs.GetFloat("Sensitivity").ToString();

        fovSlider.value = PlayerPrefs.GetFloat("FOV");
        fovTxt.text = PlayerPrefs.GetFloat("FOV").ToString();

        MaxSpeedtxt.text = "~"+PlayerPrefs.GetInt("StatisticsOfMaxSpeed").ToString();
        ColvoJumpstxt.text = PlayerPrefs.GetInt("StatisticsOfJumps").ToString();
        ColvoGBtxt.text = PlayerPrefs.GetInt("StatisticsOfPlacedGB").ToString();
        ColvoUGtxt.text = PlayerPrefs.GetInt("StatisticsOfUsingGrappler").ToString();
        ColvoUPtxt.text = PlayerPrefs.GetInt("StatisticsOfUsingPower").ToString();
        ColvoGGtxt.text = PlayerPrefs.GetInt("StatisticsOfUsingGG").ToString();
        ColvoO2txt.text = PlayerPrefs.GetInt("StatisticsOfUsingO2").ToString();
        ColvoFDtxt.text = PlayerPrefs.GetInt("StatisticsOfTakingFallDamage").ToString();
        ColvoDtxt.text = PlayerPrefs.GetInt("StatisticsOfDeath").ToString();
        ColvoSBtxt.text = PlayerPrefs.GetInt("StatisticsOfSpentBalance").ToString();

        //testText.DOFade(255f,1f);


        //Debug.Log("SENSA: "+ sensSlider.value);
        //Debug.Log("FOV: "+ fovSlider.value);
        //sensSlider.Set(PlayerPrefs.GetFloat("Sensitivity"));
        //fovSlider.Set(PlayerPrefs.GetFloat("FOV"));

        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        OFFInstruction();
        settingsMenu.SetActive(false);

        

        
    }
    private void Update() {
        //textColor = Color.Lerp(Color.white, Color.black, );//Mathf.Lerp(0.0f, 255f,0.001f); //Mathf.PingPong(Time.time, 1);

    }

    public void NewGame() {
        
        LoadScreenManager.idLoadScene = 2;
        SceneManager.LoadScene(1);
        

    }

    public void ONInstruction() {

        instructionOfPlay.SetActive(true);

    }

    public void OFFInstruction() {

        instructionOfPlay.SetActive(false);

    }


    public void ExitGame() {

        Application.Quit();

    }

    public void GoToLobby(){
        LoadScreenManager.idLoadScene = 3;
        SceneManager.LoadScene(1);
    }

    public void OpenSettings(){
        settingsMenu.SetActive(true);
    }

    public void CloseAndSaveSettings(){
        settingsMenu.SetActive(false);

    }

    public void ChangeSens(){
        PlayerPrefs.SetFloat("Sensitivity",sensSlider.value);
        sensTxt.text = PlayerPrefs.GetFloat("Sensitivity").ToString();

        //Debug.Log("SENSA: "+ PlayerPrefs.GetFloat("Sensitivity"));
    }
    
    public void ChangeFOV(){
        PlayerPrefs.SetFloat("FOV",fovSlider.value);
        fovTxt.text = PlayerPrefs.GetFloat("FOV").ToString();

        //Debug.Log("FOV: "+ PlayerPrefs.GetFloat("FOV"));
    }




}
