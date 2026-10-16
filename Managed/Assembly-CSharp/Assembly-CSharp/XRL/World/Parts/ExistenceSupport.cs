using System;

namespace XRL.World.Parts
{
	// Token: 0x020011E8 RID: 4584
	[Serializable]
	public class ExistenceSupport : IPart
	{
		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x0600B691 RID: 46737 RVA: 0x00429AC2 File Offset: 0x00427CC2
		// (set) Token: 0x0600B692 RID: 46738 RVA: 0x00429AFD File Offset: 0x00427CFD
		public GameObject SupportedBy
		{
			get
			{
				if ((this.SupportedByCache == null || this.SupportedByCache.IsInvalid()) && !string.IsNullOrEmpty(this.SupportedById))
				{
					this.SupportedByCache = GameObject.FindByID(this.SupportedById);
				}
				return this.SupportedByCache;
			}
			set
			{
				this.SupportedById = ((value != null) ? value.ID : null);
				this.SupportedByCache = value;
			}
		}

		// Token: 0x0600B693 RID: 46739 RVA: 0x00429B18 File Offset: 0x00427D18
		public ExistenceSupport()
		{
		}

		// Token: 0x0600B694 RID: 46740 RVA: 0x00429B20 File Offset: 0x00427D20
		public ExistenceSupport(ExistenceSupport Source)
			: this()
		{
			this.SupportedById = Source.SupportedById;
			this.ValidateEveryTurn = Source.ValidateEveryTurn;
			this.SilentRemoval = Source.SilentRemoval;
		}

		// Token: 0x0600B695 RID: 46741 RVA: 0x00429B4C File Offset: 0x00427D4C
		public override void Attach()
		{
			this.ParentObject.Flags |= 8;
		}

		// Token: 0x0600B696 RID: 46742 RVA: 0x00429B61 File Offset: 0x00427D61
		public override void Remove()
		{
			if (!Temporary.CheckTemporary(this.ParentObject))
			{
				this.ParentObject.Flags &= -9;
			}
		}

		// Token: 0x0600B697 RID: 46743 RVA: 0x00429B84 File Offset: 0x00427D84
		public override bool SameAs(IPart Part)
		{
			ExistenceSupport existenceSupport = Part as ExistenceSupport;
			return !(existenceSupport.SupportedById != this.SupportedById) && existenceSupport.ValidateEveryTurn == this.ValidateEveryTurn && existenceSupport.SilentRemoval == this.SilentRemoval && base.SameAs(Part);
		}

		// Token: 0x0600B698 RID: 46744 RVA: 0x00429BD4 File Offset: 0x00427DD4
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AdjustValueEvent.ID || ID == PooledEvent<CanBeTradedEvent>.ID || ID == DerivationCreatedEvent.ID || ID == SingletonEvent<GetDebugInternalsEvent>.ID || ID == PooledEvent<ReplaceInContextEvent>.ID || ID == SynchronizeExistenceEvent.ID || ID == WasDerivedFromEvent.ID || ID == ZoneActivatedEvent.ID || ID == ZoneDeactivatedEvent.ID || ID == ZoneThawedEvent.ID;
		}

