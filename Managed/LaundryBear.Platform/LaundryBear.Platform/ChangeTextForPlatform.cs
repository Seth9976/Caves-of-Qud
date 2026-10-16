using System;
using UnityEngine.UI;

namespace LaundryBear.PlatformServices
{
	// Token: 0x0200001D RID: 29
	public class ChangeTextForPlatform : ChangeForPlatform<string>
	{
		// Token: 0x06000057 RID: 87 RVA: 0x000023B1 File Offset: 0x000005B1
		private void Start()
		{
			this.UpdateTextForPlatform();
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000023BC File Offset: 0x000005BC
		public void UpdateTextForPlatform()
		{
			Text text;
			string text2;
			if (base.TryGetComponent<Text>(out text) && base.GetPlatformSpecificObject(Utilities.GetCurrentPlatform(), out text2))
			{
				text.text = text2;
			}
		}
	}
}
