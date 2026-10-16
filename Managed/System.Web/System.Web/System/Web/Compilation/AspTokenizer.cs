using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace System.Web.Compilation
{
	// Token: 0x02000629 RID: 1577
	internal class AspTokenizer
	{
		// Token: 0x17001550 RID: 5456
		// (get) Token: 0x06004394 RID: 17300 RVA: 0x000B461A File Offset: 0x000B281A
		public MD5 Checksum
		{
			get
			{
				return this.checksum;
			}
		}

		// Token: 0x06004395 RID: 17301 RVA: 0x000B4624 File Offset: 0x000B2824
		public AspTokenizer(TextReader reader)
		{
			this.sr = reader;
			this.sb = new StringBuilder();
			this.odds = new StringBuilder();
			this.col = (this.line = 1);
			this.hasPutBack = (this.inTag = false);
		}

		// Token: 0x17001551 RID: 5457
		// (get) Token: 0x06004396 RID: 17302 RVA: 0x000B468B File Offset: 0x000B288B
		// (set) Token: 0x06004397 RID: 17303 RVA: 0x000B4693 File Offset: 0x000B2893
		public bool Verbatim
		{
			get
			{
				return this.verbatim;
			}
			set
			{
				this.verbatim = value;
			}
		}

		// Token: 0x06004398 RID: 17304 RVA: 0x000B469C File Offset: 0x000B289C
		public void put_back()
		{
			if (this.hasPutBack && !this.inTag)
			{
				throw new HttpException("put_back called twice!");
			}
			this.hasPutBack = true;
			if (this.putBackBuffer == null)
			{
				this.putBackBuffer = new Stack();
			}
			string value = this.Value;
			this.putBackBuffer.Push(new AspTokenizer.PutBackItem(value, this.position, this.current_token, this.inTag));
			this.position -= value.Length;
		}

		// Token: 0x06004399 RID: 17305 RVA: 0x000B471C File Offset: 0x000B291C
		public int get_token()
		{
			if (this.hasPutBack)
			{
				AspTokenizer.PutBackItem putBackItem;
				if (this.verbatim)
				{
					putBackItem = this.putBackBuffer.Pop() as AspTokenizer.PutBackItem;
					string value = putBackItem.Value;
					int length = value.Length;
					if (length != 0)
					{
						if (length == 1)
						{
							putBackItem = new AspTokenizer.PutBackItem(string.Empty, putBackItem.Position, (int)value[0], false);
						}
						else
						{
							putBackItem = new AspTokenizer.PutBackItem(value, putBackItem.Position, (int)value[0], false);
						}
					}
				}
				else
				{
					putBackItem = this.putBackBuffer.Pop() as AspTokenizer.PutBackItem;
				}
				this.hasPutBack = this.putBackBuffer.Count > 0;
				this.position = putBackItem.Position;
				this.have_value = false;
				this.val = null;
				this.sb = new StringBuilder(putBackItem.Value);
				this.current_token = putBackItem.CurrentToken;
				this.inTag = putBackItem.InTag;
				return this.current_token;
			}
			this.begline = this.line;
			this.begcol = this.col;
			this.have_value = false;
			this.current_token = this.NextToken();
			return this.current_token;
		}

		// Token: 0x0600439A RID: 17306 RVA: 0x000B4834 File Offset: 0x000B2A34
		private bool is_identifier_start_character(char c)
		{
			return char.IsLetter(c) || c == '_';
		}

		// Token: 0x0600439B RID: 17307 RVA: 0x000B4845 File Offset: 0x000B2A45
		private bool is_identifier_part_character(char c)
		{
			return char.IsLetterOrDigit(c) || c == '_' || c == '-';
		}

		// Token: 0x0600439C RID: 17308 RVA: 0x000B485B File Offset: 0x000B2A5B
		private void ungetc(int value)
		{
			this.have_unget = true;
			this.unget_value = value;
			this.position--;
			this.col--;
		}

		// Token: 0x0600439D RID: 17309 RVA: 0x000B4888 File Offset: 0x000B2A88
		private void TransformNextBlock(int count, bool final)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(this.checksum_buf, 0, count);
			if (this.checksum == null)
			{
				this.checksum = MD5.Create();
			}
			if (final)
			{
				this.checksum.TransformFinalBlock(bytes, 0, bytes.Length);
			}
			else
			{
				this.checksum.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			}
			this.checksum_buf_pos = -1;
		}

		// Token: 0x0600439E RID: 17310 RVA: 0x000B48F0 File Offset: 0x000B2AF0
		private void UpdateChecksum(int c)
		{
			if (c != -1)
			{
				if (this.checksum_buf_pos + 1 >= 8192)
				{
					this.TransformNextBlock(this.checksum_buf_pos + 1, false);
				}
				char[] array = this.checksum_buf;
				int num = this.checksum_buf_pos + 1;
				this.checksum_buf_pos = num;
				array[num] = (ushort)c;
				return;
			}
			this.TransformNextBlock(this.checksum_buf_pos + 1, true);
		}

		// Token: 0x0600439F RID: 17311 RVA: 0x000B494C File Offset: 0x000B2B4C
		private int read_char()
		{
			int num;
			if (this.have_unget)
			{
				num = this.unget_value;
				this.have_unget = false;
			}
			else
			{
				num = this.sr.Read();
				this.UpdateChecksum(num);
			}
			if (num == 13 && this.sr.Peek() == 10)
			{
				num = this.sr.Read();
				this.UpdateChecksum(num);
				this.position++;
			}
			if (num == 10)
			{
				this.col = -1;
				this.line++;
			}
			if (num != -1)
			{
				this.col++;
				this.position++;
			}
			return num;
		}

		// Token: 0x060043A0 RID: 17312 RVA: 0x000B49F4 File Offset: 0x000B2BF4
		private int ReadAttValue(int start)
		{
			int num = 0;
			bool flag = false;
			if (start == 34 || start == 39)
			{
				num = start;
				flag = true;
			}
			else
			{
				this.sb.Append((char)start);
			}
			int num2 = 0;
			bool flag2 = false;
			this.alternatingQuotes = true;
			int num3;
			while ((num3 = this.sr.Peek()) != -1)
			{
				if (num3 == 37 && num2 == 60)
				{
					flag2 = true;
				}
				else if (flag2 && num3 == 62 && num2 == 37)
				{
					flag2 = false;
				}
				else if (!flag2)
				{
					if (!flag && num3 == 47)
					{
						this.read_char();
						num3 = this.sr.Peek();
						if (num3 == -1)
						{
							num3 = 47;
						}
						else if (num3 == 62)
						{
							this.ungetc(47);
							break;
						}
					}
					else
					{
						if (!flag && (num3 == 62 || char.IsWhiteSpace((char)num3)))
						{
							break;
						}
						if (flag && num3 == num && num2 != 92)
						{
							this.read_char();
							break;
						}
					}
				}
				else if (flag && num3 == num)
				{
					this.alternatingQuotes = false;
				}
				this.sb.Append((char)num3);
				this.read_char();
				num2 = num3;
			}
			return 2097155;
		}

		// Token: 0x060043A1 RID: 17313 RVA: 0x000B4AF0 File Offset: 0x000B2CF0
		private int NextToken()
		{
			this.sb.Length = 0;
			this.odds.Length = 0;
			int num;
			while ((num = this.read_char()) != -1)
			{
				if (this.verbatim)
				{
					this.inTag = false;
					this.sb.Append((char)num);
					return num;
				}
				if (this.inTag && this.expectAttrValue && (num == 34 || num == 39))
				{
					return this.ReadAttValue(num);
				}
				if (num == 60)
				{
					this.inTag = true;
					this.sb.Append((char)num);
					return num;
				}
				if (num == 62)
				{
					this.inTag = false;
					this.sb.Append((char)num);
					return num;
				}
				if (this.current_token == 60 && "%/!".IndexOf((char)num) != -1)
				{
					this.sb.Append((char)num);
					return num;
				}
				if (this.inTag && this.current_token == 37 && "@#=".IndexOf((char)num) != -1)
				{
					if (this.odds.Length == 0 || this.odds.ToString().IndexOfAny(AspTokenizer.lfcr) < 0)
					{
						this.sb.Append((char)num);
						return num;
					}
					this.sb.Append((char)num);
				}
				else
				{
					if (this.inTag && num == 45 && this.sr.Peek() == 45)
					{
						this.sb.Append("--");
						this.read_char();
						return 2097157;
					}
					if (!this.inTag)
					{
						this.sb.Append((char)num);
						while ((num = this.sr.Peek()) != -1 && num != 60)
						{
							this.sb.Append((char)this.read_char());
						}
						if (num == -1 && this.sb.Length <= 0)
						{
							return 2097152;
						}
						return 2097156;
					}
					else
					{
						if (this.inTag && this.current_token == 61 && !char.IsWhiteSpace((char)num))
						{
							return this.ReadAttValue(num);
						}
						if (this.inTag && this.is_identifier_start_character((char)num))
						{
							this.sb.Append((char)num);
							while ((num = this.sr.Peek()) != -1 && (this.is_identifier_part_character((char)num) || num == 58))
							{
								this.sb.Append((char)this.read_char());
							}
							if (this.current_token == 64 && Directive.IsDirective(this.sb.ToString()))
							{
								return 2097154;
							}
							return 2097153;
						}
						else
						{
							if (!char.IsWhiteSpace((char)num))
							{
								this.sb.Append((char)num);
								return num;
							}
							this.odds.Append((char)num);
						}
					}
				}
			}
			return 2097152;
		}

		// Token: 0x17001552 RID: 5458
		// (get) Token: 0x060043A2 RID: 17314 RVA: 0x000B4D96 File Offset: 0x000B2F96
		public string Value
		{
			get
			{
				if (this.have_value)
				{
					return this.val;
				}
				this.have_value = true;
				this.val = this.sb.ToString();
				return this.val;
			}
		}

		// Token: 0x17001553 RID: 5459
		// (get) Token: 0x060043A3 RID: 17315 RVA: 0x000B4DC5 File Offset: 0x000B2FC5
		public string Odds
		{
			get
			{
				return this.odds.ToString();
			}
		}

		// Token: 0x17001554 RID: 5460
		// (get) Token: 0x060043A4 RID: 17316 RVA: 0x000B4DD2 File Offset: 0x000B2FD2
		// (set) Token: 0x060043A5 RID: 17317 RVA: 0x000B4DDA File Offset: 0x000B2FDA
		public bool InTag
		{
			get
			{
				return this.inTag;
			}
			set
			{
				this.inTag = value;
			}
		}

		// Token: 0x17001555 RID: 5461
		// (get) Token: 0x060043A6 RID: 17318 RVA: 0x000B4DE3 File Offset: 0x000B2FE3
		// (set) Token: 0x060043A7 RID: 17319 RVA: 0x000B4DEB File Offset: 0x000B2FEB
		public bool ExpectAttrValue
		{
			get
			{
				return this.expectAttrValue;
			}
			set
			{
				this.expectAttrValue = value;
			}
		}

		// Token: 0x17001556 RID: 5462
		// (get) Token: 0x060043A8 RID: 17320 RVA: 0x000B4DF4 File Offset: 0x000B2FF4
		public bool AlternatingQuotes
		{
			get
			{
				return this.alternatingQuotes;
			}
		}

		// Token: 0x17001557 RID: 5463
		// (get) Token: 0x060043A9 RID: 17321 RVA: 0x000B4DFC File Offset: 0x000B2FFC
		public int BeginLine
		{
			get
			{
				return this.begline;
			}
		}

		// Token: 0x17001558 RID: 5464
		// (get) Token: 0x060043AA RID: 17322 RVA: 0x000B4E04 File Offset: 0x000B3004
		public int BeginColumn
		{
			get
			{
				return this.begcol;
			}
		}

		// Token: 0x17001559 RID: 5465
		// (get) Token: 0x060043AB RID: 17323 RVA: 0x000B4E0C File Offset: 0x000B300C
		public int EndLine
		{
			get
			{
				return this.line;
			}
		}

		// Token: 0x1700155A RID: 5466
		// (get) Token: 0x060043AC RID: 17324 RVA: 0x000B4E14 File Offset: 0x000B3014
		public int EndColumn
		{
			get
			{
				return this.col;
			}
		}

		// Token: 0x1700155B RID: 5467
		// (get) Token: 0x060043AD RID: 17325 RVA: 0x000B4E1C File Offset: 0x000B301C
		public int Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x04002437 RID: 9271
		private const int CHECKSUM_BUF_SIZE = 8192;

		// Token: 0x04002438 RID: 9272
		private static char[] lfcr = new char[] { '\n', '\r' };

		// Token: 0x04002439 RID: 9273
		private TextReader sr;

		// Token: 0x0400243A RID: 9274
		private int current_token;

		// Token: 0x0400243B RID: 9275
		private StringBuilder sb;

		// Token: 0x0400243C RID: 9276
		private StringBuilder odds;

		// Token: 0x0400243D RID: 9277
		private int col;

		// Token: 0x0400243E RID: 9278
		private int line;

		// Token: 0x0400243F RID: 9279
		private int begcol;

		// Token: 0x04002440 RID: 9280
		private int begline;

		// Token: 0x04002441 RID: 9281
		private int position;

		// Token: 0x04002442 RID: 9282
		private bool inTag;

		// Token: 0x04002443 RID: 9283
		private bool expectAttrValue;

		// Token: 0x04002444 RID: 9284
		private bool alternatingQuotes;

		// Token: 0x04002445 RID: 9285
		private bool hasPutBack;

		// Token: 0x04002446 RID: 9286
		private bool verbatim;

		// Token: 0x04002447 RID: 9287
		private bool have_value;

		// Token: 0x04002448 RID: 9288
		private bool have_unget;

		// Token: 0x04002449 RID: 9289
		private int unget_value;

		// Token: 0x0400244A RID: 9290
		private string val;

		// Token: 0x0400244B RID: 9291
		private Stack putBackBuffer;

		// Token: 0x0400244C RID: 9292
		private MD5 checksum;

		// Token: 0x0400244D RID: 9293
		private char[] checksum_buf = new char[8192];

		// Token: 0x0400244E RID: 9294
		private int checksum_buf_pos = -1;

		// Token: 0x0200062A RID: 1578
		private class PutBackItem
		{
			// Token: 0x060043AF RID: 17327 RVA: 0x000B4E3B File Offset: 0x000B303B
			public PutBackItem(string value, int position, int currentToken, bool inTag)
			{
				this.Value = value;
				this.Position = position;
				this.CurrentToken = currentToken;
				this.InTag = inTag;
			}

			// Token: 0x0400244F RID: 9295
			public readonly string Value;

			// Token: 0x04002450 RID: 9296
			public readonly int Position;

			// Token: 0x04002451 RID: 9297
			public readonly int CurrentToken;

			// Token: 0x04002452 RID: 9298
			public readonly bool InTag;
		}
	}
}
