using System;
using System.Linq;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.HID
{
	// Token: 0x0200009C RID: 156
	public static class HIDSupport
	{
		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000C3A RID: 3130 RVA: 0x00041F62 File Offset: 0x00040162
		// (set) Token: 0x06000C3B RID: 3131 RVA: 0x00041F70 File Offset: 0x00040170
		public static ReadOnlyArray<HIDSupport.HIDPageUsage> supportedHIDUsages
		{
			get
			{
				return HIDSupport.s_SupportedHIDUsages;
			}
			set
			{
				HIDSupport.s_SupportedHIDUsages = value.ToArray();
				InputSystem.s_Manager.AddAvailableDevicesThatAreNowRecognized();
				for (int i = 0; i < InputSystem.devices.Count; i++)
				{
					InputDevice inputDevice = InputSystem.devices[i];
					HID hid = inputDevice as HID;
					if (hid != null && !HIDSupport.s_SupportedHIDUsages.Contains(new HIDSupport.HIDPageUsage(hid.hidDescriptor.usagePage, hid.hidDescriptor.usage)))
					{
						InputSystem.RemoveLayout(inputDevice.layout);
						i--;
					}
				}
			}
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x00041FFC File Offset: 0x000401FC
		internal static void Initialize()
		{
			HIDSupport.s_SupportedHIDUsages = new HIDSupport.HIDPageUsage[]
			{
				new HIDSupport.HIDPageUsage(HID.GenericDesktop.Joystick),
				new HIDSupport.HIDPageUsage(HID.GenericDesktop.Gamepad),
				new HIDSupport.HIDPageUsage(HID.GenericDesktop.MultiAxisController)
			};
			InputSystem.RegisterLayout<HID>(null, null);
			InputSystem.onFindLayoutForDevice += HID.OnFindLayoutForDevice;
		}

		// Token: 0x04000482 RID: 1154
		private static HIDSupport.HIDPageUsage[] s_SupportedHIDUsages;

		// Token: 0x020001F7 RID: 503
		public struct HIDPageUsage
		{
			// Token: 0x060014F7 RID: 5367 RVA: 0x00062239 File Offset: 0x00060439
			public HIDPageUsage(HID.UsagePage page, int usage)
			{
				this.page = page;
				this.usage = usage;
			}

			// Token: 0x060014F8 RID: 5368 RVA: 0x00062249 File Offset: 0x00060449
			public HIDPageUsage(HID.GenericDesktop usage)
			{
				this.page = HID.UsagePage.GenericDesktop;
				this.usage = (int)usage;
			}

			// Token: 0x04000B3B RID: 2875
			public HID.UsagePage page;

			// Token: 0x04000B3C RID: 2876
			public int usage;
		}
	}
}
