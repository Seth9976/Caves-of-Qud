using System;
using System.Runtime.CompilerServices;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x02000117 RID: 279
	public class ButtonControl : AxisControl
	{
		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06001025 RID: 4133 RVA: 0x0004EE42 File Offset: 0x0004D042
		// (set) Token: 0x06001026 RID: 4134 RVA: 0x0004EE4A File Offset: 0x0004D04A
		internal bool needsToCheckFramePress { get; private set; }

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06001027 RID: 4135 RVA: 0x0004EE53 File Offset: 0x0004D053
		public float pressPointOrDefault
		{
			get
			{
				if (this.pressPoint <= 0f)
				{
					return ButtonControl.s_GlobalDefaultButtonPressPoint;
				}
				return this.pressPoint;
			}
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x0004EE70 File Offset: 0x0004D070
		public ButtonControl()
		{
			this.m_StateBlock.format = InputStateBlock.FormatBit;
			this.m_MinValue = 0f;
			this.m_MaxValue = 1f;
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x0004EECC File Offset: 0x0004D0CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public new bool IsValueConsideredPressed(float value)
		{
			return value >= this.pressPointOrDefault;
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x0600102A RID: 4138 RVA: 0x0004EEDA File Offset: 0x0004D0DA
		public unsafe bool isPressed
		{
			get
			{
				if (!this.needsToCheckFramePress)
				{
					return this.IsValueConsideredPressed(*base.value);
				}
				return this.m_LastUpdateWasPress;
			}
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x0004EEF8 File Offset: 0x0004D0F8
		private void BeginTestingForFramePresses(bool currentlyPressed, bool pressedLastFrame)
		{
			this.needsToCheckFramePress = true;
			base.device.m_ButtonControlsCheckingPressState.Add(this);
			this.m_LastUpdateWasPress = currentlyPressed;
			if (currentlyPressed && !pressedLastFrame)
			{
				this.m_UpdateCountLastPressed = base.device.m_CurrentUpdateStepCount;
				return;
			}
			if (pressedLastFrame && !currentlyPressed)
			{
				this.m_UpdateCountLastReleased = base.device.m_CurrentUpdateStepCount;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x0600102C RID: 4140 RVA: 0x0004EF54 File Offset: 0x0004D154
		public unsafe bool wasPressedThisFrame
		{
			get
			{
				if (!this.needsToCheckFramePress)
				{
					bool flag = this.IsValueConsideredPressed(*base.value);
					bool flag2 = this.IsValueConsideredPressed(base.ReadValueFromPreviousFrame());
					this.BeginTestingForFramePresses(flag, flag2);
					return base.device.wasUpdatedThisFrame && flag && !flag2;
				}
				return InputUpdate.s_UpdateStepCount == this.m_UpdateCountLastPressed;
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x0600102D RID: 4141 RVA: 0x0004EFB0 File Offset: 0x0004D1B0
		public unsafe bool wasReleasedThisFrame
		{
			get
			{
				if (!this.needsToCheckFramePress)
				{
					bool flag = this.IsValueConsideredPressed(*base.value);
					bool flag2 = this.IsValueConsideredPressed(base.ReadValueFromPreviousFrame());
					this.BeginTestingForFramePresses(flag, flag2);
					return base.device.wasUpdatedThisFrame && !flag && flag2;
				}
				return InputUpdate.s_UpdateStepCount == this.m_UpdateCountLastReleased;
			}
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x0004F00C File Offset: 0x0004D20C
		internal unsafe void UpdateWasPressed()
		{
			bool flag = this.IsValueConsideredPressed(*base.value);
			if (this.m_LastUpdateWasPress != flag)
			{
				if (flag)
				{
					this.m_UpdateCountLastPressed = base.device.m_CurrentUpdateStepCount;
				}
				else
				{
					this.m_UpdateCountLastReleased = base.device.m_CurrentUpdateStepCount;
				}
				this.m_LastUpdateWasPress = flag;
			}
		}

		// Token: 0x040006BD RID: 1725
		private bool m_NeedsToCheckFramePress;

		// Token: 0x040006BE RID: 1726
		private uint m_UpdateCountLastPressed = uint.MaxValue;

		// Token: 0x040006BF RID: 1727
		private uint m_UpdateCountLastReleased = uint.MaxValue;

		// Token: 0x040006C0 RID: 1728
		private bool m_LastUpdateWasPress;

		// Token: 0x040006C2 RID: 1730
		public float pressPoint = -1f;

		// Token: 0x040006C3 RID: 1731
		internal static float s_GlobalDefaultButtonPressPoint;

		// Token: 0x040006C4 RID: 1732
		internal static float s_GlobalDefaultButtonReleaseThreshold;

		// Token: 0x040006C5 RID: 1733
		internal const float kMinButtonPressPoint = 0.0001f;
	}
}
