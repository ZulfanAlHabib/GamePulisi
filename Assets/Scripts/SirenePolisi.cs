using UnityEngine;

public class SirenePolisi : MonoBehaviour
{
    [Header("Pengaturan Lampu")]
    public GameObject lampuMerah;
    public GameObject lampuBiru;
    public float kecepatanKedip = 0.2f; // Detik per kedipan

    [Header("Pengaturan Suara")]
    private AudioSource audioSirene;

    private bool sireneAktif = false;
    private float timer;
    private bool toggleLampu = false;

    void Start()
    {
        // Mengambil komponen Audio Source yang ada di mobil
        audioSirene = GetComponent<AudioSource>();

        // Matikan lampu saat game baru mulai
        lampuMerah.SetActive(false);
        lampuBiru.SetActive(false);
    }

    void Update()
    {
        // Jika menekan tombol F, aktifkan atau matikan sirene
        if (Input.GetKeyDown(KeyCode.F))
        {
            sireneAktif = !sireneAktif;

            if (sireneAktif)
            {
                audioSirene.Play(); // Bunyikan suara
            }
            else
            {
                audioSirene.Stop(); // Matikan suara
                lampuMerah.SetActive(false);
                lampuBiru.SetActive(false);
            }
        }

        // Logika untuk membuat lampu berkedip bergantian
        if (sireneAktif)
        {
            timer += Time.deltaTime;

            if (timer >= kecepatanKedip)
            {
                toggleLampu = !toggleLampu;
                
                // Bergantian menyalakan lampu merah dan biru
                lampuMerah.SetActive(toggleLampu);
                lampuBiru.SetActive(!toggleLampu);

                timer = 0; // Reset timer
            }
        }
    }
}