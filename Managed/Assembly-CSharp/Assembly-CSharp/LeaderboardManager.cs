using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Steamworks;
using UnityEngine;
using XRL.Core;

// Token: 0x02000064 RID: 100
public class LeaderboardManager : MonoBehaviour
{
	// Token: 0x06000367 RID: 871 RVA: 0x00016AFC File Offset: 0x00014CFC
	private static void CompletePendingRequest(LeaderboardScores scores)
	{
		Action<LeaderboardScores> action = LeaderboardManager.leaderboardEntriesCallback;
		LeaderboardManager.leaderboardEntriesCallback = null;
		LeaderboardManager.currentRequest = null;
		if (action != null)
		{
			try
			{
				action(scores);
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
			}
		}
	}

	// Token: 0x06000368 RID: 872 RVA: 0x00016B40 File Offset: 0x00014D40
	private static void OnLeaderboardFindResult(LeaderboardFindResult_t pCallback, bool failure)
	{
		try
		{
			if (failure)
			{
				Debug.LogError("STEAM LEADERBOARDS: find request failed");
				LeaderboardManager.CompletePendingRequest(LeaderboardScores.CreateCancelledLeaderboardScores());
			}
			else
			{
				Debug.Log("STEAM LEADERBOARDS: Found - " + pCallback.m_bLeaderboardFound.ToString() + " leaderboardID - " + pCallback.m_hSteamLeaderboard.m_SteamLeaderboard.ToString());
				LeaderboardManager.currentLeaderboard = pCallback.m_hSteamLeaderboard;
				if (pCallback.m_bLeaderboardFound != 0)
				{
					if (LeaderboardManager.leaderboardID.ContainsKey(LeaderboardManager.currentRequest.id))
					{
						LeaderboardManager.leaderboardID[LeaderboardManager.currentRequest.id] = pCallback.m_hSteamLeaderboard;
					}
					else
					{
						LeaderboardManager.leaderboardID.Add(LeaderboardManager.currentRequest.id, pCallback.m_hSteamLeaderboard);
					}
					if (LeaderboardManager.currentRequest.type == "getleaderboard")
					{
						ELeaderboardDataRequest eleaderboardDataRequest = (LeaderboardManager.currentRequest.friendsonly ? ELeaderboardDataRequest.k_ELeaderboardDataRequestFriends : ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal);
						int num = Math.Max(1, LeaderboardManager.currentRequest.requestTop);
						int num2 = num + LeaderboardManager.currentRequest.numRecords;
						SteamAPICall_t steamAPICall_t = SteamUserStats.DownloadLeaderboardEntries(LeaderboardManager.currentLeaderboard, eleaderboardDataRequest, num, num2);
						LeaderboardManager.m_downloadResult = new CallResult<LeaderboardScoresDownloaded_t>(null);
						LeaderboardManager.m_downloadResult.Set(steamAPICall_t, new CallResult<LeaderboardScoresDownloaded_t>.APIDispatchDelegate(LeaderboardManager.OnDownloadLeaderboardEntries));
					}
					else if (LeaderboardManager.currentRequest.type == "submitleaderboardscore")
					{
						SteamAPICall_t steamAPICall_t2 = SteamUserStats.UploadLeaderboardScore(LeaderboardManager.currentLeaderboard, ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodKeepBest, LeaderboardManager.currentRequest.score, null, 0);
						LeaderboardManager.m_uploadResult = new CallResult<LeaderboardScoreUploaded_t>(null);
						LeaderboardManager.m_uploadResult.Set(steamAPICall_t2, new CallResult<LeaderboardScoreUploaded_t>.APIDispatchDelegate(LeaderboardManager.OnLeaderboardUploadResult));
					}
					else if (LeaderboardManager.currentRequest.type == "getleaderboardrank")
					{
						SteamAPICall_t steamAPICall_t3 = SteamUserStats.DownloadLeaderboardEntries(LeaderboardManager.currentLeaderboard, ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobalAroundUser, -4, 5);
						LeaderboardManager.m_downloadResult = new CallResult<LeaderboardScoresDownloaded_t>(null);
						LeaderboardManager.m_downloadResult.Set(steamAPICall_t3, new CallResult<LeaderboardScoresDownloaded_t>.APIDispatchDelegate(LeaderboardManager.OnDownloadLeaderboardEntries));
					}
					else
					{
						LeaderboardManager.CompletePendingRequest(LeaderboardScores.CreateCancelledLeaderboardScores());
					}
				}
				else
				{
					Debug.LogError("Couldn't find leaderboard");
					LeaderboardManager.CompletePendingRequest(LeaderboardScores.CreateCancelledLeaderboardScores());
				}
			}
		}
		catch (Exception ex)
		{
			XRLCore.LogError("OnLeaderboardFindResult", ex);
			LeaderboardManager.CompletePendingRequest(LeaderboardScores.CreateCancelledLeaderboardScores());
		}
	}

