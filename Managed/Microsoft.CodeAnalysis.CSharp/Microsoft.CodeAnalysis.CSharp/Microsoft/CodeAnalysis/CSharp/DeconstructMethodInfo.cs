using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000049 RID: 73
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal struct DeconstructMethodInfo
	{
		// Token: 0x06000772 RID: 1906 RVA: 0x0003E178 File Offset: 0x0003C378
		internal DeconstructMethodInfo(BoundExpression invocation, BoundDeconstructValuePlaceholder inputPlaceholder, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundDeconstructValuePlaceholder> outputPlaceholders)
		{
			this.Invocation = invocation;
			this.InputPlaceholder = inputPlaceholder;
			this.OutputPlaceholders = outputPlaceholders;
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000773 RID: 1907 RVA: 0x0003E1A0 File Offset: 0x0003C3A0
		internal bool IsDefault
		{
			get
			{
				return this.Invocation == null;
			}
		}

		// Token: 0x040000E2 RID: 226
		internal readonly BoundExpression Invocation;

		// Token: 0x040000E3 RID: 227
		internal readonly BoundDeconstructValuePlaceholder InputPlaceholder;

		// Token: 0x040000E4 RID: 228
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal readonly ImmutableArray<BoundDeconstructValuePlaceholder> OutputPlaceholders;
	}
}
