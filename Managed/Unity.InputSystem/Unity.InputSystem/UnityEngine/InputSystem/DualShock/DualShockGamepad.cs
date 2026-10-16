using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Haptics;
using UnityEngine.InputSystem.HID;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem.DualShock
{
	// Token: 0x020000A2 RID: 162
	[InputControlLayout(displayName = "PlayStation Controller")]
	public class DualShockGamepad : Gamepad, IDualShockHaptics, IDualMotorRumble, IHaptics
	{
		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000C99 RID: 3225 RVA: 0x000434AE File Offset: 0x000416AE
		// (set) Token: 0x06000C9A RID: 3226 RVA: 0x000434B6 File Offset: 0x000416B6
		[InputControl(name = "buttonWest", displayName = "Square", shortDisplayName = "Square")]
		[InputControl(name = "buttonNorth", displayName = "Triangle", shortDisplayName = "Triangle")]
		[InputControl(name = "buttonEast", displayName = "Circle", shortDisplayName = "Circle")]
		[InputControl(name = "buttonSouth", displayName = "Cross", shortDisplayName = "Cross")]
		[InputControl]
		public ButtonControl touchpadButton { get; protected set; }

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000C9B RID: 3227 RVA: 0x000434BF File Offset: 0x000416BF
		// (set) Token: 0x06000C9C RID: 3228 RVA: 0x000434C7 File Offset: 0x000416C7
		[InputControl(name = "start", displayName = "Options")]
		public ButtonControl optionsButton { get; protected set; }

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000C9D RID: 3229 RVA: 0x000434D0 File Offset: 0x000416D0
		// (set) Token: 0x06000C9E RID: 3230 RVA: 0x000434D8 File Offset: 0x000416D8
		[InputControl(name = "select", displayName = "Share")]
		public ButtonControl shareButton { get; protected set; }

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000C9F RID: 3231 RVA: 0x000434E1 File Offset: 0x000416E1
		// (set) Token: 0x06000CA0 RID: 3232 RVA: 0x000434E9 File Offset: 0x000416E9
		[InputControl(name = "leftShoulder", displayName = "L1", shortDisplayName = "L1")]
		public ButtonControl L1 { get; protected set; }

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x000434F2 File Offset: 0x000416F2
		// (set) Token: 0x06000CA2 RID: 3234 RVA: 0x000434FA File Offset: 0x000416FA
		[InputControl(name = "rightShoulder", displayName = "R1", shortDisplayName = "R1")]
		public ButtonControl R1 { get; protected set; }

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000CA3 RID: 3235 RVA: 0x00043503 File Offset: 0x00041703
		// (set) Token: 0x06000CA4 RID: 3236 RVA: 0x0004350B File Offset: 0x0004170B
		[InputControl(name = "leftTrigger", displayName = "L2", shortDisplayName = "L2")]
		public ButtonControl L2 { get; protected set; }

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000CA5 RID: 3237 RVA: 0x00043514 File Offset: 0x00041714
		// (set) Token: 0x06000CA6 RID: 3238 RVA: 0x0004351C File Offset: 0x0004171C
		[InputControl(name = "rightTrigger", displayName = "R2", shortDisplayName = "R2")]
		public ButtonControl R2 { get; protected set; }

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000CA7 RID: 3239 RVA: 0x00043525 File Offset: 0x00041725
		// (set) Token: 0x06000CA8 RID: 3240 RVA: 0x0004352D File Offset: 0x0004172D
		[InputControl(name = "leftStickPress", displayName = "L3", shortDisplayName = "L3")]
		public ButtonControl L3 { get; protected set; }

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000CA9 RID: 3241 RVA: 0x00043536 File Offset: 0x00041736
		// (set) Token: 0x06000CAA RID: 3242 RVA: 0x0004353E File Offset: 0x0004173E
		[InputControl(name = "rightStickPress", displayName = "R3", shortDisplayName = "R3")]
		public ButtonControl R3 { get; protected set; }

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x00043547 File Offset: 0x00041747
		// (set) Token: 0x06000CAC RID: 3244 RVA: 0x0004354E File Offset: 0x0004174E
		public new static DualShockGamepad current { get; private set; }

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000CAD RID: 3245 RVA: 0x00043556 File Offset: 0x00041756
		// (set) Token: 0x06000CAE RID: 3246 RVA: 0x0004355E File Offset: 0x0004175E
		internal HID.HIDDeviceDescriptor hidDescriptor { get; private set; }

		// Token: 0x06000CAF RID: 3247 RVA: 0x00043567 File Offset: 0x00041767
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			DualShockGamepad.current = this;
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x00043575 File Offset: 0x00041775
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (DualShockGamepad.current == this)
			{
				DualShockGamepad.current = null;
			}
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x0004358C File Offset: 0x0004178C
		protected override void FinishSetup()
		{
			base.FinishSetup();
			this.touchpadButton = base.GetChildControl<ButtonControl>("touchpadButton");
			this.optionsButton = base.startButton;
			this.shareButton = base.selectButton;
			this.L1 = base.leftShoulder;
			this.R1 = base.rightShoulder;
			this.L2 = base.leftTrigger;
			this.R2 = base.rightTrigger;
			this.L3 = base.leftStickButton;
			this.R3 = base.rightStickButton;
			if (this.m_Description.capabilities != null && this.m_Description.interfaceName == "HID")
			{
				this.hidDescriptor = HID.HIDDeviceDescriptor.FromJson(this.m_Description.capabilities);
			}
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x0004364A File Offset: 0x0004184A
		public virtual void SetLightBarColor(Color color)
		{
		}
	}
}
