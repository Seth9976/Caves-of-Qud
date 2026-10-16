using System;
using System.Collections.Generic;
using Qud.API;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x02000882 RID: 2178
	[Serializable]
	public class PlayerMuralGameState : IGameStateSingleton, IComposite
	{
		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x0600565B RID: 22107 RVA: 0x0019AE48 File Offset: 0x00199048
		public static PlayerMuralGameState Instance
		{
			get
			{
				if (PlayerMuralGameState._Instance == null)
				{
					PlayerMuralGameState._Instance = (PlayerMuralGameState)The.Game.GetObjectGameState("PlayerMuralGameState");
					if (PlayerMuralGameState._Instance == null)
					{
						PlayerMuralGameState._Instance = new PlayerMuralGameState();
						The.Game.SetObjectGameState("PlayerMuralGameState", PlayerMuralGameState._Instance);
					}
				}
				return PlayerMuralGameState._Instance;
			}
		}

		// Token: 0x0600565C RID: 22108 RVA: 0x0019AEA0 File Offset: 0x001990A0
		public List<JournalAccomplishment> GetAccomplishments()
		{
			PlayerMuralController playerMuralController;
			if (this.Accomplishments.IsNullOrEmpty<JournalAccomplishment>() && The.ZoneManager.GetZone("JoppaWorld.53.3.1.0.0").FindObject("PlayerMuralController").TryGetPart<PlayerMuralController>(out playerMuralController))
			{
				playerMuralController.initializeMurals();
				this.Accomplishments.AddRange(playerMuralController.playerMuralEventList);
			}
			return this.Accomplishments;
		}

		// Token: 0x0600565D RID: 22109 RVA: 0x0019AEF9 File Offset: 0x001990F9
		public void SetAccomplishments(IReadOnlyList<JournalAccomplishment> Accomplishments)
		{
			this.Accomplishments.Clear();
			this.Accomplishments.AddRange(Accomplishments);
		}

		// Token: 0x0600565E RID: 22110 RVA: 0x0019AF14 File Offset: 0x00199114
		public void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.Accomplishments.Count);
			foreach (JournalAccomplishment journalAccomplishment in this.Accomplishments)
			{
				Writer.Write(journalAccomplishment);
			}
		}

		// Token: 0x0600565F RID: 22111 RVA: 0x0019AF78 File Offset: 0x00199178
		public void Read(SerializationReader Reader)
		{
			int num = Reader.ReadOptimizedInt32();
			this.Accomplishments.EnsureCapacity<JournalAccomplishment>(num);
			for (int i = 0; i < num; i++)
			{
				this.Accomplishments.Add((JournalAccomplishment)Reader.ReadComposite());
			}
		}

		// Token: 0x04002B83 RID: 11139
		public const string ID = "PlayerMuralGameState";

		// Token: 0x04002B84 RID: 11140
		public const string CONTROLLER_ID = "JoppaWorld.53.3.1.0.0";

		// Token: 0x04002B85 RID: 11141
		private static PlayerMuralGameState _Instance;

		// Token: 0x04002B86 RID: 11142
		private List<JournalAccomplishment> Accomplishments = new List<JournalAccomplishment>();
	}
}
