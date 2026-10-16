using System;
using System.Text;

namespace UnityEngine.Networking
{
	// Token: 0x02000011 RID: 17
	public class MultipartFormFileSection : IMultipartFormSection
	{
		// Token: 0x06000082 RID: 130 RVA: 0x00003C0C File Offset: 0x00001E0C
		private void Init(string name, byte[] data, string fileName, string contentType)
		{
			this.name = name;
			this.data = data;
			this.file = fileName;
			this.content = contentType;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003C2C File Offset: 0x00001E2C
		public MultipartFormFileSection(string name, byte[] data, string fileName, string contentType)
		{
			bool flag = data == null || data.Length < 1;
			if (flag)
			{
				throw new ArgumentException("Cannot create a multipart form file section without body data");
			}
			bool flag2 = string.IsNullOrEmpty(fileName);
			if (flag2)
			{
				fileName = "file.dat";
			}
			bool flag3 = string.IsNullOrEmpty(contentType);
			if (flag3)
			{
				contentType = "application/octet-stream";
			}
			this.Init(name, data, fileName, contentType);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003C90 File Offset: 0x00001E90
		public MultipartFormFileSection(byte[] data)
			: this(null, data, null, null)
		{
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003C9E File Offset: 0x00001E9E
		public MultipartFormFileSection(string fileName, byte[] data)
			: this(null, data, fileName, null)
		{
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003CAC File Offset: 0x00001EAC
		public MultipartFormFileSection(string name, string data, Encoding dataEncoding, string fileName)
		{
			bool flag = string.IsNullOrEmpty(data);
			if (flag)
			{
				throw new ArgumentException("Cannot create a multipart form file section without body data");
			}
			bool flag2 = dataEncoding == null;
			if (flag2)
			{
				dataEncoding = Encoding.UTF8;
			}
			byte[] bytes = dataEncoding.GetBytes(data);
			bool flag3 = string.IsNullOrEmpty(fileName);
			if (flag3)
			{
				fileName = "file.txt";
			}
			bool flag4 = string.IsNullOrEmpty(this.content);
			if (flag4)
			{
				this.content = "text/plain; charset=" + dataEncoding.WebName;
			}
			this.Init(name, bytes, fileName, this.content);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003D3E File Offset: 0x00001F3E
		public MultipartFormFileSection(string data, Encoding dataEncoding, string fileName)
			: this(null, data, dataEncoding, fileName)
		{
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003D4C File Offset: 0x00001F4C
		public MultipartFormFileSection(string data, string fileName)
			: this(data, null, fileName)
		{
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00003D5C File Offset: 0x00001F5C
		public string sectionName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00003D74 File Offset: 0x00001F74
		public byte[] sectionData
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003D8C File Offset: 0x00001F8C
		public string fileName
		{
			get
			{
				return this.file;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00003DA4 File Offset: 0x00001FA4
		public string contentType
		{
			get
			{
				return this.content;
			}
		}

		// Token: 0x0400001F RID: 31
		private string name;

		// Token: 0x04000020 RID: 32
		private byte[] data;

		// Token: 0x04000021 RID: 33
		private string file;

		// Token: 0x04000022 RID: 34
		private string content;
	}
}
