using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject panelPause;
    public GameObject panelGameOver;

    private int jumlahTabrakan = 0;
    private const int maxTabrakan = 3;

    void Start()
    {
        // Pastikan game berjalan normal saat scene s1 baru dimulai
        Time.timeScale = 1f; 
        
        if (panelPause != null) panelPause.SetActive(false);
        if (panelGameOver != null) panelGameOver.SetActive(false);
    }

    // --- FUNGSI PAUSE & RESUME ---
    public void PauseGame()
    {
        panelPause.SetActive(true);
        Time.timeScale = 0f; // Bekukan fisika game
    }

    public void ResumeGame()
    {
        panelPause.SetActive(false);
        Time.timeScale = 1f; // Jalankan lagi fisika game
    }

    // --- FUNGSI DETEKSI & GAME OVER ---
    public void MobilMenabrakMusuh()
    {
        jumlahTabrakan++;
        Debug.Log("Tabrakan ke-" + jumlahTabrakan);

        if (jumlahTabrakan >= maxTabrakan)
        {
            TriggerGameOver();
        }
    }

    private void TriggerGameOver()
    {
        Time.timeScale = 0f; // Hentikan mobil karena sudah kalah
        if (panelGameOver != null)
        {
            panelGameOver.SetActive(true); // Munculkan panel game over
        }
    }

    // --- FUNGSI PINDAH SCENE ---
    public void MainUlang()
    {
        Time.timeScale = 1f;
        // Load ulang scene yang sedang aktif saat ini (scene s1)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    public void KeluarKeMainMenu()
    {
        Time.timeScale = 1f; // Sangat penting agar menu tidak ikut membeku
        SceneManager.LoadScene("MainMenu"); 
    }
}