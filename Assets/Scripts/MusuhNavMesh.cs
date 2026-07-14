using UnityEngine;
using UnityEngine.AI;

public class MusuhNavMesh : MonoBehaviour
{
    public Transform targetFinish; // Masukkan objek GarisFinish ke sini lewat Inspector
    private NavMeshAgent agen;

    void Start()
    {
        agen = GetComponent<NavMeshAgent>();
        if (targetFinish != null)
        {
            agen.SetDestination(targetFinish.position);
        }
    }
}