using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000138 RID: 312
public class PlatformTextSwitcher : MonoBehaviour
{
	// Token: 0x06000BD0 RID: 3024 RVA: 0x0004EC8D File Offset: 0x0004CE8D
	private void OnEnable()
	{
		if (this.m_Label == null)
		{
			return;
		}
		this.m_Label.text = this.m_WindowsText;
	}

	// Token: 0x0400085A RID: 2138
	public Text m_Label;

	// Token: 0x0400085B RID: 2139
	public string m_WindowsText = "";

	// Token: 0x0400085C RID: 2140
	public string m_iOSText = "";

	// Token: 0x0400085D RID: 2141
	public string m_AndroidText = "";
}
