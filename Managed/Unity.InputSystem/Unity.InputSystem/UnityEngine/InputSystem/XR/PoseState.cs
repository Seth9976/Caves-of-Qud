using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.XR;

namespace UnityEngine.InputSystem.XR
{
	// Token: 0x02000063 RID: 99
	[StructLayout(LayoutKind.Explicit, Size = 60)]
	public struct PoseState : IInputStateTypeInfo
	{
		// Token: 0x1700027F RID: 639
		// (get) Token: 0x060009DB RID: 2523 RVA: 0x00037321 File Offset: 0x00035521
		public FourCC format
		{
			get
			{
				return PoseState.s_Format;
			}
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x00037328 File Offset: 0x00035528
		public PoseState(bool isTracked, InputTrackingState trackingState, Vector3 position, Quaternion rotation, Vector3 velocity, Vector3 angularVelocity)
		{
			this.isTracked = isTracked;
			this.trackingState = trackingState;
			this.position = position;
			this.rotation = rotation;
			this.velocity = velocity;
			this.angularVelocity = angularVelocity;
		}

		// Token: 0x04000340 RID: 832
		internal const int kSizeInBytes = 60;

		// Token: 0x04000341 RID: 833
		internal static readonly FourCC s_Format = new FourCC('P', 'o', 's', 'e');

		// Token: 0x04000342 RID: 834
		[InputControl(displayName = "Is Tracked", layout = "Button", sizeInBits = 8U)]
		[FieldOffset(0)]
		public bool isTracked;

		// Token: 0x04000343 RID: 835
		[InputControl(displayName = "Tracking State", layout = "Integer")]
		[FieldOffset(4)]
		public InputTrackingState trackingState;

		// Token: 0x04000344 RID: 836
		[InputControl(displayName = "Position", noisy = true)]
		[FieldOffset(8)]
		public Vector3 position;

		// Token: 0x04000345 RID: 837
		[InputControl(displayName = "Rotation", noisy = true)]
		[FieldOffset(20)]
		public Quaternion rotation;

		// Token: 0x04000346 RID: 838
		[InputControl(displayName = "Velocity", noisy = true)]
		[FieldOffset(36)]
		public Vector3 velocity;

		// Token: 0x04000347 RID: 839
		[InputControl(displayName = "Angular Velocity", noisy = true)]
		[FieldOffset(48)]
		public Vector3 angularVelocity;
	}
}
