using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIGraphicRaycaster : MonoBehaviour
{
    public static UIGraphicRaycaster GetInstance() => _instance;
    
    private GraphicRaycaster m_Raycaster;
    private PointerEventData m_PointerEventData;
    private EventSystem m_EventSystem;
    private static UIGraphicRaycaster _instance;

    private void Awake()
    {
        if (_instance != null)
            Debug.LogWarning($"More than one instance of {this} found in the scene");
        _instance = this;
    }

    private void Start()
    {
        m_Raycaster = GetComponent<GraphicRaycaster>();
        m_EventSystem = GetComponent<EventSystem>();
    }

    public List<RaycastResult> RaycastUIElement()
    {
        m_PointerEventData = new PointerEventData(m_EventSystem)
        {
            position = Input.mousePosition
        };
        var results = new List<RaycastResult>();
        m_Raycaster.Raycast(m_PointerEventData, results);
        
        return results;
    }
}
