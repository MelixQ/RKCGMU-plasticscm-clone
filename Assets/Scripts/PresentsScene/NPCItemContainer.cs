using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(
    typeof(NPCDialogueText),
    typeof(NPCPresentObject))]
public class NPCItemContainer : MonoBehaviour
{
    private NPCDialogueText _npcDialogueText;
    private NPCPresentObject _npcPresentObject;

    private void Awake()
    {
        _npcDialogueText = GetComponent<NPCDialogueText>(); 
        _npcPresentObject = GetComponent<NPCPresentObject>();
    }

    public string GetNPCDialogueText() => _npcDialogueText.GetNPCDialogueText();
    public GameObject GetNPCPresentObject() => _npcPresentObject.GetNPCPresentObject();
    internal PresentsTypeEnum.PresentType GetPresentType() => _npcPresentObject.GetPresentType();
}