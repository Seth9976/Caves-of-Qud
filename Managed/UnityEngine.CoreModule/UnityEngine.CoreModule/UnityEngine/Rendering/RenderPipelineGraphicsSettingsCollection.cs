using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x020004EF RID: 1263
	[Serializable]
	public class RenderPipelineGraphicsSettingsCollection
	{
		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06003193 RID: 12691 RVA: 0x0005F57D File Offset: 0x0005D77D
		public List<IRenderPipelineGraphicsSettings> settingsList
		{
			get
			{
				return this.m_List;
			}
		}

		// Token: 0x04000F50 RID: 3920
		[SerializeReference]
		private List<IRenderPipelineGraphicsSettings> m_List = new List<IRenderPipelineGraphicsSettings>();
	}
}
