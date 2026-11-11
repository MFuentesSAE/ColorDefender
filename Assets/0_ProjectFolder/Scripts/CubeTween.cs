using DG.Tweening;
using UnityEngine;

public class CubeTween : MonoBehaviour
{
    [Header("MoveTween")]
    public float moveTime;
    public Transform destination;
    public KeyCode moveKey;


    [Header("FlickerTween")]
    public float flickerTime;
    public Color flickerColor;
    public int loop;
    public Renderer rend;

    public KeyCode flickerKey;
    private Tween flickerTween;

    void Update()
    {
        if (Input.GetKeyDown(moveKey))
        {
            Move();
        }

        if (Input.GetKeyDown(flickerKey))
        {
            Flicker();
        }
    }

    void Move()
    {
        transform.DOMove(destination.position, moveTime);
    }

    void Flicker()
    {
        //if(flickerTween != null)
        //{
        //    flickerTween.Kill();
        //}

        flickerTween?.Kill(true);

        flickerTween = rend.material.DOColor(flickerColor, flickerTime)
            .SetLoops(loop, LoopType.Yoyo);
    }
}
