using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Platform.IO;

namespace XRL.Core
{
	// Token: 0x02000637 RID: 1591
	public class ScoreEntry2
	{
		// Token: 0x0600428B RID: 17035 RVA: 0x00160090 File Offset: 0x0015E290
		public ScoreEntry2()
		{
		}

		// Token: 0x0600428C RID: 17036 RVA: 0x00160098 File Offset: 0x0015E298
		[Obsolete]
		public ScoreEntry2(ScoreEntry legacy)
		{
			this.Score = legacy.Score;
			this.Details = legacy.Details;
			this.Turns = 0L;
			this.GameId = "";
			this.GameMode = "Classic";
		}

		// Token: 0x0600428D RID: 17037 RVA: 0x001600D8 File Offset: 0x0015E2D8
		public ScoreEntry2(int _Score, string _Details, long Turns, string GameId, string GameMode, int Level, string Name)
		{
			this.Score = _Score;
			this.Details = _Details;
			this.Turns = Turns;
			this.GameId = GameId;
			this.GameMode = GameMode;
			this.Level = Level;
			this.Name = Name;
			this.Version = 2;
		}

		// Token: 0x0600428E RID: 17038 RVA: 0x00160128 File Offset: 0x0015E328
		public void CheckVersion()
		{
			if (this.Version < 2)
			{
				Match match = ScoreEntry2.RE_Level.Match(this.Details);
				if (match.Success)
				{
					this.Level = Convert.ToInt32(match.Groups["Level"].Value);
				}
				match = ScoreEntry2.RE_Turns.Match(this.Details);
				if (match.Success)
				{
					this.Turns = (long)Convert.ToInt32(match.Groups["Turns"].Value);
				}
				match = ScoreEntry2.RE_Name.Match(this.Details);
				if (match.Success)
				{
					this.Name = match.Groups["Name"].Value;
				}
				match = ScoreEntry2.RE_Mode.Match(this.Details);
				if (match.Success)
				{
					this.GameMode = match.Groups["Mode"].Value;
				}
				this.Version = 2;
			}
		}

		// Token: 0x0600428F RID: 17039 RVA: 0x00160223 File Offset: 0x0015E423
		public bool HasCoda()
		{
			return File.Exists(DataManager.SyncedPath("Codas/coda-" + this.GameId + ".sav.gz"));
		}

		// Token: 0x06004290 RID: 17040 RVA: 0x00160244 File Offset: 0x0015E444
		public async Task<XRLGame> LoadCoda()
		{
			if (File.Exists(DataManager.SyncedPath("Codas/coda-" + this.GameId.ToLower() + ".sav.gz")))
			{
				XRLGame xrlgame = await XRLGame.LoadGame(DataManager.SyncedPath("Codas/coda-" + this.GameId.ToLower()), false, false, null);
				if (xrlgame != null)
				{
					xrlgame.DontSaveThisIsAReplay = true;
					return xrlgame;
				}
			}
			return null;
		}

		// Token: 0x06004291 RID: 17041 RVA: 0x00160287 File Offset: 0x0015E487
		public void DeleteCoda()
		{
			File.Delete(DataManager.SyncedPath("Codas/coda-" + this.GameId.ToLower() + ".sav.gz"));
		}

		// Token: 0x040020E0 RID: 8416
		public int Score;

		// Token: 0x040020E1 RID: 8417
		public string Details;

		// Token: 0x040020E2 RID: 8418
		public long Turns;

		// Token: 0x040020E3 RID: 8419
		public string GameId;

		// Token: 0x040020E4 RID: 8420
		public string GameMode;

		// Token: 0x040020E5 RID: 8421
		public string Name;

		// Token: 0x040020E6 RID: 8422
		public int Level;

		// Token: 0x040020E7 RID: 8423
		public int Version;

		// Token: 0x040020E8 RID: 8424
		public static Regex RE_Level = new Regex("You were level \\{\\{C\\|(?<Level>\\d+)", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		// Token: 0x040020E9 RID: 8425
		public static Regex RE_Turns = new Regex("You survived for \\{\\{C\\|(?<Turns>\\d+)", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		// Token: 0x040020EA RID: 8426
		public static Regex RE_Name = new Regex("\\{\\{C\\|.\\}\\} Game summary for \\{\\{W\\|(?<Name>[^\\n]*)\\}\\} \\{\\{C\\|.\\}\\}", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		// Token: 0x040020EB RID: 8427
		public static Regex RE_Mode = new Regex("This game was played in (?<Mode>.+?) mode.", RegexOptions.ExplicitCapture | RegexOptions.Compiled);
	}
}
