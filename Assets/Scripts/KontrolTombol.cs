using UnityEngine;

public class KontrolTombol : MonoBehaviour
{
    public bool sedangGas = false;

    // Fungsi untuk tombol Gas yang ditahan
    public void TekanGas()
    {
        sedangGas = true;
        Debug.Log("Gas diinjak!"); // Cek console untuk memastikan tombol bekerja
    }

    public void LepasGas()
    {
        sedangGas = false;
        Debug.Log("Gas dilepas!");
    }

    // Fungsi untuk tombol Sirine yang ditekan sekali
    public void KlikSirine()
    {
        Debug.Log("Sirine menyala!");
    }
}