using System;
using System.Collections.Generic;
using System.Text;
using XRL.World.Parts;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000C10 RID: 3088
	public class QuestSignpost : IConversationPart
	{
		// Token: 0x06007BD2 RID: 31698 RVA: 0x002E8720 File Offset: 0x002E6920
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == PrepareTextEvent.ID;
		}

		// Token: 0x06007BD3 RID: 31699 RVA: 0x002E8738 File Offset: 0x002E6938
		public override bool HandleEvent(PrepareTextEvent E)
		{
			XRLGame game = The.Game;
			List<GameObject> list = Event.NewGameObjectList();
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			GameObject speaker = The.Speaker;
			Zone parentZone = speaker.CurrentCell.ParentZone;
			Brain brain = speaker.Brain;
			foreach (GameObject gameObject in parentZone.IterateObjects())
			{
				if (brain.InSameFactionAs(gameObject, Brain.AllegianceLevel.Member) && game.GetQuestGiverState(gameObject) == 0)
				{
					list.Add(gameObject);
				}
			}
			list.Remove(speaker);
			string text = "";
			bool flag = false;
			int i = 0;
			int num = list.Count - 1;
			while (i <= num)
			{
				GameObject gameObject2 = list[i];
				string text2 = speaker.DescribeDirectionToward(gameObject2, true, false);
				if (i > 0)
				{
					if (i == num)
					{
						stringBuilder.Append(flag ? ", or " : " or ");
					}
					else
					{
						stringBuilder.Append(", ");
					}
				}
				stringBuilder.Append("{{y|" + gameObject2.GetDisplayName(int.MaxValue, null, null, false, false, false, false, true, false, true, true, false, false, true, false, false, null, false, false, false, false, null, false, null, false, true) + "}}");
				IZoneLandmark bestFor = IZoneLandmark.GetBestFor(gameObject2.CurrentCell);
				if (bestFor != null)
				{
					stringBuilder.Append(',');
					bestFor.Append(stringBuilder, gameObject2);
				}
				if (!text2.IsNullOrEmpty())
				{
					if (bestFor == null)
					{
						stringBuilder.Append(',');
					}
					stringBuilder.Append((text2 == text) ? " also " : " ").Append(text2);
					flag = true;
					text = text2;
				}
				i++;
			}
			E.Text.Replace("=questgivers=", Event.FinalizeString(stringBuilder));
			return base.HandleEvent(E);
		}
	}
}
