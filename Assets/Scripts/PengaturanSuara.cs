using UnityEngine;
using UnityEngine.UI; // Wajib ditambahkan untuk memanipulasi UI

public class PengaturanSuara : MonoBehaviour
{
    [Header("Komponen UI")]
    public Slider sliderVolume;

    void Start()
    {
        // Mengambil nilai volume yang terakhir disimpan. Jika belum pernah diatur, defaultnya 1 (maksimal)
        float volumeTersimpan = PlayerPrefs.GetFloat("VolumeGame", 1f);
        
        // Menyamakan posisi tuas slider dengan volume yang tersimpan
        if (sliderVolume != null)
        {
            sliderVolume.value = volumeTersimpan;
        }
        
        // Menerapkan volume ke seluruh suara di dalam game
        AudioListener.volume = volumeTersimpan;
    }

    // Fungsi ini akan terus dipanggil setiap kali pemain menggeser tuas slider
    public void UbahVolume(float nilai)
    {
        // Mengubah volume game secara real-time
        AudioListener.volume = nilai;
        
        // Menyimpan nilai volume agar tidak reset saat game ditutup
        PlayerPrefs.SetFloat("VolumeGame", nilai);
    }
}