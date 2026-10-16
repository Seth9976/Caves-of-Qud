using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000618 RID: 1560
	internal struct SerializedCommand
	{
		// Token: 0x040014EA RID: 5354
		public SerializedCommandType type;

		// Token: 0x040014EB RID: 5355
		public IntPtr vertexBuffer;

		// Token: 0x040014EC RID: 5356
		public IntPtr indexBuffer;

		// Token: 0x040014ED RID: 5357
		public int firstRange;

		// Token: 0x040014EE RID: 5358
		public int rangeCount;

		// Token: 0x040014EF RID: 5359
		public int textureName;

		// Token: 0x040014F0 RID: 5360
		public Texture texture;

		// Token: 0x040014F1 RID: 5361
		public int gpuDataOffset;

		// Token: 0x040014F2 RID: 5362
		public Vector4 gpuData0;

		// Token: 0x040014F3 RID: 5363
		public Vector4 gpuData1;
	}
}
