using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveItem : MonoBehaviour, IItem
{
    public string GetDraggableItemTypeName() => nameof(ToLeaveDragItem);
}
