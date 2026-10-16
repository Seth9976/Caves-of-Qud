using System;
using UnityEngine;
using UnityEngine.UI;

namespace uFileBrowser
{
	// Token: 0x02000187 RID: 391
	public class FileButton : MonoBehaviour
	{
		// Token: 0x06000EFE RID: 3838 RVA: 0x0005F964 File Offset: 0x0005DB64
		public void Select()
		{
			this.button.transition = Selectable.Transition.None;
			this.buttonImage.overrideSprite = this.selectedSprite;
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x0005F983 File Offset: 0x0005DB83
		public void Unselect()
		{
			this.button.transition = Selectable.Transition.SpriteSwap;
			this.buttonImage.overrideSprite = null;
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x0005F99D File Offset: 0x0005DB9D
		public void OnClick()
		{
			if (this.browser)
			{
				this.browser.OnFileClick(this.id);
			}
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x0005F9C0 File Offset: 0x0005DBC0
		public void Set(FileBrowser b, string txt, string path, bool dir, int i)
		{
			this.browser = b;
			this.text = txt;
			this.fullPath = path;
			this.isDir = dir;
			this.id = i;
			this.label.text = this.text;
			if (this.isDir)
			{
				this.fileIcon.sprite = b.folderIcon;
				return;
			}
			this.fileIcon.sprite = b.GetFileIcon(txt);
		}

		// Token: 0x04000A5F RID: 2655
		public Button button;

		// Token: 0x04000A60 RID: 2656
		public Image buttonImage;

		// Token: 0x04000A61 RID: 2657
		public Image fileIcon;

		// Token: 0x04000A62 RID: 2658
		public Text label;

		// Token: 0x04000A63 RID: 2659
		public Sprite selectedSprite;

		// Token: 0x04000A64 RID: 2660
		[HideInInspector]
		public string text;

		// Token: 0x04000A65 RID: 2661
		[HideInInspector]
		public string fullPath;

		// Token: 0x04000A66 RID: 2662
		[HideInInspector]
		public bool isDir;

		// Token: 0x04000A67 RID: 2663
		[HideInInspector]
		public int id;

		// Token: 0x04000A68 RID: 2664
		private FileBrowser browser;
	}
}
