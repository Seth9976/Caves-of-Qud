using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200015C RID: 348
	[NativeHeader("Runtime/Export/Graphics/BillboardRenderer.bindings.h")]
	[NativeHeader("Runtime/Graphics/Billboard/BillboardAsset.h")]
	public sealed class BillboardAsset : Object
	{
		// Token: 0x06000B05 RID: 2821 RVA: 0x00014748 File Offset: 0x00012948
		public BillboardAsset()
		{
			BillboardAsset.Internal_Create(this);
		}

		// Token: 0x06000B06 RID: 2822
		[FreeFunction(Name = "BillboardRenderer_Bindings::Internal_Create")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] BillboardAsset obj);

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000B07 RID: 2823 RVA: 0x0001475C File Offset: 0x0001295C
		// (set) Token: 0x06000B08 RID: 2824 RVA: 0x00014780 File Offset: 0x00012980
		public float width
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BillboardAsset>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return BillboardAsset.get_width_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BillboardAsset>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BillboardAsset.set_width_Injected(intPtr, value);
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000B09 RID: 2825 RVA: 0x000147A4 File Offset: 0x000129A4
		// (set) Token: 0x06000B0A RID: 2826 RVA: 0x000147C8 File Offset: 0x000129C8
		public float height
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BillboardAsset>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return BillboardAsset.get_height_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BillboardAsset>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BillboardAsset.set_height_Injected(intPtr, value);
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000B0B RID: 2827 RVA: 0x000147EC File Offset: 0x000129EC
		// (set) Token: 0x06000B0C RID: 2828 RVA: 0x00014810 File Offset: 0x00012A10
		public float bottom
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BillboardAsset>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return BillboardAsset.get_bottom_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BillboardAsset>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BillboardAsset.set_bottom_Injected(intPtr, value);
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000B0D RID: 2829 RVA: 0x00014834 File Offset: 0x00012A34
		public int imageCount
		{
			[NativeMethod("GetNumImages")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BillboardAsset>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return BillboardAsset.get_imageCount_Injected(intPtr);
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000B0E RID: 2830 RVA: 0x00014858 File Offset: 0x00012A58
		public int vertexCount
		{
			[NativeMethod("GetNumVertices")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BillboardAsset>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return BillboardAsset.get_vertexCount_Injected(intPtr);
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x0001487C File Offset: 0x00012A7C
		public int indexCount
		{
			[NativeMethod("GetNumIndices")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BillboardAsset>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return BillboardAsset.get_indexCount_Injected(intPtr);
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000B10 RID: 2832 RVA: 0x000148A0 File Offset: 0x00012AA0
		// (set) Token: 0x06000B11 RID: 2833 RVA: 0x000148C8 File Offset: 0x00012AC8
		public Material material
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BillboardAsset>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Material>(BillboardAsset.get_material_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BillboardAsset>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BillboardAsset.set_material_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Material>(value));
			}
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x000148F0 File Offset: 0x00012AF0
		public void GetImageTexCoords(List<Vector4> imageTexCoords)
		{
			bool flag = imageTexCoords == null;
			if (flag)
			{
				throw new ArgumentNullException("imageTexCoords");
			}
			this.GetImageTexCoordsInternal(imageTexCoords);
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x0001491C File Offset: 0x00012B1C
		[NativeMethod("GetBillboardDataReadonly().GetImageTexCoords")]
		public Vector4[] GetImageTexCoords()
		{
			Vector4[] array2;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BillboardAsset>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				BillboardAsset.GetImageTexCoords_Injected(intPtr, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				Vector4[] array;
				blittableArrayWrapper.Unmarshal<Vector4>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x00014960 File Offset: 0x00012B60
		[FreeFunction(Name = "BillboardRenderer_Bindings::GetImageTexCoordsInternal", HasExplicitThis = true)]
		internal void GetImageTexCoordsInternal(object list)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BillboardAsset>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			BillboardAsset.GetImageTexCoordsInternal_Injected(intPtr, list);
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x00014984 File Offset: 0x00012B84
		public void SetImageTexCoords(List<Vector4> imageTexCoords)
		{
			bool flag = imageTexCoords == null;
			if (flag)
			{
				throw new ArgumentNullException("imageTexCoords");
			}
			this.SetImageTexCoordsInternalList(imageTexCoords);
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x000149B0 File Offset: 0x00012BB0
		[FreeFunction(Name = "BillboardRenderer_Bindings::SetImageTexCoords", HasExplicitThis = true)]
		public unsafe void SetImageTexCoords([NotNull] Vector4[] imageTexCoords)
		{
			if (imageTexCoords == null)
			{
				ThrowHelper.ThrowArgumentNullException(imageTexCoords, "imageTexCoords");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BillboardAsset>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Vector4> span = new Span<Vector4>(imageTexCoords);
			fixed (Vector4* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				BillboardAsset.SetImageTexCoords_Injected(intPtr, ref managedSpanWrapper);
			}
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x00014A08 File Offset: 0x00012C08
		[FreeFunction(Name = "BillboardRenderer_Bindings::SetImageTexCoordsInternalList", HasExplicitThis = true)]
		internal void SetImageTexCoordsInternalList(object list)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BillboardAsset>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			BillboardAsset.SetImageTexCoordsInternalList_Injected(intPtr, list);
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x00014A2C File Offset: 0x00012C2C
		public void GetVertices(List<Vector2> vertices)
		{
			bool flag = vertices == null;
			if (flag)
			{
				throw new ArgumentNullException("vertices");
			}
			this.GetVerticesInternal(vertices);
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x00014A58 File Offset: 0x00012C58
		[NativeMethod("GetBillboardDataReadonly().GetVertices")]
		public Vector2[] GetVertices()
		{
			Vector2[] array2;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BillboardAsset>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				BillboardAsset.GetVertices_Injected(intPtr, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				Vector2[] array;
				blittableArrayWrapper.Unmarshal<Vector2>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x00014A9C File Offset: 0x00012C9C
		[FreeFunction(Name = "BillboardRenderer_Bindings::GetVerticesInternal", HasExplicitThis = true)]
		internal void GetVerticesInternal(object list)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BillboardAsset>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			BillboardAsset.GetVerticesInternal_Injected(intPtr, list);
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x00014AC0 File Offset: 0x00012CC0
		public void SetVertices(List<Vector2> vertices)
		{
			bool flag = vertices == null;
			if (flag)
			{
				throw new ArgumentNullException("vertices");
			}
			this.SetVerticesInternalList(vertices);
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x00014AEC File Offset: 0x00012CEC
		[FreeFunction(Name = "BillboardRenderer_Bindings::SetVertices", HasExplicitThis = true)]
		public unsafe void SetVertices([NotNull] Vector2[] vertices)
		{
			if (vertices == null)
			{
				ThrowHelper.ThrowArgumentNullException(vertices, "vertices");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BillboardAsset>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Vector2> span = new Span<Vector2>(vertices);
			fixed (Vector2* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				BillboardAsset.SetVertices_Injected(intPtr, ref managedSpanWrapper);
			}
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x00014B44 File Offset: 0x00012D44
		[FreeFunction(Name = "BillboardRenderer_Bindings::SetVerticesInternalList", HasExplicitThis = true)]
		internal void SetVerticesInternalList(object list)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BillboardAsset>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			BillboardAsset.SetVerticesInternalList_Injected(intPtr, list);
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x00014B68 File Offset: 0x00012D68
		public void GetIndices(List<ushort> indices)
		{
			bool flag = indices == null;
			if (flag)
			{
				throw new ArgumentNullException("indices");
			}
			this.GetIndicesInternal(indices);
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x00014B94 File Offset: 0x00012D94
		[NativeMethod("GetBillboardDataReadonly().GetIndices")]
		public ushort[] GetIndices()
		{
			ushort[] array2;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BillboardAsset>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				BillboardAsset.GetIndices_Injected(intPtr, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				ushort[] array;
				blittableArrayWrapper.Unmarshal<ushort>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x00014BD8 File Offset: 0x00012DD8
		[FreeFunction(Name = "BillboardRenderer_Bindings::GetIndicesInternal", HasExplicitThis = true)]
		internal void GetIndicesInternal(object list)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BillboardAsset>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			BillboardAsset.GetIndicesInternal_Injected(intPtr, list);
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x00014BFC File Offset: 0x00012DFC
		public void SetIndices(List<ushort> indices)
		{
			bool flag = indices == null;
			if (flag)
			{
				throw new ArgumentNullException("indices");
			}
			this.SetIndicesInternalList(indices);
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x00014C28 File Offset: 0x00012E28
		[FreeFunction(Name = "BillboardRenderer_Bindings::SetIndices", HasExplicitThis = true)]
		public unsafe void SetIndices([NotNull] ushort[] indices)
		{
			if (indices == null)
			{
				ThrowHelper.ThrowArgumentNullException(indices, "indices");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BillboardAsset>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<ushort> span = new Span<ushort>(indices);
			fixed (ushort* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				BillboardAsset.SetIndices_Injected(intPtr, ref managedSpanWrapper);
			}
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x00014C80 File Offset: 0x00012E80
		[FreeFunction(Name = "BillboardRenderer_Bindings::SetIndicesInternalList", HasExplicitThis = true)]
		internal void SetIndicesInternalList(object list)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BillboardAsset>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			BillboardAsset.SetIndicesInternalList_Injected(intPtr, list);
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x00014CA4 File Offset: 0x00012EA4
		[FreeFunction(Name = "BillboardRenderer_Bindings::MakeMaterialProperties", HasExplicitThis = true)]
		internal void MakeMaterialProperties(MaterialPropertyBlock properties, Camera camera)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BillboardAsset>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			BillboardAsset.MakeMaterialProperties_Injected(intPtr, (properties == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(properties), Object.MarshalledUnityObject.Marshal<Camera>(camera));
		}

		// Token: 0x06000B25 RID: 2853
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_width_Injected(IntPtr _unity_self);

		// Token: 0x06000B26 RID: 2854
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_width_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000B27 RID: 2855
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_height_Injected(IntPtr _unity_self);

		// Token: 0x06000B28 RID: 2856
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_height_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000B29 RID: 2857
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_bottom_Injected(IntPtr _unity_self);

		// Token: 0x06000B2A RID: 2858
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_bottom_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000B2B RID: 2859
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_imageCount_Injected(IntPtr _unity_self);

		// Token: 0x06000B2C RID: 2860
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_vertexCount_Injected(IntPtr _unity_self);

		// Token: 0x06000B2D RID: 2861
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_indexCount_Injected(IntPtr _unity_self);

		// Token: 0x06000B2E RID: 2862
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_material_Injected(IntPtr _unity_self);

		// Token: 0x06000B2F RID: 2863
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_material_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x06000B30 RID: 2864
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetImageTexCoords_Injected(IntPtr _unity_self, out BlittableArrayWrapper ret);

		// Token: 0x06000B31 RID: 2865
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetImageTexCoordsInternal_Injected(IntPtr _unity_self, object list);

		// Token: 0x06000B32 RID: 2866
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetImageTexCoords_Injected(IntPtr _unity_self, ref ManagedSpanWrapper imageTexCoords);

		// Token: 0x06000B33 RID: 2867
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetImageTexCoordsInternalList_Injected(IntPtr _unity_self, object list);

		// Token: 0x06000B34 RID: 2868
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetVertices_Injected(IntPtr _unity_self, out BlittableArrayWrapper ret);

		// Token: 0x06000B35 RID: 2869
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetVerticesInternal_Injected(IntPtr _unity_self, object list);

		// Token: 0x06000B36 RID: 2870
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetVertices_Injected(IntPtr _unity_self, ref ManagedSpanWrapper vertices);

		// Token: 0x06000B37 RID: 2871
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetVerticesInternalList_Injected(IntPtr _unity_self, object list);

		// Token: 0x06000B38 RID: 2872
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetIndices_Injected(IntPtr _unity_self, out BlittableArrayWrapper ret);

		// Token: 0x06000B39 RID: 2873
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetIndicesInternal_Injected(IntPtr _unity_self, object list);

		// Token: 0x06000B3A RID: 2874
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetIndices_Injected(IntPtr _unity_self, ref ManagedSpanWrapper indices);

		// Token: 0x06000B3B RID: 2875
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetIndicesInternalList_Injected(IntPtr _unity_self, object list);

		// Token: 0x06000B3C RID: 2876
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MakeMaterialProperties_Injected(IntPtr _unity_self, IntPtr properties, IntPtr camera);
	}
}
