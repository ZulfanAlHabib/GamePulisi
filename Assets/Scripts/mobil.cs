using UnityEngine;

public class mobil : MonoBehaviour
{
    [Header("Mobile Input Settings")]
    public Joystick joystick; // Kolom untuk memasukkan analog dari Canvas
    private bool isBraking;   // Status rem (dikontrol lewat tombol UI nanti)

    private float horizontalInput, verticalInput;
    private float currentSteerAngle, currentBrakeForce;

    // Settings
    [Header("Car Settings")]
    [SerializeField] private float motorForce = 1500f;
    [SerializeField] private float brakeForce = 3000f;
    [SerializeField] private float maxSteerAngle = 30f;

    // Wheel Colliders
    [Header("Wheel Colliders")]
    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    // Wheel Transforms
    [Header("Wheel Transforms")]
    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void GetInput()
    {
        // 1. Cek apakah Joystick sudah dipasang di Inspector
        if (joystick != null)
        {
            // Steering (Kanan/Kiri)
            horizontalInput = joystick.Horizontal;

            // Gas / Mundur (Atas/Bawah)
            verticalInput = joystick.Vertical;
        }
        else
        {
            // Fallback: Kalau Joystick belum dipasang, tetap bisa pakai Keyboard untuk testing di Laptop
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
            
            // Rem pakai spasi hanya berlaku kalau lagi main di PC/Laptop
            if (Input.GetKey(KeyCode.Space)) 
            {
                isBraking = true;
            } 
            else if (!Input.GetKey(KeyCode.Space) && joystick == null) 
            {
                // Bagian ini sengaja dikosongkan agar rem UI tidak bertabrakan dengan keyboard
            }
        }
    }

    private void HandleMotor()
    {
        if (isBraking)
        {
            // Matikan tenaga mesin saat rem
            rearLeftWheelCollider.motorTorque = 0f;
            rearRightWheelCollider.motorTorque = 0f;

            currentBrakeForce = brakeForce;
        }
        else
        {
            // RWD = roda belakang penggerak
            rearLeftWheelCollider.motorTorque = verticalInput * motorForce;
            rearRightWheelCollider.motorTorque = verticalInput * motorForce;

            currentBrakeForce = 0f;
        }

        ApplyBraking();
    }

    private void ApplyBraking()
    {
        frontLeftWheelCollider.brakeTorque = currentBrakeForce;
        frontRightWheelCollider.brakeTorque = currentBrakeForce;
        rearLeftWheelCollider.brakeTorque = currentBrakeForce;
        rearRightWheelCollider.brakeTorque = currentBrakeForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;

        // Steering hanya roda depan
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform, true);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform, false);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform, true);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform, false);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform, bool isLeftWheel)
    {
        Vector3 pos;
        Quaternion rot;

        wheelCollider.GetWorldPose(out pos, out rot);

        wheelTransform.position = pos;

        // Membalik rotasi roda kiri agar normal
        if (isLeftWheel)
        {
            wheelTransform.rotation = rot * Quaternion.Euler(0, 180, 0);
        }
        else
        {
            wheelTransform.rotation = rot;
        }
    }

    // --- FUNGSI BARU UNTUK TOMBOL REM DI LAYAR HP ---
    public void TekanRem()
    {
        isBraking = true;
    }

    public void LepasRem()
    {
        isBraking = false;
    }
}