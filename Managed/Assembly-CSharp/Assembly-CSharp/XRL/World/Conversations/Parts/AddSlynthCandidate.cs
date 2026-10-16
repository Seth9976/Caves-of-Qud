using System;
using System.Collections.Generic;
using XRL.Language;
using XRL.UI;
using XRL.World.Parts;
using XRL.World.Quests;

namespace XRL.World.Conversations.Parts
{
	/// <summary>Adds the current location as a possible sanctuary for the slynth during the quest Landing Pads.</summary>
	// Token: 0x02000BF1 RID: 3057
	public class AddSlynthCandidate : IConversationPart
	{
		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x06007AF0 RID: 31472 RVA: 0x002E2DD8 File Offset: 0x002E0FD8
		public string SactuaryZoneID
		{
			get
			{
				GameObject speaker = The.Speaker;
				string text;
				if (speaker == null)
				{
					text = null;
				}
				else
				{
					Brain brain = speaker.Brain;
					if (brain == null)
					{
						text = null;
					}
					else
					{
						GlobalLocation startingCell = brain.StartingCell;
						text = ((startingCell != null) ? startingCell.ZoneID : null);
					}
				}
				string text2;
				if ((text2 = text) == null)
				{
					GameObject speaker2 = The.Speaker;
					string text3;
					if (speaker2 == null)
					{
						text3 = null;
					}
					else
					{
						Zone currentZone = speaker2.CurrentZone;
						text3 = ((currentZone != null) ? currentZone.ZoneID : null);
					}
					text2 = text3 ?? The.ActiveZone.ZoneID;
				}
				return text2;
			}
		}

		// Token: 0x06007AF1 RID: 31473 RVA: 0x002E2E3D File Offset: 0x002E103D
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == GetChoiceTagEvent.ID || ID == EnteredElementEvent.ID;
		}

		// Token: 0x06007AF2 RID: 31474 RVA: 0x002E2E5B File Offset: 0x002E105B
		public string GetSanctuaryName()
		{
			if (this.Sanctuary.IsNullOrEmpty())
			{
				this.Sanctuary = The.ZoneManager.GetZoneDisplayName(this.SactuaryZoneID, true, false, false, false);
			}
			return this.Sanctuary;
		}

		// Token: 0x06007AF3 RID: 31475 RVA: 0x002E2E8A File Offset: 0x002E108A
		public override bool HandleEvent(GetChoiceTagEvent E)
		{
			E.Tag = "{{W|[confirm " + this.GetSanctuaryName() + " as a sanctuary option]}}";
			return false;
		}

		// Token: 0x06007AF4 RID: 31476 RVA: 0x002E2EA8 File Offset: 0x002E10A8
		public override bool HandleEvent(EnteredElementEvent E)
		{
			LandingPadsSystem system = The.Game.GetSystem<LandingPadsSystem>();
			Popup.Show(Grammar.InitCap(this.GetSanctuaryName()) + (this.Plural ? " are" : " is") + " now a sanctuary option for the slynth.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			List<string> candidateFactions = system.candidateFactions;
			GameObject speaker = The.Speaker;
			candidateFactions.Add((speaker != null) ? speaker.GetPropertyOrTag("Mayor", null) : null);
			system.candidateFactionZones.Add(this.SactuaryZoneID);
			system.updateQuestStatus();
			return base.HandleEvent(E);
		}

		/// <summary>Optional explicit sanctuary name to use instead of zone name.</summary>
		// Token: 0x0400380E RID: 14350
		public string Sanctuary;

		/// <summary>Whether the explicit sanctuary name is plural.</summary>
		// Token: 0x0400380F RID: 14351
		public bool Plural;
	}
}
