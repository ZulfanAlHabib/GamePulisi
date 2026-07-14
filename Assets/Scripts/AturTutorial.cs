using UnityEngine;

public class AturTutorial : MonoBehaviour
{
    [Header("Pengaturan UI")]
    public GameObject panelGameplay; // Slot untuk menyembunyikan tombol saat tutorial

    void Start()
    {
        // SAAT LEVEL BARU DIMULAI (Tutorial Muncul):
        
        // 1. Sembunyikan semua tombol permainan (termasuk Pause, Sirine, Gas, dll)
        if (panelGameplay != null)
        {
            panelGameplay.SetActive(false);
        }
        
        // 2. Bekukan waktu permainan
        Time.timeScale = 0f; 
    }

    // Fungsi ini dipanggil oleh tombol "Mulai" di dalam panel tutorial
    public void TutupTutorialDanMain()
    {
        // SAAT TOMBOL MULAI DIKLIK:
        
        // 1. Tampilkan kembali semua tombol permainan
        if (panelGameplay != null)
        {
            panelGameplay.SetActive(true);
        }

        // 2. Jalankan waktu kembali agar mobil bisa bergerak
        Time.timeScale = 1f; 
    }
}