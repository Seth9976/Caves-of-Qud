using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using XRL.EditorFormats.Map;
using XRL.World;

namespace Overlay.MapEditor
{
	// Token: 0x020003DD RID: 989
	public class SelectedCellContentsView : MonoBehaviour
	{
		// Token: 0x060024E0 RID: 9440 RVA: 0x000B9958 File Offset: 0x000B7B58
		public void Set(MapFileRegion value, bool allowReplace = true)
		{
			for (int i = 0; i < this.Items.Count; i++)
			{
				global::UnityEngine.Object.Destroy(this.Items[i]);
			}
			this.Items.Clear();
			this.Contents.Clear();
			foreach (MapFileObjectReference mapFileObjectReference in value.AllObjects(null))
			{
				List<MapFileCell> list;
				if (!this.Contents.TryGetValue(mapFileObjectReference.blueprint, out list))
				{
					list = new List<MapFileCell>();
					this.Contents.Add(mapFileObjectReference.blueprint, list);
				}
				list.Add(mapFileObjectReference.cell);
			}
			foreach (KeyValuePair<MapFileObjectBlueprint, List<MapFileCell>> keyValuePair in this.Contents)
			{
				this.Items.Add(PooledPrefabManager.Instantiate(this.CellContentsPrefab));
				MapEditorSelectedObjectsRow component = this.Items[this.Items.Count - 1].GetComponent<MapEditorSelectedObjectsRow>();
				component.bp = new MapFileObjectBlueprint(keyValuePair.Key);
				component.BlueprintName.text = keyValuePair.Key.Name;
				if (GameObjectFactory.Factory.Blueprints.ContainsKey(keyValuePair.Key.Name))
				{
					if (!keyValuePair.Key.Properties.IsNullOrEmpty<KeyValuePair<string, string>>() || !keyValuePair.Key.IntProperties.IsNullOrEmpty<KeyValuePair<string, int>>())
					{
						component.BlueprintName.color = Color.cyan;
					}
					else
					{
						component.BlueprintName.color = Color.white;
					}
				}
				else
				{
					component.BlueprintName.color = Color.red;
				}
				component.Count.text = string.Format("x{0}", keyValuePair.Value.Count);
				this.SB.Clear();
				if (!keyValuePair.Key.Owner.IsNullOrEmpty())
				{
					this.SB.Append('[').Append(keyValuePair.Key.Owner).Append(']');
				}
				if (!keyValuePair.Key.Part.IsNullOrEmpty())
				{
					this.SB.Compound('[').Append(keyValuePair.Key.Part).Append(']');
				}
				component.Owner.text = this.SB.ToString();
				component.ReplaceButton.SetActive(allowReplace);
				component.transform.SetParent(this.ScrollParent.transform, false);
				this.Items[this.Items.Count - 1].SetActive(true);
			}
		}

		// Token: 0x04001271 RID: 4721
		public global::UnityEngine.GameObject CellContentsPrefab;

		// Token: 0x04001272 RID: 4722
		public global::UnityEngine.GameObject ScrollParent;

		// Token: 0x04001273 RID: 4723
		public List<global::UnityEngine.GameObject> Items = new List<global::UnityEngine.GameObject>();

		// Token: 0x04001274 RID: 4724
		private Dictionary<MapFileObjectBlueprint, List<MapFileCell>> Contents = new Dictionary<MapFileObjectBlueprint, List<MapFileCell>>();

		// Token: 0x04001275 RID: 4725
		private StringBuilder SB = new StringBuilder();
	}
}
