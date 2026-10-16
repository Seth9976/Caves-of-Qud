using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200042B RID: 1067
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class VariableDeclarationOperation : Operation, IVariableDeclarationOperation, IOperation
	{
		// Token: 0x060028E6 RID: 10470 RVA: 0x000779C4 File Offset: 0x00075BC4
		internal VariableDeclarationOperation([Nullable(new byte[] { 0, 1 })] ImmutableArray<IVariableDeclaratorOperation> declarators, IVariableInitializerOperation initializer, [Nullable(new byte[] { 0, 1 })] ImmutableArray<IOperation> ignoredDimensions, SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Declarators = Operation.SetParentOperation<IVariableDeclaratorOperation>(declarators, this);
			this.Initializer = Operation.SetParentOperation<IVariableInitializerOperation>(initializer, this);
			this.IgnoredDimensions = Operation.SetParentOperation<IOperation>(ignoredDimensions, this);
		}

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x060028E7 RID: 10471 RVA: 0x000779F9 File Offset: 0x00075BF9
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<IVariableDeclaratorOperation> Declarators
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x060028E8 RID: 10472 RVA: 0x00077A01 File Offset: 0x00075C01
		public IVariableInitializerOperation Initializer { get; }

		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x060028E9 RID: 10473 RVA: 0x00077A09 File Offset: 0x00075C09
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<IOperation> IgnoredDimensions
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x060028EA RID: 10474 RVA: 0x00077A14 File Offset: 0x00075C14
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			switch (slot)
			{
			case 0:
				if (index < this.IgnoredDimensions.Length)
				{
					return this.IgnoredDimensions[index];
				}
				break;
			case 1:
				if (index < this.Declarators.Length)
				{
					return this.Declarators[index];
				}
				break;
			case 2:
				if (this.Initializer != null)
				{
					return this.Initializer;
				}
				break;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x060028EB RID: 10475 RVA: 0x00077AA4 File Offset: 0x00075CA4
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (!this.IgnoredDimensions.IsEmpty)
				{
					return new ValueTuple<bool, int, int>(true, 0, 0);
				}
				break;
			case 0:
				if (previousIndex + 1 < this.IgnoredDimensions.Length)
				{
					return new ValueTuple<bool, int, int>(true, 0, previousIndex + 1);
				}
				break;
			case 1:
				if (previousIndex + 1 < this.Declarators.Length)
				{
					return new ValueTuple<bool, int, int>(true, 1, previousIndex + 1);
				}
				goto IL_0091;
			case 2:
			case 3:
				IL_00A2:
				return new ValueTuple<bool, int, int>(false, 3, 0);
			default:
				throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(previousSlot, previousIndex));
			}
			if (!this.Declarators.IsEmpty)
			{
				return new ValueTuple<bool, int, int>(true, 1, 0);
			}
			IL_0091:
			if (this.Initializer != null)
			{
				return new ValueTuple<bool, int, int>(true, 2, 0);
			}
			goto IL_00A2;
		}

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x060028EC RID: 10476 RVA: 0x00077B6D File Offset: 0x00075D6D
		public override ITypeSymbol Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x060028ED RID: 10477 RVA: 0x00077B70 File Offset: 0x00075D70
		internal override ConstantValue OperationConstantValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x060028EE RID: 10478 RVA: 0x00077B73 File Offset: 0x00075D73
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.VariableDeclaration;
			}
		}

		// Token: 0x060028EF RID: 10479 RVA: 0x00077B77 File Offset: 0x00075D77
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitVariableDeclaration(this);
		}

		// Token: 0x060028F0 RID: 10480 RVA: 0x00077B80 File Offset: 0x00075D80
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitVariableDeclaration(this, argument);
		}
	}
}
