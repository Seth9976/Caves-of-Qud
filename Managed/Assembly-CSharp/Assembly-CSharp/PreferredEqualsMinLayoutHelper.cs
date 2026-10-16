using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000079 RID: 121
[ExecuteAlways]
public class PreferredEqualsMinLayoutHelper : MonoBehaviour, ILayoutElement
{
	// Token: 0x1700007A RID: 122
	// (get) Token: 0x06000482 RID: 1154 RVA: 0x0001DE3D File Offset: 0x0001C03D
	public ILayoutElement otherLayoutElement
	{
		get
		{
			return this.PreferredSource as ILayoutElement;
		}
	}

	// Token: 0x1700007B RID: 123
	// (get) Token: 0x06000483 RID: 1155 RVA: 0x0001DE4A File Offset: 0x0001C04A
	int ILayoutElement.layoutPriority
	{
		get
		{
			if (this.otherLayoutElement == null)
			{
				Debug.LogError(base.gameObject.name + " layout helper error - otherLayoutElement not set");
			}
			if (this.otherLayoutElement != null)
			{
				return this.otherLayoutElement.layoutPriority + 1;
			}
			return 0;
		}
	}

	// Token: 0x1700007C RID: 124
	// (get) Token: 0x06000484 RID: 1156 RVA: 0x0001DE85 File Offset: 0x0001C085
	float ILayoutElement.minWidth
	{
		get
		{
			if (!this.UseWidth)
			{
				return -1f;
			}
			return this.otherLayoutElement.preferredWidth;
		}
	}

	// Token: 0x1700007D RID: 125
	// (get) Token: 0x06000485 RID: 1157 RVA: 0x0001DEA0 File Offset: 0x0001C0A0
	float ILayoutElement.minHeight
	{
		get
		{
			if (!this.UseHeight)
			{
				return -1f;
			}
			return this.otherLayoutElement.preferredHeight;
		}
	}

	// Token: 0x1700007E RID: 126
	// (get) Token: 0x06000486 RID: 1158 RVA: 0x0001DEBB File Offset: 0x0001C0BB
	float ILayoutElement.preferredWidth
	{
		get
		{
			return -1f;
		}
	}

	// Token: 0x1700007F RID: 127
	// (get) Token: 0x06000487 RID: 1159 RVA: 0x0001DEC2 File Offset: 0x0001C0C2
	float ILayoutElement.flexibleWidth
	{
		get
		{
			return -1f;
		}
	}

	// Token: 0x17000080 RID: 128
	// (get) Token: 0x06000488 RID: 1160 RVA: 0x0001DEC9 File Offset: 0x0001C0C9
	float ILayoutElement.preferredHeight
	{
		get
		{
			return -1f;
		}
	}

	// Token: 0x17000081 RID: 129
	// (get) Token: 0x06000489 RID: 1161 RVA: 0x0001DED0 File Offset: 0x0001C0D0
	float ILayoutElement.flexibleHeight
	{
		get
		{
			return -1f;
		}
	}

	// Token: 0x0600048A RID: 1162 RVA: 0x0001DED7 File Offset: 0x0001C0D7
	void ILayoutElement.CalculateLayoutInputHorizontal()
	{
	}

	// Token: 0x0600048B RID: 1163 RVA: 0x0001DED9 File Offset: 0x0001C0D9
	void ILayoutElement.CalculateLayoutInputVertical()
	{
	}

	// Token: 0x04000371 RID: 881
	public MonoBehaviour PreferredSource;

	// Token: 0x04000372 RID: 882
	public bool UseWidth = true;

	// Token: 0x04000373 RID: 883
	public bool UseHeight;
}
