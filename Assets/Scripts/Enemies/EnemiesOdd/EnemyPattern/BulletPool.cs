using System.Collections.Generic;
using UnityEngine;

namespace Apamate.BulletHellSystem
{
    public class BulletPool : MonoBehaviour
    {
        private static BulletPool _instance; 
        public static BulletPool Instance
        {
            get
            {
                if (_instance == null)
                    Debug.LogError("BulletPoot Instance missing.");

                return _instance;
            }
        }

        [SerializeField] private BulletOdd bulletPrefab; //Referencia al prefab de la bala que se va a instanciar en el pool, referenciando el codigo de Bullet
        [SerializeField] private int initialPoolSize = 10; //Tamaño inicial del pool de balas, es decir, cuantas balas se van a instanciar al inicio del juego

        private List<BulletOdd> bulletPool = new List<BulletOdd>(); //Lista que contiene todas las balas instanciadas en el pool

        private void Awake()
        {
            // Singleton pattern 
            if (_instance != null && _instance != this) //tienes solo una instancia y es facil de acceder desde donde sea 
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                _instance = this; 
            }
            // Pool initialization
            AddBulletsToPool(initialPoolSize);
        }

        private void AddBulletsToPool(int amount) //Instancia las balas y las agrega a la lista del pool, desactivandolas para que no se vean en el juego
        {
            for (int i = 0; i < amount; i++)
            {
                BulletOdd bullet = Instantiate(bulletPrefab);
                bullet.gameObject.SetActive(false); //Desactiva la bala para que no se vea en el juego
                bulletPool.Add(bullet);
                bullet.transform.parent = transform;
            }
        }

        public BulletOdd RequestBullet() //Solicita una bala del pool, si hay alguna desactivada la activa y la devuelve, si no hay ninguna desactivada instancia una nueva bala y la devuelve
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
}