using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004C4 RID: 1220
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal static class AttributeDataExtensions
	{
		// Token: 0x06006841 RID: 26689 RVA: 0x00187C1C File Offset: 0x00185E1C
		internal static int IndexOfAttribute([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] this ImmutableArray<CSharpAttributeData> attributes, Symbol targetSymbol, AttributeDescription description)
		{
			for (int i = 0; i < attributes.Length; i++)
			{
				if (attributes[i].IsTargetAttribute(targetSymbol, description))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06006842 RID: 26690 RVA: 0x00187C4F File Offset: 0x00185E4F
		internal static CSharpSyntaxNode GetAttributeArgumentSyntax(this AttributeData attribute, int parameterIndex, AttributeSyntax attributeSyntax)
		{
			return ((SourceAttributeData)attribute).GetAttributeArgumentSyntax(parameterIndex, attributeSyntax);
		}

		// Token: 0x06006843 RID: 26691 RVA: 0x00187C60 File Offset: 0x00185E60
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal static string DecodeNotNullIfNotNullAttribute(this CSharpAttributeData attribute)
		{
			ImmutableArray<TypedConstant> commonConstructorArguments = attribute.CommonConstructorArguments;
			string text;
			if (commonConstructorArguments.Length != 1 || !commonConstructorArguments[0].TryDecodeValue<string>(SpecialType.System_String, out text))
			{
				return null;
			}
			return text;
		}

		// Token: 0x06006844 RID: 26692 RVA: 0x00187C97 File Offset: 0x00185E97
		internal static Location GetAttributeArgumentSyntaxLocation(this AttributeData attribute, int parameterIndex, [global::System.Runtime.CompilerServices.Nullable(2)] AttributeSyntax attributeSyntaxOpt)
		{
			if (attributeSyntaxOpt == null)
			{
				return NoLocation.Singleton;
			}
			return ((SourceAttributeData)attribute).GetAttributeArgumentSyntax(parameterIndex, attributeSyntaxOpt).Location;
		}
	}
}
