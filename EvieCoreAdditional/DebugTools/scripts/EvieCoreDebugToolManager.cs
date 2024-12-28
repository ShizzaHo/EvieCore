using UnityEngine;
using UnityEngine.Events;

public class EvieCoreDebugToolManager : MonoBehaviour
{
    public string itWorkingInStage = "playing";
    public string whichStageIsActivated = "debug";

    private CursorLockMode resetCLM;
    private bool resetCursorVisible;


    private void Start()
    {
        if (StateManager.Instance == null)
        {
            Debug.LogError($"[EVIECORE/ERROR] StateManager not found! Make sure it is added to the scene before using it {gameObject.name}.");
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F10))
        {
            if (StateManager.Instance.IsCurrentState(itWorkingInStage))
            {
                ActivateDebug();

                StateManager.Instance.SetState(whichStageIsActivated);
            }
            else if (StateManager.Instance.IsCurrentState(whichStageIsActivated))
            {
                DeactivateDebug();

                StateManager.Instance.SetState(itWorkingInStage);
            }
        }

        if (!StateManager.Instance.IsCurrentState("debug")) return;

        if (Input.GetKeyDown(KeyCode.F9))
        {
            ShowHideDebugUI();
        }

        if (Input.GetKeyDown(KeyCode.F11))
        {
            ShowHideCursor();
        }
    }

    public void ActivateDebug()
    {
        CanvasGroup CanvasGroup = GetComponent<CanvasGroup>();
        CanvasGroup.alpha = 1.0f;
        CanvasGroup.interactable = true;

        resetCursorVisible = Cursor.visible;
        resetCLM = Cursor.lockState;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void DeactivateDebug()
    {
        DEBUGHUDManager.Instance.DeactivateAll();

        CanvasGroup CanvasGroup = GetComponent<CanvasGroup>();
        CanvasGroup.alpha = 0f;
        CanvasGroup.interactable = false;
        Cursor.visible = resetCursorVisible;
        Cursor.lockState = resetCLM;
    }

    public void ShowHideDebugUI()
    {
        CanvasGroup CanvasGroup = GetComponent<CanvasGroup>();

        if (CanvasGroup.alpha == 1)
        {
            CanvasGroup.alpha = 0;
        }
        else
        {
            CanvasGroup.alpha = 1;
        }
    }

    public void ShowHideCursor()
    {
        if (Cursor.visible == true)
        {
            Cursor.visible = false;
        } else
        {
            Cursor.visible = true;
        }
    }
}
