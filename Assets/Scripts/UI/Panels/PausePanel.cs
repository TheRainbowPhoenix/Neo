using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Phoebe.Neo.Controller;

namespace Phoebe.Neo.UI.Panels {
    public class PausePanel : BasePanel
    {
        [SerializeField]
        private GameObject HidingUI;

        public override void Show() {
            
            GameManager.Game_Input.InputSwitch = false;
		    Time.timeScale = 0f;
            base.Show();
        }

        public void OpenLoadingsFrame() {
            // Managers.UImanager.PushPanel(UIPanelType.LoadingsPanel, new Dictionary<string, object> { { "model", "Load" } });
        }

        public void OpenSettingsFrame() {
            // Managers.UImanager.PushPanel(UIPanelType.SettingsPanel);
        }

        public void ReturnToMainMenu() {
            Time.timeScale = 1f;
            GameManager.Game_Input.InputSwitch = true;
            // StartCoroutine(IE_ReturnToMainMenu());
        }

        public override void OnPause() {
		    HidingUI.SetActive(value: false);
            base.OnPause();
        }

        public override void OnResume() {
            HidingUI.SetActive(value: true);
            Show();
            base.OnResume();
        }

        public override void OnExit() {
            Time.timeScale = 1f;
            GameManager.Game_Input.InputSwitch = true;
            base.OnExit();
        }
    }
}