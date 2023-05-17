using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineSelectionResponse : BaseMonoBehavior, ISelectionResponse
{
    private void Start()
    {

    }
    private Outline GetOutlineComponent(Transform selection)
    {
        return selection.GetComponent<Outline>() ?? selection.GetComponentInChildren<Outline>();
    }

    public void OnDeselect(Transform selection)
    {
        var outline = GetOutlineComponent(selection);
        if (!outline) return;
        outline.OutlineWidth = 0;
    }

    public void OnSelect(Transform selection)
    {
        var outline = GetOutlineComponent(selection);
        if (!outline) return;
        outline.OutlineWidth = 10;
    }
}
