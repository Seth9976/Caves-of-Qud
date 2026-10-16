using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000004 RID: 4
	[NativeClass("UI::CanvasRenderer")]
	[NativeHeader("Modules/UI/CanvasRenderer.h")]
	public sealed class CanvasRenderer : Component
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002194 File Offset: 0x00000394
		// (set) Token: 0x06000015 RID: 21 RVA: 0x000021B8 File Offset: 0x000003B8
		public bool hasPopInstruction
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CanvasRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CanvasRenderer.get_hasPopInstruction_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CanvasRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CanvasRenderer.set_hasPopInstruction_Injected(intPtr, value);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000021DC File Offset: 0x000003DC
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002200 File Offset: 0x00000400
		public int materialCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CanvasRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CanvasRenderer.get_materialCount_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CanvasRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CanvasRenderer.set_materialCount_Injected(intPtr, value);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002224 File Offset: 0x00000424
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002248 File Offset: 0x00000448
		public int popMaterialCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CanvasRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CanvasRenderer.get_popMaterialCount_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CanvasRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CanvasRenderer.set_popMaterialCount_Injected(intPtr, value);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000226C File Offset: 0x0000046C
		public int absoluteDepth
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CanvasRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CanvasRenderer.get_absoluteDepth_Injected(intPtr);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002290 File Offset: 0x00000490
		public bool hasMoved
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CanvasRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CanvasRenderer.get_hasMoved_Injected(intPtr);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000022B4 File Offset: 0x000004B4
		// (set) Token: 0x0600001D RID: 29 RVA: 0x000022D8 File Offset: 0x000004D8
		public bool cullTransparentMesh
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CanvasRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CanvasRenderer.get_cullTransparentMesh_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CanvasRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CanvasRenderer.set_cullTransparentMesh_Injected(intPtr, value);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000022FC File Offset: 0x000004FC
		[NativeProperty("RectClipping", false, TargetType.Function)]
		public bool hasRectClipping
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CanvasRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CanvasRenderer.get_hasRectClipping_Injected(intPtr);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002320 File Offset: 0x00000520
		[NativeProperty("Depth", false, TargetType.Function)]
		public int relativeDepth
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CanvasRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CanvasRenderer.get_relativeDepth_Injected(intPtr);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002344 File Offset: 0x00000544
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002368 File Offset: 0x00000568
		[NativeProperty("ShouldCull", false, TargetType.Function)]
		public bool cull
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CanvasRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CanvasRenderer.get_cull_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CanvasRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CanvasRenderer.set_cull_Injected(intPtr, value);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000022 RID: 34 RVA: 0x0000238B File Offset: 0x0000058B
		// (set) Token: 0x06000023 RID: 35 RVA: 0x00002393 File Offset: 0x00000593
		[Obsolete("isMask is no longer supported.See EnableClipping for vertex clipping configuration", false)]
		public bool isMask { get; set; }

		// Token: 0x06000024 RID: 36 RVA: 0x0000239C File Offset: 0x0000059C
		public void SetColor(Color color)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CanvasRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CanvasRenderer.SetColor_Injected(intPtr, ref color);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000023C0 File Offset: 0x000005C0
		public Color GetColor()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CanvasRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Color color;
			CanvasRenderer.GetColor_Injected(intPtr, out color);
			return color;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000023E8 File Offset: 0x000005E8
		public void EnableRectClipping(Rect rect)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CanvasRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CanvasRenderer.EnableRectClipping_Injected(intPtr, ref rect);
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000240C File Offset: 0x0000060C
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002434 File Offset: 0x00000634
		public Vector2 clippingSoftness
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CanvasRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				CanvasRenderer.get_clippingSoftness_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CanvasRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CanvasRenderer.set_clippingSoftness_Injected(intPtr, ref value);
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002458 File Offset: 0x00000658
		public void DisableRectClipping()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CanvasRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CanvasRenderer.DisableRectClipping_Injected(intPtr);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000247C File Offset: 0x0000067C
		public void SetMaterial(Material material, int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CanvasRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CanvasRenderer.SetMaterial_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Material>(material), index);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000024A8 File Offset: 0x000006A8
		public Material GetMaterial(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CanvasRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Material>(CanvasRenderer.GetMaterial_Injected(intPtr, index));
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000024D0 File Offset: 0x000006D0
		public void SetPopMaterial(Material material, int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CanvasRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CanvasRenderer.SetPopMaterial_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Material>(material), index);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000024FC File Offset: 0x000006FC
		public Material GetPopMaterial(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CanvasRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Material>(CanvasRenderer.GetPopMaterial_Injected(intPtr, index));
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002524 File Offset: 0x00000724
		public void SetTexture(Texture texture)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CanvasRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CanvasRenderer.SetTexture_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Texture>(texture));
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000254C File Offset: 0x0000074C
		public void SetAlphaTexture(Texture texture)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CanvasRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CanvasRenderer.SetAlphaTexture_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Texture>(texture));
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002574 File Offset: 0x00000774
		public void SetMesh(Mesh mesh)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CanvasRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CanvasRenderer.SetMesh_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Mesh>(mesh));
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000259C File Offset: 0x0000079C
		public Mesh GetMesh()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CanvasRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Mesh>(CanvasRenderer.GetMesh_Injected(intPtr));
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000025C4 File Offset: 0x000007C4
		public void Clear()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CanvasRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CanvasRenderer.Clear_Injected(intPtr);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000025E8 File Offset: 0x000007E8
		public float GetAlpha()
		{
			return this.GetColor().a;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002608 File Offset: 0x00000808
		public void SetAlpha(float alpha)
		{
			Color color = this.GetColor();
			color.a = alpha;
			this.SetColor(color);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002630 File Offset: 0x00000830
		public float GetInheritedAlpha()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CanvasRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return CanvasRenderer.GetInheritedAlpha_Injected(intPtr);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002652 File Offset: 0x00000852
		public void SetMaterial(Material material, Texture texture)
		{
			this.materialCount = Math.Max(1, this.materialCount);
			this.SetMaterial(material, 0);
			this.SetTexture(texture);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000267C File Offset: 0x0000087C
		public Material GetMaterial()
		{
			return this.GetMaterial(0);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002698 File Offset: 0x00000898
		public static void SplitUIVertexStreams(List<UIVertex> verts, List<Vector3> positions, List<Color32> colors, List<Vector4> uv0S, List<Vector4> uv1S, List<Vector3> normals, List<Vector4> tangents, List<int> indices)
		{
			CanvasRenderer.SplitUIVertexStreams(verts, positions, colors, uv0S, uv1S, new List<Vector4>(), new List<Vector4>(), normals, tangents, indices);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000026C4 File Offset: 0x000008C4
		public static void SplitUIVertexStreams(List<UIVertex> verts, List<Vector3> positions, List<Color32> colors, List<Vector4> uv0S, List<Vector4> uv1S, List<Vector4> uv2S, List<Vector4> uv3S, List<Vector3> normals, List<Vector4> tangents, List<int> indices)
		{
			CanvasRenderer.SplitUIVertexStreamsInternal(verts, positions, colors, uv0S, uv1S, uv2S, uv3S, normals, tangents);
			CanvasRenderer.SplitIndicesStreamsInternal(verts, indices);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000026F0 File Offset: 0x000008F0
		public static void CreateUIVertexStream(List<UIVertex> verts, List<Vector3> positions, List<Color32> colors, List<Vector4> uv0S, List<Vector4> uv1S, List<Vector3> normals, List<Vector4> tangents, List<int> indices)
		{
			CanvasRenderer.CreateUIVertexStream(verts, positions, colors, uv0S, uv1S, new List<Vector4>(), new List<Vector4>(), normals, tangents, indices);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000271C File Offset: 0x0000091C
		public static void CreateUIVertexStream(List<UIVertex> verts, List<Vector3> positions, List<Color32> colors, List<Vector4> uv0S, List<Vector4> uv1S, List<Vector4> uv2S, List<Vector4> uv3S, List<Vector3> normals, List<Vector4> tangents, List<int> indices)
		{
			CanvasRenderer.CreateUIVertexStreamInternal(verts, positions, colors, uv0S, uv1S, uv2S, uv3S, normals, tangents, indices);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002740 File Offset: 0x00000940
		public static void AddUIVertexStream(List<UIVertex> verts, List<Vector3> positions, List<Color32> colors, List<Vector4> uv0S, List<Vector4> uv1S, List<Vector3> normals, List<Vector4> tangents)
		{
			CanvasRenderer.AddUIVertexStream(verts, positions, colors, uv0S, uv1S, new List<Vector4>(), new List<Vector4>(), normals, tangents);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002768 File Offset: 0x00000968
		public static void AddUIVertexStream(List<UIVertex> verts, List<Vector3> positions, List<Color32> colors, List<Vector4> uv0S, List<Vector4> uv1S, List<Vector4> uv2S, List<Vector4> uv3S, List<Vector3> normals, List<Vector4> tangents)
		{
			CanvasRenderer.SplitUIVertexStreamsInternal(verts, positions, colors, uv0S, uv1S, uv2S, uv3S, normals, tangents);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000278A File Offset: 0x0000098A
		[Obsolete("UI System now uses meshes.Generate a mesh and use 'SetMesh' instead", false)]
		public void SetVertices(List<UIVertex> vertices)
		{
			this.SetVertices(vertices.ToArray(), vertices.Count);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000027A0 File Offset: 0x000009A0
		[Obsolete("UI System now uses meshes.Generate a mesh and use 'SetMesh' instead", false)]
		public void SetVertices(UIVertex[] vertices, int size)
		{
			Mesh mesh = new Mesh();
			List<Vector3> list = new List<Vector3>();
			List<Color32> list2 = new List<Color32>();
			List<Vector4> list3 = new List<Vector4>();
			List<Vector4> list4 = new List<Vector4>();
			List<Vector4> list5 = new List<Vector4>();
			List<Vector4> list6 = new List<Vector4>();
			List<Vector3> list7 = new List<Vector3>();
			List<Vector4> list8 = new List<Vector4>();
			List<int> list9 = new List<int>();
			for (int i = 0; i < size; i += 4)
			{
				for (int j = 0; j < 4; j++)
				{
					list.Add(vertices[i + j].position);
					list2.Add(vertices[i + j].color);
					list3.Add(vertices[i + j].uv0);
					list4.Add(vertices[i + j].uv1);
					list5.Add(vertices[i + j].uv2);
					list6.Add(vertices[i + j].uv3);
					list7.Add(vertices[i + j].normal);
					list8.Add(vertices[i + j].tangent);
				}
				list9.Add(i);
				list9.Add(i + 1);
				list9.Add(i + 2);
				list9.Add(i + 2);
				list9.Add(i + 3);
				list9.Add(i);
			}
			mesh.SetVertices(list);
			mesh.SetColors(list2);
			mesh.SetNormals(list7);
			mesh.SetTangents(list8);
			mesh.SetUVs(0, list3);
			mesh.SetUVs(1, list4);
			mesh.SetUVs(2, list5);
			mesh.SetUVs(3, list6);
			mesh.SetIndices(list9.ToArray(), MeshTopology.Triangles, 0);
			this.SetMesh(mesh);
			Object.DestroyImmediate(mesh);
		}

		// Token: 0x06000040 RID: 64
		[StaticAccessor("UI", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SplitIndicesStreamsInternal(object verts, object indices);

		// Token: 0x06000041 RID: 65
		[StaticAccessor("UI", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SplitUIVertexStreamsInternal(object verts, object positions, object colors, object uv0S, object uv1S, object uv2S, object uv3S, object normals, object tangents);

		// Token: 0x06000042 RID: 66
		[StaticAccessor("UI", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateUIVertexStreamInternal(object verts, object positions, object colors, object uv0S, object uv1S, object uv2S, object uv3S, object normals, object tangents, object indices);

		// Token: 0x06000044 RID: 68
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_hasPopInstruction_Injected(IntPtr _unity_self);

		// Token: 0x06000045 RID: 69
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_hasPopInstruction_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000046 RID: 70
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_materialCount_Injected(IntPtr _unity_self);

		// Token: 0x06000047 RID: 71
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_materialCount_Injected(IntPtr _unity_self, int value);

		// Token: 0x06000048 RID: 72
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_popMaterialCount_Injected(IntPtr _unity_self);

		// Token: 0x06000049 RID: 73
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_popMaterialCount_Injected(IntPtr _unity_self, int value);

		// Token: 0x0600004A RID: 74
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_absoluteDepth_Injected(IntPtr _unity_self);

		// Token: 0x0600004B RID: 75
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_hasMoved_Injected(IntPtr _unity_self);

		// Token: 0x0600004C RID: 76
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_cullTransparentMesh_Injected(IntPtr _unity_self);

		// Token: 0x0600004D RID: 77
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_cullTransparentMesh_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600004E RID: 78
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_hasRectClipping_Injected(IntPtr _unity_self);

		// Token: 0x0600004F RID: 79
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_relativeDepth_Injected(IntPtr _unity_self);

		// Token: 0x06000050 RID: 80
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_cull_Injected(IntPtr _unity_self);

		// Token: 0x06000051 RID: 81
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_cull_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000052 RID: 82
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetColor_Injected(IntPtr _unity_self, [In] ref Color color);

		// Token: 0x06000053 RID: 83
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetColor_Injected(IntPtr _unity_self, out Color ret);

		// Token: 0x06000054 RID: 84
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EnableRectClipping_Injected(IntPtr _unity_self, [In] ref Rect rect);

		// Token: 0x06000055 RID: 85
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_clippingSoftness_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x06000056 RID: 86
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_clippingSoftness_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x06000057 RID: 87
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DisableRectClipping_Injected(IntPtr _unity_self);

		// Token: 0x06000058 RID: 88
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMaterial_Injected(IntPtr _unity_self, IntPtr material, int index);

		// Token: 0x06000059 RID: 89
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetMaterial_Injected(IntPtr _unity_self, int index);

		// Token: 0x0600005A RID: 90
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPopMaterial_Injected(IntPtr _unity_self, IntPtr material, int index);

		// Token: 0x0600005B RID: 91
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetPopMaterial_Injected(IntPtr _unity_self, int index);

		// Token: 0x0600005C RID: 92
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTexture_Injected(IntPtr _unity_self, IntPtr texture);

		// Token: 0x0600005D RID: 93
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetAlphaTexture_Injected(IntPtr _unity_self, IntPtr texture);

		// Token: 0x0600005E RID: 94
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMesh_Injected(IntPtr _unity_self, IntPtr mesh);

		// Token: 0x0600005F RID: 95
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetMesh_Injected(IntPtr _unity_self);

		// Token: 0x06000060 RID: 96
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Clear_Injected(IntPtr _unity_self);

		// Token: 0x06000061 RID: 97
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetInheritedAlpha_Injected(IntPtr _unity_self);
	}
}
