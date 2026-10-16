using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020002F5 RID: 757
	[NativeHeader("Modules/UIElements/Core/Native/Renderer/UIRMeshBuilder.bindings.h")]
	internal static class MeshBuilderNative
	{
		// Token: 0x06001588 RID: 5512 RVA: 0x00055A00 File Offset: 0x00053C00
		[ThreadSafe]
		public static MeshWriteDataInterface MakeBorder(MeshBuilderNative.NativeBorderParams borderParams, float posZ)
		{
			MeshWriteDataInterface meshWriteDataInterface;
			MeshBuilderNative.MakeBorder_Injected(ref borderParams, posZ, out meshWriteDataInterface);
			return meshWriteDataInterface;
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x00055A18 File Offset: 0x00053C18
		[ThreadSafe]
		public static MeshWriteDataInterface MakeSolidRect(MeshBuilderNative.NativeRectParams rectParams, float posZ)
		{
			MeshWriteDataInterface meshWriteDataInterface;
			MeshBuilderNative.MakeSolidRect_Injected(ref rectParams, posZ, out meshWriteDataInterface);
			return meshWriteDataInterface;
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x00055A30 File Offset: 0x00053C30
		[ThreadSafe]
		public static MeshWriteDataInterface MakeTexturedRect(MeshBuilderNative.NativeRectParams rectParams, float posZ)
		{
			MeshWriteDataInterface meshWriteDataInterface;
			MeshBuilderNative.MakeTexturedRect_Injected(ref rectParams, posZ, out meshWriteDataInterface);
			return meshWriteDataInterface;
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x00055A48 File Offset: 0x00053C48
		[ThreadSafe]
		public unsafe static MeshWriteDataInterface MakeVectorGraphicsStretchBackground(Vertex[] svgVertices, ushort[] svgIndices, float svgWidth, float svgHeight, Rect targetRect, Rect sourceUV, ScaleMode scaleMode, Color tint, MeshBuilderNative.NativeColorPage colorPage)
		{
			Span<Vertex> span = new Span<Vertex>(svgVertices);
			fixed (Vertex* ptr = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, span.Length);
				Span<ushort> span2 = new Span<ushort>(svgIndices);
				MeshWriteDataInterface meshWriteDataInterface;
				fixed (ushort* pinnableReference = span2.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper2 = new ManagedSpanWrapper((void*)pinnableReference, span2.Length);
					MeshBuilderNative.MakeVectorGraphicsStretchBackground_Injected(ref managedSpanWrapper, ref managedSpanWrapper2, svgWidth, svgHeight, ref targetRect, ref sourceUV, scaleMode, ref tint, ref colorPage, out meshWriteDataInterface);
					ptr = null;
				}
				return meshWriteDataInterface;
			}
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x00055ABC File Offset: 0x00053CBC
		[ThreadSafe]
		public unsafe static MeshWriteDataInterface MakeVectorGraphics9SliceBackground(Vertex[] svgVertices, ushort[] svgIndices, float svgWidth, float svgHeight, Rect targetRect, Vector4 sliceLTRB, Color tint, MeshBuilderNative.NativeColorPage colorPage)
		{
			Span<Vertex> span = new Span<Vertex>(svgVertices);
			fixed (Vertex* ptr = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, span.Length);
				Span<ushort> span2 = new Span<ushort>(svgIndices);
				MeshWriteDataInterface meshWriteDataInterface;
				fixed (ushort* pinnableReference = span2.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper2 = new ManagedSpanWrapper((void*)pinnableReference, span2.Length);
					MeshBuilderNative.MakeVectorGraphics9SliceBackground_Injected(ref managedSpanWrapper, ref managedSpanWrapper2, svgWidth, svgHeight, ref targetRect, ref sliceLTRB, ref tint, ref colorPage, out meshWriteDataInterface);
					ptr = null;
				}
				return meshWriteDataInterface;
			}
		}

		// Token: 0x0600158D RID: 5517
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MakeBorder_Injected([In] ref MeshBuilderNative.NativeBorderParams borderParams, float posZ, out MeshWriteDataInterface ret);

		// Token: 0x0600158E RID: 5518
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MakeSolidRect_Injected([In] ref MeshBuilderNative.NativeRectParams rectParams, float posZ, out MeshWriteDataInterface ret);

		// Token: 0x0600158F RID: 5519
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MakeTexturedRect_Injected([In] ref MeshBuilderNative.NativeRectParams rectParams, float posZ, out MeshWriteDataInterface ret);

		// Token: 0x06001590 RID: 5520
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MakeVectorGraphicsStretchBackground_Injected(ref ManagedSpanWrapper svgVertices, ref ManagedSpanWrapper svgIndices, float svgWidth, float svgHeight, [In] ref Rect targetRect, [In] ref Rect sourceUV, ScaleMode scaleMode, [In] ref Color tint, [In] ref MeshBuilderNative.NativeColorPage colorPage, out MeshWriteDataInterface ret);

		// Token: 0x06001591 RID: 5521
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MakeVectorGraphics9SliceBackground_Injected(ref ManagedSpanWrapper svgVertices, ref ManagedSpanWrapper svgIndices, float svgWidth, float svgHeight, [In] ref Rect targetRect, [In] ref Vector4 sliceLTRB, [In] ref Color tint, [In] ref MeshBuilderNative.NativeColorPage colorPage, out MeshWriteDataInterface ret);

		// Token: 0x04000B9F RID: 2975
		public const float kEpsilon = 0.001f;

		// Token: 0x020002F6 RID: 758
		public struct NativeColorPage
		{
			// Token: 0x04000BA0 RID: 2976
			public int isValid;

			// Token: 0x04000BA1 RID: 2977
			public Color32 pageAndID;
		}

		// Token: 0x020002F7 RID: 759
		public struct NativeBorderParams
		{
			// Token: 0x04000BA2 RID: 2978
			public Rect rect;

			// Token: 0x04000BA3 RID: 2979
			public Color leftColor;

			// Token: 0x04000BA4 RID: 2980
			public Color topColor;

			// Token: 0x04000BA5 RID: 2981
			public Color rightColor;

			// Token: 0x04000BA6 RID: 2982
			public Color bottomColor;

			// Token: 0x04000BA7 RID: 2983
			public float leftWidth;

			// Token: 0x04000BA8 RID: 2984
			public float topWidth;

			// Token: 0x04000BA9 RID: 2985
			public float rightWidth;

			// Token: 0x04000BAA RID: 2986
			public float bottomWidth;

			// Token: 0x04000BAB RID: 2987
			public Vector2 topLeftRadius;

			// Token: 0x04000BAC RID: 2988
			public Vector2 topRightRadius;

			// Token: 0x04000BAD RID: 2989
			public Vector2 bottomRightRadius;

			// Token: 0x04000BAE RID: 2990
			public Vector2 bottomLeftRadius;

			// Token: 0x04000BAF RID: 2991
			internal MeshBuilderNative.NativeColorPage leftColorPage;

			// Token: 0x04000BB0 RID: 2992
			internal MeshBuilderNative.NativeColorPage topColorPage;

			// Token: 0x04000BB1 RID: 2993
			internal MeshBuilderNative.NativeColorPage rightColorPage;

			// Token: 0x04000BB2 RID: 2994
			internal MeshBuilderNative.NativeColorPage bottomColorPage;
		}

		// Token: 0x020002F8 RID: 760
		public struct NativeRectParams
		{
			// Token: 0x04000BB3 RID: 2995
			public Rect rect;

			// Token: 0x04000BB4 RID: 2996
			public Rect subRect;

			// Token: 0x04000BB5 RID: 2997
			public Rect uv;

			// Token: 0x04000BB6 RID: 2998
			public Color color;

			// Token: 0x04000BB7 RID: 2999
			public ScaleMode scaleMode;

			// Token: 0x04000BB8 RID: 3000
			public IntPtr backgroundRepeatInstanceList;

			// Token: 0x04000BB9 RID: 3001
			public int backgroundRepeatInstanceListStartIndex;

			// Token: 0x04000BBA RID: 3002
			public int backgroundRepeatInstanceListEndIndex;

			// Token: 0x04000BBB RID: 3003
			public Vector2 topLeftRadius;

			// Token: 0x04000BBC RID: 3004
			public Vector2 topRightRadius;

			// Token: 0x04000BBD RID: 3005
			public Vector2 bottomRightRadius;

			// Token: 0x04000BBE RID: 3006
			public Vector2 bottomLeftRadius;

			// Token: 0x04000BBF RID: 3007
			public Rect backgroundRepeatRect;

			// Token: 0x04000BC0 RID: 3008
			public IntPtr texture;

			// Token: 0x04000BC1 RID: 3009
			public IntPtr sprite;

			// Token: 0x04000BC2 RID: 3010
			public IntPtr vectorImage;

			// Token: 0x04000BC3 RID: 3011
			public IntPtr spriteTexture;

			// Token: 0x04000BC4 RID: 3012
			public IntPtr spriteVertices;

			// Token: 0x04000BC5 RID: 3013
			public IntPtr spriteUVs;

			// Token: 0x04000BC6 RID: 3014
			public IntPtr spriteTriangles;

			// Token: 0x04000BC7 RID: 3015
			public Rect spriteGeomRect;

			// Token: 0x04000BC8 RID: 3016
			public Vector2 contentSize;

			// Token: 0x04000BC9 RID: 3017
			public Vector2 textureSize;

			// Token: 0x04000BCA RID: 3018
			public float texturePixelsPerPoint;

			// Token: 0x04000BCB RID: 3019
			public int leftSlice;

			// Token: 0x04000BCC RID: 3020
			public int topSlice;

			// Token: 0x04000BCD RID: 3021
			public int rightSlice;

			// Token: 0x04000BCE RID: 3022
			public int bottomSlice;

			// Token: 0x04000BCF RID: 3023
			public float sliceScale;

			// Token: 0x04000BD0 RID: 3024
			public Vector4 rectInset;

			// Token: 0x04000BD1 RID: 3025
			public MeshBuilderNative.NativeColorPage colorPage;

			// Token: 0x04000BD2 RID: 3026
			public int meshFlags;
		}
	}
}
