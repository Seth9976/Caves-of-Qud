using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000416 RID: 1046
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class TupleOperation : Operation, ITupleOperation, IOperation
	{
		// Token: 0x06002828 RID: 10280 RVA: 0x00076A66 File Offset: 0x00074C66
		internal TupleOperation([Nullable(new byte[] { 0, 1 })] ImmutableArray<IOperation> elements, ITypeSymbol naturalType, SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, ITypeSymbol type, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Elements = Operation.SetParentOperation<IOperation>(elements, this);
			this.NaturalType = naturalType;
			this.Type = type;
		}

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x06002829 RID: 10281 RVA: 0x00076A8F File Offset: 0x00074C8F
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<IOperation> Elements
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x0600282A RID: 10282 RVA: 0x00076A97 File Offset: 0x00074C97
		public ITypeSymbol NaturalType { get; }

		// Token: 0x0600282B RID: 10283 RVA: 0x00076AA0 File Offset: 0x00074CA0
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot == 0 && index < this.Elements.Length)
			{
				return this.Elements[index];
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x0600282C RID: 10284 RVA: 0x00076AE8 File Offset: 0x00074CE8
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (!this.Elements.IsEmpty)
				{
					return new ValueTuple<bool, int, int>(true, 0, 0);
				}
				break;
			case 0:
				if (previousIndex + 1 < this.Elements.Length)
				{
					return new ValueTuple<bool, int, int>(true, 0, previousIndex + 1);
				}
				break;
			case 1:
				break;
			default:
				throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(previousSlot, previousIndex));
			}
			return new ValueTuple<bool, int, int>(false, 1, 0);
		}

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x0600282D RID: 10285 RVA: 0x00076B5E File Offset: 0x00074D5E
		public override ITypeSymbol Type { get; }

		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x0600282E RID: 10286 RVA: 0x00076B66 File Offset: 0x00074D66
		internal override ConstantValue OperationConstantValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x0600282F RID: 10287 RVA: 0x00076B69 File Offset: 0x00074D69
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.Tuple;
			}
		}

		// Token: 0x06002830 RID: 10288 RVA: 0x00076B6D File Offset: 0x00074D6D
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitTuple(this);
		}

		// Token: 0x06002831 RID: 10289 RVA: 0x00076B76 File Offset: 0x00074D76
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitTuple(this, argument);
		}
	}
}
