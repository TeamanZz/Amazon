using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Piston : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IEPlayAnimation());
    }

    private IEnumerator IEPlayAnimation()
    {
        transform.DOLocalMoveY(0.5f, 0.5f).SetEase(Ease.InOutBack);
        yield return new WaitForSeconds(0.6f);
        transform.DOLocalMoveY(0f, 0.5f).SetEase(Ease.InOutBack);
        yield return new WaitForSeconds(0.6f);
        yield return IEPlayAnimation();
    }
}