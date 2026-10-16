using System;
using System.Collections.Generic;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x0200001F RID: 31
	[Serializable]
	internal abstract class OpenTypeLayoutLookup
	{
		// Token: 0x0600014B RID: 331
		public abstract void InitializeLookupDictionary();

		// Token: 0x0600014C RID: 332 RVA: 0x000059F9 File Offset: 0x00003BF9
		public virtual void UpdateRecords(int lookupIndex, uint glyphIndex)
		{
		}

		// Token: 0x0600014D RID: 333 RVA: 0x000059F9 File Offset: 0x00003BF9
		public virtual void UpdateRecords(int lookupIndex, uint glyphIndex, float emScale)
		{
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000059F9 File Offset: 0x00003BF9
		public virtual void UpdateRecords(int lookupIndex, List<uint> glyphIndexes)
		{
		}

		// Token: 0x0600014F RID: 335 RVA: 0x000059F9 File Offset: 0x00003BF9
		public virtual void UpdateRecords(int lookupIndex, List<uint> glyphIndexes, float emScale)
		{
		}

		// Token: 0x06000150 RID: 336
		public abstract void ClearRecords();

		// Token: 0x040000CB RID: 203
		public uint lookupType;

		// Token: 0x040000CC RID: 204
		public uint lookupFlag;

		// Token: 0x040000CD RID: 205
		public uint markFilteringSet;
	}
}
