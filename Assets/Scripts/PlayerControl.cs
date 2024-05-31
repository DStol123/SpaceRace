using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody rb;

    // The block of code below imports variables from the VehicleData and GameData scripts.
    // 　VehicleDataとGameDataのスクリプトをインポートする
    public VehicleMovementInfoSO defaultInitializer;
    public GameDataSO gameDataInitializer;
    private VehicleData vehicleInfo;
    private GameData gameInfo;

    // These will be input variables.
    // インプットの変数になる
    private float forwardInput;
    private float sideInput;
    private float verticalInput;
    private float mouseX;
    private float mouseY;
    private float roll;
    private Vector3 initialPosition = new Vector3();
    private Quaternion initialOrientation;
    private float gaugeMeter;
    private bool boosting;

    // This method assigns user inputs into the input variables.
    // プレーヤーのインプットを変数にする。
    private void CheckInput()
    {
        if (Time.time >= gameInfo.StartTime)
        {
            forwardInput = Input.GetAxis("Vertical");
            sideInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Jump"); //Make sure to set the negative button in the Unity input manager.
            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");
            CheckRoll();
            if(vehicleInfo.InertialDampenersOn) { StopMovement(); }
        }
    }

    // This method allows the vehicle to come to a stop automatically
    // 自動に止める
    private void StopMovement()
    {
        if (forwardInput == 0f && Mathf.Sign(transform.InverseTransformDirection(rb.velocity).z) != 0)
        {
            forwardInput = -1f * Mathf.Sign(transform.InverseTransformDirection(rb.velocity).z);
        }
        if (sideInput == 0f && Mathf.Sign(transform.InverseTransformDirection(rb.velocity).x) != 0)
        {
            sideInput = -1f * Mathf.Sign(transform.InverseTransformDirection(rb.velocity).x);
        }
        if (verticalInput == 0f && Mathf.Sign(transform.InverseTransformDirection(rb.velocity).y) != 0)
        {
            verticalInput = -1f * Mathf.Sign(transform.InverseTransformDirection(rb.velocity).y);
        }
    }
    //When this method is called, the character will go back to the start of the course.
    //Updateに ResetToStart();は発動したら、キャラがコースのスタートに戻る
    private void ResetToStart()
    {
        transform.position = initialPosition;
        rb.velocity = new Vector3(0f, 0f, 0f);
        rb.transform.rotation = initialOrientation;
        Debug.Log("Vehicle Crashed. Reset to start. 衝突、リセットしました。");
    }

    //When this method is called, the character will go back to the checkpoint
    private void ResetToCheckpoint()
    {
        Checkpoint CheckPoint;
        GameObject obj = GameObject.Find("space-cart-1");
        CheckPoint = obj.GetComponent<Checkpoint>();
        transform.position = CheckPoint.checkpoint;
        rb.velocity = new Vector3(0f, 0f, 0f);
        rb.transform.rotation = initialOrientation;
        Debug.Log("Vehicle Crashed. Reset to checkpoint. 衝突、リセットしました。");
    }
    //When this method is called in update, the character's forward speed will increase
    //発動したらキャラの前の速度が上がる
    private void CheckBoost()
    {
        if(Input.GetKey("f") && gaugeMeter >= vehicleInfo.GaugeCapacity && Time.time >= gameInfo.StartTime)
        {
            boosting = true;
        }
    }

    private void Boost() 
    {
        if(boosting)
        { 
            rb.AddRelativeForce(new Vector3(0, 0, vehicleInfo.BoostSpeed * Time.deltaTime));
            gaugeMeter = gaugeMeter - vehicleInfo.BoostConsumption * Time.deltaTime;
            Debug.Log("Boosting. ブースト中");
            if(gaugeMeter <= 0)
            { 
                gaugeMeter = 0;
                boosting = false;
            }
        }
    }

    // This method gradually increases the boost charge
    // 待ってたらブーストがまた使えるようになる
    private void RegenerateGuage() 
    {
        if(gaugeMeter < vehicleInfo.GaugeCapacity && !boosting)
        {
            gaugeMeter = gaugeMeter + vehicleInfo.GaugeFillSpeed * Time.deltaTime;
            Debug.Log("Boost: " + gaugeMeter + "/" + vehicleInfo.GaugeCapacity);
        }
        else if(gaugeMeter >= vehicleInfo.GaugeCapacity && !boosting)
        {
            gaugeMeter = vehicleInfo.GaugeCapacity;
            Debug.Log("ブースト可能！");
        }
    }

    // This method creates a roll axis.
    // ローリングのインプットを読む
    private void CheckRoll()
    {
        if (Input.GetKey("q"))
        {
            roll = 1f;
        }
        else if (Input.GetKey("e"))
        {
            roll = -1f;
        }
        else
        {
            roll = 0;
        }
    }

    // This method transforms the input variables into ingame movement.
    // プレーヤーのインプットを動きにする
    private void MovePlayer()
    {
        CheckInput();
        float x = sideInput * vehicleInfo.LateralThrust;
        float y = verticalInput * vehicleInfo.LateralThrust;
        float z = forwardInput * vehicleInfo.ForwardThrust;
        float rotX = mouseY * vehicleInfo.RotationSpeed;
        float rotY = mouseX * vehicleInfo.RotationSpeed;
        float rotZ = roll * vehicleInfo.RollSpeed;
        rb.AddRelativeForce(new Vector3(x, y, z) * Time.deltaTime / vehicleInfo.AccelerationDebuff); //Add acceleration. 加速つける
        transform.localRotation = transform.localRotation * Quaternion.Euler(new Vector3(-rotX, rotY, rotZ) * Time.deltaTime / vehicleInfo.AccelerationDebuff); //Add rotation. 回る
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > vehicleInfo.DamageResistance)
        {
            ResetToCheckpoint();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Sets the object the script is attached to as the rigidbody in this script

        // Hides the cursor and puts it in the middle of the screen.　
        // カーソルを見えないようにして、画面の中央に届く。
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Imports data from VehicleData and GameData DO NOT MODIFY
        //　VehicleDataとGameDataを読めるようにする。変更しないでください
        vehicleInfo = new VehicleData(defaultInitializer);
        gameInfo = new GameData(gameDataInitializer);
        
        initialPosition = transform.position;
        initialOrientation = rb.transform.rotation;
        gaugeMeter = vehicleInfo.GaugeCapacity;
        boosting = false;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        CheckBoost();
        Boost();
        RegenerateGuage();
        
    }
}
