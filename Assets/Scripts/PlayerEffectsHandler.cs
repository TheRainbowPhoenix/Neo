using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Spine;
using Spine.Unity;

namespace Phoebe.Neo {
    public class PlayerEffectsHandler : MonoBehaviour {
        public PlayerController eventSource;
		public UnityEvent OnJump, OnLand, OnHardLand;

        private void Awake() {
			if (eventSource == null)
				return;

			eventSource.OnLand += OnLand.Invoke;
			eventSource.OnJump += OnJump.Invoke;
			eventSource.OnHardLand += OnHardLand.Invoke;
        }

    }
}