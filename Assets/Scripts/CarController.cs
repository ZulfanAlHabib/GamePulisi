using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Pengaturan Mobil")]
    public float speed = 15f;      // Kecepatan maju/mundur
    public float turnSpeed = 70f;  // Kecepatan belok

    [Header("Input Kontrol")]
    public Joystick joystick;      // Slot untuk memasukkan Joystick dari layar

    void Update()
    {
        // 1. Mengambil nilai input dari analog (nilainya selalu antara -1 sampai 1)
        float gasBrake = joystick.Vertical;   // Menggeser analog ke Atas/Bawah
        float steering = joystick.Horizontal; // Menggeser analog ke Kiri/Kanan

        // 2. Logika Maju & Mundur
        // Mobil bergerak searah sumbu Z (depan) dikali nilai gas dan kecepatan
        transform.Translate(Vector3.forward * gasBrake * speed * Time.deltaTime);

        // 3. Logika Belok
        // Mobil berputar di sumbu Y (atas) dikali nilai setir dan kecepatan belok.
        // Syarat: Mobil hanya bisa belok kalau sedang di-gas atau di-rem (gasBrake != 0).
        if (gasBrake != 0)
        {
            // Jika mundur (gasBrake < 0), arah belok harus dibalik agar realistis
            float arahBelok = gasBrake > 0 ? 1 : -1;
            transform.Rotate(Vector3.up * steering * arahBelok * turnSpeed * Time.deltaTime);
        }
    }
}