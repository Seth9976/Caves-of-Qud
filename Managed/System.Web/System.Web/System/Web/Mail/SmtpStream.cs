using System;
using System.IO;
using System.Text;

namespace System.Web.Mail
{
	// Token: 0x020000FE RID: 254
	internal class SmtpStream
	{
		// Token: 0x06000D53 RID: 3411 RVA: 0x00023D26 File Offset: 0x00021F26
		public SmtpStream(Stream stream)
		{
			this.stream = stream;
			this.encoding = new ASCIIEncoding();
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06000D54 RID: 3412 RVA: 0x00023D4B File Offset: 0x00021F4B
		public Stream Stream
		{
			get
			{
				return this.stream;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06000D55 RID: 3413 RVA: 0x00023D53 File Offset: 0x00021F53
		public SmtpResponse LastResponse
		{
			get
			{
				return this.lastResponse;
			}
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x00023D5B File Offset: 0x00021F5B
		public void WriteRset()
		{
			this.command = "RSET";
			this.WriteLine(this.command);
			this.ReadResponse();
			this.CheckForStatusCode(250);
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x00023D85 File Offset: 0x00021F85
		public void WriteAuthLogin()
		{
			this.command = "AUTH LOGIN";
			this.WriteLine(this.command);
			this.ReadResponse();
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x00023DA4 File Offset: 0x00021FA4
		public bool WriteStartTLS()
		{
			this.command = "STARTTLS";
			this.WriteLine(this.command);
			this.ReadResponse();
			return this.LastResponse.StatusCode == 220;
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x00023DD5 File Offset: 0x00021FD5
		public void WriteEhlo(string hostName)
		{
			this.command = "EHLO " + hostName;
			this.WriteLine(this.command);
			this.ReadResponse();
			this.CheckForStatusCode(250);
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x00023E05 File Offset: 0x00022005
		public void WriteHelo(string hostName)
		{
			this.command = "HELO " + hostName;
			this.WriteLine(this.command);
			this.ReadResponse();
			this.CheckForStatusCode(250);
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x00023E35 File Offset: 0x00022035
		public void WriteMailFrom(string from)
		{
			this.command = "MAIL FROM: <" + from + ">";
			this.WriteLine(this.command);
			this.ReadResponse();
			this.CheckForStatusCode(250);
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x00023E6A File Offset: 0x0002206A
		public void WriteRcptTo(string to)
		{
			this.command = "RCPT TO: <" + to + ">";
			this.WriteLine(this.command);
			this.ReadResponse();
			this.CheckForStatusCode(250);
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x00023E9F File Offset: 0x0002209F
		public void WriteData()
		{
			this.command = "DATA";
			this.WriteLine(this.command);
			this.ReadResponse();
			this.CheckForStatusCode(354);
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x00023EC9 File Offset: 0x000220C9
		public void WriteQuit()
		{
			this.command = "QUIT";
			this.WriteLine(this.command);
			this.ReadResponse();
			this.CheckForStatusCode(221);
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x00023EF3 File Offset: 0x000220F3
		public void WriteBoundary(string boundary)
		{
			this.WriteLine("\r\n--{0}", new object[] { boundary });
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x00023F0A File Offset: 0x0002210A
		public void WriteFinalBoundary(string boundary)
		{
			this.WriteLine("\r\n--{0}--", new object[] { boundary });
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x00023F21 File Offset: 0x00022121
		public void WriteDataEndTag()
		{
			this.command = "\r\n.";
			this.WriteLine(this.command);
			this.ReadResponse();
			this.CheckForStatusCode(250);
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x00023F4C File Offset: 0x0002214C
		public void WriteHeader(MailHeader header)
		{
			foreach (string text in header.Data.AllKeys)
			{
				this.WriteLine("{0}: {1}", new object[]
				{
					text,
					header.Data[text]
				});
			}
			this.WriteLine("");
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x00023FA8 File Offset: 0x000221A8
		public void CheckForStatusCode(int statusCode)
		{
			if (this.LastResponse.StatusCode != statusCode)
			{
				throw new SmtpException(string.Concat(new string[]
				{
					"Server reponse: '",
					this.lastResponse.RawResponse,
					"';Status code: '",
					this.lastResponse.StatusCode.ToString(),
					"';Expected status code: '",
					statusCode.ToString(),
					"';Last command: '",
					this.command,
					"'"
				}));
			}
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x00024034 File Offset: 0x00022234
		public void WriteBytes(byte[] buffer)
		{
			this.stream.Write(buffer, 0, buffer.Length);
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x00024046 File Offset: 0x00022246
		public void WriteLine(string format, params object[] args)
		{
			this.WriteLine(string.Format(format, args));
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x00024058 File Offset: 0x00022258
		public void WriteLine(string line)
		{
			byte[] bytes = this.encoding.GetBytes(line + "\r\n");
			this.stream.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x0002408C File Offset: 0x0002228C
		public void ReadResponse()
		{
			byte[] array = new byte[512];
			int num = 0;
			bool flag = false;
			do
			{
				int num2 = this.stream.Read(array, num, array.Length - num);
				if (num2 > 0)
				{
					int num3 = num + num2 - 1;
					if (num3 > 4 && (array[num3] == 10 || array[num3] == 13))
					{
						int num4 = num3 - 3;
						while (num4 >= 0 && array[num4] != 10 && array[num4] != 13)
						{
							num4--;
						}
						flag = array[num4 + 4] == 32;
					}
					num += num2;
					if (num == array.Length)
					{
						byte[] array2 = new byte[array.Length * 2];
						Array.Copy(array, 0, array2, 0, array.Length);
						array = array2;
					}
				}
			}
			while (!flag);
			string @string = this.encoding.GetString(array, 0, num - 1);
			this.lastResponse = SmtpResponse.Parse(@string);
		}

		// Token: 0x04001156 RID: 4438
		protected Stream stream;

		// Token: 0x04001157 RID: 4439
		protected Encoding encoding;

		// Token: 0x04001158 RID: 4440
		protected SmtpResponse lastResponse;

		// Token: 0x04001159 RID: 4441
		protected string command = "";
	}
}
