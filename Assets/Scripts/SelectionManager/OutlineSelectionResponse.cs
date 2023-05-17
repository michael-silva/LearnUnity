using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineSelectionResponse : BaseMonoBehavior, ISelectionResponse
{
    private void Start()
    {

    }

    public void OnDeselect(Transform selection)
    {
        var outline = selection.GetComponent<Outline>();
        if (!outline) return;
        outline.OutlineWidth = 0;
    }

    public void OnSelect(Transform selection)
    {
        var outline = selection.GetComponent<Outline>();
        if (!outline) return;
        outline.OutlineWidth = 10;
    }
}
