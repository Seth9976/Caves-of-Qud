using System;
using System.Collections.Generic;
using Qud.API;
using XRL.Rules;
using XRL.World.ObjectBuilders;

namespace XRL.World.Parts
{
	// Token: 0x02000EA2 RID: 3746
	[Serializable]
	public class DromadCaravan : IBondedLeader
	{
		// Token: 0x06009682 RID: 38530 RVA: 0x003687FB File Offset: 0x003669FB
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009683 RID: 38531 RVA: 0x003687FE File Offset: 0x003669FE
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID || ID == PooledEvent<GetPointsOfInterestEvent>.ID;
		}

		// Token: 0x06009684 RID: 38532 RVA: 0x0036881C File Offset: 0x00366A1C
		public override bool HandleEvent(GetPointsOfInterestEvent E)
		{
			if (this.Seen && this.IsWorkingCaravan() && E.StandardChecks(this, E.Actor, null))
			{
				E.Add(this.ParentObject, this.ParentObject.GetReferenceDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, false, false, null, false), null, null, null, null, null, -1, null, 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009685 RID: 38533 RVA: 0x00368884 File Offset: 0x00366A84
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (!this.Created)
			{
				try
				{
					this.Created = true;
					string text = "DromadMerchant_" + this.ParentObject.CurrentZone.ZoneID;
					this.ParentObject.SetStringProperty("nosecret", text, false);
					List<Cell> adjacentCells = this.ParentObject.CurrentCell.GetAdjacentCells(4, true, true);
					List<Cell> list = new List<Cell>();
					foreach (Cell cell in adjacentCells)
					{
						if (cell.IsEmpty())
						{
							list.Add(cell);
						}
					}
					List<string> list2 = new List<string>();
					int num = Stat.Random(1, 3);
					for (int i = 0; i < num; i++)
					{
						list2.Add("Great Saltback");
					}
					int num2 = Stat.Random(2, 4);
					for (int j = 0; j < num2; j++)
					{
						list2.Add("Caravan Guard");
					}
					Tier1HumanoidEquipment tier1HumanoidEquipment = new Tier1HumanoidEquipment();
					int num3 = 0;
					while (num3 < list2.Count && list.Count > 0)
					{
						GameObject gameObject = GameObject.Create(list2[num3]);
						tier1HumanoidEquipment.Apply(gameObject, null);
						gameObject.AddPart<Companion>(new Companion(this.ParentObject, null, null, null, null, false), true, false);
						Cell randomElement = list.GetRandomElement<Cell>(null);
						randomElement.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						gameObject.MakeActive(false);
						list.Remove(randomElement);
						num3++;
					}
				}
				catch
				{
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009686 RID: 38534 RVA: 0x00368A40 File Offset: 0x00366C40
		public override bool Render(RenderEvent E)
		{
			if (!this.Seen)
			{
				this.Seen = true;
				if (this.IsWorkingCaravan())
				{
					string text = "DromadMerchant_" + this.ParentObject.CurrentZone.ZoneID;
					JournalMapNote mapNote = JournalAPI.GetMapNote(text);
					if (mapNote != null)
					{
						if (!mapNote.Revealed)
						{
							IBaseJournalEntry.DisplayMessage("You spot a {{w|dromad}} caravan.", null);
							JournalAPI.RevealMapNote(mapNote, false, null);
						}
					}
					else
					{
						IBaseJournalEntry.DisplayMessage("You spot a {{w|dromad}} caravan.", null);
						JournalAPI.AddMapNote(this.ParentObject.CurrentZone.ZoneID, "A {{w|dromad}} caravan", "Merchants", new string[] { "dromad", "merchant" }, text, true, false, -1L, false);
					}
				}
			}
			return base.Render(E);
		}

		// Token: 0x06009687 RID: 38535 RVA: 0x00368AFC File Offset: 0x00366CFC
		public bool IsWorkingCaravan()
		{
			return !this.ParentObject.HasPart<PsychicThrall>() && !this.ParentObject.HasPart<DomesticatedSlave>() && !this.ParentObject.IsHostileTowards(The.Player) && !this.ParentObject.IsPlayerControlled();
		}

		// Token: 0x04003E5A RID: 15962
		public bool Seen;

		// Token: 0x04003E5B RID: 15963
		public bool Created;
	}
}
