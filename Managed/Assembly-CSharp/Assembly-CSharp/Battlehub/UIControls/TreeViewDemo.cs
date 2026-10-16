using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Battlehub.UIControls
{
	// Token: 0x020003C0 RID: 960
	public class TreeViewDemo : MonoBehaviour
	{
		// Token: 0x0600238E RID: 9102 RVA: 0x000B4798 File Offset: 0x000B2998
		public static bool IsPrefab(Transform This)
		{
			if (Application.isEditor && !Application.isPlaying)
			{
				throw new InvalidOperationException("Does not work in edit mode");
			}
			return This.gameObject.scene.buildIndex < 0;
		}

		// Token: 0x0600238F RID: 9103 RVA: 0x000B47D4 File Offset: 0x000B29D4
		private void Start()
		{
			if (!this.TreeView)
			{
				Debug.LogError("Set TreeView field");
				return;
			}
			IEnumerable<GameObject> enumerable = from go in Resources.FindObjectsOfTypeAll<GameObject>()
				where !TreeViewDemo.IsPrefab(go.transform) && go.transform.parent == null
				select go into t
				orderby t.transform.GetSiblingIndex()
				select t;
			this.TreeView.ItemDataBinding += this.OnItemDataBinding;
			this.TreeView.SelectionChanged += this.OnSelectionChanged;
			this.TreeView.ItemsRemoved += this.OnItemsRemoved;
			this.TreeView.ItemExpanding += this.OnItemExpanding;
			this.TreeView.ItemBeginDrag += this.OnItemBeginDrag;
			this.TreeView.ItemDrop += this.OnItemDrop;
			this.TreeView.ItemBeginDrop += this.OnItemBeginDrop;
			this.TreeView.ItemEndDrag += this.OnItemEndDrag;
			this.TreeView.Items = enumerable;
		}

		// Token: 0x06002390 RID: 9104 RVA: 0x000B490B File Offset: 0x000B2B0B
		private void OnItemBeginDrop(object sender, ItemDropCancelArgs e)
		{
		}

		// Token: 0x06002391 RID: 9105 RVA: 0x000B4910 File Offset: 0x000B2B10
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

		// Token: 0x06002392 RID: 9106 RVA: 0x000B49E4 File Offset: 0x000B2BE4
		private void OnItemExpanding(object sender, ItemExpandingArgs e)
		{
			GameObject gameObject = (GameObject)e.Item;
			if (gameObject.transform.childCount > 0)
			{
				GameObject[] array = new GameObject[gameObject.transform.childCount];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = gameObject.transform.GetChild(i).gameObject;
				}
				e.Children = array;
			}
		}

		// Token: 0x06002393 RID: 9107 RVA: 0x000B4A45 File Offset: 0x000B2C45
		private void OnSelectionChanged(object sender, SelectionChangedArgs e)
		{
		}

		// Token: 0x06002394 RID: 9108 RVA: 0x000B4A48 File Offset: 0x000B2C48
		private void OnItemsRemoved(object sender, ItemsRemovedArgs e)
		{
			for (int i = 0; i < e.Items.Length; i++)
			{
				GameObject gameObject = (GameObject)e.Items[i];
				if (gameObject != null)
				{
					global::UnityEngine.Object.Destroy(gameObject);
				}
			}
		}

		// Token: 0x06002395 RID: 9109 RVA: 0x000B4A88 File Offset: 0x000B2C88
		private void OnItemDataBinding(object sender, TreeViewItemDataBindingArgs e)
		{
			GameObject gameObject = e.Item as GameObject;
			if (gameObject != null)
			{
				e.ItemPresenter.GetComponentInChildren<Text>(true).text = gameObject.name;
				e.ItemPresenter.GetComponentsInChildren<Image>()[4].sprite = Resources.Load<Sprite>("cube");
				if (gameObject.name != "TreeView")
				{
					e.HasChildren = gameObject.transform.childCount > 0;
				}
			}
		}

		// Token: 0x06002396 RID: 9110 RVA: 0x000B4B03 File Offset: 0x000B2D03
		private void OnItemBeginDrag(object sender, ItemArgs e)
		{
		}

		// Token: 0x06002397 RID: 9111 RVA: 0x000B4B08 File Offset: 0x000B2D08
		private void OnItemDrop(object sender, ItemDropArgs e)
		{
			if (e.DropTarget == null)
			{
				return;
			}
			Transform transform = ((GameObject)e.DropTarget).transform;
			if (e.Action == ItemDropAction.SetLastChild)
			{
				for (int i = 0; i < e.DragItems.Length; i++)
				{
					Transform transform2 = ((GameObject)e.DragItems[i]).transform;
					transform2.SetParent(transform, true);
					transform2.SetAsLastSibling();
				}
				return;
			}
			if (e.Action == ItemDropAction.SetNextSibling)
			{
				for (int j = e.DragItems.Length - 1; j >= 0; j--)
				{
					Transform transform3 = ((GameObject)e.DragItems[j]).transform;
					int siblingIndex = transform.GetSiblingIndex();
					if (transform3.parent != transform.parent)
					{
						transform3.SetParent(transform.parent, true);
						transform3.SetSiblingIndex(siblingIndex + 1);
					}
					else
					{
						int siblingIndex2 = transform3.GetSiblingIndex();
						if (siblingIndex < siblingIndex2)
						{
							transform3.SetSiblingIndex(siblingIndex + 1);
						}
						else
						{
							transform3.SetSiblingIndex(siblingIndex);
						}
					}
				}
				return;
			}
			if (e.Action == ItemDropAction.SetPrevSibling)
			{
				for (int k = 0; k < e.DragItems.Length; k++)
				{
					Transform transform4 = ((GameObject)e.DragItems[k]).transform;
					if (transform4.parent != transform.parent)
					{
						transform4.SetParent(transform.parent, true);
					}
					int siblingIndex3 = transform.GetSiblingIndex();
					int siblingIndex4 = transform4.GetSiblingIndex();
					if (siblingIndex3 > siblingIndex4)
					{
						transform4.SetSiblingIndex(siblingIndex3 - 1);
					}
					else
					{
						transform4.SetSiblingIndex(siblingIndex3);
					}
				}
			}
		}

		// Token: 0x06002398 RID: 9112 RVA: 0x000B4C77 File Offset: 0x000B2E77
		private void OnItemEndDrag(object sender, ItemArgs e)
		{
		}

		// Token: 0x06002399 RID: 9113 RVA: 0x000B4C7C File Offset: 0x000B2E7C
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.J))
			{
				this.TreeView.SelectedItems = this.TreeView.Items.OfType<object>().Take<object>(5).ToArray<object>();
				return;
			}
			if (Input.GetKeyDown(KeyCode.K))
			{
				this.TreeView.SelectedItem = null;
			}
		}

		// Token: 0x040011E4 RID: 4580
		public TreeView TreeView;
	}
}
