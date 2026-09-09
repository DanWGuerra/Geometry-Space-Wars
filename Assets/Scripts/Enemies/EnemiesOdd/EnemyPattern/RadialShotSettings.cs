using UnityEngine;

namespace Apamate.BulletHellSystem
{
    [System.Serializable]
    public class RadialShotSettings //Clase que contiene los ajustes para el disparo radial de un enemigo
    {
        [Header("Base Settings")]
        public int alternateBullet = 5;
        public float speed = 10f;
        public float CooldownAfterShot;

        [Header("Offsets")]
        [Range(-1f, 1f)] public float PhaseOffset = 0f;
        [Range(-180f, 180f)] public float AngleOffset = 0f;

        [Header("Mask")]
        public bool RadialMask;
        [Range(0f, 360f)] public float MaskAngle = 360f;
    }
}