using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI.Extensions;

public class UICardAction : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent actionClickEvent;
    public UnityEvent actionUpEvent;
    [SerializeField] private UILineRenderer line;
    public void OnPointerClick(PointerEventData eventData)
    {
        actionClickEvent.Invoke();
        SetLineRenderer();
        line.gameObject.SetActive(true);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        actionClickEvent.Invoke();
        SetLineRenderer();
        line.gameObject.SetActive(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        actionUpEvent.Invoke();
    }
    private void SetLineRenderer()
    {
        Vector3 halfScreen = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
        line.Points[0] = this.gameObject.transform.position - halfScreen;
    }

}
