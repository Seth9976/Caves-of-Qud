using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001E1 RID: 481
	[NativeHeader("Runtime/Graphics/Mesh/SkinnedMeshRenderer.h")]
	[RequiredByNativeCode]
	public class SkinnedMeshRenderer : Renderer
	{
		// Token: 0x17000393 RID: 915
		// (get) Token: 0x060014E8 RID: 5352 RVA: 0x00023734 File Offset: 0x00021934
		// (set) Token: 0x060014E9 RID: 5353 RVA: 0x00023758 File Offset: 0x00021958
		public SkinQuality quality
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SkinnedMeshRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SkinnedMeshRenderer.get_quality_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SkinnedMeshRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SkinnedMeshRenderer.set_quality_Injected(intPtr, value);
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x060014EA RID: 5354 RVA: 0x0002377C File Offset: 0x0002197C
		// (set) Token: 0x060014EB RID: 5355 RVA: 0x000237A0 File Offset: 0x000219A0
		public bool updateWhenOffscreen
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SkinnedMeshRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SkinnedMeshRenderer.get_updateWhenOffscreen_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SkinnedMeshRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SkinnedMeshRenderer.set_updateWhenOffscreen_Injected(intPtr, value);
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x060014EC RID: 5356 RVA: 0x000237C4 File Offset: 0x000219C4
		// (set) Token: 0x060014ED RID: 5357 RVA: 0x000237E8 File Offset: 0x000219E8
		public bool forceMatrixRecalculationPerRender
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SkinnedMeshRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SkinnedMeshRenderer.get_forceMatrixRecalculationPerRender_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SkinnedMeshRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SkinnedMeshRenderer.set_forceMatrixRecalculationPerRender_Injected(intPtr, value);
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x060014EE RID: 5358 RVA: 0x0002380C File Offset: 0x00021A0C
		// (set) Token: 0x060014EF RID: 5359 RVA: 0x00023834 File Offset: 0x00021A34
		public Transform rootBone
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SkinnedMeshRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Transform>(SkinnedMeshRenderer.get_rootBone_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SkinnedMeshRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SkinnedMeshRenderer.set_rootBone_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Transform>(value));
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x060014F0 RID: 5360 RVA: 0x0002385C File Offset: 0x00021A5C
		// (set) Token: 0x060014F1 RID: 5361 RVA: 0x00023880 File Offset: 0x00021A80
		public Transform[] bones
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SkinnedMeshRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SkinnedMeshRenderer.get_bones_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SkinnedMeshRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SkinnedMeshRenderer.set_bones_Injected(intPtr, value);
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x060014F2 RID: 5362 RVA: 0x000238A4 File Offset: 0x00021AA4
		// (set) Token: 0x060014F3 RID: 5363 RVA: 0x000238CC File Offset: 0x00021ACC
		[NativeProperty("Mesh")]
		public Mesh sharedMesh
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SkinnedMeshRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Mesh>(SkinnedMeshRenderer.get_sharedMesh_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SkinnedMeshRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SkinnedMeshRenderer.set_sharedMesh_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Mesh>(value));
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x060014F4 RID: 5364 RVA: 0x000238F4 File Offset: 0x00021AF4
		// (set) Token: 0x060014F5 RID: 5365 RVA: 0x00023918 File Offset: 0x00021B18
		[NativeProperty("SkinnedMeshMotionVectors")]
		public bool skinnedMotionVectors
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SkinnedMeshRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SkinnedMeshRenderer.get_skinnedMotionVectors_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SkinnedMeshRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SkinnedMeshRenderer.set_skinnedMotionVectors_Injected(intPtr, value);
			}
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x0002393C File Offset: 0x00021B3C
		public float GetBlendShapeWeight(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SkinnedMeshRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return SkinnedMeshRenderer.GetBlendShapeWeight_Injected(intPtr, index);
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x00023960 File Offset: 0x00021B60
		public void SetBlendShapeWeight(int index, float value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SkinnedMeshRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			SkinnedMeshRenderer.SetBlendShapeWeight_Injected(intPtr, index, value);
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x00023984 File Offset: 0x00021B84
		public void BakeMesh(Mesh mesh)
		{
			this.BakeMesh(mesh, false);
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x00023990 File Offset: 0x00021B90
		public void BakeMesh([NotNull] Mesh mesh, bool useScale)
		{
			if (mesh == null)
			{
				ThrowHelper.ThrowArgumentNullException(mesh, "mesh");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SkinnedMeshRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(mesh);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(mesh, "mesh");
			}
			SkinnedMeshRenderer.BakeMesh_Injected(intPtr, intPtr2, useScale);
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x000239D8 File Offset: 0x00021BD8
		public GraphicsBuffer GetVertexBuffer()
		{
			bool flag = this == null;
			if (flag)
			{
				throw new NullReferenceException();
			}
			return this.GetVertexBufferImpl();
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x00023A04 File Offset: 0x00021C04
		public GraphicsBuffer GetPreviousVertexBuffer()
		{
			bool flag = this == null;
			if (flag)
			{
				throw new NullReferenceException();
			}
			return this.GetPreviousVertexBufferImpl();
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x00023A30 File Offset: 0x00021C30
		[FreeFunction(Name = "SkinnedMeshRendererScripting::GetVertexBufferPtr", HasExplicitThis = true)]
		private GraphicsBuffer GetVertexBufferImpl()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SkinnedMeshRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr vertexBufferImpl_Injected = SkinnedMeshRenderer.GetVertexBufferImpl_Injected(intPtr);
			return (vertexBufferImpl_Injected == 0) ? null : GraphicsBuffer.BindingsMarshaller.ConvertToManaged(vertexBufferImpl_Injected);
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x00023A60 File Offset: 0x00021C60
		[FreeFunction(Name = "SkinnedMeshRendererScripting::GetPreviousVertexBufferPtr", HasExplicitThis = true)]
		private GraphicsBuffer GetPreviousVertexBufferImpl()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SkinnedMeshRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr previousVertexBufferImpl_Injected = SkinnedMeshRenderer.GetPreviousVertexBufferImpl_Injected(intPtr);
			return (previousVertexBufferImpl_Injected == 0) ? null : GraphicsBuffer.BindingsMarshaller.ConvertToManaged(previousVertexBufferImpl_Injected);
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x060014FE RID: 5374 RVA: 0x00023A90 File Offset: 0x00021C90
		// (set) Token: 0x060014FF RID: 5375 RVA: 0x00023AB4 File Offset: 0x00021CB4
		public GraphicsBuffer.Target vertexBufferTarget
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SkinnedMeshRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SkinnedMeshRenderer.get_vertexBufferTarget_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SkinnedMeshRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SkinnedMeshRenderer.set_vertexBufferTarget_Injected(intPtr, value);
			}
		}

		// Token: 0x06001501 RID: 5377
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern SkinQuality get_quality_Injected(IntPtr _unity_self);

		// Token: 0x06001502 RID: 5378
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_quality_Injected(IntPtr _unity_self, SkinQuality value);

		// Token: 0x06001503 RID: 5379
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_updateWhenOffscreen_Injected(IntPtr _unity_self);

		// Token: 0x06001504 RID: 5380
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_updateWhenOffscreen_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06001505 RID: 5381
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_forceMatrixRecalculationPerRender_Injected(IntPtr _unity_self);

		// Token: 0x06001506 RID: 5382
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_forceMatrixRecalculationPerRender_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06001507 RID: 5383
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_rootBone_Injected(IntPtr _unity_self);

		// Token: 0x06001508 RID: 5384
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_rootBone_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x06001509 RID: 5385
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Transform[] get_bones_Injected(IntPtr _unity_self);

		// Token: 0x0600150A RID: 5386
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_bones_Injected(IntPtr _unity_self, Transform[] value);

		// Token: 0x0600150B RID: 5387
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_sharedMesh_Injected(IntPtr _unity_self);

		// Token: 0x0600150C RID: 5388
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_sharedMesh_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x0600150D RID: 5389
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_skinnedMotionVectors_Injected(IntPtr _unity_self);

		// Token: 0x0600150E RID: 5390
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_skinnedMotionVectors_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600150F RID: 5391
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetBlendShapeWeight_Injected(IntPtr _unity_self, int index);

		// Token: 0x06001510 RID: 5392
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetBlendShapeWeight_Injected(IntPtr _unity_self, int index, float value);

		// Token: 0x06001511 RID: 5393
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BakeMesh_Injected(IntPtr _unity_self, IntPtr mesh, bool useScale);

		// Token: 0x06001512 RID: 5394
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetVertexBufferImpl_Injected(IntPtr _unity_self);

		// Token: 0x06001513 RID: 5395
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetPreviousVertexBufferImpl_Injected(IntPtr _unity_self);

		// Token: 0x06001514 RID: 5396
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GraphicsBuffer.Target get_vertexBufferTarget_Injected(IntPtr _unity_self);

		// Token: 0x06001515 RID: 5397
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_vertexBufferTarget_Injected(IntPtr _unity_self, GraphicsBuffer.Target value);
	}
}
