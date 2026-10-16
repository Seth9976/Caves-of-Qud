using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Analytics
{
	// Token: 0x0200000A RID: 10
	[RequiredByNativeCode]
	[NativeHeader("UnityAnalyticsScriptingClasses.h")]
	[NativeHeader("Modules/UnityAnalytics/Public/UnityAnalytics.h")]
	public static class AnalyticsSessionInfo
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000094 RID: 148 RVA: 0x00003A58 File Offset: 0x00001C58
		// (remove) Token: 0x06000095 RID: 149 RVA: 0x00003A8C File Offset: 0x00001C8C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event AnalyticsSessionInfo.SessionStateChanged sessionStateChanged;

		// Token: 0x06000096 RID: 150 RVA: 0x00003AC0 File Offset: 0x00001CC0
		[RequiredByNativeCode]
		internal static void CallSessionStateChanged(AnalyticsSessionState sessionState, long sessionId, long sessionElapsedTime, bool sessionChanged)
		{
			AnalyticsSessionInfo.SessionStateChanged sessionStateChanged = AnalyticsSessionInfo.sessionStateChanged;
			bool flag = sessionStateChanged != null;
			if (flag)
			{
				sessionStateChanged(sessionState, sessionId, sessionElapsedTime, sessionChanged);
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000097 RID: 151
		public static extern AnalyticsSessionState sessionState
		{
			[NativeMethod("GetPlayerSessionState")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000098 RID: 152
		public static extern long sessionId
		{
			[NativeMethod("GetPlayerSessionId")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000099 RID: 153
		public static extern long sessionCount
		{
			[NativeMethod("GetPlayerSessionCount")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600009A RID: 154
		public static extern long sessionElapsedTime
		{
			[NativeMethod("GetPlayerSessionElapsedTime")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600009B RID: 155
		public static extern bool sessionFirstRun
		{
			[NativeMethod("GetPlayerSessionFirstRun", false, true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00003AE8 File Offset: 0x00001CE8
		public static string userId
		{
			[NativeMethod("GetUserId")]
			get
			{
				string stringAndDispose;
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					AnalyticsSessionInfo.get_userId_Injected(out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00003B18 File Offset: 0x00001D18
		// (set) Token: 0x0600009E RID: 158 RVA: 0x00003B40 File Offset: 0x00001D40
		public static string customUserId
		{
			get
			{
				bool flag = !Analytics.IsInitialized();
				string text;
				if (flag)
				{
					text = null;
				}
				else
				{
					text = AnalyticsSessionInfo.customUserIdInternal;
				}
				return text;
			}
			set
			{
				bool flag = Analytics.IsInitialized();
				if (flag)
				{
					AnalyticsSessionInfo.customUserIdInternal = value;
				}
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00003B60 File Offset: 0x00001D60
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x00003B88 File Offset: 0x00001D88
		public static string customDeviceId
		{
			get
			{
				bool flag = !Analytics.IsInitialized();
				string text;
				if (flag)
				{
					text = null;
				}
				else
				{
					text = AnalyticsSessionInfo.customDeviceIdInternal;
				}
				return text;
			}
			set
			{
				bool flag = Analytics.IsInitialized();
				if (flag)
				{
					AnalyticsSessionInfo.customDeviceIdInternal = value;
				}
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060000A1 RID: 161 RVA: 0x00003BA8 File Offset: 0x00001DA8
		// (remove) Token: 0x060000A2 RID: 162 RVA: 0x00003BDC File Offset: 0x00001DDC
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event AnalyticsSessionInfo.IdentityTokenChanged identityTokenChanged;

		// Token: 0x060000A3 RID: 163 RVA: 0x00003C10 File Offset: 0x00001E10
		[RequiredByNativeCode]
		internal static void CallIdentityTokenChanged(string token)
		{
			AnalyticsSessionInfo.IdentityTokenChanged identityTokenChanged = AnalyticsSessionInfo.identityTokenChanged;
			bool flag = identityTokenChanged != null;
			if (flag)
			{
				identityTokenChanged(token);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00003C34 File Offset: 0x00001E34
		public static string identityToken
		{
			get
			{
				bool flag = !Analytics.IsInitialized();
				string text;
				if (flag)
				{
					text = null;
				}
				else
				{
					text = AnalyticsSessionInfo.identityTokenInternal;
				}
				return text;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00003C5C File Offset: 0x00001E5C
		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		private static string identityTokenInternal
		{
			[NativeMethod("GetIdentityToken")]
			get
			{
				string stringAndDispose;
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					AnalyticsSessionInfo.get_identityTokenInternal_Injected(out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00003C8C File Offset: 0x00001E8C
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00003CBC File Offset: 0x00001EBC
		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		private unsafe static string customUserIdInternal
		{
			[NativeMethod("GetCustomUserId")]
			get
			{
				string stringAndDispose;
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					AnalyticsSessionInfo.get_customUserIdInternal_Injected(out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
			[NativeMethod("SetCustomUserId")]
			set
			{
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					if (!StringMarshaller.TryMarshalEmptyOrNullString(value, ref managedSpanWrapper))
					{
						ReadOnlySpan<char> readOnlySpan = value.AsSpan();
						fixed (char* ptr = readOnlySpan.GetPinnableReference())
						{
							managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
						}
					}
					AnalyticsSessionInfo.set_customUserIdInternal_Injected(ref managedSpanWrapper);
				}
				finally
				{
					char* ptr = null;
				}
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00003D10 File Offset: 0x00001F10
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x00003D40 File Offset: 0x00001F40
		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		private unsafe static string customDeviceIdInternal
		{
			[NativeMethod("GetCustomDeviceId")]
			get
			{
				string stringAndDispose;
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					AnalyticsSessionInfo.get_customDeviceIdInternal_Injected(out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
			[NativeMethod("SetCustomDeviceId")]
			set
			{
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					if (!StringMarshaller.TryMarshalEmptyOrNullString(value, ref managedSpanWrapper))
					{
						ReadOnlySpan<char> readOnlySpan = value.AsSpan();
						fixed (char* ptr = readOnlySpan.GetPinnableReference())
						{
							managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
						}
					}
					AnalyticsSessionInfo.set_customDeviceIdInternal_Injected(ref managedSpanWrapper);
				}
				finally
				{
					char* ptr = null;
				}
			}
		}

		// Token: 0x060000AA RID: 170
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_userId_Injected(out ManagedSpanWrapper ret);

		// Token: 0x060000AB RID: 171
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_identityTokenInternal_Injected(out ManagedSpanWrapper ret);

		// Token: 0x060000AC RID: 172
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_customUserIdInternal_Injected(out ManagedSpanWrapper ret);

		// Token: 0x060000AD RID: 173
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_customUserIdInternal_Injected(ref ManagedSpanWrapper value);

		// Token: 0x060000AE RID: 174
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_customDeviceIdInternal_Injected(out ManagedSpanWrapper ret);

		// Token: 0x060000AF RID: 175
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_customDeviceIdInternal_Injected(ref ManagedSpanWrapper value);

		// Token: 0x0200000B RID: 11
		// (Invoke) Token: 0x060000B1 RID: 177
		public delegate void SessionStateChanged(AnalyticsSessionState sessionState, long sessionId, long sessionElapsedTime, bool sessionChanged);

		// Token: 0x0200000C RID: 12
		// (Invoke) Token: 0x060000B5 RID: 181
		public delegate void IdentityTokenChanged(string token);
	}
}
