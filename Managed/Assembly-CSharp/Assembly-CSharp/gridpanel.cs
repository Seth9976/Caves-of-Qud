using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000133 RID: 307
public class gridpanel : MonoBehaviour
{
	// Token: 0x06000BA7 RID: 2983 RVA: 0x0004E459 File Offset: 0x0004C659
	public string GetTileTypeName()
	{
		if (this.m_TileType >= 0 && this.m_TileType < gridpanel.typeList.Length)
		{
			return gridpanel.typeList[this.m_TileType];
		}
		return "invalid";
	}

	// Token: 0x06000BA8 RID: 2984 RVA: 0x0004E485 File Offset: 0x0004C685
	public static string GetTileTypeName(int tileType)
	{
		if (tileType >= 0 && tileType < gridpanel.typeList.Length)
		{
			return gridpanel.typeList[tileType];
		}
		return "invalid";
	}

	// Token: 0x06000BA9 RID: 2985 RVA: 0x0004E4A2 File Offset: 0x0004C6A2
	private void SetBGType(int bgIndex = 0)
	{
		this.m_BGType = bgIndex;
		if (this.m_Button != null)
		{
			this.m_Button.GetComponent<Image>().sprite = this.m_BGTexture[bgIndex];
		}
	}

	// Token: 0x06000BAA RID: 2986 RVA: 0x0004E4D4 File Offset: 0x0004C6D4
	public void SetTileType(int tileType)
	{
		this.m_TileType = tileType;
		if (this.m_Tile == null)
		{
			this.m_Tile = global::UnityEngine.Object.Instantiate(Resources.Load("Button")) as GameObject;
			this.m_Tile.transform.SetParent(base.transform, false);
			this.m_Tile.transform.SetAsFirstSibling();
			this.m_AccessibilityHelper = this.m_Tile.GetComponentInChildren<UAP_BaseElement>();
			this.m_AccessibilityHelper.m_TryToReadLabel = false;
			this.m_Button = this.m_Tile.GetComponentInChildren<Button>();
			this.m_Button.onClick.AddListener(new UnityAction(this.OnButtonPress));
			EventTrigger eventTrigger = this.m_Button.gameObject.AddComponent<EventTrigger>();
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.EndDrag;
			entry.callback.AddListener(delegate(BaseEventData data)
			{
				this.OnDragEndDelegate((PointerEventData)data);
			});
			eventTrigger.triggers.Add(entry);
			EventTrigger.Entry entry2 = new EventTrigger.Entry();
			entry2.eventID = EventTriggerType.Drag;
			entry2.callback.AddListener(delegate(BaseEventData data)
			{
				this.OnDragUpdateDelegate((PointerEventData)data);
			});
			eventTrigger.triggers.Add(entry2);
			EventTrigger.Entry entry3 = new EventTrigger.Entry();
			entry3.eventID = EventTriggerType.PointerDown;
			entry3.callback.AddListener(delegate(BaseEventData data)
			{
				this.OnPointerDownDelegate((PointerEventData)data);
			});
			eventTrigger.triggers.Add(entry3);
			EventTrigger.Entry entry4 = new EventTrigger.Entry();
			entry4.eventID = EventTriggerType.PointerUp;
			entry4.callback.AddListener(delegate(BaseEventData data)
			{
				this.OnPointerUpDelegate((PointerEventData)data);
			});
			eventTrigger.triggers.Add(entry4);
			this.SetBGType(this.m_BGType);
		}
		if (tileType < 0)
		{
			this.m_AccessibilityHelper.m_Text = "none";
			this.m_GemImage.sprite = null;
			this.m_GemImage.gameObject.SetActive(false);
			return;
		}
		this.m_AccessibilityHelper.m_Text = this.GetTileTypeName();
		this.m_GemImage.sprite = this.m_GemTextures[tileType];
		this.m_GemImage.gameObject.SetActive(true);
		this.m_Button.gameObject.name = this.GetTileTypeName();
		base.gameObject.name = this.GetTileTypeName();
	}

	// Token: 0x06000BAB RID: 2987 RVA: 0x0004E6EE File Offset: 0x0004C8EE
	public int GetTileType()
	{
		return this.m_TileType;
	}

	// Token: 0x06000BAC RID: 2988 RVA: 0x0004E6F8 File Offset: 0x0004C8F8
	public void SetIndex(int index, int xWidth)
	{
		this.m_Index = index;
		this.m_xWidth = xWidth;
		if (xWidth % 2 == 1)
		{
			this.SetBGType(index % 2);
			return;
		}
		int i = index;
		bool flag = true;
		while (i >= xWidth)
		{
			flag = !flag;
			i -= xWidth;
		}
		if (flag)
		{
			index++;
		}
		this.SetBGType(index % 2);
	}

	// Token: 0x06000BAD RID: 2989 RVA: 0x0004E747 File Offset: 0x0004C947
	public int GetIndex()
	{
		return this.m_Index;
	}

	// Token: 0x06000BAE RID: 2990 RVA: 0x0004E74F File Offset: 0x0004C94F
	public void OnPointerDownDelegate(PointerEventData data)
	{
		if (UAP_AccessibilityManager.IsEnabled())
		{
			return;
		}
		gameplay.Instance.ActivateTile(this.m_Index);
	}

