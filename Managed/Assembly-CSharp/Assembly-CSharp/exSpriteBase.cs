using System;
using UnityEngine;

// Token: 0x020000B7 RID: 183
[ExecuteInEditMode]
public abstract class exSpriteBase : exPlane, exISpriteBase, IMonoBehaviour
{
	// Token: 0x170000CA RID: 202
	// (get) Token: 0x060006B8 RID: 1720 RVA: 0x0002E51D File Offset: 0x0002C71D
	// (set) Token: 0x060006B9 RID: 1721 RVA: 0x0002E525 File Offset: 0x0002C725
	public virtual bool customSize
	{
		get
		{
			return this.customSize_;
		}
		set
		{
			this.customSize_ = value;
			this.updateFlags |= exUpdateFlags.Vertex;
		}
	}

	// Token: 0x170000CB RID: 203
	// (get) Token: 0x060006BA RID: 1722 RVA: 0x0002E53C File Offset: 0x0002C73C
	// (set) Token: 0x060006BB RID: 1723 RVA: 0x0002E544 File Offset: 0x0002C744
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
				this.updateFlags |= exUpdateFlags.Color;
			}
		}
	}

	// Token: 0x170000CC RID: 204
	// (get) Token: 0x060006BC RID: 1724 RVA: 0x0002E569 File Offset: 0x0002C769
	// (set) Token: 0x060006BD RID: 1725 RVA: 0x0002E571 File Offset: 0x0002C771
	public Vector2 shear
	{
		get
		{
			return this.shear_;
		}
		set
		{
			if (this.shear_ != value)
			{
				this.shear_ = value;
				this.updateFlags |= exUpdateFlags.Vertex;
			}
		}
	}

	// Token: 0x170000CD RID: 205
	// (get) Token: 0x060006BE RID: 1726 RVA: 0x0002E596 File Offset: 0x0002C796
	// (set) Token: 0x060006BF RID: 1727 RVA: 0x0002E59E File Offset: 0x0002C79E
	public Shader shader
	{
		get
		{
			return this.shader_;
		}
		set
		{
			if (this.shader_ == value)
			{
				return;
			}
			this.shader_ = value;
			this.UpdateMaterial();
		}
	}

	// Token: 0x170000CE RID: 206
	// (get) Token: 0x060006C0 RID: 1728 RVA: 0x0002E5B7 File Offset: 0x0002C7B7
	// (set) Token: 0x060006C1 RID: 1729 RVA: 0x0002E5BF File Offset: 0x0002C7BF
	exUpdateFlags exISpriteBase.updateFlags
	{
		get
		{
			return this.updateFlags;
		}
		set
		{
			this.updateFlags = value;
		}
	}

	// Token: 0x170000CF RID: 207
	// (get) Token: 0x060006C2 RID: 1730 RVA: 0x0002E5C8 File Offset: 0x0002C7C8
	// (set) Token: 0x060006C3 RID: 1731 RVA: 0x0002E5D0 File Offset: 0x0002C7D0
	public exClipping clip
	{
		get
		{
			return this.clip_;
		}
		set
		{
			if (this.clip_ != value)
			{
				this.clip_ = value;
				this.UpdateMaterial();
			}
		}
	}

	// Token: 0x170000D0 RID: 208
	// (get) Token: 0x060006C4 RID: 1732 RVA: 0x0002E5ED File Offset: 0x0002C7ED
	Matrix4x4 exISpriteBase.cachedWorldMatrix
	{
		get
		{
			return this.cachedWorldMatrix;
		}
	}

	// Token: 0x170000D1 RID: 209
	// (get) Token: 0x060006C5 RID: 1733 RVA: 0x0002E5F5 File Offset: 0x0002C7F5
	// (set) Token: 0x060006C6 RID: 1734 RVA: 0x0002E5FD File Offset: 0x0002C7FD
	public override float width
	{
		get
		{
			return this.width_;
		}
		set
		{
			if (this.width_ != value)
			{
				this.width_ = value;
				this.updateFlags |= exUpdateFlags.Vertex;
				this.customSize = true;
			}
		}
	}

	// Token: 0x170000D2 RID: 210
	// (get) Token: 0x060006C7 RID: 1735 RVA: 0x0002E624 File Offset: 0x0002C824
	// (set) Token: 0x060006C8 RID: 1736 RVA: 0x0002E62C File Offset: 0x0002C82C
	public override float height
	{
		get
		{
			return this.height_;
		}
		set
		{
			if (this.height_ != value)
			{
				this.height_ = value;
				this.updateFlags |= exUpdateFlags.Vertex;
				this.customSize = true;
			}
		}
	}

	// Token: 0x170000D3 RID: 211
	// (get) Token: 0x060006C9 RID: 1737 RVA: 0x0002E653 File Offset: 0x0002C853
	// (set) Token: 0x060006CA RID: 1738 RVA: 0x0002E65B File Offset: 0x0002C85B
	public override Anchor anchor
	{
		get
		{
			return this.anchor_;
		}
		set
		{
			if (this.anchor_ != value)
			{
				this.anchor_ = value;
				this.updateFlags |= exUpdateFlags.Vertex;
			}
		}
	}

	// Token: 0x170000D4 RID: 212
	// (get) Token: 0x060006CB RID: 1739 RVA: 0x0002E67B File Offset: 0x0002C87B
	// (set) Token: 0x060006CC RID: 1740 RVA: 0x0002E683 File Offset: 0x0002C883
	public override Vector2 offset
	{
		get
		{
			return this.offset_;
		}
		set
		{
			if (this.offset_ != value)
			{
				this.offset_ = value;
				this.updateFlags |= exUpdateFlags.Vertex;
			}
		}
	}

	// Token: 0x170000D5 RID: 213
	// (get) Token: 0x060006CD RID: 1741 RVA: 0x0002E6A8 File Offset: 0x0002C8A8
	public int vertexCount
	{
		get
		{
			return this.vertexCount_;
		}
	}

	// Token: 0x170000D6 RID: 214
	// (get) Token: 0x060006CE RID: 1742 RVA: 0x0002E6B0 File Offset: 0x0002C8B0
	public int indexCount
	{
		get
		{
			return this.indexCount_;
		}
	}

	// Token: 0x170000D7 RID: 215
	// (get) Token: 0x060006CF RID: 1743 RVA: 0x0002E6B8 File Offset: 0x0002C8B8
	public Material material
	{
		get
		{
			if (this.material_ != null)
			{
				return this.material_;
			}
			if (this.clip_ != null)
			{
				this.material_ = this.clip_.GetClippedMaterial(this.shader_, this.texture);
				if (this.material_ == null)
				{
					this.material_ = ex2DRenderer.GetMaterial(this.shader_, this.texture);
				}
			}
			else
			{
				this.material_ = ex2DRenderer.GetMaterial(this.shader_, this.texture);
			}
			return this.material_;
		}
	}

	// Token: 0x170000D8 RID: 216
	// (get) Token: 0x060006D0 RID: 1744
	protected abstract Texture texture { get; }

	// Token: 0x170000D9 RID: 217
	// (get) Token: 0x060006D1 RID: 1745 RVA: 0x0002E749 File Offset: 0x0002C949
	public virtual bool visible
	{
		get
		{
			return this.isOnEnabled;
		}
	}

	// Token: 0x060006D2 RID: 1746 RVA: 0x0002E751 File Offset: 0x0002C951
	protected void OnEnable()
	{
		this.isOnEnabled = true;
		if (this.visible)
		{
			this.Show();
		}
	}

	// Token: 0x060006D3 RID: 1747 RVA: 0x0002E768 File Offset: 0x0002C968
	protected void OnDisable()
	{
		this.isOnEnabled = false;
		this.Hide();
	}

	// Token: 0x060006D4 RID: 1748 RVA: 0x0002E777 File Offset: 0x0002C977
	protected void OnDestroy()
	{
		if (this.clip_ != null)
		{
			this.clip_.Remove(this);
		}
	}

	// Token: 0x060006D5 RID: 1749 RVA: 0x0002E793 File Offset: 0x0002C993
	public virtual void SetClip(exClipping _clip = null)
	{
		if (this.clip_ == _clip)
		{
			return;
		}
		if (_clip != null)
		{
			_clip.Add(this);
			return;
		}
		if (this.clip_ != null)
		{
			this.clip_.Remove(this);
		}
	}

	// Token: 0x060006D6 RID: 1750
	internal abstract exUpdateFlags UpdateBuffers(exList<Vector3> _vertices, exList<Vector2> _uvs, exList<Color32> _colors32, exList<int> _indices = null);

	// Token: 0x060006D7 RID: 1751
	public abstract float GetScaleX(Space _space);

	// Token: 0x060006D8 RID: 1752
	public abstract float GetScaleY(Space _space);

	// Token: 0x060006D9 RID: 1753 RVA: 0x0002E7CA File Offset: 0x0002C9CA
	void exISpriteBase.UpdateMaterial()
	{
		this.UpdateMaterial();
	}

	// Token: 0x060006DA RID: 1754
	protected abstract void UpdateMaterial();

	// Token: 0x060006DB RID: 1755 RVA: 0x0002E7D2 File Offset: 0x0002C9D2
	protected virtual void Show()
	{
	}

	// Token: 0x060006DC RID: 1756 RVA: 0x0002E7D4 File Offset: 0x0002C9D4
	protected virtual void Hide()
	{
	}

	// Token: 0x060006DE RID: 1758 RVA: 0x0002E82B File Offset: 0x0002CA2B
	Transform IMonoBehaviour.get_transform()
	{
		return base.transform;
	}

	// Token: 0x060006DF RID: 1759 RVA: 0x0002E833 File Offset: 0x0002CA33
	GameObject IMonoBehaviour.get_gameObject()
	{
		return base.gameObject;
	}

	// Token: 0x040004E3 RID: 1251
	[SerializeField]
	protected bool customSize_;

	// Token: 0x040004E4 RID: 1252
	[SerializeField]
	protected Color color_ = new Color(1f, 1f, 1f, 1f);

	// Token: 0x040004E5 RID: 1253
	[SerializeField]
	protected Vector2 shear_ = Vector2.zero;

	// Token: 0x040004E6 RID: 1254
	[SerializeField]
	private Shader shader_;

	// Token: 0x040004E7 RID: 1255
	[NonSerialized]
	protected bool isOnEnabled;

	// Token: 0x040004E8 RID: 1256
	[NonSerialized]
	public exUpdateFlags updateFlags = exUpdateFlags.All;

	// Token: 0x040004E9 RID: 1257
	[NonSerialized]
	protected exClipping clip_;

	// Token: 0x040004EA RID: 1258
	[NonSerialized]
	internal Matrix4x4 cachedWorldMatrix;

	// Token: 0x040004EB RID: 1259
	[NonSerialized]
	protected int vertexCount_ = -1;

	// Token: 0x040004EC RID: 1260
	[NonSerialized]
	protected int indexCount_ = -1;

	// Token: 0x040004ED RID: 1261
	[NonSerialized]
	protected Material material_;
}
