using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000438 RID: 1080
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class BasePatternOperation : Operation, IPatternOperation, IOperation
	{
		// Token: 0x06002966 RID: 10598 RVA: 0x000785AD File Offset: 0x000767AD
		protected BasePatternOperation(ITypeSymbol inputType, ITypeSymbol narrowedType, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.InputType = inputType;
			this.NarrowedType = narrowedType;
		}

		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x06002967 RID: 10599 RVA: 0x000785C8 File Offset: 0x000767C8
		public ITypeSymbol InputType { get; }

		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x06002968 RID: 10600 RVA: 0x000785D0 File Offset: 0x000767D0
		public ITypeSymbol NarrowedType { get; }
	}
}
