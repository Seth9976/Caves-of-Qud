using System;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using UnityEngine;
using XRL.World;

// Token: 0x0200008A RID: 138
public class BlueprintScrollerController : MonoBehaviour, IEnhancedScrollerDelegate
{
	// Token: 0x1700008A RID: 138
	// (get) Token: 0x060004EA RID: 1258 RVA: 0x00020C4C File Offset: 0x0001EE4C
	public List<GameObjectBlueprint> Blueprints
	{
		get
		{
			if (this._bps != null)
			{
				return this._bps;
			}
			this._bps = new List<GameObjectBlueprint>(GameObjectFactory.Factory.BlueprintList);
			this._bps.Sort((GameObjectBlueprint a, GameObjectBlueprint b) => string.Compare(a.Name, b.Name));
			return this._bps;
		}
	}

	// Token: 0x060004EB RID: 1259 RVA: 0x00020CAD File Offset: 0x0001EEAD
	public void ResetCaches()
	{
		this._bps = null;
		this._data.Clear();
		this.scrollDataCache.Clear();
	}

	// Token: 0x060004EC RID: 1260 RVA: 0x00020CCC File Offset: 0x0001EECC
	public void UpdateDataFilter(string ObjectFilter, HashSet<string> SecondaryFilter)
	{
		this._data.Clear();
		string text = "";
		if (ObjectFilter != "")
		{
			text = ObjectFilter.ToUpper();
		}
		foreach (GameObjectBlueprint gameObjectBlueprint in this.Blueprints)
		{
			if ((ObjectFilter == "" || gameObjectBlueprint.Name.ToUpper().Contains(text)) && (SecondaryFilter == null || SecondaryFilter.Contains(gameObjectBlueprint.Name)))
			{
				BlueprintScrollerData blueprintScrollerData;
				if (!this.scrollDataCache.TryGetValue(gameObjectBlueprint.Name, out blueprintScrollerData))
				{
					blueprintScrollerData = new BlueprintScrollerData(gameObjectBlueprint.Name);
					this.scrollDataCache[gameObjectBlueprint.Name] = blueprintScrollerData;
				}
				this._data.Add(blueprintScrollerData);
			}
		}
		this.myScroller.ReloadData(0f);
	}

	// Token: 0x060004ED RID: 1261 RVA: 0x00020DC0 File Offset: 0x0001EFC0
	public void UpdateSelected()
	{
		this.myScroller.RefreshActiveCellViews();
	}

	// Token: 0x060004EE RID: 1262 RVA: 0x00020DCD File Offset: 0x0001EFCD
	private void Start()
	{
		this.myScroller.Delegate = this;
	}

	// Token: 0x060004EF RID: 1263 RVA: 0x00020DDB File Offset: 0x0001EFDB
	public int GetNumberOfCells(EnhancedScroller scroller)
	{
		return this._data.Count;
	}

	// Token: 0x060004F0 RID: 1264 RVA: 0x00020DE8 File Offset: 0x0001EFE8
	public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
	{
		return 24f;
	}

	// Token: 0x060004F1 RID: 1265 RVA: 0x00020DEF File Offset: 0x0001EFEF
	public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
	{
		BlueprintCellView blueprintCellView = scroller.GetCellView(this.animalCellViewPrefab) as BlueprintCellView;
		blueprintCellView.SetData(this._data[dataIndex]);
		return blueprintCellView;
	}

	// Token: 0x040003D1 RID: 977
	private List<BlueprintScrollerData> _data = new List<BlueprintScrollerData>();

	// Token: 0x040003D2 RID: 978
	public EnhancedScroller myScroller;

	// Token: 0x040003D3 RID: 979
	public BlueprintCellView animalCellViewPrefab;

	// Token: 0x040003D4 RID: 980
	public Dictionary<string, BlueprintScrollerData> scrollDataCache = new Dictionary<string, BlueprintScrollerData>();

	// Token: 0x040003D5 RID: 981
	private List<GameObjectBlueprint> _bps;
}
