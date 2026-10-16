using System;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using UnityEngine;
using XRL;

// Token: 0x02000093 RID: 147
public class ModScrollerController : MonoBehaviour, IEnhancedScrollerDelegate
{
	// Token: 0x06000510 RID: 1296 RVA: 0x00021458 File Offset: 0x0001F658
	public void Refresh()
	{
		this._data.Clear();
		foreach (ModInfo modInfo in ModManager.Mods)
		{
			if (modInfo.Source == ModSource.Local)
			{
				this._data.Add(new ModScrollerData(modInfo));
			}
		}
		this.myScroller.ReloadData(0f);
	}

	// Token: 0x06000511 RID: 1297 RVA: 0x000214D8 File Offset: 0x0001F6D8
	private void Start()
	{
		this.myScroller.Delegate = this;
	}

	// Token: 0x06000512 RID: 1298 RVA: 0x000214E6 File Offset: 0x0001F6E6
	public int GetNumberOfCells(EnhancedScroller scroller)
	{
		return this._data.Count;
	}

	// Token: 0x06000513 RID: 1299 RVA: 0x000214F3 File Offset: 0x0001F6F3
	public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
	{
		return 20f;
	}

	// Token: 0x06000514 RID: 1300 RVA: 0x000214FA File Offset: 0x0001F6FA
	public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
	{
		ModCellView modCellView = scroller.GetCellView(this.animalCellViewPrefab) as ModCellView;
		modCellView.SetData(this._data[dataIndex]);
		return modCellView;
	}

	// Token: 0x040003E7 RID: 999
	private List<ModScrollerData> _data = new List<ModScrollerData>();

	// Token: 0x040003E8 RID: 1000
	public EnhancedScroller myScroller;

	// Token: 0x040003E9 RID: 1001
	public ModCellView animalCellViewPrefab;
}
