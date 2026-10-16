using System;
using System.Collections.Immutable;
using System.Reflection.Metadata;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.Metadata.PE
{
	// Token: 0x020005E2 RID: 1506
	internal sealed class MemberRefMetadataDecoder : MetadataDecoder
	{
		// Token: 0x06007DAF RID: 32175 RVA: 0x001C3D87 File Offset: 0x001C1F87
		public MemberRefMetadataDecoder(PEModuleSymbol moduleSymbol, Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol containingType)
			: base(moduleSymbol, containingType as PENamedTypeSymbol)
		{
			this._containingType = containingType;
		}

		// Token: 0x06007DB0 RID: 32176 RVA: 0x001C3D9D File Offset: 0x001C1F9D
		protected override Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol GetGenericMethodTypeParamSymbol(int position)
		{
			return IndexedTypeParameterSymbol.GetTypeParameter(position);
		}

		// Token: 0x06007DB1 RID: 32177 RVA: 0x001C3DA8 File Offset: 0x001C1FA8
		protected override Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol GetGenericTypeParamSymbol(int position)
		{
			PENamedTypeSymbol penamedTypeSymbol = this._containingType as PENamedTypeSymbol;
			if (penamedTypeSymbol != null)
			{
				while (penamedTypeSymbol != null && penamedTypeSymbol.MetadataArity - penamedTypeSymbol.Arity > position)
				{
					penamedTypeSymbol = penamedTypeSymbol.ContainingSymbol as PENamedTypeSymbol;
				}
				if (penamedTypeSymbol == null || penamedTypeSymbol.MetadataArity <= position)
				{
					return new UnsupportedMetadataTypeSymbol(null);
				}
				position -= penamedTypeSymbol.MetadataArity - penamedTypeSymbol.Arity;
				return penamedTypeSymbol.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics[position].Type;
			}
			else
			{
				NamedTypeSymbol namedTypeSymbol = this._containingType as NamedTypeSymbol;
				if (namedTypeSymbol == null)
				{
					return new UnsupportedMetadataTypeSymbol(null);
				}
				int num;
				Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol typeSymbol;
				MemberRefMetadataDecoder.GetGenericTypeArgumentSymbol(position, namedTypeSymbol, out num, out typeSymbol);
				if (typeSymbol != null)
				{
					return typeSymbol;
				}
				return new UnsupportedMetadataTypeSymbol(null);
			}
		}

		// Token: 0x06007DB2 RID: 32178 RVA: 0x001C3E50 File Offset: 0x001C2050
		private static void GetGenericTypeArgumentSymbol(int position, NamedTypeSymbol namedType, out int cumulativeArity, out Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol typeArgument)
		{
			cumulativeArity = namedType.Arity;
			typeArgument = null;
			int num = 0;
			NamedTypeSymbol containingType = namedType.ContainingType;
			if (containingType != null)
			{
				int num2;
				MemberRefMetadataDecoder.GetGenericTypeArgumentSymbol(position, containingType, out num2, out typeArgument);
				cumulativeArity += num2;
				num = num2;
			}
			if (num <= position && position < cumulativeArity)
			{
				typeArgument = namedType.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics[position - num].Type;
			}
		}

		// Token: 0x06007DB3 RID: 32179 RVA: 0x001C3EAC File Offset: 0x001C20AC
		internal Microsoft.CodeAnalysis.CSharp.Symbol FindMember(Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol targetTypeSymbol, MemberReferenceHandle memberRef, bool methodsOnly)
		{
			if (targetTypeSymbol == null)
			{
				return null;
			}
			Microsoft.CodeAnalysis.CSharp.Symbol symbol;
			try
			{
				string memberRefNameOrThrow = this.Module.GetMemberRefNameOrThrow(memberRef);
				BlobHandle signatureOrThrow = this.Module.GetSignatureOrThrow(memberRef);
				SignatureHeader signatureHeader;
				BlobReader blobReader = base.DecodeSignatureHeaderOrThrow(signatureOrThrow, out signatureHeader);
				int num = (int)(signatureHeader.RawValue & 15);
				if (num != 0 && num != 5)
				{
					if (num != 6)
					{
						symbol = null;
					}
					else if (methodsOnly)
					{
						symbol = null;
					}
					else
					{
						ImmutableArray<ModifierInfo<Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol>> immutableArray;
						Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol typeSymbol = base.DecodeFieldSignature(ref blobReader, out immutableArray);
						symbol = MemberRefMetadataDecoder.FindFieldBySignature(targetTypeSymbol, memberRefNameOrThrow, immutableArray, typeSymbol);
					}
				}
				else
				{
					int num2;
					ParamInfo<Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol>[] array = base.DecodeSignatureParametersOrThrow(ref blobReader, signatureHeader, out num2, true, false);
					symbol = MemberRefMetadataDecoder.FindMethodBySignature(targetTypeSymbol, memberRefNameOrThrow, signatureHeader, num2, array);
				}
			}
			catch (BadImageFormatException)
			{
				symbol = null;
			}
			return symbol;
		}

		// Token: 0x06007DB4 RID: 32180 RVA: 0x001C3F60 File Offset: 0x001C2160
		private static Microsoft.CodeAnalysis.CSharp.Symbols.FieldSymbol FindFieldBySignature(Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol targetTypeSymbol, string targetMemberName, ImmutableArray<ModifierInfo<Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol>> customModifiers, Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol type)
		{
			foreach (Microsoft.CodeAnalysis.CSharp.Symbol symbol in targetTypeSymbol.GetMembers(targetMemberName))
			{
				Microsoft.CodeAnalysis.CSharp.Symbols.FieldSymbol fieldSymbol = symbol as Microsoft.CodeAnalysis.CSharp.Symbols.FieldSymbol;
				if (fieldSymbol != null)
				{
					TypeWithAnnotations typeWithAnnotations2;
					TypeWithAnnotations typeWithAnnotations = (typeWithAnnotations2 = fieldSymbol.TypeWithAnnotations);
					if (Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol.Equals(typeWithAnnotations2.Type, type, TypeCompareKind.CLRSignatureCompareOptions) && MemberRefMetadataDecoder.CustomModifiersMatch(typeWithAnnotations.CustomModifiers, customModifiers))
					{
						return fieldSymbol;
					}
				}
			}
			return null;
		}

		// Token: 0x06007DB5 RID: 32181 RVA: 0x001C3FC8 File Offset: 0x001C21C8
		private static Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol FindMethodBySignature(Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol targetTypeSymbol, string targetMemberName, SignatureHeader targetMemberSignatureHeader, int targetMemberTypeParamCount, ParamInfo<Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol>[] targetParamInfo)
		{
			foreach (Microsoft.CodeAnalysis.CSharp.Symbol symbol in targetTypeSymbol.GetMembers(targetMemberName))
			{
				Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol methodSymbol = symbol as Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol;
				if (methodSymbol != null && (byte)methodSymbol.CallingConvention == targetMemberSignatureHeader.RawValue && targetMemberTypeParamCount == methodSymbol.Arity && MemberRefMetadataDecoder.MethodSymbolMatchesParamInfo(methodSymbol, targetParamInfo))
				{
					return methodSymbol;
				}
			}
			return null;
		}

		// Token: 0x06007DB6 RID: 32182 RVA: 0x001C4028 File Offset: 0x001C2228
		private static bool MethodSymbolMatchesParamInfo(Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol candidateMethod, ParamInfo<Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol>[] targetParamInfo)
		{
			int num = targetParamInfo.Length - 1;
			if (candidateMethod.ParameterCount != num)
			{
				return false;
			}
			TypeMap typeMap = new TypeMap(candidateMethod.TypeParameters, IndexedTypeParameterSymbol.Take(candidateMethod.Arity), true);
			if (!MemberRefMetadataDecoder.ReturnTypesMatch(candidateMethod, typeMap, ref targetParamInfo[0]))
			{
				return false;
			}
			for (int i = 0; i < num; i++)
			{
				if (!MemberRefMetadataDecoder.ParametersMatch(candidateMethod.Parameters[i], typeMap, ref targetParamInfo[i + 1]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06007DB7 RID: 32183 RVA: 0x001C40A0 File Offset: 0x001C22A0
		private static bool ParametersMatch(ParameterSymbol candidateParam, TypeMap candidateMethodTypeMap, ref ParamInfo<Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol> targetParam)
		{
			if (candidateParam.RefKind > RefKind.None != targetParam.IsByRef)
			{
				return false;
			}
			TypeWithAnnotations typeWithAnnotations = candidateParam.TypeWithAnnotations.SubstituteType(candidateMethodTypeMap);
			return Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol.Equals(typeWithAnnotations.Type, targetParam.Type, TypeCompareKind.CLRSignatureCompareOptions) && MemberRefMetadataDecoder.CustomModifiersMatch(typeWithAnnotations.CustomModifiers, targetParam.CustomModifiers) && MemberRefMetadataDecoder.CustomModifiersMatch(candidateMethodTypeMap.SubstituteCustomModifiers(candidateParam.RefCustomModifiers), targetParam.RefCustomModifiers);
		}

		// Token: 0x06007DB8 RID: 32184 RVA: 0x001C4118 File Offset: 0x001C2318
		private static bool ReturnTypesMatch(Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol candidateMethod, TypeMap candidateMethodTypeMap, ref ParamInfo<Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol> targetReturnParam)
		{
			if (candidateMethod.ReturnsByRef != targetReturnParam.IsByRef)
			{
				return false;
			}
			TypeWithAnnotations returnTypeWithAnnotations = candidateMethod.ReturnTypeWithAnnotations;
			Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol type = targetReturnParam.Type;
			TypeWithAnnotations typeWithAnnotations = returnTypeWithAnnotations.SubstituteType(candidateMethodTypeMap);
			return Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol.Equals(typeWithAnnotations.Type, type, TypeCompareKind.CLRSignatureCompareOptions) && MemberRefMetadataDecoder.CustomModifiersMatch(typeWithAnnotations.CustomModifiers, targetReturnParam.CustomModifiers) && MemberRefMetadataDecoder.CustomModifiersMatch(candidateMethodTypeMap.SubstituteCustomModifiers(candidateMethod.RefCustomModifiers), targetReturnParam.RefCustomModifiers);
		}

		// Token: 0x06007DB9 RID: 32185 RVA: 0x001C4190 File Offset: 0x001C2390
		private static bool CustomModifiersMatch(ImmutableArray<CustomModifier> candidateCustomModifiers, ImmutableArray<ModifierInfo<Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol>> targetCustomModifiers)
		{
			if (targetCustomModifiers.IsDefault || targetCustomModifiers.IsEmpty)
			{
				return candidateCustomModifiers.IsDefault || candidateCustomModifiers.IsEmpty;
			}
			if (candidateCustomModifiers.IsDefault)
			{
				return false;
			}
			int length = candidateCustomModifiers.Length;
			if (targetCustomModifiers.Length != length)
			{
				return false;
			}
			for (int i = 0; i < length; i++)
			{
				ModifierInfo<Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol> modifierInfo = targetCustomModifiers[i];
				CustomModifier customModifier = candidateCustomModifiers[i];
				if (modifierInfo.IsOptional != customModifier.IsOptional || !object.Equals(modifierInfo.Modifier, ((CSharpCustomModifier)customModifier).ModifierSymbol))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04001DA1 RID: 7585
		private readonly Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol _containingType;
	}
}
