using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CodeGen
{
	// Token: 0x020004C3 RID: 1219
	[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
	internal struct DebugId : IEquatable<DebugId>
	{
		// Token: 0x0600312E RID: 12590 RVA: 0x0008CAEA File Offset: 0x0008ACEA
		public DebugId(int ordinal, int generation)
		{
			this.Ordinal = ordinal;
			this.Generation = generation;
		}

		// Token: 0x0600312F RID: 12591 RVA: 0x0008CAFA File Offset: 0x0008ACFA
		public bool Equals(DebugId other)
		{
			return this.Ordinal == other.Ordinal && this.Generation == other.Generation;
		}

		// Token: 0x06003130 RID: 12592 RVA: 0x0008CB1A File Offset: 0x0008AD1A
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is DebugId && this.Equals((DebugId)obj);
		}

		// Token: 0x06003131 RID: 12593 RVA: 0x0008CB32 File Offset: 0x0008AD32
		public override int GetHashCode()
		{
			return Hash.Combine(this.Ordinal, this.Generation);
		}

		// Token: 0x06003132 RID: 12594 RVA: 0x0008CB48 File Offset: 0x0008AD48
		[NullableContext(1)]
		internal string GetDebuggerDisplay()
		{
			if (this.Generation <= 0)
			{
				return this.Ordinal.ToString();
			}
			return string.Format("{0}#{1}", this.Ordinal, this.Generation);
		}

		// Token: 0x040012F5 RID: 4853
		public const int UndefinedOrdinal = -1;

		// Token: 0x040012F6 RID: 4854
		public readonly int Ordinal;

		// Token: 0x040012F7 RID: 4855
		public readonly int Generation;
	}
}
