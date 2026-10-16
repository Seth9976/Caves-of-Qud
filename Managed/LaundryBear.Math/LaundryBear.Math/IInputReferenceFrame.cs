using System;
using UnityEngine;

namespace LaundryBear.Math
{
	// Token: 0x0200000D RID: 13
	public interface IInputReferenceFrame
	{
		// Token: 0x06000042 RID: 66
		Vector3 ProjectOnGround(Vector2 directionalInput, Vector3 up);

		// Token: 0x06000043 RID: 67
		Vector3 GetWorldForward();

		// Token: 0x06000044 RID: 68
		Vector3 GetWorldPosition();

		// Token: 0x06000045 RID: 69
		Vector3 ProcessAnalogueStick(Vector2 directionalInput, Vector3 targetObjectPosition, Vector3 targetObjectForward);

		// Token: 0x06000046 RID: 70
		Vector3 ProcessAnalogueStick(Vector2 directionalInput, Vector3 targetObjectPosition, Vector3 targetObjectForward, Vector3 upVector);

		// Token: 0x06000047 RID: 71
		void ProcessAnalogueStick(Vector3 targetObjectPosition, Vector3 targetObjectForward, out Vector3 forwardVector, out Vector3 rightVector);

		// Token: 0x06000048 RID: 72
		void ProcessAnalogueStick(Vector3 targetObjectPosition, Vector3 targetObjectForward, Vector3 upVector, out Vector3 forwardVector, out Vector3 rightVector);
	}
}
