using System;
using System.Collections.Generic;
using System.Text;
using HistoryKit;
using Qud.API;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02001135 RID: 4405
	[Serializable]
	public class ModEngraved : IModification
	{
		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x0600AE89 RID: 44681 RVA: 0x0040738B File Offset: 0x0040558B
		// (set) Token: 0x0600AE8A RID: 44682 RVA: 0x004073C0 File Offset: 0x004055C0
		public HistoricEvent EngravedEvent
		{
			get
			{
				if (this._EngravedEvent == null && this.EngravedEventID != -1L)
				{
					this._EngravedEvent = The.Game.sultanHistory.GetEvent(this.EngravedEventID);
				}
				return this._EngravedEvent;
			}
			set
			{
				this._EngravedEvent = value;
				this.EngravedEventID = ((value != null) ? value.id : (-1L));
			}
		}

		// Token: 0x0600AE8B RID: 44683 RVA: 0x004073DC File Offset: 0x004055DC
		public ModEngraved()
		{
		}

		// Token: 0x0600AE8C RID: 44684 RVA: 0x004073F7 File Offset: 0x004055F7
		public ModEngraved(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600AE8D RID: 44685 RVA: 0x00407413 File Offset: 0x00405613
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600AE8E RID: 44686 RVA: 0x0040741C File Offset: 0x0040561C
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600AE8F RID: 44687 RVA: 0x0040741F File Offset: 0x0040561F
		public override void ApplyModification(GameObject Object)
		{
			Object.ModIntProperty("NoCostMods", 1, false);
		}

		// Token: 0x0600AE90 RID: 44688 RVA: 0x0040742F File Offset: 0x0040562F
		public override void Remove()
		{
			this.ParentObject.ModIntProperty("NoCostMods", 1, true);
			base.Remove();
		}

		// Token: 0x0600AE91 RID: 44689 RVA: 0x0040744A File Offset: 0x0040564A
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AutoexploreObjectEvent.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID || ID == GetUnknownShortDescriptionEvent.ID || ID == ObjectCreatedEvent.ID;
		}

		// Token: 0x0600AE92 RID: 44690 RVA: 0x00407480 File Offset: 0x00405680
		public override bool HandleEvent(AutoexploreObjectEvent E)
		{
			if (!E.AutogetOnlyMode && E.Command != "Look" && !this.LookedAt && this.HasUnrevealedSecret())
			{
				E.Command = "Look";
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AE93 RID: 44691 RVA: 0x004074BE File Offset: 0x004056BE
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (!E.Understood() || !E.Object.HasProperName)
			{
				E.AddAdjective("{{engraved|engraved}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AE94 RID: 44692 RVA: 0x004074E8 File Offset: 0x004056E8
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			this.AddEngraving(E);
			return base.HandleEvent(E);
		}

		// Token: 0x0600AE95 RID: 44693 RVA: 0x004074F8 File Offset: 0x004056F8
		public override bool HandleEvent(GetUnknownShortDescriptionEvent E)
		{
			this.AddEngraving(E);
			return base.HandleEvent(E);
		}

		// Token: 0x0600AE96 RID: 44694 RVA: 0x00407508 File Offset: 0x00405708
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			this.GenerateEngraving();
			return base.HandleEvent(E);
		}

		// Token: 0x0600AE97 RID: 44695 RVA: 0x00407517 File Offset: 0x00405717
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600AE98 RID: 44696 RVA: 0x0040751A File Offset: 0x0040571A
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterLookedAt");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AE99 RID: 44697 RVA: 0x00407530 File Offset: 0x00405730
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AfterLookedAt" && !this.LookedAt)
			{
				this.LookedAt = true;
				HistoricEvent engravedEvent = this.EngravedEvent;
				if (engravedEvent != null)
				{
					engravedEvent.Reveal(this.ParentObject.DisplayName);
				}
				if (this.HasUnrevealedSecret())
				{
					string text = "after reveal still had unrevealed secret from ";
					HistoricEvent engravedEvent2 = this.EngravedEvent;
					MetricsManager.LogError(text + ((engravedEvent2 != null) ? engravedEvent2.GetUnrevealedSecretSource() : null));
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600AE9A RID: 44698 RVA: 0x004075AB File Offset: 0x004057AB
		public bool HasUnrevealedSecret()
		{
			return this.EngravedEvent != null && this.EngravedEvent.HasUnrevealedSecret();
		}

		// Token: 0x0600AE9B RID: 44699 RVA: 0x004075C4 File Offset: 0x004057C4
		private void GenerateEngraving()
		{
			if (this.Engraving != null)
			{
				return;
			}
			XRLGame game = The.Game;
			History history = ((game != null) ? game.sultanHistory : null);
			if (history == null)
			{
				return;
			}
			HistoricEntity randomElement = history.GetEntitiesWherePropertyEquals("type", "sultan").GetRandomElement(Stat.Rand);
			this.Sultan = randomElement.GetCurrentSnapshot().GetProperty("name", "unknown");
			List<HistoricEvent> list = new List<HistoricEvent>();
			for (int i = 0; i < randomElement.events.Count; i++)
			{
				if (randomElement.events[i].HasEventProperty("gospel"))
				{
					list.Add(randomElement.events[i]);
				}
			}
			if (list.Count > 0)
			{
				this.EngravedEvent = list.GetRandomElement<HistoricEvent>(null);
				this.Engraving = this.EngravedEvent.GetEventProperty("gospel", null);
			}
			else
			{
				this.Engraving = "<marred and unreadable>";
			}
			string propertyOrTag = this.ParentObject.GetPropertyOrTag("Mods", null);
			if (propertyOrTag != null && !propertyOrTag.Contains("PotteryMods"))
			{
				string property = randomElement.GetCurrentSnapshot().GetProperty("period", "0");
				StringBuilder stringBuilder = Event.NewStringBuilder(null);
				stringBuilder.Append(Faction.GetSultanFactionName(property)).Append(":").Append(Stat.Random(8, 12) * 5);
				AddsRep.AddModifier(this.ParentObject, stringBuilder.ToString());
			}
		}

		// Token: 0x0600AE9C RID: 44700 RVA: 0x00407728 File Offset: 0x00405928
		private void AddEngraving(IShortDescriptionEvent E)
		{
			if (this.EngravedEvent == null)
			{
				this.GenerateEngraving();
			}
			E.Postfix.Append("\n{{cyan|Engraved: This item is engraved with a scene from the life of the ancient ").Append(HistoryAPI.GetSultanTerm()).Append(" {{magenta|")
				.Append(this.Sultan)
				.Append("}}:\n\n")
				.Append(this.Engraving.Coalesce("<marred and unreadable>"))
				.Append("}}\n");
		}

		// Token: 0x040047EC RID: 18412
		public long EngravedEventID = -1L;

		// Token: 0x040047ED RID: 18413
		public bool LookedAt;

		// Token: 0x040047EE RID: 18414
		public string Engraving;

		// Token: 0x040047EF RID: 18415
		public string Sultan = "";

		// Token: 0x040047F0 RID: 18416
		[NonSerialized]
		private HistoricEvent _EngravedEvent;
	}
}
