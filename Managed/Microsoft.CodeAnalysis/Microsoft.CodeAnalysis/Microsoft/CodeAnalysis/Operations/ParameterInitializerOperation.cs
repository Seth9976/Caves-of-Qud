using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000428 RID: 1064
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ParameterInitializerOperation : BaseSymbolInitializerOperation, IParameterInitializerOperation, ISymbolInitializerOperation, IOperation
	{
		// Token: 0x060028C9 RID: 10441 RVA: 0x000776CC File Offset: 0x000758CC
		internal ParameterInitializerOperation(IParameterSymbol parameter, [Nullable(new byte[] { 0, 1 })] ImmutableArray<ILocalSymbol> locals, IOperation value, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(locals, value, semanticModel, syntax, isImplicit)
		{
			this.Parameter = parameter;
		}

		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x060028CA RID: 10442 RVA: 0x000776E3 File Offset: 0x000758E3
		public IParameterSymbol Parameter { get; }

		// Token: 0x060028CB RID: 10443 RVA: 0x000776EC File Offset: 0x000758EC
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot == 0 && base.Value != null)
			{
				return base.Value;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x060028CC RID: 10444 RVA: 0x00077720 File Offset: 0x00075920
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
			else if (base.Value != null)
			{
				return new ValueTuple<bool, int, int>(true, 0, 0);
			}
			return new ValueTuple<bool, int, int>(false, 1, 0);
		}

		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x060028CD RID: 10445 RVA: 0x00077757 File Offset: 0x00075957
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x060028CE RID: 10446 RVA: 0x0007775A File Offset: 0x0007595A
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x060028CF RID: 10447 RVA: 0x0007775D File Offset: 0x0007595D
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.ParameterInitializer;
			}
		}

		// Token: 0x060028D0 RID: 10448 RVA: 0x00077761 File Offset: 0x00075961
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitParameterInitializer(this);
		}

		// Token: 0x060028D1 RID: 10449 RVA: 0x0007776A File Offset: 0x0007596A
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitParameterInitializer(this, argument);
		}
	}
}
