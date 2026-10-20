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

        if (lastRotationTarget == null || lastRotationTarget != shootPivot)
        {
            if (rotationFlag)
            {
                return;
            }
            
            rotationFlag = true;
            RotateCharacter(point, () =>
            {
				Shoot(projectileColor);
                lastRotationTarget = shootPivot;
                rotationFlag = false;
            });
            return;
        }

		Shoot(projectileColor);
    }

    private void Shoot(EColorType projectileColor)
    {
        //Crear el proyectil
		Projectile projectile = Instantiate(gameManager.projectilePrefab, shootTransform.position,shootTransform.rotation);

        //Dispar el proyectil
		projectile?.Shoot(shootForce, projectileColor);
		animator?.SetTrigger(SHOOT);
		lastFireRate = Time.time + fireRate;
	}

    protected override void DeathBehavior()
    {
        blockInput = true;
        animator?.SetTrigger(DIE);
        gameManager?.EndGame();
    }
}