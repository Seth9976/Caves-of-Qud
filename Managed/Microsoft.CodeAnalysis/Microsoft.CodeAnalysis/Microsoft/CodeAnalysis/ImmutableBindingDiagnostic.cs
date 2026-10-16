using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Symbols;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200010A RID: 266
	[NullableContext(1)]
	[Nullable(0)]
	internal readonly struct ImmutableBindingDiagnostic<TAssemblySymbol> where TAssemblySymbol : class, IAssemblySymbolInternal
	{
		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x0001A447 File Offset: 0x00018647
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<Diagnostic> Diagnostics
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._diagnostics.NullToEmpty<Diagnostic>();
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x0001A454 File Offset: 0x00018654
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<TAssemblySymbol> Dependencies
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._dependencies.NullToEmpty<TAssemblySymbol>();
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x0001A464 File Offset: 0x00018664
		[Nullable(new byte[] { 0, 1 })]
		public static ImmutableBindingDiagnostic<TAssemblySymbol> Empty
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return new ImmutableBindingDiagnostic<TAssemblySymbol>(default(ImmutableArray<Diagnostic>), default(ImmutableArray<TAssemblySymbol>));
			}
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0001A488 File Offset: 0x00018688
		public ImmutableBindingDiagnostic([Nullable(new byte[] { 0, 1 })] ImmutableArray<Diagnostic> diagnostics, [Nullable(new byte[] { 0, 1 })] ImmutableArray<TAssemblySymbol> dependencies)
		{
			this._diagnostics = diagnostics.NullToEmpty<Diagnostic>();
			this._dependencies = dependencies.NullToEmpty<TAssemblySymbol>();
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0001A4A2 File Offset: 0x000186A2
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableBindingDiagnostic<TAssemblySymbol> NullToEmpty()
		{
			return new ImmutableBindingDiagnostic<TAssemblySymbol>(this.Diagnostics, this.Dependencies);
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0001A4B5 File Offset: 0x000186B5
		public static bool operator ==([Nullable(new byte[] { 0, 1 })] ImmutableBindingDiagnostic<TAssemblySymbol> first, [Nullable(new byte[] { 0, 1 })] ImmutableBindingDiagnostic<TAssemblySymbol> second)
		{
			return first.Diagnostics == second.Diagnostics && first.Dependencies == second.Dependencies;
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x0001A4E1 File Offset: 0x000186E1
		public static bool operator !=([Nullable(new byte[] { 0, 1 })] ImmutableBindingDiagnostic<TAssemblySymbol> first, [Nullable(new byte[] { 0, 1 })] ImmutableBindingDiagnostic<TAssemblySymbol> second)
		{
			return !(first == second);
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0001A4F0 File Offset: 0x000186F0
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			ImmutableBindingDiagnostic<TAssemblySymbol>? immutableBindingDiagnostic;
			return obj is ImmutableBindingDiagnostic<TAssemblySymbol>? && immutableBindingDiagnostic.GetValueOrDefault().Equals(this);
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0001A529 File Offset: 0x00018729
		public bool Equals([Nullable(new byte[] { 0, 1 })] ImmutableBindingDiagnostic<TAssemblySymbol> other)
		{
			return this == other;
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x0001A538 File Offset: 0x00018738
		public override int GetHashCode()
		{
			return this.Diagnostics.GetHashCode();
		}

		// Token: 0x04000295 RID: 661
		[Nullable(new byte[] { 0, 1 })]
		private readonly ImmutableArray<Diagnostic> _diagnostics;

		// Token: 0x04000296 RID: 662
		[Nullable(new byte[] { 0, 1 })]
		private readonly ImmutableArray<TAssemblySymbol> _dependencies;
	}
}
