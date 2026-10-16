using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003C2 RID: 962
	[NullableContext(2)]
	public interface IInterpolationOperation : IInterpolatedStringContentOperation, IOperation
	{
		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x060025BB RID: 9659
		[Nullable(1)]
		IOperation Expression
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x060025BC RID: 9660
		IOperation Alignment { get; }

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x060025BD RID: 9661
		IOperation FormatString { get; }
	}
}
