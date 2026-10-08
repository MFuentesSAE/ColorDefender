using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class CharacterEnemy : Character
{
    public EColorType colorType;
    public GameObject explosionFX;
    private Tweener moveTween;
    
    private const string MOVE = "Move";
    private const string FRESNEL = "_FresnelColor";

    private void OnEnable()
    {
        if (gameManager == null)
        {
            gameManager = GameManager.instance; 
        }
        
        gameManager.onGameOverEvent += StopMovement;
    }

    private void OnDisable()
    {
        gameManager.onGameOverEvent -= StopMovement;
    }

    public void Kill(EColorType attackerColor)
    {
        if (colorType == attackerColor)
        {
            base.Kill();
            gameManager.AddScore();
        }
    }

    private void SetRandomColor()
    {
        EColorType randomColor = (EColorType)Random.Range(0, System.Enum.GetValues(typeof(EColorType)).Length);
        colorType = randomColor;
        Color color = gameManager.GetColor(colorType);
        meshRenderer.material?.SetColor(FRESNEL, color);
        
    }

    private void Attack()
    {
        explosionFX?.SetActive(true);
        meshRenderer.enabled = false;
        gameManager.player.Kill();
    }

    private void StopMovement()
    {
        moveTween?.Kill();
        animator?.SetTrigger(IDLE);
    }

    protected override void DeathBehavior()
    {
        gameObject.SetActive(false);
    }

    public void Move(float moveTime)
    {
        SetRandomColor();
        animator?.SetTrigger(MOVE); 
        moveTween?.Kill();
        moveTween = transform.DOMove(gameManager.player.transform.position, moveTime);
    }

    void OnTriggerEnter(Collider other)
    {
        CharacterPlayer characterPlayer = other.gameObject.GetComponent<CharacterPlayer>();

        if (characterPlayer != null)
        {
            Attack();
        }
    }
}
