using System;
using System.IO;

namespace System.Resources
{
	// Token: 0x02000875 RID: 2165
	internal class Win32EncodedResource : Win32Resource
	{
		// Token: 0x0600481E RID: 18462 RVA: 0x000ED188 File Offset: 0x000EB388
		internal Win32EncodedResource(NameOrId type, NameOrId name, int language, byte[] data)
			: base(type, name, language)
		{
			this.data = data;
		}

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x0600481F RID: 18463 RVA: 0x000ED19B File Offset: 0x000EB39B
		public byte[] Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x06004820 RID: 18464 RVA: 0x000ED1A3 File Offset: 0x000EB3A3
		public override void WriteTo(Stream s)
		{
			s.Write(this.data, 0, this.data.Length);
		}

		// Token: 0x04002E2D RID: 11821
		private byte[] data;
	}
}
