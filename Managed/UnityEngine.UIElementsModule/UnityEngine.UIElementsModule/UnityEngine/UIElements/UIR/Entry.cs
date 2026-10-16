using System;
using Unity.Collections;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200063E RID: 1598
	internal class Entry
	{
		// Token: 0x06002EE2 RID: 12002 RVA: 0x000AB191 File Offset: 0x000A9391
		public void Reset()
		{
			this.nextSibling = null;
			this.firstChild = null;
			this.lastChild = null;
			this.texture = null;
			this.material = null;
			this.gradientsOwner = null;
			this.flags = (EntryFlags)0;
			this.immediateCallback = null;
		}

		// Token: 0x0400160C RID: 5644
		public EntryType type;

		// Token: 0x0400160D RID: 5645
		public EntryFlags flags;

		// Token: 0x0400160E RID: 5646
		public NativeSlice<Vertex> vertices;

		// Token: 0x0400160F RID: 5647
		public NativeSlice<ushort> indices;

		// Token: 0x04001610 RID: 5648
		public Texture texture;

		// Token: 0x04001611 RID: 5649
		public float textScale;

		// Token: 0x04001612 RID: 5650
		public float fontSharpness;

		// Token: 0x04001613 RID: 5651
		public VectorImage gradientsOwner;

		// Token: 0x04001614 RID: 5652
		public Material material;

		// Token: 0x04001615 RID: 5653
		public Action immediateCallback;

		// Token: 0x04001616 RID: 5654
		public Entry nextSibling;

		// Token: 0x04001617 RID: 5655
		public Entry firstChild;

		// Token: 0x04001618 RID: 5656
		public Entry lastChild;
	}
}
