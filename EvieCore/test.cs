using UnityEngine;
using UnityEngine.SceneManagement;

public class test : MonoBehaviour, EvieCoreUpdate
{
    void Start()
    {
        UpdateManager.Instance.Register(this);
    }

    void OnDestroy()
    {
        UpdateManager.Instance.Unregister(this);
    }

    public void OnUpdate()
    {
        gameObject.transform.Rotate(new Vector3(1, 1, 1));
    }

    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
