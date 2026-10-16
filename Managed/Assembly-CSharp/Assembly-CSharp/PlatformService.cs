using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using LaundryBear;
using LaundryBear.PlatformServices;
using Platform.IO;
using UnityEngine;

// Token: 0x020000F9 RID: 249
[NullableContext(1)]
[Nullable(0)]
public class PlatformService : MonoBehaviour, IService
{
	// Token: 0x17000130 RID: 304
	// (get) Token: 0x06000966 RID: 2406 RVA: 0x0003B3DA File Offset: 0x000395DA
	public string Name
	{
		get
		{
			return "Platform Service";
		}
	}

	// Token: 0x17000131 RID: 305
	// (get) Token: 0x06000967 RID: 2407 RVA: 0x0003B3E1 File Offset: 0x000395E1
	// (set) Token: 0x06000968 RID: 2408 RVA: 0x0003B3E9 File Offset: 0x000395E9
	public ServiceLocator.ServiceInitializationStatus InitializationStatus { get; private set; }

	// Token: 0x06000969 RID: 2409 RVA: 0x0003B3F2 File Offset: 0x000395F2
	public IStorage GetStorage()
	{
		return this.m_storage;
	}

	// Token: 0x0600096A RID: 2410 RVA: 0x0003B3FA File Offset: 0x000395FA
	public IEnumerator Initialize(bool sync = false)
	{
		ServiceLocator.ManagersInitializedEvent += this.OnServicesInitialized;
		this.m_mainContext = SynchronizationContext.Current;
		this.InitializationStatus = ServiceLocator.ServiceInitializationStatus.Ready;
		yield break;
	}

	// Token: 0x0600096B RID: 2411 RVA: 0x0003B409 File Offset: 0x00039609
	private void OnServicesInitialized()
	{
		ServiceLocator.ManagersInitializedEvent -= this.OnServicesInitialized;
	}

	// Token: 0x0600096C RID: 2412 RVA: 0x0003B41C File Offset: 0x0003961C
	public async Task<bool> SetupLaunchUserAndStorage()
	{
		IUserService userService;
		if (ServiceLocator.TryGetService<IUserService>(out userService))
		{
			IUser user = await PlatformService.GetLaunchUserAsync(userService);
			this.m_user = user;
			if (this.m_user == null)
			{
				Debug.LogError("Couldn't get user from IUserService, halting setup.");
				return false;
			}
		}
		IStorageService storageService;
		bool flag;
		if (!ServiceLocator.TryGetService<IStorageService>(out storageService))
		{
			Debug.LogError("Couldn't get IStorageService, halting setup.)");
			flag = false;
		}
		else
		{
			string text = string.Empty;
			if (this.m_user != null)
			{
				text = "save";
			}
			ValueTuple<StorageResult, IStorage> valueTuple = await PlatformService.GetStorageAsync(storageService, this.m_user, text);
			StorageResult item = valueTuple.Item1;
			IStorage item2 = valueTuple.Item2;
			if (item == StorageResult.QuotaExceeded)
			{
				Debug.LogError(string.Format("{0} handling not implemented. On certain consoles (i think ps5) you are required to let the user retry after having time to try a different user or storage device... this should probably be handled by the platform service implementation of OpenOrCreate. but it is handled directly in PlatformService for now", item));
				flag = await this.SetupLaunchUserAndStorage();
			}
			else if (item != StorageResult.Success)
			{
				Debug.LogError(string.Format("{0} handling not implemented. On certain consoles (i think ps5) you are required to let the user retry after having time to try a different user or storage device... this should probably be handled by the platform service implementation of OpenOrCreate", item));
				flag = false;
			}
			else
			{
				this.m_storage = item2;
				State.SetStorage(item2);
				flag = true;
			}
		}
		return flag;
	}

	// Token: 0x0600096D RID: 2413 RVA: 0x0003B460 File Offset: 0x00039660
	[return: Nullable(new byte[] { 1, 2 })]
	private static Task<IUser> GetLaunchUserAsync(IUserService service)
	{
		TaskCompletionSource<IUser> tcs = new TaskCompletionSource<IUser>();
		service.GetLaunchUser(delegate(SignInResult result, IUser user)
		{
			tcs.SetResult((result == SignInResult.Success) ? user : null);
		});
		return tcs.Task;
	}

	// Token: 0x0600096E RID: 2414 RVA: 0x0003B49C File Offset: 0x0003969C
	[return: TupleElementNames(new string[] { "result", "storage" })]
	[return: Nullable(new byte[] { 1, 0, 2 })]
	private static Task<ValueTuple<StorageResult, IStorage>> GetStorageAsync(IStorageService service, [Nullable(2)] IUser user, string mountPoint)
	{
		PlatformService.<>c__DisplayClass16_0 CS$<>8__locals1 = new PlatformService.<>c__DisplayClass16_0();
		CS$<>8__locals1.tcs = new TaskCompletionSource<ValueTuple<StorageResult, IStorage>>();
		if (user != null)
		{
			service.OpenOrCreate(user, mountPoint, new OnCreateStorage(CS$<>8__locals1.<GetStorageAsync>g__callback|0));
		}
		else
		{
			service.OpenOrCreate(mountPoint, new OnCreateStorage(CS$<>8__locals1.<GetStorageAsync>g__callback|0));
		}
		return CS$<>8__locals1.tcs.Task;
	}

	// Token: 0x0600096F RID: 2415 RVA: 0x0003B4F1 File Offset: 0x000396F1
	public void BeginShutdownNoticeSection()
	{
	}

	// Token: 0x06000970 RID: 2416 RVA: 0x0003B4F3 File Offset: 0x000396F3
	public void EndShutdownNoticeSection()
	{
	}

	// Token: 0x04000628 RID: 1576
	[Nullable(2)]
	private IUser m_user;

	// Token: 0x04000629 RID: 1577
	private IStorage m_storage;

	// Token: 0x0400062A RID: 1578
	private const string MOUNTNAME_CONSOLE = "save";

	// Token: 0x0400062B RID: 1579
	private const string GAME_NETWORK_ID = "t-b08cccc4";

	// Token: 0x0400062C RID: 1580
	private SynchronizationContext m_mainContext;

	// Token: 0x020015DF RID: 5599
	[Nullable(0)]
	public class InitializationBundle
	{
		// Token: 0x0600D647 RID: 54855 RVA: 0x004F7E8D File Offset: 0x004F608D
		public InitializationBundle(LeaderboardResult result, string leaderboardId)
		{
			this.Result = result;
			this.LeaderboardId = leaderboardId;
		}

		// Token: 0x04005833 RID: 22579
		public readonly LeaderboardResult Result;

		// Token: 0x04005834 RID: 22580
		public readonly string LeaderboardId;
	}
}
