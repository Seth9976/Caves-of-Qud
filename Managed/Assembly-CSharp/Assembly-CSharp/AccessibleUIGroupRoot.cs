using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

// Token: 0x02000150 RID: 336
[AddComponentMenu("Accessibility/UI/Accessible UI Group Root")]
public class AccessibleUIGroupRoot : MonoBehaviour
{
	// Token: 0x06000D64 RID: 3428 RVA: 0x00057743 File Offset: 0x00055943
	public bool IsConstrainedToContainer(UAP_AccessibilityManager.ESDirection direction)
	{
		switch (direction)
		{
		case UAP_AccessibilityManager.ESDirection.EUp:
			return this.m_ConstrainToContainerUp;
		case UAP_AccessibilityManager.ESDirection.EDown:
			return this.m_ConstrainToContainerDown;
		case UAP_AccessibilityManager.ESDirection.ELeft:
			return this.m_ConstrainToContainerLeft;
		case UAP_AccessibilityManager.ESDirection.ERight:
			return this.m_ConstrainToContainerRight;
		default:
			return false;
		}
	}

	// Token: 0x06000D65 RID: 3429 RVA: 0x0005777C File Offset: 0x0005597C
	public static void GetAbsoluteAnchors(RectTransform t, out Vector2 anchorMin, out Vector2 anchorMax, out Vector2 centerPos, bool stopAtScrollView = false)
	{
		centerPos = t.TransformPoint(0.5f, 0.5f, 0f);
		anchorMin = t.anchorMin;
		anchorMax = t.anchorMax;
		Vector2 vector = new Vector3(1f / (float)Screen.width, 1f / (float)Screen.height, 0f);
		Vector2 vector2 = t.TransformPoint(0f, 0f, 0f);
		Vector2 vector3 = t.TransformPoint(1f, 1f, 0f);
		vector2.Scale(vector);
		vector3.Scale(vector);
		Transform transform = t.parent;
		while (transform != null && (!transform.gameObject.GetComponent<Canvas>() || !(transform.parent == null)))
		{
			if (stopAtScrollView && transform.gameObject.GetComponent<ScrollRect>() != null)
			{
				return;
			}
			RectTransform component = transform.gameObject.GetComponent<RectTransform>();
			transform = transform.parent;
			if (!(component == null))
			{
				anchorMin.x = component.anchorMin.x + anchorMin.x * (component.anchorMax.x - component.anchorMin.x);
				anchorMin.y = component.anchorMin.y + anchorMin.y * (component.anchorMax.y - component.anchorMin.y);
				anchorMax.x = component.anchorMin.x + anchorMax.x * (component.anchorMax.x - component.anchorMin.x);
				anchorMax.y = component.anchorMin.y + anchorMax.y * (component.anchorMax.y - component.anchorMin.y);
			}
		}
	}

