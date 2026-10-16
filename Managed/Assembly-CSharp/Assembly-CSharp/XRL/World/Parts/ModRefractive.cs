using System;
using XRL.Language;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02001161 RID: 4449
	[Serializable]
	public class ModRefractive : IModification
	{
		// Token: 0x0600B0E3 RID: 45283 RVA: 0x00410557 File Offset: 0x0040E757
		public ModRefractive()
		{
		}

		// Token: 0x0600B0E4 RID: 45284 RVA: 0x0041055F File Offset: 0x0040E75F
		public ModRefractive(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B0E5 RID: 45285 RVA: 0x00410568 File Offset: 0x0040E768
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B0E6 RID: 45286 RVA: 0x00410571 File Offset: 0x0040E771
		public override bool ModificationApplicable(GameObject Object)
		{
			return (Object.HasPart<Armor>() || Object.HasPart<Shield>()) && !Object.HasPart<RefractLight>();
		}

		// Token: 0x0600B0E7 RID: 45287 RVA: 0x0041058E File Offset: 0x0040E78E
		public override void ApplyModification(GameObject Object)
		{
			base.IncreaseDifficultyAndComplexity(1, 1, null);
		}

		// Token: 0x0600B0E8 RID: 45288 RVA: 0x00410599 File Offset: 0x0040E799
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EquippedEvent.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == GetShortDescriptionEvent.ID || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600B0E9 RID: 45289 RVA: 0x004105CF File Offset: 0x0040E7CF
		public override bool HandleEvent(EquippedEvent E)
		{
			E.Actor.RegisterPartEvent(this, "RefractLight");
			return base.HandleEvent(E);
		}

		// Token: 0x0600B0EA RID: 45290 RVA: 0x004105E9 File Offset: 0x0040E7E9
		public override bool HandleEvent(UnequippedEvent E)
		{
			E.Actor.UnregisterPartEvent(this, "RefractLight");
			return base.HandleEvent(E);
		}

		// Token: 0x0600B0EB RID: 45291 RVA: 0x00410603 File Offset: 0x0040E803
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("{{refractive|refractive}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B0EC RID: 45292 RVA: 0x0041062D File Offset: 0x0040E82D
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("glass", 2);
				E.Add("stars", 2);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B0ED RID: 45293 RVA: 0x0041065C File Offset: 0x0040E85C
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Base.Compound(this.GetPhysicalDescription());
			E.Postfix.AppendRules(this.GetInstanceDescription());
			return base.HandleEvent(E);
		}

		// Token: 0x0600B0EE RID: 45294 RVA: 0x00410689 File Offset: 0x0040E889
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x0600B0EF RID: 45295 RVA: 0x0041068C File Offset: 0x0040E88C
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("RefractLight");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B0F0 RID: 45296 RVA: 0x004106A4 File Offset: 0x0040E8A4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "RefractLight" && this.GetRefractChance().in100())
			{
				E.SetParameter("By", this.ParentObject);
				float num = (float)E.GetParameter("Angle");
				E.SetParameter("Direction", (int)num + 180 + 90 - Stat.Random(0, 180));
				return false;
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600B0F1 RID: 45297 RVA: 0x00410720 File Offset: 0x0040E920
		public int GetRefractChance()
		{
			Armor part = this.ParentObject.GetPart<Armor>();
			if (part != null)
			{
				if (part.WornOn == "Body")
				{
					return 40;
				}
				if (part.WornOn == "Back")
				{
					return 40;
				}
				if (part.WornOn == "Arm")
				{
					return 20;
				}
				return 10;
			}
			else
			{
				Shield part2 = this.ParentObject.GetPart<Shield>();
				if (part2 == null)
				{
					return 0;
				}
				if (part2.WornOn == "Hand")
				{
					return 30;
				}
				return 10;
			}
		}

		// Token: 0x0600B0F2 RID: 45298 RVA: 0x004107A6 File Offset: 0x0040E9A6
		public string GetPhysicalDescription()
		{
			return "";
		}

		// Token: 0x0600B0F3 RID: 45299 RVA: 0x004107AD File Offset: 0x0040E9AD
		public static string GetDescription(int Tier)
		{
			return "Refractive: This item has a chance to refract light-based attacks.";
		}

		// Token: 0x0600B0F4 RID: 45300 RVA: 0x004107B4 File Offset: 0x0040E9B4
		public string GetInstanceDescription()
		{
			return string.Concat(new string[]
			{
				"Refractive: This item has ",
				Grammar.AOrAnBeforeNumber(this.GetRefractChance()),
				" ",
				this.GetRefractChance().ToString(),
				"% chance to refract light-based attacks."
			});
		}
	}
}
