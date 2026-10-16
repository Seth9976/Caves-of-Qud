using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000424 RID: 1060
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class BaseSymbolInitializerOperation : Operation, ISymbolInitializerOperation, IOperation
	{
		// Token: 0x060028AC RID: 10412 RVA: 0x000774B3 File Offset: 0x000756B3
		protected BaseSymbolInitializerOperation([Nullable(new byte[] { 0, 1 })] ImmutableArray<ILocalSymbol> locals, IOperation value, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Locals = locals;
			this.Value = Operation.SetParentOperation<IOperation>(value, this);
		}

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x060028AD RID: 10413 RVA: 0x000774D4 File Offset: 0x000756D4
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<ILocalSymbol> Locals
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x060028AE RID: 10414 RVA: 0x000774DC File Offset: 0x000756DC
		public IOperation Value { get; }
	}
}
