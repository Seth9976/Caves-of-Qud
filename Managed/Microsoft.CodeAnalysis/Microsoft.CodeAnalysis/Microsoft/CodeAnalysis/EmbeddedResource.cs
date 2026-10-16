using System;
using System.Reflection;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200019C RID: 412
	internal struct EmbeddedResource
	{
		// Token: 0x06001082 RID: 4226 RVA: 0x00030245 File Offset: 0x0002E445
		internal EmbeddedResource(uint offset, ManifestResourceAttributes attributes, string name)
		{
			this.Offset = offset;
			this.Attributes = attributes;
			this.Name = name;
		}

		// Token: 0x04000516 RID: 1302
		public readonly uint Offset;

		// Token: 0x04000517 RID: 1303
		public readonly ManifestResourceAttributes Attributes;

		// Token: 0x04000518 RID: 1304
		public readonly string Name;
	}
}
