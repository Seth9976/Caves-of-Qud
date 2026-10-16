using System;
using ex2D.Detail;
using UnityEngine;

// Token: 0x020000E8 RID: 232
[AddComponentMenu("ex2D/3D Sprite2")]
public class ex3DSprite2 : exStandaloneSprite, exISprite, exISpriteBase, IMonoBehaviour
{
	// Token: 0x17000118 RID: 280
	// (set) Token: 0x060008A8 RID: 2216 RVA: 0x0003867C File Offset: 0x0003687C
	public float scale
	{
		set
		{
			base.gameObject.transform.localScale = new Vector3(value, value, value);
		}
	}

	// Token: 0x17000119 RID: 281
	// (get) Token: 0x060008A9 RID: 2217 RVA: 0x00038696 File Offset: 0x00036896
	// (set) Token: 0x060008AA RID: 2218 RVA: 0x000386A0 File Offset: 0x000368A0
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

	// Token: 0x1700011A RID: 282
	// (get) Token: 0x060008AB RID: 2219 RVA: 0x000386FB File Offset: 0x000368FB
	// (set) Token: 0x060008AC RID: 2220 RVA: 0x00038703 File Offset: 0x00036903
	public Color backcolor
	{
		get
		{
			return this._backcolor;
		}
		set
		{
			if (this._backcolor != value)
			{
				this.updateFlags |= exUpdateFlags.Color;
				this._backcolor = value;
			}
		}
	}

	// Token: 0x1700011B RID: 283
	// (get) Token: 0x060008AD RID: 2221 RVA: 0x00038728 File Offset: 0x00036928
	// (set) Token: 0x060008AE RID: 2222 RVA: 0x00038730 File Offset: 0x00036930
	public Color detailcolor
	{
		get
		{
			return this._detailcolor;
		}
		set
		{
			if (this._detailcolor != value)
			{
				this.updateFlags |= exUpdateFlags.Color;
				this._detailcolor = value;
			}
		}
	}

	// Token: 0x1700011C RID: 284
	// (get) Token: 0x060008AF RID: 2223 RVA: 0x00038755 File Offset: 0x00036955
	// (set) Token: 0x060008B0 RID: 2224 RVA: 0x0003875D File Offset: 0x0003695D
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

	// Token: 0x1700011D RID: 285
	// (get) Token: 0x060008B1 RID: 2225 RVA: 0x0003877D File Offset: 0x0003697D
	// (set) Token: 0x060008B2 RID: 2226 RVA: 0x00038788 File Offset: 0x00036988
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

	// Token: 0x1700011E RID: 286
	// (get) Token: 0x060008B3 RID: 2227 RVA: 0x000387FD File Offset: 0x000369FD
	// (set) Token: 0x060008B4 RID: 2228 RVA: 0x00038805 File Offset: 0x00036A05
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

	// Token: 0x1700011F RID: 287
	// (get) Token: 0x060008B5 RID: 2229 RVA: 0x00038830 File Offset: 0x00036A30
	// (set) Token: 0x060008B6 RID: 2230 RVA: 0x00038838 File Offset: 0x00036A38
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

	// Token: 0x17000120 RID: 288
	// (get) Token: 0x060008B7 RID: 2231 RVA: 0x00038868 File Offset: 0x00036A68
	// (set) Token: 0x060008B8 RID: 2232 RVA: 0x00038870 File Offset: 0x00036A70
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

	// Token: 0x17000121 RID: 289
	// (get) Token: 0x060008B9 RID: 2233 RVA: 0x00038899 File Offset: 0x00036A99
	// (set) Token: 0x060008BA RID: 2234 RVA: 0x000388A1 File Offset: 0x00036AA1
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

	// Token: 0x17000122 RID: 290
	// (get) Token: 0x060008BB RID: 2235 RVA: 0x000388CA File Offset: 0x00036ACA
	// (set) Token: 0x060008BC RID: 2236 RVA: 0x000388D2 File Offset: 0x00036AD2
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

	// Token: 0x17000123 RID: 291
	// (get) Token: 0x060008BD RID: 2237 RVA: 0x000388FB File Offset: 0x00036AFB
	// (set) Token: 0x060008BE RID: 2238 RVA: 0x00038903 File Offset: 0x00036B03
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

	// Token: 0x17000124 RID: 292
	// (get) Token: 0x060008BF RID: 2239 RVA: 0x0003892C File Offset: 0x00036B2C
	// (set) Token: 0x060008C0 RID: 2240 RVA: 0x00038934 File Offset: 0x00036B34
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

	// Token: 0x17000125 RID: 293
	// (get) Token: 0x060008C1 RID: 2241 RVA: 0x0003895D File Offset: 0x00036B5D
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

