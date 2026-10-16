using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000426 RID: 1062
	internal sealed class VariableInitializerOperation : BaseSymbolInitializerOperation, IVariableInitializerOperation, ISymbolInitializerOperation, IOperation
	{
		// Token: 0x060028B8 RID: 10424 RVA: 0x0007758C File Offset: 0x0007578C
		[NullableContext(1)]
		internal VariableInitializerOperation([Nullable(new byte[] { 0, 1 })] ImmutableArray<ILocalSymbol> locals, IOperation value, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(locals, value, semanticModel, syntax, isImplicit)
		{
		}

		// Token: 0x060028B9 RID: 10425 RVA: 0x0007759C File Offset: 0x0007579C
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot == 0 && base.Value != null)
			{
				return base.Value;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x060028BA RID: 10426 RVA: 0x000775D0 File Offset: 0x000757D0
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

		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x060028BB RID: 10427 RVA: 0x00077607 File Offset: 0x00075807
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x060028BC RID: 10428 RVA: 0x0007760A File Offset: 0x0007580A
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x060028BD RID: 10429 RVA: 0x0007760D File Offset: 0x0007580D
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.VariableInitializer;
			}
		}

		// Token: 0x060028BE RID: 10430 RVA: 0x00077611 File Offset: 0x00075811
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitVariableInitializer(this);
		}

		// Token: 0x060028BF RID: 10431 RVA: 0x0007761A File Offset: 0x0007581A
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitVariableInitializer(this, argument);
		}
	}
}
