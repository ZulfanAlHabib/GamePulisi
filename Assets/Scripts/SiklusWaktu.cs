using UnityEngine;

public class SiklusWaktu : MonoBehaviour
{
    [Header("Kecepatan Waktu")]
    [Tooltip("Semakin besar angkanya, perubahan siang ke malam akan semakin cepat")]
    public float kecepatanSiklus = 1f; 

    void Update()
    {
        // Memutar matahari secara perlahan pada sumbu X (Pitch)
        // Kecepatan putaran dikalikan dengan Time.deltaTime agar halus
        transform.Rotate(Vector3.right * kecepatanSiklus * Time.deltaTime);
    }
}