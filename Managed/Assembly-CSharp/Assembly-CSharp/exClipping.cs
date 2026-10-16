using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000BB RID: 187
[AddComponentMenu("ex2D/Clipping")]
public class exClipping : exPlane
{
	// Token: 0x170000EF RID: 239
	// (get) Token: 0x0600072E RID: 1838 RVA: 0x0002F7EC File Offset: 0x0002D9EC
	// (set) Token: 0x0600072F RID: 1839 RVA: 0x0002F7F4 File Offset: 0x0002D9F4
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
				this.dirty = true;
			}
		}
	}

	// Token: 0x170000F0 RID: 240
	// (get) Token: 0x06000730 RID: 1840 RVA: 0x0002F80D File Offset: 0x0002DA0D
	// (set) Token: 0x06000731 RID: 1841 RVA: 0x0002F815 File Offset: 0x0002DA15
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
				this.dirty = true;
			}
		}
	}

	// Token: 0x170000F1 RID: 241
	// (get) Token: 0x06000732 RID: 1842 RVA: 0x0002F82E File Offset: 0x0002DA2E
	// (set) Token: 0x06000733 RID: 1843 RVA: 0x0002F836 File Offset: 0x0002DA36
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
				this.dirty = true;
			}
		}
	}

	// Token: 0x170000F2 RID: 242
	// (get) Token: 0x06000734 RID: 1844 RVA: 0x0002F84F File Offset: 0x0002DA4F
	// (set) Token: 0x06000735 RID: 1845 RVA: 0x0002F857 File Offset: 0x0002DA57
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
				this.dirty = true;
			}
		}
	}

	// Token: 0x06000736 RID: 1846 RVA: 0x0002F878 File Offset: 0x0002DA78
	private void Awake()
	{
		exSpriteBase[] componentsInChildren = base.gameObject.GetComponentsInChildren<exSpriteBase>(true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].clip = this;
		}
		this.currentPos = new Vector2(base.transform.position.x, base.transform.position.y);
		this.dirty = false;
		this.UpdateClipMaterials();
	}

	// Token: 0x06000737 RID: 1847 RVA: 0x0002F8E1 File Offset: 0x0002DAE1
	private void OnDestroy()
	{
		this.Remove(base.gameObject);
	}

	// Token: 0x06000738 RID: 1848 RVA: 0x0002F8F0 File Offset: 0x0002DAF0
	private void LateUpdate()
	{
		if (base.transform.hasChanged)
		{
			Vector2 vector = new Vector2(base.transform.position.x, base.transform.position.y);
			if (vector != this.currentPos)
			{
				this.currentPos = vector;
				this.dirty = true;
			}
		}
		if (this.dirty)
		{
			this.UpdateClipMaterials();
			this.dirty = false;
		}
	}

	// Token: 0x06000739 RID: 1849 RVA: 0x0002F962 File Offset: 0x0002DB62
	public void SetDirty()
	{
		this.dirty = true;
	}

	// Token: 0x0600073A RID: 1850 RVA: 0x0002F96B File Offset: 0x0002DB6B
	public void CheckDirty()
	{
		if (this.dirty)
		{
			this.UpdateClipMaterials();
			this.dirty = false;
		}
	}

	// Token: 0x0600073B RID: 1851 RVA: 0x0002F984 File Offset: 0x0002DB84
	private void UpdateClipMaterials()
	{
		Rect worldAABoundingRect = base.GetWorldAABoundingRect();
		Vector4 vector = new Vector4(worldAABoundingRect.center.x, worldAABoundingRect.center.y, worldAABoundingRect.width, worldAABoundingRect.height);
		foreach (Material material in this.materialTable.Values)
		{
			material.SetVector("_ClipRect", vector);
		}
	}

	// Token: 0x0600073C RID: 1852 RVA: 0x0002FA14 File Offset: 0x0002DC14
	public void Add(exSpriteBase _sprite)
	{
		exClipping clip = _sprite.clip;
		if (clip == this)
		{
			return;
		}
		if (clip != null)
		{
			clip.Remove(_sprite);
		}
		exSpriteBase[] componentsInChildren = _sprite.GetComponentsInChildren<exSpriteBase>(true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].clip = this;
		}
		if (!_sprite.transform.IsChildOf(base.transform))
		{
			_sprite.transform.parent = base.transform;
		}
		this.dirty = true;
	}

	// Token: 0x0600073D RID: 1853 RVA: 0x0002FA88 File Offset: 0x0002DC88
	public void Add(GameObject _gameObject)
	{
		exSpriteBase[] componentsInChildren = _gameObject.GetComponentsInChildren<exSpriteBase>(true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].clip = this;
		}
		if (!_gameObject.transform.IsChildOf(base.transform))
		{
			_gameObject.transform.parent = base.transform;
		}
		this.dirty = true;
	}

	// Token: 0x0600073E RID: 1854 RVA: 0x0002FADF File Offset: 0x0002DCDF
	public void Remove(exSpriteBase _sprite)
	{
		this.Remove(_sprite.gameObject);
	}

	// Token: 0x0600073F RID: 1855 RVA: 0x0002FAF0 File Offset: 0x0002DCF0
	public void Remove(GameObject _gameObject)
	{
		exSpriteBase[] componentsInChildren = _gameObject.GetComponentsInChildren<exSpriteBase>(true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].clip = null;
		}
		this.dirty = true;
	}

	// Token: 0x06000740 RID: 1856 RVA: 0x0002FB24 File Offset: 0x0002DD24
	public Material GetClippedMaterial(Shader _shader, Texture _texture)
	{
		if (_shader == null)
		{
			_shader = Shader.Find("ex2D/Alpha Blended (Clipping)");
			if (_shader == null)
			{
				return null;
			}
		}
		else
		{
			Shader shader = Shader.Find(_shader.name + " (Clipping)");
			if (shader == null)
			{
				Debug.LogError("Failed to find clip shader named " + _shader.name + " (Clipping)");
				return null;
			}
			_shader = shader;
		}
		MaterialTableKey materialTableKey = new MaterialTableKey(_shader, _texture);
		Material material;
		if (!this.materialTable.TryGetValue(materialTableKey, out material) || material == null)
		{
			material = new Material(_shader);
			material.hideFlags = HideFlags.DontSave;
			material.mainTexture = _texture;
			Rect worldAABoundingRect = base.GetWorldAABoundingRect();
			Vector4 vector = new Vector4(worldAABoundingRect.center.x, worldAABoundingRect.center.y, worldAABoundingRect.width, worldAABoundingRect.height);
			material.SetVector("_ClipRect", vector);
			this.materialTable[materialTableKey] = material;
		}
		return material;
	}

	// Token: 0x04000506 RID: 1286
	private const string shaderPostfix = " (Clipping)";

	// Token: 0x04000507 RID: 1287
	private Dictionary<MaterialTableKey, Material> materialTable = new Dictionary<MaterialTableKey, Material>(MaterialTableKey.Comparer.instance);

	// Token: 0x04000508 RID: 1288
	private Vector2 currentPos = Vector2.zero;

	// Token: 0x04000509 RID: 1289
	private bool dirty;
}
