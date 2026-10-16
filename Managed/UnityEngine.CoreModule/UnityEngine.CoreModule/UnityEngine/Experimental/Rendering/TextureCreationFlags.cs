using System;
using UnityEngine.Internal;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x02000596 RID: 1430
	[Flags]
	public enum TextureCreationFlags
	{
		// Token: 0x040012EB RID: 4843
		None = 0,
		// Token: 0x040012EC RID: 4844
		MipChain = 1,
		// Token: 0x040012ED RID: 4845
		DontInitializePixels = 4,
		// Token: 0x040012EE RID: 4846
		Crunch = 64,
		// Token: 0x040012EF RID: 4847
		DontUploadUponCreate = 1024,
		// Token: 0x040012F0 RID: 4848
		[Obsolete("IgnoreMipmapLimit flag is no longer used since this is now the default behavior for all Texture shapes. Please provide mipmap limit information using a MipmapLimitDescriptor argument.", false)]
		[ExcludeFromDocs]
		IgnoreMipmapLimit = 2048
	}
}
