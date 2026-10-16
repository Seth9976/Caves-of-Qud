using System;
using System.Collections.Generic;
using HistoryKit;
using Newtonsoft.Json.Linq;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000C15 RID: 3093
	public class SpiceContext : IConversationPart
	{
		// Token: 0x06007BEC RID: 31724 RVA: 0x002E91D5 File Offset: 0x002E73D5
		public SpiceContext()
		{
			this.Priority = -1000;
		}

		// Token: 0x06007BED RID: 31725 RVA: 0x002E91FE File Offset: 0x002E73FE
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == PrepareTextEvent.ID;
		}

		// Token: 0x06007BEE RID: 31726 RVA: 0x002E9214 File Offset: 0x002E7414
		public override bool HandleEvent(PrepareTextEvent E)
		{
			for (int i = E.Text.IndexOf("=spice"); i >= 0; i = E.Text.IndexOf("=spice"))
			{
				int num = E.Text.IndexOf('=', i + 5);
				int num2 = E.Text.IndexOf(".store");
				if (num2 < num)
				{
					E.Text.Remove(num2, 6);
					num -= 6;
				}
				for (;;)
				{
					Random random = new Random(The.Speaker.ID.GetHashCode());
					string text = HistoricStringExpander.ExpandQuery(E.Text.ToString(i + 1, num - i - 1), null, null, this.Variables, this.Nodes, random);
					E.Text.Remove(i, num - i + 1);
					E.Text.Insert(i, text);
					int num3 = text.IndexOf('<');
					if (num3 < 0)
					{
						break;
					}
					num = i + text.IndexOf('>', num3 + 1);
					i += num3;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04003852 RID: 14418
		public Dictionary<string, string> Variables = new Dictionary<string, string>();

		// Token: 0x04003853 RID: 14419
		public Dictionary<string, JToken> Nodes = new Dictionary<string, JToken>();
	}
}
