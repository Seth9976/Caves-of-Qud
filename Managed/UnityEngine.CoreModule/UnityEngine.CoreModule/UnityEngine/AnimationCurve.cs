using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000FF RID: 255
	[NativeHeader("Runtime/Math/AnimationCurve.bindings.h")]
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class AnimationCurve : IEquatable<AnimationCurve>
	{
		// Token: 0x06000541 RID: 1345
		[FreeFunction("AnimationCurveBindings::Internal_Destroy", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Destroy(IntPtr ptr);

		// Token: 0x06000542 RID: 1346 RVA: 0x00009AEC File Offset: 0x00007CEC
		[FreeFunction("AnimationCurveBindings::Internal_Create", IsThreadSafe = true)]
		private unsafe static IntPtr Internal_Create(Keyframe[] keys)
		{
			Span<Keyframe> span = new Span<Keyframe>(keys);
			IntPtr intPtr;
			fixed (Keyframe* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				intPtr = AnimationCurve.Internal_Create_Injected(ref managedSpanWrapper);
			}
			return intPtr;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00009B24 File Offset: 0x00007D24
		[FreeFunction("AnimationCurveBindings::Internal_Equals", HasExplicitThis = true, IsThreadSafe = true)]
		private bool Internal_Equals(IntPtr other)
		{
			IntPtr intPtr = AnimationCurve.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return AnimationCurve.Internal_Equals_Injected(intPtr, other);
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00009B48 File Offset: 0x00007D48
		[FreeFunction("AnimationCurveBindings::Internal_CopyFrom", HasExplicitThis = true, IsThreadSafe = true)]
		private void Internal_CopyFrom(IntPtr other)
		{
			IntPtr intPtr = AnimationCurve.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AnimationCurve.Internal_CopyFrom_Injected(intPtr, other);
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00009B6C File Offset: 0x00007D6C
		protected override void Finalize()
		{
			try
			{
				bool requiresNativeCleanup = this.m_RequiresNativeCleanup;
				if (requiresNativeCleanup)
				{
					AnimationCurve.Internal_Destroy(this.m_Ptr);
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00009BAC File Offset: 0x00007DAC
		[ThreadSafe]
		public float Evaluate(float time)
		{
			IntPtr intPtr = AnimationCurve.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return AnimationCurve.Evaluate_Injected(intPtr, time);
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x00009BD0 File Offset: 0x00007DD0
		// (set) Token: 0x06000548 RID: 1352 RVA: 0x00009BE8 File Offset: 0x00007DE8
		public Keyframe[] keys
		{
			get
			{
				return this.GetKeys();
			}
			set
			{
				this.SetKeys(value);
			}
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00009BF4 File Offset: 0x00007DF4
		[FreeFunction("AnimationCurveBindings::AddKeySmoothTangents", HasExplicitThis = true, IsThreadSafe = true)]
		public int AddKey(float time, float value)
		{
			IntPtr intPtr = AnimationCurve.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return AnimationCurve.AddKey_Injected(intPtr, time, value);
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00009C18 File Offset: 0x00007E18
		public int AddKey(Keyframe key)
		{
			return this.AddKey_Internal(key);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00009C34 File Offset: 0x00007E34
		[NativeMethod("AddKey", IsThreadSafe = true)]
		private int AddKey_Internal(Keyframe key)
		{
			IntPtr intPtr = AnimationCurve.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return AnimationCurve.AddKey_Internal_Injected(intPtr, ref key);
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00009C58 File Offset: 0x00007E58
		[NativeThrows]
		[FreeFunction("AnimationCurveBindings::MoveKey", HasExplicitThis = true, IsThreadSafe = true)]
		public int MoveKey(int index, Keyframe key)
		{
			IntPtr intPtr = AnimationCurve.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return AnimationCurve.MoveKey_Injected(intPtr, index, ref key);
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00009C80 File Offset: 0x00007E80
		[FreeFunction("AnimationCurveBindings::ClearKeys", HasExplicitThis = true, IsThreadSafe = true)]
		public void ClearKeys()
		{
			IntPtr intPtr = AnimationCurve.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AnimationCurve.ClearKeys_Injected(intPtr);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00009CA4 File Offset: 0x00007EA4
		[FreeFunction("AnimationCurveBindings::RemoveKey", HasExplicitThis = true, IsThreadSafe = true)]
		[NativeThrows]
		public void RemoveKey(int index)
		{
			IntPtr intPtr = AnimationCurve.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AnimationCurve.RemoveKey_Injected(intPtr, index);
		}

		// Token: 0x170000D5 RID: 213
		public Keyframe this[int index]
		{
			get
			{
				return this.GetKey(index);
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x00009CE4 File Offset: 0x00007EE4
		public int length
		{
			[NativeMethod("GetKeyCount", IsThreadSafe = true)]
			get
			{
				IntPtr intPtr = AnimationCurve.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AnimationCurve.get_length_Injected(intPtr);
			}
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00009D08 File Offset: 0x00007F08
		[FreeFunction("AnimationCurveBindings::SetKeys", HasExplicitThis = true, IsThreadSafe = true)]
		private unsafe void SetKeys(Keyframe[] keys)
		{
			IntPtr intPtr = AnimationCurve.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Keyframe> span = new Span<Keyframe>(keys);
			fixed (Keyframe* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				AnimationCurve.SetKeys_Injected(intPtr, ref managedSpanWrapper);
			}
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00009D50 File Offset: 0x00007F50
		[FreeFunction("AnimationCurveBindings::GetKey", HasExplicitThis = true, IsThreadSafe = true)]
		[NativeThrows]
		private Keyframe GetKey(int index)
		{
			IntPtr intPtr = AnimationCurve.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Keyframe keyframe;
			AnimationCurve.GetKey_Injected(intPtr, index, out keyframe);
			return keyframe;
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00009D78 File Offset: 0x00007F78
		[FreeFunction("AnimationCurveBindings::GetKeys", HasExplicitThis = true, IsThreadSafe = true)]
		private Keyframe[] GetKeys()
		{
			Keyframe[] array2;
			try
			{
				IntPtr intPtr = AnimationCurve.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				AnimationCurve.GetKeys_Injected(intPtr, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				Keyframe[] array;
				blittableArrayWrapper.Unmarshal<Keyframe>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00009DBC File Offset: 0x00007FBC
		[FreeFunction("AnimationCurveBindings::GetHashCode", HasExplicitThis = true, IsThreadSafe = true)]
		public override int GetHashCode()
		{
			IntPtr intPtr = AnimationCurve.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return AnimationCurve.GetHashCode_Injected(intPtr);
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00009DE0 File Offset: 0x00007FE0
		[NativeThrows]
		[FreeFunction("AnimationCurveBindings::SmoothTangents", HasExplicitThis = true, IsThreadSafe = true)]
		public void SmoothTangents(int index, float weight)
		{
			IntPtr intPtr = AnimationCurve.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AnimationCurve.SmoothTangents_Injected(intPtr, index, weight);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00009E04 File Offset: 0x00008004
		public static AnimationCurve Constant(float timeStart, float timeEnd, float value)
		{
			return AnimationCurve.Linear(timeStart, value, timeEnd, value);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00009E20 File Offset: 0x00008020
		public static AnimationCurve Linear(float timeStart, float valueStart, float timeEnd, float valueEnd)
		{
			bool flag = timeStart == timeEnd;
			AnimationCurve animationCurve;
			if (flag)
			{
				Keyframe keyframe = new Keyframe(timeStart, valueStart);
				animationCurve = new AnimationCurve(new Keyframe[] { keyframe });
			}
			else
			{
				float num = (valueEnd - valueStart) / (timeEnd - timeStart);
				Keyframe[] array = new Keyframe[]
				{
					new Keyframe(timeStart, valueStart, 0f, num),
					new Keyframe(timeEnd, valueEnd, num, 0f)
				};
				animationCurve = new AnimationCurve(array);
			}
			return animationCurve;
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00009E9C File Offset: 0x0000809C
		public static AnimationCurve EaseInOut(float timeStart, float valueStart, float timeEnd, float valueEnd)
		{
			bool flag = timeStart == timeEnd;
			AnimationCurve animationCurve;
			if (flag)
			{
				Keyframe keyframe = new Keyframe(timeStart, valueStart);
				animationCurve = new AnimationCurve(new Keyframe[] { keyframe });
			}
			else
			{
				Keyframe[] array = new Keyframe[]
				{
					new Keyframe(timeStart, valueStart, 0f, 0f),
					new Keyframe(timeEnd, valueEnd, 0f, 0f)
				};
				animationCurve = new AnimationCurve(array);
			}
			return animationCurve;
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x00009F14 File Offset: 0x00008114
		// (set) Token: 0x0600055A RID: 1370 RVA: 0x00009F38 File Offset: 0x00008138
		public WrapMode preWrapMode
		{
			[NativeMethod("GetPreInfinity", IsThreadSafe = true)]
			get
			{
				IntPtr intPtr = AnimationCurve.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AnimationCurve.get_preWrapMode_Injected(intPtr);
			}
			[NativeMethod("SetPreInfinity", IsThreadSafe = true)]
			set
			{
				IntPtr intPtr = AnimationCurve.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AnimationCurve.set_preWrapMode_Injected(intPtr, value);
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x00009F5C File Offset: 0x0000815C
		// (set) Token: 0x0600055C RID: 1372 RVA: 0x00009F80 File Offset: 0x00008180
		public WrapMode postWrapMode
		{
			[NativeMethod("GetPostInfinity", IsThreadSafe = true)]
			get
			{
				IntPtr intPtr = AnimationCurve.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AnimationCurve.get_postWrapMode_Injected(intPtr);
			}
			[NativeMethod("SetPostInfinity", IsThreadSafe = true)]
			set
			{
				IntPtr intPtr = AnimationCurve.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AnimationCurve.set_postWrapMode_Injected(intPtr, value);
			}
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00009FA3 File Offset: 0x000081A3
		public AnimationCurve(params Keyframe[] keys)
		{
			this.m_Ptr = AnimationCurve.Internal_Create(keys);
			this.m_RequiresNativeCleanup = true;
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00009FC0 File Offset: 0x000081C0
		[RequiredByNativeCode]
		public AnimationCurve()
		{
			this.m_Ptr = AnimationCurve.Internal_Create(null);
			this.m_RequiresNativeCleanup = true;
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00009FDD File Offset: 0x000081DD
		[VisibleToOtherModules(new string[] { "UnityEngine.ParticleSystemModule" })]
		internal AnimationCurve(IntPtr ptr, bool ownMemory)
		{
			this.m_Ptr = ptr;
			this.m_RequiresNativeCleanup = ownMemory;
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00009FF8 File Offset: 0x000081F8
		public override bool Equals(object o)
		{
			bool flag = o == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this == o;
				flag2 = flag3 || (o.GetType() == base.GetType() && this.Equals((AnimationCurve)o));
			}
			return flag2;
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0000A048 File Offset: 0x00008248
		public bool Equals(AnimationCurve other)
		{
			bool flag = other == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this == other;
				flag2 = flag3 || this.m_Ptr.Equals(other.m_Ptr) || this.Internal_Equals(other.m_Ptr);
			}
			return flag2;
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0000A09B File Offset: 0x0000829B
		public void CopyFrom(AnimationCurve other)
		{
			this.Internal_CopyFrom(other.m_Ptr);
		}

		// Token: 0x06000563 RID: 1379
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_Create_Injected(ref ManagedSpanWrapper keys);

		// Token: 0x06000564 RID: 1380
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_Equals_Injected(IntPtr _unity_self, IntPtr other);

		// Token: 0x06000565 RID: 1381
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CopyFrom_Injected(IntPtr _unity_self, IntPtr other);

		// Token: 0x06000566 RID: 1382
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float Evaluate_Injected(IntPtr _unity_self, float time);

		// Token: 0x06000567 RID: 1383
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int AddKey_Injected(IntPtr _unity_self, float time, float value);

		// Token: 0x06000568 RID: 1384
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int AddKey_Internal_Injected(IntPtr _unity_self, [In] ref Keyframe key);

		// Token: 0x06000569 RID: 1385
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int MoveKey_Injected(IntPtr _unity_self, int index, [In] ref Keyframe key);

		// Token: 0x0600056A RID: 1386
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearKeys_Injected(IntPtr _unity_self);

		// Token: 0x0600056B RID: 1387
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RemoveKey_Injected(IntPtr _unity_self, int index);

		// Token: 0x0600056C RID: 1388
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_length_Injected(IntPtr _unity_self);

		// Token: 0x0600056D RID: 1389
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetKeys_Injected(IntPtr _unity_self, ref ManagedSpanWrapper keys);

		// Token: 0x0600056E RID: 1390
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetKey_Injected(IntPtr _unity_self, int index, out Keyframe ret);

		// Token: 0x0600056F RID: 1391
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetKeys_Injected(IntPtr _unity_self, out BlittableArrayWrapper ret);

		// Token: 0x06000570 RID: 1392
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetHashCode_Injected(IntPtr _unity_self);

		// Token: 0x06000571 RID: 1393
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SmoothTangents_Injected(IntPtr _unity_self, int index, float weight);

		// Token: 0x06000572 RID: 1394
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern WrapMode get_preWrapMode_Injected(IntPtr _unity_self);

		// Token: 0x06000573 RID: 1395
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_preWrapMode_Injected(IntPtr _unity_self, WrapMode value);

		// Token: 0x06000574 RID: 1396
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern WrapMode get_postWrapMode_Injected(IntPtr _unity_self);

		// Token: 0x06000575 RID: 1397
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_postWrapMode_Injected(IntPtr _unity_self, WrapMode value);

		// Token: 0x040002FD RID: 765
		[VisibleToOtherModules(new string[] { "UnityEngine.ParticleSystemModule" })]
		internal IntPtr m_Ptr;

		// Token: 0x040002FE RID: 766
		private bool m_RequiresNativeCleanup;

		// Token: 0x02000100 RID: 256
		internal static class BindingsMarshaller
		{
			// Token: 0x06000576 RID: 1398 RVA: 0x0000A0AB File Offset: 0x000082AB
			public static AnimationCurve ConvertToManaged(IntPtr ptr)
			{
				return new AnimationCurve(ptr, true);
			}

			// Token: 0x06000577 RID: 1399 RVA: 0x0000A0B4 File Offset: 0x000082B4
			public static IntPtr ConvertToNative(AnimationCurve animationCurve)
			{
				return animationCurve.m_Ptr;
			}
		}
	}
}
