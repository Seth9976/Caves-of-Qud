using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000D0 RID: 208
	internal struct JoystickState : IInputStateTypeInfo
	{
		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000D63 RID: 3427 RVA: 0x0004508D File Offset: 0x0004328D
		public static FourCC kFormat
		{
			get
			{
				return new FourCC('J', 'O', 'Y', ' ');
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000D64 RID: 3428 RVA: 0x0004509C File Offset: 0x0004329C
		public FourCC format
		{
			get
			{
				return JoystickState.kFormat;
			}
		}

		// Token: 0x04000560 RID: 1376
		[InputControl(name = "trigger", displayName = "Trigger", layout = "Button", usages = new string[] { "PrimaryTrigger", "PrimaryAction", "Submit" }, bit = 4U)]
		public int buttons;

		// Token: 0x04000561 RID: 1377
		[InputControl(displayName = "Stick", layout = "Stick", usage = "Primary2DMotion", processors = "stickDeadzone")]
		public Vector2 stick;

		// Token: 0x0200020E RID: 526
		public enum Button
		{
			// Token: 0x04000B97 RID: 2967
			HatSwitchUp,
			// Token: 0x04000B98 RID: 2968
			HatSwitchDown,
			// Token: 0x04000B99 RID: 2969
			HatSwitchLeft,
			// Token: 0x04000B9A RID: 2970
			HatSwitchRight,
			// Token: 0x04000B9B RID: 2971
			Trigger
		}
	}
}
