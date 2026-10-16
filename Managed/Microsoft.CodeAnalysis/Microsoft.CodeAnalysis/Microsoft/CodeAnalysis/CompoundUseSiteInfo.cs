using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200010C RID: 268
	internal struct CompoundUseSiteInfo<[Nullable(1)] TAssemblySymbol> where TAssemblySymbol : class, IAssemblySymbolInternal
	{
		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x0001A6B0 File Offset: 0x000188B0
		[Nullable(new byte[] { 0, 1 })]
		public static CompoundUseSiteInfo<TAssemblySymbol> Discarded
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return new CompoundUseSiteInfo<TAssemblySymbol>(CompoundUseSiteInfo<TAssemblySymbol>.DiscardLevel.DiagnosticsAndDependencies);
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x0001A6B8 File Offset: 0x000188B8
		[Nullable(new byte[] { 0, 1 })]
		public static CompoundUseSiteInfo<TAssemblySymbol> DiscardedDependencies
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return new CompoundUseSiteInfo<TAssemblySymbol>(CompoundUseSiteInfo<TAssemblySymbol>.DiscardLevel.Dependencies);
			}
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0001A6C0 File Offset: 0x000188C0
		[NullableContext(1)]
		public CompoundUseSiteInfo(TAssemblySymbol assemblyBeingBuilt)
		{
			this = default(CompoundUseSiteInfo<TAssemblySymbol>);
			this._assemblyBeingBuilt = assemblyBeingBuilt;
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0001A6D0 File Offset: 0x000188D0
		[NullableContext(1)]
		public CompoundUseSiteInfo([Nullable(new byte[] { 2, 1 })] BindingDiagnosticBag<TAssemblySymbol> futureDestination, TAssemblySymbol assemblyBeingBuilt)
		{
			this = default(CompoundUseSiteInfo<TAssemblySymbol>);
			if (futureDestination == null)
			{
				this._discardLevel = CompoundUseSiteInfo<TAssemblySymbol>.DiscardLevel.DiagnosticsAndDependencies;
				return;
			}
			if (!futureDestination.AccumulatesDependencies)
			{
				this._discardLevel = CompoundUseSiteInfo<TAssemblySymbol>.DiscardLevel.Dependencies;
				return;
			}
			this._discardLevel = CompoundUseSiteInfo<TAssemblySymbol>.DiscardLevel.None;
			this._assemblyBeingBuilt = assemblyBeingBuilt;
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0001A702 File Offset: 0x00018902
		public CompoundUseSiteInfo([Nullable(new byte[] { 0, 1 })] CompoundUseSiteInfo<TAssemblySymbol> template)
		{
			this = default(CompoundUseSiteInfo<TAssemblySymbol>);
			this._discardLevel = template._discardLevel;
			this._assemblyBeingBuilt = template._assemblyBeingBuilt;
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x0001A723 File Offset: 0x00018923
		private CompoundUseSiteInfo(CompoundUseSiteInfo<TAssemblySymbol>.DiscardLevel discardLevel)
		{
			this = default(CompoundUseSiteInfo<TAssemblySymbol>);
			this._discardLevel = discardLevel;
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x0001A733 File Offset: 0x00018933
		[Nullable(2)]
		public TAssemblySymbol AssemblyBeingBuilt
		{
			[NullableContext(2)]
			get
			{
				return this._assemblyBeingBuilt;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x0001A73B File Offset: 0x0001893B
		private CompoundUseSiteInfo<TAssemblySymbol>.DiscardLevel DiscardLevelWithValidation
		{
			get
			{
				return this._discardLevel;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000946 RID: 2374 RVA: 0x0001A743 File Offset: 0x00018943
		public bool AccumulatesDiagnostics
		{
			get
			{
				return this.DiscardLevelWithValidation != CompoundUseSiteInfo<TAssemblySymbol>.DiscardLevel.DiagnosticsAndDependencies;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x0001A751 File Offset: 0x00018951
		[Nullable(new byte[] { 2, 1 })]
		public IReadOnlyCollection<DiagnosticInfo> Diagnostics
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get
			{
				return this._diagnostics;
			}
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x0001A759 File Offset: 0x00018959
		[Conditional("DEBUG")]
		private readonly void AssertInternalConsistency()
		{
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x0001A75B File Offset: 0x0001895B
		public bool AccumulatesDependencies
		{
			get
			{
				return this.DiscardLevelWithValidation == CompoundUseSiteInfo<TAssemblySymbol>.DiscardLevel.None;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x0600094A RID: 2378 RVA: 0x0001A766 File Offset: 0x00018966
		[Nullable(new byte[] { 2, 1 })]
		public IReadOnlyCollection<TAssemblySymbol> Dependencies
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get
			{
				return this._dependencies;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x0600094B RID: 2379 RVA: 0x0001A76E File Offset: 0x0001896E
		public bool HasErrors
		{
			get
			{
				return this._hasErrors;
			}
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x0001A776 File Offset: 0x00018976
		public void AddDiagnostics([Nullable(new byte[] { 0, 1 })] UseSiteInfo<TAssemblySymbol> info)
		{
			if (!this.AccumulatesDiagnostics)
			{
				return;
			}
			if (HashSetExtensions.InitializeAndAdd<DiagnosticInfo>(ref this._diagnostics, info.DiagnosticInfo))
			{
				DiagnosticInfo diagnosticInfo = info.DiagnosticInfo;
				if (diagnosticInfo != null && diagnosticInfo.Severity == DiagnosticSeverity.Error)
				{
					this.RecordPresenceOfAnError();
				}
			}
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0001A7B1 File Offset: 0x000189B1
		private void RecordPresenceOfAnError()
		{
			if (!this._hasErrors)
			{
				this._hasErrors = true;
				this._dependencies = null;
			}
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x0001A7CC File Offset: 0x000189CC
		public void AddDiagnostics([Nullable(new byte[] { 2, 1 })] ICollection<DiagnosticInfo> diagnostics)
		{
			if (!this.AccumulatesDiagnostics)
			{
				return;
			}
			if (!diagnostics.IsNullOrEmpty<DiagnosticInfo>())
			{
				if (this._diagnostics == null)
				{
					this._diagnostics = new HashSet<DiagnosticInfo>();
				}
				foreach (DiagnosticInfo diagnosticInfo in diagnostics)
				{
					if (this._diagnostics.Add(diagnosticInfo) && diagnosticInfo != null && diagnosticInfo.Severity == DiagnosticSeverity.Error)
					{
						this.RecordPresenceOfAnError();
					}
				}
			}
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x0001A854 File Offset: 0x00018A54
		public void AddDiagnostics([Nullable(new byte[] { 2, 1 })] IReadOnlyCollection<DiagnosticInfo> diagnostics)
		{
			if (!this.AccumulatesDiagnostics)
			{
				return;
			}
			if (!diagnostics.IsNullOrEmpty<DiagnosticInfo>())
			{
				if (this._diagnostics == null)
				{
					this._diagnostics = new HashSet<DiagnosticInfo>();
				}
				foreach (DiagnosticInfo diagnosticInfo in diagnostics)
				{
					if (this._diagnostics.Add(diagnosticInfo) && diagnosticInfo != null && diagnosticInfo.Severity == DiagnosticSeverity.Error)
					{
						this.RecordPresenceOfAnError();
					}
				}
			}
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x0001A8DC File Offset: 0x00018ADC
		public void AddDiagnostics([Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticInfo> diagnostics)
		{
			if (!this.AccumulatesDiagnostics)
			{
				return;
			}
			if (!diagnostics.IsDefaultOrEmpty)
			{
				if (this._diagnostics == null)
				{
					this._diagnostics = new HashSet<DiagnosticInfo>();
				}
				foreach (DiagnosticInfo diagnosticInfo in diagnostics)
				{
					if (this._diagnostics.Add(diagnosticInfo) && diagnosticInfo != null && diagnosticInfo.Severity == DiagnosticSeverity.Error)
					{
						this.RecordPresenceOfAnError();
					}
				}
			}
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x0001A94C File Offset: 0x00018B4C
		public void AddDependencies([Nullable(new byte[] { 0, 1 })] UseSiteInfo<TAssemblySymbol> info)
		{
			if (!this._hasErrors && this.AccumulatesDependencies)
			{
				if (info.PrimaryDependency != this._assemblyBeingBuilt)
				{
					HashSetExtensions.InitializeAndAdd<TAssemblySymbol>(ref this._dependencies, info.PrimaryDependency);
				}
				ImmutableHashSet<TAssemblySymbol> secondaryDependencies = info.SecondaryDependencies;
				if (secondaryDependencies != null && !secondaryDependencies.IsEmpty && (this._assemblyBeingBuilt == null || info.SecondaryDependencies.AsSingleton<TAssemblySymbol>() != this._assemblyBeingBuilt))
				{
					HashSet<TAssemblySymbol> hashSet;
					if ((hashSet = this._dependencies) == null)
					{
						hashSet = (this._dependencies = new HashSet<TAssemblySymbol>());
					}
					hashSet.AddAll<TAssemblySymbol>(info.SecondaryDependencies);
				}
			}
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x0001A9FE File Offset: 0x00018BFE
		public void AddDependencies([Nullable(new byte[] { 0, 1 })] CompoundUseSiteInfo<TAssemblySymbol> info)
		{
			if (!this._hasErrors && this.AccumulatesDependencies)
			{
				this.AddDependencies(info.Dependencies);
			}
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x0001AA20 File Offset: 0x00018C20
		public void AddDependencies([Nullable(new byte[] { 2, 1 })] ICollection<TAssemblySymbol> dependencies)
		{
			if (!this._hasErrors && this.AccumulatesDependencies && !dependencies.IsNullOrEmpty<TAssemblySymbol>() && (this._assemblyBeingBuilt == null || dependencies.AsSingleton<TAssemblySymbol>() != this._assemblyBeingBuilt))
			{
				HashSet<TAssemblySymbol> hashSet;
				if ((hashSet = this._dependencies) == null)
				{
					hashSet = (this._dependencies = new HashSet<TAssemblySymbol>());
				}
				hashSet.AddAll<TAssemblySymbol>(dependencies);
			}
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x0001AA8C File Offset: 0x00018C8C
		public void AddDependencies([Nullable(new byte[] { 2, 1 })] IReadOnlyCollection<TAssemblySymbol> dependencies)
		{
			if (!this._hasErrors && this.AccumulatesDependencies && !dependencies.IsNullOrEmpty<TAssemblySymbol>() && (this._assemblyBeingBuilt == null || dependencies.AsSingleton<TAssemblySymbol>() != this._assemblyBeingBuilt))
			{
				HashSet<TAssemblySymbol> hashSet;
				if ((hashSet = this._dependencies) == null)
				{
					hashSet = (this._dependencies = new HashSet<TAssemblySymbol>());
				}
				hashSet.AddAll<TAssemblySymbol>(dependencies);
			}
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x0001AAF8 File Offset: 0x00018CF8
		public void AddDependencies([Nullable(new byte[] { 0, 1 })] ImmutableArray<TAssemblySymbol> dependencies)
		{
			if (!this._hasErrors && this.AccumulatesDependencies && !dependencies.IsDefaultOrEmpty && (this._assemblyBeingBuilt == null || dependencies.Length != 1 || dependencies[0] != this._assemblyBeingBuilt))
			{
				HashSet<TAssemblySymbol> hashSet;
				if ((hashSet = this._dependencies) == null)
				{
					hashSet = (this._dependencies = new HashSet<TAssemblySymbol>());
				}
				hashSet.AddAll<TAssemblySymbol>(dependencies);
			}
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x0001AB70 File Offset: 0x00018D70
		public void MergeAndClear([Nullable(new byte[] { 0, 1 })] ref CompoundUseSiteInfo<TAssemblySymbol> other)
		{
			if (!this.AccumulatesDiagnostics)
			{
				other._diagnostics = null;
				other._dependencies = null;
				other._hasErrors = false;
				return;
			}
			CompoundUseSiteInfo<TAssemblySymbol>.<MergeAndClear>g__mergeAndClear|39_0<DiagnosticInfo>(ref this._diagnostics, ref other._diagnostics);
			if (other._hasErrors)
			{
				this.RecordPresenceOfAnError();
				other._hasErrors = false;
			}
			if (!this._hasErrors && this.AccumulatesDependencies)
			{
				CompoundUseSiteInfo<TAssemblySymbol>.<MergeAndClear>g__mergeAndClear|39_0<TAssemblySymbol>(ref this._dependencies, ref other._dependencies);
				return;
			}
			other._dependencies = null;
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x0001ABEA File Offset: 0x00018DEA
		public void Add([Nullable(new byte[] { 0, 1 })] UseSiteInfo<TAssemblySymbol> other)
		{
			if (!this.AccumulatesDiagnostics)
			{
				return;
			}
			this.AddDiagnostics(other);
			this.AddDependencies(other);
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x0001AC03 File Offset: 0x00018E03
		[CompilerGenerated]
		internal static void <MergeAndClear>g__mergeAndClear|39_0<T>([Nullable(new byte[] { 2, 0 })] ref HashSet<T> self, [Nullable(new byte[] { 2, 0 })] ref HashSet<T> other)
		{
			if (self == null)
			{
				self = other;
			}
			else if (other != null)
			{
				self.AddAll<T>(other);
			}
			other = null;
		}

		// Token: 0x0400029A RID: 666
		private bool _hasErrors;

		// Token: 0x0400029B RID: 667
		private readonly CompoundUseSiteInfo<TAssemblySymbol>.DiscardLevel _discardLevel;

		// Token: 0x0400029C RID: 668
		[Nullable(new byte[] { 2, 1 })]
		private HashSet<DiagnosticInfo> _diagnostics;

		// Token: 0x0400029D RID: 669
		[Nullable(new byte[] { 2, 1 })]
		private HashSet<TAssemblySymbol> _dependencies;

		// Token: 0x0400029E RID: 670
		[Nullable(2)]
		private readonly TAssemblySymbol _assemblyBeingBuilt;

		// Token: 0x02000539 RID: 1337
		private enum DiscardLevel : byte
		{
			// Token: 0x040014D2 RID: 5330
			None,
			// Token: 0x040014D3 RID: 5331
			Dependencies,
			// Token: 0x040014D4 RID: 5332
			DiagnosticsAndDependencies
		}
	}
}
