using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000014 RID: 20
	[NativeType("Modules/Animation/AnimationClip.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationClip.bindings.h")]
	public sealed class AnimationClip : Motion
	{
		// Token: 0x060000CB RID: 203 RVA: 0x000036B2 File Offset: 0x000018B2
		public AnimationClip()
		{
			AnimationClip.Internal_CreateAnimationClip(this);
		}

		// Token: 0x060000CC RID: 204
		[FreeFunction("AnimationClipBindings::Internal_CreateAnimationClip")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateAnimationClip([Writable] AnimationClip self);

		// Token: 0x060000CD RID: 205 RVA: 0x000036C3 File Offset: 0x000018C3
		public void SampleAnimation(GameObject go, float time)
		{
			AnimationClip.SampleAnimation(go, this, time, this.wrapMode);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000036D8 File Offset: 0x000018D8
		[FreeFunction]
		[NativeHeader("Modules/Animation/AnimationUtility.h")]
		internal static void SampleAnimation([NotNull] GameObject go, [NotNull] AnimationClip clip, float inTime, WrapMode wrapMode)
		{
			if (go == null)
			{
				ThrowHelper.ThrowArgumentNullException(go, "go");
			}
			if (clip == null)
			{
				ThrowHelper.ThrowArgumentNullException(clip, "clip");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(go);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(go, "go");
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(clip);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(clip, "clip");
			}
			AnimationClip.SampleAnimation_Injected(intPtr, intPtr2, inTime, wrapMode);
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00003734 File Offset: 0x00001934
		[NativeProperty("Length", false, TargetType.Function)]
		public float length
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AnimationClip.get_length_Injected(intPtr);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00003758 File Offset: 0x00001958
		[NativeProperty("StartTime", false, TargetType.Function)]
		internal float startTime
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AnimationClip.get_startTime_Injected(intPtr);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x0000377C File Offset: 0x0000197C
		[NativeProperty("StopTime", false, TargetType.Function)]
		internal float stopTime
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AnimationClip.get_stopTime_Injected(intPtr);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x000037A0 File Offset: 0x000019A0
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x000037C4 File Offset: 0x000019C4
		[NativeProperty("SampleRate", false, TargetType.Function)]
		public float frameRate
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AnimationClip.get_frameRate_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AnimationClip.set_frameRate_Injected(intPtr, value);
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000037E8 File Offset: 0x000019E8
		[FreeFunction("AnimationClipBindings::Internal_SetCurve", HasExplicitThis = true)]
		public unsafe void SetCurve([NotNull] string relativePath, [NotNull] Type type, [NotNull] string propertyName, AnimationCurve curve)
		{
			if (relativePath == null)
			{
				ThrowHelper.ThrowArgumentNullException(relativePath, "relativePath");
			}
			if (type == null)
			{
				ThrowHelper.ThrowArgumentNullException(type, "type");
			}
			if (propertyName == null)
			{
				ThrowHelper.ThrowArgumentNullException(propertyName, "propertyName");
			}
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(relativePath, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = relativePath.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(propertyName, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = propertyName.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				AnimationClip.SetCurve_Injected(intPtr, ref managedSpanWrapper, type, ref managedSpanWrapper2, (curve == null) ? ((IntPtr)0) : AnimationCurve.BindingsMarshaller.ConvertToNative(curve));
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000038C0 File Offset: 0x00001AC0
		public void EnsureQuaternionContinuity()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AnimationClip.EnsureQuaternionContinuity_Injected(intPtr);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000038E4 File Offset: 0x00001AE4
		public void ClearCurves()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AnimationClip.ClearCurves_Injected(intPtr);
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00003908 File Offset: 0x00001B08
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x0000392C File Offset: 0x00001B2C
		[NativeProperty("WrapMode", false, TargetType.Function)]
		public WrapMode wrapMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AnimationClip.get_wrapMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AnimationClip.set_wrapMode_Injected(intPtr, value);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00003950 File Offset: 0x00001B50
		// (set) Token: 0x060000DA RID: 218 RVA: 0x00003978 File Offset: 0x00001B78
		[NativeProperty("Bounds", false, TargetType.Function)]
		public Bounds localBounds
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Bounds bounds;
				AnimationClip.get_localBounds_Injected(intPtr, out bounds);
				return bounds;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AnimationClip.set_localBounds_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000DB RID: 219 RVA: 0x0000399C File Offset: 0x00001B9C
		// (set) Token: 0x060000DC RID: 220 RVA: 0x000039C0 File Offset: 0x00001BC0
		public new bool legacy
		{
			[NativeMethod("IsLegacy")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AnimationClip.get_legacy_Injected(intPtr);
			}
			[NativeMethod("SetLegacy")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AnimationClip.set_legacy_Injected(intPtr, value);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000DD RID: 221 RVA: 0x000039E4 File Offset: 0x00001BE4
		public bool humanMotion
		{
			[NativeMethod("IsHumanMotion")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AnimationClip.get_humanMotion_Injected(intPtr);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00003A08 File Offset: 0x00001C08
		public bool empty
		{
			[NativeMethod("IsEmpty")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AnimationClip.get_empty_Injected(intPtr);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00003A2C File Offset: 0x00001C2C
		public bool hasGenericRootTransform
		{
			[NativeMethod("HasGenericRootTransform")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AnimationClip.get_hasGenericRootTransform_Injected(intPtr);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00003A50 File Offset: 0x00001C50
		public bool hasMotionFloatCurves
		{
			[NativeMethod("HasMotionFloatCurves")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AnimationClip.get_hasMotionFloatCurves_Injected(intPtr);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00003A74 File Offset: 0x00001C74
		public bool hasMotionCurves
		{
			[NativeMethod("HasMotionCurves")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AnimationClip.get_hasMotionCurves_Injected(intPtr);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00003A98 File Offset: 0x00001C98
		public bool hasRootCurves
		{
			[NativeMethod("HasRootCurves")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AnimationClip.get_hasRootCurves_Injected(intPtr);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00003ABC File Offset: 0x00001CBC
		internal bool hasRootMotion
		{
			[FreeFunction(Name = "AnimationClipBindings::Internal_GetHasRootMotion", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AnimationClip.get_hasRootMotion_Injected(intPtr);
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00003AE0 File Offset: 0x00001CE0
		public void AddEvent(AnimationEvent evt)
		{
			bool flag = evt == null;
			if (flag)
			{
				throw new ArgumentNullException("evt");
			}
			AnimationEventBlittable animationEventBlittable = AnimationEventBlittable.FromAnimationEvent(evt);
			this.AddEventInternal(animationEventBlittable);
			animationEventBlittable.Dispose();
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00003B20 File Offset: 0x00001D20
		[FreeFunction(Name = "AnimationClipBindings::AddEventInternal", HasExplicitThis = true)]
		private void AddEventInternal(object evt)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AnimationClip.AddEventInternal_Injected(intPtr, evt);
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00003B44 File Offset: 0x00001D44
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00003B74 File Offset: 0x00001D74
		public unsafe AnimationEvent[] events
		{
			get
			{
				IntPtr intPtr;
				int num;
				this.GetEventsInternal(out intPtr, out num);
				AnimationEvent[] array = AnimationEventBlittable.PointerToAnimationEvents(intPtr, num);
				AnimationEventBlittable.DisposeEvents(intPtr, num);
				return array;
			}
			set
			{
				using (NativeArray<AnimationEventBlittable> nativeArray = new NativeArray<AnimationEventBlittable>(value.Length, Allocator.Temp, NativeArrayOptions.UninitializedMemory))
				{
					AnimationEventBlittable* ptr = (AnimationEventBlittable*)nativeArray.GetUnsafePtr<AnimationEventBlittable>();
					AnimationEventBlittable.FromAnimationEvents(value, ptr);
					this.SetEventsInternal((void*)ptr, nativeArray.Length);
					for (int i = 0; i < value.Length; i++)
					{
						ptr->Dispose();
						ptr++;
					}
				}
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00003BF4 File Offset: 0x00001DF4
		[FreeFunction(Name = "AnimationClipBindings::SetEventsInternal", HasExplicitThis = true)]
		private unsafe void SetEventsInternal(void* data, int length)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AnimationClip.SetEventsInternal_Injected(intPtr, data, length);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00003C18 File Offset: 0x00001E18
		[FreeFunction(Name = "AnimationClipBindings::GetEventsInternal", HasExplicitThis = true)]
		private void GetEventsInternal(out IntPtr values, out int size)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AnimationClip.GetEventsInternal_Injected(intPtr, out values, out size);
		}

		// Token: 0x060000EA RID: 234
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SampleAnimation_Injected(IntPtr go, IntPtr clip, float inTime, WrapMode wrapMode);

		// Token: 0x060000EB RID: 235
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_length_Injected(IntPtr _unity_self);

		// Token: 0x060000EC RID: 236
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_startTime_Injected(IntPtr _unity_self);

		// Token: 0x060000ED RID: 237
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_stopTime_Injected(IntPtr _unity_self);

		// Token: 0x060000EE RID: 238
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_frameRate_Injected(IntPtr _unity_self);

		// Token: 0x060000EF RID: 239
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_frameRate_Injected(IntPtr _unity_self, float value);

		// Token: 0x060000F0 RID: 240
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetCurve_Injected(IntPtr _unity_self, ref ManagedSpanWrapper relativePath, Type type, ref ManagedSpanWrapper propertyName, IntPtr curve);

		// Token: 0x060000F1 RID: 241
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EnsureQuaternionContinuity_Injected(IntPtr _unity_self);

		// Token: 0x060000F2 RID: 242
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearCurves_Injected(IntPtr _unity_self);

		// Token: 0x060000F3 RID: 243
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern WrapMode get_wrapMode_Injected(IntPtr _unity_self);

		// Token: 0x060000F4 RID: 244
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_wrapMode_Injected(IntPtr _unity_self, WrapMode value);

		// Token: 0x060000F5 RID: 245
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_localBounds_Injected(IntPtr _unity_self, out Bounds ret);

		// Token: 0x060000F6 RID: 246
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_localBounds_Injected(IntPtr _unity_self, [In] ref Bounds value);

		// Token: 0x060000F7 RID: 247
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_legacy_Injected(IntPtr _unity_self);

		// Token: 0x060000F8 RID: 248
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_legacy_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060000F9 RID: 249
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_humanMotion_Injected(IntPtr _unity_self);

		// Token: 0x060000FA RID: 250
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_empty_Injected(IntPtr _unity_self);

		// Token: 0x060000FB RID: 251
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_hasGenericRootTransform_Injected(IntPtr _unity_self);

		// Token: 0x060000FC RID: 252
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_hasMotionFloatCurves_Injected(IntPtr _unity_self);

		// Token: 0x060000FD RID: 253
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_hasMotionCurves_Injected(IntPtr _unity_self);

		// Token: 0x060000FE RID: 254
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_hasRootCurves_Injected(IntPtr _unity_self);

		// Token: 0x060000FF RID: 255
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_hasRootMotion_Injected(IntPtr _unity_self);

		// Token: 0x06000100 RID: 256
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddEventInternal_Injected(IntPtr _unity_self, object evt);

		// Token: 0x06000101 RID: 257
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void SetEventsInternal_Injected(IntPtr _unity_self, void* data, int length);

		// Token: 0x06000102 RID: 258
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetEventsInternal_Injected(IntPtr _unity_self, out IntPtr values, out int size);
	}
}
