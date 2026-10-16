using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020004E0 RID: 1248
	[HelpURL("UIE-tss")]
	[Serializable]
	public class ThemeStyleSheet : StyleSheet
	{
		// Token: 0x0600260C RID: 9740 RVA: 0x0008A1A3 File Offset: 0x000883A3
		internal override void OnEnable()
		{
			base.isDefaultStyleSheet = true;
			base.OnEnable();
		}
	}
}
