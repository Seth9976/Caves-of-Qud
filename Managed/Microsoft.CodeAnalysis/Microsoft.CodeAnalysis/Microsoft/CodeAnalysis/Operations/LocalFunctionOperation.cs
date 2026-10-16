using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003F0 RID: 1008
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class LocalFunctionOperation : Operation, ILocalFunctionOperation, IOperation
	{
		// Token: 0x060026AC RID: 9900 RVA: 0x000747CC File Offset: 0x000729CC
		internal LocalFunctionOperation([Nullable(1)] IMethodSymbol symbol, IBlockOperation body, IBlockOperation ignoredBody, SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Symbol = symbol;
			this.Body = Operation.SetParentOperation<IBlockOperation>(body, this);
			this.IgnoredBody = Operation.SetParentOperation<IBlockOperation>(ignoredBody, this);
		}

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x060026AD RID: 9901 RVA: 0x000747FB File Offset: 0x000729FB
		[Nullable(1)]
		public IMethodSymbol Symbol
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x060026AE RID: 9902 RVA: 0x00074803 File Offset: 0x00072A03
		public IBlockOperation Body { get; }

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x060026AF RID: 9903 RVA: 0x0007480B File Offset: 0x00072A0B
		public IBlockOperation IgnoredBody { get; }

		// Token: 0x060026B0 RID: 9904 RVA: 0x00074814 File Offset: 0x00072A14
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot != 0)
			{
				if (slot == 1)
				{
					if (this.IgnoredBody != null)
					{
						return this.IgnoredBody;
					}
				}
			}
			else if (this.Body != null)
			{
				return this.Body;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x060026B1 RID: 9905 RVA: 0x00074864 File Offset: 0x00072A64
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (this.Body != null)
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
			if (this.IgnoredBody != null)
			{
				return new ValueTuple<bool, int, int>(true, 1, 0);
			}
			IL_003C:
			return new ValueTuple<bool, int, int>(false, 2, 0);
		}

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x060026B2 RID: 9906 RVA: 0x000748C7 File Offset: 0x00072AC7
		public override ITypeSymbol Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x060026B3 RID: 9907 RVA: 0x000748CA File Offset: 0x00072ACA
		internal override ConstantValue OperationConstantValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x060026B4 RID: 9908 RVA: 0x000748CD File Offset: 0x00072ACD
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.LocalFunction;
			}
		}

		// Token: 0x060026B5 RID: 9909 RVA: 0x000748D1 File Offset: 0x00072AD1
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitLocalFunction(this);
		}

		// Token: 0x060026B6 RID: 9910 RVA: 0x000748DA File Offset: 0x00072ADA
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitLocalFunction(this, argument);
		}
	}
}