	// Token: 0x06000369 RID: 873 RVA: 0x00016D70 File Offset: 0x00014F70
	private static void OnDownloadLeaderboardEntries(LeaderboardScoresDownloaded_t pCallback, bool failure)
	{
		if (failure)
		{
			Debug.LogError("STEAM LEADERBOARDS: download request failed");
			LeaderboardManager.CompletePendingRequest(LeaderboardScores.CreateCancelledLeaderboardScores());
			return;
		}
		try
		{
			LeaderboardManager.DownloadUserData(pCallback);
			LeaderboardManager.CompletePendingRequest(new LeaderboardScores(pCallback));
		}
		catch (Exception ex)
		{
			Debug.LogException(ex);
			LeaderboardManager.CompletePendingRequest(LeaderboardScores.CreateCancelledLeaderboardScores());
		}
	}

	// Token: 0x0600036A RID: 874 RVA: 0x00016DCC File Offset: 0x00014FCC
	private static void DownloadUserData(LeaderboardScoresDownloaded_t pCallback)
	{
		if (LeaderboardManager.m_personaStateChange == null)
		{
			LeaderboardManager.m_personaStateChange = Callback<PersonaStateChange_t>.Create(new Callback<PersonaStateChange_t>.DispatchDelegate(LeaderboardManager.OnPersonaStateChange));
		}
		for (int i = 0; i < pCallback.m_cEntryCount; i++)
		{
			LeaderboardEntry_t leaderboardEntry_t;
			TaskCompletionSource<string> taskCompletionSource;
			if (SteamUserStats.GetDownloadedLeaderboardEntry(pCallback.m_hSteamLeaderboardEntries, i, out leaderboardEntry_t, null, 0) && !LeaderboardManager.nameCache.TryGetValue(leaderboardEntry_t.m_steamIDUser.m_SteamID, out taskCompletionSource))
			{
				taskCompletionSource = new TaskCompletionSource<string>();
				LeaderboardManager.nameCache.Add(leaderboardEntry_t.m_steamIDUser.m_SteamID, taskCompletionSource);
				if (!SteamFriends.RequestUserInformation(leaderboardEntry_t.m_steamIDUser, true))
				{
					taskCompletionSource.TrySetResult(SteamFriends.GetFriendPersonaName(leaderboardEntry_t.m_steamIDUser));
				}
			}
		}
	}

	// Token: 0x0600036B RID: 875 RVA: 0x00016E70 File Offset: 0x00015070
	private static void OnPersonaStateChange(PersonaStateChange_t pCallback)
	{
		TaskCompletionSource<string> taskCompletionSource;
		if (LeaderboardManager.nameCache.TryGetValue(pCallback.m_ulSteamID, out taskCompletionSource) && !taskCompletionSource.Task.IsCompleted)
		{
			CSteamID csteamID = new CSteamID(pCallback.m_ulSteamID);
			taskCompletionSource.TrySetResult(SteamFriends.GetFriendPersonaName(csteamID));
		}
	}

