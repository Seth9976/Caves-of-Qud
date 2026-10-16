using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000415 RID: 1045
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class NameOfOperation : Operation, INameOfOperation, IOperation
	{
		// Token: 0x0600281F RID: 10271 RVA: 0x000769A1 File Offset: 0x00074BA1
		internal NameOfOperation([Nullable(1)] IOperation argument, SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, ITypeSymbol type, ConstantValue constantValue, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Argument = Operation.SetParentOperation<IOperation>(argument, this);
			this.OperationConstantValue = constantValue;
			this.Type = type;
		}

		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x06002820 RID: 10272 RVA: 0x000769CA File Offset: 0x00074BCA
		[Nullable(1)]
		public IOperation Argument
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x06002821 RID: 10273 RVA: 0x000769D4 File Offset: 0x00074BD4
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot == 0 && this.Argument != null)
			{
				return this.Argument;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002822 RID: 10274 RVA: 0x00076A08 File Offset: 0x00074C08
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			if (previousSlot != -1)
			{
				if (previousSlot > 1)
				{
					throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(previousSlot, previousIndex));
				}
			}
			else if (this.Argument != null)
			{
				return new ValueTuple<bool, int, int>(true, 0, 0);
			}
			return new ValueTuple<bool, int, int>(false, 1, 0);
		}

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x06002823 RID: 10275 RVA: 0x00076A3F File Offset: 0x00074C3F
		public override ITypeSymbol Type { get; }

		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x06002824 RID: 10276 RVA: 0x00076A47 File Offset: 0x00074C47
		internal override ConstantValue OperationConstantValue { get; }

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x06002825 RID: 10277 RVA: 0x00076A4F File Offset: 0x00074C4F
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.NameOf;
			}
		}

		// Token: 0x06002826 RID: 10278 RVA: 0x00076A53 File Offset: 0x00074C53
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitNameOf(this);
		}

		// Token: 0x06002827 RID: 10279 RVA: 0x00076A5C File Offset: 0x00074C5C
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitNameOf(this, argument);
		}
	}
}
