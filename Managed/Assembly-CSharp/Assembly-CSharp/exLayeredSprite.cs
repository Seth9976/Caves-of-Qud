using System;
using UnityEngine;

// Token: 0x020000B2 RID: 178
public abstract class exLayeredSprite : exSpriteBase, IComparable<exLayeredSprite>, exLayer.IFriendOfLayer
{
	// Token: 0x170000BC RID: 188
	// (get) Token: 0x06000670 RID: 1648 RVA: 0x0002D097 File Offset: 0x0002B297
	// (set) Token: 0x06000671 RID: 1649 RVA: 0x0002D09F File Offset: 0x0002B29F
	public float depth
	{
		get
		{
			return this.depth_;
		}
		set
		{
			if (this.depth_ != value)
			{
				this.depth_ = value;
				this.SetDepthDirty();
			}
		}
	}

	// Token: 0x170000BD RID: 189
	// (get) Token: 0x06000672 RID: 1650 RVA: 0x0002D0B7 File Offset: 0x0002B2B7
	// (set) Token: 0x06000673 RID: 1651 RVA: 0x0002D0BF File Offset: 0x0002B2BF
	public bool transparent
	{
		get
		{
			return this.transparent_;
		}
		set
		{
			if (this.transparent_ != value)
			{
				this.transparent_ = value;
				this.updateFlags |= exUpdateFlags.Transparent;
			}
		}
	}

	// Token: 0x170000BE RID: 190
	// (get) Token: 0x06000674 RID: 1652 RVA: 0x0002D0E0 File Offset: 0x0002B2E0
	public exLayer layer
	{
		get
		{
			return this.layer_;
		}
	}

	// Token: 0x170000BF RID: 191
	// (get) Token: 0x06000675 RID: 1653 RVA: 0x0002D0E8 File Offset: 0x0002B2E8
	// (set) Token: 0x06000676 RID: 1654 RVA: 0x0002D0F0 File Offset: 0x0002B2F0
	float exLayer.IFriendOfLayer.globalDepth
	{
		get
		{
			return this.globalDepth_;
		}
		set
		{
			this.globalDepth_ = value;
		}
	}

	// Token: 0x170000C0 RID: 192
	// (get) Token: 0x06000677 RID: 1655 RVA: 0x0002D0F9 File Offset: 0x0002B2F9
	// (set) Token: 0x06000678 RID: 1656 RVA: 0x0002D107 File Offset: 0x0002B307
	public bool isInIndexBuffer
	{
		get
		{
			return this.indexBufferIndex != -1;
		}
		set
		{
			if (!value)
			{
				this.indexBufferIndex = -1;
				return;
			}
			Debug.LogError("isInIndexBuffer can not set to true, use SetLayer instead.");
		}
	}

	// Token: 0x170000C1 RID: 193
	// (get) Token: 0x06000679 RID: 1657 RVA: 0x0002D11E File Offset: 0x0002B31E
	public Transform cachedTransform
	{
		get
		{
			if (this.cachedTransform_ == null)
			{
				this.cachedTransform_ = base.transform;
				this.cachedWorldMatrix = this.cachedTransform_.localToWorldMatrix;
			}
			return this.cachedTransform_;
		}
	}

	// Token: 0x0600067A RID: 1658 RVA: 0x0002D14B File Offset: 0x0002B34B
	protected override void Show()
	{
		if (this.layer_ != null)
		{
			if (exLayeredSprite.enableFastShowHide)
			{
				this.layer_.FastShowSprite(this);
				return;
			}
			this.layer_.ShowSprite(this);
		}
	}

	// Token: 0x0600067B RID: 1659 RVA: 0x0002D17B File Offset: 0x0002B37B
	protected override void Hide()
	{
		if (this.layer_ != null)
		{
			if (exLayeredSprite.enableFastShowHide)
			{
				this.layer_.FastHideSprite(this);
				return;
			}
			this.layer_.HideSprite(this);
		}
	}

	// Token: 0x0600067C RID: 1660 RVA: 0x0002D1AB File Offset: 0x0002B3AB
	protected new void OnDestroy()
	{
		base.OnDestroy();
		if (this.layer_ != null)
		{
			this.layer_.Remove(this, false);
		}
	}

	// Token: 0x0600067D RID: 1661 RVA: 0x0002D1CE File Offset: 0x0002B3CE
	protected override void UpdateMaterial()
	{
		if (this.layer_ != null)
		{
			this.layer_.RefreshSpriteMaterial(this);
			return;
		}
		this.material_ = null;
	}

	// Token: 0x0600067E RID: 1662 RVA: 0x0002D1F4 File Offset: 0x0002B3F4
	public override float GetScaleX(Space _space)
	{
		if (_space == Space.World)
		{
			return new Vector3(this.cachedWorldMatrix.m00, this.cachedWorldMatrix.m10, this.cachedWorldMatrix.m20).magnitude;
		}
		return this.cachedTransform.localScale.x;
	}

