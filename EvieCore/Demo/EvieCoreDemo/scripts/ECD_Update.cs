using TMPro;
using UnityEngine;

namespace Eviecore
{
    public class ECD_Update : MonoBehaviour, EvieCoreUpdate
    {
        public int value = 0;

        void Start()
        {
            UpdateManager.Instance.Register(this);
        }

        public void OnUpdate()
        {
            value++;

            GetComponent<TMP_Text>().text = value.ToString();
        }

        void OnDestroy()
        {
            UpdateManager.Instance.Unregister(this);
        }
    }
}