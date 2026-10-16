using System;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x0200010D RID: 269
	public class StickDeadzoneProcessor : InputProcessor<Vector2>
	{
		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06000F4E RID: 3918 RVA: 0x0004AC58 File Offset: 0x00048E58
		private float minOrDefault
		{
			get
			{
				if (this.min != 0f)
				{
					return this.min;
				}
				return InputSystem.settings.defaultDeadzoneMin;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06000F4F RID: 3919 RVA: 0x0004AC78 File Offset: 0x00048E78
		private float maxOrDefault
		{
			get
			{
				if (this.max != 0f)
				{
					return this.max;
				}
				return InputSystem.settings.defaultDeadzoneMax;
			}
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x0004AC98 File Offset: 0x00048E98
		public override Vector2 Process(Vector2 value, InputControl control = null)
		{
			float magnitude = value.magnitude;
			float deadZoneAdjustedValue = this.GetDeadZoneAdjustedValue(magnitude);
			if (deadZoneAdjustedValue == 0f)
			{
				value = Vector2.zero;
			}
			else
			{
				value *= deadZoneAdjustedValue / magnitude;
			}
			return value;
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x0004ACD4 File Offset: 0x00048ED4
		private float GetDeadZoneAdjustedValue(float value)
		{
			float minOrDefault = this.minOrDefault;
			float maxOrDefault = this.maxOrDefault;
			float num = Mathf.Abs(value);
			if (num < minOrDefault)
			{
				return 0f;
			}
			if (num > maxOrDefault)
			{
				return Mathf.Sign(value);
			}
			return Mathf.Sign(value) * ((num - minOrDefault) / (maxOrDefault - minOrDefault));
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x0004AD19 File Offset: 0x00048F19
		public override string ToString()
		{
			return string.Format("StickDeadzone(min={0},max={1})", this.minOrDefault, this.maxOrDefault);
		}

		// Token: 0x04000666 RID: 1638
		public float min;

		// Token: 0x04000667 RID: 1639
		public float max;
	}
}
