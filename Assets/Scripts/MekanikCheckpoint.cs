using UnityEngine;

public class MekanikCheckpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Cek apakah yang menabrak memiliki Tag "Player" (Mobil Polisi)
        if (other.CompareTag("Player") || other.transform.root.CompareTag("Player"))
        {
            // Beritahu manager game kalau checkpoint ini sudah diambil
            GameManagerCP.instance.CheckpointBerhasilDiambil();
            
            // Hancurkan objek checkpoint ini agar hilang dari jalanan
            Destroy(gameObject);
        }
    }
}