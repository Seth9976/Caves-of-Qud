using System;

namespace XRL.World.Parts
{
	// Token: 0x02001170 RID: 4464
	[Serializable]
	public class ModSturdy : IModification
	{
		// Token: 0x0600B198 RID: 45464 RVA: 0x00412060 File Offset: 0x00410260
		public ModSturdy()
		{
		}

		// Token: 0x0600B199 RID: 45465 RVA: 0x00412068 File Offset: 0x00410268
		public ModSturdy(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B19A RID: 45466 RVA: 0x00412071 File Offset: 0x00410271
		public override void Configure()
		{
			this.WorksOnSelf = true;
			this.NameForStatus = "StructuralSupport";
		}

		// Token: 0x0600B19B RID: 45467 RVA: 0x00412085 File Offset: 0x00410285
		public override void ApplyModification(GameObject Object)
		{
			Object.RemovePart<BreakableInMelee>();
			base.StatShifter.SetStatShift(Object, "Hitpoints", Object.hitpoints / 4, true);
		}

		// Token: 0x0600B19C RID: 45468 RVA: 0x004120A9 File Offset: 0x004102A9
		public override void Remove()
		{
			base.StatShifter.RemoveStatShifts(this.ParentObject);
			base.Remove();
		}

		// Token: 0x0600B19D RID: 45469 RVA: 0x004120C2 File Offset: 0x004102C2
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B19E RID: 45470 RVA: 0x004120E0 File Offset: 0x004102E0
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName && !E.Object.IsNatural())
			{
				E.AddAdjective("sturdy", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B19F RID: 45471 RVA: 0x00412117 File Offset: 0x00410317
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(ModSturdy.GetDescription(this.Tier, this.ParentObject));
			return base.HandleEvent(E);
		}

		// Token: 0x0600B1A0 RID: 45472 RVA: 0x0041213D File Offset: 0x0041033D
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B1A1 RID: 45473 RVA: 0x00412140 File Offset: 0x00410340
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ApplyBroken");
			Registrar.Register("ApplyShatterArmor");
			Registrar.Register("ApplyShatteredArmor");
			Registrar.Register("CanApplyBroken");
			Registrar.Register("CanApplyShatterArmor");
			Registrar.Register("CanApplyShatteredArmor");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B1A2 RID: 45474 RVA: 0x00412198 File Offset: 0x00410398
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CanApplyBroken" || E.ID == "CanApplyShatteredArmor" || E.ID == "CanApplyShatterArmor")
			{
				if (base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					return false;
				}
			}
			else if ((E.ID == "ApplyBroken" || E.ID == "ApplyShatteredArmor" || E.ID == "ApplyShatterArmor") && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				return false;
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600B1A3 RID: 45475 RVA: 0x0041226C File Offset: 0x0041046C
		public static string GetDescription(int Tier)
		{
			return "Sturdy: This item cannot break or crack, though it can still be destroyed.";
		}

		// Token: 0x0600B1A4 RID: 45476 RVA: 0x00412273 File Offset: 0x00410473
		public static string GetDescription(int Tier, GameObject obj)
		{
			if (obj.HasPart<NoDamage>())
			{
				return "Sturdy: This item cannot break or crack.";
			}
			return "Sturdy: This item cannot break or crack, though it can still be destroyed.";
		}
	}
}
