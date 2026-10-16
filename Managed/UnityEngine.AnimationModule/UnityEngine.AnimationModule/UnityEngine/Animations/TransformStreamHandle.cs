using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	// Token: 0x02000061 RID: 97
	[NativeHeader("Modules/Animation/Director/AnimationStreamHandles.h")]
	[MovedFrom("UnityEngine.Experimental.Animations")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationStreamHandles.bindings.h")]
	public struct TransformStreamHandle
	{
		// Token: 0x0600054F RID: 1359 RVA: 0x0000A1D0 File Offset: 0x000083D0
		public bool IsValid(AnimationStream stream)
		{
			return this.IsValidInternal(ref stream);
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0000A1EC File Offset: 0x000083EC
		private bool IsValidInternal(ref AnimationStream stream)
		{
			return stream.isValid && this.createdByNative && this.hasHandleIndex;
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x0000A218 File Offset: 0x00008418
		private bool createdByNative
		{
			get
			{
				return this.animatorBindingsVersion > 0U;
			}
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0000A234 File Offset: 0x00008434
		private bool IsSameVersionAsStream(ref AnimationStream stream)
		{
			return this.animatorBindingsVersion == stream.animatorBindingsVersion;
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x0000A254 File Offset: 0x00008454
		private bool hasHandleIndex
		{
			get
			{
				return this.handleIndex != -1;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x0000A274 File Offset: 0x00008474
		private bool hasSkeletonIndex
		{
			get
			{
				return this.skeletonIndex != -1;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x0000A29C File Offset: 0x0000849C
		// (set) Token: 0x06000555 RID: 1365 RVA: 0x0000A292 File Offset: 0x00008492
		internal uint animatorBindingsVersion
		{
			get
			{
				return this.m_AnimatorBindingsVersion;
			}
			private set
			{
				this.m_AnimatorBindingsVersion = value;
			}
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0000A2B4 File Offset: 0x000084B4
		public void Resolve(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0000A2C0 File Offset: 0x000084C0
		public bool IsResolved(AnimationStream stream)
		{
			return this.IsResolvedInternal(ref stream);
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0000A2DC File Offset: 0x000084DC
		private bool IsResolvedInternal(ref AnimationStream stream)
		{
			return this.IsValidInternal(ref stream) && this.IsSameVersionAsStream(ref stream) && this.hasSkeletonIndex;
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0000A30C File Offset: 0x0000850C
		private void CheckIsValidAndResolve(ref AnimationStream stream)
		{
			stream.CheckIsValid();
			bool flag = this.IsResolvedInternal(ref stream);
			if (!flag)
			{
				bool flag2 = !this.createdByNative || !this.hasHandleIndex;
				if (flag2)
				{
					throw new InvalidOperationException("The TransformStreamHandle is invalid. Please use proper function to create the handle.");
				}
				bool flag3 = !this.IsSameVersionAsStream(ref stream) || (this.hasHandleIndex && !this.hasSkeletonIndex);
				if (flag3)
				{
					this.ResolveInternal(ref stream);
				}
				bool flag4 = this.hasHandleIndex && !this.hasSkeletonIndex;
				if (flag4)
				{
					throw new InvalidOperationException("The TransformStreamHandle cannot be resolved.");
				}
			}
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0000A3A4 File Offset: 0x000085A4
		public Vector3 GetPosition(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
			return this.GetPositionInternal(ref stream);
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0000A3C7 File Offset: 0x000085C7
		public void SetPosition(AnimationStream stream, Vector3 position)
		{
			this.CheckIsValidAndResolve(ref stream);
			this.SetPositionInternal(ref stream, position);
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0000A3E0 File Offset: 0x000085E0
		public Quaternion GetRotation(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
			return this.GetRotationInternal(ref stream);
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0000A403 File Offset: 0x00008603
		public void SetRotation(AnimationStream stream, Quaternion rotation)
		{
			this.CheckIsValidAndResolve(ref stream);
			this.SetRotationInternal(ref stream, rotation);
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0000A41C File Offset: 0x0000861C
		public Vector3 GetLocalPosition(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
			return this.GetLocalPositionInternal(ref stream);
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0000A43F File Offset: 0x0000863F
		public void SetLocalPosition(AnimationStream stream, Vector3 position)
		{
			this.CheckIsValidAndResolve(ref stream);
			this.SetLocalPositionInternal(ref stream, position);
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0000A458 File Offset: 0x00008658
		public Quaternion GetLocalRotation(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
			return this.GetLocalRotationInternal(ref stream);
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0000A47B File Offset: 0x0000867B
		public void SetLocalRotation(AnimationStream stream, Quaternion rotation)
		{
			this.CheckIsValidAndResolve(ref stream);
			this.SetLocalRotationInternal(ref stream, rotation);
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0000A494 File Offset: 0x00008694
		public Vector3 GetLocalScale(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
			return this.GetLocalScaleInternal(ref stream);
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0000A4B7 File Offset: 0x000086B7
		public void SetLocalScale(AnimationStream stream, Vector3 scale)
		{
			this.CheckIsValidAndResolve(ref stream);
			this.SetLocalScaleInternal(ref stream, scale);
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0000A4D0 File Offset: 0x000086D0
		public Matrix4x4 GetLocalToParentMatrix(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
			return this.GetLocalToParentMatrixInternal(ref stream);
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0000A4F4 File Offset: 0x000086F4
		public bool GetPositionReadMask(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
			return this.GetPositionReadMaskInternal(ref stream);
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0000A518 File Offset: 0x00008718
		public bool GetRotationReadMask(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
			return this.GetRotationReadMaskInternal(ref stream);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0000A53C File Offset: 0x0000873C
		public bool GetScaleReadMask(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
			return this.GetScaleReadMaskInternal(ref stream);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0000A55F File Offset: 0x0000875F
		public void GetLocalTRS(AnimationStream stream, out Vector3 position, out Quaternion rotation, out Vector3 scale)
		{
			this.CheckIsValidAndResolve(ref stream);
			this.GetLocalTRSInternal(ref stream, out position, out rotation, out scale);
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0000A578 File Offset: 0x00008778
		public void SetLocalTRS(AnimationStream stream, Vector3 position, Quaternion rotation, Vector3 scale, bool useMask)
		{
			this.CheckIsValidAndResolve(ref stream);
			this.SetLocalTRSInternal(ref stream, position, rotation, scale, useMask);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0000A593 File Offset: 0x00008793
		public void GetGlobalTR(AnimationStream stream, out Vector3 position, out Quaternion rotation)
		{
			this.CheckIsValidAndResolve(ref stream);
			this.GetGlobalTRInternal(ref stream, out position, out rotation);
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0000A5AC File Offset: 0x000087AC
		public Matrix4x4 GetLocalToWorldMatrix(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
			return this.GetLocalToWorldMatrixInternal(ref stream);
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0000A5CF File Offset: 0x000087CF
		public void SetGlobalTR(AnimationStream stream, Vector3 position, Quaternion rotation, bool useMask)
		{
			this.CheckIsValidAndResolve(ref stream);
			this.SetGlobalTRInternal(ref stream, position, rotation, useMask);
		}

		// Token: 0x0600056E RID: 1390
		[NativeMethod(Name = "Resolve", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ResolveInternal(ref AnimationStream stream);

		// Token: 0x0600056F RID: 1391 RVA: 0x0000A5E8 File Offset: 0x000087E8
		[NativeMethod(Name = "TransformStreamHandleBindings::GetPositionInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private Vector3 GetPositionInternal(ref AnimationStream stream)
		{
			Vector3 vector;
			TransformStreamHandle.GetPositionInternal_Injected(ref this, ref stream, out vector);
			return vector;
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0000A600 File Offset: 0x00008800
		[NativeMethod(Name = "TransformStreamHandleBindings::SetPositionInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private void SetPositionInternal(ref AnimationStream stream, Vector3 position)
		{
			TransformStreamHandle.SetPositionInternal_Injected(ref this, ref stream, ref position);
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0000A618 File Offset: 0x00008818
		[NativeMethod(Name = "TransformStreamHandleBindings::GetRotationInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private Quaternion GetRotationInternal(ref AnimationStream stream)
		{
			Quaternion quaternion;
			TransformStreamHandle.GetRotationInternal_Injected(ref this, ref stream, out quaternion);
			return quaternion;
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0000A630 File Offset: 0x00008830
		[NativeMethod(Name = "TransformStreamHandleBindings::SetRotationInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private void SetRotationInternal(ref AnimationStream stream, Quaternion rotation)
		{
			TransformStreamHandle.SetRotationInternal_Injected(ref this, ref stream, ref rotation);
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0000A648 File Offset: 0x00008848
		[NativeMethod(Name = "TransformStreamHandleBindings::GetLocalPositionInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private Vector3 GetLocalPositionInternal(ref AnimationStream stream)
		{
			Vector3 vector;
			TransformStreamHandle.GetLocalPositionInternal_Injected(ref this, ref stream, out vector);
			return vector;
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0000A660 File Offset: 0x00008860
		[NativeMethod(Name = "TransformStreamHandleBindings::SetLocalPositionInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private void SetLocalPositionInternal(ref AnimationStream stream, Vector3 position)
		{
			TransformStreamHandle.SetLocalPositionInternal_Injected(ref this, ref stream, ref position);
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0000A678 File Offset: 0x00008878
		[NativeMethod(Name = "TransformStreamHandleBindings::GetLocalRotationInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private Quaternion GetLocalRotationInternal(ref AnimationStream stream)
		{
			Quaternion quaternion;
			TransformStreamHandle.GetLocalRotationInternal_Injected(ref this, ref stream, out quaternion);
			return quaternion;
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0000A690 File Offset: 0x00008890
		[NativeMethod(Name = "TransformStreamHandleBindings::SetLocalRotationInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private void SetLocalRotationInternal(ref AnimationStream stream, Quaternion rotation)
		{
			TransformStreamHandle.SetLocalRotationInternal_Injected(ref this, ref stream, ref rotation);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0000A6A8 File Offset: 0x000088A8
		[NativeMethod(Name = "TransformStreamHandleBindings::GetLocalScaleInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private Vector3 GetLocalScaleInternal(ref AnimationStream stream)
		{
			Vector3 vector;
			TransformStreamHandle.GetLocalScaleInternal_Injected(ref this, ref stream, out vector);
			return vector;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0000A6C0 File Offset: 0x000088C0
		[NativeMethod(Name = "TransformStreamHandleBindings::SetLocalScaleInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private void SetLocalScaleInternal(ref AnimationStream stream, Vector3 scale)
		{
			TransformStreamHandle.SetLocalScaleInternal_Injected(ref this, ref stream, ref scale);
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0000A6D8 File Offset: 0x000088D8
		[NativeMethod(Name = "TransformStreamHandleBindings::GetLocalToParentMatrixInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private Matrix4x4 GetLocalToParentMatrixInternal(ref AnimationStream stream)
		{
			Matrix4x4 matrix4x;
			TransformStreamHandle.GetLocalToParentMatrixInternal_Injected(ref this, ref stream, out matrix4x);
			return matrix4x;
		}

		// Token: 0x0600057A RID: 1402
		[NativeMethod(Name = "TransformStreamHandleBindings::GetPositionReadMaskInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool GetPositionReadMaskInternal(ref AnimationStream stream);

		// Token: 0x0600057B RID: 1403
		[NativeMethod(Name = "TransformStreamHandleBindings::GetRotationReadMaskInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool GetRotationReadMaskInternal(ref AnimationStream stream);

		// Token: 0x0600057C RID: 1404
		[NativeMethod(Name = "TransformStreamHandleBindings::GetScaleReadMaskInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool GetScaleReadMaskInternal(ref AnimationStream stream);

		// Token: 0x0600057D RID: 1405
		[NativeMethod(Name = "TransformStreamHandleBindings::GetLocalTRSInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetLocalTRSInternal(ref AnimationStream stream, out Vector3 position, out Quaternion rotation, out Vector3 scale);

		// Token: 0x0600057E RID: 1406 RVA: 0x0000A6F0 File Offset: 0x000088F0
		[NativeMethod(Name = "TransformStreamHandleBindings::SetLocalTRSInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private void SetLocalTRSInternal(ref AnimationStream stream, Vector3 position, Quaternion rotation, Vector3 scale, bool useMask)
		{
			TransformStreamHandle.SetLocalTRSInternal_Injected(ref this, ref stream, ref position, ref rotation, ref scale, useMask);
		}

		// Token: 0x0600057F RID: 1407
		[NativeMethod(Name = "TransformStreamHandleBindings::GetGlobalTRInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetGlobalTRInternal(ref AnimationStream stream, out Vector3 position, out Quaternion rotation);

		// Token: 0x06000580 RID: 1408 RVA: 0x0000A70C File Offset: 0x0000890C
		[NativeMethod(Name = "TransformStreamHandleBindings::GetLocalToWorldMatrixInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private Matrix4x4 GetLocalToWorldMatrixInternal(ref AnimationStream stream)
		{
			Matrix4x4 matrix4x;
			TransformStreamHandle.GetLocalToWorldMatrixInternal_Injected(ref this, ref stream, out matrix4x);
			return matrix4x;
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0000A724 File Offset: 0x00008924
		[NativeMethod(Name = "TransformStreamHandleBindings::SetGlobalTRInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private void SetGlobalTRInternal(ref AnimationStream stream, Vector3 position, Quaternion rotation, bool useMask)
		{
			TransformStreamHandle.SetGlobalTRInternal_Injected(ref this, ref stream, ref position, ref rotation, useMask);
		}

		// Token: 0x06000582 RID: 1410
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPositionInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, out Vector3 ret);

		// Token: 0x06000583 RID: 1411
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPositionInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, [In] ref Vector3 position);

		// Token: 0x06000584 RID: 1412
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRotationInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, out Quaternion ret);

		// Token: 0x06000585 RID: 1413
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRotationInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, [In] ref Quaternion rotation);

		// Token: 0x06000586 RID: 1414
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalPositionInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, out Vector3 ret);

		// Token: 0x06000587 RID: 1415
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLocalPositionInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, [In] ref Vector3 position);

		// Token: 0x06000588 RID: 1416
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalRotationInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, out Quaternion ret);

		// Token: 0x06000589 RID: 1417
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLocalRotationInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, [In] ref Quaternion rotation);

		// Token: 0x0600058A RID: 1418
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalScaleInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, out Vector3 ret);

		// Token: 0x0600058B RID: 1419
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLocalScaleInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, [In] ref Vector3 scale);

		// Token: 0x0600058C RID: 1420
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalToParentMatrixInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, out Matrix4x4 ret);

		// Token: 0x0600058D RID: 1421
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLocalTRSInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, [In] ref Vector3 position, [In] ref Quaternion rotation, [In] ref Vector3 scale, bool useMask);

		// Token: 0x0600058E RID: 1422
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalToWorldMatrixInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, out Matrix4x4 ret);

		// Token: 0x0600058F RID: 1423
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalTRInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, [In] ref Vector3 position, [In] ref Quaternion rotation, bool useMask);

		// Token: 0x0400017C RID: 380
		private uint m_AnimatorBindingsVersion;

		// Token: 0x0400017D RID: 381
		private int handleIndex;

		// Token: 0x0400017E RID: 382
		private int skeletonIndex;
	}
}
