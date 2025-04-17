using UnityEngine;
using UnityEngine.EventSystems;

namespace EdCon.MiniGameTemplate.HUD
{
    public class HealButton : CustomizableHUDElement
    {
        public override void Select()
        {
            base.Select();
            Debug.Log("Heal button selected");
        }
    }
}