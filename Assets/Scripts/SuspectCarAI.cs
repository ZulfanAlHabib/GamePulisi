using UnityEngine;

public class SuspectCarAI : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 15f;
    public float turnSpeed = 5f;
    private int currentWaypointIndex = 0;
    
    private Rigidbody rb;

    void Start()
    {
        // Mengambil komponen Rigidbody saat game dimulai
        rb = GetComponent<Rigidbody>();
    }

    // Menggunakan FixedUpdate karena kita sekarang berurusan dengan Fisika (Rigidbody)
    void FixedUpdate()
    {
        if (waypoints.Length == 0) return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = targetWaypoint.position - transform.position;
        direction.y = 0; // Kunci sumbu Y agar mobil tidak mendongak/menunduk aneh
        
        if (direction != Vector3.zero)
        {
            // Putar mobil secara halus
            Quaternion rotation = Quaternion.LookRotation(direction);
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, rotation, Time.fixedDeltaTime * turnSpeed));
        }

        // Gerakkan mobil maju menggunakan mesin fisika Rigidbody
        Vector3 moveForce = transform.forward * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + moveForce);

        // Jika sudah dekat dengan waypoint saat ini, lanjut ke waypoint berikutnya
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 5f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }
}