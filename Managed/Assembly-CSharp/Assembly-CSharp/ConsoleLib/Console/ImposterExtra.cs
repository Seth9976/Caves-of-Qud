using System;
using System.Collections.Generic;
using UnityEngine;
using XRL.UI;

namespace ConsoleLib.Console
{
	// Token: 0x02001531 RID: 5425
	public class ImposterExtra : IConsoleCharExtra
	{
		// Token: 0x0600D35D RID: 54109 RVA: 0x004E7562 File Offset: 0x004E5762
		public void Add(string prefab, int layer = 0, string config = null)
		{
			this.imposters.Add(new ImposterExtra.ImposterInfo(prefab, layer, config));
		}

		// Token: 0x0600D35E RID: 54110 RVA: 0x004E7577 File Offset: 0x004E5777
		public void Add(string prefab, Vector2 offset, int layer = 0, string config = null)
		{
			this.imposters.Add(new ImposterExtra.ImposterInfo(prefab, offset, layer, config));
		}

		// Token: 0x0600D35F RID: 54111 RVA: 0x004E758E File Offset: 0x004E578E
		public void Add(string prefab, Vector3 offset, int layer = 0, string config = null)
		{
			this.imposters.Add(new ImposterExtra.ImposterInfo(prefab, offset, layer, config));
		}

		// Token: 0x0600D360 RID: 54112 RVA: 0x004E75A5 File Offset: 0x004E57A5
		public void Add(ImposterExtra.ImposterInfo newImposter)
		{
			this.imposters.Add(newImposter);
		}

		// Token: 0x0600D361 RID: 54113 RVA: 0x004E75B3 File Offset: 0x004E57B3
		public void CopyFrom(ImposterExtra extra)
		{
			if (this.imposters.Count > 0)
			{
				this.imposters.Clear();
			}
			if (extra != null && extra.imposters.Count > 0)
			{
				this.imposters.AddRange(extra.imposters);
			}
		}

		// Token: 0x0600D362 RID: 54114 RVA: 0x004E75F0 File Offset: 0x004E57F0
		public override IConsoleCharExtra Copy()
		{
			ImposterExtra imposterExtra = new ImposterExtra();
			imposterExtra.imposters.AddRange(this.imposters);
			return imposterExtra;
		}

		// Token: 0x0600D363 RID: 54115 RVA: 0x004E7608 File Offset: 0x004E5808
		public override void Clear(bool overtyping)
		{
			this.imposters.Clear();
		}

