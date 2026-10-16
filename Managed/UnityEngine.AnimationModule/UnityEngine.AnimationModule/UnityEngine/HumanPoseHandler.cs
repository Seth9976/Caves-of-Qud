using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200003D RID: 61
	[NativeHeader("Modules/Animation/HumanPoseHandler.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/Animation.bindings.h")]
	public class HumanPoseHandler : IDisposable
	{
		// Token: 0x06000358 RID: 856 RVA: 0x00007578 File Offset: 0x00005778
		[FreeFunction("AnimationBindings::CreateHumanPoseHandler")]
		private static IntPtr Internal_CreateFromRoot(Avatar avatar, Transform root)
		{
			return HumanPoseHandler.Internal_CreateFromRoot_Injected(Object.MarshalledUnityObject.Marshal<Avatar>(avatar), Object.MarshalledUnityObject.Marshal<Transform>(root));
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00007598 File Offset: 0x00005798
		[FreeFunction("AnimationBindings::CreateHumanPoseHandler", IsThreadSafe = true)]
		private static IntPtr Internal_CreateFromJointPaths(Avatar avatar, string[] jointPaths)
		{
			return HumanPoseHandler.Internal_CreateFromJointPaths_Injected(Object.MarshalledUnityObject.Marshal<Avatar>(avatar), jointPaths);
		}

		// Token: 0x0600035A RID: 858
		[FreeFunction("AnimationBindings::DestroyHumanPoseHandler")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Destroy(IntPtr ptr);

		// Token: 0x0600035B RID: 859 RVA: 0x000075B4 File Offset: 0x000057B4
		private unsafe void GetHumanPose(out Vector3 bodyPosition, out Quaternion bodyRotation, [Out] float[] muscles)
		{
			try
			{
				IntPtr intPtr = HumanPoseHandler.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				if (muscles != null)
				{
					fixed (float[] array = muscles)
					{
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
					}
				}
				HumanPoseHandler.GetHumanPose_Injected(intPtr, out bodyPosition, out bodyRotation, out blittableArrayWrapper);
			}
			finally
			{
				float[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<float>(ref array);
			}
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00007614 File Offset: 0x00005814
		private unsafe void SetHumanPose(ref Vector3 bodyPosition, ref Quaternion bodyRotation, float[] muscles)
		{
			IntPtr intPtr = HumanPoseHandler.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<float> span = new Span<float>(muscles);
			fixed (float* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				HumanPoseHandler.SetHumanPose_Injected(intPtr, ref bodyPosition, ref bodyRotation, ref managedSpanWrapper);
			}
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00007660 File Offset: 0x00005860
		[ThreadSafe]
		private unsafe void GetInternalHumanPose(out Vector3 bodyPosition, out Quaternion bodyRotation, [Out] float[] muscles)
		{
			try
			{
				IntPtr intPtr = HumanPoseHandler.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				if (muscles != null)
				{
					fixed (float[] array = muscles)
					{
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
					}
				}
				HumanPoseHandler.GetInternalHumanPose_Injected(intPtr, out bodyPosition, out bodyRotation, out blittableArrayWrapper);
			}
			finally
			{
				float[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<float>(ref array);
			}
		}

		// Token: 0x0600035E RID: 862 RVA: 0x000076C0 File Offset: 0x000058C0
		[ThreadSafe]
		private unsafe void SetInternalHumanPose(ref Vector3 bodyPosition, ref Quaternion bodyRotation, float[] muscles)
		{
			IntPtr intPtr = HumanPoseHandler.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<float> span = new Span<float>(muscles);
			fixed (float* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				HumanPoseHandler.SetInternalHumanPose_Injected(intPtr, ref bodyPosition, ref bodyRotation, ref managedSpanWrapper);
			}
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000770C File Offset: 0x0000590C
		[ThreadSafe]
		private unsafe void GetInternalAvatarPose(void* avatarPose, int avatarPoseLength)
		{
			IntPtr intPtr = HumanPoseHandler.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			HumanPoseHandler.GetInternalAvatarPose_Injected(intPtr, avatarPose, avatarPoseLength);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00007730 File Offset: 0x00005930
		[ThreadSafe]
		private unsafe void SetInternalAvatarPose(void* avatarPose, int avatarPoseLength)
		{
			IntPtr intPtr = HumanPoseHandler.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			HumanPoseHandler.SetInternalAvatarPose_Injected(intPtr, avatarPose, avatarPoseLength);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00007754 File Offset: 0x00005954
		public void Dispose()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				HumanPoseHandler.Internal_Destroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00007798 File Offset: 0x00005998
		public HumanPoseHandler(Avatar avatar, Transform root)
		{
			this.m_Ptr = IntPtr.Zero;
			bool flag = root == null;
			if (flag)
			{
				throw new ArgumentNullException("HumanPoseHandler root Transform is null");
			}
			bool flag2 = avatar == null;
			if (flag2)
			{
				throw new ArgumentNullException("HumanPoseHandler avatar is null");
			}
			bool flag3 = !avatar.isValid;
			if (flag3)
			{
				throw new ArgumentException("HumanPoseHandler avatar is invalid");
			}
			bool flag4 = !avatar.isHuman;
			if (flag4)
			{
				throw new ArgumentException("HumanPoseHandler avatar is not human");
			}
			this.m_Ptr = HumanPoseHandler.Internal_CreateFromRoot(avatar, root);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00007824 File Offset: 0x00005A24
		public HumanPoseHandler(Avatar avatar, string[] jointPaths)
		{
			this.m_Ptr = IntPtr.Zero;
			bool flag = jointPaths == null;
			if (flag)
			{
				throw new ArgumentNullException("HumanPoseHandler jointPaths array is null");
			}
			bool flag2 = avatar == null;
			if (flag2)
			{
				throw new ArgumentNullException("HumanPoseHandler avatar is null");
			}
			bool flag3 = !avatar.isValid;
			if (flag3)
			{
				throw new ArgumentException("HumanPoseHandler avatar is invalid");
			}
			bool flag4 = !avatar.isHuman;
			if (flag4)
			{
				throw new ArgumentException("HumanPoseHandler avatar is not human");
			}
			this.m_Ptr = HumanPoseHandler.Internal_CreateFromJointPaths(avatar, jointPaths);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x000078AC File Offset: 0x00005AAC
		public void GetHumanPose(ref HumanPose humanPose)
		{
			bool flag = this.m_Ptr == IntPtr.Zero;
			if (flag)
			{
				throw new NullReferenceException("HumanPoseHandler is not initialized properly");
			}
			humanPose.Init();
			this.GetHumanPose(out humanPose.bodyPosition, out humanPose.bodyRotation, humanPose.muscles);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x000078FC File Offset: 0x00005AFC
		public void SetHumanPose(ref HumanPose humanPose)
		{
			bool flag = this.m_Ptr == IntPtr.Zero;
			if (flag)
			{
				throw new NullReferenceException("HumanPoseHandler is not initialized properly");
			}
			humanPose.Init();
			this.SetHumanPose(ref humanPose.bodyPosition, ref humanPose.bodyRotation, humanPose.muscles);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000794C File Offset: 0x00005B4C
		public void GetInternalHumanPose(ref HumanPose humanPose)
		{
			bool flag = this.m_Ptr == IntPtr.Zero;
			if (flag)
			{
				throw new NullReferenceException("HumanPoseHandler is not initialized properly");
			}
			humanPose.Init();
			this.GetInternalHumanPose(out humanPose.bodyPosition, out humanPose.bodyRotation, humanPose.muscles);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000799C File Offset: 0x00005B9C
		public void SetInternalHumanPose(ref HumanPose humanPose)
		{
			bool flag = this.m_Ptr == IntPtr.Zero;
			if (flag)
			{
				throw new NullReferenceException("HumanPoseHandler is not initialized properly");
			}
			humanPose.Init();
			this.SetInternalHumanPose(ref humanPose.bodyPosition, ref humanPose.bodyRotation, humanPose.muscles);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x000079EC File Offset: 0x00005BEC
		public void GetInternalAvatarPose(NativeArray<float> avatarPose)
		{
			bool flag = this.m_Ptr == IntPtr.Zero;
			if (flag)
			{
				throw new NullReferenceException("HumanPoseHandler is not initialized properly");
			}
			this.GetInternalAvatarPose(avatarPose.GetUnsafePtr<float>(), avatarPose.Length);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00007A30 File Offset: 0x00005C30
		public void SetInternalAvatarPose(NativeArray<float> avatarPose)
		{
			bool flag = this.m_Ptr == IntPtr.Zero;
			if (flag)
			{
				throw new NullReferenceException("HumanPoseHandler is not initialized properly");
			}
			this.SetInternalAvatarPose(avatarPose.GetUnsafeReadOnlyPtr<float>(), avatarPose.Length);
		}

		// Token: 0x0600036A RID: 874
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_CreateFromRoot_Injected(IntPtr avatar, IntPtr root);

		// Token: 0x0600036B RID: 875
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_CreateFromJointPaths_Injected(IntPtr avatar, string[] jointPaths);

		// Token: 0x0600036C RID: 876
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetHumanPose_Injected(IntPtr _unity_self, out Vector3 bodyPosition, out Quaternion bodyRotation, out BlittableArrayWrapper muscles);

		// Token: 0x0600036D RID: 877
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetHumanPose_Injected(IntPtr _unity_self, ref Vector3 bodyPosition, ref Quaternion bodyRotation, ref ManagedSpanWrapper muscles);

		// Token: 0x0600036E RID: 878
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetInternalHumanPose_Injected(IntPtr _unity_self, out Vector3 bodyPosition, out Quaternion bodyRotation, out BlittableArrayWrapper muscles);

		// Token: 0x0600036F RID: 879
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetInternalHumanPose_Injected(IntPtr _unity_self, ref Vector3 bodyPosition, ref Quaternion bodyRotation, ref ManagedSpanWrapper muscles);

		// Token: 0x06000370 RID: 880
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void GetInternalAvatarPose_Injected(IntPtr _unity_self, void* avatarPose, int avatarPoseLength);

		// Token: 0x06000371 RID: 881
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void SetInternalAvatarPose_Injected(IntPtr _unity_self, void* avatarPose, int avatarPoseLength);

		// Token: 0x04000147 RID: 327
		internal IntPtr m_Ptr;

		// Token: 0x0200003E RID: 62
		internal static class BindingsMarshaller
		{
			// Token: 0x06000372 RID: 882 RVA: 0x00007A71 File Offset: 0x00005C71
			public static IntPtr ConvertToNative(HumanPoseHandler humanPoseHandler)
			{
				return humanPoseHandler.m_Ptr;
			}
		}
	}
}
