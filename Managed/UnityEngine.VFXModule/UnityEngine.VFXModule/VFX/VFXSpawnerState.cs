using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.VFX
{
	// Token: 0x0200001B RID: 27
	[NativeType(Header = "Modules/VFX/Public/VFXSpawnerState.h")]
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class VFXSpawnerState : IDisposable
	{
		// Token: 0x060000B3 RID: 179 RVA: 0x00002F3C File Offset: 0x0000113C
		public VFXSpawnerState()
			: this(VFXSpawnerState.Internal_Create(), true)
		{
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00002F4C File Offset: 0x0000114C
		internal VFXSpawnerState(IntPtr ptr, bool owner)
		{
			this.m_Ptr = ptr;
			this.m_Owner = owner;
		}

		// Token: 0x060000B5 RID: 181
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr Internal_Create();

		// Token: 0x060000B6 RID: 182 RVA: 0x00002F64 File Offset: 0x00001164
		[RequiredByNativeCode]
		internal static VFXSpawnerState CreateSpawnerStateWrapper()
		{
			VFXSpawnerState vfxspawnerState = new VFXSpawnerState(IntPtr.Zero, false);
			vfxspawnerState.PrepareWrapper();
			return vfxspawnerState;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00002F8C File Offset: 0x0000118C
		private void PrepareWrapper()
		{
			bool owner = this.m_Owner;
			if (owner)
			{
				throw new Exception("VFXSpawnerState : SetWrapValue is reserved to CreateWrapper object");
			}
			bool flag = this.m_WrapEventAttribute != null;
			if (flag)
			{
				throw new Exception("VFXSpawnerState : Unexpected calling twice prepare wrapper");
			}
			this.m_WrapEventAttribute = VFXEventAttribute.CreateEventAttributeWrapper();
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00002FD4 File Offset: 0x000011D4
		[RequiredByNativeCode]
		internal void SetWrapValue(IntPtr ptrToSpawnerState, IntPtr ptrToEventAttribute)
		{
			bool owner = this.m_Owner;
			if (owner)
			{
				throw new Exception("VFXSpawnerState : SetWrapValue is reserved to CreateWrapper object");
			}
			bool flag = this.m_WrapEventAttribute == null;
			if (flag)
			{
				throw new Exception("VFXSpawnerState : Missing PrepareWrapper");
			}
			this.m_Ptr = ptrToSpawnerState;
			this.m_WrapEventAttribute.SetWrapValue(ptrToEventAttribute);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003024 File Offset: 0x00001224
		internal IntPtr GetPtr()
		{
			return this.m_Ptr;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000303C File Offset: 0x0000123C
		private void Release()
		{
			bool flag = this.m_Ptr != IntPtr.Zero && this.m_Owner;
			if (flag)
			{
				VFXSpawnerState.Internal_Destroy(this.m_Ptr);
			}
			this.m_Ptr = IntPtr.Zero;
			this.m_WrapEventAttribute = null;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000308C File Offset: 0x0000128C
		~VFXSpawnerState()
		{
			this.Release();
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000030BC File Offset: 0x000012BC
		public void Dispose()
		{
			this.Release();
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000BD RID: 189
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Destroy(IntPtr ptr);

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060000BE RID: 190 RVA: 0x000030D0 File Offset: 0x000012D0
		// (set) Token: 0x060000BF RID: 191 RVA: 0x000030EB File Offset: 0x000012EB
		public bool playing
		{
			get
			{
				return this.loopState == VFXSpawnerLoopState.Looping;
			}
			set
			{
				this.loopState = (value ? VFXSpawnerLoopState.Looping : VFXSpawnerLoopState.Finished);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x000030FC File Offset: 0x000012FC
		public bool newLoop
		{
			get
			{
				IntPtr intPtr = VFXSpawnerState.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VFXSpawnerState.get_newLoop_Injected(intPtr);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00003120 File Offset: 0x00001320
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x00003144 File Offset: 0x00001344
		public VFXSpawnerLoopState loopState
		{
			get
			{
				IntPtr intPtr = VFXSpawnerState.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VFXSpawnerState.get_loopState_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = VFXSpawnerState.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VFXSpawnerState.set_loopState_Injected(intPtr, value);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00003168 File Offset: 0x00001368
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x0000318C File Offset: 0x0000138C
		public float spawnCount
		{
			get
			{
				IntPtr intPtr = VFXSpawnerState.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VFXSpawnerState.get_spawnCount_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = VFXSpawnerState.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VFXSpawnerState.set_spawnCount_Injected(intPtr, value);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x000031B0 File Offset: 0x000013B0
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x000031D4 File Offset: 0x000013D4
		public float deltaTime
		{
			get
			{
				IntPtr intPtr = VFXSpawnerState.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VFXSpawnerState.get_deltaTime_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = VFXSpawnerState.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VFXSpawnerState.set_deltaTime_Injected(intPtr, value);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x000031F8 File Offset: 0x000013F8
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x0000321C File Offset: 0x0000141C
		public float totalTime
		{
			get
			{
				IntPtr intPtr = VFXSpawnerState.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VFXSpawnerState.get_totalTime_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = VFXSpawnerState.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VFXSpawnerState.set_totalTime_Injected(intPtr, value);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00003240 File Offset: 0x00001440
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00003264 File Offset: 0x00001464
		public float delayBeforeLoop
		{
			get
			{
				IntPtr intPtr = VFXSpawnerState.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VFXSpawnerState.get_delayBeforeLoop_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = VFXSpawnerState.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VFXSpawnerState.set_delayBeforeLoop_Injected(intPtr, value);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00003288 File Offset: 0x00001488
		// (set) Token: 0x060000CC RID: 204 RVA: 0x000032AC File Offset: 0x000014AC
		public float loopDuration
		{
			get
			{
				IntPtr intPtr = VFXSpawnerState.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VFXSpawnerState.get_loopDuration_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = VFXSpawnerState.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VFXSpawnerState.set_loopDuration_Injected(intPtr, value);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000CD RID: 205 RVA: 0x000032D0 File Offset: 0x000014D0
		// (set) Token: 0x060000CE RID: 206 RVA: 0x000032F4 File Offset: 0x000014F4
		public float delayAfterLoop
		{
			get
			{
				IntPtr intPtr = VFXSpawnerState.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VFXSpawnerState.get_delayAfterLoop_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = VFXSpawnerState.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VFXSpawnerState.set_delayAfterLoop_Injected(intPtr, value);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00003318 File Offset: 0x00001518
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x0000333C File Offset: 0x0000153C
		public int loopIndex
		{
			get
			{
				IntPtr intPtr = VFXSpawnerState.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VFXSpawnerState.get_loopIndex_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = VFXSpawnerState.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VFXSpawnerState.set_loopIndex_Injected(intPtr, value);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00003360 File Offset: 0x00001560
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00003384 File Offset: 0x00001584
		public int loopCount
		{
			get
			{
				IntPtr intPtr = VFXSpawnerState.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VFXSpawnerState.get_loopCount_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = VFXSpawnerState.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VFXSpawnerState.set_loopCount_Injected(intPtr, value);
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000033A8 File Offset: 0x000015A8
		internal VFXEventAttribute Internal_GetVFXEventAttribute()
		{
			IntPtr intPtr = VFXSpawnerState.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = VFXSpawnerState.Internal_GetVFXEventAttribute_Injected(intPtr);
			return (intPtr2 == 0) ? null : VFXEventAttribute.BindingsMarshaller.ConvertToManaged(intPtr2);
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x000033D8 File Offset: 0x000015D8
		public VFXEventAttribute vfxEventAttribute
		{
			get
			{
				bool flag = !this.m_Owner && this.m_WrapEventAttribute != null;
				VFXEventAttribute vfxeventAttribute;
				if (flag)
				{
					vfxeventAttribute = this.m_WrapEventAttribute;
				}
				else
				{
					vfxeventAttribute = this.Internal_GetVFXEventAttribute();
				}
				return vfxeventAttribute;
			}
		}

		// Token: 0x060000D5 RID: 213
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_newLoop_Injected(IntPtr _unity_self);

		// Token: 0x060000D6 RID: 214
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern VFXSpawnerLoopState get_loopState_Injected(IntPtr _unity_self);

		// Token: 0x060000D7 RID: 215
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_loopState_Injected(IntPtr _unity_self, VFXSpawnerLoopState value);

		// Token: 0x060000D8 RID: 216
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_spawnCount_Injected(IntPtr _unity_self);

		// Token: 0x060000D9 RID: 217
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_spawnCount_Injected(IntPtr _unity_self, float value);

		// Token: 0x060000DA RID: 218
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_deltaTime_Injected(IntPtr _unity_self);

		// Token: 0x060000DB RID: 219
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_deltaTime_Injected(IntPtr _unity_self, float value);

		// Token: 0x060000DC RID: 220
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_totalTime_Injected(IntPtr _unity_self);

		// Token: 0x060000DD RID: 221
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_totalTime_Injected(IntPtr _unity_self, float value);

		// Token: 0x060000DE RID: 222
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_delayBeforeLoop_Injected(IntPtr _unity_self);

		// Token: 0x060000DF RID: 223
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_delayBeforeLoop_Injected(IntPtr _unity_self, float value);

		// Token: 0x060000E0 RID: 224
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_loopDuration_Injected(IntPtr _unity_self);

		// Token: 0x060000E1 RID: 225
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_loopDuration_Injected(IntPtr _unity_self, float value);

		// Token: 0x060000E2 RID: 226
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_delayAfterLoop_Injected(IntPtr _unity_self);

		// Token: 0x060000E3 RID: 227
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_delayAfterLoop_Injected(IntPtr _unity_self, float value);

		// Token: 0x060000E4 RID: 228
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_loopIndex_Injected(IntPtr _unity_self);

		// Token: 0x060000E5 RID: 229
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_loopIndex_Injected(IntPtr _unity_self, int value);

		// Token: 0x060000E6 RID: 230
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_loopCount_Injected(IntPtr _unity_self);

		// Token: 0x060000E7 RID: 231
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_loopCount_Injected(IntPtr _unity_self, int value);

		// Token: 0x060000E8 RID: 232
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_GetVFXEventAttribute_Injected(IntPtr _unity_self);

		// Token: 0x04000124 RID: 292
		private IntPtr m_Ptr;

		// Token: 0x04000125 RID: 293
		private bool m_Owner;

		// Token: 0x04000126 RID: 294
		private VFXEventAttribute m_WrapEventAttribute;

		// Token: 0x0200001C RID: 28
		internal static class BindingsMarshaller
		{
			// Token: 0x060000E9 RID: 233 RVA: 0x00003411 File Offset: 0x00001611
			public static IntPtr ConvertToNative(VFXSpawnerState vfxSpawnerState)
			{
				return vfxSpawnerState.m_Ptr;
			}
		}
	}
}
