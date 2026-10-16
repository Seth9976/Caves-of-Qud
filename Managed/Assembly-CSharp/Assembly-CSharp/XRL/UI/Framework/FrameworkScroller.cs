using System;
using System.Collections.Generic;
using System.Linq;
using Qud.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using XRL.CharacterBuilds;

namespace XRL.UI.Framework
{
	// Token: 0x0200150A RID: 5386
	public class FrameworkScroller : MonoBehaviour, IFrameworkContext
	{
		// Token: 0x0600D1F8 RID: 53752 RVA: 0x004DF517 File Offset: 0x004DD717
		public virtual NavigationContext GetNavigationContext()
		{
			return this.scrollContext;
		}

		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x0600D1F9 RID: 53753 RVA: 0x004DF51F File Offset: 0x004DD71F
		public GridLayoutGroup gridLayout
		{
			get
			{
				if (!this._gridLayoutInitialized)
				{
					this._gridLayout = this.childRoot.GetComponent<GridLayoutGroup>();
					this._gridLayoutInitialized = true;
				}
				return this._gridLayout;
			}
		}

		// Token: 0x0600D1FA RID: 53754 RVA: 0x004DF547 File Offset: 0x004DD747
		public void Start()
		{
			this.selectedPosition = 0;
			if (this.scrollContext != null)
			{
				this.scrollContext.data = this.choices;
			}
		}

		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x0600D1FB RID: 53755 RVA: 0x004DF569 File Offset: 0x004DD769
		public InputAxisTypes PageAxis
		{
			get
			{
				if (this.NavigationAxis == InputAxisTypes.NavigationXAxis)
				{
					return InputAxisTypes.NavigationPageXAxis;
				}
				if (this.NavigationAxis == InputAxisTypes.NavigationYAxis)
				{
					return InputAxisTypes.NavigationPageYAxis;
				}
				throw new Exception("Unhandled page axis");
			}
		}

		// Token: 0x0600D1FC RID: 53756 RVA: 0x004DF58C File Offset: 0x004DD78C
		public virtual FrameworkUnityScrollChild GetPrefabForIndex(int x)
		{
			if (this.selectionPrefab == null)
			{
				MetricsManager.LogError("selectionPrefab is null!");
				return null;
			}
			while (this.selectionClones.Count <= x)
			{
				if (this.allowPrefabPoolingDuringLayoutChildren && this.prefabPool.Count > 0)
				{
					GameObject gameObject = this.prefabPool.Dequeue();
					FrameworkUnityScrollChild frameworkUnityScrollChild = ((gameObject != null) ? gameObject.GetComponent<FrameworkUnityScrollChild>() : null);
					if (frameworkUnityScrollChild != null)
					{
						GameObject gameObject2 = frameworkUnityScrollChild.gameObject;
						if (gameObject2 != null)
						{
							gameObject2.SetActive(true);
						}
						frameworkUnityScrollChild.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
						this.selectionClones.Add(frameworkUnityScrollChild);
					}
					else
					{
						this.selectionClones.Add(global::UnityEngine.Object.Instantiate<FrameworkUnityScrollChild>(this.selectionPrefab));
					}
				}
				else
				{
					this.selectionClones.Add(global::UnityEngine.Object.Instantiate<FrameworkUnityScrollChild>(this.selectionPrefab));
				}
			}
			return this.selectionClones[x];
		}

		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x0600D1FD RID: 53757 RVA: 0x004DF67F File Offset: 0x004DD87F
		public virtual bool allowPrefabPoolingDuringLayoutChildren
		{
			get
			{
				return this.basePrefabPoolingAllowed;
			}
		}

