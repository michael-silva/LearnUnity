using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightSelectionResponse : BaseMonoBehavior, ISelectionResponse
{
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material highlightMaterial;

    public void OnDeselect(Transform selection)
    {
        var selectionRenderer = selection.GetComponent<Renderer>();
        if (!selectionRenderer) return;
        selectionRenderer.material = defaultMaterial;
    }

    public void OnSelect(Transform selection)
    {
        var selectionRenderer = selection.GetComponent<Renderer>();
        if (!selectionRenderer) return;
        selectionRenderer.material = highlightMaterial;
    }
}
