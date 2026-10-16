using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200000F RID: 15
	internal sealed class BinderFactory
	{
		// Token: 0x060004C6 RID: 1222 RVA: 0x00032C40 File Offset: 0x00030E40
		internal static Binder MakeCrefBinder(CrefSyntax crefSyntax, MemberDeclarationSyntax memberSyntax, BinderFactory factory, bool inParameterOrReturnType = false)
		{
			BaseTypeDeclarationSyntax baseTypeDeclarationSyntax = memberSyntax as BaseTypeDeclarationSyntax;
			Binder binder = ((baseTypeDeclarationSyntax == null) ? factory.GetBinder(memberSyntax, null, null) : factory.GetBinder(memberSyntax, baseTypeDeclarationSyntax.OpenBraceToken.SpanStart, null, null));
			return BinderFactory.MakeCrefBinderInternal(crefSyntax, binder, inParameterOrReturnType);
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00032C84 File Offset: 0x00030E84
		private static Binder MakeCrefBinderInternal(CrefSyntax crefSyntax, Binder binder, bool inParameterOrReturnType)
		{
			BinderFlags binderFlags = BinderFlags.SuppressConstraintChecks | BinderFlags.Cref | BinderFlags.UnsafeRegion;
			if (inParameterOrReturnType)
			{
				binderFlags |= BinderFlags.CrefParameterOrReturnType;
			}
			binder = binder.WithAdditionalFlags(binderFlags);
			binder = new WithCrefTypeParametersBinder(crefSyntax, binder);
			return binder;
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00032CB8 File Offset: 0x00030EB8
		internal static MemberDeclarationSyntax GetAssociatedMemberForXmlSyntax(CSharpSyntaxNode xmlSyntax)
		{
			for (CSharpSyntaxNode csharpSyntaxNode = (CSharpSyntaxNode)BinderFactory.GetEnclosingDocumentationComment(xmlSyntax).ParentTrivia.Token.Parent; csharpSyntaxNode != null; csharpSyntaxNode = csharpSyntaxNode.Parent)
			{
				MemberDeclarationSyntax memberDeclarationSyntax = csharpSyntaxNode as MemberDeclarationSyntax;
				if (memberDeclarationSyntax != null)
				{
					return memberDeclarationSyntax;
				}
			}
			return null;
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00032D00 File Offset: 0x00030F00
		private static DocumentationCommentTriviaSyntax GetEnclosingDocumentationComment(CSharpSyntaxNode xmlSyntax)
		{
			CSharpSyntaxNode csharpSyntaxNode = xmlSyntax;
			while (!SyntaxFacts.IsDocumentationCommentTrivia(csharpSyntaxNode.Kind()))
			{
				csharpSyntaxNode = csharpSyntaxNode.Parent;
			}
			return (DocumentationCommentTriviaSyntax)csharpSyntaxNode;
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00032D2C File Offset: 0x00030F2C
		internal BinderFactory(CSharpCompilation compilation, SyntaxTree syntaxTree, bool ignoreAccessibility)
		{
			this._compilation = compilation;
			this._syntaxTree = syntaxTree;
			this._ignoreAccessibility = ignoreAccessibility;
			this._binderFactoryVisitorPool = new ObjectPool<BinderFactory.BinderFactoryVisitor>(() => new BinderFactory.BinderFactoryVisitor(this), 64);
			this._binderCache = new ConcurrentCache<BinderFactory.BinderCacheKey, Binder>(50);
			this._buckStopsHereBinder = new BuckStopsHereBinder(compilation);
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x00032D86 File Offset: 0x00030F86
		internal SyntaxTree SyntaxTree
		{
			get
			{
				return this._syntaxTree;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x00032D8E File Offset: 0x00030F8E
		private bool InScript
		{
			get
			{
				return this._syntaxTree.Options.Kind == SourceCodeKind.Script;
			}
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00032DA4 File Offset: 0x00030FA4
		internal Binder GetBinder(SyntaxNode node, CSharpSyntaxNode memberDeclarationOpt = null, Symbol memberOpt = null)
		{
			int spanStart = node.SpanStart;
			if ((!this.InScript || node.Kind() != SyntaxKind.CompilationUnit) && node.Parent != null)
			{
				node = node.Parent;
			}
			return this.GetBinder(node, spanStart, memberDeclarationOpt, memberOpt);
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00032DE8 File Offset: 0x00030FE8
		internal Binder GetBinder(SyntaxNode node, int position, CSharpSyntaxNode memberDeclarationOpt = null, Symbol memberOpt = null)
		{
			BinderFactory.BinderFactoryVisitor binderFactoryVisitor = this._binderFactoryVisitorPool.Allocate();
			binderFactoryVisitor.Initialize(position, memberDeclarationOpt, memberOpt);
			Binder binder = binderFactoryVisitor.Visit(node);
			this._binderFactoryVisitorPool.Free(binderFactoryVisitor);
			return binder;
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00032E20 File Offset: 0x00031020
		internal InMethodBinder GetRecordConstructorInMethodBinder(SynthesizedRecordConstructor constructor)
		{
			RecordDeclarationSyntax syntax = constructor.GetSyntax();
			BinderFactory.NodeUsage nodeUsage = BinderFactory.NodeUsage.MethodTypeParameters;
			BinderFactory.BinderCacheKey binderCacheKey = BinderFactory.BinderFactoryVisitor.CreateBinderCacheKey(syntax, nodeUsage);
			Binder binder;
			if (!this._binderCache.TryGetValue(binderCacheKey, out binder))
			{
				binder = new InMethodBinder(constructor, this.GetInRecordBodyBinder(syntax));
				this._binderCache.TryAdd(binderCacheKey, binder);
			}
			return (InMethodBinder)binder;
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00032E70 File Offset: 0x00031070
		internal Binder GetInRecordBodyBinder(RecordDeclarationSyntax typeDecl)
		{
			BinderFactory.BinderFactoryVisitor binderFactoryVisitor = this._binderFactoryVisitorPool.Allocate();
			binderFactoryVisitor.Initialize(typeDecl.SpanStart, null, null);
			Binder binder = binderFactoryVisitor.VisitTypeDeclarationCore(typeDecl, BinderFactory.NodeUsage.MethodBody);
			this._binderFactoryVisitorPool.Free(binderFactoryVisitor);
			return binder;
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00032EAC File Offset: 0x000310AC
		internal Binder GetInNamespaceBinder(CSharpSyntaxNode unit)
		{
			SyntaxKind syntaxKind = unit.Kind();
			if (syntaxKind == SyntaxKind.CompilationUnit)
			{
				BinderFactory.BinderFactoryVisitor binderFactoryVisitor = this._binderFactoryVisitorPool.Allocate();
				binderFactoryVisitor.Initialize(0, null, null);
				Binder binder = binderFactoryVisitor.VisitCompilationUnit((CompilationUnitSyntax)unit, false, this.InScript);
				this._binderFactoryVisitorPool.Free(binderFactoryVisitor);
				return binder;
			}
			if (syntaxKind == SyntaxKind.NamespaceDeclaration)
			{
				BinderFactory.BinderFactoryVisitor binderFactoryVisitor2 = this._binderFactoryVisitorPool.Allocate();
				binderFactoryVisitor2.Initialize(0, null, null);
				Binder binder2 = binderFactoryVisitor2.VisitNamespaceDeclaration((NamespaceDeclarationSyntax)unit, unit.SpanStart, true, false);
				this._binderFactoryVisitorPool.Free(binderFactoryVisitor2);
				return binder2;
			}
			return null;
		}

		// Token: 0x0400001C RID: 28
		private readonly ConcurrentCache<BinderFactory.BinderCacheKey, Binder> _binderCache;

		// Token: 0x0400001D RID: 29
		private readonly CSharpCompilation _compilation;

		// Token: 0x0400001E RID: 30
		private readonly SyntaxTree _syntaxTree;

		// Token: 0x0400001F RID: 31
		private readonly BuckStopsHereBinder _buckStopsHereBinder;

		// Token: 0x04000020 RID: 32
		private readonly bool _ignoreAccessibility;

		// Token: 0x04000021 RID: 33
		private readonly ObjectPool<BinderFactory.BinderFactoryVisitor> _binderFactoryVisitorPool;

		// Token: 0x0200062B RID: 1579
		private sealed class BinderFactoryVisitor : CSharpSyntaxVisitor<Binder>
		{
			// Token: 0x060081F7 RID: 33271 RVA: 0x001D8894 File Offset: 0x001D6A94
			internal BinderFactoryVisitor(BinderFactory factory)
			{
				this._factory = factory;
			}

			// Token: 0x060081F8 RID: 33272 RVA: 0x001D88A3 File Offset: 0x001D6AA3
			internal void Initialize(int position, CSharpSyntaxNode memberDeclarationOpt, Symbol memberOpt)
			{
				this._position = position;
				this._memberDeclarationOpt = memberDeclarationOpt;
				this._memberOpt = memberOpt;
			}

			// Token: 0x17002674 RID: 9844
			// (get) Token: 0x060081F9 RID: 33273 RVA: 0x001D88BA File Offset: 0x001D6ABA
			private CSharpCompilation compilation
			{
				get
				{
					return this._factory._compilation;
				}
			}

			// Token: 0x17002675 RID: 9845
			// (get) Token: 0x060081FA RID: 33274 RVA: 0x001D88C7 File Offset: 0x001D6AC7
			private SyntaxTree syntaxTree
			{
				get
				{
					return this._factory._syntaxTree;
				}
			}

			// Token: 0x17002676 RID: 9846
			// (get) Token: 0x060081FB RID: 33275 RVA: 0x001D88D4 File Offset: 0x001D6AD4
			private BuckStopsHereBinder buckStopsHereBinder
			{
				get
				{
					return this._factory._buckStopsHereBinder;
				}
			}

			// Token: 0x17002677 RID: 9847
			// (get) Token: 0x060081FC RID: 33276 RVA: 0x001D88E1 File Offset: 0x001D6AE1
			private ConcurrentCache<BinderFactory.BinderCacheKey, Binder> binderCache
			{
				get
				{
					return this._factory._binderCache;
				}
			}

			// Token: 0x17002678 RID: 9848
			// (get) Token: 0x060081FD RID: 33277 RVA: 0x001D88EE File Offset: 0x001D6AEE
			private bool InScript
			{
				get
				{
					return this._factory.InScript;
				}
			}

			// Token: 0x060081FE RID: 33278 RVA: 0x001D88FB File Offset: 0x001D6AFB
			public override Binder DefaultVisit(SyntaxNode parent)
			{
				return this.VisitCore(parent.Parent);
			}

			// Token: 0x060081FF RID: 33279 RVA: 0x001D8909 File Offset: 0x001D6B09
			public override Binder Visit(SyntaxNode node)
			{
				return this.VisitCore(node);
			}

			// Token: 0x06008200 RID: 33280 RVA: 0x001D8912 File Offset: 0x001D6B12
			private Binder VisitCore(SyntaxNode node)
			{
				return ((CSharpSyntaxNode)node).Accept<Binder>(this);
			}

			// Token: 0x06008201 RID: 33281 RVA: 0x001D8920 File Offset: 0x001D6B20
			public override Binder VisitGlobalStatement(GlobalStatementSyntax node)
			{
				if (!SyntaxFacts.IsSimpleProgramTopLevelStatement(node))
				{
					return base.VisitGlobalStatement(node);
				}
				CompilationUnitSyntax compilationUnitSyntax = (CompilationUnitSyntax)node.Parent;
				if (compilationUnitSyntax != this.syntaxTree.GetRoot(default(CancellationToken)))
				{
					throw new ArgumentOutOfRangeException("node", "node not part of tree");
				}
				BinderFactory.BinderCacheKey binderCacheKey = BinderFactory.BinderFactoryVisitor.CreateBinderCacheKey(compilationUnitSyntax, BinderFactory.NodeUsage.MethodBody);
				Binder binder;
				if (!this.binderCache.TryGetValue(binderCacheKey, out binder))
				{
					binder = SimpleProgramNamedTypeSymbol.GetSimpleProgramEntryPoint(this.compilation, (CompilationUnitSyntax)node.Parent, false).GetBodyBinder(this._factory._ignoreAccessibility).GetBinder(compilationUnitSyntax);
					this.binderCache.TryAdd(binderCacheKey, binder);
				}
				return binder;
			}

			// Token: 0x06008202 RID: 33282 RVA: 0x001D89C8 File Offset: 0x001D6BC8
			public override Binder VisitMethodDeclaration(MethodDeclarationSyntax methodDecl)
			{
				if (!LookupPosition.IsInMethodDeclaration(this._position, methodDecl))
				{
					return this.VisitCore(methodDecl.Parent);
				}
				BinderFactory.NodeUsage nodeUsage;
				if (LookupPosition.IsInBody(this._position, methodDecl))
				{
					nodeUsage = BinderFactory.NodeUsage.MethodBody;
				}
				else if (LookupPosition.IsInMethodTypeParameterScope(this._position, methodDecl))
				{
					nodeUsage = BinderFactory.NodeUsage.MethodTypeParameters;
				}
				else
				{
					nodeUsage = BinderFactory.NodeUsage.Normal;
				}
				BinderFactory.BinderCacheKey binderCacheKey = BinderFactory.BinderFactoryVisitor.CreateBinderCacheKey(methodDecl, nodeUsage);
				Binder binder;
				if (!this.binderCache.TryGetValue(binderCacheKey, out binder))
				{
					TypeDeclarationSyntax typeDeclarationSyntax = methodDecl.Parent as TypeDeclarationSyntax;
					if (typeDeclarationSyntax != null)
					{
						binder = this.VisitTypeDeclarationCore(typeDeclarationSyntax, BinderFactory.NodeUsage.MethodBody);
					}
					else
					{
						binder = this.VisitCore(methodDecl.Parent);
					}
					SourceMemberMethodSymbol sourceMemberMethodSymbol = null;
					if (nodeUsage != BinderFactory.NodeUsage.Normal && methodDecl.TypeParameterList != null)
					{
						sourceMemberMethodSymbol = this.GetMethodSymbol(methodDecl, binder);
						binder = new WithMethodTypeParametersBinder(sourceMemberMethodSymbol, binder);
					}
					if (nodeUsage == BinderFactory.NodeUsage.MethodBody)
					{
						sourceMemberMethodSymbol = sourceMemberMethodSymbol ?? this.GetMethodSymbol(methodDecl, binder);
						binder = new InMethodBinder(sourceMemberMethodSymbol, binder);
					}
					binder = binder.WithUnsafeRegionIfNecessary(methodDecl.Modifiers);
					this.binderCache.TryAdd(binderCacheKey, binder);
				}
				return binder;
			}

			// Token: 0x06008203 RID: 33283 RVA: 0x001D8AB4 File Offset: 0x001D6CB4
			public override Binder VisitConstructorDeclaration(ConstructorDeclarationSyntax parent)
			{
				if (!LookupPosition.IsInMethodDeclaration(this._position, parent))
				{
					return this.VisitCore(parent.Parent);
				}
				bool flag = LookupPosition.IsInConstructorParameterScope(this._position, parent);
				BinderFactory.NodeUsage nodeUsage = (flag ? BinderFactory.NodeUsage.MethodTypeParameters : BinderFactory.NodeUsage.Normal);
				BinderFactory.BinderCacheKey binderCacheKey = BinderFactory.BinderFactoryVisitor.CreateBinderCacheKey(parent, nodeUsage);
				Binder binder;
				if (!this.binderCache.TryGetValue(binderCacheKey, out binder))
				{
					binder = this.VisitCore(parent.Parent);
					if (flag)
					{
						SourceMemberMethodSymbol methodSymbol = this.GetMethodSymbol(parent, binder);
						if (methodSymbol != null)
						{
							binder = new InMethodBinder(methodSymbol, binder);
						}
					}
					binder = binder.WithUnsafeRegionIfNecessary(parent.Modifiers);
					this.binderCache.TryAdd(binderCacheKey, binder);
				}
				return binder;
			}

			// Token: 0x06008204 RID: 33284 RVA: 0x001D8B4C File Offset: 0x001D6D4C
			public override Binder VisitDestructorDeclaration(DestructorDeclarationSyntax parent)
			{
				if (!LookupPosition.IsInMethodDeclaration(this._position, parent))
				{
					return this.VisitCore(parent.Parent);
				}
				BinderFactory.BinderCacheKey binderCacheKey = BinderFactory.BinderFactoryVisitor.CreateBinderCacheKey(parent, BinderFactory.NodeUsage.Normal);
				Binder binder;
				if (!this.binderCache.TryGetValue(binderCacheKey, out binder))
				{
					binder = this.VisitCore(parent.Parent);
					binder = new InMethodBinder(this.GetMethodSymbol(parent, binder), binder);
					binder = binder.WithUnsafeRegionIfNecessary(parent.Modifiers);
					this.binderCache.TryAdd(binderCacheKey, binder);
				}
				return binder;
			}

			// Token: 0x06008205 RID: 33285 RVA: 0x001D8BC4 File Offset: 0x001D6DC4
			public override Binder VisitAccessorDeclaration(AccessorDeclarationSyntax parent)
			{
				if (!LookupPosition.IsInMethodDeclaration(this._position, parent))
				{
					return this.VisitCore(parent.Parent);
				}
				bool flag = LookupPosition.IsInBody(this._position, parent);
				BinderFactory.NodeUsage nodeUsage = (flag ? BinderFactory.NodeUsage.MethodTypeParameters : BinderFactory.NodeUsage.Normal);
				BinderFactory.BinderCacheKey binderCacheKey = BinderFactory.BinderFactoryVisitor.CreateBinderCacheKey(parent, nodeUsage);
				Binder binder;
				if (!this.binderCache.TryGetValue(binderCacheKey, out binder))
				{
					binder = this.VisitCore(parent.Parent);
					if (flag)
					{
						CSharpSyntaxNode parent2 = parent.Parent.Parent;
						MethodSymbol methodSymbol = null;
						SyntaxKind syntaxKind = parent2.Kind();
						if (syntaxKind != SyntaxKind.EventFieldDeclaration)
						{
							switch (syntaxKind)
							{
							case SyntaxKind.PropertyDeclaration:
							case SyntaxKind.IndexerDeclaration:
							{
								SourcePropertySymbol propertySymbol = this.GetPropertySymbol((BasePropertyDeclarationSyntax)parent2, binder);
								if (propertySymbol != null)
								{
									methodSymbol = ((parent.Kind() == SyntaxKind.GetAccessorDeclaration) ? propertySymbol.GetMethod : propertySymbol.SetMethod);
									goto IL_0117;
								}
								goto IL_0117;
							}
							case SyntaxKind.EventDeclaration:
								break;
							default:
								throw ExceptionUtilities.UnexpectedValue(parent2.Kind());
							}
						}
						SourceEventSymbol eventSymbol = this.GetEventSymbol((EventDeclarationSyntax)parent2, binder);
						if (eventSymbol != null)
						{
							methodSymbol = ((parent.Kind() == SyntaxKind.AddAccessorDeclaration) ? eventSymbol.AddMethod : eventSymbol.RemoveMethod);
						}
						IL_0117:
						if (methodSymbol != null)
						{
							binder = new InMethodBinder(methodSymbol, binder);
						}
					}
					this.binderCache.TryAdd(binderCacheKey, binder);
				}
				return binder;
			}

			// Token: 0x06008206 RID: 33286 RVA: 0x001D8D04 File Offset: 0x001D6F04
			private Binder VisitOperatorOrConversionDeclaration(BaseMethodDeclarationSyntax parent)
			{
				if (!LookupPosition.IsInMethodDeclaration(this._position, parent))
				{
					return this.VisitCore(parent.Parent);
				}
				bool flag = LookupPosition.IsInBody(this._position, parent);
				BinderFactory.NodeUsage nodeUsage = (flag ? BinderFactory.NodeUsage.MethodTypeParameters : BinderFactory.NodeUsage.Normal);
				BinderFactory.BinderCacheKey binderCacheKey = BinderFactory.BinderFactoryVisitor.CreateBinderCacheKey(parent, nodeUsage);
				Binder binder;
				if (!this.binderCache.TryGetValue(binderCacheKey, out binder))
				{
					binder = this.VisitCore(parent.Parent);
					MethodSymbol methodSymbol = this.GetMethodSymbol(parent, binder);
					if (methodSymbol != null && flag)
					{
						binder = new InMethodBinder(methodSymbol, binder);
					}
					binder = binder.WithUnsafeRegionIfNecessary(parent.Modifiers);
					this.binderCache.TryAdd(binderCacheKey, binder);
				}
				return binder;
			}

			// Token: 0x06008207 RID: 33287 RVA: 0x001D8D9E File Offset: 0x001D6F9E
			public override Binder VisitOperatorDeclaration(OperatorDeclarationSyntax parent)
			{
				return this.VisitOperatorOrConversionDeclaration(parent);
			}

			// Token: 0x06008208 RID: 33288 RVA: 0x001D8DA7 File Offset: 0x001D6FA7
			public override Binder VisitConversionOperatorDeclaration(ConversionOperatorDeclarationSyntax parent)
			{
				return this.VisitOperatorOrConversionDeclaration(parent);
			}

			// Token: 0x06008209 RID: 33289 RVA: 0x001D8DB0 File Offset: 0x001D6FB0
			public override Binder VisitFieldDeclaration(FieldDeclarationSyntax parent)
			{
				return this.VisitCore(parent.Parent).WithUnsafeRegionIfNecessary(parent.Modifiers);
			}

			// Token: 0x0600820A RID: 33290 RVA: 0x001D8DC9 File Offset: 0x001D6FC9
			public override Binder VisitEventDeclaration(EventDeclarationSyntax parent)
			{
				return this.VisitCore(parent.Parent).WithUnsafeRegionIfNecessary(parent.Modifiers);
			}

			// Token: 0x0600820B RID: 33291 RVA: 0x001D8DE2 File Offset: 0x001D6FE2
			public override Binder VisitEventFieldDeclaration(EventFieldDeclarationSyntax parent)
			{
				return this.VisitCore(parent.Parent).WithUnsafeRegionIfNecessary(parent.Modifiers);
			}

			// Token: 0x0600820C RID: 33292 RVA: 0x001D8DFB File Offset: 0x001D6FFB
			public override Binder VisitPropertyDeclaration(PropertyDeclarationSyntax parent)
			{
				if (!LookupPosition.IsInBody(this._position, parent))
				{
					return this.VisitCore(parent.Parent).WithUnsafeRegionIfNecessary(parent.Modifiers);
				}
				return this.VisitPropertyOrIndexerExpressionBody(parent);
			}

			// Token: 0x0600820D RID: 33293 RVA: 0x001D8E2A File Offset: 0x001D702A
			public override Binder VisitIndexerDeclaration(IndexerDeclarationSyntax parent)
			{
				if (!LookupPosition.IsInBody(this._position, parent))
				{
					return this.VisitCore(parent.Parent).WithUnsafeRegionIfNecessary(parent.Modifiers);
				}
				return this.VisitPropertyOrIndexerExpressionBody(parent);
			}

			// Token: 0x0600820E RID: 33294 RVA: 0x001D8E5C File Offset: 0x001D705C
			private Binder VisitPropertyOrIndexerExpressionBody(BasePropertyDeclarationSyntax parent)
			{
				BinderFactory.BinderCacheKey binderCacheKey = BinderFactory.BinderFactoryVisitor.CreateBinderCacheKey(parent, BinderFactory.NodeUsage.MethodTypeParameters);
				Binder binder;
				if (!this.binderCache.TryGetValue(binderCacheKey, out binder))
				{
					binder = this.VisitCore(parent.Parent).WithUnsafeRegionIfNecessary(parent.Modifiers);
					MethodSymbol getMethod = this.GetPropertySymbol(parent, binder).GetMethod;
					if (getMethod != null)
					{
						binder = new InMethodBinder(getMethod, binder);
					}
					this.binderCache.TryAdd(binderCacheKey, binder);
				}
				return binder;
			}

			// Token: 0x0600820F RID: 33295 RVA: 0x001D8EC4 File Offset: 0x001D70C4
			private NamedTypeSymbol GetContainerType(Binder binder, CSharpSyntaxNode node)
			{
				Symbol containingMemberOrLambda = binder.ContainingMemberOrLambda;
				NamedTypeSymbol namedTypeSymbol = containingMemberOrLambda as NamedTypeSymbol;
				if (namedTypeSymbol == null)
				{
					if (node.Parent.Kind() == SyntaxKind.CompilationUnit && this.syntaxTree.Options.Kind != SourceCodeKind.Regular)
					{
						namedTypeSymbol = this.compilation.ScriptClass;
					}
					else
					{
						namedTypeSymbol = ((NamespaceSymbol)containingMemberOrLambda).ImplicitType;
					}
				}
				return namedTypeSymbol;
			}

			// Token: 0x06008210 RID: 33296 RVA: 0x001D8F24 File Offset: 0x001D7124
			private static string GetMethodName(BaseMethodDeclarationSyntax baseMethodDeclarationSyntax, Binder outerBinder)
			{
				SyntaxKind syntaxKind = baseMethodDeclarationSyntax.Kind();
				switch (syntaxKind)
				{
				case SyntaxKind.MethodDeclaration:
				{
					MethodDeclarationSyntax methodDeclarationSyntax = (MethodDeclarationSyntax)baseMethodDeclarationSyntax;
					return ExplicitInterfaceHelpers.GetMemberName(outerBinder, methodDeclarationSyntax.ExplicitInterfaceSpecifier, methodDeclarationSyntax.Identifier.ValueText);
				}
				case SyntaxKind.OperatorDeclaration:
					return OperatorFacts.OperatorNameFromDeclaration((OperatorDeclarationSyntax)baseMethodDeclarationSyntax);
				case SyntaxKind.ConversionOperatorDeclaration:
					if (((ConversionOperatorDeclarationSyntax)baseMethodDeclarationSyntax).ImplicitOrExplicitKeyword.Kind() != SyntaxKind.ImplicitKeyword)
					{
						return "op_Explicit";
					}
					return "op_Implicit";
				case SyntaxKind.ConstructorDeclaration:
					if (!baseMethodDeclarationSyntax.Modifiers.Any(SyntaxKind.StaticKeyword))
					{
						return ".ctor";
					}
					return ".cctor";
				default:
					if (syntaxKind != SyntaxKind.DestructorDeclaration)
					{
						throw ExceptionUtilities.UnexpectedValue(baseMethodDeclarationSyntax.Kind());
					}
					return "Finalize";
				}
			}

			// Token: 0x06008211 RID: 33297 RVA: 0x001D8FE4 File Offset: 0x001D71E4
			private static string GetPropertyOrEventName(BasePropertyDeclarationSyntax basePropertyDeclarationSyntax, Binder outerBinder)
			{
				ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier = basePropertyDeclarationSyntax.ExplicitInterfaceSpecifier;
				SyntaxKind syntaxKind = basePropertyDeclarationSyntax.Kind();
				if (syntaxKind != SyntaxKind.EventFieldDeclaration)
				{
					switch (syntaxKind)
					{
					case SyntaxKind.PropertyDeclaration:
					{
						PropertyDeclarationSyntax propertyDeclarationSyntax = (PropertyDeclarationSyntax)basePropertyDeclarationSyntax;
						return ExplicitInterfaceHelpers.GetMemberName(outerBinder, explicitInterfaceSpecifier, propertyDeclarationSyntax.Identifier.ValueText);
					}
					case SyntaxKind.EventDeclaration:
						break;
					case SyntaxKind.IndexerDeclaration:
						return ExplicitInterfaceHelpers.GetMemberName(outerBinder, explicitInterfaceSpecifier, "this[]");
					default:
						throw ExceptionUtilities.UnexpectedValue(basePropertyDeclarationSyntax.Kind());
					}
				}
				EventDeclarationSyntax eventDeclarationSyntax = (EventDeclarationSyntax)basePropertyDeclarationSyntax;
				return ExplicitInterfaceHelpers.GetMemberName(outerBinder, explicitInterfaceSpecifier, eventDeclarationSyntax.Identifier.ValueText);
			}

			// Token: 0x06008212 RID: 33298 RVA: 0x001D907C File Offset: 0x001D727C
			private SourceMemberMethodSymbol GetMethodSymbol(BaseMethodDeclarationSyntax baseMethodDeclarationSyntax, Binder outerBinder)
			{
				if (baseMethodDeclarationSyntax == this._memberDeclarationOpt)
				{
					return (SourceMemberMethodSymbol)this._memberOpt;
				}
				NamedTypeSymbol containerType = this.GetContainerType(outerBinder, baseMethodDeclarationSyntax);
				if (containerType == null)
				{
					return null;
				}
				string methodName = BinderFactory.BinderFactoryVisitor.GetMethodName(baseMethodDeclarationSyntax, outerBinder);
				return (SourceMemberMethodSymbol)this.GetMemberSymbol(methodName, baseMethodDeclarationSyntax.FullSpan, containerType, SymbolKind.Method);
			}

			// Token: 0x06008213 RID: 33299 RVA: 0x001D90CC File Offset: 0x001D72CC
			private SourcePropertySymbol GetPropertySymbol(BasePropertyDeclarationSyntax basePropertyDeclarationSyntax, Binder outerBinder)
			{
				if (basePropertyDeclarationSyntax == this._memberDeclarationOpt)
				{
					return (SourcePropertySymbol)this._memberOpt;
				}
				NamedTypeSymbol containerType = this.GetContainerType(outerBinder, basePropertyDeclarationSyntax);
				if (containerType == null)
				{
					return null;
				}
				string propertyOrEventName = BinderFactory.BinderFactoryVisitor.GetPropertyOrEventName(basePropertyDeclarationSyntax, outerBinder);
				return (SourcePropertySymbol)this.GetMemberSymbol(propertyOrEventName, basePropertyDeclarationSyntax.Span, containerType, SymbolKind.Property);
			}

			// Token: 0x06008214 RID: 33300 RVA: 0x001D911C File Offset: 0x001D731C
			private SourceEventSymbol GetEventSymbol(EventDeclarationSyntax eventDeclarationSyntax, Binder outerBinder)
			{
				if (eventDeclarationSyntax == this._memberDeclarationOpt)
				{
					return (SourceEventSymbol)this._memberOpt;
				}
				NamedTypeSymbol containerType = this.GetContainerType(outerBinder, eventDeclarationSyntax);
				if (containerType == null)
				{
					return null;
				}
				string propertyOrEventName = BinderFactory.BinderFactoryVisitor.GetPropertyOrEventName(eventDeclarationSyntax, outerBinder);
				return (SourceEventSymbol)this.GetMemberSymbol(propertyOrEventName, eventDeclarationSyntax.Span, containerType, SymbolKind.Event);
			}

			// Token: 0x06008215 RID: 33301 RVA: 0x001D9168 File Offset: 0x001D7368
			private Symbol GetMemberSymbol(string memberName, TextSpan memberSpan, NamedTypeSymbol container, SymbolKind kind)
			{
				foreach (Symbol symbol in container.GetMembers(memberName))
				{
					if (symbol.Kind == kind)
					{
						if (symbol.Kind == SymbolKind.Method)
						{
							if (BinderFactory.BinderFactoryVisitor.InSpan(symbol.Locations[0], this.syntaxTree, memberSpan))
							{
								return symbol;
							}
							MethodSymbol partialImplementationPart = ((MethodSymbol)symbol).PartialImplementationPart;
							if (partialImplementationPart != null && BinderFactory.BinderFactoryVisitor.InSpan(partialImplementationPart.Locations[0], this.syntaxTree, memberSpan))
							{
								return partialImplementationPart;
							}
						}
						else if (BinderFactory.BinderFactoryVisitor.InSpan(symbol.Locations, this.syntaxTree, memberSpan))
						{
							return symbol;
						}
					}
				}
				return null;
			}

			// Token: 0x06008216 RID: 33302 RVA: 0x001D9218 File Offset: 0x001D7418
			private static bool InSpan(Location location, SyntaxTree syntaxTree, TextSpan span)
			{
				return location.SourceTree == syntaxTree && span.Contains(location.SourceSpan);
			}

			// Token: 0x06008217 RID: 33303 RVA: 0x001D9234 File Offset: 0x001D7434
			private static bool InSpan(ImmutableArray<Location> locations, SyntaxTree syntaxTree, TextSpan span)
			{
				ImmutableArray<Location>.Enumerator enumerator = locations.GetEnumerator();
				while (enumerator.MoveNext())
				{
					if (BinderFactory.BinderFactoryVisitor.InSpan(enumerator.Current, syntaxTree, span))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06008218 RID: 33304 RVA: 0x001D9268 File Offset: 0x001D7468
			public override Binder VisitDelegateDeclaration(DelegateDeclarationSyntax parent)
			{
				if (!LookupPosition.IsInDelegateDeclaration(this._position, parent))
				{
					return this.VisitCore(parent.Parent);
				}
				BinderFactory.BinderCacheKey binderCacheKey = BinderFactory.BinderFactoryVisitor.CreateBinderCacheKey(parent, BinderFactory.NodeUsage.Normal);
				Binder binder;
				if (!this.binderCache.TryGetValue(binderCacheKey, out binder))
				{
					Binder binder2 = this.VisitCore(parent.Parent);
					SourceNamedTypeSymbol sourceTypeMember = ((NamespaceOrTypeSymbol)binder2.ContainingMemberOrLambda).GetSourceTypeMember(parent);
					binder = new InContainerBinder(sourceTypeMember, binder2);
					if (parent.TypeParameterList != null)
					{
						binder = new WithClassTypeParametersBinder(sourceTypeMember, binder);
					}
					binder = binder.WithUnsafeRegionIfNecessary(parent.Modifiers);
					this.binderCache.TryAdd(binderCacheKey, binder);
				}
				return binder;
			}

			// Token: 0x06008219 RID: 33305 RVA: 0x001D92FC File Offset: 0x001D74FC
			public override Binder VisitEnumDeclaration(EnumDeclarationSyntax parent)
			{
				if (!LookupPosition.IsBetweenTokens(this._position, parent.OpenBraceToken, parent.CloseBraceToken) && !LookupPosition.IsInAttributeSpecification(this._position, parent.AttributeLists))
				{
					return this.VisitCore(parent.Parent);
				}
				BinderFactory.BinderCacheKey binderCacheKey = BinderFactory.BinderFactoryVisitor.CreateBinderCacheKey(parent, BinderFactory.NodeUsage.Normal);
				Binder binder;
				if (!this.binderCache.TryGetValue(binderCacheKey, out binder))
				{
					Binder binder2 = this.VisitCore(parent.Parent);
					binder = new InContainerBinder(((NamespaceOrTypeSymbol)binder2.ContainingMemberOrLambda).GetSourceTypeMember(parent.Identifier.ValueText, 0, SyntaxKind.EnumDeclaration, parent), binder2);
					binder = binder.WithUnsafeRegionIfNecessary(parent.Modifiers);
					this.binderCache.TryAdd(binderCacheKey, binder);
				}
				return binder;
			}

			// Token: 0x0600821A RID: 33306 RVA: 0x001D93B4 File Offset: 0x001D75B4
			private Binder VisitTypeDeclarationCore(TypeDeclarationSyntax parent)
			{
				if (!LookupPosition.IsInTypeDeclaration(this._position, parent))
				{
					return this.VisitCore(parent.Parent);
				}
				BinderFactory.NodeUsage nodeUsage = BinderFactory.NodeUsage.Normal;
				if (parent.OpenBraceToken != default(SyntaxToken) && parent.CloseBraceToken != default(SyntaxToken) && (LookupPosition.IsBetweenTokens(this._position, parent.OpenBraceToken, parent.CloseBraceToken) || LookupPosition.IsInAttributeSpecification(this._position, parent.AttributeLists)))
				{
					nodeUsage = BinderFactory.NodeUsage.MethodBody;
				}
				else if (LookupPosition.IsInTypeParameterList(this._position, parent))
				{
					nodeUsage = BinderFactory.NodeUsage.MethodBody;
				}
				else if (LookupPosition.IsBetweenTokens(this._position, parent.Keyword, parent.OpenBraceToken))
				{
					nodeUsage = BinderFactory.NodeUsage.NamedTypeBaseListOrParameterList;
				}
				return this.VisitTypeDeclarationCore(parent, nodeUsage);
			}

			// Token: 0x0600821B RID: 33307 RVA: 0x001D9470 File Offset: 0x001D7670
			internal Binder VisitTypeDeclarationCore(TypeDeclarationSyntax parent, BinderFactory.NodeUsage extraInfo)
			{
				BinderFactory.BinderCacheKey binderCacheKey = BinderFactory.BinderFactoryVisitor.CreateBinderCacheKey(parent, extraInfo);
				Binder binder;
				if (!this.binderCache.TryGetValue(binderCacheKey, out binder))
				{
					binder = this.VisitCore(parent.Parent);
					if (extraInfo != BinderFactory.NodeUsage.Normal)
					{
						SourceNamedTypeSymbol sourceTypeMember = ((NamespaceOrTypeSymbol)binder.ContainingMemberOrLambda).GetSourceTypeMember(parent);
						if (extraInfo == BinderFactory.NodeUsage.NamedTypeBaseListOrParameterList)
						{
							binder = new WithClassTypeParametersBinder(sourceTypeMember, binder);
						}
						else
						{
							binder = new InContainerBinder(sourceTypeMember, binder);
							if (parent.TypeParameterList != null)
							{
								binder = new WithClassTypeParametersBinder(sourceTypeMember, binder);
							}
						}
					}
					binder = binder.WithUnsafeRegionIfNecessary(parent.Modifiers);
					this.binderCache.TryAdd(binderCacheKey, binder);
				}
				return binder;
			}

			// Token: 0x0600821C RID: 33308 RVA: 0x001D94F9 File Offset: 0x001D76F9
			public override Binder VisitClassDeclaration(ClassDeclarationSyntax node)
			{
				return this.VisitTypeDeclarationCore(node);
			}

			// Token: 0x0600821D RID: 33309 RVA: 0x001D9502 File Offset: 0x001D7702
			public override Binder VisitStructDeclaration(StructDeclarationSyntax node)
			{
				return this.VisitTypeDeclarationCore(node);
			}

			// Token: 0x0600821E RID: 33310 RVA: 0x001D950B File Offset: 0x001D770B
			public override Binder VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
			{
				return this.VisitTypeDeclarationCore(node);
			}

			// Token: 0x0600821F RID: 33311 RVA: 0x001D9514 File Offset: 0x001D7714
			public override Binder VisitRecordDeclaration(RecordDeclarationSyntax node)
			{
				return this.VisitTypeDeclarationCore(node);
			}

			// Token: 0x06008220 RID: 33312 RVA: 0x001D9520 File Offset: 0x001D7720
			public override Binder VisitNamespaceDeclaration(NamespaceDeclarationSyntax parent)
			{
				if (!LookupPosition.IsInNamespaceDeclaration(this._position, parent))
				{
					return this.VisitCore(parent.Parent);
				}
				bool flag = LookupPosition.IsBetweenTokens(this._position, parent.OpenBraceToken, parent.CloseBraceToken);
				bool flag2 = this.IsInUsing(parent);
				return this.VisitNamespaceDeclaration(parent, this._position, flag, flag2);
			}

			// Token: 0x06008221 RID: 33313 RVA: 0x001D9578 File Offset: 0x001D7778
			internal Binder VisitNamespaceDeclaration(NamespaceDeclarationSyntax parent, int position, bool inBody, bool inUsing)
			{
				BinderFactory.NodeUsage nodeUsage = (inUsing ? BinderFactory.NodeUsage.MethodBody : (inBody ? BinderFactory.NodeUsage.MethodTypeParameters : BinderFactory.NodeUsage.Normal));
				BinderFactory.BinderCacheKey binderCacheKey = BinderFactory.BinderFactoryVisitor.CreateBinderCacheKey(parent, nodeUsage);
				Binder binder;
				if (!this.binderCache.TryGetValue(binderCacheKey, out binder))
				{
					CSharpSyntaxNode parent2 = parent.Parent;
					Binder binder2;
					if (this.InScript && parent2.Kind() == SyntaxKind.CompilationUnit)
					{
						binder2 = this.VisitCompilationUnit((CompilationUnitSyntax)parent2, false, false);
					}
					else
					{
						binder2 = this._factory.GetBinder(parent.Parent, position, null, null);
					}
					if (!inBody)
					{
						binder = binder2;
					}
					else
					{
						binder = BinderFactory.BinderFactoryVisitor.MakeNamespaceBinder(parent, parent.Name, binder2, inUsing);
					}
					this.binderCache.TryAdd(binderCacheKey, binder);
				}
				return binder;
			}

			// Token: 0x06008222 RID: 33314 RVA: 0x001D9618 File Offset: 0x001D7818
			private static Binder MakeNamespaceBinder(CSharpSyntaxNode node, NameSyntax name, Binder outer, bool inUsing)
			{
				QualifiedNameSyntax qualifiedNameSyntax = name as QualifiedNameSyntax;
				if (qualifiedNameSyntax != null)
				{
					outer = BinderFactory.BinderFactoryVisitor.MakeNamespaceBinder(qualifiedNameSyntax.Left, qualifiedNameSyntax.Left, outer, false);
					name = qualifiedNameSyntax.Right;
				}
				InContainerBinder inContainerBinder = outer as InContainerBinder;
				NamespaceOrTypeSymbol namespaceOrTypeSymbol;
				if (inContainerBinder != null)
				{
					namespaceOrTypeSymbol = inContainerBinder.Container;
				}
				else
				{
					namespaceOrTypeSymbol = outer.Compilation.GlobalNamespace;
				}
				NamespaceSymbol nestedNamespace = ((NamespaceSymbol)namespaceOrTypeSymbol).GetNestedNamespace(name);
				if (nestedNamespace == null)
				{
					return outer;
				}
				NamespaceDeclarationSyntax namespaceDeclarationSyntax = node as NamespaceDeclarationSyntax;
				if (namespaceDeclarationSyntax != null)
				{
					outer = BinderFactory.BinderFactoryVisitor.AddInImportsBinders((SourceNamespaceSymbol)outer.Compilation.SourceModule.GetModuleNamespace(nestedNamespace), namespaceDeclarationSyntax, outer, inUsing);
				}
				return new InContainerBinder(nestedNamespace, outer);
			}

			// Token: 0x06008223 RID: 33315 RVA: 0x001D96B1 File Offset: 0x001D78B1
			public override Binder VisitCompilationUnit(CompilationUnitSyntax parent)
			{
				return this.VisitCompilationUnit(parent, this.IsInUsing(parent), this.InScript);
			}

			// Token: 0x06008224 RID: 33316 RVA: 0x001D96C8 File Offset: 0x001D78C8
			internal Binder VisitCompilationUnit(CompilationUnitSyntax compilationUnit, bool inUsing, bool inScript)
			{
				if (compilationUnit != this.syntaxTree.GetRoot(default(CancellationToken)))
				{
					throw new ArgumentOutOfRangeException("compilationUnit", "node not part of tree");
				}
				BinderFactory.NodeUsage nodeUsage = (inUsing ? (inScript ? BinderFactory.NodeUsage.NamedTypeBaseListOrParameterList : BinderFactory.NodeUsage.MethodTypeParameters) : (inScript ? BinderFactory.NodeUsage.MethodBody : BinderFactory.NodeUsage.Normal));
				BinderFactory.BinderCacheKey binderCacheKey = BinderFactory.BinderFactoryVisitor.CreateBinderCacheKey(compilationUnit, nodeUsage);
				Binder binder;
				if (!this.binderCache.TryGetValue(binderCacheKey, out binder))
				{
					binder = this.buckStopsHereBinder;
					if (inScript)
					{
						bool flag = this.compilation.IsSubmissionSyntaxTree(compilationUnit.SyntaxTree);
						NamedTypeSymbol scriptClass = this.compilation.ScriptClass;
						bool isSubmissionClass = scriptClass.IsSubmissionClass;
						if (!inUsing)
						{
							binder = WithUsingNamespacesAndTypesBinder.Create(this.compilation.GlobalImports, binder, true);
							if (isSubmissionClass)
							{
								binder = WithUsingNamespacesAndTypesBinder.Create((SourceNamespaceSymbol)this.compilation.SourceModule.GlobalNamespace, compilationUnit, binder, this.compilation.PreviousSubmission != null && flag, true);
							}
						}
						binder = new InContainerBinder(this.compilation.GlobalNamespace, binder);
						if (this.compilation.HostObjectType != null)
						{
							binder = new HostObjectModelBinder(binder);
						}
						if (isSubmissionClass)
						{
							binder = new InSubmissionClassBinder(scriptClass, binder, compilationUnit, inUsing);
						}
						else
						{
							binder = BinderFactory.BinderFactoryVisitor.AddInImportsBinders((SourceNamespaceSymbol)this.compilation.SourceModule.GlobalNamespace, compilationUnit, binder, inUsing);
							binder = new InContainerBinder(scriptClass, binder);
						}
					}
					else
					{
						NamespaceOrTypeSymbol globalNamespace = this.compilation.GlobalNamespace;
						binder = BinderFactory.BinderFactoryVisitor.AddInImportsBinders((SourceNamespaceSymbol)this.compilation.SourceModule.GlobalNamespace, compilationUnit, binder, inUsing);
						binder = new InContainerBinder(globalNamespace, binder);
						if (!inUsing)
						{
							SynthesizedSimpleProgramEntryPointSymbol simpleProgramEntryPoint = SimpleProgramNamedTypeSymbol.GetSimpleProgramEntryPoint(this.compilation, compilationUnit, true);
							if (simpleProgramEntryPoint != null)
							{
								ExecutableCodeBinder bodyBinder = simpleProgramEntryPoint.GetBodyBinder(this._factory._ignoreAccessibility);
								binder = new SimpleProgramUnitBinder(binder, (SimpleProgramBinder)bodyBinder.GetBinder(simpleProgramEntryPoint.SyntaxNode));
							}
						}
					}
					this.binderCache.TryAdd(binderCacheKey, binder);
				}
				return binder;
			}

			// Token: 0x06008225 RID: 33317 RVA: 0x001D9896 File Offset: 0x001D7A96
			private static Binder AddInImportsBinders(SourceNamespaceSymbol declaringSymbol, CSharpSyntaxNode declarationSyntax, Binder next, bool inUsing)
			{
				if (inUsing)
				{
					return WithExternAliasesBinder.Create(declaringSymbol, declarationSyntax, next);
				}
				return WithExternAndUsingAliasesBinder.Create(declaringSymbol, declarationSyntax, WithUsingNamespacesAndTypesBinder.Create(declaringSymbol, declarationSyntax, next, false, false));
			}

			// Token: 0x06008226 RID: 33318 RVA: 0x001D98B5 File Offset: 0x001D7AB5
			internal static BinderFactory.BinderCacheKey CreateBinderCacheKey(CSharpSyntaxNode node, BinderFactory.NodeUsage usage)
			{
				return new BinderFactory.BinderCacheKey(node, usage);
			}

			// Token: 0x06008227 RID: 33319 RVA: 0x001D98C0 File Offset: 0x001D7AC0
			private bool IsInUsing(CSharpSyntaxNode containingNode)
			{
				TextSpan span = containingNode.Span;
				SyntaxToken syntaxToken;
				if (containingNode.Kind() != SyntaxKind.CompilationUnit && this._position == span.End)
				{
					syntaxToken = containingNode.GetLastToken(false, false, false, false);
				}
				else
				{
					if (this._position < span.Start || this._position > span.End)
					{
						return false;
					}
					syntaxToken = containingNode.FindToken(this._position, false);
				}
				SyntaxNode syntaxNode = syntaxToken.Parent;
				while (syntaxNode != null && syntaxNode != containingNode)
				{
					if (syntaxNode.IsKind(SyntaxKind.UsingDirective) && syntaxNode.Parent == containingNode)
					{
						return true;
					}
					syntaxNode = syntaxNode.Parent;
				}
				return false;
			}

			// Token: 0x06008228 RID: 33320 RVA: 0x001D995C File Offset: 0x001D7B5C
			public override Binder VisitDocumentationCommentTrivia(DocumentationCommentTriviaSyntax parent)
			{
				return this.VisitCore(parent.ParentTrivia.Token.Parent);
			}

			// Token: 0x06008229 RID: 33321 RVA: 0x001D9988 File Offset: 0x001D7B88
			public override Binder VisitCrefParameter(CrefParameterSyntax parent)
			{
				XmlCrefAttributeSyntax xmlCrefAttributeSyntax = parent.FirstAncestorOrSelf<XmlCrefAttributeSyntax>(null, false);
				return this.VisitXmlCrefAttributeInternal(xmlCrefAttributeSyntax, BinderFactory.NodeUsage.MethodTypeParameters);
			}

			// Token: 0x0600822A RID: 33322 RVA: 0x001D99A8 File Offset: 0x001D7BA8
			public override Binder VisitConversionOperatorMemberCref(ConversionOperatorMemberCrefSyntax parent)
			{
				if (parent.Type.Span.Contains(this._position))
				{
					XmlCrefAttributeSyntax xmlCrefAttributeSyntax = parent.FirstAncestorOrSelf<XmlCrefAttributeSyntax>(null, false);
					return this.VisitXmlCrefAttributeInternal(xmlCrefAttributeSyntax, BinderFactory.NodeUsage.MethodTypeParameters);
				}
				return base.VisitConversionOperatorMemberCref(parent);
			}

			// Token: 0x0600822B RID: 33323 RVA: 0x001D99EC File Offset: 0x001D7BEC
			public override Binder VisitXmlCrefAttribute(XmlCrefAttributeSyntax parent)
			{
				if (!LookupPosition.IsInXmlAttributeValue(this._position, parent))
				{
					return this.VisitCore(parent.Parent);
				}
				BinderFactory.NodeUsage nodeUsage = BinderFactory.NodeUsage.Normal;
				return this.VisitXmlCrefAttributeInternal(parent, nodeUsage);
			}

			// Token: 0x0600822C RID: 33324 RVA: 0x001D9A20 File Offset: 0x001D7C20
			private Binder VisitXmlCrefAttributeInternal(XmlCrefAttributeSyntax parent, BinderFactory.NodeUsage extraInfo)
			{
				BinderFactory.BinderCacheKey binderCacheKey = BinderFactory.BinderFactoryVisitor.CreateBinderCacheKey(parent, extraInfo);
				Binder binder;
				if (!this.binderCache.TryGetValue(binderCacheKey, out binder))
				{
					CrefSyntax cref = parent.Cref;
					MemberDeclarationSyntax associatedMemberForXmlSyntax = BinderFactory.GetAssociatedMemberForXmlSyntax(parent);
					bool flag = extraInfo == BinderFactory.NodeUsage.MethodTypeParameters;
					binder = ((associatedMemberForXmlSyntax == null) ? BinderFactory.MakeCrefBinderInternal(cref, this.VisitCore(parent.Parent), flag) : BinderFactory.MakeCrefBinder(cref, associatedMemberForXmlSyntax, this._factory, flag));
					this.binderCache.TryAdd(binderCacheKey, binder);
				}
				return binder;
			}

			// Token: 0x0600822D RID: 33325 RVA: 0x001D9A94 File Offset: 0x001D7C94
			public override Binder VisitXmlNameAttribute(XmlNameAttributeSyntax parent)
			{
				if (!LookupPosition.IsInXmlAttributeValue(this._position, parent))
				{
					return this.VisitCore(parent.Parent);
				}
				XmlNameAttributeElementKind elementKind = parent.GetElementKind();
				BinderFactory.NodeUsage nodeUsage;
				switch (elementKind)
				{
				case XmlNameAttributeElementKind.Parameter:
				case XmlNameAttributeElementKind.ParameterReference:
					nodeUsage = BinderFactory.NodeUsage.MethodTypeParameters;
					break;
				case XmlNameAttributeElementKind.TypeParameter:
					nodeUsage = BinderFactory.NodeUsage.MethodBody;
					break;
				case XmlNameAttributeElementKind.TypeParameterReference:
					nodeUsage = BinderFactory.NodeUsage.NamedTypeBaseListOrParameterList;
					break;
				default:
					throw ExceptionUtilities.UnexpectedValue(elementKind);
				}
				BinderFactory.BinderCacheKey binderCacheKey = BinderFactory.BinderFactoryVisitor.CreateBinderCacheKey(BinderFactory.GetEnclosingDocumentationComment(parent), nodeUsage);
				Binder binder;
				if (!this.binderCache.TryGetValue(binderCacheKey, out binder))
				{
					binder = this.buckStopsHereBinder;
					Binder binder2 = this.VisitCore(BinderFactory.GetEnclosingDocumentationComment(parent));
					if (binder2 != null)
					{
						binder = binder.WithContainingMemberOrLambda(binder2.ContainingMemberOrLambda);
					}
					MemberDeclarationSyntax associatedMemberForXmlSyntax = BinderFactory.GetAssociatedMemberForXmlSyntax(parent);
					if (associatedMemberForXmlSyntax != null)
					{
						switch (elementKind)
						{
						case XmlNameAttributeElementKind.Parameter:
						case XmlNameAttributeElementKind.ParameterReference:
							binder = this.GetParameterNameAttributeValueBinder(associatedMemberForXmlSyntax, binder);
							break;
						case XmlNameAttributeElementKind.TypeParameter:
							binder = this.GetTypeParameterNameAttributeValueBinder(associatedMemberForXmlSyntax, false, binder);
							break;
						case XmlNameAttributeElementKind.TypeParameterReference:
							binder = this.GetTypeParameterNameAttributeValueBinder(associatedMemberForXmlSyntax, true, binder);
							break;
						}
					}
					this.binderCache.TryAdd(binderCacheKey, binder);
				}
				return binder;
			}

			// Token: 0x0600822E RID: 33326 RVA: 0x001D9B90 File Offset: 0x001D7D90
			private Binder GetParameterNameAttributeValueBinder(MemberDeclarationSyntax memberSyntax, Binder nextBinder)
			{
				BaseMethodDeclarationSyntax baseMethodDeclarationSyntax = memberSyntax as BaseMethodDeclarationSyntax;
				if (baseMethodDeclarationSyntax != null)
				{
					ParameterListSyntax parameterListSyntax = baseMethodDeclarationSyntax.ParameterList;
					if (parameterListSyntax != null && parameterListSyntax.ParameterCount > 0)
					{
						Binder binder = this.VisitCore(memberSyntax.Parent);
						return new WithParametersBinder(this.GetMethodSymbol(baseMethodDeclarationSyntax, binder).Parameters, nextBinder);
					}
				}
				RecordDeclarationSyntax recordDeclarationSyntax = memberSyntax as RecordDeclarationSyntax;
				if (recordDeclarationSyntax != null)
				{
					ParameterListSyntax parameterListSyntax = recordDeclarationSyntax.ParameterList;
					if (parameterListSyntax != null && parameterListSyntax.ParameterCount > 0)
					{
						SynthesizedRecordConstructor synthesizedRecordConstructor = Enumerable.SingleOrDefault<SynthesizedRecordConstructor>(((NamespaceOrTypeSymbol)this.VisitCore(memberSyntax).ContainingMemberOrLambda).GetSourceTypeMember((TypeDeclarationSyntax)memberSyntax).GetMembersUnordered().OfType<SynthesizedRecordConstructor>());
						if (synthesizedRecordConstructor.SyntaxRef.SyntaxTree == memberSyntax.SyntaxTree && synthesizedRecordConstructor.GetSyntax() == memberSyntax)
						{
							return new WithParametersBinder(synthesizedRecordConstructor.Parameters, nextBinder);
						}
					}
				}
				SyntaxKind syntaxKind = memberSyntax.Kind();
				if (syntaxKind == SyntaxKind.PropertyDeclaration || syntaxKind == SyntaxKind.IndexerDeclaration)
				{
					Binder binder2 = this.VisitCore(memberSyntax.Parent);
					BasePropertyDeclarationSyntax basePropertyDeclarationSyntax = (BasePropertyDeclarationSyntax)memberSyntax;
					PropertySymbol propertySymbol = this.GetPropertySymbol(basePropertyDeclarationSyntax, binder2);
					ImmutableArray<ParameterSymbol> immutableArray = propertySymbol.Parameters;
					if (propertySymbol.SetMethod != null)
					{
						immutableArray = immutableArray.Add(propertySymbol.SetMethod.Parameters.Last<ParameterSymbol>());
					}
					if (immutableArray.Any<ParameterSymbol>())
					{
						return new WithParametersBinder(immutableArray, nextBinder);
					}
				}
				else if (syntaxKind == SyntaxKind.DelegateDeclaration)
				{
					ImmutableArray<ParameterSymbol> parameters = ((NamespaceOrTypeSymbol)this.VisitCore(memberSyntax.Parent).ContainingMemberOrLambda).GetSourceTypeMember((DelegateDeclarationSyntax)memberSyntax).DelegateInvokeMethod.Parameters;
					if (parameters.Any<ParameterSymbol>())
					{
						return new WithParametersBinder(parameters, nextBinder);
					}
				}
				return nextBinder;
			}

			// Token: 0x0600822F RID: 33327 RVA: 0x001D9D18 File Offset: 0x001D7F18
			private Binder GetTypeParameterNameAttributeValueBinder(MemberDeclarationSyntax memberSyntax, bool includeContainingSymbols, Binder nextBinder)
			{
				if (includeContainingSymbols)
				{
					for (NamedTypeSymbol namedTypeSymbol = this.VisitCore(memberSyntax.Parent).ContainingType; namedTypeSymbol != null; namedTypeSymbol = namedTypeSymbol.ContainingType)
					{
						if (namedTypeSymbol.Arity > 0)
						{
							nextBinder = new WithClassTypeParametersBinder(namedTypeSymbol, nextBinder);
						}
					}
				}
				TypeDeclarationSyntax typeDeclarationSyntax = memberSyntax as TypeDeclarationSyntax;
				if (typeDeclarationSyntax != null && typeDeclarationSyntax.Arity > 0)
				{
					return new WithClassTypeParametersBinder(((NamespaceOrTypeSymbol)this.VisitCore(memberSyntax.Parent).ContainingMemberOrLambda).GetSourceTypeMember(typeDeclarationSyntax), nextBinder);
				}
				if (memberSyntax.Kind() == SyntaxKind.MethodDeclaration)
				{
					MethodDeclarationSyntax methodDeclarationSyntax = (MethodDeclarationSyntax)memberSyntax;
					if (methodDeclarationSyntax.Arity > 0)
					{
						Binder binder = this.VisitCore(memberSyntax.Parent);
						return new WithMethodTypeParametersBinder(this.GetMethodSymbol(methodDeclarationSyntax, binder), nextBinder);
					}
				}
				else if (memberSyntax.Kind() == SyntaxKind.DelegateDeclaration)
				{
					SourceNamedTypeSymbol sourceTypeMember = ((NamespaceOrTypeSymbol)this.VisitCore(memberSyntax.Parent).ContainingMemberOrLambda).GetSourceTypeMember((DelegateDeclarationSyntax)memberSyntax);
					if (sourceTypeMember.TypeParameters.Any<TypeParameterSymbol>())
					{
						return new WithClassTypeParametersBinder(sourceTypeMember, nextBinder);
					}
				}
				return nextBinder;
			}

			// Token: 0x04001F39 RID: 7993
			private int _position;

			// Token: 0x04001F3A RID: 7994
			private CSharpSyntaxNode _memberDeclarationOpt;

			// Token: 0x04001F3B RID: 7995
			private Symbol _memberOpt;

			// Token: 0x04001F3C RID: 7996
			private readonly BinderFactory _factory;
		}

		// Token: 0x0200062C RID: 1580
		private struct BinderCacheKey : IEquatable<BinderFactory.BinderCacheKey>
		{
			// Token: 0x06008230 RID: 33328 RVA: 0x001D9E0F File Offset: 0x001D800F
			public BinderCacheKey(CSharpSyntaxNode syntaxNode, BinderFactory.NodeUsage usage)
			{
				this.syntaxNode = syntaxNode;
				this.usage = usage;
			}

			// Token: 0x06008231 RID: 33329 RVA: 0x001D9E1F File Offset: 0x001D801F
			bool IEquatable<BinderFactory.BinderCacheKey>.Equals(BinderFactory.BinderCacheKey other)
			{
				return this.syntaxNode == other.syntaxNode && this.usage == other.usage;
			}

			// Token: 0x06008232 RID: 33330 RVA: 0x001D9E3F File Offset: 0x001D803F
			public override int GetHashCode()
			{
				return Hash.Combine(this.syntaxNode.GetHashCode(), (int)this.usage);
			}

			// Token: 0x06008233 RID: 33331 RVA: 0x001D9E57 File Offset: 0x001D8057
			public override bool Equals(object obj)
			{
				throw new NotSupportedException();
			}

			// Token: 0x04001F3D RID: 7997
			public readonly CSharpSyntaxNode syntaxNode;

			// Token: 0x04001F3E RID: 7998
			public readonly BinderFactory.NodeUsage usage;
		}

		// Token: 0x0200062D RID: 1581
		internal enum NodeUsage : byte
		{
			// Token: 0x04001F40 RID: 8000
			Normal,
			// Token: 0x04001F41 RID: 8001
			MethodTypeParameters,
			// Token: 0x04001F42 RID: 8002
			MethodBody,
			// Token: 0x04001F43 RID: 8003
			ConstructorBodyOrInitializer = 1,
			// Token: 0x04001F44 RID: 8004
			AccessorBody = 1,
			// Token: 0x04001F45 RID: 8005
			OperatorBody = 1,
			// Token: 0x04001F46 RID: 8006
			NamedTypeBodyOrTypeParameters,
			// Token: 0x04001F47 RID: 8007
			NamedTypeBaseListOrParameterList = 4,
			// Token: 0x04001F48 RID: 8008
			NamespaceBody = 1,
			// Token: 0x04001F49 RID: 8009
			NamespaceUsings,
			// Token: 0x04001F4A RID: 8010
			CompilationUnitUsings = 1,
			// Token: 0x04001F4B RID: 8011
			CompilationUnitScript,
			// Token: 0x04001F4C RID: 8012
			CompilationUnitScriptUsings = 4,
			// Token: 0x04001F4D RID: 8013
			DocumentationCommentParameter = 1,
			// Token: 0x04001F4E RID: 8014
			DocumentationCommentTypeParameter,
			// Token: 0x04001F4F RID: 8015
			DocumentationCommentTypeParameterReference = 4,
			// Token: 0x04001F50 RID: 8016
			CrefParameterOrReturnType = 1
		}
	}
}
