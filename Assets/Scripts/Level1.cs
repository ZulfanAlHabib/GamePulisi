using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 
using System.Collections;
using TMPro;

public class Level1 : MonoBehaviour
{
    [Header("Pengaturan UI")]
    public GameObject panelTutorial; 
    public GameObject panelMenang;
    public GameObject panelKalah;
    public GameObject panelGameplay;
    public GameObject panelPause;
    
    [Header("Tombol yang Dikunci")]
    public Button btnPause; 
    public Button btnSirine; 
    
    [Header("Audio Control")]
    public AudioSource audioSirine;
    
    [Header("Hitung Mundur")]
    public TextMeshProUGUI teksCountdown; 

    void Start()
    {
        Time.timeScale = 0f;
        
        if (panelTutorial != null) panelTutorial.SetActive(true);
        if (panelMenang != null) panelMenang.SetActive(false);
        if (panelKalah != null) panelKalah.SetActive(false);
        if (panelGameplay != null) panelGameplay.SetActive(false); 
        if (panelPause != null) panelPause.SetActive(false);
        if (teksCountdown != null) teksCountdown.gameObject.SetActive(false);

        // Kunci tombol di awal
        if (btnPause != null) btnPause.interactable = false;
        if (btnSirine != null) btnSirine.interactable = false;
    }

    public void TutupTutorialDanMulaiHitungMundur()
    {
        if (panelTutorial != null) panelTutorial.SetActive(false);
        if (panelGameplay != null) panelGameplay.SetActive(true);
        StartCoroutine(HitungMundurCoroutine());
    }

    IEnumerator HitungMundurCoroutine()
    {
        if (teksCountdown != null)
        {
            teksCountdown.gameObject.SetActive(true);
            teksCountdown.text = "3";
            yield return new WaitForSecondsRealtime(1f);
            teksCountdown.text = "2";
            yield return new WaitForSecondsRealtime(1f);
            teksCountdown.text = "1";
            yield return new WaitForSecondsRealtime(1f);
            teksCountdown.text = "MULAI!";
            yield return new WaitForSecondsRealtime(1f);
            teksCountdown.gameObject.SetActive(false);
        }

        // Aktifkan kembali tombol
        if (btnPause != null) btnPause.interactable = true;
        if (btnSirine != null) btnSirine.interactable = true;
        
        Time.timeScale = 1f;
    }

    public void KondisiMenang()
    {
        PlayerPrefs.SetInt("LevelTerbuka", 2);
        PlayerPrefs.Save();
        
        // Hentikan suara
        if (audioSirine != null) audioSirine.Stop();

        if (panelMenang != null) panelMenang.SetActive(true);
        if (panelGameplay != null) panelGameplay.SetActive(false); 
        Time.timeScale = 0f; 
    }

    public void KondisiKalah()
    {
        // Hentikan suara
        if (audioSirine != null) audioSirine.Stop();

        if (panelKalah != null) panelKalah.SetActive(true);
        if (panelGameplay != null) panelGameplay.SetActive(false); 
        Time.timeScale = 0f; 
    }

    public void PauseGame()
    {
        if (panelPause != null) panelPause.SetActive(true);
        Time.timeScale = 0f;
    }

    public void LanjutGame()
    {
        if (panelPause != null) panelPause.SetActive(false);
        Time.timeScale = 1f;
    }

    public void UlangiLevel() { Time.timeScale = 1f; SceneManager.LoadScene("s1"); }
    public void KeMenuUtama() { Time.timeScale = 1f; SceneManager.LoadScene("MainMenu"); }
    public void LanjutLevel2() { Time.timeScale = 1f; SceneManager.LoadScene("s2"); }
}