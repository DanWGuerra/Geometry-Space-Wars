using Apamate.BulletHellSystem;
using UnityEngine;

public class WeirdBullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private float maxLife = 5f; 

    [Header("Destruction")]
    [SerializeField] private bool canBeDestroyed = true; //Determina si la bala puede ser destruida por una bala del jugador

    [Header("Colors")]
    [SerializeField] private SpriteRenderer spriteRenderer; //render del sprite de la bala, para cambiar su color dependiendo de si puede ser destruida o no
    [SerializeField] private Color destroyableColor = Color.cyan; 
    [SerializeField] private Color indestructibleColor = Color.red; //color de la bala cuando no puede ser destruida

    public bool CanBeDestroyed => canBeDestroyed; //Permite que otros scripts puedan consultar si la bala puede ser destruida para => es una propiedad de solo lectura que devuelve el valor de la variable canBeDestroyed, indicando si la bala puede ser destruida o no
    public Vector2 Velocity {get; private set;}
    private float lifeTime; //tiempo que lleva activa la bala
    private Transform enemyReference; //guarda una referencia al enemigo que creó la bala
    private Vector3 previousenemyReferencePosition; //guarda la posición anterior del enemigo para calcular cuánto se movió

    public void Initialize(Transform bulletenemyReference, Vector2 velocity, bool destroyable) // Inicializa la bala con su dueño, velocidad y posibilidad de ser destruida
    {
        enemyReference = bulletenemyReference; // Guarda la referencia al enemigo que creó la bala
        Velocity = velocity; //la velocidad inicial de la bala
        canBeDestroyed = destroyable; // Establece si esta bala específica puede ser destruida
        lifeTime = 0f; //reinicia el contador de tiempo de vida cada vez que la bala es reutilizada
        ColorBullet(); //actualiza el color de la bala dependiendo de si puede ser destruida o no
        if (enemyReference != null)
        {
            previousenemyReferencePosition = enemyReference.position; //Si existe un enemigo dueño, guarda su posición inicial
        }
        else
        {
            previousenemyReferencePosition = transform.position; //Si no existe un dueño, utiliza la posición actual de la bala
        }
    }

    private void Update()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime >= maxLife) //Comprueba si la bala ha alcanzado su tiempo máximo de vida
        {
            Disable(); //Desactiva la bala cuando supera su tiempo máximo de vida
            return; //Detiene el resto del código de Update en este frame
        }
        Vector3 enemyReferenceMovement = Vector3.zero; // Inicializa el movimiento del enemigo en cero

        if (enemyReference != null) //Comprueba si la bala todavía tiene un enemigo como dueño
        {
            enemyReferenceMovement = enemyReference.position - previousenemyReferencePosition; //calcula cuánto se movió el enemigo desde el frame anterior
            previousenemyReferencePosition = enemyReference.position; //Guarda la posición actual del enemigo para calcular el siguiente movimiento
        }
        Vector3 bulletMovement = (Vector3)(Velocity * Time.deltaTime); //Calcula el desplazamiento de la bala utilizando su velocidad y el tiempo transcurrido // Declara el movimiento que tendrá la bala durante este frame
        transform.position += enemyReferenceMovement + bulletMovement; //Mueve la bala combinando el movimiento del enemigo y su propio movimiento
    }

    private void ColorBullet()
    {
        if (spriteRenderer == null) //Comprueba si la referencia al SpriteRenderer está vacía
            return; //Detiene el método si no existe un SpriteRenderer asignado

        if (canBeDestroyed)
        {
            spriteRenderer.color = destroyableColor; //Si la bala puede destruirse, cambia su color al color de bala destruible
        }
        else
        {
            spriteRenderer.color = indestructibleColor; //Si no puede destruirse, cambia su color al color de bala indestructible
        }
    }

    public void Disable() //Desactiva la bala para devolverla al sistema de pooling //Referenced on bullet script, when the bullet collides with another bullet or when it exceeds its lifetime
    {
        lifeTime = 0f;
        gameObject.SetActive(false);
    }
}


