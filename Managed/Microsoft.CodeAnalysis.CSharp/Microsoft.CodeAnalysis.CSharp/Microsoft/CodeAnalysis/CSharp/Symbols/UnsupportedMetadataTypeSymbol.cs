using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020005AD RID: 1453
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class UnsupportedMetadataTypeSymbol : ErrorTypeSymbol
	{
		// Token: 0x06007A09 RID: 31241 RVA: 0x001BEA7C File Offset: 0x001BCC7C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal UnsupportedMetadataTypeSymbol(BadImageFormatException mrEx = null)
			: base(null)
		{
			this._mrEx = mrEx;
		}

		// Token: 0x06007A0A RID: 31242 RVA: 0x001BEA8C File Offset: 0x001BCC8C
		protected override NamedTypeSymbol WithTupleDataCore(NamedTypeSymbol.TupleExtraData newData)
		{
			return this;
		}

		// Token: 0x170022BB RID: 8891
		// (get) Token: 0x06007A0B RID: 31243 RVA: 0x001BEA8F File Offset: 0x001BCC8F
		internal override DiagnosticInfo ErrorInfo
		{
			get
			{
				return new CSDiagnosticInfo(ErrorCode.ERR_BogusType, new object[] { string.Empty });
			}
		}

		// Token: 0x170022BC RID: 8892
		// (get) Token: 0x06007A0C RID: 31244 RVA: 0x001BEAA9 File Offset: 0x001BCCA9
		internal override bool MangleName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04001D29 RID: 7465
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private readonly BadImageFormatException _mrEx;
	}
}