		// Token: 0x0600D1FE RID: 53758 RVA: 0x004DF688 File Offset: 0x004DD888
		public virtual void LayoutChildren()
		{
			List<GameObject> list = new List<GameObject>();
			foreach (object obj in this.childRoot.transform)
			{
				Transform transform = (Transform)obj;
				if (!this.selectionClones.Contains(transform.GetComponent<FrameworkUnityScrollChild>()) && !this.spacerClones.Contains(transform.gameObject))
				{
					list.Add(transform.gameObject);
				}
			}
			if (this.selectionClones.Count > this.choices.Count)
			{
				list.AddRange(from clone in this.selectionClones.Skip<FrameworkUnityScrollChild>(this.choices.Count)
					select clone.gameObject);
				this.selectionClones.RemoveRange(this.choices.Count, this.selectionClones.Count - this.choices.Count);
				if (this.spacerPrefab != null)
				{
					List<GameObject> list2 = this.spacerClones.Skip<GameObject>(this.choices.Count + (this.putSpacersTopAndBottom ? 1 : (-1))).ToList<GameObject>();
					list.AddRange(list2);
					foreach (GameObject gameObject in list2)
					{
						this.spacerClones.Remove(gameObject);
					}
				}
			}
			list.ForEach(delegate(GameObject child)
			{
				try
				{
					if (this.allowPrefabPoolingDuringLayoutChildren)
					{
						child.SetActive(false);
						child.transform.SetParent(null);
						this.prefabPool.Enqueue(child);
					}
					else
					{
						child.DestroyImmediate();
					}
				}
				catch
				{
				}
			});
			foreach (FrameworkDataElement frameworkDataElement in this.choices)
			{
				int count = this.scrollContext.contexts.Count;
				int num = (this.putSpacersTopAndBottom ? (-1) : 0);
				if (count > num && this.spacerPrefab != null && this.spacerClones.Count < count - num)
				{
					GameObject gameObject2 = global::UnityEngine.Object.Instantiate<GameObject>(this.spacerPrefab);
					gameObject2.transform.SetParent(this.childRoot.transform, false);
					this.spacerClones.Add(gameObject2);
				}
				ScrollChildContext scrollChildContext = this.MakeContextFor(frameworkDataElement, count);
				this.scrollContext.contexts.Add(scrollChildContext);
				NavigationContext navigationContext = this.lastContexts.ElementAtOrDefault(count, null);
				if (navigationContext != null && navigationContext.IsActive(true))
				{
					scrollChildContext.Activate(false);
				}
				FrameworkUnityScrollChild prefabForIndex = this.GetPrefabForIndex(count);
				if (prefabForIndex.gameObject.GetComponent<ControlId>() == null)
				{
					prefabForIndex.gameObject.AddComponent<ControlId>();
				}
				prefabForIndex.gameObject.GetComponent<ControlId>().id = frameworkDataElement.Id;
				prefabForIndex.transform.SetParent(this.childRoot.transform, false);
				this.SetupPrefab(prefabForIndex, scrollChildContext, frameworkDataElement, count);
				scrollChildContext.Setup();
			}
			if (this.putSpacersTopAndBottom && this.spacerClones.Count < this.choices.Count + 1)
			{
				GameObject gameObject3 = global::UnityEngine.Object.Instantiate<GameObject>(this.spacerPrefab);
				gameObject3.transform.SetParent(this.childRoot.transform, false);
				this.spacerClones.Add(gameObject3);
			}
		}

		// Token: 0x0600D1FF RID: 53759 RVA: 0x004DFA20 File Offset: 0x004DDC20
		public virtual void ChangePrefabs(FrameworkUnityScrollChild newSelectionPrefab, GameObject newSpacerPrefab = null)
		{
			if (newSelectionPrefab == null)
			{
				MetricsManager.LogError("changing to a null selection prefab!");
			}
			this.selectionPrefab = newSelectionPrefab;
			this.spacerPrefab = newSpacerPrefab;
			this.InvalidatePrefabs();
		}

		// Token: 0x0600D200 RID: 53760 RVA: 0x004DFA4C File Offset: 0x004DDC4C
		protected virtual void InvalidatePrefabs()
		{
			foreach (Transform transform in this.selectionClones.Select<FrameworkUnityScrollChild, Transform>((FrameworkUnityScrollChild s) => s.transform).Concat<Transform>(this.spacerClones.Select<GameObject, Transform>((GameObject s) => s.transform)))
			{
				global::UnityEngine.Object.Destroy(transform);
			}
			this.selectionClones.Clear();
			this.spacerClones.Clear();
		}

