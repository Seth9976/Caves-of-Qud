using System;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000F0 RID: 240
	internal interface IInputRuntime
	{
		// Token: 0x06000E54 RID: 3668
		int AllocateDeviceId();

		// Token: 0x06000E55 RID: 3669
		void Update(InputUpdateType type);

		// Token: 0x06000E56 RID: 3670
		unsafe void QueueEvent(InputEvent* ptr);

		// Token: 0x06000E57 RID: 3671
		unsafe long DeviceCommand(int deviceId, InputDeviceCommand* commandPtr);

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000E58 RID: 3672
		// (set) Token: 0x06000E59 RID: 3673
		InputUpdateDelegate onUpdate { get; set; }

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000E5A RID: 3674
		// (set) Token: 0x06000E5B RID: 3675
		Action<InputUpdateType> onBeforeUpdate { get; set; }

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000E5C RID: 3676
		// (set) Token: 0x06000E5D RID: 3677
		Func<InputUpdateType, bool> onShouldRunUpdate { get; set; }

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000E5E RID: 3678
		// (set) Token: 0x06000E5F RID: 3679
		Action<int, string> onDeviceDiscovered { get; set; }

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000E60 RID: 3680
		// (set) Token: 0x06000E61 RID: 3681
		Action<bool> onPlayerFocusChanged { get; set; }

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000E62 RID: 3682
		bool isPlayerFocused { get; }

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000E63 RID: 3683
		// (set) Token: 0x06000E64 RID: 3684
		Action onShutdown { get; set; }

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000E65 RID: 3685
		// (set) Token: 0x06000E66 RID: 3686
		float pollingFrequency { get; set; }

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000E67 RID: 3687
		double currentTime { get; }

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000E68 RID: 3688
		double currentTimeForFixedUpdate { get; }

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000E69 RID: 3689
		float unscaledGameTime { get; }

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000E6A RID: 3690
		double currentTimeOffsetToRealtimeSinceStartup { get; }

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000E6B RID: 3691
		// (set) Token: 0x06000E6C RID: 3692
		bool runInBackground { get; set; }

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000E6D RID: 3693
		Vector2 screenSize { get; }

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000E6E RID: 3694
		ScreenOrientation screenOrientation { get; }

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000E6F RID: 3695
		// (set) Token: 0x06000E70 RID: 3696
		bool normalizeScrollWheelDelta { get; set; }

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000E71 RID: 3697
		float scrollWheelDeltaPerTick { get; }
	}
}
