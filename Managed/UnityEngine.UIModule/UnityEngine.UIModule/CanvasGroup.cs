using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000003 RID: 3
	[NativeHeader("Modules/UI/CanvasGroup.h")]
	[NativeClass("UI::CanvasGroup")]
	public sealed class CanvasGroup : Behaviour, ICanvasRaycastFilter
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000003 RID: 3 RVA: 0x00002074 File Offset: 0x00000274
		[NativeProperty("Alpha", false, TargetType.Function)]
		public float alpha
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CanvasGroup>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CanvasGroup.get_alpha_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CanvasGroup>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CanvasGroup.set_alpha_Injected(intPtr, value);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002098 File Offset: 0x00000298
		// (set) Token: 0x06000005 RID: 5 RVA: 0x000020BC File Offset: 0x000002BC
		[NativeProperty("Interactable", false, TargetType.Function)]
		public bool interactable
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CanvasGroup>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CanvasGroup.get_interactable_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CanvasGroup>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CanvasGroup.set_interactable_Injected(intPtr, value);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020E0 File Offset: 0x000002E0
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002104 File Offset: 0x00000304
		[NativeProperty("BlocksRaycasts", false, TargetType.Function)]
		public bool blocksRaycasts
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CanvasGroup>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CanvasGroup.get_blocksRaycasts_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CanvasGroup>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CanvasGroup.set_blocksRaycasts_Injected(intPtr, value);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002128 File Offset: 0x00000328
		// (set) Token: 0x06000009 RID: 9 RVA: 0x0000214C File Offset: 0x0000034C
		[NativeProperty("IgnoreParentGroups", false, TargetType.Function)]
		public bool ignoreParentGroups
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CanvasGroup>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CanvasGroup.get_ignoreParentGroups_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CanvasGroup>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CanvasGroup.set_ignoreParentGroups_Injected(intPtr, value);
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002170 File Offset: 0x00000370
		public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
		{
			return this.blocksRaycasts;
		}

		// Token: 0x0600000C RID: 12
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_alpha_Injected(IntPtr _unity_self);

		// Token: 0x0600000D RID: 13
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_alpha_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600000E RID: 14
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_interactable_Injected(IntPtr _unity_self);

		// Token: 0x0600000F RID: 15
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_interactable_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000010 RID: 16
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_blocksRaycasts_Injected(IntPtr _unity_self);

		// Token: 0x06000011 RID: 17
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_blocksRaycasts_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000012 RID: 18
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_ignoreParentGroups_Injected(IntPtr _unity_self);

		// Token: 0x06000013 RID: 19
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_ignoreParentGroups_Injected(IntPtr _unity_self, bool value);
	}
}
