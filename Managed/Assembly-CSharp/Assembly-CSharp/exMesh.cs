using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// Token: 0x020000B4 RID: 180
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class exMesh : MonoBehaviour
{
	// Token: 0x170000C2 RID: 194
	// (get) Token: 0x06000693 RID: 1683 RVA: 0x0002D728 File Offset: 0x0002B928
	// (set) Token: 0x06000694 RID: 1684 RVA: 0x0002D744 File Offset: 0x0002B944
	public Material material
	{
		get
		{
			if (this.cachedRenderer)
			{
				return this.cachedRenderer.sharedMaterial;
			}
			return null;
		}
		set
		{
			if (this.cachedRenderer)
			{
				this.cachedRenderer.sharedMaterial = value;
				return;
			}
			global::UnityEngine.Debug.LogError("no MeshRenderer");
		}
	}

	// Token: 0x170000C3 RID: 195
	// (get) Token: 0x06000695 RID: 1685 RVA: 0x0002D76A File Offset: 0x0002B96A
	public bool hasTriangle
	{
		get
		{
			return this.sortedSpriteList.Count > 0;
		}
	}

	// Token: 0x170000C4 RID: 196
	// (get) Token: 0x06000696 RID: 1686 RVA: 0x0002D77A File Offset: 0x0002B97A
	private bool isDynamic
	{
		get
		{
			return this.mesh0 != null && this.mesh1 != null;
		}
	}

	// Token: 0x06000697 RID: 1687 RVA: 0x0002D78F File Offset: 0x0002B98F
	private void Awake()
	{
		this.Init();
	}

	// Token: 0x06000698 RID: 1688 RVA: 0x0002D798 File Offset: 0x0002B998
	private void OnDestroy()
	{
		this.spriteList = null;
		this.sortedSpriteList = null;
		this.vertices = null;
		this.indices = null;
		this.uvs = null;
		this.colors32 = null;
		if (this.mesh0 != null)
		{
			this.mesh0.Destroy();
		}
		this.mesh0 = null;
		if (this.mesh1 != null)
		{
			this.mesh1.Destroy();
		}
		this.mesh1 = null;
		this.cachedFilter.sharedMesh = null;
		this.cachedFilter = null;
		this.cachedRenderer.sharedMaterial = null;
		this.cachedRenderer = null;
	}

	// Token: 0x06000699 RID: 1689 RVA: 0x0002D835 File Offset: 0x0002BA35
	public static exMesh Create(exLayer _layer)
	{
		exMesh exMesh = new GameObject
		{
			hideFlags = HideFlags.HideAndDontSave
		}.AddComponent<exMesh>();
		exMesh.Init();
		return exMesh;
	}

	// Token: 0x0600069A RID: 1690 RVA: 0x0002D850 File Offset: 0x0002BA50
	public static void FlushBuffers(Mesh _mesh, exUpdateFlags _updateFlags, exList<Vector3> _vertices, exList<int> _indices, exList<Vector2> _uvs, exList<Color32> _colors32, exList<Vector4> _tangents, exList<Vector3> _normals)
	{
		if ((_updateFlags & exUpdateFlags.VertexAndIndex) == exUpdateFlags.VertexAndIndex)
		{
			_mesh.triangles = null;
		}
		if ((_updateFlags & exUpdateFlags.Vertex) != exUpdateFlags.None || (_updateFlags & exUpdateFlags.Index) != exUpdateFlags.None)
		{
			_mesh.vertices = _vertices.FastToArray();
		}
		if ((_updateFlags & exUpdateFlags.UV) != exUpdateFlags.None)
		{
			_mesh.uv = _uvs.FastToArray();
		}
		if ((_updateFlags & exUpdateFlags.Color) != exUpdateFlags.None)
		{
			_mesh.colors32 = _colors32.FastToArray();
			_mesh.tangents = _tangents.FastToArray();
			_mesh.normals = _normals.FastToArray();
		}
		if ((_updateFlags & exUpdateFlags.Index) != exUpdateFlags.None)
		{
			_mesh.triangles = _indices.FastToArray();
		}
		if ((_updateFlags & exUpdateFlags.Index) != exUpdateFlags.None || (_updateFlags & exUpdateFlags.Vertex) != exUpdateFlags.None)
		{
			_mesh.RecalculateBounds();
		}
		if ((_updateFlags & exUpdateFlags.Normal) != exUpdateFlags.None)
		{
			Vector3[] array = new Vector3[_vertices.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new Vector3(0f, 0f, -1f);
			}
			_mesh.normals = array;
		}
	}

	// Token: 0x0600069B RID: 1691 RVA: 0x0002D924 File Offset: 0x0002BB24
	public void Apply(exUpdateFlags _additionalUpdateFlags = exUpdateFlags.None)
	{
		this.updateFlags |= _additionalUpdateFlags;
		Mesh mesh;
		if (this.isDynamic && this.updateFlags != exUpdateFlags.None)
		{
			mesh = this.SwapMeshBuffer();
		}
		else
		{
			mesh = this.GetMeshBuffer();
		}
		exMesh.FlushBuffers(mesh, this.updateFlags, this.vertices, this.indices, this.uvs, this.colors32, null, null);
		if ((this.updateFlags & exUpdateFlags.Vertex) != exUpdateFlags.None)
		{
			bool flag = false;
			if (this.spriteList.Count > 10)
			{
				flag = true;
			}
			else
			{
				int i = 0;
				int count = this.spriteList.Count;
				while (i < count)
				{
					if (this.spriteList[i].visible)
					{
						flag = true;
						break;
					}
					i++;
				}
			}
			if (base.gameObject.activeSelf != flag)
			{
				base.gameObject.SetActive(flag);
			}
		}
		else if ((this.updateFlags & exUpdateFlags.Index) != exUpdateFlags.None)
		{
			bool flag2 = this.indices.Count > 0;
			if (base.gameObject.activeSelf != flag2)
			{
				base.gameObject.SetActive(flag2);
			}
		}
		this.updateFlags = exUpdateFlags.None;
	}

	// Token: 0x0600069C RID: 1692 RVA: 0x0002DA2C File Offset: 0x0002BC2C
	public void Compact()
	{
		this.spriteList.TrimExcess();
		this.vertices.TrimExcess();
		this.indices.TrimExcess();
		this.uvs.TrimExcess();
		this.colors32.TrimExcess();
		this.updateFlags |= exUpdateFlags.UV | exUpdateFlags.Color | exUpdateFlags.Normal;
	}

	// Token: 0x0600069D RID: 1693 RVA: 0x0002DA80 File Offset: 0x0002BC80
	public void SetDynamic(bool _dynamic)
	{
		if (this.isDynamic == _dynamic)
		{
			return;
		}
		if (_dynamic)
		{
			if (this.mesh0 == null)
			{
				this.mesh0 = this.CreateMesh();
			}
			this.mesh0.MarkDynamic();
			if (this.mesh1 == null)
			{
				this.mesh1 = this.CreateMesh();
			}
			this.mesh1.MarkDynamic();
			this.lastUpdateFlags = exUpdateFlags.All;
			return;
		}
		if (!this.isEvenMeshBuffer)
		{
			this.isEvenMeshBuffer = true;
			this.updateFlags |= this.lastUpdateFlags;
		}
		if (this.mesh1 != null)
		{
			this.mesh1.Destroy();
		}
		this.mesh1 = null;
	}

	// Token: 0x0600069E RID: 1694 RVA: 0x0002DB2F File Offset: 0x0002BD2F
	[ContextMenu("Recalculate Bounds")]
	[Conditional("EX_DEBUG")]
	public void RecalculateBounds()
	{
		this.GetMeshBuffer().RecalculateBounds();
	}

	// Token: 0x0600069F RID: 1695 RVA: 0x0002DB3C File Offset: 0x0002BD3C
	[ContextMenu("Output Mesh Info")]
	[Conditional("EX_DEBUG")]
	public void OutputDebugInfo(bool outputBuffer = false)
	{
		Mesh meshBuffer = this.GetMeshBuffer();
		if (meshBuffer == null)
		{
			global::UnityEngine.Debug.Log("mesh is null");
			return;
		}
		string text = string.Join(", ", this.spriteList.ConvertAll<string>((exLayeredSprite x) => x.gameObject.name).ToArray());
		global::UnityEngine.Debug.Log(string.Format("exMesh: {3} spriteList[{0}]: ({2}) CurBufferId: {1}", new object[]
		{
			this.spriteList.Count,
			this.isEvenMeshBuffer ? 0 : 1,
			text,
			base.gameObject.name
		}), this);
		if (outputBuffer)
		{
			string text2 = "Sprite Info: ";
			foreach (exLayeredSprite exLayeredSprite in this.sortedSpriteList)
			{
				global::UnityEngine.Debug.Log(string.Format("{4}: vertexBufferIndex: {0} vertexCount: {1} indexBufferIndex: {2} indexCount: {3} ", new object[]
				{
					exLayeredSprite.vertexBufferIndex,
					exLayeredSprite.vertexCount,
					exLayeredSprite.indexBufferIndex,
					exLayeredSprite.indexCount,
					exLayeredSprite.gameObject.name
				}), exLayeredSprite);
			}
			global::UnityEngine.Debug.Log(text2, this);
		}
		if (outputBuffer)
		{
			string text3 = "Vertex Buffer: ";
			foreach (Vector3 vector in this.vertices.buffer)
			{
				string text4 = text3;
				Vector3 vector2 = vector;
				text3 = text4 + vector2.ToString();
				text3 += " ";
			}
			global::UnityEngine.Debug.Log(text3, this);
		}
		string text5 = "Mesh.vertices[" + meshBuffer.vertexCount.ToString() + "]: ";
		foreach (Vector3 vector3 in meshBuffer.vertices)
		{
			text5 += vector3.ToString("F3");
			text5 += ", ";
		}
		global::UnityEngine.Debug.Log(text5, this);
		if (outputBuffer)
		{
			string text6 = "Index Buffer: ";
			foreach (int num in this.indices.buffer)
			{
				text6 += num.ToString();
				text6 += ",";
			}
			global::UnityEngine.Debug.Log(text6, this);
		}
		string text7 = "Mesh.indices[" + meshBuffer.triangles.Length.ToString() + "]: ";
		foreach (int num2 in meshBuffer.triangles)
		{
			text7 += num2.ToString();
			text7 += ",";
		}
		global::UnityEngine.Debug.Log(text7, this);
		if (outputBuffer)
		{
			string text8 = "UV Buffer[" + this.uvs.buffer.Length.ToString() + "]: ";
			foreach (Vector2 vector4 in this.uvs.buffer)
			{
				string text9 = text8;
				Vector2 vector5 = vector4;
				text8 = text9 + vector5.ToString();
				text8 += ",";
			}
			global::UnityEngine.Debug.Log(text8, this);
		}
		string text10 = "Mesh.uvs[" + meshBuffer.uv.Length.ToString() + "]: ";
		foreach (Vector2 vector6 in meshBuffer.uv)
		{
			text10 += vector6.ToString("F4");
			text10 += ",";
		}
		global::UnityEngine.Debug.Log(text10, this);
		text10 = "Mesh.colors: ";
		foreach (Color color in meshBuffer.colors)
		{
			string text11 = text10;
			Color color2 = color;
			text10 = text11 + color2.ToString();
			text10 += ",";
		}
		global::UnityEngine.Debug.Log(text10, this);
	}

	// Token: 0x060006A0 RID: 1696 RVA: 0x0002DF88 File Offset: 0x0002C188
	public void Clear()
	{
		this.spriteList.Clear();
		this.sortedSpriteList.Clear();
		this.vertices.Clear();
		this.indices.Clear();
		this.uvs.Clear();
		this.colors32.Clear();
		if (this.mesh0 != null)
		{
			this.mesh0.Clear();
		}
		if (this.mesh1 != null)
		{
			this.mesh1.Clear();
		}
		this.updateFlags = exUpdateFlags.None;
	}

	// Token: 0x060006A1 RID: 1697 RVA: 0x0002E010 File Offset: 0x0002C210
	private Mesh SwapMeshBuffer()
	{
		if (exMesh.enableDoubleBuffer)
		{
			this.isEvenMeshBuffer = !this.isEvenMeshBuffer;
			exUpdateFlags exUpdateFlags = this.updateFlags;
			this.updateFlags |= this.lastUpdateFlags;
			this.lastUpdateFlags = exUpdateFlags;
		}
		return this.GetMeshBuffer();
	}

	// Token: 0x060006A2 RID: 1698 RVA: 0x0002E05C File Offset: 0x0002C25C
	private Mesh GetMeshBuffer()
	{
		if (this.isEvenMeshBuffer)
		{
			if (this.mesh0 == null)
			{
				this.mesh0 = this.CreateMesh();
			}
			if (this.cachedFilter.sharedMesh != this.mesh0)
			{
				this.cachedFilter.sharedMesh = this.mesh0;
			}
			return this.mesh0;
		}
		if (this.mesh1 == null)
		{
			this.mesh1 = this.CreateMesh();
		}
		if (this.cachedFilter.sharedMesh != this.mesh1)
		{
			this.cachedFilter.sharedMesh = this.mesh1;
		}
		return this.mesh1;
	}

	// Token: 0x060006A3 RID: 1699 RVA: 0x0002E104 File Offset: 0x0002C304
	private Mesh CreateMesh()
	{
		return new Mesh
		{
			hideFlags = HideFlags.DontSave
		};
	}

	// Token: 0x060006A4 RID: 1700 RVA: 0x0002E114 File Offset: 0x0002C314
	private void Init()
	{
		if (this.cachedFilter == null)
		{
			this.cachedFilter = base.gameObject.GetComponent<MeshFilter>();
		}
		if (this.mesh0 == null)
		{
			this.mesh0 = this.CreateMesh();
		}
		if (this.cachedRenderer == null)
		{
			this.cachedRenderer = base.gameObject.GetComponent<MeshRenderer>();
			this.cachedRenderer.receiveShadows = false;
		}
	}

	// Token: 0x060006A5 RID: 1701 RVA: 0x0002E185 File Offset: 0x0002C385
	[Conditional("EX_DEBUG")]
	public void UpdateDebugName(exLayer layer = null)
	{
	}

	// Token: 0x040004C1 RID: 1217
	public static bool enableDoubleBuffer = true;

	// Token: 0x040004C2 RID: 1218
	public const int QUAD_INDEX_COUNT = 6;

	// Token: 0x040004C3 RID: 1219
	public const int QUAD_VERTEX_COUNT = 4;

	// Token: 0x040004C4 RID: 1220
	public const int MAX_VERTEX_COUNT = 65000;

	// Token: 0x040004C5 RID: 1221
	public const int MAX_QUAD_COUNT = 16250;

	// Token: 0x040004C6 RID: 1222
	[NonSerialized]
	private Renderer cachedRenderer;

	// Token: 0x040004C7 RID: 1223
	[NonSerialized]
	private MeshFilter cachedFilter;

	// Token: 0x040004C8 RID: 1224
	[NonSerialized]
	public List<exLayeredSprite> spriteList = new List<exLayeredSprite>();

	// Token: 0x040004C9 RID: 1225
	[NonSerialized]
	public List<exLayeredSprite> sortedSpriteList = new List<exLayeredSprite>();

	// Token: 0x040004CA RID: 1226
	[NonSerialized]
	private Mesh mesh0;

	// Token: 0x040004CB RID: 1227
	[NonSerialized]
	private Mesh mesh1;

	// Token: 0x040004CC RID: 1228
	[NonSerialized]
	private bool isEvenMeshBuffer = true;

	// Token: 0x040004CD RID: 1229
	[NonSerialized]
	public exList<Vector3> vertices = new exList<Vector3>();

	// Token: 0x040004CE RID: 1230
	[NonSerialized]
	public exList<int> indices = new exList<int>(6);

	// Token: 0x040004CF RID: 1231
	[NonSerialized]
	public exList<Vector2> uvs = new exList<Vector2>();

	// Token: 0x040004D0 RID: 1232
	[NonSerialized]
	public exList<Color32> colors32 = new exList<Color32>();

	// Token: 0x040004D1 RID: 1233
	[NonSerialized]
	public exList<Vector3> tangents = new exList<Vector3>();

	// Token: 0x040004D2 RID: 1234
	[NonSerialized]
	public exList<Vector4> normals = new exList<Vector4>();

	// Token: 0x040004D3 RID: 1235
	[NonSerialized]
	public exUpdateFlags updateFlags;

	// Token: 0x040004D4 RID: 1236
	[NonSerialized]
	public exUpdateFlags lastUpdateFlags;
}
