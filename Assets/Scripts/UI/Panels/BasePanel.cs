using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

using Phoebe.Neo.Base;

namespace Phoebe.Neo.UI.Panels {
    public class BasePanel : MonoBehaviour, IObject {
        protected CanvasGroup canvasGroup;

        protected float ShadeTime = 0.5f;

        public bool IsActive => base.gameObject.activeSelf;

        public virtual void Create(Dictionary<string, object> valueDic)
        {
            if (canvasGroup == null)
            {
                canvasGroup = base.gameObject.AddComponent<CanvasGroup>();
            }
            Show();
        }

        public virtual void Show()
        {
            base.gameObject.SetActive(value: true);
        }

        public virtual void OnPause()
        {
            canvasGroup.blocksRaycasts = false;
        }

        public virtual void OnResume()
        {
            canvasGroup.blocksRaycasts = true;
        }

        public virtual void OnExit()
        {
            base.gameObject.SetActive(value: false);
        }

        public void Hide()
        {
            Managers.UImanager.PopPanel();
        }

        public void Delete()
        {
            Object.Destroy(this);
        }
    }
}