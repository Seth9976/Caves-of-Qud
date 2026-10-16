using System;
using HistoryKit;
using Qud.API;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000C1E RID: 3102
	public class VillageContext : IConversationPart
	{
		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x06007C23 RID: 31779 RVA: 0x002E9FB8 File Offset: 0x002E81B8
		public HistoricEntitySnapshot History
		{
			get
			{
				if (this._History != null)
				{
					return this._History;
				}
				string text;
				if (!this.GameState.IsNullOrEmpty() && The.Game.StringGameState.TryGetValue(this.GameState, out text))
				{
					this._History = HistoryAPI.GetVillageSnapshot(text);
					if (this._History != null)
					{
						return this._History;
					}
				}
				GameObject speaker = The.Speaker;
				text = ((speaker != null) ? speaker.GetPropertyOrTag("Mayor", null) : null);
				if (!text.IsNullOrEmpty())
				{
					this._History = HistoryAPI.GetVillageSnapshot(text);
					if (this._History != null)
					{
						return this._History;
					}
				}
				text = speaker.GetPrimaryFaction(false);
				if (!text.IsNullOrEmpty())
				{
					this._History = HistoryAPI.GetVillageSnapshot(text);
					if (this._History != null)
					{
						return this._History;
					}
				}
				return null;
			}
		}

		// Token: 0x06007C24 RID: 31780 RVA: 0x002EA07D File Offset: 0x002E827D
		public VillageContext()
		{
			this.Priority = -1000;
		}

		// Token: 0x06007C25 RID: 31781 RVA: 0x002EA090 File Offset: 0x002E8290
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == PrepareTextEvent.ID;
		}

		// Token: 0x06007C26 RID: 31782 RVA: 0x002EA0A6 File Offset: 0x002E82A6
		public override bool HandleEvent(PrepareTextEvent E)
		{
			if (E.Text.Contains("=village"))
			{
				HistoryAPI.ExpandVillageText(E.Text, null, this.History);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04003875 RID: 14453
		public string GameState;

		// Token: 0x04003876 RID: 14454
		private HistoricEntitySnapshot _History;
	}
}
