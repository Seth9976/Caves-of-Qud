using System;
using ex2D.Detail;
using UnityEngine;

// Token: 0x020000BA RID: 186
[AddComponentMenu("ex2D/3D Sprite")]
public class ex3DSprite : exStandaloneSprite, exISprite, exISpriteBase, IMonoBehaviour
{
	// Token: 0x170000E0 RID: 224
	// (get) Token: 0x0600070A RID: 1802 RVA: 0x0002F1BA File Offset: 0x0002D3BA
	// (set) Token: 0x0600070B RID: 1803 RVA: 0x0002F1C4 File Offset: 0x0002D3C4
	public exTextureInfo textureInfo
	{
		get
		{
			return this.textureInfo_;
		}
		set
		{
			if (value != null)
			{
				if (this.isOnEnabled)
				{
					this.Show();
				}
			}
			else if (this.isOnEnabled && this.textureInfo_ != null)
			{
				this.Hide();
			}
			exSpriteUtility.SetTextureInfo(this, ref this.textureInfo_, value, this.useTextureOffset_, this.spriteType_);
		}
	}

	// Token: 0x170000E1 RID: 225
	// (get) Token: 0x0600070C RID: 1804 RVA: 0x0002F21F File Offset: 0x0002D41F
	// (set) Token: 0x0600070D RID: 1805 RVA: 0x0002F227 File Offset: 0x0002D427
	public bool useTextureOffset
	{
		get
		{
			return this.useTextureOffset_;
		}
		set
		{
			if (this.useTextureOffset_ != value)
			{
				this.useTextureOffset_ = value;
				this.updateFlags |= exUpdateFlags.Vertex;
			}
		}
	}

	// Token: 0x170000E2 RID: 226
	// (get) Token: 0x0600070E RID: 1806 RVA: 0x0002F247 File Offset: 0x0002D447
	// (set) Token: 0x0600070F RID: 1807 RVA: 0x0002F250 File Offset: 0x0002D450
	public exSpriteType spriteType
	{
		get
		{
			return this.spriteType_;
		}
		set
		{
			if (this.spriteType_ != value)
			{
				if (value == exSpriteType.Tiled)
				{
					this.customSize_ = true;
				}
				else if (value == exSpriteType.Diced && this.textureInfo_ != null && this.textureInfo_.diceUnitWidth == 0 && this.textureInfo_.diceUnitHeight == 0)
				{
					Debug.LogWarning("The texture info is not diced!");
				}
				this.spriteType_ = value;
				base.UpdateBufferSize();
				this.updateFlags |= exUpdateFlags.All;
			}
		}
	}

	// Token: 0x170000E3 RID: 227
	// (get) Token: 0x06000710 RID: 1808 RVA: 0x0002F2C5 File Offset: 0x0002D4C5
	// (set) Token: 0x06000711 RID: 1809 RVA: 0x0002F2CD File Offset: 0x0002D4CD
	public Vector2 tiledSpacing
	{
		get
		{
			return this.tiledSpacing_;
		}
		set
		{
			if (this.tiledSpacing_ != value)
			{
				this.tiledSpacing_ = value;
				base.UpdateBufferSize();
				this.updateFlags |= exUpdateFlags.Vertex | exUpdateFlags.UV;
			}
		}
	}

	// Token: 0x170000E4 RID: 228
	// (get) Token: 0x06000712 RID: 1810 RVA: 0x0002F2F8 File Offset: 0x0002D4F8
	// (set) Token: 0x06000713 RID: 1811 RVA: 0x0002F300 File Offset: 0x0002D500
	public bool borderOnly
	{
		get
		{
			return this.borderOnly_;
		}
		set
		{
			if (this.borderOnly_ != value)
			{
				this.borderOnly_ = value;
				if (this.spriteType_ == exSpriteType.Sliced)
				{
					base.UpdateBufferSize();
					this.updateFlags |= exUpdateFlags.All;
				}
			}
		}
	}

