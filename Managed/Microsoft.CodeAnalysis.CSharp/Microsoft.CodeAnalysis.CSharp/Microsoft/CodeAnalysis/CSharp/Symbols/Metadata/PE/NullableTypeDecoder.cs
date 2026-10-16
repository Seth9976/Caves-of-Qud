using System;
using System.Collections.Immutable;
using System.Reflection.Metadata;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.Metadata.PE
{
	// Token: 0x020005E5 RID: 1509
	internal static class NullableTypeDecoder
	{
		// Token: 0x06007DDA RID: 32218 RVA: 0x001C4D30 File Offset: 0x001C2F30
		internal static TypeWithAnnotations TransformType(TypeWithAnnotations metadataType, EntityHandle targetSymbolToken, PEModuleSymbol containingModule, Symbol accessSymbol, Symbol nullableContext)
		{
			byte valueOrDefault;
			ImmutableArray<byte> immutableArray;
			if (!containingModule.Module.HasNullableAttribute(targetSymbolToken, out valueOrDefault, out immutableArray))
			{
				byte? nullableContextValue = nullableContext.GetNullableContextValue();
				if (nullableContextValue == null)
				{
					return metadataType;
				}
				valueOrDefault = nullableContextValue.GetValueOrDefault();
			}
			if (!containingModule.ShouldDecodeNullableAttributes(accessSymbol))
			{
				return metadataType;
			}
			return NullableTypeDecoder.TransformType(metadataType, valueOrDefault, immutableArray);
		}

		// Token: 0x06007DDB RID: 32219 RVA: 0x001C4D80 File Offset: 0x001C2F80
		internal static TypeWithAnnotations TransformType(TypeWithAnnotations metadataType, byte defaultTransformFlag, ImmutableArray<byte> nullableTransformFlags)
		{
			if (nullableTransformFlags.IsDefault && defaultTransformFlag == 0)
			{
				return metadataType;
			}
			int num = 0;
			TypeWithAnnotations typeWithAnnotations;
			if (metadataType.ApplyNullableTransforms(defaultTransformFlag, nullableTransformFlags, ref num, out typeWithAnnotations) && (nullableTransformFlags.IsDefault || num == nullableTransformFlags.Length))
			{
				return typeWithAnnotations;
			}
			return metadataType;
		}
	}
}
