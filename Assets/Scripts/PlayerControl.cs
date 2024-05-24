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
            Debug.Log(forwardInput + sideInput + verticalInput + mouseX + mouseY);
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
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }
}
