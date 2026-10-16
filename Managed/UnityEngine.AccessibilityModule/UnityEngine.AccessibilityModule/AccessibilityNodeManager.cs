using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Accessibility
{
	// Token: 0x0200000A RID: 10
	[NativeHeader("Modules/Accessibility/Native/AccessibilityNodeManager.h")]
	internal static class AccessibilityNodeManager
	{
		// Token: 0x0600005F RID: 95
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool CreateNativeNode(int id);

		// Token: 0x06000060 RID: 96 RVA: 0x00002954 File Offset: 0x00000B54
		internal static bool CreateNativeNodeWithData(AccessibilityNodeData nodeData)
		{
			return AccessibilityNodeManager.CreateNativeNodeWithData_Injected(ref nodeData);
		}

		// Token: 0x06000061 RID: 97
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void DestroyNativeNode(int id, int parentId);

		// Token: 0x06000062 RID: 98 RVA: 0x00002968 File Offset: 0x00000B68
		internal static void SetNodeData(int id, AccessibilityNodeData nodeData)
		{
			AccessibilityNodeManager.SetNodeData_Injected(id, ref nodeData);
		}

		// Token: 0x06000063 RID: 99
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetIsActive(int id, bool isActive);

		// Token: 0x06000064 RID: 100 RVA: 0x00002980 File Offset: 0x00000B80
		internal unsafe static void SetLabel(int id, string label)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(label, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = label.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				AccessibilityNodeManager.SetLabel_Injected(id, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000029D8 File Offset: 0x00000BD8
		internal unsafe static void SetValue(int id, string value)
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
				AccessibilityNodeManager.SetValue_Injected(id, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002A30 File Offset: 0x00000C30
		internal unsafe static void SetHint(int id, string hint)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(hint, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = hint.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				AccessibilityNodeManager.SetHint_Injected(id, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000067 RID: 103
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetRole(int id, AccessibilityRole role);

		// Token: 0x06000068 RID: 104
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetAllowsDirectInteraction(int id, bool allows);

		// Token: 0x06000069 RID: 105
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetState(int id, AccessibilityState state);

		// Token: 0x0600006A RID: 106 RVA: 0x00002A88 File Offset: 0x00000C88
		internal static void SetFrame(int id, Rect frame)
		{
			AccessibilityNodeManager.SetFrame_Injected(id, ref frame);
		}

		// Token: 0x0600006B RID: 107
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetParent(int id, int parentId, int index = -1);

		// Token: 0x0600006C RID: 108 RVA: 0x00002AA0 File Offset: 0x00000CA0
		internal unsafe static void SetChildren(int id, int[] childIds)
		{
			Span<int> span = new Span<int>(childIds);
			fixed (int* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				AccessibilityNodeManager.SetChildren_Injected(id, ref managedSpanWrapper);
			}
		}

		// Token: 0x0600006D RID: 109
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool GetIsFocused(int id);

		// Token: 0x0600006E RID: 110
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetActions(int id, AccessibilityAction[] actions);

		// Token: 0x0600006F RID: 111
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetLanguage(int id, SystemLanguage language);

		// Token: 0x06000070 RID: 112 RVA: 0x00002ADC File Offset: 0x00000CDC
		[RequiredByNativeCode]
		internal static void Internal_InvokeFocusChanged(int id, bool isNodeFocused)
		{
			AccessibilityHierarchyService service = AssistiveSupport.GetService<AccessibilityHierarchyService>();
			bool flag = service == null;
			if (!flag)
			{
				AccessibilityNode accessibilityNode;
				bool flag2 = service.TryGetNode(id, out accessibilityNode);
				if (flag2)
				{
					accessibilityNode.NotifyFocusChanged(isNodeFocused);
				}
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002B14 File Offset: 0x00000D14
		[RequiredByNativeCode]
		internal static bool Internal_InvokeSelected(int id)
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
				flag2 = flag3 && accessibilityNode.InvokeSelected();
			}
			return flag2;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002B54 File Offset: 0x00000D54
		[RequiredByNativeCode]
		internal static void Internal_InvokeIncremented(int id)
		{
			AccessibilityHierarchyService service = AssistiveSupport.GetService<AccessibilityHierarchyService>();
			bool flag = service == null;
			if (!flag)
			{
				AccessibilityNode accessibilityNode;
				bool flag2 = service.TryGetNode(id, out accessibilityNode);
				if (flag2)
				{
					accessibilityNode.InvokeIncremented();
				}
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002B88 File Offset: 0x00000D88
		[RequiredByNativeCode]
		internal static void Internal_InvokeDecremented(int id)
		{
			AccessibilityHierarchyService service = AssistiveSupport.GetService<AccessibilityHierarchyService>();
			bool flag = service == null;
			if (!flag)
			{
				AccessibilityNode accessibilityNode;
				bool flag2 = service.TryGetNode(id, out accessibilityNode);
				if (flag2)
				{
					accessibilityNode.InvokeDecremented();
				}
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002BBC File Offset: 0x00000DBC
		[RequiredByNativeCode]
		internal static bool Internal_InvokeDismissed(int id)
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
				flag2 = flag3 && accessibilityNode.Dismissed();
			}
			return flag2;
		}

		// Token: 0x06000075 RID: 117
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateNativeNodeWithData_Injected([In] ref AccessibilityNodeData nodeData);

		// Token: 0x06000076 RID: 118
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetNodeData_Injected(int id, [In] ref AccessibilityNodeData nodeData);

		// Token: 0x06000077 RID: 119
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLabel_Injected(int id, ref ManagedSpanWrapper label);

		// Token: 0x06000078 RID: 120
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetValue_Injected(int id, ref ManagedSpanWrapper value);

		// Token: 0x06000079 RID: 121
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetHint_Injected(int id, ref ManagedSpanWrapper hint);

		// Token: 0x0600007A RID: 122
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetFrame_Injected(int id, [In] ref Rect frame);

		// Token: 0x0600007B RID: 123
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetChildren_Injected(int id, ref ManagedSpanWrapper childIds);

		// Token: 0x0400002F RID: 47
		internal const int k_InvalidNodeId = -1;
	}
}
