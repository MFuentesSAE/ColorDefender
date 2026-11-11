using UnityEngine;
using DG.Tweening;

public class CubeTweener : MonoBehaviour
{
    [Header("MoveTween")]
    public Transform destination;
    public float moveTime;
    public KeyCode testMoveKey;
    private Tween moveTween;

    [Header("FlickrTween")]
    public Renderer meshRenderer;
    public Color flickrColor = Color.red;
    public float flickrTime;
    public int loops = 4;
	public KeyCode testFlickrKey;
    private Tween flickrTween;



	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(testMoveKey))
        {
            MoveTween();
        }

		if (Input.GetKeyDown(testFlickrKey))
		{
			FlickrTween();
		}
	}

    private void MoveTween()
    {
        transform.DOMove(destination.position, moveTime);
    }

    private void FlickrTween()
    {
        flickrTween?.Kill(true);
		flickrTween = meshRenderer.material.DOColor(flickrColor, flickrTime)
            .SetLoops(loops, LoopType.Yoyo);
	}
}
