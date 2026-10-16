using System;
using System.Linq;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020004CC RID: 1228
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	[Serializable]
	internal class StyleSelector
	{
		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x060025A9 RID: 9641 RVA: 0x000889F0 File Offset: 0x00086BF0
		// (set) Token: 0x060025AA RID: 9642 RVA: 0x00088A08 File Offset: 0x00086C08
		public StyleSelectorPart[] parts
		{
			get
			{
				return this.m_Parts;
			}
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			internal set
			{
				this.m_Parts = value;
			}
		}

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x060025AB RID: 9643 RVA: 0x00088A14 File Offset: 0x00086C14
		// (set) Token: 0x060025AC RID: 9644 RVA: 0x00088A2C File Offset: 0x00086C2C
		public StyleSelectorRelationship previousRelationship
		{
			get
			{
				return this.m_PreviousRelationship;
			}
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			internal set
			{
				this.m_PreviousRelationship = value;
			}
		}

		// Token: 0x060025AD RID: 9645 RVA: 0x00088A38 File Offset: 0x00086C38
		public override string ToString()
		{
			return string.Join(", ", this.parts.Select<StyleSelectorPart, string>((StyleSelectorPart p) => p.ToString()).ToArray<string>());
		}

		// Token: 0x040010C2 RID: 4290
		[SerializeField]
		private StyleSelectorPart[] m_Parts;

		// Token: 0x040010C3 RID: 4291
		[SerializeField]
		private StyleSelectorRelationship m_PreviousRelationship;

		// Token: 0x040010C4 RID: 4292
		internal int pseudoStateMask = -1;

		// Token: 0x040010C5 RID: 4293
		internal int negatedPseudoStateMask = -1;
	}
}