	// Token: 0x17000126 RID: 294
	// (get) Token: 0x060008C2 RID: 2242 RVA: 0x0003897A File Offset: 0x00036B7A
	// (set) Token: 0x060008C3 RID: 2243 RVA: 0x00038990 File Offset: 0x00036B90
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

	// Token: 0x17000127 RID: 295
	// (get) Token: 0x060008C4 RID: 2244 RVA: 0x00038A2E File Offset: 0x00036C2E
	// (set) Token: 0x060008C5 RID: 2245 RVA: 0x00038A5C File Offset: 0x00036C5C
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

	// Token: 0x17000128 RID: 296
	// (get) Token: 0x060008C6 RID: 2246 RVA: 0x00038A82 File Offset: 0x00036C82
	// (set) Token: 0x060008C7 RID: 2247 RVA: 0x00038AB0 File Offset: 0x00036CB0
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

	// Token: 0x17000129 RID: 297
	// (get) Token: 0x060008C8 RID: 2248 RVA: 0x00038AD6 File Offset: 0x00036CD6
	public override bool visible
	{
		get
		{
			return this.isOnEnabled && this.textureInfo_ != null;
		}
	}

	// Token: 0x060008C9 RID: 2249 RVA: 0x00038AF0 File Offset: 0x00036CF0
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
		Vector4 vector = new Vector4(this.backcolor.r, this.backcolor.g, this.backcolor.b, this.backcolor.a);
		this.tangents.Clear();
		for (int j = 0; j < this.vertexCount_; j++)
		{
			this.tangents.Add(vector);
		}
		Vector3 vector2 = new Vector4(this.detailcolor.r, this.detailcolor.g, this.detailcolor.b);
		this.normals.Clear();
		for (int k = 0; k < this.vertexCount_; k++)
		{
			this.normals.Add(vector2);
		}
		exUpdateFlags updateFlags = this.updateFlags;
		this.updateFlags = exUpdateFlags.None;
		return updateFlags;
	}

	// Token: 0x060008CA RID: 2250 RVA: 0x00038CB7 File Offset: 0x00036EB7
	private void TiledUpdateBuffers(exList<Vector3> _vertices, exList<Vector2> _uvs, exList<int> _indices)
	{
	}

	// Token: 0x060008CB RID: 2251 RVA: 0x00038CBC File Offset: 0x00036EBC
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

	// Token: 0x060008CC RID: 2252 RVA: 0x00038D97 File Offset: 0x00036F97
	protected override void UpdateVertexAndIndexCount()
	{
		this.GetVertexAndIndexCount(out this.vertexCount_, out this.indexCount_);
	}

	// Token: 0x060008CD RID: 2253 RVA: 0x00038DAB File Offset: 0x00036FAB
	void exISprite.UpdateBufferSize()
	{
		base.UpdateBufferSize();
	}

	// Token: 0x060008CF RID: 2255 RVA: 0x00038E1A File Offset: 0x0003701A
	Transform IMonoBehaviour.get_transform()
	{
		return base.transform;
	}

	// Token: 0x060008D0 RID: 2256 RVA: 0x00038E22 File Offset: 0x00037022
	GameObject IMonoBehaviour.get_gameObject()
	{
		return base.gameObject;
	}

	// Token: 0x040005F0 RID: 1520
	[SerializeField]
	private exTextureInfo textureInfo_;

	// Token: 0x040005F1 RID: 1521
	[SerializeField]
	protected bool useTextureOffset_;

	// Token: 0x040005F2 RID: 1522
	public Color _backcolor = new Color(0f, 0f, 0f, 1f);

	// Token: 0x040005F3 RID: 1523
	public Color _detailcolor = new Color(0f, 0f, 0f, 1f);

	// Token: 0x040005F4 RID: 1524
	[SerializeField]
	protected exSpriteType spriteType_;

	// Token: 0x040005F5 RID: 1525
	[SerializeField]
	protected Vector2 tiledSpacing_ = new Vector2(0f, 0f);

	// Token: 0x040005F6 RID: 1526
	[SerializeField]
	protected bool borderOnly_;

	// Token: 0x040005F7 RID: 1527
	[SerializeField]
	protected bool customBorderSize_;

	// Token: 0x040005F8 RID: 1528
	[SerializeField]
	protected float leftBorderSize_;

	// Token: 0x040005F9 RID: 1529
	[SerializeField]
	protected float rightBorderSize_;

	// Token: 0x040005FA RID: 1530
	[SerializeField]
	protected float topBorderSize_;

	// Token: 0x040005FB RID: 1531
	[SerializeField]
	protected float bottomBorderSize_;
}