	// Token: 0x0600036C RID: 876 RVA: 0x00016EB8 File Offset: 0x000150B8
	private static void OnLeaderboardUploadResult(LeaderboardScoreUploaded_t pCallback, bool failure)
	{
		try
		{
			if (failure)
			{
				Debug.LogError("STEAM LEADERBOARDS: upload request failed");
				LeaderboardManager.CompletePendingRequest(LeaderboardScores.CreateCancelledLeaderboardScores());
			}
			else
			{
				Debug.Log(string.Concat(new string[]
				{
					"STEAM LEADERBOARDS: Completed - ",
					pCallback.m_bSuccess.ToString(),
					" NewScore: ",
					pCallback.m_nGlobalRankNew.ToString(),
					" Score ",
					pCallback.m_nScore.ToString(),
					" HasChanged - ",
					pCallback.m_bScoreChanged.ToString()
				}));
				SteamAPICall_t steamAPICall_t = SteamUserStats.DownloadLeaderboardEntries(LeaderboardManager.currentLeaderboard, ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobalAroundUser, -4, 5);
				LeaderboardManager.m_downloadResult = new CallResult<LeaderboardScoresDownloaded_t>(null);
				LeaderboardManager.m_downloadResult.Set(steamAPICall_t, new CallResult<LeaderboardScoresDownloaded_t>.APIDispatchDelegate(LeaderboardManager.OnDownloadLeaderboardEntries));
			}
		}
		catch (Exception ex)
		{
			XRLCore.LogError("OnLeaderboardUploadResult", ex);
			LeaderboardManager.CompletePendingRequest(LeaderboardScores.CreateCancelledLeaderboardScores());
		}
	}

	// Token: 0x0600036D RID: 877 RVA: 0x00016FA8 File Offset: 0x000151A8
	public void Update()
	{
		if (LeaderboardManager.requests.Count > 0 && LeaderboardManager.currentRequest == null)
		{
			Queue<LeaderboardManager.LeaderboardRecordRequest> queue = LeaderboardManager.requests;
			lock (queue)
			{
				LeaderboardManager.currentRequest = LeaderboardManager.requests.Dequeue();
				LeaderboardManager.leaderboardEntriesCallback = LeaderboardManager.currentRequest.scoreCallback;
				LeaderboardManager.FindOrCreateLeaderboard(LeaderboardManager.currentRequest.id);
			}
		}
	}

	// Token: 0x0600036E RID: 878 RVA: 0x00017024 File Offset: 0x00015224
	public static void FindOrCreateLeaderboard(string id)
	{
		try
		{
			string[] array = id.Split(':', StringSplitOptions.None);
			string text = "unknown " + id;
			if (array.Length >= 3 && array[0] == "weekly")
			{
				text = string.Concat(new string[]
				{
					array[0],
					" for week ",
					array[2],
					" of ",
					array[1]
				});
			}
			else if (array.Length >= 3 && array[0] == "daily")
			{
				text = string.Concat(new string[]
				{
					array[0],
					" for day ",
					array[2],
					" of ",
					array[1]
				});
			}
			SteamAPICall_t steamAPICall_t = SteamUserStats.FindOrCreateLeaderboard(text, ELeaderboardSortMethod.k_ELeaderboardSortMethodDescending, ELeaderboardDisplayType.k_ELeaderboardDisplayTypeNumeric);
			LeaderboardManager.m_findResult = new CallResult<LeaderboardFindResult_t>(null);
			LeaderboardManager.m_findResult.Set(steamAPICall_t, new CallResult<LeaderboardFindResult_t>.APIDispatchDelegate(LeaderboardManager.OnLeaderboardFindResult));
		}
		catch (Exception ex)
		{
			XRLCore.LogError("FindOrCreateLeaderboard", ex);
			LeaderboardManager.CompletePendingRequest(LeaderboardScores.CreateCancelledLeaderboardScores());
		}
	}

	// Token: 0x0600036F RID: 879 RVA: 0x00017124 File Offset: 0x00015324
	public static Task<PlatformManager.TryGetNetworkResult> TryGoOnline()
	{
		return LeaderboardManager.TryGoOnlineInternal();
	}

	// Token: 0x06000370 RID: 880 RVA: 0x0001712B File Offset: 0x0001532B
	private static Task<PlatformManager.TryGetNetworkResult> TryGoOnlineInternal()
	{
		return Task.FromResult<PlatformManager.TryGetNetworkResult>(PlatformManager.Steam.Initialized ? PlatformManager.TryGetNetworkResult.Success : PlatformManager.TryGetNetworkResult.Failure);
	}

	// Token: 0x06000371 RID: 881 RVA: 0x00017142 File Offset: 0x00015342
	public static void TryGoOnline(Action<bool> onComplete)
	{
		if (onComplete != null)
		{
			onComplete(false);
		}
	}

