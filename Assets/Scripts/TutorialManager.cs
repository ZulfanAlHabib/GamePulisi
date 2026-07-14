using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject panelTutorial;

    void Start()
    {
        // Memunculkan panel tutorial secara otomatis saat Scene s0 dimulai
        panelTutorial.SetActive(true);
        
        // Menjeda game (pause) agar karakter/musuh tidak bergerak
        Time.timeScale = 0f; 
    }

    // Fungsi ini akan dipanggil saat tombol "Paham/Mulai Bermain" diklik
    public void TutupTutorialDanMulai()
    {
        // Menyembunyikan panel tutorial
        panelTutorial.SetActive(false);
        
        // Melanjutkan waktu game menjadi normal
        Time.timeScale = 1f; 
    }
}