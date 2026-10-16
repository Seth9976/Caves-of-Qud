using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000425 RID: 1061
	internal sealed class FieldInitializerOperation : BaseSymbolInitializerOperation, IFieldInitializerOperation, ISymbolInitializerOperation, IOperation
	{
		// Token: 0x060028AF RID: 10415 RVA: 0x000774E4 File Offset: 0x000756E4
		[NullableContext(1)]
		internal FieldInitializerOperation([Nullable(new byte[] { 0, 1 })] ImmutableArray<IFieldSymbol> initializedFields, [Nullable(new byte[] { 0, 1 })] ImmutableArray<ILocalSymbol> locals, IOperation value, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(locals, value, semanticModel, syntax, isImplicit)
		{
			this.InitializedFields = initializedFields;
		}

		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x060028B0 RID: 10416 RVA: 0x000774FB File Offset: 0x000756FB
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<IFieldSymbol> InitializedFields
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x060028B1 RID: 10417 RVA: 0x00077504 File Offset: 0x00075704
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot == 0 && base.Value != null)
			{
				return base.Value;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x060028B2 RID: 10418 RVA: 0x00077538 File Offset: 0x00075738
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

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x060028B3 RID: 10419 RVA: 0x0007756F File Offset: 0x0007576F
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x060028B4 RID: 10420 RVA: 0x00077572 File Offset: 0x00075772
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x060028B5 RID: 10421 RVA: 0x00077575 File Offset: 0x00075775
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.FieldInitializer;
			}
		}

		// Token: 0x060028B6 RID: 10422 RVA: 0x00077579 File Offset: 0x00075779
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitFieldInitializer(this);
		}

		// Token: 0x060028B7 RID: 10423 RVA: 0x00077582 File Offset: 0x00075782
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitFieldInitializer(this, argument);
		}
	}
}
