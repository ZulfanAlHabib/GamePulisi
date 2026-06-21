using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject panelInfo;

    [Header("Audio Settings")]
    public AudioSource audioSource; // Referensi ke "speaker"
    public AudioClip suaraSirine;   // Referensi ke file MP3 kamu

    void Start()
    {
        if (panelInfo != null)
        {
            panelInfo.SetActive(false);
        }
    }

    // Fungsi khusus untuk memutar suara
    // Fungsi khusus untuk memutar suara
    public void MainkanSuara()
    {
        if (audioSource != null && suaraSirine != null)
        {
            // Masukkan kaset ke speaker dan putar
            audioSource.clip = suaraSirine;
            audioSource.Play();
            
            // Suruh Unity menjalankan fungsi HentikanSuara setelah 1.5 detik
            Invoke("HentikanSuara", 0.5f); 
        }
    }

    // Fungsi untuk mematikan speaker
    private void HentikanSuara()
    {
        if (audioSource != null)
        {
            audioSource.Stop(); // Matikan suara secara paksa
        }
    }

    public void MulaiGame()
    {
        MainkanSuara();
        // Kita gunakan Invoke agar ada jeda 0.5 detik untuk memutar suara sebelum pindah scene
        Invoke("LoadSceneGameplay", 0.5f); 
    }

    private void LoadSceneGameplay()
    {
        SceneManager.LoadScene("s1"); 
    }

    public void BukaInfo()
    {
        MainkanSuara();
        panelInfo.SetActive(true);
    }

    public void TutupInfo()
    {
        MainkanSuara();
        panelInfo.SetActive(false);
    }

    public void KeluarGame()
    {
        MainkanSuara();
        Debug.Log("Keluar dari Game!");
        Application.Quit();
    }
}