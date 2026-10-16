using System;
using System.Text;

namespace System.Web.Mail
{
	// Token: 0x020000EF RID: 239
	internal class MailAddress
	{
		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06000CD0 RID: 3280 RVA: 0x00022726 File Offset: 0x00020926
		// (set) Token: 0x06000CD1 RID: 3281 RVA: 0x0002272E File Offset: 0x0002092E
		public string User
		{
			get
			{
				return this.user;
			}
			set
			{
				this.user = value;
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06000CD2 RID: 3282 RVA: 0x00022737 File Offset: 0x00020937
		// (set) Token: 0x06000CD3 RID: 3283 RVA: 0x0002273F File Offset: 0x0002093F
		public string Host
		{
			get
			{
				return this.host;
			}
			set
			{
				this.host = value;
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06000CD4 RID: 3284 RVA: 0x00022748 File Offset: 0x00020948
		// (set) Token: 0x06000CD5 RID: 3285 RVA: 0x00022750 File Offset: 0x00020950
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x00022759 File Offset: 0x00020959
		// (set) Token: 0x06000CD7 RID: 3287 RVA: 0x00022774 File Offset: 0x00020974
		public string Address
		{
			get
			{
				return this.user + "@" + this.host;
			}
			set
			{
				string[] array = value.Split(new char[] { '@' });
				if (array.Length != 2)
				{
					throw new FormatException("Invalid e-mail address: '" + value + "'.");
				}
				this.user = array[0];
				this.host = array[1];
			}
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x000227C4 File Offset: 0x000209C4
		public static MailAddress Parse(string str)
		{
			if (str == null || str.Trim() == "")
			{
				return null;
			}
			MailAddress mailAddress = new MailAddress();
			string text = null;
			string text2 = null;
			foreach (string text3 in str.Split(new char[] { ' ', '<' }))
			{
				if (text3.IndexOf('@') > 0)
				{
					text = text3;
					break;
				}
				text2 = text2 + text3 + " ";
			}
			if (text == null)
			{
				throw new FormatException("Invalid e-mail address: '" + str + "'.");
			}
			text = text.Trim(new char[] { '<', '>', '(', ')' });
			mailAddress.Address = text;
			if (text2 != null)
			{
				mailAddress.Name = text2.Trim(new char[] { ' ', '"' });
				mailAddress.Name = ((mailAddress.Name.Length == 0) ? null : mailAddress.Name);
			}
			return mailAddress;
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x000228B4 File Offset: 0x00020AB4
		public override string ToString()
		{
			string text;
			if (this.name == null)
			{
				text = "<" + this.Address + ">";
			}
			else
			{
				string text2 = this.Name;
				if (MailUtil.NeedEncoding(text2))
				{
					text2 = string.Concat(new string[]
					{
						"=?",
						Encoding.Default.BodyName,
						"?B?",
						MailUtil.Base64Encode(text2),
						"?="
					});
				}
				text = string.Concat(new string[] { "\"", text2, "\" <", this.Address, ">" });
			}
			return text;
		}

		// Token: 0x04001122 RID: 4386
		protected string user;

		// Token: 0x04001123 RID: 4387
		protected string host;

		// Token: 0x04001124 RID: 4388
		protected string name;
	}
}
