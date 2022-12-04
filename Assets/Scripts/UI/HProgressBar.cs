using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

using Phoebe.Neo.Controller;

namespace Phoebe.Neo.UI {

    public class HProgressBar : MonoBehaviour
    {
        [SerializeField]
        private Transform HAnimProgress;

        [SerializeField]
        private SpriteRenderer LeftButton;

        [SerializeField]
        private SpriteRenderer RightButton;

        [SerializeField]
        private Sprite Button_HighLighted;

        [SerializeField]
        private Sprite Button_Pressed;

        private int StruggleTime;

        private int MaxStruggleTime = 10;

        private Action EndEvent;

        public void Show(Action endEvent)
        {
            EndEvent = endEvent;
            StruggleTime = 0;
            LeftButton.sprite = Button_HighLighted;
            RightButton.sprite = Button_HighLighted;
            base.gameObject.SetActive(value: true);
            // StartCoroutine(IE_Struggle());
        }

        private void Update()
        {
            if (HAnimProgress.localScale.x * (float)MaxStruggleTime < (float)StruggleTime)
            {
                HAnimProgress.localScale += new Vector3(Time.deltaTime, 0f, 0f);
            }
            else
            {
                HAnimProgress.localScale = new Vector3((float)StruggleTime / (float)MaxStruggleTime, 1f, 1f);
            }
        }

        private IEnumerator IE_Struggle()
        {
            while (StruggleTime < MaxStruggleTime)
            {
                yield return new WaitUntil(() => GameManager.Game_Input.GetKeyDown(GameInput.Left));
                StartCoroutine(IE_ButtonClick(LeftButton));
                StruggleTime++;
                yield return new WaitUntil(() => GameManager.Game_Input.GetKeyDown(GameInput.Right));
                StartCoroutine(IE_ButtonClick(RightButton));
                StruggleTime++;
            }
            EndEvent?.Invoke();
            
        }

        private IEnumerator IE_ButtonClick(SpriteRenderer sr)
        {
            sr.sprite = Button_Pressed;
            yield return new WaitForSeconds(0.1f);
            sr.sprite = Button_HighLighted;
        }

        public void Hide()
        {
            base.gameObject.SetActive(value: false);
        }
    }
}