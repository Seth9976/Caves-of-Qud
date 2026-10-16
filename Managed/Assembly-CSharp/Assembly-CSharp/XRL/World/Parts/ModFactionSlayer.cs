using System;
using XRL.World.Parts.Skill;

namespace XRL.World.Parts
{
	// Token: 0x02001137 RID: 4407
	[Serializable]
	public class ModFactionSlayer : IModification
	{
		// Token: 0x0600AEB1 RID: 44721 RVA: 0x004087B7 File Offset: 0x004069B7
		public ModFactionSlayer()
		{
		}

		// Token: 0x0600AEB2 RID: 44722 RVA: 0x004087BF File Offset: 0x004069BF
		public ModFactionSlayer(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600AEB3 RID: 44723 RVA: 0x004087C8 File Offset: 0x004069C8
		public ModFactionSlayer(int Tier, string Faction)
			: this(Tier)
		{
			if (!Factions.Exists(Faction))
			{
				string newFaction = CompatManager.GetNewFaction(Faction);
				if (newFaction != null)
				{
					MetricsManager.LogWarning(string.Concat(new string[] { "faction name ", Faction, " should be ", newFaction, ", support will be removed after Q2 2024" }));
					Faction = newFaction;
				}
			}
			this.Faction = Faction;
		}

		// Token: 0x0600AEB4 RID: 44724 RVA: 0x00408828 File Offset: 0x00406A28
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			base.Read(Basis, Reader);
			if (!Factions.Exists(this.Faction))
			{
				string newFaction = CompatManager.GetNewFaction(this.Faction);
				if (newFaction != null)
				{
					if (Reader.FileVersion >= 350)
					{
						MetricsManager.LogWarning(string.Concat(new string[] { "faction name ", this.Faction, " should be ", newFaction, ", support will be removed after Q2 2024" }));
					}
					this.Faction = newFaction;
					return;
				}
				Faction byDisplayName = Factions.GetByDisplayName(this.Faction);
				if (byDisplayName != null)
				{
					this.Faction = byDisplayName.Name;
				}
			}
		}

		// Token: 0x0600AEB5 RID: 44725 RVA: 0x004088BE File Offset: 0x00406ABE
		public override void Configure()
		{
			this.WorksOnSelf = true;
			this.NameForStatus = "ProfilingEngine";
		}

		// Token: 0x0600AEB6 RID: 44726 RVA: 0x004088D2 File Offset: 0x00406AD2
		public override bool SameAs(IPart p)
		{
			return !((p as ModFactionSlayer).Faction != this.Faction) && base.SameAs(p);
		}

		// Token: 0x0600AEB7 RID: 44727 RVA: 0x004088F5 File Offset: 0x00406AF5
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600AEB8 RID: 44728 RVA: 0x0040890C File Offset: 0x00406B0C
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			int @for = GetSpecialEffectChanceEvent.GetFor(this.ParentObject.Equipped ?? this.ParentObject.Implantee, this.ParentObject, "Modification ModFactionSlayer Decapitate", this.Tier, null, null, true, false);
			E.Postfix.AppendRules(@for.ToString() + "% chance to behead " + XRL.World.Faction.GetFormattedName(this.Faction) + " on hit.");
			return base.HandleEvent(E);
		}

		// Token: 0x0600AEB9 RID: 44729 RVA: 0x00408982 File Offset: 0x00406B82
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600AEBA RID: 44730 RVA: 0x00408985 File Offset: 0x00406B85
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WeaponHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AEBB RID: 44731 RVA: 0x0040899C File Offset: 0x00406B9C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponHit")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Defender");
				if (gameObjectParameter2.IsFactionMember(this.Faction))
				{
					GameObject gameObject = gameObjectParameter;
					GameObject parentObject = this.ParentObject;
					string text = "Modification ModFactionSlayer Decapitate";
					GameObject gameObject2 = gameObjectParameter2;
					if (GetSpecialEffectChanceEvent.GetFor(gameObject, parentObject, text, this.Tier, gameObject2, null, true, false).in100() && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
					{
						Axe_Decapitate.Decapitate(gameObjectParameter, gameObjectParameter2, null, null, null, null, false, false);
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x040047FA RID: 18426
		public string Faction;
	}
}
