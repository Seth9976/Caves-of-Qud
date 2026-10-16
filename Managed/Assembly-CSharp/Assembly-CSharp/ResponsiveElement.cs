using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using XRL.UI;

// Token: 0x02000127 RID: 295
public class ResponsiveElement : MonoBehaviour
{
	// Token: 0x17000142 RID: 322
	// (get) Token: 0x06000B4E RID: 2894 RVA: 0x0004B426 File Offset: 0x00049626
	public TextMeshProUGUI tmp
	{
		get
		{
			if (this._tmp == null)
			{
				this._tmp = base.gameObject.GetComponent<TextMeshProUGUI>();
			}
			return this._tmp;
		}
	}

	// Token: 0x17000143 RID: 323
	// (get) Token: 0x06000B4F RID: 2895 RVA: 0x0004B450 File Offset: 0x00049650
	public Image image
	{
		get
		{
			if (this._image == null)
			{
				this._image = base.gameObject.GetComponent<Image>();
			}
			if (this._image == null && this.controlImage)
			{
				this._image = base.gameObject.AddComponent<Image>();
			}
			return this._image;
		}
	}

	// Token: 0x17000144 RID: 324
	// (get) Token: 0x06000B50 RID: 2896 RVA: 0x0004B4AC File Offset: 0x000496AC
	public Canvas canvas
	{
		get
		{
			if (this._canvas == null)
			{
				this._canvas = base.gameObject.GetComponent<Canvas>();
			}
			if (this._canvas == null && this.controlCanvas)
			{
				this._canvas = base.gameObject.AddComponent<Canvas>();
			}
			return this._canvas;
		}
	}

	// Token: 0x17000145 RID: 325
	// (get) Token: 0x06000B51 RID: 2897 RVA: 0x0004B508 File Offset: 0x00049708
	public LayoutElement layoutElement
	{
		get
		{
			if (this._layoutElement == null)
			{
				this._layoutElement = base.gameObject.GetComponent<LayoutElement>();
			}
			if (this._layoutElement == null && this.controlLayoutGroup)
			{
				this._layoutElement = base.gameObject.AddComponent<LayoutElement>();
			}
			return this._layoutElement;
		}
	}

	// Token: 0x06000B52 RID: 2898 RVA: 0x0004B564 File Offset: 0x00049764
	private void Check()
	{
		if (this.lastClass == Media.sizeClass)
		{
			return;
		}
		this.lastClass = Media.sizeClass;
		if ((this.MinimumClass == Media.SizeClass.Unset || Media.sizeClass >= this.MinimumClass) && (this.MaximumClass == Media.SizeClass.Unset || Media.sizeClass <= this.MaximumClass))
		{
			if (this.controlImage && !this.image.enabled)
			{
				this.image.enabled = true;
			}
			if (this.controlCanvas && !this.canvas.enabled)
			{
				this.canvas.enabled = true;
			}
			if (this.controlLayoutGroup && this.layoutElement.ignoreLayout)
			{
				this.layoutElement.ignoreLayout = false;
			}
			if (this.controlTMPFontSize)
			{
				if (Media.sizeClass <= Media.SizeClass.Small)
				{
					this.tmp.fontSize = (float)this.smallTMPSize;
					return;
				}
				if (Media.sizeClass == Media.SizeClass.Medium)
				{
					this.tmp.fontSize = (float)this.mediumTMPSize;
					return;
				}
				if (Media.sizeClass >= Media.SizeClass.Large)
				{
					this.tmp.fontSize = (float)this.largeTMPSize;
					return;
				}
			}
		}
		else
		{
			if (this.controlImage && this.image.enabled)
			{
				this.image.enabled = false;
			}
			if (this.controlCanvas && this.canvas.enabled)
			{
				this.canvas.enabled = false;
			}
			if (this.controlLayoutGroup && !this.layoutElement.ignoreLayout)
			{
				this.layoutElement.ignoreLayout = true;
			}
		}
	}

	// Token: 0x06000B53 RID: 2899 RVA: 0x0004B6EB File Offset: 0x000498EB
	private void Awake()
	{
		this.Check();
	}

	// Token: 0x06000B54 RID: 2900 RVA: 0x0004B6F3 File Offset: 0x000498F3
	private void Update()
	{
		this.Check();
	}

	// Token: 0x040007C1 RID: 1985
	public Media.SizeClass MinimumClass;

	// Token: 0x040007C2 RID: 1986
	public Media.SizeClass MaximumClass;

	// Token: 0x040007C3 RID: 1987
	public bool controlTMPFontSize;

	// Token: 0x040007C4 RID: 1988
	public int smallTMPSize;

	// Token: 0x040007C5 RID: 1989
	public int mediumTMPSize;

	// Token: 0x040007C6 RID: 1990
	public int largeTMPSize;

	// Token: 0x040007C7 RID: 1991
	private TextMeshProUGUI _tmp;

	// Token: 0x040007C8 RID: 1992
	public bool controlImage;

	// Token: 0x040007C9 RID: 1993
	public Image _image;

	// Token: 0x040007CA RID: 1994
	public bool controlCanvas;

	// Token: 0x040007CB RID: 1995
	public Canvas _canvas;

	// Token: 0x040007CC RID: 1996
	public bool controlLayoutGroup;

	// Token: 0x040007CD RID: 1997
	public LayoutElement _layoutElement;

	// Token: 0x040007CE RID: 1998
	private Media.SizeClass lastClass = Media.SizeClass.Unset;
}
