using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000114 RID: 276
public class Geometry
{
	// Token: 0x06000ADB RID: 2779 RVA: 0x00046214 File Offset: 0x00044414
	public static void SetIsoMeshQuadTexture(GameObject Mesh, int widthSegments, int heightSegments, int x, int y, string Atlas, string Sprite)
	{
	}

	// Token: 0x06000ADC RID: 2780 RVA: 0x00046218 File Offset: 0x00044418
	public static GameObject CreateIsoMesh(float width, float height, int widthSegments, int heightSegments, Geometry.Plane2AnchorPoint anchor = Geometry.Plane2AnchorPoint.BottomLeft, string optionalName = "IsoMesh", bool addCollider = false)
	{
		GameObject gameObject = new GameObject();
		if (!string.IsNullOrEmpty(optionalName))
		{
			gameObject.name = optionalName;
		}
		else
		{
			gameObject.name = "IsoPlane";
		}
		gameObject.transform.position = Vector3.zero;
		Vector2 zero;
		switch (anchor)
		{
		case Geometry.Plane2AnchorPoint.TopLeft:
			zero = new Vector2(-width / 2f, height / 2f);
			goto IL_0126;
		case Geometry.Plane2AnchorPoint.TopHalf:
			zero = new Vector2(0f, height / 2f);
			goto IL_0126;
		case Geometry.Plane2AnchorPoint.TopRight:
			zero = new Vector2(width / 2f, height / 2f);
			goto IL_0126;
		case Geometry.Plane2AnchorPoint.RightHalf:
			zero = new Vector2(width / 2f, 0f);
			goto IL_0126;
		case Geometry.Plane2AnchorPoint.BottomRight:
			zero = new Vector2(width / 2f, -height / 2f);
			goto IL_0126;
		case Geometry.Plane2AnchorPoint.BottomHalf:
			zero = new Vector2(0f, -height / 2f);
			goto IL_0126;
		case Geometry.Plane2AnchorPoint.BottomLeft:
			zero = new Vector2(-width / 2f, -height / 2f);
			goto IL_0126;
		case Geometry.Plane2AnchorPoint.LeftHalf:
			zero = new Vector2(-width / 2f, 0f);
			goto IL_0126;
		}
		zero = Vector2.zero;
		IL_0126:
		MeshFilter meshFilter = (MeshFilter)gameObject.AddComponent(typeof(MeshFilter));
		gameObject.AddComponent(typeof(MeshRenderer));
		Mesh mesh = new Mesh();
		mesh.name = gameObject.name;
		int num = widthSegments * heightSegments * 6;
		int num2 = widthSegments * heightSegments * 4;
		Vector3[] array = new Vector3[num2];
		Vector2[] array2 = new Vector2[num2];
		int[] array3 = new int[num];
		int num3 = 0;
		float num4 = 1f / (float)widthSegments;
		float num5 = 1f / (float)heightSegments;
		float num6 = width / (float)widthSegments;
		float num7 = height / (float)heightSegments / 2f;
		bool flag = false;
		for (float num8 = 0f; num8 < (float)heightSegments; num8 += 1f)
		{
			float num9 = 0f;
			if (flag)
			{
				num9 = num6 / 2f;
			}
			for (float num10 = 0f; num10 < (float)widthSegments; num10 += 1f)
			{
				array[num3] = new Vector3(num10 * num6 - width / 2f - zero.x + num9, (num8 + 2f) * num7 - height / 2f - zero.y, 0f);
				array2[num3] = new Vector2(num10 * num4, num8 * num5);
				array[num3 + 1] = new Vector3(num10 * num6 - width / 2f - zero.x + num9 - num6 / 2f, (num8 + 2f) * num7 - height / 2f - zero.y - num7, 0f);
				array2[num3 + 1] = new Vector2(num10 * num4, num8 * num5);
				array[num3 + 2] = new Vector3(num10 * num6 - width / 2f - zero.x + num9 + num6 / 2f, (num8 + 2f) * num7 - height / 2f - zero.y - num7, 0f);
				array2[num3 + 2] = new Vector2(num10 * num4, num8 * num5);
				array[num3 + 3] = new Vector3(num10 * num6 - width / 2f - zero.x + num9, (num8 + 2f) * num7 - height / 2f - zero.y - num7 * 2f, 0f);
				array2[num3 + 3] = new Vector2(num10 * num4, num8 * num5);
				num3 += 4;
			}
			flag = !flag;
		}
		num3 = 0;
		int num11 = 0;
		for (int i = 0; i < heightSegments; i++)
		{
			for (int j = 0; j < widthSegments; j++)
			{
				array3[num3] = num11;
				array3[num3 + 1] = num11 + 1;
				array3[num3 + 2] = num11 + 2;
				array3[num3 + 3] = num11 + 1;
				array3[num3 + 4] = num11 + 3;
				array3[num3 + 5] = num11 + 2;
				num3 += 6;
				num11 += 4;
			}
		}
		mesh.vertices = array;
		mesh.uv = array2;
		mesh.triangles = array3;
		mesh.RecalculateNormals();
		meshFilter.sharedMesh = mesh;
		mesh.RecalculateBounds();
		if (addCollider)
		{
			gameObject.AddComponent(typeof(BoxCollider));
		}
		return gameObject;
	}

