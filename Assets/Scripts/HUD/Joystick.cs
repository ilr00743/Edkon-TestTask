using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EdCon.MiniGameTemplate.HUD
{
    public class Joystick : CustomizableHUDElement
    {
        private HUDCustomizationService _hudCustomizationService;
        
        private void Start()
        {
            _hudCustomizationService = HUDCustomizationService.Instance;
        }

        public override void Select()
        {
            base.Select();
            Debug.Log("Joystick selected");
        }
    }
}