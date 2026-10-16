using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Analytics;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000004 RID: 4
	[NativeHeader("Modules/UnityAnalytics/RemoteSettings/RemoteSettings.h")]
	[NativeHeader("UnityAnalyticsScriptingClasses.h")]
	[ExcludeFromDocs]
	[NativeHeader("Modules/UnityAnalyticsCommon/Public/UnityAnalyticsCommon.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class RemoteConfigSettings : IDisposable
	{
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600002D RID: 45 RVA: 0x0000266C File Offset: 0x0000086C
		// (remove) Token: 0x0600002E RID: 46 RVA: 0x000026A4 File Offset: 0x000008A4
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<bool> Updated;

		// Token: 0x0600002F RID: 47 RVA: 0x000026D9 File Offset: 0x000008D9
		private RemoteConfigSettings()
		{
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000026E3 File Offset: 0x000008E3
		public RemoteConfigSettings(string configKey)
		{
			this.m_Ptr = RemoteConfigSettings.Internal_Create(this, configKey);
			this.Updated = null;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002704 File Offset: 0x00000904
		~RemoteConfigSettings()
		{
			this.Destroy();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002734 File Offset: 0x00000934
		private void Destroy()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				RemoteConfigSettings.Internal_Destroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000276F File Offset: 0x0000096F
		public void Dispose()
		{
			this.Destroy();
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002780 File Offset: 0x00000980
		internal unsafe static IntPtr Internal_Create([Unmarshalled] RemoteConfigSettings rcs, string configKey)
		{
			IntPtr intPtr;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(configKey, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = configKey.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				intPtr = RemoteConfigSettings.Internal_Create_Injected(rcs, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return intPtr;
		}

		// Token: 0x06000035 RID: 53
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_Destroy(IntPtr ptr);

		// Token: 0x06000036 RID: 54 RVA: 0x000027D8 File Offset: 0x000009D8
		[RequiredByNativeCode]
		internal static void RemoteConfigSettingsUpdated(RemoteConfigSettings rcs, bool wasLastUpdatedFromServer)
		{
			Action<bool> updated = rcs.Updated;
			bool flag = updated != null;
			if (flag)
			{
				updated(wasLastUpdatedFromServer);
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002800 File Offset: 0x00000A00
		public unsafe static AnalyticsResult QueueConfig(string name, object param, int ver = 1, string prefix = "")
		{
			AnalyticsResult analyticsResult;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(prefix, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = prefix.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				analyticsResult = RemoteConfigSettings.QueueConfig_Injected(ref managedSpanWrapper, param, ver, ref managedSpanWrapper2);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
			return analyticsResult;
		}

		// Token: 0x06000038 RID: 56
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool SendDeviceInfoInConfigRequest();

		// Token: 0x06000039 RID: 57 RVA: 0x0000288C File Offset: 0x00000A8C
		public unsafe static void AddSessionTag(string tag)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(tag, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = tag.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				RemoteConfigSettings.AddSessionTag_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000028E0 File Offset: 0x00000AE0
		public void ForceUpdate()
		{
			IntPtr intPtr = RemoteConfigSettings.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RemoteConfigSettings.ForceUpdate_Injected(intPtr);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002904 File Offset: 0x00000B04
		public bool WasLastUpdatedFromServer()
		{
			IntPtr intPtr = RemoteConfigSettings.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return RemoteConfigSettings.WasLastUpdatedFromServer_Injected(intPtr);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002928 File Offset: 0x00000B28
		[ExcludeFromDocs]
		public int GetInt(string key)
		{
			return this.GetInt(key, 0);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002944 File Offset: 0x00000B44
		public unsafe int GetInt(string key, [DefaultValue("0")] int defaultValue)
		{
			int int_Injected;
			try
			{
				IntPtr intPtr = RemoteConfigSettings.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(key, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = key.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				int_Injected = RemoteConfigSettings.GetInt_Injected(intPtr, ref managedSpanWrapper, defaultValue);
			}
			finally
			{
				char* ptr = null;
			}
			return int_Injected;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000029AC File Offset: 0x00000BAC
		[ExcludeFromDocs]
		public long GetLong(string key)
		{
			return this.GetLong(key, 0L);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000029C8 File Offset: 0x00000BC8
		public unsafe long GetLong(string key, [DefaultValue("0")] long defaultValue)
		{
			long long_Injected;
			try
			{
				IntPtr intPtr = RemoteConfigSettings.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(key, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = key.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				long_Injected = RemoteConfigSettings.GetLong_Injected(intPtr, ref managedSpanWrapper, defaultValue);
			}
			finally
			{
				char* ptr = null;
			}
			return long_Injected;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002A30 File Offset: 0x00000C30
		[ExcludeFromDocs]
		public float GetFloat(string key)
		{
			return this.GetFloat(key, 0f);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002A50 File Offset: 0x00000C50
		public unsafe float GetFloat(string key, [DefaultValue("0.0F")] float defaultValue)
		{
			float float_Injected;
			try
			{
				IntPtr intPtr = RemoteConfigSettings.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(key, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = key.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				float_Injected = RemoteConfigSettings.GetFloat_Injected(intPtr, ref managedSpanWrapper, defaultValue);
			}
			finally
			{
				char* ptr = null;
			}
			return float_Injected;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002AB8 File Offset: 0x00000CB8
		[ExcludeFromDocs]
		public string GetString(string key)
		{
			return this.GetString(key, "");
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002AD8 File Offset: 0x00000CD8
		public unsafe string GetString(string key, [DefaultValue("\"\"")] string defaultValue)
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = RemoteConfigSettings.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(key, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = key.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(defaultValue, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = defaultValue.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper3;
				RemoteConfigSettings.GetString_Injected(intPtr, ref managedSpanWrapper, ref managedSpanWrapper2, out managedSpanWrapper3);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
				ManagedSpanWrapper managedSpanWrapper3;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper3);
			}
			return stringAndDispose;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002B7C File Offset: 0x00000D7C
		[ExcludeFromDocs]
		public bool GetBool(string key)
		{
			return this.GetBool(key, false);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002B98 File Offset: 0x00000D98
		public unsafe bool GetBool(string key, [DefaultValue("false")] bool defaultValue)
		{
			bool bool_Injected;
			try
			{
				IntPtr intPtr = RemoteConfigSettings.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(key, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = key.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				bool_Injected = RemoteConfigSettings.GetBool_Injected(intPtr, ref managedSpanWrapper, defaultValue);
			}
			finally
			{
				char* ptr = null;
			}
			return bool_Injected;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002C00 File Offset: 0x00000E00
		public unsafe bool HasKey(string key)
		{
			bool flag;
			try
			{
				IntPtr intPtr = RemoteConfigSettings.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(key, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = key.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = RemoteConfigSettings.HasKey_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002C68 File Offset: 0x00000E68
		public int GetCount()
		{
			IntPtr intPtr = RemoteConfigSettings.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return RemoteConfigSettings.GetCount_Injected(intPtr);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002C8C File Offset: 0x00000E8C
		public string[] GetKeys()
		{
			IntPtr intPtr = RemoteConfigSettings.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return RemoteConfigSettings.GetKeys_Injected(intPtr);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002CB0 File Offset: 0x00000EB0
		public T GetObject<T>(string key = "")
		{
			return (T)((object)this.GetObject(typeof(T), key));
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002CD8 File Offset: 0x00000ED8
		public object GetObject(Type type, string key = "")
		{
			bool flag = type == null;
			if (flag)
			{
				throw new ArgumentNullException("type");
			}
			bool flag2 = type.IsAbstract || type.IsSubclassOf(typeof(Object));
			if (flag2)
			{
				throw new ArgumentException("Cannot deserialize to new instances of type '" + type.Name + ".'");
			}
			return this.GetAsScriptingObject(type, null, key);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002D44 File Offset: 0x00000F44
		public object GetObject(string key, object defaultValue)
		{
			bool flag = defaultValue == null;
			if (flag)
			{
				throw new ArgumentNullException("defaultValue");
			}
			Type type = defaultValue.GetType();
			bool flag2 = type.IsAbstract || type.IsSubclassOf(typeof(Object));
			if (flag2)
			{
				throw new ArgumentException("Cannot deserialize to new instances of type '" + type.Name + ".'");
			}
			return this.GetAsScriptingObject(type, defaultValue, key);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002DB4 File Offset: 0x00000FB4
		internal unsafe object GetAsScriptingObject(Type t, object defaultValue, string key)
		{
			object asScriptingObject_Injected;
			try
			{
				IntPtr intPtr = RemoteConfigSettings.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(key, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = key.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				asScriptingObject_Injected = RemoteConfigSettings.GetAsScriptingObject_Injected(intPtr, t, defaultValue, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return asScriptingObject_Injected;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002E1C File Offset: 0x0000101C
		public IDictionary<string, object> GetDictionary(string key = "")
		{
			this.UseSafeLock();
			IDictionary<string, object> dictionary = RemoteConfigSettingsHelper.GetDictionary(this.GetSafeTopMap(), key);
			this.ReleaseSafeLock();
			return dictionary;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002E4C File Offset: 0x0000104C
		internal void UseSafeLock()
		{
			IntPtr intPtr = RemoteConfigSettings.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RemoteConfigSettings.UseSafeLock_Injected(intPtr);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002E70 File Offset: 0x00001070
		internal void ReleaseSafeLock()
		{
			IntPtr intPtr = RemoteConfigSettings.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RemoteConfigSettings.ReleaseSafeLock_Injected(intPtr);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002E94 File Offset: 0x00001094
		internal IntPtr GetSafeTopMap()
		{
			IntPtr intPtr = RemoteConfigSettings.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return RemoteConfigSettings.GetSafeTopMap_Injected(intPtr);
		}

		// Token: 0x06000051 RID: 81
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_Create_Injected(RemoteConfigSettings rcs, ref ManagedSpanWrapper configKey);

		// Token: 0x06000052 RID: 82
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnalyticsResult QueueConfig_Injected(ref ManagedSpanWrapper name, object param, int ver, ref ManagedSpanWrapper prefix);

		// Token: 0x06000053 RID: 83
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddSessionTag_Injected(ref ManagedSpanWrapper tag);

		// Token: 0x06000054 RID: 84
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ForceUpdate_Injected(IntPtr _unity_self);

		// Token: 0x06000055 RID: 85
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool WasLastUpdatedFromServer_Injected(IntPtr _unity_self);

		// Token: 0x06000056 RID: 86
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetInt_Injected(IntPtr _unity_self, ref ManagedSpanWrapper key, [DefaultValue("0")] int defaultValue);

		// Token: 0x06000057 RID: 87
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long GetLong_Injected(IntPtr _unity_self, ref ManagedSpanWrapper key, [DefaultValue("0")] long defaultValue);

		// Token: 0x06000058 RID: 88
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetFloat_Injected(IntPtr _unity_self, ref ManagedSpanWrapper key, [DefaultValue("0.0F")] float defaultValue);

		// Token: 0x06000059 RID: 89
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetString_Injected(IntPtr _unity_self, ref ManagedSpanWrapper key, [DefaultValue("\"\"")] ref ManagedSpanWrapper defaultValue, out ManagedSpanWrapper ret);

		// Token: 0x0600005A RID: 90
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetBool_Injected(IntPtr _unity_self, ref ManagedSpanWrapper key, [DefaultValue("false")] bool defaultValue);

		// Token: 0x0600005B RID: 91
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasKey_Injected(IntPtr _unity_self, ref ManagedSpanWrapper key);

		// Token: 0x0600005C RID: 92
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetCount_Injected(IntPtr _unity_self);

		// Token: 0x0600005D RID: 93
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] GetKeys_Injected(IntPtr _unity_self);

		// Token: 0x0600005E RID: 94
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object GetAsScriptingObject_Injected(IntPtr _unity_self, Type t, object defaultValue, ref ManagedSpanWrapper key);

		// Token: 0x0600005F RID: 95
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UseSafeLock_Injected(IntPtr _unity_self);

		// Token: 0x06000060 RID: 96
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReleaseSafeLock_Injected(IntPtr _unity_self);

		// Token: 0x06000061 RID: 97
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetSafeTopMap_Injected(IntPtr _unity_self);

		// Token: 0x04000004 RID: 4
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x02000005 RID: 5
		internal static class BindingsMarshaller
		{
			// Token: 0x06000062 RID: 98 RVA: 0x00002EB6 File Offset: 0x000010B6
			public static IntPtr ConvertToNative(RemoteConfigSettings remoteConfigSettings)
			{
				return remoteConfigSettings.m_Ptr;
			}
		}
	}
}
