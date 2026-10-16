using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000307 RID: 775
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
	public sealed class AnalyzerImageReference : AnalyzerReference
	{
		// Token: 0x06002214 RID: 8724 RVA: 0x0006C6CC File Offset: 0x0006A8CC
		[NullableContext(2)]
		public AnalyzerImageReference([Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, string fullPath = null, string display = null)
		{
			if (analyzers.Any<DiagnosticAnalyzer>((DiagnosticAnalyzer a) => a == null))
			{
				throw new ArgumentException("Cannot have null-valued analyzer", "analyzers");
			}
			this._analyzers = analyzers;
			this._fullPath = fullPath;
			this._display = display;
			this._id = Guid.NewGuid().ToString();
		}

		// Token: 0x06002215 RID: 8725 RVA: 0x0006C744 File Offset: 0x0006A944
		[return: Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<DiagnosticAnalyzer> GetAnalyzersForAllLanguages()
		{
			return this._analyzers;
		}

		// Token: 0x06002216 RID: 8726 RVA: 0x0006C74C File Offset: 0x0006A94C
		[return: Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<DiagnosticAnalyzer> GetAnalyzers(string language)
		{
			return this._analyzers;
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06002217 RID: 8727 RVA: 0x0006C754 File Offset: 0x0006A954
		[Nullable(2)]
		public override string FullPath
		{
			[NullableContext(2)]
			get
			{
				return this._fullPath;
			}
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06002218 RID: 8728 RVA: 0x0006C75C File Offset: 0x0006A95C
		public override string Display
		{
			get
			{
				string text;
				if ((text = this._display) == null)
				{
					text = this._fullPath ?? CodeAnalysisResources.InMemoryAssembly;
				}
				return text;
			}
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06002219 RID: 8729 RVA: 0x0006C777 File Offset: 0x0006A977
		public override object Id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x0600221A RID: 8730 RVA: 0x0006C780 File Offset: 0x0006A980
		private string GetDebuggerDisplay()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Assembly");
			if (this._fullPath != null)
			{
				stringBuilder.Append(" Path='");
				stringBuilder.Append(this._fullPath);
				stringBuilder.Append("'");
			}
			if (this._display != null)
			{
				stringBuilder.Append(" Display='");
				stringBuilder.Append(this._display);
				stringBuilder.Append("'");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000EAC RID: 3756
		[Nullable(new byte[] { 0, 1 })]
		private readonly ImmutableArray<DiagnosticAnalyzer> _analyzers;

		// Token: 0x04000EAD RID: 3757
		[Nullable(2)]
		private readonly string _fullPath;

		// Token: 0x04000EAE RID: 3758
		[Nullable(2)]
		private readonly string _display;

		// Token: 0x04000EAF RID: 3759
		private readonly string _id;
	}
}
