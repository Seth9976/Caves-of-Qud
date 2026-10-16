using System;
using ConsoleLib.Console;
using Kobold;
using UnityEngine;
using UnityEngine.UI;
using XRL.World;

// Token: 0x02000117 RID: 279
public class UIThreeColorProperties : MonoBehaviour
{
	// Token: 0x1700013F RID: 319
	// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x000474DE File Offset: 0x000456DE
	// (set) Token: 0x06000AF3 RID: 2803 RVA: 0x000474E6 File Offset: 0x000456E6
	public Color Foreground
	{
		get
		{
			return this._Foreground;
		}
		set
		{
			if (this._Foreground != value)
			{
				this._Foreground = value;
				this.UpdateColors();
			}
		}
	}

	// Token: 0x17000140 RID: 320
	// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x00047503 File Offset: 0x00045703
	// (set) Token: 0x06000AF5 RID: 2805 RVA: 0x0004750B File Offset: 0x0004570B
	public Color Detail
	{
		get
		{
			return this._Detail;
		}
		set
		{
			if (this._Detail != value)
			{
				this._Detail = value;
				this.UpdateColors();
			}
		}
	}

	// Token: 0x17000141 RID: 321
	// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x00047528 File Offset: 0x00045728
	// (set) Token: 0x06000AF7 RID: 2807 RVA: 0x00047530 File Offset: 0x00045730
	public Color Background
	{
		get
		{
			return this._Background;
		}
		set
		{
			if (this._Background != value)
			{
				this._Background = value;
				this.UpdateColors();
			}
		}
	}

	// Token: 0x06000AF8 RID: 2808 RVA: 0x0004754D File Offset: 0x0004574D
	public void SetColors(Color F, Color D, Color B)
	{
		this._Foreground = F;
		this._Detail = D;
		this._Background = B;
		this.UpdateColors();
	}

	// Token: 0x06000AF9 RID: 2809 RVA: 0x0004756C File Offset: 0x0004576C
	public void FromConsoleChar(ConsoleChar c)
	{
		if (c.Char == '\0')
		{
			this.image.sprite = SpriteManager.GetUnitySprite(c.Tile);
			this.SetColors(c.Foreground, c.Detail, c.Background);
			return;
		}
		this.image.sprite = SpriteManager.GetUnitySprite("Text/" + ((int)c.Char).ToString() + ".bmp");
		this.SetColors(c.Background, c.Foreground, c.Foreground);
	}

	// Token: 0x06000AFA RID: 2810 RVA: 0x000475F5 File Offset: 0x000457F5
	public void FromRenderEvent(RenderEvent e)
	{
		this.FromRenderable(e, true);
	}

	// Token: 0x06000AFB RID: 2811 RVA: 0x00047600 File Offset: 0x00045800
	public void SetHFlip(bool Value)
	{
		Transform transform = this.image.transform;
		Vector3 localScale = transform.localScale;
		if (Value)
		{
			transform.localScale = new Vector3(Mathf.Abs(localScale.x) * -1f, localScale.y, localScale.z);
			return;
		}
		transform.localScale = new Vector3(Mathf.Abs(localScale.x), localScale.y, localScale.z);
	}

	// Token: 0x06000AFC RID: 2812 RVA: 0x00047670 File Offset: 0x00045870
	public void SetVFlip(bool Value)
	{
		Transform transform = this.image.transform;
		Vector3 localScale = transform.localScale;
		if (Value)
		{
			transform.localScale = new Vector3(localScale.x, Mathf.Abs(localScale.y) * -1f, localScale.z);
			return;
		}
		transform.localScale = new Vector3(localScale.x, Mathf.Abs(localScale.y), localScale.z);
	}

