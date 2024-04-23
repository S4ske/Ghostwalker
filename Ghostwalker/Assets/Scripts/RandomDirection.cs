using UnityEngine;

namespace GhostWalker.RandomDirecton
{
    public static class RandomDirection
    {
        public static Vector3 GetRandomDirection()
        {
            return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
        }
    }
}
