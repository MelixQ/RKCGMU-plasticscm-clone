using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeItem : MonoBehaviour, IItem
{
    public string GetDraggableItemTypeName() => nameof(ToTakeDragItem);
}