	// Token: 0x06000BAF RID: 2991 RVA: 0x0004E76C File Offset: 0x0004C96C
	public void OnPointerUpDelegate(PointerEventData data)
	{
		this.m_LastPreviewDir = gridpanel.EVecDir.None;
		if (UAP_AccessibilityManager.IsEnabled())
		{
			return;
		}
		if (((data.pointerEnter != null) ? base.gameObject.GetComponent<gridpanel>() : null) == this && !data.dragging)
		{
			gameplay.Instance.ActivateTile(this.m_Index);
		}
	}

	// Token: 0x06000BB0 RID: 2992 RVA: 0x0004E7C4 File Offset: 0x0004C9C4
	public void OnDragUpdateDelegate(PointerEventData data)
	{
		if (UAP_AccessibilityManager.IsEnabled())
		{
			return;
		}
		Vector2 normalized = (data.position - data.pressPosition).normalized;
		gridpanel.EVecDir evecDir = this.GetVectorDirection(normalized);
		if (this.IsSameTile(data.pointerEnter))
		{
			evecDir = gridpanel.EVecDir.None;
		}
		if (evecDir == this.m_LastPreviewDir)
		{
			return;
		}
		this.m_LastPreviewDir = evecDir;
		if (evecDir != gridpanel.EVecDir.None)
		{
			gameplay.Instance.PreviewDrag(this.m_Index, this.GetNeighbourIndex(evecDir));
			return;
		}
		gameplay.Instance.CancelPreview(false);
	}

	// Token: 0x06000BB1 RID: 2993 RVA: 0x0004E844 File Offset: 0x0004CA44
	public void OnDragEndDelegate(PointerEventData data)
	{
		if (UAP_AccessibilityManager.IsEnabled())
		{
			return;
		}
		Vector2 normalized = (data.position - data.pressPosition).normalized;
		gridpanel.EVecDir vectorDirection = this.GetVectorDirection(normalized);
		if (this.IsSameTile(data.pointerEnter))
		{
			gameplay.Instance.ActivateTile(this.m_Index);
			return;
		}
		gameplay.Instance.ActivateTile(this.GetNeighbourIndex(vectorDirection));
	}

	// Token: 0x06000BB2 RID: 2994 RVA: 0x0004E8AC File Offset: 0x0004CAAC
	private bool IsSameTile(GameObject pointerEnter)
	{
		if (pointerEnter != null && pointerEnter.transform.parent != null)
		{
			gridpanel component = pointerEnter.transform.parent.gameObject.GetComponent<gridpanel>();
			if (component != null && component == this)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000BB3 RID: 2995 RVA: 0x0004E904 File Offset: 0x0004CB04
	private int GetNeighbourIndex(gridpanel.EVecDir dir)
	{
		switch (dir)
		{
		case gridpanel.EVecDir.Up:
			return this.m_Index - this.m_xWidth;
		case gridpanel.EVecDir.Left:
			return this.m_Index - 1;
		case gridpanel.EVecDir.Right:
			return this.m_Index + 1;
		case gridpanel.EVecDir.Down:
			return this.m_Index + this.m_xWidth;
		default:
			return this.m_Index;
		}
	}

	// Token: 0x06000BB4 RID: 2996 RVA: 0x0004E95F File Offset: 0x0004CB5F
	private gridpanel.EVecDir GetVectorDirection(Vector2 vector)
	{
		if (Mathf.Abs(vector.x) > Mathf.Abs(vector.y))
		{
			if (vector.x > 0f)
			{
				return gridpanel.EVecDir.Right;
			}
			return gridpanel.EVecDir.Left;
		}
		else
		{
			if (vector.y > 0f)
			{
				return gridpanel.EVecDir.Up;
			}
			return gridpanel.EVecDir.Down;
		}
	}

	// Token: 0x06000BB5 RID: 2997 RVA: 0x0004E99A File Offset: 0x0004CB9A
	public void OnButtonPress()
	{
		if (!UAP_AccessibilityManager.IsEnabled())
		{
			return;
		}
		gameplay.Instance.ActivateTile(this.m_Index);
	}

	// Token: 0x04000848 RID: 2120
	public Image m_GemImage;

	// Token: 0x04000849 RID: 2121
	public Sprite[] m_BGTexture = new Sprite[2];

	// Token: 0x0400084A RID: 2122
	private GameObject m_Tile;

	// Token: 0x0400084B RID: 2123
	private UAP_BaseElement m_AccessibilityHelper;

	// Token: 0x0400084C RID: 2124
	private Button m_Button;

	// Token: 0x0400084D RID: 2125
	private int m_Index = -1;

	// Token: 0x0400084E RID: 2126
	private int m_xWidth = -1;

	// Token: 0x0400084F RID: 2127
	private int m_TileType = -1;

	// Token: 0x04000850 RID: 2128
	private static string[] typeList = new string[] { "Ruby", "Blue", "Emerald", "Purple", "Gold", "Orange", "Crystal" };

	// Token: 0x04000851 RID: 2129
	public static int tileTypeCount = 7;

	// Token: 0x04000852 RID: 2130
	public Sprite[] m_GemTextures = new Sprite[gridpanel.tileTypeCount];

	// Token: 0x04000853 RID: 2131
	private int m_BGType;

	// Token: 0x04000854 RID: 2132
	private gridpanel.EVecDir m_LastPreviewDir;

	// Token: 0x020015EA RID: 5610
	private enum EVecDir
	{
		// Token: 0x0400585C RID: 22620
		None,
		// Token: 0x0400585D RID: 22621
		Up,
		// Token: 0x0400585E RID: 22622
		Left,
		// Token: 0x0400585F RID: 22623
		Right,
		// Token: 0x04005860 RID: 22624
		Down
	}
}
