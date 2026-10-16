using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000071 RID: 113
	public static class TMPro_EventManager
	{
		// Token: 0x0600059C RID: 1436 RVA: 0x0002DB7B File Offset: 0x0002BD7B
		public static void ON_MATERIAL_PROPERTY_CHANGED(bool isChanged, Material mat)
		{
			TMPro_EventManager.MATERIAL_PROPERTY_EVENT.Call(isChanged, mat);
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0002DB89 File Offset: 0x0002BD89
		public static void ON_FONT_PROPERTY_CHANGED(bool isChanged, global::UnityEngine.Object obj)
		{
			TMPro_EventManager.FONT_PROPERTY_EVENT.Call(isChanged, obj);
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0002DB97 File Offset: 0x0002BD97
		public static void ON_SPRITE_ASSET_PROPERTY_CHANGED(bool isChanged, global::UnityEngine.Object obj)
		{
			TMPro_EventManager.SPRITE_ASSET_PROPERTY_EVENT.Call(isChanged, obj);
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0002DBA5 File Offset: 0x0002BDA5
		public static void ON_TEXTMESHPRO_PROPERTY_CHANGED(bool isChanged, global::UnityEngine.Object obj)
		{
			TMPro_EventManager.TEXTMESHPRO_PROPERTY_EVENT.Call(isChanged, obj);
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0002DBB3 File Offset: 0x0002BDB3
		public static void ON_DRAG_AND_DROP_MATERIAL_CHANGED(GameObject sender, Material currentMaterial, Material newMaterial)
		{
			TMPro_EventManager.DRAG_AND_DROP_MATERIAL_EVENT.Call(sender, currentMaterial, newMaterial);
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0002DBC2 File Offset: 0x0002BDC2
		public static void ON_TEXT_STYLE_PROPERTY_CHANGED(bool isChanged)
		{
			TMPro_EventManager.TEXT_STYLE_PROPERTY_EVENT.Call(isChanged);
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0002DBCF File Offset: 0x0002BDCF
		public static void ON_COLOR_GRADIENT_PROPERTY_CHANGED(global::UnityEngine.Object obj)
		{
			TMPro_EventManager.COLOR_GRADIENT_PROPERTY_EVENT.Call(obj);
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0002DBDC File Offset: 0x0002BDDC
		public static void ON_TEXT_CHANGED(global::UnityEngine.Object obj)
		{
			TMPro_EventManager.TEXT_CHANGED_EVENT.Call(obj);
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0002DBE9 File Offset: 0x0002BDE9
		public static void ON_TMP_SETTINGS_CHANGED()
		{
			TMPro_EventManager.TMP_SETTINGS_PROPERTY_EVENT.Call();
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0002DBF5 File Offset: 0x0002BDF5
		public static void ON_RESOURCES_LOADED()
		{
			TMPro_EventManager.RESOURCE_LOAD_EVENT.Call();
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0002DC01 File Offset: 0x0002BE01
		public static void ON_TEXTMESHPRO_UGUI_PROPERTY_CHANGED(bool isChanged, global::UnityEngine.Object obj)
		{
			TMPro_EventManager.TEXTMESHPRO_UGUI_PROPERTY_EVENT.Call(isChanged, obj);
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0002DC0F File Offset: 0x0002BE0F
		public static void ON_COMPUTE_DT_EVENT(object Sender, Compute_DT_EventArgs e)
		{
			TMPro_EventManager.COMPUTE_DT_EVENT.Call(Sender, e);
		}

		// Token: 0x040004F5 RID: 1269
		public static readonly FastAction<object, Compute_DT_EventArgs> COMPUTE_DT_EVENT = new FastAction<object, Compute_DT_EventArgs>();

		// Token: 0x040004F6 RID: 1270
		public static readonly FastAction<bool, Material> MATERIAL_PROPERTY_EVENT = new FastAction<bool, Material>();

		// Token: 0x040004F7 RID: 1271
		public static readonly FastAction<bool, global::UnityEngine.Object> FONT_PROPERTY_EVENT = new FastAction<bool, global::UnityEngine.Object>();

		// Token: 0x040004F8 RID: 1272
		public static readonly FastAction<bool, global::UnityEngine.Object> SPRITE_ASSET_PROPERTY_EVENT = new FastAction<bool, global::UnityEngine.Object>();

		// Token: 0x040004F9 RID: 1273
		public static readonly FastAction<bool, global::UnityEngine.Object> TEXTMESHPRO_PROPERTY_EVENT = new FastAction<bool, global::UnityEngine.Object>();

		// Token: 0x040004FA RID: 1274
		public static readonly FastAction<GameObject, Material, Material> DRAG_AND_DROP_MATERIAL_EVENT = new FastAction<GameObject, Material, Material>();

		// Token: 0x040004FB RID: 1275
		public static readonly FastAction<bool> TEXT_STYLE_PROPERTY_EVENT = new FastAction<bool>();

		// Token: 0x040004FC RID: 1276
		public static readonly FastAction<global::UnityEngine.Object> COLOR_GRADIENT_PROPERTY_EVENT = new FastAction<global::UnityEngine.Object>();

		// Token: 0x040004FD RID: 1277
		public static readonly FastAction TMP_SETTINGS_PROPERTY_EVENT = new FastAction();

		// Token: 0x040004FE RID: 1278
		public static readonly FastAction RESOURCE_LOAD_EVENT = new FastAction();

		// Token: 0x040004FF RID: 1279
		public static readonly FastAction<bool, global::UnityEngine.Object> TEXTMESHPRO_UGUI_PROPERTY_EVENT = new FastAction<bool, global::UnityEngine.Object>();

		// Token: 0x04000500 RID: 1280
		public static readonly FastAction<global::UnityEngine.Object> TEXT_CHANGED_EVENT = new FastAction<global::UnityEngine.Object>();
	}
}
