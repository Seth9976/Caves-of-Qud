using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Jobs
{
	// Token: 0x02000308 RID: 776
	[NativeHeader("Runtime/Transform/ScriptBindings/TransformAccess.bindings.h")]
	public struct TransformAccess
	{
		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06002664 RID: 9828 RVA: 0x0004A350 File Offset: 0x00048550
		// (set) Token: 0x06002665 RID: 9829 RVA: 0x0004A36C File Offset: 0x0004856C
		public Vector3 position
		{
			get
			{
				Vector3 vector;
				TransformAccess.GetPosition(ref this, out vector);
				return vector;
			}
			set
			{
				TransformAccess.SetPosition(ref this, ref value);
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06002666 RID: 9830 RVA: 0x0004A378 File Offset: 0x00048578
		// (set) Token: 0x06002667 RID: 9831 RVA: 0x0004A394 File Offset: 0x00048594
		public Quaternion rotation
		{
			get
			{
				Quaternion quaternion;
				TransformAccess.GetRotation(ref this, out quaternion);
				return quaternion;
			}
			set
			{
				TransformAccess.SetRotation(ref this, ref value);
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06002668 RID: 9832 RVA: 0x0004A3A0 File Offset: 0x000485A0
		// (set) Token: 0x06002669 RID: 9833 RVA: 0x0004A3BC File Offset: 0x000485BC
		public Vector3 localPosition
		{
			get
			{
				Vector3 vector;
				TransformAccess.GetLocalPosition(ref this, out vector);
				return vector;
			}
			set
			{
				TransformAccess.SetLocalPosition(ref this, ref value);
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x0600266A RID: 9834 RVA: 0x0004A3C8 File Offset: 0x000485C8
		// (set) Token: 0x0600266B RID: 9835 RVA: 0x0004A3E4 File Offset: 0x000485E4
		public Quaternion localRotation
		{
			get
			{
				Quaternion quaternion;
				TransformAccess.GetLocalRotation(ref this, out quaternion);
				return quaternion;
			}
			set
			{
				TransformAccess.SetLocalRotation(ref this, ref value);
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x0600266C RID: 9836 RVA: 0x0004A3F0 File Offset: 0x000485F0
		// (set) Token: 0x0600266D RID: 9837 RVA: 0x0004A40C File Offset: 0x0004860C
		public Vector3 localScale
		{
			get
			{
				Vector3 vector;
				TransformAccess.GetLocalScale(ref this, out vector);
				return vector;
			}
			set
			{
				TransformAccess.SetLocalScale(ref this, ref value);
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x0600266E RID: 9838 RVA: 0x0004A418 File Offset: 0x00048618
		public Matrix4x4 localToWorldMatrix
		{
			get
			{
				Matrix4x4 matrix4x;
				TransformAccess.GetLocalToWorldMatrix(ref this, out matrix4x);
				return matrix4x;
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x0600266F RID: 9839 RVA: 0x0004A434 File Offset: 0x00048634
		public Matrix4x4 worldToLocalMatrix
		{
			get
			{
				Matrix4x4 matrix4x;
				TransformAccess.GetWorldToLocalMatrix(ref this, out matrix4x);
				return matrix4x;
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06002670 RID: 9840 RVA: 0x0004A450 File Offset: 0x00048650
		public bool isValid
		{
			get
			{
				return this.hierarchy != IntPtr.Zero;
			}
		}

		// Token: 0x06002671 RID: 9841 RVA: 0x0004A462 File Offset: 0x00048662
		public void SetPositionAndRotation(Vector3 position, Quaternion rotation)
		{
			TransformAccess.SetPositionAndRotation_Internal(ref this, ref position, ref rotation);
		}

		// Token: 0x06002672 RID: 9842 RVA: 0x0004A470 File Offset: 0x00048670
		public void SetLocalPositionAndRotation(Vector3 localPosition, Quaternion localRotation)
		{
			TransformAccess.SetLocalPositionAndRotation_Internal(ref this, ref localPosition, ref localRotation);
		}

		// Token: 0x06002673 RID: 9843 RVA: 0x0004A47E File Offset: 0x0004867E
		public void GetPositionAndRotation(out Vector3 position, out Quaternion rotation)
		{
			TransformAccess.GetPositionAndRotation_Internal(ref this, out position, out rotation);
		}

		// Token: 0x06002674 RID: 9844 RVA: 0x0004A48A File Offset: 0x0004868A
		public void GetLocalPositionAndRotation(out Vector3 localPosition, out Quaternion localRotation)
		{
			TransformAccess.GetLocalPositionAndRotation_Internal(ref this, out localPosition, out localRotation);
		}

		// Token: 0x06002675 RID: 9845
		[NativeMethod(Name = "TransformAccessBindings::SetPositionAndRotation", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPositionAndRotation_Internal(ref TransformAccess access, ref Vector3 position, ref Quaternion rotation);

		// Token: 0x06002676 RID: 9846
		[NativeMethod(Name = "TransformAccessBindings::SetLocalPositionAndRotation", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLocalPositionAndRotation_Internal(ref TransformAccess access, ref Vector3 localPosition, ref Quaternion localRotation);

		// Token: 0x06002677 RID: 9847
		[NativeMethod(Name = "TransformAccessBindings::GetPositionAndRotation", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPositionAndRotation_Internal(ref TransformAccess access, out Vector3 position, out Quaternion rotation);

		// Token: 0x06002678 RID: 9848
		[NativeMethod(Name = "TransformAccessBindings::GetLocalPositionAndRotation", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalPositionAndRotation_Internal(ref TransformAccess access, out Vector3 localPosition, out Quaternion localRotation);

		// Token: 0x06002679 RID: 9849
		[NativeMethod(Name = "TransformAccessBindings::GetPosition", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPosition(ref TransformAccess access, out Vector3 p);

		// Token: 0x0600267A RID: 9850
		[NativeMethod(Name = "TransformAccessBindings::SetPosition", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPosition(ref TransformAccess access, ref Vector3 p);

		// Token: 0x0600267B RID: 9851
		[NativeMethod(Name = "TransformAccessBindings::GetRotation", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRotation(ref TransformAccess access, out Quaternion r);

		// Token: 0x0600267C RID: 9852
		[NativeMethod(Name = "TransformAccessBindings::SetRotation", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRotation(ref TransformAccess access, ref Quaternion r);

		// Token: 0x0600267D RID: 9853
		[NativeMethod(Name = "TransformAccessBindings::GetLocalPosition", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalPosition(ref TransformAccess access, out Vector3 p);

		// Token: 0x0600267E RID: 9854
		[NativeMethod(Name = "TransformAccessBindings::SetLocalPosition", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLocalPosition(ref TransformAccess access, ref Vector3 p);

		// Token: 0x0600267F RID: 9855
		[NativeMethod(Name = "TransformAccessBindings::GetLocalRotation", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalRotation(ref TransformAccess access, out Quaternion r);

		// Token: 0x06002680 RID: 9856
		[NativeMethod(Name = "TransformAccessBindings::SetLocalRotation", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLocalRotation(ref TransformAccess access, ref Quaternion r);

		// Token: 0x06002681 RID: 9857
		[NativeMethod(Name = "TransformAccessBindings::GetLocalScale", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalScale(ref TransformAccess access, out Vector3 r);

		// Token: 0x06002682 RID: 9858
		[NativeMethod(Name = "TransformAccessBindings::SetLocalScale", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLocalScale(ref TransformAccess access, ref Vector3 r);

		// Token: 0x06002683 RID: 9859
		[NativeMethod(Name = "TransformAccessBindings::GetLocalToWorldMatrix", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalToWorldMatrix(ref TransformAccess access, out Matrix4x4 m);

		// Token: 0x06002684 RID: 9860
		[NativeMethod(Name = "TransformAccessBindings::GetWorldToLocalMatrix", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetWorldToLocalMatrix(ref TransformAccess access, out Matrix4x4 m);

		// Token: 0x06002685 RID: 9861 RVA: 0x0004A498 File Offset: 0x00048698
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void CheckHierarchyValid()
		{
			bool flag = !this.isValid;
			if (flag)
			{
				throw new NullReferenceException("The TransformAccess is not valid and points to an invalid hierarchy");
			}
		}

		// Token: 0x06002686 RID: 9862 RVA: 0x00002925 File Offset: 0x00000B25
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void MarkReadWrite()
		{
		}

		// Token: 0x06002687 RID: 9863 RVA: 0x00002925 File Offset: 0x00000B25
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void MarkReadOnly()
		{
		}

		// Token: 0x06002688 RID: 9864 RVA: 0x00002925 File Offset: 0x00000B25
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void CheckWriteAccess()
		{
		}

		// Token: 0x04000AC2 RID: 2754
		private IntPtr hierarchy;

		// Token: 0x04000AC3 RID: 2755
		private int index;
	}
}