	// Token: 0x06000ADD RID: 2781 RVA: 0x00046680 File Offset: 0x00044880
	public static GameObject CreateIsoPlane(float width, float height, int widthSegments, int heightSegments, Geometry.Plane2AnchorPoint anchor = Geometry.Plane2AnchorPoint.BottomLeft, string optionalName = null, bool addCollider = false)
	{
		GameObject gameObject = new GameObject();
		if (!string.IsNullOrEmpty(optionalName))
		{
			gameObject.name = optionalName;
		}
		else
		{
			gameObject.name = "IsoPlane";
		}
		gameObject.transform.position = Vector3.zero;
		Vector2 zero;
		switch (anchor)
		{
		case Geometry.Plane2AnchorPoint.TopLeft:
			zero = new Vector2(-width / 2f, height / 2f);
			goto IL_0126;
		case Geometry.Plane2AnchorPoint.TopHalf:
			zero = new Vector2(0f, height / 2f);
			goto IL_0126;
		case Geometry.Plane2AnchorPoint.TopRight:
			zero = new Vector2(width / 2f, height / 2f);
			goto IL_0126;
		case Geometry.Plane2AnchorPoint.RightHalf:
			zero = new Vector2(width / 2f, 0f);
			goto IL_0126;
		case Geometry.Plane2AnchorPoint.BottomRight:
			zero = new Vector2(width / 2f, -height / 2f);
			goto IL_0126;
		case Geometry.Plane2AnchorPoint.BottomHalf:
			zero = new Vector2(0f, -height / 2f);
			goto IL_0126;
		case Geometry.Plane2AnchorPoint.BottomLeft:
			zero = new Vector2(-width / 2f, -height / 2f);
			goto IL_0126;
		case Geometry.Plane2AnchorPoint.LeftHalf:
			zero = new Vector2(-width / 2f, 0f);
			goto IL_0126;
		}
		zero = Vector2.zero;
		IL_0126:
		MeshFilter meshFilter = (MeshFilter)gameObject.AddComponent(typeof(MeshFilter));
		gameObject.AddComponent(typeof(MeshRenderer));
		Mesh mesh = new Mesh();
		mesh.name = gameObject.name;
		int num = widthSegments + 1;
		int num2 = heightSegments + 2;
		int num3 = widthSegments * heightSegments * 6;
		int num4 = num * num2;
		Vector3[] array = new Vector3[num4];
		Vector2[] array2 = new Vector2[num4];
		int[] array3 = new int[num3];
		int num5 = 0;
		float num6 = 1f / (float)widthSegments;
		float num7 = 1f / (float)heightSegments;
		float num8 = width / (float)widthSegments;
		float num9 = height / (float)heightSegments / 2f;
		bool flag = false;
		for (float num10 = 0f; num10 < (float)num2; num10 += 1f)
		{
			float num11 = 0f;
			if (flag)
			{
				num11 = num8 / 2f;
			}
			for (float num12 = 0f; num12 < (float)num; num12 += 1f)
			{
				array[num5] = new Vector3(num12 * num8 - width / 2f - zero.x - num11, num10 * num9 - height / 2f - zero.y, 0f);
				array2[num5] = new Vector2(num12 * num6, num10 * num7);
				num5++;
			}
			flag = !flag;
		}
		num5 = 0;
		for (int i = 0; i < heightSegments; i++)
		{
			for (int j = 0; j < widthSegments; j++)
			{
				if (i % 2 == 0)
				{
					int num13 = i * num + j;
					array3[num5] = num13;
					array3[num5 + 1] = num13 + num;
					array3[num5 + 2] = num13 + num + 1;
					array3[num5 + 3] = num13 + num;
					array3[num5 + 4] = (i + 2) * num + j;
					array3[num5 + 5] = num13 + num + 1;
				}
				else
				{
					int num14 = i * num + j + 1;
					array3[num5] = num14;
					array3[num5 + 1] = num14 + num - 1;
					array3[num5 + 2] = num14 + num;
					array3[num5 + 3] = num14 + num - 1;
					array3[num5 + 4] = (i + 2) * num + 1 + j;
					array3[num5 + 5] = num14 + num;
				}
				num5 += 6;
			}
		}
		mesh.vertices = array;
		mesh.uv = array2;
		mesh.triangles = array3;
		mesh.RecalculateNormals();
		meshFilter.sharedMesh = mesh;
		mesh.RecalculateBounds();
		if (addCollider)
		{
			gameObject.AddComponent(typeof(BoxCollider));
		}
		return gameObject;
	}

