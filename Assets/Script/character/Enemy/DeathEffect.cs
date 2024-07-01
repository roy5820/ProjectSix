using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    public float playEffectSpeed = 0.1f;
    public List<GameObject> effects = new List<GameObject>();
    public GameObject cutSecenCamera = null;
    public float shakeMagnitude = 0.3f;
    private void Start()
    {
        StartCoroutine(PlayerEffect());
    }

    IEnumerator PlayerEffect()
    {
        for(int i = 0; i < effects.Count; i++)
        {
            effects[i].SetActive(true);
            yield return new WaitForSeconds(playEffectSpeed);
        }
    }

    private void Update()
    {
        Vector3 originalPos = cutSecenCamera.transform.localPosition;

        float x = Random.Range(-1f, 1f) * shakeMagnitude;
        float y = Random.Range(-1f, 1f) * shakeMagnitude;

        cutSecenCamera.transform.localPosition = new Vector3(x, y, originalPos.z);
    }
}
