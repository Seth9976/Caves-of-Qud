using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using XRL.World;

namespace Battlehub.UIControls
{
	// Token: 0x020003BD RID: 957
	public class BlueprintBrowserTreeView : MonoBehaviour
	{
		// Token: 0x06002379 RID: 9081 RVA: 0x000B4208 File Offset: 0x000B2408
		public static bool IsPrefab(Transform This)
		{
			if (Application.isEditor && !Application.isPlaying)
			{
				throw new InvalidOperationException("Does not work in edit mode");
			}
			return This.gameObject.scene.buildIndex < 0;
		}

		// Token: 0x0600237A RID: 9082 RVA: 0x000B4244 File Offset: 0x000B2444
		private void Awake()
		{
			if (!this.TreeView)
			{
				Debug.LogError("Set TreeView field");
				return;
			}
			IOrderedEnumerable<GameObjectBlueprint> orderedEnumerable = from blueprint in GameObjectFactory.Factory.BlueprintList
				where string.IsNullOrEmpty(blueprint.Inherits)
				orderby blueprint.Name
				select blueprint;
			this.TreeView.ItemDataBinding += this.OnItemDataBinding;
			this.TreeView.SelectionChanged += this.OnSelectionChanged;
			this.TreeView.ItemsRemoved += this.OnItemsRemoved;
			this.TreeView.ItemExpanding += this.OnItemExpanding;
			this.TreeView.ItemBeginDrag += this.OnItemBeginDrag;
			this.TreeView.ItemDrop += this.OnItemDrop;
			this.TreeView.ItemBeginDrop += this.OnItemBeginDrop;
			this.TreeView.ItemEndDrag += this.OnItemEndDrag;
			this.TreeView.Items = orderedEnumerable;
		}

		// Token: 0x0600237B RID: 9083 RVA: 0x000B4380 File Offset: 0x000B2580
		private void OnItemBeginDrop(object sender, ItemDropCancelArgs e)
		{
		}

		// Token: 0x0600237C RID: 9084 RVA: 0x000B4384 File Offset: 0x000B2584
		private void OnDestroy()
		{
			if (!this.TreeView)
			{
				return;
			}
			this.TreeView.ItemDataBinding -= this.OnItemDataBinding;
			this.TreeView.SelectionChanged -= this.OnSelectionChanged;
			this.TreeView.ItemsRemoved -= this.OnItemsRemoved;
			this.TreeView.ItemExpanding -= this.OnItemExpanding;
			this.TreeView.ItemBeginDrag -= this.OnItemBeginDrag;
			this.TreeView.ItemBeginDrop -= this.OnItemBeginDrop;
			this.TreeView.ItemDrop -= this.OnItemDrop;
			this.TreeView.ItemEndDrag -= this.OnItemEndDrag;
		}

		// Token: 0x0600237D RID: 9085 RVA: 0x000B4458 File Offset: 0x000B2658
		private void OnItemExpanding(object sender, ItemExpandingArgs e)
		{
			GameObjectBlueprint dataItem = (GameObjectBlueprint)e.Item;
			if (dataItem.hasChildren)
			{
				e.Children = from blueprint in GameObjectFactory.Factory.BlueprintList
					where blueprint.Inherits == dataItem.Name
					orderby blueprint.Name
					select blueprint;
			}
		}

		// Token: 0x0600237E RID: 9086 RVA: 0x000B44D0 File Offset: 0x000B26D0
		private void OnSelectionChanged(object sender, SelectionChangedArgs e)
		{
			IEnumerable<GameObjectBlueprint> enumerable = e.NewItems.OfType<GameObjectBlueprint>();
			if (enumerable.Count<GameObjectBlueprint>() > 0)
			{
				GameObjectBlueprint gameObjectBlueprint = enumerable.First<GameObjectBlueprint>();
				this.text.text = gameObjectBlueprint.BlueprintXML();
			}
		}

		// Token: 0x0600237F RID: 9087 RVA: 0x000B450C File Offset: 0x000B270C
		private void OnItemsRemoved(object sender, ItemsRemovedArgs e)
		{
			for (int i = 0; i < e.Items.Length; i++)
			{
			}
		}

		// Token: 0x06002380 RID: 9088 RVA: 0x000B452C File Offset: 0x000B272C
		private void OnItemDataBinding(object sender, TreeViewItemDataBindingArgs e)
		{
			GameObjectBlueprint gameObjectBlueprint = e.Item as GameObjectBlueprint;
			if (gameObjectBlueprint != null)
			{
				e.ItemPresenter.GetComponentInChildren<Text>(true).text = gameObjectBlueprint.Name;
				e.ItemPresenter.GetComponentsInChildren<Image>()[4].sprite = Resources.Load<Sprite>("cube");
				e.HasChildren = gameObjectBlueprint.hasChildren;
			}
		}

		// Token: 0x06002381 RID: 9089 RVA: 0x000B4587 File Offset: 0x000B2787
		private void OnItemBeginDrag(object sender, ItemArgs e)
		{
		}

		// Token: 0x06002382 RID: 9090 RVA: 0x000B4589 File Offset: 0x000B2789
		private void OnItemDrop(object sender, ItemDropArgs e)
		{
		}

		// Token: 0x06002383 RID: 9091 RVA: 0x000B458B File Offset: 0x000B278B
		private void OnItemEndDrag(object sender, ItemArgs e)
		{
		}

		// Token: 0x06002384 RID: 9092 RVA: 0x000B458D File Offset: 0x000B278D
		private void Update()
		{
		}

		// Token: 0x040011DF RID: 4575
		public TreeView TreeView;

		// Token: 0x040011E0 RID: 4576
		public InputField text;
	}
}
