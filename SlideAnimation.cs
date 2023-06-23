using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideAnimation : MonoBehaviour
{
    [SerializeField] float openDistance, speed;
    Vector3 startPos;

    private void Start()
    {
        startPos = transform.localPosition;
    }

    public void Open()
    {
        StopAllCoroutines();
        StartCoroutine(Anim(true));
    }

    public void Close()
    {
        StopAllCoroutines();
        StartCoroutine(Anim(false));
    }

    IEnumerator Anim(bool open)
    {
        Vector3 targetPos = startPos + (open ? Vector3.down * openDistance : Vector3.zero);
        while (true)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPos, speed * Time.deltaTime);
            yield return null;
        }
    }
}
