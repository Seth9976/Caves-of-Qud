using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Rendering.RendererUtils;

namespace UnityEngine.Rendering
{
	// Token: 0x02000554 RID: 1364
	[NativeHeader("Modules/UI/Canvas.h")]
	[NativeHeader("Runtime/Export/RenderPipeline/ScriptableRenderContext.bindings.h")]
	[NativeType("Runtime/Graphics/ScriptableRenderLoop/ScriptableRenderContext.h")]
	[NativeHeader("Runtime/Graphics/ScriptableRenderLoop/ScriptableDrawRenderersUtility.h")]
	[NativeHeader("Runtime/Export/RenderPipeline/ScriptableRenderPipeline.bindings.h")]
	[NativeHeader("Modules/UI/CanvasManager.h")]
	public struct ScriptableRenderContext : IEquatable<ScriptableRenderContext>
	{
		// Token: 0x06003448 RID: 13384
		[FreeFunction("ScriptableRenderContext::BeginRenderPass")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BeginRenderPass_Internal(IntPtr self, int width, int height, int volumeDepth, int samples, IntPtr colors, int colorCount, int depthAttachmentIndex);

		// Token: 0x06003449 RID: 13385
		[FreeFunction("ScriptableRenderContext::BeginSubPass")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BeginSubPass_Internal(IntPtr self, IntPtr colors, int colorCount, IntPtr inputs, int inputCount, bool isDepthReadOnly, bool isStencilReadOnly);

		// Token: 0x0600344A RID: 13386
		[FreeFunction("ScriptableRenderContext::EndSubPass")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EndSubPass_Internal(IntPtr self);

		// Token: 0x0600344B RID: 13387
		[FreeFunction("ScriptableRenderContext::EndRenderPass")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EndRenderPass_Internal(IntPtr self);

		// Token: 0x0600344C RID: 13388
		[FreeFunction("ScriptableRenderContext::HasInvokeOnRenderObjectCallbacks")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasInvokeOnRenderObjectCallbacks_Internal();

		// Token: 0x0600344D RID: 13389 RVA: 0x00066904 File Offset: 0x00064B04
		[FreeFunction("ScriptableRenderPipeline_Bindings::Internal_Cull")]
		private static void Internal_Cull(ref ScriptableCullingParameters parameters, ScriptableRenderContext renderLoop, IntPtr results)
		{
			ScriptableRenderContext.Internal_Cull_Injected(ref parameters, ref renderLoop, results);
		}

		// Token: 0x0600344E RID: 13390 RVA: 0x0006691C File Offset: 0x00064B1C
		[FreeFunction("ScriptableRenderPipeline_Bindings::Internal_CullShadowCasters")]
		private static void Internal_CullShadowCasters(ScriptableRenderContext renderLoop, IntPtr context)
		{
			ScriptableRenderContext.Internal_CullShadowCasters_Injected(ref renderLoop, context);
		}

		// Token: 0x0600344F RID: 13391 RVA: 0x00066934 File Offset: 0x00064B34
		[FreeFunction("InitializeSortSettings")]
		internal static void InitializeSortSettings(Camera camera, out SortingSettings sortingSettings)
		{
			ScriptableRenderContext.InitializeSortSettings_Injected(Object.MarshalledUnityObject.Marshal<Camera>(camera), out sortingSettings);
		}

		// Token: 0x06003450 RID: 13392
		[FreeFunction("ScriptableRenderContext::PushDisableApiRenderers")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void PushDisableApiRenderers();

		// Token: 0x06003451 RID: 13393
		[FreeFunction("ScriptableRenderContext::PopDisableApiRenderers")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void PopDisableApiRenderers();

		// Token: 0x06003452 RID: 13394
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Submit_Internal();

		// Token: 0x06003453 RID: 13395
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool SubmitForRenderPassValidation_Internal();

		// Token: 0x06003454 RID: 13396
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetCameras_Internal(Type listType, object resultList);

		// Token: 0x06003455 RID: 13397 RVA: 0x00066950 File Offset: 0x00064B50
		private void DrawRenderers_Internal(IntPtr cullResults, ref DrawingSettings drawingSettings, ref FilteringSettings filteringSettings, ShaderTagId tagName, bool isPassTagName, IntPtr tagValues, IntPtr stateBlocks, int stateCount)
		{
			ScriptableRenderContext.DrawRenderers_Internal_Injected(ref this, cullResults, ref drawingSettings, ref filteringSettings, ref tagName, isPassTagName, tagValues, stateBlocks, stateCount);
		}

		// Token: 0x06003456 RID: 13398
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void DrawShadows_Internal(IntPtr shadowDrawingSettings);

		// Token: 0x06003457 RID: 13399 RVA: 0x00066970 File Offset: 0x00064B70
		[FreeFunction("PlayerEmitCanvasGeometryForCamera")]
		public static void EmitGeometryForCamera(Camera camera)
		{
			ScriptableRenderContext.EmitGeometryForCamera_Injected(Object.MarshalledUnityObject.Marshal<Camera>(camera));
		}

		// Token: 0x06003458 RID: 13400 RVA: 0x00066988 File Offset: 0x00064B88
		[NativeThrows]
		private void ExecuteCommandBuffer_Internal(CommandBuffer commandBuffer)
		{
			ScriptableRenderContext.ExecuteCommandBuffer_Internal_Injected(ref this, (commandBuffer == null) ? ((IntPtr)0) : CommandBuffer.BindingsMarshaller.ConvertToNative(commandBuffer));
		}

		// Token: 0x06003459 RID: 13401 RVA: 0x000669AC File Offset: 0x00064BAC
		[NativeThrows]
		private void ExecuteCommandBufferAsync_Internal(CommandBuffer commandBuffer, ComputeQueueType queueType)
		{
			ScriptableRenderContext.ExecuteCommandBufferAsync_Internal_Injected(ref this, (commandBuffer == null) ? ((IntPtr)0) : CommandBuffer.BindingsMarshaller.ConvertToNative(commandBuffer), queueType);
		}

		// Token: 0x0600345A RID: 13402 RVA: 0x000669D0 File Offset: 0x00064BD0
		private void SetupCameraProperties_Internal([NotNull] Camera camera, bool stereoSetup, int eye)
		{
			if (camera == null)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(camera);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			ScriptableRenderContext.SetupCameraProperties_Internal_Injected(ref this, intPtr, stereoSetup, eye);
		}

		// Token: 0x0600345B RID: 13403 RVA: 0x00066A0C File Offset: 0x00064C0C
		private void StereoEndRender_Internal([NotNull] Camera camera, int eye, bool isFinalPass)
		{
			if (camera == null)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(camera);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			ScriptableRenderContext.StereoEndRender_Internal_Injected(ref this, intPtr, eye, isFinalPass);
		}

		// Token: 0x0600345C RID: 13404 RVA: 0x00066A48 File Offset: 0x00064C48
		private void StartMultiEye_Internal([NotNull] Camera camera, int eye)
		{
			if (camera == null)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(camera);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			ScriptableRenderContext.StartMultiEye_Internal_Injected(ref this, intPtr, eye);
		}

		// Token: 0x0600345D RID: 13405 RVA: 0x00066A80 File Offset: 0x00064C80
		private void StopMultiEye_Internal([NotNull] Camera camera)
		{
			if (camera == null)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(camera);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			ScriptableRenderContext.StopMultiEye_Internal_Injected(ref this, intPtr);
		}

		// Token: 0x0600345E RID: 13406 RVA: 0x00066AB8 File Offset: 0x00064CB8
		private void DrawSkybox_Internal([NotNull] Camera camera)
		{
			if (camera == null)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(camera);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			ScriptableRenderContext.DrawSkybox_Internal_Injected(ref this, intPtr);
		}

		// Token: 0x0600345F RID: 13407
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InvokeOnRenderObjectCallback_Internal();

		// Token: 0x06003460 RID: 13408 RVA: 0x00066AF0 File Offset: 0x00064CF0
		private void DrawGizmos_Internal([NotNull] Camera camera, GizmoSubset gizmoSubset)
		{
			if (camera == null)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(camera);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			ScriptableRenderContext.DrawGizmos_Internal_Injected(ref this, intPtr, gizmoSubset);
		}

		// Token: 0x06003461 RID: 13409 RVA: 0x00066B28 File Offset: 0x00064D28
		private void DrawWireOverlay_Impl([NotNull] Camera camera)
		{
			if (camera == null)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(camera);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			ScriptableRenderContext.DrawWireOverlay_Impl_Injected(ref this, intPtr);
		}

		// Token: 0x06003462 RID: 13410 RVA: 0x00066B60 File Offset: 0x00064D60
		private void DrawUIOverlay_Internal([NotNull] Camera camera)
		{
			if (camera == null)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(camera);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			ScriptableRenderContext.DrawUIOverlay_Internal_Injected(ref this, intPtr);
		}

		// Token: 0x06003463 RID: 13411 RVA: 0x00066B98 File Offset: 0x00064D98
		internal IntPtr Internal_GetPtr()
		{
			return this.m_Ptr;
		}

		// Token: 0x06003464 RID: 13412 RVA: 0x00066BB0 File Offset: 0x00064DB0
		private RendererList CreateRendererList_Internal(IntPtr cullResults, ref DrawingSettings drawingSettings, ref FilteringSettings filteringSettings, ShaderTagId tagName, bool isPassTagName, IntPtr tagValues, IntPtr stateBlocks, int stateCount)
		{
			RendererList rendererList;
			ScriptableRenderContext.CreateRendererList_Internal_Injected(ref this, cullResults, ref drawingSettings, ref filteringSettings, ref tagName, isPassTagName, tagValues, stateBlocks, stateCount, out rendererList);
			return rendererList;
		}

		// Token: 0x06003465 RID: 13413 RVA: 0x00066BD4 File Offset: 0x00064DD4
		private RendererList CreateShadowRendererList_Internal(IntPtr shadowDrawinSettings)
		{
			RendererList rendererList;
			ScriptableRenderContext.CreateShadowRendererList_Internal_Injected(ref this, shadowDrawinSettings, out rendererList);
			return rendererList;
		}

		// Token: 0x06003466 RID: 13414 RVA: 0x00066BEC File Offset: 0x00064DEC
		private RendererList CreateSkyboxRendererList_Internal([NotNull] Camera camera, int mode, Matrix4x4 proj, Matrix4x4 view, Matrix4x4 projR, Matrix4x4 viewR)
		{
			if (camera == null)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(camera);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			RendererList rendererList;
			ScriptableRenderContext.CreateSkyboxRendererList_Internal_Injected(ref this, intPtr, mode, ref proj, ref view, ref projR, ref viewR, out rendererList);
			return rendererList;
		}

		// Token: 0x06003467 RID: 13415 RVA: 0x00066C30 File Offset: 0x00064E30
		private RendererList CreateGizmoRendererList_Internal([NotNull] Camera camera, GizmoSubset gizmoSubset)
		{
			if (camera == null)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(camera);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			RendererList rendererList;
			ScriptableRenderContext.CreateGizmoRendererList_Internal_Injected(ref this, intPtr, gizmoSubset, out rendererList);
			return rendererList;
		}

		// Token: 0x06003468 RID: 13416 RVA: 0x00066C6C File Offset: 0x00064E6C
		private RendererList CreateUIOverlayRendererList_Internal([NotNull] Camera camera, UISubset uiSubset)
		{
			if (camera == null)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(camera);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			RendererList rendererList;
			ScriptableRenderContext.CreateUIOverlayRendererList_Internal_Injected(ref this, intPtr, uiSubset, out rendererList);
			return rendererList;
		}

		// Token: 0x06003469 RID: 13417 RVA: 0x00066CA8 File Offset: 0x00064EA8
		private RendererList CreateWireOverlayRendererList_Internal([NotNull] Camera camera)
		{
			if (camera == null)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(camera);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			RendererList rendererList;
			ScriptableRenderContext.CreateWireOverlayRendererList_Internal_Injected(ref this, intPtr, out rendererList);
			return rendererList;
		}

		// Token: 0x0600346A RID: 13418
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void PrepareRendererListsAsync_Internal(object rendererLists);

		// Token: 0x0600346B RID: 13419 RVA: 0x00066CE4 File Offset: 0x00064EE4
		private RendererListStatus QueryRendererListStatus_Internal(RendererList handle)
		{
			return ScriptableRenderContext.QueryRendererListStatus_Internal_Injected(ref this, ref handle);
		}

		// Token: 0x0600346C RID: 13420 RVA: 0x00066CF9 File Offset: 0x00064EF9
		internal ScriptableRenderContext(IntPtr ptr)
		{
			this.m_Ptr = ptr;
		}

		// Token: 0x0600346D RID: 13421 RVA: 0x00066D03 File Offset: 0x00064F03
		public void BeginRenderPass(int width, int height, int volumeDepth, int samples, NativeArray<AttachmentDescriptor> attachments, int depthAttachmentIndex = -1)
		{
			ScriptableRenderContext.BeginRenderPass_Internal(this.m_Ptr, width, height, volumeDepth, samples, (IntPtr)attachments.GetUnsafeReadOnlyPtr<AttachmentDescriptor>(), attachments.Length, depthAttachmentIndex);
		}

		// Token: 0x0600346E RID: 13422 RVA: 0x00066D2C File Offset: 0x00064F2C
		public void BeginRenderPass(int width, int height, int samples, NativeArray<AttachmentDescriptor> attachments, int depthAttachmentIndex = -1)
		{
			ScriptableRenderContext.BeginRenderPass_Internal(this.m_Ptr, width, height, 1, samples, (IntPtr)attachments.GetUnsafeReadOnlyPtr<AttachmentDescriptor>(), attachments.Length, depthAttachmentIndex);
		}

		// Token: 0x0600346F RID: 13423 RVA: 0x00066D54 File Offset: 0x00064F54
		public ScopedRenderPass BeginScopedRenderPass(int width, int height, int samples, NativeArray<AttachmentDescriptor> attachments, int depthAttachmentIndex = -1)
		{
			this.BeginRenderPass(width, height, samples, attachments, depthAttachmentIndex);
			return new ScopedRenderPass(this);
		}

		// Token: 0x06003470 RID: 13424 RVA: 0x00066D7F File Offset: 0x00064F7F
		public void BeginSubPass(NativeArray<int> colors, NativeArray<int> inputs, bool isDepthReadOnly, bool isStencilReadOnly)
		{
			ScriptableRenderContext.BeginSubPass_Internal(this.m_Ptr, (IntPtr)colors.GetUnsafeReadOnlyPtr<int>(), colors.Length, (IntPtr)inputs.GetUnsafeReadOnlyPtr<int>(), inputs.Length, isDepthReadOnly, isStencilReadOnly);
		}

		// Token: 0x06003471 RID: 13425 RVA: 0x00066DB5 File Offset: 0x00064FB5
		public void BeginSubPass(NativeArray<int> colors, NativeArray<int> inputs, bool isDepthStencilReadOnly = false)
		{
			ScriptableRenderContext.BeginSubPass_Internal(this.m_Ptr, (IntPtr)colors.GetUnsafeReadOnlyPtr<int>(), colors.Length, (IntPtr)inputs.GetUnsafeReadOnlyPtr<int>(), inputs.Length, isDepthStencilReadOnly, isDepthStencilReadOnly);
		}

		// Token: 0x06003472 RID: 13426 RVA: 0x00066DEA File Offset: 0x00064FEA
		public void BeginSubPass(NativeArray<int> colors, bool isDepthReadOnly, bool isStencilReadOnly)
		{
			ScriptableRenderContext.BeginSubPass_Internal(this.m_Ptr, (IntPtr)colors.GetUnsafeReadOnlyPtr<int>(), colors.Length, IntPtr.Zero, 0, isDepthReadOnly, isStencilReadOnly);
		}

		// Token: 0x06003473 RID: 13427 RVA: 0x00066E13 File Offset: 0x00065013
		public void BeginSubPass(NativeArray<int> colors, bool isDepthStencilReadOnly = false)
		{
			ScriptableRenderContext.BeginSubPass_Internal(this.m_Ptr, (IntPtr)colors.GetUnsafeReadOnlyPtr<int>(), colors.Length, IntPtr.Zero, 0, isDepthStencilReadOnly, isDepthStencilReadOnly);
		}

		// Token: 0x06003474 RID: 13428 RVA: 0x00066E3C File Offset: 0x0006503C
		public ScopedSubPass BeginScopedSubPass(NativeArray<int> colors, NativeArray<int> inputs, bool isDepthReadOnly, bool isStencilReadOnly)
		{
			this.BeginSubPass(colors, inputs, isDepthReadOnly, isStencilReadOnly);
			return new ScopedSubPass(this);
		}

		// Token: 0x06003475 RID: 13429 RVA: 0x00066E68 File Offset: 0x00065068
		public ScopedSubPass BeginScopedSubPass(NativeArray<int> colors, NativeArray<int> inputs, bool isDepthStencilReadOnly = false)
		{
			this.BeginSubPass(colors, inputs, isDepthStencilReadOnly);
			return new ScopedSubPass(this);
		}

		// Token: 0x06003476 RID: 13430 RVA: 0x00066E90 File Offset: 0x00065090
		public ScopedSubPass BeginScopedSubPass(NativeArray<int> colors, bool isDepthReadOnly, bool isStencilReadOnly)
		{
			this.BeginSubPass(colors, isDepthReadOnly, isStencilReadOnly);
			return new ScopedSubPass(this);
		}

		// Token: 0x06003477 RID: 13431 RVA: 0x00066EB8 File Offset: 0x000650B8
		public ScopedSubPass BeginScopedSubPass(NativeArray<int> colors, bool isDepthStencilReadOnly = false)
		{
			this.BeginSubPass(colors, isDepthStencilReadOnly);
			return new ScopedSubPass(this);
		}

		// Token: 0x06003478 RID: 13432 RVA: 0x00066EDE File Offset: 0x000650DE
		public void EndSubPass()
		{
			ScriptableRenderContext.EndSubPass_Internal(this.m_Ptr);
		}

		// Token: 0x06003479 RID: 13433 RVA: 0x00066EED File Offset: 0x000650ED
		public void EndRenderPass()
		{
			ScriptableRenderContext.EndRenderPass_Internal(this.m_Ptr);
		}

		// Token: 0x0600347A RID: 13434 RVA: 0x00066EFC File Offset: 0x000650FC
		public void Submit()
		{
			this.Submit_Internal();
		}

		// Token: 0x0600347B RID: 13435 RVA: 0x00066F08 File Offset: 0x00065108
		public bool SubmitForRenderPassValidation()
		{
			return this.SubmitForRenderPassValidation_Internal();
		}

		// Token: 0x0600347C RID: 13436 RVA: 0x00066F20 File Offset: 0x00065120
		public bool HasInvokeOnRenderObjectCallbacks()
		{
			return ScriptableRenderContext.HasInvokeOnRenderObjectCallbacks_Internal();
		}

		// Token: 0x0600347D RID: 13437 RVA: 0x00066F37 File Offset: 0x00065137
		internal void GetCameras(List<Camera> results)
		{
			this.GetCameras_Internal(typeof(Camera), results);
		}

		// Token: 0x0600347E RID: 13438 RVA: 0x00066F4C File Offset: 0x0006514C
		[Obsolete("DrawRenderers is obsolete and replaced with the RendererList API: construct a RendererList using ScriptableRenderContext.CreateRendererList and execture it using CommandBuffer.DrawRendererList.", false)]
		public void DrawRenderers(CullingResults cullingResults, ref DrawingSettings drawingSettings, ref FilteringSettings filteringSettings)
		{
			this.DrawRenderers_Internal(cullingResults.ptr, ref drawingSettings, ref filteringSettings, ShaderTagId.none, false, IntPtr.Zero, IntPtr.Zero, 0);
		}

		// Token: 0x0600347F RID: 13439 RVA: 0x00066F7C File Offset: 0x0006517C
		[Obsolete("DrawRenderers is obsolete and replaced with the RendererList API: construct a RendererList using ScriptableRenderContext.CreateRendererList and execture it using CommandBuffer.DrawRendererList.", false)]
		public unsafe void DrawRenderers(CullingResults cullingResults, ref DrawingSettings drawingSettings, ref FilteringSettings filteringSettings, ref RenderStateBlock stateBlock)
		{
			ShaderTagId shaderTagId = default(ShaderTagId);
			fixed (RenderStateBlock* ptr = &stateBlock)
			{
				RenderStateBlock* ptr2 = ptr;
				this.DrawRenderers_Internal(cullingResults.ptr, ref drawingSettings, ref filteringSettings, ShaderTagId.none, false, (IntPtr)((void*)(&shaderTagId)), (IntPtr)((void*)ptr2), 1);
			}
		}

		// Token: 0x06003480 RID: 13440 RVA: 0x00066FC4 File Offset: 0x000651C4
		[Obsolete("DrawRenderers is obsolete and replaced with the RendererList API: construct a RendererList using ScriptableRenderContext.CreateRendererList and execture it using CommandBuffer.DrawRendererList.", false)]
		public void DrawRenderers(CullingResults cullingResults, ref DrawingSettings drawingSettings, ref FilteringSettings filteringSettings, NativeArray<ShaderTagId> renderTypes, NativeArray<RenderStateBlock> stateBlocks)
		{
			bool flag = renderTypes.Length != stateBlocks.Length;
			if (flag)
			{
				throw new ArgumentException(string.Format("Arrays {0} and {1} should have same length, but {2} had length {3} while {4} had length {5}.", new object[] { "renderTypes", "stateBlocks", "renderTypes", renderTypes.Length, "stateBlocks", stateBlocks.Length }));
			}
			this.DrawRenderers_Internal(cullingResults.ptr, ref drawingSettings, ref filteringSettings, ScriptableRenderContext.kRenderTypeTag, false, (IntPtr)renderTypes.GetUnsafeReadOnlyPtr<ShaderTagId>(), (IntPtr)stateBlocks.GetUnsafeReadOnlyPtr<RenderStateBlock>(), renderTypes.Length);
		}

		// Token: 0x06003481 RID: 13441 RVA: 0x00067074 File Offset: 0x00065274
		[Obsolete("DrawRenderers is obsolete and replaced with the RendererList API: construct a RendererList using ScriptableRenderContext.CreateRendererList and execture it using CommandBuffer.DrawRendererList.", false)]
		public void DrawRenderers(CullingResults cullingResults, ref DrawingSettings drawingSettings, ref FilteringSettings filteringSettings, ShaderTagId tagName, bool isPassTagName, NativeArray<ShaderTagId> tagValues, NativeArray<RenderStateBlock> stateBlocks)
		{
			bool flag = tagValues.Length != stateBlocks.Length;
			if (flag)
			{
				throw new ArgumentException(string.Format("Arrays {0} and {1} should have same length, but {2} had length {3} while {4} had length {5}.", new object[] { "tagValues", "stateBlocks", "tagValues", tagValues.Length, "stateBlocks", stateBlocks.Length }));
			}
			this.DrawRenderers_Internal(cullingResults.ptr, ref drawingSettings, ref filteringSettings, tagName, isPassTagName, (IntPtr)tagValues.GetUnsafeReadOnlyPtr<ShaderTagId>(), (IntPtr)stateBlocks.GetUnsafeReadOnlyPtr<RenderStateBlock>(), tagValues.Length);
		}

		// Token: 0x06003482 RID: 13442 RVA: 0x00067120 File Offset: 0x00065320
		[Obsolete("DrawShadows is obsolete and replaced with the RendererList API: construct a RendererList using ScriptableRenderContext.CreateShadowRendererList and execture it using CommandBuffer.DrawRendererList.", false)]
		public unsafe void DrawShadows(ref ShadowDrawingSettings settings)
		{
			fixed (ShadowDrawingSettings* ptr = &settings)
			{
				ShadowDrawingSettings* ptr2 = ptr;
				this.DrawShadows_Internal((IntPtr)((void*)ptr2));
			}
		}

		// Token: 0x06003483 RID: 13443 RVA: 0x00067148 File Offset: 0x00065348
		public void ExecuteCommandBuffer(CommandBuffer commandBuffer)
		{
			bool flag = commandBuffer == null;
			if (flag)
			{
				throw new ArgumentNullException("commandBuffer");
			}
			bool flag2 = commandBuffer.m_Ptr == IntPtr.Zero;
			if (flag2)
			{
				throw new ObjectDisposedException("commandBuffer");
			}
			this.ExecuteCommandBuffer_Internal(commandBuffer);
		}

		// Token: 0x06003484 RID: 13444 RVA: 0x00067190 File Offset: 0x00065390
		public void ExecuteCommandBufferAsync(CommandBuffer commandBuffer, ComputeQueueType queueType)
		{
			bool flag = commandBuffer == null;
			if (flag)
			{
				throw new ArgumentNullException("commandBuffer");
			}
			bool flag2 = commandBuffer.m_Ptr == IntPtr.Zero;
			if (flag2)
			{
				throw new ObjectDisposedException("commandBuffer");
			}
			this.ExecuteCommandBufferAsync_Internal(commandBuffer, queueType);
		}

		// Token: 0x06003485 RID: 13445 RVA: 0x000671D9 File Offset: 0x000653D9
		public void SetupCameraProperties(Camera camera, bool stereoSetup = false)
		{
			this.SetupCameraProperties(camera, stereoSetup, 0);
		}

		// Token: 0x06003486 RID: 13446 RVA: 0x000671E6 File Offset: 0x000653E6
		public void SetupCameraProperties(Camera camera, bool stereoSetup, int eye)
		{
			this.SetupCameraProperties_Internal(camera, stereoSetup, eye);
		}

		// Token: 0x06003487 RID: 13447 RVA: 0x000671F3 File Offset: 0x000653F3
		public void StereoEndRender(Camera camera)
		{
			this.StereoEndRender(camera, 0, true);
		}

		// Token: 0x06003488 RID: 13448 RVA: 0x00067200 File Offset: 0x00065400
		public void StereoEndRender(Camera camera, int eye)
		{
			this.StereoEndRender(camera, eye, true);
		}

		// Token: 0x06003489 RID: 13449 RVA: 0x0006720D File Offset: 0x0006540D
		public void StereoEndRender(Camera camera, int eye, bool isFinalPass)
		{
			this.StereoEndRender_Internal(camera, eye, isFinalPass);
		}

		// Token: 0x0600348A RID: 13450 RVA: 0x0006721A File Offset: 0x0006541A
		public void StartMultiEye(Camera camera)
		{
			this.StartMultiEye(camera, 0);
		}

		// Token: 0x0600348B RID: 13451 RVA: 0x00067226 File Offset: 0x00065426
		public void StartMultiEye(Camera camera, int eye)
		{
			this.StartMultiEye_Internal(camera, eye);
		}

		// Token: 0x0600348C RID: 13452 RVA: 0x00067232 File Offset: 0x00065432
		public void StopMultiEye(Camera camera)
		{
			this.StopMultiEye_Internal(camera);
		}

		// Token: 0x0600348D RID: 13453 RVA: 0x0006723D File Offset: 0x0006543D
		[Obsolete("DrawSkybox is obsolete and replaced with the RendererList API: construct a RendererList using ScriptableRenderContext.CreateSkyboxRendererList and execture it using CommandBuffer.DrawRendererList.", false)]
		public void DrawSkybox(Camera camera)
		{
			this.DrawSkybox_Internal(camera);
		}

		// Token: 0x0600348E RID: 13454 RVA: 0x00067248 File Offset: 0x00065448
		public void InvokeOnRenderObjectCallback()
		{
			this.InvokeOnRenderObjectCallback_Internal();
		}

		// Token: 0x0600348F RID: 13455 RVA: 0x00067252 File Offset: 0x00065452
		public void DrawGizmos(Camera camera, GizmoSubset gizmoSubset)
		{
			this.DrawGizmos_Internal(camera, gizmoSubset);
		}

		// Token: 0x06003490 RID: 13456 RVA: 0x0006725E File Offset: 0x0006545E
		public void DrawWireOverlay(Camera camera)
		{
			this.DrawWireOverlay_Impl(camera);
		}

		// Token: 0x06003491 RID: 13457 RVA: 0x00067269 File Offset: 0x00065469
		public void DrawUIOverlay(Camera camera)
		{
			this.DrawUIOverlay_Internal(camera);
		}

		// Token: 0x06003492 RID: 13458 RVA: 0x00067274 File Offset: 0x00065474
		public unsafe CullingResults Cull(ref ScriptableCullingParameters parameters)
		{
			CullingResults cullingResults = default(CullingResults);
			ScriptableRenderContext.Internal_Cull(ref parameters, this, (IntPtr)((void*)(&cullingResults)));
			return cullingResults;
		}

		// Token: 0x06003493 RID: 13459 RVA: 0x000672A4 File Offset: 0x000654A4
		private unsafe void ValidateCullShadowCastersParameters(in CullingResults cullingResults, in ShadowCastersCullingInfos cullingInfos)
		{
			bool flag = false;
			if (flag)
			{
				throw new UnityException("CullingResults is null");
			}
			NativeArray<LightShadowCasterCullingInfo> nativeArray = cullingInfos.perLightInfos;
			bool flag2 = nativeArray.Length == 0;
			if (!flag2)
			{
				CullingResults cullingResults2 = cullingResults;
				int length = cullingResults2.visibleLights.Length;
				nativeArray = cullingInfos.perLightInfos;
				bool flag3 = length != nativeArray.Length;
				if (flag3)
				{
					string text = "CullingResults.visibleLights.Length ({0}) != ShadowCastersCullingInfos.perLightInfos.Length ({1}). ";
					cullingResults2 = cullingResults;
					object obj = cullingResults2.visibleLights.Length;
					nativeArray = cullingInfos.perLightInfos;
					throw new UnityException(string.Format(text, obj, nativeArray.Length) + "ShadowCastersCullingInfos.perLightInfos must have one entry per visible light.");
				}
				LightShadowCasterCullingInfo* unsafeReadOnlyPtr = (LightShadowCasterCullingInfo*)cullingInfos.perLightInfos.GetUnsafeReadOnlyPtr<LightShadowCasterCullingInfo>();
				int num = 0;
				ref LightShadowCasterCullingInfo ptr;
				RangeInt splitRange;
				NativeArray<ShadowSplitData> nativeArray2;
				for (;;)
				{
					int num2 = num;
					nativeArray = cullingInfos.perLightInfos;
					if (num2 >= nativeArray.Length)
					{
						return;
					}
					ptr = ref unsafeReadOnlyPtr[num];
					splitRange = ptr.splitRange;
					int start = splitRange.start;
					int length2 = splitRange.length;
					int num3 = start + length2;
					bool flag4 = start == 0 && length2 == 0;
					if (!flag4)
					{
						bool flag5;
						if (start >= 0)
						{
							int num4 = start;
							nativeArray2 = cullingInfos.splitBuffer;
							flag5 = num4 <= nativeArray2.Length;
						}
						else
						{
							flag5 = false;
						}
						bool flag6 = flag5;
						bool flag7 = length2 >= 0 && length2 <= 6;
						bool flag8;
						if (num3 >= start)
						{
							int num5 = num3;
							nativeArray2 = cullingInfos.splitBuffer;
							flag8 = num5 <= nativeArray2.Length;
						}
						else
						{
							flag8 = false;
						}
						bool flag9 = flag8;
						bool flag10 = flag6 && flag7 && flag9;
						bool flag11 = !flag10;
						if (flag11)
						{
							break;
						}
						bool flag12 = length2 > 0 && ptr.projectionType == BatchCullingProjectionType.Unknown;
						if (flag12)
						{
							goto Block_11;
						}
						bool flag13 = ptr.splitExclusionMask >> length2 != 0;
						if (flag13)
						{
							goto Block_12;
						}
					}
					num++;
				}
				string text2 = string.Format("ShadowCastersCullingInfos.perLightInfos[{0}] is referring to an invalid memory location. ", num);
				string text3 = string.Format("splitRange.start ({0}) splitRange.length ({1}) ", splitRange.start, splitRange.length);
				string text4 = "ShadowCastersCullingInfos.splitBuffer.Length ({0}).";
				nativeArray2 = cullingInfos.splitBuffer;
				throw new UnityException(text2 + text3 + string.Format(text4, nativeArray2.Length));
				Block_11:
				throw new UnityException(string.Format("ShadowCastersCullingInfos.perLightInfos[{0}].projectionType == {1}. ", num, ptr.projectionType) + string.Format("The range however appears to be valid. splitRange.start ({0}) splitRange.length ({1}).", splitRange.start, splitRange.length));
				Block_12:
				string text5 = Convert.ToString((int)ptr.splitExclusionMask, 2);
				throw new UnityException(string.Format("ShadowCastersCullingInfos.perLightInfos[{0}].splitExclusionMask == 0b{1}. ", num, text5) + string.Format("The highest bit set must be less than the split count. splitRange.start ({0}) splitRange.length ({1}).", splitRange.start, splitRange.length));
			}
		}

		// Token: 0x06003494 RID: 13460 RVA: 0x00067568 File Offset: 0x00065768
		public unsafe void CullShadowCasters(CullingResults cullingResults, ShadowCastersCullingInfos infos)
		{
			ScriptableRenderContext.CullShadowCastersContext cullShadowCastersContext = default(ScriptableRenderContext.CullShadowCastersContext);
			cullShadowCastersContext.cullResults = cullingResults.ptr;
			cullShadowCastersContext.splitBuffer = (ShadowSplitData*)infos.splitBuffer.GetUnsafePtr<ShadowSplitData>();
			cullShadowCastersContext.splitBufferLength = infos.splitBuffer.Length;
			cullShadowCastersContext.perLightInfos = (LightShadowCasterCullingInfo*)infos.perLightInfos.GetUnsafePtr<LightShadowCasterCullingInfo>();
			cullShadowCastersContext.perLightInfoCount = infos.perLightInfos.Length;
			ScriptableRenderContext.Internal_CullShadowCasters(this, (IntPtr)((void*)(&cullShadowCastersContext)));
		}

		// Token: 0x06003495 RID: 13461 RVA: 0x00002925 File Offset: 0x00000B25
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		internal void Validate()
		{
		}

		// Token: 0x06003496 RID: 13462 RVA: 0x000675EC File Offset: 0x000657EC
		public bool Equals(ScriptableRenderContext other)
		{
			return this.m_Ptr.Equals(other.m_Ptr);
		}

		// Token: 0x06003497 RID: 13463 RVA: 0x00067614 File Offset: 0x00065814
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is ScriptableRenderContext && this.Equals((ScriptableRenderContext)obj);
		}

		// Token: 0x06003498 RID: 13464 RVA: 0x0006764C File Offset: 0x0006584C
		public override int GetHashCode()
		{
			return this.m_Ptr.GetHashCode();
		}

		// Token: 0x06003499 RID: 13465 RVA: 0x0006766C File Offset: 0x0006586C
		public static bool operator ==(ScriptableRenderContext left, ScriptableRenderContext right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600349A RID: 13466 RVA: 0x00067688 File Offset: 0x00065888
		public static bool operator !=(ScriptableRenderContext left, ScriptableRenderContext right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0600349B RID: 13467 RVA: 0x000676A8 File Offset: 0x000658A8
		public RendererList CreateRendererList(RendererListDesc desc)
		{
			RendererListParams rendererListParams = RendererListDesc.ConvertToParameters(in desc);
			RendererList rendererList = this.CreateRendererList(ref rendererListParams);
			rendererListParams.Dispose();
			return rendererList;
		}

		// Token: 0x0600349C RID: 13468 RVA: 0x000676D4 File Offset: 0x000658D4
		public RendererList CreateRendererList(ref RendererListParams param)
		{
			param.Validate();
			return this.CreateRendererList_Internal(param.cullingResults.ptr, ref param.drawSettings, ref param.filteringSettings, param.tagName, param.isPassTagName, param.tagsValuePtr, param.stateBlocksPtr, param.numStateBlocks);
		}

		// Token: 0x0600349D RID: 13469 RVA: 0x0006772C File Offset: 0x0006592C
		public unsafe RendererList CreateShadowRendererList(ref ShadowDrawingSettings settings)
		{
			fixed (ShadowDrawingSettings* ptr = &settings)
			{
				ShadowDrawingSettings* ptr2 = ptr;
				return this.CreateShadowRendererList_Internal((IntPtr)((void*)ptr2));
			}
		}

		// Token: 0x0600349E RID: 13470 RVA: 0x00067750 File Offset: 0x00065950
		public RendererList CreateSkyboxRendererList(Camera camera, Matrix4x4 projectionMatrixL, Matrix4x4 viewMatrixL, Matrix4x4 projectionMatrixR, Matrix4x4 viewMatrixR)
		{
			return this.CreateSkyboxRendererList_Internal(camera, 2, projectionMatrixL, viewMatrixL, projectionMatrixR, viewMatrixR);
		}

		// Token: 0x0600349F RID: 13471 RVA: 0x00067770 File Offset: 0x00065970
		public RendererList CreateSkyboxRendererList(Camera camera, Matrix4x4 projectionMatrix, Matrix4x4 viewMatrix)
		{
			return this.CreateSkyboxRendererList_Internal(camera, 1, projectionMatrix, viewMatrix, Matrix4x4.identity, Matrix4x4.identity);
		}

		// Token: 0x060034A0 RID: 13472 RVA: 0x00067798 File Offset: 0x00065998
		public RendererList CreateSkyboxRendererList(Camera camera)
		{
			return this.CreateSkyboxRendererList_Internal(camera, 0, Matrix4x4.identity, Matrix4x4.identity, Matrix4x4.identity, Matrix4x4.identity);
		}

		// Token: 0x060034A1 RID: 13473 RVA: 0x000677C8 File Offset: 0x000659C8
		public RendererList CreateGizmoRendererList(Camera camera, GizmoSubset gizmoSubset)
		{
			return this.CreateGizmoRendererList_Internal(camera, gizmoSubset);
		}

		// Token: 0x060034A2 RID: 13474 RVA: 0x000677E4 File Offset: 0x000659E4
		public RendererList CreateUIOverlayRendererList(Camera camera)
		{
			return this.CreateUIOverlayRendererList_Internal(camera, UISubset.All);
		}

		// Token: 0x060034A3 RID: 13475 RVA: 0x00067800 File Offset: 0x00065A00
		public RendererList CreateUIOverlayRendererList(Camera camera, UISubset uiSubset)
		{
			return this.CreateUIOverlayRendererList_Internal(camera, uiSubset);
		}

		// Token: 0x060034A4 RID: 13476 RVA: 0x0006781C File Offset: 0x00065A1C
		public RendererList CreateWireOverlayRendererList(Camera camera)
		{
			return this.CreateWireOverlayRendererList_Internal(camera);
		}

		// Token: 0x060034A5 RID: 13477 RVA: 0x00067835 File Offset: 0x00065A35
		public void PrepareRendererListsAsync(List<RendererList> rendererLists)
		{
			this.PrepareRendererListsAsync_Internal(rendererLists);
		}

		// Token: 0x060034A6 RID: 13478 RVA: 0x00067840 File Offset: 0x00065A40
		public RendererListStatus QueryRendererListStatus(RendererList rendererList)
		{
			return this.QueryRendererListStatus_Internal(rendererList);
		}

		// Token: 0x060034A8 RID: 13480
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Cull_Injected(ref ScriptableCullingParameters parameters, [In] ref ScriptableRenderContext renderLoop, IntPtr results);

		// Token: 0x060034A9 RID: 13481
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CullShadowCasters_Injected([In] ref ScriptableRenderContext renderLoop, IntPtr context);

		// Token: 0x060034AA RID: 13482
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InitializeSortSettings_Injected(IntPtr camera, out SortingSettings sortingSettings);

		// Token: 0x060034AB RID: 13483
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DrawRenderers_Internal_Injected(ref ScriptableRenderContext _unity_self, IntPtr cullResults, ref DrawingSettings drawingSettings, ref FilteringSettings filteringSettings, [In] ref ShaderTagId tagName, bool isPassTagName, IntPtr tagValues, IntPtr stateBlocks, int stateCount);

		// Token: 0x060034AC RID: 13484
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EmitGeometryForCamera_Injected(IntPtr camera);

		// Token: 0x060034AD RID: 13485
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ExecuteCommandBuffer_Internal_Injected(ref ScriptableRenderContext _unity_self, IntPtr commandBuffer);

		// Token: 0x060034AE RID: 13486
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ExecuteCommandBufferAsync_Internal_Injected(ref ScriptableRenderContext _unity_self, IntPtr commandBuffer, ComputeQueueType queueType);

		// Token: 0x060034AF RID: 13487
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetupCameraProperties_Internal_Injected(ref ScriptableRenderContext _unity_self, IntPtr camera, bool stereoSetup, int eye);

		// Token: 0x060034B0 RID: 13488
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void StereoEndRender_Internal_Injected(ref ScriptableRenderContext _unity_self, IntPtr camera, int eye, bool isFinalPass);

		// Token: 0x060034B1 RID: 13489
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void StartMultiEye_Internal_Injected(ref ScriptableRenderContext _unity_self, IntPtr camera, int eye);

		// Token: 0x060034B2 RID: 13490
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void StopMultiEye_Internal_Injected(ref ScriptableRenderContext _unity_self, IntPtr camera);

		// Token: 0x060034B3 RID: 13491
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DrawSkybox_Internal_Injected(ref ScriptableRenderContext _unity_self, IntPtr camera);

		// Token: 0x060034B4 RID: 13492
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DrawGizmos_Internal_Injected(ref ScriptableRenderContext _unity_self, IntPtr camera, GizmoSubset gizmoSubset);

		// Token: 0x060034B5 RID: 13493
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DrawWireOverlay_Impl_Injected(ref ScriptableRenderContext _unity_self, IntPtr camera);

		// Token: 0x060034B6 RID: 13494
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DrawUIOverlay_Internal_Injected(ref ScriptableRenderContext _unity_self, IntPtr camera);

		// Token: 0x060034B7 RID: 13495
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateRendererList_Internal_Injected(ref ScriptableRenderContext _unity_self, IntPtr cullResults, ref DrawingSettings drawingSettings, ref FilteringSettings filteringSettings, [In] ref ShaderTagId tagName, bool isPassTagName, IntPtr tagValues, IntPtr stateBlocks, int stateCount, out RendererList ret);

		// Token: 0x060034B8 RID: 13496
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateShadowRendererList_Internal_Injected(ref ScriptableRenderContext _unity_self, IntPtr shadowDrawinSettings, out RendererList ret);

		// Token: 0x060034B9 RID: 13497
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateSkyboxRendererList_Internal_Injected(ref ScriptableRenderContext _unity_self, IntPtr camera, int mode, [In] ref Matrix4x4 proj, [In] ref Matrix4x4 view, [In] ref Matrix4x4 projR, [In] ref Matrix4x4 viewR, out RendererList ret);

		// Token: 0x060034BA RID: 13498
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateGizmoRendererList_Internal_Injected(ref ScriptableRenderContext _unity_self, IntPtr camera, GizmoSubset gizmoSubset, out RendererList ret);

		// Token: 0x060034BB RID: 13499
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateUIOverlayRendererList_Internal_Injected(ref ScriptableRenderContext _unity_self, IntPtr camera, UISubset uiSubset, out RendererList ret);

		// Token: 0x060034BC RID: 13500
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateWireOverlayRendererList_Internal_Injected(ref ScriptableRenderContext _unity_self, IntPtr camera, out RendererList ret);

		// Token: 0x060034BD RID: 13501
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RendererListStatus QueryRendererListStatus_Internal_Injected(ref ScriptableRenderContext _unity_self, [In] ref RendererList handle);

		// Token: 0x040011A2 RID: 4514
		private static readonly ShaderTagId kRenderTypeTag = new ShaderTagId("RenderType");

		// Token: 0x040011A3 RID: 4515
		private IntPtr m_Ptr;

		// Token: 0x040011A4 RID: 4516
		private const bool deprecateDrawXmethods = false;

		// Token: 0x02000555 RID: 1365
		internal enum SkyboxXRMode
		{
			// Token: 0x040011A6 RID: 4518
			Off,
			// Token: 0x040011A7 RID: 4519
			Enabled,
			// Token: 0x040011A8 RID: 4520
			LegacySinglePass
		}

		// Token: 0x02000556 RID: 1366
		private struct CullShadowCastersContext
		{
			// Token: 0x040011A9 RID: 4521
			public IntPtr cullResults;

			// Token: 0x040011AA RID: 4522
			public unsafe ShadowSplitData* splitBuffer;

			// Token: 0x040011AB RID: 4523
			public int splitBufferLength;

			// Token: 0x040011AC RID: 4524
			public unsafe LightShadowCasterCullingInfo* perLightInfos;

			// Token: 0x040011AD RID: 4525
			public int perLightInfoCount;
		}
	}
}
