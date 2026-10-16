using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Platform.IO;

namespace XRL.Core
{
	// Token: 0x02000639 RID: 1593
	public class Scoreboard2
	{
		// Token: 0x06004294 RID: 17044 RVA: 0x00160314 File Offset: 0x0015E514
		public void Sort()
		{
			this.Scores.Sort(this.sorter);
		}

		// Token: 0x06004295 RID: 17045 RVA: 0x00160328 File Offset: 0x0015E528
		public void Add(int Score, string Details, long Turn, string GameId, string GameMode, bool ReplaceOnId = false, int Level = 0, string Name = "")
		{
			if (ReplaceOnId && !string.IsNullOrEmpty(GameId))
			{
				if (this.Scores.Any<ScoreEntry2>((ScoreEntry2 e) => e.GameId == GameId))
				{
					if (this.Scores.Where<ScoreEntry2>((ScoreEntry2 e) => e.GameId == GameId).First<ScoreEntry2>().Score < Score)
					{
						this.Scores.RemoveAll((ScoreEntry2 e) => e.GameId == GameId);
						this.Scores.Add(new ScoreEntry2(Score, Details, Turn, GameId, GameMode, Level, Name));
					}
				}
				else
				{
					this.Scores.Add(new ScoreEntry2(Score, Details, Turn, GameId, GameMode, Level, Name));
				}
			}
			else
			{
				this.Scores.Add(new ScoreEntry2(Score, Details, Turn, GameId, GameMode, Level, Name));
			}
			this.Save().Wait();
		}

		// Token: 0x06004296 RID: 17046 RVA: 0x0016041C File Offset: 0x0015E61C
		public async Task Save()
		{
			try
			{
				this.Sort();
				await Platform.IO.File.WriteAllJsonAsync(DataManager.SyncedPath("HighScores.json"), this, null);
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("Scoreboard Save", ex, "game_exception");
			}
		}

		// Token: 0x06004297 RID: 17047 RVA: 0x00160460 File Offset: 0x0015E660
		public static async Task UpgradeScoreIfNecessary()
		{
			string oldSavePath = DataManager.LocalPath("HighScores.dat");
			TaskAwaiter<bool> taskAwaiter = Platform.IO.File.ExistsAsync(DataManager.SyncedPath("HighScores.json")).GetAwaiter();
			TaskAwaiter<bool> taskAwaiter2;
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<bool>);
			}
			if (!taskAwaiter.GetResult())
			{
				taskAwaiter = Platform.IO.File.ExistsAsync(oldSavePath).GetAwaiter();
				if (!taskAwaiter.IsCompleted)
				{
					await taskAwaiter;
					taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter<bool>);
				}
				if (taskAwaiter.GetResult())
				{
					LoadBytesResult loadBytesResult = await Blob.ReadAllBytesAsync(oldSavePath);
					if (loadBytesResult.LogErrorIfFailed().WasSuccessful())
					{
						using (MemoryStream oldLoadStream = new MemoryStream(loadBytesResult.content))
						{
							Scoreboard scoreboard = ((IFormatter)new BinaryFormatter()).Deserialize(oldLoadStream) as Scoreboard;
							Scoreboard2 scoreboard2 = new Scoreboard2();
							foreach (ScoreEntry scoreEntry in scoreboard.Scores)
							{
								scoreboard2.Scores.Add(new ScoreEntry2(scoreEntry));
							}
							await scoreboard2.Save();
						}
					}
				}
			}
		}

		// Token: 0x06004298 RID: 17048 RVA: 0x0016049C File Offset: 0x0015E69C
		public static async Task<Scoreboard2> Load()
		{
			await Scoreboard2.UpgradeScoreIfNecessary();
			Scoreboard2 result = null;
			try
			{
				TaskAwaiter<bool> taskAwaiter = Platform.IO.File.ExistsAsync(DataManager.SyncedPath("HighScores.json")).GetAwaiter();
				if (!taskAwaiter.IsCompleted)
				{
					await taskAwaiter;
					TaskAwaiter<bool> taskAwaiter2;
					taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter<bool>);
				}
				if (taskAwaiter.GetResult())
				{
					result = await Platform.IO.File.ReadAllJsonAsync<Scoreboard2>(DataManager.SyncedPath("HighScores.json"), null);
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("Exception loading high scores", ex, "game_exception");
				result = new Scoreboard2();
			}
			if (result == null)
			{
				result = new Scoreboard2();
			}
			if (result.Scores == null)
			{
				result.Scores = new List<ScoreEntry2>();
			}
			result.Scores.RemoveAll((ScoreEntry2 s) => s == null);
			return result;
		}

		// Token: 0x040020ED RID: 8429
		private SortScoreEntry2 sorter = new SortScoreEntry2();

		// Token: 0x040020EE RID: 8430
		public List<ScoreEntry2> Scores = new List<ScoreEntry2>();
	}
}
