using System;
using AiUnity.Common.Serialization.ClassTypeReference;
using UnityEngine;

namespace AiUnity.Common.Attributes
{
	// Token: 0x020002E5 RID: 741
	public abstract class ClassTypeConstraintAttribute : PropertyAttribute
	{
		// Token: 0x17000380 RID: 896
		// (get) Token: 0x0600196C RID: 6508 RVA: 0x0008155A File Offset: 0x0007F75A
		// (set) Token: 0x0600196D RID: 6509 RVA: 0x00081562 File Offset: 0x0007F762
		public bool AllowAbstract
		{
			get
			{
				return this._allowAbstract;
			}
			set
			{
				this._allowAbstract = value;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x0600196E RID: 6510 RVA: 0x0008156B File Offset: 0x0007F76B
		// (set) Token: 0x0600196F RID: 6511 RVA: 0x00081573 File Offset: 0x0007F773
		public ClassGrouping Grouping
		{
			get
			{
				return this._grouping;
			}
			set
			{
				this._grouping = value;
			}
		}

		// Token: 0x06001970 RID: 6512 RVA: 0x0008157C File Offset: 0x0007F77C
		public ClassTypeConstraintAttribute(GUIContent labelContent)
		{
			this.LabelContent = labelContent;
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x00081592 File Offset: 0x0007F792
		public ClassTypeConstraintAttribute(string labelName, string tooltip = "")
		{
			this.LabelContent = new GUIContent(labelName, tooltip);
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x000815AE File Offset: 0x0007F7AE
		public virtual bool IsConstraintSatisfied(Type type)
		{
			return this.AllowAbstract || !type.IsAbstract;
		}

		// Token: 0x04000EE2 RID: 3810
		public GUIContent LabelContent;

		// Token: 0x04000EE3 RID: 3811
		private bool _allowAbstract;

		// Token: 0x04000EE4 RID: 3812
		private ClassGrouping _grouping = ClassGrouping.ByNamespaceFlat;
	}
}
