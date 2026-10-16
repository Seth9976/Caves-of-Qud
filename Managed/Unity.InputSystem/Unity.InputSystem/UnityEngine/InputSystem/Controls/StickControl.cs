using System;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x0200011F RID: 287
	public class StickControl : Vector2Control
	{
		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001064 RID: 4196 RVA: 0x0004F9EB File Offset: 0x0004DBEB
		// (set) Token: 0x06001065 RID: 4197 RVA: 0x0004F9F3 File Offset: 0x0004DBF3
		[InputControl(useStateFrom = "y", processors = "axisDeadzone", parameters = "clamp=2,clampMin=0,clampMax=1", synthetic = true, displayName = "Up")]
		[InputControl(name = "x", minValue = -1f, maxValue = 1f, layout = "Axis", processors = "axisDeadzone")]
		[InputControl(name = "y", minValue = -1f, maxValue = 1f, layout = "Axis", processors = "axisDeadzone")]
		public ButtonControl up { get; set; }

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06001066 RID: 4198 RVA: 0x0004F9FC File Offset: 0x0004DBFC
		// (set) Token: 0x06001067 RID: 4199 RVA: 0x0004FA04 File Offset: 0x0004DC04
		[InputControl(useStateFrom = "y", processors = "axisDeadzone", parameters = "clamp=2,clampMin=-1,clampMax=0,invert", synthetic = true, displayName = "Down")]
		public ButtonControl down { get; set; }

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001068 RID: 4200 RVA: 0x0004FA0D File Offset: 0x0004DC0D
		// (set) Token: 0x06001069 RID: 4201 RVA: 0x0004FA15 File Offset: 0x0004DC15
		[InputControl(useStateFrom = "x", processors = "axisDeadzone", parameters = "clamp=2,clampMin=-1,clampMax=0,invert", synthetic = true, displayName = "Left")]
		public ButtonControl left { get; set; }

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x0600106A RID: 4202 RVA: 0x0004FA1E File Offset: 0x0004DC1E
		// (set) Token: 0x0600106B RID: 4203 RVA: 0x0004FA26 File Offset: 0x0004DC26
		[InputControl(useStateFrom = "x", processors = "axisDeadzone", parameters = "clamp=2,clampMin=0,clampMax=1", synthetic = true, displayName = "Right")]
		public ButtonControl right { get; set; }

		// Token: 0x0600106C RID: 4204 RVA: 0x0004FA30 File Offset: 0x0004DC30
		protected override void FinishSetup()
		{
			base.FinishSetup();
			this.up = base.GetChildControl<ButtonControl>("up");
			this.down = base.GetChildControl<ButtonControl>("down");
			this.left = base.GetChildControl<ButtonControl>("left");
			this.right = base.GetChildControl<ButtonControl>("right");
		}
	}
}
