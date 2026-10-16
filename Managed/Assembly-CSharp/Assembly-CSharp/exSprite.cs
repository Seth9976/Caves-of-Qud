using System;
using ex2D.Detail;
using UnityEngine;

// Token: 0x020000BF RID: 191
[AddComponentMenu("ex2D/2D Sprite")]
public class exSprite : exLayeredSprite, exISprite, exISpriteBase, IMonoBehaviour
{
	// Token: 0x170000F9 RID: 249
	// (get) Token: 0x0600077B RID: 1915 RVA: 0x0003143D File Offset: 0x0002F63D
	// (set) Token: 0x0600077C RID: 1916 RVA: 0x00031448 File Offset: 0x0002F648
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

	// Token: 0x170000FA RID: 250
	// (get) Token: 0x0600077D RID: 1917 RVA: 0x000314A3 File Offset: 0x0002F6A3
	// (set) Token: 0x0600077E RID: 1918 RVA: 0x000314AB File Offset: 0x0002F6AB
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

	// Token: 0x170000FB RID: 251
	// (get) Token: 0x0600077F RID: 1919 RVA: 0x000314CB File Offset: 0x0002F6CB
	// (set) Token: 0x06000780 RID: 1920 RVA: 0x000314D4 File Offset: 0x0002F6D4
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
				if (this.layer_ != null)
				{
					((exISprite)this).UpdateBufferSize();
					this.updateFlags |= exUpdateFlags.All;
				}
			}
		}
	}

	// Token: 0x170000FC RID: 252
	// (get) Token: 0x06000781 RID: 1921 RVA: 0x00031557 File Offset: 0x0002F757
	// (set) Token: 0x06000782 RID: 1922 RVA: 0x0003155F File Offset: 0x0002F75F
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
				if (this.layer_ != null)
				{
					((exISprite)this).UpdateBufferSize();
					this.updateFlags |= exUpdateFlags.Vertex | exUpdateFlags.UV;
				}
			}
		}
	}

	// Token: 0x170000FD RID: 253
	// (get) Token: 0x06000783 RID: 1923 RVA: 0x00031598 File Offset: 0x0002F798
	// (set) Token: 0x06000784 RID: 1924 RVA: 0x000315A0 File Offset: 0x0002F7A0
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
				if (this.spriteType_ == exSpriteType.Sliced && this.layer_ != null)
				{
					((exISprite)this).UpdateBufferSize();
					this.updateFlags |= exUpdateFlags.All;
				}
			}
		}
	}

	// Token: 0x170000FE RID: 254
	// (get) Token: 0x06000785 RID: 1925 RVA: 0x000315DE File Offset: 0x0002F7DE
	// (set) Token: 0x06000786 RID: 1926 RVA: 0x000315E6 File Offset: 0x0002F7E6
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
				if (this.spriteType_ == exSpriteType.Sliced && this.layer_ != null)
				{
					this.updateFlags |= exUpdateFlags.Vertex;
				}
			}
		}
	}

	// Token: 0x170000FF RID: 255
	// (get) Token: 0x06000787 RID: 1927 RVA: 0x0003161D File Offset: 0x0002F81D
	// (set) Token: 0x06000788 RID: 1928 RVA: 0x0003163A File Offset: 0x0002F83A
	public float leftBorderSize
	{
		get
		{
			if (!this.customBorderSize)
			{
				return (float)this.textureInfo_.borderLeft;
			}
			return this.leftBorderSize_;
		}
		set
		{
			if (this.leftBorderSize_ != value)
			{
				this.leftBorderSize_ = value;
				if (this.spriteType_ == exSpriteType.Sliced && this.layer_ != null)
				{
					this.updateFlags |= exUpdateFlags.Vertex;
				}
			}
		}
	}

	// Token: 0x17000100 RID: 256
	// (get) Token: 0x06000789 RID: 1929 RVA: 0x00031671 File Offset: 0x0002F871
	// (set) Token: 0x0600078A RID: 1930 RVA: 0x0003168E File Offset: 0x0002F88E
	public float rightBorderSize
	{
		get
		{
			if (!this.customBorderSize)
			{
				return (float)this.textureInfo_.borderRight;
			}
			return this.rightBorderSize_;
		}
		set
		{
			if (this.rightBorderSize_ != value)
			{
				this.rightBorderSize_ = value;
				if (this.spriteType_ == exSpriteType.Sliced && this.layer_ != null)
				{
					this.updateFlags |= exUpdateFlags.Vertex;
				}
			}
		}
	}

	// Token: 0x17000101 RID: 257
	// (get) Token: 0x0600078B RID: 1931 RVA: 0x000316C5 File Offset: 0x0002F8C5
	// (set) Token: 0x0600078C RID: 1932 RVA: 0x000316E2 File Offset: 0x0002F8E2
	public float topBorderSize
	{
		get
		{
			if (!this.customBorderSize)
			{
				return (float)this.textureInfo_.borderTop;
			}
			return this.topBorderSize_;
		}
		set
		{
			if (this.topBorderSize_ != value)
			{
				this.topBorderSize_ = value;
				if (this.spriteType_ == exSpriteType.Sliced && this.layer_ != null)
				{
					this.updateFlags |= exUpdateFlags.Vertex;
				}
			}
		}
	}

	// Token: 0x17000102 RID: 258
	// (get) Token: 0x0600078D RID: 1933 RVA: 0x00031719 File Offset: 0x0002F919
	// (set) Token: 0x0600078E RID: 1934 RVA: 0x00031736 File Offset: 0x0002F936
	public float bottomBorderSize
	{
		get
		{
			if (!this.customBorderSize)
			{
				return (float)this.textureInfo_.borderBottom;
			}
			return this.bottomBorderSize_;
		}
		set
		{
			if (this.bottomBorderSize_ != value)
			{
				this.bottomBorderSize_ = value;
				if (this.spriteType_ == exSpriteType.Sliced && this.layer_ != null)
				{
					this.updateFlags |= exUpdateFlags.Vertex;
				}
			}
		}
	}

	// Token: 0x17000103 RID: 259
	// (get) Token: 0x0600078F RID: 1935 RVA: 0x0003176D File Offset: 0x0002F96D
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

	// Token: 0x17000104 RID: 260
	// (get) Token: 0x06000790 RID: 1936 RVA: 0x0003178A File Offset: 0x0002F98A
	// (set) Token: 0x06000791 RID: 1937 RVA: 0x000317A0 File Offset: 0x0002F9A0
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
				if ((this.textureInfo_ != null && (float)this.textureInfo_.width != this.width_) || (float)this.textureInfo_.height != this.height_)
				{
					if (!this.customSize_)
					{
						this.width_ = (float)this.textureInfo_.width;
						this.height_ = (float)this.textureInfo_.height;
					}
					this.updateFlags |= exUpdateFlags.Vertex;
				}
			}
		}
	}

	// Token: 0x17000105 RID: 261
	// (get) Token: 0x06000792 RID: 1938 RVA: 0x0003183E File Offset: 0x0002FA3E
	// (set) Token: 0x06000793 RID: 1939 RVA: 0x0003186C File Offset: 0x0002FA6C
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
			if (this.spriteType_ == exSpriteType.Tiled && this.layer_ != null)
			{
				((exISprite)this).UpdateBufferSize();
				this.updateFlags |= exUpdateFlags.UV;
			}
		}
	}

	// Token: 0x17000106 RID: 262
	// (get) Token: 0x06000794 RID: 1940 RVA: 0x000318A0 File Offset: 0x0002FAA0
	// (set) Token: 0x06000795 RID: 1941 RVA: 0x000318CE File Offset: 0x0002FACE
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
			if (this.spriteType_ == exSpriteType.Tiled && this.layer_ != null)
			{
				((exISprite)this).UpdateBufferSize();
				this.updateFlags |= exUpdateFlags.UV;
			}
		}
	}

	// Token: 0x17000107 RID: 263
	// (get) Token: 0x06000796 RID: 1942 RVA: 0x00031902 File Offset: 0x0002FB02
	public override bool visible
	{
		get
		{
			return this.isOnEnabled && this.textureInfo_ != null;
		}
	}

	// Token: 0x06000797 RID: 1943 RVA: 0x0003191C File Offset: 0x0002FB1C
	internal override exUpdateFlags UpdateBuffers(exList<Vector3> _vertices, exList<Vector2> _uvs, exList<Color32> _colors32, exList<int> _indices)
	{
		exUpdateFlags exUpdateFlags = base.UpdateBuffers(_vertices, _uvs, _colors32, _indices);
		if (this.textureInfo_ != null)
		{
			switch (this.spriteType_)
			{
			case exSpriteType.Simple:
				SpriteBuilder.SimpleUpdateBuffers(this, this.textureInfo_, this.useTextureOffset_, Space.World, _vertices, _uvs, _indices, this.vertexBufferIndex, this.indexBufferIndex);
				break;
			case exSpriteType.Sliced:
				SpriteBuilder.SlicedUpdateBuffers(this, this.textureInfo_, this.useTextureOffset_, Space.World, _vertices, _uvs, _indices, this.vertexBufferIndex, this.indexBufferIndex);
				break;
			case exSpriteType.Tiled:
				SpriteBuilder.TiledUpdateBuffers(this, this.textureInfo_, this.useTextureOffset_, this.tiledSpacing_, Space.World, _vertices, _uvs, _indices, this.vertexBufferIndex, this.indexBufferIndex);
				break;
			case exSpriteType.Diced:
				SpriteBuilder.DicedUpdateBuffers(this, this.textureInfo_, this.useTextureOffset_, Space.World, _vertices, _uvs, _indices, this.vertexBufferIndex, this.indexBufferIndex);
				break;
			}
			if ((this.updateFlags & exUpdateFlags.Color) != exUpdateFlags.None && _colors32 != null)
			{
				Color32 color = new Color(this.color_.r, this.color_.g, this.color_.b, this.color_.a * this.layer_.alpha);
				for (int i = 0; i < this.vertexCount_; i++)
				{
					_colors32.buffer[this.vertexBufferIndex + i] = color;
				}
			}
		}
		else if (this.updateFlags != exUpdateFlags.None)
		{
			if (_indices != null)
			{
				_vertices.buffer[this.vertexBufferIndex] = base.cachedTransform.position;
				for (int j = this.indexBufferIndex; j < this.indexBufferIndex + this.indexCount_; j++)
				{
					_indices.buffer[j] = this.vertexBufferIndex;
				}
				exUpdateFlags |= exUpdateFlags.VertexAndIndex;
			}
			else
			{
				Vector3 position = base.cachedTransform.position;
				for (int k = this.vertexBufferIndex; k < this.vertexBufferIndex + this.vertexCount_; k++)
				{
					_vertices.buffer[k] = position;
				}
				exUpdateFlags |= exUpdateFlags.Vertex;
			}
		}
		exUpdateFlags |= this.updateFlags;
		this.updateFlags = exUpdateFlags.None;
		return exUpdateFlags;
	}

	// Token: 0x06000798 RID: 1944 RVA: 0x00031B34 File Offset: 0x0002FD34
	protected override Vector3[] GetVertices(Space _space)
	{
		if (this.textureInfo_ == null || this.layer_ == null)
		{
			return new Vector3[0];
		}
		exList<Vector3> tempList = exList<Vector3>.GetTempList();
		((exISprite)this).UpdateBufferSize();
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

	// Token: 0x06000799 RID: 1945 RVA: 0x00031C1D File Offset: 0x0002FE1D
	protected override void OnPreAddToLayer()
	{
		this.GetVertexAndIndexCount(out this.vertexCount_, out this.indexCount_);
	}

	// Token: 0x0600079A RID: 1946 RVA: 0x00031C34 File Offset: 0x0002FE34
	void exISprite.UpdateBufferSize()
	{
		int num;
		int num2;
		this.GetVertexAndIndexCount(out num, out num2);
		if (this.vertexCount_ != num || this.indexCount_ != num2)
		{
			if (this.layer_ != null)
			{
				this.layer_.SetSpriteBufferSize(this, num, num2);
				return;
			}
			this.vertexCount_ = num;
			this.indexCount_ = num2;
		}
	}

	// Token: 0x0600079C RID: 1948 RVA: 0x00031CA5 File Offset: 0x0002FEA5
	Transform IMonoBehaviour.get_transform()
	{
		return base.transform;
	}

	// Token: 0x0600079D RID: 1949 RVA: 0x00031CAD File Offset: 0x0002FEAD
	GameObject IMonoBehaviour.get_gameObject()
	{
		return base.gameObject;
	}

	// Token: 0x0400051F RID: 1311
	[SerializeField]
	private exTextureInfo textureInfo_;

	// Token: 0x04000520 RID: 1312
	[SerializeField]
	protected bool useTextureOffset_;

	// Token: 0x04000521 RID: 1313
	[SerializeField]
	protected exSpriteType spriteType_;

	// Token: 0x04000522 RID: 1314
	[SerializeField]
	protected Vector2 tiledSpacing_ = new Vector2(0f, 0f);

	// Token: 0x04000523 RID: 1315
	[SerializeField]
	protected bool borderOnly_;

	// Token: 0x04000524 RID: 1316
	[SerializeField]
	protected bool customBorderSize_;

	// Token: 0x04000525 RID: 1317
	[SerializeField]
	protected float leftBorderSize_;

	// Token: 0x04000526 RID: 1318
	[SerializeField]
	protected float rightBorderSize_;

	// Token: 0x04000527 RID: 1319
	[SerializeField]
	protected float topBorderSize_;

	// Token: 0x04000528 RID: 1320
	[SerializeField]
	protected float bottomBorderSize_;
}
