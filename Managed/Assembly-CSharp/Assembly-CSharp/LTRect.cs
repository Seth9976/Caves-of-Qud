using System;
using UnityEngine;

// Token: 0x0200010F RID: 271
[Serializable]
public class LTRect
{
	// Token: 0x06000AA6 RID: 2726 RVA: 0x0004514C File Offset: 0x0004334C
	public LTRect()
	{
		this.reset();
		this.rotateEnabled = (this.alphaEnabled = true);
		this._rect = new Rect(0f, 0f, 1f, 1f);
	}

	// Token: 0x06000AA7 RID: 2727 RVA: 0x000451D0 File Offset: 0x000433D0
	public LTRect(Rect rect)
	{
		this._rect = rect;
		this.reset();
	}

	// Token: 0x06000AA8 RID: 2728 RVA: 0x0004522C File Offset: 0x0004342C
	public LTRect(float x, float y, float width, float height)
	{
		this._rect = new Rect(x, y, width, height);
		this.alpha = 1f;
		this.rotation = 0f;
		this.rotateEnabled = (this.alphaEnabled = false);
	}

	// Token: 0x06000AA9 RID: 2729 RVA: 0x000452B4 File Offset: 0x000434B4
	public LTRect(float x, float y, float width, float height, float alpha)
	{
		this._rect = new Rect(x, y, width, height);
		this.alpha = alpha;
		this.rotation = 0f;
		this.rotateEnabled = (this.alphaEnabled = false);
	}

	// Token: 0x06000AAA RID: 2730 RVA: 0x00045338 File Offset: 0x00043538
	public LTRect(float x, float y, float width, float height, float alpha, float rotation)
	{
		this._rect = new Rect(x, y, width, height);
		this.alpha = alpha;
		this.rotation = rotation;
		this.rotateEnabled = (this.alphaEnabled = false);
		if (rotation != 0f)
		{
			this.rotateEnabled = true;
			this.resetForRotation();
		}
	}

	// Token: 0x17000138 RID: 312
	// (get) Token: 0x06000AAB RID: 2731 RVA: 0x000453CD File Offset: 0x000435CD
	public bool hasInitiliazed
	{
		get
		{
			return this._id != -1;
		}
	}

	// Token: 0x17000139 RID: 313
	// (get) Token: 0x06000AAC RID: 2732 RVA: 0x000453DB File Offset: 0x000435DB
	public int id
	{
		get
		{
			return this._id | (this.counter << 16);
		}
	}

	// Token: 0x06000AAD RID: 2733 RVA: 0x000453ED File Offset: 0x000435ED
	public void setId(int id, int counter)
	{
		this._id = id;
		this.counter = counter;
	}

	// Token: 0x06000AAE RID: 2734 RVA: 0x00045400 File Offset: 0x00043600
	public void reset()
	{
		this.alpha = 1f;
		this.rotation = 0f;
		this.rotateEnabled = (this.alphaEnabled = false);
		this.margin = Vector2.zero;
		this.sizeByHeight = false;
		this.useColor = false;
	}

	// Token: 0x06000AAF RID: 2735 RVA: 0x0004544C File Offset: 0x0004364C
	public void resetForRotation()
	{
		Vector3 vector = new Vector3(GUI.matrix[0, 0], GUI.matrix[1, 1], GUI.matrix[2, 2]);
		if (this.pivot == Vector2.zero)
		{
			this.pivot = new Vector2((this._rect.x + this._rect.width * 0.5f) * vector.x + GUI.matrix[0, 3], (this._rect.y + this._rect.height * 0.5f) * vector.y + GUI.matrix[1, 3]);
		}
	}

	// Token: 0x1700013A RID: 314
	// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x00045512 File Offset: 0x00043712
	// (set) Token: 0x06000AB1 RID: 2737 RVA: 0x0004551F File Offset: 0x0004371F
	public float x
	{
		get
		{
			return this._rect.x;
		}
		set
		{
			this._rect.x = value;
		}
	}

	// Token: 0x1700013B RID: 315
	// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x0004552D File Offset: 0x0004372D
	// (set) Token: 0x06000AB3 RID: 2739 RVA: 0x0004553A File Offset: 0x0004373A
	public float y
	{
		get
		{
			return this._rect.y;
		}
		set
		{
			this._rect.y = value;
		}
	}

	// Token: 0x1700013C RID: 316
	// (get) Token: 0x06000AB4 RID: 2740 RVA: 0x00045548 File Offset: 0x00043748
	// (set) Token: 0x06000AB5 RID: 2741 RVA: 0x00045555 File Offset: 0x00043755
	public float width
	{
		get
		{
			return this._rect.width;
		}
		set
		{
			this._rect.width = value;
		}
	}

	// Token: 0x1700013D RID: 317
	// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x00045563 File Offset: 0x00043763
	// (set) Token: 0x06000AB7 RID: 2743 RVA: 0x00045570 File Offset: 0x00043770
	public float height
	{
		get
		{
			return this._rect.height;
		}
		set
		{
			this._rect.height = value;
		}
	}

