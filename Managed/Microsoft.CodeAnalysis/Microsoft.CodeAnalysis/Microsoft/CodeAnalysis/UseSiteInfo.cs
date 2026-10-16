using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Symbols;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200010B RID: 267
	[NullableContext(2)]
	[Nullable(0)]
	internal readonly struct UseSiteInfo<[Nullable(1)] TAssemblySymbol> where TAssemblySymbol : class, IAssemblySymbolInternal
	{
		// Token: 0x06000936 RID: 2358 RVA: 0x0001A559 File Offset: 0x00018759
		public UseSiteInfo(TAssemblySymbol primaryDependency)
		{
			this = new UseSiteInfo<TAssemblySymbol>(null, primaryDependency, null);
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0001A564 File Offset: 0x00018764
		public UseSiteInfo([Nullable(new byte[] { 2, 1 })] ImmutableHashSet<TAssemblySymbol> secondaryDependencies)
		{
			this = new UseSiteInfo<TAssemblySymbol>(null, default(TAssemblySymbol), secondaryDependencies);
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0001A584 File Offset: 0x00018784
		public UseSiteInfo(DiagnosticInfo diagnosticInfo)
		{
			this = new UseSiteInfo<TAssemblySymbol>(diagnosticInfo, default(TAssemblySymbol), null);
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0001A5A2 File Offset: 0x000187A2
		public UseSiteInfo(DiagnosticInfo diagnosticInfo, TAssemblySymbol primaryDependency)
		{
			this = new UseSiteInfo<TAssemblySymbol>(diagnosticInfo, primaryDependency, null);
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0001A5AD File Offset: 0x000187AD
		public UseSiteInfo(DiagnosticInfo diagnosticInfo, TAssemblySymbol primaryDependency, [Nullable(new byte[] { 2, 1 })] ImmutableHashSet<TAssemblySymbol> secondaryDependencies)
		{
			this.DiagnosticInfo = diagnosticInfo;
			this.PrimaryDependency = primaryDependency;
			this.SecondaryDependencies = secondaryDependencies ?? ImmutableHashSet<TAssemblySymbol>.Empty;
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x0001A5CD File Offset: 0x000187CD
		public bool IsEmpty
		{
			get
			{
				if (this.DiagnosticInfo == null && this.PrimaryDependency == null)
				{
					ImmutableHashSet<TAssemblySymbol> secondaryDependencies = this.SecondaryDependencies;
					return secondaryDependencies == null || secondaryDependencies.IsEmpty;
				}
				return false;
			}
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x0001A5F7 File Offset: 0x000187F7
		[return: Nullable(new byte[] { 0, 1 })]
		public UseSiteInfo<TAssemblySymbol> AdjustDiagnosticInfo(DiagnosticInfo diagnosticInfo)
		{
			if (this.DiagnosticInfo == diagnosticInfo)
			{
				return this;
			}
			if (diagnosticInfo != null && diagnosticInfo.Severity == DiagnosticSeverity.Error)
			{
				return new UseSiteInfo<TAssemblySymbol>(diagnosticInfo);
			}
			return new UseSiteInfo<TAssemblySymbol>(diagnosticInfo, this.PrimaryDependency, this.SecondaryDependencies);
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x0001A630 File Offset: 0x00018830
		public void MergeDependencies(ref TAssemblySymbol primaryDependency, [Nullable(new byte[] { 2, 1 })] ref ImmutableHashSet<TAssemblySymbol> secondaryDependencies)
		{
			secondaryDependencies = (secondaryDependencies ?? ImmutableHashSet<TAssemblySymbol>.Empty).Union(this.SecondaryDependencies ?? ImmutableHashSet<TAssemblySymbol>.Empty);
			if (primaryDependency == null)
			{
				primaryDependency = this.PrimaryDependency;
			}
			if (!object.Equals(primaryDependency, this.PrimaryDependency) && this.PrimaryDependency != null)
			{
				secondaryDependencies = secondaryDependencies.Add(this.PrimaryDependency);
			}
		}

		// Token: 0x04000297 RID: 663
		public readonly DiagnosticInfo DiagnosticInfo;

		// Token: 0x04000298 RID: 664
		public readonly TAssemblySymbol PrimaryDependency;

		// Token: 0x04000299 RID: 665
		[Nullable(new byte[] { 2, 1 })]
		public readonly ImmutableHashSet<TAssemblySymbol> SecondaryDependencies;
	}
}
