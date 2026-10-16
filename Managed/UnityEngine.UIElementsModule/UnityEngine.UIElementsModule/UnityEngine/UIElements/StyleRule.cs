using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020004CB RID: 1227
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	[Serializable]
	internal class StyleRule
	{
		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x060025A6 RID: 9638 RVA: 0x000889CC File Offset: 0x00086BCC
		// (set) Token: 0x060025A7 RID: 9639 RVA: 0x000889E4 File Offset: 0x00086BE4
		public StyleProperty[] properties
		{
			get
			{
				return this.m_Properties;
			}
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			internal set
			{
				this.m_Properties = value;
			}
		}

		// Token: 0x040010BF RID: 4287
		[SerializeField]
		private StyleProperty[] m_Properties;

		// Token: 0x040010C0 RID: 4288
		[SerializeField]
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal int line;

		// Token: 0x040010C1 RID: 4289
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		[NonSerialized]
		internal int customPropertiesCount;
	}
}
