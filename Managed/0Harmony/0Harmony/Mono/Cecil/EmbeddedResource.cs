using System;
using System.IO;

namespace Mono.Cecil
{
	// Token: 0x020001FD RID: 509
	internal sealed class EmbeddedResource : Resource
	{
		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000A5C RID: 2652 RVA: 0x00018941 File Offset: 0x00016B41
		public override ResourceType ResourceType
		{
			get
			{
				return ResourceType.Embedded;
			}
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x000268DC File Offset: 0x00024ADC
		public EmbeddedResource(string name, ManifestResourceAttributes attributes, byte[] data)
			: base(name, attributes)
		{
			this.data = data;
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x000268ED File Offset: 0x00024AED
		public EmbeddedResource(string name, ManifestResourceAttributes attributes, Stream stream)
			: base(name, attributes)
		{
			this.stream = stream;
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x000268FE File Offset: 0x00024AFE
		internal EmbeddedResource(string name, ManifestResourceAttributes attributes, uint offset, MetadataReader reader)
			: base(name, attributes)
		{
			this.offset = new uint?(offset);
			this.reader = reader;
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0002691C File Offset: 0x00024B1C
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

		// Token: 0x06000A61 RID: 2657 RVA: 0x0002697C File Offset: 0x00024B7C
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

		// Token: 0x06000A62 RID: 2658 RVA: 0x000269D8 File Offset: 0x00024BD8
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

		// Token: 0x040002F2 RID: 754
		private readonly MetadataReader reader;

		// Token: 0x040002F3 RID: 755
		private uint? offset;

		// Token: 0x040002F4 RID: 756
		private byte[] data;

		// Token: 0x040002F5 RID: 757
		private Stream stream;
	}
}
