using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003E1 RID: 993
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class VariableDeclarationGroupOperation : Operation, IVariableDeclarationGroupOperation, IOperation
	{
		// Token: 0x06002603 RID: 9731 RVA: 0x000734FD File Offset: 0x000716FD
		[NullableContext(1)]
		internal VariableDeclarationGroupOperation([Nullable(new byte[] { 0, 1 })] ImmutableArray<IVariableDeclarationOperation> declarations, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Declarations = Operation.SetParentOperation<IVariableDeclarationOperation>(declarations, this);
		}

		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x06002604 RID: 9732 RVA: 0x00073516 File Offset: 0x00071716
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<IVariableDeclarationOperation> Declarations
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x06002605 RID: 9733 RVA: 0x00073520 File Offset: 0x00071720
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot == 0 && index < this.Declarations.Length)
			{
				return this.Declarations[index];
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002606 RID: 9734 RVA: 0x00073568 File Offset: 0x00071768
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (!this.Declarations.IsEmpty)
				{
					return new ValueTuple<bool, int, int>(true, 0, 0);
				}
				break;
			case 0:
				if (previousIndex + 1 < this.Declarations.Length)
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

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x06002607 RID: 9735 RVA: 0x000735DE File Offset: 0x000717DE
		public override ITypeSymbol Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06002608 RID: 9736 RVA: 0x000735E1 File Offset: 0x000717E1
		internal override ConstantValue OperationConstantValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06002609 RID: 9737 RVA: 0x000735E4 File Offset: 0x000717E4
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.VariableDeclarationGroup;
			}
		}

		// Token: 0x0600260A RID: 9738 RVA: 0x000735E7 File Offset: 0x000717E7
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitVariableDeclarationGroup(this);
		}

		// Token: 0x0600260B RID: 9739 RVA: 0x000735F0 File Offset: 0x000717F0
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitVariableDeclarationGroup(this, argument);
		}
	}
}
