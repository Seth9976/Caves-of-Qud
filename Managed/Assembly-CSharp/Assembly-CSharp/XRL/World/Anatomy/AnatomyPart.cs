using System;
using System.Collections.Generic;

namespace XRL.World.Anatomy
{
	// Token: 0x02000BC2 RID: 3010
	[Serializable]
	public class AnatomyPart
	{
		// Token: 0x060078F7 RID: 30967 RVA: 0x002DAB64 File Offset: 0x002D8D64
		public AnatomyPart(BodyPartType Type)
		{
			this.Type = Type;
		}

		// Token: 0x060078F8 RID: 30968 RVA: 0x002DAB80 File Offset: 0x002D8D80
		public void ApplyTo(BodyPart parent)
		{
			if (parent == null)
			{
				string text = "called with null parent, type ";
				BodyPartType type = this.Type;
				MetricsManager.LogError(text + ((type != null) ? type.ToString() : null));
				return;
			}
			BodyPartType type2 = this.Type;
			string supportsDependent = this.SupportsDependent;
			string dependsOn = this.DependsOn;
			string requiresType = this.RequiresType;
			string defaultBehavior = this.DefaultBehavior;
			int? category = this.Category;
			int valueOrDefault = this.Laterality.GetValueOrDefault();
			string text2 = defaultBehavior;
			string text3 = supportsDependent;
			string text4 = dependsOn;
			string text5 = requiresType;
			string text6 = null;
			int? num = category;
			int? requiresLaterality = this.RequiresLaterality;
			int? mobility = this.Mobility;
			bool? integral = this.Integral;
			bool? mortal = this.Mortal;
			bool? @abstract = this.Abstract;
			bool? extrinsic = this.Extrinsic;
			bool? plural = this.Plural;
			bool? mass = this.Mass;
			bool? contact = this.Contact;
			bool? ignorePosition = this.IgnorePosition;
			BodyPart bodyPart = parent.AddPart(type2, valueOrDefault, text2, text3, text4, text5, text6, num, requiresLaterality, mobility, null, integral, mortal, @abstract, extrinsic, null, plural, mass, contact, ignorePosition, true);
			foreach (AnatomyPart anatomyPart in this.Subparts)
			{
				anatomyPart.ApplyTo(bodyPart);
			}
		}

		// Token: 0x04003712 RID: 14098
		public BodyPartType Type;

		// Token: 0x04003713 RID: 14099
		public string SupportsDependent;

		// Token: 0x04003714 RID: 14100
		public string DependsOn;

		// Token: 0x04003715 RID: 14101
		public string RequiresType;

		// Token: 0x04003716 RID: 14102
		public string DefaultBehavior;

		// Token: 0x04003717 RID: 14103
		public int? Category;

		// Token: 0x04003718 RID: 14104
		public int? Laterality;

		// Token: 0x04003719 RID: 14105
		public int? RequiresLaterality;

		// Token: 0x0400371A RID: 14106
		public int? Mobility;

		// Token: 0x0400371B RID: 14107
		public bool? Integral;

		// Token: 0x0400371C RID: 14108
		public bool? Mortal;

		// Token: 0x0400371D RID: 14109
		public bool? Abstract;

		// Token: 0x0400371E RID: 14110
		public bool? Extrinsic;

		// Token: 0x0400371F RID: 14111
		public bool? Plural;

		// Token: 0x04003720 RID: 14112
		public bool? Mass;

		// Token: 0x04003721 RID: 14113
		public bool? Contact;

		// Token: 0x04003722 RID: 14114
		public bool? IgnorePosition;

		// Token: 0x04003723 RID: 14115
		public List<AnatomyPart> Subparts = new List<AnatomyPart>(0);
	}
}
