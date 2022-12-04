using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Phoebe.Neo.Base;
using Phoebe.Neo.UI;

namespace Phoebe.Neo {
    public class Managers : MonoBehaviour {
	    private List<IGameManager> _startSequence;
        
	    public static GameManager Gamemanager { get; private set; }

	    public static UIManager UImanager { get; private set; }

        private void Awake() {
            Gamemanager = base.gameObject.AddComponent<GameManager>();
            UImanager = new UIManager();

		    _startSequence = new List<IGameManager> { Gamemanager, UImanager};
            StartCoroutine(StartupManagers());
            foreach (IGameManager item in _startSequence) {
                item.Startup();
            }
            Object.DontDestroyOnLoad(base.gameObject);
        }

        private IEnumerator StartupManagers() {
            int numModles = _startSequence.Count;
            int numReady = 0;
            yield return null;
            while (numReady < numModles)
            {
                numReady = 0;
                foreach (IGameManager item in _startSequence)
                {
                    if (item.status == ManagerStatus.Started)
                    {
                        numReady++;
                    }
                }
                yield return null;
            }
        }

    }
}