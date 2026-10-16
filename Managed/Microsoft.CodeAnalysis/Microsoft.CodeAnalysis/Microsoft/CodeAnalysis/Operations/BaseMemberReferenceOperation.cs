using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003FA RID: 1018
	[NullableContext(2)]
	[Nullable(0)]
	internal abstract class BaseMemberReferenceOperation : Operation, IMemberReferenceOperation, IOperation
	{
		// Token: 0x06002710 RID: 10000 RVA: 0x00074FFE File Offset: 0x000731FE
		protected BaseMemberReferenceOperation(IOperation instance, SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Instance = Operation.SetParentOperation<IOperation>(instance, this);
		}

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x06002711 RID: 10001 RVA: 0x00075017 File Offset: 0x00073217
		public IOperation Instance { get; }

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x06002712 RID: 10002
		[Nullable(1)]
		public abstract ISymbol Member
		{
			[NullableContext(1)]
			get;
		}
	}
}
