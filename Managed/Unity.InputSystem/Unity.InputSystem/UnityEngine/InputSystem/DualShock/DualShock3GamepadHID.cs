using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.DualShock.LowLevel;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem.DualShock
{
	// Token: 0x020000A5 RID: 165
	[InputControlLayout(stateType = typeof(DualShock3HIDInputReport), hideInUI = true, displayName = "PS3 Controller")]
	public class DualShock3GamepadHID : DualShockGamepad
	{
		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000CDC RID: 3292 RVA: 0x0004423E File Offset: 0x0004243E
		// (set) Token: 0x06000CDD RID: 3293 RVA: 0x00044246 File Offset: 0x00042446
		public ButtonControl leftTriggerButton { get; protected set; }

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000CDE RID: 3294 RVA: 0x0004424F File Offset: 0x0004244F
		// (set) Token: 0x06000CDF RID: 3295 RVA: 0x00044257 File Offset: 0x00042457
		public ButtonControl rightTriggerButton { get; protected set; }

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x00044260 File Offset: 0x00042460
		// (set) Token: 0x06000CE1 RID: 3297 RVA: 0x00044268 File Offset: 0x00042468
		public ButtonControl playStationButton { get; protected set; }

		// Token: 0x06000CE2 RID: 3298 RVA: 0x00044271 File Offset: 0x00042471
		protected override void FinishSetup()
		{
			this.leftTriggerButton = base.GetChildControl<ButtonControl>("leftTriggerButton");
			this.rightTriggerButton = base.GetChildControl<ButtonControl>("rightTriggerButton");
			this.playStationButton = base.GetChildControl<ButtonControl>("systemButton");
			base.FinishSetup();
		}
	}
}
