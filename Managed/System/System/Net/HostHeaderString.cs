using System;
using System.Text;

namespace System.Net
{
	// Token: 0x02000611 RID: 1553
	internal class HostHeaderString
	{
		// Token: 0x06003119 RID: 12569 RVA: 0x000A96AE File Offset: 0x000A78AE
		internal HostHeaderString()
		{
			this.Init(null);
		}

		// Token: 0x0600311A RID: 12570 RVA: 0x000A96BD File Offset: 0x000A78BD
		internal HostHeaderString(string s)
		{
			this.Init(s);
		}

		// Token: 0x0600311B RID: 12571 RVA: 0x000A96CC File Offset: 0x000A78CC
		private void Init(string s)
		{
			this.m_String = s;
			this.m_Converted = false;
			this.m_Bytes = null;
		}

		// Token: 0x0600311C RID: 12572 RVA: 0x000A96E4 File Offset: 0x000A78E4
		private void Convert()
		{
			if (this.m_String != null && !this.m_Converted)
			{
				this.m_Bytes = Encoding.Default.GetBytes(this.m_String);
				string @string = Encoding.Default.GetString(this.m_Bytes);
				if (string.Compare(this.m_String, @string, StringComparison.Ordinal) != 0)
				{
					this.m_Bytes = Encoding.UTF8.GetBytes(this.m_String);
				}
			}
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x0600311D RID: 12573 RVA: 0x000A974D File Offset: 0x000A794D
		// (set) Token: 0x0600311E RID: 12574 RVA: 0x000A9755 File Offset: 0x000A7955
		internal string String
		{
			get
			{
				return this.m_String;
			}
			set
			{
				this.Init(value);
			}
		}

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x0600311F RID: 12575 RVA: 0x000A975E File Offset: 0x000A795E
		internal int ByteCount
		{
			get
			{
				this.Convert();
				return this.m_Bytes.Length;
			}
		}

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06003120 RID: 12576 RVA: 0x000A976E File Offset: 0x000A796E
		internal byte[] Bytes
		{
			get
			{
				this.Convert();
				return this.m_Bytes;
			}
		}

		// Token: 0x06003121 RID: 12577 RVA: 0x000A977C File Offset: 0x000A797C
		internal void Copy(byte[] destBytes, int destByteIndex)
		{
			this.Convert();
			Array.Copy(this.m_Bytes, 0, destBytes, destByteIndex, this.m_Bytes.Length);
		}

		// Token: 0x04001CB5 RID: 7349
		private bool m_Converted;

		// Token: 0x04001CB6 RID: 7350
		private string m_String;

		// Token: 0x04001CB7 RID: 7351
		private byte[] m_Bytes;
	}
}
