using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000B9 RID: 185
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("ex2D/2D Renderer")]
public class ex2DRenderer : MonoBehaviour
{
	// Token: 0x170000DD RID: 221
	// (get) Token: 0x060006F5 RID: 1781 RVA: 0x0002ECCE File Offset: 0x0002CECE
	// (set) Token: 0x060006F6 RID: 1782 RVA: 0x0002ECD6 File Offset: 0x0002CED6
	public bool customizeLayerZ
	{
		get
		{
			return this.customizeLayerZ_;
		}
		set
		{
			if (this.customizeLayerZ_ == value)
			{
				return;
			}
			this.customizeLayerZ_ = value;
			this.ResortLayerDepth();
		}
	}

	// Token: 0x170000DE RID: 222
	// (get) Token: 0x060006F7 RID: 1783 RVA: 0x0002ECEF File Offset: 0x0002CEEF
	public static ex2DRenderer instance
	{
		get
		{
			if (ex2DRenderer.instance_ == null)
			{
				ex2DRenderer.instance_ = global::UnityEngine.Object.FindFirstObjectByType<ex2DRenderer>();
			}
			return ex2DRenderer.instance_;
		}
	}

	// Token: 0x170000DF RID: 223
	// (get) Token: 0x060006F8 RID: 1784 RVA: 0x0002ED0D File Offset: 0x0002CF0D
	public Camera cachedCamera
	{
		get
		{
			if (this.cachedCamera_ == null)
			{
				this.cachedCamera_ = base.GetComponent<Camera>();
			}
			return this.cachedCamera_;
		}
	}

	// Token: 0x060006F9 RID: 1785 RVA: 0x0002ED30 File Offset: 0x0002CF30
	private void OnEnable()
	{
		if (ex2DRenderer.instance_ == null)
		{
			ex2DRenderer.instance_ = this;
		}
		for (int i = 0; i < this.layerList.Count; i++)
		{
			exLayer exLayer = this.layerList[i];
			if (exLayer != null)
			{
				exLayer.GenerateMeshes();
			}
		}
		this.ResortLayerDepth();
	}

	// Token: 0x060006FA RID: 1786 RVA: 0x0002ED88 File Offset: 0x0002CF88
	private void OnDisable()
	{
		for (int i = 0; i < this.layerList.Count; i++)
		{
			exLayer exLayer = this.layerList[i];
			if (exLayer != null)
			{
				exLayer.DestroyMeshes();
			}
		}
		if (this == ex2DRenderer.instance_)
		{
			ex2DRenderer.instance_ = null;
		}
	}

	// Token: 0x060006FB RID: 1787 RVA: 0x0002EDD5 File Offset: 0x0002CFD5
	private void OnDestroy()
	{
		this.cachedCamera_ = null;
	}

	// Token: 0x060006FC RID: 1788 RVA: 0x0002EDE0 File Offset: 0x0002CFE0
	private void LateUpdate()
	{
		for (int i = this.layerList.Count - 1; i >= 0; i--)
		{
			if (this.layerList[i] == null)
			{
				this.layerList.RemoveAt(i);
			}
		}
		this.UpdateLayers();
	}

	// Token: 0x060006FD RID: 1789 RVA: 0x0002EE2C File Offset: 0x0002D02C
	public exLayer CreateLayer(string name = "New Layer", int _idx = -1)
	{
		exLayer exLayer = new GameObject(name).AddComponent<exLayer>();
		if (_idx == -1)
		{
			this.layerList.Add(exLayer);
		}
		else
		{
			this.layerList.Insert(_idx, exLayer);
		}
		this.ResortLayerDepth();
		return exLayer;
	}

	// Token: 0x060006FE RID: 1790 RVA: 0x0002EE6B File Offset: 0x0002D06B
	public void DestroyLayer(int _idx)
	{
		this.DestroyLayer(this.layerList[_idx]);
	}

	// Token: 0x060006FF RID: 1791 RVA: 0x0002EE7F File Offset: 0x0002D07F
	public void DestroyLayer(exLayer _layer)
	{
		this.layerList.Remove(_layer);
		if (_layer != null)
		{
			_layer.gameObject.Destroy();
		}
	}

	// Token: 0x06000700 RID: 1792 RVA: 0x0002EEA4 File Offset: 0x0002D0A4
	public void InsertLayer(int _idx, exLayer _layer)
	{
		_idx = Mathf.Clamp(_idx, 0, this.layerList.Count);
		if (this.layerList.Contains(_layer))
		{
			Debug.LogWarning("Layer already exists in ex2DRenderer: " + ((_layer != null) ? _layer.ToString() : null), _layer);
			return;
		}
		this.layerList.Insert(_idx, _layer);
		_layer.GenerateMeshes();
		this.ResortLayerDepth();
	}

