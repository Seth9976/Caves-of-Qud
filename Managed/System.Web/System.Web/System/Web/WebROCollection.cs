using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x020000EA RID: 234
	internal class WebROCollection : NameValueCollection
	{
		// Token: 0x06000C7F RID: 3199 RVA: 0x0002167D File Offset: 0x0001F87D
		public WebROCollection()
			: base(SecureHashCodeProvider.DefaultInvariant, CaseInsensitiveComparer.DefaultInvariant)
		{
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06000C80 RID: 3200 RVA: 0x0002168F File Offset: 0x0001F88F
		public bool GotID
		{
			get
			{
				return this.got_id;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06000C81 RID: 3201 RVA: 0x00021697 File Offset: 0x0001F897
		// (set) Token: 0x06000C82 RID: 3202 RVA: 0x0002169F File Offset: 0x0001F89F
		public int ID
		{
			get
			{
				return this.id;
			}
			set
			{
				this.got_id = true;
				this.id = value;
			}
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x000216AF File Offset: 0x0001F8AF
		public void Protect()
		{
			base.IsReadOnly = true;
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x000216B8 File Offset: 0x0001F8B8
		public void Unprotect()
		{
			base.IsReadOnly = false;
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x000216C4 File Offset: 0x0001F8C4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string text in this.AllKeys)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append('&');
				}
				if (text != null && text.Length > 0)
				{
					stringBuilder.Append(text);
					stringBuilder.Append('=');
				}
				stringBuilder.Append(this.Get(text));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001117 RID: 4375
		private bool got_id;

		// Token: 0x04001118 RID: 4376
		private int id;
	}
}