		// Token: 0x0600D201 RID: 53761 RVA: 0x004DFB00 File Offset: 0x004DDD00
		public virtual void UpdateSelections(IEnumerable<FrameworkDataElement> selections = null)
		{
			if (selections == null)
			{
				selections = this.choices;
			}
			List<FrameworkDataElement> list = this.choices;
			if (list != null)
			{
				list.Clear();
			}
			List<FrameworkDataElement> list2 = this.choices;
			if (list2 != null)
			{
				list2.AddRange(selections);
			}
			this.scrollContext.data = this.choices;
			if (this.selectedPosition >= selections.Count<FrameworkDataElement>() && selections.Count<FrameworkDataElement>() > 0)
			{
				this.scrollContext.SelectIndex(selections.Count<FrameworkDataElement>() - 1);
			}
			if (this.autoHotkey && CapabilityManager.AllowKeyboardHotkeys)
			{
				List<KeyCode>.Enumerator enumerator = ControlManager.GetHotkeySpread(new List<string> { "Menus", "UINav", "Chargen" }).GetEnumerator();
				foreach (IFrameworkDataHotkey frameworkDataHotkey in from choice in this.choices.SelectMany<FrameworkDataElement, FrameworkDataElement>(new Func<FrameworkDataElement, IEnumerable<FrameworkDataElement>>(this.ChoiceAndChildren))
					select choice as IFrameworkDataHotkey)
				{
					if (frameworkDataHotkey != null)
					{
						if (enumerator.MoveNext())
						{
							KeyCode keyCode = enumerator.Current;
							frameworkDataHotkey.Hotkey = "[{{W|" + keyCode.ToString() + "}}]";
							this.hotkeyChoices.Add(keyCode, frameworkDataHotkey as FrameworkDataElement);
						}
						else
						{
							frameworkDataHotkey.Hotkey = "";
						}
					}
				}
			}
			this.lastContexts.Clear();
			this.lastContexts.AddRange(this.scrollContext.contexts);
			this.scrollContext.contexts.Clear();
			this.LayoutChildren();
			this.scrollContext.Setup();
			if (this.selectedPosition < this.scrollContext.data.Count && this.scrollContext.IsActive(true) && this.selectedPosition > 0 && this.scrollContext.data != null && this.selectedPosition < this.scrollContext.data.Count)
			{
				UnityEvent<FrameworkDataElement> unityEvent = this.onHighlight;
				if (unityEvent == null)
				{
					return;
				}
				unityEvent.Invoke(this.scrollContext.data[this.selectedPosition]);
			}
		}

		// Token: 0x0600D202 RID: 53762 RVA: 0x004DFD3C File Offset: 0x004DDF3C
		public void BeforeShow(IEnumerable<FrameworkDataElement> selections = null)
		{
			this.BeforeShow(null, selections);
		}

		// Token: 0x0600D203 RID: 53763 RVA: 0x004DFD48 File Offset: 0x004DDF48
		public virtual void BeforeShow(EmbarkBuilderModuleWindowDescriptor descriptor, IEnumerable<FrameworkDataElement> selections = null)
		{
			this.lastHeight = -1f;
			this.hotkeyChoices.Clear();
			if (this.titleText != null && descriptor != null)
			{
				this.titleText.SetText(descriptor.title);
			}
			this.scrollContext.wraps = false;
			this.scrollContext.SetAxis(this.NavigationAxis, false);
			if (this.EnablePaging)
			{
				this.scrollContext.axisHandlers.Set(this.PageAxis, Event.Helpers.Handle(Event.Helpers.Axis(new Action(this.DoPageDown), new Action(this.DoPageUp), null)));
			}
			this.UpdateSelections(selections);
		}

		// Token: 0x0600D204 RID: 53764 RVA: 0x004DFDF8 File Offset: 0x004DDFF8
		public virtual void SelectHighest()
		{
			if (this.selectionClones.Count <= 0)
			{
				return;
			}
			int num = this.selectionClones.IndexOf(this.selectionClones.FindAll((FrameworkUnityScrollChild c) => ScrollViewCalcs.GetScrollViewCalcs(c.GetComponent<RectTransform>(), this.scrollCalcs).isAnyInView).First<FrameworkUnityScrollChild>());
			this.scrollContext.SelectIndex(num);
			CategoryMenuController categoryMenuController = this.selectionClones[num].FrameworkControl as CategoryMenuController;
			if (categoryMenuController != null && categoryMenuController.scroller != null)
			{
				categoryMenuController.scroller.SelectHighest();
			}
		}

