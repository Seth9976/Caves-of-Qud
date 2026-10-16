using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020004D6 RID: 1238
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	[Serializable]
	internal struct StyleValueHandle
	{
		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x060025EB RID: 9707 RVA: 0x000897D8 File Offset: 0x000879D8
		// (set) Token: 0x060025EC RID: 9708 RVA: 0x000897F0 File Offset: 0x000879F0
		public StyleValueType valueType
		{
			get
			{
				return this.m_ValueType;
			}
			internal set
			{
				this.m_ValueType = value;
			}
		}

		// Token: 0x060025ED RID: 9709 RVA: 0x000897FA File Offset: 0x000879FA
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal StyleValueHandle(int valueIndex, StyleValueType valueType)
		{
			this.valueIndex = valueIndex;
			this.m_ValueType = valueType;
		}

		// Token: 0x040010FB RID: 4347
		[SerializeField]
		private StyleValueType m_ValueType;

		// Token: 0x040010FC RID: 4348
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		[SerializeField]
		internal int valueIndex;
	}
}
