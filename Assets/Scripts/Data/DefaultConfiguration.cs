using UnityEngine;

namespace EdCon.MiniGameTemplate.HUD
{
    [CreateAssetMenu(fileName = "HUD Element Configuration", menuName = "HUD/Default Element Configuration", order = 0)]
    public class DefaultConfiguration : ScriptableObject
    {
        [Range(0,1)] public float DefaultOpacity;
        public Vector2 DefaultPosition;
        public Vector2 DefaultScale;
    }
}