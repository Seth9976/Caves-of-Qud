using System;

namespace Mono.Cecil
{
	// Token: 0x020001B0 RID: 432
	internal struct ArrayDimension
	{
		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x0001A94F File Offset: 0x00018B4F
		// (set) Token: 0x0600079D RID: 1949 RVA: 0x0001A957 File Offset: 0x00018B57
		public int? LowerBound
		{
			get
			{
				return this.lower_bound;
			}
			set
			{
				this.lower_bound = value;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x0001A960 File Offset: 0x00018B60
		// (set) Token: 0x0600079F RID: 1951 RVA: 0x0001A968 File Offset: 0x00018B68
		public int? UpperBound
		{
			get
			{
				return this.upper_bound;
			}
			set
			{
				this.upper_bound = value;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x0001A971 File Offset: 0x00018B71
		public bool IsSized
		{
			get
			{
				return this.lower_bound != null || this.upper_bound != null;
			}
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0001A98D File Offset: 0x00018B8D
		public ArrayDimension(int? lowerBound, int? upperBound)
		{
			this.lower_bound = lowerBound;
			this.upper_bound = upperBound;
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0001A9A0 File Offset: 0x00018BA0
		public override string ToString()
		{
			if (this.IsSized)
			{
				int? num = this.lower_bound;
				string text = num.ToString();
				string text2 = "...";
				num = this.upper_bound;
				return text + text2 + num.ToString();
			}
			return string.Empty;
		}

		// Token: 0x0400026A RID: 618
		private int? lower_bound;

		// Token: 0x0400026B RID: 619
		private int? upper_bound;
	}
}