	// Token: 0x06000ADE RID: 2782 RVA: 0x00046A24 File Offset: 0x00044C24
	public static GameObject CreatePlane2(out Vector3[] vertices, float width, float length, int widthSegments, int lengthSegments, Geometry.Plane2AnchorPoint anchor = Geometry.Plane2AnchorPoint.BottomLeft, Geometry.Plane2Orientation orientation = Geometry.Plane2Orientation.Vertical, string optionalName = null, bool addCollider = false)
	{
		GameObject gameObject = new GameObject();
		if (!string.IsNullOrEmpty(optionalName))
		{
			gameObject.name = optionalName;
		}
		else
		{
			gameObject.name = "Plane";
		}
		gameObject.transform.position = Vector3.zero;
		Vector2 zero;
		switch (anchor)
		{
		case Geometry.Plane2AnchorPoint.TopLeft:
			zero = new Vector2(-width / 2f, length / 2f);
			goto IL_0126;
		case Geometry.Plane2AnchorPoint.TopHalf:
			zero = new Vector2(0f, length / 2f);
			goto IL_0126;
		case Geometry.Plane2AnchorPoint.TopRight:
			zero = new Vector2(width / 2f, length / 2f);
			goto IL_0126;
		case Geometry.Plane2AnchorPoint.RightHalf:
			zero = new Vector2(width / 2f, 0f);
			goto IL_0126;
		case Geometry.Plane2AnchorPoint.BottomRight:
			zero = new Vector2(width / 2f, -length / 2f);
			goto IL_0126;
		case Geometry.Plane2AnchorPoint.BottomHalf:
			zero = new Vector2(0f, -length / 2f);
			goto IL_0126;
		case Geometry.Plane2AnchorPoint.BottomLeft:
			zero = new Vector2(-width / 2f, -length / 2f);
			goto IL_0126;
		case Geometry.Plane2AnchorPoint.LeftHalf:
			zero = new Vector2(-width / 2f, 0f);
			goto IL_0126;
		}
		zero = Vector2.zero;
		IL_0126:
		MeshFilter meshFilter = (MeshFilter)gameObject.AddComponent(typeof(MeshFilter));
		gameObject.AddComponent(typeof(MeshRenderer));
		Mesh mesh = new Mesh();
		mesh.name = gameObject.name;
		int num = widthSegments + 1;
		int num2 = lengthSegments + 1;
		int num3 = widthSegments * lengthSegments * 6;
		int num4 = num * num2;
		vertices = new Vector3[num4];
		Vector2[] array = new Vector2[num4];
		Color[] array2 = new Color[num4];
		int[] array3 = new int[num3];
		int num5 = 0;
		float num6 = 1f / (float)widthSegments;
		float num7 = 1f / (float)lengthSegments;
		float num8 = width / (float)widthSegments;
		float num9 = length / (float)lengthSegments;
		for (float num10 = 0f; num10 < (float)num2; num10 += 1f)
		{
			for (float num11 = 0f; num11 < (float)num; num11 += 1f)
			{
				if (orientation == Geometry.Plane2Orientation.Horizontal)
				{
					vertices[num5] = new Vector3(num11 * num8 - width / 2f - zero.x, 0f, num10 * num9 - length / 2f - zero.y);
				}
				else
				{
					vertices[num5] = new Vector3(num11 * num8 - width / 2f - zero.x, num10 * num9 - length / 2f - zero.y, 0f);
				}
				array2[num5] = new Color(1f, 1f, 1f, 1f);
				array[num5++] = new Vector2(num11 * num6, num10 * num7);
			}
		}
		num5 = 0;
		for (int i = 0; i < lengthSegments; i++)
		{
			for (int j = 0; j < widthSegments; j++)
			{
				array3[num5] = i * num + j;
				array3[num5 + 1] = (i + 1) * num + j;
				array3[num5 + 2] = i * num + j + 1;
				array3[num5 + 3] = (i + 1) * num + j;
				array3[num5 + 4] = (i + 1) * num + j + 1;
				array3[num5 + 5] = i * num + j + 1;
				num5 += 6;
			}
		}
		mesh.vertices = vertices;
		mesh.colors = array2;
		mesh.uv = array;
		mesh.triangles = array3;
		mesh.RecalculateNormals();
		meshFilter.sharedMesh = mesh;
		mesh.RecalculateBounds();
		if (addCollider)
		{
			gameObject.AddComponent(typeof(BoxCollider));
		}
		return gameObject;
	}

