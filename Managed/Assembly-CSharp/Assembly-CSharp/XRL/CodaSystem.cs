using System;
using System.Threading;
using Qud.UI;
using XRL.UI;
using XRL.World;

namespace XRL
{
	// Token: 0x020004F0 RID: 1264
	[Serializable]
	public class CodaSystem : IPlayerSystem
	{
		// Token: 0x06003693 RID: 13971 RVA: 0x001077E5 File Offset: 0x001059E5
		public static bool InCoda()
		{
			return The.Game.GetSystem<CodaSystem>() != null;
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06003694 RID: 13972 RVA: 0x001077F6 File Offset: 0x001059F6
		public int EndYear
		{
			get
			{
				return Calendar.GetYear(this.EndTime);
			}
		}

		// Token: 0x06003695 RID: 13973 RVA: 0x00107803 File Offset: 0x00105A03
		public override void Write(SerializationWriter Writer)
		{
			Writer.WriteGameObject(this.Sultan);
			Writer.WriteOptimized(this.EndTime);
		}

		// Token: 0x06003696 RID: 13974 RVA: 0x0010781D File Offset: 0x00105A1D
		public override void Read(SerializationReader Reader)
		{
			this.Sultan = Reader.ReadGameObject(null);
			this.EndTime = Reader.ReadOptimizedInt64();
		}

		// Token: 0x06003697 RID: 13975 RVA: 0x00107838 File Offset: 0x00105A38
		public override void Register(XRLGame Game, IEventRegistrar Registrar)
		{
			Registrar.Register(AwardingXPEvent.ID, 0, false);
		}

		// Token: 0x06003698 RID: 13976 RVA: 0x00107847 File Offset: 0x00105A47
		public override void RegisterPlayer(GameObject Player, IEventRegistrar Registrar)
		{
			Registrar.Register(EnteringZoneEvent.ID, 0, false);
		}

		// Token: 0x06003699 RID: 13977 RVA: 0x00107856 File Offset: 0x00105A56
		public override bool HandleEvent(AwardingXPEvent E)
		{
			return false;
		}

		// Token: 0x0600369A RID: 13978 RVA: 0x0010785C File Offset: 0x00105A5C
		public override bool HandleEvent(EnteringZoneEvent E)
		{
			InteriorZone interiorZone = E.Cell.ParentZone as InteriorZone;
			if (interiorZone != null && interiorZone.Schema == "Coda" && interiorZone.X == 1 && interiorZone.Y == 1)
			{
				return true;
			}
			CodaSystem.EndGamePrompt();
			return false;
		}

		// Token: 0x0600369B RID: 13979 RVA: 0x001078AC File Offset: 0x00105AAC
		public static void EndGamePrompt()
		{
			string text = "END GAME";
			if (Popup.AskString("Leaving the village will end the game.\n\nType " + text + " to confirm.", "", "Sounds/UI/ui_notification", null, text, text.Length, 0, false, true, new bool?(false)).ToUpper() == text)
			{
				SoundManager.StopMusic("music", true, 3f);
				if (The.Game.DontSaveThisIsAReplay)
				{
					The.Game.DeathReason = "<nodeath>";
					The.Game.DeathCategory = "exit";
					The.Game.Running = false;
					return;
				}
				FadeToBlackHigh.FadeOut(10f, false);
				Thread.Sleep(3000);
				SoundManager.PlayMusic("Music/CodaOut", "music", true, 2f, 1f, null, true);
				Thread.Sleep(7000);
				The.Game.DeathReason = "You ended the game.";
				The.Game.Running = false;
			}
		}

		// Token: 0x04001B49 RID: 6985
		[NonSerialized]
		public GameObject Sultan;

		// Token: 0x04001B4A RID: 6986
		[NonSerialized]
		public long EndTime;
	}
}
