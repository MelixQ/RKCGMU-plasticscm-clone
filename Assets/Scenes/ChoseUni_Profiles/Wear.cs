using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Wear: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerClickHandler
{
    [Header("Place corresponding outfit type gameobject from\nCanvas -> manSlot/womanSlot Gameobject")]
    [SerializeField] private WearController _correctOutfitTypeControllerSlot;
    
    public Image ghost;
    public Image ghost_out;

    public bool isTrue;

    private void Awake()
    {
        ghost.raycastTarget = false;
        ghost_out.raycastTarget = false;

        ghost.enabled = false;
        ghost_out.enabled = false;
    }
    
    public void OnPointerClick(PointerEventData eventData) => 
        _correctOutfitTypeControllerSlot.OnDrop(eventData: eventData);

    public void OnBeginDrag(PointerEventData eventData)
    {
        GetComponent<Image>().color = new Color(0, 0, 0, 0);

        ghost.transform.position = transform.position;
        ghost.enabled = true;

        ghost_out.transform.position = transform.position;
        ghost_out.enabled = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        ghost.transform.position += (Vector3)eventData.delta;
        ghost_out.transform.position += (Vector3)eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<Image>().color = new Color(255, 255, 255, 255);

        ghost.enabled = false;
        ghost_out.enabled = false;
    }

    public void OnDrop(PointerEventData data)
    {
        var fromItem = data.pointerDrag;
        if (data.pointerDrag == null) return;
        
        Debug.Log("dropped  " + fromItem.name + " onto " + gameObject.name);
    }
}