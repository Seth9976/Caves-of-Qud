using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000185 RID: 389
	[NullableContext(1)]
	[Nullable(0)]
	public static class DocumentationCommentId
	{
		// Token: 0x06001012 RID: 4114 RVA: 0x0002ED35 File Offset: 0x0002CF35
		public static string CreateDeclarationId(ISymbol symbol)
		{
			if (symbol == null)
			{
				throw new ArgumentNullException("symbol");
			}
			StringBuilder stringBuilder = new StringBuilder();
			new DocumentationCommentId.DeclarationGenerator(stringBuilder).Visit(symbol);
			return stringBuilder.ToString();
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x0002ED5B File Offset: 0x0002CF5B
		public static string CreateReferenceId(ISymbol symbol)
		{
			if (symbol == null)
			{
				throw new ArgumentNullException("symbol");
			}
			if (symbol is INamespaceSymbol)
			{
				return DocumentationCommentId.CreateDeclarationId(symbol);
			}
			StringBuilder stringBuilder = new StringBuilder();
			new DocumentationCommentId.ReferenceGenerator(stringBuilder, null).Visit(symbol);
			return stringBuilder.ToString();
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x0002ED94 File Offset: 0x0002CF94
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<ISymbol> GetSymbolsForDeclarationId(string id, Compilation compilation)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (compilation == null)
			{
				throw new ArgumentNullException("compilation");
			}
			List<ISymbol> list = DocumentationCommentId.s_symbolListPool.Allocate();
			ImmutableArray<ISymbol> immutableArray;
			try
			{
				DocumentationCommentId.Parser.ParseDeclaredSymbolId(id, compilation, list);
				immutableArray = list.ToImmutableArray<ISymbol>();
			}
			finally
			{
				DocumentationCommentId.s_symbolListPool.ClearAndFree(list);
			}
			return immutableArray;
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x0002EDF8 File Offset: 0x0002CFF8
		private static bool TryGetSymbolsForDeclarationId(string id, Compilation compilation, List<ISymbol> results)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (compilation == null)
			{
				throw new ArgumentNullException("compilation");
			}
			if (results == null)
			{
				throw new ArgumentNullException("results");
			}
			return DocumentationCommentId.Parser.ParseDeclaredSymbolId(id, compilation, results);
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x0002EE2C File Offset: 0x0002D02C
		[return: Nullable(2)]
		public static ISymbol GetFirstSymbolForDeclarationId(string id, Compilation compilation)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (compilation == null)
			{
				throw new ArgumentNullException("compilation");
			}
			List<ISymbol> list = DocumentationCommentId.s_symbolListPool.Allocate();
			ISymbol symbol;
			try
			{
				DocumentationCommentId.Parser.ParseDeclaredSymbolId(id, compilation, list);
				symbol = ((list.Count == 0) ? null : list[0]);
			}
			finally
			{
				DocumentationCommentId.s_symbolListPool.ClearAndFree(list);
			}
			return symbol;
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x0002EE9C File Offset: 0x0002D09C
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<ISymbol> GetSymbolsForReferenceId(string id, Compilation compilation)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (compilation == null)
			{
				throw new ArgumentNullException("compilation");
			}
			List<ISymbol> list = DocumentationCommentId.s_symbolListPool.Allocate();
			ImmutableArray<ISymbol> immutableArray;
			try
			{
				DocumentationCommentId.TryGetSymbolsForReferenceId(id, compilation, list);
				immutableArray = list.ToImmutableArray<ISymbol>();
			}
			finally
			{
				DocumentationCommentId.s_symbolListPool.ClearAndFree(list);
			}
			return immutableArray;
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x0002EF00 File Offset: 0x0002D100
		private static bool TryGetSymbolsForReferenceId(string id, Compilation compilation, List<ISymbol> results)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (compilation == null)
			{
				throw new ArgumentNullException("compilation");
			}
			if (results == null)
			{
				throw new ArgumentNullException("results");
			}
			if (id.Length > 1 && id.get_Chars(0) == 'N' && id.get_Chars(1) == ':')
			{
				return DocumentationCommentId.TryGetSymbolsForDeclarationId(id, compilation, results);
			}
			return DocumentationCommentId.Parser.ParseReferencedSymbolId(id, compilation, results);
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x0002EF68 File Offset: 0x0002D168
		[return: Nullable(2)]
		public static ISymbol GetFirstSymbolForReferenceId(string id, Compilation compilation)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (compilation == null)
			{
				throw new ArgumentNullException("compilation");
			}
			if (id.Length > 1 && id.get_Chars(0) == 'N' && id.get_Chars(1) == ':')
			{
				return DocumentationCommentId.GetFirstSymbolForDeclarationId(id, compilation);
			}
			List<ISymbol> list = DocumentationCommentId.s_symbolListPool.Allocate();
			ISymbol symbol;
			try
			{
				DocumentationCommentId.Parser.ParseReferencedSymbolId(id, compilation, list);
				symbol = ((list.Count == 0) ? null : list[0]);
			}
			finally
			{
				DocumentationCommentId.s_symbolListPool.ClearAndFree(list);
			}
			return symbol;
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x0002F000 File Offset: 0x0002D200
		[NullableContext(2)]
		private static int GetTotalTypeParameterCount(INamedTypeSymbol symbol)
		{
			int num = 0;
			while (symbol != null)
			{
				num += symbol.TypeParameters.Length;
				symbol = symbol.ContainingSymbol as INamedTypeSymbol;
			}
			return num;
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x0002F033 File Offset: 0x0002D233
		private static string EncodeName(string name)
		{
			if (name.IndexOf('.') >= 0)
			{
				return name.Replace('.', '#');
			}
			return name;
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x0002F04C File Offset: 0x0002D24C
		private static string EncodePropertyName(string name)
		{
			if (name == "this[]")
			{
				name = "Item";
			}
			else if (name.EndsWith(".this[]"))
			{
				name = name.Substring(0, name.Length - 6) + "Item";
			}
			return name;
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x0002F098 File Offset: 0x0002D298
		private static string DecodePropertyName(string name, string language)
		{
			if (language == "C#")
			{
				if (name == "Item")
				{
					name = "this[]";
				}
				else if (name.EndsWith(".Item"))
				{
					name = name.Substring(0, name.Length - 4) + "this[]";
				}
			}
			return name;
		}

		// Token: 0x04000473 RID: 1139
		private static readonly DocumentationCommentId.ListPool<ISymbol> s_symbolListPool = new DocumentationCommentId.ListPool<ISymbol>();

		// Token: 0x04000474 RID: 1140
		private static readonly DocumentationCommentId.ListPool<INamespaceOrTypeSymbol> s_namespaceOrTypeListPool = new DocumentationCommentId.ListPool<INamespaceOrTypeSymbol>();

		// Token: 0x020005AA RID: 1450
		[Nullable(new byte[] { 0, 1, 1 })]
		private class ListPool<[Nullable(2)] T> : ObjectPool<List<T>>
		{
			// Token: 0x060035B3 RID: 13747 RVA: 0x00096EF9 File Offset: 0x000950F9
			public ListPool()
				: base(() => new List<T>(10), 10)
			{
			}

			// Token: 0x060035B4 RID: 13748 RVA: 0x00096F22 File Offset: 0x00095122
			public void ClearAndFree(List<T> list)
			{
				list.Clear();
				base.Free(list);
			}

			// Token: 0x060035B5 RID: 13749 RVA: 0x00096F31 File Offset: 0x00095131
			[Obsolete("Do not use Free, Use ClearAndFree instead.", true)]
			public new void Free(List<T> list)
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x020005AB RID: 1451
		[Nullable(0)]
		private class DeclarationGenerator : SymbolVisitor
		{
			// Token: 0x060035B6 RID: 13750 RVA: 0x00096F38 File Offset: 0x00095138
			public DeclarationGenerator(StringBuilder builder)
			{
				this._builder = builder;
				this._generator = new DocumentationCommentId.DeclarationGenerator.Generator(builder);
			}

			// Token: 0x060035B7 RID: 13751 RVA: 0x00096F53 File Offset: 0x00095153
			public override void DefaultVisit(ISymbol symbol)
			{
				throw new InvalidOperationException("Cannot generated a documentation comment id for symbol.");
			}

			// Token: 0x060035B8 RID: 13752 RVA: 0x00096F5F File Offset: 0x0009515F
			public override void VisitEvent(IEventSymbol symbol)
			{
				this._builder.Append("E:");
				this._generator.Visit(symbol);
			}

			// Token: 0x060035B9 RID: 13753 RVA: 0x00096F7F File Offset: 0x0009517F
			public override void VisitField(IFieldSymbol symbol)
			{
				this._builder.Append("F:");
				this._generator.Visit(symbol);
			}

			// Token: 0x060035BA RID: 13754 RVA: 0x00096F9F File Offset: 0x0009519F
			public override void VisitProperty(IPropertySymbol symbol)
			{
				this._builder.Append("P:");
				this._generator.Visit(symbol);
			}

			// Token: 0x060035BB RID: 13755 RVA: 0x00096FBF File Offset: 0x000951BF
			public override void VisitMethod(IMethodSymbol symbol)
			{
				this._builder.Append("M:");
				this._generator.Visit(symbol);
			}

			// Token: 0x060035BC RID: 13756 RVA: 0x00096FDF File Offset: 0x000951DF
			public override void VisitNamespace(INamespaceSymbol symbol)
			{
				this._builder.Append("N:");
				this._generator.Visit(symbol);
			}

			// Token: 0x060035BD RID: 13757 RVA: 0x00096FFF File Offset: 0x000951FF
			public override void VisitNamedType(INamedTypeSymbol symbol)
			{
				this._builder.Append("T:");
				this._generator.Visit(symbol);
			}

			// Token: 0x04001656 RID: 5718
			private readonly StringBuilder _builder;

			// Token: 0x04001657 RID: 5719
			private readonly DocumentationCommentId.DeclarationGenerator.Generator _generator;

			// Token: 0x020007BD RID: 1981
			[Nullable(0)]
			private class Generator : SymbolVisitor<bool>
			{
				// Token: 0x06003FC4 RID: 16324 RVA: 0x000B22FF File Offset: 0x000B04FF
				public Generator(StringBuilder builder)
				{
					this._builder = builder;
				}

				// Token: 0x06003FC5 RID: 16325 RVA: 0x000B230E File Offset: 0x000B050E
				private DocumentationCommentId.ReferenceGenerator GetReferenceGenerator(ISymbol typeParameterContext)
				{
					if (this._referenceGenerator == null || this._referenceGenerator.TypeParameterContext != typeParameterContext)
					{
						this._referenceGenerator = new DocumentationCommentId.ReferenceGenerator(this._builder, typeParameterContext);
					}
					return this._referenceGenerator;
				}

				// Token: 0x06003FC6 RID: 16326 RVA: 0x000B233E File Offset: 0x000B053E
				public override bool DefaultVisit(ISymbol symbol)
				{
					throw new InvalidOperationException("Cannot generated a documentation comment id for symbol.");
				}

				// Token: 0x06003FC7 RID: 16327 RVA: 0x000B234A File Offset: 0x000B054A
				public override bool VisitEvent(IEventSymbol symbol)
				{
					if (this.Visit(symbol.ContainingSymbol))
					{
						this._builder.Append(".");
					}
					this._builder.Append(DocumentationCommentId.EncodeName(symbol.Name));
					return true;
				}

				// Token: 0x06003FC8 RID: 16328 RVA: 0x000B2383 File Offset: 0x000B0583
				public override bool VisitField(IFieldSymbol symbol)
				{
					if (this.Visit(symbol.ContainingSymbol))
					{
						this._builder.Append(".");
					}
					this._builder.Append(DocumentationCommentId.EncodeName(symbol.Name));
					return true;
				}

				// Token: 0x06003FC9 RID: 16329 RVA: 0x000B23BC File Offset: 0x000B05BC
				public override bool VisitProperty(IPropertySymbol symbol)
				{
					if (this.Visit(symbol.ContainingSymbol))
					{
						this._builder.Append(".");
					}
					string text = DocumentationCommentId.EncodePropertyName(symbol.Name);
					this._builder.Append(DocumentationCommentId.EncodeName(text));
					this.AppendParameters(symbol.Parameters);
					return true;
				}

				// Token: 0x06003FCA RID: 16330 RVA: 0x000B2414 File Offset: 0x000B0614
				public override bool VisitMethod(IMethodSymbol symbol)
				{
					if (this.Visit(symbol.ContainingSymbol))
					{
						this._builder.Append(".");
						this._builder.Append(DocumentationCommentId.EncodeName(symbol.Name));
					}
					if (symbol.TypeParameters.Length > 0)
					{
						this._builder.Append("``");
						this._builder.Append(symbol.TypeParameters.Length);
					}
					this.AppendParameters(symbol.Parameters);
					if (!symbol.ReturnsVoid)
					{
						this._builder.Append("~");
						this.GetReferenceGenerator(symbol).Visit(symbol.ReturnType);
					}
					return true;
				}

				// Token: 0x06003FCB RID: 16331 RVA: 0x000B24CC File Offset: 0x000B06CC
				private void AppendParameters([Nullable(new byte[] { 0, 1 })] ImmutableArray<IParameterSymbol> parameters)
				{
					if (parameters.Length > 0)
					{
						this._builder.Append("(");
						int i = 0;
						int length = parameters.Length;
						while (i < length)
						{
							if (i > 0)
							{
								this._builder.Append(",");
							}
							IParameterSymbol parameterSymbol = parameters[i];
							this.GetReferenceGenerator(parameterSymbol.ContainingSymbol).Visit(parameterSymbol.Type);
							if (parameterSymbol.RefKind != RefKind.None)
							{
								this._builder.Append("@");
							}
							i++;
						}
						this._builder.Append(")");
					}
				}

				// Token: 0x06003FCC RID: 16332 RVA: 0x000B256C File Offset: 0x000B076C
				public override bool VisitNamespace(INamespaceSymbol symbol)
				{
					if (symbol.IsGlobalNamespace)
					{
						return false;
					}
					if (this.Visit(symbol.ContainingSymbol))
					{
						this._builder.Append(".");
					}
					this._builder.Append(DocumentationCommentId.EncodeName(symbol.Name));
					return true;
				}

				// Token: 0x06003FCD RID: 16333 RVA: 0x000B25BC File Offset: 0x000B07BC
				public override bool VisitNamedType(INamedTypeSymbol symbol)
				{
					if (this.Visit(symbol.ContainingSymbol))
					{
						this._builder.Append(".");
					}
					this._builder.Append(DocumentationCommentId.EncodeName(symbol.Name));
					if (symbol.TypeParameters.Length > 0)
					{
						this._builder.Append("`");
						this._builder.Append(symbol.TypeParameters.Length);
					}
					return true;
				}

				// Token: 0x04001DD2 RID: 7634
				private readonly StringBuilder _builder;

				// Token: 0x04001DD3 RID: 7635
				[Nullable(2)]
				private DocumentationCommentId.ReferenceGenerator _referenceGenerator;
			}
		}

		// Token: 0x020005AC RID: 1452
		[Nullable(0)]
		private class ReferenceGenerator : SymbolVisitor<bool>
		{
			// Token: 0x060035BE RID: 13758 RVA: 0x0009701F File Offset: 0x0009521F
			public ReferenceGenerator(StringBuilder builder, [Nullable(2)] ISymbol typeParameterContext)
			{
				this._builder = builder;
				this._typeParameterContext = typeParameterContext;
			}

			// Token: 0x17000F45 RID: 3909
			// (get) Token: 0x060035BF RID: 13759 RVA: 0x00097035 File Offset: 0x00095235
			[Nullable(2)]
			public ISymbol TypeParameterContext
			{
				[NullableContext(2)]
				get
				{
					return this._typeParameterContext;
				}
			}

			// Token: 0x060035C0 RID: 13760 RVA: 0x0009703D File Offset: 0x0009523D
			private void BuildDottedName(ISymbol symbol)
			{
				if (this.Visit(symbol.ContainingSymbol))
				{
					this._builder.Append(".");
				}
				this._builder.Append(DocumentationCommentId.EncodeName(symbol.Name));
			}

			// Token: 0x060035C1 RID: 13761 RVA: 0x00097075 File Offset: 0x00095275
			public override bool VisitAlias(IAliasSymbol symbol)
			{
				return symbol.Target.Accept<bool>(this);
			}

			// Token: 0x060035C2 RID: 13762 RVA: 0x00097083 File Offset: 0x00095283
			public override bool VisitNamespace(INamespaceSymbol symbol)
			{
				if (symbol.IsGlobalNamespace)
				{
					return false;
				}
				this.BuildDottedName(symbol);
				return true;
			}

			// Token: 0x060035C3 RID: 13763 RVA: 0x00097098 File Offset: 0x00095298
			public override bool VisitNamedType(INamedTypeSymbol symbol)
			{
				this.BuildDottedName(symbol);
				if (symbol.IsGenericType)
				{
					if (symbol.OriginalDefinition == symbol)
					{
						this._builder.Append("`");
						this._builder.Append(symbol.TypeParameters.Length);
					}
					else if (symbol.TypeArguments.Length > 0)
					{
						this._builder.Append("{");
						int i = 0;
						int length = symbol.TypeArguments.Length;
						while (i < length)
						{
							if (i > 0)
							{
								this._builder.Append(",");
							}
							this.Visit(symbol.TypeArguments[i]);
							i++;
						}
						this._builder.Append("}");
					}
				}
				return true;
			}

			// Token: 0x060035C4 RID: 13764 RVA: 0x00097167 File Offset: 0x00095367
			public override bool VisitDynamicType(IDynamicTypeSymbol symbol)
			{
				this._builder.Append("System.Object");
				return true;
			}

			// Token: 0x060035C5 RID: 13765 RVA: 0x0009717C File Offset: 0x0009537C
			public override bool VisitArrayType(IArrayTypeSymbol symbol)
			{
				this.Visit(symbol.ElementType);
				this._builder.Append("[");
				int i = 0;
				int rank = symbol.Rank;
				while (i < rank)
				{
					if (i > 0)
					{
						this._builder.Append(",");
					}
					i++;
				}
				this._builder.Append("]");
				return true;
			}

			// Token: 0x060035C6 RID: 13766 RVA: 0x000971E1 File Offset: 0x000953E1
			public override bool VisitPointerType(IPointerTypeSymbol symbol)
			{
				this.Visit(symbol.PointedAtType);
				this._builder.Append("*");
				return true;
			}

			// Token: 0x060035C7 RID: 13767 RVA: 0x00097204 File Offset: 0x00095404
			public override bool VisitTypeParameter(ITypeParameterSymbol symbol)
			{
				if (!this.IsInScope(symbol))
				{
					new DocumentationCommentId.DeclarationGenerator(this._builder).Visit(symbol.ContainingSymbol);
					this._builder.Append(":");
				}
				if (symbol.DeclaringMethod != null)
				{
					this._builder.Append("``");
					this._builder.Append(symbol.Ordinal);
				}
				else
				{
					ISymbol containingSymbol = symbol.ContainingSymbol;
					int totalTypeParameterCount = DocumentationCommentId.GetTotalTypeParameterCount(((containingSymbol != null) ? containingSymbol.ContainingSymbol : null) as INamedTypeSymbol);
					this._builder.Append("`");
					this._builder.Append(totalTypeParameterCount + symbol.Ordinal);
				}
				return true;
			}

			// Token: 0x060035C8 RID: 13768 RVA: 0x000972B4 File Offset: 0x000954B4
			private bool IsInScope(ITypeParameterSymbol typeParameterSymbol)
			{
				ISymbol containingSymbol = typeParameterSymbol.ContainingSymbol;
				for (ISymbol symbol = this._typeParameterContext; symbol != null; symbol = symbol.ContainingSymbol)
				{
					if (symbol == containingSymbol)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x04001658 RID: 5720
			private readonly StringBuilder _builder;

			// Token: 0x04001659 RID: 5721
			[Nullable(2)]
			private readonly ISymbol _typeParameterContext;
		}

		// Token: 0x020005AD RID: 1453
		[Nullable(0)]
		private static class Parser
		{
			// Token: 0x060035C9 RID: 13769 RVA: 0x000972E4 File Offset: 0x000954E4
			public static bool ParseDeclaredSymbolId(string id, Compilation compilation, List<ISymbol> results)
			{
				if (id == null)
				{
					return false;
				}
				if (id.Length < 2)
				{
					return false;
				}
				int num = 0;
				results.Clear();
				DocumentationCommentId.Parser.ParseDeclaredId(id, ref num, compilation, results);
				return results.Count > 0;
			}

			// Token: 0x060035CA RID: 13770 RVA: 0x0009731C File Offset: 0x0009551C
			public static bool ParseReferencedSymbolId(string id, Compilation compilation, List<ISymbol> results)
			{
				if (id == null)
				{
					return false;
				}
				int num = 0;
				results.Clear();
				DocumentationCommentId.Parser.ParseTypeSymbol(id, ref num, compilation, null, results);
				return results.Count > 0;
			}

			// Token: 0x060035CB RID: 13771 RVA: 0x0009734C File Offset: 0x0009554C
			private static void ParseDeclaredId(string id, ref int index, Compilation compilation, List<ISymbol> results)
			{
				char c = DocumentationCommentId.Parser.PeekNextChar(id, index);
				SymbolKind symbolKind;
				if (c <= 'F')
				{
					if (c != 'E')
					{
						if (c != 'F')
						{
							return;
						}
						symbolKind = SymbolKind.Field;
					}
					else
					{
						symbolKind = SymbolKind.Event;
					}
				}
				else
				{
					switch (c)
					{
					case 'M':
						symbolKind = SymbolKind.Method;
						break;
					case 'N':
						symbolKind = SymbolKind.Namespace;
						break;
					case 'O':
						return;
					case 'P':
						symbolKind = SymbolKind.Property;
						break;
					default:
						if (c != 'T')
						{
							return;
						}
						symbolKind = SymbolKind.NamedType;
						break;
					}
				}
				index++;
				if (DocumentationCommentId.Parser.PeekNextChar(id, index) == ':')
				{
					index++;
				}
				List<INamespaceOrTypeSymbol> list = DocumentationCommentId.s_namespaceOrTypeListPool.Allocate();
				try
				{
					list.Add(compilation.GlobalNamespace);
					for (;;)
					{
						string text = DocumentationCommentId.Parser.ParseName(id, ref index);
						int num = 0;
						if (DocumentationCommentId.Parser.PeekNextChar(id, index) == '`')
						{
							index++;
							if (DocumentationCommentId.Parser.PeekNextChar(id, index) == '`')
							{
								index++;
							}
							num = DocumentationCommentId.Parser.ReadNextInteger(id, ref index);
						}
						if (DocumentationCommentId.Parser.PeekNextChar(id, index) != '.')
						{
							goto IL_0120;
						}
						index++;
						if (num > 0)
						{
							DocumentationCommentId.Parser.GetMatchingTypes(list, text, num, results);
						}
						else if (symbolKind == SymbolKind.Namespace)
						{
							DocumentationCommentId.Parser.GetMatchingNamespaces(list, text, results);
						}
						else
						{
							DocumentationCommentId.Parser.GetMatchingNamespaceOrTypes(list, text, results);
						}
						if (results.Count == 0)
						{
							break;
						}
						list.Clear();
						list.AddRange(Enumerable.OfType<INamespaceOrTypeSymbol>(results));
						results.Clear();
					}
					return;
					IL_0120:
					switch (symbolKind)
					{
					case SymbolKind.Event:
					{
						string text;
						DocumentationCommentId.Parser.GetMatchingEvents(list, text, results);
						break;
					}
					case SymbolKind.Field:
					{
						string text;
						DocumentationCommentId.Parser.GetMatchingFields(list, text, results);
						break;
					}
					case SymbolKind.Method:
					{
						string text;
						int num;
						DocumentationCommentId.Parser.GetMatchingMethods(id, ref index, list, text, num, compilation, results);
						break;
					}
					case SymbolKind.NamedType:
					{
						string text;
						int num;
						DocumentationCommentId.Parser.GetMatchingTypes(list, text, num, results);
						break;
					}
					case SymbolKind.Namespace:
					{
						string text;
						DocumentationCommentId.Parser.GetMatchingNamespaces(list, text, results);
						break;
					}
					case SymbolKind.Property:
					{
						string text;
						DocumentationCommentId.Parser.GetMatchingProperties(id, ref index, list, text, compilation, results);
						break;
					}
					}
				}
				finally
				{
					DocumentationCommentId.s_namespaceOrTypeListPool.ClearAndFree(list);
				}
			}

			// Token: 0x060035CC RID: 13772 RVA: 0x00097520 File Offset: 0x00095720
			[return: Nullable(2)]
			private static ITypeSymbol ParseTypeSymbol(string id, ref int index, Compilation compilation, [Nullable(2)] ISymbol typeParameterContext)
			{
				List<ISymbol> list = DocumentationCommentId.s_symbolListPool.Allocate();
				ITypeSymbol typeSymbol;
				try
				{
					DocumentationCommentId.Parser.ParseTypeSymbol(id, ref index, compilation, typeParameterContext, list);
					if (list.Count == 0)
					{
						typeSymbol = null;
					}
					else
					{
						typeSymbol = (ITypeSymbol)list[0];
					}
				}
				finally
				{
					DocumentationCommentId.s_symbolListPool.ClearAndFree(list);
				}
				return typeSymbol;
			}

			// Token: 0x060035CD RID: 13773 RVA: 0x0009757C File Offset: 0x0009577C
			private static void ParseTypeSymbol(string id, ref int index, Compilation compilation, [Nullable(2)] ISymbol typeParameterContext, List<ISymbol> results)
			{
				char c = DocumentationCommentId.Parser.PeekNextChar(id, index);
				if ((c == 'M' || c == 'T') && DocumentationCommentId.Parser.PeekNextChar(id, index + 1) == ':')
				{
					List<ISymbol> list = DocumentationCommentId.s_symbolListPool.Allocate();
					try
					{
						DocumentationCommentId.Parser.ParseDeclaredId(id, ref index, compilation, list);
						if (list.Count == 0)
						{
							return;
						}
						if (DocumentationCommentId.Parser.PeekNextChar(id, index) == ':')
						{
							index++;
							int num = index;
							using (List<ISymbol>.Enumerator enumerator = list.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									ISymbol symbol = enumerator.Current;
									index = num;
									DocumentationCommentId.Parser.ParseTypeSymbol(id, ref index, compilation, symbol, results);
								}
								return;
							}
						}
						results.AddRange(Enumerable.OfType<ITypeSymbol>(list));
						return;
					}
					finally
					{
						DocumentationCommentId.s_symbolListPool.ClearAndFree(list);
					}
				}
				if (c == '`')
				{
					DocumentationCommentId.Parser.ParseTypeParameterSymbol(id, ref index, typeParameterContext, results);
				}
				else
				{
					DocumentationCommentId.Parser.ParseNamedTypeSymbol(id, ref index, compilation, typeParameterContext, results);
				}
				int num2 = index;
				int num3 = index;
				for (int i = 0; i < results.Count; i++)
				{
					index = num2;
					ITypeSymbol typeSymbol = (ITypeSymbol)results[i];
					for (;;)
					{
						if (DocumentationCommentId.Parser.PeekNextChar(id, index) == '[')
						{
							int num4 = DocumentationCommentId.Parser.ParseArrayBounds(id, ref index);
							typeSymbol = compilation.CreateArrayTypeSymbol(typeSymbol, num4);
						}
						else
						{
							if (DocumentationCommentId.Parser.PeekNextChar(id, index) != '*')
							{
								break;
							}
							index++;
							typeSymbol = compilation.CreatePointerTypeSymbol(typeSymbol);
						}
					}
					results[i] = typeSymbol;
					num3 = index;
				}
				index = num3;
			}

			// Token: 0x060035CE RID: 13774 RVA: 0x00097700 File Offset: 0x00095900
			private static void ParseTypeParameterSymbol(string id, ref int index, [Nullable(2)] ISymbol typeParameterContext, List<ISymbol> results)
			{
				index++;
				if (DocumentationCommentId.Parser.PeekNextChar(id, index) == '`')
				{
					index++;
					int num = DocumentationCommentId.Parser.ReadNextInteger(id, ref index);
					IMethodSymbol methodSymbol = typeParameterContext as IMethodSymbol;
					if (methodSymbol != null)
					{
						int length = methodSymbol.TypeParameters.Length;
						if (length > 0 && num < length)
						{
							results.Add(methodSymbol.TypeParameters[num]);
							return;
						}
					}
				}
				else
				{
					int num2 = DocumentationCommentId.Parser.ReadNextInteger(id, ref index);
					IMethodSymbol methodSymbol2 = typeParameterContext as IMethodSymbol;
					INamedTypeSymbol namedTypeSymbol = ((methodSymbol2 != null) ? methodSymbol2.ContainingType : (typeParameterContext as INamedTypeSymbol));
					if (namedTypeSymbol != null)
					{
						results.Add(DocumentationCommentId.Parser.GetNthTypeParameter(namedTypeSymbol, num2));
					}
				}
			}

			// Token: 0x060035CF RID: 13775 RVA: 0x000977A0 File Offset: 0x000959A0
			private static void ParseNamedTypeSymbol(string id, ref int index, Compilation compilation, [Nullable(2)] ISymbol typeParameterContext, List<ISymbol> results)
			{
				List<INamespaceOrTypeSymbol> list = DocumentationCommentId.s_namespaceOrTypeListPool.Allocate();
				try
				{
					list.Add(compilation.GlobalNamespace);
					for (;;)
					{
						string text = DocumentationCommentId.Parser.ParseName(id, ref index);
						List<ITypeSymbol> list2 = null;
						int num = 0;
						if (DocumentationCommentId.Parser.PeekNextChar(id, index) == '{')
						{
							list2 = new List<ITypeSymbol>();
							if (!DocumentationCommentId.Parser.ParseTypeArguments(id, ref index, compilation, typeParameterContext, list2))
							{
								continue;
							}
							num = list2.Count;
						}
						else if (DocumentationCommentId.Parser.PeekNextChar(id, index) == '`')
						{
							index++;
							num = DocumentationCommentId.Parser.ReadNextInteger(id, ref index);
						}
						if (num != 0 || DocumentationCommentId.Parser.PeekNextChar(id, index) != '.')
						{
							DocumentationCommentId.Parser.GetMatchingTypes(list, text, num, results);
							if (num != 0 && list2 != null && list2.Count != 0)
							{
								ITypeSymbol[] array = list2.ToArray();
								for (int i = 0; i < results.Count; i++)
								{
									results[i] = ((INamedTypeSymbol)results[i]).Construct(array);
								}
							}
						}
						else
						{
							DocumentationCommentId.Parser.GetMatchingNamespaceOrTypes(list, text, results);
						}
						if (DocumentationCommentId.Parser.PeekNextChar(id, index) != '.')
						{
							break;
						}
						index++;
						list.Clear();
						DocumentationCommentId.Parser.CopyTo<ISymbol, INamespaceOrTypeSymbol>(results, list);
						results.Clear();
					}
				}
				finally
				{
					DocumentationCommentId.s_namespaceOrTypeListPool.ClearAndFree(list);
				}
			}

			// Token: 0x060035D0 RID: 13776 RVA: 0x000978CC File Offset: 0x00095ACC
			private static int ParseArrayBounds(string id, ref int index)
			{
				index++;
				int num = 0;
				for (;;)
				{
					if (char.IsDigit(DocumentationCommentId.Parser.PeekNextChar(id, index)))
					{
						DocumentationCommentId.Parser.ReadNextInteger(id, ref index);
					}
					if (DocumentationCommentId.Parser.PeekNextChar(id, index) == ':')
					{
						index++;
						if (char.IsDigit(DocumentationCommentId.Parser.PeekNextChar(id, index)))
						{
							DocumentationCommentId.Parser.ReadNextInteger(id, ref index);
						}
					}
					num++;
					if (DocumentationCommentId.Parser.PeekNextChar(id, index) != ',')
					{
						break;
					}
					index++;
				}
				if (DocumentationCommentId.Parser.PeekNextChar(id, index) == ']')
				{
					index++;
				}
				return num;
			}

			// Token: 0x060035D1 RID: 13777 RVA: 0x0009794C File Offset: 0x00095B4C
			private static bool ParseTypeArguments(string id, ref int index, Compilation compilation, [Nullable(2)] ISymbol typeParameterContext, List<ITypeSymbol> typeArguments)
			{
				index++;
				for (;;)
				{
					ITypeSymbol typeSymbol = DocumentationCommentId.Parser.ParseTypeSymbol(id, ref index, compilation, typeParameterContext);
					if (typeSymbol == null)
					{
						break;
					}
					typeArguments.Add(typeSymbol);
					if (DocumentationCommentId.Parser.PeekNextChar(id, index) != ',')
					{
						goto IL_0031;
					}
					index++;
				}
				return false;
				IL_0031:
				if (DocumentationCommentId.Parser.PeekNextChar(id, index) == '}')
				{
					index++;
				}
				return true;
			}

			// Token: 0x060035D2 RID: 13778 RVA: 0x000979A0 File Offset: 0x00095BA0
			private static void GetMatchingTypes(List<INamespaceOrTypeSymbol> containers, string memberName, int arity, List<ISymbol> results)
			{
				int i = 0;
				int count = containers.Count;
				while (i < count)
				{
					DocumentationCommentId.Parser.GetMatchingTypes(containers[i], memberName, arity, results);
					i++;
				}
			}

			// Token: 0x060035D3 RID: 13779 RVA: 0x000979D0 File Offset: 0x00095BD0
			private static void GetMatchingTypes(INamespaceOrTypeSymbol container, string memberName, int arity, List<ISymbol> results)
			{
				foreach (ISymbol symbol in container.GetMembers(memberName))
				{
					if (symbol.Kind == SymbolKind.NamedType)
					{
						INamedTypeSymbol namedTypeSymbol = (INamedTypeSymbol)symbol;
						if (namedTypeSymbol.Arity == arity)
						{
							results.Add(namedTypeSymbol);
						}
					}
				}
			}

			// Token: 0x060035D4 RID: 13780 RVA: 0x00097A24 File Offset: 0x00095C24
			private static void GetMatchingNamespaceOrTypes(List<INamespaceOrTypeSymbol> containers, string memberName, List<ISymbol> results)
			{
				int i = 0;
				int count = containers.Count;
				while (i < count)
				{
					DocumentationCommentId.Parser.GetMatchingNamespaceOrTypes(containers[i], memberName, results);
					i++;
				}
			}

			// Token: 0x060035D5 RID: 13781 RVA: 0x00097A54 File Offset: 0x00095C54
			private static void GetMatchingNamespaceOrTypes(INamespaceOrTypeSymbol container, string memberName, List<ISymbol> results)
			{
				foreach (ISymbol symbol in container.GetMembers(memberName))
				{
					if (symbol.Kind == SymbolKind.Namespace || (symbol.Kind == SymbolKind.NamedType && ((INamedTypeSymbol)symbol).Arity == 0))
					{
						results.Add(symbol);
					}
				}
			}

			// Token: 0x060035D6 RID: 13782 RVA: 0x00097AAC File Offset: 0x00095CAC
			private static void GetMatchingNamespaces(List<INamespaceOrTypeSymbol> containers, string memberName, List<ISymbol> results)
			{
				int i = 0;
				int count = containers.Count;
				while (i < count)
				{
					DocumentationCommentId.Parser.GetMatchingNamespaces(containers[i], memberName, results);
					i++;
				}
			}

			// Token: 0x060035D7 RID: 13783 RVA: 0x00097ADC File Offset: 0x00095CDC
			private static void GetMatchingNamespaces(INamespaceOrTypeSymbol container, string memberName, List<ISymbol> results)
			{
				foreach (ISymbol symbol in container.GetMembers(memberName))
				{
					if (symbol.Kind == SymbolKind.Namespace)
					{
						results.Add(symbol);
					}
				}
			}

			// Token: 0x060035D8 RID: 13784 RVA: 0x00097B20 File Offset: 0x00095D20
			private static void GetMatchingMethods(string id, ref int index, List<INamespaceOrTypeSymbol> containers, string memberName, int arity, Compilation compilation, List<ISymbol> results)
			{
				List<DocumentationCommentId.Parser.ParameterInfo> list = DocumentationCommentId.Parser.s_parameterListPool.Allocate();
				try
				{
					int num = index;
					int num2 = index;
					int i = 0;
					int count = containers.Count;
					while (i < count)
					{
						foreach (ISymbol symbol in containers[i].GetMembers(memberName))
						{
							index = num;
							IMethodSymbol methodSymbol = symbol as IMethodSymbol;
							if (methodSymbol != null && methodSymbol.Arity == arity)
							{
								list.Clear();
								if ((DocumentationCommentId.Parser.PeekNextChar(id, index) != '(' || DocumentationCommentId.Parser.ParseParameterList(id, ref index, compilation, methodSymbol, list)) && DocumentationCommentId.Parser.AllParametersMatch(methodSymbol.Parameters, list))
								{
									if (DocumentationCommentId.Parser.PeekNextChar(id, index) == '~')
									{
										index++;
										ITypeSymbol typeSymbol = DocumentationCommentId.Parser.ParseTypeSymbol(id, ref index, compilation, methodSymbol);
										if (typeSymbol != null && methodSymbol.ReturnType.Equals(typeSymbol, SymbolEqualityComparer.CLRSignature))
										{
											results.Add(methodSymbol);
											num2 = index;
										}
									}
									else
									{
										results.Add(methodSymbol);
										num2 = index;
									}
								}
							}
						}
						i++;
					}
					index = num2;
				}
				finally
				{
					DocumentationCommentId.Parser.s_parameterListPool.ClearAndFree(list);
				}
			}

			// Token: 0x060035D9 RID: 13785 RVA: 0x00097C4C File Offset: 0x00095E4C
			private static void GetMatchingProperties(string id, ref int index, List<INamespaceOrTypeSymbol> containers, string memberName, Compilation compilation, List<ISymbol> results)
			{
				int num = index;
				int num2 = index;
				List<DocumentationCommentId.Parser.ParameterInfo> list = null;
				try
				{
					int i = 0;
					int count = containers.Count;
					while (i < count)
					{
						memberName = DocumentationCommentId.DecodePropertyName(memberName, compilation.Language);
						foreach (ISymbol symbol in containers[i].GetMembers(memberName))
						{
							index = num;
							IPropertySymbol propertySymbol = symbol as IPropertySymbol;
							if (propertySymbol != null)
							{
								if (DocumentationCommentId.Parser.PeekNextChar(id, index) == '(')
								{
									if (list == null)
									{
										list = DocumentationCommentId.Parser.s_parameterListPool.Allocate();
									}
									else
									{
										list.Clear();
									}
									if (DocumentationCommentId.Parser.ParseParameterList(id, ref index, compilation, propertySymbol.ContainingSymbol, list) && DocumentationCommentId.Parser.AllParametersMatch(propertySymbol.Parameters, list))
									{
										results.Add(propertySymbol);
										num2 = index;
									}
								}
								else if (propertySymbol.Parameters.Length == 0)
								{
									results.Add(propertySymbol);
									num2 = index;
								}
							}
						}
						i++;
					}
					index = num2;
				}
				finally
				{
					if (list != null)
					{
						DocumentationCommentId.Parser.s_parameterListPool.ClearAndFree(list);
					}
				}
			}

			// Token: 0x060035DA RID: 13786 RVA: 0x00097D60 File Offset: 0x00095F60
			private static void GetMatchingFields(List<INamespaceOrTypeSymbol> containers, string memberName, List<ISymbol> results)
			{
				int i = 0;
				int count = containers.Count;
				while (i < count)
				{
					foreach (ISymbol symbol in containers[i].GetMembers(memberName))
					{
						if (symbol.Kind == SymbolKind.Field)
						{
							results.Add(symbol);
						}
					}
					i++;
				}
			}

			// Token: 0x060035DB RID: 13787 RVA: 0x00097DBC File Offset: 0x00095FBC
			private static void GetMatchingEvents(List<INamespaceOrTypeSymbol> containers, string memberName, List<ISymbol> results)
			{
				int i = 0;
				int count = containers.Count;
				while (i < count)
				{
					foreach (ISymbol symbol in containers[i].GetMembers(memberName))
					{
						if (symbol.Kind == SymbolKind.Event)
						{
							results.Add(symbol);
						}
					}
					i++;
				}
			}

			// Token: 0x060035DC RID: 13788 RVA: 0x00097E18 File Offset: 0x00096018
			private static bool AllParametersMatch([Nullable(new byte[] { 0, 1 })] ImmutableArray<IParameterSymbol> symbolParameters, List<DocumentationCommentId.Parser.ParameterInfo> expectedParameters)
			{
				if (symbolParameters.Length != expectedParameters.Count)
				{
					return false;
				}
				for (int i = 0; i < expectedParameters.Count; i++)
				{
					if (!DocumentationCommentId.Parser.ParameterMatches(symbolParameters[i], expectedParameters[i]))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x060035DD RID: 13789 RVA: 0x00097E60 File Offset: 0x00096060
			private static bool ParameterMatches(IParameterSymbol symbol, DocumentationCommentId.Parser.ParameterInfo parameterInfo)
			{
				if (symbol.RefKind == RefKind.None == parameterInfo.IsRefOrOut)
				{
					return false;
				}
				ITypeSymbol type = parameterInfo.Type;
				return type != null && symbol.Type.Equals(type, SymbolEqualityComparer.CLRSignature);
			}

			// Token: 0x060035DE RID: 13790 RVA: 0x00097EA0 File Offset: 0x000960A0
			private static ITypeParameterSymbol GetNthTypeParameter(INamedTypeSymbol typeSymbol, int n)
			{
				int typeParameterCount = DocumentationCommentId.Parser.GetTypeParameterCount(typeSymbol.ContainingType);
				if (n < typeParameterCount)
				{
					return DocumentationCommentId.Parser.GetNthTypeParameter(typeSymbol.ContainingType, n);
				}
				int num = n - typeParameterCount;
				return typeSymbol.TypeParameters[num];
			}

			// Token: 0x060035DF RID: 13791 RVA: 0x00097EE0 File Offset: 0x000960E0
			private static int GetTypeParameterCount(INamedTypeSymbol typeSymbol)
			{
				if (typeSymbol == null)
				{
					return 0;
				}
				return typeSymbol.TypeParameters.Length + DocumentationCommentId.Parser.GetTypeParameterCount(typeSymbol.ContainingType);
			}

			// Token: 0x060035E0 RID: 13792 RVA: 0x00097F0C File Offset: 0x0009610C
			private static bool ParseParameterList(string id, ref int index, Compilation compilation, ISymbol typeParameterContext, List<DocumentationCommentId.Parser.ParameterInfo> parameters)
			{
				index++;
				if (DocumentationCommentId.Parser.PeekNextChar(id, index) == ')')
				{
					index++;
					return true;
				}
				DocumentationCommentId.Parser.ParameterInfo? parameterInfo = DocumentationCommentId.Parser.ParseParameter(id, ref index, compilation, typeParameterContext);
				if (parameterInfo == null)
				{
					return false;
				}
				parameters.Add(parameterInfo.Value);
				while (DocumentationCommentId.Parser.PeekNextChar(id, index) == ',')
				{
					index++;
					parameterInfo = DocumentationCommentId.Parser.ParseParameter(id, ref index, compilation, typeParameterContext);
					if (parameterInfo == null)
					{
						return false;
					}
					parameters.Add(parameterInfo.Value);
				}
				if (DocumentationCommentId.Parser.PeekNextChar(id, index) == ')')
				{
					index++;
				}
				return true;
			}

			// Token: 0x060035E1 RID: 13793 RVA: 0x00097FA0 File Offset: 0x000961A0
			private static DocumentationCommentId.Parser.ParameterInfo? ParseParameter(string id, ref int index, Compilation compilation, [Nullable(2)] ISymbol typeParameterContext)
			{
				bool flag = false;
				ITypeSymbol typeSymbol = DocumentationCommentId.Parser.ParseTypeSymbol(id, ref index, compilation, typeParameterContext);
				if (typeSymbol == null)
				{
					return default(DocumentationCommentId.Parser.ParameterInfo?);
				}
				if (DocumentationCommentId.Parser.PeekNextChar(id, index) == '@')
				{
					index++;
					flag = true;
				}
				return new DocumentationCommentId.Parser.ParameterInfo?(new DocumentationCommentId.Parser.ParameterInfo(typeSymbol, flag));
			}

			// Token: 0x060035E2 RID: 13794 RVA: 0x00097FE6 File Offset: 0x000961E6
			private static char PeekNextChar(string id, int index)
			{
				if (index < id.Length)
				{
					return id.get_Chars(index);
				}
				return '\0';
			}

			// Token: 0x060035E3 RID: 13795 RVA: 0x00097FFC File Offset: 0x000961FC
			private static string ParseName(string id, ref int index)
			{
				int num = id.IndexOfAny(DocumentationCommentId.Parser.s_nameDelimiters, index);
				string text;
				if (num >= 0)
				{
					text = id.Substring(index, num - index);
					index = num;
				}
				else
				{
					text = id.Substring(index);
					index = id.Length;
				}
				return DocumentationCommentId.Parser.DecodeName(text);
			}

			// Token: 0x060035E4 RID: 13796 RVA: 0x00098044 File Offset: 0x00096244
			private static string DecodeName(string name)
			{
				if (name.IndexOf('#') >= 0)
				{
					return name.Replace('#', '.');
				}
				return name;
			}

			// Token: 0x060035E5 RID: 13797 RVA: 0x00098060 File Offset: 0x00096260
			private static int ReadNextInteger(string id, ref int index)
			{
				int num = 0;
				while (index < id.Length && char.IsDigit(id.get_Chars(index)))
				{
					num = num * 10 + (int)(id.get_Chars(index) - '0');
					index++;
				}
				return num;
			}

			// Token: 0x060035E6 RID: 13798 RVA: 0x000980A4 File Offset: 0x000962A4
			private static void CopyTo<TSource, TDestination>(List<TSource> source, List<TDestination> destination) where TSource : class where TDestination : class
			{
				if (destination.Count + source.Count > destination.Capacity)
				{
					destination.Capacity = destination.Count + source.Count;
				}
				int i = 0;
				int count = source.Count;
				while (i < count)
				{
					destination.Add((TDestination)((object)source[i]));
					i++;
				}
			}

			// Token: 0x0400165A RID: 5722
			private static readonly DocumentationCommentId.ListPool<DocumentationCommentId.Parser.ParameterInfo> s_parameterListPool = new DocumentationCommentId.ListPool<DocumentationCommentId.Parser.ParameterInfo>();

			// Token: 0x0400165B RID: 5723
			private static readonly char[] s_nameDelimiters = new char[]
			{
				':', '.', '(', ')', '{', '}', '[', ']', ',', '\'',
				'@', '*', '`', '~'
			};

			// Token: 0x020007BE RID: 1982
			[Nullable(0)]
			[StructLayout(3)]
			private struct ParameterInfo
			{
				// Token: 0x06003FCE RID: 16334 RVA: 0x000B263C File Offset: 0x000B083C
				public ParameterInfo(ITypeSymbol type, bool isRefOrOut)
				{
					this.Type = type;
					this.IsRefOrOut = isRefOrOut;
				}

				// Token: 0x04001DD4 RID: 7636
				internal readonly ITypeSymbol Type;

				// Token: 0x04001DD5 RID: 7637
				internal readonly bool IsRefOrOut;
			}
		}
	}
}
