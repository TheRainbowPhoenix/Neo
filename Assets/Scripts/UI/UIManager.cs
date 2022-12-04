using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

using Phoebe.Neo.Base;
using Phoebe.Neo.UI.Panels;

namespace Phoebe.Neo.UI {

    public class UIManager : IGameManager {

        private Stack<BasePanel> PanelStack;

        private BasePanel MainPanel;

        private Transform canvastransform;

        public Transform CanvasTransform {
            get {
                if (canvastransform == null) {
                    canvastransform = GameObject.Find("Canvas").transform;
                }
                return canvastransform;
            }
        }

        public ManagerStatus status { get; private set; }

        public void Startup() {
            PanelStack = new Stack<BasePanel>();
		    string[] names = Enum.GetNames(typeof(UIPanelType));
            foreach (string name in names) {
                ObjectPool.Instance.AddObject(ResourceType.UIPrefab, name);
            }
            
        }

        public bool PopPanel() {
            if (PanelStack.Count == 0) {
                return false;
            }
            PanelStack.Peek().OnExit();
            PanelStack.Pop();
            if (PanelStack.Count > 0) {
                PanelStack.Peek().OnResume();
            }
            else if (MainPanel != null) {
                MainPanel.OnResume();
            }
            return true;
        }

        public void PushPanel(UIPanelType panelType, Dictionary<string, object> valueDic = null, bool IsStatic = false) {
            if (!IsStatic) {
                if (PanelStack.Count > 0) {
                    PanelStack.Peek().OnPause();
                }
                else if (MainPanel != null) {
                    MainPanel.OnPause();
                }
                BasePanel panel = GetPanel(Enum.GetName(typeof(UIPanelType), panelType), valueDic);
                PanelStack.Push(panel);
            } else {
                MainPanel = GetPanel(Enum.GetName(typeof(UIPanelType), panelType), valueDic);
            }
        }

        public BasePanel PushPanel(string panelName, Dictionary<string, object> valueDic = null) {
            if (PanelStack.Count > 0)
            {
                PanelStack.Peek().OnPause();
            }
            else if (MainPanel != null)
            {
                MainPanel.OnPause();
            }
            BasePanel panel = GetPanel(panelName, valueDic);
		    PanelStack.Push(panel);
		    return panel;
        }

        public BasePanel GetPanel(UIPanelType panelType, Dictionary<string, object> valueDic = null) {
            return GetPanel(Enum.GetName(typeof(UIPanelType), panelType), valueDic);
        }

        private BasePanel GetPanel(string panelName, Dictionary<string, object> valueDic) {
            BasePanel obj = (BasePanel)ObjectPool.Instance.GetObject(panelName, valueDic);
            obj.transform.SetParent(CanvasTransform, worldPositionStays: false);
            obj.transform.SetAsLastSibling();
            return obj;
        }
    }
}