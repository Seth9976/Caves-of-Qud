using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000086 RID: 134
[RequireComponent(typeof(ScrollRect))]
public abstract class PooledScrollRect<T, Y> : MonoBehaviour, IPointerExitHandler, IEventSystemHandler, IPointerEnterHandler, ICollectionManagement<T> where Y : PooledScrollRectElement<T>
{
	// Token: 0x060004C4 RID: 1220 RVA: 0x0002017A File Offset: 0x0001E37A
	public T Get(int index)
	{
		return this._scrollListData[index];
	}

	// Token: 0x060004C5 RID: 1221 RVA: 0x00020188 File Offset: 0x0001E388
	public void Reflow()
	{
		if (this._scrollRect == null)
		{
			return;
		}
		if (!this.contentLayoutGroup.enabled)
		{
			if (!this.contentSizeFitter.enabled)
			{
				this.contentSizeFitter.enabled = true;
			}
			if (!this.contentLayoutGroup.enabled)
			{
				this.contentLayoutGroup.enabled = true;
			}
			this.update = 2;
			this.Reflowing = true;
		}
	}

	// Token: 0x060004C6 RID: 1222 RVA: 0x000201F4 File Offset: 0x0001E3F4
	public void Add(T value)
	{
		if (this.clearNextUpdate)
		{
			this._Clear();
			this.clearNextUpdate = false;
		}
		this.unlocked = false;
		this.forceLocked = this.forceLockCount;
		this.update = 2;
		if (this._scrollRect == null)
		{
			return;
		}
		if (!this.contentLayoutGroup.enabled)
		{
			if (!this.contentSizeFitter.enabled)
			{
				this.contentSizeFitter.enabled = true;
			}
			if (!this.contentLayoutGroup.enabled)
			{
				this.contentLayoutGroup.enabled = true;
			}
			this.update = 2;
		}
		Y y = default(Y);
		if (this.elements.Count < this.MaxElements)
		{
			y = global::UnityEngine.Object.Instantiate<Y>(this.Template);
			if (!y.gameObject.activeSelf)
			{
				y.gameObject.SetActive(true);
			}
			y.transform.SetParent(this._scrollRect.content, false);
		}
		else
		{
			y = this.elements.Dequeue();
			y.transform.SetAsLastSibling();
		}
		y.GetComponent<Y>().Setup(0, new List<T> { value });
		this.elements.Enqueue(y);
		this.ScrollToBot();
	}

	// Token: 0x17000086 RID: 134
	// (get) Token: 0x060004C7 RID: 1223 RVA: 0x0002033D File Offset: 0x0001E53D
	private RectTransform contentLayoutRect
	{
		get
		{
			if (this._contentLayoutRect == null)
			{
				this._contentLayoutRect = this.contentLayoutGroup.gameObject.GetComponent<RectTransform>();
			}
			return this._contentLayoutRect;
		}
	}

	// Token: 0x060004C8 RID: 1224 RVA: 0x0002036C File Offset: 0x0001E56C
	public void Update()
	{
		if (this.clearNextUpdate)
		{
			this._Clear();
			this.clearNextUpdate = false;
		}
		if (this.forceLocked > 0)
		{
			this.forceLocked--;
		}
		if (this.update > 0)
		{
			this.update--;
			if (this.update <= 0 && this.contentLayoutGroup != null && this.contentLayoutGroup.enabled)
			{
				this.contentSizeFitter.enabled = false;
				this.contentLayoutGroup.enabled = false;
			}
			if (this.Reflowing)
			{
				this.ScrollToBot();
			}
		}
		else
		{
			this.Reflowing = false;
		}
		if (!this.unlocked && !this.IsHovered)
		{
			this.ScrollToBot();
		}
	}

	// Token: 0x060004C9 RID: 1225 RVA: 0x00020425 File Offset: 0x0001E625
	public void RemoveAt(int index)
	{
		this._scrollListData.RemoveAt(index);
		this.ScrollToBot();
	}

	// Token: 0x060004CA RID: 1226 RVA: 0x00020439 File Offset: 0x0001E639
	public void RemoveRange(int index, int count)
	{
		this._scrollListData.RemoveRange(index, Math.Max(count, this._scrollListData.Count - index));
		this.ScrollToBot();
	}

