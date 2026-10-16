using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000458 RID: 1112
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class WithOperation : Operation, IWithOperation, IOperation
	{
		// Token: 0x06002A98 RID: 10904 RVA: 0x0007A024 File Offset: 0x00078224
		internal WithOperation(IOperation operand, [Nullable(2)] IMethodSymbol cloneMethod, IObjectOrCollectionInitializerOperation initializer, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, [Nullable(2)] ITypeSymbol type, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Operand = Operation.SetParentOperation<IOperation>(operand, this);
			this.CloneMethod = cloneMethod;
			this.Initializer = Operation.SetParentOperation<IObjectOrCollectionInitializerOperation>(initializer, this);
			this.Type = type;
		}

		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x06002A99 RID: 10905 RVA: 0x0007A05B File Offset: 0x0007825B
		public IOperation Operand { get; }

		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x06002A9A RID: 10906 RVA: 0x0007A063 File Offset: 0x00078263
		[Nullable(2)]
		public IMethodSymbol CloneMethod
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x06002A9B RID: 10907 RVA: 0x0007A06B File Offset: 0x0007826B
		public IObjectOrCollectionInitializerOperation Initializer { get; }

		// Token: 0x06002A9C RID: 10908 RVA: 0x0007A074 File Offset: 0x00078274
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot != 0)
			{
				if (slot == 1)
				{
					if (this.Initializer != null)
					{
						return this.Initializer;
					}
				}
			}
			else if (this.Operand != null)
			{
				return this.Operand;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002A9D RID: 10909 RVA: 0x0007A0C4 File Offset: 0x000782C4
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (this.Operand != null)
				{
					return new ValueTuple<bool, int, int>(true, 0, 0);
				}
				break;
			case 0:
				break;
			case 1:
			case 2:
				goto IL_003C;
			default:
				throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(previousSlot, previousIndex));
			}
			if (this.Initializer != null)
			{
				return new ValueTuple<bool, int, int>(true, 1, 0);
			}
			IL_003C:
			return new ValueTuple<bool, int, int>(false, 2, 0);
		}

		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x06002A9E RID: 10910 RVA: 0x0007A127 File Offset: 0x00078327
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x06002A9F RID: 10911 RVA: 0x0007A12F File Offset: 0x0007832F
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x06002AA0 RID: 10912 RVA: 0x0007A132 File Offset: 0x00078332
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.With;
			}
		}

		// Token: 0x06002AA1 RID: 10913 RVA: 0x0007A136 File Offset: 0x00078336
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitWith(this);
		}

		// Token: 0x06002AA2 RID: 10914 RVA: 0x0007A13F File Offset: 0x0007833F
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitWith(this, argument);
		}
	}
}
