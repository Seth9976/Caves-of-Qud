using System;
using System.Collections.Generic;
using System.Linq;
using Qud.API;
using XRL.World.AI.GoalHandlers;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000E26 RID: 3622
	[Serializable]
	public class AIPilgrim : AIBehaviorPart
	{
		// Token: 0x0600934B RID: 37707 RVA: 0x00350F86 File Offset: 0x0034F186
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<AIBoredEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600934C RID: 37708 RVA: 0x00350FA4 File Offset: 0x0034F1A4
		public override bool HandleEvent(AIBoredEvent E)
		{
			return !this.CheckStartPilgrimage() && base.HandleEvent(E);
		}

		// Token: 0x0600934D RID: 37709 RVA: 0x00350FB7 File Offset: 0x0034F1B7
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("travel", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600934E RID: 37710 RVA: 0x00350FDA File Offset: 0x0034F1DA
		public override void Initialize()
		{
			if (!this.Chance.in100())
			{
				this.Ignore = true;
			}
			base.Initialize();
		}

		// Token: 0x0600934F RID: 37711 RVA: 0x00350FF8 File Offset: 0x0034F1F8
		public bool CheckStartPilgrimage()
		{
			if (this.Ignore)
			{
				return false;
			}
			if (!this.ParentObject.FireEvent("CanAIDoIndependentBehavior"))
			{
				return false;
			}
			if (this.ParentObject.PartyLeader != null)
			{
				return false;
			}
			if (this.ParentObject.HasTagOrProperty("ExcludeFromDynamicEncounters"))
			{
				return false;
			}
			if (this.FoundTarget)
			{
				return false;
			}
			if (this.ParentObject.HasIntProperty("LairOwner"))
			{
				return false;
			}
			if (this.ParentObject.HasEffect<Lost>() || (this.ParentObject.HasEffect<XRL.World.Effects.Confused>() && !this.ParentObject.HasEffect<FuriouslyConfused>()))
			{
				return false;
			}
			if (!string.IsNullOrEmpty(this.MapNoteAttributes))
			{
				if (this.MapNoteAttributes == "invalid-tag")
				{
					return false;
				}
				if (this.ParentObject.CurrentZone == null)
				{
					return false;
				}
				IEnumerable<JournalMapNote> mapNotesWithAllAttributes = JournalAPI.GetMapNotesWithAllAttributes(this.MapNoteAttributes);
				this.MapNoteAttributes = "invalid-tag";
				if (mapNotesWithAllAttributes.Count<JournalMapNote>() == 0)
				{
					return false;
				}
				JournalMapNote randomElement = mapNotesWithAllAttributes.ToList<JournalMapNote>().GetRandomElement<JournalMapNote>(null);
				this.StiltWx = randomElement.ParasangX;
				this.StiltWy = randomElement.ParasangY;
				this.StiltXx = randomElement.ZoneX;
				this.StiltYx = randomElement.ZoneY;
				this.StiltZx = randomElement.ZoneZ;
				this.StiltZoneID = randomElement.ZoneID;
				this.StiltEntranceZoneID = randomElement.ZoneID;
			}
			this.ParentObject.Brain.PushGoal(new GoOnAPilgrimage(this.StiltWx, this.StiltWy, this.StiltXx, this.StiltYx, this.StiltZx, this.TargetObject, this.StiltZoneID, this.StiltEntranceZoneID));
			return true;
		}

		// Token: 0x04003CE7 RID: 15591
		public bool FoundTarget;

		// Token: 0x04003CE8 RID: 15592
		public int StiltWx = 5;

		// Token: 0x04003CE9 RID: 15593
		public int StiltWy = 2;

		// Token: 0x04003CEA RID: 15594
		public int StiltXx = 1;

		// Token: 0x04003CEB RID: 15595
		public int StiltYx = 1;

		// Token: 0x04003CEC RID: 15596
		public int StiltZx = 10;

		// Token: 0x04003CED RID: 15597
		public string StiltZoneID = "JoppaWorld.5.2.1.1.10";

		// Token: 0x04003CEE RID: 15598
		public string StiltEntranceZoneID = "JoppaWorld.5.2.1.2.10";

		// Token: 0x04003CEF RID: 15599
		public string TargetObject = "StiltWell";

		// Token: 0x04003CF0 RID: 15600
		public string MapNoteAttributes;

		// Token: 0x04003CF1 RID: 15601
		public int Chance = 100;

		// Token: 0x04003CF2 RID: 15602
		public bool Ignore;
	}
}