		// Token: 0x0600D205 RID: 53765 RVA: 0x004DFE84 File Offset: 0x004DE084
		public virtual void SelectLowest()
		{
			if (this.selectionClones.Count <= 0)
			{
				return;
			}
			int num = this.selectionClones.IndexOf(this.selectionClones.FindAll((FrameworkUnityScrollChild c) => ScrollViewCalcs.GetScrollViewCalcs(c.GetComponent<RectTransform>(), this.scrollCalcs).isAnyInView).Last<FrameworkUnityScrollChild>());
			this.scrollContext.SelectIndex(num);
			CategoryMenuController categoryMenuController = this.selectionClones[num].FrameworkControl as CategoryMenuController;
			if (categoryMenuController != null && categoryMenuController.scroller != null)
			{
				categoryMenuController.scroller.SelectLowest();
			}
		}

		// Token: 0x0600D206 RID: 53766 RVA: 0x004DFF10 File Offset: 0x004DE110
		public virtual void DoScrollUp()
		{
			this.scrollRect.verticalNormalizedPosition += this.scrollRect.viewport.rect.height / this.scrollRect.content.rect.height / 5f;
			this.SelectHighest();
		}

		// Token: 0x0600D207 RID: 53767 RVA: 0x004DFF6C File Offset: 0x004DE16C
		public virtual void DoScrollDown()
		{
			this.scrollRect.verticalNormalizedPosition -= this.scrollRect.viewport.rect.height / this.scrollRect.content.rect.height / 5f;
			this.SelectLowest();
		}

		// Token: 0x0600D208 RID: 53768 RVA: 0x004DFFC8 File Offset: 0x004DE1C8
		public virtual void DoPageUp()
		{
			this.scrollRect.verticalNormalizedPosition += this.scrollRect.viewport.rect.height / this.scrollRect.content.rect.height;
			this.SelectHighest();
		}

		// Token: 0x0600D209 RID: 53769 RVA: 0x004E0020 File Offset: 0x004DE220
		public virtual void DoPageDown()
		{
			this.scrollRect.verticalNormalizedPosition -= this.scrollRect.viewport.rect.height / this.scrollRect.content.rect.height;
			this.SelectLowest();
		}

		// Token: 0x0600D20A RID: 53770 RVA: 0x004E0078 File Offset: 0x004DE278
		public virtual void RefreshDatas()
		{
			for (int i = 0; i < this.choices.Count; i++)
			{
				FrameworkDataElement frameworkDataElement = this.choices[i];
				FrameworkUnityScrollChild prefabForIndex = this.GetPrefabForIndex(i);
				ScrollChildContext scrollChildContext = this.scrollContext.contexts[i] as ScrollChildContext;
				this.SetupPrefab(prefabForIndex, scrollChildContext, frameworkDataElement, i);
			}
		}

		// Token: 0x0600D20B RID: 53771 RVA: 0x004E00D1 File Offset: 0x004DE2D1
		public virtual void SetupPrefab(FrameworkUnityScrollChild newChild, ScrollChildContext context, FrameworkDataElement data, int index)
		{
			newChild.Setup(data, context);
			this.PostSetup.Invoke(newChild, context, data, index);
		}

		// Token: 0x0600D20C RID: 53772 RVA: 0x004E00EC File Offset: 0x004DE2EC
		public virtual ScrollChildContext MakeContextFor(FrameworkDataElement data, int index)
		{
			ScrollChildContext context = new ScrollChildContext
			{
				index = index
			};
			context.buttonHandlers = new Dictionary<InputButtonTypes, Action> { 
			{
				InputButtonTypes.AcceptButton,
				Event.Helpers.Handle(delegate
				{
					if (TutorialManager.AllowOnSelected(this.choices[context.index]))
					{
						FrameworkDataElement frameworkDataElement = this.choices.ElementAtOrDefault(context.index, null);
						if (frameworkDataElement == null)
						{
							MetricsManager.LogError(string.Format("Scroll Child Context Activation handler can't find choices[{0}]", index));
							return;
						}
						UnityEvent<FrameworkDataElement> unityEvent = this.onSelected;
						if (unityEvent == null)
						{
							return;
						}
						unityEvent.Invoke(frameworkDataElement);
					}
				})
			} };
			return context;
		}

