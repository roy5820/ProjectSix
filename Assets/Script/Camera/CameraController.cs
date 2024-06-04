using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//화면 흔들림 구현 객체
public class CameraController : Singleton<CameraController>
{
    private Coroutine cameraShakeCoroutine = null;//화면 흔들림 구현 코루틴
    //화면 흔들림 호출 함수
    public void OnShake(float duration, float shakeMagnitude)
    {
        if (cameraShakeCoroutine != null)
            StopCoroutine(cameraShakeCoroutine);
        Debug.Log("ShakeCamera");
        cameraShakeCoroutine = StartCoroutine(CameraShake(duration, shakeMagnitude));
    }

    //화면 흔들림 구현 코루틴 함수
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
