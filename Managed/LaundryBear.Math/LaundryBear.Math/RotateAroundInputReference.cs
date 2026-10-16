using System;
using UnityEngine;

namespace LaundryBear.Math
{
	// Token: 0x0200000E RID: 14
	public class RotateAroundInputReference : MonoBehaviour, IInputReferenceFrame
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00002CD9 File Offset: 0x00000ED9
		public Vector3 GetWorldForward()
		{
			return base.transform.forward;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002CE6 File Offset: 0x00000EE6
		public Vector3 GetWorldPosition()
		{
			return base.transform.position;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002CF4 File Offset: 0x00000EF4
		public Vector3 ProcessAnalogueStick(Vector2 directionalInput, Vector3 targetObjectPosition, Vector3 targetObjectForward)
		{
			Vector3 vector;
			Vector3 vector2;
			this.ProcessAnalogueStick(targetObjectPosition, targetObjectForward, out vector, out vector2);
			return directionalInput.x * vector2 + directionalInput.y * vector;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002D2C File Offset: 0x00000F2C
		public Vector3 ProcessAnalogueStick(Vector2 directionalInput, Vector3 targetObjectPosition, Vector3 targetObjectForward, Vector3 upVector)
		{
			Vector3 vector;
			Vector3 vector2;
			this.ProcessAnalogueStick(targetObjectPosition, targetObjectForward, upVector, out vector, out vector2);
			return directionalInput.x * vector2 + directionalInput.y * vector;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002D64 File Offset: 0x00000F64
		public void ProcessAnalogueStick(Vector3 targetObjectPosition, Vector3 targetObjectForward, out Vector3 forwardVector, out Vector3 rightVector)
		{
			Vector3 normalized = new Vector3(targetObjectPosition.x - base.transform.position.x, 0f, targetObjectPosition.z - base.transform.position.z).normalized;
			forwardVector = normalized;
			rightVector = Vector3.Cross(Vector3.up, forwardVector);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002DD0 File Offset: 0x00000FD0
		public void ProcessAnalogueStick(Vector3 targetObjectPosition, Vector3 targetObjectForward, Vector3 upVector, out Vector3 forwardVector, out Vector3 rightVector)
		{
			Vector3 normalized = new Vector3(targetObjectPosition.x - base.transform.position.x, 0f, targetObjectPosition.z - base.transform.position.z).normalized;
			forwardVector = normalized;
			rightVector = Vector3.Cross(upVector, forwardVector);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002E3A File Offset: 0x0000103A
		public Vector3 ProjectOnGround(Vector2 directionalInput, Vector3 up)
		{
			throw new NotImplementedException();
		}
	}
}
