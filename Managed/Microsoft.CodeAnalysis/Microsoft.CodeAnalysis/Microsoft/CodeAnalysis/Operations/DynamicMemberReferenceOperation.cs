using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000417 RID: 1047
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class DynamicMemberReferenceOperation : Operation, IDynamicMemberReferenceOperation, IOperation
	{
		// Token: 0x06002832 RID: 10290 RVA: 0x00076B80 File Offset: 0x00074D80
		internal DynamicMemberReferenceOperation(IOperation instance, [Nullable(1)] string memberName, [Nullable(new byte[] { 0, 1 })] ImmutableArray<ITypeSymbol> typeArguments, ITypeSymbol containingType, SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, ITypeSymbol type, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Instance = Operation.SetParentOperation<IOperation>(instance, this);
			this.MemberName = memberName;
			this.TypeArguments = typeArguments;
			this.ContainingType = containingType;
			this.Type = type;
		}

		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x06002833 RID: 10291 RVA: 0x00076BB9 File Offset: 0x00074DB9
		public IOperation Instance { get; }

		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x06002834 RID: 10292 RVA: 0x00076BC1 File Offset: 0x00074DC1
		[Nullable(1)]
		public string MemberName
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x06002835 RID: 10293 RVA: 0x00076BC9 File Offset: 0x00074DC9
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<ITypeSymbol> TypeArguments
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x06002836 RID: 10294 RVA: 0x00076BD1 File Offset: 0x00074DD1
		public ITypeSymbol ContainingType { get; }

		// Token: 0x06002837 RID: 10295 RVA: 0x00076BDC File Offset: 0x00074DDC
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot == 0 && this.Instance != null)
			{
				return this.Instance;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002838 RID: 10296 RVA: 0x00076C10 File Offset: 0x00074E10
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
			else if (this.Instance != null)
			{
				return new ValueTuple<bool, int, int>(true, 0, 0);
			}
			return new ValueTuple<bool, int, int>(false, 1, 0);
		}

		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x06002839 RID: 10297 RVA: 0x00076C47 File Offset: 0x00074E47
		public override ITypeSymbol Type { get; }

		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x0600283A RID: 10298 RVA: 0x00076C4F File Offset: 0x00074E4F
		internal override ConstantValue OperationConstantValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x0600283B RID: 10299 RVA: 0x00076C52 File Offset: 0x00074E52
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.DynamicMemberReference;
			}
		}

		// Token: 0x0600283C RID: 10300 RVA: 0x00076C56 File Offset: 0x00074E56
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitDynamicMemberReference(this);
		}

		// Token: 0x0600283D RID: 10301 RVA: 0x00076C5F File Offset: 0x00074E5F
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitDynamicMemberReference(this, argument);
		}
	}
}
