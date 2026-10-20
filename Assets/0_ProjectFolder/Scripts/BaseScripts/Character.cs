using UnityEngine;
using DG.Tweening;
using System;
public class Character : MonoBehaviour
{
    [SerializeField] 
    protected Hp hp;
    
    [SerializeField]
    protected Animator animator;
    
    [SerializeField]
    protected Renderer meshRenderer;

    protected Tween rotTween;
    protected float tweenTime = 0.2f;
    protected const string IDLE = "Idle";
    protected GameManager gameManager;

    protected virtual void Start()
    {
        gameManager = GameManager.instance;
        hp.onDeathEvent.AddListener(DeathBehavior);
    }

    public virtual void RotateCharacter(Vector3 point, Action endRotationCallback = null)
    {
        Vector3 dir = point - transform.position;
        rotTween?.Kill();
        rotTween = transform.DOLookAt(dir, tweenTime).OnComplete(()=>endRotationCallback?.Invoke());
    }

    public virtual void Kill()
    {
        hp?.InstaKill();
    }

    protected virtual void DeathBehavior()
    {
        
    }
}
