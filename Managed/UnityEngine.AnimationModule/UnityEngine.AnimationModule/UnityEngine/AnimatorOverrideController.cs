using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000028 RID: 40
	[NativeHeader("Modules/Animation/ScriptBindings/Animation.bindings.h")]
	[HelpURL("AnimatorOverrideController")]
	[UsedByNativeCode]
	[NativeHeader("Modules/Animation/AnimatorOverrideController.h")]
	public class AnimatorOverrideController : RuntimeAnimatorController
	{
		// Token: 0x060002CA RID: 714 RVA: 0x00006706 File Offset: 0x00004906
		public AnimatorOverrideController()
		{
			AnimatorOverrideController.Internal_Create(this, null);
			this.OnOverrideControllerDirty = null;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000671F File Offset: 0x0000491F
		public AnimatorOverrideController(RuntimeAnimatorController controller)
		{
			AnimatorOverrideController.Internal_Create(this, controller);
			this.OnOverrideControllerDirty = null;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00006738 File Offset: 0x00004938
		[FreeFunction("AnimationBindings::CreateAnimatorOverrideController")]
		private static void Internal_Create([Writable] AnimatorOverrideController self, RuntimeAnimatorController controller)
		{
			AnimatorOverrideController.Internal_Create_Injected(self, Object.MarshalledUnityObject.Marshal<RuntimeAnimatorController>(controller));
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060002CD RID: 717 RVA: 0x00006754 File Offset: 0x00004954
		// (set) Token: 0x060002CE RID: 718 RVA: 0x0000677C File Offset: 0x0000497C
		public RuntimeAnimatorController runtimeAnimatorController
		{
			[NativeMethod("GetAnimatorController")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimatorOverrideController>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<RuntimeAnimatorController>(AnimatorOverrideController.get_runtimeAnimatorController_Injected(intPtr));
			}
			[NativeMethod("SetAnimatorController")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimatorOverrideController>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AnimatorOverrideController.set_runtimeAnimatorController_Injected(intPtr, Object.MarshalledUnityObject.Marshal<RuntimeAnimatorController>(value));
			}
		}

		// Token: 0x17000086 RID: 134
		public AnimationClip this[string name]
		{
			get
			{
				return this.Internal_GetClipByName(name, true);
			}
			set
			{
				this.Internal_SetClipByName(name, value);
			}
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x000067CC File Offset: 0x000049CC
		[NativeMethod("GetClip")]
		private unsafe AnimationClip Internal_GetClipByName(string name, bool returnEffectiveClip)
		{
			AnimationClip animationClip;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimatorOverrideController>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				IntPtr intPtr2 = AnimatorOverrideController.Internal_GetClipByName_Injected(intPtr, ref managedSpanWrapper, returnEffectiveClip);
			}
			finally
			{
				IntPtr intPtr2;
				animationClip = Unmarshal.UnmarshalUnityObject<AnimationClip>(intPtr2);
				char* ptr = null;
			}
			return animationClip;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000683C File Offset: 0x00004A3C
		[NativeMethod("SetClip")]
		private unsafe void Internal_SetClipByName(string name, AnimationClip clip)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimatorOverrideController>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				AnimatorOverrideController.Internal_SetClipByName_Injected(intPtr, ref managedSpanWrapper, Object.MarshalledUnityObject.Marshal<AnimationClip>(clip));
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x17000087 RID: 135
		public AnimationClip this[AnimationClip clip]
		{
			get
			{
				return this.GetClip(clip, true);
			}
			set
			{
				this.SetClip(clip, value, true);
			}
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x000068D0 File Offset: 0x00004AD0
		private AnimationClip GetClip(AnimationClip originalClip, bool returnEffectiveClip)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimatorOverrideController>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<AnimationClip>(AnimatorOverrideController.GetClip_Injected(intPtr, Object.MarshalledUnityObject.Marshal<AnimationClip>(originalClip), returnEffectiveClip));
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00006900 File Offset: 0x00004B00
		private void SetClip(AnimationClip originalClip, AnimationClip overrideClip, bool notify)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimatorOverrideController>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AnimatorOverrideController.SetClip_Injected(intPtr, Object.MarshalledUnityObject.Marshal<AnimationClip>(originalClip), Object.MarshalledUnityObject.Marshal<AnimationClip>(overrideClip), notify);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00006930 File Offset: 0x00004B30
		private void SendNotification()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimatorOverrideController>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AnimatorOverrideController.SendNotification_Injected(intPtr);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00006954 File Offset: 0x00004B54
		private AnimationClip GetOriginalClip(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimatorOverrideController>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<AnimationClip>(AnimatorOverrideController.GetOriginalClip_Injected(intPtr, index));
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000697C File Offset: 0x00004B7C
		private AnimationClip GetOverrideClip(AnimationClip originalClip)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimatorOverrideController>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<AnimationClip>(AnimatorOverrideController.GetOverrideClip_Injected(intPtr, Object.MarshalledUnityObject.Marshal<AnimationClip>(originalClip)));
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060002DA RID: 730 RVA: 0x000069AC File Offset: 0x00004BAC
		public int overridesCount
		{
			[NativeMethod("GetOriginalClipsCount")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimatorOverrideController>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AnimatorOverrideController.get_overridesCount_Injected(intPtr);
			}
		}

		// Token: 0x060002DB RID: 731 RVA: 0x000069D0 File Offset: 0x00004BD0
		public void GetOverrides(List<KeyValuePair<AnimationClip, AnimationClip>> overrides)
		{
			bool flag = overrides == null;
			if (flag)
			{
				throw new ArgumentNullException("overrides");
			}
			int overridesCount = this.overridesCount;
			bool flag2 = overrides.Capacity < overridesCount;
			if (flag2)
			{
				overrides.Capacity = overridesCount;
			}
			overrides.Clear();
			for (int i = 0; i < overridesCount; i++)
			{
				AnimationClip originalClip = this.GetOriginalClip(i);
				overrides.Add(new KeyValuePair<AnimationClip, AnimationClip>(originalClip, this.GetOverrideClip(originalClip)));
			}
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00006A48 File Offset: 0x00004C48
		public void ApplyOverrides(IList<KeyValuePair<AnimationClip, AnimationClip>> overrides)
		{
			bool flag = overrides == null;
			if (flag)
			{
				throw new ArgumentNullException("overrides");
			}
			for (int i = 0; i < overrides.Count; i++)
			{
				this.SetClip(overrides[i].Key, overrides[i].Value, false);
			}
			this.SendNotification();
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002DD RID: 733 RVA: 0x00006AAC File Offset: 0x00004CAC
		// (set) Token: 0x060002DE RID: 734 RVA: 0x00006B10 File Offset: 0x00004D10
		[Obsolete("AnimatorOverrideController.clips property is deprecated. Use AnimatorOverrideController.GetOverrides and AnimatorOverrideController.ApplyOverrides instead.")]
		public AnimationClipPair[] clips
		{
			get
			{
				int overridesCount = this.overridesCount;
				AnimationClipPair[] array = new AnimationClipPair[overridesCount];
				for (int i = 0; i < overridesCount; i++)
				{
					array[i] = new AnimationClipPair();
					array[i].originalClip = this.GetOriginalClip(i);
					array[i].overrideClip = this.GetOverrideClip(array[i].originalClip);
				}
				return array;
			}
			set
			{
				for (int i = 0; i < value.Length; i++)
				{
					this.SetClip(value[i].originalClip, value[i].overrideClip, false);
				}
				this.SendNotification();
			}
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00006B50 File Offset: 0x00004D50
		[NativeConditional("UNITY_EDITOR")]
		internal void PerformOverrideClipListCleanup()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimatorOverrideController>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AnimatorOverrideController.PerformOverrideClipListCleanup_Injected(intPtr);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00006B74 File Offset: 0x00004D74
		[RequiredByNativeCode]
		[NativeConditional("UNITY_EDITOR")]
		internal static void OnInvalidateOverrideController(AnimatorOverrideController controller)
		{
			bool flag = controller.OnOverrideControllerDirty != null;
			if (flag)
			{
				controller.OnOverrideControllerDirty();
			}
		}

		// Token: 0x060002E1 RID: 737
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create_Injected([Writable] AnimatorOverrideController self, IntPtr controller);

		// Token: 0x060002E2 RID: 738
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_runtimeAnimatorController_Injected(IntPtr _unity_self);

		// Token: 0x060002E3 RID: 739
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_runtimeAnimatorController_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x060002E4 RID: 740
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_GetClipByName_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name, bool returnEffectiveClip);

		// Token: 0x060002E5 RID: 741
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetClipByName_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name, IntPtr clip);

		// Token: 0x060002E6 RID: 742
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetClip_Injected(IntPtr _unity_self, IntPtr originalClip, bool returnEffectiveClip);

		// Token: 0x060002E7 RID: 743
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetClip_Injected(IntPtr _unity_self, IntPtr originalClip, IntPtr overrideClip, bool notify);

		// Token: 0x060002E8 RID: 744
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SendNotification_Injected(IntPtr _unity_self);

		// Token: 0x060002E9 RID: 745
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetOriginalClip_Injected(IntPtr _unity_self, int index);

		// Token: 0x060002EA RID: 746
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetOverrideClip_Injected(IntPtr _unity_self, IntPtr originalClip);

		// Token: 0x060002EB RID: 747
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_overridesCount_Injected(IntPtr _unity_self);

		// Token: 0x060002EC RID: 748
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PerformOverrideClipListCleanup_Injected(IntPtr _unity_self);

		// Token: 0x04000080 RID: 128
		internal AnimatorOverrideController.OnOverrideControllerDirtyCallback OnOverrideControllerDirty;

		// Token: 0x02000029 RID: 41
		// (Invoke) Token: 0x060002EE RID: 750
		internal delegate void OnOverrideControllerDirtyCallback();
	}
}
