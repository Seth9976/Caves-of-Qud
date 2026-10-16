using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000182 RID: 386
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SuppressionDescriptor : IEquatable<SuppressionDescriptor>
	{
		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06001001 RID: 4097 RVA: 0x0002EAD0 File Offset: 0x0002CCD0
		public string Id { get; }

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06001002 RID: 4098 RVA: 0x0002EAD8 File Offset: 0x0002CCD8
		public string SuppressedDiagnosticId { get; }

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06001003 RID: 4099 RVA: 0x0002EAE0 File Offset: 0x0002CCE0
		public LocalizableString Justification { get; }

		// Token: 0x06001004 RID: 4100 RVA: 0x0002EAE8 File Offset: 0x0002CCE8
		public SuppressionDescriptor(string id, string suppressedDiagnosticId, string justification)
			: this(id, suppressedDiagnosticId, justification)
		{
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x0002EAF8 File Offset: 0x0002CCF8
		public SuppressionDescriptor(string id, string suppressedDiagnosticId, LocalizableString justification)
		{
			if (string.IsNullOrWhiteSpace(id))
			{
				throw new ArgumentException(CodeAnalysisResources.SuppressionIdCantBeNullOrWhitespace, "id");
			}
			if (string.IsNullOrWhiteSpace(suppressedDiagnosticId))
			{
				throw new ArgumentException(CodeAnalysisResources.DiagnosticIdCantBeNullOrWhitespace, "suppressedDiagnosticId");
			}
			this.Id = id;
			this.SuppressedDiagnosticId = suppressedDiagnosticId;
			if (justification == null)
			{
				throw new ArgumentNullException("justification");
			}
			this.Justification = justification;
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x0002EB60 File Offset: 0x0002CD60
		[NullableContext(2)]
		public bool Equals(SuppressionDescriptor other)
		{
			return this == other || (other != null && this.Id == other.Id && this.SuppressedDiagnosticId == other.SuppressedDiagnosticId && this.Justification.Equals(other.Justification));
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x0002EBAF File Offset: 0x0002CDAF
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SuppressionDescriptor);
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x0002EBBD File Offset: 0x0002CDBD
		public override int GetHashCode()
		{
			return Hash.Combine(this.Id.GetHashCode(), Hash.Combine(this.SuppressedDiagnosticId.GetHashCode(), this.Justification.GetHashCode()));
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x0002EBEC File Offset: 0x0002CDEC
		internal bool IsDisabled(CompilationOptions compilationOptions)
		{
			if (compilationOptions == null)
			{
				throw new ArgumentNullException("compilationOptions");
			}
			ReportDiagnostic reportDiagnostic;
			return compilationOptions.SpecificDiagnosticOptions.TryGetValue(this.Id, out reportDiagnostic) && reportDiagnostic == ReportDiagnostic.Suppress;
		}
	}
}
