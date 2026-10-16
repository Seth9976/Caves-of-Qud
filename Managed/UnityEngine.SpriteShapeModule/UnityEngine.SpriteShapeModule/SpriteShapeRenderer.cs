using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.U2D
{
	// Token: 0x02000005 RID: 5
	[MovedFrom("UnityEngine.Experimental.U2D")]
	[NativeType(Header = "Modules/SpriteShape/Public/SpriteShapeRenderer.h")]
	public class SpriteShapeRenderer : Renderer
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020E0 File Offset: 0x000002E0
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002108 File Offset: 0x00000308
		public Color color
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteShapeRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Color color;
				SpriteShapeRenderer.get_color_Injected(intPtr, out color);
				return color;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteShapeRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SpriteShapeRenderer.set_color_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000212C File Offset: 0x0000032C
		// (set) Token: 0x0600000C RID: 12 RVA: 0x00002150 File Offset: 0x00000350
		public SpriteMaskInteraction maskInteraction
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteShapeRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SpriteShapeRenderer.get_maskInteraction_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteShapeRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SpriteShapeRenderer.set_maskInteraction_Injected(intPtr, value);
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002174 File Offset: 0x00000374
		public void Prepare(JobHandle handle, SpriteShapeParameters shapeParams, Sprite[] sprites)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteShapeRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			SpriteShapeRenderer.Prepare_Injected(intPtr, ref handle, ref shapeParams, sprites);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000219C File Offset: 0x0000039C
		private NativeArray<T> GetNativeDataArray<T>(SpriteShapeDataType dataType) where T : struct
		{
			SpriteChannelInfo dataInfo = this.GetDataInfo(dataType);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>(dataInfo.buffer, dataInfo.count, Allocator.Invalid);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021CC File Offset: 0x000003CC
		private unsafe NativeSlice<T> GetChannelDataArray<T>(SpriteShapeDataType dataType, VertexAttribute channel) where T : struct
		{
			SpriteChannelInfo channelInfo = this.GetChannelInfo(channel);
			byte* ptr = (byte*)channelInfo.buffer + channelInfo.offset;
			return NativeSliceUnsafeUtility.ConvertExistingDataToNativeSlice<T>((void*)ptr, channelInfo.stride, channelInfo.count);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000220C File Offset: 0x0000040C
		private void SetSegmentCount(int geomCount)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteShapeRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			SpriteShapeRenderer.SetSegmentCount_Injected(intPtr, geomCount);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002230 File Offset: 0x00000430
		private void SetMeshDataCount(int vertexCount, int indexCount)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteShapeRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			SpriteShapeRenderer.SetMeshDataCount_Injected(intPtr, vertexCount, indexCount);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002254 File Offset: 0x00000454
		private void SetMeshChannelInfo(int vertexCount, int indexCount, int hotChannelMask)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteShapeRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			SpriteShapeRenderer.SetMeshChannelInfo_Injected(intPtr, vertexCount, indexCount, hotChannelMask);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000227C File Offset: 0x0000047C
		private SpriteChannelInfo GetDataInfo(SpriteShapeDataType arrayType)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteShapeRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			SpriteChannelInfo spriteChannelInfo;
			SpriteShapeRenderer.GetDataInfo_Injected(intPtr, arrayType, out spriteChannelInfo);
			return spriteChannelInfo;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022A4 File Offset: 0x000004A4
		private SpriteChannelInfo GetChannelInfo(VertexAttribute channel)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteShapeRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			SpriteChannelInfo spriteChannelInfo;
			SpriteShapeRenderer.GetChannelInfo_Injected(intPtr, channel, out spriteChannelInfo);
			return spriteChannelInfo;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000022CC File Offset: 0x000004CC
		public void SetLocalAABB(Bounds bounds)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteShapeRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			SpriteShapeRenderer.SetLocalAABB_Injected(intPtr, ref bounds);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022F0 File Offset: 0x000004F0
		public int GetSplineMeshCount()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteShapeRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return SpriteShapeRenderer.GetSplineMeshCount_Injected(intPtr);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002314 File Offset: 0x00000514
		public NativeArray<Bounds> GetBounds()
		{
			return this.GetNativeDataArray<Bounds>(SpriteShapeDataType.BoundingBox);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002330 File Offset: 0x00000530
		public NativeArray<SpriteShapeSegment> GetSegments(int dataSize)
		{
			this.SetSegmentCount(dataSize);
			return this.GetNativeDataArray<SpriteShapeSegment>(SpriteShapeDataType.Segment);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002351 File Offset: 0x00000551
		public void GetChannels(int dataSize, out NativeArray<ushort> indices, out NativeSlice<Vector3> vertices, out NativeSlice<Vector2> texcoords)
		{
			this.SetMeshDataCount(dataSize, dataSize);
			indices = this.GetNativeDataArray<ushort>(SpriteShapeDataType.Index);
			vertices = this.GetChannelDataArray<Vector3>(SpriteShapeDataType.ChannelVertex, VertexAttribute.Position);
			texcoords = this.GetChannelDataArray<Vector2>(SpriteShapeDataType.ChannelTexCoord0, VertexAttribute.TexCoord0);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002388 File Offset: 0x00000588
		public void GetChannels(int dataSize, out NativeArray<ushort> indices, out NativeSlice<Vector3> vertices, out NativeSlice<Vector2> texcoords, out NativeSlice<Color32> colors)
		{
			this.SetMeshChannelInfo(dataSize, dataSize, 8);
			indices = this.GetNativeDataArray<ushort>(SpriteShapeDataType.Index);
			vertices = this.GetChannelDataArray<Vector3>(SpriteShapeDataType.ChannelVertex, VertexAttribute.Position);
			texcoords = this.GetChannelDataArray<Vector2>(SpriteShapeDataType.ChannelTexCoord0, VertexAttribute.TexCoord0);
			colors = this.GetChannelDataArray<Color32>(SpriteShapeDataType.ChannelColor, VertexAttribute.Color);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000023DC File Offset: 0x000005DC
		public void GetChannels(int dataSize, out NativeArray<ushort> indices, out NativeSlice<Vector3> vertices, out NativeSlice<Vector2> texcoords, out NativeSlice<Vector4> tangents)
		{
			this.SetMeshChannelInfo(dataSize, dataSize, 4);
			indices = this.GetNativeDataArray<ushort>(SpriteShapeDataType.Index);
			vertices = this.GetChannelDataArray<Vector3>(SpriteShapeDataType.ChannelVertex, VertexAttribute.Position);
			texcoords = this.GetChannelDataArray<Vector2>(SpriteShapeDataType.ChannelTexCoord0, VertexAttribute.TexCoord0);
			tangents = this.GetChannelDataArray<Vector4>(SpriteShapeDataType.ChannelTangent, VertexAttribute.Tangent);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002430 File Offset: 0x00000630
		public void GetChannels(int dataSize, out NativeArray<ushort> indices, out NativeSlice<Vector3> vertices, out NativeSlice<Vector2> texcoords, out NativeSlice<Color32> colors, out NativeSlice<Vector4> tangents)
		{
			this.SetMeshChannelInfo(dataSize, dataSize, 12);
			indices = this.GetNativeDataArray<ushort>(SpriteShapeDataType.Index);
			vertices = this.GetChannelDataArray<Vector3>(SpriteShapeDataType.ChannelVertex, VertexAttribute.Position);
			texcoords = this.GetChannelDataArray<Vector2>(SpriteShapeDataType.ChannelTexCoord0, VertexAttribute.TexCoord0);
			colors = this.GetChannelDataArray<Color32>(SpriteShapeDataType.ChannelColor, VertexAttribute.Color);
			tangents = this.GetChannelDataArray<Vector4>(SpriteShapeDataType.ChannelTangent, VertexAttribute.Tangent);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002494 File Offset: 0x00000694
		public void GetChannels(int dataSize, out NativeArray<ushort> indices, out NativeSlice<Vector3> vertices, out NativeSlice<Vector2> texcoords, out NativeSlice<Vector4> tangents, out NativeSlice<Vector3> normals)
		{
			this.SetMeshChannelInfo(dataSize, dataSize, 6);
			indices = this.GetNativeDataArray<ushort>(SpriteShapeDataType.Index);
			vertices = this.GetChannelDataArray<Vector3>(SpriteShapeDataType.ChannelVertex, VertexAttribute.Position);
			texcoords = this.GetChannelDataArray<Vector2>(SpriteShapeDataType.ChannelTexCoord0, VertexAttribute.TexCoord0);
			tangents = this.GetChannelDataArray<Vector4>(SpriteShapeDataType.ChannelTangent, VertexAttribute.Tangent);
			normals = this.GetChannelDataArray<Vector3>(SpriteShapeDataType.ChannelNormal, VertexAttribute.Normal);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000024F4 File Offset: 0x000006F4
		public void GetChannels(int dataSize, out NativeArray<ushort> indices, out NativeSlice<Vector3> vertices, out NativeSlice<Vector2> texcoords, out NativeSlice<Color32> colors, out NativeSlice<Vector4> tangents, out NativeSlice<Vector3> normals)
		{
			this.SetMeshChannelInfo(dataSize, dataSize, 14);
			indices = this.GetNativeDataArray<ushort>(SpriteShapeDataType.Index);
			vertices = this.GetChannelDataArray<Vector3>(SpriteShapeDataType.ChannelVertex, VertexAttribute.Position);
			texcoords = this.GetChannelDataArray<Vector2>(SpriteShapeDataType.ChannelTexCoord0, VertexAttribute.TexCoord0);
			colors = this.GetChannelDataArray<Color32>(SpriteShapeDataType.ChannelColor, VertexAttribute.Color);
			tangents = this.GetChannelDataArray<Vector4>(SpriteShapeDataType.ChannelTangent, VertexAttribute.Tangent);
			normals = this.GetChannelDataArray<Vector3>(SpriteShapeDataType.ChannelNormal, VertexAttribute.Normal);
		}

		// Token: 0x06000020 RID: 32
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_color_Injected(IntPtr _unity_self, out Color ret);

		// Token: 0x06000021 RID: 33
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_color_Injected(IntPtr _unity_self, [In] ref Color value);

		// Token: 0x06000022 RID: 34
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern SpriteMaskInteraction get_maskInteraction_Injected(IntPtr _unity_self);

		// Token: 0x06000023 RID: 35
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_maskInteraction_Injected(IntPtr _unity_self, SpriteMaskInteraction value);

		// Token: 0x06000024 RID: 36
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Prepare_Injected(IntPtr _unity_self, [In] ref JobHandle handle, [In] ref SpriteShapeParameters shapeParams, Sprite[] sprites);

		// Token: 0x06000025 RID: 37
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSegmentCount_Injected(IntPtr _unity_self, int geomCount);

		// Token: 0x06000026 RID: 38
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMeshDataCount_Injected(IntPtr _unity_self, int vertexCount, int indexCount);

		// Token: 0x06000027 RID: 39
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMeshChannelInfo_Injected(IntPtr _unity_self, int vertexCount, int indexCount, int hotChannelMask);

		// Token: 0x06000028 RID: 40
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetDataInfo_Injected(IntPtr _unity_self, SpriteShapeDataType arrayType, out SpriteChannelInfo ret);

		// Token: 0x06000029 RID: 41
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetChannelInfo_Injected(IntPtr _unity_self, VertexAttribute channel, out SpriteChannelInfo ret);

		// Token: 0x0600002A RID: 42
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLocalAABB_Injected(IntPtr _unity_self, [In] ref Bounds bounds);

		// Token: 0x0600002B RID: 43
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetSplineMeshCount_Injected(IntPtr _unity_self);
	}
}
