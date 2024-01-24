using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    private TextMeshProUGUI _itemName;
    private GameObject _item;

    private void Awake()
    {
        _itemName = GetComponent<TextMeshProUGUI>();
    }

    public void SetItem(GameObject item)
    {
        _item = item;
        _itemName.text = item.name;
        item.transform.parent = gameObject.transform;
        item.SetActive(false);
    }

    public void ResetItem() => _item = null;

    public GameObject GetItem()
    {
        if (_item == null)
            Debug.LogError("No item object attached to this itemSlot");
        return _item;
    }

    public string GetItemTypeName()
    {
        if (!_item.TryGetComponent<BaseDragItem>(out var dragComponent))
            Debug.LogError("No item object attached to this itemSlot");

        var name = dragComponent.GetType().Name;
        return name;
    }
}
