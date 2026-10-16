using System;
using System.Collections.Generic;
using System.Text;

namespace XRL.World.Parts
{
	// Token: 0x02000E15 RID: 3605
	public class HeirloomsBook : IBookContents
	{
		// Token: 0x060092DB RID: 37595 RVA: 0x0034EBB5 File Offset: 0x0034CDB5
		public override string GetTitle()
		{
			return "Heirlooms of Qud";
		}

		// Token: 0x060092DC RID: 37596 RVA: 0x0034EBBC File Offset: 0x0034CDBC
		public override List<IBookContents.BookPageInfo> GetContents()
		{
			List<IBookContents.BookPageInfo> list = new List<IBookContents.BookPageInfo>();
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			int num2 = 22;
			stringBuilder.Length = 0;
			IBookContents.BookPageInfo bookPageInfo = new IBookContents.BookPageInfo();
			stringBuilder.AppendLine("Attachment to physical objects is a peculiar phenomenon. Often the bonds we form transcend fiscal values. They are steeped in history and rolled in tradition, and they elude rational explanation.");
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("It's unsurprising, then, that my research findings indicate certain preferences are held by the myriad factions of Qud for certain types of items. These preferences, each surely sculpted by some historical event lost to memory, persist in the veneration of certain cherished heirlooms.");
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("I've assembled a catalogue of these preferences for posterity.");
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("[{{c|compiled from the notes of Sheba Hegadias}}]");
			bookPageInfo.Format = "Auto";
			bookPageInfo.Text = stringBuilder.ToString();
			bookPageInfo.Title = this.GetTitle();
			list.Add(bookPageInfo);
			stringBuilder.Length = 0;
			stringBuilder.AppendLine();
			bookPageInfo = new IBookContents.BookPageInfo();
			List<Faction> list2 = new List<Faction>();
			foreach (Faction faction in Factions.Loop())
			{
				if (faction.Visible)
				{
					list2.Add(faction);
				}
			}
			list2.Sort((Faction a, Faction b) => a.DisplayName.CompareTo(b.DisplayName));
			foreach (Faction faction2 in list2)
			{
				base.AppendCatalogLine("          " + faction2.DisplayName, Items.ItemCategoryDisplayNames[faction2.Heirloom] + "          ", ".", 75, stringBuilder);
				stringBuilder.AppendLine();
				num++;
				if (num >= num2 - 1)
				{
					stringBuilder.AppendLine();
					bookPageInfo.Text = stringBuilder.ToString();
					bookPageInfo.Title = this.GetTitle();
					list.Add(bookPageInfo);
					bookPageInfo = new IBookContents.BookPageInfo();
					stringBuilder.Length = 0;
					stringBuilder.AppendLine();
					num = 0;
				}
			}
			stringBuilder.AppendLine();
			bookPageInfo.Text = stringBuilder.ToString();
			bookPageInfo.Title = this.GetTitle();
			list.Add(bookPageInfo);
			return list;
		}
	}
}
