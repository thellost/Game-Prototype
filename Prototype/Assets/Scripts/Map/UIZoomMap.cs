using UnityEngine;
using UnityEngine.EventSystems;

public class UIZoomMap : MonoBehaviour, IScrollHandler
{
    [SerializeField] private float zoomSpeed = 0.1f;
    [SerializeField] private float maxZoomScale;

    private Vector3 initialScale;

    private void Awake()
    {
        initialScale = transform.localScale;
    }

    public void OnScroll(PointerEventData eventData)
    {
        var delta = Vector3.one * (eventData.scrollDelta.y * zoomSpeed);
        var desiredScale = transform.localScale + delta;

        desiredScale = ClampDesiredScale(desiredScale);

        transform.localScale = desiredScale;
    }

    private Vector3 ClampDesiredScale(Vector3 desiredScale)
    {
        desiredScale = Vector3.Max(initialScale, desiredScale);
        desiredScale = Vector3.Min(initialScale * maxZoomScale, desiredScale);
        return desiredScale;
    }
}