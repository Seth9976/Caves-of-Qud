using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Playables
{
	// Token: 0x02000587 RID: 1415
	[NativeHeader("Runtime/Camera//Director/CameraPlayable.h")]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[StaticAccessor("CameraPlayableBindings", StaticAccessorType.DoubleColon)]
	[RequiredByNativeCode]
	[NativeHeader("Runtime/Export/Director/CameraPlayable.bindings.h")]
	public struct CameraPlayable : IPlayable, IEquatable<CameraPlayable>
	{
		// Token: 0x060036B5 RID: 14005 RVA: 0x0006C12C File Offset: 0x0006A32C
		public static CameraPlayable Create(PlayableGraph graph, Camera camera)
		{
			PlayableHandle playableHandle = CameraPlayable.CreateHandle(graph, camera);
			return new CameraPlayable(playableHandle);
		}

		// Token: 0x060036B6 RID: 14006 RVA: 0x0006C14C File Offset: 0x0006A34C
		private static PlayableHandle CreateHandle(PlayableGraph graph, Camera camera)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !CameraPlayable.InternalCreateCameraPlayable(ref graph, camera, ref @null);
			PlayableHandle playableHandle;
			if (flag)
			{
				playableHandle = PlayableHandle.Null;
			}
			else
			{
				playableHandle = @null;
			}
			return playableHandle;
		}

		// Token: 0x060036B7 RID: 14007 RVA: 0x0006C180 File Offset: 0x0006A380
		internal CameraPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<CameraPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an CameraPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x060036B8 RID: 14008 RVA: 0x0006C1BC File Offset: 0x0006A3BC
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x060036B9 RID: 14009 RVA: 0x0006C1D4 File Offset: 0x0006A3D4
		public static implicit operator Playable(CameraPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x060036BA RID: 14010 RVA: 0x0006C1F4 File Offset: 0x0006A3F4
		public static explicit operator CameraPlayable(Playable playable)
		{
			return new CameraPlayable(playable.GetHandle());
		}

		// Token: 0x060036BB RID: 14011 RVA: 0x0006C214 File Offset: 0x0006A414
		public bool Equals(CameraPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x060036BC RID: 14012 RVA: 0x0006C238 File Offset: 0x0006A438
		public Camera GetCamera()
		{
			return CameraPlayable.GetCameraInternal(ref this.m_Handle);
		}

		// Token: 0x060036BD RID: 14013 RVA: 0x0006C255 File Offset: 0x0006A455
		public void SetCamera(Camera value)
		{
			CameraPlayable.SetCameraInternal(ref this.m_Handle, value);
		}

		// Token: 0x060036BE RID: 14014 RVA: 0x0006C268 File Offset: 0x0006A468
		[NativeThrows]
		private static Camera GetCameraInternal(ref PlayableHandle hdl)
		{
			return Unmarshal.UnmarshalUnityObject<Camera>(CameraPlayable.GetCameraInternal_Injected(ref hdl));
		}

		// Token: 0x060036BF RID: 14015 RVA: 0x0006C280 File Offset: 0x0006A480
		[NativeThrows]
		private static void SetCameraInternal(ref PlayableHandle hdl, Camera camera)
		{
			CameraPlayable.SetCameraInternal_Injected(ref hdl, Object.MarshalledUnityObject.Marshal<Camera>(camera));
		}

		// Token: 0x060036C0 RID: 14016 RVA: 0x0006C29C File Offset: 0x0006A49C
		[NativeThrows]
		private static bool InternalCreateCameraPlayable(ref PlayableGraph graph, Camera camera, ref PlayableHandle handle)
		{
			return CameraPlayable.InternalCreateCameraPlayable_Injected(ref graph, Object.MarshalledUnityObject.Marshal<Camera>(camera), ref handle);
		}

		// Token: 0x060036C1 RID: 14017
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ValidateType(ref PlayableHandle hdl);

		// Token: 0x060036C2 RID: 14018
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetCameraInternal_Injected(ref PlayableHandle hdl);

		// Token: 0x060036C3 RID: 14019
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetCameraInternal_Injected(ref PlayableHandle hdl, IntPtr camera);

		// Token: 0x060036C4 RID: 14020
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool InternalCreateCameraPlayable_Injected(ref PlayableGraph graph, IntPtr camera, ref PlayableHandle handle);

		// Token: 0x040012DC RID: 4828
		private PlayableHandle m_Handle;
	}
}
