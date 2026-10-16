using System;
using UnityEngine.TextCore.Text;

namespace UnityEngine.UIElements
{
	// Token: 0x020004EC RID: 1260
	public class PanelTextSettings : TextSettings
	{
		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x0600266E RID: 9838 RVA: 0x0008C5BC File Offset: 0x0008A7BC
		internal static PanelTextSettings defaultPanelTextSettings
		{
			get
			{
				PanelTextSettings.InitializeDefaultPanelTextSettingsIfNull();
				return PanelTextSettings.s_DefaultPanelTextSettings;
			}
		}

		// Token: 0x0600266F RID: 9839 RVA: 0x0008C5DC File Offset: 0x0008A7DC
		internal static void InitializeDefaultPanelTextSettingsIfNull()
		{
			bool flag = PanelTextSettings.s_DefaultPanelTextSettings == null;
			if (flag)
			{
				PanelTextSettings.s_DefaultPanelTextSettings = ScriptableObject.CreateInstance<PanelTextSettings>();
			}
		}

		// Token: 0x04001169 RID: 4457
		private static PanelTextSettings s_DefaultPanelTextSettings;
	}
}
