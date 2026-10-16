using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020004C2 RID: 1218
	public enum OpenGLESVersion
	{
		// Token: 0x04000E8D RID: 3725
		None,
		// Token: 0x04000E8E RID: 3726
		[Obsolete("OpenGL ES 2.0 is no longer supported in Unity 2023.1")]
		OpenGLES20,
		// Token: 0x04000E8F RID: 3727
		OpenGLES30,
		// Token: 0x04000E90 RID: 3728
		OpenGLES31,
		// Token: 0x04000E91 RID: 3729
		OpenGLES31AEP,
		// Token: 0x04000E92 RID: 3730
		OpenGLES32
	}
}
