using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.Analytics
{
	// Token: 0x0200000F RID: 15
	[NativeHeader("Modules/UnityAnalytics/Public/UnityAnalytics.h")]
	[NativeHeader("Modules/UnityAnalyticsCommon/Public/UnityAnalyticsCommon.h")]
	[NativeHeader("Modules/UnityConnect/UnityConnectSettings.h")]
	[NativeHeader("Modules/UnityAnalytics/Public/Events/UserCustomEvent.h")]
	[StructLayout(LayoutKind.Sequential)]
	public static class Analytics
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x000044C0 File Offset: 0x000026C0
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x000044E8 File Offset: 0x000026E8
		public static bool initializeOnStartup
		{
			get
			{
				bool flag = !Analytics.IsInitialized();
				return !flag && Analytics.initializeOnStartupInternal;
			}
			set
			{
				bool flag = Analytics.IsInitialized();
				if (flag)
				{
					Analytics.initializeOnStartupInternal = value;
				}
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004508 File Offset: 0x00002708
		public static AnalyticsResult ResumeInitialization()
		{
			bool flag = !Analytics.IsInitialized();
			AnalyticsResult analyticsResult;
			if (flag)
			{
				analyticsResult = AnalyticsResult.NotInitialized;
			}
			else
			{
				analyticsResult = Analytics.ResumeInitializationInternal();
			}
			return analyticsResult;
		}

		// Token: 0x060000D3 RID: 211
		[NativeMethod("ResumeInitialization")]
		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnalyticsResult ResumeInitializationInternal();

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000D4 RID: 212
		// (set) Token: 0x060000D5 RID: 213
		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		private static extern bool initializeOnStartupInternal
		{
			[NativeMethod("GetInitializeOnStartup")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeMethod("SetInitializeOnStartup")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x060000D6 RID: 214
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsInitialized();

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000D7 RID: 215
		// (set) Token: 0x060000D8 RID: 216
		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		private static extern bool enabledInternal
		{
			[NativeMethod("GetEnabled")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeMethod("SetEnabled")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000D9 RID: 217
		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		private static extern bool playerOptedOutInternal
		{
			[NativeMethod("GetPlayerOptedOut")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00004530 File Offset: 0x00002730
		[StaticAccessor("GetUnityConnectSettings()", StaticAccessorType.Dot)]
		private static string eventUrlInternal
		{
			[NativeMethod("GetEventUrl")]
			get
			{
				string stringAndDispose;
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					Analytics.get_eventUrlInternal_Injected(out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00004560 File Offset: 0x00002760
		[StaticAccessor("GetUnityConnectSettings()", StaticAccessorType.Dot)]
		private static string configUrlInternal
		{
			[NativeMethod("GetConfigUrl")]
			get
			{
				string stringAndDispose;
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					Analytics.get_configUrlInternal_Injected(out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00004590 File Offset: 0x00002790
		[StaticAccessor("GetUnityConnectSettings()", StaticAccessorType.Dot)]
		private static string dashboardUrlInternal
		{
			[NativeMethod("GetDashboardUrl")]
			get
			{
				string stringAndDispose;
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					Analytics.get_dashboardUrlInternal_Injected(out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000DD RID: 221
		// (set) Token: 0x060000DE RID: 222
		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		private static extern bool limitUserTrackingInternal
		{
			[NativeMethod("GetLimitUserTracking")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeMethod("SetLimitUserTracking")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000DF RID: 223
		// (set) Token: 0x060000E0 RID: 224
		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		private static extern bool deviceStatsEnabledInternal
		{
			[NativeMethod("GetDeviceStatsEnabled")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeMethod("SetDeviceStatsEnabled")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x060000E1 RID: 225
		[NativeMethod("FlushEvents")]
		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool FlushArchivedEvents();

		// Token: 0x060000E2 RID: 226 RVA: 0x000045C0 File Offset: 0x000027C0
		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		private unsafe static AnalyticsResult Transaction(string productId, double amount, string currency, string receiptPurchaseData, string signature, bool usingIAPService)
		{
			AnalyticsResult analyticsResult;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(productId, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = productId.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(currency, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = currency.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper3;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(receiptPurchaseData, ref managedSpanWrapper3))
				{
					ReadOnlySpan<char> readOnlySpan3 = receiptPurchaseData.AsSpan();
					fixed (char* ptr3 = readOnlySpan3.GetPinnableReference())
					{
						managedSpanWrapper3 = new ManagedSpanWrapper((void*)ptr3, readOnlySpan3.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper4;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(signature, ref managedSpanWrapper4))
				{
					ReadOnlySpan<char> readOnlySpan4 = signature.AsSpan();
					fixed (char* ptr4 = readOnlySpan4.GetPinnableReference())
					{
						managedSpanWrapper4 = new ManagedSpanWrapper((void*)ptr4, readOnlySpan4.Length);
					}
				}
				analyticsResult = Analytics.Transaction_Injected(ref managedSpanWrapper, amount, ref managedSpanWrapper2, ref managedSpanWrapper3, ref managedSpanWrapper4, usingIAPService);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
				char* ptr3 = null;
				char* ptr4 = null;
			}
			return analyticsResult;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000046B8 File Offset: 0x000028B8
		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		private unsafe static AnalyticsResult SendCustomEventName(string customEventName)
		{
			AnalyticsResult analyticsResult;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(customEventName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = customEventName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				analyticsResult = Analytics.SendCustomEventName_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return analyticsResult;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004710 File Offset: 0x00002910
		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		private static AnalyticsResult SendCustomEvent(CustomEventData eventData)
		{
			return Analytics.SendCustomEvent_Injected((eventData == null) ? ((IntPtr)0) : CustomEventData.BindingsMarshaller.ConvertToNative(eventData));
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004734 File Offset: 0x00002934
		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		internal unsafe static AnalyticsResult IsCustomEventWithLimitEnabled(string customEventName)
		{
			AnalyticsResult analyticsResult;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(customEventName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = customEventName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				analyticsResult = Analytics.IsCustomEventWithLimitEnabled_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return analyticsResult;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000478C File Offset: 0x0000298C
		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		internal unsafe static AnalyticsResult EnableCustomEventWithLimit(string customEventName, bool enable)
		{
			AnalyticsResult analyticsResult;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(customEventName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = customEventName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				analyticsResult = Analytics.EnableCustomEventWithLimit_Injected(ref managedSpanWrapper, enable);
			}
			finally
			{
				char* ptr = null;
			}
			return analyticsResult;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000047E4 File Offset: 0x000029E4
		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		internal unsafe static AnalyticsResult IsEventWithLimitEnabled(string eventName, int ver, string prefix)
		{
			AnalyticsResult analyticsResult;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(eventName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = eventName.AsSpan();
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
				analyticsResult = Analytics.IsEventWithLimitEnabled_Injected(ref managedSpanWrapper, ver, ref managedSpanWrapper2);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
			return analyticsResult;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004870 File Offset: 0x00002A70
		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		internal unsafe static AnalyticsResult EnableEventWithLimit(string eventName, bool enable, int ver, string prefix)
		{
			AnalyticsResult analyticsResult;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(eventName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = eventName.AsSpan();
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
				analyticsResult = Analytics.EnableEventWithLimit_Injected(ref managedSpanWrapper, enable, ver, ref managedSpanWrapper2);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
			return analyticsResult;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000048FC File Offset: 0x00002AFC
		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		internal unsafe static AnalyticsResult RegisterEventWithLimit(string eventName, int maxEventPerHour, int maxItems, string vendorKey, int ver, string prefix, string assemblyInfo, bool notifyServer)
		{
			AnalyticsResult analyticsResult;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(eventName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = eventName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(vendorKey, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = vendorKey.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper3;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(prefix, ref managedSpanWrapper3))
				{
					ReadOnlySpan<char> readOnlySpan3 = prefix.AsSpan();
					fixed (char* ptr3 = readOnlySpan3.GetPinnableReference())
					{
						managedSpanWrapper3 = new ManagedSpanWrapper((void*)ptr3, readOnlySpan3.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper4;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(assemblyInfo, ref managedSpanWrapper4))
				{
					ReadOnlySpan<char> readOnlySpan4 = assemblyInfo.AsSpan();
					fixed (char* ptr4 = readOnlySpan4.GetPinnableReference())
					{
						managedSpanWrapper4 = new ManagedSpanWrapper((void*)ptr4, readOnlySpan4.Length);
					}
				}
				analyticsResult = Analytics.RegisterEventWithLimit_Injected(ref managedSpanWrapper, maxEventPerHour, maxItems, ref managedSpanWrapper2, ver, ref managedSpanWrapper3, ref managedSpanWrapper4, notifyServer);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
				char* ptr3 = null;
				char* ptr4 = null;
			}
			return analyticsResult;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000049F8 File Offset: 0x00002BF8
		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		internal unsafe static AnalyticsResult RegisterEventsWithLimit(string[] eventName, int maxEventPerHour, int maxItems, string vendorKey, int ver, string prefix, string assemblyInfo, bool notifyServer)
		{
			AnalyticsResult analyticsResult;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(vendorKey, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = vendorKey.AsSpan();
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
				ManagedSpanWrapper managedSpanWrapper3;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(assemblyInfo, ref managedSpanWrapper3))
				{
					ReadOnlySpan<char> readOnlySpan3 = assemblyInfo.AsSpan();
					fixed (char* ptr3 = readOnlySpan3.GetPinnableReference())
					{
						managedSpanWrapper3 = new ManagedSpanWrapper((void*)ptr3, readOnlySpan3.Length);
					}
				}
				analyticsResult = Analytics.RegisterEventsWithLimit_Injected(eventName, maxEventPerHour, maxItems, ref managedSpanWrapper, ver, ref managedSpanWrapper2, ref managedSpanWrapper3, notifyServer);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
				char* ptr3 = null;
			}
			return analyticsResult;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004AC0 File Offset: 0x00002CC0
		[ThreadSafe]
		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		internal unsafe static AnalyticsResult SendEventWithLimit(string eventName, object parameters, int ver, string prefix)
		{
			AnalyticsResult analyticsResult;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(eventName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = eventName.AsSpan();
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
				analyticsResult = Analytics.SendEventWithLimit_Injected(ref managedSpanWrapper, parameters, ver, ref managedSpanWrapper2);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
			return analyticsResult;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004B4C File Offset: 0x00002D4C
		[ThreadSafe]
		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		internal unsafe static AnalyticsResult SetEventWithLimitEndPoint(string eventName, string endPoint, int ver, string prefix)
		{
			AnalyticsResult analyticsResult;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(eventName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = eventName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(endPoint, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = endPoint.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper3;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(prefix, ref managedSpanWrapper3))
				{
					ReadOnlySpan<char> readOnlySpan3 = prefix.AsSpan();
					fixed (char* ptr3 = readOnlySpan3.GetPinnableReference())
					{
						managedSpanWrapper3 = new ManagedSpanWrapper((void*)ptr3, readOnlySpan3.Length);
					}
				}
				analyticsResult = Analytics.SetEventWithLimitEndPoint_Injected(ref managedSpanWrapper, ref managedSpanWrapper2, ver, ref managedSpanWrapper3);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
				char* ptr3 = null;
			}
			return analyticsResult;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004C0C File Offset: 0x00002E0C
		[ThreadSafe]
		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		internal unsafe static AnalyticsResult SetEventWithLimitPriority(string eventName, AnalyticsEventPriority eventPriority, int ver, string prefix)
		{
			AnalyticsResult analyticsResult;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(eventName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = eventName.AsSpan();
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
				analyticsResult = Analytics.SetEventWithLimitPriority_Injected(ref managedSpanWrapper, eventPriority, ver, ref managedSpanWrapper2);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
			return analyticsResult;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004C98 File Offset: 0x00002E98
		[ThreadSafe]
		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		internal unsafe static AnalyticsResult QueueEvent(string eventName, object parameters, int ver, string prefix)
		{
			AnalyticsResult analyticsResult;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(eventName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = eventName.AsSpan();
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
				analyticsResult = Analytics.QueueEvent_Injected(ref managedSpanWrapper, parameters, ver, ref managedSpanWrapper2);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
			return analyticsResult;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00004D24 File Offset: 0x00002F24
		public static bool playerOptedOut
		{
			get
			{
				bool flag = !Analytics.IsInitialized();
				return !flag && Analytics.playerOptedOutInternal;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00004D4C File Offset: 0x00002F4C
		public static string eventUrl
		{
			get
			{
				bool flag = !Analytics.IsInitialized();
				string text;
				if (flag)
				{
					text = string.Empty;
				}
				else
				{
					text = Analytics.eventUrlInternal;
				}
				return text;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00004D78 File Offset: 0x00002F78
		public static string dashboardUrl
		{
			get
			{
				bool flag = !Analytics.IsInitialized();
				string text;
				if (flag)
				{
					text = string.Empty;
				}
				else
				{
					text = Analytics.dashboardUrlInternal;
				}
				return text;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00004DA4 File Offset: 0x00002FA4
		public static string configUrl
		{
			get
			{
				bool flag = !Analytics.IsInitialized();
				string text;
				if (flag)
				{
					text = string.Empty;
				}
				else
				{
					text = Analytics.configUrlInternal;
				}
				return text;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00004DD0 File Offset: 0x00002FD0
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x00004DF8 File Offset: 0x00002FF8
		public static bool limitUserTracking
		{
			get
			{
				bool flag = !Analytics.IsInitialized();
				return !flag && Analytics.limitUserTrackingInternal;
			}
			set
			{
				bool flag = Analytics.IsInitialized();
				if (flag)
				{
					Analytics.limitUserTrackingInternal = value;
				}
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00004E18 File Offset: 0x00003018
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x00004E40 File Offset: 0x00003040
		public static bool deviceStatsEnabled
		{
			get
			{
				bool flag = !Analytics.IsInitialized();
				return !flag && Analytics.deviceStatsEnabledInternal;
			}
			set
			{
				bool flag = Analytics.IsInitialized();
				if (flag)
				{
					Analytics.deviceStatsEnabledInternal = value;
				}
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00004E60 File Offset: 0x00003060
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x00004E88 File Offset: 0x00003088
		public static bool enabled
		{
			get
			{
				bool flag = !Analytics.IsInitialized();
				return !flag && Analytics.enabledInternal;
			}
			set
			{
				bool flag = Analytics.IsInitialized();
				if (flag)
				{
					Analytics.enabledInternal = value;
				}
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00004EA8 File Offset: 0x000030A8
		public static AnalyticsResult FlushEvents()
		{
			bool flag = !Analytics.IsInitialized();
			AnalyticsResult analyticsResult;
			if (flag)
			{
				analyticsResult = AnalyticsResult.NotInitialized;
			}
			else
			{
				analyticsResult = (Analytics.FlushArchivedEvents() ? AnalyticsResult.Ok : AnalyticsResult.NotInitialized);
			}
			return analyticsResult;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004ED8 File Offset: 0x000030D8
		[Obsolete("SetUserId is no longer supported", true)]
		public static AnalyticsResult SetUserId(string userId)
		{
			bool flag = string.IsNullOrEmpty(userId);
			if (flag)
			{
				throw new ArgumentException("Cannot set userId to an empty or null string");
			}
			return AnalyticsResult.InvalidData;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00004F00 File Offset: 0x00003100
		[Obsolete("SetUserGender is no longer supported", true)]
		public static AnalyticsResult SetUserGender(Gender gender)
		{
			return AnalyticsResult.InvalidData;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00004F14 File Offset: 0x00003114
		[Obsolete("SetUserBirthYear is no longer supported", true)]
		public static AnalyticsResult SetUserBirthYear(int birthYear)
		{
			return AnalyticsResult.InvalidData;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00004F28 File Offset: 0x00003128
		[Obsolete("SendUserInfoEvent is no longer supported", true)]
		private static AnalyticsResult SendUserInfoEvent(object param)
		{
			return AnalyticsResult.InvalidData;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004F3C File Offset: 0x0000313C
		public static AnalyticsResult Transaction(string productId, decimal amount, string currency)
		{
			return Analytics.Transaction(productId, amount, currency, null, null, false);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00004F5C File Offset: 0x0000315C
		public static AnalyticsResult Transaction(string productId, decimal amount, string currency, string receiptPurchaseData, string signature)
		{
			return Analytics.Transaction(productId, amount, currency, receiptPurchaseData, signature, false);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00004F7C File Offset: 0x0000317C
		public static AnalyticsResult Transaction(string productId, decimal amount, string currency, string receiptPurchaseData, string signature, bool usingIAPService)
		{
			bool flag = string.IsNullOrEmpty(productId);
			if (flag)
			{
				throw new ArgumentException("Cannot set productId to an empty or null string");
			}
			bool flag2 = string.IsNullOrEmpty(currency);
			if (flag2)
			{
				throw new ArgumentException("Cannot set currency to an empty or null string");
			}
			bool flag3 = !Analytics.IsInitialized();
			AnalyticsResult analyticsResult;
			if (flag3)
			{
				analyticsResult = AnalyticsResult.NotInitialized;
			}
			else
			{
				bool flag4 = receiptPurchaseData == null;
				if (flag4)
				{
					receiptPurchaseData = string.Empty;
				}
				bool flag5 = signature == null;
				if (flag5)
				{
					signature = string.Empty;
				}
				analyticsResult = Analytics.Transaction(productId, Convert.ToDouble(amount), currency, receiptPurchaseData, signature, usingIAPService);
			}
			return analyticsResult;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005000 File Offset: 0x00003200
		public static AnalyticsResult CustomEvent(string customEventName)
		{
			bool flag = string.IsNullOrEmpty(customEventName);
			if (flag)
			{
				throw new ArgumentException("Cannot set custom event name to an empty or null string");
			}
			bool flag2 = !Analytics.IsInitialized();
			AnalyticsResult analyticsResult;
			if (flag2)
			{
				analyticsResult = AnalyticsResult.NotInitialized;
			}
			else
			{
				analyticsResult = Analytics.SendCustomEventName(customEventName);
			}
			return analyticsResult;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005040 File Offset: 0x00003240
		public static AnalyticsResult CustomEvent(string customEventName, Vector3 position)
		{
			bool flag = string.IsNullOrEmpty(customEventName);
			if (flag)
			{
				throw new ArgumentException("Cannot set custom event name to an empty or null string");
			}
			bool flag2 = !Analytics.IsInitialized();
			AnalyticsResult analyticsResult;
			if (flag2)
			{
				analyticsResult = AnalyticsResult.NotInitialized;
			}
			else
			{
				CustomEventData customEventData = new CustomEventData(customEventName);
				customEventData.AddDouble("x", (double)Convert.ToDecimal(position.x));
				customEventData.AddDouble("y", (double)Convert.ToDecimal(position.y));
				customEventData.AddDouble("z", (double)Convert.ToDecimal(position.z));
				AnalyticsResult analyticsResult2 = Analytics.SendCustomEvent(customEventData);
				customEventData.Dispose();
				analyticsResult = analyticsResult2;
			}
			return analyticsResult;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000050E8 File Offset: 0x000032E8
		public static AnalyticsResult CustomEvent(string customEventName, IDictionary<string, object> eventData)
		{
			bool flag = string.IsNullOrEmpty(customEventName);
			if (flag)
			{
				throw new ArgumentException("Cannot set custom event name to an empty or null string");
			}
			bool flag2 = !Analytics.IsInitialized();
			AnalyticsResult analyticsResult;
			if (flag2)
			{
				analyticsResult = AnalyticsResult.NotInitialized;
			}
			else
			{
				bool flag3 = eventData == null;
				if (flag3)
				{
					analyticsResult = Analytics.SendCustomEventName(customEventName);
				}
				else
				{
					CustomEventData customEventData = new CustomEventData(customEventName);
					AnalyticsResult analyticsResult2 = AnalyticsResult.InvalidData;
					try
					{
						customEventData.AddDictionary(eventData);
						analyticsResult2 = Analytics.SendCustomEvent(customEventData);
					}
					finally
					{
						customEventData.Dispose();
					}
					analyticsResult = analyticsResult2;
				}
			}
			return analyticsResult;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005170 File Offset: 0x00003370
		public static AnalyticsResult EnableCustomEvent(string customEventName, bool enabled)
		{
			bool flag = string.IsNullOrEmpty(customEventName);
			if (flag)
			{
				throw new ArgumentException("Cannot set event name to an empty or null string");
			}
			bool flag2 = !Analytics.IsInitialized();
			AnalyticsResult analyticsResult;
			if (flag2)
			{
				analyticsResult = AnalyticsResult.NotInitialized;
			}
			else
			{
				analyticsResult = Analytics.EnableCustomEventWithLimit(customEventName, enabled);
			}
			return analyticsResult;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000051B0 File Offset: 0x000033B0
		public static AnalyticsResult IsCustomEventEnabled(string customEventName)
		{
			bool flag = string.IsNullOrEmpty(customEventName);
			if (flag)
			{
				throw new ArgumentException("Cannot set event name to an empty or null string");
			}
			bool flag2 = !Analytics.IsInitialized();
			AnalyticsResult analyticsResult;
			if (flag2)
			{
				analyticsResult = AnalyticsResult.NotInitialized;
			}
			else
			{
				analyticsResult = Analytics.IsCustomEventWithLimitEnabled(customEventName);
			}
			return analyticsResult;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000051F0 File Offset: 0x000033F0
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static AnalyticsResult RegisterEvent(string eventName, int maxEventPerHour, int maxItems, string vendorKey = "", string prefix = "")
		{
			string text = string.Empty;
			text = Assembly.GetCallingAssembly().FullName;
			return Analytics.RegisterEvent(eventName, maxEventPerHour, maxItems, vendorKey, 1, prefix, text);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00005220 File Offset: 0x00003420
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static AnalyticsResult RegisterEvent(string eventName, int maxEventPerHour, int maxItems, string vendorKey, int ver, string prefix = "")
		{
			string text = string.Empty;
			text = Assembly.GetCallingAssembly().FullName;
			return Analytics.RegisterEvent(eventName, maxEventPerHour, maxItems, vendorKey, ver, prefix, text);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005254 File Offset: 0x00003454
		private static AnalyticsResult RegisterEvent(string eventName, int maxEventPerHour, int maxItems, string vendorKey, int ver, string prefix, string assemblyInfo)
		{
			bool flag = string.IsNullOrEmpty(eventName);
			if (flag)
			{
				throw new ArgumentException("Cannot set event name to an empty or null string");
			}
			bool flag2 = !Analytics.IsInitialized();
			AnalyticsResult analyticsResult;
			if (flag2)
			{
				analyticsResult = AnalyticsResult.NotInitialized;
			}
			else
			{
				analyticsResult = Analytics.RegisterEventWithLimit(eventName, maxEventPerHour, maxItems, vendorKey, ver, prefix, assemblyInfo, true);
			}
			return analyticsResult;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000529C File Offset: 0x0000349C
		public static AnalyticsResult SendEvent(string eventName, object parameters, int ver = 1, string prefix = "")
		{
			bool flag = string.IsNullOrEmpty(eventName);
			if (flag)
			{
				throw new ArgumentException("Cannot set event name to an empty or null string");
			}
			bool flag2 = parameters == null;
			if (flag2)
			{
				throw new ArgumentException("Cannot set parameters to null");
			}
			bool flag3 = !Analytics.IsInitialized();
			AnalyticsResult analyticsResult;
			if (flag3)
			{
				analyticsResult = AnalyticsResult.NotInitialized;
			}
			else
			{
				analyticsResult = Analytics.SendEventWithLimit(eventName, parameters, ver, prefix);
			}
			return analyticsResult;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000052F0 File Offset: 0x000034F0
		public static AnalyticsResult SetEventEndPoint(string eventName, string endPoint, int ver = 1, string prefix = "")
		{
			bool flag = string.IsNullOrEmpty(eventName);
			if (flag)
			{
				throw new ArgumentException("Cannot set event name to an empty or null string");
			}
			bool flag2 = endPoint == null;
			if (flag2)
			{
				throw new ArgumentException("Cannot set parameters to null");
			}
			bool flag3 = !Analytics.IsInitialized();
			AnalyticsResult analyticsResult;
			if (flag3)
			{
				analyticsResult = AnalyticsResult.NotInitialized;
			}
			else
			{
				analyticsResult = Analytics.SetEventWithLimitEndPoint(eventName, endPoint, ver, prefix);
			}
			return analyticsResult;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00005344 File Offset: 0x00003544
		public static AnalyticsResult SetEventPriority(string eventName, AnalyticsEventPriority eventPriority, int ver = 1, string prefix = "")
		{
			bool flag = string.IsNullOrEmpty(eventName);
			if (flag)
			{
				throw new ArgumentException("Cannot set event name to an empty or null string");
			}
			bool flag2 = !Analytics.IsInitialized();
			AnalyticsResult analyticsResult;
			if (flag2)
			{
				analyticsResult = AnalyticsResult.NotInitialized;
			}
			else
			{
				analyticsResult = Analytics.SetEventWithLimitPriority(eventName, eventPriority, ver, prefix);
			}
			return analyticsResult;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00005384 File Offset: 0x00003584
		public static AnalyticsResult EnableEvent(string eventName, bool enabled, int ver = 1, string prefix = "")
		{
			bool flag = string.IsNullOrEmpty(eventName);
			if (flag)
			{
				throw new ArgumentException("Cannot set event name to an empty or null string");
			}
			bool flag2 = !Analytics.IsInitialized();
			AnalyticsResult analyticsResult;
			if (flag2)
			{
				analyticsResult = AnalyticsResult.NotInitialized;
			}
			else
			{
				analyticsResult = Analytics.EnableEventWithLimit(eventName, enabled, ver, prefix);
			}
			return analyticsResult;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000053C4 File Offset: 0x000035C4
		public static AnalyticsResult IsEventEnabled(string eventName, int ver = 1, string prefix = "")
		{
			bool flag = string.IsNullOrEmpty(eventName);
			if (flag)
			{
				throw new ArgumentException("Cannot set event name to an empty or null string");
			}
			bool flag2 = !Analytics.IsInitialized();
			AnalyticsResult analyticsResult;
			if (flag2)
			{
				analyticsResult = AnalyticsResult.NotInitialized;
			}
			else
			{
				analyticsResult = Analytics.IsEventWithLimitEnabled(eventName, ver, prefix);
			}
			return analyticsResult;
		}

		// Token: 0x0600010E RID: 270
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_eventUrlInternal_Injected(out ManagedSpanWrapper ret);

		// Token: 0x0600010F RID: 271
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_configUrlInternal_Injected(out ManagedSpanWrapper ret);

		// Token: 0x06000110 RID: 272
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_dashboardUrlInternal_Injected(out ManagedSpanWrapper ret);

		// Token: 0x06000111 RID: 273
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnalyticsResult Transaction_Injected(ref ManagedSpanWrapper productId, double amount, ref ManagedSpanWrapper currency, ref ManagedSpanWrapper receiptPurchaseData, ref ManagedSpanWrapper signature, bool usingIAPService);

		// Token: 0x06000112 RID: 274
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnalyticsResult SendCustomEventName_Injected(ref ManagedSpanWrapper customEventName);

		// Token: 0x06000113 RID: 275
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnalyticsResult SendCustomEvent_Injected(IntPtr eventData);

		// Token: 0x06000114 RID: 276
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnalyticsResult IsCustomEventWithLimitEnabled_Injected(ref ManagedSpanWrapper customEventName);

		// Token: 0x06000115 RID: 277
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnalyticsResult EnableCustomEventWithLimit_Injected(ref ManagedSpanWrapper customEventName, bool enable);

		// Token: 0x06000116 RID: 278
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnalyticsResult IsEventWithLimitEnabled_Injected(ref ManagedSpanWrapper eventName, int ver, ref ManagedSpanWrapper prefix);

		// Token: 0x06000117 RID: 279
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnalyticsResult EnableEventWithLimit_Injected(ref ManagedSpanWrapper eventName, bool enable, int ver, ref ManagedSpanWrapper prefix);

		// Token: 0x06000118 RID: 280
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnalyticsResult RegisterEventWithLimit_Injected(ref ManagedSpanWrapper eventName, int maxEventPerHour, int maxItems, ref ManagedSpanWrapper vendorKey, int ver, ref ManagedSpanWrapper prefix, ref ManagedSpanWrapper assemblyInfo, bool notifyServer);

		// Token: 0x06000119 RID: 281
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnalyticsResult RegisterEventsWithLimit_Injected(string[] eventName, int maxEventPerHour, int maxItems, ref ManagedSpanWrapper vendorKey, int ver, ref ManagedSpanWrapper prefix, ref ManagedSpanWrapper assemblyInfo, bool notifyServer);

		// Token: 0x0600011A RID: 282
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnalyticsResult SendEventWithLimit_Injected(ref ManagedSpanWrapper eventName, object parameters, int ver, ref ManagedSpanWrapper prefix);

		// Token: 0x0600011B RID: 283
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnalyticsResult SetEventWithLimitEndPoint_Injected(ref ManagedSpanWrapper eventName, ref ManagedSpanWrapper endPoint, int ver, ref ManagedSpanWrapper prefix);

		// Token: 0x0600011C RID: 284
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnalyticsResult SetEventWithLimitPriority_Injected(ref ManagedSpanWrapper eventName, AnalyticsEventPriority eventPriority, int ver, ref ManagedSpanWrapper prefix);

		// Token: 0x0600011D RID: 285
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnalyticsResult QueueEvent_Injected(ref ManagedSpanWrapper eventName, object parameters, int ver, ref ManagedSpanWrapper prefix);
	}
}
