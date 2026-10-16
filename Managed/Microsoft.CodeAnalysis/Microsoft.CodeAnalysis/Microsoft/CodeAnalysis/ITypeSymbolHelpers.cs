using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000253 RID: 595
	[NullableContext(2)]
	[Nullable(0)]
	internal static class ITypeSymbolHelpers
	{
		// Token: 0x0600170E RID: 5902 RVA: 0x000457DF File Offset: 0x000439DF
		internal static bool IsNullableType([NotNullWhen(true)] ITypeSymbol typeOpt)
		{
			return typeOpt != null && typeOpt.OriginalDefinition.SpecialType == SpecialType.System_Nullable_T;
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x000457F5 File Offset: 0x000439F5
		internal static bool IsNullableOfBoolean([NotNullWhen(true)] ITypeSymbol type)
		{
			return ITypeSymbolHelpers.IsNullableType(type) && ITypeSymbolHelpers.IsBooleanType(ITypeSymbolHelpers.GetNullableUnderlyingType(type));
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x0004580C File Offset: 0x00043A0C
		[NullableContext(1)]
		internal static ITypeSymbol GetNullableUnderlyingType(ITypeSymbol type)
		{
			return ((INamedTypeSymbol)type).TypeArguments[0];
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x0004582D File Offset: 0x00043A2D
		internal static bool IsBooleanType([NotNullWhen(true)] ITypeSymbol type)
		{
			return type != null && type.SpecialType == SpecialType.System_Boolean;
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x0004583D File Offset: 0x00043A3D
		internal static bool IsObjectType([NotNullWhen(true)] ITypeSymbol type)
		{
			return type != null && type.SpecialType == SpecialType.System_Object;
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x0004584D File Offset: 0x00043A4D
		internal static bool IsSignedIntegralType([NotNullWhen(true)] ITypeSymbol type)
		{
			return type != null && type.SpecialType.IsSignedIntegralType();
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x0004585F File Offset: 0x00043A5F
		internal static bool IsUnsignedIntegralType([NotNullWhen(true)] ITypeSymbol type)
		{
			return type != null && type.SpecialType.IsUnsignedIntegralType();
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x00045871 File Offset: 0x00043A71
		internal static bool IsNumericType([NotNullWhen(true)] ITypeSymbol type)
		{
			return type != null && type.SpecialType.IsNumericType();
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x00045883 File Offset: 0x00043A83
		internal static ITypeSymbol GetEnumUnderlyingType(ITypeSymbol type)
		{
			INamedTypeSymbol namedTypeSymbol = type as INamedTypeSymbol;
			if (namedTypeSymbol == null)
			{
				return null;
			}
			return namedTypeSymbol.EnumUnderlyingType;
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x00045896 File Offset: 0x00043A96
		[return: NotNullIfNotNull("type")]
		internal static ITypeSymbol GetEnumUnderlyingTypeOrSelf(ITypeSymbol type)
		{
			return ITypeSymbolHelpers.GetEnumUnderlyingType(type) ?? type;
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x000458A3 File Offset: 0x00043AA3
		internal static bool IsDynamicType([NotNullWhen(true)] ITypeSymbol type)
		{
			return type != null && type.Kind == SymbolKind.DynamicType;
		}
	}
}
