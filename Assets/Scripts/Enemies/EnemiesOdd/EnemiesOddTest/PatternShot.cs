using UnityEngine;

public class PatternShot : MonoBehaviour
{
    [Header("Pattern")]
    [SerializeField] private int numberOfBullets = 5;
    [SerializeField] private float shotCooldown = 0.25f;

    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float rotation = 12f;

    [Header("Bullet Types")]
    [SerializeField] private bool alternateBullets = true;
    [SerializeField] private bool defaultDestroyable = true;

    private float timer;
    private float currentAngle;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= shotCooldown) //If the timer exceeds the shot cooldown, fire the pattern and reset the timer
        {
            timer = 0f; //Reset the timer to 0 to start counting for the next shot
            FirePattern();
            currentAngle += rotation; //Rotate the entire pattern after every shot
        }
    }

    private void FirePattern()
    {
        if (WeirdBulletPool.Instance == null) //If the bullet pool instance is null, we cannot fire bullets, so we return early, reference instance from the bullet code
            return;

        float angleRotation = 360f/numberOfBullets; //angle rotation

        for (int i = 0; i < numberOfBullets; i++) //Loop through the number of bullets to be fired in this shot
        {
            float angle = currentAngle + (angleRotation * i); //Calculate the angle for each bullet based on the current angle and the angle step
            float radians = angle * Mathf.Deg2Rad; //Degrees-to-radians conversion constant

            Vector2 direction = new Vector2(Mathf.Cos(radians),Mathf.Sin(radians)); //Calculate the direction vector based on the angle in radians

            WeirdBullet bullet = WeirdBulletPool.Instance.RequestBullet(); //request a bullet from the bullet pool
            bullet.transform.position = transform.position; //Set the bullet's position to the enemy's position
            bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle); //Rotate the bullet to face the direction it will move
            bool destroyable = defaultDestroyable; //Set the destroyable property based on the default value

            if (alternateBullets) //If alternate bullet types is true, alternate between destroyable and indestructible bullets
            {
                destroyable = i % 2 == 0; //alternate between destroyable and indestructible bullets
            }

            bullet.Initialize(transform, direction * speed,destroyable); //Initialize the bullet with the enemy's transform, direction, speed, and destroyable property
        }
    }
}
