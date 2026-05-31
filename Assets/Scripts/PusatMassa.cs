using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PusatMassa : MonoBehaviour
{
    public Transform targetCenterOfMass; // Slot untuk memasukkan objek CoM
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Mengubah pusat massa rigidbody ke posisi objek CoM yang kita buat
        if (targetCenterOfMass != null)
        {
            rb.centerOfMass = targetCenterOfMass.localPosition;
        }
    }
}