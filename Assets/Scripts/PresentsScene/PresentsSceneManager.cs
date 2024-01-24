using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PresentsSceneManager : MonoBehaviour
{
    [Header("Provide object that contains NPCs")]
    [SerializeField] private NPCContainer _npcs;

    [Header("Object with hints to player")]
    [SerializeField] private GameObject _helperNote;

    [Header("Used for elements of UI that needs hiding. Not necessary")]
    [SerializeField] private List<GameObject> _toHideUIElementsObjects;

    [Header("Ending scene canvas object")]
    [SerializeField] private GameObject _endingCanvas;
    
    [Header("Main scene canvas object")]
    [SerializeField] private GameObject _mainCanvas;

    [Header("Sounds for player choices")]
    [SerializeField] private AudioSource _correctAudio;
    [SerializeField] private AudioSource _incorrectAudio;

    public void SubmitAccept()
    {
        if (!_npcs.GetCurrentNPC().NPCInTriggerArea) return;
        var npcItemType = GetCurrentNPCPresentType();

        bool wereIncorrect = false;
        if (npcItemType != PresentsTypeEnum.PresentType.Acceptable)
        {
            wereIncorrect = true;
            PlayIncorrectSound();
            HideUI();
            _helperNote.SetActive(true);
        }
        else
            _npcs.DeactivateNPC();

        if (!wereIncorrect)
            TryFinish();
    }

    public void SubmitIgnore()
    {
        if (!_npcs.GetCurrentNPC().NPCInTriggerArea) return;
        var npcItemType = GetCurrentNPCPresentType();

        bool wereIncorrect = false;
        if (npcItemType != PresentsTypeEnum.PresentType.Ignore)
        {
            wereIncorrect = true;
            PlayIncorrectSound();
            HideUI();
            _helperNote.SetActive(true);
        }
        else
            _npcs.DeactivateNPC();

        if (!wereIncorrect)
            TryFinish();
    }

    public void SubmitReport()
    {
        if (!_npcs.GetCurrentNPC().NPCInTriggerArea) return;
        var npcItemType = GetCurrentNPCPresentType();

        bool wereIncorrect = false;
        if (npcItemType != PresentsTypeEnum.PresentType.Illegal)
        {
            wereIncorrect = true;
            PlayIncorrectSound();
            HideUI();
            _helperNote.SetActive(true);
        }
        else
            _npcs.DeactivateNPC();

        if (!wereIncorrect)
            TryFinish();
    }

    public void RestartNPCAudio() => _npcs.GetCurrentNPC().PlayNPCAudio();

    private void PlayCorrectSound() => _correctAudio.Play();
    private void PlayIncorrectSound() => _incorrectAudio.Play();

    private PresentsTypeEnum.PresentType GetCurrentNPCPresentType()
    {
        return _npcs
                    .GetCurrentNPC()
                    .GetNPCItem()
                    .GetPresentType();
    }

    private void HideUI()
    {
        _npcs.GetCurrentNPC().StopNPCAudio();
        if (_toHideUIElementsObjects == null) return;
        foreach (var element in _toHideUIElementsObjects)
        {
            element.SetActive(false);
        }
    }

    private void TryFinish()
    {
        PlayCorrectSound();
        if (_npcs.IsLastNPC())
            FinishScene();
    }

    private void FinishScene()
    {
        _endingCanvas.SetActive(true);
        _endingCanvas.GetComponent<AudioSource>().PlayDelayed(.5f);
        _mainCanvas.SetActive(false);
    }
}
