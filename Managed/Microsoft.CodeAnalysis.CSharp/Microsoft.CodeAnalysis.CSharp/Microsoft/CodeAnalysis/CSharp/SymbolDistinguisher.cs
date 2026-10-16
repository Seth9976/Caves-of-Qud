using System;
using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000222 RID: 546
	internal sealed class SymbolDistinguisher
	{
		// Token: 0x060027EA RID: 10218 RVA: 0x000DA812 File Offset: 0x000D8A12
		public SymbolDistinguisher(CSharpCompilation compilation, Symbol symbol0, Symbol symbol1)
		{
			this._compilation = compilation;
			this._symbol0 = symbol0;
			this._symbol1 = symbol1;
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x060027EB RID: 10219 RVA: 0x000DA82F File Offset: 0x000D8A2F
		public IFormattable First
		{
			get
			{
				return new SymbolDistinguisher.Description(this, 0);
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x060027EC RID: 10220 RVA: 0x000DA838 File Offset: 0x000D8A38
		public IFormattable Second
		{
			get
			{
				return new SymbolDistinguisher.Description(this, 1);
			}
		}

		// Token: 0x060027ED RID: 10221 RVA: 0x000DA844 File Offset: 0x000D8A44
		[Conditional("DEBUG")]
		private static void CheckSymbolKind(Symbol symbol)
		{
			switch (symbol.Kind)
			{
			default:
				throw ExceptionUtilities.UnexpectedValue(symbol.Kind);
			case SymbolKind.ArrayType:
			case SymbolKind.DynamicType:
			case SymbolKind.ErrorType:
			case SymbolKind.Event:
			case SymbolKind.Field:
			case SymbolKind.Method:
			case SymbolKind.NamedType:
			case SymbolKind.Parameter:
			case SymbolKind.PointerType:
			case SymbolKind.Property:
			case SymbolKind.TypeParameter:
			case SymbolKind.FunctionPointerType:
				return;
			}
		}

		// Token: 0x060027EE RID: 10222 RVA: 0x000DA8C4 File Offset: 0x000D8AC4
		private void MakeDescriptions()
		{
			if (!this._lazyDescriptions.IsDefault)
			{
				return;
			}
			string text = this._symbol0.ToDisplayString(null);
			string text2 = this._symbol1.ToDisplayString(null);
			if (text == text2)
			{
				Symbol symbol = SymbolDistinguisher.UnwrapSymbol(this._symbol0);
				Symbol symbol2 = SymbolDistinguisher.UnwrapSymbol(this._symbol1);
				string text3 = SymbolDistinguisher.GetLocationString(this._compilation, symbol);
				string text4 = SymbolDistinguisher.GetLocationString(this._compilation, symbol2);
				if (text3 == text4)
				{
					AssemblySymbol containingAssembly = symbol.ContainingAssembly;
					AssemblySymbol containingAssembly2 = symbol2.ContainingAssembly;
					if (containingAssembly != null && containingAssembly2 != null)
					{
						text3 = containingAssembly.Identity.ToString();
						text4 = containingAssembly2.Identity.ToString();
					}
				}
				if (text3 != text4)
				{
					if (text3 != null)
					{
						text = text + " [" + text3 + "]";
					}
					if (text4 != null)
					{
						text2 = text2 + " [" + text4 + "]";
					}
				}
			}
			if (!this._lazyDescriptions.IsDefault)
			{
				return;
			}
			ImmutableInterlocked.InterlockedInitialize<string>(ref this._lazyDescriptions, ImmutableArray.Create<string>(text, text2));
		}

		// Token: 0x060027EF RID: 10223 RVA: 0x000DA9D4 File Offset: 0x000D8BD4
		private static Symbol UnwrapSymbol(Symbol symbol)
		{
			for (;;)
			{
				SymbolKind kind = symbol.Kind;
				if (kind != SymbolKind.ArrayType)
				{
					if (kind != SymbolKind.Parameter)
					{
						if (kind != SymbolKind.PointerType)
						{
							break;
						}
						symbol = ((PointerTypeSymbol)symbol).PointedAtType;
					}
					else
					{
						symbol = ((ParameterSymbol)symbol).Type;
					}
				}
				else
				{
					symbol = ((ArrayTypeSymbol)symbol).ElementType;
				}
			}
			return symbol;
		}

		// Token: 0x060027F0 RID: 10224 RVA: 0x000DAA28 File Offset: 0x000D8C28
		private static string GetLocationString(CSharpCompilation compilation, Symbol unwrappedSymbol)
		{
			ImmutableArray<SyntaxReference> declaringSyntaxReferences = unwrappedSymbol.DeclaringSyntaxReferences;
			if (declaringSyntaxReferences.Length > 0)
			{
				SyntaxTree syntaxTree = declaringSyntaxReferences[0].SyntaxTree;
				TextSpan span = declaringSyntaxReferences[0].Span;
				string displayPath = syntaxTree.GetDisplayPath(span, (compilation != null) ? compilation.Options.SourceReferenceResolver : null);
				if (!string.IsNullOrEmpty(displayPath))
				{
					return string.Format("{0}({1})", displayPath, syntaxTree.GetDisplayLineNumber(span));
				}
			}
			AssemblySymbol containingAssembly = unwrappedSymbol.ContainingAssembly;
			if (containingAssembly != null)
			{
				if (compilation != null)
				{
					PortableExecutableReference portableExecutableReference = compilation.GetMetadataReference(containingAssembly) as PortableExecutableReference;
					if (portableExecutableReference != null)
					{
						string filePath = portableExecutableReference.FilePath;
						if (!string.IsNullOrEmpty(filePath))
						{
							return filePath;
						}
					}
				}
				return containingAssembly.Identity.ToString();
			}
			return null;
		}

		// Token: 0x060027F1 RID: 10225 RVA: 0x000DAADF File Offset: 0x000D8CDF
		private string GetDescription(int index)
		{
			this.MakeDescriptions();
			return this._lazyDescriptions[index];
		}

		// Token: 0x0400113F RID: 4415
		private readonly CSharpCompilation _compilation;

		// Token: 0x04001140 RID: 4416
		private readonly Symbol _symbol0;

		// Token: 0x04001141 RID: 4417
		private readonly Symbol _symbol1;

		// Token: 0x04001142 RID: 4418
		private ImmutableArray<string> _lazyDescriptions;

		// Token: 0x02000757 RID: 1879
		private sealed class Description : IFormattable
		{
			// Token: 0x060086C4 RID: 34500 RVA: 0x001E8EFE File Offset: 0x001E70FE
			public Description(SymbolDistinguisher distinguisher, int index)
			{
				this._distinguisher = distinguisher;
				this._index = index;
			}

			// Token: 0x060086C5 RID: 34501 RVA: 0x001E8F14 File Offset: 0x001E7114
			private Symbol GetSymbol()
			{
				if (this._index != 0)
				{
					return this._distinguisher._symbol1;
				}
				return this._distinguisher._symbol0;
			}

			// Token: 0x060086C6 RID: 34502 RVA: 0x001E8F38 File Offset: 0x001E7138
			public override bool Equals(object obj)
			{
				SymbolDistinguisher.Description description = obj as SymbolDistinguisher.Description;
				return description != null && this._distinguisher._compilation == description._distinguisher._compilation && this.GetSymbol() == description.GetSymbol();
			}

			// Token: 0x060086C7 RID: 34503 RVA: 0x001E8F7C File Offset: 0x001E717C
			public override int GetHashCode()
			{
				int num = this.GetSymbol().GetHashCode();
				CSharpCompilation compilation = this._distinguisher._compilation;
				if (compilation != null)
				{
					num = Hash.Combine(num, compilation.GetHashCode());
				}
				return num;
			}

			// Token: 0x060086C8 RID: 34504 RVA: 0x001E8FB2 File Offset: 0x001E71B2
			public override string ToString()
			{
				return this._distinguisher.GetDescription(this._index);
			}

			// Token: 0x060086C9 RID: 34505 RVA: 0x001E8FC5 File Offset: 0x001E71C5
			string IFormattable.ToString(string format, IFormatProvider formatProvider)
			{
				return this.ToString();
			}

			// Token: 0x04002351 RID: 9041
			private readonly SymbolDistinguisher _distinguisher;

			// Token: 0x04002352 RID: 9042
			private readonly int _index;
		}
	}
}
