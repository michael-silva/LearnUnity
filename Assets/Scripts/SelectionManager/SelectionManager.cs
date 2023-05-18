using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : BaseMonoBehavior
{
    [InjectOnAwake]
    private IRayProvider _rayProvider;
    [InjectOnAwake]
    private ISelector _selector;
    [InjectOnAwake]
    private ISelectionResponse _selectionResponse;

    private Transform _currentSelection;

    void Update()
    {
        _selector.Check(_rayProvider.CreateRay());
        var selection = _selector.GetSelection();
        if (IsNewSelection(selection))
        {
            if (_currentSelection) _selectionResponse.OnDeselect(_currentSelection);
            if (selection) _selectionResponse.OnSelect(selection);
        }
        _currentSelection = selection;
        if (_currentSelection && Input.GetMouseButtonDown(0))
        {
            var interaction = _currentSelection.GetComponent<IInteraction>();
            if (interaction != null)
            {
                interaction.Interact();
            }
        }

    }

    bool IsNewSelection(Transform selection)
    {
        return _currentSelection != selection;
    }
}
