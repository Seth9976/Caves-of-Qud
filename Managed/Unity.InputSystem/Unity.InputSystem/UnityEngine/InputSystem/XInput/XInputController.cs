using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem.XInput
{
	// Token: 0x0200007C RID: 124
	[InputControlLayout(displayName = "Xbox Controller")]
	public class XInputController : Gamepad
	{
		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000A93 RID: 2707 RVA: 0x0003946D File Offset: 0x0003766D
		// (set) Token: 0x06000A94 RID: 2708 RVA: 0x00039475 File Offset: 0x00037675
		[InputControl(name = "buttonSouth", displayName = "A")]
		[InputControl(name = "buttonEast", displayName = "B")]
		[InputControl(name = "buttonWest", displayName = "X")]
		[InputControl(name = "buttonNorth", displayName = "Y")]
		[InputControl(name = "leftShoulder", displayName = "Left Bumper", shortDisplayName = "LB")]
		[InputControl(name = "rightShoulder", displayName = "Right Bumper", shortDisplayName = "RB")]
		[InputControl(name = "leftTrigger", shortDisplayName = "LT")]
		[InputControl(name = "rightTrigger", shortDisplayName = "RT")]
		[InputControl(name = "start", displayName = "Menu", alias = "menu")]
		[InputControl(name = "select", displayName = "View", alias = "view")]
		public ButtonControl menu { get; protected set; }

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000A95 RID: 2709 RVA: 0x0003947E File Offset: 0x0003767E
		// (set) Token: 0x06000A96 RID: 2710 RVA: 0x00039486 File Offset: 0x00037686
		public ButtonControl view { get; protected set; }

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000A97 RID: 2711 RVA: 0x0003948F File Offset: 0x0003768F
		public XInputController.DeviceSubType subType
		{
			get
			{
				if (!this.m_HaveParsedCapabilities)
				{
					this.ParseCapabilities();
				}
				return this.m_SubType;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000A98 RID: 2712 RVA: 0x000394A5 File Offset: 0x000376A5
		public XInputController.DeviceFlags flags
		{
			get
			{
				if (!this.m_HaveParsedCapabilities)
				{
					this.ParseCapabilities();
				}
				return this.m_Flags;
			}
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x000394BB File Offset: 0x000376BB
		protected override void FinishSetup()
		{
			base.FinishSetup();
			this.menu = base.startButton;
			this.view = base.selectButton;
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x000394DC File Offset: 0x000376DC
		private void ParseCapabilities()
		{
			if (!string.IsNullOrEmpty(base.description.capabilities))
			{
				XInputController.Capabilities capabilities = JsonUtility.FromJson<XInputController.Capabilities>(base.description.capabilities);
				this.m_SubType = capabilities.subType;
				this.m_Flags = capabilities.flags;
			}
			this.m_HaveParsedCapabilities = true;
		}

		// Token: 0x040003B8 RID: 952
		private bool m_HaveParsedCapabilities;

		// Token: 0x040003B9 RID: 953
		private XInputController.DeviceSubType m_SubType;

		// Token: 0x040003BA RID: 954
		private XInputController.DeviceFlags m_Flags;

		// Token: 0x020001CA RID: 458
		internal enum DeviceType
		{
			// Token: 0x04000975 RID: 2421
			Gamepad
		}

		// Token: 0x020001CB RID: 459
		public enum DeviceSubType
		{
			// Token: 0x04000977 RID: 2423
			Unknown,
			// Token: 0x04000978 RID: 2424
			Gamepad,
			// Token: 0x04000979 RID: 2425
			Wheel,
			// Token: 0x0400097A RID: 2426
			ArcadeStick,
			// Token: 0x0400097B RID: 2427
			FlightStick,
			// Token: 0x0400097C RID: 2428
			DancePad,
			// Token: 0x0400097D RID: 2429
			Guitar,
			// Token: 0x0400097E RID: 2430
			GuitarAlternate,
			// Token: 0x0400097F RID: 2431
			DrumKit,
			// Token: 0x04000980 RID: 2432
			GuitarBass = 11,
			// Token: 0x04000981 RID: 2433
			ArcadePad = 19
		}

		// Token: 0x020001CC RID: 460
		[Flags]
		public new enum DeviceFlags
		{
			// Token: 0x04000983 RID: 2435
			ForceFeedbackSupported = 1,
			// Token: 0x04000984 RID: 2436
			Wireless = 2,
			// Token: 0x04000985 RID: 2437
			VoiceSupported = 4,
			// Token: 0x04000986 RID: 2438
			PluginModulesSupported = 8,
			// Token: 0x04000987 RID: 2439
			NoNavigation = 16
		}

		// Token: 0x020001CD RID: 461
		[Serializable]
		internal struct Capabilities
		{
			// Token: 0x04000988 RID: 2440
			public XInputController.DeviceType type;

			// Token: 0x04000989 RID: 2441
			public XInputController.DeviceSubType subType;

			// Token: 0x0400098A RID: 2442
			public XInputController.DeviceFlags flags;
		}
	}
}
