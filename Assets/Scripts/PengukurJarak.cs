using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class PengukurJarak : MonoBehaviour
{
    [Header("Target Objek 3D")]
    public Transform mobilPolisi; // Pemain
    public Transform mobilMusuh;  // Target

    [Header("Teks & Visual UI")]
    public TextMeshProUGUI teksJarak; 
    public Slider sliderJarak; 
    
    [Header("Pengaturan Jarak")]
    public float jarakMaksimal = 200f; // Jarak di mana musuh ada di ujung paling kiri layar

    void Start()
    {
        if (sliderJarak != null)
        {
            sliderJarak.maxValue = jarakMaksimal;
        }
    }

    void Update()
    {
        if (mobilPolisi != null && mobilMusuh != null)
        {
            // Menghitung jarak 3D secara realtime
            float jarak = Vector3.Distance(mobilPolisi.position, mobilMusuh.position);

            // Menampilkan angka
            if (teksJarak != null)
            {
                teksJarak.text = Mathf.RoundToInt(jarak).ToString() + " m";
            }

            // Menggerakkan ikon mobil di Slider
            if (sliderJarak != null)
            {
                // Semakin dekat (jarak mengecil), ikon polisi makin ke kanan
                sliderJarak.value = jarakMaksimal - jarak; 
                
                if (sliderJarak.value < 0) 
                {
                    sliderJarak.value = 0;
                }
            }
        }
    }
}