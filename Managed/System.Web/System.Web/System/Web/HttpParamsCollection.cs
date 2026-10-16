using System;
using System.Collections.Specialized;
using System.Runtime.Serialization;

namespace System.Web
{
	// Token: 0x0200009C RID: 156
	internal class HttpParamsCollection : WebROCollection
	{
		// Token: 0x0600076F RID: 1903 RVA: 0x0001135D File Offset: 0x0000F55D
		public HttpParamsCollection(NameValueCollection queryString, NameValueCollection form, NameValueCollection serverVariables, HttpCookieCollection cookies)
		{
			this._queryString = queryString;
			this._form = form;
			this._serverVariables = serverVariables;
			this._cookies = cookies;
			this._merged = false;
			base.Protect();
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0001138F File Offset: 0x0000F58F
		public override string Get(string name)
		{
			this.MergeCollections();
			return base.Get(name);
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x000113A0 File Offset: 0x0000F5A0
		private void MergeCollections()
		{
			if (this._merged)
			{
				return;
			}
			base.Unprotect();
			base.Add(this._queryString);
			base.Add(this._form);
			base.Add(this._serverVariables);
			for (int i = 0; i < this._cookies.Count; i++)
			{
				HttpCookie httpCookie = this._cookies[i];
				this.Add(httpCookie.Name, httpCookie.Value);
			}
			this._merged = true;
			base.Protect();
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00011422 File Offset: 0x0000F622
		public override string Get(int index)
		{
			this.MergeCollections();
			return base.Get(index);
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00011431 File Offset: 0x0000F631
		public override string GetKey(int index)
		{
			this.MergeCollections();
			return base.GetKey(index);
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00011440 File Offset: 0x0000F640
		public override string[] GetValues(int index)
		{
			this.MergeCollections();
			return base.GetValues(index);
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0001144F File Offset: 0x0000F64F
		public override string[] GetValues(string name)
		{
			this.MergeCollections();
			return base.GetValues(name);
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00007638 File Offset: 0x00005838
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new SerializationException();
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000777 RID: 1911 RVA: 0x0001145E File Offset: 0x0000F65E
		public override string[] AllKeys
		{
			get
			{
				this.MergeCollections();
				return base.AllKeys;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000778 RID: 1912 RVA: 0x0001146C File Offset: 0x0000F66C
		public override int Count
		{
			get
			{
				this.MergeCollections();
				return base.Count;
			}
		}

		// Token: 0x04000F67 RID: 3943
		private NameValueCollection _queryString;

		// Token: 0x04000F68 RID: 3944
		private NameValueCollection _form;

		// Token: 0x04000F69 RID: 3945
		private NameValueCollection _serverVariables;

		// Token: 0x04000F6A RID: 3946
		private HttpCookieCollection _cookies;

		// Token: 0x04000F6B RID: 3947
		private bool _merged;
	}
}