		// Token: 0x0600D364 RID: 54116 RVA: 0x004E7618 File Offset: 0x004E5818
		public override void AfterRender(int x, int y, ConsoleChar ch, ex3DSprite2 sprite, ScreenBuffer buffer)
		{
			List<GameObject> list = ImposterExtra.renderedImposters[x + y * 80];
			if (list != null && list.Count > 0)
			{
				for (int i = 0; i < list.Count; i++)
				{
					string text = "";
					if (list[i] != null && this.imposters != null && this.imposters.Count > i)
					{
						ImposterExtra.objectNameCache.TryGetValue(list[i], out text);
					}
					if (list[i] != null && (this.imposters == null || this.imposters.Count <= i || this.imposters[i].prefab != text))
					{
						ImposterExtra.free(list[i]);
						list[i] = null;
					}
				}
			}
			if (this.imposters == null || this.imposters.Count == 0)
			{
				if (list != null)
				{
					list.Clear();
				}
				return;
			}
			if (Options.DisableImposters)
			{
				return;
			}
			if (list == null)
			{
				list = new List<GameObject>();
				ImposterExtra.renderedImposters[x + y * 80] = list;
			}
			while (list.Count < this.imposters.Count)
			{
				list.Add(null);
			}
			for (int j = 0; j < this.imposters.Count; j++)
			{
				if (list[j] == null && this.imposters[j].prefab != null)
				{
					list[j] = ImposterExtra.next(this.imposters[j].prefab);
					list[j].transform.position = sprite.transform.position;
					list[j].transform.position += new Vector3(0f, 0f, -10f) + this.imposters[j].offset;
					if (this.imposters[j].config != null)
					{
						IConfigurableExtra[] components = list[j].GetComponents<IConfigurableExtra>();
						if (components != null)
						{
							IConfigurableExtra[] array = components;
							for (int k = 0; k < array.Length; k++)
							{
								array[k].Configure(this.imposters[j].config);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600D365 RID: 54117 RVA: 0x004E7860 File Offset: 0x004E5A60
		private static void free(GameObject prefab)
		{
			if (prefab != null)
			{
				prefab.SetActive(false);
				Queue<GameObject> queue;
				if (!ImposterExtra.pools.TryGetValue(prefab.name, out queue))
				{
					queue = new Queue<GameObject>();
					ImposterExtra.pools.Add(prefab.name, queue);
				}
				queue.Enqueue(prefab);
			}
		}

		// Token: 0x0600D366 RID: 54118 RVA: 0x004E78B0 File Offset: 0x004E5AB0
		public static GameObject next(string prefab)
		{
			if (prefab == null)
			{
				return null;
			}
			Queue<GameObject> queue;
			if (!ImposterExtra.pools.TryGetValue(prefab, out queue))
			{
				queue = new Queue<GameObject>();
				ImposterExtra.pools.Add(prefab, queue);
			}
			if (queue.Count == 0)
			{
				GameObject gameObject;
				if (!ImposterExtra.prototypes.TryGetValue(prefab, out gameObject))
				{
					gameObject = Resources.Load(prefab) as GameObject;
					gameObject.SetActive(false);
					ImposterExtra.prototypes.Add(prefab, gameObject);
				}
				GameObject gameObject2 = global::UnityEngine.Object.Instantiate<GameObject>(Resources.Load(prefab) as GameObject);
				if (ImposterExtra.imposterRoot == null)
				{
					ImposterExtra.imposterRoot = new GameObject("ImposterRoot");
				}
				gameObject2.transform.parent = ImposterExtra.imposterRoot.transform;
				queue.Enqueue(gameObject2);
			}
			GameObject gameObject3 = queue.Dequeue();
			gameObject3.name = prefab;
			gameObject3.SetActive(true);
			if (gameObject3 != null)
			{
				ImposterExtra.objectNameCache[gameObject3] = gameObject3.name;
			}
			return gameObject3;
		}

		// Token: 0x040053E1 RID: 21473
		private static List<GameObject>[] renderedImposters = new List<GameObject>[2000];

		// Token: 0x040053E2 RID: 21474
		public List<ImposterExtra.ImposterInfo> imposters = new List<ImposterExtra.ImposterInfo>();

		// Token: 0x040053E3 RID: 21475
		private static Dictionary<GameObject, string> objectNameCache = new Dictionary<GameObject, string>();

		// Token: 0x040053E4 RID: 21476
		private static GameObject imposterRoot;

		// Token: 0x040053E5 RID: 21477
		private static Dictionary<string, GameObject> prototypes = new Dictionary<string, GameObject>();

		// Token: 0x040053E6 RID: 21478
		private static Dictionary<string, Queue<GameObject>> pools = new Dictionary<string, Queue<GameObject>>();

		// Token: 0x02001E39 RID: 7737
		public struct ImposterInfo
		{
			// Token: 0x0600F6E1 RID: 63201 RVA: 0x0055AAA8 File Offset: 0x00558CA8
			public ImposterInfo(string prefab, int layer = 0, string config = null)
			{
				this.prefab = prefab;
				this.offset = new Vector3(0f, 0f, 0f);
				this.layer = layer;
				this.config = config;
			}

			// Token: 0x0600F6E2 RID: 63202 RVA: 0x0055AAD9 File Offset: 0x00558CD9
			public ImposterInfo(string prefab, Vector2 offset, int layer = 0, string config = null)
			{
				this.prefab = prefab;
				this.offset = new Vector3(offset.x, offset.y, 0f);
				this.layer = layer;
				this.config = config;
			}

			// Token: 0x0600F6E3 RID: 63203 RVA: 0x0055AB0D File Offset: 0x00558D0D
			public ImposterInfo(string prefab, Vector3 offset, int layer = 0, string config = null)
			{
				this.prefab = prefab;
				this.offset = offset;
				this.layer = layer;
				this.config = config;
			}

			// Token: 0x0600F6E4 RID: 63204 RVA: 0x0055AB2C File Offset: 0x00558D2C
			public bool SameAs(ImposterExtra.ImposterInfo info)
			{
				return this.prefab == info.prefab && this.offset == info.offset && this.config == info.config;
			}

			// Token: 0x04007614 RID: 30228
			public int layer;

			// Token: 0x04007615 RID: 30229
			public string prefab;

			// Token: 0x04007616 RID: 30230
			public string config;

			// Token: 0x04007617 RID: 30231
			public Vector3 offset;
		}
	}
}
