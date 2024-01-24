using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] private InputAction _mouseClick;
    [SerializeField] private float _dragSpeed = 0f;
    [SerializeField] private int _dragLayer;
    
    private Camera _mainCamera;
    private Coroutine _coroutine;
    private Vector3 _velocity = Vector3.zero;
    private bool _isPanelOut = false;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        _mouseClick.Enable();
        _mouseClick.performed += MousePressed;
    }

    public void SwitchPanelState(bool state) => _isPanelOut = state;

    private void MousePressed(InputAction.CallbackContext context)
    {
        var ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (!Physics.Raycast(ray, out var hit) ||
            !hit.collider ||
            hit.collider.gameObject.layer.CompareTo(_dragLayer) != 0 ||
            hit.collider.gameObject.GetComponent<BaseDragItem>() == null)
            return;

        if (_coroutine != null) StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(DragUpdate(hit.collider.gameObject));
    }

    private IEnumerator DragUpdate(GameObject interactedObject)
    {
        interactedObject.TryGetComponent<BaseDragItem>(out var dragComponent);
        dragComponent.OnStartDrag();
        Cursor.visible = false;
        var initialDistance = Vector3.Distance(interactedObject.transform.position, _mainCamera.transform.position);
        while (_mouseClick.ReadValue<float>() != 0)
        {
            CheckIfOverMainPanel();
            var ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            interactedObject.transform.position = Vector3.SmoothDamp(interactedObject.transform.position, ray.GetPoint(initialDistance),
                ref _velocity, _dragSpeed);
            yield return null;
        }

        Cursor.visible = true;
        dragComponent.OnEndDrag();
    }

    private void CheckIfOverMainPanel()
    {
        var interactedObject = UIGraphicRaycaster.GetInstance().RaycastUIElement()
           .Where(hit => hit.gameObject.CompareTag("InventoryMainPanel"))
           .Select(hit => hit.gameObject)
           .FirstOrDefault();

        if (interactedObject == null)
            return;

        AnimatePanelOut(interactedObject);
    }

    private void AnimatePanelOut(GameObject panel)
    {
        if (_isPanelOut) return;
        var canvas = panel.transform.parent.gameObject;
        var anim = canvas.GetComponent<Animator>();
        anim.SetTrigger(AnimatorParameterIdList.Out);
        _isPanelOut = true;
    }

    private void OnDisable()
    {
        _mouseClick.performed -= MousePressed;
        _mouseClick.Disable();
    }
}