		// Token: 0x0600D20D RID: 53773 RVA: 0x004E0154 File Offset: 0x004DE354
		public virtual void UpdateSelection()
		{
			this.ScrollSelectedIntoView();
			if (this.selectedPosition < this.scrollContext.data.Count)
			{
				UnityEvent<FrameworkDataElement> unityEvent = this.onHighlight;
				if (unityEvent == null)
				{
					return;
				}
				unityEvent.Invoke(this.scrollContext.data[this.selectedPosition]);
			}
		}

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x0600D20E RID: 53774 RVA: 0x004E01A8 File Offset: 0x004DE3A8
		private RectTransform rtWidth
		{
			get
			{
				RectTransform rectTransform;
				if ((rectTransform = this._rtWidth) == null)
				{
					ScrollRect scrollRect = this.scrollRect;
					RectTransform rectTransform2;
					if (scrollRect == null)
					{
						rectTransform2 = null;
					}
					else
					{
						Transform parent = scrollRect.transform.parent;
						rectTransform2 = ((parent != null) ? parent.GetComponent<RectTransform>() : null);
					}
					rectTransform = (this._rtWidth = rectTransform2);
				}
				return rectTransform;
			}
		}

		// Token: 0x0600D20F RID: 53775 RVA: 0x004E01EB File Offset: 0x004DE3EB
		public virtual void UpdateWidth()
		{
		}

		// Token: 0x0600D210 RID: 53776 RVA: 0x004E01ED File Offset: 0x004DE3ED
		public void DisableFirstLateUpdate()
		{
			this.FirstLateUpdate = false;
		}

		// Token: 0x0600D211 RID: 53777 RVA: 0x004E01F6 File Offset: 0x004DE3F6
		public void DisableForcedUpdatesOnLayout()
		{
			this._DisableForcedUpdatesOnLayout = true;
		}

		// Token: 0x0600D212 RID: 53778 RVA: 0x004E01FF File Offset: 0x004DE3FF
		public virtual void LateUpdate()
		{
			if (this.FirstLateUpdate)
			{
				if (!this._DisableForcedUpdatesOnLayout)
				{
					Canvas.ForceUpdateCanvases();
				}
				this.UpdateLayout();
				if (!this._DisableForcedUpdatesOnLayout)
				{
					Canvas.ForceUpdateCanvases();
				}
				this.FirstLateUpdate = false;
			}
		}

		// Token: 0x0600D213 RID: 53779 RVA: 0x004E0230 File Offset: 0x004DE430
		private void UpdateLayout()
		{
			this.scrollContext.IsActive(true);
			if (this.rtWidth != null)
			{
				RectTransform rtWidth = this.rtWidth;
				float? num = ((rtWidth != null) ? new float?(rtWidth.rect.width) : null);
				float num2 = this.lastWidth;
				if (!((num.GetValueOrDefault() == num2) & (num != null)))
				{
					RectTransform rtWidth2 = this.rtWidth;
					this.lastWidth = ((rtWidth2 != null) ? rtWidth2.rect.width : 0f);
					this.UpdateWidth();
				}
			}
			if (this.scrollRect != null && this.gridLayout != null)
			{
				if (this.layout.preferredHeight != this.gridLayout.preferredHeight + 10f)
				{
					this.layout.preferredHeight = this.gridLayout.preferredHeight + 10f + (float)(this.autoHotkey ? 16 : 0);
				}
			}
			else if (this.scrollRect != null && this.contentFitterLayoutElement != null)
			{
				float num3 = this.contentFitterLayoutElementRectTransform.rect.height + 10f + (float)(this.autoHotkey ? 16 : 0);
				float num4 = this.contentFitterLayoutElementRectTransform.rect.width + 10f;
				if (this.layout.preferredHeight != num3)
				{
					this.layout.preferredHeight = num3;
				}
				if (this.layout.preferredWidth != num4)
				{
					this.layout.preferredWidth = num4;
				}
			}
			this.UpdateCentering();
		}

		// Token: 0x0600D214 RID: 53780 RVA: 0x004E03D4 File Offset: 0x004DE5D4
		public virtual void Update()
		{
			bool flag = this.scrollContext.IsActive(true);
			if (flag != this.wasActive || (flag && this.scrollContext.selectedPosition != this.selectedPosition))
			{
				this.wasActive = flag;
				this.selectedPosition = this.scrollContext.selectedPosition;
				if (flag)
				{
					this.UpdateSelection();
				}
			}
			this.UpdateLayout();
			if (flag)
			{
				foreach (KeyValuePair<KeyCode, FrameworkDataElement> keyValuePair in this.hotkeyChoices)
				{
					if (ControlManager.isKeyDown(keyValuePair.Key))
					{
						if (The.CurrentContext == The.UiContext)
						{
							ControlManager.ResetInput(false, true);
						}
						UnityEvent<FrameworkDataElement> unityEvent = this.onSelected;
						if (unityEvent == null)
						{
							break;
						}
						unityEvent.Invoke(keyValuePair.Value);
						break;
					}
				}
			}
		}

