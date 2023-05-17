using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : BaseMonoBehavior
{
    private IRayProvider _rayProvider = new MainCameraRayCastProvider();
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

    }

    bool IsNewSelection(Transform selection)
    {
        return _currentSelection != selection;
    }
}