	// Token: 0x06000D66 RID: 3430 RVA: 0x00057970 File Offset: 0x00055B70
	public void CheckForRegister(UAP_BaseElement item)
	{
		if (!this.m_HasStarted)
		{
			return;
		}
		bool flag = true;
		using (List<AccessibleUIGroupRoot.Accessible_UIElement>.Enumerator enumerator = this.m_AllElements.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.m_Object == item)
				{
					flag = false;
					if (item.m_ForceStartHere && item.IsElementActive())
					{
						this.m_CurrentStartItem = item;
						break;
					}
					break;
				}
			}
		}
		if (flag)
		{
			this.RefreshContainer();
		}
	}

	// Token: 0x06000D67 RID: 3431 RVA: 0x000579F8 File Offset: 0x00055BF8
	public void SetAsStartItem(UAP_BaseElement item)
	{
		using (List<AccessibleUIGroupRoot.Accessible_UIElement>.Enumerator enumerator = this.m_AllElements.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.m_Object == item)
				{
					if (item.m_ForceStartHere && item.IsElementActive())
					{
						this.m_CurrentStartItem = item;
						this.m_RefreshNextFrame = true;
					}
					return;
				}
			}
		}
		this.Register_Item(item);
	}

	// Token: 0x06000D68 RID: 3432 RVA: 0x00057A78 File Offset: 0x00055C78
	public void UnRegister(UAP_BaseElement item)
	{
		foreach (AccessibleUIGroupRoot.Accessible_UIElement accessible_UIElement in this.m_AllElements)
		{
			if (accessible_UIElement.m_Object == item)
			{
				this.m_AllElements.Remove(accessible_UIElement);
				UAP_AccessibilityManager.ElementRemoved(accessible_UIElement);
				break;
			}
		}
	}

	// Token: 0x06000D69 RID: 3433 RVA: 0x00057AE8 File Offset: 0x00055CE8
	public void RefreshContainer()
	{
		bool flag = false;
		if (this.m_CurrentStartItem != null && this.m_CurrentItemIndex >= 0 && this.m_AllElements[this.m_CurrentItemIndex].m_Object == this.m_CurrentStartItem)
		{
			flag = true;
		}
		int count = this.m_AllElements.Count;
		this.m_AllElements.Clear();
		List<UAP_BaseElement> list = new List<UAP_BaseElement>();
		list.AddRange(base.gameObject.GetComponentsInChildren<UAP_BaseElement>());
		foreach (UAP_BaseElement uap_BaseElement in list)
		{
			this.Register_Item(uap_BaseElement);
		}
		if (count == 0 && this.m_AllElements.Count > 0)
		{
			this.ResetToStart();
			this.m_ActivateContainerNextFrame = true;
			return;
		}
		if (flag)
		{
			this.ResetToStart();
		}
	}

	// Token: 0x06000D6A RID: 3434 RVA: 0x00057BC8 File Offset: 0x00055DC8
	private void ActivateContainer_Internal()
	{
		UAP_AccessibilityManager.ActivateContainer(this, true);
		this.m_ActivateContainerNextFrame = false;
	}

	// Token: 0x06000D6B RID: 3435 RVA: 0x00057BD8 File Offset: 0x00055DD8
	private void Register_Item(UAP_BaseElement item)
	{
		AccessibleUIGroupRoot.EUIElement type = item.m_Type;
		Transform transform = item.transform;
		AccessibleUIGroupRoot accessibleUIGroupRoot = transform.gameObject.GetComponent<AccessibleUIGroupRoot>();
		while (accessibleUIGroupRoot == null && transform.parent != null)
		{
			transform = transform.parent;
			accessibleUIGroupRoot = transform.gameObject.GetComponent<AccessibleUIGroupRoot>();
		}
		if (accessibleUIGroupRoot != this)
		{
			return;
		}
		if (accessibleUIGroupRoot == null)
		{
			Debug.LogError("[Accessibility] A UI element tried to register with " + base.gameObject.name + " but not only am I not the right container, there seems to be no container in it's parent hierarchy.");
			return;
		}
		AccessibleUIGroupRoot.Accessible_UIElement accessible_UIElement = new AccessibleUIGroupRoot.Accessible_UIElement(item, type, this.m_AllElements.Count);
		int positionOrder = accessible_UIElement.m_PositionOrder;
		int secondaryOrder = accessible_UIElement.m_SecondaryOrder;
		int count = this.m_AllElements.Count;
		bool flag = false;
		for (int i = 0; i < count; i++)
		{
			if (positionOrder < this.m_AllElements[i].m_PositionOrder)
			{
				this.m_AllElements.Insert(i, accessible_UIElement);
				flag = true;
				break;
			}
			if (positionOrder == this.m_AllElements[i].m_PositionOrder && this.m_AllElements[i].m_SecondaryOrder - secondaryOrder > 0)
			{
				this.m_AllElements.Insert(i, accessible_UIElement);
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			this.m_AllElements.Add(accessible_UIElement);
		}
		if (item.m_ForceStartHere)
		{
			this.m_CurrentStartItem = item;
		}
	}

	// Token: 0x06000D6C RID: 3436 RVA: 0x00057D2D File Offset: 0x00055F2D
	private void OnEnable()
	{
		if (!this.m_HasStarted)
		{
			return;
		}
		this.RefreshContainer();
		this.ResetToStart();
		if (this.m_AllElements.Count > 0)
		{
			this.m_ActivateContainerNextFrame = true;
		}
	}

	// Token: 0x06000D6D RID: 3437 RVA: 0x00057D59 File Offset: 0x00055F59
	private void Start()
	{
		this.m_HasStarted = true;
		this.RefreshContainer();
		this.ResetToStart();
		if (this.m_AllElements.Count > 0)
		{
			this.m_ActivateContainerNextFrame = true;
		}
	}

	// Token: 0x06000D6E RID: 3438 RVA: 0x00057D84 File Offset: 0x00055F84
	public void ResetToStart()
	{
		this.m_CurrentItemIndex = 0;
		if (this.m_CurrentStartItem != null)
		{
			for (int i = 0; i < this.m_AllElements.Count; i++)
			{
				if (this.m_AllElements[i].m_Object == this.m_CurrentStartItem)
				{
					this.m_CurrentItemIndex = i;
					return;
				}
			}
		}
	}

	// Token: 0x06000D6F RID: 3439 RVA: 0x00057DE2 File Offset: 0x00055FE2
	private void OnDisable()
	{
		UAP_AccessibilityManager.ActivateContainer(this, false);
	}

	// Token: 0x06000D70 RID: 3440 RVA: 0x00057DEB File Offset: 0x00055FEB
	private void OnDestroy()
	{
		UAP_AccessibilityManager.ActivateContainer(this, false);
	}

	// Token: 0x06000D71 RID: 3441 RVA: 0x00057DF4 File Offset: 0x00055FF4
	public AccessibleUIGroupRoot.Accessible_UIElement GetCurrentElement(bool rollOverAllowed)
	{
		this.m_CurrentItemIndex = this.FindFirstActiveItemIndex(this.m_CurrentItemIndex, rollOverAllowed);
		if (this.m_CurrentItemIndex < 0 || this.m_AllElements.Count == 0)
		{
			return null;
		}
		return this.m_AllElements[this.m_CurrentItemIndex];
	}

	// Token: 0x06000D72 RID: 3442 RVA: 0x00057E34 File Offset: 0x00056034
	private int FindFirstActiveItemIndex(int startIndex, bool rollOverAllowed)
	{
		if (this.m_RefreshNextFrame)
		{
			this.RefreshContainer();
		}
		if (this.m_AllElements.Count == 0)
		{
			return -1;
		}
		if (startIndex < 0)
		{
			startIndex = 0;
		}
		if (startIndex >= this.m_AllElements.Count)
		{
			if (!rollOverAllowed)
			{
				return -1;
			}
			startIndex = 0;
		}
		for (int i = 0; i < this.m_AllElements.Count; i++)
		{
			int num = i + startIndex;
			if (num >= this.m_AllElements.Count)
			{
				if (!rollOverAllowed)
				{
					return -1;
				}
				num -= this.m_AllElements.Count;
			}
			if (this.m_AllElements[num].m_Object == null)
			{
				this.RefreshContainer();
				return this.FindFirstActiveItemIndex(startIndex, rollOverAllowed);
			}
			if (this.m_AllElements[num].m_Object.IsElementActive())
			{
				return num;
			}
		}
		return -1;
	}

	// Token: 0x06000D73 RID: 3443 RVA: 0x00057F00 File Offset: 0x00056100
	private int FindPreviousActiveItemIndex(int startIndex, bool rollOverAllowed)
	{
		if (this.m_RefreshNextFrame)
		{
			this.RefreshContainer();
		}
		if (this.m_AllElements.Count == 0)
		{
			return -1;
		}
		if (startIndex < 0)
		{
			if (!rollOverAllowed)
			{
				return -1;
			}
			startIndex = this.m_AllElements.Count - 1;
		}
		if (startIndex >= this.m_AllElements.Count)
		{
			startIndex = this.m_AllElements.Count - 1;
		}
		for (int i = this.m_AllElements.Count; i > 0; i--)
		{
			int num = i + startIndex - this.m_AllElements.Count;
			if (num < 0)
			{
				if (!rollOverAllowed)
				{
					return -1;
				}
				num += this.m_AllElements.Count;
			}
			if (this.m_AllElements[num].m_Object == null)
			{
				this.RefreshContainer();
				return this.FindFirstActiveItemIndex(startIndex, rollOverAllowed);
			}
			if (this.m_AllElements[num].m_Object.IsElementActive())
			{
				return num;
			}
		}
		return -1;
	}

	// Token: 0x06000D74 RID: 3444 RVA: 0x00057FE4 File Offset: 0x000561E4
	public bool IncrementCurrentItem(bool rollOverAllowed)
	{
		int num = this.FindFirstActiveItemIndex(this.m_CurrentItemIndex + 1, rollOverAllowed);
		if (num < 0)
		{
			return false;
		}
		if (num <= this.m_CurrentItemIndex)
		{
			if (rollOverAllowed)
			{
				this.m_CurrentItemIndex = num;
			}
			return false;
		}
		this.m_CurrentItemIndex = num;
		return true;
	}

	// Token: 0x06000D75 RID: 3445 RVA: 0x00058024 File Offset: 0x00056224
	public bool DecrementCurrentItem(bool rollOverAllowed)
	{
		int num = this.FindPreviousActiveItemIndex(this.m_CurrentItemIndex - 1, rollOverAllowed);
		if (num < 0)
		{
			return false;
		}
		if (num >= this.m_CurrentItemIndex)
		{
			if (rollOverAllowed)
			{
				this.m_CurrentItemIndex = num;
			}
			return false;
		}
		this.m_CurrentItemIndex = num;
		return true;
	}

	// Token: 0x06000D76 RID: 3446 RVA: 0x00058064 File Offset: 0x00056264
	public bool MoveFocus2D(UAP_AccessibilityManager.ESDirection direction)
	{
		Vector2 pos = this.m_AllElements[this.m_CurrentItemIndex].m_Pos;
		int num = -1;
		float num2 = -1f;
		for (int i = 0; i < this.m_AllElements.Count; i++)
		{
			if (i != this.m_CurrentItemIndex && this.m_AllElements[i].m_Object.IsElementActive())
			{
				Vector2 vector = this.m_AllElements[i].m_Pos - pos;
				float num3 = 1.1f;
				bool flag = false;
				switch (direction)
				{
				case UAP_AccessibilityManager.ESDirection.EUp:
					if (vector.y > 0f && vector.y > Mathf.Abs(vector.x) * num3)
					{
						flag = true;
					}
					break;
				case UAP_AccessibilityManager.ESDirection.EDown:
					if (vector.y < 0f && -vector.y > Mathf.Abs(vector.x) * num3)
					{
						flag = true;
					}
					break;
				case UAP_AccessibilityManager.ESDirection.ELeft:
					if (vector.x < 0f && -vector.x > Mathf.Abs(vector.y) * num3)
					{
						flag = true;
					}
					break;
				case UAP_AccessibilityManager.ESDirection.ERight:
					if (vector.x > 0f && vector.x > Mathf.Abs(vector.y) * num3)
					{
						flag = true;
					}
					break;
				}
				if (flag)
				{
					float num4 = vector.SqrMagnitude();
					if (num < 0 || num4 < num2)
					{
						num = i;
						num2 = num4;
					}
				}
			}
		}
		if (num < 0)
		{
			return false;
		}
		this.m_CurrentItemIndex = num;
		return true;
	}

	// Token: 0x06000D77 RID: 3447 RVA: 0x000581E4 File Offset: 0x000563E4
	private void Update()
	{
		if (this.m_ActivateContainerNextFrame)
		{
			if (this.m_DoubleCheckUIElementsPositions && this.m_NeedsRefreshBeforeActivation)
			{
				this.m_RefreshNextFrame = true;
				this.m_NeedsRefreshBeforeActivation = false;
			}
			else
			{
				this.ActivateContainer_Internal();
				this.m_NeedsRefreshBeforeActivation = this.m_DoubleCheckUIElementsPositions;
			}
		}
		if (this.m_RefreshNextFrame)
		{
			this.m_RefreshNextFrame = false;
			this.RefreshContainer();
		}
	}

	// Token: 0x06000D78 RID: 3448 RVA: 0x00058240 File Offset: 0x00056440
	public void RefreshNextUpdate()
	{
		this.m_RefreshNextFrame = true;
	}

	// Token: 0x06000D79 RID: 3449 RVA: 0x00058249 File Offset: 0x00056449
	public void JumpToFirst()
	{
		this.m_CurrentItemIndex = 0;
		this.FindFirstActiveItemIndex(this.m_CurrentItemIndex, false);
	}

	// Token: 0x06000D7A RID: 3450 RVA: 0x00058260 File Offset: 0x00056460
	public void JumpToLast()
	{
		this.m_CurrentItemIndex = this.m_AllElements.Count - 1;
		this.FindPreviousActiveItemIndex(this.m_CurrentItemIndex, false);
	}

	// Token: 0x06000D7B RID: 3451 RVA: 0x00058284 File Offset: 0x00056484
	public void SetActiveElementIndex(int index, bool rollOverAllowed)
	{
		if (index < 0 || index >= this.m_AllElements.Count)
		{
			Debug.LogError("[Accessibility] UI Group: Trying to set an out of bounds index for current item. Setting index to first valid and active item to prevent crash.");
			index = 0;
		}
		if (!this.m_AllElements[index].m_Object.IsElementActive())
		{
			Debug.LogWarning("[Accessibility] UI Group: Trying to set a current item index to an inactive element. Setting index to next valid and active item to prevent issues.");
		}
		this.m_CurrentItemIndex = this.FindFirstActiveItemIndex(index, rollOverAllowed);
	}

	// Token: 0x06000D7C RID: 3452 RVA: 0x000582E0 File Offset: 0x000564E0
	public int GetCurrentElementIndex()
	{
		return this.m_CurrentItemIndex;
	}

	// Token: 0x06000D7D RID: 3453 RVA: 0x000582E8 File Offset: 0x000564E8
	public List<AccessibleUIGroupRoot.Accessible_UIElement> GetElements()
	{
		return this.m_AllElements;
	}

	// Token: 0x06000D7E RID: 3454 RVA: 0x000582F0 File Offset: 0x000564F0
	public bool SelectItem(UAP_BaseElement element, bool forceRepeatItem = false)
	{
		for (int i = 0; i < this.m_AllElements.Count; i++)
		{
			if (this.m_AllElements[i].m_Object == element)
			{
				this.m_CurrentItemIndex = i;
				return UAP_AccessibilityManager.MakeActiveContainer(this, forceRepeatItem);
			}
		}
		return false;
	}

	// Token: 0x06000D7F RID: 3455 RVA: 0x0005833C File Offset: 0x0005653C
	public string GetContainerName(bool useGameObjectNameIfNone = false)
	{
		if (this.m_ContainerName.Length > 0)
		{
			if (this.IsNameLocalizationKey())
			{
				return UAP_AccessibilityManager.Localize(this.m_ContainerName);
			}
			return this.m_ContainerName;
		}
		else
		{
			if (useGameObjectNameIfNone)
			{
				return base.gameObject.name;
			}
			return "";
		}
	}

	// Token: 0x06000D80 RID: 3456 RVA: 0x0005837B File Offset: 0x0005657B
	public bool IsNameLocalizationKey()
	{
		return this.m_IsLocalizationKey;
	}

	// Token: 0x0400094F RID: 2383
	public bool m_PopUp;

	// Token: 0x04000950 RID: 2384
	public bool m_AllowExternalJoining;

	// Token: 0x04000951 RID: 2385
	public bool m_AutoRead;

	// Token: 0x04000952 RID: 2386
	public int m_Priority;

	// Token: 0x04000953 RID: 2387
	public string m_ContainerName = "";

	// Token: 0x04000954 RID: 2388
	[FormerlySerializedAs("m_IsNGUILocalizationKey")]
	public bool m_IsLocalizationKey;

	// Token: 0x04000955 RID: 2389
	public bool m_2DNavigation;

	// Token: 0x04000956 RID: 2390
	public bool m_ConstrainToContainerUp;

	// Token: 0x04000957 RID: 2391
	public bool m_ConstrainToContainerDown;

	// Token: 0x04000958 RID: 2392
	public bool m_ConstrainToContainerLeft;

	// Token: 0x04000959 RID: 2393
	public bool m_ConstrainToContainerRight;

	// Token: 0x0400095A RID: 2394
	public bool m_AllowTouchExplore = true;

	// Token: 0x0400095B RID: 2395
	private bool m_HasStarted;

	// Token: 0x0400095C RID: 2396
	private bool m_RefreshNextFrame;

	// Token: 0x0400095D RID: 2397
	private bool m_ActivateContainerNextFrame;

	// Token: 0x0400095E RID: 2398
	[Tooltip("This causes a 2 frame delay before the interface is accessible, but solves issues with screens that perform automatic UI elements ordering at start - such as any dynamically built UI, expanding scroll views, horizontal grids etc")]
	public bool m_DoubleCheckUIElementsPositions = true;

	// Token: 0x0400095F RID: 2399
	private bool m_NeedsRefreshBeforeActivation = true;

	// Token: 0x04000960 RID: 2400
	private List<AccessibleUIGroupRoot.Accessible_UIElement> m_AllElements = new List<AccessibleUIGroupRoot.Accessible_UIElement>();

	// Token: 0x04000961 RID: 2401
	private UAP_BaseElement m_CurrentStartItem;

	// Token: 0x04000962 RID: 2402
	private int m_CurrentItemIndex;

	// Token: 0x020015FC RID: 5628
	public enum EUIElement
	{
		// Token: 0x0400588C RID: 22668
		EUndefined,
		// Token: 0x0400588D RID: 22669
		EButton,
		// Token: 0x0400588E RID: 22670
		ELabel,
		// Token: 0x0400588F RID: 22671
		EToggle,
		// Token: 0x04005890 RID: 22672
		ESlider,
		// Token: 0x04005891 RID: 22673
		ETextEdit,
		// Token: 0x04005892 RID: 22674
		EDropDown
	}

	// Token: 0x020015FD RID: 5629
	public class Accessible_UIElement
	{
		// Token: 0x0600D684 RID: 54916 RVA: 0x004F858B File Offset: 0x004F678B
		public bool AllowsVoiceOver()
		{
			return this.m_Object.m_AllowVoiceOver;
		}

		// Token: 0x0600D685 RID: 54917 RVA: 0x004F8598 File Offset: 0x004F6798
		public bool ReadType()
		{
			return this.m_Object.m_ReadType;
		}

		// Token: 0x0600D686 RID: 54918 RVA: 0x004F85A8 File Offset: 0x004F67A8
		public void CalculatePositionOrder(UAP_BaseElement uiElement, int backupIndex)
		{
			GameObject gameObject = uiElement.gameObject;
			if (uiElement.m_ManualPositionParent == null)
			{
				AccessibleUIGroupRoot uigroupContainer = uiElement.GetUIGroupContainer();
				uiElement.m_ManualPositionParent = ((uigroupContainer != null) ? uigroupContainer.gameObject : null);
			}
			bool flag = uiElement.m_ManualPositionOrder >= 0 && uiElement.m_ManualPositionParent != null;
			if (flag)
			{
				gameObject = uiElement.m_ManualPositionParent;
			}
			Vector2 vector = default(Vector2);
			Vector2 vector2 = default(Vector2);
			Vector2 vector3 = default(Vector2);
			bool flag2 = false;
			bool flag3 = false;
			RectTransform component = gameObject.GetComponent<RectTransform>();
			if (component != null)
			{
				Transform transform = component;
				RectTransform rectTransform = component;
				while (transform.parent != null)
				{
					if (transform.parent.gameObject.GetComponent<ScrollRect>() != null)
					{
						rectTransform = transform.parent.gameObject.GetComponent<RectTransform>();
						flag3 = true;
						break;
					}
					transform = transform.parent;
				}
				AccessibleUIGroupRoot.GetAbsoluteAnchors(rectTransform, out vector, out vector2, out vector3, false);
				flag2 = true;
			}
			if (flag2)
			{
				Vector3 vector4 = vector3;
				this.m_Pos.x = vector4.x;
				this.m_Pos.y = vector4.y;
				this.m_Pos.Scale(new Vector2(1f / (float)Screen.width, 1f / (float)Screen.height));
				this.m_PositionOrder = (int)(10f * (this.m_Pos.x + 1000f * (1f - this.m_Pos.y)));
				if (flag)
				{
					this.m_PositionOrder += uiElement.m_ManualPositionOrder;
				}
				this.m_Pos.Scale(new Vector2((float)Screen.width, (float)Screen.height));
				gameObject = uiElement.gameObject;
				if (flag3)
				{
					Vector2 vector5 = gameObject.transform.TransformPoint(new Vector3(0.5f, 0.5f, 0f));
					this.m_SecondaryOrder = (int)(10f * (vector5.x + 1000f * (1f - vector5.y)));
					return;
				}
			}
			else
			{
				if (uiElement is UAP_BaseElement_3D)
				{
					if (uiElement.m_ManualPositionOrder >= 0)
					{
						this.m_PositionOrder = uiElement.m_ManualPositionOrder;
					}
					else
					{
						this.m_PositionOrder = backupIndex;
					}
					this.m_Pos = gameObject.transform.position;
					return;
				}
				Debug.LogWarning("[Accessibility] Could not find any UI transforms on UI element " + gameObject.name + " that the accessibility plugin can understand - ordering UI elements by manual position index (if present) or order of initialization.");
				if (uiElement.m_ManualPositionOrder >= 0)
				{
					this.m_PositionOrder = uiElement.m_ManualPositionOrder;
				}
				else
				{
					this.m_PositionOrder = backupIndex;
				}
				this.m_Pos = gameObject.transform.position;
			}
		}

		// Token: 0x0600D687 RID: 54919 RVA: 0x004F884C File Offset: 0x004F6A4C
		public Accessible_UIElement(UAP_BaseElement item, AccessibleUIGroupRoot.EUIElement type, int index)
		{
			this.m_Type = type;
			this.m_Object = item;
			if (item.GetTargetGameObject().GetComponentInParent<ScrollRect>() != null)
			{
				item.m_IsInsideScrollView = true;
			}
			this.CalculatePositionOrder(item, index);
			this.m_Object.m_PositionOrder = this.m_PositionOrder;
			this.m_Object.m_SecondaryOrder = this.m_SecondaryOrder;
			this.m_Object.m_Pos = this.m_Pos;
		}

		// Token: 0x04005893 RID: 22675
		public AccessibleUIGroupRoot.EUIElement m_Type;

		// Token: 0x04005894 RID: 22676
		public UAP_BaseElement m_Object;

		// Token: 0x04005895 RID: 22677
		public Vector2 m_Pos = new Vector2(0f, 0f);

		// Token: 0x04005896 RID: 22678
		public int m_PositionOrder = -1;

		// Token: 0x04005897 RID: 22679
		public int m_SecondaryOrder;
	}
}