		// Token: 0x0600B699 RID: 46745 RVA: 0x00429C3D File Offset: 0x00427E3D
		public override bool HandleEvent(GetDebugInternalsEvent E)
		{
			E.AddEntry(this, "SupportedById", this.SupportedById);
			E.AddEntry(this, "ValidateEveryTurn", this.ValidateEveryTurn);
			E.AddEntry(this, "SilentRemoval", this.SilentRemoval);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B69A RID: 46746 RVA: 0x00429C7C File Offset: 0x00427E7C
		public override bool HandleEvent(WasDerivedFromEvent E)
		{
			E.ApplyToEach(delegate(GameObject obj)
			{
				obj.RemovePart<ExistenceSupport>();
			});
			E.ApplyToEach(delegate(GameObject obj)
			{
				obj.AddPart<ExistenceSupport>(new ExistenceSupport(this), true, false);
			});
			return base.HandleEvent(E);
		}

		// Token: 0x0600B69B RID: 46747 RVA: 0x00429CBC File Offset: 0x00427EBC
		public override bool HandleEvent(DerivationCreatedEvent E)
		{
			if (!E.Original.HasPart<ExistenceSupport>())
			{
				E.Object.RemovePart(this);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B69C RID: 46748 RVA: 0x00429CDE File Offset: 0x00427EDE
		public override bool HandleEvent(AdjustValueEvent E)
		{
			E.AdjustValue(0.0);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B69D RID: 46749 RVA: 0x00429CF6 File Offset: 0x00427EF6
		public override bool HandleEvent(CanBeTradedEvent E)
		{
			return false;
		}

		// Token: 0x0600B69E RID: 46750 RVA: 0x00429CF9 File Offset: 0x00427EF9
		public override bool HandleEvent(SynchronizeExistenceEvent E)
		{
			this.CheckSupport();
			return base.HandleEvent(E);
		}

		// Token: 0x0600B69F RID: 46751 RVA: 0x00429D08 File Offset: 0x00427F08
		public override bool HandleEvent(ZoneThawedEvent E)
		{
			this.CheckSupport();
			return base.HandleEvent(E);
		}

		// Token: 0x0600B6A0 RID: 46752 RVA: 0x00429D17 File Offset: 0x00427F17
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			this.CheckSupport();
			return base.HandleEvent(E);
		}

		// Token: 0x0600B6A1 RID: 46753 RVA: 0x00429D26 File Offset: 0x00427F26
		public override bool HandleEvent(ZoneDeactivatedEvent E)
		{
			this.CheckSupport();
			return base.HandleEvent(E);
		}

		// Token: 0x0600B6A2 RID: 46754 RVA: 0x00429D35 File Offset: 0x00427F35
		public override bool HandleEvent(ReplaceInContextEvent E)
		{
			E.Replacement.RemovePart<ExistenceSupport>();
			E.Replacement.AddPart<ExistenceSupport>(new ExistenceSupport(this), true, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B6A3 RID: 46755 RVA: 0x00429D5E File Offset: 0x00427F5E
		public override bool WantTurnTick()
		{
			return this.ValidateEveryTurn;
		}

		// Token: 0x0600B6A4 RID: 46756 RVA: 0x00429D66 File Offset: 0x00427F66
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.CheckSupport();
		}

		// Token: 0x0600B6A5 RID: 46757 RVA: 0x00429D6E File Offset: 0x00427F6E
		public void CheckSupport()
		{
			if (!this.IsSupported())
			{
				this.Unsupported(false);
			}
		}

		// Token: 0x0600B6A6 RID: 46758 RVA: 0x00429D7F File Offset: 0x00427F7F
		public bool IsSupported()
		{
			return CheckExistenceSupportEvent.Check(this.SupportedBy, this.ParentObject);
		}

		// Token: 0x0600B6A7 RID: 46759 RVA: 0x00429D94 File Offset: 0x00427F94
		public void Unsupported(bool Silent = false)
		{
			if (this.SilentRemoval)
			{
				Silent = true;
			}
			Temporary temporary;
			if (this.ParentObject.TryGetPart<Temporary>(out temporary))
			{
				temporary.Expire(Silent);
				return;
			}
			this.ParentObject.RemoveContents(Silent, true);
			if (!Silent)
			{
				base.DidX("disappear", null, null, null, null, null, null, false, false, null, null, true, false, false, false, false, null);
			}
			this.ParentObject.Obliterate(null, Silent, null);
		}

		// Token: 0x040049A6 RID: 18854
		public string SupportedById;

		// Token: 0x040049A7 RID: 18855
		public bool ValidateEveryTurn;

		// Token: 0x040049A8 RID: 18856
		public bool SilentRemoval;

		// Token: 0x040049A9 RID: 18857
		[NonSerialized]
		private GameObject SupportedByCache;
	}
}
