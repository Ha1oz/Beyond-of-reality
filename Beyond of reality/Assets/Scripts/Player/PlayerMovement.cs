using System.Collections;
using System.IO;
using UnityEngine;
//using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    //public PlayerConfiguration config;


    
    public static float jumpForce, moveSpeed,rotateSpeed;
    public static int jumpsCounter, jumpsSaver, FDCounter, FDSaver, maxSpeedCounter;
    //public Transform _cam;
    public Transform camera;
    public GameObject EToUsetxt;

    //private Animator anim;
    private Rigidbody body;

    private CapsuleCollider col;

    private float maxSpeed = 0f;


/*
    ////////// TestJSON
    TestMyClass mySave = new TestMyClass();
    private string path;*/
    

    private bool hitFallDamage;



    //private float normalSize = 4f;

    //private float normalSpeed,sprintSpeed;//crouchSpeed  Убрана реализация ускорения и приседания

   // private bool isCrouching,isAcelSpeed;
    
    void Start()
    {
        
        //anim = GetComponentInChildren<Animator>(); // get first child (id[0])

        
        body = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();

        hitFallDamage = false;

        jumpsCounter = 0;
        FDCounter = 0;
        //isCrouching = false;
        //isAcelSpeed = false;
        /*
        jumpForce = config.jumpFrc;
        moveSpeed = config.speed;
        rotateSpeed = config.rotateSpeedHor;
        */
        //normalSpeed = moveSpeed;
        //crouchSpeed = moveSpeed / 2;
        //sprintSpeed = moveSpeed * 1.5f;

        //hitFallDamage = false;
        //anim.SetBool("hitFallDamage",hitFallDamage);

        //Test JSON
        /*
        path = Path.Combine("B:\\Oleg/Unity/Beyond of reality/Assets/Save.json");//Application.persistentDataPath + "/Save.json"

        if (File.Exists(path))
        {
            mySave = JsonUtility.FromJson<TestMyClass>(File.ReadAllText(path));
        }*/
        ///////

    }

    
    void Update()
    {
        Move();
        Jump();
        Rotate();
        CanUse();
        CheckFallDamage();
        MaxSpeedCheck();
        
        /*
        if (Input.GetKey(KeyCode.V))
        {
            Debug.Log("Вертикальное ускорение: " + body.velocity.y);
            
        }*/
/*
        if (Input.GetKeyDown(KeyCode.P))
        {
            
            SaveAsJSON();  
            
        }
        
        if (Input.GetKeyDown(KeyCode.L))
        {
            string json = JsonUtility.ToJson(mySave);
            Debug.Log("Loading as JSON: " + json);
            
        }
        */
        
        


        
        ////////////////////////////////////////////////////////////////
        
        /*
        if (Input.GetKey(KeyCode.LeftControl)&& !isAcelSpeed)
        {
            Crouch();
        }
        else {

            col.height = normalSize;
            isCrouching = false;
            moveSpeed = normalSpeed;
        }
        */
    /*
        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching)
        {
            isAcelSpeed = true;
            moveSpeed = sprintSpeed;
        }
        else {
            isAcelSpeed = false;
            moveSpeed = moveSpeed/(sprintSpeed/moveSpeed);//1.5f
            
        }
        */
    }

    private void Move()
    {
        var hor = Input.GetAxisRaw("Horizontal");
        var vert = Input.GetAxisRaw("Vertical");

        Vector3 movHor = transform.right * hor;
        Vector3 movVert = transform.forward * vert;
        Vector3 velocity = (movHor + movVert).normalized * moveSpeed;

        body.MovePosition(transform.position + velocity * Time.fixedDeltaTime);
    }

    private void CanUse(){
        RaycastHit hit;

        if(Physics.Raycast(camera.position, camera.forward, out hit ,PlayerCharacteristics.range)){
            
            if(hit.transform.gameObject.CompareTag("ItCanUse")){
                EToUsetxt.SetActive(true);
            }else{
                EToUsetxt.SetActive(false);
            }
        }else{
                EToUsetxt.SetActive(false);
        }
    }

    private void Jump()
    {
        /*
        if (isCrouching) {
            return;
        }
        */

        if(Input.GetButtonDown("Jump") && Physics.Raycast(transform.position, Vector3.down, 2.4f) && !hitFallDamage)//2.1?
        {
            jumpsCounter++;     
            jumpsSaver = PlayerPrefs.GetInt("StatisticsOfJumps");
            jumpsSaver++;

            PlayerPrefs.SetInt("StatisticsOfJumps", jumpsSaver);
            

            body.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }      
    }

    private void Rotate()
    {
        var rotY = Input.GetAxis("Mouse X");
        Vector3 rotation = new Vector3(0f, rotY * rotateSpeed * PlayerPrefs.GetFloat("Sensitivity"), 0f);
        body.MoveRotation(body.rotation * Quaternion.Euler(rotation));
    }

    private void CheckFallDamage(){
        if(body.velocity.y <= -30){

            if(Physics.Raycast(transform.position, Vector3.down, 2.4f)&& !hitFallDamage){
               //Debug.Log("FALL DAMAGE!!!!");
                hitFallDamage = true;

                FDCounter++;     
                FDSaver = PlayerPrefs.GetInt("StatisticsOfTakingFallDamage");
                FDSaver++;

                PlayerPrefs.SetInt("StatisticsOfTakingFallDamage", FDSaver);

               StartCoroutine(FallDamage());              
               GameManager.Instance.FallDamageIconInstatiate();
            }

        }
    }

    /*
    private void Crouch() {
        isCrouching = true;
        col.height = normalSize / 2;
        moveSpeed = crouchSpeed;
    }
    *//*
    private void SaveAsJSON(){
    // что-то сохраняем
        int i = Random.RandomRange(1,1000);

        mySave.justAName = i + "Haloz" + i;
        
        mySave.justALevel = Random.RandomRange(1,20);

        string json = JsonUtility.ToJson(mySave);

        File.WriteAllText(path, JsonUtility.ToJson(mySave));

        Debug.Log("Saving as JSON: " + json);
    }
*/
    IEnumerator FallDamage(){

        moveSpeed /= 2; 
        body.mass *= 5;
        PlayerCharacteristics.currentO2 -= 20f; 
        //Debug.Log("Current02 = "+ PlayerCharacteristics.currentO2);
        yield return new WaitForSeconds(3f);
        moveSpeed *= 2;
        body.mass /= 5;
        hitFallDamage = false; 


    }
    private void MaxSpeedCheck(){
        if(body.velocity.x > maxSpeed){
            maxSpeed = body.velocity.x;
            maxSpeedCounter = Mathf.RoundToInt(maxSpeed);
            if (maxSpeedCounter > PlayerPrefs.GetInt("StatisticsOfMaxSpeed")){
                PlayerPrefs.SetInt("StatisticsOfMaxSpeed", maxSpeedCounter);
            }
        }
    }
}
