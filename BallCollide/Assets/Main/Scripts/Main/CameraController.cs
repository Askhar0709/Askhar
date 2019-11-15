using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraType
{
    ConstDis,
    ConstX,
    BetweenX,
    WallX
}

public class CameraController : Singleton<CameraController>
{
    public static bool IsZoom = false;
    public CameraType type;
    public Transform followTf;
    [Header("Smooth")]
    public bool isSmooth = false;
    public float smoothSpeed = 5;
    [Header("Shake")]
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.1f;
    [Header("Zoom")]
    public float zoomDuration = 1;
    public float zoomMagnitude = 10;
    Vector3 offset, deltaOffset;
    void Start()
    {
        IsZoom = false;
        offset = transform.position - followTf.position;
        deltaOffset = Vector3.zero;
    }
    void Update()
    {
        if (A.IsPlaying && followTf.NotNull())
        {
            if (isSmooth)
            {
                transform.position = Vector3.Lerp(
                    transform.position,
                    offset + deltaOffset + followTf.position,
                    Time.deltaTime * smoothSpeed
                );
            }
            else
            {
                transform.position = offset + deltaOffset + followTf.position;
            }
        }
    }
    public void Shake()
    {
        StartCoroutine(Shake(shakeDuration, shakeMagnitude));
    }
    IEnumerator Shake(float duration, float magnitude)
    {
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            A.Cam.transform.localPosition = transform.TransformDirection(new Vector3(A.Rnd, A.Rnd, 0) * magnitude);
            yield return null;
        }
        A.Cam.transform.localPosition = Vector3.zero;
    }
    public void OffsetZoom(Vector3 pos)
    {
        StartCoroutine(OffsetZoom(pos, Vector3.Distance(transform.position, pos) / zoomMagnitude * zoomDuration));
    }
    IEnumerator OffsetZoom(Vector3 pos, float duration)
    {
        Vector3 deltaOffsetEnd = transform.position - (offset + followTf.position);
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            deltaOffset = Vector3.Lerp(deltaOffset, deltaOffsetEnd, t / duration);
            yield return null;
        }
        transform.position = pos;
    }
    public void Zoom(Vector3 pos)
    {
        StartCoroutine(Zoom(pos, Vector3.Distance(transform.position, pos) / zoomMagnitude * zoomDuration));
    }
    IEnumerator Zoom(Vector3 pos, float duration)
    {
        if (!IsZoom)
        {
            IsZoom = true;
            Vector3 startPos = transform.position;
            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                transform.position = Vector3.Lerp(startPos, pos, t / duration);
                yield return null;
            }
            transform.position = pos;
            IsZoom = false;
        }
    }
}