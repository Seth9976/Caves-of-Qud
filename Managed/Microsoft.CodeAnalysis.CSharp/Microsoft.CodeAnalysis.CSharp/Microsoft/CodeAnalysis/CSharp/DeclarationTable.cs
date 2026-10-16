using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000125 RID: 293
	internal sealed class DeclarationTable
	{
		// Token: 0x0600185F RID: 6239 RVA: 0x00083110 File Offset: 0x00081310
		private DeclarationTable(ImmutableSetWithInsertionOrder<RootSingleNamespaceDeclaration> allOlderRootDeclarations, Lazy<RootSingleNamespaceDeclaration> latestLazyRootDeclaration, DeclarationTable.Cache cache)
		{
			this._allOlderRootDeclarations = allOlderRootDeclarations;
			this._latestLazyRootDeclaration = latestLazyRootDeclaration;
			this._cache = cache ?? new DeclarationTable.Cache(this);
			this._typeNames = new Lazy<ICollection<string>>(new Func<ICollection<string>>(this.GetMergedTypeNames));
			this._namespaceNames = new Lazy<ICollection<string>>(new Func<ICollection<string>>(this.GetMergedNamespaceNames));
			this._referenceDirectives = new Lazy<ICollection<ReferenceDirective>>(new Func<ICollection<ReferenceDirective>>(this.GetMergedReferenceDirectives));
		}

		// Token: 0x06001860 RID: 6240 RVA: 0x00083187 File Offset: 0x00081387
		public DeclarationTable AddRootDeclaration(Lazy<RootSingleNamespaceDeclaration> lazyRootDeclaration)
		{
			if (this._latestLazyRootDeclaration == null)
			{
				return new DeclarationTable(this._allOlderRootDeclarations, lazyRootDeclaration, this._cache);
			}
			return new DeclarationTable(this._allOlderRootDeclarations.Add(this._latestLazyRootDeclaration.Value), lazyRootDeclaration, null);
		}

		// Token: 0x06001861 RID: 6241 RVA: 0x000831C1 File Offset: 0x000813C1
		public DeclarationTable RemoveRootDeclaration(Lazy<RootSingleNamespaceDeclaration> lazyRootDeclaration)
		{
			if (this._latestLazyRootDeclaration == lazyRootDeclaration)
			{
				return new DeclarationTable(this._allOlderRootDeclarations, null, this._cache);
			}
			return new DeclarationTable(this._allOlderRootDeclarations.Remove(lazyRootDeclaration.Value), this._latestLazyRootDeclaration, null);
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x000831FC File Offset: 0x000813FC
		public MergedNamespaceDeclaration GetMergedRoot(CSharpCompilation compilation)
		{
			if (this._mergedRoot == null)
			{
				Interlocked.CompareExchange<MergedNamespaceDeclaration>(ref this._mergedRoot, this.CalculateMergedRoot(compilation), null);
			}
			return this._mergedRoot;
		}

		// Token: 0x06001863 RID: 6243 RVA: 0x00083220 File Offset: 0x00081420
		internal MergedNamespaceDeclaration CalculateMergedRoot(CSharpCompilation compilation)
		{
			MergedNamespaceDeclaration value = this._cache.MergedRoot.Value;
			if (this._latestLazyRootDeclaration == null)
			{
				return value;
			}
			if (value == null)
			{
				return MergedNamespaceDeclaration.Create(this._latestLazyRootDeclaration.Value);
			}
			ImmutableArray<SingleNamespaceDeclaration> declarations = value.Declarations;
			ArrayBuilder<SingleNamespaceDeclaration> instance = ArrayBuilder<SingleNamespaceDeclaration>.GetInstance(declarations.Length + 1);
			instance.AddRange(declarations);
			instance.Add(this._latestLazyRootDeclaration.Value);
			if (compilation != null)
			{
				instance.Sort(new DeclarationTable.RootNamespaceLocationComparer(compilation));
			}
			return MergedNamespaceDeclaration.Create(instance.ToImmutableAndFree());
		}

		// Token: 0x06001864 RID: 6244 RVA: 0x000832A4 File Offset: 0x000814A4
		private ICollection<string> GetMergedTypeNames()
		{
			ISet<string> value = this._cache.TypeNames.Value;
			if (this._latestLazyRootDeclaration == null)
			{
				return value;
			}
			return UnionCollection<string>.Create(value, DeclarationTable.GetTypeNames(this._latestLazyRootDeclaration.Value));
		}

		// Token: 0x06001865 RID: 6245 RVA: 0x000832E4 File Offset: 0x000814E4
		private ICollection<string> GetMergedNamespaceNames()
		{
			ISet<string> value = this._cache.NamespaceNames.Value;
			if (this._latestLazyRootDeclaration == null)
			{
				return value;
			}
			return UnionCollection<string>.Create(value, DeclarationTable.GetNamespaceNames(this._latestLazyRootDeclaration.Value));
		}

		// Token: 0x06001866 RID: 6246 RVA: 0x00083324 File Offset: 0x00081524
		private ICollection<ReferenceDirective> GetMergedReferenceDirectives()
		{
			ImmutableArray<ReferenceDirective> value = this._cache.ReferenceDirectives.Value;
			if (this._latestLazyRootDeclaration == null)
			{
				return value;
			}
			return UnionCollection<ReferenceDirective>.Create(value, this._latestLazyRootDeclaration.Value.ReferenceDirectives);
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x00083371 File Offset: 0x00081571
		private static ISet<string> GetTypeNames(Declaration declaration)
		{
			return DeclarationTable.GetNames(declaration, DeclarationTable.s_isTypePredicate);
		}

		// Token: 0x06001868 RID: 6248 RVA: 0x0008337E File Offset: 0x0008157E
		private static ISet<string> GetNamespaceNames(Declaration declaration)
		{
			return DeclarationTable.GetNames(declaration, DeclarationTable.s_isNamespacePredicate);
		}

		// Token: 0x06001869 RID: 6249 RVA: 0x0008338C File Offset: 0x0008158C
		private static ISet<string> GetNames(Declaration declaration, Predicate<Declaration> predicate)
		{
			HashSet<string> hashSet = new HashSet<string>();
			Stack<Declaration> stack = new Stack<Declaration>();
			stack.Push(declaration);
			while (stack.Count > 0)
			{
				Declaration declaration2 = stack.Pop();
				if (declaration2 != null)
				{
					if (predicate.Invoke(declaration2))
					{
						hashSet.Add(declaration2.Name);
					}
					foreach (Declaration declaration3 in declaration2.Children)
					{
						stack.Push(declaration3);
					}
				}
			}
			return SpecializedCollections.ReadOnlySet<string>(hashSet);
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x0600186A RID: 6250 RVA: 0x00083409 File Offset: 0x00081609
		public ICollection<string> TypeNames
		{
			get
			{
				return this._typeNames.Value;
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x0600186B RID: 6251 RVA: 0x00083416 File Offset: 0x00081616
		public ICollection<string> NamespaceNames
		{
			get
			{
				return this._namespaceNames.Value;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x0600186C RID: 6252 RVA: 0x00083423 File Offset: 0x00081623
		public IEnumerable<ReferenceDirective> ReferenceDirectives
		{
			get
			{
				return this._referenceDirectives.Value;
			}
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x00083430 File Offset: 0x00081630
		public static bool ContainsName(MergedNamespaceDeclaration mergedRoot, string name, SymbolFilter filter, CancellationToken cancellationToken)
		{
			return DeclarationTable.ContainsNameHelper(mergedRoot, (string n) => n == name, filter, (SingleTypeDeclaration t) => t.MemberNames.Contains(name), cancellationToken);
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x0008346C File Offset: 0x0008166C
		public static bool ContainsName(MergedNamespaceDeclaration mergedRoot, Func<string, bool> predicate, SymbolFilter filter, CancellationToken cancellationToken)
		{
			return DeclarationTable.ContainsNameHelper(mergedRoot, predicate, filter, delegate(SingleTypeDeclaration t)
			{
				foreach (string text in t.MemberNames)
				{
					if (predicate.Invoke(text))
					{
						return true;
					}
				}
				return false;
			}, cancellationToken);
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x000834A0 File Offset: 0x000816A0
		private static bool ContainsNameHelper(MergedNamespaceDeclaration mergedRoot, Func<string, bool> predicate, SymbolFilter filter, Func<SingleTypeDeclaration, bool> typePredicate, CancellationToken cancellationToken)
		{
			bool flag = (filter & SymbolFilter.Namespace) == SymbolFilter.Namespace;
			bool flag2 = (filter & SymbolFilter.Type) == SymbolFilter.Type;
			bool flag3 = (filter & SymbolFilter.Member) == SymbolFilter.Member;
			Stack<MergedNamespaceOrTypeDeclaration> stack = new Stack<MergedNamespaceOrTypeDeclaration>();
			stack.Push(mergedRoot);
			while (stack.Count > 0)
			{
				cancellationToken.ThrowIfCancellationRequested();
				MergedNamespaceOrTypeDeclaration mergedNamespaceOrTypeDeclaration = stack.Pop();
				if (mergedNamespaceOrTypeDeclaration != null)
				{
					if (mergedNamespaceOrTypeDeclaration.Kind == DeclarationKind.Namespace)
					{
						if (flag && predicate.Invoke(mergedNamespaceOrTypeDeclaration.Name))
						{
							return true;
						}
					}
					else
					{
						if (flag2 && predicate.Invoke(mergedNamespaceOrTypeDeclaration.Name))
						{
							return true;
						}
						if (flag3)
						{
							foreach (SingleTypeDeclaration singleTypeDeclaration in ((MergedTypeDeclaration)mergedNamespaceOrTypeDeclaration).Declarations)
							{
								if (typePredicate.Invoke(singleTypeDeclaration))
								{
									return true;
								}
							}
						}
					}
					foreach (Declaration declaration in mergedNamespaceOrTypeDeclaration.Children)
					{
						MergedNamespaceOrTypeDeclaration mergedNamespaceOrTypeDeclaration2 = declaration as MergedNamespaceOrTypeDeclaration;
						if (mergedNamespaceOrTypeDeclaration2 != null && (flag3 || flag2 || mergedNamespaceOrTypeDeclaration2.Kind == DeclarationKind.Namespace))
						{
							stack.Push(mergedNamespaceOrTypeDeclaration2);
						}
					}
				}
			}
			return false;
		}

		// Token: 0x040006BA RID: 1722
		public static readonly DeclarationTable Empty = new DeclarationTable(ImmutableSetWithInsertionOrder<RootSingleNamespaceDeclaration>.Empty, null, null);

		// Token: 0x040006BB RID: 1723
		private readonly ImmutableSetWithInsertionOrder<RootSingleNamespaceDeclaration> _allOlderRootDeclarations;

		// Token: 0x040006BC RID: 1724
		private readonly Lazy<RootSingleNamespaceDeclaration> _latestLazyRootDeclaration;

		// Token: 0x040006BD RID: 1725
		private readonly DeclarationTable.Cache _cache;

		// Token: 0x040006BE RID: 1726
		private MergedNamespaceDeclaration _mergedRoot;

		// Token: 0x040006BF RID: 1727
		private readonly Lazy<ICollection<string>> _typeNames;

		// Token: 0x040006C0 RID: 1728
		private readonly Lazy<ICollection<string>> _namespaceNames;

		// Token: 0x040006C1 RID: 1729
		private readonly Lazy<ICollection<ReferenceDirective>> _referenceDirectives;

		// Token: 0x040006C2 RID: 1730
		private static readonly Predicate<Declaration> s_isNamespacePredicate = (Declaration d) => d.Kind == DeclarationKind.Namespace;

		// Token: 0x040006C3 RID: 1731
		private static readonly Predicate<Declaration> s_isTypePredicate = (Declaration d) => d.Kind > DeclarationKind.Namespace;

		// Token: 0x020006CE RID: 1742
		private class Cache
		{
			// Token: 0x06008473 RID: 33907 RVA: 0x001E0584 File Offset: 0x001DE784
			public Cache(DeclarationTable table)
			{
				DeclarationTable.Cache <>4__this = this;
				this.MergedRoot = new Lazy<MergedNamespaceDeclaration>(() => MergedNamespaceDeclaration.Create(table._allOlderRootDeclarations.InInsertionOrder.AsImmutable<SingleNamespaceDeclaration>()));
				this.TypeNames = new Lazy<ISet<string>>(() => DeclarationTable.GetTypeNames(<>4__this.MergedRoot.Value));
				this.NamespaceNames = new Lazy<ISet<string>>(() => DeclarationTable.GetNamespaceNames(<>4__this.MergedRoot.Value));
				this.ReferenceDirectives = new Lazy<ImmutableArray<ReferenceDirective>>(() => Enumerable.SelectMany<RootSingleNamespaceDeclaration, ReferenceDirective>(<>4__this.MergedRoot.Value.Declarations.OfType<RootSingleNamespaceDeclaration>(), (RootSingleNamespaceDeclaration r) => r.ReferenceDirectives).AsImmutable<ReferenceDirective>());
			}

			// Token: 0x040021A4 RID: 8612
			internal readonly Lazy<MergedNamespaceDeclaration> MergedRoot;

			// Token: 0x040021A5 RID: 8613
			internal readonly Lazy<ISet<string>> TypeNames;

			// Token: 0x040021A6 RID: 8614
			internal readonly Lazy<ISet<string>> NamespaceNames;

			// Token: 0x040021A7 RID: 8615
			internal readonly Lazy<ImmutableArray<ReferenceDirective>> ReferenceDirectives;
		}

		// Token: 0x020006CF RID: 1743
		private sealed class RootNamespaceLocationComparer : IComparer<SingleNamespaceDeclaration>
		{
			// Token: 0x06008474 RID: 33908 RVA: 0x001E0607 File Offset: 0x001DE807
			internal RootNamespaceLocationComparer(CSharpCompilation compilation)
			{
				this._compilation = compilation;
			}

			// Token: 0x06008475 RID: 33909 RVA: 0x001E0616 File Offset: 0x001DE816
			public int Compare(SingleNamespaceDeclaration x, SingleNamespaceDeclaration y)
			{
				return this._compilation.CompareSourceLocations(x.SyntaxReference, y.SyntaxReference);
			}

			// Token: 0x040021A8 RID: 8616
			private readonly CSharpCompilation _compilation;
		}
	}
}
