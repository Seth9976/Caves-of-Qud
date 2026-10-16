using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000503 RID: 1283
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class NoPiaAmbiguousCanonicalTypeSymbol : ErrorTypeSymbol
	{
		// Token: 0x06006C6B RID: 27755 RVA: 0x00190796 File Offset: 0x0018E996
		public NoPiaAmbiguousCanonicalTypeSymbol(AssemblySymbol embeddingAssembly, NamedTypeSymbol firstCandidate, NamedTypeSymbol secondCandidate, [global::System.Runtime.CompilerServices.Nullable(2)] NamedTypeSymbol.TupleExtraData tupleData = null)
			: base(tupleData)
		{
			this._embeddingAssembly = embeddingAssembly;
			this._firstCandidate = firstCandidate;
			this._secondCandidate = secondCandidate;
		}

		// Token: 0x06006C6C RID: 27756 RVA: 0x001907B5 File Offset: 0x0018E9B5
		protected override NamedTypeSymbol WithTupleDataCore(NamedTypeSymbol.TupleExtraData newData)
		{
			return new NoPiaAmbiguousCanonicalTypeSymbol(this._embeddingAssembly, this._firstCandidate, this._secondCandidate, newData);
		}

		// Token: 0x17001C07 RID: 7175
		// (get) Token: 0x06006C6D RID: 27757 RVA: 0x001907CF File Offset: 0x0018E9CF
		internal override bool MangleName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001C08 RID: 7176
		// (get) Token: 0x06006C6E RID: 27758 RVA: 0x001907D2 File Offset: 0x0018E9D2
		public AssemblySymbol EmbeddingAssembly
		{
			get
			{
				return this._embeddingAssembly;
			}
		}

		// Token: 0x17001C09 RID: 7177
		// (get) Token: 0x06006C6F RID: 27759 RVA: 0x001907DA File Offset: 0x0018E9DA
		public NamedTypeSymbol FirstCandidate
		{
			get
			{
				return this._firstCandidate;
			}
		}

		// Token: 0x17001C0A RID: 7178
		// (get) Token: 0x06006C70 RID: 27760 RVA: 0x001907E2 File Offset: 0x0018E9E2
		public NamedTypeSymbol SecondCandidate
		{
			get
			{
				return this._secondCandidate;
			}
		}

		// Token: 0x17001C0B RID: 7179
		// (get) Token: 0x06006C71 RID: 27761 RVA: 0x001907EA File Offset: 0x0018E9EA
		internal override DiagnosticInfo ErrorInfo
		{
			get
			{
				return new CSDiagnosticInfo(ErrorCode.ERR_NoCanonicalView, new object[] { this._firstCandidate });
			}
		}

		// Token: 0x06006C72 RID: 27762 RVA: 0x00190805 File Offset: 0x0018EA05
		public override int GetHashCode()
		{
			return RuntimeHelpers.GetHashCode(this);
		}

		// Token: 0x06006C73 RID: 27763 RVA: 0x0019080D File Offset: 0x0018EA0D
		internal override bool Equals(TypeSymbol t2, TypeCompareKind comparison)
		{
			return this == t2;
		}

		// Token: 0x04001ACE RID: 6862
		private readonly AssemblySymbol _embeddingAssembly;

		// Token: 0x04001ACF RID: 6863
		private readonly NamedTypeSymbol _firstCandidate;

		// Token: 0x04001AD0 RID: 6864
		private readonly NamedTypeSymbol _secondCandidate;
	}
}
