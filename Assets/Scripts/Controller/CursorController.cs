using System.Collections;
using Spine.Unity;
using UnityEngine;
using UnityEngine.EventSystems;

using Spine;
using Spine.Unity;

namespace Phoebe.Neo.Controller {

	public class CursorController : MonoBehaviour {
        [SerializeField]
        private Texture2D TransparentCursor;

        [SerializeField]
        private Transform CursorEffect;

        // [SerializeField]
        // private SkeletonAnimation AimPoint;

        // [SerializeField]
        // private SkeletonAnimation HitEffect;

        // [SerializeField]
        // private SkeletonAnimation HeadshootEffect;

        // Scene Status
        
	    public static CursorController Instance;

        // public Vector3 WorldMousePosition => Camera.main.ScreenToWorldPoint(GameManager.Game_Input.GetMousePoint());

        private void Awake()
        {
            Instance = this;
            Object.DontDestroyOnLoad(base.gameObject);
        }

        private void LateUpdate()
        {
            // CursorEffect.position = WorldMousePosition;
        }

    }
}