	// Token: 0x06000AFD RID: 2813 RVA: 0x000476E0 File Offset: 0x000458E0
	public void FromRenderable(IRenderable e, bool transparentBlackBackgrounds = true)
	{
		if (e == null || (e.getTile() == null && e.getRenderString() == null))
		{
			this.SetColors(Color.clear, Color.clear, Color.clear);
			this.image.transform.localScale = new Vector3(Mathf.Abs(this.image.transform.localScale.x), Mathf.Abs(this.image.transform.localScale.y), this.image.transform.localScale.z);
			return;
		}
		this.SetHFlip(e.getHFlip());
		this.SetVFlip(e.getVFlip());
		ColorChars value = ((e != null) ? new ColorChars?(e.getColorChars()) : null).Value;
		char c = value.background;
		if (transparentBlackBackgrounds && value.background == 'k')
		{
			c = '\0';
		}
		if (e != null && e.getTile() != null)
		{
			this.image.sprite = SpriteManager.GetUnitySprite(e.getTile());
			this.SetColors(ConsoleLib.Console.ColorUtility.colorFromChar(value.foreground), ConsoleLib.Console.ColorUtility.colorFromChar(value.detail), ConsoleLib.Console.ColorUtility.colorFromChar(c));
			return;
		}
		if (e != null && e.getRenderString() != null)
		{
			this.image.sprite = SpriteManager.GetUnitySprite("Text/" + ((int)e.getRenderString()[0]).ToString() + ".bmp");
			this.SetColors(ConsoleLib.Console.ColorUtility.colorFromChar(value.background), ConsoleLib.Console.ColorUtility.colorFromChar(value.foreground), ConsoleLib.Console.ColorUtility.colorFromChar(c));
			return;
		}
		MetricsManager.LogWarning("What is this render" + ((e != null) ? e.ToString() : null));
	}

	// Token: 0x06000AFE RID: 2814 RVA: 0x00047884 File Offset: 0x00045A84
	private void UpdateColors()
	{
		if (this._Foreground == this._currentForeground && this._Background == this._currentBackground && this._Detail == this._currentDetail)
		{
			return;
		}
		if (this.image == null)
		{
			return;
		}
		if (!this.set)
		{
			if (this.image.material == null || !this.image.material.name.StartsWith("UI Tile Material"))
			{
				this.image.material = Resources.Load<Material>("Materials/UI Tile Material");
			}
			Material material = global::UnityEngine.Object.Instantiate<Material>(this.image.material);
			material.name = string.Format("UI Tile Material ({0})", UIThreeColorProperties.val += 1L);
			this.image.material = material;
			this.set = true;
		}
		this.image.material.SetColor("_Foreground", this._Foreground);
		this.image.material.SetColor("_Detail", this._Detail);
		this.image.material.SetColor("_Background", this._Background);
		this.image.enabled = false;
		this.image.enabled = true;
		this._currentForeground = this._Foreground;
		this._currentBackground = this._Background;
		this._currentDetail = this._Detail;
	}

	// Token: 0x06000AFF RID: 2815 RVA: 0x000479FD File Offset: 0x00045BFD
	private void Awake()
	{
		this.UpdateColors();
	}

	// Token: 0x06000B00 RID: 2816 RVA: 0x00047A05 File Offset: 0x00045C05
	private void Start()
	{
	}

	// Token: 0x06000B01 RID: 2817 RVA: 0x00047A07 File Offset: 0x00045C07
	private void Update()
	{
	}

	// Token: 0x0400074C RID: 1868
	[SerializeField]
	private Color _Foreground = Color.white;

	// Token: 0x0400074D RID: 1869
	[SerializeField]
	private Color _Detail = Color.black;

	// Token: 0x0400074E RID: 1870
	[SerializeField]
	private Color _Background = new Color(0f, 0f, 0f, 0f);

	// Token: 0x0400074F RID: 1871
	public Image image;

	// Token: 0x04000750 RID: 1872
	public bool Updated;

	// Token: 0x04000751 RID: 1873
	public Color _currentForeground = new Color(1f, 2f, 3f, 4f);

	// Token: 0x04000752 RID: 1874
	public Color _currentDetail = new Color(1f, 2f, 3f, 4f);

	// Token: 0x04000753 RID: 1875
	public Color _currentBackground = new Color(1f, 2f, 3f, 4f);

	// Token: 0x04000754 RID: 1876
	private static long val;

	// Token: 0x04000755 RID: 1877
	private bool set;
}
