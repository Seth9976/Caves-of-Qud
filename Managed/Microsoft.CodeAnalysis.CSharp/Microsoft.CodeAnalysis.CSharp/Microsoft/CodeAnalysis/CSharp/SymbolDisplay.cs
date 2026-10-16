using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200021B RID: 539
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public static class SymbolDisplay
	{
		// Token: 0x0600275F RID: 10079 RVA: 0x000D6DB9 File Offset: 0x000D4FB9
		public static string ToDisplayString(ISymbol symbol, [global::System.Runtime.CompilerServices.Nullable(2)] SymbolDisplayFormat format = null)
		{
			return SymbolDisplay.ToDisplayParts(symbol, format).ToDisplayString();
		}

		// Token: 0x06002760 RID: 10080 RVA: 0x000D6DC7 File Offset: 0x000D4FC7
		public static string ToDisplayString(ITypeSymbol symbol, NullableFlowState nullableFlowState, [global::System.Runtime.CompilerServices.Nullable(2)] SymbolDisplayFormat format = null)
		{
			return SymbolDisplay.ToDisplayParts(symbol, nullableFlowState, format).ToDisplayString();
		}

		// Token: 0x06002761 RID: 10081 RVA: 0x000D6DD6 File Offset: 0x000D4FD6
		public static string ToDisplayString(ITypeSymbol symbol, NullableAnnotation nullableAnnotation, [global::System.Runtime.CompilerServices.Nullable(2)] SymbolDisplayFormat format = null)
		{
			return SymbolDisplay.ToDisplayParts(symbol, nullableAnnotation, format).ToDisplayString();
		}

		// Token: 0x06002762 RID: 10082 RVA: 0x000D6DE5 File Offset: 0x000D4FE5
		public static string ToMinimalDisplayString(ISymbol symbol, SemanticModel semanticModel, int position, [global::System.Runtime.CompilerServices.Nullable(2)] SymbolDisplayFormat format = null)
		{
			return SymbolDisplay.ToMinimalDisplayParts(symbol, semanticModel, position, format).ToDisplayString();
		}

		// Token: 0x06002763 RID: 10083 RVA: 0x000D6DF5 File Offset: 0x000D4FF5
		public static string ToMinimalDisplayString(ITypeSymbol symbol, NullableFlowState nullableFlowState, SemanticModel semanticModel, int position, [global::System.Runtime.CompilerServices.Nullable(2)] SymbolDisplayFormat format = null)
		{
			return SymbolDisplay.ToMinimalDisplayParts(symbol, nullableFlowState, semanticModel, position, format).ToDisplayString();
		}

		// Token: 0x06002764 RID: 10084 RVA: 0x000D6E07 File Offset: 0x000D5007
		public static string ToMinimalDisplayString(ITypeSymbol symbol, NullableAnnotation nullableAnnotation, SemanticModel semanticModel, int position, [global::System.Runtime.CompilerServices.Nullable(2)] SymbolDisplayFormat format = null)
		{
			return SymbolDisplay.ToMinimalDisplayParts(symbol, nullableAnnotation, semanticModel, position, format).ToDisplayString();
		}

		// Token: 0x06002765 RID: 10085 RVA: 0x000D6E19 File Offset: 0x000D5019
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public static ImmutableArray<SymbolDisplayPart> ToDisplayParts([global::System.Runtime.CompilerServices.Nullable(1)] ISymbol symbol, [global::System.Runtime.CompilerServices.Nullable(2)] SymbolDisplayFormat format = null)
		{
			format = format ?? SymbolDisplayFormat.CSharpErrorMessageFormat;
			return SymbolDisplay.ToDisplayParts(symbol, null, -1, format, false);
		}

		// Token: 0x06002766 RID: 10086 RVA: 0x000D6E31 File Offset: 0x000D5031
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public static ImmutableArray<SymbolDisplayPart> ToDisplayParts([global::System.Runtime.CompilerServices.Nullable(1)] ITypeSymbol symbol, NullableFlowState nullableFlowState, [global::System.Runtime.CompilerServices.Nullable(2)] SymbolDisplayFormat format = null)
		{
			format = format ?? SymbolDisplayFormat.CSharpErrorMessageFormat;
			return SymbolDisplay.ToDisplayParts(symbol, nullableFlowState, null, -1, format, false);
		}

		// Token: 0x06002767 RID: 10087 RVA: 0x000D6E4A File Offset: 0x000D504A
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public static ImmutableArray<SymbolDisplayPart> ToDisplayParts([global::System.Runtime.CompilerServices.Nullable(1)] ITypeSymbol symbol, NullableAnnotation nullableAnnotation, [global::System.Runtime.CompilerServices.Nullable(2)] SymbolDisplayFormat format = null)
		{
			if (format == null)
			{
				format = SymbolDisplayFormat.CSharpErrorMessageFormat;
			}
			return SymbolDisplay.ToDisplayParts(symbol.WithNullableAnnotation(nullableAnnotation), null, -1, format, false);
		}

		// Token: 0x06002768 RID: 10088 RVA: 0x000D6E66 File Offset: 0x000D5066
		[return: global::System.Runtime.CompilerServices.Nullable(0)]
		public static ImmutableArray<SymbolDisplayPart> ToMinimalDisplayParts(ISymbol symbol, SemanticModel semanticModel, int position, [global::System.Runtime.CompilerServices.Nullable(2)] SymbolDisplayFormat format = null)
		{
			if (format == null)
			{
				format = SymbolDisplayFormat.MinimallyQualifiedFormat;
			}
			return SymbolDisplay.ToDisplayParts(symbol, semanticModel, position, format, true);
		}

		// Token: 0x06002769 RID: 10089 RVA: 0x000D6E7C File Offset: 0x000D507C
		[return: global::System.Runtime.CompilerServices.Nullable(0)]
		public static ImmutableArray<SymbolDisplayPart> ToMinimalDisplayParts(ITypeSymbol symbol, NullableFlowState nullableFlowState, SemanticModel semanticModel, int position, [global::System.Runtime.CompilerServices.Nullable(2)] SymbolDisplayFormat format = null)
		{
			if (format == null)
			{
				format = SymbolDisplayFormat.MinimallyQualifiedFormat;
			}
			return SymbolDisplay.ToDisplayParts(symbol, nullableFlowState, semanticModel, position, format, true);
		}

		// Token: 0x0600276A RID: 10090 RVA: 0x000D6E95 File Offset: 0x000D5095
		[return: global::System.Runtime.CompilerServices.Nullable(0)]
		public static ImmutableArray<SymbolDisplayPart> ToMinimalDisplayParts(ITypeSymbol symbol, NullableAnnotation nullableAnnotation, SemanticModel semanticModel, int position, [global::System.Runtime.CompilerServices.Nullable(2)] SymbolDisplayFormat format = null)
		{
			if (format == null)
			{
				format = SymbolDisplayFormat.MinimallyQualifiedFormat;
			}
			return SymbolDisplay.ToDisplayParts(symbol.WithNullableAnnotation(nullableAnnotation), semanticModel, position, format, true);
		}

		// Token: 0x0600276B RID: 10091 RVA: 0x000D6EB3 File Offset: 0x000D50B3
		[return: global::System.Runtime.CompilerServices.Nullable(0)]
		private static ImmutableArray<SymbolDisplayPart> ToDisplayParts(ITypeSymbol symbol, NullableFlowState nullableFlowState, [global::System.Runtime.CompilerServices.Nullable(2)] SemanticModel semanticModelOpt, int positionOpt, SymbolDisplayFormat format, bool minimal)
		{
			return SymbolDisplay.ToDisplayParts(symbol.WithNullableAnnotation(nullableFlowState.ToAnnotation()), semanticModelOpt, positionOpt, format, minimal);
		}

		// Token: 0x0600276C RID: 10092 RVA: 0x000D6ECC File Offset: 0x000D50CC
		[return: global::System.Runtime.CompilerServices.Nullable(0)]
		private static ImmutableArray<SymbolDisplayPart> ToDisplayParts(ISymbol symbol, [global::System.Runtime.CompilerServices.Nullable(2)] SemanticModel semanticModelOpt, int positionOpt, SymbolDisplayFormat format, bool minimal)
		{
			if (symbol == null)
			{
				throw new ArgumentNullException("symbol");
			}
			if (minimal)
			{
				if (semanticModelOpt == null)
				{
					throw new ArgumentException(CSharpResources.SyntaxTreeSemanticModelMust);
				}
				if (positionOpt < 0 || positionOpt > semanticModelOpt.SyntaxTree.Length)
				{
					throw new ArgumentOutOfRangeException(CSharpResources.PositionNotWithinTree);
				}
			}
			Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel.MethodSymbol methodSymbol = symbol as Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel.MethodSymbol;
			if (((methodSymbol != null) ? methodSymbol.UnderlyingMethodSymbol : null) is SynthesizedSimpleProgramEntryPointSymbol)
			{
				return ImmutableArray.Create<SymbolDisplayPart>(new SymbolDisplayPart(SymbolDisplayPartKind.MethodName, symbol, "<top-level-statements-entry-point>"));
			}
			ArrayBuilder<SymbolDisplayPart> instance = ArrayBuilder<SymbolDisplayPart>.GetInstance();
			SymbolDisplayVisitor symbolDisplayVisitor = new SymbolDisplayVisitor(instance, format, semanticModelOpt, positionOpt);
			symbol.Accept(symbolDisplayVisitor);
			return instance.ToImmutableAndFree();
		}

		// Token: 0x0600276D RID: 10093 RVA: 0x000D6F60 File Offset: 0x000D5160
		public static string FormatPrimitive(object obj, bool quoteStrings, bool useHexadecimalNumbers)
		{
			ObjectDisplayOptions objectDisplayOptions = ObjectDisplayOptions.EscapeNonPrintableCharacters;
			if (quoteStrings)
			{
				objectDisplayOptions |= ObjectDisplayOptions.UseQuotes;
			}
			if (useHexadecimalNumbers)
			{
				objectDisplayOptions |= ObjectDisplayOptions.UseHexadecimalNumbers;
			}
			return ObjectDisplay.FormatPrimitive(obj, objectDisplayOptions);
		}

		// Token: 0x0600276E RID: 10094 RVA: 0x000D6F88 File Offset: 0x000D5188
		public static string FormatLiteral(string value, bool quote)
		{
			ObjectDisplayOptions objectDisplayOptions = ObjectDisplayOptions.EscapeNonPrintableCharacters | (quote ? ObjectDisplayOptions.UseQuotes : ObjectDisplayOptions.None);
			return ObjectDisplay.FormatLiteral(value, objectDisplayOptions);
		}

		// Token: 0x0600276F RID: 10095 RVA: 0x000D6FA8 File Offset: 0x000D51A8
		public static string FormatLiteral(char c, bool quote)
		{
			ObjectDisplayOptions objectDisplayOptions = ObjectDisplayOptions.EscapeNonPrintableCharacters | (quote ? ObjectDisplayOptions.UseQuotes : ObjectDisplayOptions.None);
			return ObjectDisplay.FormatLiteral(c, objectDisplayOptions);
		}
	}
}
