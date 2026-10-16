using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Symbols;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200006D RID: 109
	[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
	internal abstract class Symbol : IReference, ISymbolInternal, IFormattable
	{
		// Token: 0x060009E2 RID: 2530 RVA: 0x0004D484 File Offset: 0x0004B684
		public static bool IsSymbolAccessible(Symbol symbol, NamedTypeSymbol within, NamedTypeSymbol throughTypeOpt = null)
		{
			if (symbol == null)
			{
				throw new ArgumentNullException("symbol");
			}
			if (within == null)
			{
				throw new ArgumentNullException("within");
			}
			CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
			return AccessCheck.IsSymbolAccessible(symbol, within, ref discarded, throughTypeOpt);
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x0004D4C0 File Offset: 0x0004B6C0
		public static bool IsSymbolAccessible(Symbol symbol, AssemblySymbol within)
		{
			if (symbol == null)
			{
				throw new ArgumentNullException("symbol");
			}
			if (within == null)
			{
				throw new ArgumentNullException("within");
			}
			CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
			return AccessCheck.IsSymbolAccessible(symbol, within, ref discarded);
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x0004D4F8 File Offset: 0x0004B6F8
		IDefinition IReference.AsDefinition(EmitContext context)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0004D4FF File Offset: 0x0004B6FF
		ISymbolInternal IReference.GetInternalSymbol()
		{
			return this.AdaptedSymbol;
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x0004D507 File Offset: 0x0004B707
		void IReference.Dispatch(MetadataVisitor visitor)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x0004D50E File Offset: 0x0004B70E
		IEnumerable<ICustomAttribute> IReference.GetAttributes(EmitContext context)
		{
			return this.AdaptedSymbol.GetCustomAttributesToEmit((PEModuleBuilder)context.Module);
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x0004D526 File Offset: 0x0004B726
		internal Symbol AdaptedSymbol
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0004D529 File Offset: 0x0004B729
		internal Symbol GetCciAdapter()
		{
			return this;
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x0004D52C File Offset: 0x0004B72C
		[Conditional("DEBUG")]
		protected internal void CheckDefinitionInvariant()
		{
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x0004D52E File Offset: 0x0004B72E
		IReference ISymbolInternal.GetCciAdapter()
		{
			return this.GetCciAdapter();
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x0004D536 File Offset: 0x0004B736
		internal bool IsDefinitionOrDistinct()
		{
			return this.IsDefinition || !this.Equals(this.OriginalDefinition, SymbolEqualityComparer.ConsiderEverything.CompareKind);
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x0004D55B File Offset: 0x0004B75B
		internal virtual IEnumerable<CSharpAttributeData> GetCustomAttributesToEmit(PEModuleBuilder moduleBuilder)
		{
			return this.GetCustomAttributesToEmit(moduleBuilder, false);
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x0004D568 File Offset: 0x0004B768
		internal IEnumerable<CSharpAttributeData> GetCustomAttributesToEmit(PEModuleBuilder moduleBuilder, bool emittingAssemblyAttributesInNetModule)
		{
			ArrayBuilder<SynthesizedAttributeData> arrayBuilder = null;
			ImmutableArray<CSharpAttributeData> attributes = this.GetAttributes();
			this.AddSynthesizedAttributes(moduleBuilder, ref arrayBuilder);
			return this.GetCustomAttributesToEmit(attributes, arrayBuilder, false, emittingAssemblyAttributesInNetModule);
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0004D591 File Offset: 0x0004B791
		internal IEnumerable<CSharpAttributeData> GetCustomAttributesToEmit(ImmutableArray<CSharpAttributeData> userDefined, ArrayBuilder<SynthesizedAttributeData> synthesized, bool isReturnType, bool emittingAssemblyAttributesInNetModule)
		{
			if (userDefined.IsEmpty && synthesized == null)
			{
				return SpecializedCollections.EmptyEnumerable<CSharpAttributeData>();
			}
			return this.GetCustomAttributesToEmitIterator(userDefined, synthesized, isReturnType, emittingAssemblyAttributesInNetModule);
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x0004D5B0 File Offset: 0x0004B7B0
		private IEnumerable<CSharpAttributeData> GetCustomAttributesToEmitIterator(ImmutableArray<CSharpAttributeData> userDefined, ArrayBuilder<SynthesizedAttributeData> synthesized, bool isReturnType, bool emittingAssemblyAttributesInNetModule)
		{
			if (synthesized != null)
			{
				foreach (SynthesizedAttributeData synthesizedAttributeData in synthesized)
				{
					yield return synthesizedAttributeData;
				}
				ArrayBuilder<SynthesizedAttributeData>.Enumerator enumerator = default(ArrayBuilder<SynthesizedAttributeData>.Enumerator);
				synthesized.Free();
			}
			int num;
			for (int i = 0; i < userDefined.Length; i = num + 1)
			{
				CSharpAttributeData csharpAttributeData = userDefined[i];
				if ((this.Kind != SymbolKind.Assembly || !((SourceAssemblySymbol)this).IsIndexOfOmittedAssemblyAttribute(i)) && csharpAttributeData.ShouldEmitAttribute(this, isReturnType, emittingAssemblyAttributesInNetModule))
				{
					yield return csharpAttributeData;
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060009F1 RID: 2545 RVA: 0x0004D5DD File Offset: 0x0004B7DD
		internal virtual bool RequiresCompletion
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x0004D5E0 File Offset: 0x0004B7E0
		internal virtual void ForceComplete(SourceLocation locationOpt, CancellationToken cancellationToken)
		{
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x0004D5E2 File Offset: 0x0004B7E2
		internal virtual bool HasComplete(CompletionPart part)
		{
			return true;
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x0004D5E5 File Offset: 0x0004B7E5
		public virtual string Name
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060009F5 RID: 2549 RVA: 0x0004D5EC File Offset: 0x0004B7EC
		public virtual string MetadataName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060009F6 RID: 2550
		public abstract SymbolKind Kind { get; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060009F7 RID: 2551
		public abstract Symbol ContainingSymbol { get; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060009F8 RID: 2552 RVA: 0x0004D5F4 File Offset: 0x0004B7F4
		public virtual NamedTypeSymbol ContainingType
		{
			get
			{
				Symbol containingSymbol = this.ContainingSymbol;
				NamedTypeSymbol namedTypeSymbol = containingSymbol as NamedTypeSymbol;
				if (namedTypeSymbol == containingSymbol)
				{
					return namedTypeSymbol;
				}
				return containingSymbol.ContainingType;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060009F9 RID: 2553 RVA: 0x0004D61C File Offset: 0x0004B81C
		public virtual NamespaceSymbol ContainingNamespace
		{
			get
			{
				for (Symbol symbol = this.ContainingSymbol; symbol != null; symbol = symbol.ContainingSymbol)
				{
					NamespaceSymbol namespaceSymbol = symbol as NamespaceSymbol;
					if (namespaceSymbol != null)
					{
						return namespaceSymbol;
					}
				}
				return null;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x0004D64C File Offset: 0x0004B84C
		public virtual AssemblySymbol ContainingAssembly
		{
			get
			{
				Symbol containingSymbol = this.ContainingSymbol;
				if (containingSymbol == null)
				{
					return null;
				}
				return containingSymbol.ContainingAssembly;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x0004D66C File Offset: 0x0004B86C
		internal virtual CSharpCompilation DeclaringCompilation
		{
			get
			{
				SymbolKind kind = this.Kind;
				if (kind == SymbolKind.Assembly)
				{
					return null;
				}
				if (kind == SymbolKind.ErrorType)
				{
					return null;
				}
				if (kind == SymbolKind.NetModule)
				{
					return null;
				}
				SourceModuleSymbol sourceModuleSymbol = this.ContainingModule as SourceModuleSymbol;
				if (sourceModuleSymbol != null)
				{
					return sourceModuleSymbol.DeclaringCompilation;
				}
				return null;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060009FC RID: 2556 RVA: 0x0004D6AC File Offset: 0x0004B8AC
		Compilation ISymbolInternal.DeclaringCompilation
		{
			get
			{
				return this.DeclaringCompilation;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x0004D6B4 File Offset: 0x0004B8B4
		string ISymbolInternal.Name
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060009FE RID: 2558 RVA: 0x0004D6BC File Offset: 0x0004B8BC
		string ISymbolInternal.MetadataName
		{
			get
			{
				return this.MetadataName;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x0004D6C4 File Offset: 0x0004B8C4
		ISymbolInternal ISymbolInternal.ContainingSymbol
		{
			get
			{
				return this.ContainingSymbol;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x0004D6CC File Offset: 0x0004B8CC
		IModuleSymbolInternal ISymbolInternal.ContainingModule
		{
			get
			{
				return this.ContainingModule;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x0004D6D4 File Offset: 0x0004B8D4
		IAssemblySymbolInternal ISymbolInternal.ContainingAssembly
		{
			get
			{
				return this.ContainingAssembly;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000A02 RID: 2562 RVA: 0x0004D6DC File Offset: 0x0004B8DC
		ImmutableArray<Location> ISymbolInternal.Locations
		{
			get
			{
				return this.Locations;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x0004D6E4 File Offset: 0x0004B8E4
		INamespaceSymbolInternal ISymbolInternal.ContainingNamespace
		{
			get
			{
				return this.ContainingNamespace;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000A04 RID: 2564 RVA: 0x0004D6EC File Offset: 0x0004B8EC
		bool ISymbolInternal.IsImplicitlyDeclared
		{
			get
			{
				return this.IsImplicitlyDeclared;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x0004D6F4 File Offset: 0x0004B8F4
		INamedTypeSymbolInternal ISymbolInternal.ContainingType
		{
			get
			{
				return this.ContainingType;
			}
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x0004D6FC File Offset: 0x0004B8FC
		ISymbol ISymbolInternal.GetISymbol()
		{
			return this.ISymbol;
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x0004D704 File Offset: 0x0004B904
		internal virtual ModuleSymbol ContainingModule
		{
			get
			{
				Symbol containingSymbol = this.ContainingSymbol;
				if (containingSymbol == null)
				{
					return null;
				}
				return containingSymbol.ContainingModule;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x0004D724 File Offset: 0x0004B924
		internal virtual int? MemberIndexOpt
		{
			get
			{
				return default(int?);
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x0004D73A File Offset: 0x0004B93A
		public Symbol OriginalDefinition
		{
			get
			{
				return this.OriginalSymbolDefinition;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x0004D742 File Offset: 0x0004B942
		protected virtual Symbol OriginalSymbolDefinition
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x0004D745 File Offset: 0x0004B945
		public bool IsDefinition
		{
			get
			{
				return this == this.OriginalDefinition;
			}
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x0004D750 File Offset: 0x0004B950
		internal virtual LexicalSortKey GetLexicalSortKey()
		{
			ImmutableArray<Location> locations = this.Locations;
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			if (locations.Length <= 0)
			{
				return LexicalSortKey.NotInSource;
			}
			return new LexicalSortKey(locations[0], declaringCompilation);
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000A0D RID: 2573
		public abstract ImmutableArray<Location> Locations { get; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000A0E RID: 2574
		public abstract ImmutableArray<SyntaxReference> DeclaringSyntaxReferences { get; }

		// Token: 0x06000A0F RID: 2575 RVA: 0x0004D78C File Offset: 0x0004B98C
		internal static ImmutableArray<SyntaxReference> GetDeclaringSyntaxReferenceHelper<TNode>(ImmutableArray<Location> locations) where TNode : CSharpSyntaxNode
		{
			if (locations.IsEmpty)
			{
				return ImmutableArray<SyntaxReference>.Empty;
			}
			ArrayBuilder<SyntaxReference> instance = ArrayBuilder<SyntaxReference>.GetInstance();
			ImmutableArray<Location>.Enumerator enumerator = locations.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Location location = enumerator.Current;
				if (!(location == null) && location.IsInSource)
				{
					if (location.SourceSpan.Length != 0)
					{
						SyntaxToken syntaxToken = location.SourceTree.GetRoot(default(CancellationToken)).FindToken(location.SourceSpan.Start, false);
						if (syntaxToken.Kind() != SyntaxKind.None)
						{
							CSharpSyntaxNode csharpSyntaxNode = syntaxToken.Parent.FirstAncestorOrSelf<TNode>(null, true);
							if (csharpSyntaxNode != null)
							{
								instance.Add(csharpSyntaxNode.GetReference());
							}
						}
					}
					else
					{
						SyntaxNode root = location.SourceTree.GetRoot(default(CancellationToken));
						SyntaxNode syntaxNode = null;
						Func<SyntaxNode, bool> func;
						Func<SyntaxNode, bool> <>9__0;
						if ((func = <>9__0) == null)
						{
							func = (<>9__0 = (SyntaxNode c) => c.Location.SourceSpan.Contains(location.SourceSpan));
						}
						foreach (SyntaxNode syntaxNode2 in root.DescendantNodesAndSelf(func, false))
						{
							if (syntaxNode2 is TNode && syntaxNode2.Location.SourceSpan.Contains(location.SourceSpan))
							{
								syntaxNode = syntaxNode2;
							}
						}
						if (syntaxNode != null)
						{
							instance.Add(syntaxNode.GetReference());
						}
					}
				}
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000A10 RID: 2576
		public abstract Accessibility DeclaredAccessibility { get; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000A11 RID: 2577
		public abstract bool IsStatic { get; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000A12 RID: 2578
		public abstract bool IsVirtual { get; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000A13 RID: 2579
		public abstract bool IsOverride { get; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000A14 RID: 2580
		public abstract bool IsAbstract { get; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000A15 RID: 2581
		public abstract bool IsSealed { get; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000A16 RID: 2582
		public abstract bool IsExtern { get; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000A17 RID: 2583 RVA: 0x0004D944 File Offset: 0x0004BB44
		public virtual bool IsImplicitlyDeclared
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000A18 RID: 2584 RVA: 0x0004D948 File Offset: 0x0004BB48
		public bool CanBeReferencedByName
		{
			get
			{
				switch (this.Kind)
				{
				case SymbolKind.Alias:
				case SymbolKind.Label:
				case SymbolKind.Local:
				case SymbolKind.RangeVariable:
					return true;
				case SymbolKind.ArrayType:
				case SymbolKind.Assembly:
				case SymbolKind.DynamicType:
				case SymbolKind.NetModule:
				case SymbolKind.PointerType:
				case SymbolKind.Discard:
				case SymbolKind.FunctionPointerType:
					return false;
				case SymbolKind.ErrorType:
				case SymbolKind.Event:
				case SymbolKind.Field:
				case SymbolKind.Namespace:
				case SymbolKind.Parameter:
				case SymbolKind.TypeParameter:
					goto IL_0109;
				case SymbolKind.Method:
				{
					MethodSymbol methodSymbol = (MethodSymbol)this;
					MethodKind methodKind = methodSymbol.MethodKind;
					switch (methodKind)
					{
					case MethodKind.DelegateInvoke:
						return true;
					case MethodKind.Destructor:
						return true;
					case MethodKind.EventAdd:
					case MethodKind.EventRaise:
					case MethodKind.EventRemove:
					case MethodKind.ExplicitInterfaceImplementation:
					case MethodKind.UserDefinedOperator:
						break;
					case MethodKind.Ordinary:
					case MethodKind.ReducedExtension:
						goto IL_0109;
					case MethodKind.PropertyGet:
					case MethodKind.PropertySet:
						if (!((PropertySymbol)methodSymbol.AssociatedSymbol).CanCallMethodsDirectly())
						{
							return false;
						}
						goto IL_0109;
					default:
						if (methodKind == MethodKind.LocalFunction)
						{
							goto IL_0109;
						}
						break;
					}
					return false;
				}
				case SymbolKind.NamedType:
					if (((NamedTypeSymbol)this).IsSubmissionClass)
					{
						return false;
					}
					goto IL_0109;
				case SymbolKind.Property:
				{
					PropertySymbol propertySymbol = (PropertySymbol)this;
					if (propertySymbol.IsIndexer || propertySymbol.MustCallMethodsDirectly)
					{
						return false;
					}
					goto IL_0109;
				}
				}
				throw ExceptionUtilities.UnexpectedValue(this.Kind);
				IL_0109:
				return SyntaxFacts.IsValidIdentifier(this.Name) && !SyntaxFacts.ContainsDroppedIdentifierCharacters(this.Name);
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000A19 RID: 2585 RVA: 0x0004DA7C File Offset: 0x0004BC7C
		internal bool CanBeReferencedByNameIgnoringIllegalCharacters
		{
			get
			{
				if (this.Kind == SymbolKind.Method)
				{
					MethodSymbol methodSymbol = (MethodSymbol)this;
					MethodKind methodKind = methodSymbol.MethodKind;
					if (methodKind <= MethodKind.Ordinary)
					{
						if (methodKind - MethodKind.DelegateInvoke > 1 && methodKind != MethodKind.Ordinary)
						{
							return false;
						}
					}
					else
					{
						if (methodKind - MethodKind.PropertyGet <= 1)
						{
							return ((PropertySymbol)methodSymbol.AssociatedSymbol).CanCallMethodsDirectly();
						}
						if (methodKind != MethodKind.LocalFunction)
						{
							return false;
						}
					}
					return true;
				}
				return true;
			}
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x0004DAD5 File Offset: 0x0004BCD5
		internal virtual void AfterAddingTypeMembersChecks(ConversionsBase conversions, BindingDiagnosticBag diagnostics)
		{
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x0004DAD7 File Offset: 0x0004BCD7
		public static bool operator ==(Symbol left, Symbol right)
		{
			if (right == null)
			{
				return left == null;
			}
			return left == right || right.Equals(left);
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x0004DAEE File Offset: 0x0004BCEE
		public static bool operator !=(Symbol left, Symbol right)
		{
			if (right == null)
			{
				return left != null;
			}
			return left != right && !right.Equals(left);
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x0004DB08 File Offset: 0x0004BD08
		public sealed override bool Equals(object obj)
		{
			return this.Equals(obj as Symbol, SymbolEqualityComparer.Default.CompareKind);
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0004DB20 File Offset: 0x0004BD20
		bool ISymbolInternal.Equals(ISymbolInternal other, TypeCompareKind compareKind)
		{
			return this.Equals(other as Symbol, compareKind);
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0004DB2F File Offset: 0x0004BD2F
		public virtual bool Equals(Symbol other, TypeCompareKind compareKind)
		{
			return this == other;
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0004DB35 File Offset: 0x0004BD35
		public override int GetHashCode()
		{
			return RuntimeHelpers.GetHashCode(this);
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0004DB3D File Offset: 0x0004BD3D
		public static bool Equals(Symbol first, Symbol second, TypeCompareKind compareKind)
		{
			if (first == null)
			{
				return second == null;
			}
			return first.Equals(second, compareKind);
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0004DB4F File Offset: 0x0004BD4F
		public sealed override string ToString()
		{
			return this.ToDisplayString(null);
		}

		// Token: 0x06000A23 RID: 2595
		internal abstract TResult Accept<TArgument, TResult>(CSharpSymbolVisitor<TArgument, TResult> visitor, TArgument a);

		// Token: 0x06000A24 RID: 2596 RVA: 0x0004DB58 File Offset: 0x0004BD58
		internal Symbol()
		{
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x0004DB60 File Offset: 0x0004BD60
		internal virtual void AddSynthesizedAttributes(PEModuleBuilder moduleBuilder, ref ArrayBuilder<SynthesizedAttributeData> attributes)
		{
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0004DB62 File Offset: 0x0004BD62
		internal static void AddSynthesizedAttribute(ref ArrayBuilder<SynthesizedAttributeData> attributes, SynthesizedAttributeData attribute)
		{
			if (attribute != null)
			{
				if (attributes == null)
				{
					attributes = new ArrayBuilder<SynthesizedAttributeData>(1);
				}
				attributes.Add(attribute);
			}
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0004DB7B File Offset: 0x0004BD7B
		internal CharSet? GetEffectiveDefaultMarshallingCharSet()
		{
			return this.ContainingModule.DefaultMarshallingCharSet;
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0004DB88 File Offset: 0x0004BD88
		internal bool IsFromCompilation(CSharpCompilation compilation)
		{
			return compilation == this.DeclaringCompilation;
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000A29 RID: 2601 RVA: 0x0004DB93 File Offset: 0x0004BD93
		internal bool Dangerous_IsFromSomeCompilation
		{
			get
			{
				return this.DeclaringCompilation != null;
			}
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0004DBA0 File Offset: 0x0004BDA0
		internal virtual bool IsDefinedInSourceTree(SyntaxTree tree, TextSpan? definedWithinSpan, CancellationToken cancellationToken = default(CancellationToken))
		{
			ImmutableArray<SyntaxReference> declaringSyntaxReferences = this.DeclaringSyntaxReferences;
			if (this.IsImplicitlyDeclared && declaringSyntaxReferences.Length == 0)
			{
				return this.ContainingSymbol.IsDefinedInSourceTree(tree, definedWithinSpan, cancellationToken);
			}
			foreach (SyntaxReference syntaxReference in declaringSyntaxReferences)
			{
				cancellationToken.ThrowIfCancellationRequested();
				if (syntaxReference.SyntaxTree == tree && (definedWithinSpan == null || syntaxReference.Span.IntersectsWith(definedWithinSpan.Value)))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0004DC22 File Offset: 0x0004BE22
		internal static void ForceCompleteMemberByLocation(SourceLocation locationOpt, Symbol member, CancellationToken cancellationToken)
		{
			if (locationOpt == null || member.IsDefinedInSourceTree(locationOpt.SourceTree, new TextSpan?(locationOpt.SourceSpan), cancellationToken))
			{
				cancellationToken.ThrowIfCancellationRequested();
				member.ForceComplete(locationOpt, cancellationToken);
			}
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x0004DC58 File Offset: 0x0004BE58
		public virtual string GetDocumentationCommentId()
		{
			PooledStringBuilder instance = PooledStringBuilder.GetInstance();
			string text;
			try
			{
				StringBuilder builder = instance.Builder;
				DocumentationCommentIDVisitor.Instance.Visit(this, builder);
				text = ((builder.Length == 0) ? null : builder.ToString());
			}
			finally
			{
				instance.Free();
			}
			return text;
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x0004DCAC File Offset: 0x0004BEAC
		public virtual string GetDocumentationCommentXml(CultureInfo preferredCulture = null, bool expandIncludes = false, CancellationToken cancellationToken = default(CancellationToken))
		{
			return "";
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x0004DCB3 File Offset: 0x0004BEB3
		internal virtual string GetDebuggerDisplay()
		{
			return string.Format("{0} {1}", this.Kind, this.ToDisplayString(Symbol.s_debuggerDisplayFormat));
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0004DCD8 File Offset: 0x0004BED8
		internal virtual void AddDeclarationDiagnostics(BindingDiagnosticBag diagnostics)
		{
			DiagnosticBag diagnosticBag = diagnostics.DiagnosticBag;
			if (diagnosticBag == null || diagnosticBag.IsEmptyWithoutResolution)
			{
				ICollection<AssemblySymbol> dependenciesBag = diagnostics.DependenciesBag;
				if (dependenciesBag == null || dependenciesBag.Count <= 0)
				{
					return;
				}
			}
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			declaringCompilation.AddUsedAssemblies(diagnostics.DependenciesBag);
			DiagnosticBag diagnosticBag2 = diagnostics.DiagnosticBag;
			if (diagnosticBag2 != null && !diagnosticBag2.IsEmptyWithoutResolution)
			{
				declaringCompilation.DeclarationDiagnostics.AddRange(diagnostics.DiagnosticBag);
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000A30 RID: 2608 RVA: 0x0004DD4E File Offset: 0x0004BF4E
		internal bool HasUseSiteError
		{
			get
			{
				DiagnosticInfo diagnosticInfo = this.GetUseSiteInfo().DiagnosticInfo;
				return diagnosticInfo != null && diagnosticInfo.Severity == DiagnosticSeverity.Error;
			}
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0004DD6C File Offset: 0x0004BF6C
		internal virtual UseSiteInfo<AssemblySymbol> GetUseSiteInfo()
		{
			return default(UseSiteInfo<AssemblySymbol>);
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000A32 RID: 2610 RVA: 0x0004DD84 File Offset: 0x0004BF84
		protected AssemblySymbol PrimaryDependency
		{
			get
			{
				AssemblySymbol containingAssembly = this.ContainingAssembly;
				if (containingAssembly != null && containingAssembly.CorLibrary == containingAssembly)
				{
					return null;
				}
				return containingAssembly;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000A33 RID: 2611 RVA: 0x0004DDAC File Offset: 0x0004BFAC
		protected virtual int HighestPriorityUseSiteError
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000A34 RID: 2612 RVA: 0x0004DDB3 File Offset: 0x0004BFB3
		public virtual bool HasUnsupportedMetadata
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0004DDB8 File Offset: 0x0004BFB8
		internal bool MergeUseSiteDiagnostics(ref DiagnosticInfo result, DiagnosticInfo info)
		{
			if (info == null)
			{
				return false;
			}
			if (info.Severity == DiagnosticSeverity.Error && (info.Code == this.HighestPriorityUseSiteError || this.HighestPriorityUseSiteError == 2147483647))
			{
				result = info;
				return true;
			}
			if (result == null || (result.Severity == DiagnosticSeverity.Warning && info.Severity == DiagnosticSeverity.Error))
			{
				result = info;
				return false;
			}
			return false;
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0004DE10 File Offset: 0x0004C010
		internal bool MergeUseSiteInfo(ref UseSiteInfo<AssemblySymbol> result, UseSiteInfo<AssemblySymbol> info)
		{
			DiagnosticInfo diagnosticInfo = result.DiagnosticInfo;
			bool flag = this.MergeUseSiteDiagnostics(ref diagnosticInfo, info.DiagnosticInfo);
			if (diagnosticInfo != null && diagnosticInfo.Severity == DiagnosticSeverity.Error)
			{
				result = new UseSiteInfo<AssemblySymbol>(diagnosticInfo);
				return flag;
			}
			ImmutableHashSet<AssemblySymbol> secondaryDependencies = result.SecondaryDependencies;
			AssemblySymbol primaryDependency = result.PrimaryDependency;
			info.MergeDependencies(ref primaryDependency, ref secondaryDependencies);
			result = new UseSiteInfo<AssemblySymbol>(diagnosticInfo, primaryDependency, secondaryDependencies);
			return flag;
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x0004DE78 File Offset: 0x0004C078
		internal static bool ReportUseSiteDiagnostic(DiagnosticInfo info, DiagnosticBag diagnostics, Location location)
		{
			if (info.Code == 1702 || info.Code == 1701 || info.Code == 1705)
			{
				location = NoLocation.Singleton;
			}
			diagnostics.Add(info, location);
			return info.Severity == DiagnosticSeverity.Error;
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0004DEC4 File Offset: 0x0004C0C4
		internal static bool ReportUseSiteDiagnostic(DiagnosticInfo info, BindingDiagnosticBag diagnostics, Location location)
		{
			return diagnostics.ReportUseSiteDiagnostic(info, location);
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0004DED0 File Offset: 0x0004C0D0
		internal bool DeriveUseSiteInfoFromType(ref UseSiteInfo<AssemblySymbol> result, TypeSymbol type)
		{
			UseSiteInfo<AssemblySymbol> useSiteInfo = type.GetUseSiteInfo();
			DiagnosticInfo diagnosticInfo = useSiteInfo.DiagnosticInfo;
			if (diagnosticInfo != null && diagnosticInfo.Code == 648)
			{
				this.GetSymbolSpecificUnsupportedMetadataUseSiteErrorInfo(ref useSiteInfo);
			}
			return this.MergeUseSiteInfo(ref result, useSiteInfo);
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0004DF10 File Offset: 0x0004C110
		private void GetSymbolSpecificUnsupportedMetadataUseSiteErrorInfo(ref UseSiteInfo<AssemblySymbol> info)
		{
			SymbolKind kind = this.Kind;
			if (kind - SymbolKind.Event <= 1 || kind == SymbolKind.Method || kind == SymbolKind.Property)
			{
				info = info.AdjustDiagnosticInfo(new CSDiagnosticInfo(ErrorCode.ERR_BindToBogus, new object[] { this }));
			}
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x0004DF54 File Offset: 0x0004C154
		private UseSiteInfo<AssemblySymbol> GetSymbolSpecificUnsupportedMetadataUseSiteErrorInfo()
		{
			UseSiteInfo<AssemblySymbol> useSiteInfo = new UseSiteInfo<AssemblySymbol>(new CSDiagnosticInfo(ErrorCode.ERR_BogusType, new object[] { string.Empty }));
			this.GetSymbolSpecificUnsupportedMetadataUseSiteErrorInfo(ref useSiteInfo);
			return useSiteInfo;
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0004DF89 File Offset: 0x0004C189
		internal bool DeriveUseSiteInfoFromType(ref UseSiteInfo<AssemblySymbol> result, TypeWithAnnotations type, Symbol.AllowedRequiredModifierType allowedRequiredModifierType)
		{
			return this.DeriveUseSiteInfoFromType(ref result, type.Type) || this.DeriveUseSiteInfoFromCustomModifiers(ref result, type.CustomModifiers, allowedRequiredModifierType);
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0004DFAC File Offset: 0x0004C1AC
		internal bool DeriveUseSiteInfoFromParameter(ref UseSiteInfo<AssemblySymbol> result, ParameterSymbol param)
		{
			if (!this.DeriveUseSiteInfoFromType(ref result, param.TypeWithAnnotations, Symbol.AllowedRequiredModifierType.None))
			{
				ImmutableArray<CustomModifier> refCustomModifiers = param.RefCustomModifiers;
				MethodSymbol methodSymbol = this as MethodSymbol;
				return this.DeriveUseSiteInfoFromCustomModifiers(ref result, refCustomModifiers, (methodSymbol != null && methodSymbol.MethodKind == MethodKind.FunctionPointerSignature) ? (Symbol.AllowedRequiredModifierType.System_Runtime_InteropServices_InAttribute | Symbol.AllowedRequiredModifierType.System_Runtime_CompilerServices_OutAttribute) : Symbol.AllowedRequiredModifierType.System_Runtime_InteropServices_InAttribute);
			}
			return true;
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0004DFF4 File Offset: 0x0004C1F4
		internal bool DeriveUseSiteInfoFromParameters(ref UseSiteInfo<AssemblySymbol> result, ImmutableArray<ParameterSymbol> parameters)
		{
			foreach (ParameterSymbol parameterSymbol in parameters)
			{
				if (this.DeriveUseSiteInfoFromParameter(ref result, parameterSymbol))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0004E02C File Offset: 0x0004C22C
		internal bool DeriveUseSiteInfoFromCustomModifiers(ref UseSiteInfo<AssemblySymbol> result, ImmutableArray<CustomModifier> customModifiers, Symbol.AllowedRequiredModifierType allowedRequiredModifierType)
		{
			Symbol.AllowedRequiredModifierType allowedRequiredModifierType2 = Symbol.AllowedRequiredModifierType.None;
			bool flag = true;
			foreach (CustomModifier customModifier in customModifiers)
			{
				NamedTypeSymbol namedTypeSymbol = ((CSharpCustomModifier)customModifier).ModifierSymbol;
				if (flag && !customModifier.IsOptional)
				{
					Symbol.AllowedRequiredModifierType allowedRequiredModifierType3 = Symbol.AllowedRequiredModifierType.None;
					if ((allowedRequiredModifierType & Symbol.AllowedRequiredModifierType.System_Runtime_InteropServices_InAttribute) != Symbol.AllowedRequiredModifierType.None && namedTypeSymbol.IsWellKnownTypeInAttribute())
					{
						allowedRequiredModifierType3 = Symbol.AllowedRequiredModifierType.System_Runtime_InteropServices_InAttribute;
					}
					else if ((allowedRequiredModifierType & Symbol.AllowedRequiredModifierType.System_Runtime_CompilerServices_Volatile) != Symbol.AllowedRequiredModifierType.None && namedTypeSymbol.SpecialType == SpecialType.System_Runtime_CompilerServices_IsVolatile)
					{
						allowedRequiredModifierType3 = Symbol.AllowedRequiredModifierType.System_Runtime_CompilerServices_Volatile;
					}
					else if ((allowedRequiredModifierType & Symbol.AllowedRequiredModifierType.System_Runtime_CompilerServices_IsExternalInit) != Symbol.AllowedRequiredModifierType.None && namedTypeSymbol.IsWellKnownTypeIsExternalInit())
					{
						allowedRequiredModifierType3 = Symbol.AllowedRequiredModifierType.System_Runtime_CompilerServices_IsExternalInit;
					}
					else if ((allowedRequiredModifierType & Symbol.AllowedRequiredModifierType.System_Runtime_CompilerServices_OutAttribute) != Symbol.AllowedRequiredModifierType.None && namedTypeSymbol.IsWellKnownTypeOutAttribute())
					{
						allowedRequiredModifierType3 = Symbol.AllowedRequiredModifierType.System_Runtime_CompilerServices_OutAttribute;
					}
					if (allowedRequiredModifierType3 == Symbol.AllowedRequiredModifierType.None || (allowedRequiredModifierType3 != allowedRequiredModifierType2 && allowedRequiredModifierType2 != Symbol.AllowedRequiredModifierType.None))
					{
						if (this.MergeUseSiteInfo(ref result, this.GetSymbolSpecificUnsupportedMetadataUseSiteErrorInfo()))
						{
							return true;
						}
						flag = false;
					}
					allowedRequiredModifierType2 |= allowedRequiredModifierType3;
				}
				if (namedTypeSymbol.IsUnboundGenericType)
				{
					namedTypeSymbol = namedTypeSymbol.OriginalDefinition;
				}
				if (this.DeriveUseSiteInfoFromType(ref result, namedTypeSymbol))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0004E10C File Offset: 0x0004C30C
		internal static bool GetUnificationUseSiteDiagnosticRecursive<T>(ref DiagnosticInfo result, ImmutableArray<T> types, Symbol owner, ref HashSet<TypeSymbol> checkedTypes) where T : TypeSymbol
		{
			ImmutableArray<T>.Enumerator enumerator = types.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.GetUnificationUseSiteDiagnosticRecursive(ref result, owner, ref checkedTypes))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0004E148 File Offset: 0x0004C348
		internal static bool GetUnificationUseSiteDiagnosticRecursive(ref DiagnosticInfo result, ImmutableArray<TypeWithAnnotations> types, Symbol owner, ref HashSet<TypeSymbol> checkedTypes)
		{
			foreach (TypeWithAnnotations typeWithAnnotations in types)
			{
				if (typeWithAnnotations.GetUnificationUseSiteDiagnosticRecursive(ref result, owner, ref checkedTypes))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0004E180 File Offset: 0x0004C380
		internal static bool GetUnificationUseSiteDiagnosticRecursive(ref DiagnosticInfo result, ImmutableArray<CustomModifier> modifiers, Symbol owner, ref HashSet<TypeSymbol> checkedTypes)
		{
			ImmutableArray<CustomModifier>.Enumerator enumerator = modifiers.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (((CSharpCustomModifier)enumerator.Current).ModifierSymbol.GetUnificationUseSiteDiagnosticRecursive(ref result, owner, ref checkedTypes))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0004E1C0 File Offset: 0x0004C3C0
		internal static bool GetUnificationUseSiteDiagnosticRecursive(ref DiagnosticInfo result, ImmutableArray<ParameterSymbol> parameters, Symbol owner, ref HashSet<TypeSymbol> checkedTypes)
		{
			foreach (ParameterSymbol parameterSymbol in parameters)
			{
				if (parameterSymbol.TypeWithAnnotations.GetUnificationUseSiteDiagnosticRecursive(ref result, owner, ref checkedTypes) || Symbol.GetUnificationUseSiteDiagnosticRecursive(ref result, parameterSymbol.RefCustomModifiers, owner, ref checkedTypes))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0004E210 File Offset: 0x0004C410
		internal static bool GetUnificationUseSiteDiagnosticRecursive(ref DiagnosticInfo result, ImmutableArray<TypeParameterSymbol> typeParameters, Symbol owner, ref HashSet<TypeSymbol> checkedTypes)
		{
			foreach (TypeParameterSymbol typeParameterSymbol in typeParameters)
			{
				if (Symbol.GetUnificationUseSiteDiagnosticRecursive(ref result, typeParameterSymbol.ConstraintTypesNoUseSiteDiagnostics, owner, ref checkedTypes))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x0004E24C File Offset: 0x0004C44C
		internal ThreeState ObsoleteState
		{
			get
			{
				switch (this.ObsoleteKind)
				{
				case ObsoleteAttributeKind.None:
				case ObsoleteAttributeKind.Experimental:
					return ThreeState.False;
				case ObsoleteAttributeKind.Uninitialized:
					return ThreeState.Unknown;
				}
				return ThreeState.True;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x0004E284 File Offset: 0x0004C484
		internal ObsoleteAttributeKind ObsoleteKind
		{
			get
			{
				ObsoleteAttributeData obsoleteAttributeData = this.ObsoleteAttributeData;
				if (obsoleteAttributeData != null)
				{
					return obsoleteAttributeData.Kind;
				}
				return ObsoleteAttributeKind.None;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000A47 RID: 2631
		internal abstract ObsoleteAttributeData ObsoleteAttributeData { get; }

		// Token: 0x06000A48 RID: 2632 RVA: 0x0004E2A4 File Offset: 0x0004C4A4
		internal bool GetGuidStringDefaultImplementation(out string guidString)
		{
			foreach (CSharpAttributeData csharpAttributeData in this.GetAttributes())
			{
				if (csharpAttributeData.IsTargetAttribute(this, AttributeDescription.GuidAttribute) && csharpAttributeData.TryGetGuidAttributeValue(out guidString))
				{
					return true;
				}
			}
			guidString = null;
			return false;
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0004E2F0 File Offset: 0x0004C4F0
		public string ToDisplayString(SymbolDisplayFormat format = null)
		{
			return SymbolDisplay.ToDisplayString(this.ISymbol, format);
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0004E2FE File Offset: 0x0004C4FE
		public ImmutableArray<SymbolDisplayPart> ToDisplayParts(SymbolDisplayFormat format = null)
		{
			return SymbolDisplay.ToDisplayParts(this.ISymbol, format);
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0004E30C File Offset: 0x0004C50C
		public string ToMinimalDisplayString(SemanticModel semanticModel, int position, SymbolDisplayFormat format = null)
		{
			return SymbolDisplay.ToMinimalDisplayString(this.ISymbol, semanticModel, position, format);
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0004E31C File Offset: 0x0004C51C
		public ImmutableArray<SymbolDisplayPart> ToMinimalDisplayParts(SemanticModel semanticModel, int position, SymbolDisplayFormat format = null)
		{
			return SymbolDisplay.ToMinimalDisplayParts(this.ISymbol, semanticModel, position, format);
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x0004E32C File Offset: 0x0004C52C
		internal static void ReportErrorIfHasConstraints(SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, DiagnosticBag diagnostics)
		{
			if (constraintClauses.Count > 0)
			{
				diagnostics.Add(ErrorCode.ERR_ConstraintOnlyAllowedOnGenericDecl, constraintClauses[0].WhereKeyword.GetLocation());
			}
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0004E361 File Offset: 0x0004C561
		internal static void CheckForBlockAndExpressionBody(CSharpSyntaxNode block, CSharpSyntaxNode expression, CSharpSyntaxNode syntax, BindingDiagnosticBag diagnostics)
		{
			if (block != null && expression != null)
			{
				diagnostics.Add(ErrorCode.ERR_BlockBodyAndExpressionBody, syntax.GetLocation());
			}
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0004E37C File Offset: 0x0004C57C
		internal bool ReportExplicitUseOfReservedAttributes(in DecodeWellKnownAttributeArguments<AttributeSyntax, CSharpAttributeData, AttributeLocation> arguments, Symbol.ReservedAttributes reserved)
		{
			Symbol.<>c__DisplayClass158_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CSharpAttributeData attribute = arguments.Attribute;
			CS$<>8__locals1.diagnostics = (BindingDiagnosticBag)arguments.Diagnostics;
			if ((reserved & Symbol.ReservedAttributes.DynamicAttribute) != (Symbol.ReservedAttributes)0 && attribute.IsTargetAttribute(this, AttributeDescription.DynamicAttribute))
			{
				CS$<>8__locals1.diagnostics.Add(ErrorCode.ERR_ExplicitDynamicAttr, arguments.AttributeSyntaxOpt.Location);
			}
			else if (((reserved & Symbol.ReservedAttributes.IsReadOnlyAttribute) == (Symbol.ReservedAttributes)0 || !this.<ReportExplicitUseOfReservedAttributes>g__reportExplicitUseOfReservedAttribute|158_0(attribute, in arguments, in AttributeDescription.IsReadOnlyAttribute, ref CS$<>8__locals1)) && ((reserved & Symbol.ReservedAttributes.IsUnmanagedAttribute) == (Symbol.ReservedAttributes)0 || !this.<ReportExplicitUseOfReservedAttributes>g__reportExplicitUseOfReservedAttribute|158_0(attribute, in arguments, in AttributeDescription.IsUnmanagedAttribute, ref CS$<>8__locals1)) && ((reserved & Symbol.ReservedAttributes.IsByRefLikeAttribute) == (Symbol.ReservedAttributes)0 || !this.<ReportExplicitUseOfReservedAttributes>g__reportExplicitUseOfReservedAttribute|158_0(attribute, in arguments, in AttributeDescription.IsByRefLikeAttribute, ref CS$<>8__locals1)))
			{
				if ((reserved & Symbol.ReservedAttributes.TupleElementNamesAttribute) != (Symbol.ReservedAttributes)0 && attribute.IsTargetAttribute(this, AttributeDescription.TupleElementNamesAttribute))
				{
					CS$<>8__locals1.diagnostics.Add(ErrorCode.ERR_ExplicitTupleElementNamesAttribute, arguments.AttributeSyntaxOpt.Location);
				}
				else if ((reserved & Symbol.ReservedAttributes.NullableAttribute) != (Symbol.ReservedAttributes)0 && attribute.IsTargetAttribute(this, AttributeDescription.NullableAttribute))
				{
					CS$<>8__locals1.diagnostics.Add(ErrorCode.ERR_ExplicitNullableAttribute, arguments.AttributeSyntaxOpt.Location);
				}
				else if (((reserved & Symbol.ReservedAttributes.NullableContextAttribute) == (Symbol.ReservedAttributes)0 || !this.<ReportExplicitUseOfReservedAttributes>g__reportExplicitUseOfReservedAttribute|158_0(attribute, in arguments, in AttributeDescription.NullableContextAttribute, ref CS$<>8__locals1)) && ((reserved & Symbol.ReservedAttributes.NullablePublicOnlyAttribute) == (Symbol.ReservedAttributes)0 || !this.<ReportExplicitUseOfReservedAttributes>g__reportExplicitUseOfReservedAttribute|158_0(attribute, in arguments, in AttributeDescription.NullablePublicOnlyAttribute, ref CS$<>8__locals1)) && ((reserved & Symbol.ReservedAttributes.NativeIntegerAttribute) == (Symbol.ReservedAttributes)0 || !this.<ReportExplicitUseOfReservedAttributes>g__reportExplicitUseOfReservedAttribute|158_0(attribute, in arguments, in AttributeDescription.NativeIntegerAttribute, ref CS$<>8__locals1)))
				{
					if ((reserved & Symbol.ReservedAttributes.CaseSensitiveExtensionAttribute) == (Symbol.ReservedAttributes)0 || !attribute.IsTargetAttribute(this, AttributeDescription.CaseSensitiveExtensionAttribute))
					{
						return false;
					}
					CS$<>8__locals1.diagnostics.Add(ErrorCode.ERR_ExplicitExtension, arguments.AttributeSyntaxOpt.Location);
				}
			}
			return true;
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x0004E51C File Offset: 0x0004C71C
		internal virtual byte? GetNullableContextValue()
		{
			byte? localNullableContextValue = this.GetLocalNullableContextValue();
			if (localNullableContextValue != null)
			{
				return localNullableContextValue;
			}
			Symbol containingSymbol = this.ContainingSymbol;
			if (containingSymbol == null)
			{
				return default(byte?);
			}
			return containingSymbol.GetNullableContextValue();
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0004E554 File Offset: 0x0004C754
		internal virtual byte? GetLocalNullableContextValue()
		{
			return default(byte?);
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0004E56C File Offset: 0x0004C76C
		internal void GetCommonNullableValues(CSharpCompilation compilation, ref MostCommonNullableValueBuilder builder)
		{
			switch (this.Kind)
			{
			case SymbolKind.Event:
				if (compilation.ShouldEmitNullableAttributes(this))
				{
					builder.AddValue(((EventSymbol)this).TypeWithAnnotations);
					return;
				}
				break;
			case SymbolKind.Field:
			{
				FieldSymbol fieldSymbol = (FieldSymbol)this;
				TupleElementFieldSymbol tupleElementFieldSymbol = fieldSymbol as TupleElementFieldSymbol;
				if (tupleElementFieldSymbol != null)
				{
					fieldSymbol = tupleElementFieldSymbol.TupleUnderlyingField;
				}
				if (compilation.ShouldEmitNullableAttributes(fieldSymbol))
				{
					builder.AddValue(fieldSymbol.TypeWithAnnotations);
					return;
				}
				break;
			}
			case SymbolKind.Label:
			case SymbolKind.Local:
			case SymbolKind.NetModule:
			case SymbolKind.Namespace:
			case SymbolKind.PointerType:
			case SymbolKind.RangeVariable:
				break;
			case SymbolKind.Method:
				if (compilation.ShouldEmitNullableAttributes(this))
				{
					builder.AddValue(this.GetLocalNullableContextValue());
					return;
				}
				break;
			case SymbolKind.NamedType:
				if (compilation.ShouldEmitNullableAttributes(this))
				{
					builder.AddValue(this.GetLocalNullableContextValue());
					return;
				}
				break;
			case SymbolKind.Parameter:
				builder.AddValue(((ParameterSymbol)this).TypeWithAnnotations);
				return;
			case SymbolKind.Property:
				if (compilation.ShouldEmitNullableAttributes(this))
				{
					builder.AddValue(((PropertySymbol)this).TypeWithAnnotations);
					return;
				}
				break;
			case SymbolKind.TypeParameter:
			{
				SourceTypeParameterSymbolBase sourceTypeParameterSymbolBase = this as SourceTypeParameterSymbolBase;
				if (sourceTypeParameterSymbolBase != null)
				{
					builder.AddValue(sourceTypeParameterSymbolBase.GetSynthesizedNullableAttributeValue());
					foreach (TypeWithAnnotations typeWithAnnotations in sourceTypeParameterSymbolBase.ConstraintTypesNoUseSiteDiagnostics)
					{
						builder.AddValue(typeWithAnnotations);
					}
				}
				break;
			}
			default:
				return;
			}
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0004E6AC File Offset: 0x0004C8AC
		internal bool ShouldEmitNullableContextValue(out byte value)
		{
			byte? localNullableContextValue = this.GetLocalNullableContextValue();
			if (localNullableContextValue == null)
			{
				value = 0;
				return false;
			}
			value = localNullableContextValue.GetValueOrDefault();
			Symbol containingSymbol = this.ContainingSymbol;
			byte valueOrDefault = ((containingSymbol != null) ? containingSymbol.GetNullableContextValue() : default(byte?)).GetValueOrDefault();
			return value != valueOrDefault;
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0004E704 File Offset: 0x0004C904
		internal static bool IsCaptured(Symbol variable, SourceMethodSymbol containingSymbol)
		{
			switch (variable.Kind)
			{
			case SymbolKind.Event:
			case SymbolKind.Field:
			case SymbolKind.Property:
			case SymbolKind.RangeVariable:
				return false;
			case SymbolKind.Local:
				if (((LocalSymbol)variable).IsConst)
				{
					return false;
				}
				goto IL_007E;
			case SymbolKind.Method:
			{
				LocalFunctionSymbol localFunctionSymbol = variable as LocalFunctionSymbol;
				if (localFunctionSymbol == null)
				{
					throw ExceptionUtilities.UnexpectedValue(variable);
				}
				if (localFunctionSymbol.IsStatic)
				{
					return false;
				}
				goto IL_007E;
			}
			case SymbolKind.Parameter:
				goto IL_007E;
			}
			throw ExceptionUtilities.UnexpectedValue(variable.Kind);
			IL_007E:
			for (Symbol symbol = variable.ContainingSymbol; symbol != null; symbol = symbol.ContainingSymbol)
			{
				if (symbol == containingSymbol)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000A55 RID: 2645 RVA: 0x0004E7A9 File Offset: 0x0004C9A9
		bool ISymbolInternal.IsStatic
		{
			get
			{
				return this.IsStatic;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x0004E7B1 File Offset: 0x0004C9B1
		bool ISymbolInternal.IsVirtual
		{
			get
			{
				return this.IsVirtual;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000A57 RID: 2647 RVA: 0x0004E7B9 File Offset: 0x0004C9B9
		bool ISymbolInternal.IsOverride
		{
			get
			{
				return this.IsOverride;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000A58 RID: 2648 RVA: 0x0004E7C1 File Offset: 0x0004C9C1
		bool ISymbolInternal.IsAbstract
		{
			get
			{
				return this.IsAbstract;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x0004E7C9 File Offset: 0x0004C9C9
		Accessibility ISymbolInternal.DeclaredAccessibility
		{
			get
			{
				return this.DeclaredAccessibility;
			}
		}

		// Token: 0x06000A5A RID: 2650
		public abstract void Accept(CSharpSymbolVisitor visitor);

		// Token: 0x06000A5B RID: 2651
		public abstract TResult Accept<TResult>(CSharpSymbolVisitor<TResult> visitor);

		// Token: 0x06000A5C RID: 2652 RVA: 0x0004E7D1 File Offset: 0x0004C9D1
		string IFormattable.ToString(string format, IFormatProvider formatProvider)
		{
			return this.ToString();
		}

		// Token: 0x06000A5D RID: 2653
		protected abstract ISymbol CreateISymbol();

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000A5E RID: 2654 RVA: 0x0004E7D9 File Offset: 0x0004C9D9
		internal ISymbol ISymbol
		{
			get
			{
				if (this._lazyISymbol == null)
				{
					Interlocked.CompareExchange<ISymbol>(ref this._lazyISymbol, this.CreateISymbol(), null);
				}
				return this._lazyISymbol;
			}
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x0004E7FC File Offset: 0x0004C9FC
		public virtual ImmutableArray<CSharpAttributeData> GetAttributes()
		{
			return ImmutableArray<CSharpAttributeData>.Empty;
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0004E804 File Offset: 0x0004CA04
		internal virtual AttributeTargets GetAttributeTarget()
		{
			switch (this.Kind)
			{
			case SymbolKind.Assembly:
				return 1;
			case SymbolKind.Event:
				return 512;
			case SymbolKind.Field:
				return 256;
			case SymbolKind.Method:
			{
				MethodKind methodKind = ((MethodSymbol)this).MethodKind;
				if (methodKind == MethodKind.Constructor || methodKind == MethodKind.StaticConstructor)
				{
					return 32;
				}
				return 64;
			}
			case SymbolKind.NetModule:
				return 2;
			case SymbolKind.NamedType:
			{
				NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)this;
				switch (namedTypeSymbol.TypeKind)
				{
				case TypeKind.Class:
					return 4;
				case TypeKind.Delegate:
					return 4096;
				case TypeKind.Enum:
					return 16;
				case TypeKind.Interface:
					return 1024;
				case TypeKind.Struct:
					return 8;
				case TypeKind.TypeParameter:
					return 16384;
				case TypeKind.Submission:
					throw ExceptionUtilities.UnexpectedValue(namedTypeSymbol.TypeKind);
				}
				break;
			}
			case SymbolKind.Parameter:
				return 2048;
			case SymbolKind.Property:
				return 128;
			case SymbolKind.TypeParameter:
				return 16384;
			}
			return 0;
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0004E911 File Offset: 0x0004CB11
		internal virtual void EarlyDecodeWellKnownAttributeType(NamedTypeSymbol attributeType, AttributeSyntax attributeSyntax)
		{
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0004E913 File Offset: 0x0004CB13
		internal virtual void PostEarlyDecodeWellKnownAttributeTypes()
		{
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0004E915 File Offset: 0x0004CB15
		internal virtual CSharpAttributeData EarlyDecodeWellKnownAttribute(ref EarlyDecodeWellKnownAttributeArguments<EarlyWellKnownAttributeBinder, NamedTypeSymbol, AttributeSyntax, AttributeLocation> arguments)
		{
			return null;
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0004E918 File Offset: 0x0004CB18
		internal static bool EarlyDecodeDeprecatedOrExperimentalOrObsoleteAttribute(ref EarlyDecodeWellKnownAttributeArguments<EarlyWellKnownAttributeBinder, NamedTypeSymbol, AttributeSyntax, AttributeLocation> arguments, out CSharpAttributeData attributeData, out ObsoleteAttributeData obsoleteData)
		{
			NamedTypeSymbol attributeType = arguments.AttributeType;
			AttributeSyntax attributeSyntax = arguments.AttributeSyntax;
			ObsoleteAttributeKind obsoleteAttributeKind;
			if (CSharpAttributeData.IsTargetEarlyAttribute(attributeType, attributeSyntax, AttributeDescription.ObsoleteAttribute))
			{
				obsoleteAttributeKind = ObsoleteAttributeKind.Obsolete;
			}
			else if (CSharpAttributeData.IsTargetEarlyAttribute(attributeType, attributeSyntax, AttributeDescription.DeprecatedAttribute))
			{
				obsoleteAttributeKind = ObsoleteAttributeKind.Deprecated;
			}
			else
			{
				if (!CSharpAttributeData.IsTargetEarlyAttribute(attributeType, attributeSyntax, AttributeDescription.ExperimentalAttribute))
				{
					obsoleteData = null;
					attributeData = null;
					return false;
				}
				obsoleteAttributeKind = ObsoleteAttributeKind.Experimental;
			}
			bool flag;
			attributeData = arguments.Binder.GetAttribute(attributeSyntax, attributeType, out flag);
			if (!attributeData.HasErrors)
			{
				obsoleteData = attributeData.DecodeObsoleteAttribute(obsoleteAttributeKind);
				if (flag)
				{
					attributeData = null;
				}
			}
			else
			{
				obsoleteData = null;
				attributeData = null;
			}
			return true;
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0004E9A4 File Offset: 0x0004CBA4
		internal virtual void DecodeWellKnownAttribute(ref DecodeWellKnownAttributeArguments<AttributeSyntax, CSharpAttributeData, AttributeLocation> arguments)
		{
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0004E9A6 File Offset: 0x0004CBA6
		internal virtual void PostDecodeWellKnownAttributes(ImmutableArray<CSharpAttributeData> boundAttributes, ImmutableArray<AttributeSyntax> allAttributeSyntaxNodes, BindingDiagnosticBag diagnostics, AttributeLocation symbolPart, WellKnownAttributeData decodedData)
		{
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0004E9A8 File Offset: 0x0004CBA8
		internal bool LoadAndValidateAttributes(OneOrMany<SyntaxList<AttributeListSyntax>> attributesSyntaxLists, ref CustomAttributesBag<CSharpAttributeData> lazyCustomAttributesBag, AttributeLocation symbolPart = AttributeLocation.None, bool earlyDecodingOnly = false, Binder binderOpt = null, Func<AttributeSyntax, bool> attributeMatchesOpt = null)
		{
			BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance();
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			ImmutableArray<Binder> immutableArray;
			ImmutableArray<AttributeSyntax> attributesToBind = this.GetAttributesToBind(attributesSyntaxLists, symbolPart, instance, declaringCompilation, attributeMatchesOpt, binderOpt, out immutableArray);
			ImmutableArray<CSharpAttributeData> immutableArray3;
			WellKnownAttributeData wellKnownAttributeData;
			if (attributesToBind.Any<AttributeSyntax>())
			{
				if (lazyCustomAttributesBag == null)
				{
					Interlocked.CompareExchange<CustomAttributesBag<CSharpAttributeData>>(ref lazyCustomAttributesBag, new CustomAttributesBag<CSharpAttributeData>(), null);
				}
				int length = attributesToBind.Length;
				NamedTypeSymbol[] array = new NamedTypeSymbol[length];
				Binder.BindAttributeTypes(immutableArray, attributesToBind, this, array, instance);
				ImmutableArray<NamedTypeSymbol> immutableArray2 = array.AsImmutableOrNull<NamedTypeSymbol>();
				this.EarlyDecodeWellKnownAttributeTypes(immutableArray2, attributesToBind);
				this.PostEarlyDecodeWellKnownAttributeTypes();
				CSharpAttributeData[] array2 = new CSharpAttributeData[length];
				EarlyWellKnownAttributeData earlyWellKnownAttributeData = this.EarlyDecodeWellKnownAttributes(immutableArray, immutableArray2, attributesToBind, symbolPart, array2);
				lazyCustomAttributesBag.SetEarlyDecodedWellKnownAttributeData(earlyWellKnownAttributeData);
				if (earlyDecodingOnly)
				{
					instance.Free();
					return false;
				}
				Binder.GetAttributes(immutableArray, attributesToBind, immutableArray2, array2, instance);
				immutableArray3 = array2.AsImmutableOrNull<CSharpAttributeData>();
				wellKnownAttributeData = this.ValidateAttributeUsageAndDecodeWellKnownAttributes(immutableArray, attributesToBind, immutableArray3, instance, symbolPart);
				lazyCustomAttributesBag.SetDecodedWellKnownAttributeData(wellKnownAttributeData);
			}
			else
			{
				if (earlyDecodingOnly)
				{
					instance.Free();
					return false;
				}
				immutableArray3 = ImmutableArray<CSharpAttributeData>.Empty;
				wellKnownAttributeData = null;
				Interlocked.CompareExchange<CustomAttributesBag<CSharpAttributeData>>(ref lazyCustomAttributesBag, CustomAttributesBag<CSharpAttributeData>.WithEmptyData(), null);
				this.PostEarlyDecodeWellKnownAttributeTypes();
			}
			this.PostDecodeWellKnownAttributes(immutableArray3, attributesToBind, instance, symbolPart, wellKnownAttributeData);
			bool flag = false;
			if (lazyCustomAttributesBag.SetAttributes(immutableArray3))
			{
				if (attributeMatchesOpt == null)
				{
					this.RecordPresenceOfBadAttributes(immutableArray3);
					this.AddDeclarationDiagnostics(instance);
				}
				flag = true;
				if (lazyCustomAttributesBag.IsEmpty)
				{
					lazyCustomAttributesBag = CustomAttributesBag<CSharpAttributeData>.Empty;
				}
			}
			instance.Free();
			return flag;
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0004EAF0 File Offset: 0x0004CCF0
		private void RecordPresenceOfBadAttributes(ImmutableArray<CSharpAttributeData> boundAttributes)
		{
			ImmutableArray<CSharpAttributeData>.Enumerator enumerator = boundAttributes.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.HasErrors)
				{
					((SourceModuleSymbol)this.DeclaringCompilation.SourceModule).RecordPresenceOfBadAttributes();
					return;
				}
			}
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0004EB34 File Offset: 0x0004CD34
		private ImmutableArray<AttributeSyntax> GetAttributesToBind(OneOrMany<SyntaxList<AttributeListSyntax>> attributeDeclarationSyntaxLists, AttributeLocation symbolPart, BindingDiagnosticBag diagnostics, CSharpCompilation compilation, Func<AttributeSyntax, bool> attributeMatchesOpt, Binder rootBinderOpt, out ImmutableArray<Binder> binders)
		{
			IAttributeTargetSymbol attributeTargetSymbol = (IAttributeTargetSymbol)this;
			ArrayBuilder<AttributeSyntax> arrayBuilder = null;
			ArrayBuilder<Binder> arrayBuilder2 = null;
			int num = 0;
			for (int i = 0; i < attributeDeclarationSyntaxLists.Count; i++)
			{
				SyntaxList<AttributeListSyntax> syntaxList = attributeDeclarationSyntaxLists[i];
				if (syntaxList.Any())
				{
					int num2 = num;
					foreach (AttributeListSyntax attributeListSyntax in syntaxList)
					{
						if (Symbol.MatchAttributeTarget(attributeTargetSymbol, symbolPart, attributeListSyntax.Target, diagnostics))
						{
							if (arrayBuilder == null)
							{
								arrayBuilder = new ArrayBuilder<AttributeSyntax>();
								arrayBuilder2 = new ArrayBuilder<Binder>();
							}
							SeparatedSyntaxList<AttributeSyntax> attributes = attributeListSyntax.Attributes;
							if (attributeMatchesOpt == null)
							{
								arrayBuilder.AddRange(attributes);
								num += attributes.Count;
							}
							else
							{
								foreach (AttributeSyntax attributeSyntax in attributes)
								{
									if (attributeMatchesOpt.Invoke(attributeSyntax))
									{
										arrayBuilder.Add(attributeSyntax);
										num++;
									}
								}
							}
						}
					}
					if (num != num2)
					{
						SyntaxTree syntaxTree = syntaxList.Node.SyntaxTree;
						Binder binder = rootBinderOpt ?? compilation.GetBinderFactory(syntaxTree, false).GetBinder(syntaxList.Node, null, null);
						binder = new ContextualAttributeBinder(binder, this);
						for (int j = 0; j < num - num2; j++)
						{
							arrayBuilder2.Add(binder);
						}
					}
				}
			}
			if (arrayBuilder != null)
			{
				binders = arrayBuilder2.ToImmutableAndFree();
				return arrayBuilder.ToImmutableAndFree();
			}
			binders = ImmutableArray<Binder>.Empty;
			return ImmutableArray<AttributeSyntax>.Empty;
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0004ECA4 File Offset: 0x0004CEA4
		private static bool MatchAttributeTarget(IAttributeTargetSymbol attributeTarget, AttributeLocation symbolPart, AttributeTargetSpecifierSyntax targetOpt, BindingDiagnosticBag diagnostics)
		{
			IAttributeTargetSymbol attributesOwner = attributeTarget.AttributesOwner;
			bool flag = symbolPart == AttributeLocation.None && attributesOwner == attributeTarget;
			if (targetOpt == null)
			{
				return flag;
			}
			AttributeLocation allowedAttributeLocations = attributesOwner.AllowedAttributeLocations;
			AttributeLocation attributeLocation = targetOpt.GetAttributeLocation();
			if (attributeLocation == AttributeLocation.None)
			{
				if (flag)
				{
					diagnostics.Add(ErrorCode.WRN_InvalidAttributeLocation, targetOpt.Identifier.GetLocation(), new object[]
					{
						targetOpt.Identifier.ValueText,
						allowedAttributeLocations.ToDisplayString()
					});
				}
				return false;
			}
			if ((attributeLocation & allowedAttributeLocations) == AttributeLocation.None)
			{
				if (flag)
				{
					if (allowedAttributeLocations == AttributeLocation.None)
					{
						AttributeLocation defaultAttributeLocation = attributeTarget.DefaultAttributeLocation;
						if (defaultAttributeLocation - AttributeLocation.Assembly > 1)
						{
							throw ExceptionUtilities.UnexpectedValue(attributeTarget.DefaultAttributeLocation);
						}
						diagnostics.Add(ErrorCode.ERR_GlobalAttributesNotAllowed, targetOpt.Identifier.GetLocation());
					}
					else
					{
						diagnostics.Add(ErrorCode.WRN_AttributeLocationOnBadDeclaration, targetOpt.Identifier.GetLocation(), new object[]
						{
							targetOpt.Identifier.ToString(),
							allowedAttributeLocations.ToDisplayString()
						});
					}
				}
				return false;
			}
			if (symbolPart == AttributeLocation.None)
			{
				return attributeLocation == attributeTarget.DefaultAttributeLocation;
			}
			return attributeLocation == symbolPart;
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0004EDC0 File Offset: 0x0004CFC0
		internal EarlyWellKnownAttributeData EarlyDecodeWellKnownAttributes(ImmutableArray<Binder> binders, ImmutableArray<NamedTypeSymbol> boundAttributeTypes, ImmutableArray<AttributeSyntax> attributesToBind, AttributeLocation symbolPart, CSharpAttributeData[] boundAttributesBuilder)
		{
			EarlyWellKnownAttributeBinder earlyWellKnownAttributeBinder = new EarlyWellKnownAttributeBinder(binders[0]);
			EarlyDecodeWellKnownAttributeArguments<EarlyWellKnownAttributeBinder, NamedTypeSymbol, AttributeSyntax, AttributeLocation> earlyDecodeWellKnownAttributeArguments = default(EarlyDecodeWellKnownAttributeArguments<EarlyWellKnownAttributeBinder, NamedTypeSymbol, AttributeSyntax, AttributeLocation>);
			earlyDecodeWellKnownAttributeArguments.SymbolPart = symbolPart;
			for (int i = 0; i < boundAttributeTypes.Length; i++)
			{
				NamedTypeSymbol namedTypeSymbol = boundAttributeTypes[i];
				if (!namedTypeSymbol.IsErrorType())
				{
					if (binders[i] != earlyWellKnownAttributeBinder.Next)
					{
						earlyWellKnownAttributeBinder = new EarlyWellKnownAttributeBinder(binders[i]);
					}
					earlyDecodeWellKnownAttributeArguments.Binder = earlyWellKnownAttributeBinder;
					earlyDecodeWellKnownAttributeArguments.AttributeType = namedTypeSymbol;
					earlyDecodeWellKnownAttributeArguments.AttributeSyntax = attributesToBind[i];
					CSharpAttributeData csharpAttributeData = this.EarlyDecodeWellKnownAttribute(ref earlyDecodeWellKnownAttributeArguments);
					boundAttributesBuilder[i] = csharpAttributeData;
				}
			}
			if (!earlyDecodeWellKnownAttributeArguments.HasDecodedData)
			{
				return null;
			}
			return earlyDecodeWellKnownAttributeArguments.DecodedData;
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0004EE70 File Offset: 0x0004D070
		private void EarlyDecodeWellKnownAttributeTypes(ImmutableArray<NamedTypeSymbol> attributeTypes, ImmutableArray<AttributeSyntax> attributeSyntaxList)
		{
			for (int i = 0; i < attributeTypes.Length; i++)
			{
				NamedTypeSymbol namedTypeSymbol = attributeTypes[i];
				if (!namedTypeSymbol.IsErrorType())
				{
					this.EarlyDecodeWellKnownAttributeType(namedTypeSymbol, attributeSyntaxList[i]);
				}
			}
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x0004EEB0 File Offset: 0x0004D0B0
		private WellKnownAttributeData ValidateAttributeUsageAndDecodeWellKnownAttributes(ImmutableArray<Binder> binders, ImmutableArray<AttributeSyntax> attributeSyntaxList, ImmutableArray<CSharpAttributeData> boundAttributes, BindingDiagnosticBag diagnostics, AttributeLocation symbolPart)
		{
			int length = boundAttributes.Length;
			HashSet<NamedTypeSymbol> hashSet = new HashSet<NamedTypeSymbol>();
			DecodeWellKnownAttributeArguments<AttributeSyntax, CSharpAttributeData, AttributeLocation> decodeWellKnownAttributeArguments = default(DecodeWellKnownAttributeArguments<AttributeSyntax, CSharpAttributeData, AttributeLocation>);
			decodeWellKnownAttributeArguments.Diagnostics = diagnostics;
			decodeWellKnownAttributeArguments.AttributesCount = length;
			decodeWellKnownAttributeArguments.SymbolPart = symbolPart;
			for (int i = 0; i < length; i++)
			{
				CSharpAttributeData csharpAttributeData = boundAttributes[i];
				AttributeSyntax attributeSyntax = attributeSyntaxList[i];
				Binder binder = binders[i];
				if (!csharpAttributeData.HasErrors && this.ValidateAttributeUsage(csharpAttributeData, attributeSyntax, binder.Compilation, symbolPart, diagnostics, hashSet))
				{
					decodeWellKnownAttributeArguments.Attribute = csharpAttributeData;
					decodeWellKnownAttributeArguments.AttributeSyntaxOpt = attributeSyntax;
					decodeWellKnownAttributeArguments.Index = i;
					this.DecodeWellKnownAttribute(ref decodeWellKnownAttributeArguments);
				}
			}
			if (!decodeWellKnownAttributeArguments.HasDecodedData)
			{
				return null;
			}
			return decodeWellKnownAttributeArguments.DecodedData;
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0004EF6C File Offset: 0x0004D16C
		private bool ValidateAttributeUsage(CSharpAttributeData attribute, AttributeSyntax node, CSharpCompilation compilation, AttributeLocation symbolPart, BindingDiagnosticBag diagnostics, HashSet<NamedTypeSymbol> uniqueAttributeTypes)
		{
			NamedTypeSymbol attributeClass = attribute.AttributeClass;
			AttributeUsageInfo attributeUsageInfo = attributeClass.GetAttributeUsageInfo();
			if (!uniqueAttributeTypes.Add(attributeClass) && !attributeUsageInfo.AllowMultiple)
			{
				diagnostics.Add(ErrorCode.ERR_DuplicateAttribute, node.Name.Location, new object[] { node.GetErrorDisplayName() });
				return false;
			}
			AttributeTargets attributeTargets;
			if (symbolPart == AttributeLocation.Return)
			{
				attributeTargets = 8192;
			}
			else
			{
				attributeTargets = this.GetAttributeTarget();
			}
			if ((attributeTargets & attributeUsageInfo.ValidTargets) == null)
			{
				diagnostics.Add(ErrorCode.ERR_AttributeOnBadSymbolType, node.Name.Location, new object[]
				{
					node.GetErrorDisplayName(),
					attributeUsageInfo.GetValidTargetsErrorArgument()
				});
				return false;
			}
			if (attribute.IsSecurityAttribute(compilation))
			{
				SymbolKind kind = this.Kind;
				if (kind != SymbolKind.Assembly && kind != SymbolKind.Method && kind != SymbolKind.NamedType)
				{
					diagnostics.Add(ErrorCode.ERR_SecurityAttributeInvalidTarget, node.Name.Location, new object[] { node.GetErrorDisplayName() });
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x0004F061 File Offset: 0x0004D261
		internal void ForceCompleteObsoleteAttribute()
		{
			if (this.ObsoleteState == ThreeState.Unknown)
			{
				this.GetAttributes();
			}
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0004F090 File Offset: 0x0004D290
		[CompilerGenerated]
		private bool <ReportExplicitUseOfReservedAttributes>g__reportExplicitUseOfReservedAttribute|158_0(CSharpAttributeData attribute, in DecodeWellKnownAttributeArguments<AttributeSyntax, CSharpAttributeData, AttributeLocation> arguments, in AttributeDescription attributeDescription, ref Symbol.<>c__DisplayClass158_0 A_4)
		{
			if (attribute.IsTargetAttribute(this, attributeDescription))
			{
				BindingDiagnosticBag diagnostics = A_4.diagnostics;
				ErrorCode errorCode = ErrorCode.ERR_ExplicitReservedAttr;
				Location location = arguments.AttributeSyntaxOpt.Location;
				object[] array = new object[1];
				int num = 0;
				AttributeDescription attributeDescription2 = attributeDescription;
				array[num] = attributeDescription2.FullName;
				diagnostics.Add(errorCode, location, array);
				return true;
			}
			return false;
		}

		// Token: 0x0400045F RID: 1119
		private ISymbol _lazyISymbol;

		// Token: 0x04000460 RID: 1120
		private static readonly SymbolDisplayFormat s_debuggerDisplayFormat = SymbolDisplayFormat.TestFormat.AddMiscellaneousOptions(SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier | SymbolDisplayMiscellaneousOptions.IncludeNotNullableReferenceTypeModifier).WithCompilerInternalOptions(SymbolDisplayCompilerInternalOptions.None);

		// Token: 0x02000671 RID: 1649
		[Flags]
		internal enum AllowedRequiredModifierType
		{
			// Token: 0x04002045 RID: 8261
			None = 0,
			// Token: 0x04002046 RID: 8262
			System_Runtime_CompilerServices_Volatile = 1,
			// Token: 0x04002047 RID: 8263
			System_Runtime_InteropServices_InAttribute = 2,
			// Token: 0x04002048 RID: 8264
			System_Runtime_CompilerServices_IsExternalInit = 4,
			// Token: 0x04002049 RID: 8265
			System_Runtime_CompilerServices_OutAttribute = 8
		}

		// Token: 0x02000672 RID: 1650
		[Flags]
		internal enum ReservedAttributes
		{
			// Token: 0x0400204B RID: 8267
			DynamicAttribute = 2,
			// Token: 0x0400204C RID: 8268
			IsReadOnlyAttribute = 4,
			// Token: 0x0400204D RID: 8269
			IsUnmanagedAttribute = 8,
			// Token: 0x0400204E RID: 8270
			IsByRefLikeAttribute = 16,
			// Token: 0x0400204F RID: 8271
			TupleElementNamesAttribute = 32,
			// Token: 0x04002050 RID: 8272
			NullableAttribute = 64,
			// Token: 0x04002051 RID: 8273
			NullableContextAttribute = 128,
			// Token: 0x04002052 RID: 8274
			NullablePublicOnlyAttribute = 256,
			// Token: 0x04002053 RID: 8275
			NativeIntegerAttribute = 512,
			// Token: 0x04002054 RID: 8276
			CaseSensitiveExtensionAttribute = 1024
		}
	}
}
