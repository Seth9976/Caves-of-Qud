using System;

namespace XRL.CharacterBuilds.Qud
{
	// Token: 0x020005A6 RID: 1446
	public class QudGamemodeModuleData : AbstractEmbarkBuilderModuleData
	{
		// Token: 0x06003DBF RID: 15807 RVA: 0x0012F1DB File Offset: 0x0012D3DB
		public bool DoesSupportCharacterSelection()
		{
			return this.Mode != "Daily";
		}

		// Token: 0x04001EAB RID: 7851
		public string Mode;
	}
}
