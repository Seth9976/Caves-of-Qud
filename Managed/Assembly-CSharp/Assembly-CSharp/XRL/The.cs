using System;
using System.Text;
using System.Threading;
using ConsoleLib.Console;
using XRL.Core;
using XRL.Help;
using XRL.UI;
using XRL.World;
using XRL.World.Conversations;

namespace XRL
{
	// Token: 0x020004FA RID: 1274
	public static class The
	{
		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x060036EB RID: 14059 RVA: 0x0010A1BC File Offset: 0x001083BC
		public static XRLCore Core
		{
			get
			{
				return XRLCore.Core;
			}
		}

		/// you just lost
		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x060036EC RID: 14060 RVA: 0x0010A1C3 File Offset: 0x001083C3
		public static XRLGame Game
		{
			get
			{
				XRLCore core = The.Core;
				if (core == null)
				{
					return null;
				}
				return core.Game;
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x060036ED RID: 14061 RVA: 0x0010A1D5 File Offset: 0x001083D5
		public static long CurrentTurn
		{
			get
			{
				return XRLCore.CurrentTurn;
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x060036EE RID: 14062 RVA: 0x0010A1DC File Offset: 0x001083DC
		public static GameObject Player
		{
			get
			{
				XRLGame game = The.Game;
				if (game == null)
				{
					return null;
				}
				GamePlayer player = game.Player;
				if (player == null)
				{
					return null;
				}
				return player.Body;
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x060036EF RID: 14063 RVA: 0x0010A1F9 File Offset: 0x001083F9
		public static Cell PlayerCell
		{
			get
			{
				GameObject player = The.Player;
				if (player == null)
				{
					return null;
				}
				return player.CurrentCell;
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x060036F0 RID: 14064 RVA: 0x0010A20B File Offset: 0x0010840B
		public static ParticleManager ParticleManager
		{
			get
			{
				return XRLCore.ParticleManager;
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x060036F1 RID: 14065 RVA: 0x0010A212 File Offset: 0x00108412
		public static ActionManager ActionManager
		{
			get
			{
				XRLGame game = The.Game;
				if (game == null)
				{
					return null;
				}
				return game.ActionManager;
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x060036F2 RID: 14066 RVA: 0x0010A224 File Offset: 0x00108424
		public static ZoneManager ZoneManager
		{
			get
			{
				XRLGame game = The.Game;
				if (game == null)
				{
					return null;
				}
				return game.ZoneManager;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x060036F3 RID: 14067 RVA: 0x0010A236 File Offset: 0x00108436
		public static Zone ActiveZone
		{
			get
			{
				ZoneManager zoneManager = The.ZoneManager;
				if (zoneManager == null)
				{
					return null;
				}
				return zoneManager.ActiveZone;
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x060036F4 RID: 14068 RVA: 0x0010A248 File Offset: 0x00108448
		public static Graveyard Graveyard
		{
			get
			{
				ZoneManager zoneManager = The.ZoneManager;
				if (zoneManager == null)
				{
					return null;
				}
				return zoneManager.Graveyard;
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x060036F5 RID: 14069 RVA: 0x0010A25A File Offset: 0x0010845A
		public static ColorUtility.ColorCollection Color
		{
			get
			{
				return ColorUtility.Colors;
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x060036F6 RID: 14070 RVA: 0x0010A261 File Offset: 0x00108461
		public static SynchronizationContext CurrentContext
		{
			get
			{
				return SynchronizationContext.Current;
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x060036F7 RID: 14071 RVA: 0x0010A268 File Offset: 0x00108468
		public static SynchronizationContext UiContext
		{
			get
			{
				return GameManager.Instance.uiSynchronizationContext;
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x060036F8 RID: 14072 RVA: 0x0010A274 File Offset: 0x00108474
		public static SynchronizationContext GameContext
		{
			get
			{
				return GameManager.Instance.gameThreadSynchronizationContext;
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x060036F9 RID: 14073 RVA: 0x0010A280 File Offset: 0x00108480
		public static GameObject Speaker
		{
			get
			{
				return ConversationUI.Speaker;
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x060036FA RID: 14074 RVA: 0x0010A287 File Offset: 0x00108487
		public static GameObject Listener
		{
			get
			{
				return ConversationUI.Listener;
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x060036FB RID: 14075 RVA: 0x0010A28E File Offset: 0x0010848E
		public static Conversation Conversation
		{
			get
			{
				return ConversationUI.CurrentConversation;
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x060036FC RID: 14076 RVA: 0x0010A295 File Offset: 0x00108495
		public static XRLManual Manual
		{
			get
			{
				return XRLCore.Manual;
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x060036FD RID: 14077 RVA: 0x0010A29C File Offset: 0x0010849C
		public static StringBuilder StringBuilder
		{
			get
			{
				The._stringBuilder.Clear();
				return The._stringBuilder;
			}
		}

		// Token: 0x04001B60 RID: 7008
		private static StringBuilder _stringBuilder = new StringBuilder(4096);
	}
}
