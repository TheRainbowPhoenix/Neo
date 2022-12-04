using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

using Phoebe.Neo.Controller;

namespace Phoebe.Neo {
    public class InputController {
        
        public bool InputSwitch = true;

        public bool SystemInputSwitch = true;

        private Vector3 MousePoint;
    

        private Dictionary<GameInput, KeyCode> InputKeyDic = new Dictionary<GameInput, KeyCode>
        {
            {
                GameInput.Right,
                KeyCode.D
            },
            {
                GameInput.Left,
                KeyCode.A
            },
            {
                GameInput.Up,
                KeyCode.Space
            },
            {
                GameInput.Down,
                KeyCode.S
            },
            {
                GameInput.Shift,
                KeyCode.LeftShift
            },
            {
                GameInput.Fire1,
                KeyCode.Mouse0
            },
            {
                GameInput.Fire2,
                KeyCode.Mouse1
            },
            {
                GameInput.Item1,
                KeyCode.Alpha1
            },
            {
                GameInput.Item2,
                KeyCode.Alpha2
            },
            {
                GameInput.Item3,
                KeyCode.Alpha3
            },
            {
                GameInput.Item4,
                KeyCode.Alpha4
            },
            {
                GameInput.Item5,
                KeyCode.Alpha5
            },
            {
                GameInput.E,
                KeyCode.E
            },
            {
                GameInput.Q,
                KeyCode.Q
            },
            {
                GameInput.R,
                KeyCode.R
            },
            {
                GameInput.F,
                KeyCode.F
            },
            {
                GameInput.C,
                KeyCode.C
            }
        };

        private Dictionary<SystemInput, KeyCode> SystemInputDic = new Dictionary<SystemInput, KeyCode> { 
        {
            SystemInput.Pause,
            KeyCode.Escape
        } };

        public bool GetKey(GameInput input)
        {
            if (InputSwitch)
            {
                return Input.GetKey(InputKeyDic[input]);
            }
            return false;
        }

        public bool GetKeyDown(GameInput input)
        {
            if (InputSwitch)
            {
                return Input.GetKeyDown(InputKeyDic[input]);
            }
            return false;
        }

        public Vector3 GetMousePoint()
        {
            if (InputSwitch)
            {
                MousePoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
            }
            return MousePoint;
        }

        public bool GetSystemInput(SystemInput input)
        {
            if (SystemInputSwitch)
            {
                return Input.GetKeyDown(SystemInputDic[input]);
            }
            return false;
        }
    }
}