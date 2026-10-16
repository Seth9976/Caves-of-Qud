using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000157 RID: 343
public static class AssetManager
{
	// Token: 0x06000DA3 RID: 3491 RVA: 0x000587DC File Offset: 0x000569DC
	public static GameObject NewBorder(string PrefabID)
	{
		if (AssetManager.Borders == null)
		{
			AssetManager.Borders = new Dictionary<string, GameObject>();
		}
		if (!AssetManager.Borders.ContainsKey(PrefabID))
		{
			AssetManager.Borders.Add(PrefabID, null);
		}
		if (AssetManager.Borders[PrefabID] == null)
		{
			AssetManager.Borders[PrefabID] = (GameObject)Resources.Load(PrefabID);
		}
		return global::UnityEngine.Object.Instantiate<GameObject>(AssetManager.Borders[PrefabID]);
	}

	// Token: 0x1700014D RID: 333
	// (get) Token: 0x06000DA4 RID: 3492 RVA: 0x0005884C File Offset: 0x00056A4C
	public static GameObject BuildingBackplateInstance
	{
		get
		{
			if (AssetManager._BuildingBackplatePrefab == null)
			{
				AssetManager._BuildingBackplatePrefab = (GameObject)Resources.Load("Prefabs/BuildingBackplate");
			}
			return global::UnityEngine.Object.Instantiate<GameObject>(AssetManager._BuildingBackplatePrefab);
		}
	}

	// Token: 0x1700014E RID: 334
	// (get) Token: 0x06000DA5 RID: 3493 RVA: 0x00058879 File Offset: 0x00056A79
	public static GameObject ResultsBackplatePrefab
	{
		get
		{
			if (AssetManager._ResultsBackplatePrefab == null)
			{
				AssetManager._ResultsBackplatePrefab = (GameObject)Resources.Load("Images/UI/Results/ResultTileBorderPrefab");
			}
			return global::UnityEngine.Object.Instantiate<GameObject>(AssetManager._ResultsBackplatePrefab);
		}
	}

	// Token: 0x1700014F RID: 335
	// (get) Token: 0x06000DA6 RID: 3494 RVA: 0x000588A6 File Offset: 0x00056AA6
	public static GameObject BrownButtonPrefab
	{
		get
		{
			if (AssetManager._BrownButtonPrefab == null)
			{
				AssetManager._BrownButtonPrefab = (GameObject)Resources.Load("Images/UI/BrownButtonPrefab");
			}
			return global::UnityEngine.Object.Instantiate<GameObject>(AssetManager._BrownButtonPrefab);
		}
	}

	// Token: 0x17000150 RID: 336
	// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x000588D3 File Offset: 0x00056AD3
	public static GameObject GenericButtonInstance
	{
		get
		{
			if (AssetManager._GenericButtonPrefab == null)
			{
				AssetManager._GenericButtonPrefab = (GameObject)Resources.Load("Prefabs/GenericButton");
			}
			return global::UnityEngine.Object.Instantiate<GameObject>(AssetManager._GenericButtonPrefab);
		}
	}

	// Token: 0x17000151 RID: 337
	// (get) Token: 0x06000DA8 RID: 3496 RVA: 0x00058900 File Offset: 0x00056B00
	public static GameObject TinyFontInstance
	{
		get
		{
			if (AssetManager._TinyFontPrefab == null)
			{
				AssetManager._TinyFontPrefab = (GameObject)Resources.Load("Fonts/FontPrefabPixel2");
			}
			return global::UnityEngine.Object.Instantiate<GameObject>(AssetManager._TinyFontPrefab);
		}
	}

	// Token: 0x17000152 RID: 338
	// (get) Token: 0x06000DA9 RID: 3497 RVA: 0x0005892D File Offset: 0x00056B2D
	public static GameObject SmallFontInstance
	{
		get
		{
			if (AssetManager._SmallFontPrefab == null)
			{
				AssetManager._SmallFontPrefab = (GameObject)Resources.Load("Fonts/FontPrefabPixel2");
			}
			return global::UnityEngine.Object.Instantiate<GameObject>(AssetManager._SmallFontPrefab);
		}
	}

	// Token: 0x17000153 RID: 339
	// (get) Token: 0x06000DAA RID: 3498 RVA: 0x0005895A File Offset: 0x00056B5A
	public static GameObject GenericDialogInstance
	{
		get
		{
			if (AssetManager._GenericDialogPrefab == null)
			{
				AssetManager._GenericDialogPrefab = (GameObject)Resources.Load("Prefabs/BuildingBackplate");
			}
			return global::UnityEngine.Object.Instantiate<GameObject>(AssetManager._GenericDialogPrefab);
		}
	}

	// Token: 0x17000154 RID: 340
	// (get) Token: 0x06000DAB RID: 3499 RVA: 0x00058987 File Offset: 0x00056B87
	public static GameObject MediumFontInstance
	{
		get
		{
			if (AssetManager._MediumFontPrefab == null)
			{
				AssetManager._MediumFontPrefab = (GameObject)Resources.Load("Fonts/FontPrefabPixel2");
			}
			return global::UnityEngine.Object.Instantiate<GameObject>(AssetManager._MediumFontPrefab);
		}
	}

	// Token: 0x0400096F RID: 2415
	private static Dictionary<string, GameObject> Borders;

	// Token: 0x04000970 RID: 2416
	private static GameObject _BuildingBackplatePrefab;

	// Token: 0x04000971 RID: 2417
	private static GameObject _ResultsBackplatePrefab;

	// Token: 0x04000972 RID: 2418
	private static GameObject _BrownButtonPrefab;

	// Token: 0x04000973 RID: 2419
	private static GameObject _GenericButtonPrefab;

	// Token: 0x04000974 RID: 2420
	private static GameObject _TinyFontPrefab;

	// Token: 0x04000975 RID: 2421
	private static GameObject _SmallFontPrefab;

	// Token: 0x04000976 RID: 2422
	private static GameObject _GenericDialogPrefab;

	// Token: 0x04000977 RID: 2423
	private static GameObject _MediumFontPrefab;
}