	// Token: 0x170000E5 RID: 229
	// (get) Token: 0x06000714 RID: 1812 RVA: 0x0002F330 File Offset: 0x0002D530
	// (set) Token: 0x06000715 RID: 1813 RVA: 0x0002F338 File Offset: 0x0002D538
	public bool customBorderSize
	{
		get
		{
			return this.customBorderSize_;
		}
		set
		{
			if (this.customBorderSize_ != value)
			{
				this.customBorderSize_ = value;
				if (this.spriteType_ == exSpriteType.Sliced)
				{
					this.updateFlags |= exUpdateFlags.Vertex;
				}
			}
		}
	}

	// Token: 0x170000E6 RID: 230
	// (get) Token: 0x06000716 RID: 1814 RVA: 0x0002F361 File Offset: 0x0002D561
	// (set) Token: 0x06000717 RID: 1815 RVA: 0x0002F369 File Offset: 0x0002D569
	public float leftBorderSize
	{
		get
		{
			return this.leftBorderSize_;
		}
		set
		{
			if (this.leftBorderSize_ != value)
			{
				this.leftBorderSize_ = value;
				if (this.spriteType_ == exSpriteType.Sliced)
				{
					this.updateFlags |= exUpdateFlags.Vertex;
				}
			}
		}
	}

	// Token: 0x170000E7 RID: 231
	// (get) Token: 0x06000718 RID: 1816 RVA: 0x0002F392 File Offset: 0x0002D592
	// (set) Token: 0x06000719 RID: 1817 RVA: 0x0002F39A File Offset: 0x0002D59A
	public float rightBorderSize
	{
		get
		{
			return this.rightBorderSize_;
		}
		set
		{
			if (this.rightBorderSize_ != value)
			{
				this.rightBorderSize_ = value;
				if (this.spriteType_ == exSpriteType.Sliced)
				{
					this.updateFlags |= exUpdateFlags.Vertex;
				}
			}
		}
	}

	// Token: 0x170000E8 RID: 232
	// (get) Token: 0x0600071A RID: 1818 RVA: 0x0002F3C3 File Offset: 0x0002D5C3
	// (set) Token: 0x0600071B RID: 1819 RVA: 0x0002F3CB File Offset: 0x0002D5CB
	public float topBorderSize
	{
		get
		{
			return this.topBorderSize_;
		}
		set
		{
			if (this.topBorderSize_ != value)
			{
				this.topBorderSize_ = value;
				if (this.spriteType_ == exSpriteType.Sliced)
				{
					this.updateFlags |= exUpdateFlags.Vertex;
				}
			}
		}
	}

	// Token: 0x170000E9 RID: 233
	// (get) Token: 0x0600071C RID: 1820 RVA: 0x0002F3F4 File Offset: 0x0002D5F4
	// (set) Token: 0x0600071D RID: 1821 RVA: 0x0002F3FC File Offset: 0x0002D5FC
	public float bottomBorderSize
	{
		get
		{
			return this.bottomBorderSize_;
		}
		set
		{
			if (this.bottomBorderSize_ != value)
			{
				this.bottomBorderSize_ = value;
				if (this.spriteType_ == exSpriteType.Sliced)
				{
					this.updateFlags |= exUpdateFlags.Vertex;
				}
			}
		}
	}

	// Token: 0x170000EA RID: 234
	// (get) Token: 0x0600071E RID: 1822 RVA: 0x0002F425 File Offset: 0x0002D625
	protected override Texture texture
	{
		get
		{
			if (this.textureInfo_ != null)
			{
				return this.textureInfo_.texture;
			}
			return null;
		}
	}

