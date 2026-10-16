using System;
using System.Collections.Specialized;
using System.IO;

namespace System.Net.Cache
{
	// Token: 0x02000785 RID: 1925
	internal abstract class RequestCache
	{
		// Token: 0x06003C98 RID: 15512 RVA: 0x000CE60F File Offset: 0x000CC80F
		protected RequestCache(bool isPrivateCache, bool canWrite)
		{
			this._IsPrivateCache = isPrivateCache;
			this._CanWrite = canWrite;
		}

		// Token: 0x17000DDE RID: 3550
		// (get) Token: 0x06003C99 RID: 15513 RVA: 0x000CE625 File Offset: 0x000CC825
		internal bool IsPrivateCache
		{
			get
			{
				return this._IsPrivateCache;
			}
		}

		// Token: 0x17000DDF RID: 3551
		// (get) Token: 0x06003C9A RID: 15514 RVA: 0x000CE62D File Offset: 0x000CC82D
		internal bool CanWrite
		{
			get
			{
				return this._CanWrite;
			}
		}

		// Token: 0x06003C9B RID: 15515
		internal abstract Stream Retrieve(string key, out RequestCacheEntry cacheEntry);

		// Token: 0x06003C9C RID: 15516
		internal abstract Stream Store(string key, long contentLength, DateTime expiresUtc, DateTime lastModifiedUtc, TimeSpan maxStale, StringCollection entryMetadata, StringCollection systemMetadata);

		// Token: 0x06003C9D RID: 15517
		internal abstract void Remove(string key);

		// Token: 0x06003C9E RID: 15518
		internal abstract void Update(string key, DateTime expiresUtc, DateTime lastModifiedUtc, DateTime lastSynchronizedUtc, TimeSpan maxStale, StringCollection entryMetadata, StringCollection systemMetadata);

		// Token: 0x06003C9F RID: 15519
		internal abstract bool TryRetrieve(string key, out RequestCacheEntry cacheEntry, out Stream readStream);

		// Token: 0x06003CA0 RID: 15520
		internal abstract bool TryStore(string key, long contentLength, DateTime expiresUtc, DateTime lastModifiedUtc, TimeSpan maxStale, StringCollection entryMetadata, StringCollection systemMetadata, out Stream writeStream);

		// Token: 0x06003CA1 RID: 15521
		internal abstract bool TryRemove(string key);

		// Token: 0x06003CA2 RID: 15522
		internal abstract bool TryUpdate(string key, DateTime expiresUtc, DateTime lastModifiedUtc, DateTime lastSynchronizedUtc, TimeSpan maxStale, StringCollection entryMetadata, StringCollection systemMetadata);

		// Token: 0x06003CA3 RID: 15523
		internal abstract void UnlockEntry(Stream retrieveStream);

		// Token: 0x040023F4 RID: 9204
		internal static readonly char[] LineSplits = new char[] { '\r', '\n' };

		// Token: 0x040023F5 RID: 9205
		private bool _IsPrivateCache;

		// Token: 0x040023F6 RID: 9206
		private bool _CanWrite;
	}
}
