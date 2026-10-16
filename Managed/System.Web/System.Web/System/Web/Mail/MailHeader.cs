using System;
using System.Collections.Specialized;

namespace System.Web.Mail
{
	// Token: 0x020000F4 RID: 244
	internal class MailHeader
	{
		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x00022ADE File Offset: 0x00020CDE
		// (set) Token: 0x06000CE8 RID: 3304 RVA: 0x00022AF0 File Offset: 0x00020CF0
		public string To
		{
			get
			{
				return this.data["To"];
			}
			set
			{
				this.data["To"] = value;
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x00022B03 File Offset: 0x00020D03
		// (set) Token: 0x06000CEA RID: 3306 RVA: 0x00022B15 File Offset: 0x00020D15
		public string From
		{
			get
			{
				return this.data["From"];
			}
			set
			{
				this.data["From"] = value;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06000CEB RID: 3307 RVA: 0x00022B28 File Offset: 0x00020D28
		// (set) Token: 0x06000CEC RID: 3308 RVA: 0x00022B3A File Offset: 0x00020D3A
		public string Cc
		{
			get
			{
				return this.data["Cc"];
			}
			set
			{
				this.data["Cc"] = value;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06000CED RID: 3309 RVA: 0x00022B4D File Offset: 0x00020D4D
		// (set) Token: 0x06000CEE RID: 3310 RVA: 0x00022B5F File Offset: 0x00020D5F
		public string Bcc
		{
			get
			{
				return this.data["Bcc"];
			}
			set
			{
				this.data["Bcc"] = value;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06000CEF RID: 3311 RVA: 0x00022B72 File Offset: 0x00020D72
		// (set) Token: 0x06000CF0 RID: 3312 RVA: 0x00022B84 File Offset: 0x00020D84
		public string Subject
		{
			get
			{
				return this.data["Subject"];
			}
			set
			{
				this.data["Subject"] = value;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06000CF1 RID: 3313 RVA: 0x00022B97 File Offset: 0x00020D97
		// (set) Token: 0x06000CF2 RID: 3314 RVA: 0x00022BA9 File Offset: 0x00020DA9
		public string Importance
		{
			get
			{
				return this.data["Importance"];
			}
			set
			{
				this.data["Importance"] = value;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x00022BBC File Offset: 0x00020DBC
		// (set) Token: 0x06000CF4 RID: 3316 RVA: 0x00022BCE File Offset: 0x00020DCE
		public string Priority
		{
			get
			{
				return this.data["Priority"];
			}
			set
			{
				this.data["Priority"] = value;
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x00022BE1 File Offset: 0x00020DE1
		// (set) Token: 0x06000CF6 RID: 3318 RVA: 0x00022BF3 File Offset: 0x00020DF3
		public string MimeVersion
		{
			get
			{
				return this.data["Mime-Version"];
			}
			set
			{
				this.data["Mime-Version"] = value;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x00022C06 File Offset: 0x00020E06
		// (set) Token: 0x06000CF8 RID: 3320 RVA: 0x00022C18 File Offset: 0x00020E18
		public string ContentType
		{
			get
			{
				return this.data["Content-Type"];
			}
			set
			{
				this.data["Content-Type"] = value;
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06000CF9 RID: 3321 RVA: 0x00022C2B File Offset: 0x00020E2B
		// (set) Token: 0x06000CFA RID: 3322 RVA: 0x00022C3D File Offset: 0x00020E3D
		public string ContentTransferEncoding
		{
			get
			{
				return this.data["Content-Transfer-Encoding"];
			}
			set
			{
				this.data["Content-Transfer-Encoding"] = value;
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06000CFB RID: 3323 RVA: 0x00022C50 File Offset: 0x00020E50
		// (set) Token: 0x06000CFC RID: 3324 RVA: 0x00022C62 File Offset: 0x00020E62
		public string ContentDisposition
		{
			get
			{
				return this.data["Content-Disposition"];
			}
			set
			{
				this.data["Content-Disposition"] = value;
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06000CFD RID: 3325 RVA: 0x00022C75 File Offset: 0x00020E75
		// (set) Token: 0x06000CFE RID: 3326 RVA: 0x00022C87 File Offset: 0x00020E87
		public string ContentBase
		{
			get
			{
				return this.data["Content-Base"];
			}
			set
			{
				this.data["Content-Base"] = value;
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06000CFF RID: 3327 RVA: 0x00022C9A File Offset: 0x00020E9A
		// (set) Token: 0x06000D00 RID: 3328 RVA: 0x00022CAC File Offset: 0x00020EAC
		public string ContentLocation
		{
			get
			{
				return this.data["Content-Location"];
			}
			set
			{
				this.data["Content-Location"] = value;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06000D01 RID: 3329 RVA: 0x00022CBF File Offset: 0x00020EBF
		public NameValueCollection Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x0400112E RID: 4398
		protected NameValueCollection data = new NameValueCollection();
	}
}