	// Token: 0x170000EB RID: 235
	// (get) Token: 0x0600071F RID: 1823 RVA: 0x0002F442 File Offset: 0x0002D642
	// (set) Token: 0x06000720 RID: 1824 RVA: 0x0002F458 File Offset: 0x0002D658
	public override bool customSize
	{
		get
		{
			return this.spriteType_ == exSpriteType.Tiled || this.customSize_;
		}
		set
		{
			if (this.spriteType_ == exSpriteType.Tiled)
			{
				this.customSize_ = true;
				return;
			}
			if (this.customSize_ != value)
			{
				this.customSize_ = value;
				if (!this.customSize_ && this.textureInfo_ != null && ((float)this.textureInfo_.width != this.width_ || (float)this.textureInfo_.height != this.height_))
				{
					this.width_ = (float)this.textureInfo_.width;
					this.height_ = (float)this.textureInfo_.height;
					this.updateFlags |= exUpdateFlags.Vertex;
				}
			}
		}
	}

	// Token: 0x170000EC RID: 236
	// (get) Token: 0x06000721 RID: 1825 RVA: 0x0002F4F6 File Offset: 0x0002D6F6
	// (set) Token: 0x06000722 RID: 1826 RVA: 0x0002F524 File Offset: 0x0002D724
	public override float width
	{
		get
		{
			if (!this.customSize_)
			{
				return (float)((this.textureInfo_ != null) ? this.textureInfo_.width : 0);
			}
			return this.width_;
		}
		set
		{
			base.width = value;
			if (this.spriteType_ == exSpriteType.Tiled)
			{
				base.UpdateBufferSize();
				this.updateFlags |= exUpdateFlags.UV;
			}
		}
	}

	// Token: 0x170000ED RID: 237
	// (get) Token: 0x06000723 RID: 1827 RVA: 0x0002F54A File Offset: 0x0002D74A
	// (set) Token: 0x06000724 RID: 1828 RVA: 0x0002F578 File Offset: 0x0002D778
	public override float height
	{
		get
		{
			if (!this.customSize_)
			{
				return (float)((this.textureInfo_ != null) ? this.textureInfo_.height : 0);
			}
			return this.height_;
		}
		set
		{
			base.height = value;
			if (this.spriteType_ == exSpriteType.Tiled)
			{
				base.UpdateBufferSize();
				this.updateFlags |= exUpdateFlags.UV;
			}
		}
	}

	// Token: 0x170000EE RID: 238
	// (get) Token: 0x06000725 RID: 1829 RVA: 0x0002F59E File Offset: 0x0002D79E
	public override bool visible
	{
		get
		{
			return this.isOnEnabled && this.textureInfo_ != null;
		}
	}

	// Token: 0x06000726 RID: 1830 RVA: 0x0002F5B8 File Offset: 0x0002D7B8
	internal override exUpdateFlags UpdateBuffers(exList<Vector3> _vertices, exList<Vector2> _uvs, exList<Color32> _colors32, exList<int> _indices)
	{
		switch (this.spriteType_)
		{
		case exSpriteType.Simple:
			SpriteBuilder.SimpleUpdateBuffers(this, this.textureInfo_, this.useTextureOffset_, Space.Self, _vertices, _uvs, _indices, 0, 0);
			break;
		case exSpriteType.Sliced:
			SpriteBuilder.SlicedUpdateBuffers(this, this.textureInfo_, this.useTextureOffset_, Space.Self, _vertices, _uvs, _indices, 0, 0);
			break;
		case exSpriteType.Tiled:
			SpriteBuilder.TiledUpdateBuffers(this, this.textureInfo_, this.useTextureOffset_, this.tiledSpacing_, Space.Self, _vertices, _uvs, _indices, 0, 0);
			break;
		case exSpriteType.Diced:
			SpriteBuilder.DicedUpdateBuffers(this, this.textureInfo_, this.useTextureOffset_, Space.Self, _vertices, _uvs, _indices, 0, 0);
			break;
		}
		if ((this.updateFlags & exUpdateFlags.Color) != exUpdateFlags.None && _colors32 != null)
		{
			Color32 color = new Color(this.color_.r, this.color_.g, this.color_.b, this.color_.a);
			for (int i = 0; i < this.vertexCount_; i++)
			{
				_colors32.buffer[i] = color;
			}
		}
		exUpdateFlags updateFlags = this.updateFlags;
		this.updateFlags = exUpdateFlags.None;
		return updateFlags;
	}

