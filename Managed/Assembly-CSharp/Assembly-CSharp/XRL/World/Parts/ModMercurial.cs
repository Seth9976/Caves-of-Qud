using System;

namespace XRL.World.Parts
{
	// Token: 0x02001150 RID: 4432
	[Serializable]
	public class ModMercurial : IModification
	{
		// Token: 0x0600B003 RID: 45059 RVA: 0x0040D6EA File Offset: 0x0040B8EA
		public ModMercurial()
		{
			this.WorksOnEquipper = true;
			this.ChargeUse = 100;
			this.IsEMPSensitive = true;
			this.IsBootSensitive = true;
			base.IsTechScannable = true;
			this.NameForStatus = "ReactiveTeleporter";
		}

		// Token: 0x0600B004 RID: 45060 RVA: 0x0040D729 File Offset: 0x0040B929
		public ModMercurial(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B005 RID: 45061 RVA: 0x0040D73A File Offset: 0x0040B93A
		public ModMercurial(int Tier, int Chance)
			: this(Tier)
		{
			this.Chance = Chance;
		}

		// Token: 0x0600B006 RID: 45062 RVA: 0x0040D74A File Offset: 0x0040B94A
		public override void ApplyModification(GameObject Object)
		{
			base.IncreaseDifficultyAndComplexity(3, 2, null);
		}

		// Token: 0x0600B007 RID: 45063 RVA: 0x0040D755 File Offset: 0x0040B955
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EquippedEvent.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600B008 RID: 45064 RVA: 0x0040D783 File Offset: 0x0040B983
		public override bool HandleEvent(EquippedEvent E)
		{
			E.Actor.RegisterEvent(this, BeforeApplyDamageEvent.ID, 0, true);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B009 RID: 45065 RVA: 0x0040D79F File Offset: 0x0040B99F
		public override bool HandleEvent(UnequippedEvent E)
		{
			E.Actor.UnregisterEvent(this, BeforeApplyDamageEvent.ID);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B00A RID: 45066 RVA: 0x0040D7B9 File Offset: 0x0040B9B9
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("{{Y|mercurial}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B00B RID: 45067 RVA: 0x0040D7E3 File Offset: 0x0040B9E3
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(this.GetInstanceDescription(), base.GetEventSensitiveAddStatusSummary(E));
			return base.HandleEvent(E);
		}

		// Token: 0x0600B00C RID: 45068 RVA: 0x0040D808 File Offset: 0x0040BA08
		public override bool HandleEvent(BeforeApplyDamageEvent E)
		{
			if (base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				foreach (GameObject gameObject in this.GetActivePartSubjects())
				{
					if (!gameObject.OnWorldMap() && IComponent<GameObject>.CheckRealityDistortionUsability(gameObject, null, gameObject, this.ParentObject, null, null, 0) && this.Chance.in100() && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
					{
						gameObject.RandomTeleport(true, null, null, null, null, 0, 0, true, null, false, true, true, false);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B00D RID: 45069 RVA: 0x0040D8F4 File Offset: 0x0040BAF4
		public string GetInstanceDescription()
		{
			return "Mercurial: Teleports the user to safety upon taking damage" + ((this.Chance == 100) ? "" : (" (" + this.Chance.ToString() + "% chance)")) + ".";
		}

		// Token: 0x0400480A RID: 18442
		public int Chance = 100;
	}
}
