using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//ȭ�� ��鸲 ���� ��ü
public class CameraController : Singleton<CameraController>
{
    private Coroutine cameraShakeCoroutine = null;//ȭ�� ��鸲 ���� �ڷ�ƾ
    //ȭ�� ��鸲 ȣ�� �Լ�
    public void OnShake(float duration, float shakeMagnitude)
    {
        if (cameraShakeCoroutine != null)
            StopCoroutine(cameraShakeCoroutine);
        Debug.Log("ShakeCamera");
        cameraShakeCoroutine = StartCoroutine(CameraShake(duration, shakeMagnitude));
    }

    //ȭ�� ��鸲 ���� �ڷ�ƾ �Լ�
    public IEnumerator CameraShake(float duration, float shakeMagnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;

        cameraShakeCoroutine = null;
    }
}
