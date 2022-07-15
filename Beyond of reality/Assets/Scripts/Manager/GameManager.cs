using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using DG.Tweening;

public class GameManager : SingleTone<GameManager>
{
    //public PlayerConfiguration config;

    //public PlayerConfiguration status;
    public Image forceBar, O2Bar, superJumpBar,scope,firstWeaponIcon,secondWeaponIcon;

    public GameObject hitPanel,firstPowerPanel,addO2Panel,powerBar, shop,finishIcon,containerForBonusText,containerForQuestElem,portIsReady, boneIsBrokenImage,message,btnGoToHub;//forceBar=powerBar    GoToTestMessage finishMessage

    public Text O2CylendersCounts,argonCounts ,balance, plusCreditsText,plusQuestElemText,jumpForceUpgradeText,speedUpgradeText,finishMessageText,timerText,MSText,JText,PGBText,UGText,UPText,UGGText,UO2Text,TFDText,SBText;
    
    private bool isBonusActive;
    private int DCounter, DSaver;

    
    ////////// TestJSON
    
    JSONBaseStats baseStats = new JSONBaseStats();
    private string path;

    
    //TestMyClass mySave = new TestMyClass();
    //private string path;

    //Test JSON
        /*
        path = Path.Combine("B:\\Oleg/Unity/Beyond of reality/Assets/BaseStats.json");//Application.persistentDataPath + "/Save.json"

        if (File.Exists(path))
        {
            mySave = JsonUtility.FromJson<TestMyClass>(File.ReadAllText(path));
        }

        mySave.justAName = i + "Haloz" + i;
        
        mySave.justALevel = Random.RandomRange(1,20);*/
    ///////
    
    


    
     void Awake()
    {
        /*if(Instance!=this){
            Destroy(gameObject); // реализация неуничтожаемого объекта (Нужен SingleTone)
        }

        DontDestroyOnLoad(gameObject);*/

        //config = Resources.Load("PlayerConfig") as PlayerConfiguration;
        //status = Resources.Load("PlayerStatus") as PlayerConfiguration;

        
    }

    private void Start()
    {
        /*
        path = Path.Combine(Application.persistentDataPath + "/BaseStats.json");//Application.persistentDataPath + "/BaseStats.json"

        if (File.Exists(path))
        {
            baseStats= JsonUtility.FromJson<JSONBaseStats>(File.ReadAllText(path));
        }*/
        

        PlayerCharacteristics.balance = 100f;//baseStats.balance;//100config.balance;
        PlayerCharacteristics.superJump = 0f;//baseStats.superJumpBar;//0config.superJumpBar;
        PlayerCharacteristics.currentO2 = 100f;//baseStats.playerO2Health;//100config.playerO2Health;
        PlayerCharacteristics.O2Cylenders = 3;//baseStats.O2Cylenders;//3config.O2Cylenders;
        PlayerCharacteristics.Crystals = 0;//baseStats.CrystalsOfArgon;//0config.CrystalsOfArgon;
        PlayerCharacteristics.bonusOfJump = 2f;//baseStats.bonusOfSuperJump;//2config.bonusOfSuperJump;
        PlayerCharacteristics.range = 5f;//baseStats.range;//5config.range;

        PlayerMovement.jumpForce = 7f;//baseStats.jumpFrc;//config.jumpFrc;
        PlayerMovement.moveSpeed = 7f;//baseStats.speed;//config.speed;
        PlayerMovement.rotateSpeed = 1f;//baseStats.rotateSpeedHor;//config.rotateSpeedHor;


        Time.timeScale = 1;

        UpdateJumpForce(PlayerMovement.jumpForce+0.5f);
        UpdateSpeed(PlayerMovement.moveSpeed+0.5f);

        UpdateCylenders(PlayerCharacteristics.O2Cylenders);// 
        UpdateArgons(PlayerCharacteristics.Crystals); 


        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        shop.SetActive(false);
        boneIsBrokenImage.SetActive(false);

        isBonusActive = false;

        

        
        

    }

    
    
    public void UpdateJumpForce(float jumpFrc){
        jumpForceUpgradeText.text = "Upgrade Jump to " + jumpFrc.ToString()+ "\n\n100 credits";
    }
    public void UpdateSpeed(float speed){
        speedUpgradeText.text = "Upgrade Speed to " + speed.ToString()+ "\n\n150 credits";
    }
    public void UpdateArgons(int argons){
        argonCounts.text = "Argon: " + argons.ToString();
    }

