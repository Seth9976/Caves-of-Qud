using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000689 RID: 1673
	internal class UIRVEShaderInfoAllocator
	{
		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x06003096 RID: 12438 RVA: 0x000B8508 File Offset: 0x000B6708
		private static int pageWidth
		{
			get
			{
				return 32;
			}
		}

		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x06003097 RID: 12439 RVA: 0x000B851C File Offset: 0x000B671C
		private static int pageHeight
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x06003098 RID: 12440 RVA: 0x000B8530 File Offset: 0x000B6730
		private static void AcquireDefaultShaderInfoTexture()
		{
			bool flag = ++UIRVEShaderInfoAllocator.s_DefaultShaderInfoTextureRefCount == 1;
			if (flag)
			{
				UIRVEShaderInfoAllocator.s_DefaultShaderInfoTexture = new Texture2D(64, 64, TextureFormat.RGBAFloat, false);
				UIRVEShaderInfoAllocator.s_DefaultShaderInfoTexture.name = "DefaultShaderInfoTexFloat";
				UIRVEShaderInfoAllocator.s_DefaultShaderInfoTexture.hideFlags = HideFlags.HideAndDontSave;
				UIRVEShaderInfoAllocator.s_DefaultShaderInfoTexture.filterMode = FilterMode.Point;
				UIRVEShaderInfoAllocator.s_DefaultShaderInfoTexture.SetPixel(UIRVEShaderInfoAllocator.identityTransformTexel.x, UIRVEShaderInfoAllocator.identityTransformTexel.y, UIRVEShaderInfoAllocator.identityTransformRow0Value);
				UIRVEShaderInfoAllocator.s_DefaultShaderInfoTexture.SetPixel(UIRVEShaderInfoAllocator.identityTransformTexel.x, UIRVEShaderInfoAllocator.identityTransformTexel.y + 1, UIRVEShaderInfoAllocator.identityTransformRow1Value);
				UIRVEShaderInfoAllocator.s_DefaultShaderInfoTexture.SetPixel(UIRVEShaderInfoAllocator.identityTransformTexel.x, UIRVEShaderInfoAllocator.identityTransformTexel.y + 2, UIRVEShaderInfoAllocator.identityTransformRow2Value);
				UIRVEShaderInfoAllocator.s_DefaultShaderInfoTexture.SetPixel(UIRVEShaderInfoAllocator.infiniteClipRectTexel.x, UIRVEShaderInfoAllocator.infiniteClipRectTexel.y, UIRVEShaderInfoAllocator.infiniteClipRectValue);
				UIRVEShaderInfoAllocator.s_DefaultShaderInfoTexture.SetPixel(UIRVEShaderInfoAllocator.fullOpacityTexel.x, UIRVEShaderInfoAllocator.fullOpacityTexel.y, UIRVEShaderInfoAllocator.fullOpacityValue);
				UIRVEShaderInfoAllocator.s_DefaultShaderInfoTexture.SetPixel(UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.x, UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.y, Color.white);
				UIRVEShaderInfoAllocator.s_DefaultShaderInfoTexture.SetPixel(UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.x, UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.y + 1, Color.clear);
				UIRVEShaderInfoAllocator.s_DefaultShaderInfoTexture.SetPixel(UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.x, UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.y + 2, Color.clear);
				UIRVEShaderInfoAllocator.s_DefaultShaderInfoTexture.SetPixel(UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.x, UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.y + 3, Color.clear);
				UIRVEShaderInfoAllocator.s_DefaultShaderInfoTexture.Apply(false, true);
			}
		}

		// Token: 0x06003099 RID: 12441 RVA: 0x000B873C File Offset: 0x000B693C
		private static void ReleaseDefaultShaderInfoTexture()
		{
			bool flag = --UIRVEShaderInfoAllocator.s_DefaultShaderInfoTextureRefCount == 0;
			if (flag)
			{
				UIRUtility.Destroy(UIRVEShaderInfoAllocator.s_DefaultShaderInfoTexture);
				UIRVEShaderInfoAllocator.s_DefaultShaderInfoTexture = null;
			}
		}

		// Token: 0x0600309A RID: 12442 RVA: 0x000B8774 File Offset: 0x000B6974
		private static Vector2Int AllocToTexelCoord(ref BitmapAllocator32 allocator, BMPAlloc alloc)
		{
			ushort num;
			ushort num2;
			allocator.GetAllocPageAtlasLocation(alloc.page, out num, out num2);
			return new Vector2Int((int)alloc.bitIndex * allocator.entryWidth + (int)num, (int)alloc.pageLine * allocator.entryHeight + (int)num2);
		}

		// Token: 0x0600309B RID: 12443 RVA: 0x000B87BC File Offset: 0x000B69BC
		private static bool AtlasRectMatchesPage(ref BitmapAllocator32 allocator, BMPAlloc defAlloc, RectInt atlasRect)
		{
			ushort num;
			ushort num2;
			allocator.GetAllocPageAtlasLocation(defAlloc.page, out num, out num2);
			return (int)num == atlasRect.xMin && (int)num2 == atlasRect.yMin && allocator.entryWidth * UIRVEShaderInfoAllocator.pageWidth == atlasRect.width && allocator.entryHeight * UIRVEShaderInfoAllocator.pageHeight == atlasRect.height;
		}

		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x0600309C RID: 12444 RVA: 0x000B8820 File Offset: 0x000B6A20
		public Texture atlas
		{
			get
			{
				bool storageReallyCreated = this.m_StorageReallyCreated;
				Texture texture;
				if (storageReallyCreated)
				{
					texture = this.m_Storage.texture;
				}
				else
				{
					texture = UIRVEShaderInfoAllocator.s_DefaultShaderInfoTexture;
				}
				return texture;
			}
		}

		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x0600309D RID: 12445 RVA: 0x000B8850 File Offset: 0x000B6A50
		public bool internalAtlasCreated
		{
			get
			{
				return this.m_StorageReallyCreated;
			}
		}

		// Token: 0x0600309E RID: 12446 RVA: 0x000B8868 File Offset: 0x000B6A68
		public UIRVEShaderInfoAllocator(ColorSpace colorSpace)
		{
			this.m_ColorSpace = colorSpace;
			this.m_OpacityAllocator = (this.m_ColorAllocator = (this.m_ClipRectAllocator = (this.m_TransformAllocator = (this.m_TextSettingsAllocator = default(BitmapAllocator32)))));
			this.m_TransformAllocator.Construct(UIRVEShaderInfoAllocator.pageHeight, 1, 3);
			this.m_TransformAllocator.ForceFirstAlloc((ushort)UIRVEShaderInfoAllocator.identityTransformTexel.x, (ushort)UIRVEShaderInfoAllocator.identityTransformTexel.y);
			this.m_ClipRectAllocator.Construct(UIRVEShaderInfoAllocator.pageHeight, 1, 1);
			this.m_ClipRectAllocator.ForceFirstAlloc((ushort)UIRVEShaderInfoAllocator.infiniteClipRectTexel.x, (ushort)UIRVEShaderInfoAllocator.infiniteClipRectTexel.y);
			this.m_OpacityAllocator.Construct(UIRVEShaderInfoAllocator.pageHeight, 1, 1);
			this.m_OpacityAllocator.ForceFirstAlloc((ushort)UIRVEShaderInfoAllocator.fullOpacityTexel.x, (ushort)UIRVEShaderInfoAllocator.fullOpacityTexel.y);
			this.m_ColorAllocator.Construct(UIRVEShaderInfoAllocator.pageHeight, 1, 1);
			this.m_ColorAllocator.ForceFirstAlloc((ushort)UIRVEShaderInfoAllocator.clearColorTexel.x, (ushort)UIRVEShaderInfoAllocator.clearColorTexel.y);
			this.m_TextSettingsAllocator.Construct(UIRVEShaderInfoAllocator.pageHeight, 1, 4);
			this.m_TextSettingsAllocator.ForceFirstAlloc((ushort)UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.x, (ushort)UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.y);
			UIRVEShaderInfoAllocator.AcquireDefaultShaderInfoTexture();
		}

		// Token: 0x0600309F RID: 12447 RVA: 0x000B89E4 File Offset: 0x000B6BE4
		private void ReallyCreateStorage()
		{
			this.m_Storage = new ShaderInfoStorageRGBAFloat(64, 4096);
			RectInt rectInt;
			this.m_Storage.AllocateRect(UIRVEShaderInfoAllocator.pageWidth * this.m_TransformAllocator.entryWidth, UIRVEShaderInfoAllocator.pageHeight * this.m_TransformAllocator.entryHeight, out rectInt);
			RectInt rectInt2;
			this.m_Storage.AllocateRect(UIRVEShaderInfoAllocator.pageWidth * this.m_ClipRectAllocator.entryWidth, UIRVEShaderInfoAllocator.pageHeight * this.m_ClipRectAllocator.entryHeight, out rectInt2);
			RectInt rectInt3;
			this.m_Storage.AllocateRect(UIRVEShaderInfoAllocator.pageWidth * this.m_OpacityAllocator.entryWidth, UIRVEShaderInfoAllocator.pageHeight * this.m_OpacityAllocator.entryHeight, out rectInt3);
			RectInt rectInt4;
			this.m_Storage.AllocateRect(UIRVEShaderInfoAllocator.pageWidth * this.m_ColorAllocator.entryWidth, UIRVEShaderInfoAllocator.pageHeight * this.m_ColorAllocator.entryHeight, out rectInt4);
			RectInt rectInt5;
			this.m_Storage.AllocateRect(UIRVEShaderInfoAllocator.pageWidth * this.m_TextSettingsAllocator.entryWidth, UIRVEShaderInfoAllocator.pageHeight * this.m_TextSettingsAllocator.entryHeight, out rectInt5);
			bool flag = !UIRVEShaderInfoAllocator.AtlasRectMatchesPage(ref this.m_TransformAllocator, UIRVEShaderInfoAllocator.identityTransform, rectInt);
			if (flag)
			{
				throw new Exception("Atlas identity transform allocation failed unexpectedly");
			}
			bool flag2 = !UIRVEShaderInfoAllocator.AtlasRectMatchesPage(ref this.m_ClipRectAllocator, UIRVEShaderInfoAllocator.infiniteClipRect, rectInt2);
			if (flag2)
			{
				throw new Exception("Atlas infinite clip rect allocation failed unexpectedly");
			}
			bool flag3 = !UIRVEShaderInfoAllocator.AtlasRectMatchesPage(ref this.m_OpacityAllocator, UIRVEShaderInfoAllocator.fullOpacity, rectInt3);
			if (flag3)
			{
				throw new Exception("Atlas full opacity allocation failed unexpectedly");
			}
			bool flag4 = !UIRVEShaderInfoAllocator.AtlasRectMatchesPage(ref this.m_ColorAllocator, UIRVEShaderInfoAllocator.clearColor, rectInt4);
			if (flag4)
			{
				throw new Exception("Atlas clear color allocation failed unexpectedly");
			}
			bool flag5 = !UIRVEShaderInfoAllocator.AtlasRectMatchesPage(ref this.m_TextSettingsAllocator, UIRVEShaderInfoAllocator.defaultTextCoreSettings, rectInt5);
			if (flag5)
			{
				throw new Exception("Atlas text setting allocation failed unexpectedly");
			}
			this.SetTransformValue(UIRVEShaderInfoAllocator.identityTransform, UIRVEShaderInfoAllocator.identityTransformValue);
			this.SetClipRectValue(UIRVEShaderInfoAllocator.infiniteClipRect, UIRVEShaderInfoAllocator.infiniteClipRectValue);
			this.SetOpacityValue(UIRVEShaderInfoAllocator.fullOpacity, UIRVEShaderInfoAllocator.fullOpacityValue.w);
			this.SetColorValue(UIRVEShaderInfoAllocator.clearColor, UIRVEShaderInfoAllocator.clearColorValue);
			this.SetTextCoreSettingValue(UIRVEShaderInfoAllocator.defaultTextCoreSettings, UIRVEShaderInfoAllocator.defaultTextCoreSettingsValue);
			this.m_StorageReallyCreated = true;
		}

		// Token: 0x060030A0 RID: 12448 RVA: 0x000B8C14 File Offset: 0x000B6E14
		public void Dispose()
		{
			bool flag = this.m_Storage != null;
			if (flag)
			{
				this.m_Storage.Dispose();
			}
			this.m_Storage = null;
			this.m_StorageReallyCreated = false;
			UIRVEShaderInfoAllocator.ReleaseDefaultShaderInfoTexture();
		}

		// Token: 0x060030A1 RID: 12449 RVA: 0x000B8C4F File Offset: 0x000B6E4F
		public void IssuePendingStorageChanges()
		{
			BaseShaderInfoStorage storage = this.m_Storage;
			if (storage != null)
			{
				storage.UpdateTexture();
			}
		}

		// Token: 0x060030A2 RID: 12450 RVA: 0x000B8C64 File Offset: 0x000B6E64
		public BMPAlloc AllocTransform()
		{
			bool flag = !this.m_StorageReallyCreated;
			if (flag)
			{
				this.ReallyCreateStorage();
			}
			return this.m_TransformAllocator.Allocate(this.m_Storage);
		}

		// Token: 0x060030A3 RID: 12451 RVA: 0x000B8C9C File Offset: 0x000B6E9C
		public BMPAlloc AllocClipRect()
		{
			bool flag = !this.m_StorageReallyCreated;
			if (flag)
			{
				this.ReallyCreateStorage();
			}
			return this.m_ClipRectAllocator.Allocate(this.m_Storage);
		}

		// Token: 0x060030A4 RID: 12452 RVA: 0x000B8CD4 File Offset: 0x000B6ED4
		public BMPAlloc AllocOpacity()
		{
			bool flag = !this.m_StorageReallyCreated;
			if (flag)
			{
				this.ReallyCreateStorage();
			}
			return this.m_OpacityAllocator.Allocate(this.m_Storage);
		}

		// Token: 0x060030A5 RID: 12453 RVA: 0x000B8D0C File Offset: 0x000B6F0C
		public BMPAlloc AllocColor()
		{
			bool flag = !this.m_StorageReallyCreated;
			if (flag)
			{
				this.ReallyCreateStorage();
			}
			return this.m_ColorAllocator.Allocate(this.m_Storage);
		}

		// Token: 0x060030A6 RID: 12454 RVA: 0x000B8D44 File Offset: 0x000B6F44
		public BMPAlloc AllocTextCoreSettings(TextCoreSettings settings)
		{
			bool flag = !this.m_StorageReallyCreated;
			if (flag)
			{
				this.ReallyCreateStorage();
			}
			return this.m_TextSettingsAllocator.Allocate(this.m_Storage);
		}

		// Token: 0x060030A7 RID: 12455 RVA: 0x000B8D7C File Offset: 0x000B6F7C
		public void SetTransformValue(BMPAlloc alloc, Matrix4x4 xform)
		{
			Debug.Assert(alloc.IsValid());
			Vector2Int vector2Int = UIRVEShaderInfoAllocator.AllocToTexelCoord(ref this.m_TransformAllocator, alloc);
			this.m_Storage.SetTexel(vector2Int.x, vector2Int.y, xform.GetRow(0));
			this.m_Storage.SetTexel(vector2Int.x, vector2Int.y + 1, xform.GetRow(1));
			this.m_Storage.SetTexel(vector2Int.x, vector2Int.y + 2, xform.GetRow(2));
		}

		// Token: 0x060030A8 RID: 12456 RVA: 0x000B8E20 File Offset: 0x000B7020
		public void SetClipRectValue(BMPAlloc alloc, Vector4 clipRect)
		{
			Debug.Assert(alloc.IsValid());
			Vector2Int vector2Int = UIRVEShaderInfoAllocator.AllocToTexelCoord(ref this.m_ClipRectAllocator, alloc);
			this.m_Storage.SetTexel(vector2Int.x, vector2Int.y, clipRect);
		}

		// Token: 0x060030A9 RID: 12457 RVA: 0x000B8E68 File Offset: 0x000B7068
		public void SetOpacityValue(BMPAlloc alloc, float opacity)
		{
			Debug.Assert(alloc.IsValid());
			Vector2Int vector2Int = UIRVEShaderInfoAllocator.AllocToTexelCoord(ref this.m_OpacityAllocator, alloc);
			this.m_Storage.SetTexel(vector2Int.x, vector2Int.y, new Color(1f, 1f, 1f, opacity));
		}

		// Token: 0x060030AA RID: 12458 RVA: 0x000B8EC0 File Offset: 0x000B70C0
		public void SetColorValue(BMPAlloc alloc, Color color)
		{
			Debug.Assert(alloc.IsValid());
			Vector2Int vector2Int = UIRVEShaderInfoAllocator.AllocToTexelCoord(ref this.m_ColorAllocator, alloc);
			bool flag = this.m_ColorSpace == ColorSpace.Linear;
			if (flag)
			{
				this.m_Storage.SetTexel(vector2Int.x, vector2Int.y, color.linear);
			}
			else
			{
				this.m_Storage.SetTexel(vector2Int.x, vector2Int.y, color);
			}
		}

		// Token: 0x060030AB RID: 12459 RVA: 0x000B8F34 File Offset: 0x000B7134
		public void SetTextCoreSettingValue(BMPAlloc alloc, TextCoreSettings settings)
		{
			Debug.Assert(alloc.IsValid());
			Vector2Int vector2Int = UIRVEShaderInfoAllocator.AllocToTexelCoord(ref this.m_TextSettingsAllocator, alloc);
			Color color = new Color(-settings.underlayOffset.x, settings.underlayOffset.y, settings.underlaySoftness, settings.outlineWidth);
			bool flag = this.m_ColorSpace == ColorSpace.Linear;
			if (flag)
			{
				this.m_Storage.SetTexel(vector2Int.x, vector2Int.y, settings.faceColor.linear);
				this.m_Storage.SetTexel(vector2Int.x, vector2Int.y + 1, settings.outlineColor.linear);
				this.m_Storage.SetTexel(vector2Int.x, vector2Int.y + 2, settings.underlayColor.linear);
			}
			else
			{
				this.m_Storage.SetTexel(vector2Int.x, vector2Int.y, settings.faceColor);
				this.m_Storage.SetTexel(vector2Int.x, vector2Int.y + 1, settings.outlineColor);
				this.m_Storage.SetTexel(vector2Int.x, vector2Int.y + 2, settings.underlayColor);
			}
			this.m_Storage.SetTexel(vector2Int.x, vector2Int.y + 3, color);
		}

		// Token: 0x060030AC RID: 12460 RVA: 0x000B9090 File Offset: 0x000B7290
		public void FreeTransform(BMPAlloc alloc)
		{
			Debug.Assert(alloc.IsValid());
			this.m_TransformAllocator.Free(alloc);
		}

		// Token: 0x060030AD RID: 12461 RVA: 0x000B90AD File Offset: 0x000B72AD
		public void FreeClipRect(BMPAlloc alloc)
		{
			Debug.Assert(alloc.IsValid());
			this.m_ClipRectAllocator.Free(alloc);
		}

		// Token: 0x060030AE RID: 12462 RVA: 0x000B90CA File Offset: 0x000B72CA
		public void FreeOpacity(BMPAlloc alloc)
		{
			Debug.Assert(alloc.IsValid());
			this.m_OpacityAllocator.Free(alloc);
		}

		// Token: 0x060030AF RID: 12463 RVA: 0x000B90E7 File Offset: 0x000B72E7
		public void FreeColor(BMPAlloc alloc)
		{
			Debug.Assert(alloc.IsValid());
			this.m_ColorAllocator.Free(alloc);
		}

		// Token: 0x060030B0 RID: 12464 RVA: 0x000B9104 File Offset: 0x000B7304
		public void FreeTextCoreSettings(BMPAlloc alloc)
		{
			Debug.Assert(alloc.IsValid());
			this.m_TextSettingsAllocator.Free(alloc);
		}

		// Token: 0x060030B1 RID: 12465 RVA: 0x000B9124 File Offset: 0x000B7324
		public Color32 TransformAllocToVertexData(BMPAlloc alloc)
		{
			Debug.Assert(UIRVEShaderInfoAllocator.pageWidth == 32 && UIRVEShaderInfoAllocator.pageHeight == 8);
			ushort num = 0;
			ushort num2 = 0;
			this.m_TransformAllocator.GetAllocPageAtlasLocation(alloc.page, out num, out num2);
			return new Color32((byte)(num >> 5), (byte)(num2 >> 3), (byte)((int)alloc.pageLine * UIRVEShaderInfoAllocator.pageWidth + (int)alloc.bitIndex), 0);
		}

		// Token: 0x060030B2 RID: 12466 RVA: 0x000B918C File Offset: 0x000B738C
		public Color32 ClipRectAllocToVertexData(BMPAlloc alloc)
		{
			Debug.Assert(UIRVEShaderInfoAllocator.pageWidth == 32 && UIRVEShaderInfoAllocator.pageHeight == 8);
			ushort num = 0;
			ushort num2 = 0;
			this.m_ClipRectAllocator.GetAllocPageAtlasLocation(alloc.page, out num, out num2);
			return new Color32((byte)(num >> 5), (byte)(num2 >> 3), (byte)((int)alloc.pageLine * UIRVEShaderInfoAllocator.pageWidth + (int)alloc.bitIndex), 0);
		}

		// Token: 0x060030B3 RID: 12467 RVA: 0x000B91F4 File Offset: 0x000B73F4
		public Color32 OpacityAllocToVertexData(BMPAlloc alloc)
		{
			Debug.Assert(UIRVEShaderInfoAllocator.pageWidth == 32 && UIRVEShaderInfoAllocator.pageHeight == 8);
			ushort num;
			ushort num2;
			this.m_OpacityAllocator.GetAllocPageAtlasLocation(alloc.page, out num, out num2);
			return new Color32((byte)(num >> 5), (byte)(num2 >> 3), (byte)((int)alloc.pageLine * UIRVEShaderInfoAllocator.pageWidth + (int)alloc.bitIndex), 0);
		}

		// Token: 0x060030B4 RID: 12468 RVA: 0x000B9258 File Offset: 0x000B7458
		public Color32 ColorAllocToVertexData(BMPAlloc alloc)
		{
			Debug.Assert(UIRVEShaderInfoAllocator.pageWidth == 32 && UIRVEShaderInfoAllocator.pageHeight == 8);
			ushort num;
			ushort num2;
			this.m_ColorAllocator.GetAllocPageAtlasLocation(alloc.page, out num, out num2);
			return new Color32((byte)(num >> 5), (byte)(num2 >> 3), (byte)((int)alloc.pageLine * UIRVEShaderInfoAllocator.pageWidth + (int)alloc.bitIndex), 0);
		}

		// Token: 0x060030B5 RID: 12469 RVA: 0x000B92BC File Offset: 0x000B74BC
		public Color32 TextCoreSettingsToVertexData(BMPAlloc alloc)
		{
			Debug.Assert(UIRVEShaderInfoAllocator.pageWidth == 32 && UIRVEShaderInfoAllocator.pageHeight == 8);
			ushort num;
			ushort num2;
			this.m_TextSettingsAllocator.GetAllocPageAtlasLocation(alloc.page, out num, out num2);
			return new Color32((byte)(num >> 5), (byte)(num2 >> 3), (byte)((int)alloc.pageLine * UIRVEShaderInfoAllocator.pageWidth + (int)alloc.bitIndex), 0);
		}

		// Token: 0x040017F2 RID: 6130
		private BaseShaderInfoStorage m_Storage;

		// Token: 0x040017F3 RID: 6131
		private BitmapAllocator32 m_TransformAllocator;

		// Token: 0x040017F4 RID: 6132
		private BitmapAllocator32 m_ClipRectAllocator;

		// Token: 0x040017F5 RID: 6133
		private BitmapAllocator32 m_OpacityAllocator;

		// Token: 0x040017F6 RID: 6134
		private BitmapAllocator32 m_ColorAllocator;

		// Token: 0x040017F7 RID: 6135
		private BitmapAllocator32 m_TextSettingsAllocator;

		// Token: 0x040017F8 RID: 6136
		private bool m_StorageReallyCreated;

		// Token: 0x040017F9 RID: 6137
		private ColorSpace m_ColorSpace;

		// Token: 0x040017FA RID: 6138
		private static readonly Vector2Int identityTransformTexel = new Vector2Int(0, 0);

		// Token: 0x040017FB RID: 6139
		private static readonly Vector2Int infiniteClipRectTexel = new Vector2Int(0, 32);

		// Token: 0x040017FC RID: 6140
		private static readonly Vector2Int fullOpacityTexel = new Vector2Int(32, 32);

		// Token: 0x040017FD RID: 6141
		private static readonly Vector2Int clearColorTexel = new Vector2Int(0, 40);

		// Token: 0x040017FE RID: 6142
		private static readonly Vector2Int defaultTextCoreSettingsTexel = new Vector2Int(32, 0);

		// Token: 0x040017FF RID: 6143
		private static readonly Matrix4x4 identityTransformValue = Matrix4x4.identity;

		// Token: 0x04001800 RID: 6144
		private static readonly Vector4 identityTransformRow0Value = UIRVEShaderInfoAllocator.identityTransformValue.GetRow(0);

		// Token: 0x04001801 RID: 6145
		private static readonly Vector4 identityTransformRow1Value = UIRVEShaderInfoAllocator.identityTransformValue.GetRow(1);

		// Token: 0x04001802 RID: 6146
		private static readonly Vector4 identityTransformRow2Value = UIRVEShaderInfoAllocator.identityTransformValue.GetRow(2);

		// Token: 0x04001803 RID: 6147
		private static readonly Vector4 infiniteClipRectValue = new Vector4(0f, 0f, 0f, 0f);

		// Token: 0x04001804 RID: 6148
		private static readonly Vector4 fullOpacityValue = new Vector4(1f, 1f, 1f, 1f);

		// Token: 0x04001805 RID: 6149
		private static readonly Vector4 clearColorValue = new Vector4(0f, 0f, 0f, 0f);

		// Token: 0x04001806 RID: 6150
		private static readonly TextCoreSettings defaultTextCoreSettingsValue = new TextCoreSettings
		{
			faceColor = Color.white,
			outlineColor = Color.clear,
			outlineWidth = 0f,
			underlayColor = Color.clear,
			underlayOffset = Vector2.zero,
			underlaySoftness = 0f
		};

		// Token: 0x04001807 RID: 6151
		public static readonly BMPAlloc identityTransform;

		// Token: 0x04001808 RID: 6152
		public static readonly BMPAlloc infiniteClipRect;

		// Token: 0x04001809 RID: 6153
		public static readonly BMPAlloc fullOpacity;

		// Token: 0x0400180A RID: 6154
		public static readonly BMPAlloc clearColor;

		// Token: 0x0400180B RID: 6155
		public static readonly BMPAlloc defaultTextCoreSettings;

		// Token: 0x0400180C RID: 6156
		private static int s_DefaultShaderInfoTextureRefCount;

		// Token: 0x0400180D RID: 6157
		private static Texture2D s_DefaultShaderInfoTexture;
	}
}
