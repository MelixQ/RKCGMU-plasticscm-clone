using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WearController : MonoBehaviour, IDropHandler
{
    // TODO: Add item placed sound effect.
    public bool HeadSlot;
    public bool BodySlot;
    public bool LegSlot;
    public bool ShoseSlot;

    public GameObject CheckController;

    public void OnDrop(PointerEventData eventData)
    {
        var fromItem = eventData.pointerDrag;
        Debug.Log("dropped " + fromItem.name + " onto " + gameObject.name);
        
        switch (fromItem.tag)
        {
            case "Head":
                if (HeadSlot) 
                {
                    Debug.Log("1");
                    CreatePic(fromItem);
                    if (fromItem.name == "32")
                    {
                        Debug.Log("ITEM2");
                        CheckController.GetComponent<CheckWear>().head = true;
                    }
                    else
                    {
                        CheckController.GetComponent<CheckWear>().head = false;
                    }
                }
                break;

            case "Body":
                if (BodySlot)
                {
                    Debug.Log("2");
                    CreatePic(fromItem);
                    if (fromItem.name == "Item2" || fromItem.name == "Item2_w")
                    {
                        Debug.Log("ITEM2");
                        CheckController.GetComponent<CheckWear>().body = true;
                    }
                    else 
                    {
                        CheckController.GetComponent<CheckWear>().body = false;
                    }
                }
                break;

            case "Leg":
                if (LegSlot)
                {
                    Debug.Log("3");
                    CreatePic(fromItem);
                    if (fromItem.name == "21")
                    {
                        Debug.Log("ITEM2");
                        CheckController.GetComponent<CheckWear>().leg = true;
                    }
                    else
                    {
                        CheckController.GetComponent<CheckWear>().leg = false;
                    }
                }
                break;

            case "Shose":
                if (ShoseSlot)
                {
                    Debug.Log("4");
                    CreatePic(fromItem);
                    if (fromItem.name == "Item3" || fromItem.name == "31_w")
                    {
                        Debug.Log("ITEM2");
                        CheckController.GetComponent<CheckWear>().shose = true;
                    }
                    else
                    {
                        CheckController.GetComponent<CheckWear>().shose = false;
                    }
                }
                break;

            default:
                break;
        }
    }

    private void CreatePic(GameObject item) 
    {
        GetComponent<Image>().sprite = item.GetComponent<Image>().sprite;
        GetComponent<Image>().color = new Color(255, 255, 255, 255);
    }
}