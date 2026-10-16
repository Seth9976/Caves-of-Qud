using System;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004CC RID: 1228
	internal sealed class PropertyEarlyWellKnownAttributeData : CommonPropertyEarlyWellKnownAttributeData
	{
		// Token: 0x17001A5C RID: 6748
		// (get) Token: 0x06006887 RID: 26759 RVA: 0x0018836B File Offset: 0x0018656B
		// (set) Token: 0x06006888 RID: 26760 RVA: 0x00188373 File Offset: 0x00186573
		public string IndexerName
		{
			get
			{
				return this._indexerName;
			}
			set
			{
				if (this._indexerName == null)
				{
					this._indexerName = value;
				}
			}
		}

		// Token: 0x040019ED RID: 6637
		private string _indexerName;
	}
}
