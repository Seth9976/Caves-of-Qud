using System;
using XRL.UI;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000C1D RID: 3101
	public class Trade : IConversationPart
	{
		// Token: 0x06007C17 RID: 31767 RVA: 0x002E9E08 File Offset: 0x002E8008
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == GetChoiceTagEvent.ID || ID == EnterElementEvent.ID || ID == IsElementVisibleEvent.ID;
		}

		// Token: 0x06007C18 RID: 31768 RVA: 0x002E9E2E File Offset: 0x002E802E
		public override bool HandleEvent(GetChoiceTagEvent E)
		{
			E.Tag = "{{g|[begin trade]}}";
			return false;
		}

		// Token: 0x06007C19 RID: 31769 RVA: 0x002E9E3C File Offset: 0x002E803C
		public override bool HandleEvent(EnterElementEvent E)
		{
			Trade.ShowScreen();
			return false;
		}

		// Token: 0x06007C1A RID: 31770 RVA: 0x002E9E44 File Offset: 0x002E8044
		public override void Initialize()
		{
			base.Initialize();
			if (Trade.Choice == null)
			{
				Trade.Choice = this.ParentElement as Choice;
			}
		}

		// Token: 0x06007C1B RID: 31771 RVA: 0x002E9E63 File Offset: 0x002E8063
		public override bool HandleEvent(IsElementVisibleEvent E)
		{
			return Trade.Visible;
		}

		// Token: 0x06007C1C RID: 31772 RVA: 0x002E9E6A File Offset: 0x002E806A
		public static bool CheckVisible()
		{
			return Trade.Visible = Trade.Enabled && ConversationUI.CurrentNode != null && ConversationUI.CurrentNode.AllowEscape && Trade.Choice != null && Trade.Choice.CheckPredicates();
		}

		// Token: 0x06007C1D RID: 31773 RVA: 0x002E9EA0 File Offset: 0x002E80A0
		public static bool CheckEnabled(bool Base = true, bool Physical = true, bool Mental = false)
		{
			Trade.Trader = The.Speaker;
			Trade.Enabled = true;
			if (Trade.Trader == null)
			{
				Trade.Enabled = false;
			}
			else if (!Base)
			{
				Trade.Enabled = false;
			}
			else
			{
				Trade.Enabled = Trade.CanTradeWith(Trade.Trader);
				GameObject transmitter = Conversation.Transmitter;
				if (!Trade.Enabled && Trade.CanTradeWith(transmitter))
				{
					Trade.Trader = transmitter;
					Trade.Enabled = true;
				}
				Trade.Enabled = CanTradeEvent.Check(The.Player, The.Speaker, Conversation.Transmitter, Conversation.Receiver, ref Trade.Trader, Conversation.Current, Trade.Enabled, Physical, Mental);
			}
			Trade.CheckVisible();
			return Trade.Enabled;
		}

		// Token: 0x06007C1E RID: 31774 RVA: 0x002E9F41 File Offset: 0x002E8141
		public static bool CanTradeWith(GameObject Object)
		{
			return Object != null && The.Player.PhaseMatches(Object) && !Object.HasTagOrProperty("NoTrade") && Object.InSameOrAdjacentCellTo(The.Player);
		}

		// Token: 0x06007C1F RID: 31775 RVA: 0x002E9F6D File Offset: 0x002E816D
		public static void Reset()
		{
			Trade.Trader = null;
			Trade.Choice = null;
			Trade.Enabled = true;
		}

		// Token: 0x06007C20 RID: 31776 RVA: 0x002E9F81 File Offset: 0x002E8181
		public static void ShowScreen()
		{
			TradeUI.ShowTradeScreen(Trade.Trader, 1f, TradeUI.TradeScreenMode.Trade);
		}

		// Token: 0x04003871 RID: 14449
		public static GameObject Trader = null;

		// Token: 0x04003872 RID: 14450
		public static Choice Choice = null;

		// Token: 0x04003873 RID: 14451
		public static bool Enabled = true;

		// Token: 0x04003874 RID: 14452
		public static bool Visible = true;
	}
}
