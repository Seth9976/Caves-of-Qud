using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000108 RID: 264
	internal abstract class BindingDiagnosticBag
	{
		// Token: 0x0600090A RID: 2314 RVA: 0x00019F63 File Offset: 0x00018163
		[NullableContext(2)]
		protected BindingDiagnosticBag(DiagnosticBag diagnosticBag)
		{
			this.DiagnosticBag = diagnosticBag;
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x00019F72 File Offset: 0x00018172
		internal bool AccumulatesDiagnostics
		{
			get
			{
				return this.DiagnosticBag != null;
			}
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x00019F7D File Offset: 0x0001817D
		internal void AddRange<T>([Nullable(new byte[] { 0, 1 })] ImmutableArray<T> diagnostics) where T : Diagnostic
		{
			DiagnosticBag diagnosticBag = this.DiagnosticBag;
			if (diagnosticBag == null)
			{
				return;
			}
			diagnosticBag.AddRange<T>(diagnostics);
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x00019F90 File Offset: 0x00018190
		[NullableContext(1)]
		internal void AddRange(IEnumerable<Diagnostic> diagnostics)
		{
			DiagnosticBag diagnosticBag = this.DiagnosticBag;
			if (diagnosticBag == null)
			{
				return;
			}
			diagnosticBag.AddRange(diagnostics);
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x00019FA3 File Offset: 0x000181A3
		internal bool HasAnyResolvedErrors()
		{
			DiagnosticBag diagnosticBag = this.DiagnosticBag;
			return diagnosticBag != null && diagnosticBag.HasAnyResolvedErrors();
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x00019FB6 File Offset: 0x000181B6
		internal bool HasAnyErrors()
		{
			DiagnosticBag diagnosticBag = this.DiagnosticBag;
			return diagnosticBag != null && diagnosticBag.HasAnyErrors();
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x00019FC9 File Offset: 0x000181C9
		[NullableContext(1)]
		internal void Add(Diagnostic diag)
		{
			DiagnosticBag diagnosticBag = this.DiagnosticBag;
			if (diagnosticBag == null)
			{
				return;
			}
			diagnosticBag.Add(diag);
		}

		// Token: 0x04000293 RID: 659
		[Nullable(2)]
		public readonly DiagnosticBag DiagnosticBag;
	}
}
