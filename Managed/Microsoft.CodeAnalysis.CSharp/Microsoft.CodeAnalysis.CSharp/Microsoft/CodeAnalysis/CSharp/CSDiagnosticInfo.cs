using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000133 RID: 307
	internal sealed class CSDiagnosticInfo : DiagnosticInfoWithSymbols
	{
		// Token: 0x060018E6 RID: 6374 RVA: 0x0008541D File Offset: 0x0008361D
		internal CSDiagnosticInfo(ErrorCode code)
			: this(code, Array.Empty<object>(), ImmutableArray<Symbol>.Empty, ImmutableArray<Location>.Empty)
		{
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x00085435 File Offset: 0x00083635
		internal CSDiagnosticInfo(ErrorCode code, params object[] args)
			: this(code, args, ImmutableArray<Symbol>.Empty, ImmutableArray<Location>.Empty)
		{
		}

		// Token: 0x060018E8 RID: 6376 RVA: 0x00085449 File Offset: 0x00083649
		internal CSDiagnosticInfo(ErrorCode code, ImmutableArray<Symbol> symbols, object[] args)
			: this(code, args, symbols, ImmutableArray<Location>.Empty)
		{
		}

		// Token: 0x060018E9 RID: 6377 RVA: 0x0008545C File Offset: 0x0008365C
		internal CSDiagnosticInfo(ErrorCode code, object[] args, ImmutableArray<Symbol> symbols, ImmutableArray<Location> additionalLocations)
			: base(code, args, symbols)
		{
			IReadOnlyList<Location> readOnlyList2;
			if (!additionalLocations.IsDefaultOrEmpty)
			{
				IReadOnlyList<Location> readOnlyList = additionalLocations;
				readOnlyList2 = readOnlyList;
			}
			else
			{
				readOnlyList2 = SpecializedCollections.EmptyReadOnlyList<Location>();
			}
			this._additionalLocations = readOnlyList2;
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x060018EA RID: 6378 RVA: 0x00085491 File Offset: 0x00083691
		public override IReadOnlyList<Location> AdditionalLocations
		{
			get
			{
				return this._additionalLocations;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x060018EB RID: 6379 RVA: 0x00085499 File Offset: 0x00083699
		internal new ErrorCode Code
		{
			get
			{
				return (ErrorCode)base.Code;
			}
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x000854A1 File Offset: 0x000836A1
		internal static bool IsEmpty(DiagnosticInfo info)
		{
			return info == CSDiagnosticInfo.EmptyErrorInfo;
		}

		// Token: 0x040006E2 RID: 1762
		public static readonly DiagnosticInfo EmptyErrorInfo = new CSDiagnosticInfo((ErrorCode)0);

		// Token: 0x040006E3 RID: 1763
		public static readonly DiagnosticInfo VoidDiagnosticInfo = new CSDiagnosticInfo(ErrorCode.Void);

		// Token: 0x040006E4 RID: 1764
		private readonly IReadOnlyList<Location> _additionalLocations;
	}
}
