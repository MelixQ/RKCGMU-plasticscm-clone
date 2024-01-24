using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseDragItem : MonoBehaviour
{
    protected bool _isFinished = false;
    protected Outline _outline;

    private Transform _initialParent;
    private Vector3 _initialPosition;

    private void Awake()
    {
        _initialPosition = transform.position;
        _initialParent = transform.parent;
        _outline = GetComponent<Outline>();
    }

    public virtual void OnStartDrag()
    {
        _outline.OutlineColor = Color.red;
        _outline.OutlineWidth = 4;
    }

    public virtual void OnEndDrag()
    {
        var interactedObject = UIGraphicRaycaster.GetInstance().RaycastUIElement()
            .Where(hit => hit.gameObject.CompareTag("RaycastInventoryPanel"))
            .Select(hit => hit.gameObject)
            .FirstOrDefault();

        if (interactedObject == null)
        {
            _isFinished = false;
            ResetPropertiesToDefault();
            return;
        }

        interactedObject.GetComponent<InventoryGridManager>().SetItem(gameObject);
    }

    public void ResetPropertiesToDefault()
    {
        _outline.OutlineColor = Color.green;
        _outline.OutlineWidth = 4;
        transform.position = _initialPosition;
        transform.parent = _initialParent;
        if (gameObject.activeSelf == false)
            gameObject.SetActive(true);
    }
}
