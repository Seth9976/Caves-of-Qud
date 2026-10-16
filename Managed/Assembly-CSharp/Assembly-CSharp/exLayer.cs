using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// Token: 0x020000BD RID: 189
[ExecuteInEditMode]
public class exLayer : MonoBehaviour
{
	// Token: 0x170000F3 RID: 243
	// (get) Token: 0x06000742 RID: 1858 RVA: 0x0002FC39 File Offset: 0x0002DE39
	// (set) Token: 0x06000743 RID: 1859 RVA: 0x0002FC44 File Offset: 0x0002DE44
	public bool show
	{
		get
		{
			return this.show_;
		}
		set
		{
			if (this.show_ == value)
			{
				return;
			}
			for (int i = 0; i < this.meshList.Count; i++)
			{
				exMesh exMesh = this.meshList[i];
				if (exMesh != null)
				{
					exMesh.gameObject.SetActive(value && exMesh.hasTriangle);
				}
			}
			this.show_ = value;
		}
	}

	// Token: 0x170000F4 RID: 244
	// (get) Token: 0x06000744 RID: 1860 RVA: 0x0002FCA5 File Offset: 0x0002DEA5
	// (set) Token: 0x06000745 RID: 1861 RVA: 0x0002FCAD File Offset: 0x0002DEAD
	public bool ordered
	{
		get
		{
			return this.ordered_;
		}
		set
		{
			if (this.ordered_ != value)
			{
				this.ordered_ = value;
				if (this.ordered_)
				{
					this.DestroyMeshes();
					this.GenerateMeshes();
				}
			}
		}
	}

	// Token: 0x170000F5 RID: 245
	// (get) Token: 0x06000746 RID: 1862 RVA: 0x0002FCD3 File Offset: 0x0002DED3
	// (set) Token: 0x06000747 RID: 1863 RVA: 0x0002FCDC File Offset: 0x0002DEDC
	public exLayerType layerType
	{
		get
		{
			return this.layerType_;
		}
		set
		{
			if (this.layerType_ == value)
			{
				return;
			}
			this.layerType_ = value;
			bool flag = value == exLayerType.Dynamic;
			for (int i = 0; i < this.meshList.Count; i++)
			{
				this.meshList[i].SetDynamic(flag);
			}
			if (value == exLayerType.Static)
			{
				this.Compact();
			}
		}
	}

	// Token: 0x170000F6 RID: 246
	// (get) Token: 0x06000748 RID: 1864 RVA: 0x0002FD30 File Offset: 0x0002DF30
	// (set) Token: 0x06000749 RID: 1865 RVA: 0x0002FD38 File Offset: 0x0002DF38
	public float alpha
	{
		get
		{
			return this.alpha_;
		}
		set
		{
			if (this.alpha_ == value)
			{
				return;
			}
			this.alpha_ = value;
			this.alphaHasChanged = true;
		}
	}

	// Token: 0x170000F7 RID: 247
	// (get) Token: 0x0600074A RID: 1866 RVA: 0x0002FD52 File Offset: 0x0002DF52
	// (set) Token: 0x0600074B RID: 1867 RVA: 0x0002FD5A File Offset: 0x0002DF5A
	public float customZ
	{
		get
		{
			return this.zMin_;
		}
		set
		{
			if (this.zMin_ == value)
			{
				return;
			}
			this.zMin_ = value;
			this.SetWorldBoundsZMin(this.zMin_);
		}
	}

	// Token: 0x170000F8 RID: 248
	// (get) Token: 0x0600074C RID: 1868 RVA: 0x0002FD79 File Offset: 0x0002DF79
	public Transform cachedTransform
	{
		get
		{
			if (this.cachedTransform_ == null)
			{
				this.cachedTransform_ = base.transform;
			}
			return this.cachedTransform_;
		}
	}

	// Token: 0x0600074D RID: 1869 RVA: 0x0002FD95 File Offset: 0x0002DF95
	private void OnDisable()
	{
		this.DestroyMeshes();
	}

