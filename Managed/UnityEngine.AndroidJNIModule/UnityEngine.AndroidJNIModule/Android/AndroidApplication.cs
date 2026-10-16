using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Android
{
	// Token: 0x02000012 RID: 18
	[StaticAccessor("AndroidApplication", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/AndroidJNI/Public/AndroidApplication.bindings.h")]
	public static class AndroidApplication
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600023E RID: 574
		internal static extern IntPtr UnityPlayerRaw
		{
			[ThreadSafe]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600023F RID: 575
		private static extern IntPtr CurrentContextRaw
		{
			[ThreadSafe]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000240 RID: 576
		private static extern IntPtr CurrentActivityRaw
		{
			[ThreadSafe]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000A040 File Offset: 0x00008240
		[RequiredByNativeCode(GenerateProxy = true)]
		private static void AcquireMainThreadSynchronizationContext()
		{
			AndroidApplication.m_MainThreadSynchronizationContext = SynchronizationContext.Current;
			bool flag = AndroidApplication.m_MainThreadSynchronizationContext == null;
			if (flag)
			{
				throw new Exception("Failed to acquire main thread synchronization context");
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000242 RID: 578 RVA: 0x0000A070 File Offset: 0x00008270
		public static AndroidJavaObject currentContext
		{
			get
			{
				bool flag = AndroidApplication.m_Context != null;
				AndroidJavaObject androidJavaObject;
				if (flag)
				{
					androidJavaObject = AndroidApplication.m_Context;
				}
				else
				{
					AndroidApplication.m_Context = new AndroidJavaObjectUnityOwned(AndroidApplication.CurrentContextRaw);
					androidJavaObject = AndroidApplication.m_Context;
				}
				return androidJavaObject;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000A0AC File Offset: 0x000082AC
		public static AndroidJavaObject currentActivity
		{
			get
			{
				bool flag = AndroidApplication.m_Activity != null;
				AndroidJavaObject androidJavaObject;
				if (flag)
				{
					androidJavaObject = AndroidApplication.m_Activity;
				}
				else
				{
					AndroidApplication.m_Activity = new AndroidJavaObjectUnityOwned(AndroidApplication.CurrentActivityRaw);
					androidJavaObject = AndroidApplication.m_Activity;
				}
				return androidJavaObject;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000A0E8 File Offset: 0x000082E8
		public static AndroidJavaObject unityPlayer
		{
			get
			{
				bool flag = AndroidApplication.m_UnityPlayer != null;
				AndroidJavaObject androidJavaObject;
				if (flag)
				{
					androidJavaObject = AndroidApplication.m_UnityPlayer;
				}
				else
				{
					AndroidApplication.m_UnityPlayer = new AndroidJavaObjectUnityOwned(AndroidApplication.UnityPlayerRaw);
					androidJavaObject = AndroidApplication.m_UnityPlayer;
				}
				return androidJavaObject;
			}
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000A122 File Offset: 0x00008322
		[RequiredByNativeCode(GenerateProxy = true)]
		private static void SetCurrentConfiguration(AndroidConfiguration config)
		{
			AndroidApplication.m_CurrentConfiguration = config;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000A12C File Offset: 0x0000832C
		[RequiredByNativeCode(GenerateProxy = true)]
		private static AndroidConfiguration GetCurrentConfiguration()
		{
			return AndroidApplication.m_CurrentConfiguration;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000A144 File Offset: 0x00008344
		[RequiredByNativeCode(GenerateProxy = true)]
		private static void DispatchConfigurationChanged(bool notifySubscribers)
		{
			if (notifySubscribers)
			{
				Action<AndroidConfiguration> action = AndroidApplication.onConfigurationChanged;
				if (action != null)
				{
					action(AndroidApplication.m_CurrentConfiguration);
				}
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000A16D File Offset: 0x0000836D
		public static AndroidConfiguration currentConfiguration
		{
			get
			{
				return AndroidApplication.m_CurrentConfiguration;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000249 RID: 585 RVA: 0x0000A174 File Offset: 0x00008374
		// (remove) Token: 0x0600024A RID: 586 RVA: 0x0000A1A8 File Offset: 0x000083A8
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<AndroidConfiguration> onConfigurationChanged;

		// Token: 0x0600024B RID: 587 RVA: 0x0000A1DC File Offset: 0x000083DC
		public static void InvokeOnUIThread(Action action)
		{
			AndroidJNI.InvokeAttached(delegate
			{
				AndroidApplication.unityPlayer.Call("runOnUiThread", new object[]
				{
					new AndroidJavaRunnable(action.Invoke)
				});
			});
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000A20C File Offset: 0x0000840C
		public static void InvokeOnUnityMainThread(Action action)
		{
			AndroidApplication.m_MainThreadSynchronizationContext.Send(delegate(object o)
			{
				action();
			}, null);
		}

		// Token: 0x04000026 RID: 38
		private static SynchronizationContext m_MainThreadSynchronizationContext;

		// Token: 0x04000027 RID: 39
		private static AndroidJavaObjectUnityOwned m_Context;

		// Token: 0x04000028 RID: 40
		private static AndroidJavaObjectUnityOwned m_Activity;

		// Token: 0x04000029 RID: 41
		private static AndroidJavaObjectUnityOwned m_UnityPlayer;

		// Token: 0x0400002A RID: 42
		private static AndroidConfiguration m_CurrentConfiguration;
	}
}
