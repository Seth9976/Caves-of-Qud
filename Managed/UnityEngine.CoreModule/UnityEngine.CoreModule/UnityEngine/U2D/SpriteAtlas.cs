using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.U2D
{
	// Token: 0x020005BA RID: 1466
	[NativeType(Header = "Runtime/2D/SpriteAtlas/SpriteAtlas.h")]
	[NativeHeader("Runtime/Graphics/SpriteFrame.h")]
	public class SpriteAtlas : Object
	{
		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x06003836 RID: 14390 RVA: 0x0006E758 File Offset: 0x0006C958
		public bool isVariant
		{
			[NativeMethod("IsVariant")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteAtlas>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SpriteAtlas.get_isVariant_Injected(intPtr);
			}
		}

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x06003837 RID: 14391 RVA: 0x0006E77C File Offset: 0x0006C97C
		public string tag
		{
			get
			{
				string stringAndDispose;
				try
				{
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteAtlas>(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					SpriteAtlas.get_tag_Injected(intPtr, out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
		}

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x06003838 RID: 14392 RVA: 0x0006E7BC File Offset: 0x0006C9BC
		public int spriteCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteAtlas>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SpriteAtlas.get_spriteCount_Injected(intPtr);
			}
		}

		// Token: 0x06003839 RID: 14393 RVA: 0x0006E7E0 File Offset: 0x0006C9E0
		public bool CanBindTo([NotNull] Sprite sprite)
		{
			if (sprite == null)
			{
				ThrowHelper.ThrowArgumentNullException(sprite, "sprite");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteAtlas>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(sprite);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(sprite, "sprite");
			}
			return SpriteAtlas.CanBindTo_Injected(intPtr, intPtr2);
		}

		// Token: 0x0600383A RID: 14394 RVA: 0x0006E828 File Offset: 0x0006CA28
		public unsafe Sprite GetSprite(string name)
		{
			Sprite sprite;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteAtlas>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				IntPtr sprite_Injected = SpriteAtlas.GetSprite_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				IntPtr sprite_Injected;
				sprite = Unmarshal.UnmarshalUnityObject<Sprite>(sprite_Injected);
				char* ptr = null;
			}
			return sprite;
		}

		// Token: 0x0600383B RID: 14395 RVA: 0x0006E898 File Offset: 0x0006CA98
		public int GetSprites(Sprite[] sprites)
		{
			return this.GetSpritesScripting(sprites);
		}

		// Token: 0x0600383C RID: 14396 RVA: 0x0006E8B4 File Offset: 0x0006CAB4
		public int GetSprites(Sprite[] sprites, string name)
		{
			return this.GetSpritesWithNameScripting(sprites, name);
		}

		// Token: 0x0600383D RID: 14397 RVA: 0x0006E8D0 File Offset: 0x0006CAD0
		private int GetSpritesScripting([Unmarshalled] Sprite[] sprites)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteAtlas>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return SpriteAtlas.GetSpritesScripting_Injected(intPtr, sprites);
		}

		// Token: 0x0600383E RID: 14398 RVA: 0x0006E8F4 File Offset: 0x0006CAF4
		private unsafe int GetSpritesWithNameScripting([Unmarshalled] Sprite[] sprites, string name)
		{
			int spritesWithNameScripting_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteAtlas>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				spritesWithNameScripting_Injected = SpriteAtlas.GetSpritesWithNameScripting_Injected(intPtr, sprites, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return spritesWithNameScripting_Injected;
		}

		// Token: 0x06003840 RID: 14400
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isVariant_Injected(IntPtr _unity_self);

		// Token: 0x06003841 RID: 14401
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_tag_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x06003842 RID: 14402
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_spriteCount_Injected(IntPtr _unity_self);

		// Token: 0x06003843 RID: 14403
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CanBindTo_Injected(IntPtr _unity_self, IntPtr sprite);

		// Token: 0x06003844 RID: 14404
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetSprite_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name);

		// Token: 0x06003845 RID: 14405
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetSpritesScripting_Injected(IntPtr _unity_self, Sprite[] sprites);

		// Token: 0x06003846 RID: 14406
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetSpritesWithNameScripting_Injected(IntPtr _unity_self, Sprite[] sprites, ref ManagedSpanWrapper name);
	}
}
