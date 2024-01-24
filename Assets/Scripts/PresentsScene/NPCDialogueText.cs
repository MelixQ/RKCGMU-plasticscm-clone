using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueText : MonoBehaviour
{
    [Header("Place your NPC's dialogue text. Supports raw txt or Unity TextAsset file")]
    [SerializeField] private TextAsset _text;
    public string GetNPCDialogueText() => _text.text;
    
    private void Awake()
    {
        if (_text == null) 
            _text = new TextAsset("No NPC text were provided via Text field.");
    }
}
