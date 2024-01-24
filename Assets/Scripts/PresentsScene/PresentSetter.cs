using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PresentSetter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _dialogueTextPlaceholder;

    private NPCItemContainer _item;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            _item = other.gameObject.GetComponent<NPCItemContainer>();
            var npc = other.gameObject.GetComponent<NPC>();
            npc.ChangeNPCState(true);
            npc.PlayNPCAudio();
            SetItem();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            var npc = other.gameObject.GetComponent<NPC>();
            npc.ChangeNPCState(false);
            DetachItem();
        }
    }

    private void SetItem()
    {
        if (_item == null)
        {
            Debug.LogError("No item were attached to the NPC");
            return;
        }

        _dialogueTextPlaceholder.text = _item.GetNPCDialogueText();
        _item.GetNPCPresentObject().SetActive(true);
    }

    private void DetachItem()
    {
        _item.GetNPCPresentObject().SetActive(false);
        _dialogueTextPlaceholder.text = "";
        _item = null;
    }
}
