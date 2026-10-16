using System;
using System.Collections.Generic;
using HistoryKit;
using Qud.API;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02000FB7 RID: 4023
	[Serializable]
	public class SultanShrine : IPart
	{
		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x06009FE6 RID: 40934 RVA: 0x003A24AF File Offset: 0x003A06AF
		// (set) Token: 0x06009FE7 RID: 40935 RVA: 0x003A24E4 File Offset: 0x003A06E4
		public HistoricEvent RevealedEvent
		{
			get
			{
				if (this._RevealedEvent == null && this.RevealedEventID != -1L)
				{
					this._RevealedEvent = The.Game.sultanHistory.GetEvent(this.RevealedEventID);
				}
				return this._RevealedEvent;
			}
			set
			{
				this._RevealedEvent = value;
				this.RevealedEventID = ((value != null) ? value.id : (-1L));
			}
		}

		// Token: 0x06009FE8 RID: 40936 RVA: 0x003A2500 File Offset: 0x003A0700
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009FE9 RID: 40937 RVA: 0x003A2503 File Offset: 0x003A0703
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AutoexploreObjectEvent.ID || (ID == EnteredCellEvent.ID && !this.Initialized) || ID == PooledEvent<GetPointsOfInterestEvent>.ID;
		}

		// Token: 0x06009FEA RID: 40938 RVA: 0x003A2534 File Offset: 0x003A0734
		public override bool HandleEvent(GetPointsOfInterestEvent E)
		{
			if (E.StandardChecks(this, E.Actor, null))
			{
				string baseDisplayName = this.ParentObject.BaseDisplayName;
				string text = "Sultan Shrine " + baseDisplayName;
				string text2 = null;
				bool flag = true;
				PointOfInterest pointOfInterest = E.Find(text);
				if (pointOfInterest != null)
				{
					if (this.ParentObject.DistanceTo(E.Actor) < pointOfInterest.GetDistanceTo(E.Actor))
					{
						E.Remove(pointOfInterest);
						text2 = "nearest";
					}
					else
					{
						flag = false;
						pointOfInterest.Explanation = "nearest";
					}
				}
				if (flag)
				{
					E.Add(this.ParentObject, baseDisplayName, text2, text, null, null, null, 1, null, 0);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009FEB RID: 40939 RVA: 0x003A25DC File Offset: 0x003A07DC
		public static string GetStatueForSultan(string SultanName, int Period)
		{
			string text = null;
			if (Period > 5 || SultanName == "Resheph")
			{
				text = "Terrain/sw_resheph_sultanstatue.bmp";
			}
			else
			{
				Dictionary<string, string> dictionary = The.Game.GetObjectGameState("SultanAssignedStatues") as Dictionary<string, string>;
				if (dictionary == null)
				{
					dictionary = new Dictionary<string, string>();
					The.Game.SetObjectGameState("SultanAssignedStatues", dictionary);
				}
				if (!dictionary.TryGetValue(SultanName, out text))
				{
					int num = 0;
					do
					{
						text = PopulationManager.GenerateOne("SultanShrines_StatueTiles", null, null, null, null, null).Blueprint;
					}
					while (dictionary.ContainsValue(text) && num++ < 100);
					dictionary[SultanName] = text;
				}
			}
			return text;
		}

		// Token: 0x06009FEC RID: 40940 RVA: 0x003A2670 File Offset: 0x003A0870
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (!this.Initialized)
			{
				this.ParentObject.Render.Tile = "terrain/sw_tombstone_" + Stat.Random(1, 4).ToString() + ".bmp";
				this.ShrineInitialize();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009FED RID: 40941 RVA: 0x003A26C0 File Offset: 0x003A08C0
		public override bool HandleEvent(AutoexploreObjectEvent E)
		{
			if (!E.AutogetOnlyMode && E.Command != "Look" && this.HasUnrevealedSecret())
			{
				E.Command = "Look";
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009FEE RID: 40942 RVA: 0x003A26F6 File Offset: 0x003A08F6
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterLookedAt");
			Registrar.Register("SpecialInit");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009FEF RID: 40943 RVA: 0x003A2718 File Offset: 0x003A0918
		public void ShrineInitialize()
		{
			if (this.Initialized)
			{
				return;
			}
			HistoricEntityList historicEntityList = null;
			HistoricEntity historicEntity = null;
			HistoricEntitySnapshot historicEntitySnapshot = null;
			try
			{
				History sultanHistory = The.Game.sultanHistory;
				historicEntityList = sultanHistory.GetEntitiesWherePropertyEquals("type", "sultan");
				if (base.HasPropertyOrTag("ForceSultan"))
				{
					historicEntityList.entities = new List<HistoricEntity> { sultanHistory.GetEntity(base.GetPropertyOrTag("ForceSultan", null)) };
				}
				string text = null;
				if (this.PresetLocation != -1)
				{
					text = XRLCore.Core.Game.GetStringGameState("SultanDungeonPlacementOrder_" + this.PresetLocation.ToString(), "");
				}
				if (text != null)
				{
					for (int i = 0; i < historicEntityList.entities.Count; i++)
					{
						for (int j = 0; j < historicEntityList.entities[i].events.Count; j++)
						{
							if (historicEntityList.entities[i].events[j].HasEventProperty("revealsRegion") && historicEntityList.entities[i].events[j].GetEventProperty("revealsRegion", null) == text)
							{
								historicEntity = historicEntityList.entities[i];
								historicEntitySnapshot = historicEntity.GetCurrentSnapshot();
								this.RevealedEvent = historicEntityList.entities[i].events[j];
								goto IL_02BA;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.log.Error("SultanShrine initialization error:" + ex.ToString(), Array.Empty<object>());
			}
			try
			{
				List<HistoricEntity> list = historicEntityList.entities.Shuffle<HistoricEntity>(null);
				for (int k = 0; k < list.Count; k++)
				{
					if (this.Period == 0 || int.Parse(list[k].GetCurrentSnapshot().GetProperty("period", "unknown")) == this.Period)
					{
						List<HistoricEvent> events = list[k].events;
						for (int l = 0; l < events.Count; l++)
						{
							historicEntity = list[k];
							historicEntitySnapshot = historicEntity.GetCurrentSnapshot();
							this.RevealedEvent = historicEntity.GetRandomEventWhereDelegate((HistoricEvent ev) => ev.HasEventProperty("gospel") && (this.PresetGospelByProperty == null || ev.HasEventProperty(this.PresetGospelByProperty)), Stat.Rnd);
							if (this.RevealedEvent != null)
							{
								break;
							}
						}
						if (this.RevealedEvent != null)
						{
							break;
						}
					}
				}
				if (this.RevealedEvent == null)
				{
					this.RevealedEvent = historicEntity.GetRandomEventWhereDelegate((HistoricEvent ev) => ev.HasEventProperty("gospel"), Stat.Rnd);
				}
			}
			catch (Exception ex2)
			{
				Logger.log.Error("SultanShrine initialization error (2):" + ex2.ToString(), Array.Empty<object>());
			}
			IL_02BA:
			try
			{
				string text2;
				if (this.ParentObject.HasTagOrProperty("HasPregeneratedName"))
				{
					text2 = this.ParentObject.GetPropertyOrTag("PregeneratedSultanName", null) ?? historicEntitySnapshot.GetProperty("name", "<unknown name>");
				}
				else
				{
					text2 = historicEntitySnapshot.GetProperty("name", "<unknown name>");
					this.ParentObject.DisplayName = "shrine to " + text2;
					string randomElementFromListProperty = historicEntitySnapshot.GetRandomElementFromListProperty("cognomen", null, Stat.Rand);
					if (randomElementFromListProperty != null)
					{
						Render render = this.ParentObject.Render;
						render.DisplayName = render.DisplayName + ", " + randomElementFromListProperty;
					}
				}
				if (!this.ParentObject.HasTagOrProperty("Worshippable") && !text2.IsNullOrEmpty() && text2[0] != '<')
				{
					this.ParentObject.SetStringProperty("Worshippable", "yes", false);
					this.ParentObject.SetStringProperty("WorshippedAs", text2, false);
					string property = historicEntitySnapshot.GetProperty("period", "unknown");
					int num;
					if (text2 == "Resheph")
					{
						this.ParentObject.SetIntProperty("WorshipPower", 8, false);
					}
					else if (!property.IsNullOrEmpty() && int.TryParse(property, out num))
					{
						this.ParentObject.SetIntProperty("WorshipPower", 7 - num, false);
					}
					else
					{
						this.ParentObject.SetIntProperty("WorshipPower", 2, false);
					}
					if (text2 == "Resheph")
					{
						this.ParentObject.SetStringProperty("WorshipFaction", text2, false);
					}
					else
					{
						this.ParentObject.SetStringProperty("WorshipFaction", Faction.GetSultanFactionName(property), false);
					}
				}
				this.ParentObject.GetPart<Description>().Short = "The shrine depicts a significant event from the life of the ancient sultan " + text2 + ":\n\n" + this.RevealedEvent.GetEventProperty("gospel", "<unknown gospel>");
				if (historicEntitySnapshot != null)
				{
					string statueForSultan = SultanShrine.GetStatueForSultan(text2, this.Period);
					if (!statueForSultan.IsNullOrEmpty())
					{
						this.ParentObject.Render.Tile = statueForSultan;
					}
					if (this.PresetLocation == 0)
					{
						foreach (JournalSultanNote journalSultanNote in JournalAPI.GetSultanNotes(null))
						{
							if (journalSultanNote.SultanID == historicEntitySnapshot.entity.id)
							{
								journalSultanNote.Attributes.Add("include:Joppa");
							}
						}
					}
				}
			}
			catch (Exception ex3)
			{
				Logger.log.Error("SultanShrine initialization error (3):" + ex3.ToString(), Array.Empty<object>());
			}
			this.Initialized = true;
		}

		// Token: 0x06009FF0 RID: 40944 RVA: 0x003A2CE4 File Offset: 0x003A0EE4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "SpecialInit")
			{
				this.ShrineInitialize();
			}
			else if (E.ID == "AfterLookedAt")
			{
				this.ShrineInitialize();
				SultanShrine.RevealBasedOnHistoricalEvent(this.RevealedEvent, this.PresetLocation, this.PresetGospelByProperty, this.ParentObject.DisplayName);
			}
			return base.FireEvent(E);
		}

		// Token: 0x06009FF1 RID: 40945 RVA: 0x003A2D4C File Offset: 0x003A0F4C
		public static void RevealBasedOnHistoricalEvent(HistoricEvent ev, int PresetLocation = -1, string PresetGospelByProperty = null, string LearnedFrom = null)
		{
			JournalSultanNote journalSultanNote = ev.Reveal(LearnedFrom);
			if ((PresetLocation == 0 || PresetGospelByProperty == "JoppaShrine") && journalSultanNote != null && !journalSultanNote.Has("nobuy:Joppa"))
			{
				journalSultanNote.Attributes.Add("nobuy:Joppa");
				Faction faction = Factions.Get("Joppa");
				if (faction.Visible)
				{
					JournalSultanNote journalSultanNote2 = journalSultanNote;
					journalSultanNote2.History = journalSultanNote2.History + " {{K|-known by " + faction.GetFormattedName() + "}}";
				}
			}
		}

		// Token: 0x06009FF2 RID: 40946 RVA: 0x003A2DC5 File Offset: 0x003A0FC5
		public bool HasUnrevealedSecret()
		{
			return this.RevealedEvent != null && this.RevealedEvent.HasUnrevealedSecret();
		}

		// Token: 0x04004279 RID: 17017
		public long RevealedEventID = -1L;

		// Token: 0x0400427A RID: 17018
		public int PresetLocation = -1;

		// Token: 0x0400427B RID: 17019
		public int Period;

		// Token: 0x0400427C RID: 17020
		public string Inscription = "";

		// Token: 0x0400427D RID: 17021
		public string PresetGospelByProperty;

		// Token: 0x0400427E RID: 17022
		public bool Initialized;

		// Token: 0x0400427F RID: 17023
		public string RegionName;

		// Token: 0x04004280 RID: 17024
		[NonSerialized]
		private HistoricEvent _RevealedEvent;
	}
}
