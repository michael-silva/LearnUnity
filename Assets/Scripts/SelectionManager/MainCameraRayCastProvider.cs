using UnityEngine;

public class MainCameraRayCastProvider : IRayProvider
{
    public Ray CreateRay()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return ray;
    }
}