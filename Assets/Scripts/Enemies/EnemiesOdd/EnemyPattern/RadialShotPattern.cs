using UnityEngine;

namespace Apamate.BulletHellSystem
{
    [CreateAssetMenu(menuName = "BulletHell System/Radial Shot Pattern")]
    public class RadialShotPattern : ScriptableObject //scriptable object que contiene los ajustes para el patrón de disparo radial de un enemigo
    {
        public int Repetitions;
        [Range(-180f, 180f)] public float AngleOffsetBetweenReps = 0f;
        public float StartWait = 0f;
        public float EndWait = 0f;
        public RadialShotSettings[] PatternSettings;
    }
}