using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200043E RID: 1086
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class ConstructorBodyOperation : BaseMethodBodyBaseOperation, IConstructorBodyOperation, IMethodBodyBaseOperation, IOperation
	{
		// Token: 0x06002993 RID: 10643 RVA: 0x00078944 File Offset: 0x00076B44
		internal ConstructorBodyOperation([Nullable(new byte[] { 0, 1 })] ImmutableArray<ILocalSymbol> locals, IOperation initializer, IBlockOperation blockBody, IBlockOperation expressionBody, SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, bool isImplicit)
			: base(blockBody, expressionBody, semanticModel, syntax, isImplicit)
		{
			this.Locals = locals;
			this.Initializer = Operation.SetParentOperation<IOperation>(initializer, this);
		}

		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x06002994 RID: 10644 RVA: 0x00078969 File Offset: 0x00076B69
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<ILocalSymbol> Locals
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x06002995 RID: 10645 RVA: 0x00078971 File Offset: 0x00076B71
		public IOperation Initializer { get; }

		// Token: 0x06002996 RID: 10646 RVA: 0x0007897C File Offset: 0x00076B7C
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			switch (slot)
			{
			case 0:
				if (this.Initializer != null)
				{
					return this.Initializer;
				}
				break;
			case 1:
				if (base.BlockBody != null)
				{
					return base.BlockBody;
				}
				break;
			case 2:
				if (base.ExpressionBody != null)
				{
					return base.ExpressionBody;
				}
				break;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002997 RID: 10647 RVA: 0x000789E8 File Offset: 0x00076BE8
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (this.Initializer != null)
				{
					return new ValueTuple<bool, int, int>(true, 0, 0);
				}
				break;
			case 0:
				break;
			case 1:
				goto IL_0040;
			case 2:
			case 3:
				goto IL_0051;
			default:
				throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(previousSlot, previousIndex));
			}
			if (base.BlockBody != null)
			{
				return new ValueTuple<bool, int, int>(true, 1, 0);
			}
			IL_0040:
			if (base.ExpressionBody != null)
			{
				return new ValueTuple<bool, int, int>(true, 2, 0);
			}
			IL_0051:
			return new ValueTuple<bool, int, int>(false, 3, 0);
		}

		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x06002998 RID: 10648 RVA: 0x00078A60 File Offset: 0x00076C60
		public override ITypeSymbol Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x06002999 RID: 10649 RVA: 0x00078A63 File Offset: 0x00076C63
		internal override ConstantValue OperationConstantValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x0600299A RID: 10650 RVA: 0x00078A66 File Offset: 0x00076C66
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.ConstructorBody;
			}
		}

		// Token: 0x0600299B RID: 10651 RVA: 0x00078A6A File Offset: 0x00076C6A
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitConstructorBodyOperation(this);
		}

		// Token: 0x0600299C RID: 10652 RVA: 0x00078A73 File Offset: 0x00076C73
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitConstructorBodyOperation(this, argument);
		}
	}
}