	// Token: 0x06000372 RID: 882 RVA: 0x00017150 File Offset: 0x00015350
	public static string SubmitResult(string id, int score, Action<LeaderboardScores> callback)
	{
		string[] array = id.Split(':', StringSplitOptions.None);
		string leaderboardID = "unknown " + id;
		if (array[0] == "weekly")
		{
			leaderboardID = string.Concat(new string[]
			{
				array[0],
				" for week ",
				array[2],
				" of ",
				array[1]
			});
		}
		if (array[0] == "daily")
		{
			leaderboardID = string.Concat(new string[]
			{
				array[0],
				" for day ",
				array[2],
				" of ",
				array[1]
			});
		}
		PlatformManager.TryGetLeaderboardsReady(delegate(bool isReady)
		{
			if (isReady)
			{
				Queue<LeaderboardManager.LeaderboardRecordRequest> queue = LeaderboardManager.requests;
				lock (queue)
				{
					Debug.Log(string.Concat(new string[]
					{
						"uploading score(",
						score.ToString(),
						") to steam leaderboard(",
						leaderboardID,
						")"
					}));
					LeaderboardManager.LeaderboardRecordRequest leaderboardRecordRequest = new LeaderboardManager.LeaderboardRecordRequest();
					leaderboardRecordRequest.type = "submitleaderboardscore";
					leaderboardRecordRequest.id = id;
					leaderboardRecordRequest.score = score;
					leaderboardRecordRequest.scoreCallback = callback;
					LeaderboardManager.requests.Enqueue(leaderboardRecordRequest);
				}
				return;
			}
			Debug.Log("Can't upload to the leaderboard because Steam isn't connected.");
			Action<LeaderboardScores> callback2 = callback;
			if (callback2 == null)
			{
				return;
			}
			callback2(null);
		});
		return leaderboardID;
	}

	// Token: 0x06000373 RID: 883 RVA: 0x00017238 File Offset: 0x00015438
	public static string GetDailyID(int daysAgo)
	{
		CultureInfo cultureInfo = new CultureInfo("en-US");
		DateTime dateTime = DateTime.Now - new TimeSpan(daysAgo, 0, 0, 0, 0);
		if (dateTime > DateTime.Now)
		{
			dateTime = DateTime.Now;
		}
		if (dateTime < new DateTime(2016, 11, 1))
		{
			dateTime = new DateTime(2016, 11, 1);
		}
		int dayOfYear = cultureInfo.Calendar.GetDayOfYear(dateTime);
		return "daily:" + dateTime.Year.ToString() + ":" + dayOfYear.ToString();
	}

	// Token: 0x06000374 RID: 884 RVA: 0x000172CC File Offset: 0x000154CC
	public static string GetLeaderboardName(string id)
	{
		string[] array = id.Split(':', StringSplitOptions.None);
		string text = "unknown " + id;
		if (array[0] == "weekly")
		{
			text = string.Concat(new string[]
			{
				array[0],
				" for week ",
				array[2],
				" of ",
				array[1]
			});
		}
		if (array[0] == "daily")
		{
			text = string.Concat(new string[]
			{
				array[0],
				" for day ",
				array[2],
				" of ",
				array[1]
			});
		}
		return text;
	}

	// Token: 0x06000375 RID: 885 RVA: 0x0001736C File Offset: 0x0001556C
	public static async Task<LeaderboardScores> GetLeaderboardAsync(string id, int requestTop, int numRecords, bool friendsOnly, CancellationToken? ct = null)
	{
		TaskAwaiter<PlatformManager.TryGetNetworkResult> taskAwaiter = PlatformManager.TryGetLeaderboardsReady().GetAwaiter();
		if (!taskAwaiter.IsCompleted)
		{
			await taskAwaiter;
			TaskAwaiter<PlatformManager.TryGetNetworkResult> taskAwaiter2;
			taskAwaiter = taskAwaiter2;
			taskAwaiter2 = default(TaskAwaiter<PlatformManager.TryGetNetworkResult>);
		}
		switch (taskAwaiter.GetResult())
		{
		case PlatformManager.TryGetNetworkResult.Success:
		{
			if (ct != null)
			{
				ct.GetValueOrDefault().ThrowIfCancellationRequested();
			}
			LeaderboardScores leaderboardScores = await LeaderboardManager.GetLeaderboardAsyncInternal(id, requestTop, numRecords, friendsOnly);
			if (ct != null)
			{
				ct.GetValueOrDefault().ThrowIfCancellationRequested();
			}
			return leaderboardScores;
		}
		case PlatformManager.TryGetNetworkResult.Cancelled:
			return LeaderboardScores.CreateCancelledLeaderboardScores();
		}
		return null;
	}

