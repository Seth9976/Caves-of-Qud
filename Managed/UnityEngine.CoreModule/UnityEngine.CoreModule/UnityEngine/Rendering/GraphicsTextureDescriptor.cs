using System;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x020004CB RID: 1227
	[NativeHeader("Runtime/Export/Graphics/GraphicsTexture.bindings.h")]
	[UsedByNativeCode]
	[Serializable]
	public struct GraphicsTextureDescriptor
	{
		// Token: 0x06002E24 RID: 11812 RVA: 0x000577EC File Offset: 0x000559EC
		[ExcludeFromDocs]
		public GraphicsTextureDescriptor()
		{
			this = new GraphicsTextureDescriptor(0, 0, 1, 0, GraphicsFormat.None, TextureDimension.None, 0, 1, GraphicsTextureDescriptorFlags.None);
		}

		// Token: 0x06002E25 RID: 11813 RVA: 0x0005780C File Offset: 0x00055A0C
		internal GraphicsTextureDescriptor(int width, int height, int depth, int arrayLength, GraphicsFormat format, TextureDimension dimension, int mipCount = 0, int numSamples = 1, GraphicsTextureDescriptorFlags flags = GraphicsTextureDescriptorFlags.None)
		{
			this.width = width;
			this.height = height;
			this.depth = depth;
			this.arrayLength = arrayLength;
			this.format = format;
			this.dimension = dimension;
			this.mipCount = mipCount;
			this.numSamples = numSamples;
			this.flags = flags;
		}

		// Token: 0x04000EBB RID: 3771
		public int width;

		// Token: 0x04000EBC RID: 3772
		public int height;

		// Token: 0x04000EBD RID: 3773
		public int depth;

		// Token: 0x04000EBE RID: 3774
		public int arrayLength;

		// Token: 0x04000EBF RID: 3775
		public GraphicsFormat format;

		// Token: 0x04000EC0 RID: 3776
		public TextureDimension dimension;

		// Token: 0x04000EC1 RID: 3777
		public int mipCount;

		// Token: 0x04000EC2 RID: 3778
		public int numSamples;

		// Token: 0x04000EC3 RID: 3779
		public GraphicsTextureDescriptorFlags flags;
	}
}
