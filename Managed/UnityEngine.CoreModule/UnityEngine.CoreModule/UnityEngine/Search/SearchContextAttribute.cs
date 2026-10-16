using System;

namespace UnityEngine.Search
{
	// Token: 0x0200035D RID: 861
	[AttributeUsage(AttributeTargets.Field)]
	public class SearchContextAttribute : PropertyAttribute
	{
		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06002896 RID: 10390 RVA: 0x0004E722 File Offset: 0x0004C922
		// (set) Token: 0x06002897 RID: 10391 RVA: 0x0004E72A File Offset: 0x0004C92A
		public string query { get; private set; }

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06002898 RID: 10392 RVA: 0x0004E733 File Offset: 0x0004C933
		// (set) Token: 0x06002899 RID: 10393 RVA: 0x0004E73B File Offset: 0x0004C93B
		public string[] providerIds { get; private set; }

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x0600289A RID: 10394 RVA: 0x0004E744 File Offset: 0x0004C944
		// (set) Token: 0x0600289B RID: 10395 RVA: 0x0004E74C File Offset: 0x0004C94C
		public Type[] instantiableProviders { get; private set; }

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x0600289C RID: 10396 RVA: 0x0004E755 File Offset: 0x0004C955
		// (set) Token: 0x0600289D RID: 10397 RVA: 0x0004E75D File Offset: 0x0004C95D
		public SearchViewFlags flags { get; private set; }

		// Token: 0x0600289E RID: 10398 RVA: 0x0004E766 File Offset: 0x0004C966
		public SearchContextAttribute(string query)
			: this(query, null, SearchViewFlags.None)
		{
		}

		// Token: 0x0600289F RID: 10399 RVA: 0x0004E773 File Offset: 0x0004C973
		public SearchContextAttribute(string query, SearchViewFlags flags)
			: this(query, null, flags)
		{
		}

		// Token: 0x060028A0 RID: 10400 RVA: 0x0004E780 File Offset: 0x0004C980
		public SearchContextAttribute(string query, string providerIdsCommaSeparated)
			: this(query, providerIdsCommaSeparated, SearchViewFlags.None)
		{
		}

		// Token: 0x060028A1 RID: 10401 RVA: 0x0004E78D File Offset: 0x0004C98D
		public SearchContextAttribute(string query, string providerIdsCommaSeparated, SearchViewFlags flags)
			: this(query, flags, providerIdsCommaSeparated, null)
		{
		}

		// Token: 0x060028A2 RID: 10402 RVA: 0x0004E79B File Offset: 0x0004C99B
		public SearchContextAttribute(string query, params Type[] instantiableProviders)
			: this(query, SearchViewFlags.None, null, instantiableProviders)
		{
		}

		// Token: 0x060028A3 RID: 10403 RVA: 0x0004E7A9 File Offset: 0x0004C9A9
		public SearchContextAttribute(string query, SearchViewFlags flags, params Type[] instantiableProviders)
			: this(query, flags, null, instantiableProviders)
		{
		}

		// Token: 0x060028A4 RID: 10404 RVA: 0x0004E7B8 File Offset: 0x0004C9B8
		public SearchContextAttribute(string query, SearchViewFlags flags, string providerIdsCommaSeparated, params Type[] instantiableProviders)
		{
			this.query = query;
			this.providerIds = ((providerIdsCommaSeparated != null) ? providerIdsCommaSeparated.Split(new char[] { ',', ';' }) : null) ?? new string[0];
			this.instantiableProviders = instantiableProviders ?? new Type[0];
			this.flags = flags | SearchViewFlags.ObjectPicker;
		}
	}
}
