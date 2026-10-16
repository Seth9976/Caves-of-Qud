using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000450 RID: 1104
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class NoPiaObjectCreationOperation : Operation, INoPiaObjectCreationOperation, IOperation
	{
		// Token: 0x06002A4A RID: 10826 RVA: 0x00079A4B File Offset: 0x00077C4B
		internal NoPiaObjectCreationOperation(IObjectOrCollectionInitializerOperation initializer, SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, ITypeSymbol type, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Initializer = Operation.SetParentOperation<IObjectOrCollectionInitializerOperation>(initializer, this);
			this.Type = type;
		}

		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x06002A4B RID: 10827 RVA: 0x00079A6C File Offset: 0x00077C6C
		public IObjectOrCollectionInitializerOperation Initializer { get; }

		// Token: 0x06002A4C RID: 10828 RVA: 0x00079A74 File Offset: 0x00077C74
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot == 0 && this.Initializer != null)
			{
				return this.Initializer;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002A4D RID: 10829 RVA: 0x00079AA8 File Offset: 0x00077CA8
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
			else if (this.Initializer != null)
			{
				return new ValueTuple<bool, int, int>(true, 0, 0);
			}
			return new ValueTuple<bool, int, int>(false, 1, 0);
		}

		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x06002A4E RID: 10830 RVA: 0x00079ADF File Offset: 0x00077CDF
		public override ITypeSymbol Type { get; }

		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x06002A4F RID: 10831 RVA: 0x00079AE7 File Offset: 0x00077CE7
		internal override ConstantValue OperationConstantValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x06002A50 RID: 10832 RVA: 0x00079AEA File Offset: 0x00077CEA
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.None;
			}
		}

		// Token: 0x06002A51 RID: 10833 RVA: 0x00079AED File Offset: 0x00077CED
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitNoPiaObjectCreation(this);
		}

		// Token: 0x06002A52 RID: 10834 RVA: 0x00079AF6 File Offset: 0x00077CF6
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitNoPiaObjectCreation(this, argument);
		}
	}
}
