using System;
using XRL.World.Anatomy;

namespace XRL.World.Parts
{
	// Token: 0x02000F36 RID: 3894
	[Serializable]
	public class CyberneticsMotorizedTreads : IPart
	{
		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x06009B48 RID: 39752 RVA: 0x00385728 File Offset: 0x00383928
		public string AdditionsManagerID
		{
			get
			{
				return this.ParentObject.ID + "::MotorizedTreads::Add";
			}
		}

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x06009B49 RID: 39753 RVA: 0x0038573F File Offset: 0x0038393F
		public string ChangesManagerID
		{
			get
			{
				return this.ParentObject.ID + "::MotorizedTreads::Change";
			}
		}

		// Token: 0x06009B4A RID: 39754 RVA: 0x00385756 File Offset: 0x00383956
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009B4B RID: 39755 RVA: 0x00385759 File Offset: 0x00383959
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ImplantedEvent.ID || ID == UnimplantedEvent.ID || ID == PooledEvent<BeforeDismemberEvent>.ID;
		}

		// Token: 0x06009B4C RID: 39756 RVA: 0x00385780 File Offset: 0x00383980
		public override bool HandleEvent(ImplantedEvent E)
		{
			BodyPart part = E.Part;
			string text = "Tread";
			int num = 2;
			string text2 = null;
			string text3 = null;
			string text4 = null;
			string text5 = null;
			int? num2 = new int?(6);
			string additionsManagerID = this.AdditionsManagerID;
			int? num3 = num2;
			bool? flag = new bool?(true);
			BodyPart bodyPart = part.AddPartAt(text, num, text2, text3, text4, text5, additionsManagerID, num3, null, null, null, flag, null, null, null, null, null, null, null, null, "Tread", "Fungal Outcrop", true);
			BodyPart part2 = E.Part;
			BodyPart bodyPart2 = bodyPart;
			string text6 = "Tread";
			int num4 = 1;
			string text7 = null;
			string text8 = null;
			string text9 = null;
			string text10 = null;
			num2 = new int?(6);
			string additionsManagerID2 = this.AdditionsManagerID;
			int? num5 = num2;
			flag = new bool?(true);
			part2.AddPartAt(bodyPart2, text6, num4, text7, text8, text9, text10, additionsManagerID2, num5, null, null, null, flag, null, null, null, null, null, null, null, null, true);
			E.Part.Manager = this.ChangesManagerID;
			if (!E.ForDeepCopy)
			{
				this.PartName = E.Part.Name;
				this.PartDescription = E.Part.Description;
				this.PartDependsOn = E.Part.DependsOn;
				this.PartLaterality = E.Part.Laterality;
				this.PartMobility = E.Part.Mobility;
				this.PartIntegral = E.Part.Integral;
				this.PartPlural = E.Part.Plural;
				this.PartMass = E.Part.Mass;
				this.PartCategory = E.Part.Category;
				E.Part.Name = "lower body";
				E.Part.Description = "Lower Body";
				E.Part.DependsOn = null;
				E.Part.Laterality = 0;
				E.Part.Mobility = 0;
				E.Part.Integral = true;
				E.Part.Plural = false;
				E.Part.Mass = false;
				E.Part.Category = 6;
			}
			E.Implantee.WantToReequip();
			return base.HandleEvent(E);
		}

		// Token: 0x06009B4D RID: 39757 RVA: 0x00385A0C File Offset: 0x00383C0C
		public override bool HandleEvent(UnimplantedEvent E)
		{
			E.Implantee.RemoveBodyPartsByManager(this.AdditionsManagerID, true);
			BodyPart bodyPartByManager = E.Implantee.GetBodyPartByManager(this.ChangesManagerID, true);
			if (bodyPartByManager != null)
			{
				bodyPartByManager.Name = this.PartName;
				bodyPartByManager.Description = this.PartDescription;
				bodyPartByManager.DependsOn = this.PartDependsOn;
				bodyPartByManager.Laterality = this.PartLaterality;
				bodyPartByManager.Mobility = this.PartMobility;
				bodyPartByManager.Integral = this.PartIntegral;
				bodyPartByManager.Plural = this.PartPlural;
				bodyPartByManager.Mass = this.PartMass;
				bodyPartByManager.Category = this.PartCategory;
			}
			E.Implantee.WantToReequip();
			return base.HandleEvent(E);
		}

		// Token: 0x06009B4E RID: 39758 RVA: 0x00385AC0 File Offset: 0x00383CC0
		public override bool HandleEvent(BeforeDismemberEvent E)
		{
			if (E.IsObject)
			{
				BodyPart part = E.Part;
				if (((part != null) ? part.Cybernetics : null) == this.ParentObject)
				{
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009B4F RID: 39759 RVA: 0x00385AED File Offset: 0x00383CED
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0400406A RID: 16490
		public string PartName;

		// Token: 0x0400406B RID: 16491
		public string PartDescription;

		// Token: 0x0400406C RID: 16492
		public string PartDependsOn;

		// Token: 0x0400406D RID: 16493
		public int PartLaterality;

		// Token: 0x0400406E RID: 16494
		public int PartMobility;

		// Token: 0x0400406F RID: 16495
		public bool PartIntegral;

		// Token: 0x04004070 RID: 16496
		public bool PartPlural;

		// Token: 0x04004071 RID: 16497
		public bool PartMass;

		// Token: 0x04004072 RID: 16498
		public int PartCategory;
	}
}
