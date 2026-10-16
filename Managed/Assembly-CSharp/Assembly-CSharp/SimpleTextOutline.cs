using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x02000129 RID: 297
public class SimpleTextOutline : MonoBehaviour
{
	// Token: 0x06000B58 RID: 2904 RVA: 0x0004B89C File Offset: 0x00049A9C
	private void LateUpdate()
	{
		if (this.shadowmeshes.Count == 0)
		{
			this.textMesh = base.GetComponent<TextMesh>();
			this.meshRenderer = base.GetComponent<MeshRenderer>();
			for (int i = 0; i < 8; i++)
			{
				GameObject gameObject = new GameObject("outline " + i.ToString(), new Type[] { typeof(TextMesh) });
				gameObject.transform.parent = base.transform;
				gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				MeshRenderer component = gameObject.GetComponent<MeshRenderer>();
				component.material = new Material(this.meshRenderer.material);
				component.shadowCastingMode = ShadowCastingMode.Off;
				component.receiveShadows = false;
				component.sortingLayerID = this.meshRenderer.sortingLayerID;
				component.sortingLayerName = this.meshRenderer.sortingLayerName;
				this.shadowmeshes.Add(gameObject.GetComponent<TextMesh>());
				this.renderers.Add(component);
			}
		}
		if (this.lastString == this.textMesh.text)
		{
			if (this.lastAlpha != this.textMesh.color.a)
			{
				foreach (TextMesh textMesh in this.shadowmeshes)
				{
				}
				this.lastAlpha = this.textMesh.color.a;
			}
			return;
		}
		this.lastString = this.textMesh.text;
		this.lastAlpha = this.textMesh.color.a;
		Vector3 vector = Camera.main.WorldToScreenPoint(base.transform.position);
		this.outlineColor.a = this.textMesh.color.a * this.textMesh.color.a;
		for (int j = 0; j < this.shadowmeshes.Count; j++)
		{
			TextMesh textMesh2 = this.shadowmeshes[j];
			textMesh2.color = this.outlineColor;
			textMesh2.text = this.textMesh.text;
			textMesh2.alignment = this.textMesh.alignment;
			textMesh2.anchor = this.textMesh.anchor;
			textMesh2.characterSize = this.textMesh.characterSize;
			textMesh2.font = this.textMesh.font;
			textMesh2.fontSize = this.textMesh.fontSize;
			textMesh2.fontStyle = this.textMesh.fontStyle;
			textMesh2.richText = this.textMesh.richText;
			textMesh2.tabSize = this.textMesh.tabSize;
			textMesh2.lineSpacing = this.textMesh.lineSpacing;
			textMesh2.offsetZ = this.textMesh.offsetZ;
			bool flag = this.resolutionDependant && (Screen.width > this.doubleResolution || Screen.height > this.doubleResolution);
			Vector3 vector2 = this.GetOffset(j) * (flag ? (2f * this.pixelSize) : this.pixelSize);
			Vector3 vector3 = Camera.main.ScreenToWorldPoint(vector + vector2);
			textMesh2.transform.position = vector3;
			MeshRenderer meshRenderer = this.renderers[j];
			meshRenderer.sortingLayerID = this.meshRenderer.sortingLayerID;
			meshRenderer.sortingLayerName = this.meshRenderer.sortingLayerName;
		}
	}

	// Token: 0x06000B59 RID: 2905 RVA: 0x0004BC2C File Offset: 0x00049E2C
	private Vector3 GetOffset(int i)
	{
		switch (i % 8)
		{
		case 0:
			return new Vector3(0f, 1f, 1f);
		case 1:
			return new Vector3(1f, 1f, 1f);
		case 2:
			return new Vector3(1f, 0f, 1f);
		case 3:
			return new Vector3(1f, -1f, 1f);
		case 4:
			return new Vector3(0f, -1f, 1f);
		case 5:
			return new Vector3(-1f, -1f, 1f);
		case 6:
			return new Vector3(-1f, 0f, 1f);
		case 7:
			return new Vector3(-1f, 1f, 1f);
		default:
			return Vector3.zero;
		}
	}

	// Token: 0x040007D5 RID: 2005
	public float pixelSize = 1f;

	// Token: 0x040007D6 RID: 2006
	public Color outlineColor = Color.black;

	// Token: 0x040007D7 RID: 2007
	public bool resolutionDependant;

	// Token: 0x040007D8 RID: 2008
	public int doubleResolution = 1024;

	// Token: 0x040007D9 RID: 2009
	private TextMesh textMesh;

	// Token: 0x040007DA RID: 2010
	private MeshRenderer meshRenderer;

	// Token: 0x040007DB RID: 2011
	private List<TextMesh> shadowmeshes = new List<TextMesh>();

	// Token: 0x040007DC RID: 2012
	private List<MeshRenderer> renderers = new List<MeshRenderer>();

	// Token: 0x040007DD RID: 2013
	private string lastString;

	// Token: 0x040007DE RID: 2014
	private float lastAlpha = -1f;
}
