using System;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000228 RID: 552
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal static class TypeUnification
	{
		// Token: 0x0600283A RID: 10298 RVA: 0x000DB1C4 File Offset: 0x000D93C4
		public static bool CanUnify(TypeSymbol t1, TypeSymbol t2)
		{
			if (TypeSymbol.Equals(t1, t2, TypeCompareKind.CLRSignatureCompareOptions))
			{
				return true;
			}
			MutableTypeMap mutableTypeMap = null;
			return TypeUnification.CanUnifyHelper(t1, t2, ref mutableTypeMap);
		}

		// Token: 0x0600283B RID: 10299 RVA: 0x000DB1EC File Offset: 0x000D93EC
		private static bool CanUnifyHelper(TypeSymbol t1, TypeSymbol t2, [global::System.Runtime.CompilerServices.Nullable(2)] ref MutableTypeMap substitution)
		{
			return TypeUnification.CanUnifyHelper(TypeWithAnnotations.Create(t1, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), TypeWithAnnotations.Create(t2, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), ref substitution);
		}

		// Token: 0x0600283C RID: 10300 RVA: 0x000DB220 File Offset: 0x000D9420
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private static bool CanUnifyHelper(TypeWithAnnotations t1, TypeWithAnnotations t2, ref MutableTypeMap substitution)
		{
			if (!t1.HasType || !t2.HasType)
			{
				return t1.IsSameAs(t2);
			}
			if (substitution != null)
			{
				t1 = t1.SubstituteType(substitution);
				t2 = t2.SubstituteType(substitution);
			}
			if (TypeSymbol.Equals(t1.Type, t2.Type, TypeCompareKind.CLRSignatureCompareOptions) && t1.CustomModifiers.SequenceEqual<CustomModifier, CustomModifier>(t2.CustomModifiers, null))
			{
				return true;
			}
			if (!t1.Type.IsTypeParameter() && t2.Type.IsTypeParameter())
			{
				TypeWithAnnotations typeWithAnnotations = t1;
				t1 = t2;
				t2 = typeWithAnnotations;
			}
			SymbolKind kind = t1.Type.Kind;
			if (kind <= SymbolKind.ErrorType)
			{
				if (kind != SymbolKind.ArrayType)
				{
					if (kind != SymbolKind.ErrorType)
					{
						return false;
					}
				}
				else
				{
					if (t2.TypeKind != t1.TypeKind || !t2.CustomModifiers.SequenceEqual<CustomModifier, CustomModifier>(t1.CustomModifiers, null))
					{
						return false;
					}
					ArrayTypeSymbol arrayTypeSymbol = (ArrayTypeSymbol)t1.Type;
					ArrayTypeSymbol arrayTypeSymbol2 = (ArrayTypeSymbol)t2.Type;
					return arrayTypeSymbol.HasSameShapeAs(arrayTypeSymbol2) && TypeUnification.CanUnifyHelper(arrayTypeSymbol.ElementTypeWithAnnotations, arrayTypeSymbol2.ElementTypeWithAnnotations, ref substitution);
				}
			}
			else if (kind != SymbolKind.NamedType)
			{
				if (kind != SymbolKind.PointerType)
				{
					if (kind != SymbolKind.TypeParameter)
					{
						return false;
					}
					if (t2.Type.IsPointerOrFunctionPointer() || t2.IsVoidType())
					{
						return false;
					}
					TypeParameterSymbol typeParameterSymbol = (TypeParameterSymbol)t1.Type;
					if (TypeUnification.Contains(t2.Type, typeParameterSymbol))
					{
						return false;
					}
					if (t1.CustomModifiers.IsDefaultOrEmpty)
					{
						TypeUnification.AddSubstitution(ref substitution, typeParameterSymbol, t2);
						return true;
					}
					if (t1.CustomModifiers.SequenceEqual<CustomModifier, CustomModifier>(t2.CustomModifiers, null))
					{
						TypeUnification.AddSubstitution(ref substitution, typeParameterSymbol, TypeWithAnnotations.Create(t2.Type, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)));
						return true;
					}
					if (t1.CustomModifiers.Length < t2.CustomModifiers.Length && t1.CustomModifiers.SequenceEqual<CustomModifier, CustomModifier>(Enumerable.Take<CustomModifier>(t2.CustomModifiers, t1.CustomModifiers.Length), null))
					{
						TypeUnification.AddSubstitution(ref substitution, typeParameterSymbol, TypeWithAnnotations.Create(t2.Type, NullableAnnotation.Oblivious, ImmutableArray.Create<CustomModifier>(t2.CustomModifiers, t1.CustomModifiers.Length, t2.CustomModifiers.Length - t1.CustomModifiers.Length)));
						return true;
					}
					if (t2.Type.IsTypeParameter())
					{
						TypeParameterSymbol typeParameterSymbol2 = (TypeParameterSymbol)t2.Type;
						if (t2.CustomModifiers.IsDefaultOrEmpty)
						{
							TypeUnification.AddSubstitution(ref substitution, typeParameterSymbol2, t1);
							return true;
						}
						if (t2.CustomModifiers.Length < t1.CustomModifiers.Length && t2.CustomModifiers.SequenceEqual<CustomModifier, CustomModifier>(Enumerable.Take<CustomModifier>(t1.CustomModifiers, t2.CustomModifiers.Length), null))
						{
							TypeUnification.AddSubstitution(ref substitution, typeParameterSymbol2, TypeWithAnnotations.Create(t1.Type, NullableAnnotation.Oblivious, ImmutableArray.Create<CustomModifier>(t1.CustomModifiers, t2.CustomModifiers.Length, t1.CustomModifiers.Length - t2.CustomModifiers.Length)));
							return true;
						}
					}
					return false;
				}
				else
				{
					if (t2.TypeKind != t1.TypeKind || !t2.CustomModifiers.SequenceEqual<CustomModifier, CustomModifier>(t1.CustomModifiers, null))
					{
						return false;
					}
					PointerTypeSymbol pointerTypeSymbol = (PointerTypeSymbol)t1.Type;
					PointerTypeSymbol pointerTypeSymbol2 = (PointerTypeSymbol)t2.Type;
					return TypeUnification.CanUnifyHelper(pointerTypeSymbol.PointedAtTypeWithAnnotations, pointerTypeSymbol2.PointedAtTypeWithAnnotations, ref substitution);
				}
			}
			if (t2.TypeKind != t1.TypeKind || !t2.CustomModifiers.SequenceEqual<CustomModifier, CustomModifier>(t1.CustomModifiers, null))
			{
				return false;
			}
			NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)t1.Type;
			NamedTypeSymbol namedTypeSymbol2 = (NamedTypeSymbol)t2.Type;
			if (!namedTypeSymbol.IsGenericType || !namedTypeSymbol2.IsGenericType)
			{
				return false;
			}
			int arity = namedTypeSymbol.Arity;
			if (namedTypeSymbol2.Arity != arity || !TypeSymbol.Equals(namedTypeSymbol2.OriginalDefinition, namedTypeSymbol.OriginalDefinition, TypeCompareKind.ConsiderEverything))
			{
				return false;
			}
			ImmutableArray<TypeWithAnnotations> typeArgumentsWithAnnotationsNoUseSiteDiagnostics = namedTypeSymbol.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics;
			ImmutableArray<TypeWithAnnotations> typeArgumentsWithAnnotationsNoUseSiteDiagnostics2 = namedTypeSymbol2.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics;
			for (int i = 0; i < arity; i++)
			{
				if (!TypeUnification.CanUnifyHelper(typeArgumentsWithAnnotationsNoUseSiteDiagnostics[i], typeArgumentsWithAnnotationsNoUseSiteDiagnostics2[i], ref substitution))
				{
					return false;
				}
			}
			return namedTypeSymbol.ContainingType == null || TypeUnification.CanUnifyHelper(namedTypeSymbol.ContainingType, namedTypeSymbol2.ContainingType, ref substitution);
		}

		// Token: 0x0600283D RID: 10301 RVA: 0x000DB6A2 File Offset: 0x000D98A2
		private static void AddSubstitution([global::System.Runtime.CompilerServices.Nullable(2)] ref MutableTypeMap substitution, TypeParameterSymbol tp1, TypeWithAnnotations t2)
		{
			if (substitution == null)
			{
				substitution = new MutableTypeMap();
			}
			substitution.Add(tp1, t2);
		}

		// Token: 0x0600283E RID: 10302 RVA: 0x000DB6B8 File Offset: 0x000D98B8
		private static bool Contains(TypeSymbol type, TypeParameterSymbol typeParam)
		{
			SymbolKind kind = type.Kind;
			if (kind <= SymbolKind.ErrorType)
			{
				if (kind == SymbolKind.ArrayType)
				{
					return TypeUnification.Contains(((ArrayTypeSymbol)type).ElementType, typeParam);
				}
				if (kind != SymbolKind.ErrorType)
				{
					return false;
				}
			}
			else if (kind != SymbolKind.NamedType)
			{
				if (kind == SymbolKind.PointerType)
				{
					return TypeUnification.Contains(((PointerTypeSymbol)type).PointedAtType, typeParam);
				}
				if (kind != SymbolKind.TypeParameter)
				{
					return false;
				}
				return TypeSymbol.Equals(type, typeParam, TypeCompareKind.ConsiderEverything);
			}
			for (NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)type; namedTypeSymbol != null; namedTypeSymbol = namedTypeSymbol.ContainingType)
			{
				foreach (TypeWithAnnotations typeWithAnnotations in (namedTypeSymbol.IsTupleType ? namedTypeSymbol.TupleElementTypesWithAnnotations : namedTypeSymbol.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics))
				{
					if (TypeUnification.Contains(typeWithAnnotations.Type, typeParam))
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
