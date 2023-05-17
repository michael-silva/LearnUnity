using UnityEngine;

public class MainCameraRayCastProvider : MonoBehaviour, IRayProvider
{
    public Ray CreateRay()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return ray;
    }
}