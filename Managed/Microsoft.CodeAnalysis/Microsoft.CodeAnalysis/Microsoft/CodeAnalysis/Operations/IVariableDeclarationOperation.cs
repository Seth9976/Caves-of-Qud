using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003B6 RID: 950
	[NullableContext(2)]
	public interface IVariableDeclarationOperation : IOperation
	{
		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x060025A0 RID: 9632
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IVariableDeclaratorOperation> Declarators
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x060025A1 RID: 9633
		IVariableInitializerOperation Initializer { get; }

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x060025A2 RID: 9634
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IOperation> IgnoredDimensions
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}
	}
}
