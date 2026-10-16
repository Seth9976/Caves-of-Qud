using System;
using Genkit;
using UnityEngine;
using UnityEngine.UI;
using XRL.Rules;
using XRL.World.ZoneBuilders;

// Token: 0x020000FA RID: 250
public class MapTesterSceneManager : MonoBehaviour
{
	// Token: 0x06000972 RID: 2418 RVA: 0x0003B4FD File Offset: 0x000396FD
	private void Start()
	{
	}

	// Token: 0x06000973 RID: 2419 RVA: 0x0003B4FF File Offset: 0x000396FF
	private void Update()
	{
	}

	// Token: 0x06000974 RID: 2420 RVA: 0x0003B504 File Offset: 0x00039704
	public void test1()
	{
		CatacombsMapTemplate catacombsMapTemplate = new CatacombsMapTemplate(5);
		foreach (InfluenceMapRegion influenceMapRegion in catacombsMapTemplate.regions.Regions)
		{
			Color4 color = new Color4((float)Stat.Random(1, 100) / 100f, (float)Stat.Random(1, 100) / 100f, (float)Stat.Random(1, 100) / 100f);
			foreach (Location2D location2D in influenceMapRegion.Cells)
			{
				catacombsMapTemplate.grid.set(location2D.X, location2D.Y, color);
			}
		}
		foreach (Rect2D rect2D in catacombsMapTemplate.maskingAreas)
		{
			catacombsMapTemplate.grid.box(rect2D, () => Color4.magenta);
		}
		this.image.sprite = catacombsMapTemplate.grid.toSprite();
	}

	// Token: 0x0400062D RID: 1581
	public Image image;
}
