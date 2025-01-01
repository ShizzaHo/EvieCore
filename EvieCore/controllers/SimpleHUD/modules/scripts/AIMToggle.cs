using NaughtyAttributes;
using System.Collections;
using UnityEngine;

public class AIMToggle : MonoBehaviour
{
    [Header("Basic")]
    public Transform aimFilled; // Объект, на который изменяется масштаб
    public float duration = 0.5f; // Длительность анимации в секундах

    private Coroutine scaleCoroutine;

    [Header("Message Settings")]
    public bool SendMessage = true;
    [ShowIf("SendMessage")]
    public string messageAIM = "AIMTrigger";

    private void Start()
    {
        aimFilled.localScale = Vector3.zero;

        if (SendMessage && !string.IsNullOrEmpty(messageAIM))
        {
            MessageManager.Instance.Subscribe<bool>(messageAIM, Message);
        }
        else
        {
            Debug.LogError("[EVIECORE/AIMTOGGLE/ERROR] messageAIM not specified.");
        }
    }

    public void FillAim()
    {
        // Останавливаем текущую корутину, если она есть
        if (scaleCoroutine != null)
            StopCoroutine(scaleCoroutine);

        // Запускаем анимацию увеличения масштаба
        scaleCoroutine = StartCoroutine(ScaleOverTime(Vector3.one * 1.5f));
    }

    public void UnfillAim()
    {
        // Останавливаем текущую корутину, если она есть
        if (scaleCoroutine != null)
            StopCoroutine(scaleCoroutine);

        // Запускаем анимацию уменьшения масштаба
        scaleCoroutine = StartCoroutine(ScaleOverTime(Vector3.zero));
    }

    private IEnumerator ScaleOverTime(Vector3 targetScale)
    {
        Vector3 initialScale = aimFilled.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Вычисляем новый масштаб с использованием SmoothStep для плавного изменения
            float t = elapsedTime / duration;
            aimFilled.localScale = Vector3.Lerp(initialScale, targetScale, Mathf.SmoothStep(0, 1, t));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Устанавливаем точный конечный масштаб (на случай неточностей при завершении)
        aimFilled.localScale = targetScale;
        scaleCoroutine = null;
    }

    public void Message(bool state)
    {
        if (state)
        {
            FillAim();
        }
        else
        {
            UnfillAim();
        }
    }
}