using System;
using EnhancedUI.EnhancedScroller;
using Overlay.MapEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XRL.EditorFormats.Map;
using XRL.World;

// Token: 0x02000089 RID: 137
public class BlueprintCellView : EnhancedScrollerCellView, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x060004E1 RID: 1249 RVA: 0x00020954 File Offset: 0x0001EB54
	public void SetData(BlueprintScrollerData data)
	{
		this.blueprintID.text = data.ID;
		this.RefreshCellView();
	}

	// Token: 0x060004E2 RID: 1250 RVA: 0x00020970 File Offset: 0x0001EB70
	public override void RefreshCellView()
	{
		Button component = base.gameObject.GetComponent<Button>();
		ColorBlock colors = component.colors;
		string text = (this.mapEditorView.IsSingleObjectBrush() ? this.mapEditorView.GetBrushText() : "");
		if (this.blueprintID.text == text)
		{
			colors.normalColor = new Color(0f, 1f, 0f, 0.25f);
			BlueprintCellView.selectedCell = this;
		}
		else
		{
			colors.normalColor = new Color(0f, 0f, 0f, 0f);
		}
		MapFileRegion mapFileRegion = new MapFileRegion(1, 1);
		mapFileRegion.GetOrCreateCellAt(0, 0).Objects.Add(new MapFileObjectBlueprint(this.blueprintID.text, null, null));
		MapFileCellRender mapFileCellRender = new MapFileCellRender();
		mapFileCellRender.RenderBlueprint(GameObjectFactory.Factory.Blueprints[this.blueprintID.text], new MapFileCellReference(mapFileRegion, 0, 0, null));
		mapFileCellRender.To3C(this.image);
		component.colors = colors;
	}

	// Token: 0x17000088 RID: 136
	// (get) Token: 0x060004E3 RID: 1251 RVA: 0x00020A77 File Offset: 0x0001EC77
	private MapEditorView mapEditorView
	{
		get
		{
			return MapEditorView.instance;
		}
	}

	// Token: 0x060004E4 RID: 1252 RVA: 0x00020A7E File Offset: 0x0001EC7E
	public void OnSelected()
	{
		MapEditorView.instance.SetSelectedBlueprint(this.blueprintID.text);
	}

	// Token: 0x17000089 RID: 137
	// (get) Token: 0x060004E5 RID: 1253 RVA: 0x00020A95 File Offset: 0x0001EC95
	private global::UnityEngine.GameObject blueprintReadout
	{
		get
		{
			if (this._blueprintReadout == null)
			{
				this._blueprintReadout = this.mapEditorView.FindChild("BlueprintReadout");
			}
			return this._blueprintReadout;
		}
	}

	// Token: 0x060004E6 RID: 1254 RVA: 0x00020AC4 File Offset: 0x0001ECC4
	public void Update()
	{
		if (this.pointerIn)
		{
			if (!RectTransformUtility.RectangleContainsScreenPoint(base.gameObject.GetComponent<RectTransform>(), Input.mousePosition))
			{
				this.pointerIn = false;
				this.blueprintReadout.SetActive(false);
				return;
			}
			if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
			{
				if (!this.blueprintReadout.activeInHierarchy)
				{
					this.blueprintReadout.SetActive(true);
					this.mapEditorView.FindChild("BlueprintReadout/ReadoutText").SetActive(true);
					this.mapEditorView.FindChild("BlueprintReadout/ReadoutText").GetComponent<Text>().text = GameObjectFactory.Factory.Blueprints[this.blueprintID.text].BlueprintXML();
					return;
				}
			}
			else if (this.blueprintReadout.activeInHierarchy)
			{
				this.blueprintReadout.SetActive(false);
			}
		}
	}

	// Token: 0x060004E7 RID: 1255 RVA: 0x00020BA8 File Offset: 0x0001EDA8
	public void OnPointerEnter(PointerEventData data)
	{
		this.pointerIn = true;
		if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
		{
			this.blueprintReadout.SetActive(true);
			this.mapEditorView.FindChild("BlueprintReadout/ReadoutText").SetActive(true);
			this.mapEditorView.FindChild("BlueprintReadout/ReadoutText").GetComponent<Text>().text = GameObjectFactory.Factory.Blueprints[this.blueprintID.text].BlueprintXML();
		}
	}

	// Token: 0x060004E8 RID: 1256 RVA: 0x00020C2F File Offset: 0x0001EE2F
	public void OnPointerExit(PointerEventData data)
	{
		this.pointerIn = false;
		this.blueprintReadout.SetActive(false);
	}

	// Token: 0x040003CC RID: 972
	public Text blueprintID;

	// Token: 0x040003CD RID: 973
	public UIThreeColorProperties image;

	// Token: 0x040003CE RID: 974
	public static BlueprintCellView selectedCell;

	// Token: 0x040003CF RID: 975
	private global::UnityEngine.GameObject _blueprintReadout;

	// Token: 0x040003D0 RID: 976
	private bool pointerIn;
}
