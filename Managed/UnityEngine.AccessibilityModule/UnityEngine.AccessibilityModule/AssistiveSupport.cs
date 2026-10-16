using System;
using System.Diagnostics;
using UnityEngine.Bindings;

namespace UnityEngine.Accessibility
{
	// Token: 0x0200000E RID: 14
	public static class AssistiveSupport
	{
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000097 RID: 151 RVA: 0x00002E64 File Offset: 0x00001064
		// (remove) Token: 0x06000098 RID: 152 RVA: 0x00002E98 File Offset: 0x00001098
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<AccessibilityNode> nodeFocusChanged;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000099 RID: 153 RVA: 0x00002ECC File Offset: 0x000010CC
		// (remove) Token: 0x0600009A RID: 154 RVA: 0x00002F00 File Offset: 0x00001100
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<bool> screenReaderStatusChanged;

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00002F33 File Offset: 0x00001133
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00002F3A File Offset: 0x0000113A
		public static bool isScreenReaderEnabled { get; private set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00002F42 File Offset: 0x00001142
		public static IAccessibilityNotificationDispatcher notificationDispatcher { get; } = new AssistiveSupport.NotificationDispatcher();

		// Token: 0x0600009E RID: 158 RVA: 0x00002F49 File Offset: 0x00001149
		internal static void Initialize()
		{
			AssistiveSupport.isScreenReaderEnabled = AccessibilityManager.IsScreenReaderEnabled();
			AccessibilityManager.screenReaderStatusChanged += AssistiveSupport.ScreenReaderStatusChanged;
			AccessibilityManager.nodeFocusChanged += AssistiveSupport.NodeFocusChanged;
			AssistiveSupport.s_ServiceManager = new ServiceManager();
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00002F88 File Offset: 0x00001188
		internal static T GetService<T>() where T : IService
		{
			bool flag = AssistiveSupport.s_ServiceManager == null;
			T t;
			if (flag)
			{
				t = default(T);
			}
			else
			{
				t = AssistiveSupport.s_ServiceManager.GetService<T>();
			}
			return t;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00002FC0 File Offset: 0x000011C0
		internal static bool IsServiceRunning<T>() where T : IService
		{
			IService service = AssistiveSupport.GetService<T>();
			return service != null;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00002FE1 File Offset: 0x000011E1
		internal static void SetApplicationAccessibilityLanguage(SystemLanguage language)
		{
			AccessibilityManager.SetApplicationAccessibilityLanguage(language);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00002FEC File Offset: 0x000011EC
		private static void ScreenReaderStatusChanged(bool screenReaderEnabled)
		{
			bool flag = AssistiveSupport.isScreenReaderEnabled == screenReaderEnabled;
			if (!flag)
			{
				AssistiveSupport.isScreenReaderEnabled = screenReaderEnabled;
				Action<bool> action = AssistiveSupport.screenReaderStatusChanged;
				if (action != null)
				{
					action(AssistiveSupport.isScreenReaderEnabled);
				}
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003026 File Offset: 0x00001226
		private static void NodeFocusChanged(AccessibilityNode currentNode)
		{
			Action<AccessibilityNode> action = AssistiveSupport.nodeFocusChanged;
			if (action != null)
			{
				action(currentNode);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x000030A0 File Offset: 0x000012A0
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x0000303C File Offset: 0x0000123C
		public static AccessibilityHierarchy activeHierarchy
		{
			get
			{
				AccessibilityHierarchyService service = AssistiveSupport.GetService<AccessibilityHierarchyService>();
				return (service != null) ? service.hierarchy : null;
			}
			set
			{
				AssistiveSupport.CheckPlatformSupported();
				using (AccessibilityManager.GetExclusiveLock())
				{
					AccessibilityHierarchyService service = AssistiveSupport.GetService<AccessibilityHierarchyService>();
					bool flag = service != null;
					if (flag)
					{
						service.hierarchy = value;
						Action<AccessibilityHierarchy> action = AssistiveSupport.s_ActiveHierarchyChanged;
						if (action != null)
						{
							action(value);
						}
					}
				}
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060000A6 RID: 166 RVA: 0x000030B4 File Offset: 0x000012B4
		// (remove) Token: 0x060000A7 RID: 167 RVA: 0x000030E8 File Offset: 0x000012E8
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static event Action<AccessibilityHierarchy> s_ActiveHierarchyChanged;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060000A8 RID: 168 RVA: 0x0000311B File Offset: 0x0000131B
		// (remove) Token: 0x060000A9 RID: 169 RVA: 0x00003125 File Offset: 0x00001325
		internal static event Action<AccessibilityHierarchy> activeHierarchyChanged
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.AccessibilityModule" })]
			add
			{
				AssistiveSupport.s_ActiveHierarchyChanged += value;
			}
			[VisibleToOtherModules(new string[] { "UnityEditor.AccessibilityModule" })]
			remove
			{
				AssistiveSupport.s_ActiveHierarchyChanged -= value;
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003130 File Offset: 0x00001330
		internal static void OnHierarchyNodeFramesRefreshed(AccessibilityHierarchy hierarchy)
		{
			bool flag = AssistiveSupport.activeHierarchy == hierarchy;
			if (flag)
			{
				AssistiveSupport.notificationDispatcher.SendLayoutChanged(null);
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003158 File Offset: 0x00001358
		private static void CheckPlatformSupported()
		{
			bool flag = !Application.isEditor && !AccessibilityManager.isSupportedPlatform;
			if (flag)
			{
				throw new PlatformNotSupportedException(string.Format("This API is not supported for platform {0}", Application.platform));
			}
		}

		// Token: 0x0400004A RID: 74
		private static ServiceManager s_ServiceManager;

		// Token: 0x0200000F RID: 15
		internal class NotificationDispatcher : IAccessibilityNotificationDispatcher
		{
			// Token: 0x060000AD RID: 173 RVA: 0x000031A3 File Offset: 0x000013A3
			private static void Send(in AccessibilityNotificationContext context)
			{
				AccessibilityManager.SendAccessibilityNotification(in context);
			}

			// Token: 0x060000AE RID: 174 RVA: 0x000031B0 File Offset: 0x000013B0
			public void SendAnnouncement(string announcement)
			{
				AccessibilityNotificationContext accessibilityNotificationContext = new AccessibilityNotificationContext
				{
					notification = AccessibilityNotification.Announcement,
					announcement = announcement
				};
				AssistiveSupport.NotificationDispatcher.Send(in accessibilityNotificationContext);
			}

			// Token: 0x060000AF RID: 175 RVA: 0x000031E4 File Offset: 0x000013E4
			public void SendPageScrolledAnnouncement(string announcement)
			{
				AccessibilityNotificationContext accessibilityNotificationContext = new AccessibilityNotificationContext
				{
					notification = AccessibilityNotification.PageScrolled,
					announcement = announcement
				};
				AssistiveSupport.NotificationDispatcher.Send(in accessibilityNotificationContext);
			}

			// Token: 0x060000B0 RID: 176 RVA: 0x00003218 File Offset: 0x00001418
			public void SendScreenChanged(AccessibilityNode nodeToFocus = null)
			{
				AccessibilityNotificationContext accessibilityNotificationContext = new AccessibilityNotificationContext
				{
					notification = AccessibilityNotification.ScreenChanged,
					nextNodeId = ((nodeToFocus == null) ? (-1) : nodeToFocus.id)
				};
				AssistiveSupport.NotificationDispatcher.Send(in accessibilityNotificationContext);
			}

			// Token: 0x060000B1 RID: 177 RVA: 0x00003258 File Offset: 0x00001458
			public void SendLayoutChanged(AccessibilityNode nodeToFocus = null)
			{
				AccessibilityNotificationContext accessibilityNotificationContext = new AccessibilityNotificationContext
				{
					notification = AccessibilityNotification.LayoutChanged,
					nextNodeId = ((nodeToFocus == null) ? (-1) : nodeToFocus.id)
				};
				AssistiveSupport.NotificationDispatcher.Send(in accessibilityNotificationContext);
			}
		}
	}
}
