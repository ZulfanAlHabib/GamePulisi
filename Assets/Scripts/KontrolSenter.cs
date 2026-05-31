using UnityEngine;

public class KontrolSenter : MonoBehaviour
{
    [Header("Kecepatan Putar Senter")]
    public float kecepatanPutar = 5f;

    private Camera kameraUtama;

    void Start()
    {
        // Mengambil referensi kamera utama game
        kameraUtama = Camera.main;
    }

    void Update()
    {
        // 1. Membuat garis imajiner (Ray) dari posisi kursor mouse di layar ke arah dunia 3D
        Ray ray = kameraUtama.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // 2. Jika garis tersebut menabrak objek di game (jalanan, gedung, tanah)
        if (Physics.Raycast(ray, out hit))
        {
            // 3. Hitung arah dari posisi lampu senter ke titik yang ditabrak mouse
            Vector3 arahTarget = hit.point - transform.position;
            
            // Mengunci sumbu X agar senter tidak berputar jungkir balik terlalu ekstrem ke bawah/atas
            // arahTarget.y = 0; // Jalankan baris ini jika ingin senter hanya menoleh kanan-kiri saja

            if (arahTarget != Vector3.zero)
            {
                // 4. Buat rotasi target berdasarkan arah tersebut
                Quaternion rotasiTarget = Quaternion.LookRotation(arahTarget);
                
                // 5. Putar lampu senter secara halus menuju koordinat mouse
                transform.rotation = Quaternion.Slerp(transform.rotation, rotasiTarget, kecepatanPutar * Time.deltaTime);
            }
        }
    }
}