    public void UpdateScore(float blc)
    {
        balance.text = "Credits: " + blc.ToString(); 
    }
    public void UpdateCylenders(float O2cld)
    {
        O2CylendersCounts.text = "O2: " + O2cld.ToString(); 
    }
    public void UpdateHealthBar(float hpPlayer)
    {
        O2Bar.fillAmount = hpPlayer / 100; 
    }
    public void UpdateSuperJumpBar(float jumpPower,bool isActive)
    {
        superJumpBar.fillAmount = jumpPower; 
        firstPowerPanel.SetActive(isActive);
    }

    public void UpdateForceOfWeapon(float power)
    {
        forceBar.fillAmount = power; 
    }

    public void StopTimerAndWriteTime(string m, string s){
        timerText.text = m+","+s;
    }

    /*
    public void OnMessageAboutFinish(){
        finishMessage.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
    }
    public void OffMessageAboutFinish(){
        finishMessage.SetActive(false);
        finishIcon.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }*/
    
    public void OnMessage(){
        message.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
    }
    public void OffMessage(){
        message.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }
    

    public void OnPortIsReadyText(){
        portIsReady.SetActive(true);
    }
    public void OffPortIsReadyText(){
        portIsReady.SetActive(false);
    }


    public void OnShop(){
        shop.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
    }
    public void OffShop(){
        shop.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }

    public void ONParticleSystem(ParticleSystem particle){
        particle.Play();
    }
    public void OFFParticleSystem(ParticleSystem particle){
        particle.Stop();
    }
    public void ONObject(GameObject go){  // можно объединить
        go.SetActive(true);
    }
    public void OFFObject(GameObject go){
        go.SetActive(false);
    }



    public void OnOrOffForceBar(int value){
        
        switch (value)
        {
          case 0:
              powerBar.SetActive(false);
              break;
          case 1:
              powerBar.SetActive(true);
              break;

        }
    }

    public void SwitchingWeaponIcon(int value){

        switch (value)
        {
          case 0:
              firstWeaponIcon.color = Color.white; //Color.White
              secondWeaponIcon.color = Color.gray;// Color.Gray
              break;
          case 1:
              firstWeaponIcon.color = Color.gray;
              secondWeaponIcon.color = Color.white;
              break;

        }
    }
    
    /*
    whiteClr = new Color(255,255,255);
    greyClr = new Color(145,145,145);
    */

    /*
    public void GoToMenu(){
        SceneManager.LoadScene(0);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    public void GoToTestLocation(){
        SceneManager.LoadScene(1);
    }
    public void GoToHUB(){
        SceneManager.LoadScene(2);
    }
    */
    public void SceneLoad(int value){
        LoadScreenManager.idLoadScene = value;
        SceneManager.LoadScene(1);
    }
    

    public void ChangeScopeColor()
    {
        StartCoroutine(ChangeColor());
    }

    public void DeadOrWinEffect(bool howFinish)
    {
        StartCoroutine(DeadOrWin(howFinish));
    }

    public void AddO2Effect()
    {
        StartCoroutine(AddO2());
    }



    public void SphereCreate(Vector3 pos,GameObject other)
    {
        StartCoroutine(SphereIndicator(pos,other));
    }
    public void GravZero(Rigidbody rb,GameObject obj)
    {
        StartCoroutine(ChangingWithGravityZero(rb,obj));
    }



    public void PlusQuestItem(int questItem){
        plusQuestElemText.text = "+ "+ questItem.ToString()+" quest item";
        StartCoroutine(AddQuestElem());
    }

    public void PlusCredits(float bonusCredits){
        plusCreditsText.text = "+ "+ bonusCredits.ToString()+" credits";
        StartCoroutine(AddCredits());
    }

    public void FallDamageIconInstatiate(){
        StartCoroutine(FallDamageIcon());
    }
    
    public void GoToHub(){
        SceneLoad(3);
    }




    IEnumerator AddO2(){
        UpdateCylenders(PlayerCharacteristics.O2Cylenders);

        addO2Panel.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        addO2Panel.SetActive(false);

    }
    
    IEnumerator AddQuestElem(){

        if(!isBonusActive){
            isBonusActive = true;
            containerForQuestElem.SetActive(true);

            yield return new WaitForSeconds(1f);

            containerForQuestElem.SetActive(false);
            isBonusActive=false;
        }else{
            yield return new WaitForSeconds(1f);
        }
        
    }


    IEnumerator AddCredits(){

        if(!isBonusActive){
            isBonusActive = true;
            containerForBonusText.SetActive(true);

            yield return new WaitForSeconds(1f);

            containerForBonusText.SetActive(false);
            isBonusActive=false;
        }else{
            yield return new WaitForSeconds(1f);
        }
        
    }


