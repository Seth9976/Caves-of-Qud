using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Microsoft.Cci
{
	// Token: 0x02000081 RID: 129
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class DebugSourceDocument
	{
		// Token: 0x0600034F RID: 847 RVA: 0x0000CE21 File Offset: 0x0000B021
		public DebugSourceDocument(string location, Guid language)
		{
			this._location = location;
			this._language = language;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000CE37 File Offset: 0x0000B037
		public DebugSourceDocument(string location, Guid language, Func<DebugSourceInfo> sourceInfo)
			: this(location, language)
		{
			this._sourceInfo = Task.Run<DebugSourceInfo>(sourceInfo);
			this._isComputedChecksum = true;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000CE54 File Offset: 0x0000B054
		[NullableContext(0)]
		public DebugSourceDocument([Nullable(1)] string location, Guid language, ImmutableArray<byte> checksum, Guid algorithm)
			: this(location, language)
		{
			this._sourceInfo = Task.FromResult<DebugSourceInfo>(new DebugSourceInfo(checksum, algorithm, default(ImmutableArray<byte>)));
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0000CE85 File Offset: 0x0000B085
		public Guid DocumentType
		{
			get
			{
				return DebugSourceDocument.s_corSymDocumentTypeText;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0000CE8C File Offset: 0x0000B08C
		public Guid Language
		{
			get
			{
				return this._language;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0000CE94 File Offset: 0x0000B094
		public Guid LanguageVendor
		{
			get
			{
				return DebugSourceDocument.s_corSymLanguageVendorMicrosoft;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0000CE9B File Offset: 0x0000B09B
		public string Location
		{
			get
			{
				return this._location;
			}
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000CEA4 File Offset: 0x0000B0A4
		public DebugSourceInfo GetSourceInfo()
		{
			Task<DebugSourceInfo> sourceInfo = this._sourceInfo;
			if (sourceInfo == null)
			{
				return default(DebugSourceInfo);
			}
			return sourceInfo.Result;
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0000CECA File Offset: 0x0000B0CA
		internal bool IsComputedChecksum
		{
			get
			{
				return this._isComputedChecksum;
			}
		}

		// Token: 0x04000167 RID: 359
		internal static readonly Guid CorSymLanguageTypeCSharp = new Guid("{3f5162f8-07c6-11d3-9053-00c04fa302a1}");

		// Token: 0x04000168 RID: 360
		internal static readonly Guid CorSymLanguageTypeBasic = new Guid("{3a12d0b8-c26c-11d0-b442-00a0244a1dd2}");

		// Token: 0x04000169 RID: 361
		private static readonly Guid s_corSymLanguageVendorMicrosoft = new Guid("{994b45c4-e6e9-11d2-903f-00c04fa302a1}");

		// Token: 0x0400016A RID: 362
		private static readonly Guid s_corSymDocumentTypeText = new Guid("{5a869d0b-6611-11d3-bd2a-0000f80849bd}");

		// Token: 0x0400016B RID: 363
		private readonly string _location;

		// Token: 0x0400016C RID: 364
		private readonly Guid _language;

		// Token: 0x0400016D RID: 365
		private readonly bool _isComputedChecksum;

		// Token: 0x0400016E RID: 366
		[Nullable(2)]
		private readonly Task<DebugSourceInfo> _sourceInfo;
	}
}
