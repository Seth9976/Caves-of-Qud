using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.Symbols;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200010D RID: 269
	[NullableContext(1)]
	[Nullable(0)]
	internal struct CachedUseSiteInfo<TAssemblySymbol> where TAssemblySymbol : class, IAssemblySymbolInternal
	{
		// Token: 0x06000959 RID: 2393 RVA: 0x0001AC20 File Offset: 0x00018E20
		private CachedUseSiteInfo(object info)
		{
			this._info = info;
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x0001AC29 File Offset: 0x00018E29
		public bool IsInitialized
		{
			get
			{
				return this._info != CachedUseSiteInfo<TAssemblySymbol>.Sentinel;
			}
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x0001AC3B File Offset: 0x00018E3B
		[NullableContext(2)]
		public void Initialize(DiagnosticInfo diagnosticInfo)
		{
			this.Initialize(diagnosticInfo, ImmutableHashSet<TAssemblySymbol>.Empty);
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x0001AC49 File Offset: 0x00018E49
		[NullableContext(2)]
		public void Initialize(TAssemblySymbol primaryDependency, [Nullable(new byte[] { 0, 1 })] UseSiteInfo<TAssemblySymbol> useSiteInfo)
		{
			this.Initialize(useSiteInfo.DiagnosticInfo, CachedUseSiteInfo<TAssemblySymbol>.GetDependenciesToCache(primaryDependency, useSiteInfo));
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x0001AC60 File Offset: 0x00018E60
		private static ImmutableHashSet<TAssemblySymbol> GetDependenciesToCache([Nullable(2)] TAssemblySymbol primaryDependency, [Nullable(new byte[] { 0, 1 })] UseSiteInfo<TAssemblySymbol> useSiteInfo)
		{
			ImmutableHashSet<TAssemblySymbol> immutableHashSet = useSiteInfo.SecondaryDependencies ?? ImmutableHashSet<TAssemblySymbol>.Empty;
			if (useSiteInfo.PrimaryDependency != null)
			{
				return immutableHashSet.Remove(useSiteInfo.PrimaryDependency);
			}
			return immutableHashSet;
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x0001AC98 File Offset: 0x00018E98
		[return: Nullable(new byte[] { 0, 1 })]
		public UseSiteInfo<TAssemblySymbol> ToUseSiteInfo(TAssemblySymbol primaryDependency)
		{
			DiagnosticInfo diagnosticInfo;
			ImmutableHashSet<TAssemblySymbol> immutableHashSet;
			CachedUseSiteInfo<TAssemblySymbol>.Expand(this._info, out diagnosticInfo, out immutableHashSet);
			if (diagnosticInfo != null && diagnosticInfo.Severity == DiagnosticSeverity.Error)
			{
				return new UseSiteInfo<TAssemblySymbol>(diagnosticInfo);
			}
			return new UseSiteInfo<TAssemblySymbol>(diagnosticInfo, primaryDependency, immutableHashSet);
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0001ACCF File Offset: 0x00018ECF
		private void Initialize([Nullable(2)] DiagnosticInfo diagnosticInfo, ImmutableHashSet<TAssemblySymbol> dependencies)
		{
			this._info = CachedUseSiteInfo<TAssemblySymbol>.Compact(diagnosticInfo, dependencies);
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x0001ACE0 File Offset: 0x00018EE0
		[NullableContext(2)]
		private static object Compact(DiagnosticInfo diagnosticInfo, [Nullable(1)] ImmutableHashSet<TAssemblySymbol> dependencies)
		{
			object obj;
			if (dependencies.IsEmpty)
			{
				obj = diagnosticInfo;
			}
			else if (diagnosticInfo == null)
			{
				obj = dependencies;
			}
			else
			{
				obj = new CachedUseSiteInfo<TAssemblySymbol>.Boxed(diagnosticInfo, dependencies);
			}
			return obj;
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x0001AD0C File Offset: 0x00018F0C
		[NullableContext(2)]
		public void InterlockedCompareExchange(TAssemblySymbol primaryDependency, [Nullable(new byte[] { 0, 1 })] UseSiteInfo<TAssemblySymbol> value)
		{
			if (this._info == CachedUseSiteInfo<TAssemblySymbol>.Sentinel)
			{
				object obj = CachedUseSiteInfo<TAssemblySymbol>.Compact(value.DiagnosticInfo, CachedUseSiteInfo<TAssemblySymbol>.GetDependenciesToCache(primaryDependency, value));
				Interlocked.CompareExchange(ref this._info, obj, CachedUseSiteInfo<TAssemblySymbol>.Sentinel);
			}
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x0001AD4C File Offset: 0x00018F4C
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		public UseSiteInfo<TAssemblySymbol> InterlockedInitialize(TAssemblySymbol primaryDependency, [Nullable(new byte[] { 0, 1 })] UseSiteInfo<TAssemblySymbol> value)
		{
			object obj = CachedUseSiteInfo<TAssemblySymbol>.Compact(value.DiagnosticInfo, CachedUseSiteInfo<TAssemblySymbol>.GetDependenciesToCache(primaryDependency, value));
			obj = Interlocked.CompareExchange(ref this._info, obj, null);
			if (obj == null)
			{
				return value;
			}
			DiagnosticInfo diagnosticInfo;
			ImmutableHashSet<TAssemblySymbol> immutableHashSet;
			CachedUseSiteInfo<TAssemblySymbol>.Expand(obj, out diagnosticInfo, out immutableHashSet);
			return new UseSiteInfo<TAssemblySymbol>(diagnosticInfo, value.PrimaryDependency, immutableHashSet);
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0001AD98 File Offset: 0x00018F98
		[NullableContext(2)]
		private static void Expand(object info, out DiagnosticInfo diagnosticInfo, [Nullable(new byte[] { 2, 1 })] out ImmutableHashSet<TAssemblySymbol> dependencies)
		{
			if (info == null)
			{
				diagnosticInfo = null;
				dependencies = null;
				return;
			}
			DiagnosticInfo diagnosticInfo2 = info as DiagnosticInfo;
			if (diagnosticInfo2 != null)
			{
				diagnosticInfo = diagnosticInfo2;
				dependencies = null;
				return;
			}
			ImmutableHashSet<TAssemblySymbol> immutableHashSet = info as ImmutableHashSet<TAssemblySymbol>;
			if (immutableHashSet == null)
			{
				CachedUseSiteInfo<TAssemblySymbol>.Boxed boxed = (CachedUseSiteInfo<TAssemblySymbol>.Boxed)info;
				diagnosticInfo = boxed.DiagnosticInfo;
				dependencies = boxed.Dependencies;
				return;
			}
			diagnosticInfo = null;
			dependencies = immutableHashSet;
		}

		// Token: 0x0400029F RID: 671
		[Nullable(2)]
		private object _info;

		// Token: 0x040002A0 RID: 672
		private static readonly object Sentinel = new object();

		// Token: 0x040002A1 RID: 673
		[Nullable(new byte[] { 0, 1 })]
		public static readonly CachedUseSiteInfo<TAssemblySymbol> Uninitialized = new CachedUseSiteInfo<TAssemblySymbol>(CachedUseSiteInfo<TAssemblySymbol>.Sentinel);

		// Token: 0x0200053A RID: 1338
		[Nullable(0)]
		private class Boxed
		{
			// Token: 0x060033A2 RID: 13218 RVA: 0x00091E16 File Offset: 0x00090016
			public Boxed(DiagnosticInfo diagnosticInfo, ImmutableHashSet<TAssemblySymbol> dependencies)
			{
				this.DiagnosticInfo = diagnosticInfo;
				this.Dependencies = dependencies;
			}

			// Token: 0x040014D5 RID: 5333
			public readonly DiagnosticInfo DiagnosticInfo;

			// Token: 0x040014D6 RID: 5334
			public readonly ImmutableHashSet<TAssemblySymbol> Dependencies;
		}
	}
}
