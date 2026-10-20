using System;
using System.Collections; //importar para usar corutinas
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;

    [SerializeField] 
    private Renderer renderer;
    
    private const float LIFETIME = 3;
    private Coroutine lifetimeRoutine;
    private EColorType colorType;
    private GameManager gameManager;
    
    void Start()
    {
        rb.useGravity = false;
        gameManager = GameManager.instance; 
    }

    public void Shoot(float force, EColorType colorType)
    {

        if (gameManager == null)
        {
            gameManager = GameManager.instance; 
        }
        
        this.colorType = colorType; 
        Color color = gameManager.GetColor(colorType);
        renderer.material?.SetColor("_FresnelColor", color);
        
        if (lifetimeRoutine != null)
        {
            StopCoroutine(lifetimeRoutine);
        }
        
        rb.AddForce(transform.forward * force, ForceMode.Impulse);
        lifetimeRoutine = StartCoroutine(LifeTimeRoutine());
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ProjectileCollidedWith: " + other.gameObject.name);   
        CharacterEnemy enemy = other.GetComponent<CharacterEnemy>();
        if (enemy != null)
        {
            enemy.Kill(colorType);
        }
        
        Destroy(gameObject);
    }

    IEnumerator LifeTimeRoutine()
    {
        yield return new WaitForSeconds(LIFETIME);
        Destroy(gameObject);
    }
}
