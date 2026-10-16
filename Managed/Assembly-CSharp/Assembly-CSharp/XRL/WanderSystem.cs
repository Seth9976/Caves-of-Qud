using System;
using Qud.API;
using XRL.Core;
using XRL.World;
using XRL.World.Parts;

namespace XRL
{
	// Token: 0x020004F9 RID: 1273
	[Serializable]
	public class WanderSystem : IPlayerSystem
	{
		// Token: 0x060036E0 RID: 14048 RVA: 0x00109EF8 File Offset: 0x001080F8
		public static bool WanderEnabled()
		{
			XRLGame game = XRLCore.Core.Game;
			return game.GetStringGameState("GameMode", "") == "Wander" || game.GetStringGameState("WanderEnabled", "") == "yes";
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x060036E1 RID: 14049 RVA: 0x00109F48 File Offset: 0x00108148
		public static int WXU
		{
			get
			{
				int level = The.Player.Level;
				int num = Leveler.GetXPForLevel(level + 1) - Leveler.GetXPForLevel(level);
				if (level <= 2)
				{
					num /= 2;
				}
				else if (level <= 5)
				{
					num /= 3;
				}
				else if (level <= 10)
				{
					num /= 5;
				}
				else if (level <= 20)
				{
					num /= 6;
				}
				else if (level <= 30)
				{
					num /= 8;
				}
				else
				{
					num /= 10;
				}
				num = (int)Math.Round((double)((float)num / 10f)) * 10;
				return Math.Max(10, num);
			}
		}

		// Token: 0x060036E2 RID: 14050 RVA: 0x00109FC8 File Offset: 0x001081C8
		public static int AwardWXU(int n)
		{
			if (The.Player == null)
			{
				return 0;
			}
			if (WanderSystem.WanderEnabled())
			{
				int num = WanderSystem.WXU * n;
				The.Player.AwardXP(num, -1, 0, int.MaxValue, null, null, null, null, null, null);
				return num;
			}
			return 0;
		}

		// Token: 0x060036E3 RID: 14051 RVA: 0x0010A009 File Offset: 0x00108209
		public override void Register(XRLGame Game, IEventRegistrar Registrar)
		{
			Registrar.Register(SingletonEvent<EmbarkEvent>.ID, 0, false);
			Registrar.Register(QuestFinishedEvent.ID, 0, false);
			Registrar.Register(PooledEvent<WaterRitualStartEvent>.ID, 0, false);
			Registrar.Register(PooledEvent<SecretVisibilityChangedEvent>.ID, 0, false);
		}

		// Token: 0x060036E4 RID: 14052 RVA: 0x0010A03F File Offset: 0x0010823F
		public override void RegisterPlayer(GameObject Player, IEventRegistrar Registrar)
		{
			Registrar.Register(AwardingXPEvent.ID, 0, false);
		}

		// Token: 0x060036E5 RID: 14053 RVA: 0x0010A050 File Offset: 0x00108250
		public override bool HandleEvent(EmbarkEvent E)
		{
			if (E.EventID == "BootGame" && WanderSystem.WanderEnabled())
			{
				foreach (Faction faction in Factions.Loop())
				{
					if (The.Game.PlayerReputation.Get(faction) < 0 && !faction.HatesPlayer)
					{
						The.Game.PlayerReputation.Set(faction, 0);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060036E6 RID: 14054 RVA: 0x0010A0E4 File Offset: 0x001082E4
		public override bool HandleEvent(QuestFinishedEvent E)
		{
			if (WanderSystem.WanderEnabled())
			{
				WanderSystem.AwardWXU(2);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060036E7 RID: 14055 RVA: 0x0010A0FB File Offset: 0x001082FB
		public override bool HandleEvent(AwardingXPEvent E)
		{
			if (WanderSystem.WanderEnabled() && E.Kill != null)
			{
				E.Amount = 0;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060036E8 RID: 14056 RVA: 0x0010A11C File Offset: 0x0010831C
		public override bool HandleEvent(SecretVisibilityChangedEvent E)
		{
			if (WanderSystem.WanderEnabled())
			{
				JournalMapNote journalMapNote = E.Entry as JournalMapNote;
				if (journalMapNote != null && journalMapNote.Revealed && journalMapNote.Tradable)
				{
					WanderSystem.AwardWXU(1);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060036E9 RID: 14057 RVA: 0x0010A160 File Offset: 0x00108360
		public override bool HandleEvent(WaterRitualStartEvent E)
		{
			if (WanderSystem.WanderEnabled() && GameObject.Validate(E.SpeakingWith) && !E.SpeakingWith.HasIntProperty("WaterRitualWXU"))
			{
				E.SpeakingWith.SetIntProperty("WaterRitualWXU", 1, false);
				WanderSystem.AwardWXU(1);
			}
			return base.HandleEvent(E);
		}
	}
}
