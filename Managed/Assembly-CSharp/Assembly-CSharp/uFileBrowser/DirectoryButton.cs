using System;
using UnityEngine;
using UnityEngine.UI;

namespace uFileBrowser
{
	// Token: 0x02000184 RID: 388
	public class DirectoryButton : MonoBehaviour
	{
		// Token: 0x06000EE1 RID: 3809 RVA: 0x0005EE1E File Offset: 0x0005D01E
		public void OnClick()
		{
			if (this.browser)
			{
				this.browser.OnDirectoryClick(this.id);
			}
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x0005EE3E File Offset: 0x0005D03E
		public void Set(FileBrowser b, string txt, string path, int i)
		{
			this.browser = b;
			this.text = txt;
			this.fullPath = path;
			this.id = i;
			this.label.text = this.text;
		}

		// Token: 0x04000A40 RID: 2624
		public Text label;

		// Token: 0x04000A41 RID: 2625
		[HideInInspector]
		public string text;

		// Token: 0x04000A42 RID: 2626
		[HideInInspector]
		public string fullPath;

		// Token: 0x04000A43 RID: 2627
		[HideInInspector]
		public int id;

		// Token: 0x04000A44 RID: 2628
		private FileBrowser browser;
	}
}
