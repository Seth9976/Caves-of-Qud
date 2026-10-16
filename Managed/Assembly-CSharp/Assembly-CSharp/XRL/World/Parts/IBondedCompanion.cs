using System;
using XRL.World.AI;

namespace XRL.World.Parts
{
	// Token: 0x02000ECD RID: 3789
	[Serializable]
	public abstract class IBondedCompanion : IPart
	{
		// Token: 0x060097C8 RID: 38856 RVA: 0x00371A3C File Offset: 0x0036FC3C
		public IBondedCompanion(GameObject CompanionOf = null, string Faction = null, string Honorific = null, string Title = null, string ConversationID = null, bool StripGear = false)
		{
			this.CompanionOf = CompanionOf;
			this.Faction = Faction;
			this.Honorific = Honorific;
			this.Title = Title;
			this.ConversationID = ConversationID;
			this.StripGear = StripGear;
		}

		// Token: 0x060097C9 RID: 38857 RVA: 0x00371A71 File Offset: 0x0036FC71
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x060097CA RID: 38858 RVA: 0x00371A74 File Offset: 0x0036FC74
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID;
		}

		// Token: 0x060097CB RID: 38859 RVA: 0x00371A8C File Offset: 0x0036FC8C
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (!E.WithoutTitles && GameObject.Validate(ref this.CompanionOf) && E.Understood())
			{
				if (!this.Honorific.IsNullOrEmpty())
				{
					E.AddHonorific(this.Honorific, 0);
				}
				if (!this.Title.IsNullOrEmpty())
				{
					E.AddTitle(this.Title, 0);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060097CC RID: 38860 RVA: 0x00371AF4 File Offset: 0x0036FCF4
		public override void Initialize()
		{
			base.Initialize();
			if (this.ParentObject.Brain != null)
			{
				if (this.CompanionOf != null)
				{
					this.ParentObject.Brain.PartyLeader = this.CompanionOf;
					this.ParentObject.Brain.TakeAllegiance(this.CompanionOf, this.GetAllyReasonFor(this.CompanionOf));
				}
				else if (!this.Faction.IsNullOrEmpty())
				{
					this.ParentObject.Brain.Factions = this.Faction + "-100";
					this.ParentObject.FireEvent("FactionsAdded");
				}
			}
			if (!this.ConversationID.IsNullOrEmpty())
			{
				this.ParentObject.RequirePart<ConversationScript>(false).ConversationID = this.ConversationID;
			}
			if (this.StripGear)
			{
				this.ParentObject.StripOffGear();
			}
			this.ParentObject.FireEvent("VillageInit");
			this.InitializeBondedCompanion();
		}

		// Token: 0x060097CD RID: 38861 RVA: 0x00371BE5 File Offset: 0x0036FDE5
		public override void FinalizeRead(SerializationReader Reader)
		{
			base.FinalizeRead(Reader);
			GameObject.Validate(ref this.CompanionOf);
		}

		// Token: 0x060097CE RID: 38862 RVA: 0x00371BFA File Offset: 0x0036FDFA
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x060097CF RID: 38863 RVA: 0x00371BFD File Offset: 0x0036FDFD
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CanAIDoIndependentBehavior");
			base.Register(Object, Registrar);
		}

		// Token: 0x060097D0 RID: 38864 RVA: 0x00371C12 File Offset: 0x0036FE12
		public override bool FireEvent(Event E)
		{
			return (!(E.ID == "CanAIDoIndependentBehavior") || !GameObject.Validate(ref this.CompanionOf)) && base.FireEvent(E);
		}

		// Token: 0x060097D1 RID: 38865 RVA: 0x00371C3C File Offset: 0x0036FE3C
		public virtual void InitializeBondedCompanion()
		{
		}

		// Token: 0x060097D2 RID: 38866 RVA: 0x00371C3E File Offset: 0x0036FE3E
		public virtual IAllyReason GetAllyReasonFor(GameObject Leader)
		{
			return new AllyBond();
		}

		// Token: 0x04003F1B RID: 16155
		public GameObject CompanionOf;

		// Token: 0x04003F1C RID: 16156
		public string Faction;

		// Token: 0x04003F1D RID: 16157
		public string Honorific;

		// Token: 0x04003F1E RID: 16158
		public string Title;

		// Token: 0x04003F1F RID: 16159
		public string ConversationID;

		// Token: 0x04003F20 RID: 16160
		public bool StripGear;
	}
}
