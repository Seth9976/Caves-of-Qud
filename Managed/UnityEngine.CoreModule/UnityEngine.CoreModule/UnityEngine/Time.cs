using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.IntegerTime;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020002E5 RID: 741
	[NativeHeader("Runtime/Input/TimeManager.h")]
	[StaticAccessor("GetTimeManager()", StaticAccessorType.Dot)]
	public class Time
	{
		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x0600246A RID: 9322
		[NativeProperty("CurTime")]
		public static extern float time
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x0600246B RID: 9323
		[NativeProperty("CurTime")]
		public static extern double timeAsDouble
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x0600246C RID: 9324 RVA: 0x00046814 File Offset: 0x00044A14
		[NativeProperty("CurTimeRational")]
		public static RationalTime timeAsRational
		{
			get
			{
				RationalTime rationalTime;
				Time.get_timeAsRational_Injected(out rationalTime);
				return rationalTime;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x0600246D RID: 9325
		[NativeProperty("TimeSinceSceneLoad")]
		public static extern float timeSinceLevelLoad
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x0600246E RID: 9326
		[NativeProperty("TimeSinceSceneLoad")]
		public static extern double timeSinceLevelLoadAsDouble
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x0600246F RID: 9327
		public static extern float deltaTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06002470 RID: 9328
		public static extern float fixedTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06002471 RID: 9329
		[NativeProperty("FixedTime")]
		public static extern double fixedTimeAsDouble
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06002472 RID: 9330
		public static extern float unscaledTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06002473 RID: 9331
		[NativeProperty("UnscaledTime")]
		public static extern double unscaledTimeAsDouble
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06002474 RID: 9332
		public static extern float fixedUnscaledTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06002475 RID: 9333
		[NativeProperty("FixedUnscaledTime")]
		public static extern double fixedUnscaledTimeAsDouble
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06002476 RID: 9334
		public static extern float unscaledDeltaTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06002477 RID: 9335
		public static extern float fixedUnscaledDeltaTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06002478 RID: 9336
		// (set) Token: 0x06002479 RID: 9337
		public static extern float fixedDeltaTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x0600247A RID: 9338
		// (set) Token: 0x0600247B RID: 9339
		public static extern float maximumDeltaTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x0600247C RID: 9340
		public static extern float smoothDeltaTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x0600247D RID: 9341
		// (set) Token: 0x0600247E RID: 9342
		public static extern float maximumParticleDeltaTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x0600247F RID: 9343
		// (set) Token: 0x06002480 RID: 9344
		public static extern float timeScale
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06002481 RID: 9345
		public static extern int frameCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06002482 RID: 9346
		[NativeProperty("RenderFrameCount")]
		public static extern int renderedFrameCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06002483 RID: 9347
		[NativeProperty("Realtime")]
		public static extern float realtimeSinceStartup
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06002484 RID: 9348
		[NativeProperty("Realtime")]
		public static extern double realtimeSinceStartupAsDouble
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06002485 RID: 9349
		// (set) Token: 0x06002486 RID: 9350
		public static extern float captureDeltaTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06002487 RID: 9351 RVA: 0x0004682C File Offset: 0x00044A2C
		// (set) Token: 0x06002488 RID: 9352 RVA: 0x00046844 File Offset: 0x00044A44
		public static RationalTime captureDeltaTimeRational
		{
			get
			{
				RationalTime rationalTime;
				Time.get_captureDeltaTimeRational_Injected(out rationalTime);
				return rationalTime;
			}
			set
			{
				Time.set_captureDeltaTimeRational_Injected(ref value);
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06002489 RID: 9353 RVA: 0x00046858 File Offset: 0x00044A58
		// (set) Token: 0x0600248A RID: 9354 RVA: 0x0004688A File Offset: 0x00044A8A
		public static int captureFramerate
		{
			get
			{
				return (Time.captureDeltaTime == 0f) ? 0 : ((int)Mathf.Round(1f / Time.captureDeltaTime));
			}
			set
			{
				Time.captureDeltaTime = ((value == 0) ? 0f : (1f / (float)value));
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x0600248B RID: 9355
		public static extern bool inFixedTimeStep
		{
			[NativeName("IsUsingFixedTimeStep")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x0600248D RID: 9357
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_timeAsRational_Injected(out RationalTime ret);

		// Token: 0x0600248E RID: 9358
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_captureDeltaTimeRational_Injected(out RationalTime ret);

		// Token: 0x0600248F RID: 9359
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_captureDeltaTimeRational_Injected([In] ref RationalTime value);
	}
}
