using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private bool balapanSelesai = false; 

    private void OnTriggerEnter(Collider other)
    {
        // Radar Diagnostik Garis Finish
        Debug.Log("Garis Finish disentuh oleh: " + other.gameObject.name + " | Tag-nya: " + other.gameObject.tag);

        if (balapanSelesai) return; 

        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            balapanSelesai = true;
            Debug.Log("Waktu Habis! Memanggil Panel Kalah...");
            PanggilPanelKalah();
        }
    }

    private void PanggilPanelKalah()
    {
        // TODO: Panggil fungsi dari GameManager Anda untuk menampilkan UI Kalah
        FindFirstObjectByType<Level2>().TampilkanKalah();

        Time.timeScale = 0f; // Hentikan game
    }
}