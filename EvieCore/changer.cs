using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changer : MonoBehaviour
{
    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void edited()
    {
        string text = GameObject.Find("PIIIZDA").GetComponent<TMP_InputField>().text;

        DataManager.Instance.SetData<string>("42bratuha", text);
    }

    public void Start()
    {
        GameObject.Find("PIIIZDA").GetComponent<TMP_InputField>().text = DataManager.Instance.GetData<string>("42bratuha");
    }
}
