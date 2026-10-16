using System;
using System.Collections.Generic;
using System.Linq;
using XRL.World.Anatomy;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001414 RID: 5140
	[Serializable]
	public class Crystallinity : BaseDefaultEquipmentMutation
	{
		// Token: 0x0600C7AC RID: 51116 RVA: 0x0048B363 File Offset: 0x00489563
		public Crystallinity()
		{
			base.Type = "Physical";
		}

		// Token: 0x0600C7AD RID: 51117 RVA: 0x0048B388 File Offset: 0x00489588
		public override bool CanLevel()
		{
			return false;
		}

		// Token: 0x0600C7AE RID: 51118 RVA: 0x0048B38B File Offset: 0x0048958B
		public override bool GeneratesEquipment()
		{
			return false;
		}

		// Token: 0x0600C7AF RID: 51119 RVA: 0x0048B38E File Offset: 0x0048958E
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C7B0 RID: 51120 RVA: 0x0048B3A4 File Offset: 0x004895A4
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("jewels", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C7B1 RID: 51121 RVA: 0x0048B3C7 File Offset: 0x004895C7
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C7B2 RID: 51122 RVA: 0x0048B3D1 File Offset: 0x004895D1
		public override string GetDescription()
		{
			return "You are a crystalline being.";
		}

		// Token: 0x0600C7B3 RID: 51123 RVA: 0x0048B3D8 File Offset: 0x004895D8
		public override string GetLevelText(int Level)
		{
			return "" + "+4 AV\n" + "-50 Electrical Resistance\n" + "25% chance to refract light-based attacks\n" + "Effects that make non-biological clones of you produce twice as many.";
		}

		// Token: 0x0600C7B4 RID: 51124 RVA: 0x0048B407 File Offset: 0x00489607
		public override bool FireEvent(Event E)
		{
			return base.FireEvent(E);
		}

		// Token: 0x0600C7B5 RID: 51125 RVA: 0x0048B410 File Offset: 0x00489610
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C7B6 RID: 51126 RVA: 0x0048B41C File Offset: 0x0048961C
		public override bool Mutate(GameObject GO, int Level)
		{
			base.StatShifter.SetStatShift(GO, "ElectricResistance", -50, true);
			base.StatShifter.SetStatShift(GO, "AV", 4, true);
			GO.ModIntProperty("MentalCloneMultiplier", 2, false);
			if (!GO.HasPart<RefractLight>())
			{
				GO.AddPart<RefractLight>(true, false).Chance = 25;
				this.RefractAdded = true;
			}
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C7B7 RID: 51127 RVA: 0x0048B486 File Offset: 0x00489686
		public override bool Unmutate(GameObject GO)
		{
			base.StatShifter.RemoveStatShifts(GO);
			if (this.RefractAdded)
			{
				GO.RemovePart<RefractLight>();
			}
			GO.ModIntProperty("MentalCloneMultiplier", -2, false);
			return base.Unmutate(GO);
		}

		// Token: 0x0600C7B8 RID: 51128 RVA: 0x0048B4BC File Offset: 0x004896BC
		public override void OnRegenerateDefaultEquipment(Body body)
		{
			if (this.CreateObject)
			{
				List<BodyPart> list = (from p in body.GetParts()
					where p.VariantType == this.BodyPartType
					select p).ToList<BodyPart>();
				for (int i = 0; i < list.Count; i++)
				{
					list[i].DefaultBehavior = GameObject.Create("Crystalline Point");
					list[i].DefaultBehavior.SetStringProperty("TemporaryDefaultBehavior", "Crystallinity", false);
					list[i].DefaultBehavior.GetPart<MeleeWeapon>().BaseDamage = this.GetPointDamage(base.Level);
				}
			}
		}

		// Token: 0x0600C7B9 RID: 51129 RVA: 0x0048B558 File Offset: 0x00489758
		public string GetPointDamage(int Level)
		{
			if (Level <= 3)
			{
				return "1d2";
			}
			if (Level <= 6)
			{
				return "1d3";
			}
			if (Level <= 9)
			{
				return "1d4";
			}
			if (Level <= 12)
			{
				return "1d6";
			}
			if (Level <= 15)
			{
				return "1d8";
			}
			if (Level <= 18)
			{
				return "1d10";
			}
			return "1d12";
		}

		// Token: 0x04004E62 RID: 20066
		public string BodyPartType = "Quincunx";

		// Token: 0x04004E63 RID: 20067
		public bool CreateObject = true;

		// Token: 0x04004E64 RID: 20068
		public bool RefractAdded;
	}
}
