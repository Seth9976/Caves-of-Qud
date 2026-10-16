using System;

namespace UnityEngine
{
	// Token: 0x020002C4 RID: 708
	public class WaitForSecondsRealtime : CustomYieldInstruction
	{
		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06002270 RID: 8816 RVA: 0x0004366E File Offset: 0x0004186E
		// (set) Token: 0x06002271 RID: 8817 RVA: 0x00043676 File Offset: 0x00041876
		public float waitTime { get; set; }

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06002272 RID: 8818 RVA: 0x00043680 File Offset: 0x00041880
		public override bool keepWaiting
		{
			get
			{
				bool flag = this.m_WaitUntilTime < 0f;
				if (flag)
				{
					this.m_WaitUntilTime = Time.realtimeSinceStartup + this.waitTime;
				}
				bool flag2 = Time.realtimeSinceStartup < this.m_WaitUntilTime;
				bool flag3 = !flag2;
				if (flag3)
				{
					this.Reset();
				}
				return flag2;
			}
		}

		// Token: 0x06002273 RID: 8819 RVA: 0x000436D7 File Offset: 0x000418D7
		public WaitForSecondsRealtime(float time)
		{
			this.waitTime = time;
		}

		// Token: 0x06002274 RID: 8820 RVA: 0x000436F4 File Offset: 0x000418F4
		public override void Reset()
		{
			this.m_WaitUntilTime = -1f;
		}

		// Token: 0x04000A0A RID: 2570
		private float m_WaitUntilTime = -1f;
	}
}
