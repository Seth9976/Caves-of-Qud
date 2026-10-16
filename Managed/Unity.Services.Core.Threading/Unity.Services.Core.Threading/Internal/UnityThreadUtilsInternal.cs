using System;
using System.Threading;
using System.Threading.Tasks;
using Unity.Services.Core.Internal;

namespace Unity.Services.Core.Threading.Internal
{
	// Token: 0x02000003 RID: 3
	internal class UnityThreadUtilsInternal : IUnityThreadUtils, IServiceComponent
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020B8 File Offset: 0x000002B8
		public static Task PostAsync(Action action)
		{
			return Task.Factory.StartNew(action, CancellationToken.None, TaskCreationOptions.None, UnityThreadUtils.UnityThreadScheduler);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D0 File Offset: 0x000002D0
		public static Task PostAsync(Action<object> action, object state)
		{
			return Task.Factory.StartNew(action, state, CancellationToken.None, TaskCreationOptions.None, UnityThreadUtils.UnityThreadScheduler);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020E9 File Offset: 0x000002E9
		public static Task<T> PostAsync<T>(Func<T> action)
		{
			return Task<T>.Factory.StartNew(action, CancellationToken.None, TaskCreationOptions.None, UnityThreadUtils.UnityThreadScheduler);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002101 File Offset: 0x00000301
		public static Task<T> PostAsync<T>(Func<object, T> action, object state)
		{
			return Task<T>.Factory.StartNew(action, state, CancellationToken.None, TaskCreationOptions.None, UnityThreadUtils.UnityThreadScheduler);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000211A File Offset: 0x0000031A
		public static void Send(Action action)
		{
			if (UnityThreadUtils.IsRunningOnUnityThread)
			{
				action();
				return;
			}
			UnityThreadUtilsInternal.PostAsync(action).Wait();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002135 File Offset: 0x00000335
		public static void Send(Action<object> action, object state)
		{
			if (UnityThreadUtils.IsRunningOnUnityThread)
			{
				action(state);
				return;
			}
			UnityThreadUtilsInternal.PostAsync(action, state).Wait();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002152 File Offset: 0x00000352
		public static T Send<T>(Func<T> action)
		{
			if (UnityThreadUtils.IsRunningOnUnityThread)
			{
				return action();
			}
			Task<T> task = UnityThreadUtilsInternal.PostAsync<T>(action);
			task.Wait();
			return task.Result;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002173 File Offset: 0x00000373
		public static T Send<T>(Func<object, T> action, object state)
		{
			if (UnityThreadUtils.IsRunningOnUnityThread)
			{
				return action(state);
			}
			Task<T> task = UnityThreadUtilsInternal.PostAsync<T>(action, state);
			task.Wait();
			return task.Result;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002196 File Offset: 0x00000396
		bool IUnityThreadUtils.IsRunningOnUnityThread
		{
			get
			{
				return UnityThreadUtils.IsRunningOnUnityThread;
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000219D File Offset: 0x0000039D
		Task IUnityThreadUtils.PostAsync(Action action)
		{
			return UnityThreadUtilsInternal.PostAsync(action);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021A5 File Offset: 0x000003A5
		Task IUnityThreadUtils.PostAsync(Action<object> action, object state)
		{
			return UnityThreadUtilsInternal.PostAsync(action, state);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021AE File Offset: 0x000003AE
		Task<T> IUnityThreadUtils.PostAsync<T>(Func<T> action)
		{
			return UnityThreadUtilsInternal.PostAsync<T>(action);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021B6 File Offset: 0x000003B6
		Task<T> IUnityThreadUtils.PostAsync<T>(Func<object, T> action, object state)
		{
			return UnityThreadUtilsInternal.PostAsync<T>(action, state);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021BF File Offset: 0x000003BF
		void IUnityThreadUtils.Send(Action action)
		{
			UnityThreadUtilsInternal.Send(action);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021C7 File Offset: 0x000003C7
		void IUnityThreadUtils.Send(Action<object> action, object state)
		{
			UnityThreadUtilsInternal.Send(action, state);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000021D0 File Offset: 0x000003D0
		T IUnityThreadUtils.Send<T>(Func<T> action)
		{
			return UnityThreadUtilsInternal.Send<T>(action);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000021D8 File Offset: 0x000003D8
		T IUnityThreadUtils.Send<T>(Func<object, T> action, object state)
		{
			return UnityThreadUtilsInternal.Send<T>(action, state);
		}
	}
}
