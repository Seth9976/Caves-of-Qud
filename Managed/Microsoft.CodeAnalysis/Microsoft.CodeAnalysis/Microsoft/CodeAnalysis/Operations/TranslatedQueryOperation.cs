using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000418 RID: 1048
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class TranslatedQueryOperation : Operation, ITranslatedQueryOperation, IOperation
	{
		// Token: 0x0600283E RID: 10302 RVA: 0x00076C69 File Offset: 0x00074E69
		internal TranslatedQueryOperation(IOperation operation, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, [Nullable(2)] ITypeSymbol type, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Operation = Microsoft.CodeAnalysis.Operation.SetParentOperation<IOperation>(operation, this);
			this.Type = type;
		}

		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x0600283F RID: 10303 RVA: 0x00076C8A File Offset: 0x00074E8A
		public IOperation Operation { get; }

		// Token: 0x06002840 RID: 10304 RVA: 0x00076C94 File Offset: 0x00074E94
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot == 0 && this.Operation != null)
			{
				return this.Operation;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002841 RID: 10305 RVA: 0x00076CC8 File Offset: 0x00074EC8
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
			else if (this.Operation != null)
			{
				return new ValueTuple<bool, int, int>(true, 0, 0);
			}
			return new ValueTuple<bool, int, int>(false, 1, 0);
		}

		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x06002842 RID: 10306 RVA: 0x00076CFF File Offset: 0x00074EFF
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x06002843 RID: 10307 RVA: 0x00076D07 File Offset: 0x00074F07
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x06002844 RID: 10308 RVA: 0x00076D0A File Offset: 0x00074F0A
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.TranslatedQuery;
			}
		}

		// Token: 0x06002845 RID: 10309 RVA: 0x00076D0E File Offset: 0x00074F0E
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitTranslatedQuery(this);
		}

		// Token: 0x06002846 RID: 10310 RVA: 0x00076D17 File Offset: 0x00074F17
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitTranslatedQuery(this, argument);
		}
	}
}
