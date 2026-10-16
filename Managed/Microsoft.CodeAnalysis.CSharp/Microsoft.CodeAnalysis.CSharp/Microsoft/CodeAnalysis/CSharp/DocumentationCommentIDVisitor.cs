using System;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200012F RID: 303
	internal sealed class DocumentationCommentIDVisitor : CSharpSymbolVisitor<StringBuilder, object>
	{
		// Token: 0x060018CE RID: 6350 RVA: 0x00084FD9 File Offset: 0x000831D9
		private DocumentationCommentIDVisitor()
		{
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x00084FE1 File Offset: 0x000831E1
		public override object DefaultVisit(Symbol symbol, StringBuilder builder)
		{
			return null;
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x00084FE4 File Offset: 0x000831E4
		public override object VisitNamespace(NamespaceSymbol symbol, StringBuilder builder)
		{
			if (!symbol.IsGlobalNamespace)
			{
				builder.Append("N:");
				DocumentationCommentIDVisitor.PartVisitor.Instance.Visit(symbol, builder);
			}
			return null;
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x00085008 File Offset: 0x00083208
		public override object VisitMethod(MethodSymbol symbol, StringBuilder builder)
		{
			builder.Append("M:");
			DocumentationCommentIDVisitor.PartVisitor.Instance.Visit(symbol, builder);
			return null;
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x00085024 File Offset: 0x00083224
		public override object VisitField(FieldSymbol symbol, StringBuilder builder)
		{
			builder.Append("F:");
			DocumentationCommentIDVisitor.PartVisitor.Instance.Visit(symbol, builder);
			return null;
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x00085040 File Offset: 0x00083240
		public override object VisitEvent(EventSymbol symbol, StringBuilder builder)
		{
			builder.Append("E:");
			DocumentationCommentIDVisitor.PartVisitor.Instance.Visit(symbol, builder);
			return null;
		}

		// Token: 0x060018D4 RID: 6356 RVA: 0x0008505C File Offset: 0x0008325C
		public override object VisitProperty(PropertySymbol symbol, StringBuilder builder)
		{
			builder.Append("P:");
			DocumentationCommentIDVisitor.PartVisitor.Instance.Visit(symbol, builder);
			return null;
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x00085078 File Offset: 0x00083278
		public override object VisitNamedType(NamedTypeSymbol symbol, StringBuilder builder)
		{
			builder.Append("T:");
			DocumentationCommentIDVisitor.PartVisitor.Instance.Visit(symbol, builder);
			return null;
		}

		// Token: 0x060018D6 RID: 6358 RVA: 0x00085094 File Offset: 0x00083294
		public override object VisitDynamicType(DynamicTypeSymbol symbol, StringBuilder builder)
		{
			return this.DefaultVisit(symbol, builder);
		}

		// Token: 0x060018D7 RID: 6359 RVA: 0x0008509E File Offset: 0x0008329E
		public override object VisitErrorType(ErrorTypeSymbol symbol, StringBuilder builder)
		{
			builder.Append("!:");
			DocumentationCommentIDVisitor.PartVisitor.Instance.Visit(symbol, builder);
			return null;
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x000850BA File Offset: 0x000832BA
		public override object VisitTypeParameter(TypeParameterSymbol symbol, StringBuilder builder)
		{
			builder.Append("!:");
			builder.Append(symbol.Name);
			return null;
		}

		// Token: 0x040006DE RID: 1758
		public static readonly DocumentationCommentIDVisitor Instance = new DocumentationCommentIDVisitor();

		// Token: 0x020006D8 RID: 1752
		private sealed class PartVisitor : CSharpSymbolVisitor<StringBuilder, object>
		{
			// Token: 0x06008490 RID: 33936 RVA: 0x001E0859 File Offset: 0x001DEA59
			private PartVisitor(bool inParameterOrReturnType)
			{
				this._inParameterOrReturnType = inParameterOrReturnType;
			}

			// Token: 0x06008491 RID: 33937 RVA: 0x001E0868 File Offset: 0x001DEA68
			public override object VisitArrayType(ArrayTypeSymbol symbol, StringBuilder builder)
			{
				this.Visit(symbol.ElementType, builder);
				if (symbol.IsSZArray)
				{
					builder.Append("[]");
				}
				else
				{
					builder.Append("[0:");
					for (int i = 0; i < symbol.Rank - 1; i++)
					{
						builder.Append(",0:");
					}
					builder.Append(']');
				}
				return null;
			}

			// Token: 0x06008492 RID: 33938 RVA: 0x001E08CE File Offset: 0x001DEACE
			public override object VisitField(FieldSymbol symbol, StringBuilder builder)
			{
				this.Visit(symbol.ContainingType, builder);
				builder.Append('.');
				builder.Append(symbol.Name);
				return null;
			}

			// Token: 0x06008493 RID: 33939 RVA: 0x001E08F8 File Offset: 0x001DEAF8
			private void VisitParameters(ImmutableArray<ParameterSymbol> parameters, bool isVararg, StringBuilder builder)
			{
				builder.Append('(');
				bool flag = false;
				foreach (ParameterSymbol parameterSymbol in parameters)
				{
					if (flag)
					{
						builder.Append(',');
					}
					this.Visit(parameterSymbol, builder);
					flag = true;
				}
				if (isVararg && flag)
				{
					builder.Append(',');
				}
				builder.Append(')');
			}

			// Token: 0x06008494 RID: 33940 RVA: 0x001E095C File Offset: 0x001DEB5C
			public override object VisitMethod(MethodSymbol symbol, StringBuilder builder)
			{
				this.Visit(symbol.ContainingType, builder);
				builder.Append('.');
				builder.Append(DocumentationCommentIDVisitor.PartVisitor.GetEscapedMetadataName(symbol));
				if (symbol.Arity != 0)
				{
					builder.Append("``");
					builder.Append(symbol.Arity);
				}
				if (symbol.Parameters.Any<ParameterSymbol>() || symbol.IsVararg)
				{
					DocumentationCommentIDVisitor.PartVisitor.s_parameterOrReturnTypeInstance.VisitParameters(symbol.Parameters, symbol.IsVararg, builder);
				}
				if (symbol.MethodKind == MethodKind.Conversion)
				{
					builder.Append('~');
					DocumentationCommentIDVisitor.PartVisitor.s_parameterOrReturnTypeInstance.Visit(symbol.ReturnType, builder);
				}
				return null;
			}

			// Token: 0x06008495 RID: 33941 RVA: 0x001E0A00 File Offset: 0x001DEC00
			public override object VisitProperty(PropertySymbol symbol, StringBuilder builder)
			{
				this.Visit(symbol.ContainingType, builder);
				builder.Append('.');
				builder.Append(DocumentationCommentIDVisitor.PartVisitor.GetEscapedMetadataName(symbol));
				if (symbol.Parameters.Any<ParameterSymbol>())
				{
					DocumentationCommentIDVisitor.PartVisitor.s_parameterOrReturnTypeInstance.VisitParameters(symbol.Parameters, false, builder);
				}
				return null;
			}

			// Token: 0x06008496 RID: 33942 RVA: 0x001E0A51 File Offset: 0x001DEC51
			public override object VisitEvent(EventSymbol symbol, StringBuilder builder)
			{
				this.Visit(symbol.ContainingType, builder);
				builder.Append('.');
				builder.Append(DocumentationCommentIDVisitor.PartVisitor.GetEscapedMetadataName(symbol));
				return null;
			}

			// Token: 0x06008497 RID: 33943 RVA: 0x001E0A78 File Offset: 0x001DEC78
			public override object VisitTypeParameter(TypeParameterSymbol symbol, StringBuilder builder)
			{
				int num = 0;
				Symbol containingSymbol = symbol.ContainingSymbol;
				if (containingSymbol.Kind == SymbolKind.Method)
				{
					builder.Append("``");
				}
				else
				{
					for (NamedTypeSymbol namedTypeSymbol = containingSymbol.ContainingType; namedTypeSymbol != null; namedTypeSymbol = namedTypeSymbol.ContainingType)
					{
						num += namedTypeSymbol.Arity;
					}
					builder.Append('`');
				}
				builder.Append(symbol.Ordinal + num);
				return null;
			}

			// Token: 0x06008498 RID: 33944 RVA: 0x001E0ADC File Offset: 0x001DECDC
			public override object VisitNamedType(NamedTypeSymbol symbol, StringBuilder builder)
			{
				if (symbol.ContainingSymbol != null && symbol.ContainingSymbol.Name.Length != 0)
				{
					this.Visit(symbol.ContainingSymbol, builder);
					builder.Append('.');
				}
				builder.Append(symbol.Name);
				if (symbol.Arity != 0)
				{
					if (!this._inParameterOrReturnType && TypeSymbol.Equals(symbol, symbol.ConstructedFrom, TypeCompareKind.ConsiderEverything))
					{
						builder.Append('`');
						builder.Append(symbol.Arity);
					}
					else
					{
						builder.Append('{');
						bool flag = false;
						foreach (TypeWithAnnotations typeWithAnnotations in symbol.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics)
						{
							if (flag)
							{
								builder.Append(',');
							}
							this.Visit(typeWithAnnotations.Type, builder);
							flag = true;
						}
						builder.Append('}');
					}
				}
				return null;
			}

			// Token: 0x06008499 RID: 33945 RVA: 0x001E0BB5 File Offset: 0x001DEDB5
			public override object VisitPointerType(PointerTypeSymbol symbol, StringBuilder builder)
			{
				this.Visit(symbol.PointedAtType, builder);
				builder.Append('*');
				return null;
			}

			// Token: 0x0600849A RID: 33946 RVA: 0x001E0BD0 File Offset: 0x001DEDD0
			public override object VisitNamespace(NamespaceSymbol symbol, StringBuilder builder)
			{
				if (symbol.ContainingNamespace != null && symbol.ContainingNamespace.Name.Length != 0)
				{
					this.Visit(symbol.ContainingNamespace, builder);
					builder.Append('.');
				}
				builder.Append(symbol.Name);
				return null;
			}

			// Token: 0x0600849B RID: 33947 RVA: 0x001E0C1C File Offset: 0x001DEE1C
			public override object VisitParameter(ParameterSymbol symbol, StringBuilder builder)
			{
				this.Visit(symbol.Type, builder);
				if (symbol.RefKind != RefKind.None)
				{
					builder.Append('@');
				}
				return null;
			}

			// Token: 0x0600849C RID: 33948 RVA: 0x001E0C3E File Offset: 0x001DEE3E
			public override object VisitErrorType(ErrorTypeSymbol symbol, StringBuilder builder)
			{
				return this.VisitNamedType(symbol, builder);
			}

			// Token: 0x0600849D RID: 33949 RVA: 0x001E0C48 File Offset: 0x001DEE48
			public override object VisitDynamicType(DynamicTypeSymbol symbol, StringBuilder builder)
			{
				builder.Append("System.Object");
				return null;
			}

			// Token: 0x0600849E RID: 33950 RVA: 0x001E0C58 File Offset: 0x001DEE58
			private static string GetEscapedMetadataName(Symbol symbol)
			{
				string metadataName = symbol.MetadataName;
				int num = metadataName.IndexOf("::", 4);
				int num2 = ((num < 0) ? 0 : (num + 2));
				PooledStringBuilder instance = PooledStringBuilder.GetInstance();
				instance.Builder.Append(metadataName, num2, metadataName.Length - num2);
				instance.Builder.Replace('.', '#').Replace('<', '{').Replace('>', '}');
				return instance.ToStringAndFree();
			}

			// Token: 0x040021C0 RID: 8640
			internal static readonly DocumentationCommentIDVisitor.PartVisitor Instance = new DocumentationCommentIDVisitor.PartVisitor(false);

			// Token: 0x040021C1 RID: 8641
			private static readonly DocumentationCommentIDVisitor.PartVisitor s_parameterOrReturnTypeInstance = new DocumentationCommentIDVisitor.PartVisitor(true);

			// Token: 0x040021C2 RID: 8642
			private readonly bool _inParameterOrReturnType;
		}
	}
}
