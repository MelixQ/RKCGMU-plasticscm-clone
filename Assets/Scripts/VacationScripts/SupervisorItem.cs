using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupervisorItem : MonoBehaviour, IItem
{
    public string GetDraggableItemTypeName() => nameof(ToSupervisorDragItem);
}
