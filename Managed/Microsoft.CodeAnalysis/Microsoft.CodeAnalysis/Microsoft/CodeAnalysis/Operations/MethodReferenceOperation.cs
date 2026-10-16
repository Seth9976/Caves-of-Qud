using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003FC RID: 1020
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class MethodReferenceOperation : BaseMemberReferenceOperation, IMethodReferenceOperation, IMemberReferenceOperation, IOperation
	{
		// Token: 0x0600271E RID: 10014 RVA: 0x000750F6 File Offset: 0x000732F6
		[NullableContext(2)]
		internal MethodReferenceOperation([Nullable(1)] IMethodSymbol method, bool isVirtual, IOperation instance, SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, ITypeSymbol type, bool isImplicit)
			: base(instance, semanticModel, syntax, isImplicit)
		{
			this.Method = method;
			this.IsVirtual = isVirtual;
			this.Type = type;
		}

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x0600271F RID: 10015 RVA: 0x0007511B File Offset: 0x0007331B
		public IMethodSymbol Method { get; }

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x06002720 RID: 10016 RVA: 0x00075123 File Offset: 0x00073323
		public bool IsVirtual { get; }

		// Token: 0x06002721 RID: 10017 RVA: 0x0007512C File Offset: 0x0007332C
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot == 0 && base.Instance != null)
			{
				return base.Instance;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002722 RID: 10018 RVA: 0x00075160 File Offset: 0x00073360
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
			else if (base.Instance != null)
			{
				return new ValueTuple<bool, int, int>(true, 0, 0);
			}
			return new ValueTuple<bool, int, int>(false, 1, 0);
		}

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x06002723 RID: 10019 RVA: 0x00075197 File Offset: 0x00073397
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x06002724 RID: 10020 RVA: 0x0007519F File Offset: 0x0007339F
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x06002725 RID: 10021 RVA: 0x000751A2 File Offset: 0x000733A2
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.MethodReference;
			}
		}

		// Token: 0x06002726 RID: 10022 RVA: 0x000751A6 File Offset: 0x000733A6
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitMethodReference(this);
		}

		// Token: 0x06002727 RID: 10023 RVA: 0x000751AF File Offset: 0x000733AF
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitMethodReference(this, argument);
		}

		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x06002728 RID: 10024 RVA: 0x000751B9 File Offset: 0x000733B9
		public override ISymbol Member
		{
			get
			{
				return this.Method;
			}
		}
	}
}
