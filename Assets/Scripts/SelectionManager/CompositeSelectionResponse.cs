using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CompositeSelectionResponse : MonoBehaviour, ISelectionResponse
{
    private List<ISelectionResponse> _selectionResponses = new List<ISelectionResponse>();
    private int _currentIndex = 0;

    [SerializeField] private GameObject _selectionResponsesHolder;

    private void Start()
    {
        var selections = _selectionResponsesHolder.GetComponents<ISelectionResponse>().ToList();
    }

    [ContextMenu("Next")]
    public void Next()
    {
        _currentIndex = _currentIndex + 1 % _selectionResponses.Count;
    }

    public void OnDeselect(Transform selection)
    {
        if (!HasSelection()) return;

        _selectionResponses[_currentIndex].OnDeselect(selection);
    }

    public void OnSelect(Transform selection)
    {
        if (!HasSelection()) return;

        _selectionResponses[_currentIndex].OnDeselect(selection);
    }

    bool HasSelection()
    {
        return _currentIndex > -1 && _currentIndex < _selectionResponses.Count;
    }
}