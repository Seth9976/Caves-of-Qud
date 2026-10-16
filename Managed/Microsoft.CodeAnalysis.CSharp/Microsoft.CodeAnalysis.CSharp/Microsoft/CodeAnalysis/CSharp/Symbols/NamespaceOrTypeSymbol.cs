using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004FC RID: 1276
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class NamespaceOrTypeSymbol : Symbol, INamespaceOrTypeSymbolInternal, ISymbolInternal
	{
		// Token: 0x06006BFA RID: 27642 RVA: 0x0018F996 File Offset: 0x0018DB96
		internal NamespaceOrTypeSymbol()
		{
		}

		// Token: 0x17001BDC RID: 7132
		// (get) Token: 0x06006BFB RID: 27643 RVA: 0x0018F99E File Offset: 0x0018DB9E
		public bool IsNamespace
		{
			get
			{
				return this.Kind == SymbolKind.Namespace;
			}
		}

		// Token: 0x17001BDD RID: 7133
		// (get) Token: 0x06006BFC RID: 27644 RVA: 0x0018F9AA File Offset: 0x0018DBAA
		public bool IsType
		{
			get
			{
				return !this.IsNamespace;
			}
		}

		// Token: 0x17001BDE RID: 7134
		// (get) Token: 0x06006BFD RID: 27645 RVA: 0x0018F9B5 File Offset: 0x0018DBB5
		public sealed override bool IsVirtual
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001BDF RID: 7135
		// (get) Token: 0x06006BFE RID: 27646 RVA: 0x0018F9B8 File Offset: 0x0018DBB8
		public sealed override bool IsOverride
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001BE0 RID: 7136
		// (get) Token: 0x06006BFF RID: 27647 RVA: 0x0018F9BB File Offset: 0x0018DBBB
		public sealed override bool IsExtern
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006C00 RID: 27648
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public abstract ImmutableArray<Symbol> GetMembers();

		// Token: 0x06006C01 RID: 27649 RVA: 0x0018F9BE File Offset: 0x0018DBBE
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal virtual ImmutableArray<Symbol> GetMembersUnordered()
		{
			return this.GetMembers().ConditionallyDeOrder<Symbol>();
		}

		// Token: 0x06006C02 RID: 27650
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public abstract ImmutableArray<Symbol> GetMembers(string name);

		// Token: 0x06006C03 RID: 27651 RVA: 0x0018F9CB File Offset: 0x0018DBCB
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal virtual ImmutableArray<NamedTypeSymbol> GetTypeMembersUnordered()
		{
			return this.GetTypeMembers().ConditionallyDeOrder<NamedTypeSymbol>();
		}

		// Token: 0x06006C04 RID: 27652
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public abstract ImmutableArray<NamedTypeSymbol> GetTypeMembers();

		// Token: 0x06006C05 RID: 27653
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public abstract ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name);

		// Token: 0x06006C06 RID: 27654 RVA: 0x0018F9D8 File Offset: 0x0018DBD8
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public virtual ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name, int arity)
		{
			return this.GetTypeMembers(name).WhereAsArray((NamedTypeSymbol t, int arity) => t.Arity == arity, arity);
		}

		// Token: 0x06006C07 RID: 27655 RVA: 0x0018FA08 File Offset: 0x0018DC08
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal SourceNamedTypeSymbol GetSourceTypeMember(TypeDeclarationSyntax syntax)
		{
			return this.GetSourceTypeMember(syntax.Identifier.ValueText, syntax.Arity, syntax.Kind(), syntax);
		}

		// Token: 0x06006C08 RID: 27656 RVA: 0x0018FA38 File Offset: 0x0018DC38
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal SourceNamedTypeSymbol GetSourceTypeMember(DelegateDeclarationSyntax syntax)
		{
			return this.GetSourceTypeMember(syntax.Identifier.ValueText, syntax.Arity, syntax.Kind(), syntax);
		}

		// Token: 0x06006C09 RID: 27657 RVA: 0x0018FA68 File Offset: 0x0018DC68
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal SourceNamedTypeSymbol GetSourceTypeMember(string name, int arity, SyntaxKind kind, CSharpSyntaxNode syntax)
		{
			TypeKind typeKind = kind.ToDeclarationKind().ToTypeKind();
			foreach (NamedTypeSymbol namedTypeSymbol in this.GetTypeMembers(name, arity))
			{
				SourceNamedTypeSymbol sourceNamedTypeSymbol = namedTypeSymbol as SourceNamedTypeSymbol;
				if (sourceNamedTypeSymbol != null && sourceNamedTypeSymbol.TypeKind == typeKind)
				{
					if (syntax == null)
					{
						return sourceNamedTypeSymbol;
					}
					foreach (Location location in sourceNamedTypeSymbol.Locations)
					{
						if (location.IsInSource && location.SourceTree == syntax.SyntaxTree && syntax.Span.Contains(location.SourceSpan))
						{
							return sourceNamedTypeSymbol;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06006C0A RID: 27658 RVA: 0x0018FB1C File Offset: 0x0018DD1C
		internal virtual NamedTypeSymbol LookupMetadataType(ref MetadataTypeName emittedTypeName)
		{
			if (this.Kind == SymbolKind.ErrorType)
			{
				return new MissingMetadataTypeSymbol.Nested((NamedTypeSymbol)this, ref emittedTypeName);
			}
			NamedTypeSymbol namedTypeSymbol = null;
			bool isNamespace = this.IsNamespace;
			if (emittedTypeName.IsMangled && (emittedTypeName.ForcedArity == -1 || emittedTypeName.ForcedArity == emittedTypeName.InferredArity))
			{
				foreach (NamedTypeSymbol namedTypeSymbol2 in this.GetTypeMembers(emittedTypeName.UnmangledTypeName))
				{
					if (emittedTypeName.InferredArity == namedTypeSymbol2.Arity && namedTypeSymbol2.MangleName)
					{
						if (namedTypeSymbol != null)
						{
							namedTypeSymbol = null;
							break;
						}
						namedTypeSymbol = namedTypeSymbol2;
					}
				}
			}
			int num = emittedTypeName.ForcedArity;
			if (emittedTypeName.UseCLSCompliantNameArityEncoding)
			{
				if (emittedTypeName.InferredArity > 0)
				{
					goto IL_0100;
				}
				if (num == -1)
				{
					num = 0;
				}
				else if (num != 0)
				{
					goto IL_0100;
				}
			}
			foreach (NamedTypeSymbol namedTypeSymbol3 in this.GetTypeMembers(emittedTypeName.TypeName))
			{
				if (!namedTypeSymbol3.MangleName && (num == -1 || num == namedTypeSymbol3.Arity))
				{
					if (namedTypeSymbol != null)
					{
						namedTypeSymbol = null;
						break;
					}
					namedTypeSymbol = namedTypeSymbol3;
				}
			}
			IL_0100:
			if (namedTypeSymbol != null)
			{
				return namedTypeSymbol;
			}
			if (isNamespace)
			{
				return new MissingMetadataTypeSymbol.TopLevel(this.ContainingModule, ref emittedTypeName, null);
			}
			return new MissingMetadataTypeSymbol.Nested((NamedTypeSymbol)this, ref emittedTypeName);
		}

		// Token: 0x06006C0B RID: 27659 RVA: 0x0018FC4C File Offset: 0x0018DE4C
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })]
		internal IEnumerable<NamespaceOrTypeSymbol> GetNamespaceOrTypeByQualifiedName(IEnumerable<string> qualifiedName)
		{
			NamespaceOrTypeSymbol namespaceOrTypeSymbol = this;
			IEnumerable<NamespaceOrTypeSymbol> enumerable = null;
			foreach (string text in qualifiedName)
			{
				if (enumerable != null)
				{
					namespaceOrTypeSymbol = enumerable.OfMinimalArity();
					if (namespaceOrTypeSymbol == null)
					{
						return SpecializedCollections.EmptyEnumerable<NamespaceOrTypeSymbol>();
					}
				}
				enumerable = namespaceOrTypeSymbol.GetMembers(text).OfType<NamespaceOrTypeSymbol>();
			}
			return enumerable;
		}
	}
}
