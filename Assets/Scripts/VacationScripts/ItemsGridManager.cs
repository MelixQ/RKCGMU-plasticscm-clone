using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemsGridManager : MonoBehaviour
{
    [Header("Slots for InventoryGridManager objects")]
    [SerializeField] private InventoryGridManager[] _grids;

    [Space] [Header("Slot for Items Container Object")]
    [SerializeField] private GameObject _itemsContainerObject;

    [Space] [Header("Slot for Ending Game Canvas and Inventory Canvas")]
    [SerializeField] private GameObject _endGameCanvas;
    [SerializeField] private GameObject _inventoryCanvas;

    [Space] [Header("Slots for correct and incorrect audio")]
    [SerializeField] private AudioSource _correctAudio;
    [SerializeField] private AudioSource _incorrectAudio;

    public void ResetSolution()
    {
        foreach (var mgr in _grids)
            mgr.RemoveAllItems();
    }

    public void SubmitSolution()
    {
        var isCorrect = true;
        foreach (var mgr in _grids)
        {
            var check = mgr.TrySubmit();
            if (!check)
                isCorrect = false;
        }

        if (!isCorrect)
            _incorrectAudio.Play();
        else
        {
            if (_itemsContainerObject.transform.childCount > 0)
                _incorrectAudio.Play();
            else
                FinishScene();
        }
    }

    private void FinishScene()
    {
        _correctAudio.Play();
        _endGameCanvas.SetActive(true);
        _endGameCanvas.GetComponent<AudioSource>().PlayDelayed(.5f);
        _inventoryCanvas.SetActive(false);
    }
}
