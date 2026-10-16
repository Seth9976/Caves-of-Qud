using System;
using System.Collections.Specialized;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x02000097 RID: 151
	internal sealed class HttpHeaderCollection : NameValueCollection
	{
		// Token: 0x06000758 RID: 1880 RVA: 0x000101CF File Offset: 0x0000E3CF
		public HttpHeaderCollection()
			: base(StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x00011218 File Offset: 0x0000F418
		private bool HeaderCheckingEnabled
		{
			get
			{
				if (this.headerCheckingEnabled == null)
				{
					this.headerCheckingEnabled = new bool?(HttpRuntime.Section.EnableHeaderChecking);
				}
				return this.headerCheckingEnabled.Value;
			}
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00011247 File Offset: 0x0000F447
		public override void Add(string name, string value)
		{
			this.EncodeAndSetHeader(name, value, false);
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x00011252 File Offset: 0x0000F452
		public override void Set(string name, string value)
		{
			this.EncodeAndSetHeader(name, value, true);
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00011260 File Offset: 0x0000F460
		private void EncodeAndSetHeader(string name, string value, bool replaceExisting)
		{
			if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value))
			{
				return;
			}
			string text;
			string text2;
			if (this.HeaderCheckingEnabled)
			{
				global::System.Web.Util.HttpEncoder.Current.HeaderNameValueEncode(name, value, out text, out text2);
			}
			else
			{
				text = name;
				text2 = value;
			}
			if (replaceExisting)
			{
				base.Set(text, text2);
				return;
			}
			base.Add(text, text2);
		}

		// Token: 0x04000F66 RID: 3942
		private bool? headerCheckingEnabled;
	}
}
