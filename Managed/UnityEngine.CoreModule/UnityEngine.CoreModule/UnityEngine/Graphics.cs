using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x0200016C RID: 364
	[NativeHeader("Runtime/Camera/LightProbeProxyVolume.h")]
	[NativeHeader("Runtime/Graphics/ColorGamut.h")]
	[NativeHeader("Runtime/Graphics/CopyTexture.h")]
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	[NativeHeader("Runtime/Shaders/ComputeShader.h")]
	[NativeHeader("Runtime/Misc/PlayerSettings.h")]
	public class Graphics
	{
		// Token: 0x06000BCC RID: 3020
		[FreeFunction("GraphicsScripting::GetMaxDrawMeshInstanceCount", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetMaxDrawMeshInstanceCount();

		// Token: 0x06000BCD RID: 3021
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ColorGamut GetActiveColorGamut();

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x00015B10 File Offset: 0x00013D10
		public static ColorGamut activeColorGamut
		{
			get
			{
				return Graphics.GetActiveColorGamut();
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000BCF RID: 3023
		// (set) Token: 0x06000BD0 RID: 3024
		[StaticAccessor("GetGfxDevice()", StaticAccessorType.Dot)]
		public static extern GraphicsTier activeTier
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06000BD1 RID: 3025
		[NativeMethod(Name = "GetPreserveFramebufferAlpha")]
		[StaticAccessor("GetPlayerSettings()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool GetPreserveFramebufferAlpha();

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x00015B28 File Offset: 0x00013D28
		public static bool preserveFramebufferAlpha
		{
			get
			{
				return Graphics.GetPreserveFramebufferAlpha();
			}
		}

		// Token: 0x06000BD3 RID: 3027
		[StaticAccessor("GetPlayerSettings()", StaticAccessorType.Dot)]
		[NativeMethod(Name = "GetMinOpenGLESVersion")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern OpenGLESVersion GetMinOpenGLESVersion();

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x00015B40 File Offset: 0x00013D40
		public static OpenGLESVersion minOpenGLESVersion
		{
			get
			{
				return Graphics.GetMinOpenGLESVersion();
			}
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x00015B58 File Offset: 0x00013D58
		[FreeFunction("GraphicsScripting::GetActiveColorBuffer")]
		private static RenderBuffer GetActiveColorBuffer()
		{
			RenderBuffer renderBuffer;
			Graphics.GetActiveColorBuffer_Injected(out renderBuffer);
			return renderBuffer;
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x00015B70 File Offset: 0x00013D70
		[FreeFunction("GraphicsScripting::GetActiveDepthBuffer")]
		private static RenderBuffer GetActiveDepthBuffer()
		{
			RenderBuffer renderBuffer;
			Graphics.GetActiveDepthBuffer_Injected(out renderBuffer);
			return renderBuffer;
		}

		// Token: 0x06000BD7 RID: 3031
		[FreeFunction("GraphicsScripting::SetNullRT")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetNullRT();

		// Token: 0x06000BD8 RID: 3032 RVA: 0x00015B88 File Offset: 0x00013D88
		[NativeMethod(Name = "GraphicsScripting::SetGfxRT", IsFreeFunction = true, ThrowsException = true)]
		private static void Internal_SetGfxRT(GraphicsTexture gfxTex, int mip, CubemapFace face, int depthSlice)
		{
			Graphics.Internal_SetGfxRT_Injected((gfxTex == null) ? ((IntPtr)0) : GraphicsTexture.BindingsMarshaller.ConvertToNative(gfxTex), mip, face, depthSlice);
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x00015BAC File Offset: 0x00013DAC
		[NativeMethod(Name = "GraphicsScripting::SetRTSimple", IsFreeFunction = true, ThrowsException = true)]
		private static void Internal_SetRTSimple(RenderBuffer color, RenderBuffer depth, int mip, CubemapFace face, int depthSlice)
		{
			Graphics.Internal_SetRTSimple_Injected(ref color, ref depth, mip, face, depthSlice);
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x00015BC8 File Offset: 0x00013DC8
		[NativeMethod(Name = "GraphicsScripting::SetMRTSimple", IsFreeFunction = true, ThrowsException = true)]
		private unsafe static void Internal_SetMRTSimple([NotNull] RenderBuffer[] color, RenderBuffer depth, int mip, CubemapFace face, int depthSlice)
		{
			if (color == null)
			{
				ThrowHelper.ThrowArgumentNullException(color, "color");
			}
			Span<RenderBuffer> span = new Span<RenderBuffer>(color);
			fixed (RenderBuffer* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				Graphics.Internal_SetMRTSimple_Injected(ref managedSpanWrapper, ref depth, mip, face, depthSlice);
			}
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x00015C18 File Offset: 0x00013E18
		[NativeMethod(Name = "GraphicsScripting::SetMRTFull", IsFreeFunction = true, ThrowsException = true)]
		private unsafe static void Internal_SetMRTFullSetup([NotNull] RenderBuffer[] color, RenderBuffer depth, int mip, CubemapFace face, int depthSlice, [NotNull] RenderBufferLoadAction[] colorLA, [NotNull] RenderBufferStoreAction[] colorSA, RenderBufferLoadAction depthLA, RenderBufferStoreAction depthSA)
		{
			if (color == null)
			{
				ThrowHelper.ThrowArgumentNullException(color, "color");
			}
			if (colorLA == null)
			{
				ThrowHelper.ThrowArgumentNullException(colorLA, "colorLA");
			}
			if (colorSA == null)
			{
				ThrowHelper.ThrowArgumentNullException(colorSA, "colorSA");
			}
			Span<RenderBuffer> span = new Span<RenderBuffer>(color);
			fixed (RenderBuffer* ptr = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, span.Length);
				Span<RenderBufferLoadAction> span2 = new Span<RenderBufferLoadAction>(colorLA);
				fixed (RenderBufferLoadAction* ptr2 = span2.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, span2.Length);
					Span<RenderBufferStoreAction> span3 = new Span<RenderBufferStoreAction>(colorSA);
					fixed (RenderBufferStoreAction* pinnableReference = span3.GetPinnableReference())
					{
						ManagedSpanWrapper managedSpanWrapper3 = new ManagedSpanWrapper((void*)pinnableReference, span3.Length);
						Graphics.Internal_SetMRTFullSetup_Injected(ref managedSpanWrapper, ref depth, mip, face, depthSlice, ref managedSpanWrapper2, ref managedSpanWrapper3, depthLA, depthSA);
						ptr = null;
						ptr2 = null;
					}
				}
			}
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x00015CE0 File Offset: 0x00013EE0
		[NativeMethod(Name = "GraphicsScripting::SetRandomWriteTargetRT", IsFreeFunction = true, ThrowsException = true)]
		private static void Internal_SetRandomWriteTargetRT(int index, RenderTexture uav)
		{
			Graphics.Internal_SetRandomWriteTargetRT_Injected(index, Object.MarshalledUnityObject.Marshal<RenderTexture>(uav));
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x00015CFC File Offset: 0x00013EFC
		[FreeFunction("GraphicsScripting::SetRandomWriteTargetBuffer")]
		private static void Internal_SetRandomWriteTargetBuffer(int index, ComputeBuffer uav, bool preserveCounterValue)
		{
			Graphics.Internal_SetRandomWriteTargetBuffer_Injected(index, (uav == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(uav), preserveCounterValue);
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x00015D20 File Offset: 0x00013F20
		[FreeFunction("GraphicsScripting::SetRandomWriteTargetBuffer")]
		private static void Internal_SetRandomWriteTargetGraphicsBuffer(int index, GraphicsBuffer uav, bool preserveCounterValue)
		{
			Graphics.Internal_SetRandomWriteTargetGraphicsBuffer_Injected(index, (uav == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(uav), preserveCounterValue);
		}

		// Token: 0x06000BDF RID: 3039
		[StaticAccessor("GetGfxDevice()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ClearRandomWriteTargets();

		// Token: 0x06000BE0 RID: 3040 RVA: 0x00015D44 File Offset: 0x00013F44
		[FreeFunction("CopyTexture")]
		private static void CopyTexture_Full(Texture src, Texture dst)
		{
			Graphics.CopyTexture_Full_Injected(Object.MarshalledUnityObject.Marshal<Texture>(src), Object.MarshalledUnityObject.Marshal<Texture>(dst));
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x00015D64 File Offset: 0x00013F64
		[FreeFunction("CopyTexture")]
		private static void CopyTexture_Slice_AllMips(Texture src, int srcElement, Texture dst, int dstElement)
		{
			Graphics.CopyTexture_Slice_AllMips_Injected(Object.MarshalledUnityObject.Marshal<Texture>(src), srcElement, Object.MarshalledUnityObject.Marshal<Texture>(dst), dstElement);
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x00015D84 File Offset: 0x00013F84
		[FreeFunction("CopyTexture")]
		private static void CopyTexture_Slice(Texture src, int srcElement, int srcMip, Texture dst, int dstElement, int dstMip)
		{
			Graphics.CopyTexture_Slice_Injected(Object.MarshalledUnityObject.Marshal<Texture>(src), srcElement, srcMip, Object.MarshalledUnityObject.Marshal<Texture>(dst), dstElement, dstMip);
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x00015DA8 File Offset: 0x00013FA8
		[FreeFunction("CopyTextureRegion")]
		private static void CopyTexture_Region(Texture src, int srcElement, int srcMip, int srcX, int srcY, int srcWidth, int srcHeight, Texture dst, int dstElement, int dstMip, int dstX, int dstY)
		{
			Graphics.CopyTexture_Region_Injected(Object.MarshalledUnityObject.Marshal<Texture>(src), srcElement, srcMip, srcX, srcY, srcWidth, srcHeight, Object.MarshalledUnityObject.Marshal<Texture>(dst), dstElement, dstMip, dstX, dstY);
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x00015DD8 File Offset: 0x00013FD8
		[FreeFunction("CopyTexture")]
		private static void CopyTexture_Full_Gfx(GraphicsTexture src, GraphicsTexture dst)
		{
			Graphics.CopyTexture_Full_Gfx_Injected((src == null) ? ((IntPtr)0) : GraphicsTexture.BindingsMarshaller.ConvertToNative(src), (dst == null) ? ((IntPtr)0) : GraphicsTexture.BindingsMarshaller.ConvertToNative(dst));
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x00015E08 File Offset: 0x00014008
		[FreeFunction("CopyTexture")]
		private static void CopyTexture_Slice_AllMips_Gfx(GraphicsTexture src, int srcElement, GraphicsTexture dst, int dstElement)
		{
			Graphics.CopyTexture_Slice_AllMips_Gfx_Injected((src == null) ? ((IntPtr)0) : GraphicsTexture.BindingsMarshaller.ConvertToNative(src), srcElement, (dst == null) ? ((IntPtr)0) : GraphicsTexture.BindingsMarshaller.ConvertToNative(dst), dstElement);
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x00015E3C File Offset: 0x0001403C
		[FreeFunction("CopyTexture")]
		private static void CopyTexture_Slice_Gfx(GraphicsTexture src, int srcElement, int srcMip, GraphicsTexture dst, int dstElement, int dstMip)
		{
			Graphics.CopyTexture_Slice_Gfx_Injected((src == null) ? ((IntPtr)0) : GraphicsTexture.BindingsMarshaller.ConvertToNative(src), srcElement, srcMip, (dst == null) ? ((IntPtr)0) : GraphicsTexture.BindingsMarshaller.ConvertToNative(dst), dstElement, dstMip);
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x00015E74 File Offset: 0x00014074
		[FreeFunction("CopyTextureRegion")]
		private static void CopyTexture_Region_Gfx(GraphicsTexture src, int srcElement, int srcMip, int srcX, int srcY, int srcWidth, int srcHeight, GraphicsTexture dst, int dstElement, int dstMip, int dstX, int dstY)
		{
			Graphics.CopyTexture_Region_Gfx_Injected((src == null) ? ((IntPtr)0) : GraphicsTexture.BindingsMarshaller.ConvertToNative(src), srcElement, srcMip, srcX, srcY, srcWidth, srcHeight, (dst == null) ? ((IntPtr)0) : GraphicsTexture.BindingsMarshaller.ConvertToNative(dst), dstElement, dstMip, dstX, dstY);
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x00015EB8 File Offset: 0x000140B8
		[FreeFunction("ConvertTexture")]
		private static bool ConvertTexture_Full(Texture src, Texture dst)
		{
			return Graphics.ConvertTexture_Full_Injected(Object.MarshalledUnityObject.Marshal<Texture>(src), Object.MarshalledUnityObject.Marshal<Texture>(dst));
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x00015ED8 File Offset: 0x000140D8
		[FreeFunction("ConvertTexture")]
		private static bool ConvertTexture_Slice(Texture src, int srcElement, Texture dst, int dstElement)
		{
			return Graphics.ConvertTexture_Slice_Injected(Object.MarshalledUnityObject.Marshal<Texture>(src), srcElement, Object.MarshalledUnityObject.Marshal<Texture>(dst), dstElement);
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x00015EF8 File Offset: 0x000140F8
		[FreeFunction("ConvertTexture")]
		private static bool ConvertTexture_Full_Gfx(GraphicsTexture src, GraphicsTexture dst)
		{
			return Graphics.ConvertTexture_Full_Gfx_Injected((src == null) ? ((IntPtr)0) : GraphicsTexture.BindingsMarshaller.ConvertToNative(src), (dst == null) ? ((IntPtr)0) : GraphicsTexture.BindingsMarshaller.ConvertToNative(dst));
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x00015F28 File Offset: 0x00014128
		[FreeFunction("ConvertTexture")]
		private static bool ConvertTexture_Slice_Gfx(GraphicsTexture src, int srcElement, GraphicsTexture dst, int dstElement)
		{
			return Graphics.ConvertTexture_Slice_Gfx_Injected((src == null) ? ((IntPtr)0) : GraphicsTexture.BindingsMarshaller.ConvertToNative(src), srcElement, (dst == null) ? ((IntPtr)0) : GraphicsTexture.BindingsMarshaller.ConvertToNative(dst), dstElement);
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x00015F5C File Offset: 0x0001415C
		[FreeFunction("GraphicsScripting::CopyBuffer", ThrowsException = true)]
		private static void CopyBufferImpl([NotNull] GraphicsBuffer source, [NotNull] GraphicsBuffer dest)
		{
			if (source == null)
			{
				ThrowHelper.ThrowArgumentNullException(source, "source");
			}
			if (dest == null)
			{
				ThrowHelper.ThrowArgumentNullException(dest, "dest");
			}
			IntPtr intPtr = GraphicsBuffer.BindingsMarshaller.ConvertToNative(source);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(source, "source");
			}
			IntPtr intPtr2 = GraphicsBuffer.BindingsMarshaller.ConvertToNative(dest);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(dest, "dest");
			}
			Graphics.CopyBufferImpl_Injected(intPtr, intPtr2);
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x00015FB8 File Offset: 0x000141B8
		[FreeFunction("GraphicsScripting::DrawMeshNow")]
		private static void Internal_DrawMeshNow1([NotNull] Mesh mesh, int subsetIndex, Vector3 position, Quaternion rotation)
		{
			if (mesh == null)
			{
				ThrowHelper.ThrowArgumentNullException(mesh, "mesh");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(mesh);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(mesh, "mesh");
			}
			Graphics.Internal_DrawMeshNow1_Injected(intPtr, subsetIndex, ref position, ref rotation);
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x00015FF4 File Offset: 0x000141F4
		[FreeFunction("GraphicsScripting::DrawMeshNow")]
		private static void Internal_DrawMeshNow2([NotNull] Mesh mesh, int subsetIndex, Matrix4x4 matrix)
		{
			if (mesh == null)
			{
				ThrowHelper.ThrowArgumentNullException(mesh, "mesh");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(mesh);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(mesh, "mesh");
			}
			Graphics.Internal_DrawMeshNow2_Injected(intPtr, subsetIndex, ref matrix);
		}

		// Token: 0x06000BEF RID: 3055
		[FreeFunction("GraphicsScripting::DrawTexture")]
		[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule" })]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_DrawTexture(ref Internal_DrawTextureArguments args);

		// Token: 0x06000BF0 RID: 3056 RVA: 0x00016030 File Offset: 0x00014230
		[FreeFunction("GraphicsScripting::RenderMesh")]
		private unsafe static void Internal_RenderMesh(RenderParams rparams, [NotNull] Mesh mesh, int submeshIndex, Matrix4x4 objectToWorld, Matrix4x4* prevObjectToWorld)
		{
			if (mesh == null)
			{
				ThrowHelper.ThrowArgumentNullException(mesh, "mesh");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(mesh);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(mesh, "mesh");
			}
			Graphics.Internal_RenderMesh_Injected(ref rparams, intPtr, submeshIndex, ref objectToWorld, prevObjectToWorld);
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x00016070 File Offset: 0x00014270
		[FreeFunction("GraphicsScripting::RenderMeshInstanced")]
		private static void Internal_RenderMeshInstanced(RenderParams rparams, [NotNull] Mesh mesh, int submeshIndex, IntPtr instanceData, RenderInstancedDataLayout layout, uint instanceCount)
		{
			if (mesh == null)
			{
				ThrowHelper.ThrowArgumentNullException(mesh, "mesh");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(mesh);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(mesh, "mesh");
			}
			Graphics.Internal_RenderMeshInstanced_Injected(ref rparams, intPtr, submeshIndex, instanceData, ref layout, instanceCount);
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x000160B0 File Offset: 0x000142B0
		[FreeFunction("GraphicsScripting::RenderMeshIndirect")]
		private static void Internal_RenderMeshIndirect(RenderParams rparams, [NotNull] Mesh mesh, [NotNull] GraphicsBuffer commandBuffer, int commandCount, int startCommand)
		{
			if (mesh == null)
			{
				ThrowHelper.ThrowArgumentNullException(mesh, "mesh");
			}
			if (commandBuffer == null)
			{
				ThrowHelper.ThrowArgumentNullException(commandBuffer, "commandBuffer");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(mesh);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(mesh, "mesh");
			}
			IntPtr intPtr2 = GraphicsBuffer.BindingsMarshaller.ConvertToNative(commandBuffer);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(commandBuffer, "commandBuffer");
			}
			Graphics.Internal_RenderMeshIndirect_Injected(ref rparams, intPtr, intPtr2, commandCount, startCommand);
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x00016110 File Offset: 0x00014310
		[FreeFunction("GraphicsScripting::RenderMeshPrimitives")]
		private static void Internal_RenderMeshPrimitives(RenderParams rparams, [NotNull] Mesh mesh, int submeshIndex, int instanceCount)
		{
			if (mesh == null)
			{
				ThrowHelper.ThrowArgumentNullException(mesh, "mesh");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(mesh);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(mesh, "mesh");
			}
			Graphics.Internal_RenderMeshPrimitives_Injected(ref rparams, intPtr, submeshIndex, instanceCount);
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x0001614C File Offset: 0x0001434C
		[FreeFunction("GraphicsScripting::RenderPrimitives")]
		private static void Internal_RenderPrimitives(RenderParams rparams, MeshTopology topology, int vertexCount, int instanceCount)
		{
			Graphics.Internal_RenderPrimitives_Injected(ref rparams, topology, vertexCount, instanceCount);
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x00016164 File Offset: 0x00014364
		[FreeFunction("GraphicsScripting::RenderPrimitivesIndexed")]
		private static void Internal_RenderPrimitivesIndexed(RenderParams rparams, MeshTopology topology, [NotNull] GraphicsBuffer indexBuffer, int indexCount, int startIndex, int instanceCount)
		{
			if (indexBuffer == null)
			{
				ThrowHelper.ThrowArgumentNullException(indexBuffer, "indexBuffer");
			}
			IntPtr intPtr = GraphicsBuffer.BindingsMarshaller.ConvertToNative(indexBuffer);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(indexBuffer, "indexBuffer");
			}
			Graphics.Internal_RenderPrimitivesIndexed_Injected(ref rparams, topology, intPtr, indexCount, startIndex, instanceCount);
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x000161A4 File Offset: 0x000143A4
		[FreeFunction("GraphicsScripting::RenderPrimitivesIndirect")]
		private static void Internal_RenderPrimitivesIndirect(RenderParams rparams, MeshTopology topology, [NotNull] GraphicsBuffer commandBuffer, int commandCount, int startCommand)
		{
			if (commandBuffer == null)
			{
				ThrowHelper.ThrowArgumentNullException(commandBuffer, "commandBuffer");
			}
			IntPtr intPtr = GraphicsBuffer.BindingsMarshaller.ConvertToNative(commandBuffer);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(commandBuffer, "commandBuffer");
			}
			Graphics.Internal_RenderPrimitivesIndirect_Injected(ref rparams, topology, intPtr, commandCount, startCommand);
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x000161E0 File Offset: 0x000143E0
		[FreeFunction("GraphicsScripting::RenderPrimitivesIndexedIndirect")]
		private static void Internal_RenderPrimitivesIndexedIndirect(RenderParams rparams, MeshTopology topology, [NotNull] GraphicsBuffer indexBuffer, [NotNull] GraphicsBuffer commandBuffer, int commandCount, int startCommand)
		{
			if (indexBuffer == null)
			{
				ThrowHelper.ThrowArgumentNullException(indexBuffer, "indexBuffer");
			}
			if (commandBuffer == null)
			{
				ThrowHelper.ThrowArgumentNullException(commandBuffer, "commandBuffer");
			}
			IntPtr intPtr = GraphicsBuffer.BindingsMarshaller.ConvertToNative(indexBuffer);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(indexBuffer, "indexBuffer");
			}
			IntPtr intPtr2 = GraphicsBuffer.BindingsMarshaller.ConvertToNative(commandBuffer);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(commandBuffer, "commandBuffer");
			}
			Graphics.Internal_RenderPrimitivesIndexedIndirect_Injected(ref rparams, topology, intPtr, intPtr2, commandCount, startCommand);
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x00016244 File Offset: 0x00014444
		[FreeFunction("GraphicsScripting::DrawMesh")]
		private static void Internal_DrawMesh(Mesh mesh, int submeshIndex, Matrix4x4 matrix, Material material, int layer, Camera camera, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, Transform probeAnchor, LightProbeUsage lightProbeUsage, LightProbeProxyVolume lightProbeProxyVolume)
		{
			Graphics.Internal_DrawMesh_Injected(Object.MarshalledUnityObject.Marshal<Mesh>(mesh), submeshIndex, ref matrix, Object.MarshalledUnityObject.Marshal<Material>(material), layer, Object.MarshalledUnityObject.Marshal<Camera>(camera), (properties == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(properties), castShadows, receiveShadows, Object.MarshalledUnityObject.Marshal<Transform>(probeAnchor), lightProbeUsage, Object.MarshalledUnityObject.Marshal<LightProbeProxyVolume>(lightProbeProxyVolume));
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x00016294 File Offset: 0x00014494
		[FreeFunction("GraphicsScripting::DrawMeshInstanced")]
		private unsafe static void Internal_DrawMeshInstanced([NotNull] Mesh mesh, int submeshIndex, [NotNull] Material material, Matrix4x4[] matrices, int count, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer, Camera camera, LightProbeUsage lightProbeUsage, LightProbeProxyVolume lightProbeProxyVolume)
		{
			if (mesh == null)
			{
				ThrowHelper.ThrowArgumentNullException(mesh, "mesh");
			}
			if (material == null)
			{
				ThrowHelper.ThrowArgumentNullException(material, "material");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(mesh);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(mesh, "mesh");
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Material>(material);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(material, "material");
			}
			Span<Matrix4x4> span = new Span<Matrix4x4>(matrices);
			fixed (Matrix4x4* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				Graphics.Internal_DrawMeshInstanced_Injected(intPtr, submeshIndex, intPtr2, ref managedSpanWrapper, count, (properties == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(properties), castShadows, receiveShadows, layer, Object.MarshalledUnityObject.Marshal<Camera>(camera), lightProbeUsage, Object.MarshalledUnityObject.Marshal<LightProbeProxyVolume>(lightProbeProxyVolume));
			}
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x00016340 File Offset: 0x00014540
		[FreeFunction("GraphicsScripting::DrawMeshInstancedProcedural")]
		private static void Internal_DrawMeshInstancedProcedural([NotNull] Mesh mesh, int submeshIndex, [NotNull] Material material, Bounds bounds, int count, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer, Camera camera, LightProbeUsage lightProbeUsage, LightProbeProxyVolume lightProbeProxyVolume)
		{
			if (mesh == null)
			{
				ThrowHelper.ThrowArgumentNullException(mesh, "mesh");
			}
			if (material == null)
			{
				ThrowHelper.ThrowArgumentNullException(material, "material");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(mesh);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(mesh, "mesh");
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Material>(material);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(material, "material");
			}
			Graphics.Internal_DrawMeshInstancedProcedural_Injected(intPtr, submeshIndex, intPtr2, ref bounds, count, (properties == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(properties), castShadows, receiveShadows, layer, Object.MarshalledUnityObject.Marshal<Camera>(camera), lightProbeUsage, Object.MarshalledUnityObject.Marshal<LightProbeProxyVolume>(lightProbeProxyVolume));
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x000163C8 File Offset: 0x000145C8
		[FreeFunction("GraphicsScripting::DrawMeshInstancedIndirect")]
		private static void Internal_DrawMeshInstancedIndirect([NotNull] Mesh mesh, int submeshIndex, [NotNull] Material material, Bounds bounds, ComputeBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer, Camera camera, LightProbeUsage lightProbeUsage, LightProbeProxyVolume lightProbeProxyVolume)
		{
			if (mesh == null)
			{
				ThrowHelper.ThrowArgumentNullException(mesh, "mesh");
			}
			if (material == null)
			{
				ThrowHelper.ThrowArgumentNullException(material, "material");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(mesh);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(mesh, "mesh");
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Material>(material);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(material, "material");
			}
			Graphics.Internal_DrawMeshInstancedIndirect_Injected(intPtr, submeshIndex, intPtr2, ref bounds, (bufferWithArgs == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(bufferWithArgs), argsOffset, (properties == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(properties), castShadows, receiveShadows, layer, Object.MarshalledUnityObject.Marshal<Camera>(camera), lightProbeUsage, Object.MarshalledUnityObject.Marshal<LightProbeProxyVolume>(lightProbeProxyVolume));
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x00016460 File Offset: 0x00014660
		[FreeFunction("GraphicsScripting::DrawMeshInstancedIndirect")]
		private static void Internal_DrawMeshInstancedIndirectGraphicsBuffer([NotNull] Mesh mesh, int submeshIndex, [NotNull] Material material, Bounds bounds, GraphicsBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer, Camera camera, LightProbeUsage lightProbeUsage, LightProbeProxyVolume lightProbeProxyVolume)
		{
			if (mesh == null)
			{
				ThrowHelper.ThrowArgumentNullException(mesh, "mesh");
			}
			if (material == null)
			{
				ThrowHelper.ThrowArgumentNullException(material, "material");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(mesh);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(mesh, "mesh");
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Material>(material);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(material, "material");
			}
			Graphics.Internal_DrawMeshInstancedIndirectGraphicsBuffer_Injected(intPtr, submeshIndex, intPtr2, ref bounds, (bufferWithArgs == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(bufferWithArgs), argsOffset, (properties == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(properties), castShadows, receiveShadows, layer, Object.MarshalledUnityObject.Marshal<Camera>(camera), lightProbeUsage, Object.MarshalledUnityObject.Marshal<LightProbeProxyVolume>(lightProbeProxyVolume));
		}

		// Token: 0x06000BFD RID: 3069
		[FreeFunction("GraphicsScripting::DrawProceduralNow")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawProceduralNow(MeshTopology topology, int vertexCount, int instanceCount);

		// Token: 0x06000BFE RID: 3070 RVA: 0x000164F8 File Offset: 0x000146F8
		[FreeFunction("GraphicsScripting::DrawProceduralIndexedNow")]
		private static void Internal_DrawProceduralIndexedNow(MeshTopology topology, GraphicsBuffer indexBuffer, int indexCount, int instanceCount)
		{
			Graphics.Internal_DrawProceduralIndexedNow_Injected(topology, (indexBuffer == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(indexBuffer), indexCount, instanceCount);
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x0001651C File Offset: 0x0001471C
		[FreeFunction("GraphicsScripting::DrawProceduralIndirectNow")]
		private static void Internal_DrawProceduralIndirectNow(MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset)
		{
			Graphics.Internal_DrawProceduralIndirectNow_Injected(topology, (bufferWithArgs == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(bufferWithArgs), argsOffset);
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x00016540 File Offset: 0x00014740
		[FreeFunction("GraphicsScripting::DrawProceduralIndexedIndirectNow")]
		private static void Internal_DrawProceduralIndexedIndirectNow(MeshTopology topology, GraphicsBuffer indexBuffer, ComputeBuffer bufferWithArgs, int argsOffset)
		{
			Graphics.Internal_DrawProceduralIndexedIndirectNow_Injected(topology, (indexBuffer == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(indexBuffer), (bufferWithArgs == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(bufferWithArgs), argsOffset);
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x00016574 File Offset: 0x00014774
		[FreeFunction("GraphicsScripting::DrawProceduralIndirectNow")]
		private static void Internal_DrawProceduralIndirectNowGraphicsBuffer(MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset)
		{
			Graphics.Internal_DrawProceduralIndirectNowGraphicsBuffer_Injected(topology, (bufferWithArgs == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(bufferWithArgs), argsOffset);
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x00016598 File Offset: 0x00014798
		[FreeFunction("GraphicsScripting::DrawProceduralIndexedIndirectNow")]
		private static void Internal_DrawProceduralIndexedIndirectNowGraphicsBuffer(MeshTopology topology, GraphicsBuffer indexBuffer, GraphicsBuffer bufferWithArgs, int argsOffset)
		{
			Graphics.Internal_DrawProceduralIndexedIndirectNowGraphicsBuffer_Injected(topology, (indexBuffer == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(indexBuffer), (bufferWithArgs == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(bufferWithArgs), argsOffset);
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x000165CC File Offset: 0x000147CC
		[FreeFunction("GraphicsScripting::DrawProcedural")]
		private static void Internal_DrawProcedural(Material material, Bounds bounds, MeshTopology topology, int vertexCount, int instanceCount, Camera camera, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer)
		{
			Graphics.Internal_DrawProcedural_Injected(Object.MarshalledUnityObject.Marshal<Material>(material), ref bounds, topology, vertexCount, instanceCount, Object.MarshalledUnityObject.Marshal<Camera>(camera), (properties == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(properties), castShadows, receiveShadows, layer);
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x00016608 File Offset: 0x00014808
		[FreeFunction("GraphicsScripting::DrawProceduralIndexed")]
		private static void Internal_DrawProceduralIndexed(Material material, Bounds bounds, MeshTopology topology, GraphicsBuffer indexBuffer, int indexCount, int instanceCount, Camera camera, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer)
		{
			Graphics.Internal_DrawProceduralIndexed_Injected(Object.MarshalledUnityObject.Marshal<Material>(material), ref bounds, topology, (indexBuffer == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(indexBuffer), indexCount, instanceCount, Object.MarshalledUnityObject.Marshal<Camera>(camera), (properties == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(properties), castShadows, receiveShadows, layer);
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x00016654 File Offset: 0x00014854
		[FreeFunction("GraphicsScripting::DrawProceduralIndirect")]
		private static void Internal_DrawProceduralIndirect(Material material, Bounds bounds, MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset, Camera camera, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer)
		{
			Graphics.Internal_DrawProceduralIndirect_Injected(Object.MarshalledUnityObject.Marshal<Material>(material), ref bounds, topology, (bufferWithArgs == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(bufferWithArgs), argsOffset, Object.MarshalledUnityObject.Marshal<Camera>(camera), (properties == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(properties), castShadows, receiveShadows, layer);
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x000166A0 File Offset: 0x000148A0
		[FreeFunction("GraphicsScripting::DrawProceduralIndirect")]
		private static void Internal_DrawProceduralIndirectGraphicsBuffer(Material material, Bounds bounds, MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset, Camera camera, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer)
		{
			Graphics.Internal_DrawProceduralIndirectGraphicsBuffer_Injected(Object.MarshalledUnityObject.Marshal<Material>(material), ref bounds, topology, (bufferWithArgs == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(bufferWithArgs), argsOffset, Object.MarshalledUnityObject.Marshal<Camera>(camera), (properties == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(properties), castShadows, receiveShadows, layer);
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x000166EC File Offset: 0x000148EC
		[FreeFunction("GraphicsScripting::DrawProceduralIndexedIndirect")]
		private static void Internal_DrawProceduralIndexedIndirect(Material material, Bounds bounds, MeshTopology topology, GraphicsBuffer indexBuffer, ComputeBuffer bufferWithArgs, int argsOffset, Camera camera, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer)
		{
			Graphics.Internal_DrawProceduralIndexedIndirect_Injected(Object.MarshalledUnityObject.Marshal<Material>(material), ref bounds, topology, (indexBuffer == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(indexBuffer), (bufferWithArgs == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(bufferWithArgs), argsOffset, Object.MarshalledUnityObject.Marshal<Camera>(camera), (properties == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(properties), castShadows, receiveShadows, layer);
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x00016748 File Offset: 0x00014948
		[FreeFunction("GraphicsScripting::DrawProceduralIndexedIndirect")]
		private static void Internal_DrawProceduralIndexedIndirectGraphicsBuffer(Material material, Bounds bounds, MeshTopology topology, GraphicsBuffer indexBuffer, GraphicsBuffer bufferWithArgs, int argsOffset, Camera camera, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer)
		{
			Graphics.Internal_DrawProceduralIndexedIndirectGraphicsBuffer_Injected(Object.MarshalledUnityObject.Marshal<Material>(material), ref bounds, topology, (indexBuffer == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(indexBuffer), (bufferWithArgs == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(bufferWithArgs), argsOffset, Object.MarshalledUnityObject.Marshal<Camera>(camera), (properties == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(properties), castShadows, receiveShadows, layer);
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x000167A4 File Offset: 0x000149A4
		[FreeFunction("GraphicsScripting::BlitMaterial")]
		private static void Internal_BlitMaterial5(Texture source, RenderTexture dest, [NotNull] Material mat, int pass, bool setRT)
		{
			if (mat == null)
			{
				ThrowHelper.ThrowArgumentNullException(mat, "mat");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.Marshal<Texture>(source);
			IntPtr intPtr2 = Object.MarshalledUnityObject.Marshal<RenderTexture>(dest);
			IntPtr intPtr3 = Object.MarshalledUnityObject.MarshalNotNull<Material>(mat);
			if (intPtr3 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(mat, "mat");
			}
			Graphics.Internal_BlitMaterial5_Injected(intPtr, intPtr2, intPtr3, pass, setRT);
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x000167EC File Offset: 0x000149EC
		[FreeFunction("GraphicsScripting::BlitMaterial")]
		private static void Internal_BlitMaterial6(Texture source, RenderTexture dest, [NotNull] Material mat, int pass, bool setRT, int destDepthSlice)
		{
			if (mat == null)
			{
				ThrowHelper.ThrowArgumentNullException(mat, "mat");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.Marshal<Texture>(source);
			IntPtr intPtr2 = Object.MarshalledUnityObject.Marshal<RenderTexture>(dest);
			IntPtr intPtr3 = Object.MarshalledUnityObject.MarshalNotNull<Material>(mat);
			if (intPtr3 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(mat, "mat");
			}
			Graphics.Internal_BlitMaterial6_Injected(intPtr, intPtr2, intPtr3, pass, setRT, destDepthSlice);
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x00016834 File Offset: 0x00014A34
		[FreeFunction("GraphicsScripting::BlitMultitap")]
		private unsafe static void Internal_BlitMultiTap4(Texture source, RenderTexture dest, [NotNull] Material mat, [NotNull] Vector2[] offsets)
		{
			if (mat == null)
			{
				ThrowHelper.ThrowArgumentNullException(mat, "mat");
			}
			if (offsets == null)
			{
				ThrowHelper.ThrowArgumentNullException(offsets, "offsets");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.Marshal<Texture>(source);
			IntPtr intPtr2 = Object.MarshalledUnityObject.Marshal<RenderTexture>(dest);
			IntPtr intPtr3 = Object.MarshalledUnityObject.MarshalNotNull<Material>(mat);
			if (intPtr3 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(mat, "mat");
			}
			Span<Vector2> span = new Span<Vector2>(offsets);
			fixed (Vector2* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				Graphics.Internal_BlitMultiTap4_Injected(intPtr, intPtr2, intPtr3, ref managedSpanWrapper);
			}
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x000168AC File Offset: 0x00014AAC
		[FreeFunction("GraphicsScripting::BlitMultitap")]
		private unsafe static void Internal_BlitMultiTap5(Texture source, RenderTexture dest, [NotNull] Material mat, [NotNull] Vector2[] offsets, int destDepthSlice)
		{
			if (mat == null)
			{
				ThrowHelper.ThrowArgumentNullException(mat, "mat");
			}
			if (offsets == null)
			{
				ThrowHelper.ThrowArgumentNullException(offsets, "offsets");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.Marshal<Texture>(source);
			IntPtr intPtr2 = Object.MarshalledUnityObject.Marshal<RenderTexture>(dest);
			IntPtr intPtr3 = Object.MarshalledUnityObject.MarshalNotNull<Material>(mat);
			if (intPtr3 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(mat, "mat");
			}
			Span<Vector2> span = new Span<Vector2>(offsets);
			fixed (Vector2* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				Graphics.Internal_BlitMultiTap5_Injected(intPtr, intPtr2, intPtr3, ref managedSpanWrapper, destDepthSlice);
			}
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x00016928 File Offset: 0x00014B28
		[FreeFunction("GraphicsScripting::Blit")]
		private static void Blit2(Texture source, RenderTexture dest)
		{
			Graphics.Blit2_Injected(Object.MarshalledUnityObject.Marshal<Texture>(source), Object.MarshalledUnityObject.Marshal<RenderTexture>(dest));
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x00016948 File Offset: 0x00014B48
		[FreeFunction("GraphicsScripting::Blit")]
		private static void Blit3(Texture source, RenderTexture dest, int sourceDepthSlice, int destDepthSlice)
		{
			Graphics.Blit3_Injected(Object.MarshalledUnityObject.Marshal<Texture>(source), Object.MarshalledUnityObject.Marshal<RenderTexture>(dest), sourceDepthSlice, destDepthSlice);
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x00016968 File Offset: 0x00014B68
		[FreeFunction("GraphicsScripting::Blit")]
		private static void Blit4(Texture source, RenderTexture dest, Vector2 scale, Vector2 offset)
		{
			Graphics.Blit4_Injected(Object.MarshalledUnityObject.Marshal<Texture>(source), Object.MarshalledUnityObject.Marshal<RenderTexture>(dest), ref scale, ref offset);
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x0001698C File Offset: 0x00014B8C
		[FreeFunction("GraphicsScripting::Blit")]
		private static void Blit5(Texture source, RenderTexture dest, Vector2 scale, Vector2 offset, int sourceDepthSlice, int destDepthSlice)
		{
			Graphics.Blit5_Injected(Object.MarshalledUnityObject.Marshal<Texture>(source), Object.MarshalledUnityObject.Marshal<RenderTexture>(dest), ref scale, ref offset, sourceDepthSlice, destDepthSlice);
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x000169B4 File Offset: 0x00014BB4
		[FreeFunction("GraphicsScripting::BlitMaterial")]
		private static void Internal_BlitMaterialGfx5(Texture source, GraphicsTexture dest, [NotNull] Material mat, int pass, bool setRT)
		{
			if (mat == null)
			{
				ThrowHelper.ThrowArgumentNullException(mat, "mat");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.Marshal<Texture>(source);
			IntPtr intPtr2 = ((dest == null) ? ((IntPtr)0) : GraphicsTexture.BindingsMarshaller.ConvertToNative(dest));
			IntPtr intPtr3 = Object.MarshalledUnityObject.MarshalNotNull<Material>(mat);
			if (intPtr3 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(mat, "mat");
			}
			Graphics.Internal_BlitMaterialGfx5_Injected(intPtr, intPtr2, intPtr3, pass, setRT);
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x00016A04 File Offset: 0x00014C04
		[FreeFunction("GraphicsScripting::BlitMaterial")]
		private static void Internal_BlitMaterialGfx6(Texture source, GraphicsTexture dest, [NotNull] Material mat, int pass, bool setRT, int destDepthSlice)
		{
			if (mat == null)
			{
				ThrowHelper.ThrowArgumentNullException(mat, "mat");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.Marshal<Texture>(source);
			IntPtr intPtr2 = ((dest == null) ? ((IntPtr)0) : GraphicsTexture.BindingsMarshaller.ConvertToNative(dest));
			IntPtr intPtr3 = Object.MarshalledUnityObject.MarshalNotNull<Material>(mat);
			if (intPtr3 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(mat, "mat");
			}
			Graphics.Internal_BlitMaterialGfx6_Injected(intPtr, intPtr2, intPtr3, pass, setRT, destDepthSlice);
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x00016A54 File Offset: 0x00014C54
		[FreeFunction("GraphicsScripting::BlitMultitap")]
		private unsafe static void Internal_BlitMultiTapGfx4(Texture source, GraphicsTexture dest, [NotNull] Material mat, [NotNull] Vector2[] offsets)
		{
			if (mat == null)
			{
				ThrowHelper.ThrowArgumentNullException(mat, "mat");
			}
			if (offsets == null)
			{
				ThrowHelper.ThrowArgumentNullException(offsets, "offsets");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.Marshal<Texture>(source);
			IntPtr intPtr2 = ((dest == null) ? ((IntPtr)0) : GraphicsTexture.BindingsMarshaller.ConvertToNative(dest));
			IntPtr intPtr3 = Object.MarshalledUnityObject.MarshalNotNull<Material>(mat);
			if (intPtr3 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(mat, "mat");
			}
			Span<Vector2> span = new Span<Vector2>(offsets);
			fixed (Vector2* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				Graphics.Internal_BlitMultiTapGfx4_Injected(intPtr, intPtr2, intPtr3, ref managedSpanWrapper);
			}
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x00016AD4 File Offset: 0x00014CD4
		[FreeFunction("GraphicsScripting::BlitMultitap")]
		private unsafe static void Internal_BlitMultiTapGfx5(Texture source, GraphicsTexture dest, [NotNull] Material mat, [NotNull] Vector2[] offsets, int destDepthSlice)
		{
			if (mat == null)
			{
				ThrowHelper.ThrowArgumentNullException(mat, "mat");
			}
			if (offsets == null)
			{
				ThrowHelper.ThrowArgumentNullException(offsets, "offsets");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.Marshal<Texture>(source);
			IntPtr intPtr2 = ((dest == null) ? ((IntPtr)0) : GraphicsTexture.BindingsMarshaller.ConvertToNative(dest));
			IntPtr intPtr3 = Object.MarshalledUnityObject.MarshalNotNull<Material>(mat);
			if (intPtr3 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(mat, "mat");
			}
			Span<Vector2> span = new Span<Vector2>(offsets);
			fixed (Vector2* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				Graphics.Internal_BlitMultiTapGfx5_Injected(intPtr, intPtr2, intPtr3, ref managedSpanWrapper, destDepthSlice);
			}
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x00016B58 File Offset: 0x00014D58
		[FreeFunction("GraphicsScripting::Blit")]
		private static void BlitGfx2(Texture source, GraphicsTexture dest)
		{
			Graphics.BlitGfx2_Injected(Object.MarshalledUnityObject.Marshal<Texture>(source), (dest == null) ? ((IntPtr)0) : GraphicsTexture.BindingsMarshaller.ConvertToNative(dest));
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x00016B80 File Offset: 0x00014D80
		[FreeFunction("GraphicsScripting::Blit")]
		private static void BlitGfx3(Texture source, GraphicsTexture dest, int sourceDepthSlice, int destDepthSlice)
		{
			Graphics.BlitGfx3_Injected(Object.MarshalledUnityObject.Marshal<Texture>(source), (dest == null) ? ((IntPtr)0) : GraphicsTexture.BindingsMarshaller.ConvertToNative(dest), sourceDepthSlice, destDepthSlice);
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x00016BAC File Offset: 0x00014DAC
		[FreeFunction("GraphicsScripting::Blit")]
		private static void BlitGfx4(Texture source, GraphicsTexture dest, Vector2 scale, Vector2 offset)
		{
			Graphics.BlitGfx4_Injected(Object.MarshalledUnityObject.Marshal<Texture>(source), (dest == null) ? ((IntPtr)0) : GraphicsTexture.BindingsMarshaller.ConvertToNative(dest), ref scale, ref offset);
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x00016BD8 File Offset: 0x00014DD8
		[FreeFunction("GraphicsScripting::Blit")]
		private static void BlitGfx5(Texture source, GraphicsTexture dest, Vector2 scale, Vector2 offset, int sourceDepthSlice, int destDepthSlice)
		{
			Graphics.BlitGfx5_Injected(Object.MarshalledUnityObject.Marshal<Texture>(source), (dest == null) ? ((IntPtr)0) : GraphicsTexture.BindingsMarshaller.ConvertToNative(dest), ref scale, ref offset, sourceDepthSlice, destDepthSlice);
		}

		// Token: 0x06000C19 RID: 3097
		[NativeMethod(Name = "GraphicsScripting::CreateGPUFence", IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr CreateGPUFenceImpl(GraphicsFenceType fenceType, SynchronisationStageFlags stage);

		// Token: 0x06000C1A RID: 3098
		[NativeMethod(Name = "GraphicsScripting::WaitOnGPUFence", IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WaitOnGPUFenceImpl(IntPtr fencePtr, SynchronisationStageFlags stage);

		// Token: 0x06000C1B RID: 3099 RVA: 0x00016C08 File Offset: 0x00014E08
		[NativeMethod(Name = "GraphicsScripting::ExecuteCommandBuffer", IsFreeFunction = true, ThrowsException = true)]
		public static void ExecuteCommandBuffer([NotNull] CommandBuffer buffer)
		{
			if (buffer == null)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(buffer);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			Graphics.ExecuteCommandBuffer_Injected(intPtr);
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x00016C40 File Offset: 0x00014E40
		[NativeMethod(Name = "GraphicsScripting::ExecuteCommandBufferAsync", IsFreeFunction = true, ThrowsException = true)]
		public static void ExecuteCommandBufferAsync([NotNull] CommandBuffer buffer, ComputeQueueType queueType)
		{
			if (buffer == null)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(buffer);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			Graphics.ExecuteCommandBufferAsync_Injected(intPtr, queueType);
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x00016C78 File Offset: 0x00014E78
		internal static void CheckLoadActionValid(RenderBufferLoadAction load, string bufferType)
		{
			bool flag = load != RenderBufferLoadAction.Load && load != RenderBufferLoadAction.DontCare;
			if (flag)
			{
				throw new ArgumentException(UnityString.Format("Bad {0} LoadAction provided.", new object[] { bufferType }));
			}
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x00016CB4 File Offset: 0x00014EB4
		internal static void CheckStoreActionValid(RenderBufferStoreAction store, string bufferType)
		{
			bool flag = store != RenderBufferStoreAction.Store && store != RenderBufferStoreAction.DontCare;
			if (flag)
			{
				throw new ArgumentException(UnityString.Format("Bad {0} StoreAction provided.", new object[] { bufferType }));
			}
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x00016CF0 File Offset: 0x00014EF0
		internal static void SetRenderTargetImpl(RenderTargetSetup setup)
		{
			bool flag = setup.color.Length == 0;
			if (flag)
			{
				throw new ArgumentException("Invalid color buffer count for SetRenderTarget");
			}
			bool flag2 = setup.color.Length != setup.colorLoad.Length;
			if (flag2)
			{
				throw new ArgumentException("Color LoadAction and Buffer arrays have different sizes");
			}
			bool flag3 = setup.color.Length != setup.colorStore.Length;
			if (flag3)
			{
				throw new ArgumentException("Color StoreAction and Buffer arrays have different sizes");
			}
			foreach (RenderBufferLoadAction renderBufferLoadAction in setup.colorLoad)
			{
				Graphics.CheckLoadActionValid(renderBufferLoadAction, "Color");
			}
			foreach (RenderBufferStoreAction renderBufferStoreAction in setup.colorStore)
			{
				Graphics.CheckStoreActionValid(renderBufferStoreAction, "Color");
			}
			Graphics.CheckLoadActionValid(setup.depthLoad, "Depth");
			Graphics.CheckStoreActionValid(setup.depthStore, "Depth");
			bool flag4 = setup.cubemapFace < CubemapFace.Unknown || setup.cubemapFace > CubemapFace.NegativeZ;
			if (flag4)
			{
				throw new ArgumentException("Bad CubemapFace provided");
			}
			Graphics.Internal_SetMRTFullSetup(setup.color, setup.depth, setup.mipLevel, setup.cubemapFace, setup.depthSlice, setup.colorLoad, setup.colorStore, setup.depthLoad, setup.depthStore);
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x00016E40 File Offset: 0x00015040
		internal static void SetRenderTargetImpl(RenderBuffer colorBuffer, RenderBuffer depthBuffer, int mipLevel, CubemapFace face, int depthSlice)
		{
			Graphics.Internal_SetRTSimple(colorBuffer, depthBuffer, mipLevel, face, depthSlice);
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x00016E50 File Offset: 0x00015050
		internal static void SetRenderTargetImpl(RenderTexture rt, int mipLevel, CubemapFace face, int depthSlice)
		{
			bool flag = rt;
			if (flag)
			{
				Graphics.SetRenderTargetImpl(rt.colorBuffer, rt.depthBuffer, mipLevel, face, depthSlice);
			}
			else
			{
				Graphics.Internal_SetNullRT();
			}
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x00016E88 File Offset: 0x00015088
		internal static void SetRenderTargetImpl(GraphicsTexture rt, int mipLevel, CubemapFace face, int depthSlice)
		{
			bool flag = rt != null;
			if (flag)
			{
				Graphics.Internal_SetGfxRT(rt, mipLevel, face, depthSlice);
			}
			else
			{
				Graphics.Internal_SetNullRT();
			}
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x00016EB0 File Offset: 0x000150B0
		internal static void SetRenderTargetImpl(RenderBuffer[] colorBuffers, RenderBuffer depthBuffer, int mipLevel, CubemapFace face, int depthSlice)
		{
			Graphics.Internal_SetMRTSimple(colorBuffers, depthBuffer, mipLevel, face, depthSlice);
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x00016ECC File Offset: 0x000150CC
		public static void SetRenderTarget(RenderTexture rt, [DefaultValue("0")] int mipLevel, [DefaultValue("CubemapFace.Unknown")] CubemapFace face, [DefaultValue("0")] int depthSlice)
		{
			Graphics.SetRenderTargetImpl(rt, mipLevel, face, depthSlice);
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x00016ED9 File Offset: 0x000150D9
		public static void SetRenderTarget(GraphicsTexture rt, [DefaultValue("0")] int mipLevel, [DefaultValue("CubemapFace.Unknown")] CubemapFace face, [DefaultValue("0")] int depthSlice)
		{
			Graphics.SetRenderTargetImpl(rt, mipLevel, face, depthSlice);
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x00016EE6 File Offset: 0x000150E6
		public static void SetRenderTarget(RenderBuffer colorBuffer, RenderBuffer depthBuffer, [DefaultValue("0")] int mipLevel, [DefaultValue("CubemapFace.Unknown")] CubemapFace face, [DefaultValue("0")] int depthSlice)
		{
			Graphics.SetRenderTargetImpl(colorBuffer, depthBuffer, mipLevel, face, depthSlice);
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x00016EF5 File Offset: 0x000150F5
		public static void SetRenderTarget(RenderBuffer[] colorBuffers, RenderBuffer depthBuffer)
		{
			Graphics.SetRenderTargetImpl(colorBuffers, depthBuffer, 0, CubemapFace.Unknown, 0);
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x00016F03 File Offset: 0x00015103
		public static void SetRenderTarget(RenderTargetSetup setup)
		{
			Graphics.SetRenderTargetImpl(setup);
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000C29 RID: 3113 RVA: 0x00016F10 File Offset: 0x00015110
		public static RenderBuffer activeColorBuffer
		{
			get
			{
				return Graphics.GetActiveColorBuffer();
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000C2A RID: 3114 RVA: 0x00016F28 File Offset: 0x00015128
		public static RenderBuffer activeDepthBuffer
		{
			get
			{
				return Graphics.GetActiveDepthBuffer();
			}
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x00016F40 File Offset: 0x00015140
		public static void SetRandomWriteTarget(int index, RenderTexture uav)
		{
			bool flag = index < 0 || index >= SystemInfo.supportedRandomWriteTargetCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("must be non-negative less than {0}.", SystemInfo.supportedRandomWriteTargetCount));
			}
			Graphics.Internal_SetRandomWriteTargetRT(index, uav);
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x00016F8C File Offset: 0x0001518C
		public static void SetRandomWriteTarget(int index, ComputeBuffer uav, [DefaultValue("false")] bool preserveCounterValue)
		{
			bool flag = uav == null;
			if (flag)
			{
				throw new ArgumentNullException("uav");
			}
			bool flag2 = uav.m_Ptr == IntPtr.Zero;
			if (flag2)
			{
				throw new ObjectDisposedException("uav");
			}
			bool flag3 = index < 0 || index >= SystemInfo.supportedRandomWriteTargetCount;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("must be non-negative less than {0}.", SystemInfo.supportedRandomWriteTargetCount));
			}
			Graphics.Internal_SetRandomWriteTargetBuffer(index, uav, preserveCounterValue);
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x0001700C File Offset: 0x0001520C
		public static void SetRandomWriteTarget(int index, GraphicsBuffer uav, [DefaultValue("false")] bool preserveCounterValue)
		{
			bool flag = uav == null;
			if (flag)
			{
				throw new ArgumentNullException("uav");
			}
			bool flag2 = uav.m_Ptr == IntPtr.Zero;
			if (flag2)
			{
				throw new ObjectDisposedException("uav");
			}
			bool flag3 = index < 0 || index >= SystemInfo.supportedRandomWriteTargetCount;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("must be non-negative less than {0}.", SystemInfo.supportedRandomWriteTargetCount));
			}
			Graphics.Internal_SetRandomWriteTargetGraphicsBuffer(index, uav, preserveCounterValue);
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x0001708A File Offset: 0x0001528A
		public static void CopyTexture(Texture src, Texture dst)
		{
			Graphics.CopyTexture_Full(src, dst);
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x00017095 File Offset: 0x00015295
		public static void CopyTexture(Texture src, int srcElement, Texture dst, int dstElement)
		{
			Graphics.CopyTexture_Slice_AllMips(src, srcElement, dst, dstElement);
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x000170A2 File Offset: 0x000152A2
		public static void CopyTexture(Texture src, int srcElement, int srcMip, Texture dst, int dstElement, int dstMip)
		{
			Graphics.CopyTexture_Slice(src, srcElement, srcMip, dst, dstElement, dstMip);
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x000170B4 File Offset: 0x000152B4
		public static void CopyTexture(Texture src, int srcElement, int srcMip, int srcX, int srcY, int srcWidth, int srcHeight, Texture dst, int dstElement, int dstMip, int dstX, int dstY)
		{
			Graphics.CopyTexture_Region(src, srcElement, srcMip, srcX, srcY, srcWidth, srcHeight, dst, dstElement, dstMip, dstX, dstY);
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x000170DC File Offset: 0x000152DC
		public static void CopyTexture(GraphicsTexture src, GraphicsTexture dst)
		{
			Graphics.CopyTexture_Full_Gfx(src, dst);
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x000170E7 File Offset: 0x000152E7
		public static void CopyTexture(GraphicsTexture src, int srcElement, GraphicsTexture dst, int dstElement)
		{
			Graphics.CopyTexture_Slice_AllMips_Gfx(src, srcElement, dst, dstElement);
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x000170F4 File Offset: 0x000152F4
		public static void CopyTexture(GraphicsTexture src, int srcElement, int srcMip, GraphicsTexture dst, int dstElement, int dstMip)
		{
			Graphics.CopyTexture_Slice_Gfx(src, srcElement, srcMip, dst, dstElement, dstMip);
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x00017108 File Offset: 0x00015308
		public static void CopyTexture(GraphicsTexture src, int srcElement, int srcMip, int srcX, int srcY, int srcWidth, int srcHeight, GraphicsTexture dst, int dstElement, int dstMip, int dstX, int dstY)
		{
			Graphics.CopyTexture_Region_Gfx(src, srcElement, srcMip, srcX, srcY, srcWidth, srcHeight, dst, dstElement, dstMip, dstX, dstY);
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x00017130 File Offset: 0x00015330
		public static bool ConvertTexture(Texture src, Texture dst)
		{
			return Graphics.ConvertTexture_Full(src, dst);
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x0001714C File Offset: 0x0001534C
		public static bool ConvertTexture(Texture src, int srcElement, Texture dst, int dstElement)
		{
			return Graphics.ConvertTexture_Slice(src, srcElement, dst, dstElement);
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x00017168 File Offset: 0x00015368
		public static bool ConvertTexture(GraphicsTexture src, GraphicsTexture dst)
		{
			return Graphics.ConvertTexture_Full_Gfx(src, dst);
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x00017184 File Offset: 0x00015384
		public static bool ConvertTexture(GraphicsTexture src, int srcElement, GraphicsTexture dst, int dstElement)
		{
			return Graphics.ConvertTexture_Slice_Gfx(src, srcElement, dst, dstElement);
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x000171A0 File Offset: 0x000153A0
		public static GraphicsFence CreateAsyncGraphicsFence([DefaultValue("SynchronisationStage.PixelProcessing")] SynchronisationStage stage)
		{
			return Graphics.CreateGraphicsFence(GraphicsFenceType.AsyncQueueSynchronisation, GraphicsFence.TranslateSynchronizationStageToFlags(stage));
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x000171C0 File Offset: 0x000153C0
		public static GraphicsFence CreateAsyncGraphicsFence()
		{
			return Graphics.CreateGraphicsFence(GraphicsFenceType.AsyncQueueSynchronisation, SynchronisationStageFlags.PixelProcessing);
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x000171DC File Offset: 0x000153DC
		public static GraphicsFence CreateGraphicsFence(GraphicsFenceType fenceType, [DefaultValue("SynchronisationStage.PixelProcessing")] SynchronisationStageFlags stage)
		{
			GraphicsFence graphicsFence = default(GraphicsFence);
			graphicsFence.m_FenceType = fenceType;
			graphicsFence.m_Ptr = Graphics.CreateGPUFenceImpl(fenceType, stage);
			graphicsFence.InitPostAllocation();
			graphicsFence.Validate();
			return graphicsFence;
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x0001721D File Offset: 0x0001541D
		public static void WaitOnAsyncGraphicsFence(GraphicsFence fence)
		{
			Graphics.WaitOnAsyncGraphicsFence(fence, SynchronisationStage.PixelProcessing);
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x00017228 File Offset: 0x00015428
		public static void WaitOnAsyncGraphicsFence(GraphicsFence fence, [DefaultValue("SynchronisationStage.PixelProcessing")] SynchronisationStage stage)
		{
			bool flag = fence.m_FenceType > GraphicsFenceType.AsyncQueueSynchronisation;
			if (flag)
			{
				throw new ArgumentException("Graphics.WaitOnGraphicsFence can only be called with fences created with GraphicsFenceType.AsyncQueueSynchronization.");
			}
			fence.Validate();
			bool flag2 = fence.IsFencePending();
			if (flag2)
			{
				Graphics.WaitOnGPUFenceImpl(fence.m_Ptr, GraphicsFence.TranslateSynchronizationStageToFlags(stage));
			}
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x00017274 File Offset: 0x00015474
		internal static void ValidateCopyBuffer(GraphicsBuffer source, GraphicsBuffer dest)
		{
			bool flag = source == null;
			if (flag)
			{
				throw new ArgumentNullException("source");
			}
			bool flag2 = dest == null;
			if (flag2)
			{
				throw new ArgumentNullException("dest");
			}
			long num = (long)source.count * (long)source.stride;
			long num2 = (long)dest.count * (long)dest.stride;
			bool flag3 = num != num2;
			if (flag3)
			{
				throw new ArgumentException(string.Format("CopyBuffer source and destination buffers must be the same size, source was {0} bytes, dest was {1} bytes", num, num2));
			}
			bool flag4 = (source.target & GraphicsBuffer.Target.CopySource) == (GraphicsBuffer.Target)0;
			if (flag4)
			{
				throw new ArgumentException("CopyBuffer source must have CopySource target", "source");
			}
			bool flag5 = (dest.target & GraphicsBuffer.Target.CopyDestination) == (GraphicsBuffer.Target)0;
			if (flag5)
			{
				throw new ArgumentException("CopyBuffer destination must have CopyDestination target", "dest");
			}
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x00017333 File Offset: 0x00015533
		public static void CopyBuffer(GraphicsBuffer source, GraphicsBuffer dest)
		{
			Graphics.ValidateCopyBuffer(source, dest);
			Graphics.CopyBufferImpl(source, dest);
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x00017348 File Offset: 0x00015548
		private static void DrawTextureImpl(Rect screenRect, Texture texture, Rect sourceRect, int leftBorder, int rightBorder, int topBorder, int bottomBorder, Color color, Material mat, int pass)
		{
			Internal_DrawTextureArguments internal_DrawTextureArguments = default(Internal_DrawTextureArguments);
			internal_DrawTextureArguments.screenRect = screenRect;
			internal_DrawTextureArguments.sourceRect = sourceRect;
			internal_DrawTextureArguments.leftBorder = leftBorder;
			internal_DrawTextureArguments.rightBorder = rightBorder;
			internal_DrawTextureArguments.topBorder = topBorder;
			internal_DrawTextureArguments.bottomBorder = bottomBorder;
			internal_DrawTextureArguments.color = color;
			internal_DrawTextureArguments.leftBorderColor = Color.black;
			internal_DrawTextureArguments.topBorderColor = Color.black;
			internal_DrawTextureArguments.rightBorderColor = Color.black;
			internal_DrawTextureArguments.bottomBorderColor = Color.black;
			internal_DrawTextureArguments.pass = pass;
			internal_DrawTextureArguments.texture = texture;
			internal_DrawTextureArguments.smoothCorners = true;
			internal_DrawTextureArguments.mat = mat;
			Graphics.Internal_DrawTexture(ref internal_DrawTextureArguments);
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x000173F4 File Offset: 0x000155F4
		public static void DrawTexture(Rect screenRect, Texture texture, Rect sourceRect, int leftBorder, int rightBorder, int topBorder, int bottomBorder, Color color, [DefaultValue("null")] Material mat, [DefaultValue("-1")] int pass)
		{
			Graphics.DrawTextureImpl(screenRect, texture, sourceRect, leftBorder, rightBorder, topBorder, bottomBorder, color, mat, pass);
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x00017418 File Offset: 0x00015618
		public static void DrawTexture(Rect screenRect, Texture texture, Rect sourceRect, int leftBorder, int rightBorder, int topBorder, int bottomBorder, [DefaultValue("null")] Material mat, [DefaultValue("-1")] int pass)
		{
			Color32 color = new Color32(128, 128, 128, 128);
			Graphics.DrawTextureImpl(screenRect, texture, sourceRect, leftBorder, rightBorder, topBorder, bottomBorder, color, mat, pass);
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x0001745C File Offset: 0x0001565C
		public static void DrawTexture(Rect screenRect, Texture texture, int leftBorder, int rightBorder, int topBorder, int bottomBorder, [DefaultValue("null")] Material mat, [DefaultValue("-1")] int pass)
		{
			Graphics.DrawTexture(screenRect, texture, new Rect(0f, 0f, 1f, 1f), leftBorder, rightBorder, topBorder, bottomBorder, mat, pass);
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x00017495 File Offset: 0x00015695
		public static void DrawTexture(Rect screenRect, Texture texture, [DefaultValue("null")] Material mat, [DefaultValue("-1")] int pass)
		{
			Graphics.DrawTexture(screenRect, texture, 0, 0, 0, 0, mat, pass);
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x000174A8 File Offset: 0x000156A8
		public unsafe static void RenderMesh(in RenderParams rparams, Mesh mesh, int submeshIndex, Matrix4x4 objectToWorld, [DefaultValue("null")] Matrix4x4? prevObjectToWorld = null)
		{
			bool flag = prevObjectToWorld != null;
			if (flag)
			{
				Matrix4x4 value = prevObjectToWorld.Value;
				Graphics.Internal_RenderMesh(rparams, mesh, submeshIndex, objectToWorld, &value);
			}
			else
			{
				Graphics.Internal_RenderMesh(rparams, mesh, submeshIndex, objectToWorld, null);
			}
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x000174F0 File Offset: 0x000156F0
		internal static RenderInstancedDataLayout GetCachedRenderInstancedDataLayout(Type type)
		{
			int hashCode = type.GetHashCode();
			RenderInstancedDataLayout renderInstancedDataLayout;
			bool flag = !Graphics.s_RenderInstancedDataLayouts.TryGetValue(hashCode, out renderInstancedDataLayout);
			if (flag)
			{
				renderInstancedDataLayout = new RenderInstancedDataLayout(type);
				Graphics.s_RenderInstancedDataLayouts.Add(hashCode, renderInstancedDataLayout);
			}
			return renderInstancedDataLayout;
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x00017538 File Offset: 0x00015738
		public unsafe static void RenderMeshInstanced<[IsUnmanaged] T>(in RenderParams rparams, Mesh mesh, int submeshIndex, T[] instanceData, [DefaultValue("-1")] int instanceCount = -1, [DefaultValue("0")] int startInstance = 0) where T : struct, ValueType
		{
			bool flag = !SystemInfo.supportsInstancing;
			if (flag)
			{
				throw new InvalidOperationException("Instancing is not supported.");
			}
			bool flag2 = !rparams.material.enableInstancing;
			if (flag2)
			{
				throw new InvalidOperationException("Material needs to enable instancing for use with RenderMeshInstanced.");
			}
			bool flag3 = instanceData == null;
			if (flag3)
			{
				throw new ArgumentNullException("instanceData");
			}
			RenderInstancedDataLayout cachedRenderInstancedDataLayout = Graphics.GetCachedRenderInstancedDataLayout(typeof(T));
			uint num = Math.Min((uint)instanceCount, (uint)Math.Max(0, instanceData.Length - startInstance));
			fixed (T[] array = instanceData)
			{
				T* ptr;
				if (instanceData == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				Graphics.Internal_RenderMeshInstanced(rparams, mesh, submeshIndex, (IntPtr)((void*)(ptr + (IntPtr)startInstance * (IntPtr)sizeof(T) / (IntPtr)sizeof(T))), cachedRenderInstancedDataLayout, num);
			}
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x000175F8 File Offset: 0x000157F8
		public unsafe static void RenderMeshInstanced<[IsUnmanaged] T>(in RenderParams rparams, Mesh mesh, int submeshIndex, List<T> instanceData, [DefaultValue("-1")] int instanceCount = -1, [DefaultValue("0")] int startInstance = 0) where T : struct, ValueType
		{
			bool flag = !SystemInfo.supportsInstancing;
			if (flag)
			{
				throw new InvalidOperationException("Instancing is not supported.");
			}
			bool flag2 = !rparams.material.enableInstancing;
			if (flag2)
			{
				throw new InvalidOperationException("Material needs to enable instancing for use with RenderMeshInstanced.");
			}
			bool flag3 = instanceData == null;
			if (flag3)
			{
				throw new ArgumentNullException("instanceData");
			}
			RenderInstancedDataLayout cachedRenderInstancedDataLayout = Graphics.GetCachedRenderInstancedDataLayout(typeof(T));
			uint num = Math.Min((uint)instanceCount, (uint)Math.Max(0, instanceData.Count - startInstance));
			T[] array;
			T* ptr;
			if ((array = NoAllocHelpers.ExtractArrayFromList<T>(instanceData)) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			Graphics.Internal_RenderMeshInstanced(rparams, mesh, submeshIndex, (IntPtr)((void*)(ptr + (IntPtr)startInstance * (IntPtr)sizeof(T) / (IntPtr)sizeof(T))), cachedRenderInstancedDataLayout, num);
			array = null;
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x000176C0 File Offset: 0x000158C0
		public unsafe static void RenderMeshInstanced<[IsUnmanaged] T>(RenderParams rparams, Mesh mesh, int submeshIndex, NativeArray<T> instanceData, [DefaultValue("-1")] int instanceCount = -1, [DefaultValue("0")] int startInstance = 0) where T : struct, ValueType
		{
			bool flag = !SystemInfo.supportsInstancing;
			if (flag)
			{
				throw new InvalidOperationException("Instancing is not supported.");
			}
			bool flag2 = !rparams.material.enableInstancing;
			if (flag2)
			{
				throw new InvalidOperationException("Material needs to enable instancing for use with RenderMeshInstanced.");
			}
			RenderInstancedDataLayout cachedRenderInstancedDataLayout = Graphics.GetCachedRenderInstancedDataLayout(typeof(T));
			uint num = Math.Min((uint)instanceCount, (uint)Math.Max(0, instanceData.Length - startInstance));
			Graphics.Internal_RenderMeshInstanced(rparams, mesh, submeshIndex, (IntPtr)((void*)((byte*)instanceData.GetUnsafePtr<T>() + (IntPtr)startInstance * (IntPtr)sizeof(T))), cachedRenderInstancedDataLayout, num);
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x00017754 File Offset: 0x00015954
		public static void RenderMeshIndirect(in RenderParams rparams, Mesh mesh, GraphicsBuffer commandBuffer, [DefaultValue("1")] int commandCount = 1, [DefaultValue("0")] int startCommand = 0)
		{
			bool flag = !SystemInfo.supportsInstancing;
			if (flag)
			{
				throw new InvalidOperationException("Instancing is not supported.");
			}
			bool flag2 = !SystemInfo.supportsIndirectArgumentsBuffer;
			if (flag2)
			{
				throw new InvalidOperationException("Indirect argument buffers are not supported.");
			}
			Graphics.Internal_RenderMeshIndirect(rparams, mesh, commandBuffer, commandCount, startCommand);
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x000177A4 File Offset: 0x000159A4
		public static void RenderMeshPrimitives(in RenderParams rparams, Mesh mesh, int submeshIndex, [DefaultValue("1")] int instanceCount = 1)
		{
			bool flag = !SystemInfo.supportsInstancing;
			if (flag)
			{
				throw new InvalidOperationException("Instancing is not supported.");
			}
			Graphics.Internal_RenderMeshPrimitives(rparams, mesh, submeshIndex, instanceCount);
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x000177D8 File Offset: 0x000159D8
		public static void RenderPrimitives(in RenderParams rparams, MeshTopology topology, int vertexCount, [DefaultValue("1")] int instanceCount = 1)
		{
			bool flag = !SystemInfo.supportsInstancing;
			if (flag)
			{
				throw new InvalidOperationException("Instancing is not supported.");
			}
			Graphics.Internal_RenderPrimitives(rparams, topology, vertexCount, instanceCount);
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x0001780C File Offset: 0x00015A0C
		public static void RenderPrimitivesIndexed(in RenderParams rparams, MeshTopology topology, GraphicsBuffer indexBuffer, int indexCount, [DefaultValue("0")] int startIndex = 0, [DefaultValue("1")] int instanceCount = 1)
		{
			bool flag = !SystemInfo.supportsInstancing;
			if (flag)
			{
				throw new InvalidOperationException("Instancing is not supported.");
			}
			Graphics.Internal_RenderPrimitivesIndexed(rparams, topology, indexBuffer, indexCount, startIndex, instanceCount);
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x00017844 File Offset: 0x00015A44
		public static void RenderPrimitivesIndirect(in RenderParams rparams, MeshTopology topology, GraphicsBuffer commandBuffer, [DefaultValue("1")] int commandCount = 1, [DefaultValue("0")] int startCommand = 0)
		{
			bool flag = !SystemInfo.supportsInstancing;
			if (flag)
			{
				throw new InvalidOperationException("Instancing is not supported.");
			}
			bool flag2 = !SystemInfo.supportsIndirectArgumentsBuffer;
			if (flag2)
			{
				throw new InvalidOperationException("Indirect argument buffers are not supported.");
			}
			Graphics.Internal_RenderPrimitivesIndirect(rparams, topology, commandBuffer, commandCount, startCommand);
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x00017894 File Offset: 0x00015A94
		public static void RenderPrimitivesIndexedIndirect(in RenderParams rparams, MeshTopology topology, GraphicsBuffer indexBuffer, GraphicsBuffer commandBuffer, [DefaultValue("1")] int commandCount = 1, [DefaultValue("0")] int startCommand = 0)
		{
			bool flag = !SystemInfo.supportsInstancing;
			if (flag)
			{
				throw new InvalidOperationException("Instancing is not supported.");
			}
			bool flag2 = !SystemInfo.supportsIndirectArgumentsBuffer;
			if (flag2)
			{
				throw new InvalidOperationException("Indirect argument buffers are not supported.");
			}
			Graphics.Internal_RenderPrimitivesIndexedIndirect(rparams, topology, indexBuffer, commandBuffer, commandCount, startCommand);
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x000178E4 File Offset: 0x00015AE4
		public static void DrawMeshNow(Mesh mesh, Vector3 position, Quaternion rotation, int materialIndex)
		{
			bool flag = mesh == null;
			if (flag)
			{
				throw new ArgumentNullException("mesh");
			}
			Graphics.Internal_DrawMeshNow1(mesh, materialIndex, position, rotation);
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x00017914 File Offset: 0x00015B14
		public static void DrawMeshNow(Mesh mesh, Matrix4x4 matrix, int materialIndex)
		{
			bool flag = mesh == null;
			if (flag)
			{
				throw new ArgumentNullException("mesh");
			}
			Graphics.Internal_DrawMeshNow2(mesh, materialIndex, matrix);
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x00017941 File Offset: 0x00015B41
		public static void DrawMeshNow(Mesh mesh, Vector3 position, Quaternion rotation)
		{
			Graphics.DrawMeshNow(mesh, position, rotation, -1);
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x0001794E File Offset: 0x00015B4E
		public static void DrawMeshNow(Mesh mesh, Matrix4x4 matrix)
		{
			Graphics.DrawMeshNow(mesh, matrix, -1);
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x0001795C File Offset: 0x00015B5C
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, [DefaultValue("null")] Camera camera, [DefaultValue("0")] int submeshIndex, [DefaultValue("null")] MaterialPropertyBlock properties, [DefaultValue("true")] bool castShadows, [DefaultValue("true")] bool receiveShadows, [DefaultValue("true")] bool useLightProbes)
		{
			Graphics.DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, camera, submeshIndex, properties, castShadows ? ShadowCastingMode.On : ShadowCastingMode.Off, receiveShadows, null, useLightProbes ? LightProbeUsage.BlendProbes : LightProbeUsage.Off, null);
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x0001799C File Offset: 0x00015B9C
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows, [DefaultValue("true")] bool receiveShadows, [DefaultValue("null")] Transform probeAnchor, [DefaultValue("true")] bool useLightProbes)
		{
			Graphics.DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, camera, submeshIndex, properties, castShadows, receiveShadows, probeAnchor, useLightProbes ? LightProbeUsage.BlendProbes : LightProbeUsage.Off, null);
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x000179D8 File Offset: 0x00015BD8
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, [DefaultValue("null")] Camera camera, [DefaultValue("0")] int submeshIndex, [DefaultValue("null")] MaterialPropertyBlock properties, [DefaultValue("true")] bool castShadows, [DefaultValue("true")] bool receiveShadows, [DefaultValue("true")] bool useLightProbes)
		{
			Graphics.DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, properties, castShadows ? ShadowCastingMode.On : ShadowCastingMode.Off, receiveShadows, null, useLightProbes ? LightProbeUsage.BlendProbes : LightProbeUsage.Off, null);
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x00017A0C File Offset: 0x00015C0C
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, Transform probeAnchor, LightProbeUsage lightProbeUsage, [DefaultValue("null")] LightProbeProxyVolume lightProbeProxyVolume)
		{
			bool flag = lightProbeUsage == LightProbeUsage.UseProxyVolume && lightProbeProxyVolume == null;
			if (flag)
			{
				throw new ArgumentException("Argument lightProbeProxyVolume must not be null if lightProbeUsage is set to UseProxyVolume.", "lightProbeProxyVolume");
			}
			Graphics.Internal_DrawMesh(mesh, submeshIndex, matrix, material, layer, camera, properties, castShadows, receiveShadows, probeAnchor, lightProbeUsage, lightProbeProxyVolume);
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x00017A58 File Offset: 0x00015C58
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, Matrix4x4[] matrices, [DefaultValue("matrices.Length")] int count, [DefaultValue("null")] MaterialPropertyBlock properties, [DefaultValue("ShadowCastingMode.On")] ShadowCastingMode castShadows, [DefaultValue("true")] bool receiveShadows, [DefaultValue("0")] int layer, [DefaultValue("null")] Camera camera, [DefaultValue("LightProbeUsage.BlendProbes")] LightProbeUsage lightProbeUsage, [DefaultValue("null")] LightProbeProxyVolume lightProbeProxyVolume)
		{
			bool flag = !SystemInfo.supportsInstancing;
			if (flag)
			{
				throw new InvalidOperationException("Instancing is not supported.");
			}
			bool flag2 = mesh == null;
			if (flag2)
			{
				throw new ArgumentNullException("mesh");
			}
			bool flag3 = submeshIndex < 0 || submeshIndex >= mesh.subMeshCount;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("submeshIndex", "submeshIndex out of range.");
			}
			bool flag4 = material == null;
			if (flag4)
			{
				throw new ArgumentNullException("material");
			}
			bool flag5 = !material.enableInstancing;
			if (flag5)
			{
				throw new InvalidOperationException("Material needs to enable instancing for use with DrawMeshInstanced.");
			}
			bool flag6 = matrices == null;
			if (flag6)
			{
				throw new ArgumentNullException("matrices");
			}
			bool flag7 = count < 0 || count > Mathf.Min(Graphics.kMaxDrawMeshInstanceCount, matrices.Length);
			if (flag7)
			{
				throw new ArgumentOutOfRangeException("count", string.Format("Count must be in the range of 0 to {0}.", Mathf.Min(Graphics.kMaxDrawMeshInstanceCount, matrices.Length)));
			}
			bool flag8 = lightProbeUsage == LightProbeUsage.UseProxyVolume && lightProbeProxyVolume == null;
			if (flag8)
			{
				throw new ArgumentException("Argument lightProbeProxyVolume must not be null if lightProbeUsage is set to UseProxyVolume.", "lightProbeProxyVolume");
			}
			bool flag9 = count > 0;
			if (flag9)
			{
				Graphics.Internal_DrawMeshInstanced(mesh, submeshIndex, material, matrices, count, properties, castShadows, receiveShadows, layer, camera, lightProbeUsage, lightProbeProxyVolume);
			}
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x00017B90 File Offset: 0x00015D90
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, List<Matrix4x4> matrices, [DefaultValue("null")] MaterialPropertyBlock properties, [DefaultValue("ShadowCastingMode.On")] ShadowCastingMode castShadows, [DefaultValue("true")] bool receiveShadows, [DefaultValue("0")] int layer, [DefaultValue("null")] Camera camera, [DefaultValue("LightProbeUsage.BlendProbes")] LightProbeUsage lightProbeUsage, [DefaultValue("null")] LightProbeProxyVolume lightProbeProxyVolume)
		{
			bool flag = matrices == null;
			if (flag)
			{
				throw new ArgumentNullException("matrices");
			}
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, NoAllocHelpers.ExtractArrayFromList<Matrix4x4>(matrices), matrices.Count, properties, castShadows, receiveShadows, layer, camera, lightProbeUsage, lightProbeProxyVolume);
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x00017BD4 File Offset: 0x00015DD4
		public static void DrawMeshInstancedProcedural(Mesh mesh, int submeshIndex, Material material, Bounds bounds, int count, MaterialPropertyBlock properties = null, ShadowCastingMode castShadows = ShadowCastingMode.On, bool receiveShadows = true, int layer = 0, Camera camera = null, LightProbeUsage lightProbeUsage = LightProbeUsage.BlendProbes, LightProbeProxyVolume lightProbeProxyVolume = null)
		{
			bool flag = !SystemInfo.supportsInstancing;
			if (flag)
			{
				throw new InvalidOperationException("Instancing is not supported.");
			}
			bool flag2 = mesh == null;
			if (flag2)
			{
				throw new ArgumentNullException("mesh");
			}
			bool flag3 = submeshIndex < 0 || submeshIndex >= mesh.subMeshCount;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("submeshIndex", "submeshIndex out of range.");
			}
			bool flag4 = material == null;
			if (flag4)
			{
				throw new ArgumentNullException("material");
			}
			bool flag5 = count <= 0;
			if (flag5)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			bool flag6 = lightProbeUsage == LightProbeUsage.UseProxyVolume && lightProbeProxyVolume == null;
			if (flag6)
			{
				throw new ArgumentException("Argument lightProbeProxyVolume must not be null if lightProbeUsage is set to UseProxyVolume.", "lightProbeProxyVolume");
			}
			bool flag7 = count > 0;
			if (flag7)
			{
				Graphics.Internal_DrawMeshInstancedProcedural(mesh, submeshIndex, material, bounds, count, properties, castShadows, receiveShadows, layer, camera, lightProbeUsage, lightProbeProxyVolume);
			}
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x00017CB0 File Offset: 0x00015EB0
		public static void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, Bounds bounds, ComputeBuffer bufferWithArgs, [DefaultValue("0")] int argsOffset, [DefaultValue("null")] MaterialPropertyBlock properties, [DefaultValue("ShadowCastingMode.On")] ShadowCastingMode castShadows, [DefaultValue("true")] bool receiveShadows, [DefaultValue("0")] int layer, [DefaultValue("null")] Camera camera, [DefaultValue("LightProbeUsage.BlendProbes")] LightProbeUsage lightProbeUsage, [DefaultValue("null")] LightProbeProxyVolume lightProbeProxyVolume)
		{
			bool flag = !SystemInfo.supportsInstancing;
			if (flag)
			{
				throw new InvalidOperationException("Instancing is not supported.");
			}
			bool flag2 = !SystemInfo.supportsIndirectArgumentsBuffer;
			if (flag2)
			{
				throw new InvalidOperationException("Indirect argument buffers are not supported.");
			}
			bool flag3 = mesh == null;
			if (flag3)
			{
				throw new ArgumentNullException("mesh");
			}
			bool flag4 = submeshIndex < 0 || submeshIndex >= mesh.subMeshCount;
			if (flag4)
			{
				throw new ArgumentOutOfRangeException("submeshIndex", "submeshIndex out of range.");
			}
			bool flag5 = material == null;
			if (flag5)
			{
				throw new ArgumentNullException("material");
			}
			bool flag6 = bufferWithArgs == null;
			if (flag6)
			{
				throw new ArgumentNullException("bufferWithArgs");
			}
			bool flag7 = lightProbeUsage == LightProbeUsage.UseProxyVolume && lightProbeProxyVolume == null;
			if (flag7)
			{
				throw new ArgumentException("Argument lightProbeProxyVolume must not be null if lightProbeUsage is set to UseProxyVolume.", "lightProbeProxyVolume");
			}
			Graphics.Internal_DrawMeshInstancedIndirect(mesh, submeshIndex, material, bounds, bufferWithArgs, argsOffset, properties, castShadows, receiveShadows, layer, camera, lightProbeUsage, lightProbeProxyVolume);
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x00017D9C File Offset: 0x00015F9C
		public static void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, Bounds bounds, GraphicsBuffer bufferWithArgs, [DefaultValue("0")] int argsOffset, [DefaultValue("null")] MaterialPropertyBlock properties, [DefaultValue("ShadowCastingMode.On")] ShadowCastingMode castShadows, [DefaultValue("true")] bool receiveShadows, [DefaultValue("0")] int layer, [DefaultValue("null")] Camera camera, [DefaultValue("LightProbeUsage.BlendProbes")] LightProbeUsage lightProbeUsage, [DefaultValue("null")] LightProbeProxyVolume lightProbeProxyVolume)
		{
			bool flag = !SystemInfo.supportsInstancing;
			if (flag)
			{
				throw new InvalidOperationException("Instancing is not supported.");
			}
			bool flag2 = !SystemInfo.supportsIndirectArgumentsBuffer;
			if (flag2)
			{
				throw new InvalidOperationException("Indirect argument buffers are not supported.");
			}
			bool flag3 = mesh == null;
			if (flag3)
			{
				throw new ArgumentNullException("mesh");
			}
			bool flag4 = submeshIndex < 0 || submeshIndex >= mesh.subMeshCount;
			if (flag4)
			{
				throw new ArgumentOutOfRangeException("submeshIndex", "submeshIndex out of range.");
			}
			bool flag5 = material == null;
			if (flag5)
			{
				throw new ArgumentNullException("material");
			}
			bool flag6 = bufferWithArgs == null;
			if (flag6)
			{
				throw new ArgumentNullException("bufferWithArgs");
			}
			bool flag7 = lightProbeUsage == LightProbeUsage.UseProxyVolume && lightProbeProxyVolume == null;
			if (flag7)
			{
				throw new ArgumentException("Argument lightProbeProxyVolume must not be null if lightProbeUsage is set to UseProxyVolume.", "lightProbeProxyVolume");
			}
			Graphics.Internal_DrawMeshInstancedIndirectGraphicsBuffer(mesh, submeshIndex, material, bounds, bufferWithArgs, argsOffset, properties, castShadows, receiveShadows, layer, camera, lightProbeUsage, lightProbeProxyVolume);
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x00017E85 File Offset: 0x00016085
		public static void DrawProceduralNow(MeshTopology topology, int vertexCount, int instanceCount = 1)
		{
			Graphics.Internal_DrawProceduralNow(topology, vertexCount, instanceCount);
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x00017E94 File Offset: 0x00016094
		public static void DrawProceduralNow(MeshTopology topology, GraphicsBuffer indexBuffer, int indexCount, int instanceCount = 1)
		{
			bool flag = indexBuffer == null;
			if (flag)
			{
				throw new ArgumentNullException("indexBuffer");
			}
			Graphics.Internal_DrawProceduralIndexedNow(topology, indexBuffer, indexCount, instanceCount);
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x00017EC0 File Offset: 0x000160C0
		public static void DrawProceduralIndirectNow(MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset = 0)
		{
			bool flag = !SystemInfo.supportsIndirectArgumentsBuffer;
			if (flag)
			{
				throw new InvalidOperationException("Indirect argument buffers are not supported.");
			}
			bool flag2 = bufferWithArgs == null;
			if (flag2)
			{
				throw new ArgumentNullException("bufferWithArgs");
			}
			Graphics.Internal_DrawProceduralIndirectNow(topology, bufferWithArgs, argsOffset);
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x00017F04 File Offset: 0x00016104
		public static void DrawProceduralIndirectNow(MeshTopology topology, GraphicsBuffer indexBuffer, ComputeBuffer bufferWithArgs, int argsOffset = 0)
		{
			bool flag = !SystemInfo.supportsIndirectArgumentsBuffer;
			if (flag)
			{
				throw new InvalidOperationException("Indirect argument buffers are not supported.");
			}
			bool flag2 = indexBuffer == null;
			if (flag2)
			{
				throw new ArgumentNullException("indexBuffer");
			}
			bool flag3 = bufferWithArgs == null;
			if (flag3)
			{
				throw new ArgumentNullException("bufferWithArgs");
			}
			Graphics.Internal_DrawProceduralIndexedIndirectNow(topology, indexBuffer, bufferWithArgs, argsOffset);
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x00017F5C File Offset: 0x0001615C
		public static void DrawProceduralIndirectNow(MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset = 0)
		{
			bool flag = !SystemInfo.supportsIndirectArgumentsBuffer;
			if (flag)
			{
				throw new InvalidOperationException("Indirect argument buffers are not supported.");
			}
			bool flag2 = bufferWithArgs == null;
			if (flag2)
			{
				throw new ArgumentNullException("bufferWithArgs");
			}
			Graphics.Internal_DrawProceduralIndirectNowGraphicsBuffer(topology, bufferWithArgs, argsOffset);
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x00017FA0 File Offset: 0x000161A0
		public static void DrawProceduralIndirectNow(MeshTopology topology, GraphicsBuffer indexBuffer, GraphicsBuffer bufferWithArgs, int argsOffset = 0)
		{
			bool flag = !SystemInfo.supportsIndirectArgumentsBuffer;
			if (flag)
			{
				throw new InvalidOperationException("Indirect argument buffers are not supported.");
			}
			bool flag2 = indexBuffer == null;
			if (flag2)
			{
				throw new ArgumentNullException("indexBuffer");
			}
			bool flag3 = bufferWithArgs == null;
			if (flag3)
			{
				throw new ArgumentNullException("bufferWithArgs");
			}
			Graphics.Internal_DrawProceduralIndexedIndirectNowGraphicsBuffer(topology, indexBuffer, bufferWithArgs, argsOffset);
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x00017FF8 File Offset: 0x000161F8
		public static void DrawProcedural(Material material, Bounds bounds, MeshTopology topology, int vertexCount, int instanceCount = 1, Camera camera = null, MaterialPropertyBlock properties = null, ShadowCastingMode castShadows = ShadowCastingMode.On, bool receiveShadows = true, int layer = 0)
		{
			Graphics.Internal_DrawProcedural(material, bounds, topology, vertexCount, instanceCount, camera, properties, castShadows, receiveShadows, layer);
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x0001801C File Offset: 0x0001621C
		public static void DrawProcedural(Material material, Bounds bounds, MeshTopology topology, GraphicsBuffer indexBuffer, int indexCount, int instanceCount = 1, Camera camera = null, MaterialPropertyBlock properties = null, ShadowCastingMode castShadows = ShadowCastingMode.On, bool receiveShadows = true, int layer = 0)
		{
			bool flag = indexBuffer == null;
			if (flag)
			{
				throw new ArgumentNullException("indexBuffer");
			}
			Graphics.Internal_DrawProceduralIndexed(material, bounds, topology, indexBuffer, indexCount, instanceCount, camera, properties, castShadows, receiveShadows, layer);
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x00018058 File Offset: 0x00016258
		public static void DrawProceduralIndirect(Material material, Bounds bounds, MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset = 0, Camera camera = null, MaterialPropertyBlock properties = null, ShadowCastingMode castShadows = ShadowCastingMode.On, bool receiveShadows = true, int layer = 0)
		{
			bool flag = !SystemInfo.supportsIndirectArgumentsBuffer;
			if (flag)
			{
				throw new InvalidOperationException("Indirect argument buffers are not supported.");
			}
			bool flag2 = bufferWithArgs == null;
			if (flag2)
			{
				throw new ArgumentNullException("bufferWithArgs");
			}
			Graphics.Internal_DrawProceduralIndirect(material, bounds, topology, bufferWithArgs, argsOffset, camera, properties, castShadows, receiveShadows, layer);
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x000180A8 File Offset: 0x000162A8
		public static void DrawProceduralIndirect(Material material, Bounds bounds, MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset = 0, Camera camera = null, MaterialPropertyBlock properties = null, ShadowCastingMode castShadows = ShadowCastingMode.On, bool receiveShadows = true, int layer = 0)
		{
			bool flag = !SystemInfo.supportsIndirectArgumentsBuffer;
			if (flag)
			{
				throw new InvalidOperationException("Indirect argument buffers are not supported.");
			}
			bool flag2 = bufferWithArgs == null;
			if (flag2)
			{
				throw new ArgumentNullException("bufferWithArgs");
			}
			Graphics.Internal_DrawProceduralIndirectGraphicsBuffer(material, bounds, topology, bufferWithArgs, argsOffset, camera, properties, castShadows, receiveShadows, layer);
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x000180F8 File Offset: 0x000162F8
		public static void DrawProceduralIndirect(Material material, Bounds bounds, MeshTopology topology, GraphicsBuffer indexBuffer, ComputeBuffer bufferWithArgs, int argsOffset = 0, Camera camera = null, MaterialPropertyBlock properties = null, ShadowCastingMode castShadows = ShadowCastingMode.On, bool receiveShadows = true, int layer = 0)
		{
			bool flag = !SystemInfo.supportsIndirectArgumentsBuffer;
			if (flag)
			{
				throw new InvalidOperationException("Indirect argument buffers are not supported.");
			}
			bool flag2 = indexBuffer == null;
			if (flag2)
			{
				throw new ArgumentNullException("indexBuffer");
			}
			bool flag3 = bufferWithArgs == null;
			if (flag3)
			{
				throw new ArgumentNullException("bufferWithArgs");
			}
			Graphics.Internal_DrawProceduralIndexedIndirect(material, bounds, topology, indexBuffer, bufferWithArgs, argsOffset, camera, properties, castShadows, receiveShadows, layer);
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x0001815C File Offset: 0x0001635C
		public static void DrawProceduralIndirect(Material material, Bounds bounds, MeshTopology topology, GraphicsBuffer indexBuffer, GraphicsBuffer bufferWithArgs, int argsOffset = 0, Camera camera = null, MaterialPropertyBlock properties = null, ShadowCastingMode castShadows = ShadowCastingMode.On, bool receiveShadows = true, int layer = 0)
		{
			bool flag = !SystemInfo.supportsIndirectArgumentsBuffer;
			if (flag)
			{
				throw new InvalidOperationException("Indirect argument buffers are not supported.");
			}
			bool flag2 = indexBuffer == null;
			if (flag2)
			{
				throw new ArgumentNullException("indexBuffer");
			}
			bool flag3 = bufferWithArgs == null;
			if (flag3)
			{
				throw new ArgumentNullException("bufferWithArgs");
			}
			Graphics.Internal_DrawProceduralIndexedIndirectGraphicsBuffer(material, bounds, topology, indexBuffer, bufferWithArgs, argsOffset, camera, properties, castShadows, receiveShadows, layer);
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x000181C0 File Offset: 0x000163C0
		public static void Blit(Texture source, RenderTexture dest)
		{
			Graphics.Blit2(source, dest);
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x000181CB File Offset: 0x000163CB
		public static void Blit(Texture source, RenderTexture dest, int sourceDepthSlice, int destDepthSlice)
		{
			Graphics.Blit3(source, dest, sourceDepthSlice, destDepthSlice);
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x000181D8 File Offset: 0x000163D8
		public static void Blit(Texture source, RenderTexture dest, Vector2 scale, Vector2 offset)
		{
			Graphics.Blit4(source, dest, scale, offset);
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x000181E5 File Offset: 0x000163E5
		public static void Blit(Texture source, RenderTexture dest, Vector2 scale, Vector2 offset, int sourceDepthSlice, int destDepthSlice)
		{
			Graphics.Blit5(source, dest, scale, offset, sourceDepthSlice, destDepthSlice);
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x000181F6 File Offset: 0x000163F6
		public static void Blit(Texture source, RenderTexture dest, Material mat, [DefaultValue("-1")] int pass)
		{
			Graphics.Internal_BlitMaterial5(source, dest, mat, pass, true);
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x00018204 File Offset: 0x00016404
		public static void Blit(Texture source, RenderTexture dest, Material mat, int pass, int destDepthSlice)
		{
			Graphics.Internal_BlitMaterial6(source, dest, mat, pass, true, destDepthSlice);
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x00018214 File Offset: 0x00016414
		public static void Blit(Texture source, RenderTexture dest, Material mat)
		{
			Graphics.Blit(source, dest, mat, -1);
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x00018221 File Offset: 0x00016421
		public static void Blit(Texture source, Material mat, [DefaultValue("-1")] int pass)
		{
			Graphics.Internal_BlitMaterial5(source, null, mat, pass, false);
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x0001822F File Offset: 0x0001642F
		public static void Blit(Texture source, Material mat, int pass, int destDepthSlice)
		{
			Graphics.Internal_BlitMaterial6(source, null, mat, pass, false, destDepthSlice);
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x0001823E File Offset: 0x0001643E
		public static void Blit(Texture source, Material mat)
		{
			Graphics.Blit(source, mat, -1);
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x0001824C File Offset: 0x0001644C
		public static void BlitMultiTap(Texture source, RenderTexture dest, Material mat, params Vector2[] offsets)
		{
			bool flag = offsets.Length == 0;
			if (flag)
			{
				throw new ArgumentException("empty offsets list passed.", "offsets");
			}
			Graphics.Internal_BlitMultiTap4(source, dest, mat, offsets);
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x00018280 File Offset: 0x00016480
		public static void BlitMultiTap(Texture source, RenderTexture dest, Material mat, int destDepthSlice, params Vector2[] offsets)
		{
			bool flag = offsets.Length == 0;
			if (flag)
			{
				throw new ArgumentException("empty offsets list passed.", "offsets");
			}
			Graphics.Internal_BlitMultiTap5(source, dest, mat, offsets, destDepthSlice);
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x000182B4 File Offset: 0x000164B4
		public static void Blit(Texture source, GraphicsTexture dest)
		{
			Graphics.BlitGfx2(source, dest);
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x000182BF File Offset: 0x000164BF
		public static void Blit(Texture source, GraphicsTexture dest, int sourceDepthSlice, int destDepthSlice)
		{
			Graphics.BlitGfx3(source, dest, sourceDepthSlice, destDepthSlice);
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x000182CC File Offset: 0x000164CC
		public static void Blit(Texture source, GraphicsTexture dest, Vector2 scale, Vector2 offset)
		{
			Graphics.BlitGfx4(source, dest, scale, offset);
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x000182D9 File Offset: 0x000164D9
		public static void Blit(Texture source, GraphicsTexture dest, Vector2 scale, Vector2 offset, int sourceDepthSlice, int destDepthSlice)
		{
			Graphics.BlitGfx5(source, dest, scale, offset, sourceDepthSlice, destDepthSlice);
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x000182EA File Offset: 0x000164EA
		public static void Blit(Texture source, GraphicsTexture dest, Material mat, [DefaultValue("-1")] int pass)
		{
			Graphics.Internal_BlitMaterialGfx5(source, dest, mat, pass, true);
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x000182F8 File Offset: 0x000164F8
		public static void Blit(Texture source, GraphicsTexture dest, Material mat, int pass, int destDepthSlice)
		{
			Graphics.Internal_BlitMaterialGfx6(source, dest, mat, pass, true, destDepthSlice);
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x00018308 File Offset: 0x00016508
		public static void Blit(Texture source, GraphicsTexture dest, Material mat)
		{
			Graphics.Blit(source, dest, mat, -1);
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x00018318 File Offset: 0x00016518
		public static void BlitMultiTap(Texture source, GraphicsTexture dest, Material mat, params Vector2[] offsets)
		{
			bool flag = offsets.Length == 0;
			if (flag)
			{
				throw new ArgumentException("empty offsets list passed.", "offsets");
			}
			Graphics.Internal_BlitMultiTapGfx4(source, dest, mat, offsets);
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x0001834C File Offset: 0x0001654C
		public static void BlitMultiTap(Texture source, GraphicsTexture dest, Material mat, int destDepthSlice, params Vector2[] offsets)
		{
			bool flag = offsets.Length == 0;
			if (flag)
			{
				throw new ArgumentException("empty offsets list passed.", "offsets");
			}
			Graphics.Internal_BlitMultiTapGfx5(source, dest, mat, offsets, destDepthSlice);
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x00018380 File Offset: 0x00016580
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer)
		{
			Graphics.DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, null, 0, null, ShadowCastingMode.On, true, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x000183AC File Offset: 0x000165AC
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera)
		{
			Graphics.DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, camera, 0, null, ShadowCastingMode.On, true, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x000183DC File Offset: 0x000165DC
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera, int submeshIndex)
		{
			Graphics.DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, camera, submeshIndex, null, ShadowCastingMode.On, true, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x0001840C File Offset: 0x0001660C
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties)
		{
			Graphics.DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, camera, submeshIndex, properties, ShadowCastingMode.On, true, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x0001843C File Offset: 0x0001663C
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, bool castShadows)
		{
			Graphics.DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, camera, submeshIndex, properties, castShadows ? ShadowCastingMode.On : ShadowCastingMode.Off, true, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x00018474 File Offset: 0x00016674
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, bool castShadows, bool receiveShadows)
		{
			Graphics.DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, camera, submeshIndex, properties, castShadows ? ShadowCastingMode.On : ShadowCastingMode.Off, receiveShadows, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x000184AC File Offset: 0x000166AC
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows)
		{
			Graphics.DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, camera, submeshIndex, properties, castShadows, true, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x000184DC File Offset: 0x000166DC
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows)
		{
			Graphics.DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, camera, submeshIndex, properties, castShadows, receiveShadows, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x00018510 File Offset: 0x00016710
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, Transform probeAnchor)
		{
			Graphics.DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, camera, submeshIndex, properties, castShadows, receiveShadows, probeAnchor, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x00018544 File Offset: 0x00016744
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer)
		{
			Graphics.DrawMesh(mesh, matrix, material, layer, null, 0, null, ShadowCastingMode.On, true, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x00018564 File Offset: 0x00016764
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera)
		{
			Graphics.DrawMesh(mesh, matrix, material, layer, camera, 0, null, ShadowCastingMode.On, true, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x00018588 File Offset: 0x00016788
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex)
		{
			Graphics.DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, null, ShadowCastingMode.On, true, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x000185AC File Offset: 0x000167AC
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties)
		{
			Graphics.DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, properties, ShadowCastingMode.On, true, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x000185D0 File Offset: 0x000167D0
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, bool castShadows)
		{
			Graphics.DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, properties, castShadows ? ShadowCastingMode.On : ShadowCastingMode.Off, true, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x000185FC File Offset: 0x000167FC
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, bool castShadows, bool receiveShadows)
		{
			Graphics.DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, properties, castShadows ? ShadowCastingMode.On : ShadowCastingMode.Off, receiveShadows, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x00018628 File Offset: 0x00016828
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows)
		{
			Graphics.DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, properties, castShadows, true, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x0001864C File Offset: 0x0001684C
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows)
		{
			Graphics.DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, properties, castShadows, receiveShadows, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x00018674 File Offset: 0x00016874
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, Transform probeAnchor)
		{
			Graphics.DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, properties, castShadows, receiveShadows, probeAnchor, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x0001869C File Offset: 0x0001689C
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows, [DefaultValue("true")] bool receiveShadows, [DefaultValue("null")] Transform probeAnchor, [DefaultValue("true")] bool useLightProbes)
		{
			Graphics.DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, properties, castShadows, receiveShadows, probeAnchor, useLightProbes ? LightProbeUsage.BlendProbes : LightProbeUsage.Off, null);
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x000186CC File Offset: 0x000168CC
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, Transform probeAnchor, LightProbeUsage lightProbeUsage)
		{
			Graphics.Internal_DrawMesh(mesh, submeshIndex, matrix, material, layer, camera, properties, castShadows, receiveShadows, probeAnchor, lightProbeUsage, null);
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x000186F4 File Offset: 0x000168F4
		[ExcludeFromDocs]
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, Matrix4x4[] matrices)
		{
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, matrices, matrices.Length, null, ShadowCastingMode.On, true, 0, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x00018718 File Offset: 0x00016918
		[ExcludeFromDocs]
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, Matrix4x4[] matrices, int count)
		{
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, matrices, count, null, ShadowCastingMode.On, true, 0, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x0001873C File Offset: 0x0001693C
		[ExcludeFromDocs]
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, Matrix4x4[] matrices, int count, MaterialPropertyBlock properties)
		{
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, matrices, count, properties, ShadowCastingMode.On, true, 0, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x00018760 File Offset: 0x00016960
		[ExcludeFromDocs]
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, Matrix4x4[] matrices, int count, MaterialPropertyBlock properties, ShadowCastingMode castShadows)
		{
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, matrices, count, properties, castShadows, true, 0, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x00018784 File Offset: 0x00016984
		[ExcludeFromDocs]
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, Matrix4x4[] matrices, int count, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows)
		{
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, matrices, count, properties, castShadows, receiveShadows, 0, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x000187A8 File Offset: 0x000169A8
		[ExcludeFromDocs]
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, Matrix4x4[] matrices, int count, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer)
		{
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, matrices, count, properties, castShadows, receiveShadows, layer, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x000187D0 File Offset: 0x000169D0
		[ExcludeFromDocs]
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, Matrix4x4[] matrices, int count, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer, Camera camera)
		{
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, matrices, count, properties, castShadows, receiveShadows, layer, camera, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x000187F8 File Offset: 0x000169F8
		[ExcludeFromDocs]
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, Matrix4x4[] matrices, int count, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer, Camera camera, LightProbeUsage lightProbeUsage)
		{
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, matrices, count, properties, castShadows, receiveShadows, layer, camera, lightProbeUsage, null);
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x00018820 File Offset: 0x00016A20
		[ExcludeFromDocs]
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, List<Matrix4x4> matrices)
		{
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, matrices, null, ShadowCastingMode.On, true, 0, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x00018840 File Offset: 0x00016A40
		[ExcludeFromDocs]
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, List<Matrix4x4> matrices, MaterialPropertyBlock properties)
		{
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, matrices, properties, ShadowCastingMode.On, true, 0, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x00018860 File Offset: 0x00016A60
		[ExcludeFromDocs]
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, List<Matrix4x4> matrices, MaterialPropertyBlock properties, ShadowCastingMode castShadows)
		{
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, matrices, properties, castShadows, true, 0, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x00018884 File Offset: 0x00016A84
		[ExcludeFromDocs]
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, List<Matrix4x4> matrices, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows)
		{
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, matrices, properties, castShadows, receiveShadows, 0, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x000188A8 File Offset: 0x00016AA8
		[ExcludeFromDocs]
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, List<Matrix4x4> matrices, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer)
		{
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, matrices, properties, castShadows, receiveShadows, layer, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x000188CC File Offset: 0x00016ACC
		[ExcludeFromDocs]
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, List<Matrix4x4> matrices, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer, Camera camera)
		{
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, matrices, properties, castShadows, receiveShadows, layer, camera, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x000188F0 File Offset: 0x00016AF0
		[ExcludeFromDocs]
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, List<Matrix4x4> matrices, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer, Camera camera, LightProbeUsage lightProbeUsage)
		{
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, matrices, properties, castShadows, receiveShadows, layer, camera, lightProbeUsage, null);
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x00018918 File Offset: 0x00016B18
		[ExcludeFromDocs]
		public static void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, Bounds bounds, ComputeBuffer bufferWithArgs, int argsOffset = 0, MaterialPropertyBlock properties = null, ShadowCastingMode castShadows = ShadowCastingMode.On, bool receiveShadows = true, int layer = 0, Camera camera = null, LightProbeUsage lightProbeUsage = LightProbeUsage.BlendProbes)
		{
			Graphics.DrawMeshInstancedIndirect(mesh, submeshIndex, material, bounds, bufferWithArgs, argsOffset, properties, castShadows, receiveShadows, layer, camera, lightProbeUsage, null);
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x00018944 File Offset: 0x00016B44
		[ExcludeFromDocs]
		public static void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, Bounds bounds, GraphicsBuffer bufferWithArgs, int argsOffset = 0, MaterialPropertyBlock properties = null, ShadowCastingMode castShadows = ShadowCastingMode.On, bool receiveShadows = true, int layer = 0, Camera camera = null, LightProbeUsage lightProbeUsage = LightProbeUsage.BlendProbes)
		{
			Graphics.DrawMeshInstancedIndirect(mesh, submeshIndex, material, bounds, bufferWithArgs, argsOffset, properties, castShadows, receiveShadows, layer, camera, lightProbeUsage, null);
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x00018970 File Offset: 0x00016B70
		[ExcludeFromDocs]
		public static void DrawTexture(Rect screenRect, Texture texture, Rect sourceRect, int leftBorder, int rightBorder, int topBorder, int bottomBorder, Color color, Material mat)
		{
			Graphics.DrawTexture(screenRect, texture, sourceRect, leftBorder, rightBorder, topBorder, bottomBorder, color, mat, -1);
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x00018994 File Offset: 0x00016B94
		[ExcludeFromDocs]
		public static void DrawTexture(Rect screenRect, Texture texture, Rect sourceRect, int leftBorder, int rightBorder, int topBorder, int bottomBorder, Color color)
		{
			Graphics.DrawTexture(screenRect, texture, sourceRect, leftBorder, rightBorder, topBorder, bottomBorder, color, null, -1);
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x000189B8 File Offset: 0x00016BB8
		[ExcludeFromDocs]
		public static void DrawTexture(Rect screenRect, Texture texture, Rect sourceRect, int leftBorder, int rightBorder, int topBorder, int bottomBorder, Material mat)
		{
			Graphics.DrawTexture(screenRect, texture, sourceRect, leftBorder, rightBorder, topBorder, bottomBorder, mat, -1);
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x000189DC File Offset: 0x00016BDC
		[ExcludeFromDocs]
		public static void DrawTexture(Rect screenRect, Texture texture, Rect sourceRect, int leftBorder, int rightBorder, int topBorder, int bottomBorder)
		{
			Graphics.DrawTexture(screenRect, texture, sourceRect, leftBorder, rightBorder, topBorder, bottomBorder, null, -1);
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x000189FC File Offset: 0x00016BFC
		[ExcludeFromDocs]
		public static void DrawTexture(Rect screenRect, Texture texture, int leftBorder, int rightBorder, int topBorder, int bottomBorder, Material mat)
		{
			Graphics.DrawTexture(screenRect, texture, leftBorder, rightBorder, topBorder, bottomBorder, mat, -1);
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x00018A10 File Offset: 0x00016C10
		[ExcludeFromDocs]
		public static void DrawTexture(Rect screenRect, Texture texture, int leftBorder, int rightBorder, int topBorder, int bottomBorder)
		{
			Graphics.DrawTexture(screenRect, texture, leftBorder, rightBorder, topBorder, bottomBorder, null, -1);
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x00018A23 File Offset: 0x00016C23
		[ExcludeFromDocs]
		public static void DrawTexture(Rect screenRect, Texture texture, Material mat)
		{
			Graphics.DrawTexture(screenRect, texture, mat, -1);
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x00018A30 File Offset: 0x00016C30
		[ExcludeFromDocs]
		public static void DrawTexture(Rect screenRect, Texture texture)
		{
			Graphics.DrawTexture(screenRect, texture, null, -1);
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x00018A3D File Offset: 0x00016C3D
		[ExcludeFromDocs]
		public static void SetRenderTarget(RenderTexture rt)
		{
			Graphics.SetRenderTarget(rt, 0, CubemapFace.Unknown, 0);
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x00018A4A File Offset: 0x00016C4A
		[ExcludeFromDocs]
		public static void SetRenderTarget(RenderTexture rt, int mipLevel)
		{
			Graphics.SetRenderTarget(rt, mipLevel, CubemapFace.Unknown, 0);
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x00018A57 File Offset: 0x00016C57
		[ExcludeFromDocs]
		public static void SetRenderTarget(RenderTexture rt, int mipLevel, CubemapFace face)
		{
			Graphics.SetRenderTarget(rt, mipLevel, face, 0);
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x00018A64 File Offset: 0x00016C64
		[ExcludeFromDocs]
		public static void SetRenderTarget(RenderBuffer colorBuffer, RenderBuffer depthBuffer)
		{
			Graphics.SetRenderTarget(colorBuffer, depthBuffer, 0, CubemapFace.Unknown, 0);
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x00018A72 File Offset: 0x00016C72
		[ExcludeFromDocs]
		public static void SetRenderTarget(RenderBuffer colorBuffer, RenderBuffer depthBuffer, int mipLevel)
		{
			Graphics.SetRenderTarget(colorBuffer, depthBuffer, mipLevel, CubemapFace.Unknown, 0);
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x00018A80 File Offset: 0x00016C80
		[ExcludeFromDocs]
		public static void SetRenderTarget(RenderBuffer colorBuffer, RenderBuffer depthBuffer, int mipLevel, CubemapFace face)
		{
			Graphics.SetRenderTarget(colorBuffer, depthBuffer, mipLevel, face, 0);
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x00018A8E File Offset: 0x00016C8E
		[ExcludeFromDocs]
		public static void SetRandomWriteTarget(int index, ComputeBuffer uav)
		{
			Graphics.SetRandomWriteTarget(index, uav, false);
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x00018A9A File Offset: 0x00016C9A
		[ExcludeFromDocs]
		public static void SetRandomWriteTarget(int index, GraphicsBuffer uav)
		{
			Graphics.SetRandomWriteTarget(index, uav, false);
		}

		// Token: 0x06000CB6 RID: 3254
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetActiveColorBuffer_Injected(out RenderBuffer ret);

		// Token: 0x06000CB7 RID: 3255
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetActiveDepthBuffer_Injected(out RenderBuffer ret);

		// Token: 0x06000CB8 RID: 3256
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetGfxRT_Injected(IntPtr gfxTex, int mip, CubemapFace face, int depthSlice);

		// Token: 0x06000CB9 RID: 3257
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRTSimple_Injected([In] ref RenderBuffer color, [In] ref RenderBuffer depth, int mip, CubemapFace face, int depthSlice);

		// Token: 0x06000CBA RID: 3258
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetMRTSimple_Injected(ref ManagedSpanWrapper color, [In] ref RenderBuffer depth, int mip, CubemapFace face, int depthSlice);

		// Token: 0x06000CBB RID: 3259
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetMRTFullSetup_Injected(ref ManagedSpanWrapper color, [In] ref RenderBuffer depth, int mip, CubemapFace face, int depthSlice, ref ManagedSpanWrapper colorLA, ref ManagedSpanWrapper colorSA, RenderBufferLoadAction depthLA, RenderBufferStoreAction depthSA);

		// Token: 0x06000CBC RID: 3260
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRandomWriteTargetRT_Injected(int index, IntPtr uav);

		// Token: 0x06000CBD RID: 3261
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRandomWriteTargetBuffer_Injected(int index, IntPtr uav, bool preserveCounterValue);

		// Token: 0x06000CBE RID: 3262
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRandomWriteTargetGraphicsBuffer_Injected(int index, IntPtr uav, bool preserveCounterValue);

		// Token: 0x06000CBF RID: 3263
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyTexture_Full_Injected(IntPtr src, IntPtr dst);

		// Token: 0x06000CC0 RID: 3264
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyTexture_Slice_AllMips_Injected(IntPtr src, int srcElement, IntPtr dst, int dstElement);

		// Token: 0x06000CC1 RID: 3265
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyTexture_Slice_Injected(IntPtr src, int srcElement, int srcMip, IntPtr dst, int dstElement, int dstMip);

		// Token: 0x06000CC2 RID: 3266
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyTexture_Region_Injected(IntPtr src, int srcElement, int srcMip, int srcX, int srcY, int srcWidth, int srcHeight, IntPtr dst, int dstElement, int dstMip, int dstX, int dstY);

		// Token: 0x06000CC3 RID: 3267
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyTexture_Full_Gfx_Injected(IntPtr src, IntPtr dst);

		// Token: 0x06000CC4 RID: 3268
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyTexture_Slice_AllMips_Gfx_Injected(IntPtr src, int srcElement, IntPtr dst, int dstElement);

		// Token: 0x06000CC5 RID: 3269
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyTexture_Slice_Gfx_Injected(IntPtr src, int srcElement, int srcMip, IntPtr dst, int dstElement, int dstMip);

		// Token: 0x06000CC6 RID: 3270
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyTexture_Region_Gfx_Injected(IntPtr src, int srcElement, int srcMip, int srcX, int srcY, int srcWidth, int srcHeight, IntPtr dst, int dstElement, int dstMip, int dstX, int dstY);

		// Token: 0x06000CC7 RID: 3271
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ConvertTexture_Full_Injected(IntPtr src, IntPtr dst);

		// Token: 0x06000CC8 RID: 3272
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ConvertTexture_Slice_Injected(IntPtr src, int srcElement, IntPtr dst, int dstElement);

		// Token: 0x06000CC9 RID: 3273
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ConvertTexture_Full_Gfx_Injected(IntPtr src, IntPtr dst);

		// Token: 0x06000CCA RID: 3274
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ConvertTexture_Slice_Gfx_Injected(IntPtr src, int srcElement, IntPtr dst, int dstElement);

		// Token: 0x06000CCB RID: 3275
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyBufferImpl_Injected(IntPtr source, IntPtr dest);

		// Token: 0x06000CCC RID: 3276
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawMeshNow1_Injected(IntPtr mesh, int subsetIndex, [In] ref Vector3 position, [In] ref Quaternion rotation);

		// Token: 0x06000CCD RID: 3277
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawMeshNow2_Injected(IntPtr mesh, int subsetIndex, [In] ref Matrix4x4 matrix);

		// Token: 0x06000CCE RID: 3278
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Internal_RenderMesh_Injected([In] ref RenderParams rparams, IntPtr mesh, int submeshIndex, [In] ref Matrix4x4 objectToWorld, Matrix4x4* prevObjectToWorld);

		// Token: 0x06000CCF RID: 3279
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_RenderMeshInstanced_Injected([In] ref RenderParams rparams, IntPtr mesh, int submeshIndex, IntPtr instanceData, [In] ref RenderInstancedDataLayout layout, uint instanceCount);

		// Token: 0x06000CD0 RID: 3280
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_RenderMeshIndirect_Injected([In] ref RenderParams rparams, IntPtr mesh, IntPtr commandBuffer, int commandCount, int startCommand);

		// Token: 0x06000CD1 RID: 3281
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_RenderMeshPrimitives_Injected([In] ref RenderParams rparams, IntPtr mesh, int submeshIndex, int instanceCount);

		// Token: 0x06000CD2 RID: 3282
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_RenderPrimitives_Injected([In] ref RenderParams rparams, MeshTopology topology, int vertexCount, int instanceCount);

		// Token: 0x06000CD3 RID: 3283
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_RenderPrimitivesIndexed_Injected([In] ref RenderParams rparams, MeshTopology topology, IntPtr indexBuffer, int indexCount, int startIndex, int instanceCount);

		// Token: 0x06000CD4 RID: 3284
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_RenderPrimitivesIndirect_Injected([In] ref RenderParams rparams, MeshTopology topology, IntPtr commandBuffer, int commandCount, int startCommand);

		// Token: 0x06000CD5 RID: 3285
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_RenderPrimitivesIndexedIndirect_Injected([In] ref RenderParams rparams, MeshTopology topology, IntPtr indexBuffer, IntPtr commandBuffer, int commandCount, int startCommand);

		// Token: 0x06000CD6 RID: 3286
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawMesh_Injected(IntPtr mesh, int submeshIndex, [In] ref Matrix4x4 matrix, IntPtr material, int layer, IntPtr camera, IntPtr properties, ShadowCastingMode castShadows, bool receiveShadows, IntPtr probeAnchor, LightProbeUsage lightProbeUsage, IntPtr lightProbeProxyVolume);

		// Token: 0x06000CD7 RID: 3287
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawMeshInstanced_Injected(IntPtr mesh, int submeshIndex, IntPtr material, ref ManagedSpanWrapper matrices, int count, IntPtr properties, ShadowCastingMode castShadows, bool receiveShadows, int layer, IntPtr camera, LightProbeUsage lightProbeUsage, IntPtr lightProbeProxyVolume);

		// Token: 0x06000CD8 RID: 3288
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawMeshInstancedProcedural_Injected(IntPtr mesh, int submeshIndex, IntPtr material, [In] ref Bounds bounds, int count, IntPtr properties, ShadowCastingMode castShadows, bool receiveShadows, int layer, IntPtr camera, LightProbeUsage lightProbeUsage, IntPtr lightProbeProxyVolume);

		// Token: 0x06000CD9 RID: 3289
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawMeshInstancedIndirect_Injected(IntPtr mesh, int submeshIndex, IntPtr material, [In] ref Bounds bounds, IntPtr bufferWithArgs, int argsOffset, IntPtr properties, ShadowCastingMode castShadows, bool receiveShadows, int layer, IntPtr camera, LightProbeUsage lightProbeUsage, IntPtr lightProbeProxyVolume);

		// Token: 0x06000CDA RID: 3290
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawMeshInstancedIndirectGraphicsBuffer_Injected(IntPtr mesh, int submeshIndex, IntPtr material, [In] ref Bounds bounds, IntPtr bufferWithArgs, int argsOffset, IntPtr properties, ShadowCastingMode castShadows, bool receiveShadows, int layer, IntPtr camera, LightProbeUsage lightProbeUsage, IntPtr lightProbeProxyVolume);

		// Token: 0x06000CDB RID: 3291
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawProceduralIndexedNow_Injected(MeshTopology topology, IntPtr indexBuffer, int indexCount, int instanceCount);

		// Token: 0x06000CDC RID: 3292
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawProceduralIndirectNow_Injected(MeshTopology topology, IntPtr bufferWithArgs, int argsOffset);

		// Token: 0x06000CDD RID: 3293
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawProceduralIndexedIndirectNow_Injected(MeshTopology topology, IntPtr indexBuffer, IntPtr bufferWithArgs, int argsOffset);

		// Token: 0x06000CDE RID: 3294
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawProceduralIndirectNowGraphicsBuffer_Injected(MeshTopology topology, IntPtr bufferWithArgs, int argsOffset);

		// Token: 0x06000CDF RID: 3295
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawProceduralIndexedIndirectNowGraphicsBuffer_Injected(MeshTopology topology, IntPtr indexBuffer, IntPtr bufferWithArgs, int argsOffset);

		// Token: 0x06000CE0 RID: 3296
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawProcedural_Injected(IntPtr material, [In] ref Bounds bounds, MeshTopology topology, int vertexCount, int instanceCount, IntPtr camera, IntPtr properties, ShadowCastingMode castShadows, bool receiveShadows, int layer);

		// Token: 0x06000CE1 RID: 3297
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawProceduralIndexed_Injected(IntPtr material, [In] ref Bounds bounds, MeshTopology topology, IntPtr indexBuffer, int indexCount, int instanceCount, IntPtr camera, IntPtr properties, ShadowCastingMode castShadows, bool receiveShadows, int layer);

		// Token: 0x06000CE2 RID: 3298
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawProceduralIndirect_Injected(IntPtr material, [In] ref Bounds bounds, MeshTopology topology, IntPtr bufferWithArgs, int argsOffset, IntPtr camera, IntPtr properties, ShadowCastingMode castShadows, bool receiveShadows, int layer);

		// Token: 0x06000CE3 RID: 3299
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawProceduralIndirectGraphicsBuffer_Injected(IntPtr material, [In] ref Bounds bounds, MeshTopology topology, IntPtr bufferWithArgs, int argsOffset, IntPtr camera, IntPtr properties, ShadowCastingMode castShadows, bool receiveShadows, int layer);

		// Token: 0x06000CE4 RID: 3300
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawProceduralIndexedIndirect_Injected(IntPtr material, [In] ref Bounds bounds, MeshTopology topology, IntPtr indexBuffer, IntPtr bufferWithArgs, int argsOffset, IntPtr camera, IntPtr properties, ShadowCastingMode castShadows, bool receiveShadows, int layer);

		// Token: 0x06000CE5 RID: 3301
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawProceduralIndexedIndirectGraphicsBuffer_Injected(IntPtr material, [In] ref Bounds bounds, MeshTopology topology, IntPtr indexBuffer, IntPtr bufferWithArgs, int argsOffset, IntPtr camera, IntPtr properties, ShadowCastingMode castShadows, bool receiveShadows, int layer);

		// Token: 0x06000CE6 RID: 3302
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_BlitMaterial5_Injected(IntPtr source, IntPtr dest, IntPtr mat, int pass, bool setRT);

		// Token: 0x06000CE7 RID: 3303
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_BlitMaterial6_Injected(IntPtr source, IntPtr dest, IntPtr mat, int pass, bool setRT, int destDepthSlice);

		// Token: 0x06000CE8 RID: 3304
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_BlitMultiTap4_Injected(IntPtr source, IntPtr dest, IntPtr mat, ref ManagedSpanWrapper offsets);

		// Token: 0x06000CE9 RID: 3305
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_BlitMultiTap5_Injected(IntPtr source, IntPtr dest, IntPtr mat, ref ManagedSpanWrapper offsets, int destDepthSlice);

		// Token: 0x06000CEA RID: 3306
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Blit2_Injected(IntPtr source, IntPtr dest);

		// Token: 0x06000CEB RID: 3307
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Blit3_Injected(IntPtr source, IntPtr dest, int sourceDepthSlice, int destDepthSlice);

		// Token: 0x06000CEC RID: 3308
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Blit4_Injected(IntPtr source, IntPtr dest, [In] ref Vector2 scale, [In] ref Vector2 offset);

		// Token: 0x06000CED RID: 3309
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Blit5_Injected(IntPtr source, IntPtr dest, [In] ref Vector2 scale, [In] ref Vector2 offset, int sourceDepthSlice, int destDepthSlice);

		// Token: 0x06000CEE RID: 3310
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_BlitMaterialGfx5_Injected(IntPtr source, IntPtr dest, IntPtr mat, int pass, bool setRT);

		// Token: 0x06000CEF RID: 3311
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_BlitMaterialGfx6_Injected(IntPtr source, IntPtr dest, IntPtr mat, int pass, bool setRT, int destDepthSlice);

		// Token: 0x06000CF0 RID: 3312
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_BlitMultiTapGfx4_Injected(IntPtr source, IntPtr dest, IntPtr mat, ref ManagedSpanWrapper offsets);

		// Token: 0x06000CF1 RID: 3313
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_BlitMultiTapGfx5_Injected(IntPtr source, IntPtr dest, IntPtr mat, ref ManagedSpanWrapper offsets, int destDepthSlice);

		// Token: 0x06000CF2 RID: 3314
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BlitGfx2_Injected(IntPtr source, IntPtr dest);

		// Token: 0x06000CF3 RID: 3315
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BlitGfx3_Injected(IntPtr source, IntPtr dest, int sourceDepthSlice, int destDepthSlice);

		// Token: 0x06000CF4 RID: 3316
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BlitGfx4_Injected(IntPtr source, IntPtr dest, [In] ref Vector2 scale, [In] ref Vector2 offset);

		// Token: 0x06000CF5 RID: 3317
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BlitGfx5_Injected(IntPtr source, IntPtr dest, [In] ref Vector2 scale, [In] ref Vector2 offset, int sourceDepthSlice, int destDepthSlice);

		// Token: 0x06000CF6 RID: 3318
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ExecuteCommandBuffer_Injected(IntPtr buffer);

		// Token: 0x06000CF7 RID: 3319
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ExecuteCommandBufferAsync_Injected(IntPtr buffer, ComputeQueueType queueType);

		// Token: 0x040004AB RID: 1195
		internal static readonly int kMaxDrawMeshInstanceCount = Graphics.Internal_GetMaxDrawMeshInstanceCount();

		// Token: 0x040004AC RID: 1196
		internal static Dictionary<int, RenderInstancedDataLayout> s_RenderInstancedDataLayouts = new Dictionary<int, RenderInstancedDataLayout>();
	}
}
