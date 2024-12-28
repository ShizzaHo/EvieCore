using UnityEngine;

public class SimpleTool : MonoBehaviour, EvieCoreDebugTool
{

    private bool isActive = false;

    private void Start()
    {
        DEBUGHUDManager.Instance.AddDebugTool(this, "Simple Tool");
    }

    public void OnActivated()
    {
        isActive = true;
    }

    public void OnDeactivated()
    {
        isActive = false;

    }

    public bool IsActive()
    {
        return isActive;
    }
}
