using System;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.Haptics
{
	// Token: 0x020000B0 RID: 176
	internal struct DualMotorRumble
	{
		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000CFE RID: 3326 RVA: 0x0004475B File Offset: 0x0004295B
		// (set) Token: 0x06000CFF RID: 3327 RVA: 0x00044763 File Offset: 0x00042963
		public float lowFrequencyMotorSpeed { readonly get; private set; }

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000D00 RID: 3328 RVA: 0x0004476C File Offset: 0x0004296C
		// (set) Token: 0x06000D01 RID: 3329 RVA: 0x00044774 File Offset: 0x00042974
		public float highFrequencyMotorSpeed { readonly get; private set; }

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000D02 RID: 3330 RVA: 0x0004477D File Offset: 0x0004297D
		public bool isRumbling
		{
			get
			{
				return !Mathf.Approximately(this.lowFrequencyMotorSpeed, 0f) || !Mathf.Approximately(this.highFrequencyMotorSpeed, 0f);
			}
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x000447A8 File Offset: 0x000429A8
		public void PauseHaptics(InputDevice device)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (!this.isRumbling)
			{
				return;
			}
			DualMotorRumbleCommand dualMotorRumbleCommand = DualMotorRumbleCommand.Create(0f, 0f);
			device.ExecuteCommand<DualMotorRumbleCommand>(ref dualMotorRumbleCommand);
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x000447E5 File Offset: 0x000429E5
		public void ResumeHaptics(InputDevice device)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (!this.isRumbling)
			{
				return;
			}
			this.SetMotorSpeeds(device, this.lowFrequencyMotorSpeed, this.highFrequencyMotorSpeed);
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x00044811 File Offset: 0x00042A11
		public void ResetHaptics(InputDevice device)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (!this.isRumbling)
			{
				return;
			}
			this.SetMotorSpeeds(device, 0f, 0f);
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x0004483C File Offset: 0x00042A3C
		public void SetMotorSpeeds(InputDevice device, float lowFrequency, float highFrequency)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			this.lowFrequencyMotorSpeed = Mathf.Clamp(lowFrequency, 0f, 1f);
			this.highFrequencyMotorSpeed = Mathf.Clamp(highFrequency, 0f, 1f);
			DualMotorRumbleCommand dualMotorRumbleCommand = DualMotorRumbleCommand.Create(this.lowFrequencyMotorSpeed, this.highFrequencyMotorSpeed);
			device.ExecuteCommand<DualMotorRumbleCommand>(ref dualMotorRumbleCommand);
		}
	}
}
