using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.Cci;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004EE RID: 1262
	internal sealed class MemberSignatureComparer : IEqualityComparer<Symbol>
	{
		// Token: 0x06006AB6 RID: 27318 RVA: 0x0018C8B8 File Offset: 0x0018AAB8
		private MemberSignatureComparer(bool considerName, bool considerExplicitlyImplementedInterfaces, bool considerReturnType, bool considerTypeConstraints, bool considerCallingConvention, bool considerRefKindDifferences, TypeCompareKind typeComparison = TypeCompareKind.IgnoreDynamic | TypeCompareKind.IgnoreNativeIntegers)
		{
			this._considerName = considerName;
			this._considerExplicitlyImplementedInterfaces = considerExplicitlyImplementedInterfaces;
			this._considerReturnType = considerReturnType;
			this._considerTypeConstraints = considerTypeConstraints;
			this._considerCallingConvention = considerCallingConvention;
			this._considerRefKindDifferences = considerRefKindDifferences;
			this._typeComparison = typeComparison;
			if (!considerRefKindDifferences)
			{
				this._typeComparison |= TypeCompareKind.FunctionPointerRefMatchesOutInRefReadonly;
			}
		}

		// Token: 0x06006AB7 RID: 27319 RVA: 0x0018C914 File Offset: 0x0018AB14
		public bool Equals(Symbol member1, Symbol member2)
		{
			if (member1 == member2)
			{
				return true;
			}
			if (member1 == null || member2 == null || member1.Kind != member2.Kind)
			{
				return false;
			}
			bool flag = false;
			bool flag2 = false;
			if (this._considerName)
			{
				string memberNameWithoutInterfaceName = ExplicitInterfaceHelpers.GetMemberNameWithoutInterfaceName(member1.Name);
				string memberNameWithoutInterfaceName2 = ExplicitInterfaceHelpers.GetMemberNameWithoutInterfaceName(member2.Name);
				flag = memberNameWithoutInterfaceName != member1.Name;
				flag2 = memberNameWithoutInterfaceName2 != member2.Name;
				if (memberNameWithoutInterfaceName != memberNameWithoutInterfaceName2)
				{
					return false;
				}
			}
			if (member1.GetMemberArity() != member2.GetMemberArity() || member1.GetParameterCount() != member2.GetParameterCount())
			{
				return false;
			}
			TypeMap typeMap = MemberSignatureComparer.GetTypeMap(member1);
			TypeMap typeMap2 = MemberSignatureComparer.GetTypeMap(member2);
			if (this._considerReturnType && !MemberSignatureComparer.HaveSameReturnTypes(member1, typeMap, member2, typeMap2, this._typeComparison))
			{
				return false;
			}
			if (member1.GetParameterCount() > 0 && !MemberSignatureComparer.HaveSameParameterTypes(member1.GetParameters(), typeMap, member2.GetParameters(), typeMap2, this._considerRefKindDifferences, this._typeComparison))
			{
				return false;
			}
			if (this._considerCallingConvention)
			{
				if (MemberSignatureComparer.GetCallingConvention(member1) != MemberSignatureComparer.GetCallingConvention(member2))
				{
					return false;
				}
			}
			else if (MemberSignatureComparer.IsVarargMethod(member1) != MemberSignatureComparer.IsVarargMethod(member2))
			{
				return false;
			}
			if (this._considerExplicitlyImplementedInterfaces)
			{
				if (flag != flag2)
				{
					return false;
				}
				if (flag)
				{
					if (member1.IsExplicitInterfaceImplementation() != member2.IsExplicitInterfaceImplementation())
					{
						return false;
					}
					ImmutableArray<Symbol> explicitInterfaceImplementations = member1.GetExplicitInterfaceImplementations();
					ImmutableArray<Symbol> explicitInterfaceImplementations2 = member2.GetExplicitInterfaceImplementations();
					if (!explicitInterfaceImplementations.SetEquals<Symbol>(explicitInterfaceImplementations2, SymbolEqualityComparer.ConsiderEverything))
					{
						return false;
					}
				}
			}
			return !this._considerTypeConstraints || MemberSignatureComparer.HaveSameConstraints(member1, typeMap, member2, typeMap2);
		}

		// Token: 0x06006AB8 RID: 27320 RVA: 0x0018CA74 File Offset: 0x0018AC74
		public int GetHashCode(Symbol member)
		{
			int num = 1;
			if (member != null)
			{
				num = Hash.Combine((int)member.Kind, num);
				if (this._considerName)
				{
					num = Hash.Combine<string>(ExplicitInterfaceHelpers.GetMemberNameWithoutInterfaceName(member.Name), num);
				}
				if (this._considerReturnType && member.GetMemberArity() == 0 && (this._typeComparison & TypeCompareKind.AllIgnoreOptions) == TypeCompareKind.ConsiderEverything)
				{
					num = Hash.Combine(member.GetTypeOrReturnType().GetHashCode(), num);
				}
				if (member.Kind != SymbolKind.Field)
				{
					num = Hash.Combine(member.GetMemberArity(), num);
					num = Hash.Combine(member.GetParameterCount(), num);
				}
			}
			return num;
		}

		// Token: 0x06006AB9 RID: 27321 RVA: 0x0018CB0C File Offset: 0x0018AD0C
		private static bool HaveSameReturnTypes(Symbol member1, TypeMap typeMap1, Symbol member2, TypeMap typeMap2, TypeCompareKind typeComparison)
		{
			RefKind refKind;
			TypeWithAnnotations typeWithAnnotations;
			ImmutableArray<CustomModifier> immutableArray;
			member1.GetTypeOrReturnType(out refKind, out typeWithAnnotations, out immutableArray);
			RefKind refKind2;
			TypeWithAnnotations typeWithAnnotations2;
			ImmutableArray<CustomModifier> immutableArray2;
			member2.GetTypeOrReturnType(out refKind2, out typeWithAnnotations2, out immutableArray2);
			if (refKind != refKind2)
			{
				return false;
			}
			bool flag = typeWithAnnotations.IsVoidType();
			bool flag2 = typeWithAnnotations2.IsVoidType();
			if (flag != flag2)
			{
				return false;
			}
			if (flag && ((typeComparison & TypeCompareKind.IgnoreCustomModifiersAndArraySizesAndLowerBounds) != TypeCompareKind.ConsiderEverything || (typeWithAnnotations.CustomModifiers.IsEmpty && typeWithAnnotations2.CustomModifiers.IsEmpty)))
			{
				return true;
			}
			TypeWithAnnotations typeWithAnnotations3 = MemberSignatureComparer.SubstituteType(typeMap1, typeWithAnnotations);
			TypeWithAnnotations typeWithAnnotations4 = MemberSignatureComparer.SubstituteType(typeMap2, typeWithAnnotations2);
			return typeWithAnnotations3.Equals(typeWithAnnotations4, typeComparison) && ((typeComparison & TypeCompareKind.IgnoreCustomModifiersAndArraySizesAndLowerBounds) != TypeCompareKind.ConsiderEverything || MemberSignatureComparer.HaveSameCustomModifiers(immutableArray, typeMap1, immutableArray2, typeMap2));
		}

		// Token: 0x06006ABA RID: 27322 RVA: 0x0018CBB8 File Offset: 0x0018ADB8
		private static TypeMap GetTypeMap(Symbol member)
		{
			ImmutableArray<TypeParameterSymbol> memberTypeParameters = member.GetMemberTypeParameters();
			if (!memberTypeParameters.IsEmpty)
			{
				return new TypeMap(memberTypeParameters, IndexedTypeParameterSymbol.Take(member.GetMemberArity()), true);
			}
			return null;
		}

		// Token: 0x06006ABB RID: 27323 RVA: 0x0018CBEC File Offset: 0x0018ADEC
		private static bool HaveSameConstraints(Symbol member1, TypeMap typeMap1, Symbol member2, TypeMap typeMap2)
		{
			if (member1.GetMemberArity() == 0)
			{
				return true;
			}
			ImmutableArray<TypeParameterSymbol> memberTypeParameters = member1.GetMemberTypeParameters();
			ImmutableArray<TypeParameterSymbol> memberTypeParameters2 = member2.GetMemberTypeParameters();
			return MemberSignatureComparer.HaveSameConstraints(memberTypeParameters, typeMap1, memberTypeParameters2, typeMap2);
		}

		// Token: 0x06006ABC RID: 27324 RVA: 0x0018CC18 File Offset: 0x0018AE18
		public static bool HaveSameConstraints(ImmutableArray<TypeParameterSymbol> typeParameters1, TypeMap typeMap1, ImmutableArray<TypeParameterSymbol> typeParameters2, TypeMap typeMap2)
		{
			int length = typeParameters1.Length;
			for (int i = 0; i < length; i++)
			{
				if (!MemberSignatureComparer.HaveSameConstraints(typeParameters1[i], typeMap1, typeParameters2[i], typeMap2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06006ABD RID: 27325 RVA: 0x0018CC58 File Offset: 0x0018AE58
		public static bool HaveSameConstraints(TypeParameterSymbol typeParameter1, TypeMap typeMap1, TypeParameterSymbol typeParameter2, TypeMap typeMap2)
		{
			return typeParameter1.HasConstructorConstraint == typeParameter2.HasConstructorConstraint && typeParameter1.HasReferenceTypeConstraint == typeParameter2.HasReferenceTypeConstraint && typeParameter1.HasValueTypeConstraint == typeParameter2.HasValueTypeConstraint && typeParameter1.HasUnmanagedTypeConstraint == typeParameter2.HasUnmanagedTypeConstraint && typeParameter1.Variance == typeParameter2.Variance && MemberSignatureComparer.HaveSameTypeConstraints(typeParameter1, typeMap1, typeParameter2, typeMap2, SymbolEqualityComparer.IgnoringDynamicTupleNamesAndNullability);
		}

		// Token: 0x06006ABE RID: 27326 RVA: 0x0018CCBC File Offset: 0x0018AEBC
		private static bool HaveSameTypeConstraints(TypeParameterSymbol typeParameter1, TypeMap typeMap1, TypeParameterSymbol typeParameter2, TypeMap typeMap2, IEqualityComparer<TypeSymbol> comparer)
		{
			ImmutableArray<TypeWithAnnotations> constraintTypesNoUseSiteDiagnostics = typeParameter1.ConstraintTypesNoUseSiteDiagnostics;
			ImmutableArray<TypeWithAnnotations> constraintTypesNoUseSiteDiagnostics2 = typeParameter2.ConstraintTypesNoUseSiteDiagnostics;
			if (constraintTypesNoUseSiteDiagnostics.Length == 0 && constraintTypesNoUseSiteDiagnostics2.Length == 0)
			{
				return true;
			}
			HashSet<TypeSymbol> hashSet = new HashSet<TypeSymbol>(comparer);
			HashSet<TypeSymbol> hashSet2 = new HashSet<TypeSymbol>(comparer);
			MemberSignatureComparer.SubstituteConstraintTypes(constraintTypesNoUseSiteDiagnostics, typeMap1, hashSet);
			MemberSignatureComparer.SubstituteConstraintTypes(constraintTypesNoUseSiteDiagnostics2, typeMap2, hashSet2);
			return MemberSignatureComparer.AreConstraintTypesSubset(hashSet, hashSet2, typeParameter2) && MemberSignatureComparer.AreConstraintTypesSubset(hashSet2, hashSet, typeParameter1);
		}

		// Token: 0x06006ABF RID: 27327 RVA: 0x0018CD20 File Offset: 0x0018AF20
		public static bool HaveSameNullabilityInConstraints(TypeParameterSymbol typeParameter1, TypeMap typeMap1, TypeParameterSymbol typeParameter2, TypeMap typeMap2)
		{
			if (!typeParameter1.IsValueType)
			{
				bool? isNotNullable = typeParameter1.IsNotNullable;
				bool? isNotNullable2 = typeParameter2.IsNotNullable;
				if (isNotNullable != null && isNotNullable2 != null && isNotNullable.GetValueOrDefault() != isNotNullable2.GetValueOrDefault())
				{
					return false;
				}
			}
			return MemberSignatureComparer.HaveSameTypeConstraints(typeParameter1, typeMap1, typeParameter2, typeMap2, SymbolEqualityComparer.AllIgnoreOptionsPlusNullableWithUnknownMatchesAny);
		}

		// Token: 0x06006AC0 RID: 27328 RVA: 0x0018CD78 File Offset: 0x0018AF78
		private static bool AreConstraintTypesSubset(HashSet<TypeSymbol> constraintTypes1, HashSet<TypeSymbol> constraintTypes2, TypeParameterSymbol typeParameter2)
		{
			foreach (TypeSymbol typeSymbol in constraintTypes1)
			{
				if (typeSymbol.SpecialType != SpecialType.System_Object && !constraintTypes2.Contains(typeSymbol) && (typeSymbol.SpecialType != SpecialType.System_ValueType || !typeParameter2.HasValueTypeConstraint))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06006AC1 RID: 27329 RVA: 0x0018CDEC File Offset: 0x0018AFEC
		private static void SubstituteConstraintTypes(ImmutableArray<TypeWithAnnotations> types, TypeMap typeMap, HashSet<TypeSymbol> result)
		{
			foreach (TypeWithAnnotations typeWithAnnotations in types)
			{
				result.Add(typeMap.SubstituteType(typeWithAnnotations).Type);
			}
		}

		// Token: 0x06006AC2 RID: 27330 RVA: 0x0018CE2C File Offset: 0x0018B02C
		private static bool HaveSameParameterTypes(ImmutableArray<ParameterSymbol> params1, TypeMap typeMap1, ImmutableArray<ParameterSymbol> params2, TypeMap typeMap2, bool considerRefKindDifferences, TypeCompareKind typeComparison)
		{
			int length = params1.Length;
			for (int i = 0; i < length; i++)
			{
				ParameterSymbol parameterSymbol = params1[i];
				ParameterSymbol parameterSymbol2 = params2[i];
				TypeWithAnnotations typeWithAnnotations = MemberSignatureComparer.SubstituteType(typeMap1, parameterSymbol.TypeWithAnnotations);
				TypeWithAnnotations typeWithAnnotations2 = MemberSignatureComparer.SubstituteType(typeMap2, parameterSymbol2.TypeWithAnnotations);
				if (!typeWithAnnotations.Equals(typeWithAnnotations2, typeComparison))
				{
					return false;
				}
				if ((typeComparison & TypeCompareKind.IgnoreCustomModifiersAndArraySizesAndLowerBounds) == TypeCompareKind.ConsiderEverything && !MemberSignatureComparer.HaveSameCustomModifiers(parameterSymbol.RefCustomModifiers, typeMap1, parameterSymbol2.RefCustomModifiers, typeMap2))
				{
					return false;
				}
				RefKind refKind = parameterSymbol.RefKind;
				RefKind refKind2 = parameterSymbol2.RefKind;
				if (considerRefKindDifferences)
				{
					if (refKind != refKind2)
					{
						return false;
					}
				}
				else if (refKind == RefKind.None != (refKind2 == RefKind.None))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06006AC3 RID: 27331 RVA: 0x0018CED8 File Offset: 0x0018B0D8
		private static TypeWithAnnotations SubstituteType(TypeMap typeMap, TypeWithAnnotations typeSymbol)
		{
			if (typeMap != null)
			{
				return typeSymbol.SubstituteType(typeMap);
			}
			return typeSymbol;
		}

		// Token: 0x06006AC4 RID: 27332 RVA: 0x0018CEE7 File Offset: 0x0018B0E7
		private static bool HaveSameCustomModifiers(ImmutableArray<CustomModifier> customModifiers1, TypeMap typeMap1, ImmutableArray<CustomModifier> customModifiers2, TypeMap typeMap2)
		{
			return MemberSignatureComparer.SubstituteModifiers(typeMap1, customModifiers1).SequenceEqual<CustomModifier, CustomModifier>(MemberSignatureComparer.SubstituteModifiers(typeMap2, customModifiers2), null);
		}

		// Token: 0x06006AC5 RID: 27333 RVA: 0x0018CEFD File Offset: 0x0018B0FD
		private static ImmutableArray<CustomModifier> SubstituteModifiers(TypeMap typeMap, ImmutableArray<CustomModifier> customModifiers)
		{
			if (typeMap != null)
			{
				return typeMap.SubstituteCustomModifiers(customModifiers);
			}
			return customModifiers;
		}

		// Token: 0x06006AC6 RID: 27334 RVA: 0x0018CF0C File Offset: 0x0018B10C
		private static CallingConvention GetCallingConvention(Symbol member)
		{
			SymbolKind kind = member.Kind;
			if (kind != SymbolKind.Event)
			{
				if (kind == SymbolKind.Method)
				{
					return ((MethodSymbol)member).CallingConvention;
				}
				if (kind != SymbolKind.Property)
				{
					throw ExceptionUtilities.UnexpectedValue(member.Kind);
				}
			}
			if (!member.IsStatic)
			{
				return CallingConvention.HasThis;
			}
			return CallingConvention.Default;
		}

		// Token: 0x06006AC7 RID: 27335 RVA: 0x0018CF59 File Offset: 0x0018B159
		private static bool IsVarargMethod(Symbol member)
		{
			return member.Kind == SymbolKind.Method && ((MethodSymbol)member).IsVararg;
		}

		// Token: 0x06006AC8 RID: 27336 RVA: 0x0018CF72 File Offset: 0x0018B172
		internal static bool ConsideringTupleNamesCreatesDifference(Symbol member1, Symbol member2)
		{
			return !MemberSignatureComparer.CSharpWithTupleNamesComparer.Equals(member1, member2) && MemberSignatureComparer.CSharpWithoutTupleNamesComparer.Equals(member1, member2);
		}

		// Token: 0x04001A87 RID: 6791
		public static readonly MemberSignatureComparer ExplicitImplementationComparer = new MemberSignatureComparer(false, false, true, false, true, true, TypeCompareKind.AllIgnoreOptions);

		// Token: 0x04001A88 RID: 6792
		public static readonly MemberSignatureComparer CSharpImplicitImplementationComparer = new MemberSignatureComparer(true, true, true, false, true, true, TypeCompareKind.AllIgnoreOptions);

		// Token: 0x04001A89 RID: 6793
		public static readonly MemberSignatureComparer CSharpCloseImplicitImplementationComparer = new MemberSignatureComparer(true, true, false, false, false, true, TypeCompareKind.AllIgnoreOptions);

		// Token: 0x04001A8A RID: 6794
		public static readonly MemberSignatureComparer DuplicateSourceComparer = new MemberSignatureComparer(true, true, false, false, false, false, TypeCompareKind.AllIgnoreOptions);

		// Token: 0x04001A8B RID: 6795
		public static readonly MemberSignatureComparer RecordAPISignatureComparer = new MemberSignatureComparer(true, true, false, false, false, true, TypeCompareKind.AllIgnoreOptions);

		// Token: 0x04001A8C RID: 6796
		public static readonly MemberSignatureComparer PartialMethodsComparer = new MemberSignatureComparer(true, true, false, false, false, true, TypeCompareKind.AllIgnoreOptions);

		// Token: 0x04001A8D RID: 6797
		public static readonly MemberSignatureComparer CSharpOverrideComparer = new MemberSignatureComparer(true, false, false, false, false, true, TypeCompareKind.AllIgnoreOptions);

		// Token: 0x04001A8E RID: 6798
		private static readonly MemberSignatureComparer CSharpWithTupleNamesComparer = new MemberSignatureComparer(true, false, true, false, false, false, TypeCompareKind.IgnoreCustomModifiersAndArraySizesAndLowerBounds | TypeCompareKind.IgnoreDynamic | TypeCompareKind.IgnoreNullableModifiersForReferenceTypes | TypeCompareKind.ObliviousNullableModifierMatchesAny | TypeCompareKind.IgnoreNativeIntegers);

		// Token: 0x04001A8F RID: 6799
		private static readonly MemberSignatureComparer CSharpWithoutTupleNamesComparer = new MemberSignatureComparer(true, false, true, false, false, false, TypeCompareKind.AllIgnoreOptions);

		// Token: 0x04001A90 RID: 6800
		public static readonly MemberSignatureComparer CSharpAccessorOverrideComparer = new MemberSignatureComparer(false, false, false, false, false, true, TypeCompareKind.AllIgnoreOptions);

		// Token: 0x04001A91 RID: 6801
		public static readonly MemberSignatureComparer CSharpCustomModifierOverrideComparer = new MemberSignatureComparer(true, false, true, false, false, true, TypeCompareKind.IgnoreDynamic | TypeCompareKind.IgnoreTupleNames | TypeCompareKind.IgnoreNullableModifiersForReferenceTypes | TypeCompareKind.IgnoreNativeIntegers);

		// Token: 0x04001A92 RID: 6802
		internal static readonly MemberSignatureComparer SloppyOverrideComparer = new MemberSignatureComparer(false, false, false, false, false, false, TypeCompareKind.IgnoreCustomModifiersAndArraySizesAndLowerBounds | TypeCompareKind.IgnoreDynamic | TypeCompareKind.IgnoreTupleNames | TypeCompareKind.IgnoreNullableModifiersForReferenceTypes);

		// Token: 0x04001A93 RID: 6803
		public static readonly MemberSignatureComparer RuntimeSignatureComparer = new MemberSignatureComparer(true, false, true, false, true, false, TypeCompareKind.IgnoreDynamic | TypeCompareKind.IgnoreTupleNames | TypeCompareKind.IgnoreNullableModifiersForReferenceTypes | TypeCompareKind.IgnoreNativeIntegers);

		// Token: 0x04001A94 RID: 6804
		public static readonly MemberSignatureComparer RuntimePlusRefOutSignatureComparer = new MemberSignatureComparer(true, false, true, false, true, true, TypeCompareKind.IgnoreDynamic | TypeCompareKind.IgnoreTupleNames | TypeCompareKind.IgnoreNullableModifiersForReferenceTypes | TypeCompareKind.IgnoreNativeIntegers);

		// Token: 0x04001A95 RID: 6805
		public static readonly MemberSignatureComparer RuntimeImplicitImplementationComparer = new MemberSignatureComparer(true, true, true, false, true, false, TypeCompareKind.IgnoreDynamic | TypeCompareKind.IgnoreTupleNames | TypeCompareKind.IgnoreNullableModifiersForReferenceTypes | TypeCompareKind.IgnoreNativeIntegers);

		// Token: 0x04001A96 RID: 6806
		public static readonly MemberSignatureComparer CSharpSignatureAndConstraintsAndReturnTypeComparer = new MemberSignatureComparer(true, true, true, true, true, true, TypeCompareKind.IgnoreDynamic | TypeCompareKind.IgnoreTupleNames | TypeCompareKind.IgnoreNullableModifiersForReferenceTypes | TypeCompareKind.IgnoreNativeIntegers);

		// Token: 0x04001A97 RID: 6807
		public static readonly MemberSignatureComparer RetargetedExplicitImplementationComparer = new MemberSignatureComparer(true, false, true, false, true, true, TypeCompareKind.IgnoreDynamic | TypeCompareKind.IgnoreTupleNames | TypeCompareKind.IgnoreNullableModifiersForReferenceTypes | TypeCompareKind.IgnoreNativeIntegers);

		// Token: 0x04001A98 RID: 6808
		public static readonly MemberSignatureComparer CrefComparer = new MemberSignatureComparer(false, false, false, false, false, true, TypeCompareKind.IgnoreCustomModifiersAndArraySizesAndLowerBounds | TypeCompareKind.IgnoreDynamic | TypeCompareKind.IgnoreTupleNames | TypeCompareKind.IgnoreNullableModifiersForReferenceTypes);

		// Token: 0x04001A99 RID: 6809
		internal static readonly MemberSignatureComparer MethodGroupSignatureComparer = new MemberSignatureComparer(false, false, true, false, false, true, TypeCompareKind.AllIgnoreOptions);

		// Token: 0x04001A9A RID: 6810
		private readonly bool _considerName;

		// Token: 0x04001A9B RID: 6811
		private readonly bool _considerExplicitlyImplementedInterfaces;

		// Token: 0x04001A9C RID: 6812
		private readonly bool _considerReturnType;

		// Token: 0x04001A9D RID: 6813
		private readonly bool _considerTypeConstraints;

		// Token: 0x04001A9E RID: 6814
		private readonly bool _considerCallingConvention;

		// Token: 0x04001A9F RID: 6815
		private readonly bool _considerRefKindDifferences;

		// Token: 0x04001AA0 RID: 6816
		private readonly TypeCompareKind _typeComparison;
	}
}
