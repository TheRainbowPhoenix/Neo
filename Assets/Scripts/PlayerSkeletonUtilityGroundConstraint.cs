#if UNITY_2018_3 || UNITY_2019 || UNITY_2018_3_OR_NEWER
#define NEW_PREFAB_SYSTEM
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Spine;
using Spine.Unity;

namespace Phoebe.Neo {

    #if NEW_PREFAB_SYSTEM
	[ExecuteAlways]
	#else
	[ExecuteInEditMode]
	#endif
	[RequireComponent(typeof(SkeletonUtilityBone))]
    public class PlayerSkeletonUtilityGroundConstraint : SkeletonUtilityConstraint
    {
        [Tooltip("LayerMask for what objects to raycast against")]
		public LayerMask groundMask;
		[Tooltip("Use 2D")]
		public bool use2D = false;
		[Tooltip("Uses SphereCast for 3D mode and CircleCast for 2D mode")]
		public bool useRadius = false;
		[Tooltip("The Radius")]
		public float castRadius = 0.1f;
		[Tooltip("How high above the target bone to begin casting from")]
		public float castDistance = 5f;
		[Tooltip("X-Axis adjustment")]
		public float castOffset = 0;
		[Tooltip("Y-Axis adjustment")]
		public float groundOffset = 0;
		[Tooltip("How fast the target IK position adjusts to the ground. Use smaller values to prevent snapping")]
		public float adjustSpeed = 5;

		Vector3 rayOrigin;
		Vector3 rayDir = new Vector3(0, -1, 0);
		float hitY;
		float lastHitY;

		protected override void OnEnable () {
			base.OnEnable();
			lastHitY = transform.position.y;
		}

		public override void DoUpdate () {
			rayOrigin = transform.position + new Vector3(castOffset, castDistance, 0);

			float positionScale = hierarchy.PositionScale;
			float adjustDistanceThisFrame = adjustSpeed * positionScale * Time.deltaTime;
			hitY = float.MinValue;
			if (use2D) {
				RaycastHit2D hit;

				if (useRadius)
					hit = Physics2D.CircleCast(rayOrigin, castRadius, rayDir, castDistance + groundOffset, groundMask);
				else
					hit = Physics2D.Raycast(rayOrigin, rayDir, castDistance + groundOffset, groundMask);

				if (hit.collider != null) {
					hitY = hit.point.y + groundOffset;
					if (Application.isPlaying)
						hitY = Mathf.MoveTowards(lastHitY, hitY, adjustDistanceThisFrame);
				} else {
					if (Application.isPlaying)
						hitY = Mathf.MoveTowards(lastHitY, transform.position.y, adjustDistanceThisFrame);
				}
			} else {
				RaycastHit hit;
				bool validHit = false;

				if (useRadius)
					validHit = Physics.SphereCast(rayOrigin, castRadius, rayDir, out hit, castDistance + groundOffset, groundMask);
				else
					validHit = Physics.Raycast(rayOrigin, rayDir, out hit, castDistance + groundOffset, groundMask);

				if (validHit) {
					hitY = hit.point.y + groundOffset;
					if (Application.isPlaying)
						hitY = Mathf.MoveTowards(lastHitY, hitY, adjustDistanceThisFrame);

				} else {
					if (Application.isPlaying)
						hitY = Mathf.MoveTowards(lastHitY, transform.position.y, adjustDistanceThisFrame);
				}
			}

			Vector3 v = transform.position;
			v.y = Mathf.Clamp(v.y, Mathf.Min(lastHitY, hitY), float.MaxValue);
			transform.position = v;

			bone.bone.X = transform.localPosition.x / hierarchy.PositionScale;
			bone.bone.Y = transform.localPosition.y / hierarchy.PositionScale;

			lastHitY = hitY;
		}

		void OnDrawGizmos () {
			Vector3 hitEnd = rayOrigin + (rayDir * Mathf.Min(castDistance, rayOrigin.y - hitY));
			Vector3 clearEnd = rayOrigin + (rayDir * castDistance);
			Gizmos.DrawLine(rayOrigin, hitEnd);

			if (useRadius) {
				Gizmos.DrawLine(new Vector3(hitEnd.x - castRadius, hitEnd.y - groundOffset, hitEnd.z), new Vector3(hitEnd.x + castRadius, hitEnd.y - groundOffset, hitEnd.z));
				Gizmos.DrawLine(new Vector3(clearEnd.x - castRadius, clearEnd.y, clearEnd.z), new Vector3(clearEnd.x + castRadius, clearEnd.y, clearEnd.z));
			}

			Gizmos.color = Color.red;
			Gizmos.DrawLine(hitEnd, clearEnd);
		}
    }
}




