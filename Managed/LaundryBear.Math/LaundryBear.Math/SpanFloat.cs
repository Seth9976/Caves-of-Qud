using System;
using UnityEngine;

namespace LaundryBear.Math
{
	// Token: 0x02000012 RID: 18
	[Serializable]
	public class SpanFloat : ISpan<float>
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000030D7 File Offset: 0x000012D7
		public float Start
		{
			get
			{
				return this.m_start;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600006C RID: 108 RVA: 0x000030DF File Offset: 0x000012DF
		public float Duration
		{
			get
			{
				return this.m_duration;
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000030E7 File Offset: 0x000012E7
		public bool ContainsPoint(float point)
		{
			return this.m_start <= point && point <= this.m_start + this.m_duration;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003107 File Offset: 0x00001307
		public bool ContainsSpan(ISpan<float> span)
		{
			return this.m_start <= span.Start && span.Start + span.Duration <= this.m_start + this.m_duration;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003138 File Offset: 0x00001338
		public bool Overlaps(ISpan<float> span)
		{
			if (this.StartsBefore(span))
			{
				return this.ContainsPoint(span.Start);
			}
			return span.ContainsPoint(this.m_start);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000315C File Offset: 0x0000135C
		public bool StartsBefore(ISpan<float> span)
		{
			return this.m_start < span.Start;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000316C File Offset: 0x0000136C
		public bool EndsAfter(ISpan<float> span)
		{
			return span.Start + span.Duration < this.m_start + this.m_duration;
		}

		// Token: 0x04000019 RID: 25
		[SerializeField]
		private float m_start;

		// Token: 0x0400001A RID: 26
		[SerializeField]
		private float m_duration;
	}
}