	// Token: 0x0600074E RID: 1870 RVA: 0x0002FDA0 File Offset: 0x0002DFA0
	[ContextMenu("UpdateSprites")]
	public void UpdateSprites()
	{
		if (!this.show_)
		{
			return;
		}
		for (int i = this.meshList.Count - 1; i >= 0; i--)
		{
			exMesh exMesh = this.meshList[i];
			for (int j = exMesh.spriteList.Count - 1; j >= 0; j--)
			{
				exLayeredSprite exLayeredSprite = exMesh.spriteList[j];
				if (exLayeredSprite == null)
				{
					this.RemoveFromMesh(exLayeredSprite, exMesh);
				}
			}
		}
		int k = 0;
		while (k < this.meshList.Count)
		{
			exMesh exMesh2 = this.meshList[k];
			bool flag = exMesh2 == null;
			if (flag || exMesh2.spriteList.Count == 0)
			{
				if (!flag)
				{
					exMesh2.gameObject.Destroy();
				}
				this.meshList.RemoveAt(k);
				if (k - 1 >= 0 && k < this.meshList.Count)
				{
					int num = ((this.layerType_ == exLayerType.Dynamic) ? exLayer.maxDynamicMeshVertex : 65000);
					if (this.meshList[k - 1].vertices.Count < num)
					{
						this.ShiftSpritesDown(k - 1, num, num);
					}
				}
			}
			else
			{
				k++;
			}
		}
		this.UpdateAllSpritesDepth();
		exUpdateFlags exUpdateFlags = (this.alphaHasChanged ? exUpdateFlags.Color : exUpdateFlags.None);
		this.alphaHasChanged = false;
		for (int l = this.meshList.Count - 1; l >= 0; l--)
		{
			exMesh exMesh3 = this.meshList[l];
			exUpdateFlags exUpdateFlags2 = exUpdateFlags.None;
			for (int m = 0; m < exMesh3.spriteList.Count; m++)
			{
				exLayeredSprite exLayeredSprite2 = exMesh3.spriteList[m];
				exLayeredSprite2.updateFlags |= exUpdateFlags;
				if (exLayeredSprite2.isInIndexBuffer)
				{
					exLayeredSprite2.UpdateTransform();
					if (exLayeredSprite2.updateFlags != exUpdateFlags.None)
					{
						exUpdateFlags exUpdateFlags3 = exLayeredSprite2.UpdateBuffers(exMesh3.vertices, exMesh3.uvs, exMesh3.colors32, exMesh3.indices);
						exUpdateFlags2 |= exUpdateFlags3;
					}
				}
			}
			exMesh3.Apply(exUpdateFlags2);
		}
		if (this.anyFontTextureRefreshed)
		{
			this.anyFontTextureRefreshed = false;
			for (int n = this.meshList.Count - 1; n >= 0; n--)
			{
				exMesh exMesh4 = this.meshList[n];
				exUpdateFlags exUpdateFlags4 = exUpdateFlags.None;
				for (int num2 = 0; num2 < exMesh4.spriteList.Count; num2++)
				{
					exLayeredSprite exLayeredSprite3 = exMesh4.spriteList[num2];
					if (exLayeredSprite3.isInIndexBuffer && exLayeredSprite3.updateFlags != exUpdateFlags.None)
					{
						exUpdateFlags exUpdateFlags5 = exLayeredSprite3.UpdateBuffers(exMesh4.vertices, exMesh4.uvs, exMesh4.colors32, exMesh4.indices);
						exUpdateFlags4 |= exUpdateFlags5;
					}
				}
				exMesh4.Apply(exUpdateFlags4);
			}
		}
	}

