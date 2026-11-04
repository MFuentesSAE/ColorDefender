using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class UIFader : MonoBehaviour
{
	public Image fadeImage;
	public float fadeTime;
	public float delay = 0.5f;
	private Tween fadeTween;

	public UnityEvent onFadeInEvent;

	void Start()
	{
		fadeImage.raycastTarget = false;
		fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1);
		FadeOut();
	}

	public void FadeIn()
	{
		fadeTween?.Kill();
		fadeTween = fadeImage.DOFade(1, fadeTime).OnComplete(onFadeInEvent.Invoke);
			//.SetDelay(delay);
	}

	public void FadeOut()
	{
		fadeTween?.Kill();
		fadeTween = fadeImage.DOFade(0, fadeTime)
			.SetDelay(delay); ;
	}

}