	// Token: 0x06000376 RID: 886 RVA: 0x000173D0 File Offset: 0x000155D0
	private static Task<LeaderboardScores> GetLeaderboardAsyncInternal(string id, int requestTop, int numRecords, bool friendsOnly)
	{
		TaskCompletionSource<LeaderboardScores> result = new TaskCompletionSource<LeaderboardScores>();
		if (PlatformManager.AreLeaderboardsOffline())
		{
			result.TrySetException(new Exception("Not connected to Steam API"));
		}
		else
		{
			LeaderboardManager.GetLeaderboard(id, requestTop, numRecords, friendsOnly, delegate(LeaderboardScores r)
			{
				result.TrySetResult(r);
			});
		}
		return result.Task;
	}

	// Token: 0x06000377 RID: 887 RVA: 0x00017430 File Offset: 0x00015630
	public static async Task<string> FriendNameAsync(LeaderboardEntry entry)
	{
		return await LeaderboardManager.FriendNameAsyncInternal(entry);
	}

	// Token: 0x06000378 RID: 888 RVA: 0x00017474 File Offset: 0x00015674
	public static Task<string> FriendNameAsyncInternal(LeaderboardEntry entry)
	{
		if (!string.IsNullOrEmpty(entry.displayName))
		{
			return Task.FromResult<string>(entry.displayName);
		}
		ulong num;
		TaskCompletionSource<string> taskCompletionSource;
		if (ulong.TryParse(entry.id, out num) && LeaderboardManager.nameCache.TryGetValue(num, out taskCompletionSource))
		{
			return taskCompletionSource.Task;
		}
		return Task.FromResult<string>(entry.displayName);
	}

	// Token: 0x06000379 RID: 889 RVA: 0x000174CC File Offset: 0x000156CC
	public static string GetLeaderboard(string id, int requestTop, int numRecords, bool bFriendsOnly, Action<LeaderboardScores> callback)
	{
		Task<PlatformManager.TryGetNetworkResult> task = PlatformManager.TryGetLeaderboardsReady();
		task.Wait();
		switch (task.Result)
		{
		case PlatformManager.TryGetNetworkResult.Success:
		{
			Queue<LeaderboardManager.LeaderboardRecordRequest> queue = LeaderboardManager.requests;
			lock (queue)
			{
				Debug.Log("getting steam leaderboard(" + id + ")");
				LeaderboardManager.LeaderboardRecordRequest leaderboardRecordRequest = new LeaderboardManager.LeaderboardRecordRequest();
				leaderboardRecordRequest.type = "getleaderboard";
				leaderboardRecordRequest.id = id;
				leaderboardRecordRequest.scoreCallback = callback;
				leaderboardRecordRequest.requestTop = requestTop;
				leaderboardRecordRequest.numRecords = numRecords;
				leaderboardRecordRequest.friendsonly = bFriendsOnly;
				LeaderboardManager.requests.Enqueue(leaderboardRecordRequest);
				return LeaderboardManager.GetLeaderboardName(id);
			}
			break;
		}
		case PlatformManager.TryGetNetworkResult.Failure:
			goto IL_00B8;
		case PlatformManager.TryGetNetworkResult.Cancelled:
			break;
		default:
			goto IL_00B8;
		}
		if (callback != null)
		{
			callback(LeaderboardScores.CreateCancelledLeaderboardScores());
		}
		return LeaderboardManager.GetLeaderboardName(id);
		IL_00B8:
		Debug.Log("Can't upload to the leaderboard because Steam isn't connected.");
		if (callback != null)
		{
			callback(LeaderboardScores.CreateEmptyLeaderboardScores());
		}
		return LeaderboardManager.GetLeaderboardName(id);
	}

