using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003E0 RID: 992
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class BlockOperation : Operation, IBlockOperation, IOperation
	{
		// Token: 0x060025F7 RID: 9719 RVA: 0x000733CB File Offset: 0x000715CB
		internal BlockOperation([Nullable(new byte[] { 0, 1 })] ImmutableArray<IOperation> operations, [Nullable(new byte[] { 0, 1 })] ImmutableArray<ILocalSymbol> locals, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Operations = Operation.SetParentOperation<IOperation>(operations, this);
			this.Locals = locals;
		}

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x060025F8 RID: 9720 RVA: 0x000733EC File Offset: 0x000715EC
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<IOperation> Operations
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x060025F9 RID: 9721 RVA: 0x000733F4 File Offset: 0x000715F4
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<ILocalSymbol> Locals
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x060025FA RID: 9722 RVA: 0x000733FC File Offset: 0x000715FC
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot == 0 && index < this.Operations.Length)
			{
				return this.Operations[index];
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x060025FB RID: 9723 RVA: 0x00073444 File Offset: 0x00071644
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (!this.Operations.IsEmpty)
				{
					return new ValueTuple<bool, int, int>(true, 0, 0);
				}
				break;
			case 0:
				if (previousIndex + 1 < this.Operations.Length)
				{
					return new ValueTuple<bool, int, int>(true, 0, previousIndex + 1);
				}
				break;
			case 1:
				break;
			default:
				throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(previousSlot, previousIndex));
			}
			return new ValueTuple<bool, int, int>(false, 1, 0);
		}

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x060025FC RID: 9724 RVA: 0x000734BA File Offset: 0x000716BA
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x060025FD RID: 9725 RVA: 0x000734BD File Offset: 0x000716BD
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x060025FE RID: 9726 RVA: 0x000734C0 File Offset: 0x000716C0
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.Block;
			}
		}

		// Token: 0x060025FF RID: 9727 RVA: 0x000734C3 File Offset: 0x000716C3
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitBlock(this);
		}

		// Token: 0x06002600 RID: 9728 RVA: 0x000734CC File Offset: 0x000716CC
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitBlock(this, argument);
		}

		// Token: 0x06002601 RID: 9729 RVA: 0x000734D6 File Offset: 0x000716D6
		public static BlockOperation CreateTemporaryBlock([Nullable(new byte[] { 0, 1 })] ImmutableArray<IOperation> statements, SemanticModel semanticModel, SyntaxNode syntax)
		{
			return new BlockOperation(statements, semanticModel, syntax);
		}

		// Token: 0x06002602 RID: 9730 RVA: 0x000734E0 File Offset: 0x000716E0
		private BlockOperation([Nullable(new byte[] { 0, 1 })] ImmutableArray<IOperation> statements, SemanticModel semanticModel, SyntaxNode syntax)
			: base(semanticModel, syntax, true)
		{
			this.Operations = statements;
			this.Locals = ImmutableArray<ILocalSymbol>.Empty;
		}
	}
}
