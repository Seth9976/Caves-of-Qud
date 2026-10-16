using System;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x02000325 RID: 805
	internal struct ColorPage
	{
		// Token: 0x0600172E RID: 5934 RVA: 0x00059EE4 File Offset: 0x000580E4
		public static ColorPage Init(RenderChain renderChain, BMPAlloc alloc)
		{
			bool flag = alloc.IsValid();
			return new ColorPage
			{
				isValid = flag,
				pageAndID = (flag ? renderChain.shaderInfoAllocator.ColorAllocToVertexData(alloc) : default(Color32))
			};
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x00059F30 File Offset: 0x00058130
		public MeshBuilderNative.NativeColorPage ToNativeColorPage()
		{
			return new MeshBuilderNative.NativeColorPage
			{
				isValid = (this.isValid ? 1 : 0),
				pageAndID = this.pageAndID
			};
		}

		// Token: 0x04000CBF RID: 3263
		public bool isValid;

		// Token: 0x04000CC0 RID: 3264
		public Color32 pageAndID;
	}
}