	// Token: 0x060004CB RID: 1227 RVA: 0x00020460 File Offset: 0x0001E660
	public void Clear()
	{
		if (Thread.CurrentThread == GameManager.Instance.uiQueue.threadContext)
		{
			this._Clear();
			return;
		}
		this.clearNextUpdate = true;
	}

	// Token: 0x060004CC RID: 1228 RVA: 0x00020488 File Offset: 0x0001E688
	private void _Clear()
	{
		this._scrollListData.Clear();
		while (this.elements.Count > 0)
		{
			Y y = this.elements.Dequeue();
			y.transform.parent = null;
			y.gameObject.SetActive(false);
			global::UnityEngine.Object.Destroy(y.gameObject);
		}
	}

	// Token: 0x060004CD RID: 1229 RVA: 0x000204EC File Offset: 0x0001E6EC
	public void ScrollToBot()
	{
		if (this._scrollRect != null && this._scrollRect.normalizedPosition != Vector2.zero)
		{
			this._scrollRect.normalizedPosition = new Vector2(0f, 0f);
			this.unlocked = false;
		}
	}

	// Token: 0x17000087 RID: 135
	// (get) Token: 0x060004CE RID: 1230 RVA: 0x0002053F File Offset: 0x0001E73F
	protected int _totalNumElements
	{
		get
		{
			return this._scrollListData.Count;
		}
	}

	// Token: 0x060004CF RID: 1231 RVA: 0x0002054C File Offset: 0x0001E74C
	private void OnEnable()
	{
		if (this._scrollRect == null)
		{
			this._scrollRect = base.GetComponent<ScrollRect>();
		}
		if (this._spacer == null && this.doSpacer)
		{
			this._spacer = this.SpawnSpacer();
		}
		this._scrollRect.onValueChanged.AddListener(new UnityAction<Vector2>(this.ScrollMoved));
		this.ScrollMoved(Vector2.zero);
		this.unlocked = false;
		this.forceLocked = this.forceLockCount;
	}

	// Token: 0x060004D0 RID: 1232 RVA: 0x000205CF File Offset: 0x0001E7CF
	private void OnDisable()
	{
		this._scrollRect.onValueChanged.RemoveListener(new UnityAction<Vector2>(this.ScrollMoved));
	}

	// Token: 0x060004D1 RID: 1233 RVA: 0x000205ED File Offset: 0x0001E7ED
	private void ScrollMoved(Vector2 delta)
	{
		if (this.forceLocked <= 0)
		{
			this.unlocked = true;
		}
	}

	// Token: 0x060004D2 RID: 1234 RVA: 0x00020600 File Offset: 0x0001E800
	private void AdjustContentSize(float size)
	{
		Vector2 sizeDelta = this._scrollRect.content.sizeDelta;
		if (this._scrollRect.vertical)
		{
			sizeDelta.y = size;
		}
		else
		{
			sizeDelta.x = size;
		}
		this._scrollRect.content.sizeDelta = sizeDelta;
	}

	// Token: 0x060004D3 RID: 1235 RVA: 0x0002064E File Offset: 0x0001E84E
	private void AdjustSpacer(float size)
	{
		if (this._scrollRect.vertical)
		{
			this._spacer.minHeight = size;
			return;
		}
		this._spacer.minWidth = size;
	}

	// Token: 0x060004D4 RID: 1236 RVA: 0x00020676 File Offset: 0x0001E876
	private float GetScrollRectNormalizedPosition()
	{
		return Mathf.Clamp01(this._scrollRect.vertical ? (1f - this._scrollRect.verticalNormalizedPosition) : this._scrollRect.horizontalNormalizedPosition);
	}

	// Token: 0x060004D5 RID: 1237 RVA: 0x000206A8 File Offset: 0x0001E8A8
	private LayoutElement SpawnSpacer()
	{
		LayoutElement layoutElement = new GameObject("Spacer").AddComponent<LayoutElement>();
		if (this._scrollRect.vertical)
		{
			layoutElement.minHeight = 100f;
		}
		else
		{
			layoutElement.minWidth = 100f;
		}
		layoutElement.transform.SetParent(this._scrollRect.content.transform, false);
		return layoutElement;
	}

