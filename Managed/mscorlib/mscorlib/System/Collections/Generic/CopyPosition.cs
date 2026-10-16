using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x02000AA8 RID: 2728
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	internal readonly struct CopyPosition
	{
		// Token: 0x060061AC RID: 25004 RVA: 0x00146844 File Offset: 0x00144A44
		internal CopyPosition(int row, int column)
		{
			this.Row = row;
			this.Column = column;
		}

		// Token: 0x1700115E RID: 4446
		// (get) Token: 0x060061AD RID: 25005 RVA: 0x00146854 File Offset: 0x00144A54
		public static CopyPosition Start
		{
			get
			{
				return default(CopyPosition);
			}
		}

		// Token: 0x1700115F RID: 4447
		// (get) Token: 0x060061AE RID: 25006 RVA: 0x0014686A File Offset: 0x00144A6A
		internal int Row { get; }

		// Token: 0x17001160 RID: 4448
		// (get) Token: 0x060061AF RID: 25007 RVA: 0x00146872 File Offset: 0x00144A72
		internal int Column { get; }

		// Token: 0x060061B0 RID: 25008 RVA: 0x0014687A File Offset: 0x00144A7A
		public CopyPosition Normalize(int endColumn)
		{
			if (this.Column != endColumn)
			{
				return this;
			}
			return new CopyPosition(this.Row + 1, 0);
		}

		// Token: 0x17001161 RID: 4449
		// (get) Token: 0x060061B1 RID: 25009 RVA: 0x0014689A File Offset: 0x00144A9A
		private string DebuggerDisplay
		{
			get
			{
				return string.Format("[{0}, {1}]", this.Row, this.Column);
			}
		}
	}
}
