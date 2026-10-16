using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine.U2D
{
	// Token: 0x020005B7 RID: 1463
	[NativeHeader("Runtime/2D/Common/SpriteDataAccess.h")]
	[NativeHeader("Runtime/Graphics/SpriteFrame.h")]
	public static class SpriteDataAccessExtensions
	{
		// Token: 0x060037F5 RID: 14325 RVA: 0x0006DDC4 File Offset: 0x0006BFC4
		private static void CheckAttributeTypeMatchesAndThrow<T>(VertexAttribute channel)
		{
			bool flag;
			switch (channel)
			{
			case VertexAttribute.Position:
			case VertexAttribute.Normal:
				flag = typeof(T) == typeof(Vector3);
				break;
			case VertexAttribute.Tangent:
				flag = typeof(T) == typeof(Vector4);
				break;
			case VertexAttribute.Color:
				flag = typeof(T) == typeof(Color32);
				break;
			case VertexAttribute.TexCoord0:
			case VertexAttribute.TexCoord1:
			case VertexAttribute.TexCoord2:
			case VertexAttribute.TexCoord3:
			case VertexAttribute.TexCoord4:
			case VertexAttribute.TexCoord5:
			case VertexAttribute.TexCoord6:
			case VertexAttribute.TexCoord7:
				flag = typeof(T) == typeof(Vector2);
				break;
			case VertexAttribute.BlendWeight:
				flag = typeof(T) == typeof(BoneWeight);
				break;
			default:
				throw new InvalidOperationException(string.Format("The requested channel '{0}' is unknown.", channel));
			}
			bool flag2 = !flag;
			if (flag2)
			{
				throw new InvalidOperationException(string.Format("The requested channel '{0}' does not match the return type {1}.", channel, typeof(T).Name));
			}
		}

		// Token: 0x060037F6 RID: 14326 RVA: 0x0006DEEC File Offset: 0x0006C0EC
		public unsafe static NativeSlice<T> GetVertexAttribute<T>(this Sprite sprite, VertexAttribute channel) where T : struct
		{
			SpriteDataAccessExtensions.CheckAttributeTypeMatchesAndThrow<T>(channel);
			SpriteChannelInfo channelInfo = SpriteDataAccessExtensions.GetChannelInfo(sprite, channel);
			byte* ptr = (byte*)channelInfo.buffer + channelInfo.offset;
			return NativeSliceUnsafeUtility.ConvertExistingDataToNativeSlice<T>((void*)ptr, channelInfo.stride, channelInfo.count);
		}

		// Token: 0x060037F7 RID: 14327 RVA: 0x0006DF33 File Offset: 0x0006C133
		public static void SetVertexAttribute<T>(this Sprite sprite, VertexAttribute channel, NativeArray<T> src) where T : struct
		{
			SpriteDataAccessExtensions.CheckAttributeTypeMatchesAndThrow<T>(channel);
			SpriteDataAccessExtensions.SetChannelData(sprite, channel, src.GetUnsafeReadOnlyPtr<T>());
		}

		// Token: 0x060037F8 RID: 14328 RVA: 0x0006DF4C File Offset: 0x0006C14C
		public static NativeArray<Matrix4x4> GetBindPoses(this Sprite sprite)
		{
			SpriteChannelInfo bindPoseInfo = SpriteDataAccessExtensions.GetBindPoseInfo(sprite);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Matrix4x4>(bindPoseInfo.buffer, bindPoseInfo.count, Allocator.None);
		}

		// Token: 0x060037F9 RID: 14329 RVA: 0x0006DF7B File Offset: 0x0006C17B
		public static void SetBindPoses(this Sprite sprite, NativeArray<Matrix4x4> src)
		{
			SpriteDataAccessExtensions.SetBindPoseData(sprite, src.GetUnsafeReadOnlyPtr<Matrix4x4>(), src.Length);
		}

		// Token: 0x060037FA RID: 14330 RVA: 0x0006DF94 File Offset: 0x0006C194
		public static NativeArray<ushort> GetIndices(this Sprite sprite)
		{
			SpriteChannelInfo indicesInfo = SpriteDataAccessExtensions.GetIndicesInfo(sprite);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<ushort>(indicesInfo.buffer, indicesInfo.count, Allocator.Invalid);
		}

		// Token: 0x060037FB RID: 14331 RVA: 0x0006DFC3 File Offset: 0x0006C1C3
		public static void SetIndices(this Sprite sprite, NativeArray<ushort> src)
		{
			SpriteDataAccessExtensions.SetIndicesData(sprite, src.GetUnsafeReadOnlyPtr<ushort>(), src.Length);
		}

		// Token: 0x060037FC RID: 14332 RVA: 0x0006DFDC File Offset: 0x0006C1DC
		public static SpriteBone[] GetBones(this Sprite sprite)
		{
			return SpriteDataAccessExtensions.GetBoneInfo(sprite);
		}

		// Token: 0x060037FD RID: 14333 RVA: 0x0006DFF4 File Offset: 0x0006C1F4
		public static void SetBones(this Sprite sprite, SpriteBone[] src)
		{
			SpriteDataAccessExtensions.SetBoneData(sprite, src);
		}

		// Token: 0x060037FE RID: 14334 RVA: 0x0006E000 File Offset: 0x0006C200
		[NativeName("HasChannel")]
		public static bool HasVertexAttribute([NotNull] this Sprite sprite, VertexAttribute channel)
		{
			if (sprite == null)
			{
				ThrowHelper.ThrowArgumentNullException(sprite, "sprite");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(sprite);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(sprite, "sprite");
			}
			return SpriteDataAccessExtensions.HasVertexAttribute_Injected(intPtr, channel);
		}

		// Token: 0x060037FF RID: 14335 RVA: 0x0006E038 File Offset: 0x0006C238
		public static void SetVertexCount([NotNull] this Sprite sprite, int count)
		{
			if (sprite == null)
			{
				ThrowHelper.ThrowArgumentNullException(sprite, "sprite");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(sprite);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(sprite, "sprite");
			}
			SpriteDataAccessExtensions.SetVertexCount_Injected(intPtr, count);
		}

		// Token: 0x06003800 RID: 14336 RVA: 0x0006E070 File Offset: 0x0006C270
		public static int GetVertexCount([NotNull] this Sprite sprite)
		{
			if (sprite == null)
			{
				ThrowHelper.ThrowArgumentNullException(sprite, "sprite");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(sprite);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(sprite, "sprite");
			}
			return SpriteDataAccessExtensions.GetVertexCount_Injected(intPtr);
		}

		// Token: 0x06003801 RID: 14337 RVA: 0x0006E0A8 File Offset: 0x0006C2A8
		private static SpriteChannelInfo GetBindPoseInfo([NotNull] Sprite sprite)
		{
			if (sprite == null)
			{
				ThrowHelper.ThrowArgumentNullException(sprite, "sprite");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(sprite);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(sprite, "sprite");
			}
			SpriteChannelInfo spriteChannelInfo;
			SpriteDataAccessExtensions.GetBindPoseInfo_Injected(intPtr, out spriteChannelInfo);
			return spriteChannelInfo;
		}

		// Token: 0x06003802 RID: 14338 RVA: 0x0006E0E4 File Offset: 0x0006C2E4
		private unsafe static void SetBindPoseData([NotNull] Sprite sprite, void* src, int count)
		{
			if (sprite == null)
			{
				ThrowHelper.ThrowArgumentNullException(sprite, "sprite");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(sprite);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(sprite, "sprite");
			}
			SpriteDataAccessExtensions.SetBindPoseData_Injected(intPtr, src, count);
		}

		// Token: 0x06003803 RID: 14339 RVA: 0x0006E11C File Offset: 0x0006C31C
		private static SpriteChannelInfo GetIndicesInfo([NotNull] Sprite sprite)
		{
			if (sprite == null)
			{
				ThrowHelper.ThrowArgumentNullException(sprite, "sprite");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(sprite);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(sprite, "sprite");
			}
			SpriteChannelInfo spriteChannelInfo;
			SpriteDataAccessExtensions.GetIndicesInfo_Injected(intPtr, out spriteChannelInfo);
			return spriteChannelInfo;
		}

		// Token: 0x06003804 RID: 14340 RVA: 0x0006E158 File Offset: 0x0006C358
		private unsafe static void SetIndicesData([NotNull] Sprite sprite, void* src, int count)
		{
			if (sprite == null)
			{
				ThrowHelper.ThrowArgumentNullException(sprite, "sprite");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(sprite);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(sprite, "sprite");
			}
			SpriteDataAccessExtensions.SetIndicesData_Injected(intPtr, src, count);
		}

		// Token: 0x06003805 RID: 14341 RVA: 0x0006E190 File Offset: 0x0006C390
		private static SpriteChannelInfo GetChannelInfo([NotNull] Sprite sprite, VertexAttribute channel)
		{
			if (sprite == null)
			{
				ThrowHelper.ThrowArgumentNullException(sprite, "sprite");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(sprite);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(sprite, "sprite");
			}
			SpriteChannelInfo spriteChannelInfo;
			SpriteDataAccessExtensions.GetChannelInfo_Injected(intPtr, channel, out spriteChannelInfo);
			return spriteChannelInfo;
		}

		// Token: 0x06003806 RID: 14342 RVA: 0x0006E1CC File Offset: 0x0006C3CC
		private unsafe static void SetChannelData([NotNull] Sprite sprite, VertexAttribute channel, void* src)
		{
			if (sprite == null)
			{
				ThrowHelper.ThrowArgumentNullException(sprite, "sprite");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(sprite);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(sprite, "sprite");
			}
			SpriteDataAccessExtensions.SetChannelData_Injected(intPtr, channel, src);
		}

		// Token: 0x06003807 RID: 14343 RVA: 0x0006E204 File Offset: 0x0006C404
		private static SpriteBone[] GetBoneInfo([NotNull] Sprite sprite)
		{
			if (sprite == null)
			{
				ThrowHelper.ThrowArgumentNullException(sprite, "sprite");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(sprite);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(sprite, "sprite");
			}
			return SpriteDataAccessExtensions.GetBoneInfo_Injected(intPtr);
		}

		// Token: 0x06003808 RID: 14344 RVA: 0x0006E23C File Offset: 0x0006C43C
		private static void SetBoneData([NotNull] Sprite sprite, SpriteBone[] src)
		{
			if (sprite == null)
			{
				ThrowHelper.ThrowArgumentNullException(sprite, "sprite");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(sprite);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(sprite, "sprite");
			}
			SpriteDataAccessExtensions.SetBoneData_Injected(intPtr, src);
		}

		// Token: 0x06003809 RID: 14345 RVA: 0x0006E274 File Offset: 0x0006C474
		internal static int GetPrimaryVertexStreamSize(Sprite sprite)
		{
			return SpriteDataAccessExtensions.GetPrimaryVertexStreamSize_Injected(Object.MarshalledUnityObject.Marshal<Sprite>(sprite));
		}

		// Token: 0x0600380A RID: 14346
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasVertexAttribute_Injected(IntPtr sprite, VertexAttribute channel);

		// Token: 0x0600380B RID: 14347
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetVertexCount_Injected(IntPtr sprite, int count);

		// Token: 0x0600380C RID: 14348
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetVertexCount_Injected(IntPtr sprite);

		// Token: 0x0600380D RID: 14349
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetBindPoseInfo_Injected(IntPtr sprite, out SpriteChannelInfo ret);

		// Token: 0x0600380E RID: 14350
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void SetBindPoseData_Injected(IntPtr sprite, void* src, int count);

		// Token: 0x0600380F RID: 14351
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetIndicesInfo_Injected(IntPtr sprite, out SpriteChannelInfo ret);

		// Token: 0x06003810 RID: 14352
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void SetIndicesData_Injected(IntPtr sprite, void* src, int count);

		// Token: 0x06003811 RID: 14353
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetChannelInfo_Injected(IntPtr sprite, VertexAttribute channel, out SpriteChannelInfo ret);

		// Token: 0x06003812 RID: 14354
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void SetChannelData_Injected(IntPtr sprite, VertexAttribute channel, void* src);

		// Token: 0x06003813 RID: 14355
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern SpriteBone[] GetBoneInfo_Injected(IntPtr sprite);

		// Token: 0x06003814 RID: 14356
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetBoneData_Injected(IntPtr sprite, SpriteBone[] src);

		// Token: 0x06003815 RID: 14357
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetPrimaryVertexStreamSize_Injected(IntPtr sprite);
	}
}
