using System;
using System.ComponentModel;

namespace UnityEngine.VFX
{
	// Token: 0x0200000D RID: 13
	[Flags]
	internal enum VFXInstancingDisabledReason
	{
		// Token: 0x040000F9 RID: 249
		None = 0,
		// Token: 0x040000FA RID: 250
		[Description("A system is using indirect draw.")]
		IndirectDraw = 1,
		// Token: 0x040000FB RID: 251
		[Description("The effect is using output events.")]
		OutputEvent = 2,
		// Token: 0x040000FC RID: 252
		[Description("The effect is using GPU events.")]
		GPUEvent = 4,
		// Token: 0x040000FD RID: 253
		[Description("An Initialize node has Bounds Mode set to 'Automatic'.")]
		AutomaticBounds = 8,
		// Token: 0x040000FE RID: 254
		[Description("The effect contains a mesh output.")]
		MeshOutput = 16,
		// Token: 0x040000FF RID: 255
		[Description("The effect has exposed texture, mesh or graphics buffer properties.")]
		ExposedObject = 32,
		// Token: 0x04000100 RID: 256
		[Description("The effect uses Shader Keywords in particle output.")]
		ShaderKeyword = 64,
		// Token: 0x04000101 RID: 257
		[Description("Unknown reason.")]
		Unknown = -1
	}
}
