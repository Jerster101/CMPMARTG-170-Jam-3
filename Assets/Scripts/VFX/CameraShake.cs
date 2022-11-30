using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    Camera cam = null;

    [SerializeField]
    bool shakeOnStart = false;

    [SerializeField]
    float shakeDuration = 0.5f;
    [SerializeField]
    float shakeAmount = 0.5f;

    float shakeTime = -1.0f;

    Vector3 origPos;

    // Start is called before the first frame update
    void Start()
    {
        if (cam == null)
        {
            if (GetComponent<Camera>())
                cam = GetComponent<Camera>();
            else
                cam = Camera.main;
        }

        if (shakeOnStart)
            StartShake();
    }

    // -1 means use the member values
    public void StartShake(float duration = -1.0f, float amount = -1.0f)
    {
        if (duration != -1.0f)
            shakeDuration = duration;
        if (amount != -1.0f)
            shakeAmount = duration;

        shakeTime = shakeDuration;
        origPos = cam.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeTime != -1.0f)
        {
            if (shakeTime > 0.0f)
            {
                cam.transform.localPosition = origPos + (Random.insideUnitSphere * shakeAmount * (shakeTime / shakeDuration));

                shakeTime -= Time.deltaTime;
            }
            else
            {
                cam.transform.localPosition = origPos;
                shakeTime = -1.0f;
            }
        }
    }
}
