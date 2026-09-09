using System.Collections;
using UnityEngine;

namespace Apamate.BulletHellSystem
{
    public class RadialShotWeapon : MonoBehaviour
    {
        [SerializeField] private RadialShotPattern shotPattern; //patrón de disparo radial que se ejecutará cuando el enemigo dispare

        private bool onShotPattern = false; //si nuestra arma está actualmente ejecutando un patrón de disparo radial

        private void Update()
        {
            if (onShotPattern) //si ya estamos ejecutando un patrón de disparo radial, no hacemos nada
                return;

            StartCoroutine(ExecuteRadialShotPattern(shotPattern)); //si si no estamos ejecutando un patrón de disparo radial, iniciamos la corrutina para ejecutar el patrón de disparo radial
        }

        private IEnumerator ExecuteRadialShotPattern(RadialShotPattern pattern)
        {
            onShotPattern = true;
            int lap = 0; //cual repetición del patrón de disparo radial estamos ejecutando actualmente
            Vector2 aimDirection = transform.up;
            Vector2 center = transform.position;

            yield return new WaitForSeconds(pattern.StartWait);

            while (lap < pattern.Repetitions)
            {
                if (lap > 0 && pattern.AngleOffsetBetweenReps != 0f)
                    aimDirection = aimDirection.Rotate(pattern.AngleOffsetBetweenReps);

                for (int i = 0; i < pattern.PatternSettings.Length; i++)
                {
                    ShotAttack.RadialShot(center, aimDirection, pattern.PatternSettings[i]);
                    if (i < pattern.PatternSettings.Length - 1)
                        yield return new WaitForSeconds(pattern.PatternSettings[i].CooldownAfterShot);
                }
                lap++;
                if (lap < pattern.Repetitions)
                {
                    float cooldown = pattern.PatternSettings[pattern.PatternSettings.Length - 1].CooldownAfterShot;
                    yield return new WaitForSeconds(cooldown);
                }
            }

            yield return new WaitForSeconds(pattern.EndWait);

            onShotPattern = false;
        }
    }
}