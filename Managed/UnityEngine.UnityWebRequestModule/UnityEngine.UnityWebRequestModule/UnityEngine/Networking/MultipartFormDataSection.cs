using System;
using System.Text;

namespace UnityEngine.Networking
{
	// Token: 0x02000010 RID: 16
	public class MultipartFormDataSection : IMultipartFormSection
	{
		// Token: 0x06000077 RID: 119 RVA: 0x00003AA0 File Offset: 0x00001CA0
		public MultipartFormDataSection(string name, byte[] data, string contentType)
		{
			bool flag = data == null || data.Length < 1;
			if (flag)
			{
				throw new ArgumentException("Cannot create a multipart form data section without body data");
			}
			this.name = name;
			this.data = data;
			this.content = contentType;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003AE6 File Offset: 0x00001CE6
		public MultipartFormDataSection(string name, byte[] data)
			: this(name, data, null)
		{
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003AF3 File Offset: 0x00001CF3
		public MultipartFormDataSection(byte[] data)
			: this(null, data)
		{
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003B00 File Offset: 0x00001D00
		public MultipartFormDataSection(string name, string data, Encoding encoding, string contentType)
		{
			bool flag = string.IsNullOrEmpty(data);
			if (flag)
			{
				throw new ArgumentException("Cannot create a multipart form data section without body data");
			}
			byte[] bytes = encoding.GetBytes(data);
			this.name = name;
			this.data = bytes;
			bool flag2 = contentType != null && !contentType.Contains("encoding=");
			if (flag2)
			{
				contentType = contentType.Trim() + "; encoding=" + encoding.WebName;
			}
			this.content = contentType;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003B7E File Offset: 0x00001D7E
		public MultipartFormDataSection(string name, string data, string contentType)
			: this(name, data, Encoding.UTF8, contentType)
		{
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003B90 File Offset: 0x00001D90
		public MultipartFormDataSection(string name, string data)
			: this(name, data, "text/plain")
		{
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003BA1 File Offset: 0x00001DA1
		public MultipartFormDataSection(string data)
			: this(null, data)
		{
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003BB0 File Offset: 0x00001DB0
		public string sectionName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00003BC8 File Offset: 0x00001DC8
		public byte[] sectionData
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00003BE0 File Offset: 0x00001DE0
		public string fileName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003BF4 File Offset: 0x00001DF4
		public string contentType
		{
			get
			{
				return this.content;
			}
		}

		// Token: 0x0400001C RID: 28
		private string name;

		// Token: 0x0400001D RID: 29
		private byte[] data;

		// Token: 0x0400001E RID: 30
		private string content;
	}
}
