using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200000B RID: 11
	public struct ArrayDimension
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002B6C File Offset: 0x00000D6C
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00002B74 File Offset: 0x00000D74
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

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002B7D File Offset: 0x00000D7D
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00002B85 File Offset: 0x00000D85
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

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002B8E File Offset: 0x00000D8E
		public bool IsSized
		{
			get
			{
				return this.lower_bound != null || this.upper_bound != null;
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002BAA File Offset: 0x00000DAA
		public ArrayDimension(int? lowerBound, int? upperBound)
		{
			this.lower_bound = lowerBound;
			this.upper_bound = upperBound;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002BBA File Offset: 0x00000DBA
		public override string ToString()
		{
			if (this.IsSized)
			{
				return this.lower_bound + "..." + this.upper_bound;
			}
			return string.Empty;
		}

		// Token: 0x0400000A RID: 10
		private int? lower_bound;

		// Token: 0x0400000B RID: 11
		private int? upper_bound;
	}
}
