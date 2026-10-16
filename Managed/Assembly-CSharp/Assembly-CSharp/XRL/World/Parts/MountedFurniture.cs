using System;

namespace XRL.World.Parts
{
	// Token: 0x02000FAB RID: 4011
	[Serializable]
	public class MountedFurniture : IPart
	{
		// Token: 0x06009F8A RID: 40842 RVA: 0x0039FAA8 File Offset: 0x0039DCA8
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == AfterObjectCreatedEvent.ID && this.Mounted) || (ID == PooledEvent<AnimateEvent>.ID && this.Mounted) || ID == ApplyEffectEvent.ID || ID == CanApplyEffectEvent.ID || ID == EffectAppliedEvent.ID || ID == EquipperEquippedEvent.ID || ID == SingletonEvent<GetDebugInternalsEvent>.ID || ID == PooledEvent<GetKineticResistanceEvent>.ID || ID == PooledEvent<IsRootedInPlaceEvent>.ID || ID == LeftCellEvent.ID;
		}

		// Token: 0x06009F8B RID: 40843 RVA: 0x0039FB21 File Offset: 0x0039DD21
		public override bool HandleEvent(AnimateEvent E)
		{
			this.Mounted = false;
			return base.HandleEvent(E);
		}

		// Token: 0x06009F8C RID: 40844 RVA: 0x0039FB31 File Offset: 0x0039DD31
		public override bool HandleEvent(CanApplyEffectEvent E)
		{
			return this.Check(E) && base.HandleEvent(E);
		}

		// Token: 0x06009F8D RID: 40845 RVA: 0x0039FB45 File Offset: 0x0039DD45
		public override bool HandleEvent(ApplyEffectEvent E)
		{
			return this.Check(E) && base.HandleEvent(E);
		}

		// Token: 0x06009F8E RID: 40846 RVA: 0x0039FB59 File Offset: 0x0039DD59
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			if (this.ParentObject.IsFlying)
			{
				this.Mounted = false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009F8F RID: 40847 RVA: 0x0039FB78 File Offset: 0x0039DD78
		public override bool HandleEvent(EquipperEquippedEvent E)
		{
			if (this.Mounted)
			{
				GameObject item = E.Item;
				Armor armor = ((item != null) ? item.GetPart<Armor>() : null);
				if (armor != null && (armor.WornOn == "Feet" || armor.WornOn == "Roots"))
				{
					this.Mounted = false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009F90 RID: 40848 RVA: 0x0039FBD5 File Offset: 0x0039DDD5
		public override bool HandleEvent(GetKineticResistanceEvent E)
		{
			if (this.Mounted)
			{
				E.LinearIncrease += 300;
				E.PercentageIncrease += 300;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009F91 RID: 40849 RVA: 0x0039FC0A File Offset: 0x0039DE0A
		public override bool HandleEvent(IsRootedInPlaceEvent E)
		{
			return !this.Mounted && base.HandleEvent(E);
		}

		// Token: 0x06009F92 RID: 40850 RVA: 0x0039FC1D File Offset: 0x0039DE1D
		public override bool HandleEvent(LeftCellEvent E)
		{
			this.Mounted = false;
			return base.HandleEvent(E);
		}

		// Token: 0x06009F93 RID: 40851 RVA: 0x0039FC2D File Offset: 0x0039DE2D
		public override bool HandleEvent(AfterObjectCreatedEvent E)
		{
			if (this.ParentObject.IsPotentiallyMobile())
			{
				this.Mounted = false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009F94 RID: 40852 RVA: 0x0039FC4A File Offset: 0x0039DE4A
		public override bool HandleEvent(GetDebugInternalsEvent E)
		{
			E.AddEntry(this, "Mounted", this.Mounted);
			return base.HandleEvent(E);
		}

		// Token: 0x06009F95 RID: 40853 RVA: 0x0039FC68 File Offset: 0x0039DE68
		private bool Check(IEffectCheckEvent E)
		{
			if (this.Mounted)
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

		// Token: 0x04004235 RID: 16949
		public bool Mounted = true;
	}
}