	// Token: 0x06000701 RID: 1793 RVA: 0x0002EF0A File Offset: 0x0002D10A
	public bool HasLayer(exLayer _layer)
	{
		return this.layerList.Contains(_layer);
	}

	// Token: 0x06000702 RID: 1794 RVA: 0x0002EF18 File Offset: 0x0002D118
	public exLayer GetLayer(string _layerName)
	{
		for (int i = 0; i < this.layerList.Count; i++)
		{
			exLayer exLayer = this.layerList[i];
			if (exLayer != null && exLayer.name == _layerName)
			{
				return exLayer;
			}
		}
		return null;
	}

	// Token: 0x06000703 RID: 1795 RVA: 0x0002EF64 File Offset: 0x0002D164
	public void ResortLayerDepth()
	{
		float num = this.cachedCamera.transform.position.z + this.cachedCamera.nearClipPlane;
		float num2 = (this.cachedCamera.farClipPlane - this.cachedCamera.nearClipPlane) / (float)(this.layerList.Count + 1);
		float num3 = num + num2;
		for (int i = 0; i < this.layerList.Count; i++)
		{
			exLayer exLayer = this.layerList[i];
			if (exLayer != null)
			{
				if (this.customizeLayerZ_)
				{
					exLayer.SetWorldBoundsZMin(exLayer.customZ);
				}
				else
				{
					exLayer.SetWorldBoundsZMin(num3);
					num3 += num2;
				}
			}
		}
	}

	// Token: 0x06000704 RID: 1796 RVA: 0x0002F00C File Offset: 0x0002D20C
	public static Material GetMaterial(Shader _shader, Texture _texture)
	{
		if (_shader == null)
		{
			_shader = Shader.Find("ex2D/Alpha Blended");
			if (_shader == null)
			{
				return null;
			}
		}
		MaterialTableKey materialTableKey = new MaterialTableKey(_shader, _texture);
		Material material;
		if (!ex2DRenderer.materialTable.TryGetValue(materialTableKey, out material) || material == null)
		{
			material = new Material(_shader);
			material.hideFlags = HideFlags.DontSave;
			material.mainTexture = _texture;
			ex2DRenderer.materialTable[materialTableKey] = material;
		}
		return material;
	}

	// Token: 0x06000705 RID: 1797 RVA: 0x0002F080 File Offset: 0x0002D280
	public void ForceRenderScene()
	{
		for (int i = 0; i < this.layerList.Count; i++)
		{
			exLayer exLayer = this.layerList[i];
			if (exLayer != null)
			{
				exLayer.DestroyMeshes();
				exLayer.GenerateMeshes();
			}
		}
		this.ResortLayerDepth();
		this.UpdateLayers();
	}

	// Token: 0x06000706 RID: 1798 RVA: 0x0002F0D4 File Offset: 0x0002D2D4
	public void UpdateLayers()
	{
		for (int i = 0; i < this.layerList.Count; i++)
		{
			exLayer exLayer = this.layerList[i];
			if (exLayer != null)
			{
				exLayer.UpdateSprites();
			}
		}
	}

	// Token: 0x06000707 RID: 1799 RVA: 0x0002F114 File Offset: 0x0002D314
	public void ResetCamera(bool exclusiveCamera)
	{
		if (!this.cachedCamera.orthographic)
		{
			this.cachedCamera.orthographic = true;
		}
		if (this.cachedCamera.orthographicSize != (float)Screen.height / 2f)
		{
			this.cachedCamera.orthographicSize = (float)Screen.height / 2f;
		}
		this.cachedCamera.transform.rotation = Quaternion.identity;
		this.cachedCamera.transform.SetLossyScale(Vector3.one);
	}

	// Token: 0x040004F7 RID: 1271
	public List<exLayer> layerList = new List<exLayer>();

	// Token: 0x040004F8 RID: 1272
	[SerializeField]
	private bool customizeLayerZ_;

	// Token: 0x040004F9 RID: 1273
	[NonSerialized]
	private static ex2DRenderer instance_;

	// Token: 0x040004FA RID: 1274
	[NonSerialized]
	private Camera cachedCamera_;

	// Token: 0x040004FB RID: 1275
	private static Dictionary<MaterialTableKey, Material> materialTable = new Dictionary<MaterialTableKey, Material>(MaterialTableKey.Comparer.instance);
}