    IEnumerator DeadOrWin(bool isWin) {

        //Time.timeScale = 0;
        /*
        for (int i=1;i<=3;i++){
        
        hitPanel.SetActive(true);
        yield return new WaitForSeconds(0.5f); //Первый вариант-нужно изменить панель!
        hitPanel.SetActive(false);
        }*/

        hitPanel.SetActive(true);
        btnGoToHub.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        MSText.text = "~"+PlayerMovement.maxSpeedCounter.ToString();
        JText.text = PlayerMovement.jumpsCounter.ToString();
        PGBText.text = RayCastShootMainWeapon.PBCounter.ToString();
        UGText.text = RayCastShootMainWeapon.UGCounter.ToString();
        UPText.text = PlayerCharacteristics.UPCounter.ToString();
        UGGText.text = RayCastShootSecondWeapon.GGCounter.ToString();
        UO2Text.text = PlayerCharacteristics.UOCounter.ToString();
        TFDText.text = PlayerMovement.FDCounter.ToString();
        SBText.text = ShopManager.SBCounter.ToString();

        if(isWin){
            finishMessageText.color = new Color (0f,242f,255f,255f);
      
            finishMessageText.text = "Y";
            yield return new WaitForSeconds(0.2f);
            finishMessageText.text = finishMessageText.text + "O";
            yield return new WaitForSeconds(0.2f);
            finishMessageText.text = finishMessageText.text + "U";
            yield return new WaitForSeconds(0.2f);
            finishMessageText.text = finishMessageText.text + " ";
            yield return new WaitForSeconds(0.2f); 
            finishMessageText.text = finishMessageText.text + "W";
            yield return new WaitForSeconds(0.2f);
            finishMessageText.text = finishMessageText.text + "O";
            yield return new WaitForSeconds(0.2f);
            finishMessageText.text = finishMessageText.text + "N";

        }else{
            finishMessageText.color = new Color (255f,0f,0f,255f);
        /*
        deadMessageText.text = "YOU DEAD";
        deadMessageText.color = new Color (255f,0f,0f,0f);//red with 0 alpha
        Color n = new Color (255f,0f,0f,255f);
        deadMessageText.DOColor(n,2f);*/

        
            finishMessageText.text = "Y";
            yield return new WaitForSeconds(0.2f);
            finishMessageText.text = finishMessageText.text + "O";
            yield return new WaitForSeconds(0.2f);
            finishMessageText.text = finishMessageText.text + "U";
            yield return new WaitForSeconds(0.2f);
            finishMessageText.text = finishMessageText.text + " ";
            yield return new WaitForSeconds(0.2f); 
            finishMessageText.text = finishMessageText.text + "L";
            yield return new WaitForSeconds(0.2f);
            finishMessageText.text = finishMessageText.text + "O";
            yield return new WaitForSeconds(0.2f);
            finishMessageText.text = finishMessageText.text + "S";
            yield return new WaitForSeconds(0.2f);
            finishMessageText.text = finishMessageText.text + "T";


            DCounter++;     
            DSaver = PlayerPrefs.GetInt("StatisticsOfDeath");
            DSaver++;

            PlayerPrefs.SetInt("StatisticsOfDeath", DSaver);

        }

        

        yield return new WaitForSeconds(3f);// пауза, временно. 
        
        // Cтатистика игры JText,PGBText,UGText,UPText,UGGText,UO2Text,TFDText,SBText
        btnGoToHub.SetActive(true);
        //hitPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        yield return new WaitForSeconds(20f);
            

        SceneLoad(3);

        
    }

    IEnumerator SphereIndicator(Vector3 pos,GameObject other){
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);//CreatePrimitive(PrimitiveType.Sphere)
        sphere.layer = LayerMask.NameToLayer("Ground");
        sphere.transform.position = pos;
        sphere.transform.parent = other.transform;

        yield return new WaitForSeconds(5f);

        Destroy(sphere);
    }

    IEnumerator ChangingWithGravityZero(Rigidbody rb,GameObject obj){

        if(obj.GetComponent<Renderer> ().material.color!=Color.green){ //HEX
            Color clr = obj.GetComponent<Renderer> ().material.color;
            obj.GetComponent<Renderer> ().material.color = Color.green;
            rb.useGravity = false;

            yield return new WaitForSeconds(5f);

            obj.GetComponent<Renderer> ().material.color = clr;
            rb.useGravity = true;
        }
        else
        {
            yield return new WaitForSeconds(0f);
        }

    }

    IEnumerator ChangeColor(){
            if(scope.color != Color.red){
            Color clr = scope.color;
            scope.color = Color.red;
            yield return new WaitForSeconds(0.3f);
            scope.color = clr;
        }else{
            yield return new WaitForSeconds(0f);
        }
    }

    IEnumerator FallDamageIcon(){

        boneIsBrokenImage.SetActive(true);
        yield return new WaitForSeconds(3f);
        boneIsBrokenImage.SetActive(false);


    }

}
