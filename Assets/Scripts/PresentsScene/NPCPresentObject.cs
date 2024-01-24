using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPresentObject : MonoBehaviour
{
    [Header("Place your NPC present Game object")]
    [SerializeField] private GameObject _presentObject;
    
    [Space]
    [Header("Select present type")]
    [SerializeField] private PresentsTypeEnum.PresentType _presentType = PresentsTypeEnum.PresentType.Acceptable;

    public GameObject GetNPCPresentObject() => _presentObject;
    public PresentsTypeEnum.PresentType GetPresentType() => _presentType;

    private void Awake()
    {
        if (_presentObject == null)
            _presentObject = new GameObject("EMPTY_PRESENT_GAMEOBJECT");
    }
}
