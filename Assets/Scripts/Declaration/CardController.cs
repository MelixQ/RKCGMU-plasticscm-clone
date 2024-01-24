using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardController: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    //public Image ghost;
    //public GameObject ghost_out;
    public GameObject ghost;


    void Awake()
    {
        ghost.GetComponent<Image>().raycastTarget = false;
        //ghost_out.SetActive(false);

        ghost.SetActive(false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        ghost.transform.position = transform.position;
        ghost.SetActive(true);

    }

    public void OnDrag(PointerEventData eventData)
    {
        this.gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);

        ghost.transform.position += (Vector3)eventData.delta;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        ghost.SetActive(false);

        this.gameObject.GetComponent<Image>().color = new Color32 (255, 255, 255, 80);

        //this.gameObject.SetActive(false);

        //ghost_out.SetActive(false);
    }

    public void OnDrop(PointerEventData data)
    {
        GameObject fromItem = data.pointerDrag;
        if (data.pointerDrag == null) return;

        gameObject.GetComponent<Controller>().obj = this.gameObject;
        Debug.Log("dropped  " + fromItem.name + " onto " + gameObject.name);


    }
}

