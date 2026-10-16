using System;

namespace XRL.World.Parts
{
	// Token: 0x020011FF RID: 4607
	[Serializable]
	public class PlantProperties : IPart
	{
		// Token: 0x0600B736 RID: 46902 RVA: 0x0042B64C File Offset: 0x0042984C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == PooledEvent<AnimateEvent>.ID && this.Rooted) || ID == ApplyEffectEvent.ID || ID == CanApplyEffectEvent.ID || ID == EffectAppliedEvent.ID || ID == EquipperEquippedEvent.ID || ID == SingletonEvent<GetDebugInternalsEvent>.ID || ID == PooledEvent<GetKineticResistanceEvent>.ID || ID == PooledEvent<IsRootedInPlaceEvent>.ID || ID == LeftCellEvent.ID || (ID == ObjectCreatedEvent.ID && this.Rooted);
		}

		// Token: 0x0600B737 RID: 46903 RVA: 0x0042B6C5 File Offset: 0x004298C5
		public override bool HandleEvent(CanApplyEffectEvent E)
		{
			return this.Check(E) && base.HandleEvent(E);
		}

		// Token: 0x0600B738 RID: 46904 RVA: 0x0042B6D9 File Offset: 0x004298D9
		public override bool HandleEvent(ApplyEffectEvent E)
		{
			return this.Check(E) && base.HandleEvent(E);
		}

		// Token: 0x0600B739 RID: 46905 RVA: 0x0042B6ED File Offset: 0x004298ED
		public override bool HandleEvent(AnimateEvent E)
		{
			this.Rooted = false;
			return base.HandleEvent(E);
		}

		// Token: 0x0600B73A RID: 46906 RVA: 0x0042B6FD File Offset: 0x004298FD
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			if (this.ParentObject.IsFlying)
			{
				this.Rooted = false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B73B RID: 46907 RVA: 0x0042B71C File Offset: 0x0042991C
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

		// Token: 0x0600B73C RID: 46908 RVA: 0x0042B767 File Offset: 0x00429967
		public override bool HandleEvent(GetKineticResistanceEvent E)
		{
			if (this.Rooted)
			{
				E.LinearIncrease += 300;
				E.PercentageIncrease += 200;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B73D RID: 46909 RVA: 0x0042B79C File Offset: 0x0042999C
		public override bool HandleEvent(IsRootedInPlaceEvent E)
		{
			return !this.Rooted && base.HandleEvent(E);
		}

		// Token: 0x0600B73E RID: 46910 RVA: 0x0042B7AF File Offset: 0x004299AF
		public override bool HandleEvent(LeftCellEvent E)
		{
			this.Rooted = false;
			return base.HandleEvent(E);
		}

		// Token: 0x0600B73F RID: 46911 RVA: 0x0042B7BF File Offset: 0x004299BF
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			if (this.ParentObject.IsPotentiallyMobile())
			{
				this.Rooted = false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B740 RID: 46912 RVA: 0x0042B7DC File Offset: 0x004299DC
		public override bool HandleEvent(GetDebugInternalsEvent E)
		{
			E.AddEntry(this, "Rooted", this.Rooted);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B741 RID: 46913 RVA: 0x0042B7F8 File Offset: 0x004299F8
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

		// Token: 0x040049C3 RID: 18883
		public bool Rooted = true;
	}
}
