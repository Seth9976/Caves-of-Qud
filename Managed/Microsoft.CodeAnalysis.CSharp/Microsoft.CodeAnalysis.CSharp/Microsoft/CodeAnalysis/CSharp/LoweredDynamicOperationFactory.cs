using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000206 RID: 518
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class LoweredDynamicOperationFactory
	{
		// Token: 0x0600255C RID: 9564 RVA: 0x000CB3E4 File Offset: 0x000C95E4
		internal LoweredDynamicOperationFactory(SyntheticBoundNodeFactory factory, int methodOrdinal, int localFunctionOrdinal = -1)
		{
			this._factory = factory;
			this._methodOrdinal = methodOrdinal;
			this._localFunctionOrdinal = localFunctionOrdinal;
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x0600255D RID: 9565 RVA: 0x000CB401 File Offset: 0x000C9601
		public int MethodOrdinal
		{
			get
			{
				return this._methodOrdinal;
			}
		}

		// Token: 0x0600255E RID: 9566 RVA: 0x000CB40C File Offset: 0x000C960C
		internal LoweredDynamicOperation MakeDynamicConversion(BoundExpression loweredOperand, bool isExplicit, bool isArrayIndex, bool isChecked, TypeSymbol resultType)
		{
			this._factory.Syntax = loweredOperand.Syntax;
			LoweredDynamicOperationFactory.CSharpBinderFlags csharpBinderFlags = LoweredDynamicOperationFactory.CSharpBinderFlags.None;
			if (isChecked)
			{
				csharpBinderFlags |= LoweredDynamicOperationFactory.CSharpBinderFlags.CheckedContext;
			}
			if (isExplicit)
			{
				csharpBinderFlags |= LoweredDynamicOperationFactory.CSharpBinderFlags.ConvertExplicit;
			}
			if (isArrayIndex)
			{
				csharpBinderFlags |= LoweredDynamicOperationFactory.CSharpBinderFlags.ConvertArrayIndex;
			}
			ImmutableArray<BoundExpression> immutableArray = ImmutableArray.Create<BoundExpression>(loweredOperand);
			BoundExpression boundExpression = this.MakeBinderConstruction(WellKnownMember.Microsoft_CSharp_RuntimeBinder_Binder__Convert, new BoundExpression[]
			{
				this._factory.Literal((int)csharpBinderFlags),
				this._factory.Typeof(resultType),
				this._factory.TypeofDynamicOperationContextType()
			});
			return this.MakeDynamicOperation(boundExpression, null, RefKind.None, immutableArray, default(ImmutableArray<RefKind>), null, resultType);
		}

		// Token: 0x0600255F RID: 9567 RVA: 0x000CB4A0 File Offset: 0x000C96A0
		internal LoweredDynamicOperation MakeDynamicUnaryOperator(UnaryOperatorKind operatorKind, BoundExpression loweredOperand, TypeSymbol resultType)
		{
			this._factory.Syntax = loweredOperand.Syntax;
			LoweredDynamicOperationFactory.CSharpBinderFlags csharpBinderFlags = LoweredDynamicOperationFactory.CSharpBinderFlags.None;
			if (operatorKind.IsChecked())
			{
				csharpBinderFlags |= LoweredDynamicOperationFactory.CSharpBinderFlags.CheckedContext;
			}
			ImmutableArray<BoundExpression> immutableArray = ImmutableArray.Create<BoundExpression>(loweredOperand);
			MethodSymbol argumentInfoFactory = this.GetArgumentInfoFactory();
			BoundExpression boundExpression = ((argumentInfoFactory != null) ? this.MakeBinderConstruction(WellKnownMember.Microsoft_CSharp_RuntimeBinder_Binder__UnaryOperation, new BoundExpression[]
			{
				this._factory.Literal((int)csharpBinderFlags),
				this._factory.Literal(operatorKind.ToExpressionType()),
				this._factory.TypeofDynamicOperationContextType(),
				this.MakeCallSiteArgumentInfos(argumentInfoFactory, immutableArray, default(ImmutableArray<string>), default(ImmutableArray<RefKind>), null, RefKind.None, false, null)
			}) : null);
			return this.MakeDynamicOperation(boundExpression, null, RefKind.None, immutableArray, default(ImmutableArray<RefKind>), null, resultType);
		}

		// Token: 0x06002560 RID: 9568 RVA: 0x000CB55C File Offset: 0x000C975C
		internal LoweredDynamicOperation MakeDynamicBinaryOperator(BinaryOperatorKind operatorKind, BoundExpression loweredLeft, BoundExpression loweredRight, bool isCompoundAssignment, TypeSymbol resultType)
		{
			this._factory.Syntax = loweredLeft.Syntax;
			LoweredDynamicOperationFactory.CSharpBinderFlags csharpBinderFlags = LoweredDynamicOperationFactory.CSharpBinderFlags.None;
			if (operatorKind.IsChecked())
			{
				csharpBinderFlags |= LoweredDynamicOperationFactory.CSharpBinderFlags.CheckedContext;
			}
			if (operatorKind.IsLogical())
			{
				csharpBinderFlags |= LoweredDynamicOperationFactory.CSharpBinderFlags.BinaryOperationLogical;
			}
			ImmutableArray<BoundExpression> immutableArray = ImmutableArray.Create<BoundExpression>(loweredLeft, loweredRight);
			MethodSymbol argumentInfoFactory = this.GetArgumentInfoFactory();
			BoundExpression boundExpression = ((argumentInfoFactory != null) ? this.MakeBinderConstruction(WellKnownMember.Microsoft_CSharp_RuntimeBinder_Binder__BinaryOperation, new BoundExpression[]
			{
				this._factory.Literal((int)csharpBinderFlags),
				this._factory.Literal(operatorKind.ToExpressionType(isCompoundAssignment)),
				this._factory.TypeofDynamicOperationContextType(),
				this.MakeCallSiteArgumentInfos(argumentInfoFactory, immutableArray, default(ImmutableArray<string>), default(ImmutableArray<RefKind>), null, RefKind.None, false, null)
			}) : null);
			return this.MakeDynamicOperation(boundExpression, null, RefKind.None, immutableArray, default(ImmutableArray<RefKind>), null, resultType);
		}

		// Token: 0x06002561 RID: 9569 RVA: 0x000CB628 File Offset: 0x000C9828
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal LoweredDynamicOperation MakeDynamicMemberInvocation([global::System.Runtime.CompilerServices.Nullable(1)] string name, [global::System.Runtime.CompilerServices.Nullable(1)] BoundExpression loweredReceiver, ImmutableArray<TypeWithAnnotations> typeArgumentsWithAnnotations, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> loweredArguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<string> argumentNames, ImmutableArray<RefKind> refKinds, bool hasImplicitReceiver, bool resultDiscarded)
		{
			this._factory.Syntax = loweredReceiver.Syntax;
			LoweredDynamicOperationFactory.CSharpBinderFlags csharpBinderFlags = LoweredDynamicOperationFactory.CSharpBinderFlags.None;
			if (hasImplicitReceiver && this._factory.TopLevelMethod.RequiresInstanceReceiver)
			{
				csharpBinderFlags |= LoweredDynamicOperationFactory.CSharpBinderFlags.InvokeSimpleName;
			}
			TypeSymbol typeSymbol;
			if (resultDiscarded)
			{
				csharpBinderFlags |= LoweredDynamicOperationFactory.CSharpBinderFlags.ResultDiscarded;
				typeSymbol = this._factory.SpecialType(SpecialType.System_Void);
			}
			else
			{
				typeSymbol = AssemblySymbol.DynamicType;
			}
			RefKind refKind;
			bool flag;
			if (loweredReceiver.Kind == BoundKind.TypeExpression)
			{
				loweredReceiver = this._factory.Typeof(((BoundTypeExpression)loweredReceiver).Type);
				refKind = RefKind.None;
				flag = true;
			}
			else
			{
				refKind = LoweredDynamicOperationFactory.GetReceiverRefKind(loweredReceiver);
				flag = false;
			}
			MethodSymbol argumentInfoFactory = this.GetArgumentInfoFactory();
			BoundExpression boundExpression = ((argumentInfoFactory != null) ? this.MakeBinderConstruction(WellKnownMember.Microsoft_CSharp_RuntimeBinder_Binder__InvokeMember, new BoundExpression[]
			{
				this._factory.Literal((int)csharpBinderFlags),
				this._factory.Literal(name),
				typeArgumentsWithAnnotations.IsDefaultOrEmpty ? this._factory.Null(this._factory.WellKnownArrayType(WellKnownType.System_Type)) : this._factory.ArrayOrEmpty(this._factory.WellKnownType(WellKnownType.System_Type), this._factory.TypeOfs(typeArgumentsWithAnnotations)),
				this._factory.TypeofDynamicOperationContextType(),
				this.MakeCallSiteArgumentInfos(argumentInfoFactory, loweredArguments, argumentNames, refKinds, loweredReceiver, refKind, flag, null)
			}) : null);
			return this.MakeDynamicOperation(boundExpression, loweredReceiver, refKind, loweredArguments, refKinds, null, typeSymbol);
		}

		// Token: 0x06002562 RID: 9570 RVA: 0x000CB774 File Offset: 0x000C9974
		internal LoweredDynamicOperation MakeDynamicEventAccessorInvocation(string accessorName, BoundExpression loweredReceiver, BoundExpression loweredHandler)
		{
			this._factory.Syntax = loweredReceiver.Syntax;
			LoweredDynamicOperationFactory.CSharpBinderFlags csharpBinderFlags = LoweredDynamicOperationFactory.CSharpBinderFlags.InvokeSpecialName | LoweredDynamicOperationFactory.CSharpBinderFlags.ResultDiscarded;
			ImmutableArray<BoundExpression> empty = ImmutableArray<BoundExpression>.Empty;
			TypeSymbol dynamicType = AssemblySymbol.DynamicType;
			MethodSymbol argumentInfoFactory = this.GetArgumentInfoFactory();
			BoundExpression boundExpression = ((argumentInfoFactory != null) ? this.MakeBinderConstruction(WellKnownMember.Microsoft_CSharp_RuntimeBinder_Binder__InvokeMember, new BoundExpression[]
			{
				this._factory.Literal((int)csharpBinderFlags),
				this._factory.Literal(accessorName),
				this._factory.Null(this._factory.WellKnownArrayType(WellKnownType.System_Type)),
				this._factory.TypeofDynamicOperationContextType(),
				this.MakeCallSiteArgumentInfos(argumentInfoFactory, empty, default(ImmutableArray<string>), default(ImmutableArray<RefKind>), loweredReceiver, RefKind.None, false, loweredHandler)
			}) : null);
			return this.MakeDynamicOperation(boundExpression, loweredReceiver, RefKind.None, empty, default(ImmutableArray<RefKind>), loweredHandler, dynamicType);
		}

		// Token: 0x06002563 RID: 9571 RVA: 0x000CB850 File Offset: 0x000C9A50
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal LoweredDynamicOperation MakeDynamicInvocation([global::System.Runtime.CompilerServices.Nullable(1)] BoundExpression loweredReceiver, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> loweredArguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<string> argumentNames, ImmutableArray<RefKind> refKinds, bool resultDiscarded)
		{
			this._factory.Syntax = loweredReceiver.Syntax;
			LoweredDynamicOperationFactory.CSharpBinderFlags csharpBinderFlags = LoweredDynamicOperationFactory.CSharpBinderFlags.None;
			TypeSymbol typeSymbol;
			if (resultDiscarded)
			{
				csharpBinderFlags |= LoweredDynamicOperationFactory.CSharpBinderFlags.ResultDiscarded;
				typeSymbol = this._factory.SpecialType(SpecialType.System_Void);
			}
			else
			{
				typeSymbol = AssemblySymbol.DynamicType;
			}
			MethodSymbol argumentInfoFactory = this.GetArgumentInfoFactory();
			BoundExpression boundExpression = ((argumentInfoFactory != null) ? this.MakeBinderConstruction(WellKnownMember.Microsoft_CSharp_RuntimeBinder_Binder__Invoke, new BoundExpression[]
			{
				this._factory.Literal((int)csharpBinderFlags),
				this._factory.TypeofDynamicOperationContextType(),
				this.MakeCallSiteArgumentInfos(argumentInfoFactory, loweredArguments, argumentNames, refKinds, loweredReceiver, RefKind.None, false, null)
			}) : null);
			return this.MakeDynamicOperation(boundExpression, loweredReceiver, RefKind.None, loweredArguments, refKinds, null, typeSymbol);
		}

		// Token: 0x06002564 RID: 9572 RVA: 0x000CB8F0 File Offset: 0x000C9AF0
		internal LoweredDynamicOperation MakeDynamicConstructorInvocation(SyntaxNode syntax, TypeSymbol type, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> loweredArguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<string> argumentNames, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<RefKind> refKinds)
		{
			this._factory.Syntax = syntax;
			BoundExpression boundExpression = this._factory.Typeof(type);
			MethodSymbol argumentInfoFactory = this.GetArgumentInfoFactory();
			BoundExpression boundExpression2 = ((argumentInfoFactory != null) ? this.MakeBinderConstruction(WellKnownMember.Microsoft_CSharp_RuntimeBinder_Binder__InvokeConstructor, new BoundExpression[]
			{
				this._factory.Literal(0),
				this._factory.TypeofDynamicOperationContextType(),
				this.MakeCallSiteArgumentInfos(argumentInfoFactory, loweredArguments, argumentNames, refKinds, boundExpression, RefKind.None, true, null)
			}) : null);
			return this.MakeDynamicOperation(boundExpression2, boundExpression, RefKind.None, loweredArguments, refKinds, null, type);
		}

		// Token: 0x06002565 RID: 9573 RVA: 0x000CB974 File Offset: 0x000C9B74
		internal LoweredDynamicOperation MakeDynamicGetMember(BoundExpression loweredReceiver, string name, bool resultIndexed)
		{
			this._factory.Syntax = loweredReceiver.Syntax;
			LoweredDynamicOperationFactory.CSharpBinderFlags csharpBinderFlags = LoweredDynamicOperationFactory.CSharpBinderFlags.None;
			if (resultIndexed)
			{
				csharpBinderFlags |= LoweredDynamicOperationFactory.CSharpBinderFlags.ResultIndexed;
			}
			ImmutableArray<BoundExpression> empty = ImmutableArray<BoundExpression>.Empty;
			DynamicTypeSymbol instance = DynamicTypeSymbol.Instance;
			MethodSymbol argumentInfoFactory = this.GetArgumentInfoFactory();
			BoundExpression boundExpression = ((argumentInfoFactory != null) ? this.MakeBinderConstruction(WellKnownMember.Microsoft_CSharp_RuntimeBinder_Binder__GetMember, new BoundExpression[]
			{
				this._factory.Literal((int)csharpBinderFlags),
				this._factory.Literal(name),
				this._factory.TypeofDynamicOperationContextType(),
				this.MakeCallSiteArgumentInfos(argumentInfoFactory, empty, default(ImmutableArray<string>), default(ImmutableArray<RefKind>), loweredReceiver, RefKind.None, false, null)
			}) : null);
			return this.MakeDynamicOperation(boundExpression, loweredReceiver, RefKind.None, empty, default(ImmutableArray<RefKind>), null, instance);
		}

		// Token: 0x06002566 RID: 9574 RVA: 0x000CBA34 File Offset: 0x000C9C34
		internal LoweredDynamicOperation MakeDynamicSetMember(BoundExpression loweredReceiver, string name, BoundExpression loweredRight, bool isCompoundAssignment = false, bool isChecked = false)
		{
			this._factory.Syntax = loweredReceiver.Syntax;
			LoweredDynamicOperationFactory.CSharpBinderFlags csharpBinderFlags = LoweredDynamicOperationFactory.CSharpBinderFlags.None;
			if (isCompoundAssignment)
			{
				csharpBinderFlags |= LoweredDynamicOperationFactory.CSharpBinderFlags.ValueFromCompoundAssignment;
				if (isChecked)
				{
					csharpBinderFlags |= LoweredDynamicOperationFactory.CSharpBinderFlags.CheckedContext;
				}
			}
			ImmutableArray<BoundExpression> empty = ImmutableArray<BoundExpression>.Empty;
			MethodSymbol argumentInfoFactory = this.GetArgumentInfoFactory();
			BoundExpression boundExpression = ((argumentInfoFactory != null) ? this.MakeBinderConstruction(WellKnownMember.Microsoft_CSharp_RuntimeBinder_Binder__SetMember, new BoundExpression[]
			{
				this._factory.Literal((int)csharpBinderFlags),
				this._factory.Literal(name),
				this._factory.TypeofDynamicOperationContextType(),
				this.MakeCallSiteArgumentInfos(argumentInfoFactory, empty, default(ImmutableArray<string>), default(ImmutableArray<RefKind>), loweredReceiver, RefKind.None, false, loweredRight)
			}) : null);
			return this.MakeDynamicOperation(boundExpression, loweredReceiver, RefKind.None, empty, default(ImmutableArray<RefKind>), loweredRight, AssemblySymbol.DynamicType);
		}

		// Token: 0x06002567 RID: 9575 RVA: 0x000CBB00 File Offset: 0x000C9D00
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal LoweredDynamicOperation MakeDynamicGetIndex([global::System.Runtime.CompilerServices.Nullable(1)] BoundExpression loweredReceiver, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> loweredArguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<string> argumentNames, ImmutableArray<RefKind> refKinds)
		{
			this._factory.Syntax = loweredReceiver.Syntax;
			DynamicTypeSymbol instance = DynamicTypeSymbol.Instance;
			MethodSymbol argumentInfoFactory = this.GetArgumentInfoFactory();
			BoundExpression boundExpression = ((argumentInfoFactory != null) ? this.MakeBinderConstruction(WellKnownMember.Microsoft_CSharp_RuntimeBinder_Binder__GetIndex, new BoundExpression[]
			{
				this._factory.Literal(0),
				this._factory.TypeofDynamicOperationContextType(),
				this.MakeCallSiteArgumentInfos(argumentInfoFactory, loweredArguments, argumentNames, refKinds, loweredReceiver, RefKind.None, false, null)
			}) : null);
			return this.MakeDynamicOperation(boundExpression, loweredReceiver, RefKind.None, loweredArguments, refKinds, null, instance);
		}

		// Token: 0x06002568 RID: 9576 RVA: 0x000CBB80 File Offset: 0x000C9D80
		internal LoweredDynamicOperation MakeDynamicSetIndex(BoundExpression loweredReceiver, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> loweredArguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<string> argumentNames, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<RefKind> refKinds, BoundExpression loweredRight, bool isCompoundAssignment = false, bool isChecked = false)
		{
			LoweredDynamicOperationFactory.CSharpBinderFlags csharpBinderFlags = LoweredDynamicOperationFactory.CSharpBinderFlags.None;
			if (isCompoundAssignment)
			{
				csharpBinderFlags |= LoweredDynamicOperationFactory.CSharpBinderFlags.ValueFromCompoundAssignment;
				if (isChecked)
				{
					csharpBinderFlags |= LoweredDynamicOperationFactory.CSharpBinderFlags.CheckedContext;
				}
			}
			RefKind receiverRefKind = LoweredDynamicOperationFactory.GetReceiverRefKind(loweredReceiver);
			DynamicTypeSymbol instance = DynamicTypeSymbol.Instance;
			MethodSymbol argumentInfoFactory = this.GetArgumentInfoFactory();
			BoundExpression boundExpression = ((argumentInfoFactory != null) ? this.MakeBinderConstruction(WellKnownMember.Microsoft_CSharp_RuntimeBinder_Binder__SetIndex, new BoundExpression[]
			{
				this._factory.Literal((int)csharpBinderFlags),
				this._factory.TypeofDynamicOperationContextType(),
				this.MakeCallSiteArgumentInfos(argumentInfoFactory, loweredArguments, argumentNames, refKinds, loweredReceiver, receiverRefKind, false, loweredRight)
			}) : null);
			return this.MakeDynamicOperation(boundExpression, loweredReceiver, receiverRefKind, loweredArguments, refKinds, loweredRight, instance);
		}

		// Token: 0x06002569 RID: 9577 RVA: 0x000CBC10 File Offset: 0x000C9E10
		internal LoweredDynamicOperation MakeDynamicIsEventTest(string name, BoundExpression loweredReceiver)
		{
			this._factory.Syntax = loweredReceiver.Syntax;
			NamedTypeSymbol namedTypeSymbol = this._factory.SpecialType(SpecialType.System_Boolean);
			BoundExpression boundExpression = this.MakeBinderConstruction(WellKnownMember.Microsoft_CSharp_RuntimeBinder_Binder__IsEvent, new BoundExpression[]
			{
				this._factory.Literal(0),
				this._factory.Literal(name),
				this._factory.TypeofDynamicOperationContextType()
			});
			return this.MakeDynamicOperation(boundExpression, loweredReceiver, RefKind.None, ImmutableArray<BoundExpression>.Empty, default(ImmutableArray<RefKind>), null, namedTypeSymbol);
		}

		// Token: 0x0600256A RID: 9578 RVA: 0x000CBC92 File Offset: 0x000C9E92
		private MethodSymbol GetArgumentInfoFactory()
		{
			return this._factory.WellKnownMethod(WellKnownMember.Microsoft_CSharp_RuntimeBinder_CSharpArgumentInfo__Create);
		}

		// Token: 0x0600256B RID: 9579 RVA: 0x000CBCA4 File Offset: 0x000C9EA4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		private BoundExpression MakeBinderConstruction(WellKnownMember factoryMethod, BoundExpression[] args)
		{
			Symbol symbol = this._factory.WellKnownMember(factoryMethod);
			if (symbol == null)
			{
				return null;
			}
			return this._factory.Call(null, (MethodSymbol)symbol, args.AsImmutableOrNull<BoundExpression>());
		}

		// Token: 0x0600256C RID: 9580 RVA: 0x000CBCDC File Offset: 0x000C9EDC
		internal static RefKind GetReceiverRefKind(BoundExpression loweredReceiver)
		{
			if (!loweredReceiver.Type.IsValueType)
			{
				return RefKind.None;
			}
			BoundKind kind = loweredReceiver.Kind;
			if (kind <= BoundKind.RefValueOperator)
			{
				if (kind == BoundKind.TypeExpression)
				{
					goto IL_0057;
				}
				if (kind - BoundKind.PointerIndirectionOperator > 1 && kind != BoundKind.RefValueOperator)
				{
					return RefKind.None;
				}
			}
			else if (kind != BoundKind.ArrayAccess)
			{
				switch (kind)
				{
				case BoundKind.ThisReference:
				case BoundKind.Local:
					break;
				case BoundKind.PreviousSubmissionReference:
				case BoundKind.HostObjectMemberReference:
					return RefKind.None;
				case BoundKind.BaseReference:
					goto IL_0057;
				default:
					if (kind != BoundKind.Parameter)
					{
						return RefKind.None;
					}
					break;
				}
			}
			return RefKind.Ref;
			IL_0057:
			throw ExceptionUtilities.UnexpectedValue(loweredReceiver.Kind);
		}

		// Token: 0x0600256D RID: 9581 RVA: 0x000CBD54 File Offset: 0x000C9F54
		internal BoundExpression MakeCallSiteArgumentInfos(MethodSymbol argumentInfoFactory, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> loweredArguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<string> argumentNames = default(ImmutableArray<string>), [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<RefKind> refKinds = default(ImmutableArray<RefKind>), [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression loweredReceiver = null, RefKind receiverRefKind = RefKind.None, bool receiverIsStaticType = false, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression loweredRight = null)
		{
			BoundExpression[] array = new BoundExpression[((loweredReceiver != null) ? 1 : 0) + loweredArguments.Length + ((loweredRight != null) ? 1 : 0)];
			int num = 0;
			if (loweredReceiver != null)
			{
				array[num++] = this.GetArgumentInfo(argumentInfoFactory, loweredReceiver, null, receiverRefKind, receiverIsStaticType);
			}
			for (int i = 0; i < loweredArguments.Length; i++)
			{
				array[num++] = this.GetArgumentInfo(argumentInfoFactory, loweredArguments[i], argumentNames.IsDefaultOrEmpty ? null : argumentNames[i], refKinds.IsDefault ? RefKind.None : refKinds[i], false);
			}
			if (loweredRight != null)
			{
				array[num++] = this.GetArgumentInfo(argumentInfoFactory, loweredRight, null, RefKind.None, false);
			}
			return this._factory.ArrayOrEmpty(argumentInfoFactory.ContainingType, array);
		}

		// Token: 0x0600256E RID: 9582 RVA: 0x000CBE18 File Offset: 0x000CA018
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal LoweredDynamicOperation MakeDynamicOperation(BoundExpression binderConstruction, BoundExpression loweredReceiver, RefKind receiverRefKind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> loweredArguments, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<RefKind> refKinds, BoundExpression loweredRight, [global::System.Runtime.CompilerServices.Nullable(1)] TypeSymbol resultType)
		{
			NamedTypeSymbol delegateType = this.GetDelegateType(loweredReceiver, receiverRefKind, loweredArguments, refKinds, loweredRight, resultType);
			NamedTypeSymbol namedTypeSymbol = this._factory.WellKnownType(WellKnownType.System_Runtime_CompilerServices_CallSite_T);
			MethodSymbol methodSymbol = this._factory.WellKnownMethod(WellKnownMember.System_Runtime_CompilerServices_CallSite_T__Create);
			FieldSymbol fieldSymbol = (FieldSymbol)this._factory.WellKnownMember(WellKnownMember.System_Runtime_CompilerServices_CallSite_T__Target);
			MethodSymbol delegateInvokeMethod;
			if (binderConstruction == null || delegateType == null || delegateType.IsErrorType() || (delegateInvokeMethod = delegateType.DelegateInvokeMethod) == null || namedTypeSymbol.IsErrorType() || methodSymbol == null || fieldSymbol == null)
			{
				this._factory.Diagnostics.Add(ErrorCode.ERR_DynamicRequiredTypesMissing, NoLocation.Singleton);
				return LoweredDynamicOperation.Bad(loweredReceiver, loweredArguments, loweredRight, resultType);
			}
			if (this._currentDynamicCallSiteContainer == null)
			{
				this._currentDynamicCallSiteContainer = LoweredDynamicOperationFactory.CreateCallSiteContainer(this._factory, this._methodOrdinal, this._localFunctionOrdinal);
			}
			SynthesizedContainer synthesizedContainer = (SynthesizedContainer)this._currentDynamicCallSiteContainer.OriginalDefinition;
			TypeMap typeMap = synthesizedContainer.TypeMap;
			ImmutableArray<LocalSymbol> immutableArray = this.MakeTempsForDiscardArguments(ref loweredArguments);
			NamedTypeSymbol namedTypeSymbol2 = namedTypeSymbol;
			TypeSymbol[] array = new NamedTypeSymbol[] { delegateType };
			NamedTypeSymbol namedTypeSymbol3 = namedTypeSymbol2.Construct(array);
			MethodSymbol methodSymbol2 = methodSymbol.AsMember(namedTypeSymbol3);
			FieldSymbol fieldSymbol2 = fieldSymbol.AsMember(namedTypeSymbol3);
			FieldSymbol fieldSymbol3 = this.DefineCallSiteStorageSymbol(synthesizedContainer, delegateType, typeMap);
			BoundFieldAccess boundFieldAccess = this._factory.Field(null, fieldSymbol3);
			ImmutableArray<BoundExpression> callSiteArguments = LoweredDynamicOperationFactory.GetCallSiteArguments(boundFieldAccess, loweredReceiver, loweredArguments, loweredRight);
			BoundExpression boundExpression = this._factory.Null(fieldSymbol3.Type);
			BoundExpression boundExpression2 = this._factory.Conditional(this._factory.ObjectEqual(boundFieldAccess, boundExpression), this._factory.AssignmentExpression(boundFieldAccess, this._factory.Call(null, methodSymbol2, binderConstruction), false), boundExpression, fieldSymbol3.Type);
			BoundCall boundCall = this._factory.Call(this._factory.Field(boundFieldAccess, fieldSymbol2), delegateInvokeMethod, callSiteArguments);
			return new LoweredDynamicOperation(this._factory, boundExpression2, boundCall, resultType, immutableArray);
		}

		// Token: 0x0600256F RID: 9583 RVA: 0x000CBFE0 File Offset: 0x000CA1E0
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<LocalSymbol> MakeTempsForDiscardArguments([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ref ImmutableArray<BoundExpression> loweredArguments)
		{
			int num = loweredArguments.Count<BoundExpression>((BoundExpression a) => a.Kind == BoundKind.DiscardExpression);
			if (num == 0)
			{
				return ImmutableArray<LocalSymbol>.Empty;
			}
			ArrayBuilder<LocalSymbol> instance = ArrayBuilder<LocalSymbol>.GetInstance(num);
			loweredArguments = this._factory.MakeTempsForDiscardArguments(loweredArguments, instance);
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06002570 RID: 9584 RVA: 0x000CC048 File Offset: 0x000CA248
		private static NamedTypeSymbol CreateCallSiteContainer(SyntheticBoundNodeFactory factory, int methodOrdinal, int localFunctionOrdinal)
		{
			int currentGenerationOrdinal = factory.CompilationState.ModuleBuilderOpt.CurrentGenerationOrdinal;
			DynamicSiteContainer dynamicSiteContainer = new DynamicSiteContainer(GeneratedNames.MakeDynamicCallSiteContainerName(methodOrdinal, localFunctionOrdinal, currentGenerationOrdinal), factory.TopLevelMethod, factory.CurrentFunction);
			factory.AddNestedType(dynamicSiteContainer);
			if (!dynamicSiteContainer.TypeParameters.IsEmpty)
			{
				return dynamicSiteContainer.Construct(dynamicSiteContainer.ConstructedFromTypeParameters.Cast<TypeParameterSymbol, TypeSymbol>());
			}
			return dynamicSiteContainer;
		}

		// Token: 0x06002571 RID: 9585 RVA: 0x000CC0AC File Offset: 0x000CA2AC
		internal FieldSymbol DefineCallSiteStorageSymbol(NamedTypeSymbol containerDefinition, NamedTypeSymbol delegateTypeOverMethodTypeParameters, TypeMap methodToContainerTypeParametersMap)
		{
			int callSiteIdDispenser = this._callSiteIdDispenser;
			this._callSiteIdDispenser = callSiteIdDispenser + 1;
			string text = GeneratedNames.MakeDynamicCallSiteFieldName(callSiteIdDispenser);
			NamedTypeSymbol namedTypeSymbol = methodToContainerTypeParametersMap.SubstituteNamedType(delegateTypeOverMethodTypeParameters);
			NamedTypeSymbol namedTypeSymbol2 = this._factory.Compilation.GetWellKnownType(WellKnownType.System_Runtime_CompilerServices_CallSite_T);
			this._factory.Diagnostics.ReportUseSite(namedTypeSymbol2, this._factory.Syntax);
			NamedTypeSymbol namedTypeSymbol3 = namedTypeSymbol2;
			TypeSymbol[] array = new NamedTypeSymbol[] { namedTypeSymbol };
			namedTypeSymbol2 = namedTypeSymbol3.Construct(array);
			SynthesizedFieldSymbol synthesizedFieldSymbol = new SynthesizedFieldSymbol(containerDefinition, namedTypeSymbol2, text, true, false, true);
			this._factory.AddField(containerDefinition, synthesizedFieldSymbol);
			if (!this._currentDynamicCallSiteContainer.IsGenericType)
			{
				return synthesizedFieldSymbol;
			}
			return synthesizedFieldSymbol.AsMember(this._currentDynamicCallSiteContainer);
		}

		// Token: 0x06002572 RID: 9586 RVA: 0x000CC158 File Offset: 0x000CA358
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal NamedTypeSymbol GetDelegateType(BoundExpression loweredReceiver, RefKind receiverRefKind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> loweredArguments, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<RefKind> refKinds, BoundExpression loweredRight, [global::System.Runtime.CompilerServices.Nullable(1)] TypeSymbol resultType)
		{
			NamedTypeSymbol namedTypeSymbol = this._factory.WellKnownType(WellKnownType.System_Runtime_CompilerServices_CallSite);
			if (namedTypeSymbol.IsErrorType())
			{
				return null;
			}
			TypeSymbol[] array = this.MakeCallSiteDelegateSignature(namedTypeSymbol, loweredReceiver, loweredArguments, loweredRight, resultType);
			bool flag = resultType.IsVoidType();
			bool flag2 = receiverRefKind != RefKind.None || !refKinds.IsDefaultOrEmpty;
			if (!flag2)
			{
				WellKnownType wellKnownType = (flag ? WellKnownTypes.GetWellKnownActionDelegate(array.Length) : WellKnownTypes.GetWellKnownFunctionDelegate(array.Length - 1));
				if (wellKnownType != WellKnownType.Unknown)
				{
					NamedTypeSymbol wellKnownType2 = this._factory.Compilation.GetWellKnownType(wellKnownType);
					if (!wellKnownType2.HasUseSiteError)
					{
						this._factory.Diagnostics.AddDependencies(wellKnownType2);
						return wellKnownType2.Construct(array);
					}
				}
			}
			BitVector bitVector;
			if (flag2)
			{
				bitVector = BitVector.Create(1 + ((loweredReceiver != null) ? 1 : 0) + loweredArguments.Length + ((loweredRight != null) ? 1 : 0));
				int num = 1;
				if (loweredReceiver != null)
				{
					bitVector[num++] = receiverRefKind > RefKind.None;
				}
				if (!refKinds.IsDefault)
				{
					int i = 0;
					while (i < refKinds.Length)
					{
						if (refKinds[i] != RefKind.None)
						{
							bitVector[num] = true;
						}
						i++;
						num++;
					}
				}
			}
			else
			{
				bitVector = default(BitVector);
			}
			int num2 = array.Length - (flag ? 0 : 1);
			int currentGenerationOrdinal = this._factory.CompilationState.ModuleBuilderOpt.CurrentGenerationOrdinal;
			return this._factory.Compilation.AnonymousTypeManager.SynthesizeDelegate(num2, bitVector, flag, currentGenerationOrdinal).Construct(array);
		}

		// Token: 0x06002573 RID: 9587 RVA: 0x000CC2C8 File Offset: 0x000CA4C8
		internal BoundExpression GetArgumentInfo(MethodSymbol argumentInfoFactory, BoundExpression boundArgument, [global::System.Runtime.CompilerServices.Nullable(2)] string name, RefKind refKind, bool isStaticType)
		{
			LoweredDynamicOperationFactory.CSharpArgumentInfoFlags csharpArgumentInfoFlags = LoweredDynamicOperationFactory.CSharpArgumentInfoFlags.None;
			if (isStaticType)
			{
				csharpArgumentInfoFlags |= LoweredDynamicOperationFactory.CSharpArgumentInfoFlags.IsStaticType;
			}
			if (name != null)
			{
				csharpArgumentInfoFlags |= LoweredDynamicOperationFactory.CSharpArgumentInfoFlags.NamedArgument;
			}
			if (refKind == RefKind.Out)
			{
				csharpArgumentInfoFlags |= LoweredDynamicOperationFactory.CSharpArgumentInfoFlags.UseCompileTimeType | LoweredDynamicOperationFactory.CSharpArgumentInfoFlags.IsOut;
			}
			else if (refKind == RefKind.Ref)
			{
				csharpArgumentInfoFlags |= LoweredDynamicOperationFactory.CSharpArgumentInfoFlags.UseCompileTimeType | LoweredDynamicOperationFactory.CSharpArgumentInfoFlags.IsRef;
			}
			TypeSymbol type = boundArgument.Type;
			if (boundArgument.ConstantValue != null)
			{
				csharpArgumentInfoFlags |= LoweredDynamicOperationFactory.CSharpArgumentInfoFlags.Constant;
			}
			if (type != null && !type.IsDynamic())
			{
				csharpArgumentInfoFlags |= LoweredDynamicOperationFactory.CSharpArgumentInfoFlags.UseCompileTimeType;
			}
			return this._factory.Call(null, argumentInfoFactory, this._factory.Literal((int)csharpArgumentInfoFlags), this._factory.Literal(name));
		}

		// Token: 0x06002574 RID: 9588 RVA: 0x000CC34C File Offset: 0x000CA54C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal static ImmutableArray<BoundExpression> GetCallSiteArguments([global::System.Runtime.CompilerServices.Nullable(1)] BoundExpression callSiteFieldAccess, BoundExpression receiver, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, BoundExpression right)
		{
			BoundExpression[] array = new BoundExpression[1 + ((receiver != null) ? 1 : 0) + arguments.Length + ((right != null) ? 1 : 0)];
			int num = 0;
			array[num++] = callSiteFieldAccess;
			if (receiver != null)
			{
				array[num++] = receiver;
			}
			arguments.CopyTo(array, num);
			num += arguments.Length;
			if (right != null)
			{
				array[num++] = right;
			}
			return array.AsImmutableOrNull<BoundExpression>();
		}

		// Token: 0x06002575 RID: 9589 RVA: 0x000CC3B4 File Offset: 0x000CA5B4
		internal TypeSymbol[] MakeCallSiteDelegateSignature(TypeSymbol callSiteType, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiver, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression right, TypeSymbol resultType)
		{
			NamedTypeSymbol namedTypeSymbol = this._factory.SpecialType(SpecialType.System_Object);
			TypeSymbol[] array = new TypeSymbol[1 + ((receiver != null) ? 1 : 0) + arguments.Length + ((right != null) ? 1 : 0) + (resultType.IsVoidType() ? 0 : 1)];
			int num = 0;
			array[num++] = callSiteType;
			if (receiver != null)
			{
				array[num++] = receiver.Type ?? namedTypeSymbol;
			}
			for (int i = 0; i < arguments.Length; i++)
			{
				array[num++] = arguments[i].Type ?? namedTypeSymbol;
			}
			if (right != null)
			{
				array[num++] = right.Type ?? namedTypeSymbol;
			}
			if (num < array.Length)
			{
				array[num++] = resultType ?? namedTypeSymbol;
			}
			return array;
		}

		// Token: 0x040010DE RID: 4318
		private readonly SyntheticBoundNodeFactory _factory;

		// Token: 0x040010DF RID: 4319
		private readonly int _methodOrdinal;

		// Token: 0x040010E0 RID: 4320
		private readonly int _localFunctionOrdinal;

		// Token: 0x040010E1 RID: 4321
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private NamedTypeSymbol _currentDynamicCallSiteContainer;

		// Token: 0x040010E2 RID: 4322
		private int _callSiteIdDispenser;

		// Token: 0x0200073A RID: 1850
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[Flags]
		private enum CSharpBinderFlags
		{
			// Token: 0x040022F1 RID: 8945
			None = 0,
			// Token: 0x040022F2 RID: 8946
			CheckedContext = 1,
			// Token: 0x040022F3 RID: 8947
			InvokeSimpleName = 2,
			// Token: 0x040022F4 RID: 8948
			InvokeSpecialName = 4,
			// Token: 0x040022F5 RID: 8949
			BinaryOperationLogical = 8,
			// Token: 0x040022F6 RID: 8950
			ConvertExplicit = 16,
			// Token: 0x040022F7 RID: 8951
			ConvertArrayIndex = 32,
			// Token: 0x040022F8 RID: 8952
			ResultIndexed = 64,
			// Token: 0x040022F9 RID: 8953
			ValueFromCompoundAssignment = 128,
			// Token: 0x040022FA RID: 8954
			ResultDiscarded = 256
		}

		// Token: 0x0200073B RID: 1851
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[Flags]
		private enum CSharpArgumentInfoFlags
		{
			// Token: 0x040022FC RID: 8956
			None = 0,
			// Token: 0x040022FD RID: 8957
			UseCompileTimeType = 1,
			// Token: 0x040022FE RID: 8958
			Constant = 2,
			// Token: 0x040022FF RID: 8959
			NamedArgument = 4,
			// Token: 0x04002300 RID: 8960
			IsRef = 8,
			// Token: 0x04002301 RID: 8961
			IsOut = 16,
			// Token: 0x04002302 RID: 8962
			IsStaticType = 32
		}
	}
}
