using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.Scripting;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x02000118 RID: 280
	[Preserve]
	public class DeltaControl : Vector2Control
	{
		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x0600102F RID: 4143 RVA: 0x0004F05E File Offset: 0x0004D25E
		// (set) Token: 0x06001030 RID: 4144 RVA: 0x0004F066 File Offset: 0x0004D266
		[InputControl(useStateFrom = "y", parameters = "clamp=1,clampMin=0,clampMax=3.402823E+38", synthetic = true, displayName = "Up")]
		[Preserve]
		public AxisControl up { get; set; }

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06001031 RID: 4145 RVA: 0x0004F06F File Offset: 0x0004D26F
		// (set) Token: 0x06001032 RID: 4146 RVA: 0x0004F077 File Offset: 0x0004D277
		[InputControl(useStateFrom = "y", parameters = "clamp=1,clampMin=-3.402823E+38,clampMax=0,invert", synthetic = true, displayName = "Down")]
		[Preserve]
		public AxisControl down { get; set; }

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001033 RID: 4147 RVA: 0x0004F080 File Offset: 0x0004D280
		// (set) Token: 0x06001034 RID: 4148 RVA: 0x0004F088 File Offset: 0x0004D288
		[InputControl(useStateFrom = "x", parameters = "clamp=1,clampMin=-3.402823E+38,clampMax=0,invert", synthetic = true, displayName = "Left")]
		[Preserve]
		public AxisControl left { get; set; }

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001035 RID: 4149 RVA: 0x0004F091 File Offset: 0x0004D291
		// (set) Token: 0x06001036 RID: 4150 RVA: 0x0004F099 File Offset: 0x0004D299
		[InputControl(useStateFrom = "x", parameters = "clamp=1,clampMin=0,clampMax=3.402823E+38", synthetic = true, displayName = "Right")]
		[Preserve]
		public AxisControl right { get; set; }

		// Token: 0x06001037 RID: 4151 RVA: 0x0004F0A4 File Offset: 0x0004D2A4
		protected override void FinishSetup()
		{
			base.FinishSetup();
			this.up = base.GetChildControl<AxisControl>("up");
			this.down = base.GetChildControl<AxisControl>("down");
			this.left = base.GetChildControl<AxisControl>("left");
			this.right = base.GetChildControl<AxisControl>("right");
		}
	}
}
