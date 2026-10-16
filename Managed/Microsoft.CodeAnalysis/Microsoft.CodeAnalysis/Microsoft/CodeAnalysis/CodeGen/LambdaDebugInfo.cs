using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CodeGen
{
	// Token: 0x020004C9 RID: 1225
	[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
	internal struct LambdaDebugInfo : IEquatable<LambdaDebugInfo>
	{
		// Token: 0x0600314C RID: 12620 RVA: 0x0008D70F File Offset: 0x0008B90F
		public LambdaDebugInfo(int syntaxOffset, DebugId lambdaId, int closureOrdinal)
		{
			this.SyntaxOffset = syntaxOffset;
			this.ClosureOrdinal = closureOrdinal;
			this.LambdaId = lambdaId;
		}

		// Token: 0x0600314D RID: 12621 RVA: 0x0008D728 File Offset: 0x0008B928
		public bool Equals(LambdaDebugInfo other)
		{
			return this.SyntaxOffset == other.SyntaxOffset && this.ClosureOrdinal == other.ClosureOrdinal && this.LambdaId.Equals(other.LambdaId);
		}

		// Token: 0x0600314E RID: 12622 RVA: 0x0008D767 File Offset: 0x0008B967
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is LambdaDebugInfo && this.Equals((LambdaDebugInfo)obj);
		}

		// Token: 0x0600314F RID: 12623 RVA: 0x0008D780 File Offset: 0x0008B980
		public override int GetHashCode()
		{
			return Hash.Combine(this.ClosureOrdinal, Hash.Combine(this.SyntaxOffset, this.LambdaId.GetHashCode()));
		}

		// Token: 0x06003150 RID: 12624 RVA: 0x0008D7B8 File Offset: 0x0008B9B8
		[NullableContext(1)]
		internal string GetDebuggerDisplay()
		{
			if (this.ClosureOrdinal == -1)
			{
				return string.Format("({0} @{1}, static)", this.LambdaId.GetDebuggerDisplay(), this.SyntaxOffset);
			}
			if (this.ClosureOrdinal != -2)
			{
				return string.Format("({0} @{1} in {2})", this.LambdaId.GetDebuggerDisplay(), this.SyntaxOffset, this.ClosureOrdinal);
			}
			return string.Format("(#{0} @{1}, this)", this.LambdaId.GetDebuggerDisplay(), this.SyntaxOffset);
		}

		// Token: 0x040012FF RID: 4863
		public readonly int SyntaxOffset;

		// Token: 0x04001300 RID: 4864
		public readonly int ClosureOrdinal;

		// Token: 0x04001301 RID: 4865
		public readonly DebugId LambdaId;

		// Token: 0x04001302 RID: 4866
		public const int StaticClosureOrdinal = -1;

		// Token: 0x04001303 RID: 4867
		public const int ThisOnlyClosureOrdinal = -2;

		// Token: 0x04001304 RID: 4868
		public const int MinClosureOrdinal = -2;
	}
}