	// Token: 0x06000727 RID: 1831 RVA: 0x0002F6C3 File Offset: 0x0002D8C3
	private void TiledUpdateBuffers(exList<Vector3> _vertices, exList<Vector2> _uvs, exList<int> _indices)
	{
	}

	// Token: 0x06000728 RID: 1832 RVA: 0x0002F6C8 File Offset: 0x0002D8C8
	protected override Vector3[] GetVertices(Space _space)
	{
		if (this.textureInfo_ == null)
		{
			return new Vector3[0];
		}
		exList<Vector3> tempList = exList<Vector3>.GetTempList();
		base.UpdateBufferSize();
		tempList.AddRange(this.vertexCount_);
		switch (this.spriteType_)
		{
		case exSpriteType.Simple:
			SpriteBuilder.SimpleUpdateVertexBuffer(this, this.textureInfo_, this.useTextureOffset_, _space, tempList, 0);
			break;
		case exSpriteType.Sliced:
			SpriteBuilder.SimpleUpdateVertexBuffer(this, this.textureInfo_, this.useTextureOffset_, _space, tempList, 0);
			SpriteBuilder.SimpleVertexBufferToSliced(this, this.textureInfo_, tempList, 0);
			break;
		case exSpriteType.Tiled:
			SpriteBuilder.TiledUpdateVertexBuffer(this, this.textureInfo_, this.useTextureOffset_, this.tiledSpacing_, _space, tempList, 0);
			break;
		case exSpriteType.Diced:
			SpriteBuilder.SimpleUpdateVertexBuffer(this, this.textureInfo_, this.useTextureOffset_, _space, tempList, 0);
			SpriteBuilder.SimpleVertexBufferToDiced(this, this.textureInfo_, tempList, 0);
			break;
		}
		return tempList.ToArray();
	}

	// Token: 0x06000729 RID: 1833 RVA: 0x0002F7A3 File Offset: 0x0002D9A3
	protected override void UpdateVertexAndIndexCount()
	{
		this.GetVertexAndIndexCount(out this.vertexCount_, out this.indexCount_);
	}

	// Token: 0x0600072A RID: 1834 RVA: 0x0002F7B7 File Offset: 0x0002D9B7
	void exISprite.UpdateBufferSize()
	{
		base.UpdateBufferSize();
	}

	// Token: 0x0600072C RID: 1836 RVA: 0x0002F7DC File Offset: 0x0002D9DC
	Transform IMonoBehaviour.get_transform()
	{
		return base.transform;
	}

	// Token: 0x0600072D RID: 1837 RVA: 0x0002F7E4 File Offset: 0x0002D9E4
	GameObject IMonoBehaviour.get_gameObject()
	{
		return base.gameObject;
	}

	// Token: 0x040004FC RID: 1276
	[SerializeField]
	private exTextureInfo textureInfo_;

	// Token: 0x040004FD RID: 1277
	[SerializeField]
	protected bool useTextureOffset_;

	// Token: 0x040004FE RID: 1278
	[SerializeField]
	protected exSpriteType spriteType_;

	// Token: 0x040004FF RID: 1279
	[SerializeField]
	protected Vector2 tiledSpacing_ = new Vector2(0f, 0f);

	// Token: 0x04000500 RID: 1280
	[SerializeField]
	protected bool borderOnly_;

	// Token: 0x04000501 RID: 1281
	[SerializeField]
	protected bool customBorderSize_;

	// Token: 0x04000502 RID: 1282
	[SerializeField]
	protected float leftBorderSize_;

	// Token: 0x04000503 RID: 1283
	[SerializeField]
	protected float rightBorderSize_;

	// Token: 0x04000504 RID: 1284
	[SerializeField]
	protected float topBorderSize_;

	// Token: 0x04000505 RID: 1285
	[SerializeField]
	protected float bottomBorderSize_;
}
