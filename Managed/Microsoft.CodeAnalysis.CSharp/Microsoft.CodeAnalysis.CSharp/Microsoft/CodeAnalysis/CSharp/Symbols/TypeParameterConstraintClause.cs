using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200055E RID: 1374
	internal sealed class TypeParameterConstraintClause
	{
		// Token: 0x06007439 RID: 29753 RVA: 0x001B238C File Offset: 0x001B058C
		internal static TypeParameterConstraintClause Create(TypeParameterConstraintKind constraints, ImmutableArray<TypeWithAnnotations> constraintTypes)
		{
			if (constraintTypes.IsEmpty)
			{
				if (constraints == TypeParameterConstraintKind.None)
				{
					return TypeParameterConstraintClause.Empty;
				}
				if (constraints == TypeParameterConstraintKind.ObliviousNullabilityIfReferenceType)
				{
					return TypeParameterConstraintClause.ObliviousNullabilityIfReferenceType;
				}
			}
			return new TypeParameterConstraintClause(constraints, constraintTypes);
		}

		// Token: 0x0600743A RID: 29754 RVA: 0x001B23B4 File Offset: 0x001B05B4
		private TypeParameterConstraintClause(TypeParameterConstraintKind constraints, ImmutableArray<TypeWithAnnotations> constraintTypes)
		{
			this.Constraints = constraints;
			this.ConstraintTypes = constraintTypes;
		}

		// Token: 0x0600743B RID: 29755 RVA: 0x001B23CC File Offset: 0x001B05CC
		internal static SmallDictionary<TypeParameterSymbol, bool> BuildIsValueTypeMap(Symbol container, ImmutableArray<TypeParameterSymbol> typeParameters, ImmutableArray<TypeParameterConstraintClause> constraintClauses)
		{
			SmallDictionary<TypeParameterSymbol, bool> smallDictionary = new SmallDictionary<TypeParameterSymbol, bool>(ReferenceEqualityComparer.Instance);
			foreach (TypeParameterSymbol typeParameterSymbol in typeParameters)
			{
				TypeParameterConstraintClause.<BuildIsValueTypeMap>g__isValueType|6_0(typeParameterSymbol, constraintClauses, smallDictionary, ConsList<TypeParameterSymbol>.Empty);
			}
			return smallDictionary;
		}

		// Token: 0x0600743C RID: 29756 RVA: 0x001B240C File Offset: 0x001B060C
		internal static SmallDictionary<TypeParameterSymbol, bool> BuildIsReferenceTypeFromConstraintTypesMap(Symbol container, ImmutableArray<TypeParameterSymbol> typeParameters, ImmutableArray<TypeParameterConstraintClause> constraintClauses)
		{
			SmallDictionary<TypeParameterSymbol, bool> smallDictionary = new SmallDictionary<TypeParameterSymbol, bool>(ReferenceEqualityComparer.Instance);
			foreach (TypeParameterSymbol typeParameterSymbol in typeParameters)
			{
				TypeParameterConstraintClause.<BuildIsReferenceTypeFromConstraintTypesMap>g__isReferenceTypeFromConstraintTypes|7_0(typeParameterSymbol, constraintClauses, smallDictionary, ConsList<TypeParameterSymbol>.Empty);
			}
			return smallDictionary;
		}

		// Token: 0x0600743E RID: 29758 RVA: 0x001B2470 File Offset: 0x001B0670
		[CompilerGenerated]
		internal static bool <BuildIsValueTypeMap>g__isValueType|6_0(TypeParameterSymbol thisTypeParameter, ImmutableArray<TypeParameterConstraintClause> constraintClauses, SmallDictionary<TypeParameterSymbol, bool> isValueTypeMap, ConsList<TypeParameterSymbol> inProgress)
		{
			if (inProgress.ContainsReference(thisTypeParameter))
			{
				return false;
			}
			bool flag;
			if (isValueTypeMap.TryGetValue(thisTypeParameter, out flag))
			{
				return flag;
			}
			TypeParameterConstraintClause typeParameterConstraintClause = constraintClauses[thisTypeParameter.Ordinal];
			bool flag2 = false;
			if ((typeParameterConstraintClause.Constraints & TypeParameterConstraintKind.AllValueTypeKinds) != TypeParameterConstraintKind.None)
			{
				flag2 = true;
			}
			else
			{
				Symbol containingSymbol = thisTypeParameter.ContainingSymbol;
				inProgress = inProgress.Prepend(thisTypeParameter);
				foreach (TypeWithAnnotations typeWithAnnotations in typeParameterConstraintClause.ConstraintTypes)
				{
					TypeSymbol typeSymbol = (typeWithAnnotations.IsResolved ? typeWithAnnotations.Type : typeWithAnnotations.DefaultType);
					TypeParameterSymbol typeParameterSymbol = typeSymbol as TypeParameterSymbol;
					if (typeParameterSymbol != null && typeParameterSymbol.ContainingSymbol == containingSymbol)
					{
						if (TypeParameterConstraintClause.<BuildIsValueTypeMap>g__isValueType|6_0(typeParameterSymbol, constraintClauses, isValueTypeMap, inProgress))
						{
							flag2 = true;
							break;
						}
					}
					else if (typeSymbol.IsValueType)
					{
						flag2 = true;
						break;
					}
				}
			}
			isValueTypeMap.Add(thisTypeParameter, flag2);
			return flag2;
		}

		// Token: 0x0600743F RID: 29759 RVA: 0x001B2544 File Offset: 0x001B0744
		[CompilerGenerated]
		internal static bool <BuildIsReferenceTypeFromConstraintTypesMap>g__isReferenceTypeFromConstraintTypes|7_0(TypeParameterSymbol thisTypeParameter, ImmutableArray<TypeParameterConstraintClause> constraintClauses, SmallDictionary<TypeParameterSymbol, bool> isReferenceTypeFromConstraintTypesMap, ConsList<TypeParameterSymbol> inProgress)
		{
			if (inProgress.ContainsReference(thisTypeParameter))
			{
				return false;
			}
			bool flag;
			if (isReferenceTypeFromConstraintTypesMap.TryGetValue(thisTypeParameter, out flag))
			{
				return flag;
			}
			TypeParameterConstraintClause typeParameterConstraintClause = constraintClauses[thisTypeParameter.Ordinal];
			bool flag2 = false;
			Symbol containingSymbol = thisTypeParameter.ContainingSymbol;
			inProgress = inProgress.Prepend(thisTypeParameter);
			foreach (TypeWithAnnotations typeWithAnnotations in typeParameterConstraintClause.ConstraintTypes)
			{
				TypeSymbol typeSymbol = (typeWithAnnotations.IsResolved ? typeWithAnnotations.Type : typeWithAnnotations.DefaultType);
				TypeParameterSymbol typeParameterSymbol = typeSymbol as TypeParameterSymbol;
				if (typeParameterSymbol != null)
				{
					if (typeParameterSymbol.ContainingSymbol == containingSymbol)
					{
						if (TypeParameterConstraintClause.<BuildIsReferenceTypeFromConstraintTypesMap>g__isReferenceTypeFromConstraintTypes|7_0(typeParameterSymbol, constraintClauses, isReferenceTypeFromConstraintTypesMap, inProgress))
						{
							flag2 = true;
							break;
						}
					}
					else if (typeParameterSymbol.IsReferenceTypeFromConstraintTypes)
					{
						flag2 = true;
						break;
					}
				}
				else if (TypeParameterSymbol.NonTypeParameterConstraintImpliesReferenceType(typeSymbol))
				{
					flag2 = true;
					break;
				}
			}
			isReferenceTypeFromConstraintTypesMap.Add(thisTypeParameter, flag2);
			return flag2;
		}

		// Token: 0x04001C3B RID: 7227
		internal static readonly TypeParameterConstraintClause Empty = new TypeParameterConstraintClause(TypeParameterConstraintKind.None, ImmutableArray<TypeWithAnnotations>.Empty);

		// Token: 0x04001C3C RID: 7228
		internal static readonly TypeParameterConstraintClause ObliviousNullabilityIfReferenceType = new TypeParameterConstraintClause(TypeParameterConstraintKind.ObliviousNullabilityIfReferenceType, ImmutableArray<TypeWithAnnotations>.Empty);

		// Token: 0x04001C3D RID: 7229
		public readonly TypeParameterConstraintKind Constraints;

		// Token: 0x04001C3E RID: 7230
		public readonly ImmutableArray<TypeWithAnnotations> ConstraintTypes;
	}
}
