using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000057 RID: 87
	[InputControlLayout(displayName = "Tracked Device", isGenericTypeOfDevice = true)]
	public class TrackedDevice : InputDevice
	{
		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000866 RID: 2150 RVA: 0x0002F1D6 File Offset: 0x0002D3D6
		// (set) Token: 0x06000867 RID: 2151 RVA: 0x0002F1DE File Offset: 0x0002D3DE
		[InputControl(synthetic = true)]
		public IntegerControl trackingState { get; protected set; }

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000868 RID: 2152 RVA: 0x0002F1E7 File Offset: 0x0002D3E7
		// (set) Token: 0x06000869 RID: 2153 RVA: 0x0002F1EF File Offset: 0x0002D3EF
		[InputControl(synthetic = true)]
		public ButtonControl isTracked { get; protected set; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x0600086A RID: 2154 RVA: 0x0002F1F8 File Offset: 0x0002D3F8
		// (set) Token: 0x0600086B RID: 2155 RVA: 0x0002F200 File Offset: 0x0002D400
		[InputControl(noisy = true, dontReset = true)]
		public Vector3Control devicePosition { get; protected set; }

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x0600086C RID: 2156 RVA: 0x0002F209 File Offset: 0x0002D409
		// (set) Token: 0x0600086D RID: 2157 RVA: 0x0002F211 File Offset: 0x0002D411
		[InputControl(noisy = true, dontReset = true)]
		public QuaternionControl deviceRotation { get; protected set; }

		// Token: 0x0600086E RID: 2158 RVA: 0x0002F21C File Offset: 0x0002D41C
		protected override void FinishSetup()
		{
			base.FinishSetup();
			this.trackingState = base.GetChildControl<IntegerControl>("trackingState");
			this.isTracked = base.GetChildControl<ButtonControl>("isTracked");
			this.devicePosition = base.GetChildControl<Vector3Control>("devicePosition");
			this.deviceRotation = base.GetChildControl<QuaternionControl>("deviceRotation");
		}
	}
}
