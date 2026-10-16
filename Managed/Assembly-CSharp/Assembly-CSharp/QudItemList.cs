using System;
using System.Collections.Generic;
using QupKit;
using XRL.UI;
using XRL.World;

// Token: 0x0200006E RID: 110
public class QudItemList : ObjectPool<QudItemList>
{
	// Token: 0x060003BF RID: 959 RVA: 0x0001831C File Offset: 0x0001651C
	public void Categorize()
	{
		for (int i = 0; i < this.objects.Count; i++)
		{
			this.objects[i].category = this.objects[i].go.GetInventoryCategory(false);
		}
		if (this.categories == null)
		{
			this.categories = new Dictionary<string, List<QudItemListElement>>();
		}
		this.categories.Clear();
		if (this.categoryNames == null)
		{
			this.categoryNames = new List<string>();
		}
		this.categoryNames.Clear();
		for (int j = 0; j < this.objects.Count; j++)
		{
			if (!this.categoryNames.Contains(this.objects[j].category))
			{
				this.categoryNames.Add(this.objects[j].category);
			}
			if (!this.categories.ContainsKey(this.objects[j].category))
			{
				this.categories.Add(this.objects[j].category, new List<QudItemListElement>());
			}
			this.categories[this.objects[j].category].Add(this.objects[j]);
		}
		this.categoryNames.Sort();
		for (int k = 0; k < this.categoryNames.Count; k++)
		{
			this.categories[this.categoryNames[k]].Sort((QudItemListElement o1, QudItemListElement o2) => o1.displayName.CompareTo(o2.displayName));
		}
	}

	// Token: 0x060003C0 RID: 960 RVA: 0x000184C4 File Offset: 0x000166C4
	public void Clear()
	{
		if (this.categoryNames != null)
		{
			this.categoryNames.Clear();
		}
		if (this.categories != null)
		{
			this.categories.Clear();
		}
		foreach (QudItemListElement qudItemListElement in this.objects)
		{
			qudItemListElement.PoolReset();
			ObjectPool<QudItemListElement>.Return(qudItemListElement);
		}
		this.objects.Clear();
	}

	// Token: 0x060003C1 RID: 961 RVA: 0x0001854C File Offset: 0x0001674C
	public void Add(GameObject go)
	{
		QudItemListElement qudItemListElement = ObjectPool<QudItemListElement>.Checkout();
		qudItemListElement.InitFrom(go);
		this.objects.Add(qudItemListElement);
	}

	// Token: 0x060003C2 RID: 962 RVA: 0x00018574 File Offset: 0x00016774
	public void Add(List<GameObject> gos)
	{
		foreach (GameObject gameObject in gos)
		{
			this.Add(gameObject);
		}
	}

	// Token: 0x060003C3 RID: 963 RVA: 0x000185C4 File Offset: 0x000167C4
	public void Add(IEnumerable<GameObject> gos)
	{
		foreach (GameObject gameObject in gos)
		{
			this.Add(gameObject);
		}
	}

	// Token: 0x060003C4 RID: 964 RVA: 0x0001860C File Offset: 0x0001680C
	public void Add(List<TradeEntry> gos)
	{
		foreach (TradeEntry tradeEntry in gos)
		{
			if (tradeEntry.GO != null)
			{
				this.Add(tradeEntry.GO);
			}
		}
	}

	// Token: 0x040002DC RID: 732
	public List<QudItemListElement> objects = new List<QudItemListElement>();

	// Token: 0x040002DD RID: 733
	public Dictionary<string, List<QudItemListElement>> categories;

	// Token: 0x040002DE RID: 734
	public List<string> categoryNames;

	// Token: 0x040002DF RID: 735
	public int eqWeight;
}