	// Token: 0x06000ADF RID: 2783 RVA: 0x00046DC8 File Offset: 0x00044FC8
	public static Mesh CreateQuadMesh(float Size)
	{
		Mesh mesh = new Mesh();
		Vector3[] array = new Vector3[]
		{
			new Vector3(Size, Size, 0f),
			new Vector3(Size, -Size, 0f),
			new Vector3(-Size, Size, 0f),
			new Vector3(-Size, -Size, 0f)
		};
		Vector2[] array2 = new Vector2[]
		{
			new Vector2(1f, 1f),
			new Vector2(1f, 0f),
			new Vector2(0f, 1f),
			new Vector2(0f, 0f)
		};
		int[] array3 = new int[] { 0, 1, 2, 2, 1, 3 };
		mesh.vertices = array;
		mesh.uv = array2;
		mesh.triangles = array3;
		mesh.RecalculateNormals();
		return mesh;
	}

	// Token: 0x06000AE0 RID: 2784 RVA: 0x00046EC0 File Offset: 0x000450C0
	public static Mesh CreatePlane(int tileHeight, int tileWidth, int gridHeight, int gridWidth)
	{
		Mesh mesh = new Mesh();
		float num = 1f;
		float num2 = 1f;
		List<Vector3> list = new List<Vector3>();
		List<int> list2 = new List<int>();
		List<Vector3> list3 = new List<Vector3>();
		List<Color> list4 = new List<Color>();
		List<Vector2> list5 = new List<Vector2>();
		int num3 = 0;
		for (int i = 0; i < gridWidth; i++)
		{
			for (int j = 0; j < gridHeight; j++)
			{
				Geometry.AddVertices(tileHeight, tileWidth, j, i, list);
				num3 = Geometry.AddTriangles(num3, list2);
				Geometry.AddNormals(list3);
				Geometry.AddColors(list4);
				Geometry.AddUvs(0, num2, num, list5, 0);
			}
		}
		mesh.vertices = list.ToArray();
		mesh.normals = list3.ToArray();
		mesh.triangles = list2.ToArray();
		mesh.colors = list4.ToArray();
		mesh.uv = list5.ToArray();
		mesh.RecalculateNormals();
		return mesh;
	}

