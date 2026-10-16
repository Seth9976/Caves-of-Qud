using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using Qud.UI;
using UnityEngine;

namespace XRL.UI.Framework
{
	// Token: 0x020014FC RID: 5372
	public class StartingLocationControl : MonoBehaviour, IFrameworkControl
	{
		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x0600D1C0 RID: 53696 RVA: 0x004DE988 File Offset: 0x004DCB88
		protected Dictionary<string, UIThreeColorProperties> tiles
		{
			get
			{
				if (this._tiles == null)
				{
					this._tiles = new Dictionary<string, UIThreeColorProperties>();
					foreach (object obj in this.grid.transform)
					{
						Transform transform = (Transform)obj;
						this._tiles.Add(transform.name, transform.GetComponent<UIThreeColorProperties>());
					}
				}
				return this._tiles;
			}
		}

		// Token: 0x0600D1C1 RID: 53697 RVA: 0x004DEA10 File Offset: 0x004DCC10
		public void setData(FrameworkDataElement data)
		{
			base.GetComponent<ImageTinyFrame>().image = null;
			StartingLocationData startingLocationData = data as StartingLocationData;
			if (startingLocationData == null)
			{
				throw new ArgumentException("StartingLocationData expected StartingLocationControl data");
			}
			base.GetComponent<TitledIconButton>().SetTitle(startingLocationData.Name + (string.IsNullOrEmpty(startingLocationData.Hotkey) ? "" : ("\n" + startingLocationData.Hotkey)));
			this.originals.Clear();
			foreach (KeyValuePair<string, StartingLocationGridElement> keyValuePair in startingLocationData.grid)
			{
				StartingLocationGridElement value = keyValuePair.Value;
				this.originals.Add(keyValuePair.Key, value);
			}
			this.UpdateTiles(false);
		}

		// Token: 0x0600D1C2 RID: 53698 RVA: 0x004DEAE4 File Offset: 0x004DCCE4
		public NavigationContext GetNavigationContext()
		{
			FrameworkContext component = base.GetComponent<FrameworkContext>();
			if (component == null)
			{
				return null;
			}
			return component.context;
		}

		// Token: 0x0600D1C3 RID: 53699 RVA: 0x004DEAF8 File Offset: 0x004DCCF8
		private void UpdateTiles(bool toSelected)
		{
			bool? flag = this.selected;
			if ((flag.GetValueOrDefault() == toSelected) & (flag != null))
			{
				return;
			}
			this.selected = new bool?(toSelected);
			foreach (KeyValuePair<string, UIThreeColorProperties> keyValuePair in this.tiles)
			{
				keyValuePair.Value.FromRenderable(this.originals[keyValuePair.Key], true);
				flag = this.selected;
				bool flag2 = false;
				if ((flag.GetValueOrDefault() == flag2) & (flag != null))
				{
					keyValuePair.Value.SetColors(this.disabledColor, this.disabledColor, new Color(0f, 0f, 0f, 0f));
				}
			}
		}

		// Token: 0x0600D1C4 RID: 53700 RVA: 0x004DEBDC File Offset: 0x004DCDDC
		private void Update()
		{
			this.Sync();
		}

		// Token: 0x0600D1C5 RID: 53701 RVA: 0x004DEBE4 File Offset: 0x004DCDE4
		public void Sync()
		{
			FrameworkContext componentInParent = base.GetComponentInParent<FrameworkContext>();
			bool? flag;
			if (componentInParent == null)
			{
				flag = null;
			}
			else
			{
				NavigationContext context = componentInParent.context;
				flag = ((context != null) ? new bool?(context.IsActive(true)) : null);
			}
			bool? flag2 = flag;
			bool valueOrDefault = flag2.GetValueOrDefault();
			this.UpdateTiles(valueOrDefault);
		}

		// Token: 0x040052EC RID: 21228
		public UITextSkin text;

		// Token: 0x040052ED RID: 21229
		public GameObject grid;

		// Token: 0x040052EE RID: 21230
		protected Dictionary<string, UIThreeColorProperties> _tiles;

		// Token: 0x040052EF RID: 21231
		protected Dictionary<string, IRenderable> originals = new Dictionary<string, IRenderable>();

		// Token: 0x040052F0 RID: 21232
		public Color disabledColor = new Color(0.259f, 0.392f, 0.439f);

		// Token: 0x040052F1 RID: 21233
		private bool? selected;
	}
}
