using System.Collections;
using UnityEngine;

public class AIMToggle : MonoBehaviour
{
    public Transform aimFilled; // Объект, на который изменяется масштаб
    public float duration = 0.5f; // Длительность анимации в секундах

    private Coroutine scaleCoroutine;

    private void Start()
    {
        aimFilled.localScale = Vector3.zero;
    }

    public void fillAim()
    {
        // Останавливаем текущую корутину, если она есть
        if (scaleCoroutine != null)
            StopCoroutine(scaleCoroutine);

        // Запускаем анимацию увеличения масштаба
        scaleCoroutine = StartCoroutine(ScaleOverTime(Vector3.one * 1.5f));
    }

    public void unfillAim()
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
}