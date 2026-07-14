using UnityEngine;
using UnityEngine.SceneManagement;

public class PengaturPause : MonoBehaviour
{
    [Header("Pengaturan Panel UI")]
    public GameObject panelPause;
    public GameObject panelGameplay;

    private bool sedangPause = false;

    void Start()
    {
        if (panelPause != null) panelPause.SetActive(false); 
        if (panelGameplay != null) panelGameplay.SetActive(true); 
        
        Time.timeScale = 1f; 
        
        // Pastikan telinga pemain tidak tertutup saat awal level
        AudioListener.pause = false; 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (sedangPause) LanjutkanGame();
            else PauseGame();
        }
    }

    public void PauseGame()
    {
        sedangPause = true;
        panelGameplay.SetActive(false); 
        panelPause.SetActive(true);     
        Time.timeScale = 0f;            
        
        // Mem-pause SEMUA suara di dalam game
        AudioListener.pause = true; 
    }

    public void LanjutkanGame()
    {
        sedangPause = false;
        panelPause.SetActive(false);    
        panelGameplay.SetActive(true);  
        Time.timeScale = 1f;            
        
        // Melanjutkan SEMUA suara di dalam game dari titik terakhirnya
        AudioListener.pause = false; 
    }

    public void MulaiUlangGame()
    {
        Time.timeScale = 1f; 
        AudioListener.pause = false; // SANGAT PENTING: Buka suara sebelum restart
        
        string namaSceneAktif = SceneManager.GetActiveScene().name; 
        SceneManager.LoadScene(namaSceneAktif);
    }

    public void KembaliKeMainMenu()
    {
        Time.timeScale = 1f; 
        AudioListener.pause = false; // SANGAT PENTING: Buka suara sebelum pindah menu
        
        SceneManager.LoadScene("MainMenu"); 
    }
}