using UnityEngine;

namespace Eviecore
{
    public class FreeCamera : MonoBehaviour, EvieCoreDebugTool
    {
        private bool isActive = false;
        public GameObject camera;

        private void Start()
        {
            DEBUGHUDManager.Instance.AddDebugTool(this, "Free Camera");
        }

        public void OnActivated()
        {
            isActive = true;
            camera.SetActive(true);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void OnDeactivated()
        {
            isActive = false;
            camera.SetActive(false);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        public bool IsActive()
        {
            return isActive;
        }
    }
}