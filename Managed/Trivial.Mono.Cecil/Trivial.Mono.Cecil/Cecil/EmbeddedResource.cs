using System;
using System.IO;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200002B RID: 43
	public sealed class EmbeddedResource : Resource
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00002AF8 File Offset: 0x00000CF8
		public override ResourceType ResourceType
		{
			get
			{
				return ResourceType.Embedded;
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00004B5A File Offset: 0x00002D5A
		public EmbeddedResource(string name, ManifestResourceAttributes attributes, byte[] data)
			: base(name, attributes)
		{
			this.data = data;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00004B6B File Offset: 0x00002D6B
		public EmbeddedResource(string name, ManifestResourceAttributes attributes, Stream stream)
			: base(name, attributes)
		{
			this.stream = stream;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00004B7C File Offset: 0x00002D7C
		internal EmbeddedResource(string name, ManifestResourceAttributes attributes, uint offset, MetadataReader reader)
			: base(name, attributes)
		{
			this.offset = new uint?(offset);
			this.reader = reader;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00004B9C File Offset: 0x00002D9C
		public Stream GetResourceStream()
		{
			if (this.stream != null)
			{
				return this.stream;
			}
			if (this.data != null)
			{
				return new MemoryStream(this.data);
			}
			if (this.offset != null)
			{
				return new MemoryStream(this.reader.GetManagedResource(this.offset.Value));
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00004BFC File Offset: 0x00002DFC
		public byte[] GetResourceData()
		{
			if (this.stream != null)
			{
				return EmbeddedResource.ReadStream(this.stream);
			}
			if (this.data != null)
			{
				return this.data;
			}
			if (this.offset != null)
			{
				return this.reader.GetManagedResource(this.offset.Value);
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00004C58 File Offset: 0x00002E58
		private static byte[] ReadStream(Stream stream)
		{
			int num3;
			if (stream.CanSeek)
			{
				int num = (int)stream.Length;
				byte[] array = new byte[num];
				int num2 = 0;
				while ((num3 = stream.Read(array, num2, num - num2)) > 0)
				{
					num2 += num3;
				}
				return array;
			}
			byte[] array2 = new byte[8192];
			MemoryStream memoryStream = new MemoryStream();
			while ((num3 = stream.Read(array2, 0, array2.Length)) > 0)
			{
				memoryStream.Write(array2, 0, num3);
			}
			return memoryStream.ToArray();
		}

		// Token: 0x0400004C RID: 76
		private readonly MetadataReader reader;

		// Token: 0x0400004D RID: 77
		private uint? offset;

		// Token: 0x0400004E RID: 78
		private byte[] data;

		// Token: 0x0400004F RID: 79
		private Stream stream;
	}
}
