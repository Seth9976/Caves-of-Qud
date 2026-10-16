using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x0200030E RID: 782
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class CachingSemanticModelProvider : SemanticModelProvider
	{
		// Token: 0x0600225F RID: 8799 RVA: 0x0006D717 File Offset: 0x0006B917
		public CachingSemanticModelProvider()
		{
			this._providerCache = new ConditionalWeakTable<Compilation, CachingSemanticModelProvider.PerCompilationProvider>();
		}

		// Token: 0x06002260 RID: 8800 RVA: 0x0006D72A File Offset: 0x0006B92A
		public override SemanticModel GetSemanticModel(SyntaxTree tree, Compilation compilation, bool ignoreAccessibility = false)
		{
			return this._providerCache.GetValue(compilation, CachingSemanticModelProvider.s_createProviderCallback).GetSemanticModel(tree, ignoreAccessibility);
		}

		// Token: 0x06002261 RID: 8801 RVA: 0x0006D744 File Offset: 0x0006B944
		internal void ClearCache(SyntaxTree tree, Compilation compilation)
		{
			CachingSemanticModelProvider.PerCompilationProvider perCompilationProvider;
			if (this._providerCache.TryGetValue(compilation, ref perCompilationProvider))
			{
				perCompilationProvider.ClearCachedSemanticModel(tree);
			}
		}

		// Token: 0x06002262 RID: 8802 RVA: 0x0006D768 File Offset: 0x0006B968
		internal void ClearCache(Compilation compilation)
		{
			this._providerCache.Remove(compilation);
		}

		// Token: 0x04000EC1 RID: 3777
		private static readonly ConditionalWeakTable<Compilation, CachingSemanticModelProvider.PerCompilationProvider>.CreateValueCallback s_createProviderCallback = (Compilation compilation) => new CachingSemanticModelProvider.PerCompilationProvider(compilation);

		// Token: 0x04000EC2 RID: 3778
		private readonly ConditionalWeakTable<Compilation, CachingSemanticModelProvider.PerCompilationProvider> _providerCache;

		// Token: 0x020006E3 RID: 1763
		[Nullable(0)]
		private sealed class PerCompilationProvider
		{
			// Token: 0x06003ACC RID: 15052 RVA: 0x000A5F7C File Offset: 0x000A417C
			public PerCompilationProvider(Compilation compilation)
			{
				this._compilation = compilation;
				this._semanticModelsMap = new ConcurrentDictionary<SyntaxTree, SemanticModel>();
				this._createSemanticModel = (SyntaxTree tree) => compilation.CreateSemanticModel(tree, false);
			}

			// Token: 0x06003ACD RID: 15053 RVA: 0x000A5FC5 File Offset: 0x000A41C5
			public SemanticModel GetSemanticModel(SyntaxTree tree, bool ignoreAccessibility)
			{
				if (ignoreAccessibility)
				{
					return this._compilation.CreateSemanticModel(tree, true);
				}
				return this._semanticModelsMap.GetOrAdd(tree, this._createSemanticModel);
			}

			// Token: 0x06003ACE RID: 15054 RVA: 0x000A5FEC File Offset: 0x000A41EC
			public void ClearCachedSemanticModel(SyntaxTree tree)
			{
				SemanticModel semanticModel;
				this._semanticModelsMap.TryRemove(tree, ref semanticModel);
			}

			// Token: 0x04001B14 RID: 6932
			private readonly Compilation _compilation;

			// Token: 0x04001B15 RID: 6933
			private readonly ConcurrentDictionary<SyntaxTree, SemanticModel> _semanticModelsMap;

			// Token: 0x04001B16 RID: 6934
			private readonly Func<SyntaxTree, SemanticModel> _createSemanticModel;
		}
	}
}
