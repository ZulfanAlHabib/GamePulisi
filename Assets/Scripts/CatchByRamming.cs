using UnityEngine;
using UnityEngine.SceneManagement;

public class CatchByRamming : MonoBehaviour
{
    [Header("Pengaturan Tabrakan")]
    [Tooltip("Berapa kali musuh harus ditabrak sampai hancur/berhenti")]
    public int nyawaMusuh = 3; 
    
    [Tooltip("Kecepatan minimal tabrakan agar dihitung sebagai damage")]
    public float minimalKerasTabrakan = 5f; 

    [Header("Level Selanjutnya")]
    public string nextLevelName;

    // Fungsi bawaan Unity yang otomatis terpanggil saat terjadi benturan fisik
    void OnCollisionEnter(Collision collision)
    {
        // 1. Pastikan yang menabrak adalah pemain (menggunakan Tag "Player")
        if (collision.gameObject.CompareTag("Player"))
        {
            // 2. Hitung seberapa keras tabrakannya (berdasarkan kecepatan relatif)
            float kerasTabrakan = collision.relativeVelocity.magnitude;

            if (kerasTabrakan >= minimalKerasTabrakan)
            {
                nyawaMusuh--; // Kurangi nyawa musuh 1
                Debug.Log("BAM! Musuh tertabrak. Sisa nyawa: " + nyawaMusuh);

                // Di sini kamu bisa menambahkan partikel percikan api atau suara tabrakan

                // 3. Cek apakah musuh sudah kalah
                if (nyawaMusuh <= 0)
                {
                    MusuhDitangkap();
                }
            }
            else
            {
                Debug.Log("Tabrakan terlalu pelan, tidak ada damage!");
            }
        }
    }

    void MusuhDitangkap()
    {
        Debug.Log("Musuh berhasil dihentikan!");

        // (Opsional) Matikan script AI agar mobil musuh berhenti bergerak
        SuspectCarAI aiScript = GetComponent<SuspectCarAI>();
        if (aiScript != null)
        {
            aiScript.enabled = false;
        }

        // Pindah ke level selanjutnya
        SceneManager.LoadScene(nextLevelName);
    }
}