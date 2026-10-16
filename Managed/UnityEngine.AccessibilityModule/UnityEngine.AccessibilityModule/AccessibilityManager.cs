using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Pool;
using UnityEngine.Scripting;

namespace UnityEngine.Accessibility
{
	// Token: 0x02000004 RID: 4
	[VisibleToOtherModules(new string[] { "UnityEditor.AccessibilityModule" })]
	[NativeHeader("Modules/Accessibility/Native/AccessibilityManager.h")]
	internal static class AccessibilityManager
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002234 File Offset: 0x00000434
		public static bool isSupportedPlatform
		{
			get
			{
				RuntimePlatform platform = Application.platform;
				return platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000016 RID: 22 RVA: 0x0000225C File Offset: 0x0000045C
		// (remove) Token: 0x06000017 RID: 23 RVA: 0x00002290 File Offset: 0x00000490
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<bool> screenReaderStatusChanged;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000018 RID: 24 RVA: 0x000022C4 File Offset: 0x000004C4
		// (remove) Token: 0x06000019 RID: 25 RVA: 0x000022F8 File Offset: 0x000004F8
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<AccessibilityNode> nodeFocusChanged;

		// Token: 0x0600001A RID: 26
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsScreenReaderEnabled();

		// Token: 0x0600001B RID: 27
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SendAccessibilityNotification(in AccessibilityNotificationContext context);

		// Token: 0x0600001C RID: 28
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern SystemLanguage GetApplicationAccessibilityLanguage();

		// Token: 0x0600001D RID: 29
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetApplicationAccessibilityLanguage(SystemLanguage languageCode);

		// Token: 0x0600001E RID: 30 RVA: 0x0000232B File Offset: 0x0000052B
		[RequiredByNativeCode]
		[VisibleToOtherModules(new string[] { "UnityEditor.AccessibilityModule" })]
		internal static void Internal_Initialize()
		{
			AssistiveSupport.Initialize();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002334 File Offset: 0x00000534
		[RequiredByNativeCode]
		private static void Internal_Update()
		{
			bool flag = AccessibilityManager.asyncNotificationContexts.Count == 0;
			if (!flag)
			{
				Queue<AccessibilityManager.NotificationContext> queue = AccessibilityManager.asyncNotificationContexts;
				AccessibilityManager.NotificationContext[] array;
				lock (queue)
				{
					bool flag3 = AccessibilityManager.asyncNotificationContexts.Count == 0;
					if (flag3)
					{
						return;
					}
					array = AccessibilityManager.asyncNotificationContexts.ToArray();
					AccessibilityManager.asyncNotificationContexts.Clear();
				}
				using (AccessibilityManager.GetExclusiveLock())
				{
					foreach (AccessibilityManager.NotificationContext notificationContext in array)
					{
						switch (notificationContext.notification)
						{
						case AccessibilityNotification.ScreenReaderStatusChanged:
						{
							Action<bool> action = AccessibilityManager.screenReaderStatusChanged;
							if (action != null)
							{
								action(notificationContext.isScreenReaderEnabled);
							}
							break;
						}
						case AccessibilityNotification.ElementFocused:
						{
							notificationContext.currentNode.InvokeFocusChanged(true);
							Action<AccessibilityNode> action2 = AccessibilityManager.nodeFocusChanged;
							if (action2 != null)
							{
								action2(notificationContext.currentNode);
							}
							break;
						}
						case AccessibilityNotification.ElementUnfocused:
							notificationContext.currentNode.InvokeFocusChanged(false);
							break;
						case AccessibilityNotification.FontScaleChanged:
							AccessibilitySettings.InvokeFontScaleChanged(notificationContext.fontScale);
							break;
						case AccessibilityNotification.BoldTextStatusChanged:
							AccessibilitySettings.InvokeBoldTextStatusChanged(notificationContext.isBoldTextEnabled);
							break;
						case AccessibilityNotification.ClosedCaptioningStatusChanged:
							AccessibilitySettings.InvokeClosedCaptionStatusChanged(notificationContext.isClosedCaptioningEnabled);
							break;
						}
					}
				}
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000024C8 File Offset: 0x000006C8
		[RequiredByNativeCode]
		internal static int[] Internal_GetRootNodeIds()
		{
			AccessibilityHierarchyService service = AssistiveSupport.GetService<AccessibilityHierarchyService>();
			List<AccessibilityNode> list = ((service != null) ? service.GetRootNodes() : null);
			bool flag = list == null || list.Count == 0;
			int[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				List<int> list2;
				using (CollectionPool<List<int>, int>.Get(out list2))
				{
					for (int i = 0; i < list.Count; i++)
					{
						list2.Add(list[i].id);
					}
					array = ((list2.Count == 0) ? null : list2.ToArray());
				}
			}
			return array;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002574 File Offset: 0x00000774
		[RequiredByNativeCode]
		internal static bool Internal_GetNode(int id, ref AccessibilityNodeData nodeData)
		{
			AccessibilityHierarchyService service = AssistiveSupport.GetService<AccessibilityHierarchyService>();
			bool flag = service == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				AccessibilityNode accessibilityNode;
				bool flag3 = service.TryGetNode(id, out accessibilityNode);
				if (flag3)
				{
					accessibilityNode.GetNodeData(ref nodeData);
					flag2 = true;
				}
				else
				{
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000025B8 File Offset: 0x000007B8
		[RequiredByNativeCode]
		internal static int Internal_GetNodeIdAt(float x, float y)
		{
			AccessibilityHierarchyService service = AssistiveSupport.GetService<AccessibilityHierarchyService>();
			AccessibilityNode accessibilityNode;
			return (service != null && service.TryGetNodeAt(x, y, out accessibilityNode)) ? accessibilityNode.id : (-1);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000025E8 File Offset: 0x000007E8
		[RequiredByNativeCode]
		internal static void Internal_OnAccessibilityNotificationReceived(ref AccessibilityNotificationContext context)
		{
			bool flag = context.notification == AccessibilityNotification.ElementFocused;
			if (!flag)
			{
				AccessibilityManager.QueueNotification(new AccessibilityManager.NotificationContext(ref context));
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002614 File Offset: 0x00000814
		internal static void QueueNotification(AccessibilityManager.NotificationContext notification)
		{
			Queue<AccessibilityManager.NotificationContext> queue = AccessibilityManager.asyncNotificationContexts;
			lock (queue)
			{
				AccessibilityManager.asyncNotificationContexts.Enqueue(notification);
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002660 File Offset: 0x00000860
		internal static IDisposable GetExclusiveLock()
		{
			return new AccessibilityManager.ExclusiveLock();
		}

		// Token: 0x06000026 RID: 38
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Lock();

		// Token: 0x06000027 RID: 39
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Unlock();

		// Token: 0x04000003 RID: 3
		internal static Queue<AccessibilityManager.NotificationContext> asyncNotificationContexts = new Queue<AccessibilityManager.NotificationContext>();

		// Token: 0x02000005 RID: 5
		public struct NotificationContext
		{
			// Token: 0x17000005 RID: 5
			// (get) Token: 0x06000029 RID: 41 RVA: 0x00002683 File Offset: 0x00000883
			// (set) Token: 0x0600002A RID: 42 RVA: 0x0000268B File Offset: 0x0000088B
			public AccessibilityNotification notification { readonly get; set; }

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x0600002B RID: 43 RVA: 0x00002694 File Offset: 0x00000894
			// (set) Token: 0x0600002C RID: 44 RVA: 0x0000269C File Offset: 0x0000089C
			public bool isScreenReaderEnabled { readonly get; set; }

			// Token: 0x17000007 RID: 7
			// (get) Token: 0x0600002D RID: 45 RVA: 0x000026A5 File Offset: 0x000008A5
			// (set) Token: 0x0600002E RID: 46 RVA: 0x000026AD File Offset: 0x000008AD
			public string announcement { readonly get; set; }

			// Token: 0x17000008 RID: 8
			// (get) Token: 0x0600002F RID: 47 RVA: 0x000026B6 File Offset: 0x000008B6
			// (set) Token: 0x06000030 RID: 48 RVA: 0x000026BE File Offset: 0x000008BE
			public bool wasAnnouncementSuccessful { readonly get; set; }

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x06000031 RID: 49 RVA: 0x000026C7 File Offset: 0x000008C7
			// (set) Token: 0x06000032 RID: 50 RVA: 0x000026CF File Offset: 0x000008CF
			public AccessibilityNode currentNode { readonly get; set; }

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x06000033 RID: 51 RVA: 0x000026D8 File Offset: 0x000008D8
			// (set) Token: 0x06000034 RID: 52 RVA: 0x000026E0 File Offset: 0x000008E0
			public AccessibilityNode nextNode { readonly get; set; }

			// Token: 0x1700000B RID: 11
			// (get) Token: 0x06000035 RID: 53 RVA: 0x000026E9 File Offset: 0x000008E9
			// (set) Token: 0x06000036 RID: 54 RVA: 0x000026F1 File Offset: 0x000008F1
			public float fontScale { readonly get; set; }

			// Token: 0x1700000C RID: 12
			// (get) Token: 0x06000037 RID: 55 RVA: 0x000026FA File Offset: 0x000008FA
			// (set) Token: 0x06000038 RID: 56 RVA: 0x00002702 File Offset: 0x00000902
			public bool isBoldTextEnabled { readonly get; set; }

			// Token: 0x1700000D RID: 13
			// (get) Token: 0x06000039 RID: 57 RVA: 0x0000270B File Offset: 0x0000090B
			// (set) Token: 0x0600003A RID: 58 RVA: 0x00002713 File Offset: 0x00000913
			public bool isClosedCaptioningEnabled { readonly get; set; }

			// Token: 0x1700000E RID: 14
			// (get) Token: 0x0600003B RID: 59 RVA: 0x0000271C File Offset: 0x0000091C
			// (set) Token: 0x0600003C RID: 60 RVA: 0x00002724 File Offset: 0x00000924
			public AccessibilityNotificationContext nativeContext { readonly get; set; }

			// Token: 0x0600003D RID: 61 RVA: 0x00002730 File Offset: 0x00000930
			public NotificationContext(ref AccessibilityNotificationContext nativeNotification)
			{
				this.nativeContext = nativeNotification;
				this.notification = nativeNotification.notification;
				this.isScreenReaderEnabled = nativeNotification.isScreenReaderEnabled;
				this.announcement = nativeNotification.announcement;
				this.wasAnnouncementSuccessful = nativeNotification.wasAnnouncementSuccessful;
				AccessibilityNode accessibilityNode = null;
				AccessibilityHierarchy activeHierarchy = AssistiveSupport.activeHierarchy;
				if (activeHierarchy != null)
				{
					activeHierarchy.TryGetNode(nativeNotification.currentNodeId, out accessibilityNode);
				}
				this.currentNode = accessibilityNode;
				AccessibilityHierarchy activeHierarchy2 = AssistiveSupport.activeHierarchy;
				if (activeHierarchy2 != null)
				{
					activeHierarchy2.TryGetNode(nativeNotification.nextNodeId, out accessibilityNode);
				}
				this.nextNode = accessibilityNode;
				this.fontScale = 1f;
				this.isBoldTextEnabled = false;
				this.isClosedCaptioningEnabled = false;
			}
		}

		// Token: 0x02000006 RID: 6
		private sealed class ExclusiveLock : IDisposable
		{
			// Token: 0x0600003E RID: 62 RVA: 0x000027DF File Offset: 0x000009DF
			public ExclusiveLock()
			{
				AccessibilityManager.Lock();
			}

			// Token: 0x0600003F RID: 63 RVA: 0x000027F0 File Offset: 0x000009F0
			~ExclusiveLock()
			{
				this.InternalDispose();
			}

			// Token: 0x06000040 RID: 64 RVA: 0x00002820 File Offset: 0x00000A20
			private void InternalDispose()
			{
				bool flag = !this.m_Disposed;
				if (flag)
				{
					AccessibilityManager.Unlock();
					this.m_Disposed = true;
				}
			}

			// Token: 0x06000041 RID: 65 RVA: 0x0000284A File Offset: 0x00000A4A
			public void Dispose()
			{
				this.InternalDispose();
				GC.SuppressFinalize(this);
			}

			// Token: 0x04000010 RID: 16
			private bool m_Disposed;
		}
	}
}
