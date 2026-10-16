using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000044 RID: 68
	public static class TextEventManager
	{
		// Token: 0x060001ED RID: 493 RVA: 0x0000C5A8 File Offset: 0x0000A7A8
		public static void ON_PRE_RENDER_OBJECT_CHANGED()
		{
			TextEventManager.OnPreRenderObject_Event.Call();
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000C5B6 File Offset: 0x0000A7B6
		public static void ON_MATERIAL_PROPERTY_CHANGED(bool isChanged, Material mat)
		{
			TextEventManager.MATERIAL_PROPERTY_EVENT.Call(isChanged, mat);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000C5C6 File Offset: 0x0000A7C6
		public static void ON_FONT_PROPERTY_CHANGED(bool isChanged, Object font)
		{
			TextEventManager.FONT_PROPERTY_EVENT.Call(isChanged, font);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000C5D6 File Offset: 0x0000A7D6
		public static void ON_SPRITE_ASSET_PROPERTY_CHANGED(bool isChanged, Object obj)
		{
			TextEventManager.SPRITE_ASSET_PROPERTY_EVENT.Call(isChanged, obj);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000C5E6 File Offset: 0x0000A7E6
		public static void ON_TEXTMESHPRO_PROPERTY_CHANGED(bool isChanged, Object obj)
		{
			TextEventManager.TEXTMESHPRO_PROPERTY_EVENT.Call(isChanged, obj);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000C5F6 File Offset: 0x0000A7F6
		public static void ON_DRAG_AND_DROP_MATERIAL_CHANGED(GameObject sender, Material currentMaterial, Material newMaterial)
		{
			TextEventManager.DRAG_AND_DROP_MATERIAL_EVENT.Call(sender, currentMaterial, newMaterial);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000C607 File Offset: 0x0000A807
		public static void ON_TEXT_STYLE_PROPERTY_CHANGED(bool isChanged)
		{
			TextEventManager.TEXT_STYLE_PROPERTY_EVENT.Call(isChanged);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000C616 File Offset: 0x0000A816
		public static void ON_COLOR_GRADIENT_PROPERTY_CHANGED(Object gradient)
		{
			TextEventManager.COLOR_GRADIENT_PROPERTY_EVENT.Call(gradient);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000C625 File Offset: 0x0000A825
		public static void ON_TEXT_CHANGED(Object obj)
		{
			TextEventManager.TEXT_CHANGED_EVENT.Call(obj);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000C634 File Offset: 0x0000A834
		public static void ON_TMP_SETTINGS_CHANGED()
		{
			TextEventManager.TMP_SETTINGS_PROPERTY_EVENT.Call();
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000C642 File Offset: 0x0000A842
		public static void ON_RESOURCES_LOADED()
		{
			TextEventManager.RESOURCE_LOAD_EVENT.Call();
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000C650 File Offset: 0x0000A850
		public static void ON_TEXTMESHPRO_UGUI_PROPERTY_CHANGED(bool isChanged, Object obj)
		{
			TextEventManager.TEXTMESHPRO_UGUI_PROPERTY_EVENT.Call(isChanged, obj);
		}

		// Token: 0x040001C2 RID: 450
		public static readonly FastAction<bool, Material> MATERIAL_PROPERTY_EVENT = new FastAction<bool, Material>();

		// Token: 0x040001C3 RID: 451
		public static readonly FastAction<bool, Object> FONT_PROPERTY_EVENT = new FastAction<bool, Object>();

		// Token: 0x040001C4 RID: 452
		public static readonly FastAction<bool, Object> SPRITE_ASSET_PROPERTY_EVENT = new FastAction<bool, Object>();

		// Token: 0x040001C5 RID: 453
		public static readonly FastAction<bool, Object> TEXTMESHPRO_PROPERTY_EVENT = new FastAction<bool, Object>();

		// Token: 0x040001C6 RID: 454
		public static readonly FastAction<GameObject, Material, Material> DRAG_AND_DROP_MATERIAL_EVENT = new FastAction<GameObject, Material, Material>();

		// Token: 0x040001C7 RID: 455
		public static readonly FastAction<bool> TEXT_STYLE_PROPERTY_EVENT = new FastAction<bool>();

		// Token: 0x040001C8 RID: 456
		public static readonly FastAction<Object> COLOR_GRADIENT_PROPERTY_EVENT = new FastAction<Object>();

		// Token: 0x040001C9 RID: 457
		public static readonly FastAction TMP_SETTINGS_PROPERTY_EVENT = new FastAction();

		// Token: 0x040001CA RID: 458
		public static readonly FastAction RESOURCE_LOAD_EVENT = new FastAction();

		// Token: 0x040001CB RID: 459
		public static readonly FastAction<bool, Object> TEXTMESHPRO_UGUI_PROPERTY_EVENT = new FastAction<bool, Object>();

		// Token: 0x040001CC RID: 460
		public static readonly FastAction OnPreRenderObject_Event = new FastAction();

		// Token: 0x040001CD RID: 461
		public static readonly FastAction<Object> TEXT_CHANGED_EVENT = new FastAction<Object>();
	}
}
