using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004A9 RID: 1193
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal static class MethodBodySynthesizer
	{
		// Token: 0x060062C9 RID: 25289 RVA: 0x0017B66C File Offset: 0x0017986C
		public static BoundExpression GenerateHashCombine(BoundExpression currentHashValue, MethodSymbol system_Collections_Generic_EqualityComparer_T__GetHashCode, MethodSymbol system_Collections_Generic_EqualityComparer_T__get_Default, [global::System.Runtime.CompilerServices.Nullable(2)] ref BoundLiteral boundHashFactor, BoundExpression valueToHash, SyntheticBoundNodeFactory F)
		{
			TypeSymbol type = currentHashValue.Type;
			if (boundHashFactor == null)
			{
				boundHashFactor = F.Literal(-1521134295);
			}
			currentHashValue = F.Binary(BinaryOperatorKind.IntMultiplication, type, currentHashValue, boundHashFactor);
			currentHashValue = F.Binary(BinaryOperatorKind.IntAddition, type, currentHashValue, MethodBodySynthesizer.GenerateGetHashCode(system_Collections_Generic_EqualityComparer_T__GetHashCode, system_Collections_Generic_EqualityComparer_T__get_Default, valueToHash, F));
			return currentHashValue;
		}

		// Token: 0x060062CA RID: 25290 RVA: 0x0017B6C0 File Offset: 0x001798C0
		public static BoundCall GenerateGetHashCode(MethodSymbol system_Collections_Generic_EqualityComparer_T__GetHashCode, MethodSymbol system_Collections_Generic_EqualityComparer_T__get_Default, BoundExpression valueToHash, SyntheticBoundNodeFactory F)
		{
			NamedTypeSymbol namedTypeSymbol = system_Collections_Generic_EqualityComparer_T__GetHashCode.ContainingType.Construct(new TypeSymbol[] { valueToHash.Type });
			return F.Call(F.StaticCall(namedTypeSymbol, system_Collections_Generic_EqualityComparer_T__get_Default.AsMember(namedTypeSymbol), Array.Empty<BoundExpression>()), system_Collections_Generic_EqualityComparer_T__GetHashCode.AsMember(namedTypeSymbol), valueToHash);
		}

		// Token: 0x060062CB RID: 25291 RVA: 0x0017B70C File Offset: 0x0017990C
		public static BoundExpression GenerateFieldEquals([global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression initialExpression, BoundExpression otherReceiver, ArrayBuilder<FieldSymbol> fields, SyntheticBoundNodeFactory F)
		{
			MethodSymbol methodSymbol = F.WellKnownMethod(WellKnownMember.System_Collections_Generic_EqualityComparer_T__get_Default);
			MethodSymbol methodSymbol2 = F.WellKnownMethod(WellKnownMember.System_Collections_Generic_EqualityComparer_T__Equals);
			NamedTypeSymbol containingType = methodSymbol2.ContainingType;
			BoundExpression boundExpression = initialExpression;
			foreach (FieldSymbol fieldSymbol in fields)
			{
				NamedTypeSymbol namedTypeSymbol = containingType.Construct(new TypeSymbol[] { fieldSymbol.Type });
				BoundExpression boundExpression2 = F.Call(F.StaticCall(namedTypeSymbol, methodSymbol.AsMember(namedTypeSymbol), Array.Empty<BoundExpression>()), methodSymbol2.AsMember(namedTypeSymbol), F.Field(F.This(), fieldSymbol), F.Field(otherReceiver, fieldSymbol));
				boundExpression = ((boundExpression == null) ? boundExpression2 : F.LogicalAnd(boundExpression, boundExpression2));
			}
			return boundExpression;
		}

		// Token: 0x060062CC RID: 25292 RVA: 0x0017B7B8 File Offset: 0x001799B8
		internal static BoundBlock ConstructSingleInvocationMethodBody(SyntheticBoundNodeFactory F, MethodSymbol methodToInvoke, bool useBaseReference)
		{
			ArrayBuilder<BoundExpression> instance = ArrayBuilder<BoundExpression>.GetInstance();
			foreach (ParameterSymbol parameterSymbol in F.CurrentFunction.Parameters)
			{
				instance.Add(F.Parameter(parameterSymbol));
			}
			BoundExpression boundExpression = F.Call(useBaseReference ? F.Base(methodToInvoke.ContainingType) : F.This(), methodToInvoke, instance.ToImmutableAndFree());
			if (!F.CurrentFunction.ReturnsVoid)
			{
				return F.Block(new BoundStatement[] { F.Return(boundExpression) });
			}
			return F.Block(new BoundStatement[]
			{
				F.ExpressionStatement(boundExpression),
				F.Return(null)
			});
		}

		// Token: 0x04001989 RID: 6537
		public const int HASH_FACTOR = -1521134295;
	}
}
