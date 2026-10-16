using System;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000143 RID: 323
	[NullableContext(2)]
	[Nullable(0)]
	internal readonly struct LoadDirective : IEquatable<LoadDirective>
	{
		// Token: 0x06000D14 RID: 3348 RVA: 0x00026E49 File Offset: 0x00025049
		public LoadDirective(string resolvedPath, [Nullable(new byte[] { 0, 1 })] ImmutableArray<Diagnostic> diagnostics)
		{
			this.ResolvedPath = resolvedPath;
			this.Diagnostics = diagnostics;
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x00026E59 File Offset: 0x00025059
		public bool Equals(LoadDirective other)
		{
			return this.ResolvedPath == other.ResolvedPath && this.Diagnostics.SequenceEqual<Diagnostic, Diagnostic>(other.Diagnostics, null);
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x00026E82 File Offset: 0x00025082
		public override bool Equals(object obj)
		{
			return obj is LoadDirective && this.Equals((LoadDirective)obj);
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x00026E9C File Offset: 0x0002509C
		public override int GetHashCode()
		{
			int hashCode = this.Diagnostics.GetHashCode();
			string resolvedPath = this.ResolvedPath;
			return Hash.Combine(hashCode, (resolvedPath != null) ? resolvedPath.GetHashCode() : 0);
		}

		// Token: 0x04000393 RID: 915
		public readonly string ResolvedPath;

		// Token: 0x04000394 RID: 916
		[Nullable(new byte[] { 0, 1 })]
		public readonly ImmutableArray<Diagnostic> Diagnostics;
	}
}
