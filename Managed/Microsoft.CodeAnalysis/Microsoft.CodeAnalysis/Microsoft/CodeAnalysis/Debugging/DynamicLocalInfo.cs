using System;
using System.Collections.Immutable;

namespace Microsoft.CodeAnalysis.Debugging
{
	// Token: 0x0200035F RID: 863
	internal struct DynamicLocalInfo
	{
		// Token: 0x060024F3 RID: 9459 RVA: 0x000731A4 File Offset: 0x000713A4
		public DynamicLocalInfo(ImmutableArray<bool> flags, int slotId, string localName)
		{
			this.Flags = flags;
			this.SlotId = slotId;
			this.LocalName = localName;
		}

		// Token: 0x04000FD8 RID: 4056
		public readonly ImmutableArray<bool> Flags;

		// Token: 0x04000FD9 RID: 4057
		public readonly int SlotId;

		// Token: 0x04000FDA RID: 4058
		public readonly string LocalName;
	}
}