		// Token: 0x0600D215 RID: 53781 RVA: 0x004E04B4 File Offset: 0x004DE6B4
		public virtual void Awake()
		{
		}

		// Token: 0x0600D216 RID: 53782 RVA: 0x004E04B8 File Offset: 0x004DE6B8
		public void UpdateCentering()
		{
			if (this.centeringEnabled && this.centeringFreeSpace != null && this.centeringFrame != null && this.centeringFreeSpace.rect.y != this.lastHeight)
			{
				if (!this._DisableForcedUpdatesOnLayout)
				{
					Canvas.ForceUpdateCanvases();
				}
				this.lastHeight = this.centeringFreeSpace.rect.y;
				float num = Math.Min(this.centeringFreeSpace.rect.height, Math.Max(0f, this.centeringFrame.rect.height / 2f - 140f));
				this.centeringFrame.anchoredPosition = new Vector2(this.centeringFrame.anchoredPosition.x, -num);
				if (!this._DisableForcedUpdatesOnLayout)
				{
					Canvas.ForceUpdateCanvases();
				}
				TutorialManager.RefreshHighlightPosition();
			}
		}

		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x0600D217 RID: 53783 RVA: 0x004E05AC File Offset: 0x004DE7AC
		private LayoutElement layout
		{
			get
			{
				LayoutElement layoutElement;
				if ((layoutElement = this._layout) == null)
				{
					layoutElement = (this._layout = this.scrollRect.GetComponent<LayoutElement>());
				}
				return layoutElement;
			}
		}

		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x0600D218 RID: 53784 RVA: 0x004E05D8 File Offset: 0x004DE7D8
		private RectTransform contentFitterLayoutElementRectTransform
		{
			get
			{
				RectTransform rectTransform;
				if ((rectTransform = this._contentFitterLayoutElementRectTransform) == null)
				{
					rectTransform = (this._contentFitterLayoutElementRectTransform = this.contentFitterLayoutElement.GetComponent<RectTransform>());
				}
				return rectTransform;
			}
		}

		// Token: 0x0600D219 RID: 53785 RVA: 0x004E0603 File Offset: 0x004DE803
		public virtual void ScrollSelectedIntoView()
		{
			if (this.ScrollOnSelection())
			{
				ScrollViewCalcs.ScrollIntoView(this.GetPrefabForIndex(this.selectedPosition).GetComponent<RectTransform>(), this.scrollViewCalcs);
			}
		}

		// Token: 0x0600D21A RID: 53786 RVA: 0x004E062E File Offset: 0x004DE82E
		public virtual void ScrollSelectedToTop()
		{
			ScrollViewCalcs.ScrollToTopOfRect(this.GetPrefabForIndex(this.selectedPosition).GetComponent<RectTransform>(), this.scrollViewCalcs);
		}