	// Token: 0x0600037A RID: 890 RVA: 0x000175C4 File Offset: 0x000157C4
	public static async Task<string> GetLeaderboardRank(string leaderboardID, Action<LeaderboardScores> callback)
	{
		TaskAwaiter<PlatformManager.TryGetNetworkResult> taskAwaiter = PlatformManager.TryGetLeaderboardsReady().GetAwaiter();
		if (!taskAwaiter.IsCompleted)
		{
			await taskAwaiter;
			TaskAwaiter<PlatformManager.TryGetNetworkResult> taskAwaiter2;
			taskAwaiter = taskAwaiter2;
			taskAwaiter2 = default(TaskAwaiter<PlatformManager.TryGetNetworkResult>);
		}
		switch (taskAwaiter.GetResult())
		{
		case PlatformManager.TryGetNetworkResult.Success:
		{
			Queue<LeaderboardManager.LeaderboardRecordRequest> queue = LeaderboardManager.requests;
			lock (queue)
			{
				LeaderboardManager.LeaderboardRecordRequest leaderboardRecordRequest = new LeaderboardManager.LeaderboardRecordRequest();
				leaderboardRecordRequest.type = "getleaderboardrank";
				leaderboardRecordRequest.id = leaderboardID;
				leaderboardRecordRequest.scoreCallback = callback;
				LeaderboardManager.requests.Enqueue(leaderboardRecordRequest);
				return leaderboardID;
			}
			break;
		}
		case PlatformManager.TryGetNetworkResult.Failure:
			goto IL_0105;
		case PlatformManager.TryGetNetworkResult.Cancelled:
			break;
		default:
			goto IL_0105;
		}
		if (callback != null)
		{
			callback(LeaderboardScores.CreateCancelledLeaderboardScores());
		}
		return LeaderboardManager.GetLeaderboardName(leaderboardID);
		IL_0105:
		Debug.Log("Can't upload to the leaderboard because Steam isn't connected.");
		if (callback != null)
		{
			callback(LeaderboardScores.CreateEmptyLeaderboardScores());
		}
		return LeaderboardManager.GetLeaderboardName(leaderboardID);
	}

	// Token: 0x040002AB RID: 683
	private const ELeaderboardUploadScoreMethod s_leaderboardMethod = ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodKeepBest;

	// Token: 0x040002AC RID: 684
	private static CallResult<LeaderboardFindResult_t> m_findResult = new CallResult<LeaderboardFindResult_t>(null);

	// Token: 0x040002AD RID: 685
	private static CallResult<LeaderboardScoreUploaded_t> m_uploadResult = new CallResult<LeaderboardScoreUploaded_t>(null);

	// Token: 0x040002AE RID: 686
	private static CallResult<LeaderboardScoresDownloaded_t> m_downloadResult = new CallResult<LeaderboardScoresDownloaded_t>(null);

	// Token: 0x040002AF RID: 687
	private static Callback<PersonaStateChange_t> m_personaStateChange;

	// Token: 0x040002B0 RID: 688
	private static SteamLeaderboard_t currentLeaderboard;

	// Token: 0x040002B1 RID: 689
	private static Dictionary<string, SteamLeaderboard_t> leaderboardID = new Dictionary<string, SteamLeaderboard_t>();

	// Token: 0x040002B2 RID: 690
	private static Dictionary<ulong, TaskCompletionSource<string>> nameCache = new Dictionary<ulong, TaskCompletionSource<string>>();

	// Token: 0x040002B3 RID: 691
	private static Queue<LeaderboardManager.LeaderboardRecordRequest> requests = new Queue<LeaderboardManager.LeaderboardRecordRequest>();

	// Token: 0x040002B4 RID: 692
	private static LeaderboardManager.LeaderboardRecordRequest currentRequest = null;

	// Token: 0x040002B5 RID: 693
	private static Action<LeaderboardScores> leaderboardEntriesCallback = null;

	// Token: 0x040002B6 RID: 694
	public static Dictionary<string, string> leaderboardresults = new Dictionary<string, string>();

	// Token: 0x0200157B RID: 5499
	private class LeaderboardRecordRequest
	{
		// Token: 0x04005676 RID: 22134
		public string type;

		// Token: 0x04005677 RID: 22135
		public string id;

		// Token: 0x04005678 RID: 22136
		public int requestTop;

		// Token: 0x04005679 RID: 22137
		public int numRecords;

		// Token: 0x0400567A RID: 22138
		public int score;

		// Token: 0x0400567B RID: 22139
		public bool friendsonly;

		// Token: 0x0400567C RID: 22140
		public Action<LeaderboardScores> scoreCallback;
	}
}
