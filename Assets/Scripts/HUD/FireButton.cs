using UnityEngine;
using UnityEngine.EventSystems;

namespace EdCon.MiniGameTemplate.HUD
{
    public class FireButton : CustomizableHUDElement
    {
        public override void Select()
        {
            base.Select();
            Debug.Log("Fire button selected");
        }
    }
}