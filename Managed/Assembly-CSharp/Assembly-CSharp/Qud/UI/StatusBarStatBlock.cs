using System;
using System.Collections.Generic;
using UnityEngine;
using XRL.UI;
using XRL.World;

namespace Qud.UI
{
	// Token: 0x020004A2 RID: 1186
	[ExecuteAlways]
	public class StatusBarStatBlock : MonoBehaviour
	{
		// Token: 0x06002D71 RID: 11633 RVA: 0x000F6C12 File Offset: 0x000F4E12
		public void Awake()
		{
			this.Refresh = true;
		}

		// Token: 0x06002D72 RID: 11634 RVA: 0x000F6C1C File Offset: 0x000F4E1C
		public void Update()
		{
			if (this.Refresh && Application.isPlaying)
			{
				this.Refresh = false;
				List<Transform> list = new List<Transform>(base.gameObject.transform.childCount);
				foreach (object obj in base.gameObject.transform)
				{
					Transform transform = (Transform)obj;
					list.Add(transform);
				}
				foreach (Transform transform2 in list)
				{
					transform2.gameObject.DestroyImmediate();
				}
				this._texts.Clear();
				for (int i = 0; i < this.Stats.Count; i++)
				{
					if (i > 0)
					{
						this.SpacerPrefab.Instantiate<global::UnityEngine.GameObject>().transform.SetParent(base.gameObject.transform, false);
					}
					global::UnityEngine.GameObject gameObject = this.TextPrefab.Instantiate<global::UnityEngine.GameObject>();
					gameObject.transform.SetParent(base.gameObject.transform, false);
					gameObject.GetComponent<UITextSkin>().SetText(this.Stats[i].Substring(0, 2).ToUpper() + ": 99");
					gameObject.GetComponent<UITextSkin>().color = this.Colors[i];
					this._texts.Add(gameObject);
				}
			}
		}

		// Token: 0x06002D73 RID: 11635 RVA: 0x000F6DB8 File Offset: 0x000F4FB8
		public void UpdateStats(Dictionary<string, string> stats)
		{
			int num = 0;
			while (num < this.Stats.Count && num < this._texts.Count)
			{
				string text;
				if (stats.TryGetValue(this.Stats[num], out text))
				{
					string statShortName = Statistic.GetStatShortName(this.Stats[num]);
					this._texts[num].GetComponent<UITextSkin>().SetText(string.Format("{0}: {1}", statShortName, text));
				}
				num++;
			}
		}

		// Token: 0x040019A7 RID: 6567
		public global::UnityEngine.GameObject TextPrefab;

		// Token: 0x040019A8 RID: 6568
		public global::UnityEngine.GameObject SpacerPrefab;

		// Token: 0x040019A9 RID: 6569
		public List<string> Stats;

		// Token: 0x040019AA RID: 6570
		public List<Color> Colors;

		// Token: 0x040019AB RID: 6571
		public bool Refresh;

		// Token: 0x040019AC RID: 6572
		private List<global::UnityEngine.GameObject> _texts = new List<global::UnityEngine.GameObject>();
	}
}
