using System;

namespace UnityEngine
{
	// Token: 0x0200019E RID: 414
	public enum RenderingPath
	{
		// Token: 0x0400052A RID: 1322
		UsePlayerSettings = -1,
		// Token: 0x0400052B RID: 1323
		VertexLit,
		// Token: 0x0400052C RID: 1324
		Forward,
		// Token: 0x0400052D RID: 1325
		[Obsolete("DeferredLighting has been removed. Use DeferredShading, Forward or HDRP/URP instead.", false)]
		DeferredLighting,
		// Token: 0x0400052E RID: 1326
		DeferredShading
	}
}
