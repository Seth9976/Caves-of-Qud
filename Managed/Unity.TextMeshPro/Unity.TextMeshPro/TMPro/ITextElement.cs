using System;
using UnityEngine;
using UnityEngine.UI;

namespace TMPro
{
	// Token: 0x02000051 RID: 81
	public interface ITextElement
	{
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060003C4 RID: 964
		Material sharedMaterial { get; }

		// Token: 0x060003C5 RID: 965
		void Rebuild(CanvasUpdate update);

		// Token: 0x060003C6 RID: 966
		int GetInstanceID();
	}
}
