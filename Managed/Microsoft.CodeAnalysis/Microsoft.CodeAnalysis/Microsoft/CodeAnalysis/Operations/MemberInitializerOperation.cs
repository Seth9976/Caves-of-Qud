using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000414 RID: 1044
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class MemberInitializerOperation : Operation, IMemberInitializerOperation, IOperation
	{
		// Token: 0x06002815 RID: 10261 RVA: 0x0007688C File Offset: 0x00074A8C
		internal MemberInitializerOperation(IOperation initializedMember, IObjectOrCollectionInitializerOperation initializer, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, [Nullable(2)] ITypeSymbol type, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.InitializedMember = Operation.SetParentOperation<IOperation>(initializedMember, this);
			this.Initializer = Operation.SetParentOperation<IObjectOrCollectionInitializerOperation>(initializer, this);
			this.Type = type;
		}

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x06002816 RID: 10262 RVA: 0x000768BB File Offset: 0x00074ABB
		public IOperation InitializedMember { get; }

		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x06002817 RID: 10263 RVA: 0x000768C3 File Offset: 0x00074AC3
		public IObjectOrCollectionInitializerOperation Initializer { get; }

		// Token: 0x06002818 RID: 10264 RVA: 0x000768CC File Offset: 0x00074ACC
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot != 0)
			{
				if (slot == 1)
				{
					if (this.Initializer != null)
					{
						return this.Initializer;
					}
				}
			}
			else if (this.InitializedMember != null)
			{
				return this.InitializedMember;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002819 RID: 10265 RVA: 0x0007691C File Offset: 0x00074B1C
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (this.InitializedMember != null)
				{
					return new ValueTuple<bool, int, int>(true, 0, 0);
				}
				break;
			case 0:
				break;
			case 1:
			case 2:
				goto IL_003C;
			default:
				throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(previousSlot, previousIndex));
			}
			if (this.Initializer != null)
			{
				return new ValueTuple<bool, int, int>(true, 1, 0);
			}
			IL_003C:
			return new ValueTuple<bool, int, int>(false, 2, 0);
		}

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x0600281A RID: 10266 RVA: 0x0007697F File Offset: 0x00074B7F
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x0600281B RID: 10267 RVA: 0x00076987 File Offset: 0x00074B87
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x0600281C RID: 10268 RVA: 0x0007698A File Offset: 0x00074B8A
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.MemberInitializer;
			}
		}

		// Token: 0x0600281D RID: 10269 RVA: 0x0007698E File Offset: 0x00074B8E
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitMemberInitializer(this);
		}

		// Token: 0x0600281E RID: 10270 RVA: 0x00076997 File Offset: 0x00074B97
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitMemberInitializer(this, argument);
		}
	}
}
