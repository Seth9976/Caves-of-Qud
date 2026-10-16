using System;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Symbols.Metadata.PE;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200051A RID: 1306
	internal static class CustomModifierUtils
	{
		// Token: 0x06006DAF RID: 28079 RVA: 0x00193654 File Offset: 0x00191854
		internal static void CopyMethodCustomModifiers(MethodSymbol sourceMethod, MethodSymbol destinationMethod, out TypeWithAnnotations returnType, out ImmutableArray<CustomModifier> customModifiers, out ImmutableArray<ParameterSymbol> parameters, bool alsoCopyParamsModifier)
		{
			MethodSymbol methodSymbol = sourceMethod.ConstructIfGeneric(destinationMethod.TypeArgumentsWithAnnotations);
			customModifiers = ((destinationMethod.RefKind != RefKind.None) ? methodSymbol.RefCustomModifiers : ImmutableArray<CustomModifier>.Empty);
			parameters = CustomModifierUtils.CopyParameterCustomModifiers(methodSymbol.Parameters, destinationMethod.Parameters, alsoCopyParamsModifier);
			returnType = destinationMethod.ReturnTypeWithAnnotations;
			TypeSymbol type = returnType.Type;
			TypeWithAnnotations returnTypeWithAnnotations = methodSymbol.ReturnTypeWithAnnotations;
			TypeSymbol type2 = returnTypeWithAnnotations.Type;
			if (type.Equals(type2, TypeCompareKind.AllIgnoreOptions))
			{
				returnType = returnType.WithTypeAndModifiers(CustomModifierUtils.CopyTypeCustomModifiers(type2, type, destinationMethod.ContainingAssembly), returnTypeWithAnnotations.CustomModifiers);
			}
		}

		// Token: 0x06006DB0 RID: 28080 RVA: 0x001936F0 File Offset: 0x001918F0
		internal static TypeSymbol CopyTypeCustomModifiers(TypeSymbol sourceType, TypeSymbol destinationType, AssemblySymbol containingAssembly)
		{
			ImmutableArray<bool> immutableArray = CSharpCompilation.DynamicTransformsEncoder.EncodeWithoutCustomModifierFlags(destinationType, RefKind.None);
			TypeSymbol typeSymbol = DynamicTypeDecoder.TransformTypeWithoutCustomModifierFlags(sourceType, containingAssembly, RefKind.None, immutableArray, true);
			ArrayBuilder<bool> instance = ArrayBuilder<bool>.GetInstance();
			CSharpCompilation.NativeIntegerTransformsEncoder.Encode(instance, destinationType);
			typeSymbol = NativeIntegerTypeDecoder.TransformType(typeSymbol, instance.ToImmutableAndFree());
			if (destinationType.ContainsTuple() && !sourceType.Equals(destinationType, TypeCompareKind.IgnoreCustomModifiersAndArraySizesAndLowerBounds | TypeCompareKind.IgnoreDynamic | TypeCompareKind.IgnoreNullableModifiersForReferenceTypes))
			{
				ImmutableArray<string> immutableArray2 = CSharpCompilation.TupleNamesEncoder.Encode(destinationType);
				typeSymbol = TupleTypeDecoder.DecodeTupleTypesIfApplicable(typeSymbol, immutableArray2);
			}
			ArrayBuilder<byte> instance2 = ArrayBuilder<byte>.GetInstance();
			destinationType.AddNullableTransforms(instance2);
			int num = 0;
			int count = instance2.Count;
			typeSymbol.ApplyNullableTransforms(0, instance2.ToImmutableAndFree(), ref num, out typeSymbol);
			return typeSymbol;
		}

		// Token: 0x06006DB1 RID: 28081 RVA: 0x00193778 File Offset: 0x00191978
		internal static ImmutableArray<ParameterSymbol> CopyParameterCustomModifiers(ImmutableArray<ParameterSymbol> sourceParameters, ImmutableArray<ParameterSymbol> destinationParameters, bool alsoCopyParamsModifier)
		{
			ArrayBuilder<ParameterSymbol> arrayBuilder = null;
			int length = destinationParameters.Length;
			for (int i = 0; i < length; i++)
			{
				SourceParameterSymbolBase sourceParameterSymbolBase = (SourceParameterSymbolBase)destinationParameters[i];
				ParameterSymbol parameterSymbol = sourceParameters[i];
				if (parameterSymbol.TypeWithAnnotations.CustomModifiers.Any<CustomModifier>() || parameterSymbol.RefCustomModifiers.Any<CustomModifier>() || parameterSymbol.Type.HasCustomModifiers(true) || sourceParameterSymbolBase.TypeWithAnnotations.CustomModifiers.Any<CustomModifier>() || sourceParameterSymbolBase.RefCustomModifiers.Any<CustomModifier>() || sourceParameterSymbolBase.Type.HasCustomModifiers(true) || (alsoCopyParamsModifier && parameterSymbol.IsParams != sourceParameterSymbolBase.IsParams))
				{
					if (arrayBuilder == null)
					{
						arrayBuilder = ArrayBuilder<ParameterSymbol>.GetInstance();
						arrayBuilder.AddRange(destinationParameters, i);
					}
					bool flag = (alsoCopyParamsModifier ? parameterSymbol.IsParams : sourceParameterSymbolBase.IsParams);
					arrayBuilder.Add(sourceParameterSymbolBase.WithCustomModifiersAndParams(parameterSymbol.Type, parameterSymbol.TypeWithAnnotations.CustomModifiers, (sourceParameterSymbolBase.RefKind != RefKind.None) ? parameterSymbol.RefCustomModifiers : ImmutableArray<CustomModifier>.Empty, flag));
				}
				else if (arrayBuilder != null)
				{
					arrayBuilder.Add(sourceParameterSymbolBase);
				}
			}
			if (arrayBuilder != null)
			{
				return arrayBuilder.ToImmutableAndFree();
			}
			return destinationParameters;
		}

		// Token: 0x06006DB2 RID: 28082 RVA: 0x001938A7 File Offset: 0x00191AA7
		internal static bool HasInAttributeModifier(this ImmutableArray<CustomModifier> modifiers)
		{
			return modifiers.Any<CustomModifier>((CustomModifier modifier) => !modifier.IsOptional && ((CSharpCustomModifier)modifier).ModifierSymbol.IsWellKnownTypeInAttribute());
		}

		// Token: 0x06006DB3 RID: 28083 RVA: 0x001938CE File Offset: 0x00191ACE
		internal static bool HasIsExternalInitModifier(this ImmutableArray<CustomModifier> modifiers)
		{
			return modifiers.Any<CustomModifier>((CustomModifier modifier) => !modifier.IsOptional && ((CSharpCustomModifier)modifier).ModifierSymbol.IsWellKnownTypeIsExternalInit());
		}

		// Token: 0x06006DB4 RID: 28084 RVA: 0x001938F5 File Offset: 0x00191AF5
		internal static bool HasOutAttributeModifier(this ImmutableArray<CustomModifier> modifiers)
		{
			return modifiers.Any<CustomModifier>((CustomModifier modifier) => !modifier.IsOptional && ((CSharpCustomModifier)modifier).ModifierSymbol.IsWellKnownTypeOutAttribute());
		}
	}
}
