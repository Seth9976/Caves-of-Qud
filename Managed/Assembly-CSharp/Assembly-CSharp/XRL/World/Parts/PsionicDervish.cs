using System;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x02000E88 RID: 3720
	[Serializable]
	public class PsionicDervish : IPart
	{
		// Token: 0x060095E3 RID: 38371 RVA: 0x00364269 File Offset: 0x00362469
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x060095E4 RID: 38372 RVA: 0x0036426C File Offset: 0x0036246C
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x060095E5 RID: 38373 RVA: 0x0036426F File Offset: 0x0036246F
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ObjectCreated");
			base.Register(Object, Registrar);
		}

		// Token: 0x060095E6 RID: 38374 RVA: 0x00364284 File Offset: 0x00362484
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ObjectCreated")
			{
				if (this.Tier == -1)
				{
					this.Tier = this.ParentObject.GetTier();
				}
				int num = 0;
				if (this.Tier <= 4)
				{
					num = 1;
				}
				else if (this.Tier <= 6)
				{
					num = 2;
				}
				else if (this.Tier >= 8)
				{
					num = 3;
				}
				string randomElement = new string[] { "LongBlade", "ShortBlade", "Axe", "Cudgel" }.GetRandomElement<string>(null);
				int num2 = XRL.World.Capabilities.Tier.Constrain(this.Tier + 1);
				string text = randomElement;
				if (text == "ShortBlade")
				{
					text = "Dagger";
				}
				GameObject gameObject = GameObject.Create(PopulationManager.RollOneFrom("DynamicInheritsTable:Base" + text + ":Tier" + num2.ToString(), null, null).Blueprint);
				if (!gameObject.HasPart<ModPsionic>())
				{
					gameObject.ApplyModification(new ModPsionic(), true, null, false);
				}
				this.ParentObject.ReceiveObject(gameObject, false, null);
				this.ParentObject.Brain.PerformEquip(false, true, null);
				int num3 = 0;
				if (num == 1)
				{
					num3 = 2;
				}
				if (num == 2)
				{
					num3 = 4;
				}
				if (num == 3)
				{
					num3 = 6;
				}
				string text2 = "";
				if (randomElement == "LongBlade")
				{
					text2 = "SkillPowers_LongBlades";
				}
				if (randomElement == "ShortBlade")
				{
					text2 = "SkillPowers_ShortBlades";
				}
				if (randomElement == "Axe")
				{
					text2 = "SkillPowers_Axe";
				}
				if (randomElement == "Cudgel")
				{
					text2 = "SkillPowers_Cudgel";
				}
				int num4 = 20;
				while (num3 > 0 && num4 > 0)
				{
					string blueprint = PopulationManager.RollOneFrom(text2, null, null).Blueprint;
					if (!this.ParentObject.HasPart(blueprint))
					{
						this.ParentObject.AddSkill(blueprint, null, null);
						num3--;
					}
					num4--;
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003DFD RID: 15869
		public int Tier = -1;
	}
}
