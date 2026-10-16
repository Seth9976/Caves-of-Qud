using System;
using System.Diagnostics;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000D2 RID: 210
	[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
	internal sealed class ConversionGroup
	{
		// Token: 0x0600113A RID: 4410 RVA: 0x0005DEAF File Offset: 0x0005C0AF
		internal ConversionGroup(Conversion conversion, TypeWithAnnotations explicitType = default(TypeWithAnnotations))
		{
			this.Conversion = conversion;
			this.ExplicitType = explicitType;
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x0600113B RID: 4411 RVA: 0x0005DEC5 File Offset: 0x0005C0C5
		internal bool IsExplicitConversion
		{
			get
			{
				return this.ExplicitType.HasType;
			}
		}

		// Token: 0x04000583 RID: 1411
		internal readonly Conversion Conversion;

		// Token: 0x04000584 RID: 1412
		internal readonly TypeWithAnnotations ExplicitType;
	}
}
