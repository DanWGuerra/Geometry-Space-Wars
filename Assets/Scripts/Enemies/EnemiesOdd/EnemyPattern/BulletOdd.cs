using UnityEngine;

namespace Apamate.BulletHellSystem
{
    public class BulletOdd : MonoBehaviour
    {
        private const float maxLife = 3f;
        private float lifeTime = 0f;

        public Vector2 Velocity;

        private void Update()
        {
            transform.position += (Vector3)Velocity * Time.deltaTime;
            lifeTime += Time.deltaTime;

            if (lifeTime > maxLife)
                Disable();
        }

        private void Disable()
        {
            lifeTime = 0f;
            gameObject.SetActive(false);
        }
    }
}