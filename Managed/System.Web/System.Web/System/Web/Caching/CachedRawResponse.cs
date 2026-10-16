using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;

namespace System.Web.Caching
{
	// Token: 0x02000684 RID: 1668
	internal sealed class CachedRawResponse
	{
		// Token: 0x17001606 RID: 5638
		// (get) Token: 0x06004725 RID: 18213 RVA: 0x000C614A File Offset: 0x000C434A
		private IList Data
		{
			get
			{
				if (this.data == null)
				{
					this.data = new List<CachedRawResponse.DataItem>();
				}
				return this.data;
			}
		}

		// Token: 0x06004726 RID: 18214 RVA: 0x000C6165 File Offset: 0x000C4365
		public CachedRawResponse(HttpCachePolicy policy)
		{
			this.policy = policy;
		}

		// Token: 0x17001607 RID: 5639
		// (get) Token: 0x06004727 RID: 18215 RVA: 0x000C6174 File Offset: 0x000C4374
		// (set) Token: 0x06004728 RID: 18216 RVA: 0x000C617C File Offset: 0x000C437C
		public HttpCachePolicy Policy
		{
			get
			{
				return this.policy;
			}
			set
			{
				this.policy = value;
			}
		}

		// Token: 0x17001608 RID: 5640
		// (get) Token: 0x06004729 RID: 18217 RVA: 0x000C6185 File Offset: 0x000C4385
		// (set) Token: 0x0600472A RID: 18218 RVA: 0x000C618D File Offset: 0x000C438D
		public CachedVaryBy VaryBy
		{
			get
			{
				return this.varyby;
			}
			set
			{
				this.varyby = value;
			}
		}

		// Token: 0x17001609 RID: 5641
		// (get) Token: 0x0600472B RID: 18219 RVA: 0x000C6196 File Offset: 0x000C4396
		// (set) Token: 0x0600472C RID: 18220 RVA: 0x000C619E File Offset: 0x000C439E
		public int StatusCode
		{
			get
			{
				return this.status_code;
			}
			set
			{
				this.status_code = value;
			}
		}

		// Token: 0x1700160A RID: 5642
		// (get) Token: 0x0600472D RID: 18221 RVA: 0x000C61A7 File Offset: 0x000C43A7
		// (set) Token: 0x0600472E RID: 18222 RVA: 0x000C61AF File Offset: 0x000C43AF
		public string StatusDescription
		{
			get
			{
				return this.status_desc;
			}
			set
			{
				this.status_desc = value;
			}
		}

		// Token: 0x1700160B RID: 5643
		// (get) Token: 0x0600472F RID: 18223 RVA: 0x000C61B8 File Offset: 0x000C43B8
		public NameValueCollection Headers
		{
			get
			{
				return this.headers;
			}
		}

		// Token: 0x06004730 RID: 18224 RVA: 0x000C61C0 File Offset: 0x000C43C0
		public void SetHeaders(NameValueCollection headers)
		{
			this.headers = headers;
		}

		// Token: 0x06004731 RID: 18225 RVA: 0x000C61C9 File Offset: 0x000C43C9
		public void SetData(MemoryStream ms)
		{
			if (ms == null)
			{
				return;
			}
			this.Data.Add(new CachedRawResponse.DataItem(ms.GetBuffer(), ms.Length));
		}

		// Token: 0x06004732 RID: 18226 RVA: 0x000C61EC File Offset: 0x000C43EC
		public void SetData(HttpResponseSubstitutionCallback callback)
		{
			if (callback == null)
			{
				return;
			}
			this.Data.Add(new CachedRawResponse.DataItem(callback));
		}

		// Token: 0x06004733 RID: 18227 RVA: 0x000C6204 File Offset: 0x000C4404
		public IList GetData()
		{
			if (this.data == null || this.data.Count == 0)
			{
				return null;
			}
			return this.data;
		}

		// Token: 0x0400259B RID: 9627
		private HttpCachePolicy policy;

		// Token: 0x0400259C RID: 9628
		private CachedVaryBy varyby;

		// Token: 0x0400259D RID: 9629
		private int status_code;

		// Token: 0x0400259E RID: 9630
		private string status_desc;

		// Token: 0x0400259F RID: 9631
		private NameValueCollection headers;

		// Token: 0x040025A0 RID: 9632
		private List<CachedRawResponse.DataItem> data;

		// Token: 0x02000685 RID: 1669
		public sealed class DataItem
		{
			// Token: 0x06004734 RID: 18228 RVA: 0x000C6226 File Offset: 0x000C4426
			public DataItem(byte[] buffer, long length)
			{
				this.Buffer = buffer;
				this.Length = length;
			}

			// Token: 0x06004735 RID: 18229 RVA: 0x000C623C File Offset: 0x000C443C
			public DataItem(HttpResponseSubstitutionCallback callback)
				: this(null, 0L)
			{
				this.Callback = callback;
			}

			// Token: 0x040025A1 RID: 9633
			public readonly byte[] Buffer;

			// Token: 0x040025A2 RID: 9634
			public readonly long Length;

			// Token: 0x040025A3 RID: 9635
			public readonly HttpResponseSubstitutionCallback Callback;
		}
	}
}
