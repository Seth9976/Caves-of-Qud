using System;
using System.Collections.Immutable;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000225 RID: 549
	internal class CommonTypeEarlyWellKnownAttributeData : EarlyWellKnownAttributeData
	{
		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x0600157A RID: 5498 RVA: 0x00044275 File Offset: 0x00042475
		// (set) Token: 0x0600157B RID: 5499 RVA: 0x0004427D File Offset: 0x0004247D
		public AttributeUsageInfo AttributeUsageInfo
		{
			get
			{
				return this._attributeUsageInfo;
			}
			set
			{
				this._attributeUsageInfo = value;
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x0600157C RID: 5500 RVA: 0x00044286 File Offset: 0x00042486
		// (set) Token: 0x0600157D RID: 5501 RVA: 0x0004428E File Offset: 0x0004248E
		public bool HasComImportAttribute
		{
			get
			{
				return this._hasComImportAttribute;
			}
			set
			{
				this._hasComImportAttribute = value;
			}
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x00044297 File Offset: 0x00042497
		public void AddConditionalSymbol(string name)
		{
			this._lazyConditionalSymbols = this._lazyConditionalSymbols.Add(name);
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x0600157F RID: 5503 RVA: 0x000442AB File Offset: 0x000424AB
		public ImmutableArray<string> ConditionalSymbols
		{
			get
			{
				return this._lazyConditionalSymbols;
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06001580 RID: 5504 RVA: 0x000442B3 File Offset: 0x000424B3
		// (set) Token: 0x06001581 RID: 5505 RVA: 0x000442CA File Offset: 0x000424CA
		public ObsoleteAttributeData ObsoleteAttributeData
		{
			get
			{
				if (!this._obsoleteAttributeData.IsUninitialized)
				{
					return this._obsoleteAttributeData;
				}
				return null;
			}
			set
			{
				this._obsoleteAttributeData = value;
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001582 RID: 5506 RVA: 0x000442D3 File Offset: 0x000424D3
		// (set) Token: 0x06001583 RID: 5507 RVA: 0x000442DB File Offset: 0x000424DB
		public bool HasCodeAnalysisEmbeddedAttribute
		{
			get
			{
				return this._hasCodeAnalysisEmbeddedAttribute;
			}
			set
			{
				this._hasCodeAnalysisEmbeddedAttribute = value;
			}
		}

		// Token: 0x040008D9 RID: 2265
		private AttributeUsageInfo _attributeUsageInfo = AttributeUsageInfo.Null;

		// Token: 0x040008DA RID: 2266
		private bool _hasComImportAttribute;

		// Token: 0x040008DB RID: 2267
		private ImmutableArray<string> _lazyConditionalSymbols = ImmutableArray<string>.Empty;

		// Token: 0x040008DC RID: 2268
		private ObsoleteAttributeData _obsoleteAttributeData = ObsoleteAttributeData.Uninitialized;

		// Token: 0x040008DD RID: 2269
		private bool _hasCodeAnalysisEmbeddedAttribute;
	}
}
