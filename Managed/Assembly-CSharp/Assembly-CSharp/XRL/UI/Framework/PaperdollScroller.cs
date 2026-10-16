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
	// Token: 0x020014F0 RID: 5360
	public class PaperdollScroller : MonoBehaviour, IFrameworkContext
	{
		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x0600D169 RID: 53609 RVA: 0x004DCBFC File Offset: 0x004DADFC
		public Event currentEvent
		{
			get
			{
				return NavigationController.currentEvent;
			}
		}

		// Token: 0x0600D16A RID: 53610 RVA: 0x004DCC03 File Offset: 0x004DAE03
		public virtual NavigationContext GetNavigationContext()
		{
			return this.scrollContext;
		}

		// Token: 0x0600D16B RID: 53611 RVA: 0x004DCC0B File Offset: 0x004DAE0B
		public void Start()
		{
			this.selectedPosition = 0;
			if (this.scrollContext != null)
			{
				this.scrollContext.data = this.choices;
			}
			this.scrollContext.paperdollLayoutGroup = this.paperdollLayoutGroup;
		}

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x0600D16C RID: 53612 RVA: 0x004DCC3E File Offset: 0x004DAE3E
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

		// Token: 0x0600D16D RID: 53613 RVA: 0x004DCC5F File Offset: 0x004DAE5F
		public virtual FrameworkUnityScrollChild GetPrefabForIndex(int x)
		{
			while (this.selectionClones.Count <= x)
			{
				this.selectionClones.Add(global::UnityEngine.Object.Instantiate<FrameworkUnityScrollChild>(this.selectionPrefab));
			}
			return this.selectionClones[x];
		}

		// Token: 0x0600D16E RID: 53614 RVA: 0x004DCC94 File Offset: 0x004DAE94
		public virtual void LayoutChildren()
		{
			this.paperdollElements.ForEach(delegate(PaperdollLayoutElement e)
			{
				e.clearConnector();
			});
			List<GameObject> list = new List<GameObject>();
			foreach (object obj in this.childRoot.transform)
			{
				Transform transform = (Transform)obj;
				if (!this.selectionClones.Contains(transform.GetComponent<FrameworkUnityScrollChild>()))
				{
					list.Add(transform.gameObject);
				}
			}
			if (this.selectionClones.Count > this.choices.Count)
			{
				list.AddRange(from clone in this.selectionClones.Skip<FrameworkUnityScrollChild>(this.choices.Count)
					select clone.gameObject);
				this.selectionClones.RemoveRange(this.choices.Count, this.selectionClones.Count - this.choices.Count);
			}
			list.ForEach(delegate(GameObject child)
			{
				try
				{
					child.DestroyImmediate();
				}
				catch
				{
				}
			});
			this.paperdollElements.Clear();
			foreach (FrameworkDataElement frameworkDataElement in this.choices)
			{
				int count = this.scrollContext.contexts.Count;
				ScrollChildContext scrollChildContext = this.MakeContextFor(frameworkDataElement, count);
				this.scrollContext.contexts.Add(scrollChildContext);
				NavigationContext navigationContext = this.lastContexts.ElementAtOrDefault(count, null);
				if (navigationContext != null && navigationContext.IsActive(true))
				{
					scrollChildContext.Activate(false);
				}
				FrameworkUnityScrollChild prefabForIndex = this.GetPrefabForIndex(count);
				prefabForIndex.transform.SetParent(this.childRoot.transform, false);
				this.SetupPrefab(prefabForIndex, scrollChildContext, frameworkDataElement, count);
				scrollChildContext.Setup();
				PaperdollLayoutElement component = prefabForIndex.GetComponent<PaperdollLayoutElement>();
				component.context = scrollChildContext;
				component.scrollChild = prefabForIndex;
				component.data = frameworkDataElement as EquipmentLineData;
				this.paperdollElements.Add(component);
			}
		}

		// Token: 0x0600D16F RID: 53615 RVA: 0x004DCEF8 File Offset: 0x004DB0F8
		public virtual void ChangePrefabs(FrameworkUnityScrollChild newSelectionPrefab)
		{
			this.selectionPrefab = newSelectionPrefab;
			this.InvalidatePrefabs();
		}

		// Token: 0x0600D170 RID: 53616 RVA: 0x004DCF08 File Offset: 0x004DB108
		protected virtual void InvalidatePrefabs()
		{
			foreach (Transform transform in this.selectionClones.Select<FrameworkUnityScrollChild, Transform>((FrameworkUnityScrollChild s) => s.transform))
			{
				global::UnityEngine.Object.Destroy(transform);
			}
			this.selectionClones.Clear();
		}

		// Token: 0x0600D171 RID: 53617 RVA: 0x004DCF84 File Offset: 0x004DB184
		public virtual void UpdateSelections(IEnumerable<FrameworkDataElement> selections = null)
		{
			if (selections == null)
			{
				selections = this.choices;
			}
			this.choices = (this.scrollContext.data = new List<FrameworkDataElement>(selections));
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
			this.paperdollLayoutGroup.Setup(this.paperdollElements);
		}

		// Token: 0x0600D172 RID: 53618 RVA: 0x004DD114 File Offset: 0x004DB314
		public void BeforeShow(IEnumerable<FrameworkDataElement> selections = null)
		{
			this.BeforeShow(null, selections);
		}

		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x0600D173 RID: 53619 RVA: 0x004DD11E File Offset: 0x004DB31E
		private PaperdollLayoutElement selectedElement
		{
			get
			{
				return this.paperdollElements[this.scrollContext.selectedPosition];
			}
		}

		// Token: 0x0600D174 RID: 53620 RVA: 0x004DD138 File Offset: 0x004DB338
		public virtual void HandleXAxisEvent()
		{
			if (this.currentEvent.axisValue != null)
			{
				int? axisValue = this.currentEvent.axisValue;
				int num = 0;
				if ((axisValue.GetValueOrDefault() > num) & (axisValue != null))
				{
					int num2 = this.paperdollLayoutGroup.RightOf(this.selectedElement);
					if (num2 >= 0)
					{
						this.paperdollElements[num2].GetComponent<FrameworkContext>().context.Activate(false);
						this.currentEvent.Handle();
						return;
					}
				}
				else
				{
					int num3 = this.paperdollLayoutGroup.LeftOf(this.selectedElement);
					if (num3 >= 0)
					{
						this.paperdollElements[num3].GetComponent<FrameworkContext>().context.Activate(false);
						this.currentEvent.Handle();
					}
				}
			}
		}

		// Token: 0x0600D175 RID: 53621 RVA: 0x004DD1FC File Offset: 0x004DB3FC
		public virtual void HandleYAxisEvent()
		{
			if (this.currentEvent.axisValue != null)
			{
				int? axisValue = this.currentEvent.axisValue;
				int num = 0;
				if ((axisValue.GetValueOrDefault() < num) & (axisValue != null))
				{
					int num2 = this.paperdollLayoutGroup.AboveOf(this.selectedElement);
					if (num2 >= 0)
					{
						this.paperdollElements[num2].GetComponent<FrameworkContext>().context.Activate(false);
						this.currentEvent.Handle();
						return;
					}
				}
				else
				{
					int num3 = this.paperdollLayoutGroup.BelowOf(this.selectedElement);
					if (num3 >= 0)
					{
						this.paperdollElements[num3].GetComponent<FrameworkContext>().context.Activate(false);
						this.currentEvent.Handle();
					}
				}
			}
		}

		// Token: 0x0600D176 RID: 53622 RVA: 0x004DD2C0 File Offset: 0x004DB4C0
		public virtual void BeforeShow(EmbarkBuilderModuleWindowDescriptor descriptor, IEnumerable<FrameworkDataElement> selections = null)
		{
			this.hotkeyChoices.Clear();
			this.scrollContext.wraps = false;
			PaperdollScroller.PaperdollScrollerContext<FrameworkDataElement, NavigationContext> paperdollScrollerContext = this.scrollContext;
			if (paperdollScrollerContext.axisHandlers == null)
			{
				paperdollScrollerContext.axisHandlers = new Dictionary<InputAxisTypes, Action>();
			}
			this.scrollContext.axisHandlers[InputAxisTypes.NavigationXAxis] = new Action(this.HandleXAxisEvent);
			this.scrollContext.axisHandlers[InputAxisTypes.NavigationYAxis] = new Action(this.HandleYAxisEvent);
			if (this.EnablePaging)
			{
				this.scrollContext.axisHandlers.Set(this.PageAxis, Event.Helpers.Handle(Event.Helpers.Axis(new Action(this.DoPageDown), new Action(this.DoPageUp), null)));
			}
			this.UpdateSelections(selections);
		}

		// Token: 0x0600D177 RID: 53623 RVA: 0x004DD384 File Offset: 0x004DB584
		public void SelectHighest()
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

		// Token: 0x0600D178 RID: 53624 RVA: 0x004DD410 File Offset: 0x004DB610
		public void SelectLowest()
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

		// Token: 0x0600D179 RID: 53625 RVA: 0x004DD49C File Offset: 0x004DB69C
		public virtual void DoPageUp()
		{
			this.scrollRect.verticalNormalizedPosition += this.scrollRect.viewport.rect.height / this.scrollRect.content.rect.height;
			this.SelectHighest();
		}

		// Token: 0x0600D17A RID: 53626 RVA: 0x004DD4F4 File Offset: 0x004DB6F4
		public virtual void DoPageDown()
		{
			this.scrollRect.verticalNormalizedPosition -= this.scrollRect.viewport.rect.height / this.scrollRect.content.rect.height;
			this.SelectLowest();
		}

		// Token: 0x0600D17B RID: 53627 RVA: 0x004DD54C File Offset: 0x004DB74C
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

		// Token: 0x0600D17C RID: 53628 RVA: 0x004DD5A5 File Offset: 0x004DB7A5
		public virtual void SetupPrefab(FrameworkUnityScrollChild newChild, ScrollChildContext context, FrameworkDataElement data, int index)
		{
			newChild.Setup(data, context);
			this.PostSetup.Invoke(newChild, context, data, index);
		}

		// Token: 0x0600D17D RID: 53629 RVA: 0x004DD5C0 File Offset: 0x004DB7C0
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
					UnityEvent<FrameworkDataElement> unityEvent = this.onSelected;
					if (unityEvent == null)
					{
						return;
					}
					unityEvent.Invoke(this.choices[context.index]);
				})
			} };
			return context;
		}

		// Token: 0x0600D17E RID: 53630 RVA: 0x004DD61A File Offset: 0x004DB81A
		public virtual void UpdateSelection()
		{
			this.ScrollSelectedIntoView();
			UnityEvent<FrameworkDataElement> unityEvent = this.onHighlight;
			if (unityEvent == null)
			{
				return;
			}
			unityEvent.Invoke(this.scrollContext.data[this.selectedPosition]);
		}

		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x0600D17F RID: 53631 RVA: 0x004DD648 File Offset: 0x004DB848
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

		// Token: 0x0600D180 RID: 53632 RVA: 0x004DD68B File Offset: 0x004DB88B
		public virtual void UpdateWidth()
		{
		}

		// Token: 0x0600D181 RID: 53633 RVA: 0x004DD68D File Offset: 0x004DB88D
		public virtual void LateUpdate()
		{
			if (this.FirstLateUpdate)
			{
				Canvas.ForceUpdateCanvases();
				this.UpdateLayout();
				Canvas.ForceUpdateCanvases();
				this.FirstLateUpdate = false;
			}
		}

		// Token: 0x0600D182 RID: 53634 RVA: 0x004DD6B0 File Offset: 0x004DB8B0
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
			this.UpdateCentering();
		}

		// Token: 0x0600D183 RID: 53635 RVA: 0x004DD74C File Offset: 0x004DB94C
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

		// Token: 0x0600D184 RID: 53636 RVA: 0x004DD82C File Offset: 0x004DBA2C
		public virtual void Awake()
		{
		}

		// Token: 0x0600D185 RID: 53637 RVA: 0x004DD830 File Offset: 0x004DBA30
		public void UpdateCentering()
		{
			if (this.centeringEnabled && this.centeringFreeSpace != null && this.centeringFrame != null && this.centeringFreeSpace.rect.y != this.lastHeight)
			{
				Canvas.ForceUpdateCanvases();
				this.lastHeight = this.centeringFreeSpace.rect.y;
				float num = Math.Min(this.centeringFreeSpace.rect.height, Math.Max(0f, this.centeringFrame.rect.height / 2f - 140f));
				this.centeringFrame.anchoredPosition = new Vector2(this.centeringFrame.anchoredPosition.x, -num);
				Canvas.ForceUpdateCanvases();
			}
		}

		// Token: 0x0600D186 RID: 53638 RVA: 0x004DD90F File Offset: 0x004DBB0F
		public virtual void ScrollSelectedIntoView()
		{
			if (this.ScrollOnSelection())
			{
				ScrollViewCalcs.ScrollIntoView(this.GetPrefabForIndex(this.selectedPosition).GetComponent<RectTransform>(), this.scrollViewCalcs);
			}
		}

		// Token: 0x0600D187 RID: 53639 RVA: 0x004DD93A File Offset: 0x004DBB3A
		public virtual void ScrollSelectedToTop()
		{
			ScrollViewCalcs.ScrollToTopOfRect(this.GetPrefabForIndex(this.selectedPosition).GetComponent<RectTransform>(), this.scrollViewCalcs);
		}

		// Token: 0x0600D188 RID: 53640 RVA: 0x004DD958 File Offset: 0x004DBB58
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

		// Token: 0x040052B2 RID: 21170
		public InputAxisTypes NavigationAxis = InputAxisTypes.NavigationYAxis;

		// Token: 0x040052B3 RID: 21171
		public PaperdollScroller.PaperdollScrollerContext<FrameworkDataElement, NavigationContext> scrollContext = new PaperdollScroller.PaperdollScrollerContext<FrameworkDataElement, NavigationContext>();

		// Token: 0x040052B4 RID: 21172
		public List<FrameworkDataElement> choices = new List<FrameworkDataElement>();

		// Token: 0x040052B5 RID: 21173
		public bool EnablePaging;

		// Token: 0x040052B6 RID: 21174
		public PaperdollLayoutGroup paperdollLayoutGroup;

		// Token: 0x040052B7 RID: 21175
		public GameObject childRoot;

		// Token: 0x040052B8 RID: 21176
		public FrameworkUnityScrollChild selectionPrefab;

		// Token: 0x040052B9 RID: 21177
		public UnityEvent<FrameworkDataElement> onSelected = new UnityEvent<FrameworkDataElement>();

		// Token: 0x040052BA RID: 21178
		public UnityEvent<FrameworkDataElement> onHighlight = new UnityEvent<FrameworkDataElement>();

		// Token: 0x040052BB RID: 21179
		public int selectedPosition;

		// Token: 0x040052BC RID: 21180
		public bool autoHotkey;

		// Token: 0x040052BD RID: 21181
		public Dictionary<KeyCode, FrameworkDataElement> hotkeyChoices = new Dictionary<KeyCode, FrameworkDataElement>();

		// Token: 0x040052BE RID: 21182
		public ScrollRect scrollRect;

		// Token: 0x040052BF RID: 21183
		public List<FrameworkUnityScrollChild> selectionClones = new List<FrameworkUnityScrollChild>();

		// Token: 0x040052C0 RID: 21184
		protected List<NavigationContext> lastContexts = new List<NavigationContext>();

		// Token: 0x040052C1 RID: 21185
		private List<PaperdollLayoutElement> paperdollElements = new List<PaperdollLayoutElement>();

		// Token: 0x040052C2 RID: 21186
		protected ScrollViewCalcs scrollCalcs;

		// Token: 0x040052C3 RID: 21187
		public UnityEvent<FrameworkUnityScrollChild, ScrollChildContext, FrameworkDataElement, int> PostSetup = new UnityEvent<FrameworkUnityScrollChild, ScrollChildContext, FrameworkDataElement, int>();

		// Token: 0x040052C4 RID: 21188
		public float lastWidth;

		// Token: 0x040052C5 RID: 21189
		private RectTransform _rtWidth;

		// Token: 0x040052C6 RID: 21190
		public bool wasActive = true;

		// Token: 0x040052C7 RID: 21191
		private bool FirstLateUpdate = true;

		// Token: 0x040052C8 RID: 21192
		public bool centeringEnabled = true;

		// Token: 0x040052C9 RID: 21193
		public RectTransform centeringFreeSpace;

		// Token: 0x040052CA RID: 21194
		public RectTransform centeringFrame;

		// Token: 0x040052CB RID: 21195
		public RectTransform centeringLowestObject;

		// Token: 0x040052CC RID: 21196
		public float lastHeight = float.NaN;

		// Token: 0x040052CD RID: 21197
		protected ScrollViewCalcs scrollViewCalcs = new ScrollViewCalcs();

		// Token: 0x040052CE RID: 21198
		public Func<bool> ScrollOnSelection = () => true;

		// Token: 0x02001E13 RID: 7699
		public class PaperdollScrollerContext<T, U> : ScrollContext<T, U> where U : NavigationContext
		{
			// Token: 0x0600F603 RID: 62979 RVA: 0x005588E8 File Offset: 0x00556AE8
			public override void OnEnter()
			{
				NavigationContext navigationContext = (NavigationContext)base.currentEvent.data["to"];
				Event triggeringEvent = NavigationController.instance.triggeringEvent;
				bool flag = false;
				try
				{
					InputAxisTypes? inputAxisTypes = ((triggeringEvent != null) ? triggeringEvent.data["axis"] : null) as InputAxisTypes?;
					InputAxisTypes axisType = this.AxisType;
					flag = (inputAxisTypes.GetValueOrDefault() == axisType) & (inputAxisTypes != null);
				}
				catch (Exception)
				{
				}
				if (navigationContext == this)
				{
					NavigationContext navigationContext2 = (NavigationContext)base.currentEvent.data["from"];
					IEnumerable<NavigationContext> enumerable = ((navigationContext2 != null) ? navigationContext2.parents.Prepend(navigationContext2).TakeWhile<NavigationContext>((NavigationContext c) => !this.parents.Prepend(this).Contains(c)) : null);
					object obj;
					if (enumerable == null)
					{
						obj = null;
					}
					else
					{
						obj = enumerable.FirstOrDefault<NavigationContext>((NavigationContext c) => c is AbstractScrollContext);
					}
					AbstractScrollContext abstractScrollContext = obj as AbstractScrollContext;
					if (triggeringEvent != null && triggeringEvent.axisValue != null && this.rowWidth > 0 && abstractScrollContext != null && abstractScrollContext.AxisType == this.AxisType)
					{
						if (flag)
						{
							if (triggeringEvent != null)
							{
								int? num = triggeringEvent.axisValue;
								int num2 = 0;
								if ((num.GetValueOrDefault() > num2) & (num != null))
								{
									int num3 = (this.length - 1) / this.rowWidth;
									this.SelectIndex(Math.Min(this.length - 1, this.rowWidth * Math.Min(num3, abstractScrollContext.selectedRow)));
									goto IL_0333;
								}
							}
							if (triggeringEvent != null)
							{
								int? num = triggeringEvent.axisValue;
								int num2 = 0;
								if ((num.GetValueOrDefault() < num2) & (num != null))
								{
									int num4 = Math.Min((this.length - 1) / this.rowWidth, abstractScrollContext.selectedRow);
									this.SelectIndex(Math.Min(this.length - 1, this.rowWidth * (num4 + 1) - 1));
								}
							}
						}
						else
						{
							if (triggeringEvent != null)
							{
								int? num = triggeringEvent.axisValue;
								int num2 = 0;
								if ((num.GetValueOrDefault() > num2) & (num != null))
								{
									int selectedColumn = abstractScrollContext.selectedColumn;
									this.SelectIndex(Math.Min(this.length - 1, selectedColumn));
									goto IL_0333;
								}
							}
							if (triggeringEvent != null)
							{
								int? num = triggeringEvent.axisValue;
								int num2 = 0;
								if ((num.GetValueOrDefault() < num2) & (num != null))
								{
									int num5 = abstractScrollContext.selectedColumn;
									while (num5 + this.rowWidth < this.length)
									{
										num5 += this.rowWidth;
									}
									this.SelectIndex(Math.Min(this.length - 1, num5));
								}
							}
						}
					}
					else
					{
						if (flag && triggeringEvent != null)
						{
							int? num = triggeringEvent.axisValue;
							int num2 = 1;
							if ((num.GetValueOrDefault() == num2) & (num != null))
							{
								this.SelectIndex(this.paperdollLayoutGroup.Leftmost());
								goto IL_0333;
							}
						}
						if (flag && triggeringEvent != null)
						{
							int? num = triggeringEvent.axisValue;
							int num2 = -1;
							if ((num.GetValueOrDefault() == num2) & (num != null))
							{
								this.SelectIndex(this.paperdollLayoutGroup.Rightmost());
								goto IL_0333;
							}
						}
						this.SelectIndex(Math.Min(Math.Max(0, this.selectedPosition), this.length - 1));
					}
					IL_0333:
					base.currentEvent.Cancel();
				}
				else
				{
					int? num = this.ContextIndex(navigationContext);
					if (num != null)
					{
						int valueOrDefault = num.GetValueOrDefault();
						this.selectedPosition = valueOrDefault;
						base.UpdateGridSiblings();
					}
				}
				if (this.enterHandler != null)
				{
					this.enterHandler();
				}
			}

			// Token: 0x0400756B RID: 30059
			public PaperdollLayoutGroup paperdollLayoutGroup;
		}
	}
}
