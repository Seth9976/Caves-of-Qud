using System;

namespace XRL.World.Parts
{
	// Token: 0x020011EF RID: 4591
	[Serializable]
	public class FungusProperties : IPart
	{
		// Token: 0x0600B6C9 RID: 46793 RVA: 0x0042A5B0 File Offset: 0x004287B0
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == PooledEvent<AnimateEvent>.ID && this.Rooted) || ID == ApplyEffectEvent.ID || ID == CanApplyEffectEvent.ID || ID == EffectAppliedEvent.ID || ID == EquipperEquippedEvent.ID || ID == SingletonEvent<GetDebugInternalsEvent>.ID || ID == PooledEvent<GetKineticResistanceEvent>.ID || ID == PooledEvent<IsRootedInPlaceEvent>.ID || ID == LeftCellEvent.ID || (ID == ObjectCreatedEvent.ID && this.Rooted);
		}

		// Token: 0x0600B6CA RID: 46794 RVA: 0x0042A629 File Offset: 0x00428829
		public override bool HandleEvent(CanApplyEffectEvent E)
		{
			return this.Check(E) && base.HandleEvent(E);
		}

		// Token: 0x0600B6CB RID: 46795 RVA: 0x0042A63D File Offset: 0x0042883D
		public override bool HandleEvent(ApplyEffectEvent E)
		{
			return this.Check(E) && base.HandleEvent(E);
		}

		// Token: 0x0600B6CC RID: 46796 RVA: 0x0042A651 File Offset: 0x00428851
		public override bool HandleEvent(AnimateEvent E)
		{
			this.Rooted = false;
			return base.HandleEvent(E);
		}

		// Token: 0x0600B6CD RID: 46797 RVA: 0x0042A661 File Offset: 0x00428861
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			if (this.ParentObject.IsFlying)
			{
				this.Rooted = false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B6CE RID: 46798 RVA: 0x0042A680 File Offset: 0x00428880
		public override bool HandleEvent(EquipperEquippedEvent E)
		{
			if (this.Rooted)
			{
				GameObject item = E.Item;
				Armor armor = ((item != null) ? item.GetPart<Armor>() : null);
				if (armor != null && armor.WornOn == "Roots")
				{
					this.Rooted = false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B6CF RID: 46799 RVA: 0x0042A6CB File Offset: 0x004288CB
		public override bool HandleEvent(GetKineticResistanceEvent E)
		{
			if (this.Rooted)
			{
				E.LinearIncrease += 200;
				E.PercentageIncrease += 100;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B6D0 RID: 46800 RVA: 0x0042A6FD File Offset: 0x004288FD
		public override bool HandleEvent(IsRootedInPlaceEvent E)
		{
			return !this.Rooted && base.HandleEvent(E);
		}

		// Token: 0x0600B6D1 RID: 46801 RVA: 0x0042A710 File Offset: 0x00428910
		public override bool HandleEvent(LeftCellEvent E)
		{
			this.Rooted = false;
			return base.HandleEvent(E);
		}

		// Token: 0x0600B6D2 RID: 46802 RVA: 0x0042A720 File Offset: 0x00428920
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			if (this.ParentObject.IsPotentiallyMobile())
			{
				this.Rooted = false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B6D3 RID: 46803 RVA: 0x0042A73D File Offset: 0x0042893D
		public override bool HandleEvent(GetDebugInternalsEvent E)
		{
			E.AddEntry(this, "Rooted", this.Rooted);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B6D4 RID: 46804 RVA: 0x0042A758 File Offset: 0x00428958
		private bool Check(IEffectCheckEvent E)
		{
			if (E.Name == "CardiacArrest")
			{
				return false;
			}
			if (this.Rooted)
			{
				if (E.Name == "Prone")
				{
					return false;
				}
				if (E.Name == "Wading")
				{
					return false;
				}
				if (E.Name == "Swimming")
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040049B5 RID: 18869
		public bool Rooted = true;
	}
}
