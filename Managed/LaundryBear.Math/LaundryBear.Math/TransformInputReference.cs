using System;
using UnityEngine;

namespace LaundryBear.Math
{
	// Token: 0x0200000F RID: 15
	public class TransformInputReference : MonoBehaviour, IInputReferenceFrame
	{
		// Token: 0x06000051 RID: 81 RVA: 0x00002E4C File Offset: 0x0000104C
		public Vector3 ProjectOnGround(Vector2 directionalInput, Vector3 up)
		{
			Vector3 vector = Vector3.ProjectOnPlane(base.transform.forward, up);
			Vector3 vector2 = Vector3.ProjectOnPlane(base.transform.right, up);
			return (vector * directionalInput.y + vector2 * directionalInput.x).normalized;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002EA0 File Offset: 0x000010A0
		public Vector3 GetWorldForward()
		{
			return base.transform.forward;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002EAD File Offset: 0x000010AD
		public Vector3 GetWorldPosition()
		{
			return base.transform.position;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002EBC File Offset: 0x000010BC
		public Vector3 ProcessAnalogueStick(Vector2 directionalInput, Vector3 targetObjectPosition, Vector3 targetObjectForward)
		{
			return Vector3.ProjectOnPlane(base.transform.TransformDirection(directionalInput.x, 0f, directionalInput.y), Vector3.up).normalized;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002EF8 File Offset: 0x000010F8
		public Vector3 ProcessAnalogueStick(Vector2 directionalInput, Vector3 targetObjectPosition, Vector3 targetObjectForward, Vector3 upVector)
		{
			return Vector3.ProjectOnPlane(base.transform.TransformDirection(directionalInput.x, 0f, directionalInput.y), upVector).normalized;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002F30 File Offset: 0x00001130
		public void ProcessAnalogueStick(Vector3 targetObjectPosition, Vector3 targetObjectForward, out Vector3 forwardVector, out Vector3 rightVector)
		{
			forwardVector = Vector3.ProjectOnPlane(base.transform.forward, Vector3.up).normalized;
			rightVector = Vector3.ProjectOnPlane(base.transform.right, Vector3.up).normalized;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002F84 File Offset: 0x00001184
		public void ProcessAnalogueStick(Vector3 targetObjectPosition, Vector3 targetObjectForward, Vector3 up, out Vector3 forwardVector, out Vector3 rightVector)
		{
			forwardVector = Vector3.ProjectOnPlane(base.transform.forward, up).normalized;
			rightVector = Vector3.ProjectOnPlane(base.transform.right, up).normalized;
		}
	}
}
