using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004BB RID: 1211
	internal abstract class AbstractTypeMap
	{
		// Token: 0x060067AD RID: 26541 RVA: 0x001864F8 File Offset: 0x001846F8
		internal virtual NamedTypeSymbol SubstituteTypeDeclaration(NamedTypeSymbol previous)
		{
			NamedTypeSymbol namedTypeSymbol = this.SubstituteNamedType(previous.ContainingType);
			if (namedTypeSymbol == null)
			{
				return previous;
			}
			return previous.OriginalDefinition.AsMember(namedTypeSymbol);
		}

		// Token: 0x060067AE RID: 26542 RVA: 0x00186524 File Offset: 0x00184724
		internal NamedTypeSymbol SubstituteNamedType(NamedTypeSymbol previous)
		{
			if (previous == null)
			{
				return null;
			}
			if (previous.IsUnboundGenericType)
			{
				return previous;
			}
			if (previous.IsAnonymousType)
			{
				ImmutableArray<TypeWithAnnotations> anonymousTypePropertyTypesWithAnnotations = AnonymousTypeManager.GetAnonymousTypePropertyTypesWithAnnotations(previous);
				ImmutableArray<TypeWithAnnotations> immutableArray = this.SubstituteTypes(anonymousTypePropertyTypesWithAnnotations);
				if (!(anonymousTypePropertyTypesWithAnnotations == immutableArray))
				{
					return AnonymousTypeManager.ConstructAnonymousTypeSymbol(previous, immutableArray);
				}
				return previous;
			}
			else
			{
				NamedTypeSymbol constructedFrom = previous.ConstructedFrom;
				NamedTypeSymbol namedTypeSymbol = this.SubstituteTypeDeclaration(constructedFrom);
				ImmutableArray<TypeWithAnnotations> typeArgumentsWithAnnotationsNoUseSiteDiagnostics = previous.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics;
				bool flag = constructedFrom != namedTypeSymbol;
				ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance(typeArgumentsWithAnnotationsNoUseSiteDiagnostics.Length);
				for (int i = 0; i < typeArgumentsWithAnnotationsNoUseSiteDiagnostics.Length; i++)
				{
					TypeWithAnnotations typeWithAnnotations = typeArgumentsWithAnnotationsNoUseSiteDiagnostics[i];
					TypeWithAnnotations typeWithAnnotations2 = typeWithAnnotations.SubstituteType(this);
					if (!flag && !typeWithAnnotations.IsSameAs(typeWithAnnotations2))
					{
						flag = true;
					}
					instance.Add(typeWithAnnotations2);
				}
				if (!flag)
				{
					instance.Free();
					return previous;
				}
				return namedTypeSymbol.ConstructIfGeneric(instance.ToImmutableAndFree()).WithTupleDataFrom(previous);
			}
		}

		// Token: 0x060067AF RID: 26543 RVA: 0x00186600 File Offset: 0x00184800
		internal TypeWithAnnotations SubstituteType(TypeSymbol previous)
		{
			if (previous == null)
			{
				return default(TypeWithAnnotations);
			}
			SymbolKind kind = previous.Kind;
			TypeSymbol typeSymbol;
			if (kind <= SymbolKind.NamedType)
			{
				switch (kind)
				{
				case SymbolKind.ArrayType:
					typeSymbol = this.SubstituteArrayType((ArrayTypeSymbol)previous);
					goto IL_00AA;
				case SymbolKind.Assembly:
					break;
				case SymbolKind.DynamicType:
					typeSymbol = this.SubstituteDynamicType();
					goto IL_00AA;
				case SymbolKind.ErrorType:
					return ((ErrorTypeSymbol)previous).Substitute(this);
				default:
					if (kind == SymbolKind.NamedType)
					{
						typeSymbol = this.SubstituteNamedType((NamedTypeSymbol)previous);
						goto IL_00AA;
					}
					break;
				}
			}
			else
			{
				if (kind == SymbolKind.PointerType)
				{
					typeSymbol = this.SubstitutePointerType((PointerTypeSymbol)previous);
					goto IL_00AA;
				}
				if (kind == SymbolKind.TypeParameter)
				{
					return this.SubstituteTypeParameter((TypeParameterSymbol)previous);
				}
				if (kind == SymbolKind.FunctionPointerType)
				{
					typeSymbol = this.SubstituteFunctionPointerType((FunctionPointerTypeSymbol)previous);
					goto IL_00AA;
				}
			}
			typeSymbol = previous;
			IL_00AA:
			return TypeWithAnnotations.Create(typeSymbol, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
		}

		// Token: 0x060067B0 RID: 26544 RVA: 0x001866C7 File Offset: 0x001848C7
		internal TypeWithAnnotations SubstituteType(TypeWithAnnotations previous)
		{
			return previous.SubstituteType(this);
		}

		// Token: 0x060067B1 RID: 26545 RVA: 0x001866D4 File Offset: 0x001848D4
		internal virtual ImmutableArray<CustomModifier> SubstituteCustomModifiers(ImmutableArray<CustomModifier> customModifiers)
		{
			if (customModifiers.IsDefaultOrEmpty)
			{
				return customModifiers;
			}
			for (int i = 0; i < customModifiers.Length; i++)
			{
				NamedTypeSymbol namedTypeSymbol = ((CSharpCustomModifier)customModifiers[i]).ModifierSymbol;
				NamedTypeSymbol namedTypeSymbol2 = this.SubstituteNamedType(namedTypeSymbol);
				if (!TypeSymbol.Equals(namedTypeSymbol, namedTypeSymbol2, TypeCompareKind.ConsiderEverything))
				{
					ArrayBuilder<CustomModifier> instance = ArrayBuilder<CustomModifier>.GetInstance(customModifiers.Length);
					instance.AddRange(customModifiers, i);
					instance.Add(customModifiers[i].IsOptional ? CSharpCustomModifier.CreateOptional(namedTypeSymbol2) : CSharpCustomModifier.CreateRequired(namedTypeSymbol2));
					for (i++; i < customModifiers.Length; i++)
					{
						namedTypeSymbol = ((CSharpCustomModifier)customModifiers[i]).ModifierSymbol;
						namedTypeSymbol2 = this.SubstituteNamedType(namedTypeSymbol);
						if (!TypeSymbol.Equals(namedTypeSymbol, namedTypeSymbol2, TypeCompareKind.ConsiderEverything))
						{
							instance.Add(customModifiers[i].IsOptional ? CSharpCustomModifier.CreateOptional(namedTypeSymbol2) : CSharpCustomModifier.CreateRequired(namedTypeSymbol2));
						}
						else
						{
							instance.Add(customModifiers[i]);
						}
					}
					return instance.ToImmutableAndFree();
				}
			}
			return customModifiers;
		}

		// Token: 0x060067B2 RID: 26546 RVA: 0x001867D8 File Offset: 0x001849D8
		protected virtual TypeSymbol SubstituteDynamicType()
		{
			return DynamicTypeSymbol.Instance;
		}

		// Token: 0x060067B3 RID: 26547 RVA: 0x001867E0 File Offset: 0x001849E0
		protected virtual TypeWithAnnotations SubstituteTypeParameter(TypeParameterSymbol typeParameter)
		{
			return TypeWithAnnotations.Create(typeParameter, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
		}

		// Token: 0x060067B4 RID: 26548 RVA: 0x00186800 File Offset: 0x00184A00
		private ArrayTypeSymbol SubstituteArrayType(ArrayTypeSymbol t)
		{
			TypeWithAnnotations elementTypeWithAnnotations = t.ElementTypeWithAnnotations;
			TypeWithAnnotations typeWithAnnotations = elementTypeWithAnnotations.SubstituteType(this);
			if (typeWithAnnotations.IsSameAs(elementTypeWithAnnotations))
			{
				return t;
			}
			if (t.IsSZArray)
			{
				ImmutableArray<NamedTypeSymbol> immutableArray = t.InterfacesNoUseSiteDiagnostics(null);
				if (immutableArray.Length == 1)
				{
					immutableArray = ImmutableArray.Create<NamedTypeSymbol>(this.SubstituteNamedType(immutableArray[0]));
				}
				else if (immutableArray.Length == 2)
				{
					immutableArray = ImmutableArray.Create<NamedTypeSymbol>(this.SubstituteNamedType(immutableArray[0]), this.SubstituteNamedType(immutableArray[1]));
				}
				else if (immutableArray.Length != 0)
				{
					throw ExceptionUtilities.Unreachable;
				}
				return ArrayTypeSymbol.CreateSZArray(typeWithAnnotations, t.BaseTypeNoUseSiteDiagnostics, immutableArray);
			}
			return ArrayTypeSymbol.CreateMDArray(typeWithAnnotations, t.Rank, t.Sizes, t.LowerBounds, t.BaseTypeNoUseSiteDiagnostics);
		}

		// Token: 0x060067B5 RID: 26549 RVA: 0x001868C4 File Offset: 0x00184AC4
		private PointerTypeSymbol SubstitutePointerType(PointerTypeSymbol t)
		{
			TypeWithAnnotations pointedAtTypeWithAnnotations = t.PointedAtTypeWithAnnotations;
			TypeWithAnnotations typeWithAnnotations = pointedAtTypeWithAnnotations.SubstituteType(this);
			if (typeWithAnnotations.IsSameAs(pointedAtTypeWithAnnotations))
			{
				return t;
			}
			return new PointerTypeSymbol(typeWithAnnotations);
		}

		// Token: 0x060067B6 RID: 26550 RVA: 0x001868F4 File Offset: 0x00184AF4
		private FunctionPointerTypeSymbol SubstituteFunctionPointerType(FunctionPointerTypeSymbol f)
		{
			TypeWithAnnotations typeWithAnnotations = f.Signature.ReturnTypeWithAnnotations.SubstituteType(this);
			ImmutableArray<CustomModifier> refCustomModifiers = f.Signature.RefCustomModifiers;
			ImmutableArray<CustomModifier> immutableArray = this.SubstituteCustomModifiers(refCustomModifiers);
			ImmutableArray<TypeWithAnnotations> parameterTypesWithAnnotations = f.Signature.ParameterTypesWithAnnotations;
			ImmutableArray<TypeWithAnnotations> immutableArray2 = this.SubstituteTypes(parameterTypesWithAnnotations);
			ImmutableArray<ImmutableArray<CustomModifier>> immutableArray3 = default(ImmutableArray<ImmutableArray<CustomModifier>>);
			int length = f.Signature.Parameters.Length;
			if (length > 0)
			{
				ArrayBuilder<ImmutableArray<CustomModifier>> instance = ArrayBuilder<ImmutableArray<CustomModifier>>.GetInstance(length);
				bool flag = false;
				foreach (ParameterSymbol parameterSymbol in f.Signature.Parameters)
				{
					ImmutableArray<CustomModifier> immutableArray4 = this.SubstituteCustomModifiers(parameterSymbol.RefCustomModifiers);
					instance.Add(immutableArray4);
					if (immutableArray4 != parameterSymbol.RefCustomModifiers)
					{
						flag = true;
					}
				}
				if (flag)
				{
					immutableArray3 = instance.ToImmutableAndFree();
				}
				else
				{
					instance.Free();
				}
			}
			if (immutableArray2 != parameterTypesWithAnnotations || !immutableArray3.IsDefault || !f.Signature.ReturnTypeWithAnnotations.IsSameAs(typeWithAnnotations) || immutableArray != refCustomModifiers)
			{
				f = f.SubstituteTypeSymbol(typeWithAnnotations, immutableArray2, refCustomModifiers, immutableArray3);
			}
			return f;
		}

		// Token: 0x060067B7 RID: 26551 RVA: 0x00186A1C File Offset: 0x00184C1C
		internal ImmutableArray<TypeSymbol> SubstituteTypesWithoutModifiers(ImmutableArray<TypeSymbol> original)
		{
			if (original.IsDefault)
			{
				return original;
			}
			TypeSymbol[] array = null;
			for (int i = 0; i < original.Length; i++)
			{
				TypeSymbol typeSymbol = original[i];
				TypeSymbol type = this.SubstituteType(typeSymbol).Type;
				if (type != typeSymbol && array == null)
				{
					array = new TypeSymbol[original.Length];
					for (int j = 0; j < i; j++)
					{
						array[j] = original[j];
					}
				}
				if (array != null)
				{
					array[i] = type;
				}
			}
			if (array == null)
			{
				return original;
			}
			return array.AsImmutableOrNull<TypeSymbol>();
		}

		// Token: 0x060067B8 RID: 26552 RVA: 0x00186AA8 File Offset: 0x00184CA8
		internal ImmutableArray<TypeWithAnnotations> SubstituteTypes(ImmutableArray<TypeWithAnnotations> original)
		{
			if (original.IsDefault)
			{
				return default(ImmutableArray<TypeWithAnnotations>);
			}
			ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance(original.Length);
			foreach (TypeWithAnnotations typeWithAnnotations in original)
			{
				instance.Add(this.SubstituteType(typeWithAnnotations));
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x060067B9 RID: 26553 RVA: 0x00186B04 File Offset: 0x00184D04
		internal void SubstituteConstraintTypesDistinctWithoutModifiers(TypeParameterSymbol owner, ImmutableArray<TypeWithAnnotations> original, ArrayBuilder<TypeWithAnnotations> result, HashSet<TypeParameterSymbol> ignoreTypesDependentOnTypeParametersOpt)
		{
			AbstractTypeMap.<>c__DisplayClass12_0 CS$<>8__locals1;
			CS$<>8__locals1.owner = owner;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.dynamicEraser = null;
			if (original.Length == 0)
			{
				return;
			}
			if (original.Length == 1)
			{
				TypeWithAnnotations typeWithAnnotations = original[0];
				if (ignoreTypesDependentOnTypeParametersOpt == null || !typeWithAnnotations.Type.ContainsTypeParameters(ignoreTypesDependentOnTypeParametersOpt))
				{
					result.Add(this.<SubstituteConstraintTypesDistinctWithoutModifiers>g__substituteConstraintType|12_0(typeWithAnnotations, ref CS$<>8__locals1));
					return;
				}
			}
			else
			{
				PooledDictionary<TypeSymbol, int> instance = PooledDictionary<TypeSymbol, int>.GetInstance();
				foreach (TypeWithAnnotations typeWithAnnotations2 in original)
				{
					if (ignoreTypesDependentOnTypeParametersOpt == null || !typeWithAnnotations2.Type.ContainsTypeParameters(ignoreTypesDependentOnTypeParametersOpt))
					{
						TypeWithAnnotations typeWithAnnotations3 = this.<SubstituteConstraintTypesDistinctWithoutModifiers>g__substituteConstraintType|12_0(typeWithAnnotations2, ref CS$<>8__locals1);
						int num;
						if (!instance.TryGetValue(typeWithAnnotations3.Type, ref num))
						{
							instance.Add(typeWithAnnotations3.Type, result.Count);
							result.Add(typeWithAnnotations3);
						}
						else
						{
							result[num] = ConstraintsHelper.ConstraintWithMostSignificantNullability(result[num], typeWithAnnotations3);
						}
					}
				}
				instance.Free();
			}
		}

		// Token: 0x060067BA RID: 26554 RVA: 0x00186BFA File Offset: 0x00184DFA
		internal ImmutableArray<TypeParameterSymbol> SubstituteTypeParameters(ImmutableArray<TypeParameterSymbol> original)
		{
			return original.SelectAsArray((TypeParameterSymbol tp, AbstractTypeMap m) => (TypeParameterSymbol)m.SubstituteTypeParameter(tp).AsTypeSymbolOnly(), this);
		}

		// Token: 0x060067BB RID: 26555 RVA: 0x00186C24 File Offset: 0x00184E24
		internal ImmutableArray<NamedTypeSymbol> SubstituteNamedTypes(ImmutableArray<NamedTypeSymbol> original)
		{
			NamedTypeSymbol[] array = null;
			for (int i = 0; i < original.Length; i++)
			{
				NamedTypeSymbol namedTypeSymbol = original[i];
				NamedTypeSymbol namedTypeSymbol2 = this.SubstituteNamedType(namedTypeSymbol);
				if (namedTypeSymbol2 != namedTypeSymbol && array == null)
				{
					array = new NamedTypeSymbol[original.Length];
					for (int j = 0; j < i; j++)
					{
						array[j] = original[j];
					}
				}
				if (array != null)
				{
					array[i] = namedTypeSymbol2;
				}
			}
			if (array == null)
			{
				return original;
			}
			return array.AsImmutableOrNull<NamedTypeSymbol>();
		}

		// Token: 0x060067BD RID: 26557 RVA: 0x00186CA4 File Offset: 0x00184EA4
		[CompilerGenerated]
		private TypeWithAnnotations <SubstituteConstraintTypesDistinctWithoutModifiers>g__substituteConstraintType|12_0(TypeWithAnnotations type, ref AbstractTypeMap.<>c__DisplayClass12_0 A_2)
		{
			if (A_2.dynamicEraser == null)
			{
				A_2.dynamicEraser = new DynamicTypeEraser(A_2.owner.ContainingAssembly.CorLibrary.GetSpecialType(SpecialType.System_Object));
			}
			TypeWithAnnotations typeWithAnnotations = this.SubstituteType(type);
			return typeWithAnnotations.WithTypeAndModifiers(A_2.dynamicEraser.EraseDynamic(typeWithAnnotations.Type), typeWithAnnotations.CustomModifiers);
		}
	}
}
