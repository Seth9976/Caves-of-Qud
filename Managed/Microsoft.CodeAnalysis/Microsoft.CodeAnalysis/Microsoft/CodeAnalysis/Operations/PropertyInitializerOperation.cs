using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000427 RID: 1063
	internal sealed class PropertyInitializerOperation : BaseSymbolInitializerOperation, IPropertyInitializerOperation, ISymbolInitializerOperation, IOperation
	{
		// Token: 0x060028C0 RID: 10432 RVA: 0x00077624 File Offset: 0x00075824
		[NullableContext(1)]
		internal PropertyInitializerOperation([Nullable(new byte[] { 0, 1 })] ImmutableArray<IPropertySymbol> initializedProperties, [Nullable(new byte[] { 0, 1 })] ImmutableArray<ILocalSymbol> locals, IOperation value, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(locals, value, semanticModel, syntax, isImplicit)
		{
			this.InitializedProperties = initializedProperties;
		}

		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x060028C1 RID: 10433 RVA: 0x0007763B File Offset: 0x0007583B
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<IPropertySymbol> InitializedProperties
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x060028C2 RID: 10434 RVA: 0x00077644 File Offset: 0x00075844
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot == 0 && base.Value != null)
			{
				return base.Value;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x060028C3 RID: 10435 RVA: 0x00077678 File Offset: 0x00075878
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

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x060028C4 RID: 10436 RVA: 0x000776AF File Offset: 0x000758AF
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x060028C5 RID: 10437 RVA: 0x000776B2 File Offset: 0x000758B2
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x060028C6 RID: 10438 RVA: 0x000776B5 File Offset: 0x000758B5
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.PropertyInitializer;
			}
		}

		// Token: 0x060028C7 RID: 10439 RVA: 0x000776B9 File Offset: 0x000758B9
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitPropertyInitializer(this);
		}

		// Token: 0x060028C8 RID: 10440 RVA: 0x000776C2 File Offset: 0x000758C2
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitPropertyInitializer(this, argument);
		}
	}
}
