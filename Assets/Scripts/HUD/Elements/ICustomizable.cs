using UnityEngine.EventSystems;

namespace EdCon.MiniGameTemplate.HUD
{
    public interface ICustomizable : IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        public float MinWidth { get; set; }
        public float MaxWidth { get; set; }
        
        public float MinHeight { get; set; }
        public float MaxHeight { get; set; }
        
        public float MaxOpacity { get; set; }
        public float MinOpacity { get; set; }
        
        public void SetDefaultValues();
    }
}