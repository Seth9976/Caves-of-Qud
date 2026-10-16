using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200066F RID: 1647
	internal class ShaderInfoStorageRGBA32 : ShaderInfoStorage<Color32>
	{
		// Token: 0x0600302F RID: 12335 RVA: 0x000B651B File Offset: 0x000B471B
		public ShaderInfoStorageRGBA32(int initialSize = 64, int maxSize = 4096)
			: base(TextureFormat.RGBA32, ShaderInfoStorageRGBA32.s_Convert, initialSize, maxSize)
		{
		}

		// Token: 0x04001789 RID: 6025
		private static readonly Func<Color, Color32> s_Convert = (Color c) => c;
	}
}
