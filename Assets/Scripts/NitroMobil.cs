using UnityEngine;

public class NitroMobil : MonoBehaviour
{
    [Header("Efek NOS")]
    public ParticleSystem efekApiKiri;
    public ParticleSystem efekApiKanan;
    public AudioSource suaraNos;

    // Variabel rahasia untuk mengingat status NOS
    private bool nosMenyala = false;

    // Fungsi utama yang akan dipanggil oleh tombol NOS di layar
    // Tambahkan ini di dalam NitroMobil.cs
    public void ToggleNos()
    {
        // KUNCI: Jika game sedang pause atau hitung mundur, jangan lakukan apa-apa
        if (Time.timeScale == 0f) return; 

        nosMenyala = !nosMenyala; 

        if (nosMenyala)
        {
            NyalakanEfekNos();
        }
        else
        {
            MatikanEfekNos();
        }
    }

    public void NyalakanEfekNos()
    {
        if (efekApiKiri != null) efekApiKiri.Play();
        if (efekApiKanan != null) efekApiKanan.Play();
        
        if (suaraNos != null && !suaraNos.isPlaying) 
            suaraNos.Play();
    }

    public void MatikanEfekNos()
    {
        if (efekApiKiri != null) efekApiKiri.Stop();
        if (efekApiKanan != null) efekApiKanan.Stop();
        
        if (suaraNos != null) 
            suaraNos.Stop();
    }
}