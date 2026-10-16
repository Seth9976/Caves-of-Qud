using System;
using System.Collections.Generic;
using Unity.Profiling;
using Unity.Properties;
using UnityEngine.Internal;
using UnityEngine.UIElements.Experimental;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x020002D8 RID: 728
	public class IMGUIContainer : VisualElement, IDisposable
	{
		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x060014D9 RID: 5337 RVA: 0x00053734 File Offset: 0x00051934
		// (set) Token: 0x060014DA RID: 5338 RVA: 0x0005374C File Offset: 0x0005194C
		public Action onGUIHandler
		{
			get
			{
				return this.m_OnGUIHandler;
			}
			set
			{
				bool flag = this.m_OnGUIHandler != value;
				if (flag)
				{
					this.m_OnGUIHandler = value;
					base.IncrementVersion(VersionChangeType.Layout);
					base.IncrementVersion(VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x060014DB RID: 5339 RVA: 0x00053788 File Offset: 0x00051988
		internal ObjectGUIState guiState
		{
			get
			{
				Debug.Assert(!this.useOwnerObjectGUIState, "!useOwnerObjectGUIState");
				bool flag = this.m_ObjectGUIState == null;
				if (flag)
				{
					this.m_ObjectGUIState = new ObjectGUIState();
				}
				return this.m_ObjectGUIState;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x060014DC RID: 5340 RVA: 0x000537CE File Offset: 0x000519CE
		// (set) Token: 0x060014DD RID: 5341 RVA: 0x000537D6 File Offset: 0x000519D6
		internal Rect lastWorldClip { get; set; }

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x060014DE RID: 5342 RVA: 0x000537E0 File Offset: 0x000519E0
		// (set) Token: 0x060014DF RID: 5343 RVA: 0x000537F8 File Offset: 0x000519F8
		[CreateProperty]
		public bool cullingEnabled
		{
			get
			{
				return this.m_CullingEnabled;
			}
			set
			{
				bool flag = this.m_CullingEnabled == value;
				if (!flag)
				{
					this.m_CullingEnabled = value;
					base.IncrementVersion(VersionChangeType.Repaint);
					base.NotifyPropertyChanged(in IMGUIContainer.cullingEnabledProperty);
				}
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x060014E0 RID: 5344 RVA: 0x00053834 File Offset: 0x00051A34
		private GUILayoutUtility.LayoutCache cache
		{
			get
			{
				bool flag = this.m_Cache == null;
				if (flag)
				{
					this.m_Cache = new GUILayoutUtility.LayoutCache(-1);
				}
				return this.m_Cache;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x060014E1 RID: 5345 RVA: 0x00053868 File Offset: 0x00051A68
		private float layoutMeasuredWidth
		{
			get
			{
				return Mathf.Ceil(this.cache.topLevel.maxWidth);
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x060014E2 RID: 5346 RVA: 0x00053890 File Offset: 0x00051A90
		private float layoutMeasuredHeight
		{
			get
			{
				return Mathf.Ceil(this.cache.topLevel.maxHeight);
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x060014E3 RID: 5347 RVA: 0x000538B7 File Offset: 0x00051AB7
		// (set) Token: 0x060014E4 RID: 5348 RVA: 0x000538C0 File Offset: 0x00051AC0
		[CreateProperty]
		public ContextType contextType
		{
			get
			{
				return this.m_ContextType;
			}
			set
			{
				bool flag = this.m_ContextType == value;
				if (!flag)
				{
					this.m_ContextType = value;
					base.NotifyPropertyChanged(in IMGUIContainer.contextTypeProperty);
				}
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x060014E5 RID: 5349 RVA: 0x000538F0 File Offset: 0x00051AF0
		// (set) Token: 0x060014E6 RID: 5350 RVA: 0x000538F8 File Offset: 0x00051AF8
		internal bool focusOnlyIfHasFocusableControls { get; set; } = true;

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x060014E7 RID: 5351 RVA: 0x00053901 File Offset: 0x00051B01
		public override bool canGrabFocus
		{
			get
			{
				return this.focusOnlyIfHasFocusableControls ? (this.hasFocusableControls && base.canGrabFocus) : base.canGrabFocus;
			}
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x00053924 File Offset: 0x00051B24
		static IMGUIContainer()
		{
			for (int i = 0; i <= Foldout.ussFoldoutMaxDepth; i++)
			{
				IMGUIContainer.ussFoldoutChildDepthClassNames.Add(IMGUIContainer.ussFoldoutChildDepthClassName + i.ToString());
			}
			IMGUIContainer.ussFoldoutChildDepthClassNames.Add(IMGUIContainer.ussFoldoutChildDepthClassName + "max");
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x00053A2C File Offset: 0x00051C2C
		public IMGUIContainer()
			: this(null)
		{
		}

		// Token: 0x060014EA RID: 5354 RVA: 0x00053A38 File Offset: 0x00051C38
		public IMGUIContainer(Action onGUIHandler)
		{
			this.isIMGUIContainer = true;
			base.AddToClassList(IMGUIContainer.ussClassName);
			this.onGUIHandler = onGUIHandler;
			this.contextType = ContextType.Editor;
			this.focusable = true;
			base.requireMeasureFunction = true;
			base.generateVisualContent = (Action<MeshGenerationContext>)Delegate.Combine(base.generateVisualContent, new Action<MeshGenerationContext>(this.OnGenerateVisualContent));
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x00053B04 File Offset: 0x00051D04
		private void OnGenerateVisualContent(MeshGenerationContext mgc)
		{
			BaseRuntimePanel baseRuntimePanel = base.elementPanel as BaseRuntimePanel;
			bool flag = baseRuntimePanel != null && baseRuntimePanel.drawsInCameras;
			if (flag)
			{
				Debug.LogError("IMGUIContainer cannot be used in a panel drawn by cameras.");
			}
			else
			{
				this.lastWorldClip = base.elementPanel.repaintData.currentWorldClip;
				mgc.entryRecorder.DrawImmediate(mgc.parentEntry, new Action(this.DoIMGUIRepaint), this.cullingEnabled);
			}
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x00053B78 File Offset: 0x00051D78
		private void SaveGlobals()
		{
			this.m_GUIGlobals.matrix = GUI.matrix;
			this.m_GUIGlobals.color = GUI.color;
			this.m_GUIGlobals.contentColor = GUI.contentColor;
			this.m_GUIGlobals.backgroundColor = GUI.backgroundColor;
			this.m_GUIGlobals.enabled = GUI.enabled;
			this.m_GUIGlobals.changed = GUI.changed;
			bool flag = Event.current != null;
			if (flag)
			{
				this.m_GUIGlobals.displayIndex = Event.current.displayIndex;
			}
			this.m_GUIGlobals.pixelsPerPoint = GUIUtility.pixelsPerPoint;
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x00053C1C File Offset: 0x00051E1C
		private void RestoreGlobals()
		{
			GUI.matrix = this.m_GUIGlobals.matrix;
			GUI.color = this.m_GUIGlobals.color;
			GUI.contentColor = this.m_GUIGlobals.contentColor;
			GUI.backgroundColor = this.m_GUIGlobals.backgroundColor;
			GUI.enabled = this.m_GUIGlobals.enabled;
			GUI.changed = this.m_GUIGlobals.changed;
			bool flag = Event.current != null;
			if (flag)
			{
				Event.current.displayIndex = this.m_GUIGlobals.displayIndex;
			}
			GUIUtility.pixelsPerPoint = this.m_GUIGlobals.pixelsPerPoint;
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x00053CC8 File Offset: 0x00051EC8
		private void DoOnGUI(Event evt, Matrix4x4 parentTransform, Rect clippingRect, bool isComputingLayout, Rect layoutSize, Action onGUIHandler, bool canAffectFocus = true)
		{
			bool flag = onGUIHandler == null || base.panel == null;
			if (!flag)
			{
				int num = GUIClip.Internal_GetCount();
				int guiDepth = GUIUtility.guiDepth;
				this.SaveGlobals();
				float layoutMeasuredWidth = this.layoutMeasuredWidth;
				float layoutMeasuredHeight = this.layoutMeasuredHeight;
				UIElementsUtility.BeginContainerGUI(this.cache, evt, this);
				GUI.color = base.playModeTintColor;
				GUIUtility.pixelsPerPoint = base.scaledPixelsPerPoint;
				bool flag2 = Event.current.type != EventType.Layout;
				if (flag2)
				{
					bool flag3 = this.lostFocus;
					if (flag3)
					{
						bool flag4 = this.focusController != null;
						if (flag4)
						{
							bool flag5 = GUIUtility.OwnsId(GUIUtility.keyboardControl);
							if (flag5)
							{
								GUIUtility.keyboardControl = 0;
								this.focusController.imguiKeyboardControl = 0;
							}
						}
						this.lostFocus = false;
					}
					bool flag6 = this.receivedFocus;
					if (flag6)
					{
						bool flag7 = this.hasFocusableControls;
						if (flag7)
						{
							bool flag8 = this.focusChangeDirection != FocusChangeDirection.unspecified && this.focusChangeDirection != FocusChangeDirection.none;
							if (flag8)
							{
								bool flag9;
								if (Event.current.type == EventType.KeyDown)
								{
									char character = Event.current.character;
									flag9 = character == '\t' || character == '\u0019';
								}
								else
								{
									flag9 = false;
								}
								bool flag10 = flag9;
								if (flag10)
								{
									Event.current.Use();
								}
								bool flag11 = this.focusChangeDirection == VisualElementFocusChangeDirection.left;
								if (flag11)
								{
									GUIUtility.SetKeyboardControlToLastControlId();
								}
								else
								{
									bool flag12 = this.focusChangeDirection == VisualElementFocusChangeDirection.right;
									if (flag12)
									{
										GUIUtility.SetKeyboardControlToFirstControlId();
									}
								}
							}
							else
							{
								bool flag13 = GUIUtility.keyboardControl == 0 && this.m_IsFocusDelegated;
								if (flag13)
								{
									GUIUtility.SetKeyboardControlToFirstControlId();
								}
							}
						}
						bool flag14 = this.focusController != null;
						if (flag14)
						{
							bool flag15 = this.focusController.imguiKeyboardControl != GUIUtility.keyboardControl && this.focusChangeDirection != FocusChangeDirection.unspecified;
							if (flag15)
							{
								this.newKeyboardFocusControlID = GUIUtility.keyboardControl;
							}
							this.focusController.imguiKeyboardControl = GUIUtility.keyboardControl;
						}
						this.receivedFocus = false;
						this.focusChangeDirection = FocusChangeDirection.unspecified;
					}
				}
				EventType type = Event.current.type;
				bool flag16 = false;
				bool flag17 = true;
				int num2 = 0;
				try
				{
					using (new GUIClip.ParentClipScope(parentTransform, clippingRect))
					{
						using (IMGUIContainer.k_OnGUIMarker.Auto())
						{
							onGUIHandler();
						}
					}
				}
				catch (Exception ex)
				{
					bool flag18 = type == EventType.Layout;
					if (!flag18)
					{
						bool flag19 = guiDepth > 0;
						if (flag19)
						{
							flag17 = false;
						}
						throw;
					}
					flag16 = GUIUtility.IsExitGUIException(ex);
					bool flag20 = !flag16;
					if (flag20)
					{
						Debug.LogException(ex);
					}
				}
				finally
				{
					bool flag21 = Event.current.type != EventType.Layout && canAffectFocus;
					if (flag21)
					{
						bool flag22 = Event.current.type == EventType.Used;
						int keyboardControl = GUIUtility.keyboardControl;
						int num3 = GUIUtility.CheckForTabEvent(Event.current);
						bool flag23 = this.focusController != null;
						if (flag23)
						{
							bool flag24 = num3 < 0 && !flag22;
							if (flag24)
							{
								Focusable leafFocusedElement = this.focusController.GetLeafFocusedElement();
								Focusable focusable = this.focusController.FocusNextInDirection(this, (num3 == -1) ? VisualElementFocusChangeDirection.right : VisualElementFocusChangeDirection.left);
								bool flag25 = leafFocusedElement == this;
								if (flag25)
								{
									bool flag26 = focusable == this;
									if (flag26)
									{
										bool flag27 = num3 == -2;
										if (flag27)
										{
											GUIUtility.SetKeyboardControlToLastControlId();
										}
										else
										{
											bool flag28 = num3 == -1;
											if (flag28)
											{
												GUIUtility.SetKeyboardControlToFirstControlId();
											}
										}
										this.newKeyboardFocusControlID = GUIUtility.keyboardControl;
										this.focusController.imguiKeyboardControl = GUIUtility.keyboardControl;
									}
									else
									{
										GUIUtility.keyboardControl = 0;
										this.focusController.imguiKeyboardControl = 0;
									}
								}
							}
							else
							{
								bool flag29 = num3 > 0 && !flag22;
								if (flag29)
								{
									this.focusController.imguiKeyboardControl = GUIUtility.keyboardControl;
									this.newKeyboardFocusControlID = GUIUtility.keyboardControl;
								}
								else
								{
									bool flag30 = num3 == 0;
									if (flag30)
									{
										bool flag31 = type == EventType.MouseDown && !this.focusOnlyIfHasFocusableControls;
										if (flag31)
										{
											this.focusController.SyncIMGUIFocus(GUIUtility.keyboardControl, this, true);
										}
										else
										{
											bool flag32 = keyboardControl != GUIUtility.keyboardControl || type == EventType.MouseDown;
											if (flag32)
											{
												this.focusController.SyncIMGUIFocus(GUIUtility.keyboardControl, this, false);
											}
											else
											{
												bool flag33 = GUIUtility.keyboardControl != this.focusController.imguiKeyboardControl;
												if (flag33)
												{
													this.newKeyboardFocusControlID = GUIUtility.keyboardControl;
													bool flag34 = this.focusController.GetLeafFocusedElement() == this;
													if (flag34)
													{
														this.focusController.imguiKeyboardControl = GUIUtility.keyboardControl;
													}
													else
													{
														this.focusController.SyncIMGUIFocus(GUIUtility.keyboardControl, this, false);
													}
												}
											}
										}
									}
								}
							}
						}
						this.hasFocusableControls = GUIUtility.HasFocusableControls();
					}
					bool flag35 = flag17;
					if (flag35)
					{
						UIElementsUtility.EndContainerGUI(evt, layoutSize);
						this.RestoreGlobals();
					}
					num2 = GUIClip.Internal_GetCount();
					while (GUIClip.Internal_GetCount() > num)
					{
						GUIClip.Internal_Pop();
					}
				}
				bool flag36 = evt.type == EventType.Layout && (!Mathf.Approximately(layoutMeasuredWidth, this.layoutMeasuredWidth) || !Mathf.Approximately(layoutMeasuredHeight, this.layoutMeasuredHeight));
				if (flag36)
				{
					bool flag37 = isComputingLayout && clippingRect == Rect.zero;
					if (flag37)
					{
						base.schedule.Execute(delegate
						{
							base.IncrementVersion(VersionChangeType.Layout);
						});
					}
					else
					{
						base.IncrementVersion(VersionChangeType.Layout);
					}
				}
				bool flag38 = !flag16;
				if (flag38)
				{
					bool flag39 = evt.type != EventType.Ignore && evt.type != EventType.Used;
					if (flag39)
					{
						bool flag40 = num2 > num;
						if (flag40)
						{
							Debug.LogError("GUI Error: You are pushing more GUIClips than you are popping. Make sure they are balanced.");
						}
						else
						{
							bool flag41 = num2 < num;
							if (flag41)
							{
								Debug.LogError("GUI Error: You are popping more GUIClips than you are pushing. Make sure they are balanced.");
							}
						}
					}
				}
				bool flag42 = evt.type == EventType.Used;
				if (flag42)
				{
					base.IncrementVersion(VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x0005434C File Offset: 0x0005254C
		public void MarkDirtyLayout()
		{
			this.m_RefreshCachedLayout = true;
			base.IncrementVersion(VersionChangeType.Layout);
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x00054360 File Offset: 0x00052560
		private void DoIMGUIRepaint()
		{
			using (IMGUIContainer.k_ImmediateCallbackMarker.Auto())
			{
				Utility.DisableScissor();
				using (new GUIClip.ParentClipScope(base.worldTransform, base.worldClip))
				{
					Matrix4x4 currentOffset = base.elementPanel.repaintData.currentOffset;
					this.m_CachedClippingRect = VisualElement.ComputeAAAlignedBound(base.worldClip, currentOffset);
					this.m_CachedTransform = currentOffset * base.worldTransform;
					this.HandleIMGUIEvent(base.elementPanel.repaintData.repaintEvent, this.m_CachedTransform, this.m_CachedClippingRect, this.onGUIHandler, true);
				}
			}
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x00054438 File Offset: 0x00052638
		internal bool SendEventToIMGUI(EventBase evt, bool canAffectFocus = true, bool verifyBounds = true)
		{
			bool flag = evt is IPointerEvent;
			bool flag11;
			if (flag)
			{
				bool flag2 = evt.imguiEvent != null && evt.imguiEvent.isDirectManipulationDevice;
				if (flag2)
				{
					bool flag3 = false;
					EventType rawType = evt.imguiEvent.rawType;
					bool flag4 = evt is PointerDownEvent;
					if (flag4)
					{
						flag3 = true;
						evt.imguiEvent.type = EventType.TouchDown;
					}
					else
					{
						bool flag5 = evt is PointerUpEvent;
						if (flag5)
						{
							flag3 = true;
							evt.imguiEvent.type = EventType.TouchUp;
						}
						else
						{
							bool flag6 = evt is PointerMoveEvent && evt.imguiEvent.rawType == EventType.MouseDrag;
							if (flag6)
							{
								flag3 = true;
								evt.imguiEvent.type = EventType.TouchMove;
							}
							else
							{
								bool flag7 = evt is PointerLeaveEvent;
								if (flag7)
								{
									flag3 = true;
									evt.imguiEvent.type = EventType.TouchLeave;
								}
								else
								{
									bool flag8 = evt is PointerEnterEvent;
									if (flag8)
									{
										flag3 = true;
										evt.imguiEvent.type = EventType.TouchEnter;
									}
								}
							}
						}
					}
					bool flag9 = flag3;
					if (flag9)
					{
						bool flag10 = this.SendEventToIMGUIRaw(evt, canAffectFocus, verifyBounds);
						evt.imguiEvent.type = rawType;
						return flag10;
					}
				}
				flag11 = false;
			}
			else
			{
				flag11 = this.SendEventToIMGUIRaw(evt, canAffectFocus, verifyBounds);
			}
			return flag11;
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x00054580 File Offset: 0x00052780
		private bool SendEventToIMGUIRaw(EventBase evt, bool canAffectFocus, bool verifyBounds)
		{
			bool flag = verifyBounds && !this.VerifyBounds(evt);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3;
				using (new EventDebuggerLogIMGUICall(evt))
				{
					flag3 = this.HandleIMGUIEvent(evt.imguiEvent, canAffectFocus);
				}
				flag2 = flag3;
			}
			return flag2;
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x000545E4 File Offset: 0x000527E4
		private bool VerifyBounds(EventBase evt)
		{
			return this.IsContainerCapturingTheMouse() || !this.IsLocalEvent(evt) || this.IsEventInsideLocalWindow(evt) || IMGUIContainer.IsDockAreaMouseUp(evt);
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x0005461C File Offset: 0x0005281C
		private bool IsContainerCapturingTheMouse()
		{
			IPanel panel = base.panel;
			IMGUIContainer imguicontainer;
			if (panel == null)
			{
				imguicontainer = null;
			}
			else
			{
				EventDispatcher dispatcher = panel.dispatcher;
				imguicontainer = ((dispatcher != null) ? dispatcher.pointerState.GetCapturingElement(PointerId.mousePointerId) : null);
			}
			return this == imguicontainer;
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x0005465C File Offset: 0x0005285C
		private bool IsLocalEvent(EventBase evt)
		{
			long eventTypeId = evt.eventTypeId;
			return eventTypeId == EventBase<MouseDownEvent>.TypeId() || eventTypeId == EventBase<MouseUpEvent>.TypeId() || eventTypeId == EventBase<MouseMoveEvent>.TypeId() || eventTypeId == EventBase<PointerDownEvent>.TypeId() || eventTypeId == EventBase<PointerUpEvent>.TypeId() || eventTypeId == EventBase<PointerMoveEvent>.TypeId();
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x000546A8 File Offset: 0x000528A8
		private bool IsEventInsideLocalWindow(EventBase evt)
		{
			Rect currentClipRect = this.GetCurrentClipRect();
			IPointerEvent pointerEvent = evt as IPointerEvent;
			string text = ((pointerEvent != null) ? pointerEvent.pointerType : null);
			bool flag = text == PointerType.touch || text == PointerType.pen;
			return GUIUtility.HitTest(currentClipRect, evt.originalMousePosition, flag);
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x00054700 File Offset: 0x00052900
		private static bool IsDockAreaMouseUp(EventBase evt)
		{
			bool flag;
			if (evt.eventTypeId == EventBase<MouseUpEvent>.TypeId())
			{
				IMGUIContainer elementTarget = evt.elementTarget;
				VisualElement elementTarget2 = evt.elementTarget;
				flag = elementTarget == ((elementTarget2 != null) ? elementTarget2.elementPanel.rootIMGUIContainer : null);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x00054744 File Offset: 0x00052944
		internal bool HandleIMGUIEvent(Event e, bool canAffectFocus)
		{
			return this.HandleIMGUIEvent(e, this.onGUIHandler, canAffectFocus);
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x00054764 File Offset: 0x00052964
		internal bool HandleIMGUIEvent(Event e, Action onGUIHandler, bool canAffectFocus)
		{
			IMGUIContainer.GetCurrentTransformAndClip(this, e, out this.m_CachedTransform, out this.m_CachedClippingRect);
			return this.HandleIMGUIEvent(e, this.m_CachedTransform, this.m_CachedClippingRect, onGUIHandler, canAffectFocus);
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x000547A0 File Offset: 0x000529A0
		private bool HandleIMGUIEvent(Event e, Matrix4x4 worldTransform, Rect clippingRect, Action onGUIHandler, bool canAffectFocus)
		{
			bool flag = e == null || onGUIHandler == null || base.elementPanel == null || !base.elementPanel.IMGUIEventInterests.WantsEvent(e.rawType);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				EventType rawType = e.rawType;
				bool flag3 = rawType != EventType.Layout;
				if (flag3)
				{
					bool flag4 = this.m_RefreshCachedLayout || base.elementPanel.IMGUIEventInterests.WantsLayoutPass(e.rawType);
					if (flag4)
					{
						e.type = EventType.Layout;
						this.DoOnGUI(e, worldTransform, clippingRect, false, base.layout, onGUIHandler, canAffectFocus);
						this.m_RefreshCachedLayout = false;
						e.type = rawType;
					}
					else
					{
						this.cache.ResetCursor();
					}
				}
				this.DoOnGUI(e, worldTransform, clippingRect, false, base.layout, onGUIHandler, canAffectFocus);
				bool flag5 = this.newKeyboardFocusControlID > 0;
				if (flag5)
				{
					this.newKeyboardFocusControlID = 0;
					Event @event = new Event
					{
						type = EventType.ExecuteCommand,
						commandName = "NewKeyboardFocus"
					};
					this.HandleIMGUIEvent(@event, true);
				}
				bool flag6 = e.rawType == EventType.Used;
				if (flag6)
				{
					flag2 = true;
				}
				else
				{
					bool flag7 = e.rawType == EventType.MouseUp && this.HasMouseCapture();
					if (flag7)
					{
						GUIUtility.hotControl = 0;
					}
					bool flag8 = base.elementPanel == null;
					if (flag8)
					{
						GUIUtility.ExitGUI();
					}
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x0005490B File Offset: 0x00052B0B
		[EventInterest(new Type[]
		{
			typeof(NavigationMoveEvent),
			typeof(NavigationSubmitEvent),
			typeof(NavigationCancelEvent),
			typeof(BlurEvent),
			typeof(FocusEvent),
			typeof(DetachFromPanelEvent),
			typeof(AttachToPanelEvent)
		})]
		[EventInterest(EventInterestOptionsInternal.TriggeredByOS)]
		internal override void HandleEventBubbleUpDisabled(EventBase evt)
		{
			this.HandleEventBubbleUp(evt);
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x00054918 File Offset: 0x00052B18
		[EventInterest(EventInterestOptionsInternal.TriggeredByOS)]
		[EventInterest(new Type[]
		{
			typeof(NavigationMoveEvent),
			typeof(NavigationSubmitEvent),
			typeof(NavigationCancelEvent),
			typeof(BlurEvent),
			typeof(FocusEvent),
			typeof(DetachFromPanelEvent),
			typeof(AttachToPanelEvent)
		})]
		protected override void HandleEventBubbleUp(EventBase evt)
		{
			bool flag = (evt.imguiEvent != null && this.SendEventToIMGUI(evt, true, true)) || evt.eventTypeId == EventBase<NavigationMoveEvent>.TypeId() || evt.eventTypeId == EventBase<NavigationSubmitEvent>.TypeId() || evt.eventTypeId == EventBase<NavigationCancelEvent>.TypeId();
			if (flag)
			{
				evt.StopPropagation();
				FocusController focusController = this.focusController;
				if (focusController != null)
				{
					focusController.IgnoreEvent(evt);
				}
			}
			else
			{
				bool flag2 = evt.eventTypeId == EventBase<BlurEvent>.TypeId();
				if (flag2)
				{
					this.lostFocus = true;
					base.IncrementVersion(VersionChangeType.Repaint);
				}
				else
				{
					bool flag3 = evt.eventTypeId == EventBase<FocusEvent>.TypeId();
					if (flag3)
					{
						FocusEvent focusEvent = evt as FocusEvent;
						this.receivedFocus = true;
						this.focusChangeDirection = focusEvent.direction;
						this.m_IsFocusDelegated = focusEvent.IsFocusDelegated;
					}
					else
					{
						bool flag4 = evt.eventTypeId == EventBase<DetachFromPanelEvent>.TypeId();
						if (flag4)
						{
							bool flag5 = base.elementPanel != null;
							if (flag5)
							{
								BaseVisualElementPanel elementPanel = base.elementPanel;
								int num = elementPanel.IMGUIContainersCount;
								elementPanel.IMGUIContainersCount = num - 1;
							}
						}
						else
						{
							bool flag6 = evt.eventTypeId == EventBase<AttachToPanelEvent>.TypeId();
							if (flag6)
							{
								bool flag7 = base.elementPanel != null;
								if (flag7)
								{
									BaseVisualElementPanel elementPanel2 = base.elementPanel;
									int num = elementPanel2.IMGUIContainersCount;
									elementPanel2.IMGUIContainersCount = num + 1;
									this.SetFoldoutDepthClass();
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x00054A78 File Offset: 0x00052C78
		private void SetFoldoutDepthClass()
		{
			for (int i = 0; i < IMGUIContainer.ussFoldoutChildDepthClassNames.Count; i++)
			{
				base.RemoveFromClassList(IMGUIContainer.ussFoldoutChildDepthClassNames[i]);
			}
			int num = this.GetFoldoutDepth();
			bool flag = num == 0;
			if (!flag)
			{
				num = Mathf.Min(num, IMGUIContainer.ussFoldoutChildDepthClassNames.Count - 1);
				base.AddToClassList(IMGUIContainer.ussFoldoutChildDepthClassNames[num]);
			}
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x00054AEC File Offset: 0x00052CEC
		protected internal override Vector2 DoMeasure(float desiredWidth, VisualElement.MeasureMode widthMode, float desiredHeight, VisualElement.MeasureMode heightMode)
		{
			float num = float.NaN;
			float num2 = float.NaN;
			bool flag = false;
			bool flag2 = widthMode != VisualElement.MeasureMode.Exactly || heightMode != VisualElement.MeasureMode.Exactly;
			if (flag2)
			{
				bool flag3 = Event.current != null;
				if (flag3)
				{
					IMGUIContainer.s_CurrentEvent.CopyFrom(Event.current);
					flag = true;
				}
				IMGUIContainer.s_MeasureEvent.CopyFrom(IMGUIContainer.s_DefaultMeasureEvent);
				Rect layout = base.layout;
				if (widthMode == VisualElement.MeasureMode.Exactly)
				{
					layout.width = desiredWidth;
				}
				if (heightMode == VisualElement.MeasureMode.Exactly)
				{
					layout.height = desiredHeight;
				}
				this.DoOnGUI(IMGUIContainer.s_MeasureEvent, this.m_CachedTransform, this.m_CachedClippingRect, true, layout, this.onGUIHandler, true);
				num = this.layoutMeasuredWidth;
				num2 = this.layoutMeasuredHeight;
				bool flag4 = flag;
				if (flag4)
				{
					Event.current.CopyFrom(IMGUIContainer.s_CurrentEvent);
				}
			}
			if (widthMode != VisualElement.MeasureMode.Exactly)
			{
				if (widthMode == VisualElement.MeasureMode.AtMost)
				{
					num = Mathf.Min(num, desiredWidth);
				}
			}
			else
			{
				num = desiredWidth;
			}
			if (heightMode != VisualElement.MeasureMode.Exactly)
			{
				if (heightMode == VisualElement.MeasureMode.AtMost)
				{
					num2 = Mathf.Min(num2, desiredHeight);
				}
			}
			else
			{
				num2 = desiredHeight;
			}
			return new Vector2(num, num2);
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x00054C28 File Offset: 0x00052E28
		private Rect GetCurrentClipRect()
		{
			Rect rect = this.lastWorldClip;
			bool flag = rect.width == 0f || rect.height == 0f;
			if (flag)
			{
				rect = base.worldBound;
			}
			return rect;
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x00054C70 File Offset: 0x00052E70
		private static void GetCurrentTransformAndClip(IMGUIContainer container, Event evt, out Matrix4x4 transform, out Rect clipRect)
		{
			clipRect = container.GetCurrentClipRect();
			transform = container.worldTransform;
			bool flag = evt != null && evt.rawType == EventType.Repaint && container.elementPanel != null;
			if (flag)
			{
				transform = container.elementPanel.repaintData.currentOffset * container.worldTransform;
			}
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x00054CD5 File Offset: 0x00052ED5
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x00054CE8 File Offset: 0x00052EE8
		protected virtual void Dispose(bool disposeManaged)
		{
			if (disposeManaged)
			{
				ObjectGUIState objectGUIState = this.m_ObjectGUIState;
				if (objectGUIState != null)
				{
					objectGUIState.Dispose();
				}
			}
		}

		// Token: 0x04000B41 RID: 2881
		internal static readonly BindingId cullingEnabledProperty = "cullingEnabled";

		// Token: 0x04000B42 RID: 2882
		internal static readonly BindingId contextTypeProperty = "contextType";

		// Token: 0x04000B43 RID: 2883
		private Action m_OnGUIHandler;

		// Token: 0x04000B44 RID: 2884
		private ObjectGUIState m_ObjectGUIState;

		// Token: 0x04000B45 RID: 2885
		internal bool useOwnerObjectGUIState;

		// Token: 0x04000B47 RID: 2887
		private bool m_CullingEnabled = false;

		// Token: 0x04000B48 RID: 2888
		private bool m_IsFocusDelegated = false;

		// Token: 0x04000B49 RID: 2889
		private bool m_RefreshCachedLayout = true;

		// Token: 0x04000B4A RID: 2890
		private GUILayoutUtility.LayoutCache m_Cache = null;

		// Token: 0x04000B4B RID: 2891
		private Rect m_CachedClippingRect = Rect.zero;

		// Token: 0x04000B4C RID: 2892
		private Matrix4x4 m_CachedTransform = Matrix4x4.identity;

		// Token: 0x04000B4D RID: 2893
		private ContextType m_ContextType;

		// Token: 0x04000B4E RID: 2894
		private bool lostFocus = false;

		// Token: 0x04000B4F RID: 2895
		private bool receivedFocus = false;

		// Token: 0x04000B50 RID: 2896
		private FocusChangeDirection focusChangeDirection = FocusChangeDirection.unspecified;

		// Token: 0x04000B51 RID: 2897
		private bool hasFocusableControls = false;

		// Token: 0x04000B52 RID: 2898
		private int newKeyboardFocusControlID = 0;

		// Token: 0x04000B54 RID: 2900
		public static readonly string ussClassName = "unity-imgui-container";

		// Token: 0x04000B55 RID: 2901
		internal static readonly string ussFoldoutChildDepthClassName = Foldout.ussClassName + "__" + IMGUIContainer.ussClassName + "--depth-";

		// Token: 0x04000B56 RID: 2902
		internal static readonly List<string> ussFoldoutChildDepthClassNames = new List<string>(Foldout.ussFoldoutMaxDepth + 1);

		// Token: 0x04000B57 RID: 2903
		private IMGUIContainer.GUIGlobals m_GUIGlobals;

		// Token: 0x04000B58 RID: 2904
		private static readonly ProfilerMarker k_OnGUIMarker = new ProfilerMarker("OnGUI");

		// Token: 0x04000B59 RID: 2905
		private static readonly ProfilerMarker k_ImmediateCallbackMarker = new ProfilerMarker("IMGUIContainer");

		// Token: 0x04000B5A RID: 2906
		private static Event s_DefaultMeasureEvent = new Event
		{
			type = EventType.Layout
		};

		// Token: 0x04000B5B RID: 2907
		private static Event s_MeasureEvent = new Event
		{
			type = EventType.Layout
		};

		// Token: 0x04000B5C RID: 2908
		private static Event s_CurrentEvent = new Event
		{
			type = EventType.Layout
		};

		// Token: 0x020002D9 RID: 729
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : VisualElement.UxmlSerializedData
		{
			// Token: 0x06001504 RID: 5380 RVA: 0x00054D19 File Offset: 0x00052F19
			public override object CreateInstance()
			{
				return new IMGUIContainer();
			}
		}

		// Token: 0x020002DA RID: 730
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<IMGUIContainer, IMGUIContainer.UxmlTraits>
		{
		}

		// Token: 0x020002DB RID: 731
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			// Token: 0x06001507 RID: 5383 RVA: 0x00054D29 File Offset: 0x00052F29
			public UxmlTraits()
			{
				base.focusIndex.defaultValue = 0;
				base.focusable.defaultValue = true;
			}

			// Token: 0x170003F1 RID: 1009
			// (get) Token: 0x06001508 RID: 5384 RVA: 0x00054D50 File Offset: 0x00052F50
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get
				{
					yield break;
				}
			}
		}

		// Token: 0x020002DD RID: 733
		private struct GUIGlobals
		{
			// Token: 0x04000B61 RID: 2913
			public Matrix4x4 matrix;

			// Token: 0x04000B62 RID: 2914
			public Color color;

			// Token: 0x04000B63 RID: 2915
			public Color contentColor;

			// Token: 0x04000B64 RID: 2916
			public Color backgroundColor;

			// Token: 0x04000B65 RID: 2917
			public bool enabled;

			// Token: 0x04000B66 RID: 2918
			public bool changed;

			// Token: 0x04000B67 RID: 2919
			public int displayIndex;

			// Token: 0x04000B68 RID: 2920
			public float pixelsPerPoint;
		}
	}
}
