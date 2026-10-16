using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000045 RID: 69
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal static class BestTypeInferrer
	{
		// Token: 0x06000707 RID: 1799 RVA: 0x0003D524 File Offset: 0x0003B724
		public static NullableAnnotation GetNullableAnnotation(ArrayBuilder<TypeWithAnnotations> types)
		{
			NullableAnnotation nullableAnnotation = NullableAnnotation.NotAnnotated;
			foreach (TypeWithAnnotations typeWithAnnotations in types)
			{
				nullableAnnotation = nullableAnnotation.Join(typeWithAnnotations.NullableAnnotation);
			}
			return nullableAnnotation;
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0003D55C File Offset: 0x0003B75C
		public static NullableFlowState GetNullableState(ArrayBuilder<TypeWithState> types)
		{
			NullableFlowState nullableFlowState = NullableFlowState.NotNull;
			foreach (TypeWithState typeWithState in types)
			{
				nullableFlowState = nullableFlowState.Join(typeWithState.State);
			}
			return nullableFlowState;
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0003D594 File Offset: 0x0003B794
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public static TypeSymbol InferBestType([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> exprs, ConversionsBase conversions, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			HashSet<TypeSymbol> hashSet = new HashSet<TypeSymbol>(conversions.IncludeNullability ? SymbolEqualityComparer.ConsiderEverything : SymbolEqualityComparer.IgnoringNullable);
			foreach (BoundExpression boundExpression in exprs)
			{
				TypeSymbol type = boundExpression.Type;
				if (type != null)
				{
					if (type.IsErrorType())
					{
						return type;
					}
					hashSet.Add(type);
				}
			}
			ArrayBuilder<TypeSymbol> instance = ArrayBuilder<TypeSymbol>.GetInstance(hashSet.Count);
			instance.AddRange(hashSet);
			TypeSymbol bestType = BestTypeInferrer.GetBestType(instance, conversions, ref useSiteInfo);
			instance.Free();
			return bestType;
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0003D614 File Offset: 0x0003B814
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public static TypeSymbol InferBestTypeForConditionalOperator(BoundExpression expr1, BoundExpression expr2, ConversionsBase conversions, out bool hadMultipleCandidates, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			ArrayBuilder<TypeSymbol> instance = ArrayBuilder<TypeSymbol>.GetInstance();
			TypeSymbol bestType;
			try
			{
				ConversionsBase conversionsBase = conversions.WithNullability(false);
				TypeSymbol type = expr1.Type;
				if (type != null)
				{
					if (type.IsErrorType())
					{
						hadMultipleCandidates = false;
						return type;
					}
					if (conversionsBase.ClassifyImplicitConversionFromExpression(expr2, type, ref useSiteInfo).Exists)
					{
						instance.Add(type);
					}
				}
				TypeSymbol type2 = expr2.Type;
				if (type2 != null)
				{
					if (type2.IsErrorType())
					{
						hadMultipleCandidates = false;
						return type2;
					}
					if (conversionsBase.ClassifyImplicitConversionFromExpression(expr1, type2, ref useSiteInfo).Exists)
					{
						instance.Add(type2);
					}
				}
				hadMultipleCandidates = instance.Count > 1;
				bestType = BestTypeInferrer.GetBestType(instance, conversions, ref useSiteInfo);
			}
			finally
			{
				instance.Free();
			}
			return bestType;
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0003D6CC File Offset: 0x0003B8CC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal static TypeSymbol GetBestType(ArrayBuilder<TypeSymbol> types, ConversionsBase conversions, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			int count = types.Count;
			if (count == 0)
			{
				return null;
			}
			if (count == 1)
			{
				return types[0];
			}
			TypeSymbol typeSymbol = null;
			int num = -1;
			for (int i = 0; i < types.Count; i++)
			{
				TypeSymbol typeSymbol2 = types[i];
				if (typeSymbol == null)
				{
					typeSymbol = typeSymbol2;
					num = i;
				}
				else
				{
					TypeSymbol typeSymbol3 = BestTypeInferrer.Better(typeSymbol, typeSymbol2, conversions, ref useSiteInfo);
					if (typeSymbol3 == null)
					{
						typeSymbol = null;
					}
					else
					{
						typeSymbol = typeSymbol3;
						num = i;
					}
				}
			}
			if (typeSymbol == null)
			{
				return null;
			}
			for (int j = 0; j < num; j++)
			{
				TypeSymbol typeSymbol4 = types[j];
				TypeSymbol typeSymbol5 = BestTypeInferrer.Better(typeSymbol, typeSymbol4, conversions, ref useSiteInfo);
				if (!typeSymbol.Equals(typeSymbol5, TypeCompareKind.IgnoreNullableModifiersForReferenceTypes))
				{
					return null;
				}
			}
			return typeSymbol;
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0003D770 File Offset: 0x0003B970
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		private static TypeSymbol Better(TypeSymbol type1, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type2, ConversionsBase conversions, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (type1.IsErrorType())
			{
				return type2;
			}
			if (type2 == null || type2.IsErrorType())
			{
				return type1;
			}
			ConversionsBase conversionsBase = conversions.WithNullability(false);
			bool exists = conversionsBase.ClassifyImplicitConversionFromType(type1, type2, ref useSiteInfo).Exists;
			bool exists2 = conversionsBase.ClassifyImplicitConversionFromType(type2, type1, ref useSiteInfo).Exists;
			if (exists && exists2)
			{
				if (type1.Equals(type2, TypeCompareKind.IgnoreDynamic | TypeCompareKind.IgnoreTupleNames | TypeCompareKind.IgnoreNullableModifiersForReferenceTypes))
				{
					return type1.MergeEquivalentTypes(type2, VarianceKind.Out);
				}
				return null;
			}
			else
			{
				if (exists)
				{
					return type2;
				}
				if (exists2)
				{
					return type1;
				}
				return null;
			}
		}
	}
}
