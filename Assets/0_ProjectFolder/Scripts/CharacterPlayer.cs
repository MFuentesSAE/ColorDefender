using System;
using UnityEngine;

public class CharacterPlayer : Character
{
    [SerializeField] private Transform shootTransform;

    [SerializeField] private float shootForce = 5;

    private Transform lastRotationTarget;

    private const string SHOOT = "Shoot";
    private const string DIE = "Die";

    [SerializeField] 
    private float fireRate = 0.25f;

    private float lastFireRate = 0;
    private bool rotationFlag = false;
    private bool blockInput = false;
    
    public void Shoot(Vector3 point, EColorType projectileColor, Transform shootPivot)
    {
        if (Time.time <= lastFireRate || blockInput)
        {
            return;
        }


        Action actionShoot = () =>
        {
            Projectile projectile = Instantiate(gameManager.projectilePrefab, shootTransform.position,
                shootTransform.rotation);
            projectile?.Shoot(shootForce, projectileColor);
            animator?.SetTrigger(SHOOT);
            lastFireRate = Time.time + fireRate;
        };

        if (lastRotationTarget == null || lastRotationTarget != shootPivot)
        {
            if (rotationFlag)
            {
                return;
            }
            
            rotationFlag = true;
            RotateCharacter(point, () =>
            {
                actionShoot.Invoke();
                lastRotationTarget = shootPivot;
                rotationFlag = false;
            });
            return;
        }

        actionShoot.Invoke();
    }

    protected override void DeathBehavior()
    {
        blockInput = true;
        animator?.SetTrigger(DIE);
        gameManager?.EndGame();
    }
}