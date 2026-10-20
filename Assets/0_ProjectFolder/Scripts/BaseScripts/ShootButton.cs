using System;
using UnityEngine;

public class ShootButton : MonoBehaviour
{
    [SerializeField]
    private EColorType colorType;
    
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField] 
    private Transform pivot;
    
    private CharacterPlayer player;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;

        if (gameManager == null)
        {
            return;
        }
        
        player = gameManager.player;
        spriteRenderer.color = gameManager.GetColor(colorType);
    }

    private void OnMouseDown()
    {
        player.Shoot(transform.position, colorType, pivot);
    }
}
