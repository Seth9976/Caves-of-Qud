using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CodeGen
{
	// Token: 0x020004CB RID: 1227
	internal struct LocalDebugId : IEquatable<LocalDebugId>
	{
		// Token: 0x06003162 RID: 12642 RVA: 0x0008D8E6 File Offset: 0x0008BAE6
		private LocalDebugId(bool isNone)
		{
			this.SyntaxOffset = -1;
			this.Ordinal = -1;
		}

		// Token: 0x06003163 RID: 12643 RVA: 0x0008D8F6 File Offset: 0x0008BAF6
		public LocalDebugId(int syntaxOffset, int ordinal = 0)
		{
			this.SyntaxOffset = syntaxOffset;
			this.Ordinal = ordinal;
		}

		// Token: 0x17000E11 RID: 3601
		// (get) Token: 0x06003164 RID: 12644 RVA: 0x0008D906 File Offset: 0x0008BB06
		public bool IsNone
		{
			get
			{
				return this.Ordinal == -1;
			}
		}

		// Token: 0x06003165 RID: 12645 RVA: 0x0008D911 File Offset: 0x0008BB11
		public bool Equals(LocalDebugId other)
		{
			return this.SyntaxOffset == other.SyntaxOffset && this.Ordinal == other.Ordinal;
		}

		// Token: 0x06003166 RID: 12646 RVA: 0x0008D931 File Offset: 0x0008BB31
		public override int GetHashCode()
		{
			return Hash.Combine(this.SyntaxOffset, this.Ordinal);
		}

		// Token: 0x06003167 RID: 12647 RVA: 0x0008D944 File Offset: 0x0008BB44
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is LocalDebugId && this.Equals((LocalDebugId)obj);
		}

		// Token: 0x06003168 RID: 12648 RVA: 0x0008D95C File Offset: 0x0008BB5C
		[NullableContext(1)]
		public override string ToString()
		{
			return this.SyntaxOffset.ToString() + ":" + this.Ordinal.ToString();
		}

		// Token: 0x0400130A RID: 4874
		public readonly int SyntaxOffset;

		// Token: 0x0400130B RID: 4875
		public readonly int Ordinal;

		// Token: 0x0400130C RID: 4876
		public static readonly LocalDebugId None = new LocalDebugId(true);
	}
}
