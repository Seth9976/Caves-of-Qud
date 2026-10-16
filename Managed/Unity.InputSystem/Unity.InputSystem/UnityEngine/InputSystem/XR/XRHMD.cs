using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem.XR
{
	// Token: 0x02000065 RID: 101
	[InputControlLayout(isGenericTypeOfDevice = true, displayName = "XR HMD", canRunInBackground = true)]
	public class XRHMD : TrackedDevice
	{
		// Token: 0x17000286 RID: 646
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x00037773 File Offset: 0x00035973
		// (set) Token: 0x060009F0 RID: 2544 RVA: 0x0003777B File Offset: 0x0003597B
		[InputControl(noisy = true)]
		public Vector3Control leftEyePosition { get; protected set; }

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x060009F1 RID: 2545 RVA: 0x00037784 File Offset: 0x00035984
		// (set) Token: 0x060009F2 RID: 2546 RVA: 0x0003778C File Offset: 0x0003598C
		[InputControl(noisy = true)]
		public QuaternionControl leftEyeRotation { get; protected set; }

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x00037795 File Offset: 0x00035995
		// (set) Token: 0x060009F4 RID: 2548 RVA: 0x0003779D File Offset: 0x0003599D
		[InputControl(noisy = true)]
		public Vector3Control rightEyePosition { get; protected set; }

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x060009F5 RID: 2549 RVA: 0x000377A6 File Offset: 0x000359A6
		// (set) Token: 0x060009F6 RID: 2550 RVA: 0x000377AE File Offset: 0x000359AE
		[InputControl(noisy = true)]
		public QuaternionControl rightEyeRotation { get; protected set; }

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x060009F7 RID: 2551 RVA: 0x000377B7 File Offset: 0x000359B7
		// (set) Token: 0x060009F8 RID: 2552 RVA: 0x000377BF File Offset: 0x000359BF
		[InputControl(noisy = true)]
		public Vector3Control centerEyePosition { get; protected set; }

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x060009F9 RID: 2553 RVA: 0x000377C8 File Offset: 0x000359C8
		// (set) Token: 0x060009FA RID: 2554 RVA: 0x000377D0 File Offset: 0x000359D0
		[InputControl(noisy = true)]
		public QuaternionControl centerEyeRotation { get; protected set; }

		// Token: 0x060009FB RID: 2555 RVA: 0x000377DC File Offset: 0x000359DC
		protected override void FinishSetup()
		{
			base.FinishSetup();
			this.centerEyePosition = base.GetChildControl<Vector3Control>("centerEyePosition");
			this.centerEyeRotation = base.GetChildControl<QuaternionControl>("centerEyeRotation");
			this.leftEyePosition = base.GetChildControl<Vector3Control>("leftEyePosition");
			this.leftEyeRotation = base.GetChildControl<QuaternionControl>("leftEyeRotation");
			this.rightEyePosition = base.GetChildControl<Vector3Control>("rightEyePosition");
			this.rightEyeRotation = base.GetChildControl<QuaternionControl>("rightEyeRotation");
		}
	}
}
