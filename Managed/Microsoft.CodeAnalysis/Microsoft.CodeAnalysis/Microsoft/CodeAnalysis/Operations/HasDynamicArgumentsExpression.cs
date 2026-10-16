using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200046C RID: 1132
	internal abstract class HasDynamicArgumentsExpression : Operation
	{
		// Token: 0x06002C57 RID: 11351 RVA: 0x0007D4C6 File Offset: 0x0007B6C6
		[NullableContext(2)]
		protected HasDynamicArgumentsExpression([Nullable(new byte[] { 0, 1 })] ImmutableArray<IOperation> arguments, [Nullable(new byte[] { 0, 1 })] ImmutableArray<string> argumentNames, [Nullable(0)] ImmutableArray<RefKind> argumentRefKinds, SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, ITypeSymbol type, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Arguments = Operation.SetParentOperation<IOperation>(arguments, this);
			this.ArgumentNames = argumentNames;
			this.ArgumentRefKinds = argumentRefKinds;
			this.Type = type;
		}

		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x06002C58 RID: 11352 RVA: 0x0007D4F7 File Offset: 0x0007B6F7
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<string> ArgumentNames
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x06002C59 RID: 11353 RVA: 0x0007D4FF File Offset: 0x0007B6FF
		public ImmutableArray<RefKind> ArgumentRefKinds { get; }

		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x06002C5A RID: 11354 RVA: 0x0007D507 File Offset: 0x0007B707
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<IOperation> Arguments
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x06002C5B RID: 11355 RVA: 0x0007D50F File Offset: 0x0007B70F
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get;
		}
	}
}
