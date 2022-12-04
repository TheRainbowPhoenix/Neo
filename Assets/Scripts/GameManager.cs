using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

using Phoebe.Neo.Controller;
using Phoebe.Neo.UI.Panels;
using Phoebe.Neo.Base;

namespace Phoebe.Neo {
    public class GameManager : MonoBehaviour, IGameManager
    {

        private FollowPlayerCamera GameCamera;

        private Application.LogCallback LogOutHandler;

	    public ManagerStatus status { get; private set; }

        public static InputController Game_Input { get; private set; }

        public void Startup() {
            Debug.Log("STARTUP");
            Game_Input = new InputController();
            //TODO
		    status = ManagerStatus.Started;


            LogOutHandler = (Application.LogCallback)Delegate.Combine(LogOutHandler, new Application.LogCallback(ErrorWriter));
            Application.RegisterLogCallbackThreaded(LogOutHandler);
        }


        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            // Debug.Log("E: " + Game_Input.GetKeyDown(GameInput.E));
            if (Game_Input.GetKeyDown(GameInput.E)) {
                Debug.Log("E.");
            }

            if (Game_Input.GetSystemInput(SystemInput.Pause) && !Managers.UImanager.PopPanel() /* GameState == Stats.InBattle */) {
                Debug.Log("Pause !!");
                Managers.UImanager.PushPanel(UIPanelType.PausePanel);
            }
        }

        private void ErrorWriter(string condition, string stackTrace, LogType type)
        {
            string s = string.Concat("Time:[", DateTime.Now.Month, "/", DateTime.Now.Day, " ", DateTime.Now.Hour, ":", DateTime.Now.Minute, ":", DateTime.Now.Second, "] ", condition, "\n", stackTrace, type, "\n\n");
            if (!File.Exists(Application.streamingAssetsPath + "/ErrorLog.txt"))
            {
                File.Create(Application.streamingAssetsPath + "/ErrorLog.txt");
            }
            using (FileStream fileStream = new FileStream(Application.streamingAssetsPath + "/ErrorLog.txt", FileMode.Append))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(s);
                fileStream.Write(bytes, 0, bytes.Length);
            }
        }
    }
}