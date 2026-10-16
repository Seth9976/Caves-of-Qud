using System;

namespace System.Web.Mail
{
	// Token: 0x020000FD RID: 253
	internal class SmtpResponse
	{
		// Token: 0x06000D4B RID: 3403 RVA: 0x00002050 File Offset: 0x00000250
		protected SmtpResponse()
		{
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06000D4C RID: 3404 RVA: 0x00023C58 File Offset: 0x00021E58
		// (set) Token: 0x06000D4D RID: 3405 RVA: 0x00023C60 File Offset: 0x00021E60
		public int StatusCode
		{
			get
			{
				return this.statusCode;
			}
			set
			{
				this.statusCode = value;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06000D4E RID: 3406 RVA: 0x00023C69 File Offset: 0x00021E69
		// (set) Token: 0x06000D4F RID: 3407 RVA: 0x00023C71 File Offset: 0x00021E71
		public string RawResponse
		{
			get
			{
				return this.rawResponse;
			}
			set
			{
				this.rawResponse = value;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06000D50 RID: 3408 RVA: 0x00023C7A File Offset: 0x00021E7A
		// (set) Token: 0x06000D51 RID: 3409 RVA: 0x00023C82 File Offset: 0x00021E82
		public string[] Parts
		{
			get
			{
				return this.parts;
			}
			set
			{
				this.parts = value;
			}
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x00023C8C File Offset: 0x00021E8C
		public static SmtpResponse Parse(string line)
		{
			SmtpResponse smtpResponse = new SmtpResponse();
			if (line.Length < 4)
			{
				throw new SmtpException("Response is to short " + line.Length.ToString() + ".");
			}
			if (line[3] != ' ' && line[3] != '-')
			{
				throw new SmtpException("Response format is wrong.(" + line + ")");
			}
			smtpResponse.StatusCode = int.Parse(line.Substring(0, 3));
			smtpResponse.RawResponse = line;
			smtpResponse.Parts = line.Substring(0, 3).Split(';', StringSplitOptions.None);
			return smtpResponse;
		}

		// Token: 0x04001153 RID: 4435
		private string rawResponse;

		// Token: 0x04001154 RID: 4436
		private int statusCode;

		// Token: 0x04001155 RID: 4437
		private string[] parts;
	}
}
