using System.Collections.Generic;
using UnityEngine;

public class WeirdBulletPool : MonoBehaviour
{
    private static WeirdBulletPool _instance;

    public static WeirdBulletPool Instance //Instance of the bullet pool, if it is null, it will log an error
    {
        get
        {
            //Runs extra code before returning the value
            if (_instance == null) // If the instance is null, log an error
            {
                Debug.LogError("BulletPool instance is missing");
            }
            return _instance;
        }
    }

    [SerializeField] private WeirdBullet bulletPrefab; //Referencia al prefab de la bala que se va a instanciar en el pool, referenciando el codigo de Bullet
    [SerializeField] private int initialPoolSize = 30; //Tamaño inicial del pool de balas, es decir, cuantas balas se van a instanciar al inicio del juego

    private List<WeirdBullet> bulletPool = new List<WeirdBullet>(); //Lista que contiene todas las balas instanciadas en el pool

    private void Awake()
    {
        if (_instance != null && _instance != this) //tienes solo una instancia y es facil de acceder desde donde sea 
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            _instance = this;
        }

        AddBulletsToPool(initialPoolSize);
    }

    private void AddBulletsToPool(int amount) //Instancia las balas y las agrega a la lista del pool, desactivandolas para que no se vean en el juego
    {
        for (int i = 0; i < amount; i++) //por cada bala que se va a instanciar, se instancia y se agrega a la lista del pool
        {
            WeirdBullet bullet = Instantiate(bulletPrefab);//weird bullet is instantiated as a child of the pool object
            bullet.gameObject.SetActive(false); //Desactiva la bala para que no se vea en el juego...
            bulletPool.Add(bullet); //add the bullet to the pool list
            bullet.transform.parent = transform;
        }
    }

    public WeirdBullet RequestBullet() //Solicita una bala del pool, si hay alguna desactivada la activa y la devuelve, si no hay ninguna desactivada instancia una nueva bala y la devuelve
    {
        for (int i = 0; i < bulletPool.Count; i++)
        {
            if (!bulletPool[i].gameObject.activeSelf)
            {
                bulletPool[i].gameObject.SetActive(true);
                return bulletPool[i];
            }
        }
        AddBulletsToPool(1);
        bulletPool[bulletPool.Count - 1].gameObject.SetActive(true);
        return bulletPool[bulletPool.Count - 1];
    }
}