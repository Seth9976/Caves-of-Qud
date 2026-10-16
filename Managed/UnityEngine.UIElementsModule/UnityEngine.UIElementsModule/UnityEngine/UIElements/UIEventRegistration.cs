using System;
using System.Collections.Generic;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x0200050C RID: 1292
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal static class UIEventRegistration
	{
		// Token: 0x06002820 RID: 10272 RVA: 0x00092924 File Offset: 0x00090B24
		static UIEventRegistration()
		{
			GUIUtility.takeCapture = (Action)Delegate.Combine(GUIUtility.takeCapture, new Action(delegate
			{
				UIEventRegistration.TakeCapture();
			}));
			GUIUtility.releaseCapture = (Action)Delegate.Combine(GUIUtility.releaseCapture, new Action(delegate
			{
				UIEventRegistration.ReleaseCapture();
			}));
			GUIUtility.processEvent = (Func<int, IntPtr, bool>)Delegate.Combine(GUIUtility.processEvent, new Func<int, IntPtr, bool>((int i, IntPtr ptr) => UIEventRegistration.ProcessEvent(i, ptr)));
			GUIUtility.cleanupRoots = (Action)Delegate.Combine(GUIUtility.cleanupRoots, new Action(delegate
			{
				UIEventRegistration.CleanupRoots();
			}));
			GUIUtility.endContainerGUIFromException = (Func<Exception, bool>)Delegate.Combine(GUIUtility.endContainerGUIFromException, new Func<Exception, bool>((Exception exception) => UIEventRegistration.EndContainerGUIFromException(exception)));
			GUIUtility.guiChanged = (Action)Delegate.Combine(GUIUtility.guiChanged, new Action(delegate
			{
				UIEventRegistration.MakeCurrentIMGUIContainerDirty();
			}));
		}

		// Token: 0x06002821 RID: 10273 RVA: 0x00092A14 File Offset: 0x00090C14
		internal static void RegisterUIElementSystem(IUIElementsUtility utility)
		{
			UIEventRegistration.s_Utilities.Insert(0, utility);
		}

		// Token: 0x06002822 RID: 10274 RVA: 0x00092A24 File Offset: 0x00090C24
		private static void TakeCapture()
		{
			foreach (IUIElementsUtility iuielementsUtility in UIEventRegistration.s_Utilities)
			{
				bool flag = iuielementsUtility.TakeCapture();
				if (flag)
				{
					break;
				}
			}
		}

		// Token: 0x06002823 RID: 10275 RVA: 0x00092A80 File Offset: 0x00090C80
		private static void ReleaseCapture()
		{
			foreach (IUIElementsUtility iuielementsUtility in UIEventRegistration.s_Utilities)
			{
				bool flag = iuielementsUtility.ReleaseCapture();
				if (flag)
				{
					break;
				}
			}
		}

		// Token: 0x06002824 RID: 10276 RVA: 0x00092ADC File Offset: 0x00090CDC
		private static bool EndContainerGUIFromException(Exception exception)
		{
			foreach (IUIElementsUtility iuielementsUtility in UIEventRegistration.s_Utilities)
			{
				bool flag = iuielementsUtility.EndContainerGUIFromException(exception);
				if (flag)
				{
					return true;
				}
			}
			return GUIUtility.ShouldRethrowException(exception);
		}

		// Token: 0x06002825 RID: 10277 RVA: 0x00092B48 File Offset: 0x00090D48
		private static bool ProcessEvent(int instanceID, IntPtr nativeEventPtr)
		{
			bool flag = false;
			foreach (IUIElementsUtility iuielementsUtility in UIEventRegistration.s_Utilities)
			{
				bool flag2 = iuielementsUtility.ProcessEvent(instanceID, nativeEventPtr, ref flag);
				if (flag2)
				{
					return flag;
				}
			}
			return false;
		}

		// Token: 0x06002826 RID: 10278 RVA: 0x00092BB8 File Offset: 0x00090DB8
		private static void CleanupRoots()
		{
			foreach (IUIElementsUtility iuielementsUtility in UIEventRegistration.s_Utilities)
			{
				bool flag = iuielementsUtility.CleanupRoots();
				if (flag)
				{
					break;
				}
			}
		}

		// Token: 0x06002827 RID: 10279 RVA: 0x00092C14 File Offset: 0x00090E14
		internal static void MakeCurrentIMGUIContainerDirty()
		{
			foreach (IUIElementsUtility iuielementsUtility in UIEventRegistration.s_Utilities)
			{
				bool flag = iuielementsUtility.MakeCurrentIMGUIContainerDirty();
				if (flag)
				{
					break;
				}
			}
		}

		// Token: 0x06002828 RID: 10280 RVA: 0x00092C70 File Offset: 0x00090E70
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal static void UpdateSchedulers()
		{
			foreach (IUIElementsUtility iuielementsUtility in UIEventRegistration.s_Utilities)
			{
				iuielementsUtility.UpdateSchedulers();
			}
		}

		// Token: 0x06002829 RID: 10281 RVA: 0x00092CC8 File Offset: 0x00090EC8
		internal static void RequestRepaintForPanels(Action<ScriptableObject> repaintCallback)
		{
			foreach (IUIElementsUtility iuielementsUtility in UIEventRegistration.s_Utilities)
			{
				iuielementsUtility.RequestRepaintForPanels(repaintCallback);
			}
		}

		// Token: 0x0400121E RID: 4638
		private static List<IUIElementsUtility> s_Utilities = new List<IUIElementsUtility>();
	}
}
