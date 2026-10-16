using System;
using System.ComponentModel;

namespace UnityEngine
{
	// Token: 0x0200001A RID: 26
	[ExcludeFromPreset]
	[ExcludeFromObjectFactory]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("GUILayer has been removed.", true)]
	public sealed class GUILayer
	{
		// Token: 0x060001E8 RID: 488 RVA: 0x00008A99 File Offset: 0x00006C99
		[Obsolete("GUILayer has been removed.", true)]
		public GUIElement HitTest(Vector3 screenPosition)
		{
			throw new Exception("GUILayer has been removed from Unity.");
		}
	}
}
