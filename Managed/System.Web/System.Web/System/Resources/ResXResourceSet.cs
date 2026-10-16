using System;
using System.Collections;
using System.IO;

namespace System.Resources
{
	// Token: 0x02000029 RID: 41
	public class ResXResourceSet : ResourceSet
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x00004B1E File Offset: 0x00002D1E
		public ResXResourceSet(Stream stream)
		{
			this.Reader = new ResXResourceReader(stream);
			this.Table = new Hashtable();
			this.ReadResources();
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004B43 File Offset: 0x00002D43
		public ResXResourceSet(string fileName)
		{
			this.Reader = new ResXResourceReader(fileName);
			this.Table = new Hashtable();
			this.ReadResources();
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004B68 File Offset: 0x00002D68
		public override Type GetDefaultReader()
		{
			return typeof(ResXResourceReader);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004B74 File Offset: 0x00002D74
		public override Type GetDefaultWriter()
		{
			return typeof(ResXResourceWriter);
		}
	}
}
