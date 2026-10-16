using System;
using UnityEngine;

// Token: 0x02000128 RID: 296
public class ShowSkin : MonoBehaviour
{
	// Token: 0x06000B56 RID: 2902 RVA: 0x0004B710 File Offset: 0x00049910
	private void OnGUI()
	{
		GUI.skin = this.skin;
		GUISkin guiskin = GUI.skin;
		GUI.BeginGroup(new Rect(30f, 20f, (float)(Screen.width - 60), (float)(Screen.height - 40)), guiskin.name, "window");
		GUIStyle style = GUI.skin.GetStyle("window");
		int num = 0;
		int num2 = 0;
		foreach (object obj in guiskin)
		{
			GUIStyle guistyle = (GUIStyle)obj;
			this.testBool = GUI.Toggle(new Rect((float)num * (this.elemWidth + 20f) + (float)style.padding.left, (float)num2 * (this.elemHeight + 15f) + (float)style.padding.top, this.elemWidth, this.elemHeight), this.testBool, new GUIContent(guistyle.name.ToUpper(), this.testIcon), guistyle);
			num++;
			if ((float)num * (this.elemWidth + 20f) > (float)Screen.width - this.elemWidth - 40f - (float)style.padding.right)
			{
				num = 0;
				num2++;
			}
		}
		GUI.EndGroup();
	}

	// Token: 0x040007CF RID: 1999
	public GUISkin skin;

	// Token: 0x040007D0 RID: 2000
	public float elemWidth = 100f;

	// Token: 0x040007D1 RID: 2001
	public float elemHeight = 30f;

	// Token: 0x040007D2 RID: 2002
	public Texture2D testIcon;

	// Token: 0x040007D3 RID: 2003
	private bool testBool;

	// Token: 0x040007D4 RID: 2004
	private int selection;
}
