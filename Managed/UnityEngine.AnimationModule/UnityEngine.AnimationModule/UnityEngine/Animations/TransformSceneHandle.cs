using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	// Token: 0x02000063 RID: 99
	[MovedFrom("UnityEngine.Experimental.Animations")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationStreamHandles.bindings.h")]
	[NativeHeader("Modules/Animation/Director/AnimationSceneHandles.h")]
	public struct TransformSceneHandle
	{
		// Token: 0x060005AC RID: 1452 RVA: 0x0000AB24 File Offset: 0x00008D24
		public bool IsValid(AnimationStream stream)
		{
			return stream.isValid && this.createdByNative && this.hasTransformSceneHandleDefinitionIndex && this.HasValidTransform(ref stream);
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x0000AB5C File Offset: 0x00008D5C
		private bool createdByNative
		{
			get
			{
				return this.valid > 0U;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x0000AB78 File Offset: 0x00008D78
		private bool hasTransformSceneHandleDefinitionIndex
		{
			get
			{
				return this.transformSceneHandleDefinitionIndex != -1;
			}
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0000AB98 File Offset: 0x00008D98
		private void CheckIsValid(ref AnimationStream stream)
		{
			stream.CheckIsValid();
			bool flag = !this.createdByNative || !this.hasTransformSceneHandleDefinitionIndex;
			if (flag)
			{
				throw new InvalidOperationException("The TransformSceneHandle is invalid. Please use proper function to create the handle.");
			}
			bool flag2 = !this.HasValidTransform(ref stream);
			if (flag2)
			{
				throw new NullReferenceException("The transform is invalid.");
			}
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0000ABEC File Offset: 0x00008DEC
		public Vector3 GetPosition(AnimationStream stream)
		{
			this.CheckIsValid(ref stream);
			return this.GetPositionInternal(ref stream);
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00002059 File Offset: 0x00000259
		[Obsolete("SceneHandle is now read-only; it was problematic with the engine multithreading and determinism", true)]
		public void SetPosition(AnimationStream stream, Vector3 position)
		{
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x0000AC10 File Offset: 0x00008E10
		public Vector3 GetLocalPosition(AnimationStream stream)
		{
			this.CheckIsValid(ref stream);
			return this.GetLocalPositionInternal(ref stream);
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00002059 File Offset: 0x00000259
		[Obsolete("SceneHandle is now read-only; it was problematic with the engine multithreading and determinism", true)]
		public void SetLocalPosition(AnimationStream stream, Vector3 position)
		{
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x0000AC34 File Offset: 0x00008E34
		public Quaternion GetRotation(AnimationStream stream)
		{
			this.CheckIsValid(ref stream);
			return this.GetRotationInternal(ref stream);
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00002059 File Offset: 0x00000259
		[Obsolete("SceneHandle is now read-only; it was problematic with the engine multithreading and determinism", true)]
		public void SetRotation(AnimationStream stream, Quaternion rotation)
		{
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x0000AC58 File Offset: 0x00008E58
		public Quaternion GetLocalRotation(AnimationStream stream)
		{
			this.CheckIsValid(ref stream);
			return this.GetLocalRotationInternal(ref stream);
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00002059 File Offset: 0x00000259
		[Obsolete("SceneHandle is now read-only; it was problematic with the engine multithreading and determinism", true)]
		public void SetLocalRotation(AnimationStream stream, Quaternion rotation)
		{
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x0000AC7C File Offset: 0x00008E7C
		public Vector3 GetLocalScale(AnimationStream stream)
		{
			this.CheckIsValid(ref stream);
			return this.GetLocalScaleInternal(ref stream);
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x0000AC9F File Offset: 0x00008E9F
		public void GetLocalTRS(AnimationStream stream, out Vector3 position, out Quaternion rotation, out Vector3 scale)
		{
			this.CheckIsValid(ref stream);
			this.GetLocalTRSInternal(ref stream, out position, out rotation, out scale);
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0000ACB8 File Offset: 0x00008EB8
		public Matrix4x4 GetLocalToParentMatrix(AnimationStream stream)
		{
			this.CheckIsValid(ref stream);
			return this.GetLocalToParentMatrixInternal(ref stream);
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0000ACDB File Offset: 0x00008EDB
		public void GetGlobalTR(AnimationStream stream, out Vector3 position, out Quaternion rotation)
		{
			this.CheckIsValid(ref stream);
			this.GetGlobalTRInternal(ref stream, out position, out rotation);
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0000ACF4 File Offset: 0x00008EF4
		public Matrix4x4 GetLocalToWorldMatrix(AnimationStream stream)
		{
			this.CheckIsValid(ref stream);
			return this.GetLocalToWorldMatrixInternal(ref stream);
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00002059 File Offset: 0x00000259
		[Obsolete("SceneHandle is now read-only; it was problematic with the engine multithreading and determinism", true)]
		public void SetLocalScale(AnimationStream stream, Vector3 scale)
		{
		}

		// Token: 0x060005BE RID: 1470
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool HasValidTransform(ref AnimationStream stream);

		// Token: 0x060005BF RID: 1471 RVA: 0x0000AD18 File Offset: 0x00008F18
		[NativeMethod(Name = "TransformSceneHandleBindings::GetPositionInternal", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 GetPositionInternal(ref AnimationStream stream)
		{
			Vector3 vector;
			TransformSceneHandle.GetPositionInternal_Injected(ref this, ref stream, out vector);
			return vector;
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0000AD30 File Offset: 0x00008F30
		[NativeMethod(Name = "TransformSceneHandleBindings::GetLocalPositionInternal", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 GetLocalPositionInternal(ref AnimationStream stream)
		{
			Vector3 vector;
			TransformSceneHandle.GetLocalPositionInternal_Injected(ref this, ref stream, out vector);
			return vector;
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0000AD48 File Offset: 0x00008F48
		[NativeMethod(Name = "TransformSceneHandleBindings::GetRotationInternal", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Quaternion GetRotationInternal(ref AnimationStream stream)
		{
			Quaternion quaternion;
			TransformSceneHandle.GetRotationInternal_Injected(ref this, ref stream, out quaternion);
			return quaternion;
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0000AD60 File Offset: 0x00008F60
		[NativeMethod(Name = "TransformSceneHandleBindings::GetLocalRotationInternal", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Quaternion GetLocalRotationInternal(ref AnimationStream stream)
		{
			Quaternion quaternion;
			TransformSceneHandle.GetLocalRotationInternal_Injected(ref this, ref stream, out quaternion);
			return quaternion;
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0000AD78 File Offset: 0x00008F78
		[NativeMethod(Name = "TransformSceneHandleBindings::GetLocalScaleInternal", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 GetLocalScaleInternal(ref AnimationStream stream)
		{
			Vector3 vector;
			TransformSceneHandle.GetLocalScaleInternal_Injected(ref this, ref stream, out vector);
			return vector;
		}

		// Token: 0x060005C4 RID: 1476
		[NativeMethod(Name = "TransformSceneHandleBindings::GetLocalTRSInternal", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetLocalTRSInternal(ref AnimationStream stream, out Vector3 position, out Quaternion rotation, out Vector3 scale);

		// Token: 0x060005C5 RID: 1477 RVA: 0x0000AD90 File Offset: 0x00008F90
		[NativeMethod(Name = "TransformSceneHandleBindings::GetLocalToParentMatrixInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private Matrix4x4 GetLocalToParentMatrixInternal(ref AnimationStream stream)
		{
			Matrix4x4 matrix4x;
			TransformSceneHandle.GetLocalToParentMatrixInternal_Injected(ref this, ref stream, out matrix4x);
			return matrix4x;
		}

		// Token: 0x060005C6 RID: 1478
		[NativeMethod(Name = "TransformSceneHandleBindings::GetGlobalTRInternal", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetGlobalTRInternal(ref AnimationStream stream, out Vector3 position, out Quaternion rotation);

		// Token: 0x060005C7 RID: 1479 RVA: 0x0000ADA8 File Offset: 0x00008FA8
		[NativeMethod(Name = "TransformSceneHandleBindings::GetLocalToWorldMatrixInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private Matrix4x4 GetLocalToWorldMatrixInternal(ref AnimationStream stream)
		{
			Matrix4x4 matrix4x;
			TransformSceneHandle.GetLocalToWorldMatrixInternal_Injected(ref this, ref stream, out matrix4x);
			return matrix4x;
		}

		// Token: 0x060005C8 RID: 1480
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPositionInternal_Injected(ref TransformSceneHandle _unity_self, ref AnimationStream stream, out Vector3 ret);

		// Token: 0x060005C9 RID: 1481
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalPositionInternal_Injected(ref TransformSceneHandle _unity_self, ref AnimationStream stream, out Vector3 ret);

		// Token: 0x060005CA RID: 1482
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRotationInternal_Injected(ref TransformSceneHandle _unity_self, ref AnimationStream stream, out Quaternion ret);

		// Token: 0x060005CB RID: 1483
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalRotationInternal_Injected(ref TransformSceneHandle _unity_self, ref AnimationStream stream, out Quaternion ret);

		// Token: 0x060005CC RID: 1484
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalScaleInternal_Injected(ref TransformSceneHandle _unity_self, ref AnimationStream stream, out Vector3 ret);

		// Token: 0x060005CD RID: 1485
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalToParentMatrixInternal_Injected(ref TransformSceneHandle _unity_self, ref AnimationStream stream, out Matrix4x4 ret);

		// Token: 0x060005CE RID: 1486
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalToWorldMatrixInternal_Injected(ref TransformSceneHandle _unity_self, ref AnimationStream stream, out Matrix4x4 ret);

		// Token: 0x04000183 RID: 387
		private uint valid;

		// Token: 0x04000184 RID: 388
		private int transformSceneHandleDefinitionIndex;
	}
}
