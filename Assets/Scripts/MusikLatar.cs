using UnityEngine;

public class MusikLatar : MonoBehaviour
{
    // Variabel statis untuk mengecek apakah musik sudah ada
    private static MusikLatar instance;

    void Awake()
    {
        // Mengecek apakah sebelumnya sudah ada BGM yang menyala
        if (instance == null)
        {
            // Jika belum ada, jadikan ini sebagai BGM utama
            instance = this;
            
            // Perintah ini yang membuat musik TIDAK MATI saat pindah level
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            // Jika pemain kembali ke Main Menu, cegah terjadinya lagu ganda/bertumpuk
            Destroy(gameObject); 
        }
    }
}