using UnityEngine;
using UnityEngine.AI; // Wajib untuk NavMesh

public class SuspectCarAI : MonoBehaviour
{
    [Header("Pengaturan NavMesh (Tujuan)")]
    public Transform targetTujuan; 
    private NavMeshAgent agen;

    [Header("Pengaturan Jarak (Kondisi Kalah)")]
    public Transform mobilPolisi; 
    public float batasJarakMaksimal = 100f; 

    [Header("Pengaturan Game Manager")]
    public Level1 gameManager;// Disesuaikan dengan nama script pengatur Level 1 Anda

    private bool gameSelesai = false; 

    void Start()
    {
        agen = GetComponent<NavMeshAgent>();
        
        // Memerintahkan musuh untuk langsung menyetir ke target
        if (targetTujuan != null)
        {
            agen.SetDestination(targetTujuan.position);
        }
    }

    void Update()
    {
        if (gameSelesai) return; 

        // Mengecek jarak antara musuh dan polisi
        // Mengecek jarak antara musuh dan polisi secara real-time
        if (mobilPolisi != null)
        {
            float jarakSekarang = Vector3.Distance(transform.position, mobilPolisi.position);

            // KODE BARU: Memunculkan angka jarak asli di Console
            Debug.Log("Jarak saat ini: " + jarakSekarang);

            if (jarakSekarang > batasJarakMaksimal)
            {
                Debug.Log("MISI GAGAL! Target terlalu jauh.");
                gameSelesai = true; 
                
                if (gameManager != null)
                {
                    gameManager.KondisiKalah();
                }
                Time.timeScale = 0f;
            }
        }
    }

    // --- FUNGSI DETEKSI TABRAKAN (KONDISI MENANG) ---
    private void OnCollisionEnter(Collision collision)
    {
        if (gameSelesai) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("DARRR! Target Tertangkap Polisi!");
            gameSelesai = true; 
            
            if (gameManager != null)
            {
                gameManager.KondisiMenang();
            }

            // Membekukan waktu agar mobil tidak melompat-lompat saat panel muncul
            Time.timeScale = 0f;
        }
    }
}