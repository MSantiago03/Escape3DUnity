using System.Collections;
using UnityEngine;

public class DrawerController : MonoBehaviour
{
    

    [SerializeField]
    private float Speed = 1f;

    [Header("Pulling Configs")]
    [SerializeField]
    private float PullDistance = 1f;

    private Vector3 StartPosition;
    private Vector3 EndPosition;

    private Coroutine AnimationCoroutine;
    public bool IsOpen = false;

    private void Awake()
    {
        StartPosition = transform.position;
        EndPosition = StartPosition + transform.forward * PullDistance;
    }

    public void Pull()
    {
        if (!IsOpen)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            AnimationCoroutine = StartCoroutine(AnimatePull());
        }
    }

    public void Close()
    {
        if (IsOpen)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            AnimationCoroutine = StartCoroutine(AnimateClose());
        }
    }

    private IEnumerator AnimatePull()
    {
        IsOpen = true;

        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < 1)
        {
            transform.position = Vector3.Lerp(startPosition, EndPosition, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }

        transform.position = EndPosition;
    }

    private IEnumerator AnimateClose()
    {
        IsOpen = false;

        float time = 0;
        Vector3 endPosition = StartPosition;

        while (time < 1)
        {
            transform.position = Vector3.Lerp(EndPosition, endPosition, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }

        transform.position = endPosition;
    }
}
