using UnityEngine;
using UnityEngine.EventSystems;

public class Stir : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [Header("Pengaturan Setir")]
    public float steeringValue; 
    public float maxRotationAngle = 90f; // Batas putaran (bisa diatur)
    public float smoothing = 15f;      // Kecepatan putaran visual (semakin besar semakin responsif)
    
    private RectTransform rectTransform;
    private float targetRotation;      // Sudut target setir

    void Start() { rectTransform = GetComponent<RectTransform>(); }

    void Update()
    {
        // Membuat visual setir berputar dengan mulus ke arah target
        Quaternion targetRotationQuat = Quaternion.Euler(0, 0, -steeringValue * maxRotationAngle);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotationQuat, Time.deltaTime * smoothing);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out localPoint);
        
        float radius = rectTransform.rect.width / 2f;
        steeringValue = Mathf.Clamp(localPoint.x / radius, -1f, 1f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        steeringValue = 0f; // Kembali ke tengah saat dilepas
    }

    public void OnPointerDown(PointerEventData eventData) { /* Wajib ada */ }
}