using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchSlider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityAction onPointerDownEvent;
    public UnityAction<float> onPointerDragEvent;
    public UnityAction onPointerUpEvent;

    private Slider _uiSlider;

    private void Awake()
    {
        _uiSlider = GetComponent<Slider>();
        _uiSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (onPointerDownEvent != null)
        {
            onPointerDownEvent.Invoke();
        }
        if (onPointerDragEvent != null)
        {
            onPointerDragEvent.Invoke(_uiSlider.value);
        }
    }

    private void OnSliderValueChanged(float value)
    {
        if (onPointerDragEvent != null)
        {
            onPointerDragEvent.Invoke(value);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (onPointerUpEvent != null)
        {
            onPointerUpEvent.Invoke();
        }
        _uiSlider.value = 0;
    }

    private void OnDestroy()
    {
        _uiSlider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }
}
