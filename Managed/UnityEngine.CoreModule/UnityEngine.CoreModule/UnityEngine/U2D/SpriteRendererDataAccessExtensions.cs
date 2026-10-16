using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine.U2D
{
	// Token: 0x020005B8 RID: 1464
	[NativeHeader("Runtime/2D/Common/SpriteDataAccess.h")]
	[NativeHeader("Runtime/Graphics/Mesh/SpriteRenderer.h")]
	public static class SpriteRendererDataAccessExtensions
	{
		// Token: 0x06003816 RID: 14358 RVA: 0x0006E28C File Offset: 0x0006C48C
		internal static void SetDeformableBuffer(this SpriteRenderer spriteRenderer, NativeArray<byte> src)
		{
			bool flag = spriteRenderer.sprite == null;
			if (flag)
			{
				throw new ArgumentException(string.Format("spriteRenderer does not have a valid sprite set.", Array.Empty<object>()));
			}
			bool flag2 = src.Length != SpriteDataAccessExtensions.GetPrimaryVertexStreamSize(spriteRenderer.sprite);
			if (flag2)
			{
				throw new InvalidOperationException(string.Format("custom sprite vertex data size must match sprite asset's vertex data size {0} {1}", src.Length, SpriteDataAccessExtensions.GetPrimaryVertexStreamSize(spriteRenderer.sprite)));
			}
			SpriteRendererDataAccessExtensions.SetDeformableBuffer(spriteRenderer, src.GetUnsafeReadOnlyPtr<byte>(), src.Length);
		}

		// Token: 0x06003817 RID: 14359 RVA: 0x0006E31C File Offset: 0x0006C51C
		internal static void SetDeformableBuffer(this SpriteRenderer spriteRenderer, NativeArray<Vector3> src)
		{
			bool flag = spriteRenderer.sprite == null;
			if (flag)
			{
				throw new InvalidOperationException("spriteRenderer does not have a valid sprite set.");
			}
			bool flag2 = src.Length != spriteRenderer.sprite.GetVertexCount();
			if (flag2)
			{
				throw new InvalidOperationException(string.Format("The src length {0} must match the vertex count of source Sprite {1}.", src.Length, spriteRenderer.sprite.GetVertexCount()));
			}
			SpriteRendererDataAccessExtensions.SetDeformableBuffer(spriteRenderer, src.GetUnsafeReadOnlyPtr<Vector3>(), src.Length);
		}

		// Token: 0x06003818 RID: 14360 RVA: 0x0006E3A0 File Offset: 0x0006C5A0
		internal static void SetBatchDeformableBufferAndLocalAABBArray(SpriteRenderer[] spriteRenderers, NativeArray<IntPtr> buffers, NativeArray<int> bufferSizes, NativeArray<Bounds> bounds)
		{
			int num = spriteRenderers.Length;
			bool flag = num != buffers.Length || num != bufferSizes.Length || num != bounds.Length;
			if (flag)
			{
				throw new ArgumentException("Input array sizes are not the same.");
			}
			SpriteRendererDataAccessExtensions.SetBatchDeformableBufferAndLocalAABBArray(spriteRenderers, buffers.GetUnsafeReadOnlyPtr<IntPtr>(), bufferSizes.GetUnsafeReadOnlyPtr<int>(), bounds.GetUnsafeReadOnlyPtr<Bounds>(), num);
		}

		// Token: 0x06003819 RID: 14361 RVA: 0x0006E400 File Offset: 0x0006C600
		internal static void SetBoneTransformsArray(SpriteRenderer[] spriteRenderers, NativeArray<IntPtr> buffers, NativeArray<int> bufferSizes, NativeArray<Bounds> bounds)
		{
			int num = spriteRenderers.Length;
			bool flag = num != buffers.Length || num != bufferSizes.Length || num != bounds.Length;
			if (flag)
			{
				throw new ArgumentException("Input array sizes are not the same.");
			}
			SpriteRendererDataAccessExtensions.SetBoneTransformsArray(spriteRenderers, buffers.GetUnsafeReadOnlyPtr<IntPtr>(), bufferSizes.GetUnsafeReadOnlyPtr<int>(), bounds.GetUnsafeReadOnlyPtr<Bounds>(), num);
		}

		// Token: 0x0600381A RID: 14362 RVA: 0x0006E460 File Offset: 0x0006C660
		internal unsafe static bool IsUsingDeformableBuffer(this SpriteRenderer spriteRenderer, IntPtr buffer)
		{
			return SpriteRendererDataAccessExtensions.IsUsingDeformableBuffer(spriteRenderer, (void*)buffer);
		}

		// Token: 0x0600381B RID: 14363 RVA: 0x0006E47E File Offset: 0x0006C67E
		internal static void SetBoneTransforms(this SpriteRenderer spriteRenderer, NativeArray<Matrix4x4> src)
		{
			SpriteRendererDataAccessExtensions.SetBoneTransforms(spriteRenderer, src.GetUnsafeReadOnlyPtr<Matrix4x4>(), src.Length);
		}

		// Token: 0x0600381C RID: 14364 RVA: 0x0006E498 File Offset: 0x0006C698
		public static void DeactivateDeformableBuffer([NotNull] this SpriteRenderer renderer)
		{
			if (renderer == null)
			{
				ThrowHelper.ThrowArgumentNullException(renderer, "renderer");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(renderer);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(renderer, "renderer");
			}
			SpriteRendererDataAccessExtensions.DeactivateDeformableBuffer_Injected(intPtr);
		}

		// Token: 0x0600381D RID: 14365 RVA: 0x0006E4D0 File Offset: 0x0006C6D0
		internal static void SetLocalAABB([NotNull] this SpriteRenderer renderer, Bounds aabb)
		{
			if (renderer == null)
			{
				ThrowHelper.ThrowArgumentNullException(renderer, "renderer");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(renderer);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(renderer, "renderer");
			}
			SpriteRendererDataAccessExtensions.SetLocalAABB_Injected(intPtr, ref aabb);
		}

		// Token: 0x0600381E RID: 14366 RVA: 0x0006E508 File Offset: 0x0006C708
		private unsafe static void SetDeformableBuffer([NotNull] SpriteRenderer spriteRenderer, void* src, int count)
		{
			if (spriteRenderer == null)
			{
				ThrowHelper.ThrowArgumentNullException(spriteRenderer, "spriteRenderer");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(spriteRenderer);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(spriteRenderer, "spriteRenderer");
			}
			SpriteRendererDataAccessExtensions.SetDeformableBuffer_Injected(intPtr, src, count);
		}

		// Token: 0x0600381F RID: 14367
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void SetBatchDeformableBufferAndLocalAABBArray(SpriteRenderer[] spriteRenderers, void* buffers, void* bufferSizes, void* bounds, int count);

		// Token: 0x06003820 RID: 14368 RVA: 0x0006E540 File Offset: 0x0006C740
		private unsafe static bool IsUsingDeformableBuffer([NotNull] SpriteRenderer spriteRenderer, void* buffer)
		{
			if (spriteRenderer == null)
			{
				ThrowHelper.ThrowArgumentNullException(spriteRenderer, "spriteRenderer");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(spriteRenderer);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(spriteRenderer, "spriteRenderer");
			}
			return SpriteRendererDataAccessExtensions.IsUsingDeformableBuffer_Injected(intPtr, buffer);
		}

		// Token: 0x06003821 RID: 14369 RVA: 0x0006E578 File Offset: 0x0006C778
		private unsafe static void SetBoneTransforms([NotNull] SpriteRenderer spriteRenderer, void* src, int count)
		{
			if (spriteRenderer == null)
			{
				ThrowHelper.ThrowArgumentNullException(spriteRenderer, "spriteRenderer");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(spriteRenderer);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(spriteRenderer, "spriteRenderer");
			}
			SpriteRendererDataAccessExtensions.SetBoneTransforms_Injected(intPtr, src, count);
		}

		// Token: 0x06003822 RID: 14370
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void SetBoneTransformsArray(SpriteRenderer[] spriteRenderers, void* buffers, void* bufferSizes, void* bounds, int count);

		// Token: 0x06003823 RID: 14371 RVA: 0x0006E5B0 File Offset: 0x0006C7B0
		internal static void SetupMaterialProperties([NotNull] SpriteRenderer spriteRenderer)
		{
			if (spriteRenderer == null)
			{
				ThrowHelper.ThrowArgumentNullException(spriteRenderer, "spriteRenderer");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(spriteRenderer);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(spriteRenderer, "spriteRenderer");
			}
			SpriteRendererDataAccessExtensions.SetupMaterialProperties_Injected(intPtr);
		}

		// Token: 0x06003824 RID: 14372
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsGPUSkinningEnabled();

		// Token: 0x06003825 RID: 14373 RVA: 0x0006E5E8 File Offset: 0x0006C7E8
		internal static bool IsSRPBatchingEnabled([NotNull] SpriteRenderer spriteRenderer)
		{
			if (spriteRenderer == null)
			{
				ThrowHelper.ThrowArgumentNullException(spriteRenderer, "spriteRenderer");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(spriteRenderer);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(spriteRenderer, "spriteRenderer");
			}
			return SpriteRendererDataAccessExtensions.IsSRPBatchingEnabled_Injected(intPtr);
		}

		// Token: 0x06003826 RID: 14374
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DeactivateDeformableBuffer_Injected(IntPtr renderer);

		// Token: 0x06003827 RID: 14375
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLocalAABB_Injected(IntPtr renderer, [In] ref Bounds aabb);

		// Token: 0x06003828 RID: 14376
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void SetDeformableBuffer_Injected(IntPtr spriteRenderer, void* src, int count);

		// Token: 0x06003829 RID: 14377
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern bool IsUsingDeformableBuffer_Injected(IntPtr spriteRenderer, void* buffer);

		// Token: 0x0600382A RID: 14378
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void SetBoneTransforms_Injected(IntPtr spriteRenderer, void* src, int count);

		// Token: 0x0600382B RID: 14379
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetupMaterialProperties_Injected(IntPtr spriteRenderer);

		// Token: 0x0600382C RID: 14380
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsSRPBatchingEnabled_Injected(IntPtr spriteRenderer);
	}
}