	// Token: 0x06000AE1 RID: 2785 RVA: 0x00046F9C File Offset: 0x0004519C
	private static void AddVertices(int tileHeight, int tileWidth, int y, int x, ICollection<Vector3> vertices)
	{
		vertices.Add(new Vector3((float)(x * tileWidth), (float)(y * tileHeight), 0f));
		vertices.Add(new Vector3((float)(x * tileWidth + tileWidth), (float)(y * tileHeight), 0f));
		vertices.Add(new Vector3((float)(x * tileWidth + tileWidth), (float)(y * tileHeight + tileHeight), 0f));
		vertices.Add(new Vector3((float)(x * tileWidth), (float)(y * tileHeight + tileHeight), 0f));
	}

	// Token: 0x06000AE2 RID: 2786 RVA: 0x00047015 File Offset: 0x00045215
	private static int AddTriangles(int index, ICollection<int> triangles)
	{
		triangles.Add(index + 2);
		triangles.Add(index + 1);
		triangles.Add(index);
		triangles.Add(index);
		triangles.Add(index + 3);
		triangles.Add(index + 2);
		index += 4;
		return index;
	}

	// Token: 0x06000AE3 RID: 2787 RVA: 0x00047050 File Offset: 0x00045250
	private static void AddColors(ICollection<Color> colors)
	{
		colors.Add(new Color(0.2f, 0.3f, 0.4f, 0.9f));
		colors.Add(new Color(0.2f, 0.3f, 0.4f, 0.9f));
		colors.Add(new Color(0.2f, 0.3f, 0.4f, 0.9f));
		colors.Add(new Color(0.2f, 0.3f, 0.4f, 0.9f));
	}

	// Token: 0x06000AE4 RID: 2788 RVA: 0x000470D9 File Offset: 0x000452D9
	private static void AddNormals(ICollection<Vector3> normals)
	{
		normals.Add(Vector3.forward);
		normals.Add(Vector3.forward);
		normals.Add(Vector3.forward);
		normals.Add(Vector3.forward);
	}

	// Token: 0x06000AE5 RID: 2789 RVA: 0x00047108 File Offset: 0x00045308
	private static void AddUvs(int tileRow, float tileSizeY, float tileSizeX, ICollection<Vector2> uvs, int tileColumn)
	{
		uvs.Add(new Vector2((float)tileColumn * tileSizeX, (float)tileRow * tileSizeY));
		uvs.Add(new Vector2((float)(tileColumn + 1) * tileSizeX, (float)tileRow * tileSizeY));
		uvs.Add(new Vector2((float)(tileColumn + 1) * tileSizeX, (float)(tileRow + 1) * tileSizeY));
		uvs.Add(new Vector2((float)tileColumn * tileSizeX, (float)(tileRow + 1) * tileSizeY));
	}

	// Token: 0x020015E6 RID: 5606
	public enum Plane2Orientation
	{
		// Token: 0x04005846 RID: 22598
		Horizontal,
		// Token: 0x04005847 RID: 22599
		Vertical
	}

	// Token: 0x020015E7 RID: 5607
	public enum Plane2AnchorPoint
	{
		// Token: 0x04005849 RID: 22601
		TopLeft,
		// Token: 0x0400584A RID: 22602
		TopHalf,
		// Token: 0x0400584B RID: 22603
		TopRight,
		// Token: 0x0400584C RID: 22604
		RightHalf,
		// Token: 0x0400584D RID: 22605
		BottomRight,
		// Token: 0x0400584E RID: 22606
		BottomHalf,
		// Token: 0x0400584F RID: 22607
		BottomLeft,
		// Token: 0x04005850 RID: 22608
		LeftHalf,
		// Token: 0x04005851 RID: 22609
		Center
	}
}
