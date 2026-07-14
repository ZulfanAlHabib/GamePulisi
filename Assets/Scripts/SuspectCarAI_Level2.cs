using UnityEngine;

public class SuspectCarAI_Level2 : MonoBehaviour
{

    [Header("Pengaturan Jarak (Kondisi Kalah)")]
    public Transform mobilPolisi; 
    public float batasJarakMaksimal = 100f; 

    [Header("Pengaturan Game Manager")]
    // Pastikan tipe ini sesuai dengan nama script GameManager di Level 2 Anda
    public Level2 gameManager; 

    // Mencegah panel menang/kalah terpanggil berkali-kali
    private bool gameSelesai = false; 

    void Update()
    {
        if (gameSelesai) return; 

        // Mengecek jarak antara musuh dan polisi secara real-time
        if (mobilPolisi != null)
        {
            float jarakSekarang = Vector3.Distance(transform.position, mobilPolisi.position);

            // Jika jarak melebihi batas maksimal, jalankan Kondisi Kalah
            if (jarakSekarang > batasJarakMaksimal)
            {
                Debug.Log("MISI GAGAL! Target terlalu jauh dan berhasil kabur.");
                gameSelesai = true; 
                
                if (gameManager != null)
                {
                    // Pastikan nama pemanggilan fungsinya sesuai dengan yang ada di GameManagerLevel2
                    gameManager.TampilkanKalah(); 
                }
            }
        }
    }

    // --- FUNGSI DETEKSI TABRAKAN (KONDISI MENANG) ---
    private void OnCollisionEnter(Collision collision)
    {
        if (gameSelesai) return;

        // Pastikan mobil polisi Anda memiliki tag "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("DARRR! Target Tertangkap Polisi!");
            gameSelesai = true; 
            
            if (gameManager != null)
            {
                gameManager.TampilkanMenang(); 
            }
        }
    }
}