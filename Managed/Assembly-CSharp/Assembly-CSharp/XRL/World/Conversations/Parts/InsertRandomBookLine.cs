using System;
using XRL.UI;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000C04 RID: 3076
	public class InsertRandomBookLine : IConversationPart
	{
		// Token: 0x06007B93 RID: 31635 RVA: 0x002E674D File Offset: 0x002E494D
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == PrepareTextEvent.ID;
		}

		// Token: 0x06007B94 RID: 31636 RVA: 0x002E6764 File Offset: 0x002E4964
		public override bool HandleEvent(PrepareTextEvent E)
		{
			BookInfo bookInfo;
			if (BookUI.Books.TryGetValue(this.BookID, out bookInfo) && bookInfo.Count > 0)
			{
				string randomElement = bookInfo[0].Lines.GetRandomElement<string>(null);
				if (!randomElement.IsNullOrEmpty())
				{
					if (this.Prepend)
					{
						E.Text.Insert(0, "\n\n").Insert(0, randomElement);
					}
					else
					{
						E.Text.Append("\n\n").Append(randomElement);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04003831 RID: 14385
		public string BookID;

		// Token: 0x04003832 RID: 14386
		public bool Prepend;
	}
}