	// Token: 0x1700013E RID: 318
	// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x00045580 File Offset: 0x00043780
	// (set) Token: 0x06000AB9 RID: 2745 RVA: 0x00045691 File Offset: 0x00043891
	public Rect rect
	{
		get
		{
			if (LTRect.colorTouched)
			{
				LTRect.colorTouched = false;
				GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, 1f);
			}
			if (this.rotateEnabled)
			{
				if (this.rotateFinished)
				{
					this.rotateFinished = false;
					this.rotateEnabled = false;
					this.pivot = Vector2.zero;
				}
				else
				{
					GUIUtility.RotateAroundPivot(this.rotation, this.pivot);
				}
			}
			if (this.alphaEnabled)
			{
				GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, this.alpha);
				LTRect.colorTouched = true;
			}
			if (this.fontScaleToFit)
			{
				if (this.useSimpleScale)
				{
					this.style.fontSize = (int)(this._rect.height * this.relativeRect.height);
				}
				else
				{
					this.style.fontSize = (int)this._rect.height;
				}
			}
			return this._rect;
		}
		set
		{
			this._rect = value;
		}
	}

	// Token: 0x06000ABA RID: 2746 RVA: 0x0004569A File Offset: 0x0004389A
	public LTRect setStyle(GUIStyle style)
	{
		this.style = style;
		return this;
	}

	// Token: 0x06000ABB RID: 2747 RVA: 0x000456A4 File Offset: 0x000438A4
	public LTRect setFontScaleToFit(bool fontScaleToFit)
	{
		this.fontScaleToFit = fontScaleToFit;
		return this;
	}

	// Token: 0x06000ABC RID: 2748 RVA: 0x000456AE File Offset: 0x000438AE
	public LTRect setColor(Color color)
	{
		this.color = color;
		this.useColor = true;
		return this;
	}

	// Token: 0x06000ABD RID: 2749 RVA: 0x000456BF File Offset: 0x000438BF
	public LTRect setAlpha(float alpha)
	{
		this.alpha = alpha;
		return this;
	}

	// Token: 0x06000ABE RID: 2750 RVA: 0x000456C9 File Offset: 0x000438C9
	public LTRect setLabel(string str)
	{
		this.labelStr = str;
		return this;
	}

	// Token: 0x06000ABF RID: 2751 RVA: 0x000456D3 File Offset: 0x000438D3
	public LTRect setUseSimpleScale(bool useSimpleScale, Rect relativeRect)
	{
		this.useSimpleScale = useSimpleScale;
		this.relativeRect = relativeRect;
		return this;
	}

	// Token: 0x06000AC0 RID: 2752 RVA: 0x000456E4 File Offset: 0x000438E4
	public LTRect setUseSimpleScale(bool useSimpleScale)
	{
		this.useSimpleScale = useSimpleScale;
		this.relativeRect = new Rect(0f, 0f, (float)Screen.width, (float)Screen.height);
		return this;
	}

	// Token: 0x06000AC1 RID: 2753 RVA: 0x0004570F File Offset: 0x0004390F
	public LTRect setSizeByHeight(bool sizeByHeight)
	{
		this.sizeByHeight = sizeByHeight;
		return this;
	}

	// Token: 0x06000AC2 RID: 2754 RVA: 0x0004571C File Offset: 0x0004391C
	public override string ToString()
	{
		return string.Concat(new string[]
		{
			"x:",
			this._rect.x.ToString(),
			" y:",
			this._rect.y.ToString(),
			" width:",
			this._rect.width.ToString(),
			" height:",
			this._rect.height.ToString()
		});
	}

	// Token: 0x04000721 RID: 1825
	public Rect _rect;

	// Token: 0x04000722 RID: 1826
	public float alpha = 1f;

	// Token: 0x04000723 RID: 1827
	public float rotation;

	// Token: 0x04000724 RID: 1828
	public Vector2 pivot;

	// Token: 0x04000725 RID: 1829
	public Vector2 margin;

	// Token: 0x04000726 RID: 1830
	public Rect relativeRect = new Rect(0f, 0f, float.PositiveInfinity, float.PositiveInfinity);

	// Token: 0x04000727 RID: 1831
	public bool rotateEnabled;

	// Token: 0x04000728 RID: 1832
	[HideInInspector]
	public bool rotateFinished;

	// Token: 0x04000729 RID: 1833
	public bool alphaEnabled;

	// Token: 0x0400072A RID: 1834
	public string labelStr;

	// Token: 0x0400072B RID: 1835
	public LTGUI.Element_Type type;

	// Token: 0x0400072C RID: 1836
	public GUIStyle style;

	// Token: 0x0400072D RID: 1837
	public bool useColor;

	// Token: 0x0400072E RID: 1838
	public Color color = Color.white;

	// Token: 0x0400072F RID: 1839
	public bool fontScaleToFit;

	// Token: 0x04000730 RID: 1840
	public bool useSimpleScale;

	// Token: 0x04000731 RID: 1841
	public bool sizeByHeight;

	// Token: 0x04000732 RID: 1842
	public Texture texture;

	// Token: 0x04000733 RID: 1843
	private int _id = -1;

	// Token: 0x04000734 RID: 1844
	[HideInInspector]
	public int counter;

	// Token: 0x04000735 RID: 1845
	public static bool colorTouched;
}
