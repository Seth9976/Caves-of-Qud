using System;
using System.Collections.Generic;
using System.Text;
using HistoryKit;
using Qud.API;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02001158 RID: 4440
	[Serializable]
	public class ModPainted : IModification
	{
		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x0600B06D RID: 45165 RVA: 0x0040EFE7 File Offset: 0x0040D1E7
		// (set) Token: 0x0600B06E RID: 45166 RVA: 0x0040F01C File Offset: 0x0040D21C
		public HistoricEvent PaintedEvent
		{
			get
			{
				if (this._PaintedEvent == null && this.PaintedEventID != -1L)
				{
					this._PaintedEvent = The.Game.sultanHistory.GetEvent(this.PaintedEventID);
				}
				return this._PaintedEvent;
			}
			set
			{
				this._PaintedEvent = value;
				this.PaintedEventID = ((value != null) ? value.id : (-1L));
			}
		}

		// Token: 0x0600B06F RID: 45167 RVA: 0x0040F038 File Offset: 0x0040D238
		public ModPainted()
		{
		}

		// Token: 0x0600B070 RID: 45168 RVA: 0x0040F053 File Offset: 0x0040D253
		public ModPainted(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B071 RID: 45169 RVA: 0x0040F06F File Offset: 0x0040D26F
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B072 RID: 45170 RVA: 0x0040F078 File Offset: 0x0040D278
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600B073 RID: 45171 RVA: 0x0040F07B File Offset: 0x0040D27B
		public override void ApplyModification(GameObject Object)
		{
			Object.ModIntProperty("NoCostMods", 1, false);
		}

		// Token: 0x0600B074 RID: 45172 RVA: 0x0040F08B File Offset: 0x0040D28B
		public override void Remove()
		{
			this.ParentObject.ModIntProperty("NoCostMods", 1, true);
			base.Remove();
		}

		// Token: 0x0600B075 RID: 45173 RVA: 0x0040F0A6 File Offset: 0x0040D2A6
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AutoexploreObjectEvent.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID || ID == GetUnknownShortDescriptionEvent.ID || ID == ObjectCreatedEvent.ID;
		}

		// Token: 0x0600B076 RID: 45174 RVA: 0x0040F0DC File Offset: 0x0040D2DC
		public override bool HandleEvent(AutoexploreObjectEvent E)
		{
			if (!E.AutogetOnlyMode && E.Command != "Look" && !this.LookedAt && this.HasUnrevealedSecret())
			{
				E.Command = "Look";
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B077 RID: 45175 RVA: 0x0040F11A File Offset: 0x0040D31A
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (!E.Understood() || !E.Object.HasProperName)
			{
				E.AddAdjective("{{painted|painted}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B078 RID: 45176 RVA: 0x0040F144 File Offset: 0x0040D344
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			this.AddPainting(E);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B079 RID: 45177 RVA: 0x0040F154 File Offset: 0x0040D354
		public override bool HandleEvent(GetUnknownShortDescriptionEvent E)
		{
			this.AddPainting(E);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B07A RID: 45178 RVA: 0x0040F164 File Offset: 0x0040D364
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			this.GeneratePainting();
			return base.HandleEvent(E);
		}

		// Token: 0x0600B07B RID: 45179 RVA: 0x0040F173 File Offset: 0x0040D373
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B07C RID: 45180 RVA: 0x0040F176 File Offset: 0x0040D376
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterLookedAt");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B07D RID: 45181 RVA: 0x0040F18C File Offset: 0x0040D38C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AfterLookedAt" && !this.LookedAt)
			{
				this.LookedAt = true;
				HistoricEvent paintedEvent = this.PaintedEvent;
				if (paintedEvent != null)
				{
					paintedEvent.Reveal(this.ParentObject.DisplayName);
				}
				if (this.HasUnrevealedSecret())
				{
					string text = "after reveal still had unrevealed secret from ";
					HistoricEvent paintedEvent2 = this.PaintedEvent;
					MetricsManager.LogError(text + ((paintedEvent2 != null) ? paintedEvent2.GetUnrevealedSecretSource() : null));
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600B07E RID: 45182 RVA: 0x0040F207 File Offset: 0x0040D407
		public bool HasUnrevealedSecret()
		{
			return this.PaintedEvent != null && this.PaintedEvent.HasUnrevealedSecret();
		}

		// Token: 0x0600B07F RID: 45183 RVA: 0x0040F220 File Offset: 0x0040D420
		private void GeneratePainting()
		{
			if (this.Painting != null)
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
				this.PaintedEvent = list.GetRandomElement<HistoricEvent>(null);
				this.Painting = this.PaintedEvent.GetEventProperty("gospel", null);
			}
			else
			{
				this.Painting = "<marred and unreadable>";
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

		// Token: 0x0600B080 RID: 45184 RVA: 0x0040F384 File Offset: 0x0040D584
		private void AddPainting(IShortDescriptionEvent E)
		{
			if (this.PaintedEvent == null)
			{
				this.GeneratePainting();
			}
			E.Postfix.Append("\n{{cyan|Painted: This item is painted with a scene from the life of the ancient ").Append(HistoryAPI.GetSultanTerm()).Append(" {{magenta|")
				.Append(this.Sultan)
				.Append("}}:\n\n")
				.Append(this.Painting.Coalesce("<marred and unreadable>"))
				.Append("}}\n");
		}

		// Token: 0x04004818 RID: 18456
		public long PaintedEventID = -1L;

		// Token: 0x04004819 RID: 18457
		public string Painting;

		// Token: 0x0400481A RID: 18458
		public string Sultan = "";

		// Token: 0x0400481B RID: 18459
		public bool LookedAt;

		// Token: 0x0400481C RID: 18460
		[NonSerialized]
		private HistoricEvent _PaintedEvent;
	}
}
