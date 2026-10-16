using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000413 RID: 1043
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class ObjectOrCollectionInitializerOperation : Operation, IObjectOrCollectionInitializerOperation, IOperation
	{
		// Token: 0x0600280C RID: 10252 RVA: 0x00076780 File Offset: 0x00074980
		internal ObjectOrCollectionInitializerOperation([Nullable(new byte[] { 0, 1 })] ImmutableArray<IOperation> initializers, SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, ITypeSymbol type, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Initializers = Operation.SetParentOperation<IOperation>(initializers, this);
			this.Type = type;
		}

		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x0600280D RID: 10253 RVA: 0x000767A1 File Offset: 0x000749A1
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<IOperation> Initializers
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x0600280E RID: 10254 RVA: 0x000767AC File Offset: 0x000749AC
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot == 0 && index < this.Initializers.Length)
			{
				return this.Initializers[index];
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x0600280F RID: 10255 RVA: 0x000767F4 File Offset: 0x000749F4
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (!this.Initializers.IsEmpty)
				{
					return new ValueTuple<bool, int, int>(true, 0, 0);
				}
				break;
			case 0:
				if (previousIndex + 1 < this.Initializers.Length)
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

		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x06002810 RID: 10256 RVA: 0x0007686A File Offset: 0x00074A6A
		public override ITypeSymbol Type { get; }

		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x06002811 RID: 10257 RVA: 0x00076872 File Offset: 0x00074A72
		internal override ConstantValue OperationConstantValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x06002812 RID: 10258 RVA: 0x00076875 File Offset: 0x00074A75
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.ObjectOrCollectionInitializer;
			}
		}

		// Token: 0x06002813 RID: 10259 RVA: 0x00076879 File Offset: 0x00074A79
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitObjectOrCollectionInitializer(this);
		}

		// Token: 0x06002814 RID: 10260 RVA: 0x00076882 File Offset: 0x00074A82
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitObjectOrCollectionInitializer(this, argument);
		}
	}
}
