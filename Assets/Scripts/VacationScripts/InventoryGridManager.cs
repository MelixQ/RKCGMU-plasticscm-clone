using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IItem))]
public class InventoryGridManager : MonoBehaviour
{
    [Header("Slot for InventoryItems")]
    [SerializeField] private InventoryItem[] _items;

    private string _itemsTypeName;
    private int _cursor;

    private void Start()
    {
        _itemsTypeName = GetComponent<IItem>().GetDraggableItemTypeName();
        foreach (var item in _items) 
            item.gameObject.SetActive(false);   
    }

    public void SetItem(GameObject itemObject)
    {
        if (_cursor >= _items.Length)
        {
            Debug.LogError($"Cannot add more objects. Reached maximum amount ({_items.Length} items)");
            return;
        }
        
        ConfigureItem(itemObject);
    }

    public void RemoveItemFromInventory(GameObject itemSlot)
    {
        if (!itemSlot.activeSelf) return;
        itemSlot.GetComponent<InventoryItem>().GetItem().SetActive(true);
        var itemObject = itemSlot.GetComponentInChildren<BaseDragItem>();
        DetachItemObject(itemObject);
        CascadeUpdateInventoryUI(itemSlot);
    }

    public void RemoveAllItems()
    {
        for (int i = _cursor - 1; i >= 0; i--)
            RemoveItemFromInventory(_items[_cursor - 1].gameObject);
    }

    public bool TrySubmit()
    {
        var isCorrect = true;
        for (int i = _cursor - 1; i >= 0; i--)
        {
            var check = IsItemHasCorrectType(_items[i]);
            if (!check)
            {
                RemoveItemFromInventory(_items[i].gameObject);
                isCorrect = false;
            }
        }

        return isCorrect;
    }

    private bool IsItemHasCorrectType(InventoryItem item)
    {
        return _itemsTypeName == item.GetItemTypeName();
    }

    private void ConfigureItem(GameObject itemObject)
    {
        var itemSlot = _items[_cursor];
        itemSlot.gameObject.SetActive(true);
        itemSlot.SetItem(itemObject);
        _cursor++;
    }

    private void DetachItemObject(BaseDragItem item) => 
        item.ResetPropertiesToDefault();

    private void CascadeUpdateInventoryUI(GameObject toDeleteItemSlot)
    {
        var toDeleteInventoryItem = toDeleteItemSlot.GetComponent<InventoryItem>();
        var startIndex = Array.IndexOf(_items, toDeleteInventoryItem);
        toDeleteInventoryItem.ResetItem();
        for (int i = startIndex; i < _cursor - 1; i++)
            _items[i].SetItem(_items[i + 1].GetItem());
        
        _cursor--;
        var lastActiveItemSlot = _items[_cursor];
        lastActiveItemSlot.ResetItem();
        lastActiveItemSlot.gameObject.SetActive(false);
    }
}
