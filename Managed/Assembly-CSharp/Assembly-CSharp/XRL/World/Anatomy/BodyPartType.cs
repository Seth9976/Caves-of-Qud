using System;

namespace XRL.World.Anatomy
{
	// Token: 0x02000BC6 RID: 3014
	public class BodyPartType
	{
		// Token: 0x0600790D RID: 30989 RVA: 0x002DB90C File Offset: 0x002D9B0C
		public BodyPartType(string Type, string Description = null, string Name = null)
		{
			this.FinalType = Type;
			this.Type = Type;
			this.Description = Description ?? this.Type;
			this.Name = Name ?? this.Type.ToLower();
		}

		// Token: 0x0600790E RID: 30990 RVA: 0x002DB96C File Offset: 0x002D9B6C
		public BodyPartType(BodyPartType Base)
		{
			this.Type = Base.Type;
			this.FinalType = Base.FinalType;
			this.Description = Base.Description;
			this.DescriptionPrefix = Base.DescriptionPrefix;
			this.Name = Base.Name;
			this.LimbBlueprintProperty = Base.LimbBlueprintProperty;
			this.LimbBlueprintDefault = Base.LimbBlueprintDefault;
			this.EquipSound = Base.EquipSound;
			this.UnequipSound = Base.UnequipSound;
			this.DefaultBehavior = Base.DefaultBehavior;
			this.UsuallyOn = Base.UsuallyOn;
			this.UsuallyOnVariant = Base.UsuallyOnVariant;
			this.ImpliedBy = Base.ImpliedBy;
			this.RequiresType = Base.RequiresType;
			this.Category = Base.Category;
			this.Laterality = Base.Laterality;
			this.ImpliedPer = Base.ImpliedPer;
			this.RequiresLaterality = Base.RequiresLaterality;
			this.Mobility = Base.Mobility;
			this.ChimeraWeight = Base.ChimeraWeight;
			this.Appendage = Base.Appendage;
			this.Integral = Base.Integral;
			this.Mortal = Base.Mortal;
			this.Abstract = Base.Abstract;
			this.Extrinsic = Base.Extrinsic;
			this.Plural = Base.Plural;
			this.Mass = Base.Mass;
			this.Contact = Base.Contact;
			this.IgnorePosition = Base.IgnorePosition;
			this.NoArmorAveraging = Base.NoArmorAveraging;
			this.Branching = Base.Branching;
		}

		// Token: 0x0600790F RID: 30991 RVA: 0x002DBB09 File Offset: 0x002D9D09
		public BodyPartType(BodyPartType Base, string Type)
			: this(Base)
		{
			this.Type = Type;
		}

		// Token: 0x06007910 RID: 30992 RVA: 0x002DBB1C File Offset: 0x002D9D1C
		public void ApplyTo(BodyPart Part)
		{
			Part.DefaultBehaviorBlueprint = this.DefaultBehavior;
			Part.Type = this.FinalType;
			Part.VariantType = this.Type;
			Part.Description = this.Description;
			Part.DescriptionPrefix = this.DescriptionPrefix;
			Part.Name = this.Name;
			if (this.RequiresType != null)
			{
				Part.RequiresType = this.RequiresType;
			}
			if (this.Category != null)
			{
				Part.Category = this.Category.Value;
			}
			if (this.Laterality != null)
			{
				Part.Laterality = this.Laterality.Value;
			}
			if (this.RequiresLaterality != null)
			{
				Part.RequiresLaterality = this.RequiresLaterality.Value;
			}
			if (this.Mobility != null)
			{
				Part.Mobility = this.Mobility.Value;
			}
			if (this.Appendage != null)
			{
				Part.Appendage = this.Appendage.Value;
			}
			if (this.Integral != null)
			{
				Part.Integral = this.Integral.Value;
			}
			if (this.Abstract != null)
			{
				Part.Abstract = this.Abstract.Value;
			}
			if (this.Mortal != null)
			{
				Part.Mortal = this.Mortal.Value;
			}
			if (this.Extrinsic != null)
			{
				Part.Extrinsic = this.Extrinsic.Value;
			}
			if (this.Plural != null)
			{
				Part.Plural = this.Plural.Value;
			}
			if (this.Mass != null)
			{
				Part.Mass = this.Mass.Value;
			}
			if (this.Contact != null)
			{
				Part.Contact = this.Contact.Value;
			}
			if (this.IgnorePosition != null)
			{
				Part.IgnorePosition = this.IgnorePosition.Value;
			}
		}

