using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000112 RID: 274
	internal sealed class SyntaxAndDeclarationManager : CommonSyntaxAndDeclarationManager
	{
		// Token: 0x06001645 RID: 5701 RVA: 0x0007776B File Offset: 0x0007596B
		internal SyntaxAndDeclarationManager(ImmutableArray<SyntaxTree> externalSyntaxTrees, string scriptClassName, SourceReferenceResolver resolver, CommonMessageProvider messageProvider, bool isSubmission, SyntaxAndDeclarationManager.State state)
			: base(externalSyntaxTrees, scriptClassName, resolver, messageProvider, isSubmission)
		{
			this._lazyState = state;
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x00077782 File Offset: 0x00075982
		internal SyntaxAndDeclarationManager.State GetLazyState()
		{
			if (this._lazyState == null)
			{
				Interlocked.CompareExchange<SyntaxAndDeclarationManager.State>(ref this._lazyState, SyntaxAndDeclarationManager.CreateState(this.ExternalSyntaxTrees, this.ScriptClassName, this.Resolver, this.MessageProvider, this.IsSubmission), null);
			}
			return this._lazyState;
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x000777C4 File Offset: 0x000759C4
		private static SyntaxAndDeclarationManager.State CreateState(ImmutableArray<SyntaxTree> externalSyntaxTrees, string scriptClassName, SourceReferenceResolver resolver, CommonMessageProvider messageProvider, bool isSubmission)
		{
			ArrayBuilder<SyntaxTree> instance = ArrayBuilder<SyntaxTree>.GetInstance();
			PooledDictionary<SyntaxTree, int> instance2 = PooledDictionary<SyntaxTree, int>.GetInstance();
			PooledDictionary<SyntaxTree, ImmutableArray<LoadDirective>> instance3 = PooledDictionary<SyntaxTree, ImmutableArray<LoadDirective>>.GetInstance();
			PooledDictionary<string, SyntaxTree> instance4 = PooledDictionary<string, SyntaxTree>.GetInstance();
			PooledDictionary<SyntaxTree, Lazy<RootSingleNamespaceDeclaration>> instance5 = PooledDictionary<SyntaxTree, Lazy<RootSingleNamespaceDeclaration>>.GetInstance();
			DeclarationTable empty = DeclarationTable.Empty;
			foreach (SyntaxTree syntaxTree in externalSyntaxTrees)
			{
				SyntaxAndDeclarationManager.AppendAllSyntaxTrees(instance, syntaxTree, scriptClassName, resolver, messageProvider, isSubmission, instance2, instance3, instance4, instance5, ref empty);
			}
			return new SyntaxAndDeclarationManager.State(instance.ToImmutableAndFree(), instance2.ToImmutableDictionaryAndFree(), instance3.ToImmutableDictionaryAndFree(), instance4.ToImmutableDictionaryAndFree(), instance5.ToImmutableDictionaryAndFree(), empty);
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x00077850 File Offset: 0x00075A50
		public SyntaxAndDeclarationManager AddSyntaxTrees(IEnumerable<SyntaxTree> trees)
		{
			string scriptClassName = this.ScriptClassName;
			SourceReferenceResolver resolver = this.Resolver;
			CommonMessageProvider messageProvider = this.MessageProvider;
			bool isSubmission = this.IsSubmission;
			SyntaxAndDeclarationManager.State state = this._lazyState;
			ImmutableArray<SyntaxTree> immutableArray = this.ExternalSyntaxTrees.AddRange(trees);
			if (state == null)
			{
				return this.WithExternalSyntaxTrees(immutableArray);
			}
			ImmutableDictionary<SyntaxTree, int>.Builder builder = state.OrdinalMap.ToBuilder();
			ImmutableDictionary<SyntaxTree, ImmutableArray<LoadDirective>>.Builder builder2 = state.LoadDirectiveMap.ToBuilder();
			ImmutableDictionary<string, SyntaxTree>.Builder builder3 = state.LoadedSyntaxTreeMap.ToBuilder();
			ImmutableDictionary<SyntaxTree, Lazy<RootSingleNamespaceDeclaration>>.Builder builder4 = state.RootNamespaces.ToBuilder();
			DeclarationTable declarationTable = state.DeclarationTable;
			ArrayBuilder<SyntaxTree> instance = ArrayBuilder<SyntaxTree>.GetInstance();
			instance.AddRange(state.SyntaxTrees);
			foreach (SyntaxTree syntaxTree in trees)
			{
				SyntaxAndDeclarationManager.AppendAllSyntaxTrees(instance, syntaxTree, scriptClassName, resolver, messageProvider, isSubmission, builder, builder2, builder3, builder4, ref declarationTable);
			}
			state = new SyntaxAndDeclarationManager.State(instance.ToImmutableAndFree(), builder.ToImmutableDictionary<SyntaxTree, int>(), builder2.ToImmutableDictionary<SyntaxTree, ImmutableArray<LoadDirective>>(), builder3.ToImmutableDictionary<string, SyntaxTree>(), builder4.ToImmutableDictionary<SyntaxTree, Lazy<RootSingleNamespaceDeclaration>>(), declarationTable);
			return new SyntaxAndDeclarationManager(immutableArray, scriptClassName, resolver, messageProvider, isSubmission, state);
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x00077980 File Offset: 0x00075B80
		private static void AppendAllSyntaxTrees(ArrayBuilder<SyntaxTree> treesBuilder, SyntaxTree tree, string scriptClassName, SourceReferenceResolver resolver, CommonMessageProvider messageProvider, bool isSubmission, IDictionary<SyntaxTree, int> ordinalMapBuilder, IDictionary<SyntaxTree, ImmutableArray<LoadDirective>> loadDirectiveMapBuilder, IDictionary<string, SyntaxTree> loadedSyntaxTreeMapBuilder, IDictionary<SyntaxTree, Lazy<RootSingleNamespaceDeclaration>> declMapBuilder, ref DeclarationTable declTable)
		{
			if (tree.Options.Kind == SourceCodeKind.Script)
			{
				SyntaxAndDeclarationManager.AppendAllLoadedSyntaxTrees(treesBuilder, tree, scriptClassName, resolver, messageProvider, isSubmission, ordinalMapBuilder, loadDirectiveMapBuilder, loadedSyntaxTreeMapBuilder, declMapBuilder, ref declTable);
			}
			SyntaxAndDeclarationManager.AddSyntaxTreeToDeclarationMapAndTable(tree, scriptClassName, isSubmission, declMapBuilder, ref declTable);
			treesBuilder.Add(tree);
			ordinalMapBuilder.Add(tree, ordinalMapBuilder.Count);
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x000779D8 File Offset: 0x00075BD8
		private static void AppendAllLoadedSyntaxTrees(ArrayBuilder<SyntaxTree> treesBuilder, SyntaxTree tree, string scriptClassName, SourceReferenceResolver resolver, CommonMessageProvider messageProvider, bool isSubmission, IDictionary<SyntaxTree, int> ordinalMapBuilder, IDictionary<SyntaxTree, ImmutableArray<LoadDirective>> loadDirectiveMapBuilder, IDictionary<string, SyntaxTree> loadedSyntaxTreeMapBuilder, IDictionary<SyntaxTree, Lazy<RootSingleNamespaceDeclaration>> declMapBuilder, ref DeclarationTable declTable)
		{
			ArrayBuilder<LoadDirective> arrayBuilder = null;
			foreach (LoadDirectiveTriviaSyntax loadDirectiveTriviaSyntax in tree.GetCompilationUnitRoot(default(CancellationToken)).GetLoadDirectives())
			{
				SyntaxToken file = loadDirectiveTriviaSyntax.File;
				string text = (string)file.Value;
				if (text != null)
				{
					DiagnosticBag instance = DiagnosticBag.GetInstance();
					string text2 = null;
					if (resolver == null)
					{
						instance.Add(messageProvider.CreateDiagnostic(8099, loadDirectiveTriviaSyntax.Location));
					}
					else
					{
						text2 = resolver.ResolveReference(text, tree.FilePath);
						if (text2 == null)
						{
							instance.Add(messageProvider.CreateDiagnostic(1504, file.GetLocation(), new object[]
							{
								text,
								CSharpResources.CouldNotFindFile
							}));
						}
						else if (!loadedSyntaxTreeMapBuilder.ContainsKey(text2))
						{
							try
							{
								SyntaxTree syntaxTree = SyntaxFactory.ParseSyntaxTree(resolver.ReadText(text2), tree.Options, text2, default(CancellationToken));
								loadedSyntaxTreeMapBuilder.Add(syntaxTree.FilePath, syntaxTree);
								SyntaxAndDeclarationManager.AppendAllSyntaxTrees(treesBuilder, syntaxTree, scriptClassName, resolver, messageProvider, isSubmission, ordinalMapBuilder, loadDirectiveMapBuilder, loadedSyntaxTreeMapBuilder, declMapBuilder, ref declTable);
							}
							catch (Exception ex)
							{
								instance.Add(CommonCompiler.ToFileReadDiagnostics(messageProvider, ex, text2), file.GetLocation());
							}
						}
					}
					if (arrayBuilder == null)
					{
						arrayBuilder = ArrayBuilder<LoadDirective>.GetInstance();
					}
					arrayBuilder.Add(new LoadDirective(text2, instance.ToReadOnlyAndFree()));
				}
			}
			if (arrayBuilder != null)
			{
				loadDirectiveMapBuilder.Add(tree, arrayBuilder.ToImmutableAndFree());
			}
		}

		// Token: 0x0600164B RID: 5707 RVA: 0x00077B84 File Offset: 0x00075D84
		private static void AddSyntaxTreeToDeclarationMapAndTable(SyntaxTree tree, string scriptClassName, bool isSubmission, IDictionary<SyntaxTree, Lazy<RootSingleNamespaceDeclaration>> declMapBuilder, ref DeclarationTable declTable)
		{
			Lazy<RootSingleNamespaceDeclaration> lazy = new Lazy<RootSingleNamespaceDeclaration>(() => DeclarationTreeBuilder.ForTree(tree, scriptClassName, isSubmission));
			declMapBuilder.Add(tree, lazy);
			declTable = declTable.AddRootDeclaration(lazy);
		}

		// Token: 0x0600164C RID: 5708 RVA: 0x00077BD8 File Offset: 0x00075DD8
		public SyntaxAndDeclarationManager RemoveSyntaxTrees(HashSet<SyntaxTree> trees)
		{
			SyntaxAndDeclarationManager.State state = this._lazyState;
			ImmutableArray<SyntaxTree> immutableArray = this.ExternalSyntaxTrees.RemoveAll((SyntaxTree t) => trees.Contains(t));
			if (state == null)
			{
				return this.WithExternalSyntaxTrees(immutableArray);
			}
			ImmutableArray<SyntaxTree> syntaxTrees = state.SyntaxTrees;
			ImmutableDictionary<SyntaxTree, ImmutableArray<LoadDirective>> immutableDictionary = state.LoadDirectiveMap;
			ImmutableDictionary<string, SyntaxTree> immutableDictionary2 = state.LoadedSyntaxTreeMap;
			PooledHashSet<SyntaxTree> instance = PooledHashSet<SyntaxTree>.GetInstance();
			foreach (SyntaxTree syntaxTree in trees)
			{
				int num;
				ImmutableArray<LoadDirective> immutableArray2;
				SyntaxAndDeclarationManager.GetRemoveSet(syntaxTree, true, syntaxTrees, state.OrdinalMap, immutableDictionary, immutableDictionary2, instance, out num, out immutableArray2);
			}
			ArrayBuilder<SyntaxTree> instance2 = ArrayBuilder<SyntaxTree>.GetInstance();
			PooledDictionary<SyntaxTree, int> instance3 = PooledDictionary<SyntaxTree, int>.GetInstance();
			ImmutableDictionary<SyntaxTree, Lazy<RootSingleNamespaceDeclaration>>.Builder builder = state.RootNamespaces.ToBuilder();
			DeclarationTable declarationTable = state.DeclarationTable;
			foreach (SyntaxTree syntaxTree2 in syntaxTrees)
			{
				if (instance.Contains(syntaxTree2))
				{
					immutableDictionary = immutableDictionary.Remove(syntaxTree2);
					immutableDictionary2 = immutableDictionary2.Remove(syntaxTree2.FilePath);
					SyntaxAndDeclarationManager.RemoveSyntaxTreeFromDeclarationMapAndTable(syntaxTree2, builder, ref declarationTable);
				}
				else if (!SyntaxAndDeclarationManager.IsLoadedSyntaxTree(syntaxTree2, immutableDictionary2))
				{
					SyntaxAndDeclarationManager.UpdateSyntaxTreesAndOrdinalMapOnly(instance2, syntaxTree2, instance3, immutableDictionary, immutableDictionary2);
				}
			}
			instance.Free();
			state = new SyntaxAndDeclarationManager.State(instance2.ToImmutableAndFree(), instance3.ToImmutableDictionaryAndFree(), immutableDictionary, immutableDictionary2, builder.ToImmutableDictionary<SyntaxTree, Lazy<RootSingleNamespaceDeclaration>>(), declarationTable);
			return new SyntaxAndDeclarationManager(immutableArray, this.ScriptClassName, this.Resolver, this.MessageProvider, this.IsSubmission, state);
		}

		// Token: 0x0600164D RID: 5709 RVA: 0x00077D68 File Offset: 0x00075F68
		private static void GetRemoveSet(SyntaxTree oldTree, bool includeLoadedTrees, ImmutableArray<SyntaxTree> syntaxTrees, ImmutableDictionary<SyntaxTree, int> syntaxTreeOrdinalMap, ImmutableDictionary<SyntaxTree, ImmutableArray<LoadDirective>> loadDirectiveMap, ImmutableDictionary<string, SyntaxTree> loadedSyntaxTreeMap, HashSet<SyntaxTree> removeSet, out int totalReferencedTreeCount, out ImmutableArray<LoadDirective> oldLoadDirectives)
		{
			if (includeLoadedTrees && loadDirectiveMap.TryGetValue(oldTree, out oldLoadDirectives))
			{
				SyntaxAndDeclarationManager.GetRemoveSetForLoadedTrees(oldLoadDirectives, loadDirectiveMap, loadedSyntaxTreeMap, removeSet);
			}
			else
			{
				oldLoadDirectives = ImmutableArray<LoadDirective>.Empty;
			}
			removeSet.Add(oldTree);
			totalReferencedTreeCount = removeSet.Count;
			if (removeSet.Count > 1)
			{
				for (int i = syntaxTreeOrdinalMap[oldTree] + 1; i < syntaxTrees.Length; i++)
				{
					SyntaxTree syntaxTree = syntaxTrees[i];
					ImmutableArray<LoadDirective> immutableArray;
					if (loadDirectiveMap.TryGetValue(syntaxTree, out immutableArray))
					{
						foreach (LoadDirective loadDirective in immutableArray)
						{
							SyntaxTree syntaxTree2;
							if (SyntaxAndDeclarationManager.TryGetLoadedSyntaxTree(loadedSyntaxTreeMap, loadDirective, out syntaxTree2))
							{
								removeSet.Remove(syntaxTree2);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x00077E24 File Offset: 0x00076024
		private static void GetRemoveSetForLoadedTrees(ImmutableArray<LoadDirective> loadDirectives, ImmutableDictionary<SyntaxTree, ImmutableArray<LoadDirective>> loadDirectiveMap, ImmutableDictionary<string, SyntaxTree> loadedSyntaxTreeMap, HashSet<SyntaxTree> removeSet)
		{
			foreach (LoadDirective loadDirective in loadDirectives)
			{
				SyntaxTree syntaxTree;
				ImmutableArray<LoadDirective> immutableArray;
				if (loadDirective.ResolvedPath != null && SyntaxAndDeclarationManager.TryGetLoadedSyntaxTree(loadedSyntaxTreeMap, loadDirective, out syntaxTree) && removeSet.Add(syntaxTree) && loadDirectiveMap.TryGetValue(syntaxTree, out immutableArray))
				{
					SyntaxAndDeclarationManager.GetRemoveSetForLoadedTrees(immutableArray, loadDirectiveMap, loadedSyntaxTreeMap, removeSet);
				}
			}
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x00077E7C File Offset: 0x0007607C
		private static void RemoveSyntaxTreeFromDeclarationMapAndTable(SyntaxTree tree, IDictionary<SyntaxTree, Lazy<RootSingleNamespaceDeclaration>> declMap, ref DeclarationTable declTable)
		{
			Lazy<RootSingleNamespaceDeclaration> lazy = declMap[tree];
			declTable = declTable.RemoveRootDeclaration(lazy);
			declMap.Remove(tree);
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x00077EA4 File Offset: 0x000760A4
		public SyntaxAndDeclarationManager ReplaceSyntaxTree(SyntaxTree oldTree, SyntaxTree newTree)
		{
			SyntaxAndDeclarationManager.State state = this._lazyState;
			ImmutableArray<SyntaxTree> immutableArray = this.ExternalSyntaxTrees.Replace(oldTree, newTree);
			if (state == null)
			{
				return this.WithExternalSyntaxTrees(immutableArray);
			}
			IList<LoadDirectiveTriviaSyntax> loadDirectives = newTree.GetCompilationUnitRoot(default(CancellationToken)).GetLoadDirectives();
			bool flag = !Enumerable.SequenceEqual<LoadDirectiveTriviaSyntax>(oldTree.GetCompilationUnitRoot(default(CancellationToken)).GetLoadDirectives(), loadDirectives);
			ImmutableArray<SyntaxTree> syntaxTrees = state.SyntaxTrees;
			ImmutableDictionary<SyntaxTree, int> immutableDictionary = state.OrdinalMap;
			ImmutableDictionary<SyntaxTree, ImmutableArray<LoadDirective>> loadDirectiveMap = state.LoadDirectiveMap;
			ImmutableDictionary<string, SyntaxTree> loadedSyntaxTreeMap = state.LoadedSyntaxTreeMap;
			PooledHashSet<SyntaxTree> instance = PooledHashSet<SyntaxTree>.GetInstance();
			int num;
			ImmutableArray<LoadDirective> immutableArray2;
			SyntaxAndDeclarationManager.GetRemoveSet(oldTree, flag, syntaxTrees, immutableDictionary, loadDirectiveMap, loadedSyntaxTreeMap, instance, out num, out immutableArray2);
			ImmutableDictionary<SyntaxTree, ImmutableArray<LoadDirective>>.Builder builder = loadDirectiveMap.ToBuilder();
			ImmutableDictionary<string, SyntaxTree>.Builder builder2 = loadedSyntaxTreeMap.ToBuilder();
			ImmutableDictionary<SyntaxTree, Lazy<RootSingleNamespaceDeclaration>>.Builder builder3 = state.RootNamespaces.ToBuilder();
			DeclarationTable declarationTable = state.DeclarationTable;
			foreach (SyntaxTree syntaxTree in instance)
			{
				builder.Remove(syntaxTree);
				builder2.Remove(syntaxTree.FilePath);
				SyntaxAndDeclarationManager.RemoveSyntaxTreeFromDeclarationMapAndTable(syntaxTree, builder3, ref declarationTable);
			}
			instance.Free();
			int num2 = immutableDictionary[oldTree];
			ImmutableArray<SyntaxTree> immutableArray3;
			if (flag)
			{
				ArrayBuilder<SyntaxTree> instance2 = ArrayBuilder<SyntaxTree>.GetInstance();
				PooledDictionary<SyntaxTree, int> instance3 = PooledDictionary<SyntaxTree, int>.GetInstance();
				for (int i = 0; i <= num2 - num; i++)
				{
					SyntaxTree syntaxTree2 = syntaxTrees[i];
					instance2.Add(syntaxTree2);
					instance3.Add(syntaxTree2, i);
				}
				SyntaxAndDeclarationManager.AppendAllSyntaxTrees(instance2, newTree, this.ScriptClassName, this.Resolver, this.MessageProvider, this.IsSubmission, instance3, builder, builder2, builder3, ref declarationTable);
				for (int j = num2 + 1; j < syntaxTrees.Length; j++)
				{
					SyntaxTree syntaxTree3 = syntaxTrees[j];
					if (!SyntaxAndDeclarationManager.IsLoadedSyntaxTree(syntaxTree3, loadedSyntaxTreeMap))
					{
						SyntaxAndDeclarationManager.UpdateSyntaxTreesAndOrdinalMapOnly(instance2, syntaxTree3, instance3, loadDirectiveMap, loadedSyntaxTreeMap);
					}
				}
				immutableArray3 = instance2.ToImmutableAndFree();
				immutableDictionary = instance3.ToImmutableDictionaryAndFree();
			}
			else
			{
				SyntaxAndDeclarationManager.AddSyntaxTreeToDeclarationMapAndTable(newTree, this.ScriptClassName, this.IsSubmission, builder3, ref declarationTable);
				if (Enumerable.Any<LoadDirectiveTriviaSyntax>(loadDirectives))
				{
					builder[newTree] = immutableArray2;
				}
				immutableArray3 = syntaxTrees.SetItem(num2, newTree);
				immutableDictionary = immutableDictionary.Remove(oldTree);
				immutableDictionary = immutableDictionary.SetItem(newTree, num2);
			}
			state = new SyntaxAndDeclarationManager.State(immutableArray3, immutableDictionary, builder.ToImmutable(), builder2.ToImmutable(), builder3.ToImmutable(), declarationTable);
			return new SyntaxAndDeclarationManager(immutableArray, this.ScriptClassName, this.Resolver, this.MessageProvider, this.IsSubmission, state);
		}

		// Token: 0x06001651 RID: 5713 RVA: 0x0007812C File Offset: 0x0007632C
		internal SyntaxAndDeclarationManager WithExternalSyntaxTrees(ImmutableArray<SyntaxTree> trees)
		{
			return new SyntaxAndDeclarationManager(trees, this.ScriptClassName, this.Resolver, this.MessageProvider, this.IsSubmission, null);
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x00078150 File Offset: 0x00076350
		internal static bool IsLoadedSyntaxTree(SyntaxTree tree, ImmutableDictionary<string, SyntaxTree> loadedSyntaxTreeMap)
		{
			SyntaxTree syntaxTree;
			return loadedSyntaxTreeMap.TryGetValue(tree.FilePath, out syntaxTree) && tree == syntaxTree;
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x00078174 File Offset: 0x00076374
		private static void UpdateSyntaxTreesAndOrdinalMapOnly(ArrayBuilder<SyntaxTree> treesBuilder, SyntaxTree tree, IDictionary<SyntaxTree, int> ordinalMapBuilder, ImmutableDictionary<SyntaxTree, ImmutableArray<LoadDirective>> loadDirectiveMap, ImmutableDictionary<string, SyntaxTree> loadedSyntaxTreeMap)
		{
			ImmutableArray<LoadDirective> immutableArray;
			if (tree.Options.Kind == SourceCodeKind.Script && loadDirectiveMap.TryGetValue(tree, out immutableArray))
			{
				foreach (LoadDirective loadDirective in immutableArray)
				{
					SyntaxTree syntaxTree;
					if (loadDirective.ResolvedPath != null && SyntaxAndDeclarationManager.TryGetLoadedSyntaxTree(loadedSyntaxTreeMap, loadDirective, out syntaxTree))
					{
						SyntaxAndDeclarationManager.UpdateSyntaxTreesAndOrdinalMapOnly(treesBuilder, syntaxTree, ordinalMapBuilder, loadDirectiveMap, loadedSyntaxTreeMap);
					}
				}
			}
			treesBuilder.Add(tree);
			ordinalMapBuilder.Add(tree, ordinalMapBuilder.Count);
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x000781E8 File Offset: 0x000763E8
		internal bool MayHaveReferenceDirectives()
		{
			SyntaxAndDeclarationManager.State lazyState = this._lazyState;
			if (lazyState == null)
			{
				return this.ExternalSyntaxTrees.Any<SyntaxTree>((SyntaxTree t) => t.HasReferenceOrLoadDirectives());
			}
			return Enumerable.Any<ReferenceDirective>(lazyState.DeclarationTable.ReferenceDirectives);
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x0007823A File Offset: 0x0007643A
		private static bool TryGetLoadedSyntaxTree(ImmutableDictionary<string, SyntaxTree> loadedSyntaxTreeMap, LoadDirective directive, out SyntaxTree loadedTree)
		{
			return loadedSyntaxTreeMap.TryGetValue(directive.ResolvedPath, out loadedTree);
		}

		// Token: 0x04000648 RID: 1608
		private SyntaxAndDeclarationManager.State _lazyState;

		// Token: 0x020006BB RID: 1723
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal sealed class State
		{
			// Token: 0x06008438 RID: 33848 RVA: 0x001DF3C2 File Offset: 0x001DD5C2
			internal State([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<SyntaxTree> syntaxTrees, ImmutableDictionary<SyntaxTree, int> syntaxTreeOrdinalMap, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 1, 0 })] ImmutableDictionary<SyntaxTree, ImmutableArray<LoadDirective>> loadDirectiveMap, ImmutableDictionary<string, SyntaxTree> loadedSyntaxTreeMap, ImmutableDictionary<SyntaxTree, Lazy<RootSingleNamespaceDeclaration>> rootNamespaces, DeclarationTable declarationTable)
			{
				this.SyntaxTrees = syntaxTrees;
				this.OrdinalMap = syntaxTreeOrdinalMap;
				this.LoadDirectiveMap = loadDirectiveMap;
				this.LoadedSyntaxTreeMap = loadedSyntaxTreeMap;
				this.RootNamespaces = rootNamespaces;
				this.DeclarationTable = declarationTable;
			}

			// Token: 0x04002166 RID: 8550
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			internal readonly ImmutableArray<SyntaxTree> SyntaxTrees;

			// Token: 0x04002167 RID: 8551
			internal readonly ImmutableDictionary<SyntaxTree, int> OrdinalMap;

			// Token: 0x04002168 RID: 8552
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 1, 0 })]
			internal readonly ImmutableDictionary<SyntaxTree, ImmutableArray<LoadDirective>> LoadDirectiveMap;

			// Token: 0x04002169 RID: 8553
			internal readonly ImmutableDictionary<string, SyntaxTree> LoadedSyntaxTreeMap;

			// Token: 0x0400216A RID: 8554
			internal readonly ImmutableDictionary<SyntaxTree, Lazy<RootSingleNamespaceDeclaration>> RootNamespaces;

			// Token: 0x0400216B RID: 8555
			internal readonly DeclarationTable DeclarationTable;
		}
	}
}
