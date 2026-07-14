using UnityEngine;

public class mobil : MonoBehaviour
{
    [Header("Mobile Input Settings")]
    public Stir steeringWheel; // Kolom untuk analog (Hanya untuk belok)
    public GameObject sirineObject; // Masukkan objek lampu sirine/suara di Inspector

    private float uiGasInput = 0f; // 1 untuk maju, -1 untuk mundur
    private bool isBraking;
    private bool isNosActive;

    private float horizontalInput, verticalInput;
    private float currentSteerAngle, currentBrakeForce;

    // Settings
    [Header("Car Settings")]
    [SerializeField] private float motorForce = 1500f;
    [SerializeField] private float nosMultiplier = 2f; // Kecepatan dikali 2 saat NOS
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
    // 1. STEERING (Kanan/Kiri): Mengambil dari Steering Wheel
    if (steeringWheel != null)
    {
        horizontalInput = steeringWheel.steeringValue; // Mengambil nilai dari script baru kita
    }
    else
    {
        // Fallback untuk testing di laptop
        horizontalInput = Input.GetAxis("Horizontal");
    }

    // 2. GAS (Maju/Mundur): Mengambil nilai dari tombol UI
    verticalInput = uiGasInput;

    // 3. FALLBACK KEYBOARD (Untuk testing di Laptop)
    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) verticalInput = 1f;
    if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) isBraking = true; 
    if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow)) isBraking = false;
    
    // Testing NOS dan Sirine
    if (Input.GetKeyDown(KeyCode.F)) ToggleSirine();
    if (Input.GetKeyDown(KeyCode.LeftShift)) ToggleNos();
}

    private void HandleMotor()
    {
        // Hitung total tenaga. Jika NOS aktif, kalikan motorForce dengan nosMultiplier
        float currentMotorForce = isNosActive ? (motorForce * nosMultiplier) : motorForce;

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
            rearLeftWheelCollider.motorTorque = verticalInput * currentMotorForce;
            rearRightWheelCollider.motorTorque = verticalInput * currentMotorForce;
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

        if (isLeftWheel)
        {
            wheelTransform.rotation = rot * Quaternion.Euler(0, 180, 0);
        }
        else
        {
            wheelTransform.rotation = rot;
        }
    }

    // ==========================================
    // FUNGSI UNTUK TOMBOL UI
    // ==========================================

    // GAS
    public void TekanGas() { uiGasInput = 1f; isBraking = false; }
    public void LepasGas() { uiGasInput = 0f; }

    // REM SEKALIGUS MUNDUR
    public void TekanRem() 
    { 
        uiGasInput = -1f; // Memberikan tenaga mundur (negatif)
        isBraking = false; 
    }
    
    public void LepasRem() 
    { 
        uiGasInput = 0f; // Berhenti mundur saat dilepas
    }

    // NOS (KODE DIPERBARUI MENJADI TOGGLE)
    public void ToggleNos() 
    { 
        if (Time.timeScale == 0f) return; // Kunci: Jika game stop/pause, jangan lakukan apa-apa
        isNosActive = !isNosActive; 
    }

    // SIRINE (Klik Sekali / Toggle)
    public void ToggleSirine()
    {
        if (Time.timeScale == 0f) return; // Kunci: Jika game stop/pause, jangan lakukan apa-apa
        
        if (sirineObject != null)
        {
            // Nyala-matikan objek sirine
            sirineObject.SetActive(!sirineObject.activeSelf);
        }
    }
}