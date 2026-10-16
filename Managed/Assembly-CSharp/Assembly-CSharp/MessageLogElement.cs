using System;
using System.Collections.Generic;
using TMPro;
using XRL.UI;

// Token: 0x02000084 RID: 132
public class MessageLogElement : PooledScrollRectElement<string>
{
	// Token: 0x060004BF RID: 1215 RVA: 0x000200ED File Offset: 0x0001E2ED
	public override void Setup(int placement, List<string> allData)
	{
		if (this.text.text != allData[placement])
		{
			this.text.vertexBufferAutoSizeReduction = false;
			this.text.text = allData[placement];
		}
	}

	// Token: 0x060004C0 RID: 1216 RVA: 0x00020126 File Offset: 0x0001E326
	public void Awake()
	{
		this.Update();
	}

	// Token: 0x060004C1 RID: 1217 RVA: 0x00020130 File Offset: 0x0001E330
	public void Update()
	{
		int num = 16 + Options.MessageLogLineSizeAdjustment;
		if ((float)num != this.text.fontSize)
		{
			this.text.fontSize = (float)num;
		}
	}

	// Token: 0x040003B4 RID: 948
	public TextMeshProUGUI text;

	// Token: 0x040003B5 RID: 949
	public int lastSize = 16;
}
