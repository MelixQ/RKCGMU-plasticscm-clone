using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Controller : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public bool isUp;
    public bool isDublicated;
    public bool isCorrect;

    public GameObject neighbour;

    public GameObject answer;

    public GameObject ghost;

    public GameObject obj;


    public void OnDrop(PointerEventData eventData)
    {
        GameObject fromItem = eventData.pointerDrag;
        Debug.Log("dropped " + fromItem.name + " onto " + gameObject.name);

        ghost = fromItem.GetComponent<CardController>().ghost;

        obj = fromItem;

        CreatePic(fromItem);

        var answers = answer.GetComponent<SceneController>();

        this.gameObject.SetActive(true);

        if (isUp)
        {
            if (fromItem.name == "1")
            {
                answers.f1 = true;
                if (answers.f1 & !(fromItem.name == gameObject.name) & neighbour.GetComponent<Controller>().IsTrue(neighbour)) 
                {
                    this.isDublicated = true;
                    neighbour.GetComponent<Controller>().isDublicated = true;
                }
            }
            else
            if (fromItem.name == "3")
            {
                answers.f3 = true;
                if (answers.f3 & !(fromItem.name == gameObject.name) & neighbour.GetComponent<Controller>().IsTrue(neighbour))
                {
                    this.isDublicated = true;
                    neighbour.GetComponent<Controller>().isDublicated = true;
                }
            } 
        }
        else 
        {
            if (fromItem.name == "2")
            {
                answers.f2 = true;
                if (answers.f2 & !(fromItem.name == gameObject.name) & neighbour.GetComponent<Controller>().IsTrue(neighbour))
                {
                    this.isDublicated = true;
                    neighbour.GetComponent<Controller>().isDublicated = true;
                }
            }
            else
            if (fromItem.name == "4")
            {
                answers.f4 = true;
                if (answers.f4 & !(fromItem.name == gameObject.name) & neighbour.GetComponent<Controller>().IsTrue(neighbour))
                {
                    this.isDublicated = true;
                    neighbour.GetComponent<Controller>().isDublicated = true;
                }
            }
        }
        
    }
    private void CreatePic(GameObject item)
    {
        this.GetComponent<Image>().sprite = item.GetComponent<Image>().sprite;
        this.GetComponent<Image>().color = new Color(255, 255, 255, 255);
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

        this.gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 0);

        if (!isDublicated) 
        {
            obj.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }

        remove(obj.name.Substring(0, 1));

        ghost = null;


        //this.gameObject.SetActive(false);

        //ghost_out.SetActive(false);
    }

    private void remove(string str) 
    {
        Debug.Log(str);
        switch (str)
        {
            case "1":
                if (!isDublicated)
                {
                    answer.GetComponent<SceneController>().f1 = false;
                }
                else 
                {
                    isDublicated = false;
                    neighbour.GetComponent<Controller>().isDublicated = false;
                }
                break;

            case "2":
                if (!isDublicated)
                {
                    answer.GetComponent<SceneController>().f2 = false;
                }
                else
                {
                    isDublicated = false;
                    neighbour.GetComponent<Controller>().isDublicated = false;
                }
                break;

            case "3":
                if (!isDublicated)
                {
                    answer.GetComponent<SceneController>().f3 = false;
                }
                else
                {
                    isDublicated = false;
                    neighbour.GetComponent<Controller>().isDublicated = false;
                }
                break;

            case "4":
                if (!isDublicated)
                {
                    answer.GetComponent<SceneController>().f4 = false;
                }
                else
                {
                    isDublicated = false;
                    neighbour.GetComponent<Controller>().isDublicated = false;
                }
                break;

            default:
                break;
        }
    }

    public bool IsTrue(GameObject gameObject) 
    {
        var ctrl = gameObject.GetComponent<Controller>();
        Debug.Log(ctrl.obj.name);
        switch (obj.name)
        {
            case "1":
                return answer.GetComponent<SceneController>().f1;

            case "2":
                return answer.GetComponent<SceneController>().f2;

            case "3":
                return answer.GetComponent<SceneController>().f3;

            case "4":
                return answer.GetComponent<SceneController>().f4;

            default:
                return false;
        }
    }
}
