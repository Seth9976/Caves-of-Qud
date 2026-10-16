using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CodeGen
{
	// Token: 0x020004C0 RID: 1216
	[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
	internal struct ClosureDebugInfo : IEquatable<ClosureDebugInfo>
	{
		// Token: 0x06003120 RID: 12576 RVA: 0x0008C829 File Offset: 0x0008AA29
		public ClosureDebugInfo(int syntaxOffset, DebugId closureId)
		{
			this.SyntaxOffset = syntaxOffset;
			this.ClosureId = closureId;
		}

		// Token: 0x06003121 RID: 12577 RVA: 0x0008C83C File Offset: 0x0008AA3C
		public bool Equals(ClosureDebugInfo other)
		{
			return this.SyntaxOffset == other.SyntaxOffset && this.ClosureId.Equals(other.ClosureId);
		}

		// Token: 0x06003122 RID: 12578 RVA: 0x0008C86D File Offset: 0x0008AA6D
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is ClosureDebugInfo && this.Equals((ClosureDebugInfo)obj);
		}

		// Token: 0x06003123 RID: 12579 RVA: 0x0008C888 File Offset: 0x0008AA88
		public override int GetHashCode()
		{
			return Hash.Combine(this.SyntaxOffset, this.ClosureId.GetHashCode());
		}

		// Token: 0x06003124 RID: 12580 RVA: 0x0008C8B4 File Offset: 0x0008AAB4
		[NullableContext(1)]
		internal string GetDebuggerDisplay()
		{
			return string.Format("({0} @{1})", this.ClosureId.GetDebuggerDisplay(), this.SyntaxOffset);
		}

		// Token: 0x040012ED RID: 4845
		public readonly int SyntaxOffset;

		// Token: 0x040012EE RID: 4846
		public readonly DebugId ClosureId;
	}
}