		// Token: 0x0600D21B RID: 53787 RVA: 0x004E064C File Offset: 0x004DE84C
		public IEnumerable<FrameworkDataElement> ChoiceAndChildren(FrameworkDataElement choice)
		{
			yield return choice;
			IFrameworkDataList frameworkDataList = choice as IFrameworkDataList;
			IEnumerable<FrameworkDataElement> enumerable = ((frameworkDataList != null) ? frameworkDataList.getChildren().SelectMany<FrameworkDataElement, FrameworkDataElement>(new Func<FrameworkDataElement, IEnumerable<FrameworkDataElement>>(this.ChoiceAndChildren)) : null);
			if (enumerable != null)
			{
				foreach (FrameworkDataElement frameworkDataElement in enumerable)
				{
					yield return frameworkDataElement;
				}
				IEnumerator<FrameworkDataElement> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x0400531E RID: 21278
		public InputAxisTypes NavigationAxis;

		// Token: 0x0400531F RID: 21279
		public ScrollContext<FrameworkDataElement, NavigationContext> scrollContext = new ScrollContext<FrameworkDataElement, NavigationContext>();

		// Token: 0x04005320 RID: 21280
		public List<FrameworkDataElement> choices = new List<FrameworkDataElement>();

		// Token: 0x04005321 RID: 21281
		public bool EnablePaging;

		// Token: 0x04005322 RID: 21282
		public GameObject childRoot;

		// Token: 0x04005323 RID: 21283
		public FrameworkUnityScrollChild selectionPrefab;

		// Token: 0x04005324 RID: 21284
		public LayoutElement contentFitterLayoutElement;

		// Token: 0x04005325 RID: 21285
		public GameObject spacerPrefab;

		// Token: 0x04005326 RID: 21286
		public UITextSkin titleText;

		// Token: 0x04005327 RID: 21287
		public UnityEvent<FrameworkDataElement> onSelected = new UnityEvent<FrameworkDataElement>();

		// Token: 0x04005328 RID: 21288
		public UnityEvent<FrameworkDataElement> onHighlight = new UnityEvent<FrameworkDataElement>();

		// Token: 0x04005329 RID: 21289
		public int selectedPosition;

		// Token: 0x0400532A RID: 21290
		public bool autoHotkey;

		// Token: 0x0400532B RID: 21291
		public Dictionary<KeyCode, FrameworkDataElement> hotkeyChoices = new Dictionary<KeyCode, FrameworkDataElement>();

		// Token: 0x0400532C RID: 21292
		public ScrollRect scrollRect;

		// Token: 0x0400532D RID: 21293
		private bool _gridLayoutInitialized;

		// Token: 0x0400532E RID: 21294
		private GridLayoutGroup _gridLayout;

		// Token: 0x0400532F RID: 21295
		public List<FrameworkUnityScrollChild> selectionClones = new List<FrameworkUnityScrollChild>();

		// Token: 0x04005330 RID: 21296
		public List<GameObject> spacerClones = new List<GameObject>();

		// Token: 0x04005331 RID: 21297
		protected List<NavigationContext> lastContexts = new List<NavigationContext>();

		// Token: 0x04005332 RID: 21298
		public bool putSpacersTopAndBottom;

		// Token: 0x04005333 RID: 21299
		public bool basePrefabPoolingAllowed;

		// Token: 0x04005334 RID: 21300
		private Queue<GameObject> prefabPool = new Queue<GameObject>();

		// Token: 0x04005335 RID: 21301
		protected ScrollViewCalcs scrollCalcs;

		// Token: 0x04005336 RID: 21302
		public UnityEvent<FrameworkUnityScrollChild, ScrollChildContext, FrameworkDataElement, int> PostSetup = new UnityEvent<FrameworkUnityScrollChild, ScrollChildContext, FrameworkDataElement, int>();

		// Token: 0x04005337 RID: 21303
		public float lastWidth;

		// Token: 0x04005338 RID: 21304
		private RectTransform _rtWidth;

		// Token: 0x04005339 RID: 21305
		public bool wasActive = true;

		// Token: 0x0400533A RID: 21306
		private bool FirstLateUpdate = true;

		// Token: 0x0400533B RID: 21307
		private bool _DisableForcedUpdatesOnLayout;

		// Token: 0x0400533C RID: 21308
		public bool centeringEnabled = true;

		// Token: 0x0400533D RID: 21309
		public RectTransform centeringFreeSpace;

		// Token: 0x0400533E RID: 21310
		public RectTransform centeringFrame;

		// Token: 0x0400533F RID: 21311
		public RectTransform centeringLowestObject;

		// Token: 0x04005340 RID: 21312
		public float lastHeight = float.NaN;

		// Token: 0x04005341 RID: 21313
		private LayoutElement _layout;

		// Token: 0x04005342 RID: 21314
		private RectTransform _contentFitterLayoutElementRectTransform;

		// Token: 0x04005343 RID: 21315
		protected ScrollViewCalcs scrollViewCalcs = new ScrollViewCalcs();

		// Token: 0x04005344 RID: 21316
		public Func<bool> ScrollOnSelection = () => true;

		// Token: 0x02001E1D RID: 7709
		public interface IIndexSensitiveScrollChild
		{
			// Token: 0x0600F630 RID: 63024
			void SetIndex(int windowIndex);
		}
	}
}
