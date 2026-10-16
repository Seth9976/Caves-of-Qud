using System;
using Microsoft.CodeAnalysis;

namespace RoslynCSharp.Compiler
{
	// Token: 0x02000008 RID: 8
	public struct AssemblyReferenceFromImage : IMetadataReferenceProvider
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002849 File Offset: 0x00000A49
		public byte[] Image
		{
			get
			{
				return this.image;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002854 File Offset: 0x00000A54
		public MetadataReference Reference
		{
			get
			{
				return MetadataReference.CreateFromImage(this.image, default(MetadataReferenceProperties), null, null);
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002877 File Offset: 0x00000A77
		public AssemblyReferenceFromImage(byte[] assemblyImage)
		{
			if (assemblyImage == null)
			{
				throw new ArgumentNullException("assemblyImage");
			}
			this.image = assemblyImage;
		}

		// Token: 0x0400000A RID: 10
		private byte[] image;
	}
}
