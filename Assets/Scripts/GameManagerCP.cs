using UnityEngine;

public class GameManagerCP : MonoBehaviour
{
    public static GameManagerCP instance;

    [Header("Sistem Misi")]
    private int totalCheckpointMisi = 0;
    private int checkpointDiambil = 0;
    private bool gameSelesai = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // Otomatis menghitung berapa banyak checkpoint yang kamu sebar di jalanan
        totalCheckpointMisi = GameObject.FindObjectsByType<MekanikCheckpoint>(FindObjectsSortMode.None).Length;
        Debug.Log("Total Checkpoint di Map: " + totalCheckpointMisi);
    }

    public void CheckpointBerhasilDiambil()
    {
        if (gameSelesai) return;

        checkpointDiambil++;
        Debug.Log("Checkpoint diambil: " + checkpointDiambil + "/" + totalCheckpointMisi);

        // TRIGGER KETIKA CHECKPOINT PENUH (SUDAH DIAMBIL SEMUA)
        if (checkpointDiambil >= totalCheckpointMisi)
        {
            MisiSelesaiMenang();
        }
    }

    void MisiSelesaiMenang()
    {
        gameSelesai = true;
        Debug.Log("MISI SELESAI! SEMUA CHECKPOINT PENUH!");

        // 1. Menghentikan waktu game fisik agar mobil langsung berhenti/mengerem otomatis
        Time.timeScale = 0.5f; // Efek slow motion keren saat menang

        // Tips Tambahan untuk Dosen: Di sini kamu bisa memunculkan panel UI UI_Menang.SetActive(true);
    }

    // Fungsi bantuan untuk melihat status di layar (GUI Sederhana untuk UTS)
    void OnGUI()
    {
        // Membuat teks info di pojok kiri atas layar game saat dimaikan
        GUI.skin.label.fontSize = 20;
        if (!gameSelesai)
        {
            GUI.Label(new Rect(20, 20, 300, 40), "Checkpoint: " + checkpointDiambil + " / " + totalCheckpointMisi);
        }
        else
        {
            GUI.skin.label.normal.textColor = Color.green;
            GUI.skin.label.fontSize = 40;
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 50, 400, 100), "MISSION SUCCESS");
        }
    }
}