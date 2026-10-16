using System;
using System.ComponentModel;

namespace UnityEngine.UI
{
	// Token: 0x02000016 RID: 22
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("Not supported anymore.", true)]
	public interface IMask
	{
		// Token: 0x06000110 RID: 272
		bool Enabled();

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000111 RID: 273
		RectTransform rectTransform { get; }
	}
}