		// Token: 0x06007911 RID: 30993 RVA: 0x002DBD0C File Offset: 0x002D9F0C
		public int RequiresLateralityFor(int Laterality)
		{
			if (this.RequiresLaterality != null)
			{
				int? requiresLaterality = this.RequiresLaterality;
				int num = 65535;
				if (!((requiresLaterality.GetValueOrDefault() == num) & (requiresLaterality != null)))
				{
					return this.RequiresLaterality.Value | Laterality;
				}
				return Laterality;
			}
			else
			{
				if (Laterality != 0)
				{
					return Laterality;
				}
				return 65535;
			}
		}

		// Token: 0x06007912 RID: 30994 RVA: 0x002DBD64 File Offset: 0x002D9F64
		public int GetImpliedPer()
		{
			int? impliedPer = this.ImpliedPer;
			if (impliedPer == null)
			{
				return 2;
			}
			return impliedPer.GetValueOrDefault();
		}

		// Token: 0x04003745 RID: 14149
		public string BasedOn;

		// Token: 0x04003746 RID: 14150
		public string FinalType;

		// Token: 0x04003747 RID: 14151
		public string Type;

		// Token: 0x04003748 RID: 14152
		public string Description;

		// Token: 0x04003749 RID: 14153
		public string DescriptionPrefix;

		// Token: 0x0400374A RID: 14154
		public string Name;

		// Token: 0x0400374B RID: 14155
		public string DefaultBehavior;

		// Token: 0x0400374C RID: 14156
		public string UsuallyOn;

		// Token: 0x0400374D RID: 14157
		public string UsuallyOnVariant;

		// Token: 0x0400374E RID: 14158
		public string ImpliedBy;

		// Token: 0x0400374F RID: 14159
		public string RequiresType;

		// Token: 0x04003750 RID: 14160
		public string LimbBlueprintProperty = "SeveredLimbBlueprint";

		// Token: 0x04003751 RID: 14161
		public string LimbBlueprintDefault = "GenericLimb";

		// Token: 0x04003752 RID: 14162
		public string EquipSound;

		// Token: 0x04003753 RID: 14163
		public string UnequipSound;

		// Token: 0x04003754 RID: 14164
		public int? Category;

		// Token: 0x04003755 RID: 14165
		public int? Laterality;

		// Token: 0x04003756 RID: 14166
		public int? ImpliedPer;

		// Token: 0x04003757 RID: 14167
		public int? RequiresLaterality;

		// Token: 0x04003758 RID: 14168
		public int? Mobility;

		// Token: 0x04003759 RID: 14169
		public int? ChimeraWeight;

		// Token: 0x0400375A RID: 14170
		public bool? Appendage;

		// Token: 0x0400375B RID: 14171
		public bool? Integral;

		// Token: 0x0400375C RID: 14172
		public bool? Abstract;

		// Token: 0x0400375D RID: 14173
		public bool? Mortal;

		// Token: 0x0400375E RID: 14174
		public bool? Extrinsic;

		// Token: 0x0400375F RID: 14175
		public bool? Plural;

		// Token: 0x04003760 RID: 14176
		public bool? Mass;

		// Token: 0x04003761 RID: 14177
		public bool? Contact;

		// Token: 0x04003762 RID: 14178
		public bool? IgnorePosition;

		// Token: 0x04003763 RID: 14179
		public bool? NoArmorAveraging;

		// Token: 0x04003764 RID: 14180
		public int[] Branching;
	}
}
