using System;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x02000100 RID: 256
	public class AxisDeadzoneProcessor : InputProcessor<float>
	{
		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06000F21 RID: 3873 RVA: 0x0004A6DD File Offset: 0x000488DD
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

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000F22 RID: 3874 RVA: 0x0004A6FD File Offset: 0x000488FD
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

		// Token: 0x06000F23 RID: 3875 RVA: 0x0004A720 File Offset: 0x00048920
		public override float Process(float value, InputControl control = null)
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

		// Token: 0x06000F24 RID: 3876 RVA: 0x0004A765 File Offset: 0x00048965
		public override string ToString()
		{
			return string.Format("AxisDeadzone(min={0},max={1})", this.minOrDefault, this.maxOrDefault);
		}

		// Token: 0x04000654 RID: 1620
		public float min;

		// Token: 0x04000655 RID: 1621
		public float max;
	}
}
