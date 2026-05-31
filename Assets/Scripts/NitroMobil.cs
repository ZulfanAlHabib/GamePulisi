using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class NitroMobil : MonoBehaviour
{
    [Header("Pengaturan Nitro")]
    public float kekuatanNitro = 5000f; // Seberapa kuat dorongan nitro
    public float maxEnergi = 100f;     // Total kapasitas kapasitas nitro
    public float kecepatanHabis = 25f; // Seberapa cepat energi berkurang saat dipakai
    public float kecepatanIsi = 10f;   // Seberapa cepat energi terisi kembali saat mati

    private Rigidbody rb;
    private float energiSekarang;
    private bool sedangNitro = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        energiSekarang = maxEnergi; // Mulai game dengan nitro penuh
    }

    void Update()
    {
        // Cek apakah tombol N ditekan DAN energi nitro masih ada
        if (Input.GetKey(KeyCode.N) && energiSekarang > 0)
        {
            sedangNitro = true;
        }
        else
        {
            sedangNitro = false;
        }

        // Logika pengurangan dan pengisian energi nitro
        if (sedangNitro)
        {
            energiSekarang -= kecepatanHabis * Time.deltaTime;
            // Batasi agar tidak minus
            energiSekarang = Mathf.Clamp(energiSekarang, 0f, maxEnergi); 
        }
        else
        {
            energiSekarang += kecepatanIsi * Time.deltaTime;
            // Batasi agar tidak melebihi batas maksimal
            energiSekarang = Mathf.Clamp(energiSekarang, 0f, maxEnergi);
        }
    }

    void FixedUpdate()
    {
        // Penambahan kecepatan dilakukan di FixedUpdate karena menggunakan Fisika (Rigidbody)
        if (sedangNitro)
        {
            // Menambahkan gaya ke arah DEPAN bodi mobil (transform.forward)
            rb.AddForce(transform.forward * kekuatanNitro, ForceMode.Force);
        }
    }

    // Fungsi tambahan untuk membaca sisa nitro (akan berguna untuk UI/Slider nanti)
    public float AmbilPersentaseNitro()
    {
        return energiSekarang / maxEnergi;
    }
}