using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks.Internal;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200002B RID: 43
	public static class PlayerLoopHelper
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00003054 File Offset: 0x00001254
		public static SynchronizationContext UnitySynchronizationContext
		{
			get
			{
				return PlayerLoopHelper.unitySynchronizationContext;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600009F RID: 159 RVA: 0x0000305B File Offset: 0x0000125B
		public static int MainThreadId
		{
			get
			{
				return PlayerLoopHelper.mainThreadId;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003062 File Offset: 0x00001262
		internal static string ApplicationDataPath
		{
			get
			{
				return PlayerLoopHelper.applicationDataPath;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003069 File Offset: 0x00001269
		public static bool IsMainThread
		{
			get
			{
				return Thread.CurrentThread.ManagedThreadId == PlayerLoopHelper.mainThreadId;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x0000307C File Offset: 0x0000127C
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00003083 File Offset: 0x00001283
		internal static bool IsEditorApplicationQuitting { get; private set; }

		// Token: 0x060000A4 RID: 164 RVA: 0x0000308C File Offset: 0x0000128C
		private static PlayerLoopSystem[] InsertRunner(PlayerLoopSystem loopSystem, bool injectOnFirst, Type loopRunnerYieldType, ContinuationQueue cq, Type loopRunnerType, PlayerLoopRunner runner)
		{
			PlayerLoopSystem playerLoopSystem = new PlayerLoopSystem
			{
				type = loopRunnerYieldType,
				updateDelegate = new PlayerLoopSystem.UpdateFunction(cq.Run)
			};
			PlayerLoopSystem playerLoopSystem2 = new PlayerLoopSystem
			{
				type = loopRunnerType,
				updateDelegate = new PlayerLoopSystem.UpdateFunction(runner.Run)
			};
			PlayerLoopSystem[] array = PlayerLoopHelper.RemoveRunner(loopSystem, loopRunnerYieldType, loopRunnerType);
			PlayerLoopSystem[] array2 = new PlayerLoopSystem[array.Length + 2];
			Array.Copy(array, 0, array2, injectOnFirst ? 2 : 0, array.Length);
			if (injectOnFirst)
			{
				array2[0] = playerLoopSystem;
				array2[1] = playerLoopSystem2;
			}
			else
			{
				array2[array2.Length - 2] = playerLoopSystem;
				array2[array2.Length - 1] = playerLoopSystem2;
			}
			return array2;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000313C File Offset: 0x0000133C
		private static PlayerLoopSystem[] RemoveRunner(PlayerLoopSystem loopSystem, Type loopRunnerYieldType, Type loopRunnerType)
		{
			return loopSystem.subSystemList.Where<PlayerLoopSystem>((PlayerLoopSystem ls) => ls.type != loopRunnerYieldType && ls.type != loopRunnerType).ToArray<PlayerLoopSystem>();
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000317C File Offset: 0x0000137C
		private static PlayerLoopSystem[] InsertUniTaskSynchronizationContext(PlayerLoopSystem loopSystem)
		{
			PlayerLoopSystem playerLoopSystem = new PlayerLoopSystem
			{
				type = typeof(UniTaskSynchronizationContext),
				updateDelegate = new PlayerLoopSystem.UpdateFunction(UniTaskSynchronizationContext.Run)
			};
			List<PlayerLoopSystem> list = new List<PlayerLoopSystem>(loopSystem.subSystemList.Where<PlayerLoopSystem>((PlayerLoopSystem ls) => ls.type != typeof(UniTaskSynchronizationContext)).ToArray<PlayerLoopSystem>());
			int num = list.FindIndex((PlayerLoopSystem x) => x.type.Name == "ScriptRunDelayedTasks");
			if (num == -1)
			{
				num = list.FindIndex((PlayerLoopSystem x) => x.type.Name == "UniTaskLoopRunnerUpdate");
			}
			list.Insert(num + 1, playerLoopSystem);
			return list.ToArray();
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000324C File Offset: 0x0000144C
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
		private static void Init()
		{
			PlayerLoopHelper.unitySynchronizationContext = SynchronizationContext.Current;
			PlayerLoopHelper.mainThreadId = Thread.CurrentThread.ManagedThreadId;
			try
			{
				PlayerLoopHelper.applicationDataPath = Application.dataPath;
			}
			catch
			{
			}
			if (PlayerLoopHelper.runners != null)
			{
				return;
			}
			PlayerLoopSystem currentPlayerLoop = PlayerLoop.GetCurrentPlayerLoop();
			PlayerLoopHelper.Initialize(ref currentPlayerLoop, InjectPlayerLoopTimings.All);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000032AC File Offset: 0x000014AC
		private static int FindLoopSystemIndex(PlayerLoopSystem[] playerLoopList, Type systemType)
		{
			for (int i = 0; i < playerLoopList.Length; i++)
			{
				if (playerLoopList[i].type == systemType)
				{
					return i;
				}
			}
			throw new Exception("Target PlayerLoopSystem does not found. Type:" + systemType.FullName);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000032F4 File Offset: 0x000014F4
		private static void InsertLoop(PlayerLoopSystem[] copyList, InjectPlayerLoopTimings injectTimings, Type loopType, InjectPlayerLoopTimings targetTimings, int index, bool injectOnFirst, Type loopRunnerYieldType, Type loopRunnerType, PlayerLoopTiming playerLoopTiming)
		{
			int num = PlayerLoopHelper.FindLoopSystemIndex(copyList, loopType);
			if ((injectTimings & targetTimings) == targetTimings)
			{
				copyList[num].subSystemList = PlayerLoopHelper.InsertRunner(copyList[num], injectOnFirst, loopRunnerYieldType, PlayerLoopHelper.yielders[index] = new ContinuationQueue(playerLoopTiming), loopRunnerType, PlayerLoopHelper.runners[index] = new PlayerLoopRunner(playerLoopTiming));
				return;
			}
			copyList[num].subSystemList = PlayerLoopHelper.RemoveRunner(copyList[num], loopRunnerYieldType, loopRunnerType);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003370 File Offset: 0x00001570
		public static void Initialize(ref PlayerLoopSystem playerLoop, InjectPlayerLoopTimings injectTimings = InjectPlayerLoopTimings.All)
		{
			PlayerLoopHelper.yielders = new ContinuationQueue[16];
			PlayerLoopHelper.runners = new PlayerLoopRunner[16];
			PlayerLoopSystem[] array = playerLoop.subSystemList.ToArray<PlayerLoopSystem>();
			PlayerLoopHelper.InsertLoop(array, injectTimings, typeof(Initialization), InjectPlayerLoopTimings.Initialization, 0, true, typeof(UniTaskLoopRunners.UniTaskLoopRunnerYieldInitialization), typeof(UniTaskLoopRunners.UniTaskLoopRunnerInitialization), PlayerLoopTiming.Initialization);
			PlayerLoopHelper.InsertLoop(array, injectTimings, typeof(Initialization), InjectPlayerLoopTimings.LastInitialization, 1, false, typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastYieldInitialization), typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastInitialization), PlayerLoopTiming.LastInitialization);
			PlayerLoopHelper.InsertLoop(array, injectTimings, typeof(EarlyUpdate), InjectPlayerLoopTimings.EarlyUpdate, 2, true, typeof(UniTaskLoopRunners.UniTaskLoopRunnerYieldEarlyUpdate), typeof(UniTaskLoopRunners.UniTaskLoopRunnerEarlyUpdate), PlayerLoopTiming.EarlyUpdate);
			PlayerLoopHelper.InsertLoop(array, injectTimings, typeof(EarlyUpdate), InjectPlayerLoopTimings.LastEarlyUpdate, 3, false, typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastYieldEarlyUpdate), typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastEarlyUpdate), PlayerLoopTiming.LastEarlyUpdate);
			PlayerLoopHelper.InsertLoop(array, injectTimings, typeof(FixedUpdate), InjectPlayerLoopTimings.FixedUpdate, 4, true, typeof(UniTaskLoopRunners.UniTaskLoopRunnerYieldFixedUpdate), typeof(UniTaskLoopRunners.UniTaskLoopRunnerFixedUpdate), PlayerLoopTiming.FixedUpdate);
			PlayerLoopHelper.InsertLoop(array, injectTimings, typeof(FixedUpdate), InjectPlayerLoopTimings.LastFixedUpdate, 5, false, typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastYieldFixedUpdate), typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastFixedUpdate), PlayerLoopTiming.LastFixedUpdate);
			PlayerLoopHelper.InsertLoop(array, injectTimings, typeof(PreUpdate), InjectPlayerLoopTimings.PreUpdate, 6, true, typeof(UniTaskLoopRunners.UniTaskLoopRunnerYieldPreUpdate), typeof(UniTaskLoopRunners.UniTaskLoopRunnerPreUpdate), PlayerLoopTiming.PreUpdate);
			PlayerLoopHelper.InsertLoop(array, injectTimings, typeof(PreUpdate), InjectPlayerLoopTimings.LastPreUpdate, 7, false, typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastYieldPreUpdate), typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastPreUpdate), PlayerLoopTiming.LastPreUpdate);
			PlayerLoopHelper.InsertLoop(array, injectTimings, typeof(Update), InjectPlayerLoopTimings.Update, 8, true, typeof(UniTaskLoopRunners.UniTaskLoopRunnerYieldUpdate), typeof(UniTaskLoopRunners.UniTaskLoopRunnerUpdate), PlayerLoopTiming.Update);
			PlayerLoopHelper.InsertLoop(array, injectTimings, typeof(Update), InjectPlayerLoopTimings.LastUpdate, 9, false, typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastYieldUpdate), typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastUpdate), PlayerLoopTiming.LastUpdate);
			PlayerLoopHelper.InsertLoop(array, injectTimings, typeof(PreLateUpdate), InjectPlayerLoopTimings.PreLateUpdate, 10, true, typeof(UniTaskLoopRunners.UniTaskLoopRunnerYieldPreLateUpdate), typeof(UniTaskLoopRunners.UniTaskLoopRunnerPreLateUpdate), PlayerLoopTiming.PreLateUpdate);
			PlayerLoopHelper.InsertLoop(array, injectTimings, typeof(PreLateUpdate), InjectPlayerLoopTimings.LastPreLateUpdate, 11, false, typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastYieldPreLateUpdate), typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastPreLateUpdate), PlayerLoopTiming.LastPreLateUpdate);
			PlayerLoopHelper.InsertLoop(array, injectTimings, typeof(PostLateUpdate), InjectPlayerLoopTimings.PostLateUpdate, 12, true, typeof(UniTaskLoopRunners.UniTaskLoopRunnerYieldPostLateUpdate), typeof(UniTaskLoopRunners.UniTaskLoopRunnerPostLateUpdate), PlayerLoopTiming.PostLateUpdate);
			PlayerLoopHelper.InsertLoop(array, injectTimings, typeof(PostLateUpdate), InjectPlayerLoopTimings.LastPostLateUpdate, 13, false, typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastYieldPostLateUpdate), typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastPostLateUpdate), PlayerLoopTiming.LastPostLateUpdate);
			PlayerLoopHelper.InsertLoop(array, injectTimings, typeof(TimeUpdate), InjectPlayerLoopTimings.TimeUpdate, 14, true, typeof(UniTaskLoopRunners.UniTaskLoopRunnerYieldTimeUpdate), typeof(UniTaskLoopRunners.UniTaskLoopRunnerTimeUpdate), PlayerLoopTiming.TimeUpdate);
			PlayerLoopHelper.InsertLoop(array, injectTimings, typeof(TimeUpdate), InjectPlayerLoopTimings.LastTimeUpdate, 15, false, typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastYieldTimeUpdate), typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastTimeUpdate), PlayerLoopTiming.LastTimeUpdate);
			int num = PlayerLoopHelper.FindLoopSystemIndex(array, typeof(Update));
			array[num].subSystemList = PlayerLoopHelper.InsertUniTaskSynchronizationContext(array[num]);
			playerLoop.subSystemList = array;
			PlayerLoop.SetPlayerLoop(playerLoop);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000036A1 File Offset: 0x000018A1
		public static void AddAction(PlayerLoopTiming timing, IPlayerLoopItem action)
		{
			PlayerLoopRunner playerLoopRunner = PlayerLoopHelper.runners[(int)timing];
			if (playerLoopRunner == null)
			{
				PlayerLoopHelper.ThrowInvalidLoopTiming(timing);
			}
			playerLoopRunner.AddAction(action);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000036B9 File Offset: 0x000018B9
		private static void ThrowInvalidLoopTiming(PlayerLoopTiming playerLoopTiming)
		{
			throw new InvalidOperationException("Target playerLoopTiming is not injected. Please check PlayerLoopHelper.Initialize. PlayerLoopTiming:" + playerLoopTiming.ToString());
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000036D7 File Offset: 0x000018D7
		public static void AddContinuation(PlayerLoopTiming timing, Action continuation)
		{
			ContinuationQueue continuationQueue = PlayerLoopHelper.yielders[(int)timing];
			if (continuationQueue == null)
			{
				PlayerLoopHelper.ThrowInvalidLoopTiming(timing);
			}
			continuationQueue.Enqueue(continuation);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000036F0 File Offset: 0x000018F0
		public static void DumpCurrentPlayerLoop()
		{
			ref PlayerLoopSystem currentPlayerLoop = PlayerLoop.GetCurrentPlayerLoop();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("PlayerLoop List");
			foreach (PlayerLoopSystem playerLoopSystem in currentPlayerLoop.subSystemList)
			{
				stringBuilder.AppendFormat("------{0}------", playerLoopSystem.type.Name);
				stringBuilder.AppendLine();
				if (playerLoopSystem.subSystemList == null)
				{
					stringBuilder.AppendFormat("{0} has no subsystems!", playerLoopSystem.ToString());
					stringBuilder.AppendLine();
				}
				else
				{
					foreach (PlayerLoopSystem playerLoopSystem2 in playerLoopSystem.subSystemList)
					{
						stringBuilder.AppendFormat("{0}", playerLoopSystem2.type.Name);
						stringBuilder.AppendLine();
						if (playerLoopSystem2.subSystemList != null)
						{
							Debug.LogWarning("More Subsystem:" + playerLoopSystem2.subSystemList.Length.ToString());
						}
					}
				}
			}
			Debug.Log(stringBuilder.ToString());
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000037F8 File Offset: 0x000019F8
		public static bool IsInjectedUniTaskPlayerLoop()
		{
			foreach (PlayerLoopSystem playerLoopSystem in PlayerLoop.GetCurrentPlayerLoop().subSystemList)
			{
				if (playerLoopSystem.subSystemList != null)
				{
					PlayerLoopSystem[] subSystemList2 = playerLoopSystem.subSystemList;
					for (int j = 0; j < subSystemList2.Length; j++)
					{
						if (subSystemList2[j].type == typeof(UniTaskLoopRunners.UniTaskLoopRunnerInitialization))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x04000053 RID: 83
		private static readonly ContinuationQueue ThrowMarkerContinuationQueue = new ContinuationQueue(PlayerLoopTiming.Initialization);

		// Token: 0x04000054 RID: 84
		private static readonly PlayerLoopRunner ThrowMarkerPlayerLoopRunner = new PlayerLoopRunner(PlayerLoopTiming.Initialization);

		// Token: 0x04000055 RID: 85
		private static int mainThreadId;

		// Token: 0x04000056 RID: 86
		private static string applicationDataPath;

		// Token: 0x04000057 RID: 87
		private static SynchronizationContext unitySynchronizationContext;

		// Token: 0x04000058 RID: 88
		private static ContinuationQueue[] yielders;

		// Token: 0x04000059 RID: 89
		private static PlayerLoopRunner[] runners;
	}
}
