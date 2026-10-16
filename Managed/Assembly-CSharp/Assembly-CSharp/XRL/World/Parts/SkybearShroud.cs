using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x020010A0 RID: 4256
	[Serializable]
	public class SkybearShroud : IPart
	{
		// Token: 0x0600A945 RID: 43333 RVA: 0x003DDFCE File Offset: 0x003DC1CE
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600A946 RID: 43334 RVA: 0x003DDFD1 File Offset: 0x003DC1D1
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x0600A947 RID: 43335 RVA: 0x003DDFD4 File Offset: 0x003DC1D4
		public int GetCooldown()
		{
			return 100;
		}

		// Token: 0x0600A948 RID: 43336 RVA: 0x003DDFD8 File Offset: 0x003DC1D8
		public int GetDuration()
		{
			return 10;
		}

		// Token: 0x0600A949 RID: 43337 RVA: 0x003DDFDC File Offset: 0x003DC1DC
		public void CollectStats(Templates.StatCollector stats)
		{
			stats.Set("Duration", this.GetDuration(), false, 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, this.ParentObject.Equipped), this.GetCooldown(), 0);
		}

		// Token: 0x0600A94A RID: 43338 RVA: 0x003DE018 File Offset: 0x003DC218
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetMovementAbilityListEvent.ID || ID == AIGetOffensiveAbilityListEvent.ID || ID == EquippedEvent.ID || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID || ID == UnequippedEvent.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x0600A94B RID: 43339 RVA: 0x003DE069 File Offset: 0x003DC269
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), this.ParentObject.Equipped);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A94C RID: 43340 RVA: 0x003DE098 File Offset: 0x003DC298
		public override bool HandleEvent(AIGetMovementAbilityListEvent E)
		{
			if (E.Actor == this.ParentObject.Equipped && E.Distance - E.StandoffDistance * 2 >= 10 && E.Actor.IsActivatedAbilityAIUsable(this.ActivatedAbilityID))
			{
				E.Add("ActivateSkyshroud", 1, this.ParentObject, true, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A94D RID: 43341 RVA: 0x003DE0FC File Offset: 0x003DC2FC
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Actor == this.ParentObject.Equipped && E.Actor.IsActivatedAbilityAIUsable(this.ActivatedAbilityID))
			{
				E.Add("ActivateSkyshroud", 1, this.ParentObject, true, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A94E RID: 43342 RVA: 0x003DE14C File Offset: 0x003DC34C
		public override bool HandleEvent(EquippedEvent E)
		{
			if (this.ParentObject.IsEquippedProperly(null))
			{
				E.Actor.RegisterPartEvent(this, "ActivateSkyshroud");
				this.ActivatedAbilityID = E.Actor.AddActivatedAbility("Activate Flume-Flier", "ActivateSkyshroud", "Items", null, "\a", null, false, false, false, false, false, false, false, false, true, true, false, false, -1, null, null, null, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A94F RID: 43343 RVA: 0x003DE1B8 File Offset: 0x003DC3B8
		public override bool HandleEvent(UnequippedEvent E)
		{
			E.Actor.RemoveEffect<Dashing>();
			E.Actor.UnregisterPartEvent(this, "ActivateSkyshroud");
			E.Actor.RemoveActivatedAbility(ref this.ActivatedAbilityID);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A950 RID: 43344 RVA: 0x003DE1F0 File Offset: 0x003DC3F0
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			GameObject equipped = this.ParentObject.Equipped;
			if (equipped != null && equipped.IsActivatedAbilityUsable(this.ActivatedAbilityID))
			{
				E.AddAction("Activate", "activate", "ActivateSkyshroud", null, 'a', false, 0, 0, false, false, false, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A951 RID: 43345 RVA: 0x003DE244 File Offset: 0x003DC444
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "ActivateSkyshroud")
			{
				this.ActivateSkyshroud();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A952 RID: 43346 RVA: 0x003DE266 File Offset: 0x003DC466
		public override bool FireEvent(Event E)
		{
			return (!(E.ID == "ActivateSkyshroud") || this.ActivateSkyshroud()) && base.FireEvent(E);
		}

		// Token: 0x0600A953 RID: 43347 RVA: 0x003DE28C File Offset: 0x003DC48C
		private bool ActivateSkyshroud()
		{
			GameObject equipped = this.ParentObject.Equipped;
			if (equipped == null)
			{
				return false;
			}
			if (!equipped.IsActivatedAbilityUsable(this.ActivatedAbilityID))
			{
				return false;
			}
			if (!equipped.ApplyEffect(new Dashing(this.GetDuration()), null))
			{
				return false;
			}
			IComponent<GameObject>.XDidY(equipped, "start", "dashing in a plume of flame and smoke", "!", null, null, equipped, null, false, false, null, null, false, false, false, false, false, null);
			equipped.CooldownActivatedAbility(this.ActivatedAbilityID, this.GetCooldown(), null, false);
			equipped.PlayWorldSound("Sounds/Interact/sfx_interact_jetpack_activate", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			return true;
		}

		// Token: 0x040045DB RID: 17883
		public Guid ActivatedAbilityID = Guid.Empty;
	}
}