	// Token: 0x0600067F RID: 1663 RVA: 0x0002D244 File Offset: 0x0002B444
	public override float GetScaleY(Space _space)
	{
		if (_space == Space.World)
		{
			return new Vector3(this.cachedWorldMatrix.m01, this.cachedWorldMatrix.m11, this.cachedWorldMatrix.m21).magnitude;
		}
		return this.cachedTransform.localScale.y;
	}

	// Token: 0x06000680 RID: 1664 RVA: 0x0002D294 File Offset: 0x0002B494
	public override Vector3[] GetWorldVertices()
	{
		Vector3[] vertices = this.GetVertices(Space.World);
		if (this.layer_ != null)
		{
			int num = this.layer_.IndexOfMesh(this);
			if (num != -1)
			{
				Matrix4x4 localToWorldMatrix = this.layer_.meshList[num].transform.localToWorldMatrix;
				for (int i = 0; i < vertices.Length; i++)
				{
					vertices[i] = localToWorldMatrix.MultiplyPoint3x4(vertices[i]);
				}
				return vertices;
			}
		}
		return vertices;
	}

	// Token: 0x06000681 RID: 1665 RVA: 0x0002D30C File Offset: 0x0002B50C
	public override void SetClip(exClipping _clip = null)
	{
		if (_clip != null && this.layer_ != null && !_clip.transform.IsChildOf(this.layer_.transform))
		{
			Debug.LogError("Can not add to clip which not in current layer!");
			return;
		}
		base.SetClip(_clip);
	}

	// Token: 0x06000682 RID: 1666 RVA: 0x0002D35C File Offset: 0x0002B55C
	internal override exUpdateFlags UpdateBuffers(exList<Vector3> _vertices, exList<Vector2> _uvs, exList<Color32> _colors32, exList<int> _indices = null)
	{
		if ((this.updateFlags & exUpdateFlags.Transparent) != exUpdateFlags.None)
		{
			this.updateFlags &= ~exUpdateFlags.Transparent;
			if (this.transparent_)
			{
				Vector3 vector = _vertices.buffer[0];
				for (int i = 1; i < this.vertexCount_; i++)
				{
					_vertices.buffer[this.vertexBufferIndex + i] = vector;
				}
				this.updateFlags &= ~exUpdateFlags.Vertex;
			}
			else
			{
				this.updateFlags |= exUpdateFlags.Vertex;
			}
			return exUpdateFlags.Vertex | exUpdateFlags.Transparent;
		}
		if (this.transparent_ && (this.updateFlags & exUpdateFlags.Vertex) != exUpdateFlags.None)
		{
			this.updateFlags &= ~exUpdateFlags.Vertex;
		}
		return exUpdateFlags.None;
	}

	// Token: 0x06000683 RID: 1667 RVA: 0x0002D404 File Offset: 0x0002B604
	public static bool operator >(exLayeredSprite _lhs, exLayeredSprite _rhs)
	{
		bool flag = _lhs.layer_ == null || _lhs.layer_.ordered;
		return _lhs.globalDepth_ > _rhs.globalDepth_ || (flag && _lhs.globalDepth_ == _rhs.globalDepth_ && _lhs.spriteIdInLayer > _rhs.spriteIdInLayer);
	}

	// Token: 0x06000684 RID: 1668 RVA: 0x0002D45C File Offset: 0x0002B65C
	public static bool operator >=(exLayeredSprite _lhs, exLayeredSprite _rhs)
	{
		bool flag = _lhs.layer_ == null || _lhs.layer_.ordered;
		return _lhs.globalDepth_ > _rhs.globalDepth_ || (_lhs.globalDepth_ == _rhs.globalDepth_ && (!flag || _lhs.spriteIdInLayer >= _rhs.spriteIdInLayer));
	}

	// Token: 0x06000685 RID: 1669 RVA: 0x0002D4B8 File Offset: 0x0002B6B8
	public static bool operator <(exLayeredSprite _lhs, exLayeredSprite _rhs)
	{
		bool flag = _lhs.layer_ == null || _lhs.layer_.ordered;
		return _lhs.globalDepth_ < _rhs.globalDepth_ || (flag && _lhs.globalDepth_ == _rhs.globalDepth_ && _lhs.spriteIdInLayer < _rhs.spriteIdInLayer);
	}

	// Token: 0x06000686 RID: 1670 RVA: 0x0002D510 File Offset: 0x0002B710
	public static bool operator <=(exLayeredSprite _lhs, exLayeredSprite _rhs)
	{
		bool flag = _lhs.layer_ == null || _lhs.layer_.ordered;
		return _lhs.globalDepth_ < _rhs.globalDepth_ || (_lhs.globalDepth_ == _rhs.globalDepth_ && (!flag || _lhs.spriteIdInLayer <= _rhs.spriteIdInLayer));
	}

