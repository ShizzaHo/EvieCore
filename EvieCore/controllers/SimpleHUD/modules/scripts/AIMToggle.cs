using System.Collections;
using UnityEngine;

public class AIMToggle : MonoBehaviour
{
    public Transform aimFilled; // ������, �� ������� ���������� �������
    public float duration = 0.5f; // ������������ �������� � ��������

    private Coroutine scaleCoroutine;

    private void Start()
    {
        aimFilled.localScale = Vector3.zero;
    }

    public void fillAim()
    {
        // ������������� ������� ��������, ���� ��� ����
        if (scaleCoroutine != null)
            StopCoroutine(scaleCoroutine);

        // ��������� �������� ���������� ��������
        scaleCoroutine = StartCoroutine(ScaleOverTime(Vector3.one * 1.5f));
    }

    public void unfillAim()
    {
        // ������������� ������� ��������, ���� ��� ����
        if (scaleCoroutine != null)
            StopCoroutine(scaleCoroutine);

        // ��������� �������� ���������� ��������
        scaleCoroutine = StartCoroutine(ScaleOverTime(Vector3.zero));
    }

    private IEnumerator ScaleOverTime(Vector3 targetScale)
    {
        Vector3 initialScale = aimFilled.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // ��������� ����� ������� � �������������� SmoothStep ��� �������� ���������
            float t = elapsedTime / duration;
            aimFilled.localScale = Vector3.Lerp(initialScale, targetScale, Mathf.SmoothStep(0, 1, t));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ������������� ������ �������� ������� (�� ������ ����������� ��� ����������)
        aimFilled.localScale = targetScale;
        scaleCoroutine = null;
    }
}