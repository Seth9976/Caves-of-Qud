using System;

namespace Microsoft.CodeAnalysis.CSharp.CodeGen
{
	// Token: 0x020005F7 RID: 1527
	internal struct LocalDefUseSpan
	{
		// Token: 0x060080F7 RID: 33015 RVA: 0x001D5468 File Offset: 0x001D3668
		public LocalDefUseSpan(int start)
		{
			this = new LocalDefUseSpan(start, start);
		}

		// Token: 0x060080F8 RID: 33016 RVA: 0x001D5472 File Offset: 0x001D3672
		private LocalDefUseSpan(int start, int end)
		{
			this.Start = start;
			this.End = end;
		}

		// Token: 0x060080F9 RID: 33017 RVA: 0x001D5482 File Offset: 0x001D3682
		internal LocalDefUseSpan WithEnd(int end)
		{
			return new LocalDefUseSpan(this.Start, end);
		}

		// Token: 0x060080FA RID: 33018 RVA: 0x001D5490 File Offset: 0x001D3690
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"[",
				this.Start.ToString(),
				" ,",
				this.End.ToString(),
				")"
			});
		}

		// Token: 0x060080FB RID: 33019 RVA: 0x001D54E2 File Offset: 0x001D36E2
		public bool ConflictsWith(LocalDefUseSpan other)
		{
			return this.Contains(other.Start) ^ this.Contains(other.End);
		}

		// Token: 0x060080FC RID: 33020 RVA: 0x001D54FD File Offset: 0x001D36FD
		private bool Contains(int val)
		{
			return this.Start < val && this.End > val;
		}

		// Token: 0x060080FD RID: 33021 RVA: 0x001D5513 File Offset: 0x001D3713
		public bool ConflictsWithDummy(LocalDefUseSpan dummy)
		{
			return this.Includes(dummy.Start) ^ this.Includes(dummy.End);
		}

		// Token: 0x060080FE RID: 33022 RVA: 0x001D552E File Offset: 0x001D372E
		private bool Includes(int val)
		{
			return this.Start <= val && this.End >= val;
		}

		// Token: 0x04001E4B RID: 7755
		public readonly int Start;

		// Token: 0x04001E4C RID: 7756
		public readonly int End;
	}
}