	// Token: 0x06000687 RID: 1671 RVA: 0x0002D56C File Offset: 0x0002B76C
	public int CompareTo(exLayeredSprite _other)
	{
		if (this.globalDepth_ < _other.globalDepth_)
		{
			return -1;
		}
		if (this.globalDepth_ > _other.globalDepth_)
		{
			return 1;
		}
		if (this.layer_ == null || this.layer_.ordered)
		{
			return this.spriteIdInLayer - _other.spriteIdInLayer;
		}
		return 0;
	}

	// Token: 0x06000688 RID: 1672 RVA: 0x0002D5C0 File Offset: 0x0002B7C0
	void exLayer.IFriendOfLayer.DoSetLayer(exLayer _layer)
	{
		if (this.layer_ == null && _layer != null)
		{
			this.OnPreAddToLayer();
		}
		this.layer_ = _layer;
	}

	// Token: 0x06000689 RID: 1673 RVA: 0x0002D5E6 File Offset: 0x0002B7E6
	void exLayer.IFriendOfLayer.DoSetBufferSize(int _vertexCount, int _indexCount)
	{
		this.vertexCount_ = _vertexCount;
		this.indexCount_ = _indexCount;
	}

	// Token: 0x0600068A RID: 1674 RVA: 0x0002D5F6 File Offset: 0x0002B7F6
	void exLayer.IFriendOfLayer.SetMaterialDirty()
	{
		this.material_ = null;
	}

	// Token: 0x0600068B RID: 1675 RVA: 0x0002D5FF File Offset: 0x0002B7FF
	void exLayer.IFriendOfLayer.ResetLayerProperties()
	{
		this.layer_ = null;
		this.isInIndexBuffer = false;
	}

	// Token: 0x0600068C RID: 1676 RVA: 0x0002D60F File Offset: 0x0002B80F
	public void SetLayer(exLayer _layer = null)
	{
		if (this.layer_ == _layer)
		{
			return;
		}
		if (_layer != null)
		{
			_layer.Add(this, true);
			return;
		}
		if (this.layer_ != null)
		{
			this.layer_.Remove(this, true);
		}
	}

	// Token: 0x0600068D RID: 1677 RVA: 0x0002D648 File Offset: 0x0002B848
	[ContextMenu("SetDepthDirty")]
	public void SetDepthDirty()
	{
		if (this.layer_ != null)
		{
			this.layer_.SetDepthDirty(base.gameObject);
		}
	}

	// Token: 0x0600068E RID: 1678 RVA: 0x0002D669 File Offset: 0x0002B869
	protected virtual void OnPreAddToLayer()
	{
	}

	// Token: 0x0600068F RID: 1679 RVA: 0x0002D66C File Offset: 0x0002B86C
	internal virtual void FillBuffers(exList<Vector3> _vertices, exList<Vector2> _uvs, exList<Color32> _colors32)
	{
		this.vertexBufferIndex = _vertices.Count;
		_vertices.AddRange(this.vertexCount_);
		if (_colors32 != null)
		{
			_colors32.AddRange(this.vertexCount_);
		}
		_uvs.AddRange(this.vertexCount_);
		this.updateFlags |= exUpdateFlags.AllExcludeIndex;
	}

	// Token: 0x06000690 RID: 1680 RVA: 0x0002D6BB File Offset: 0x0002B8BB
	public void UpdateTransform()
	{
		if (this.cachedTransform.hasChanged)
		{
			this.cachedTransform_.hasChanged = false;
			this.cachedWorldMatrix = this.cachedTransform_.localToWorldMatrix;
			this.updateFlags |= exUpdateFlags.Vertex;
		}
	}

	// Token: 0x040004AB RID: 1195
	public static bool enableFastShowHide = true;

	// Token: 0x040004AC RID: 1196
	[SerializeField]
	protected float depth_;

	// Token: 0x040004AD RID: 1197
	[HideInInspector]
	public int spriteIdInLayer = -1;

	// Token: 0x040004AE RID: 1198
	[NonSerialized]
	internal int spriteIndexInMesh = -1;

	// Token: 0x040004AF RID: 1199
	[NonSerialized]
	internal int vertexBufferIndex = -1;

	// Token: 0x040004B0 RID: 1200
	[NonSerialized]
	internal int indexBufferIndex = -1;

	// Token: 0x040004B1 RID: 1201
	[NonSerialized]
	protected bool transparent_ = true;

	// Token: 0x040004B2 RID: 1202
	[NonSerialized]
	protected exLayer layer_;

	// Token: 0x040004B3 RID: 1203
	[NonSerialized]
	private float globalDepth_;

	// Token: 0x040004B4 RID: 1204
	[NonSerialized]
	protected Transform cachedTransform_;
}
