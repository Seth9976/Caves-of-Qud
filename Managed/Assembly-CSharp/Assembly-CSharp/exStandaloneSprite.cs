using System;
using UnityEngine;

// Token: 0x020000B8 RID: 184
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public abstract class exStandaloneSprite : exSpriteBase
{
	// Token: 0x170000DA RID: 218
	// (get) Token: 0x060006E0 RID: 1760 RVA: 0x0002E83B File Offset: 0x0002CA3B
	public Renderer cachedRenderer
	{
		get
		{
			if (this.cachedRenderer_ == null)
			{
				this.cachedRenderer_ = base.GetComponent<Renderer>();
			}
			return this.cachedRenderer_;
		}
	}

	// Token: 0x170000DB RID: 219
	// (get) Token: 0x060006E1 RID: 1761 RVA: 0x0002E85D File Offset: 0x0002CA5D
	// (set) Token: 0x060006E2 RID: 1762 RVA: 0x0002E884 File Offset: 0x0002CA84
	public Mesh mesh
	{
		get
		{
			if (this.mesh_ == null)
			{
				this.mesh_ = base.GetComponent<MeshFilter>().sharedMesh;
			}
			return this.mesh_;
		}
		private set
		{
			this.mesh_ = value;
			base.GetComponent<MeshFilter>().sharedMesh = this.mesh_;
			this.UpdateMesh();
		}
	}

	// Token: 0x060006E3 RID: 1763 RVA: 0x0002E8A4 File Offset: 0x0002CAA4
	public void UpdateMesh()
	{
		if (this._bCollide)
		{
			base.GetComponent<MeshCollider>().sharedMesh = null;
		}
		if (this._bCollide)
		{
			base.GetComponent<MeshCollider>().sharedMesh = this.mesh_;
		}
	}

	// Token: 0x170000DC RID: 220
	// (get) Token: 0x060006E4 RID: 1764 RVA: 0x0002E8D3 File Offset: 0x0002CAD3
	// (set) Token: 0x060006E5 RID: 1765 RVA: 0x0002E8DB File Offset: 0x0002CADB
	public bool bCollide
	{
		get
		{
			return this._bCollide;
		}
		set
		{
			this._bCollide = value;
			if (this._bCollide)
			{
				base.gameObject.AddComponent<MeshCollider>();
			}
			this.UpdateMesh();
		}
	}

	// Token: 0x060006E6 RID: 1766 RVA: 0x0002E8FE File Offset: 0x0002CAFE
	protected void Awake()
	{
		this.mesh = new Mesh();
		this.mesh.name = "ex2D Mesh";
		this.mesh.hideFlags = HideFlags.DontSave;
		this.mesh.MarkDynamic();
	}

	// Token: 0x060006E7 RID: 1767 RVA: 0x0002E934 File Offset: 0x0002CB34
	protected new void OnDestroy()
	{
		this.vertices = null;
		this.indices = null;
		this.uvs = null;
		this.colors32 = null;
		this.tangents = null;
		this.normals = null;
		if (this.mesh != null)
		{
			this.mesh_.Destroy();
		}
		this.mesh = null;
		this.cachedRenderer.sharedMaterial = null;
		this.cachedRenderer_ = null;
	}

	// Token: 0x060006E8 RID: 1768 RVA: 0x0002E99E File Offset: 0x0002CB9E
	protected new void OnEnable()
	{
		this.isOnEnabled = true;
		this.Show();
		if (this.vertices.Count == 0)
		{
			this.cachedRenderer.sharedMaterial = base.material;
			this.UpdateVertexAndIndexCount();
			this.FillBuffers();
		}
	}

	// Token: 0x060006E9 RID: 1769 RVA: 0x0002E9DA File Offset: 0x0002CBDA
	protected new void OnDisable()
	{
		this.isOnEnabled = false;
		this.Hide();
	}

	// Token: 0x060006EA RID: 1770 RVA: 0x0002E9EC File Offset: 0x0002CBEC
	protected void LateUpdate()
	{
		if (this.updateFlags != exUpdateFlags.None && this.visible)
		{
			exUpdateFlags exUpdateFlags = this.UpdateBuffers(this.vertices, this.uvs, this.colors32, this.indices);
			exMesh.FlushBuffers(this.mesh, exUpdateFlags, this.vertices, this.indices, this.uvs, this.colors32, this.tangents, this.normals);
			if ((exUpdateFlags & exUpdateFlags.Index) != exUpdateFlags.None)
			{
				bool flag = this.indices.Count > 0;
				if (this.cachedRenderer.enabled != flag)
				{
					this.cachedRenderer.enabled = flag;
				}
			}
			this.UpdateMesh();
		}
	}

	// Token: 0x060006EB RID: 1771 RVA: 0x0002EA92 File Offset: 0x0002CC92
	protected override void Show()
	{
		if (!this.cachedRenderer.enabled)
		{
			this.cachedRenderer.enabled = true;
		}
	}

	// Token: 0x060006EC RID: 1772 RVA: 0x0002EAAD File Offset: 0x0002CCAD
	protected override void Hide()
	{
		if (this.cachedRenderer.enabled)
		{
			this.cachedRenderer.enabled = false;
		}
	}

	// Token: 0x060006ED RID: 1773 RVA: 0x0002EAC8 File Offset: 0x0002CCC8
	protected override void UpdateMaterial()
	{
		this.material_ = null;
		this.cachedRenderer.sharedMaterial = base.material;
	}

	// Token: 0x060006EE RID: 1774 RVA: 0x0002EAE2 File Offset: 0x0002CCE2
	public override float GetScaleX(Space _space)
	{
		if (_space == Space.World)
		{
			return base.transform.lossyScale.x;
		}
		return base.transform.localScale.x;
	}

	// Token: 0x060006EF RID: 1775 RVA: 0x0002EB08 File Offset: 0x0002CD08
	public override float GetScaleY(Space _space)
	{
		if (_space == Space.World)
		{
			return base.transform.lossyScale.y;
		}
		return base.transform.localScale.y;
	}

	// Token: 0x060006F0 RID: 1776 RVA: 0x0002EB30 File Offset: 0x0002CD30
	public override Vector3[] GetWorldVertices()
	{
		Vector3[] array = this.GetVertices(Space.Self);
		Matrix4x4 localToWorldMatrix = base.transform.localToWorldMatrix;
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = localToWorldMatrix.MultiplyPoint3x4(array[i]);
		}
		return array;
	}

	// Token: 0x060006F1 RID: 1777
	protected abstract void UpdateVertexAndIndexCount();

	// Token: 0x060006F2 RID: 1778 RVA: 0x0002EB78 File Offset: 0x0002CD78
	protected void UpdateBufferSize()
	{
		int vertexCount_ = this.vertexCount_;
		int indexCount_ = this.indexCount_;
		this.UpdateVertexAndIndexCount();
		if (this.vertexCount_ != vertexCount_ || this.indexCount_ != indexCount_)
		{
			this.tangents.Clear();
			this.normals.Clear();
			this.vertices.Clear();
			this.uvs.Clear();
			this.colors32.Clear();
			this.indices.Clear();
			this.FillBuffers();
		}
	}

	// Token: 0x060006F3 RID: 1779 RVA: 0x0002EBF4 File Offset: 0x0002CDF4
	private void FillBuffers()
	{
		this.vertices.AddRange(this.vertexCount_);
		this.colors32.AddRange(this.vertexCount_);
		this.tangents.AddRange(this.vertexCount_);
		this.normals.AddRange(this.vertexCount_);
		this.uvs.AddRange(this.vertexCount_);
		this.indices.AddRange(this.indexCount_);
		this.updateFlags |= exUpdateFlags.All;
	}

	// Token: 0x040004EE RID: 1262
	[NonSerialized]
	protected Renderer cachedRenderer_;

	// Token: 0x040004EF RID: 1263
	[NonSerialized]
	protected Mesh mesh_;

	// Token: 0x040004F0 RID: 1264
	private bool _bCollide;

	// Token: 0x040004F1 RID: 1265
	[NonSerialized]
	protected exList<Vector3> vertices = new exList<Vector3>();

	// Token: 0x040004F2 RID: 1266
	[NonSerialized]
	protected exList<int> indices = new exList<int>(6);

	// Token: 0x040004F3 RID: 1267
	[NonSerialized]
	protected exList<Vector2> uvs = new exList<Vector2>();

	// Token: 0x040004F4 RID: 1268
	[NonSerialized]
	protected exList<Color32> colors32 = new exList<Color32>();

	// Token: 0x040004F5 RID: 1269
	[NonSerialized]
	protected exList<Vector4> tangents = new exList<Vector4>();

	// Token: 0x040004F6 RID: 1270
	[NonSerialized]
	protected exList<Vector3> normals = new exList<Vector3>();
}
