using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCContainer : MonoBehaviour
{
    [SerializeField] private List<NPC> _NPCObjects;
    private int _length;
    private int _cursor;
    

    private void Start()
    {
        _length = _NPCObjects.Count;
        _cursor = 0;
        foreach (var npc in _NPCObjects)
            npc.gameObject.SetActive(false);
    }

    public void ActivateNextNPC()
    {
        if (_cursor >= _length) return;
        _NPCObjects[_cursor].gameObject.SetActive(true);
        _NPCObjects[_cursor].StartNPC();
    }

    public void DeactivateNPC()
    {
        _NPCObjects[_cursor].BeginExitState();
        _cursor++;
        if (_cursor < _length) 
            ActivateNextNPC();
    }

    public NPC GetCurrentNPC() => _NPCObjects[_cursor];
    public bool IsLastNPC() => _cursor >= _length;
}