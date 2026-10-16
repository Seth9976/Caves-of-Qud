using System;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.Editor
{
	// Token: 0x020000AF RID: 175
	internal struct SampleFrequencyCalculator
	{
		// Token: 0x06000CF6 RID: 3318 RVA: 0x000446A6 File Offset: 0x000428A6
		public SampleFrequencyCalculator(float targetFrequency, double realtimeSinceStartup)
		{
			this.targetFrequency = targetFrequency;
			this.m_SampleCount = 0;
			this.frequency = 0f;
			this.m_LastUpdateTime = realtimeSinceStartup;
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x000446C8 File Offset: 0x000428C8
		// (set) Token: 0x06000CF8 RID: 3320 RVA: 0x000446D0 File Offset: 0x000428D0
		public float targetFrequency { readonly get; private set; }

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000CF9 RID: 3321 RVA: 0x000446D9 File Offset: 0x000428D9
		// (set) Token: 0x06000CFA RID: 3322 RVA: 0x000446E1 File Offset: 0x000428E1
		public float frequency { readonly get; private set; }

		// Token: 0x06000CFB RID: 3323 RVA: 0x000446EA File Offset: 0x000428EA
		public void ProcessSample(InputEventPtr eventPtr)
		{
			if (eventPtr != null)
			{
				this.m_SampleCount++;
			}
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x00044709 File Offset: 0x00042909
		public bool Update()
		{
			return this.Update(Time.realtimeSinceStartupAsDouble);
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x00044718 File Offset: 0x00042918
		public bool Update(double realtimeSinceStartup)
		{
			double num = realtimeSinceStartup - this.m_LastUpdateTime;
			if (num < 1.0)
			{
				return false;
			}
			this.m_LastUpdateTime = realtimeSinceStartup;
			this.frequency = (float)((double)this.m_SampleCount / num);
			this.m_SampleCount = 0;
			return true;
		}

		// Token: 0x040004F9 RID: 1273
		private double m_LastUpdateTime;

		// Token: 0x040004FA RID: 1274
		private int m_SampleCount;
	}
}
