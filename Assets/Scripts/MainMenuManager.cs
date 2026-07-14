using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour 
{
    [Header("Pengaturan Panel")]
    public GameObject panelLevel; 
    public GameObject panelSettings; // KODE BARU: Kolom untuk memasukkan Panel Settings

    [Header("Pengaturan Tombol Level")]
    public Button tombolLevel1;
    public Button tombolLevel2;

    void Start()
    {
        CekStatusLevel();
        
        // Memastikan semua panel pop-up tertutup saat awal mulai
        if (panelLevel != null) panelLevel.SetActive(false);
        if (panelSettings != null) panelSettings.SetActive(false); 
    }

    private void CekStatusLevel()
    {
        int levelTerbuka = PlayerPrefs.GetInt("LevelTerbuka", 1);
        if (tombolLevel1 != null) tombolLevel1.interactable = true;

        if (levelTerbuka >= 2)
        {
            if (tombolLevel2 != null) tombolLevel2.interactable = true;
        }
        else
        {
            if (tombolLevel2 != null) tombolLevel2.interactable = false;
        }
    }

    public void BukaLevel1()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("s1"); 
    }

    public void BukaLevel2()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("s2"); 
    }

    // --- FUNGSI PANEL LEVEL ---
    public void BukaPanelLevel()
    {
        if (panelLevel != null) panelLevel.SetActive(true);
    }

    public void TutupPanelLevel()
    {
        if (panelLevel != null) panelLevel.SetActive(false);
    }

    // --- FUNGSI BARU: PANEL SETTING (SUARA) ---
    public void BukaPanelSettings()
    {
        if (panelSettings != null) panelSettings.SetActive(true);
    }

    public void TutupPanelSettings()
    {
        if (panelSettings != null) panelSettings.SetActive(false);
    }

    // --- FUNGSI BARU: KELUAR GAME ---
    public void KeluarGame()
    {
        Debug.Log("Pemain keluar dari game!"); // Ini hanya muncul di Console Unity
        Application.Quit(); // KODE PENTING: Menutup aplikasi
    }
}