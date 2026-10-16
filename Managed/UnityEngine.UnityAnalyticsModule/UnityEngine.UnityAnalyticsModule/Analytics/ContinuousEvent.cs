using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine.Analytics
{
	// Token: 0x02000008 RID: 8
	[NativeHeader("Modules/UnityAnalytics/Public/UnityAnalytics.h")]
	[RequiredByNativeCode]
	[NativeHeader("Modules/UnityAnalyticsCommon/Public/UnityAnalyticsCommon.h")]
	[NativeHeader("Modules/UnityAnalytics/ContinuousEvent/Manager.h")]
	[ExcludeFromDocs]
	public class ContinuousEvent
	{
		// Token: 0x06000083 RID: 131 RVA: 0x00003584 File Offset: 0x00001784
		public static AnalyticsResult RegisterCollector<T>(string metricName, Func<T> del) where T : struct, IComparable<T>, IEquatable<T>
		{
			bool flag = string.IsNullOrEmpty(metricName);
			if (flag)
			{
				throw new ArgumentException("Cannot set metric name to an empty or null string");
			}
			bool flag2 = !ContinuousEvent.IsInitialized();
			AnalyticsResult analyticsResult;
			if (flag2)
			{
				analyticsResult = AnalyticsResult.NotInitialized;
			}
			else
			{
				analyticsResult = ContinuousEvent.InternalRegisterCollector(typeof(T).ToString(), metricName, del);
			}
			return analyticsResult;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000035D4 File Offset: 0x000017D4
		public static AnalyticsResult SetEventHistogramThresholds<T>(string eventName, int count, T[] data, int ver = 1, string prefix = "") where T : struct, IComparable<T>, IEquatable<T>
		{
			bool flag = string.IsNullOrEmpty(eventName);
			if (flag)
			{
				throw new ArgumentException("Cannot set event name to an empty or null string");
			}
			bool flag2 = !ContinuousEvent.IsInitialized();
			AnalyticsResult analyticsResult;
			if (flag2)
			{
				analyticsResult = AnalyticsResult.NotInitialized;
			}
			else
			{
				analyticsResult = ContinuousEvent.InternalSetEventHistogramThresholds(typeof(T).ToString(), eventName, count, data, ver, prefix);
			}
			return analyticsResult;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003628 File Offset: 0x00001828
		public static AnalyticsResult SetCustomEventHistogramThresholds<T>(string eventName, int count, T[] data) where T : struct, IComparable<T>, IEquatable<T>
		{
			bool flag = string.IsNullOrEmpty(eventName);
			if (flag)
			{
				throw new ArgumentException("Cannot set event name to an empty or null string");
			}
			bool flag2 = !ContinuousEvent.IsInitialized();
			AnalyticsResult analyticsResult;
			if (flag2)
			{
				analyticsResult = AnalyticsResult.NotInitialized;
			}
			else
			{
				analyticsResult = ContinuousEvent.InternalSetCustomEventHistogramThresholds(typeof(T).ToString(), eventName, count, data);
			}
			return analyticsResult;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003678 File Offset: 0x00001878
		public static AnalyticsResult ConfigureCustomEvent(string customEventName, string metricName, float interval, float period, bool enabled = true)
		{
			bool flag = string.IsNullOrEmpty(customEventName);
			if (flag)
			{
				throw new ArgumentException("Cannot set event name to an empty or null string");
			}
			bool flag2 = !ContinuousEvent.IsInitialized();
			AnalyticsResult analyticsResult;
			if (flag2)
			{
				analyticsResult = AnalyticsResult.NotInitialized;
			}
			else
			{
				analyticsResult = ContinuousEvent.InternalConfigureCustomEvent(customEventName, metricName, interval, period, enabled);
			}
			return analyticsResult;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000036BC File Offset: 0x000018BC
		public static AnalyticsResult ConfigureEvent(string eventName, string metricName, float interval, float period, bool enabled = true, int ver = 1, string prefix = "")
		{
			bool flag = string.IsNullOrEmpty(eventName);
			if (flag)
			{
				throw new ArgumentException("Cannot set event name to an empty or null string");
			}
			bool flag2 = !ContinuousEvent.IsInitialized();
			AnalyticsResult analyticsResult;
			if (flag2)
			{
				analyticsResult = AnalyticsResult.NotInitialized;
			}
			else
			{
				analyticsResult = ContinuousEvent.InternalConfigureEvent(eventName, metricName, interval, period, enabled, ver, prefix);
			}
			return analyticsResult;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003704 File Offset: 0x00001904
		[StaticAccessor("::GetUnityAnalytics().GetContinuousEventManager()", StaticAccessorType.Dot)]
		private unsafe static AnalyticsResult InternalRegisterCollector(string type, string metricName, object collector)
		{
			AnalyticsResult analyticsResult;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(type, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = type.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(metricName, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = metricName.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				analyticsResult = ContinuousEvent.InternalRegisterCollector_Injected(ref managedSpanWrapper, ref managedSpanWrapper2, collector);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
			return analyticsResult;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003790 File Offset: 0x00001990
		[StaticAccessor("::GetUnityAnalytics().GetContinuousEventManager()", StaticAccessorType.Dot)]
		private unsafe static AnalyticsResult InternalSetEventHistogramThresholds(string type, string eventName, int count, object data, int ver, string prefix)
		{
			AnalyticsResult analyticsResult;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(type, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = type.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(eventName, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = eventName.AsSpan();
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
				analyticsResult = ContinuousEvent.InternalSetEventHistogramThresholds_Injected(ref managedSpanWrapper, ref managedSpanWrapper2, count, data, ver, ref managedSpanWrapper3);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
				char* ptr3 = null;
			}
			return analyticsResult;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003854 File Offset: 0x00001A54
		[StaticAccessor("::GetUnityAnalytics().GetContinuousEventManager()", StaticAccessorType.Dot)]
		private unsafe static AnalyticsResult InternalSetCustomEventHistogramThresholds(string type, string eventName, int count, object data)
		{
			AnalyticsResult analyticsResult;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(type, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = type.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(eventName, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = eventName.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				analyticsResult = ContinuousEvent.InternalSetCustomEventHistogramThresholds_Injected(ref managedSpanWrapper, ref managedSpanWrapper2, count, data);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
			return analyticsResult;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000038E0 File Offset: 0x00001AE0
		[StaticAccessor("::GetUnityAnalytics().GetContinuousEventManager()", StaticAccessorType.Dot)]
		private unsafe static AnalyticsResult InternalConfigureCustomEvent(string customEventName, string metricName, float interval, float period, bool enabled)
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
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(metricName, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = metricName.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				analyticsResult = ContinuousEvent.InternalConfigureCustomEvent_Injected(ref managedSpanWrapper, ref managedSpanWrapper2, interval, period, enabled);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
			return analyticsResult;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003970 File Offset: 0x00001B70
		[StaticAccessor("::GetUnityAnalytics().GetContinuousEventManager()", StaticAccessorType.Dot)]
		private unsafe static AnalyticsResult InternalConfigureEvent(string eventName, string metricName, float interval, float period, bool enabled, int ver, string prefix)
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
				if (!StringMarshaller.TryMarshalEmptyOrNullString(metricName, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = metricName.AsSpan();
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
				analyticsResult = ContinuousEvent.InternalConfigureEvent_Injected(ref managedSpanWrapper, ref managedSpanWrapper2, interval, period, enabled, ver, ref managedSpanWrapper3);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
				char* ptr3 = null;
			}
			return analyticsResult;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003A38 File Offset: 0x00001C38
		internal static bool IsInitialized()
		{
			return Analytics.IsInitialized();
		}

		// Token: 0x0600008F RID: 143
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnalyticsResult InternalRegisterCollector_Injected(ref ManagedSpanWrapper type, ref ManagedSpanWrapper metricName, object collector);

		// Token: 0x06000090 RID: 144
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnalyticsResult InternalSetEventHistogramThresholds_Injected(ref ManagedSpanWrapper type, ref ManagedSpanWrapper eventName, int count, object data, int ver, ref ManagedSpanWrapper prefix);

		// Token: 0x06000091 RID: 145
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnalyticsResult InternalSetCustomEventHistogramThresholds_Injected(ref ManagedSpanWrapper type, ref ManagedSpanWrapper eventName, int count, object data);

		// Token: 0x06000092 RID: 146
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnalyticsResult InternalConfigureCustomEvent_Injected(ref ManagedSpanWrapper customEventName, ref ManagedSpanWrapper metricName, float interval, float period, bool enabled);

		// Token: 0x06000093 RID: 147
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnalyticsResult InternalConfigureEvent_Injected(ref ManagedSpanWrapper eventName, ref ManagedSpanWrapper metricName, float interval, float period, bool enabled, int ver, ref ManagedSpanWrapper prefix);
	}
}
