using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000671 RID: 1649
	internal class ShaderInfoStorageRGBAFloat : ShaderInfoStorage<Color>
	{
		// Token: 0x06003034 RID: 12340 RVA: 0x000B6558 File Offset: 0x000B4758
		public ShaderInfoStorageRGBAFloat(int initialSize = 64, int maxSize = 4096)
			: base(TextureFormat.RGBAFloat, ShaderInfoStorageRGBAFloat.s_Convert, initialSize, maxSize)
		{
		}

		// Token: 0x0400178B RID: 6027
		private static readonly Func<Color, Color> s_Convert = (Color c) => c;
	}
}
