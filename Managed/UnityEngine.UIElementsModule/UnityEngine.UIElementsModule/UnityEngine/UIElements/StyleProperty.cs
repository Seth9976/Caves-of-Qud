using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020004CA RID: 1226
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	[Serializable]
	internal class StyleProperty
	{
		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x0600259F RID: 9631 RVA: 0x00088960 File Offset: 0x00086B60
		// (set) Token: 0x060025A0 RID: 9632 RVA: 0x00088978 File Offset: 0x00086B78
		public string name
		{
			get
			{
				return this.m_Name;
			}
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			internal set
			{
				this.m_Name = value;
			}
		}

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x060025A1 RID: 9633 RVA: 0x00088984 File Offset: 0x00086B84
		// (set) Token: 0x060025A2 RID: 9634 RVA: 0x0008899C File Offset: 0x00086B9C
		public int line
		{
			get
			{
				return this.m_Line;
			}
			internal set
			{
				this.m_Line = value;
			}
		}

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x060025A3 RID: 9635 RVA: 0x000889A8 File Offset: 0x00086BA8
		// (set) Token: 0x060025A4 RID: 9636 RVA: 0x000889C0 File Offset: 0x00086BC0
		public StyleValueHandle[] values
		{
			get
			{
				return this.m_Values;
			}
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			internal set
			{
				this.m_Values = value;
			}
		}

		// Token: 0x040010BA RID: 4282
		[SerializeField]
		private string m_Name;

		// Token: 0x040010BB RID: 4283
		[SerializeField]
		private int m_Line;

		// Token: 0x040010BC RID: 4284
		[SerializeField]
		private StyleValueHandle[] m_Values;

		// Token: 0x040010BD RID: 4285
		[NonSerialized]
		internal bool isCustomProperty;

		// Token: 0x040010BE RID: 4286
		[NonSerialized]
		internal bool requireVariableResolve;
	}
}
