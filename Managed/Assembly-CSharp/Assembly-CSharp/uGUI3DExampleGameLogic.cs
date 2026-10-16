using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200012F RID: 303
public class uGUI3DExampleGameLogic : MonoBehaviour
{
	// Token: 0x06000B71 RID: 2929 RVA: 0x0004C9AB File Offset: 0x0004ABAB
	private void Start()
	{
		this.CloseDetailsPanel();
	}

	// Token: 0x06000B72 RID: 2930 RVA: 0x0004C9B3 File Offset: 0x0004ABB3
	public void CloseDetailsPanel()
	{
		this.m_DetailPanel.SetActive(false);
		this.m_ObjectNameLabel.text = "";
	}

	// Token: 0x06000B73 RID: 2931 RVA: 0x0004C9D1 File Offset: 0x0004ABD1
	private void ShowObjectInfo(string detailsText1, string detailsText2)
	{
		this.m_DetailsLabel1.text = detailsText1;
		this.m_DetailsLabel2.text = detailsText2;
		this.m_DetailPanel.SetActive(true);
	}

	// Token: 0x06000B74 RID: 2932 RVA: 0x0004C9F7 File Offset: 0x0004ABF7
	public void ShowCylinderInfo()
	{
		this.ShowObjectInfo("A cylinder is a three dimensional shape with two round shapes at either end and two parallel lines connecting the round ends.", "An example of a cylinder is a can of tomato soup.");
	}

	// Token: 0x06000B75 RID: 2933 RVA: 0x0004CA09 File Offset: 0x0004AC09
	public void HighlightCylinder(bool highlighted, UAP_BaseElement.EHighlightSource source)
	{
		if (highlighted)
		{
			this.m_ObjectNameLabel.text = "Cylinder";
			return;
		}
		this.m_ObjectNameLabel.text = "";
	}

	// Token: 0x06000B76 RID: 2934 RVA: 0x0004CA2F File Offset: 0x0004AC2F
	public void ShowSphereInfo()
	{
		this.ShowObjectInfo("A sphere is a geometrical figure that is perfectly round, 3-dimensional and circular, with all points equidistant from a single point in space.", "An example of a sphere is a basketball.");
	}

	// Token: 0x06000B77 RID: 2935 RVA: 0x0004CA41 File Offset: 0x0004AC41
	public void HighlightSphere(bool highlighted, UAP_BaseElement.EHighlightSource source)
	{
		if (highlighted)
		{
			this.m_ObjectNameLabel.text = "Sphere";
			return;
		}
		this.m_ObjectNameLabel.text = "";
	}

	// Token: 0x06000B78 RID: 2936 RVA: 0x0004CA67 File Offset: 0x0004AC67
	public void ShowCubeInfo()
	{
		this.ShowObjectInfo("A cube is a three-dimensional solid object bounded by six square faces, facets or sides, with three meeting at each vertex.", "An example of a cube is a playing die in a game.");
	}

	// Token: 0x06000B79 RID: 2937 RVA: 0x0004CA79 File Offset: 0x0004AC79
	public void HighlightCube(bool highlighted, UAP_BaseElement.EHighlightSource source)
	{
		if (highlighted)
		{
			this.m_ObjectNameLabel.text = "Cube";
			return;
		}
		this.m_ObjectNameLabel.text = "";
	}

	// Token: 0x04000803 RID: 2051
	public GameObject m_DetailPanel;

	// Token: 0x04000804 RID: 2052
	public Text m_DetailsLabel1;

	// Token: 0x04000805 RID: 2053
	public Text m_DetailsLabel2;

	// Token: 0x04000806 RID: 2054
	public Text m_ObjectNameLabel;
}
