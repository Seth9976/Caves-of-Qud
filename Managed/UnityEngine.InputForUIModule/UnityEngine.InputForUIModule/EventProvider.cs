using System;
using System.Collections.Generic;
using Unity.IntegerTime;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.InputForUI
{
	// Token: 0x0200001F RID: 31
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal static class EventProvider
	{
		// Token: 0x06000082 RID: 130 RVA: 0x0000305C File Offset: 0x0000125C
		public static void Subscribe(EventConsumer handler, int priority = 0, int? playerId = null, params Event.Type[] type)
		{
			EventProvider.Bootstrap();
			EventProvider._registrations.Add(new EventProvider.Registration
			{
				handler = handler,
				priority = priority,
				playerId = playerId,
				_types = new HashSet<Event.Type>(type)
			});
			EventProvider._registrations.Sort((EventProvider.Registration a, EventProvider.Registration b) => a.priority.CompareTo(b.priority));
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000030D4 File Offset: 0x000012D4
		public static void Unsubscribe(EventConsumer handler)
		{
			EventProvider._registrations.RemoveAll((EventProvider.Registration x) => x.handler == handler);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003108 File Offset: 0x00001308
		public static void SetEnabled(bool enable)
		{
			EventProvider.m_IsEnabled = enable;
			if (enable)
			{
				EventProvider.Initialize();
			}
			else
			{
				EventProvider.Shutdown();
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003130 File Offset: 0x00001330
		internal static void Dispatch(in Event ev)
		{
			bool flag = EventProvider._registrations.Count == 0;
			if (!flag)
			{
				EventProvider.s_sanitizer.Inspect(in ev);
				foreach (EventProvider.Registration registration in EventProvider._registrations)
				{
					bool flag2;
					if (registration._types.Count > 0)
					{
						HashSet<Event.Type> types = registration._types;
						Event @event = ev;
						flag2 = !types.Contains(@event.type);
					}
					else
					{
						flag2 = false;
					}
					bool flag3 = flag2;
					if (!flag3)
					{
						bool flag4 = registration.handler(in ev);
						if (flag4)
						{
							break;
						}
					}
				}
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000031EC File Offset: 0x000013EC
		public static void RequestCurrentState(params Event.Type[] types)
		{
			foreach (Event.Type type in (types != null && types.Length > 0) ? types : Event.TypesWithState)
			{
				IEventProviderImpl eventProviderImpl = EventProvider.s_impl;
				bool flag = !((eventProviderImpl != null) ? new bool?(eventProviderImpl.RequestCurrentState(type)) : null).GetValueOrDefault();
				if (flag)
				{
					Debug.LogWarning(string.Format("Can't provide state for type {0}", type));
				}
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003265 File Offset: 0x00001465
		public static uint playerCount
		{
			get
			{
				IEventProviderImpl eventProviderImpl = EventProvider.s_impl;
				return (eventProviderImpl != null) ? eventProviderImpl.playerCount : 0U;
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003278 File Offset: 0x00001478
		private static void Bootstrap()
		{
			bool isEnabled = EventProvider.m_IsEnabled;
			if (isEnabled)
			{
				EventProvider.Initialize();
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003298 File Offset: 0x00001498
		private static void Initialize()
		{
			bool isInitialized = EventProvider.m_IsInitialized;
			if (!isInitialized)
			{
				EventProvider.s_sanitizer.Reset();
				IEventProviderImpl eventProviderImpl = EventProvider.s_impl;
				if (eventProviderImpl != null)
				{
					eventProviderImpl.Initialize();
				}
				bool flag = !EventProvider.s_focusChangedRegistered;
				if (flag)
				{
					Application.focusChanged += EventProvider.OnFocusChanged;
					EventProvider.s_focusChangedRegistered = true;
				}
				EventProvider.m_IsInitialized = true;
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000032FC File Offset: 0x000014FC
		private static void Shutdown()
		{
			bool flag = !EventProvider.m_IsInitialized;
			if (!flag)
			{
				EventProvider.m_IsInitialized = false;
				bool flag2 = EventProvider.s_focusChangedRegistered;
				if (flag2)
				{
					EventProvider.s_focusChangedRegistered = false;
					Application.focusChanged -= EventProvider.OnFocusChanged;
				}
				IEventProviderImpl eventProviderImpl = EventProvider.s_impl;
				if (eventProviderImpl != null)
				{
					eventProviderImpl.Shutdown();
				}
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003352 File Offset: 0x00001552
		private static void OnFocusChanged(bool focus)
		{
			IEventProviderImpl eventProviderImpl = EventProvider.s_impl;
			if (eventProviderImpl != null)
			{
				eventProviderImpl.OnFocusChanged(focus);
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003368 File Offset: 0x00001568
		[RequiredByNativeCode]
		internal static void NotifyUpdate()
		{
			bool flag = !Application.isPlaying || EventProvider._registrations.Count == 0 || !EventProvider.m_IsInitialized;
			if (!flag)
			{
				EventProvider.s_sanitizer.BeforeProviderUpdate();
				IEventProviderImpl eventProviderImpl = EventProvider.s_impl;
				if (eventProviderImpl != null)
				{
					eventProviderImpl.Update();
				}
				EventProvider.s_sanitizer.AfterProviderUpdate();
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000033C4 File Offset: 0x000015C4
		internal static void SetInputSystemProvider(IEventProviderImpl impl)
		{
			bool isInitialized = EventProvider.m_IsInitialized;
			EventProvider.Shutdown();
			EventProvider.s_impl = impl;
			bool flag = isInitialized;
			if (flag)
			{
				EventProvider.Initialize();
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000033F0 File Offset: 0x000015F0
		internal static void SetMockProvider(IEventProviderImpl impl)
		{
			bool flag = EventProvider.s_implMockBackup == null;
			if (flag)
			{
				EventProvider.s_implMockBackup = EventProvider.s_impl;
			}
			EventProvider.s_focusStateBeforeMock = Application.isFocused;
			EventProvider.Shutdown();
			EventProvider.s_impl = impl;
			EventProvider.Initialize();
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003430 File Offset: 0x00001630
		internal static void ClearMockProvider()
		{
			EventProvider.Shutdown();
			EventProvider.s_impl = EventProvider.s_implMockBackup;
			EventProvider.s_implMockBackup = null;
			EventProvider.Initialize();
			bool flag = EventProvider.s_focusStateBeforeMock != Application.isFocused;
			if (flag)
			{
				IEventProviderImpl eventProviderImpl = EventProvider.s_impl;
				if (eventProviderImpl != null)
				{
					eventProviderImpl.OnFocusChanged(Application.isFocused);
				}
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003483 File Offset: 0x00001683
		internal static string _providerClassName
		{
			get
			{
				IEventProviderImpl eventProviderImpl = EventProvider.s_impl;
				return (eventProviderImpl != null) ? eventProviderImpl.GetType().Name : null;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000091 RID: 145 RVA: 0x0000349C File Offset: 0x0000169C
		internal static RationalTime doubleClickTime
		{
			get
			{
				int doubleClickTime = Event.GetDoubleClickTime();
				return new RationalTime((long)doubleClickTime, new RationalTime.TicksPerSecond(1000U, 1U));
			}
		}

		// Token: 0x040000A6 RID: 166
		private static IEventProviderImpl s_impl = new InputManagerProvider();

		// Token: 0x040000A7 RID: 167
		private static EventSanitizer s_sanitizer;

		// Token: 0x040000A8 RID: 168
		private static IEventProviderImpl s_implMockBackup = null;

		// Token: 0x040000A9 RID: 169
		private static bool s_focusStateBeforeMock;

		// Token: 0x040000AA RID: 170
		private static bool s_focusChangedRegistered;

		// Token: 0x040000AB RID: 171
		private static bool m_IsEnabled = true;

		// Token: 0x040000AC RID: 172
		private static bool m_IsInitialized = false;

		// Token: 0x040000AD RID: 173
		private static List<EventProvider.Registration> _registrations = new List<EventProvider.Registration>();

		// Token: 0x02000020 RID: 32
		private struct Registration
		{
			// Token: 0x040000AE RID: 174
			public EventConsumer handler;

			// Token: 0x040000AF RID: 175
			public int priority;

			// Token: 0x040000B0 RID: 176
			public int? playerId;

			// Token: 0x040000B1 RID: 177
			public HashSet<Event.Type> _types;
		}
	}
}
