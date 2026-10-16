using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000C3 RID: 195
public class exSpriteColorController : MonoBehaviour
{
	// Token: 0x1700010A RID: 266
	// (get) Token: 0x060007C7 RID: 1991 RVA: 0x00032974 File Offset: 0x00030B74
	// (set) Token: 0x060007C8 RID: 1992 RVA: 0x0003297C File Offset: 0x00030B7C
	public Color color
	{
		get
		{
			return this.color_;
		}
		set
		{
			if (this.color_ != value)
			{
				this.color_ = value;
				for (int i = 0; i < this.colorInfos.Count; i++)
				{
					exSpriteColorController.ColorInfo colorInfo = this.colorInfos[i];
					colorInfo.sprite.color = colorInfo.color * this.color_;
				}
			}
		}
	}

	// Token: 0x060007C9 RID: 1993 RVA: 0x000329DD File Offset: 0x00030BDD
	private void Awake()
	{
		base.enabled = false;
	}

	// Token: 0x060007CA RID: 1994 RVA: 0x000329E8 File Offset: 0x00030BE8
	public void EnableSprites(bool _enabled)
	{
		for (int i = 0; i < this.colorInfos.Count; i++)
		{
			this.colorInfos[i].sprite.enabled = _enabled;
		}
	}

	// Token: 0x060007CB RID: 1995 RVA: 0x00032A24 File Offset: 0x00030C24
	public void RegisterColor(GameObject _go)
	{
		exSpriteBase[] componentsInChildren = _go.GetComponentsInChildren<exSpriteBase>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			this.RegisterColor(componentsInChildren[i]);
		}
	}

	// Token: 0x060007CC RID: 1996 RVA: 0x00032A50 File Offset: 0x00030C50
	public void RegisterColor(exSpriteBase _sprite)
	{
		bool flag = false;
		for (int i = 0; i < this.colorInfos.Count; i++)
		{
			exSpriteColorController.ColorInfo colorInfo = this.colorInfos[i];
			if (colorInfo.sprite == _sprite)
			{
				colorInfo.color = _sprite.color;
				flag = true;
			}
		}
		if (!flag)
		{
			exSpriteColorController.ColorInfo colorInfo2 = new exSpriteColorController.ColorInfo();
			colorInfo2.sprite = _sprite;
			colorInfo2.color = _sprite.color;
			this.colorInfos.Add(colorInfo2);
		}
	}

	// Token: 0x060007CD RID: 1997 RVA: 0x00032AC8 File Offset: 0x00030CC8
	public void UnregisterColor(GameObject _go)
	{
		exSpriteBase[] componentsInChildren = _go.GetComponentsInChildren<exSpriteBase>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			this.UnregisterColor(componentsInChildren[i]);
		}
	}

	// Token: 0x060007CE RID: 1998 RVA: 0x00032AF4 File Offset: 0x00030CF4
	public void UnregisterColor(exSpriteBase _sprite)
	{
		for (int i = 0; i < this.colorInfos.Count; i++)
		{
			if (this.colorInfos[i].sprite == _sprite)
			{
				this.colorInfos.RemoveAt(i);
				return;
			}
		}
	}

	// Token: 0x060007CF RID: 1999 RVA: 0x00032B40 File Offset: 0x00030D40
	public void RegisterAllChildren()
	{
		this.colorInfos.Clear();
		exSpriteBase[] componentsInChildren = base.GetComponentsInChildren<exSpriteBase>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			exSpriteColorController.ColorInfo colorInfo = new exSpriteColorController.ColorInfo();
			exSpriteBase exSpriteBase = componentsInChildren[i];
			colorInfo.sprite = exSpriteBase;
			colorInfo.color = exSpriteBase.color;
			this.colorInfos.Add(colorInfo);
		}
	}

	// Token: 0x060007D0 RID: 2000 RVA: 0x00032B98 File Offset: 0x00030D98
	public void RemoveNullSprites()
	{
		for (int i = this.colorInfos.Count - 1; i >= 0; i--)
		{
			if (this.colorInfos[i].sprite == null)
			{
				this.colorInfos.RemoveAt(i);
			}
		}
	}

	// Token: 0x04000541 RID: 1345
	[SerializeField]
	protected Color color_ = Color.white;

	// Token: 0x04000542 RID: 1346
	public List<exSpriteColorController.ColorInfo> colorInfos = new List<exSpriteColorController.ColorInfo>();

	// Token: 0x020015C4 RID: 5572
	[Serializable]
	public class ColorInfo
	{
		// Token: 0x040057B3 RID: 22451
		public exSpriteBase sprite;

		// Token: 0x040057B4 RID: 22452
		public Color color;
	}
}