	// Token: 0x0600074F RID: 1871 RVA: 0x00030064 File Offset: 0x0002E264
	public void Add(exLayeredSprite _sprite, bool _recursively = true)
	{
		if (_recursively)
		{
			exLayer layer = _sprite.layer;
			if (layer == this)
			{
				return;
			}
			if (layer != null)
			{
				layer.Remove(_sprite, true);
			}
			exLayeredSprite[] componentsInChildren = _sprite.GetComponentsInChildren<exLayeredSprite>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				this.DoAddSprite(componentsInChildren[i], true);
			}
			if (!_sprite.cachedTransform.IsChildOf(this.cachedTransform))
			{
				_sprite.cachedTransform.parent = this.cachedTransform_;
				return;
			}
		}
		else
		{
			this.DoAddSprite(_sprite, true);
		}
	}

	// Token: 0x06000750 RID: 1872 RVA: 0x000300E0 File Offset: 0x0002E2E0
	public void Add(GameObject _gameObject)
	{
		exLayeredSprite[] componentsInChildren = _gameObject.GetComponentsInChildren<exLayeredSprite>(true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			this.DoAddSprite(componentsInChildren[i], true);
		}
		if (!_gameObject.transform.IsChildOf(this.cachedTransform))
		{
			_gameObject.transform.parent = this.cachedTransform_;
		}
	}

	// Token: 0x06000751 RID: 1873 RVA: 0x00030134 File Offset: 0x0002E334
	public void Remove(exLayeredSprite _sprite, bool _recursively = true)
	{
		if (_recursively)
		{
			this.Remove(_sprite.gameObject, _recursively);
			return;
		}
		if (_sprite.layer != this)
		{
			global::UnityEngine.Debug.LogWarning("Sprite not in this layer.");
			return;
		}
		int num = this.IndexOfMesh(_sprite);
		if (num != -1)
		{
			this.RemoveFromMesh(_sprite, this.meshList[num]);
			((exLayer.IFriendOfLayer)_sprite).DoSetLayer(null);
		}
		else
		{
			((exLayer.IFriendOfLayer)_sprite).ResetLayerProperties();
		}
		if (_sprite.spriteIdInLayer == this.nextSpriteUniqueId - 1 && this.nextSpriteUniqueId > 0)
		{
			this.nextSpriteUniqueId--;
		}
		_sprite.spriteIdInLayer = -1;
	}

	// Token: 0x06000752 RID: 1874 RVA: 0x000301C8 File Offset: 0x0002E3C8
	public void Remove(GameObject _gameObject, bool _recursively = true)
	{
		if (_recursively)
		{
			exLayeredSprite[] componentsInChildren = _gameObject.GetComponentsInChildren<exLayeredSprite>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				this.Remove(componentsInChildren[i], false);
			}
			return;
		}
		exLayeredSprite component = _gameObject.GetComponent<exLayeredSprite>();
		if (component != null)
		{
			this.Remove(component, false);
		}
	}

	// Token: 0x06000753 RID: 1875 RVA: 0x00030214 File Offset: 0x0002E414
	internal void ShowSprite(exLayeredSprite _sprite)
	{
		if (!_sprite.isInIndexBuffer)
		{
			int num = this.IndexOfMesh(_sprite);
			if (num != -1)
			{
				this.AddIndices(this.meshList[num], _sprite);
				return;
			}
		}
		else
		{
			_sprite.transparent = false;
		}
	}

	// Token: 0x06000754 RID: 1876 RVA: 0x00030250 File Offset: 0x0002E450
	internal void HideSprite(exLayeredSprite _sprite)
	{
		if (_sprite.isInIndexBuffer)
		{
			int num = this.IndexOfMesh(_sprite);
			if (num != -1)
			{
				this.RemoveIndices(this.meshList[num], _sprite);
			}
		}
	}

	// Token: 0x06000755 RID: 1877 RVA: 0x00030284 File Offset: 0x0002E484
	internal void FastShowSprite(exLayeredSprite _sprite)
	{
		if (!_sprite.isInIndexBuffer)
		{
			this.ShowSprite(_sprite);
			return;
		}
		_sprite.transparent = false;
	}

	// Token: 0x06000756 RID: 1878 RVA: 0x0003029D File Offset: 0x0002E49D
	internal void FastHideSprite(exLayeredSprite _sprite)
	{
		_sprite.transparent = true;
	}

	// Token: 0x06000757 RID: 1879 RVA: 0x000302A8 File Offset: 0x0002E4A8
	public void Compact()
	{
		float num = 0f;
		for (int i = 0; i < this.meshList.Count; i++)
		{
			exMesh exMesh = this.meshList[i];
			if (exMesh != null)
			{
				num = exMesh.transform.position.z;
				break;
			}
		}
		this.DestroyMeshes();
		this.GenerateMeshes();
		this.meshList.TrimExcess();
		this.depthDirtyGoList.TrimExcess();
		for (int j = 0; j < this.meshList.Count; j++)
		{
			exMesh exMesh2 = this.meshList[j];
			if (exMesh2 != null)
			{
				exMesh2.transform.position = new Vector3(0f, 0f, num);
			}
		}
	}

	// Token: 0x06000758 RID: 1880 RVA: 0x00030366 File Offset: 0x0002E566
	public void SetWorldBoundsZMin(float _zMin)
	{
		this.zMin = _zMin;
		this.ResortMeshes(0);
	}

	// Token: 0x06000759 RID: 1881 RVA: 0x00030378 File Offset: 0x0002E578
	public void GenerateMeshes()
	{
		int num = 0;
		for (int i = this.meshList.Count - 1; i >= 0; i--)
		{
			exMesh exMesh = this.meshList[i];
			if (exMesh != null)
			{
				for (int j = exMesh.spriteList.Count - 1; j >= 0; j--)
				{
					int spriteIdInLayer = exMesh.spriteList[j].spriteIdInLayer;
					if (num < spriteIdInLayer)
					{
						num = spriteIdInLayer;
					}
				}
			}
		}
		foreach (exLayeredSprite exLayeredSprite in base.GetComponentsInChildren<exLayeredSprite>(true))
		{
			if (exLayeredSprite.layer != this)
			{
				exLayeredSprite.spriteIdInLayer += num;
				this.DoAddSprite(exLayeredSprite, false);
			}
		}
	}

	// Token: 0x0600075A RID: 1882 RVA: 0x00030430 File Offset: 0x0002E630
	public void DestroyMeshes()
	{
		foreach (exLayeredSprite exLayeredSprite in base.GetComponentsInChildren<exLayeredSprite>(true))
		{
			if (exLayeredSprite != null)
			{
				if (exLayeredSprite.layer != this && exLayeredSprite.layer != null)
				{
					global::UnityEngine.Debug.LogError("Sprite's hierarchy is invalid!", exLayeredSprite);
				}
				((exLayer.IFriendOfLayer)exLayeredSprite).ResetLayerProperties();
			}
		}
		for (int j = this.meshList.Count - 1; j >= 0; j--)
		{
			exMesh exMesh = this.meshList[j];
			if (exMesh != null)
			{
				exMesh.gameObject.DestroyImmediate();
			}
		}
		this.meshList.Clear();
		this.nextSpriteUniqueId = 0;
	}

	// Token: 0x0600075B RID: 1883 RVA: 0x000304D6 File Offset: 0x0002E6D6
	internal void OnFontTextureRebuilt()
	{
		this.anyFontTextureRefreshed = true;
	}

	// Token: 0x0600075C RID: 1884 RVA: 0x000304E0 File Offset: 0x0002E6E0
	public void SetDepthDirty(GameObject _go)
	{
		Transform transform = _go.transform;
		for (int i = this.depthDirtyGoList.Count - 1; i >= 0; i--)
		{
			GameObject gameObject = this.depthDirtyGoList[i];
			if (gameObject == _go)
			{
				return;
			}
			if (gameObject == null)
			{
				this.depthDirtyGoList.RemoveAt(i);
			}
			else
			{
				Transform transform2 = gameObject.transform;
				if (transform.IsChildOf(transform2))
				{
					return;
				}
				if (transform2.IsChildOf(transform))
				{
					this.depthDirtyGoList.RemoveAt(i);
				}
			}
		}
		this.depthDirtyGoList.Add(_go);
	}

	// Token: 0x0600075D RID: 1885 RVA: 0x00030568 File Offset: 0x0002E768
	private void UpdateSpriteDepth(exLayeredSprite _sprite, float _parentGlobalDepth)
	{
		float num = _parentGlobalDepth + _sprite.depth;
		if (((exLayer.IFriendOfLayer)_sprite).globalDepth == num)
		{
			return;
		}
		((exLayer.IFriendOfLayer)_sprite).globalDepth = num;
		int num2 = this.IndexOfMesh(_sprite);
		exMesh exMesh = this.meshList[num2];
		if (this.IsRenderOrderChangedAmongMeshes(_sprite, num2))
		{
			this.RemoveFromMesh(_sprite, exMesh);
			exLayer.UpdateSpriteWhileRecreating.TryUpdate(_sprite);
			this.AddToMesh(_sprite, this.GetMeshToAdd(_sprite));
			return;
		}
		int num3 = exMesh.sortedSpriteList.IndexOf(_sprite);
		if (this.IsRenderOrderChangedInMesh(_sprite, num2, num3))
		{
			this.RemoveFromMesh(_sprite, exMesh);
			exLayer.UpdateSpriteWhileRecreating.TryUpdate(_sprite);
			this.AddToMesh(_sprite, exMesh);
		}
	}

	// Token: 0x0600075E RID: 1886 RVA: 0x000305FC File Offset: 0x0002E7FC
	private float GetParentGlobalDepth(GameObject _go)
	{
		exLayeredSprite componentUpwards = _go.GetComponentUpwards<exLayeredSprite>();
		if (!(componentUpwards != null))
		{
			return 0f;
		}
		return ((exLayer.IFriendOfLayer)componentUpwards).globalDepth;
	}

	// Token: 0x0600075F RID: 1887 RVA: 0x00030628 File Offset: 0x0002E828
	private void UpdateSpriteDepthRecursively(GameObject _go, float _parentGlobalDepth)
	{
		exLayeredSprite exLayeredSprite = _go.GetComponent(typeof(exLayeredSprite)) as exLayeredSprite;
		if (exLayeredSprite != null)
		{
			this.UpdateSpriteDepth(exLayeredSprite, _parentGlobalDepth);
		}
		float num = ((exLayeredSprite != null) ? ((exLayer.IFriendOfLayer)exLayeredSprite).globalDepth : _parentGlobalDepth);
		Transform transform = _go.transform;
		int i = 0;
		int childCount = transform.childCount;
		while (i < childCount)
		{
			this.UpdateSpriteDepthRecursively(transform.GetChild(i).gameObject, num);
			i++;
		}
	}

	// Token: 0x06000760 RID: 1888 RVA: 0x000306A0 File Offset: 0x0002E8A0
	internal void SetSpriteBufferSize(exLayeredSprite _sprite, int _vertexCount, int _indexCount)
	{
		exLayer.UpdateSpriteWhileRecreating.RegisterNewBufferSize(_sprite, _vertexCount, _indexCount);
		this.UpdateAllSpritesDepth();
		if (!exLayer.UpdateSpriteWhileRecreating.Clear())
		{
			int num = this.IndexOfMesh(_sprite);
			if (num != -1)
			{
				this.RemoveFromMesh(_sprite, this.meshList[num]);
			}
			((exLayer.IFriendOfLayer)_sprite).DoSetBufferSize(_vertexCount, _indexCount);
			this.AddToMesh(_sprite, this.GetMeshToAdd(_sprite));
		}
	}

	// Token: 0x06000761 RID: 1889 RVA: 0x000306F8 File Offset: 0x0002E8F8
	internal void RefreshSpriteMaterial(exLayeredSprite _sprite)
	{
		exLayer.UpdateSpriteWhileRecreating.RegisterMaterialDirty(_sprite);
		this.UpdateAllSpritesDepth();
		if (!exLayer.UpdateSpriteWhileRecreating.Clear())
		{
			int num = this.IndexOfMesh(_sprite);
			if (num != -1)
			{
				this.RemoveFromMesh(_sprite, this.meshList[num]);
			}
			((exLayer.IFriendOfLayer)_sprite).SetMaterialDirty();
			exMesh meshToAdd = this.GetMeshToAdd(_sprite);
			this.AddToMesh(_sprite, meshToAdd);
		}
	}

	// Token: 0x06000762 RID: 1890 RVA: 0x0003074D File Offset: 0x0002E94D
	[Conditional("UNITY_EDITOR")]
	public void UpdateNowInEditMode()
	{
	}

	// Token: 0x06000763 RID: 1891 RVA: 0x00030750 File Offset: 0x0002E950
	public int IndexOfMesh(exLayeredSprite _sprite)
	{
		Material material = _sprite.material;
		for (int i = 0; i < this.meshList.Count; i++)
		{
			exMesh exMesh = this.meshList[i];
			if (exMesh.material == material && exMesh != null && (_sprite.spriteIndexInMesh >= 0 && _sprite.spriteIndexInMesh < exMesh.spriteList.Count && exMesh.spriteList[_sprite.spriteIndexInMesh] == _sprite))
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x06000764 RID: 1892 RVA: 0x000307D4 File Offset: 0x0002E9D4
	private exMesh CreateNewMesh(Material _mat, int _index)
	{
		exMesh exMesh = exMesh.Create(this);
		exMesh.material = _mat;
		exMesh.SetDynamic(this.layerType_ == exLayerType.Dynamic);
		this.meshList.Insert(_index, exMesh);
		this.ResortMeshes(_index);
		return exMesh;
	}

	// Token: 0x06000765 RID: 1893 RVA: 0x00030814 File Offset: 0x0002EA14
	private exMesh GetNewMesh(Material _mat, int _index)
	{
		int i = 0;
		int count = this.meshList.Count;
		while (i < count)
		{
			exMesh exMesh = this.meshList[i];
			if (exMesh.spriteList.Count == 0 && exMesh != null)
			{
				exMesh.material = _mat;
				if (i < _index)
				{
					this.meshList.RemoveAt(i);
					this.meshList.Insert(_index - 1, exMesh);
					this.ResortMeshes(_index - 1);
				}
				else if (i > _index)
				{
					this.meshList.RemoveAt(i);
					this.meshList.Insert(_index, exMesh);
					this.ResortMeshes(_index);
				}
				return exMesh;
			}
			i++;
		}
		return this.CreateNewMesh(_mat, _index);
	}

	// Token: 0x06000766 RID: 1894 RVA: 0x000308C0 File Offset: 0x0002EAC0
	private void ResortMeshes(int _startIndex = 0)
	{
		float num = 0.01f;
		float num2 = this.zMin - (float)_startIndex * num;
		int i = _startIndex;
		int count = this.meshList.Count;
		while (i < count)
		{
			exMesh exMesh = this.meshList[i];
			if (exMesh != null)
			{
				exMesh.transform.position = new Vector3(0f, 0f, num2);
			}
			num2 -= num;
			i++;
		}
	}

	// Token: 0x06000767 RID: 1895 RVA: 0x0003092F File Offset: 0x0002EB2F
	[Conditional("UNITY_EDITOR")]
	public void CheckDuplicated(exLayeredSprite _sprite)
	{
	}

	// Token: 0x06000768 RID: 1896 RVA: 0x00030931 File Offset: 0x0002EB31
	private void ShiftSprite(exMesh _src, exMesh _dst, exLayeredSprite _sprite)
	{
		this.RemoveFromMesh(_sprite, _src);
		this.AddToMesh(_sprite, _dst);
	}

	// Token: 0x06000769 RID: 1897 RVA: 0x00030944 File Offset: 0x0002EB44
	private void ShiftSpritesUp(int _meshIndex, int _newVertexCount, int _maxVertexCount)
	{
		exMesh exMesh = this.meshList[_meshIndex];
		int num = exMesh.vertices.Count - _newVertexCount;
		int num2 = 0;
		for (int i = exMesh.sortedSpriteList.Count - 1; i >= 0; i--)
		{
			exLayeredSprite exLayeredSprite = exMesh.sortedSpriteList[i];
			num2 += exLayeredSprite.vertexCount;
			if (num2 >= num)
			{
				exMesh exMesh2;
				if (_meshIndex == this.meshList.Count - 1 || this.meshList[_meshIndex + 1].material != exMesh.material)
				{
					exMesh2 = this.GetNewMesh(exMesh.material, _meshIndex + 1);
				}
				else
				{
					exMesh2 = this.meshList[_meshIndex + 1];
					if (exMesh2.vertices.Count + num2 > _maxVertexCount)
					{
						this.ShiftSpritesUp(_meshIndex + 1, _maxVertexCount - num2, _maxVertexCount);
					}
				}
				for (int j = exMesh.sortedSpriteList.Count - 1; j >= i; j--)
				{
					this.ShiftSprite(exMesh, exMesh2, exMesh.sortedSpriteList[j]);
				}
				return;
			}
		}
	}

	// Token: 0x0600076A RID: 1898 RVA: 0x00030A54 File Offset: 0x0002EC54
	private void ShiftSpritesDown(int _meshIndex, int _newVertexCount, int _maxVertexCount)
	{
		for (int i = _meshIndex + 1; i < this.meshList.Count; i++)
		{
			exMesh exMesh = this.meshList[i];
			exMesh exMesh2 = this.meshList[i - 1];
			if (exMesh2.material != exMesh.material)
			{
				break;
			}
			int num = ((i == _meshIndex + 1) ? _newVertexCount : _maxVertexCount);
			while (exMesh.sortedSpriteList.Count > 0)
			{
				exLayeredSprite exLayeredSprite = exMesh.sortedSpriteList[0];
				if (exMesh2.vertices.Count + exLayeredSprite.vertexCount > num)
				{
					break;
				}
				this.ShiftSprite(exMesh, exMesh2, exLayeredSprite);
			}
		}
	}

	// Token: 0x0600076B RID: 1899 RVA: 0x00030AF0 File Offset: 0x0002ECF0
	private int GetBelowVertexCountInMesh(int _meshIndex, exLayeredSprite _sprite, int _maxVertexCount, out int _aboveSpriteIndex)
	{
		exMesh exMesh = this.meshList[_meshIndex];
		_aboveSpriteIndex = exMesh.sortedSpriteList.BinarySearch(_sprite);
		if (_aboveSpriteIndex < 0)
		{
			_aboveSpriteIndex = ~_aboveSpriteIndex;
		}
		else
		{
			_aboveSpriteIndex++;
		}
		if (_aboveSpriteIndex <= exMesh.sortedSpriteList.Count)
		{
			int num = 0;
			for (int i = 0; i < _aboveSpriteIndex; i++)
			{
				num += exMesh.sortedSpriteList[i].vertexCount;
			}
			return num;
		}
		return exMesh.vertices.Count;
	}

	// Token: 0x0600076C RID: 1900 RVA: 0x00030B74 File Offset: 0x0002ED74
	private exMesh GetShiftedMesh(int _meshIndex, exLayeredSprite _sprite, int _maxVertexCount)
	{
		exMesh exMesh = this.meshList[_meshIndex];
		int vertexCount = _sprite.vertexCount;
		int num;
		this.GetBelowVertexCountInMesh(_meshIndex, _sprite, _maxVertexCount, out num);
		int num2 = exMesh.vertices.Count;
		for (int i = exMesh.sortedSpriteList.Count - 1; i >= num; i--)
		{
			exLayeredSprite exLayeredSprite = exMesh.sortedSpriteList[i];
			num2 -= exLayeredSprite.vertexCount;
			if (num2 + vertexCount <= _maxVertexCount)
			{
				this.ShiftSpritesUp(_meshIndex, num2, _maxVertexCount);
				return exMesh;
			}
		}
		if (_meshIndex + 1 < this.meshList.Count)
		{
			int num3 = exMesh.vertices.Count - num2;
			int num4 = _maxVertexCount - num3 - vertexCount;
			this.ShiftSpritesUp(_meshIndex + 1, num4, _maxVertexCount);
			this.ShiftSpritesUp(_meshIndex, num2, _maxVertexCount);
			return this.meshList[_meshIndex + 1];
		}
		return exMesh;
	}

	// Token: 0x0600076D RID: 1901 RVA: 0x00030C40 File Offset: 0x0002EE40
	private void SplitMesh(int _meshIndex, exLayeredSprite _seperatorSprite, int _maxVertexCount)
	{
		int num;
		int belowVertexCountInMesh = this.GetBelowVertexCountInMesh(_meshIndex, _seperatorSprite, _maxVertexCount, out num);
		this.ShiftSpritesUp(_meshIndex, belowVertexCountInMesh, _maxVertexCount);
	}

	// Token: 0x0600076E RID: 1902 RVA: 0x00030C64 File Offset: 0x0002EE64
	private exMesh GetMeshToAdd(exLayeredSprite _sprite)
	{
		Material material = _sprite.material;
		int num = ((this.layerType_ == exLayerType.Dynamic) ? exLayer.maxDynamicMeshVertex : 65000);
		num -= _sprite.vertexCount;
		exMesh exMesh = null;
		for (int i = this.meshList.Count - 1; i >= 0; i--)
		{
			exMesh exMesh2 = this.meshList[i];
			if (!(exMesh2 == null) && exMesh2.sortedSpriteList.Count != 0)
			{
				exLayeredSprite exLayeredSprite = exMesh2.sortedSpriteList[exMesh2.sortedSpriteList.Count - 1];
				exLayeredSprite exLayeredSprite2 = exMesh2.sortedSpriteList[0];
				bool flag = !this.ordered_ && _sprite <= exLayeredSprite2;
				if (_sprite >= exLayeredSprite)
				{
					if (exMesh2.material == material && exMesh2.vertices.Count <= num)
					{
						return exMesh2;
					}
					if (!flag)
					{
						if (exMesh != null && exMesh.material == material && exMesh.vertices.Count <= num)
						{
							return exMesh;
						}
						return this.GetNewMesh(material, i + 1);
					}
				}
				else if (_sprite >= exLayeredSprite2)
				{
					if (exMesh2.material == material)
					{
						if (exMesh2.vertices.Count <= num)
						{
							return exMesh2;
						}
						if (!flag)
						{
							return this.GetShiftedMesh(i, _sprite, num);
						}
					}
					else if (!flag)
					{
						this.SplitMesh(i, _sprite, num);
						return this.GetNewMesh(material, i + 1);
					}
				}
				else
				{
					exMesh = exMesh2;
				}
			}
		}
		if (this.meshList.Count <= 0)
		{
			return this.GetNewMesh(material, 0);
		}
		exMesh exMesh3 = this.meshList[0];
		if (exMesh3.material == material && exMesh3.vertices.Count <= num)
		{
			return exMesh3;
		}
		exMesh newMesh = this.GetNewMesh(material, 0);
		if (exMesh3.material == material)
		{
			this.ShiftSpritesDown(0, num, num);
		}
		return newMesh;
	}

	// Token: 0x0600076F RID: 1903 RVA: 0x00030E28 File Offset: 0x0002F028
	private void DoAddSprite(exLayeredSprite _sprite, bool _newSprite)
	{
		if (_sprite.material == null)
		{
			return;
		}
		((exLayer.IFriendOfLayer)_sprite).DoSetLayer(this);
		if ((this.ordered_ && _newSprite) || _sprite.spriteIdInLayer == -1)
		{
			_sprite.spriteIdInLayer = this.nextSpriteUniqueId;
			this.nextSpriteUniqueId++;
		}
		else
		{
			this.nextSpriteUniqueId = Mathf.Max(_sprite.spriteIdInLayer + 1, this.nextSpriteUniqueId);
		}
		((exLayer.IFriendOfLayer)_sprite).globalDepth = this.GetParentGlobalDepth(_sprite.gameObject) + _sprite.depth;
		exMesh meshToAdd = this.GetMeshToAdd(_sprite);
		this.AddToMesh(_sprite, meshToAdd);
	}

	// Token: 0x06000770 RID: 1904 RVA: 0x00030EBC File Offset: 0x0002F0BC
	private void AddToMesh(exLayeredSprite _sprite, exMesh _mesh)
	{
		_sprite.updateFlags = exUpdateFlags.None;
		_sprite.spriteIndexInMesh = _mesh.spriteList.Count;
		_mesh.spriteList.Add(_sprite);
		_sprite.FillBuffers(_mesh.vertices, _mesh.uvs, _mesh.colors32);
		if (exLayeredSprite.enableFastShowHide)
		{
			this.AddIndices(_mesh, _sprite);
			_sprite.transparent = !_sprite.visible;
			return;
		}
		if (_sprite.visible)
		{
			this.AddIndices(_mesh, _sprite);
		}
	}

	// Token: 0x06000771 RID: 1905 RVA: 0x00030F34 File Offset: 0x0002F134
	private void RemoveFromMesh(exLayeredSprite _sprite, exMesh _mesh)
	{
		_mesh.spriteList.RemoveAt(_sprite.spriteIndexInMesh);
		int vertexCount = _sprite.vertexCount;
		for (int i = _sprite.spriteIndexInMesh; i < _mesh.spriteList.Count; i++)
		{
			exLayeredSprite exLayeredSprite = _mesh.spriteList[i];
			exLayeredSprite.spriteIndexInMesh = i;
			exLayeredSprite.vertexBufferIndex -= vertexCount;
			if (exLayeredSprite.isInIndexBuffer)
			{
				int num = exLayeredSprite.indexBufferIndex + exLayeredSprite.indexCount;
				for (int j = exLayeredSprite.indexBufferIndex; j < num; j++)
				{
					if (_mesh.indices.buffer[j] > 0)
					{
						_mesh.indices.buffer[j] -= vertexCount;
					}
				}
			}
		}
		_mesh.updateFlags |= exUpdateFlags.VertexAndIndex;
		_mesh.vertices.RemoveRange(_sprite.vertexBufferIndex, vertexCount);
		_mesh.colors32.RemoveRange(_sprite.vertexBufferIndex, vertexCount);
		_mesh.uvs.RemoveRange(_sprite.vertexBufferIndex, vertexCount);
		if (_sprite.spriteIndexInMesh != _mesh.spriteList.Count)
		{
			_mesh.updateFlags |= exUpdateFlags.UV | exUpdateFlags.Color | exUpdateFlags.Normal;
		}
		if (_sprite.isInIndexBuffer)
		{
			this.RemoveIndices(_mesh, _sprite);
		}
	}

	// Token: 0x06000772 RID: 1906 RVA: 0x00031064 File Offset: 0x0002F264
	private void AddIndices(exMesh _mesh, exLayeredSprite _sprite)
	{
		if (!_sprite.isInIndexBuffer)
		{
			int num;
			if (_mesh.sortedSpriteList.Count > 0)
			{
				if (!this.ordered_ && _sprite >= _mesh.sortedSpriteList[_mesh.sortedSpriteList.Count - 1])
				{
					num = _mesh.sortedSpriteList.Count;
				}
				else
				{
					num = _mesh.sortedSpriteList.BinarySearch(_sprite);
				}
				if (num < 0)
				{
					num = ~num;
				}
				if (num >= _mesh.sortedSpriteList.Count)
				{
					_sprite.indexBufferIndex = _mesh.indices.Count;
				}
				else
				{
					_sprite.indexBufferIndex = _mesh.sortedSpriteList[num].indexBufferIndex;
				}
			}
			else
			{
				num = 0;
				_sprite.indexBufferIndex = 0;
			}
			int indexCount = _sprite.indexCount;
			_mesh.indices.AddRange(indexCount);
			for (int i = _mesh.indices.Count - 1 - indexCount; i >= _sprite.indexBufferIndex; i--)
			{
				_mesh.indices.buffer[i + indexCount] = _mesh.indices.buffer[i];
			}
			_sprite.updateFlags |= exUpdateFlags.Index;
			for (int j = num; j < _mesh.sortedSpriteList.Count; j++)
			{
				_mesh.sortedSpriteList[j].indexBufferIndex += indexCount;
			}
			_mesh.sortedSpriteList.Insert(num, _sprite);
		}
	}

	// Token: 0x06000773 RID: 1907 RVA: 0x000311B4 File Offset: 0x0002F3B4
	private void RemoveIndices(exMesh _mesh, exLayeredSprite _sprite)
	{
		if (_sprite.isInIndexBuffer)
		{
			_mesh.indices.RemoveRange(_sprite.indexBufferIndex, _sprite.indexCount);
			_mesh.updateFlags |= exUpdateFlags.Index;
			for (int i = _mesh.sortedSpriteList.Count - 1; i >= 0; i--)
			{
				exLayeredSprite exLayeredSprite = _mesh.sortedSpriteList[i];
				if (exLayeredSprite.indexBufferIndex <= _sprite.indexBufferIndex)
				{
					_mesh.sortedSpriteList.RemoveAt(i);
					break;
				}
				exLayeredSprite.indexBufferIndex -= _sprite.indexCount;
			}
			_sprite.isInIndexBuffer = false;
		}
	}

	// Token: 0x06000774 RID: 1908 RVA: 0x00031250 File Offset: 0x0002F450
	private exLayeredSprite GetNearestSpriteFromBelowMesh(int _curMeshIndex)
	{
		for (int i = _curMeshIndex - 1; i >= 0; i--)
		{
			exMesh exMesh = this.meshList[i];
			if (exMesh.sortedSpriteList.Count > 0 && exMesh != null)
			{
				return exMesh.sortedSpriteList[exMesh.sortedSpriteList.Count - 1];
			}
		}
		return null;
	}

	// Token: 0x06000775 RID: 1909 RVA: 0x000312AC File Offset: 0x0002F4AC
	private exLayeredSprite GetNearestSpriteFromAboveMesh(int _curMeshIndex)
	{
		for (int i = _curMeshIndex + 1; i < this.meshList.Count; i++)
		{
			exMesh exMesh = this.meshList[i];
			if (exMesh.sortedSpriteList.Count > 0 && exMesh != null)
			{
				return exMesh.sortedSpriteList[0];
			}
		}
		return null;
	}

	// Token: 0x06000776 RID: 1910 RVA: 0x00031304 File Offset: 0x0002F504
	private bool IsRenderOrderChangedAmongMeshes(exLayeredSprite _sprite, int _oldMeshIndex)
	{
		exLayeredSprite nearestSpriteFromAboveMesh = this.GetNearestSpriteFromAboveMesh(_oldMeshIndex);
		if (nearestSpriteFromAboveMesh != null && _sprite > nearestSpriteFromAboveMesh)
		{
			return true;
		}
		exLayeredSprite nearestSpriteFromBelowMesh = this.GetNearestSpriteFromBelowMesh(_oldMeshIndex);
		return nearestSpriteFromBelowMesh != null && _sprite < nearestSpriteFromBelowMesh;
	}

	// Token: 0x06000777 RID: 1911 RVA: 0x00031348 File Offset: 0x0002F548
	private bool IsRenderOrderChangedInMesh(exLayeredSprite _sprite, int _oldMeshIndex, int _oldSortedSpriteIndex)
	{
		exMesh exMesh = this.meshList[_oldMeshIndex];
		if (_oldSortedSpriteIndex < exMesh.sortedSpriteList.Count - 1)
		{
			exLayeredSprite exLayeredSprite = exMesh.sortedSpriteList[_oldSortedSpriteIndex + 1];
			if (_sprite > exLayeredSprite)
			{
				return true;
			}
		}
		if (_oldSortedSpriteIndex > 0)
		{
			exLayeredSprite exLayeredSprite2 = exMesh.sortedSpriteList[_oldSortedSpriteIndex - 1];
			return _sprite < exLayeredSprite2;
		}
		return false;
	}

	// Token: 0x06000778 RID: 1912 RVA: 0x000313A8 File Offset: 0x0002F5A8
	private void UpdateAllSpritesDepth()
	{
		for (int i = 0; i < this.depthDirtyGoList.Count; i++)
		{
			GameObject gameObject = this.depthDirtyGoList[i];
			if (gameObject != null)
			{
				this.UpdateSpriteDepthRecursively(gameObject, this.GetParentGlobalDepth(gameObject));
			}
		}
		this.depthDirtyGoList.Clear();
	}

	// Token: 0x0400050D RID: 1293
	public static int maxDynamicMeshVertex = 90000;

	// Token: 0x0400050E RID: 1294
	[SerializeField]
	private bool show_ = true;

	// Token: 0x0400050F RID: 1295
	[SerializeField]
	private bool ordered_;

	// Token: 0x04000510 RID: 1296
	[SerializeField]
	private exLayerType layerType_ = exLayerType.Dynamic;

	// Token: 0x04000511 RID: 1297
	[SerializeField]
	private float alpha_ = 1f;

	// Token: 0x04000512 RID: 1298
	[SerializeField]
	private float zMin_;

	// Token: 0x04000513 RID: 1299
	[NonSerialized]
	public List<exMesh> meshList = new List<exMesh>();

	// Token: 0x04000514 RID: 1300
	[NonSerialized]
	private Transform cachedTransform_;

	// Token: 0x04000515 RID: 1301
	[NonSerialized]
	private int nextSpriteUniqueId;

	// Token: 0x04000516 RID: 1302
	[NonSerialized]
	private bool alphaHasChanged;

	// Token: 0x04000517 RID: 1303
	[NonSerialized]
	private float zMin;

	// Token: 0x04000518 RID: 1304
	[NonSerialized]
	private bool anyFontTextureRefreshed;

	// Token: 0x04000519 RID: 1305
	[NonSerialized]
	private List<GameObject> depthDirtyGoList = new List<GameObject>();

	// Token: 0x020015C2 RID: 5570
	public interface IFriendOfLayer
	{
		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x0600D5FE RID: 54782
		// (set) Token: 0x0600D5FF RID: 54783
		float globalDepth { get; set; }

		// Token: 0x0600D600 RID: 54784
		void DoSetLayer(exLayer _layer);

		// Token: 0x0600D601 RID: 54785
		void DoSetBufferSize(int _vertexCount, int _indexCount);

		// Token: 0x0600D602 RID: 54786
		void SetMaterialDirty();

		// Token: 0x0600D603 RID: 54787
		void ResetLayerProperties();
	}

	// Token: 0x020015C3 RID: 5571
	private struct UpdateSpriteWhileRecreating
	{
		// Token: 0x0600D604 RID: 54788 RVA: 0x004F75BD File Offset: 0x004F57BD
		public static void RegisterNewBufferSize(exLayeredSprite _sprite, int _vertexCount, int _indexCount)
		{
			exLayer.UpdateSpriteWhileRecreating.spriteNeedsToSetBufferSize = _sprite;
			exLayer.UpdateSpriteWhileRecreating.newVertexCount = _vertexCount;
			exLayer.UpdateSpriteWhileRecreating.newIndexCount = _indexCount;
		}

		// Token: 0x0600D605 RID: 54789 RVA: 0x004F75D1 File Offset: 0x004F57D1
		public static void RegisterMaterialDirty(exLayeredSprite _sprite)
		{
			exLayer.UpdateSpriteWhileRecreating.spriteNeedsToSetMaterialDirty = _sprite;
		}

		// Token: 0x0600D606 RID: 54790 RVA: 0x004F75D9 File Offset: 0x004F57D9
		public static void TryUpdate(exLayeredSprite _sprite)
		{
			if (_sprite == exLayer.UpdateSpriteWhileRecreating.spriteNeedsToSetBufferSize)
			{
				((exLayer.IFriendOfLayer)exLayer.UpdateSpriteWhileRecreating.spriteNeedsToSetBufferSize).DoSetBufferSize(exLayer.UpdateSpriteWhileRecreating.newVertexCount, exLayer.UpdateSpriteWhileRecreating.newIndexCount);
				exLayer.UpdateSpriteWhileRecreating.spriteNeedsToSetBufferSize = null;
			}
			if (_sprite == exLayer.UpdateSpriteWhileRecreating.spriteNeedsToSetMaterialDirty)
			{
				((exLayer.IFriendOfLayer)exLayer.UpdateSpriteWhileRecreating.spriteNeedsToSetMaterialDirty).SetMaterialDirty();
				exLayer.UpdateSpriteWhileRecreating.spriteNeedsToSetMaterialDirty = null;
			}
		}

		// Token: 0x0600D607 RID: 54791 RVA: 0x004F7615 File Offset: 0x004F5815
		public static bool Clear()
		{
			bool flag = exLayer.UpdateSpriteWhileRecreating.spriteNeedsToSetBufferSize == null && exLayer.UpdateSpriteWhileRecreating.spriteNeedsToSetMaterialDirty == null;
			exLayer.UpdateSpriteWhileRecreating.spriteNeedsToSetBufferSize = null;
			exLayer.UpdateSpriteWhileRecreating.spriteNeedsToSetMaterialDirty = null;
			return flag;
		}

		// Token: 0x040057AF RID: 22447
		private static exLayeredSprite spriteNeedsToSetBufferSize;

		// Token: 0x040057B0 RID: 22448
		private static int newVertexCount;

		// Token: 0x040057B1 RID: 22449
		private static int newIndexCount;

		// Token: 0x040057B2 RID: 22450
		private static exLayeredSprite spriteNeedsToSetMaterialDirty;
	}
}
