using TMPro;
using UnityEngine;

namespace Eviecore
{
    public class ECD_SMandMM : MonoBehaviour, EvieCoreUpdate
    {
        void Start()
        {
            GameObject.Find("PauseUI").GetComponent<CanvasGroup>().alpha = 0;

            UpdateManager.Instance.Register(this);

            MessageManager.Instance.Subscribe("ChangeGameState", onChangeGameState);
        }
        public void OnUpdate()
        {
            string gameState = StateManager.Instance.GetCurrentState();

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameObject.Find("PauseUI").GetComponent<CanvasGroup>().alpha = 1;

                StateManager.Instance.SetState("paused");
                MessageManager.Instance.SendMessage("ChangeGameState");
            }

            if (Input.GetKeyDown(KeyCode.F1))
            {
                GameObject.Find("PauseUI").GetComponent<CanvasGroup>().alpha = 0;

                StateManager.Instance.SetState("playing");
                MessageManager.Instance.SendMessage("ChangeGameState");
            }
        }

        public void onChangeGameState()
        {
            GetComponent<TMP_Text>().text = StateManager.Instance.GetCurrentState();
        }

        void OnDestroy()
        {
            UpdateManager.Instance.Unregister(this);
        }
    }
}