	// Token: 0x060004D6 RID: 1238 RVA: 0x00020708 File Offset: 0x0001E908
	private void InitializeElements(int requiredElementsInList, int numElementsCulledAbove)
	{
		for (int i = 0; i < this._activeElements.Count; i++)
		{
			global::UnityEngine.Object.Destroy(this._activeElements[i].gameObject);
		}
		this._activeElements.Clear();
		int num = 0;
		while (num < requiredElementsInList && num + numElementsCulledAbove < this._totalNumElements)
		{
			Y y = global::UnityEngine.Object.Instantiate<Y>(this.Template);
			if (!y.gameObject.activeSelf)
			{
				y.gameObject.SetActive(true);
			}
			y.transform.SetParent(this._scrollRect.content, false);
			y.Setup(num + numElementsCulledAbove, this._scrollListData);
			this._activeElements.Add(y);
			num++;
		}
	}

	// Token: 0x060004D7 RID: 1239 RVA: 0x000207D4 File Offset: 0x0001E9D4
	private void RepurposeElement(PooledScrollRect<T, Y>.RepurposeMethod repurposeMethod, int numElementsCulledAbove)
	{
		if (repurposeMethod == PooledScrollRect<T, Y>.RepurposeMethod.TopGoesToBottom)
		{
			Y y = this._activeElements[0];
			this._activeElements.RemoveAt(0);
			this._activeElements.Add(y);
			y.transform.SetSiblingIndex(this._activeElements[this._activeElements.Count - 2].transform.GetSiblingIndex() + 1);
			y.Setup(numElementsCulledAbove + this._activeElements.Count - 1, this._scrollListData);
			return;
		}
		Y y2 = this._activeElements[this._activeElements.Count - 1];
		this._activeElements.RemoveAt(this._activeElements.Count - 1);
		this._activeElements.Insert(0, y2);
		y2.transform.SetSiblingIndex(this._activeElements[1].transform.GetSiblingIndex());
		y2.Setup(numElementsCulledAbove, this._scrollListData);
	}

	// Token: 0x060004D8 RID: 1240 RVA: 0x000208DE File Offset: 0x0001EADE
	public void OnPointerExit(PointerEventData eventData)
	{
		this.IsHovered = false;
	}

	// Token: 0x060004D9 RID: 1241 RVA: 0x000208E7 File Offset: 0x0001EAE7
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.IsHovered = true;
	}

	// Token: 0x040003B6 RID: 950
	public ScrollRect contentScrollRect;

	// Token: 0x040003B7 RID: 951
	public VerticalLayoutGroup contentLayoutGroup;

	// Token: 0x040003B8 RID: 952
	public ContentSizeFitter contentSizeFitter;

	// Token: 0x040003B9 RID: 953
	public float ElementSize;

	// Token: 0x040003BA RID: 954
	public Y Template;

	// Token: 0x040003BB RID: 955
	[SerializeField]
	protected List<T> _scrollListData = new List<T>();

	// Token: 0x040003BC RID: 956
	protected List<float> _elementHeights = new List<float>();

	// Token: 0x040003BD RID: 957
	public Queue<Y> elements = new Queue<Y>();

	// Token: 0x040003BE RID: 958
	public bool Reflowing;

	// Token: 0x040003BF RID: 959
	private int forceLockCount = 5;

	// Token: 0x040003C0 RID: 960
	public int MaxElements = 60;

	// Token: 0x040003C1 RID: 961
	public int update = 2;

	// Token: 0x040003C2 RID: 962
	private RectTransform _contentLayoutRect;

	// Token: 0x040003C3 RID: 963
	public bool NeedsReflow;

	// Token: 0x040003C4 RID: 964
	private bool clearNextUpdate;

	// Token: 0x040003C5 RID: 965
	protected ScrollRect _scrollRect;

	// Token: 0x040003C6 RID: 966
	private LayoutElement _spacer;

	// Token: 0x040003C7 RID: 967
	private List<Y> _activeElements = new List<Y>();

	// Token: 0x040003C8 RID: 968
	public bool doSpacer = true;

	// Token: 0x040003C9 RID: 969
	public int forceLocked;

	// Token: 0x040003CA RID: 970
	public bool unlocked;

	// Token: 0x040003CB RID: 971
	private bool IsHovered;

	// Token: 0x020015A2 RID: 5538
	private enum RepurposeMethod
	{
		// Token: 0x04005708 RID: 22280
		TopGoesToBottom,
		// Token: 0x04005709 RID: 22281
		BottomGoesToTop
	}
}
