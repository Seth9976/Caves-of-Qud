using System;
using System.Diagnostics;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.EnhancedTouch
{
	// Token: 0x0200009D RID: 157
	public static class EnhancedTouchSupport
	{
		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000C3D RID: 3133 RVA: 0x0004205B File Offset: 0x0004025B
		public static bool enabled
		{
			get
			{
				return EnhancedTouchSupport.s_Enabled > 0;
			}
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x00042068 File Offset: 0x00040268
		public static void Enable()
		{
			EnhancedTouchSupport.s_Enabled++;
			if (EnhancedTouchSupport.s_Enabled > 1)
			{
				return;
			}
			InputSystem.onDeviceChange += EnhancedTouchSupport.OnDeviceChange;
			InputSystem.onBeforeUpdate += Touch.BeginUpdate;
			InputSystem.onSettingsChange += EnhancedTouchSupport.OnSettingsChange;
			EnhancedTouchSupport.SetUpState();
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x000420C4 File Offset: 0x000402C4
		public static void Disable()
		{
			if (!EnhancedTouchSupport.enabled)
			{
				return;
			}
			EnhancedTouchSupport.s_Enabled--;
			if (EnhancedTouchSupport.s_Enabled > 0)
			{
				return;
			}
			InputSystem.onDeviceChange -= EnhancedTouchSupport.OnDeviceChange;
			InputSystem.onBeforeUpdate -= Touch.BeginUpdate;
			InputSystem.onSettingsChange -= EnhancedTouchSupport.OnSettingsChange;
			EnhancedTouchSupport.TearDownState();
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x00042126 File Offset: 0x00040326
		internal static void Reset()
		{
			Touch.s_GlobalState.touchscreens = default(InlinedArray<Touchscreen>);
			Touch.s_GlobalState.playerState.Destroy();
			Touch.s_GlobalState.playerState = default(Touch.FingerAndTouchState);
			EnhancedTouchSupport.s_Enabled = 0;
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x00042160 File Offset: 0x00040360
		private static void SetUpState()
		{
			Touch.s_GlobalState.playerState.updateMask = InputUpdateType.Dynamic | InputUpdateType.Fixed | InputUpdateType.Manual;
			EnhancedTouchSupport.s_UpdateMode = InputSystem.settings.updateMode;
			foreach (InputDevice inputDevice in InputSystem.devices)
			{
				EnhancedTouchSupport.OnDeviceChange(inputDevice, InputDeviceChange.Added);
			}
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x000421D4 File Offset: 0x000403D4
		internal static void TearDownState()
		{
			foreach (InputDevice inputDevice in InputSystem.devices)
			{
				EnhancedTouchSupport.OnDeviceChange(inputDevice, InputDeviceChange.Removed);
			}
			Touch.s_GlobalState.playerState.Destroy();
			Touch.s_GlobalState.playerState = default(Touch.FingerAndTouchState);
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x00042248 File Offset: 0x00040448
		private static void OnDeviceChange(InputDevice device, InputDeviceChange change)
		{
			if (change != InputDeviceChange.Added)
			{
				if (change != InputDeviceChange.Removed)
				{
					return;
				}
				Touchscreen touchscreen = device as Touchscreen;
				if (touchscreen != null)
				{
					Touch.RemoveTouchscreen(touchscreen);
				}
			}
			else
			{
				Touchscreen touchscreen2 = device as Touchscreen;
				if (touchscreen2 != null)
				{
					Touch.AddTouchscreen(touchscreen2);
					return;
				}
			}
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x00042280 File Offset: 0x00040480
		private static void OnSettingsChange()
		{
			InputSettings.UpdateMode updateMode = InputSystem.settings.updateMode;
			if (EnhancedTouchSupport.s_UpdateMode == updateMode)
			{
				return;
			}
			EnhancedTouchSupport.TearDownState();
			EnhancedTouchSupport.SetUpState();
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x000422AB File Offset: 0x000404AB
		[Conditional("DEVELOPMENT_BUILD")]
		[Conditional("UNITY_EDITOR")]
		internal static void CheckEnabled()
		{
			if (!EnhancedTouchSupport.enabled)
			{
				throw new InvalidOperationException("EnhancedTouch API is not enabled; call EnhancedTouchSupport.Enable()");
			}
		}

		// Token: 0x04000483 RID: 1155
		private static int s_Enabled;

		// Token: 0x04000484 RID: 1156
		private static InputSettings.UpdateMode s_UpdateMode;
	}
}
