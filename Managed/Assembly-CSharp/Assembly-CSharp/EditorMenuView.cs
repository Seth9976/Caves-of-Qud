using System;
using Qud.UI;
using QupKit;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x0200008D RID: 141
public class EditorMenuView : BaseView
{
	// Token: 0x060004F8 RID: 1272 RVA: 0x00020EAC File Offset: 0x0001F0AC
	public override void OnCreate()
	{
		base.AddChild(new ButtonControl("SelectMapEditor", "Map Editor", 300, 150, delegate(PointerEventData D)
		{
			UIManager.getWindow("MapEditor").Show();
		}), new ControlLayout(ControlAnchor.Middle, new Vector3(0f, 0f, 0f)));
	}

	// Token: 0x040003D8 RID: 984
	public static EditorMenuView Instance;
}
