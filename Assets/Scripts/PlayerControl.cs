using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
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

    
    private void StopMovement()
    {
        // This method allows the vehicle to come to a stop automatically
        // 自動に止める
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

    // These variables store the initial position information
    private Vector3 initialPosition = new Vector3();
    private Quaternion initialOrientation;
    private Vector3 checkpointPosition = new Vector3();
    private Quaternion checkpointOrientation;

    // These variables deal with checkpoints and resetting
    private float timeFromDeath;
    private bool isAlive;

    private void ResetToStart()
    {
        //When this method is called, the character will go back to the start of the course.
        //Updateに ResetToStart();は発動したら、キャラがコースのスタートに戻る
        transform.position = initialPosition;
        rb.velocity = new Vector3(0f, 0f, 0f);
        rb.transform.rotation = initialOrientation;
        isAlive = true;
    }

    private void ResetToCheckpoint()
    {
        //When this method is called, the character will go back to the previous checkpoint
        if (!isAlive)
        {
            if (timeFromDeath <= gameInfo.RespawnTime)
            {
                timeFromDeath += Time.deltaTime;
                Debug.Log("Time until respawn: " + (gameInfo.RespawnTime - timeFromDeath));
            }
            else if (timeFromDeath >= gameInfo.RespawnTime)
            {
                transform.position = checkpointPosition;
                rb.transform.rotation = initialOrientation;

                isAlive = true;
                rb.velocity = new Vector3(0f, 0f, 0f);
                timeFromDeath = 0f;
                boosting = false;
            }
            Debug.Log("Vehicle Crashed. Reset to checkpoint. 衝突、リセットしました。");
        }
    }

    // These variables deal with boosting
    private float gaugeMeter;
    private bool boosting;

    private void CheckBoost()
    {
    // This method checks if the boost key is being pressed, if it is, it will set boosting to true
        if(Input.GetKey("f") && gaugeMeter >= vehicleInfo.GaugeCapacity && Time.time >= gameInfo.StartTime)
        {
            boosting = true;
        }
    }

    private void Boost() 
    {
    // This method adds forward speed to a character
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

    private void RegenerateGuage() 
    {
    // This method gradually increases the boost charge
    // 待ってたらブーストがまた使えるようになる
        if (gaugeMeter < vehicleInfo.GaugeCapacity && !boosting)
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

    private void CheckRoll()
    {
        // This method creates a roll axis.
        // ローリングのインプットを読む
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
    
    private void MovePlayer()
    {
    // This method transforms the input variables into ingame movement.
    // プレーヤーのインプットを動きにする
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

    // This variable deals with air resistance
    private Vector3 airResistanceForce = new Vector3();

    private void AddAirResistanceForce()
    {
    // This slows down the character depending on the air density in the course
        airResistanceForce = rb.velocity * gameInfo.AirDensity;
        rb.AddForce(airResistanceForce * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
    // This method deals with all collisions
        if (collision.relativeVelocity.magnitude > vehicleInfo.DamageResistance && collision.gameObject.tag != "nitro")
        {
        // This calls the reset method if the character collides with something at high speeds
            isAlive = false;
            ResetToCheckpoint();
            Debug.Log("Vehicle Crashed. Reset to start. 衝突、リセットしました。");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("nitro"))
        {
            gaugeMeter = vehicleInfo.GaugeCapacity;
        }
        else if(other.gameObject.CompareTag("Bomb"))
        {
            isAlive = false;
            ResetToCheckpoint();
        }
        else if(other.gameObject.CompareTag("hit"))
        {
            checkpointPosition = transform.position;
            checkpointOrientation = rb.transform.rotation;
        }
        other.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Sets the object the script is attached to as the rigidbody in this script
        rb = GetComponent<Rigidbody>();

        // Hides the cursor and puts it in the middle of the screen.　
        // カーソルを見えないようにして、画面の中央に届く。
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Imports data from VehicleData and GameData DO NOT MODIFY
        //　VehicleDataとGameDataを読めるようにする。変更しないでください
        vehicleInfo = new VehicleData(defaultInitializer);
        gameInfo = new GameData(gameDataInitializer);

        // Sets the initial position and rotation data for the player
        initialPosition = transform.position;
        initialOrientation = rb.transform.rotation;
        checkpointPosition = initialPosition;
        checkpointOrientation = rb.transform.rotation;

        // Makes the boost gauge full, makes sure the player is not boosting automatically at start
        // and makes sure the player is alive
        gaugeMeter = vehicleInfo.GaugeCapacity;
        boosting = false;
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!PauseMenu.isPaused)
        {
            if (isAlive)
            {
                // The player should only move if they are alive
                MovePlayer();
                CheckBoost();
                Boost();
                RegenerateGuage();
              }
            ResetToCheckpoint();
            if (gameInfo.AirDensity > 0)
            {
                AddAirResistanceForce();
            }
        }
    }
}
