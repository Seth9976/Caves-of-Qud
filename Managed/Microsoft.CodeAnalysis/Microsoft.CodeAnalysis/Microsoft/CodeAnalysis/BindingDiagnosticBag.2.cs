using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Symbols;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000109 RID: 265
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class BindingDiagnosticBag<TAssemblySymbol> : BindingDiagnosticBag where TAssemblySymbol : class, IAssemblySymbolInternal
	{
		// Token: 0x06000911 RID: 2321 RVA: 0x00019FDC File Offset: 0x000181DC
		[NullableContext(2)]
		protected BindingDiagnosticBag(DiagnosticBag diagnosticBag, [Nullable(new byte[] { 2, 1 })] ICollection<TAssemblySymbol> dependenciesBag)
			: base(diagnosticBag)
		{
			this.DependenciesBag = dependenciesBag;
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x00019FEC File Offset: 0x000181EC
		protected BindingDiagnosticBag(bool usePool)
			: this(usePool ? DiagnosticBag.GetInstance() : new DiagnosticBag(), usePool ? PooledHashSet<TAssemblySymbol>.GetInstance() : new HashSet<TAssemblySymbol>())
		{
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x0001A012 File Offset: 0x00018212
		internal bool AccumulatesDependencies
		{
			get
			{
				return this.DependenciesBag != null;
			}
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0001A01D File Offset: 0x0001821D
		internal void Free()
		{
			DiagnosticBag diagnosticBag = this.DiagnosticBag;
			if (diagnosticBag != null)
			{
				diagnosticBag.Free();
			}
			PooledHashSet<TAssemblySymbol> pooledHashSet = (PooledHashSet<TAssemblySymbol>)this.DependenciesBag;
			if (pooledHashSet == null)
			{
				return;
			}
			pooledHashSet.Free();
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0001A048 File Offset: 0x00018248
		[return: Nullable(new byte[] { 0, 1 })]
		internal ImmutableBindingDiagnostic<TAssemblySymbol> ToReadOnly()
		{
			DiagnosticBag diagnosticBag = this.DiagnosticBag;
			ImmutableArray<Diagnostic> immutableArray = ((diagnosticBag != null) ? diagnosticBag.ToReadOnly() : default(ImmutableArray<Diagnostic>));
			ICollection<TAssemblySymbol> dependenciesBag = this.DependenciesBag;
			return new ImmutableBindingDiagnostic<TAssemblySymbol>(immutableArray, (dependenciesBag != null) ? dependenciesBag.ToImmutableArray<TAssemblySymbol>() : default(ImmutableArray<TAssemblySymbol>));
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x0001A08E File Offset: 0x0001828E
		[return: Nullable(new byte[] { 0, 1 })]
		internal ImmutableBindingDiagnostic<TAssemblySymbol> ToReadOnlyAndFree()
		{
			ImmutableBindingDiagnostic<TAssemblySymbol> immutableBindingDiagnostic = this.ToReadOnly();
			this.Free();
			return immutableBindingDiagnostic;
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x0001A09C File Offset: 0x0001829C
		internal void AddRangeAndFree(BindingDiagnosticBag<TAssemblySymbol> other)
		{
			this.AddRange(other, false);
			other.Free();
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x0001A0AC File Offset: 0x000182AC
		internal void Clear()
		{
			DiagnosticBag diagnosticBag = this.DiagnosticBag;
			if (diagnosticBag != null)
			{
				diagnosticBag.Clear();
			}
			ICollection<TAssemblySymbol> dependenciesBag = this.DependenciesBag;
			if (dependenciesBag == null)
			{
				return;
			}
			dependenciesBag.Clear();
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x0001A0CF File Offset: 0x000182CF
		internal void AddRange([Nullable(new byte[] { 0, 1 })] ImmutableBindingDiagnostic<TAssemblySymbol> other, bool allowMismatchInDependencyAccumulation = false)
		{
			base.AddRange<Diagnostic>(other.Diagnostics);
			this.AddDependencies(other.Dependencies);
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x0001A0EB File Offset: 0x000182EB
		internal void AddRange([Nullable(new byte[] { 2, 1 })] BindingDiagnosticBag<TAssemblySymbol> other, bool allowMismatchInDependencyAccumulation = false)
		{
			if (other != null)
			{
				this.AddRange(other.DiagnosticBag);
				this.AddDependencies(other.DependenciesBag);
			}
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0001A108 File Offset: 0x00018308
		[NullableContext(2)]
		internal void AddRange(DiagnosticBag bag)
		{
			if (bag != null)
			{
				DiagnosticBag diagnosticBag = this.DiagnosticBag;
				if (diagnosticBag == null)
				{
					return;
				}
				diagnosticBag.AddRange(bag);
			}
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0001A11E File Offset: 0x0001831E
		[NullableContext(2)]
		internal void AddDependency(TAssemblySymbol dependency)
		{
			if (dependency != null && this.DependenciesBag != null)
			{
				this.DependenciesBag.Add(dependency);
			}
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x0001A13C File Offset: 0x0001833C
		internal void AddDependencies([Nullable(new byte[] { 2, 1 })] ICollection<TAssemblySymbol> dependencies)
		{
			if (!dependencies.IsNullOrEmpty<TAssemblySymbol>() && this.DependenciesBag != null)
			{
				foreach (TAssemblySymbol tassemblySymbol in dependencies)
				{
					this.DependenciesBag.Add(tassemblySymbol);
				}
			}
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0001A19C File Offset: 0x0001839C
		internal void AddDependencies([Nullable(new byte[] { 2, 1 })] IReadOnlyCollection<TAssemblySymbol> dependencies)
		{
			if (!dependencies.IsNullOrEmpty<TAssemblySymbol>() && this.DependenciesBag != null)
			{
				foreach (TAssemblySymbol tassemblySymbol in dependencies)
				{
					this.DependenciesBag.Add(tassemblySymbol);
				}
			}
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0001A1FC File Offset: 0x000183FC
		internal void AddDependencies([Nullable(new byte[] { 2, 1 })] ImmutableHashSet<TAssemblySymbol> dependencies)
		{
			if (!dependencies.IsNullOrEmpty<TAssemblySymbol>() && this.DependenciesBag != null)
			{
				foreach (TAssemblySymbol tassemblySymbol in dependencies)
				{
					this.DependenciesBag.Add(tassemblySymbol);
				}
			}
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0001A260 File Offset: 0x00018460
		internal void AddDependencies([Nullable(new byte[] { 0, 1 })] ImmutableArray<TAssemblySymbol> dependencies)
		{
			if (!dependencies.IsDefaultOrEmpty && this.DependenciesBag != null)
			{
				foreach (TAssemblySymbol tassemblySymbol in dependencies)
				{
					this.DependenciesBag.Add(tassemblySymbol);
				}
			}
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0001A2A5 File Offset: 0x000184A5
		internal void AddDependencies(BindingDiagnosticBag<TAssemblySymbol> dependencies, bool allowMismatchInDependencyAccumulation = false)
		{
			this.AddDependencies(dependencies.DependenciesBag);
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0001A2B3 File Offset: 0x000184B3
		internal void AddDependencies([Nullable(new byte[] { 0, 1 })] UseSiteInfo<TAssemblySymbol> useSiteInfo)
		{
			if (this.DependenciesBag != null)
			{
				this.AddDependency(useSiteInfo.PrimaryDependency);
				this.AddDependencies(useSiteInfo.SecondaryDependencies);
			}
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0001A2D5 File Offset: 0x000184D5
		internal void AddDependencies([Nullable(new byte[] { 0, 1 })] CompoundUseSiteInfo<TAssemblySymbol> useSiteInfo)
		{
			if (this.DependenciesBag != null)
			{
				this.AddDependencies(useSiteInfo.Dependencies);
			}
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0001A2EC File Offset: 0x000184EC
		internal bool Add(SyntaxNode node, [Nullable(new byte[] { 0, 1 })] CompoundUseSiteInfo<TAssemblySymbol> useSiteInfo)
		{
			return this.Add(node.Location, useSiteInfo);
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0001A2FB File Offset: 0x000184FB
		internal bool AddDiagnostics(SyntaxNode node, [Nullable(new byte[] { 0, 1 })] CompoundUseSiteInfo<TAssemblySymbol> useSiteInfo)
		{
			return this.AddDiagnostics(node.Location, useSiteInfo);
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0001A30C File Offset: 0x0001850C
		internal bool AddDiagnostics(Location location, [Nullable(new byte[] { 0, 1 })] CompoundUseSiteInfo<TAssemblySymbol> useSiteInfo)
		{
			DiagnosticBag diagnosticBag = this.DiagnosticBag;
			if (diagnosticBag != null)
			{
				if (!useSiteInfo.Diagnostics.IsNullOrEmpty<DiagnosticInfo>())
				{
					bool flag = false;
					foreach (DiagnosticInfo diagnosticInfo in useSiteInfo.Diagnostics)
					{
						if (this.ReportUseSiteDiagnostic(diagnosticInfo, diagnosticBag, location))
						{
							flag = true;
						}
					}
					if (flag)
					{
						return true;
					}
				}
			}
			else if (useSiteInfo.AccumulatesDiagnostics && !useSiteInfo.Diagnostics.IsNullOrEmpty<DiagnosticInfo>())
			{
				using (IEnumerator<DiagnosticInfo> enumerator = useSiteInfo.Diagnostics.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.Severity == DiagnosticSeverity.Error)
						{
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0001A3E0 File Offset: 0x000185E0
		internal bool Add(Location location, [Nullable(new byte[] { 0, 1 })] CompoundUseSiteInfo<TAssemblySymbol> useSiteInfo)
		{
			if (this.AddDiagnostics(location, useSiteInfo))
			{
				return true;
			}
			this.AddDependencies(useSiteInfo);
			return false;
		}

		// Token: 0x06000928 RID: 2344
		protected abstract bool ReportUseSiteDiagnostic(DiagnosticInfo diagnosticInfo, DiagnosticBag diagnosticBag, Location location);

		// Token: 0x06000929 RID: 2345 RVA: 0x0001A3F6 File Offset: 0x000185F6
		internal bool Add([Nullable(new byte[] { 0, 1 })] UseSiteInfo<TAssemblySymbol> useSiteInfo, SyntaxNode node)
		{
			return this.Add(useSiteInfo, node.Location);
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x0001A405 File Offset: 0x00018605
		internal bool Add([Nullable(new byte[] { 0, 1 })] UseSiteInfo<TAssemblySymbol> info, Location location)
		{
			if (this.ReportUseSiteDiagnostic(info.DiagnosticInfo, location))
			{
				return true;
			}
			this.AddDependencies(info);
			return false;
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x0001A420 File Offset: 0x00018620
		internal bool ReportUseSiteDiagnostic([Nullable(2)] DiagnosticInfo info, Location location)
		{
			if (info == null)
			{
				return false;
			}
			if (this.DiagnosticBag != null)
			{
				return this.ReportUseSiteDiagnostic(info, this.DiagnosticBag, location);
			}
			return info.Severity == DiagnosticSeverity.Error;
		}

		// Token: 0x04000294 RID: 660
		[Nullable(new byte[] { 2, 1 })]
		public readonly ICollection<TAssemblySymbol> DependenciesBag;
	}
}
