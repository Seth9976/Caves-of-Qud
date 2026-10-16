using System;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Properties;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Serialization;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x020004F8 RID: 1272
	public class TextElement : BindableElement, ITextElement, INotifyValueChanged<string>, ITextEdition, ITextElementExperimentalFeatures, IExperimentalFeatures, ITextSelection
	{
		// Token: 0x060026CF RID: 9935 RVA: 0x0008F018 File Offset: 0x0008D218
		public TextElement()
		{
			base.requireMeasureFunction = true;
			base.tabIndex = -1;
			this.uitkTextHandle = new UITKTextHandle(this);
			base.AddToClassList(TextElement.ussClassName);
			base.generateVisualContent = (Action<MeshGenerationContext>)Delegate.Combine(base.generateVisualContent, new Action<MeshGenerationContext>(TextElement.OnGenerateVisualContent));
			this.edition.GetDefaultValueType = new Func<string>(this.GetDefaultValueType);
		}

		// Token: 0x060026D0 RID: 9936 RVA: 0x0008F140 File Offset: 0x0008D340
		private string GetDefaultValueType()
		{
			return string.Empty;
		}

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x060026D1 RID: 9937 RVA: 0x0008F157 File Offset: 0x0008D357
		// (set) Token: 0x060026D2 RID: 9938 RVA: 0x0008F15F File Offset: 0x0008D35F
		internal UITKTextHandle uitkTextHandle { get; set; }

		// Token: 0x060026D3 RID: 9939 RVA: 0x0008F168 File Offset: 0x0008D368
		[EventInterest(new Type[]
		{
			typeof(ContextualMenuPopulateEvent),
			typeof(KeyDownEvent),
			typeof(KeyUpEvent),
			typeof(ValidateCommandEvent),
			typeof(ExecuteCommandEvent),
			typeof(FocusEvent),
			typeof(BlurEvent),
			typeof(FocusInEvent),
			typeof(FocusOutEvent),
			typeof(PointerDownEvent),
			typeof(PointerUpEvent),
			typeof(PointerMoveEvent),
			typeof(NavigationMoveEvent),
			typeof(NavigationSubmitEvent),
			typeof(NavigationCancelEvent),
			typeof(IMEEvent),
			typeof(GeometryChangedEvent),
			typeof(AttachToPanelEvent),
			typeof(DetachFromPanelEvent)
		})]
		protected override void HandleEventBubbleUp(EventBase evt)
		{
			base.HandleEventBubbleUp(evt);
			bool flag = evt.target == this;
			if (flag)
			{
				GeometryChangedEvent geometryChangedEvent = evt as GeometryChangedEvent;
				if (geometryChangedEvent != null)
				{
					this.OnGeometryChanged(geometryChangedEvent);
					return;
				}
				DetachFromPanelEvent detachFromPanelEvent = evt as DetachFromPanelEvent;
				if (detachFromPanelEvent != null)
				{
					this.OnDetachFromPanel(detachFromPanelEvent);
					return;
				}
			}
			this.EditionHandleEvent(evt);
		}

		// Token: 0x060026D4 RID: 9940 RVA: 0x0008F1C9 File Offset: 0x0008D3C9
		private void OnGeometryChanged(GeometryChangedEvent e)
		{
			this.UpdateVisibleText();
		}

		// Token: 0x060026D5 RID: 9941 RVA: 0x0008F1D3 File Offset: 0x0008D3D3
		private void OnDetachFromPanel(DetachFromPanelEvent detachEvent)
		{
			this.uitkTextHandle.RemoveTextInfoFromPermanentCache();
			this.uitkTextHandle.RemoveTextInfoFromTemporaryCache();
		}

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x060026D6 RID: 9942 RVA: 0x0008F1EE File Offset: 0x0008D3EE
		// (set) Token: 0x060026D7 RID: 9943 RVA: 0x0008F1F6 File Offset: 0x0008D3F6
		[CreateProperty]
		public virtual string text
		{
			get
			{
				return ((INotifyValueChanged<string>)this).value;
			}
			set
			{
				((INotifyValueChanged<string>)this).value = value;
			}
		}

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x060026D8 RID: 9944 RVA: 0x0008F200 File Offset: 0x0008D400
		// (set) Token: 0x060026D9 RID: 9945 RVA: 0x0008F208 File Offset: 0x0008D408
		[CreateProperty]
		public bool enableRichText
		{
			get
			{
				return this.m_EnableRichText;
			}
			set
			{
				bool flag = this.m_EnableRichText == value;
				if (!flag)
				{
					this.m_EnableRichText = value;
					base.MarkDirtyRepaint();
					base.NotifyPropertyChanged(in TextElement.enableRichTextProperty);
				}
			}
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x060026DA RID: 9946 RVA: 0x0008F23F File Offset: 0x0008D43F
		// (set) Token: 0x060026DB RID: 9947 RVA: 0x0008F248 File Offset: 0x0008D448
		[CreateProperty]
		public bool emojiFallbackSupport
		{
			get
			{
				return this.m_EmojiFallbackSupport;
			}
			set
			{
				bool flag = this.m_EmojiFallbackSupport == value;
				if (!flag)
				{
					this.m_EmojiFallbackSupport = value;
					base.MarkDirtyRepaint();
					base.NotifyPropertyChanged(in TextElement.emojiFallbackSupportProperty);
				}
			}
		}

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x060026DC RID: 9948 RVA: 0x0008F27F File Offset: 0x0008D47F
		// (set) Token: 0x060026DD RID: 9949 RVA: 0x0008F288 File Offset: 0x0008D488
		[CreateProperty]
		public bool parseEscapeSequences
		{
			get
			{
				return this.m_ParseEscapeSequences;
			}
			set
			{
				bool flag = this.m_ParseEscapeSequences == value;
				if (!flag)
				{
					this.m_ParseEscapeSequences = value;
					base.MarkDirtyRepaint();
					base.NotifyPropertyChanged(in TextElement.parseEscapeSequencesProperty);
				}
			}
		}

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x060026DE RID: 9950 RVA: 0x0008F2BF File Offset: 0x0008D4BF
		// (set) Token: 0x060026DF RID: 9951 RVA: 0x0008F2C8 File Offset: 0x0008D4C8
		[CreateProperty]
		public bool displayTooltipWhenElided
		{
			get
			{
				return this.m_DisplayTooltipWhenElided;
			}
			set
			{
				bool flag = this.m_DisplayTooltipWhenElided != value;
				if (flag)
				{
					this.m_DisplayTooltipWhenElided = value;
					this.UpdateVisibleText();
					base.MarkDirtyRepaint();
					base.NotifyPropertyChanged(in TextElement.displayTooltipWhenElidedProperty);
				}
			}
		}

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x060026E0 RID: 9952 RVA: 0x0008F309 File Offset: 0x0008D509
		// (set) Token: 0x060026E1 RID: 9953 RVA: 0x0008F311 File Offset: 0x0008D511
		[CreateProperty(ReadOnly = true)]
		public bool isElided { get; private set; }

		// Token: 0x060026E2 RID: 9954 RVA: 0x0008F31C File Offset: 0x0008D51C
		internal static void OnGenerateVisualContent(MeshGenerationContext mgc)
		{
			TextElement textElement = mgc.visualElement as TextElement;
			bool flag = textElement != null;
			if (flag)
			{
				textElement.UpdateVisibleText();
				bool flag2 = TextUtilities.IsFontAssigned(textElement);
				if (flag2)
				{
					textElement.uitkTextHandle.ReleaseResourcesIfPossible();
					mgc.meshGenerator.textJobSystem.GenerateText(mgc, textElement);
				}
			}
		}

		// Token: 0x060026E3 RID: 9955 RVA: 0x0008F374 File Offset: 0x0008D574
		internal void OnGenerateTextOver(MeshGenerationContext mgc)
		{
			bool flag = this.selection.HasSelection() && this.selectingManipulator.HasFocus();
			if (flag)
			{
				this.DrawHighlighting(mgc);
			}
			else
			{
				bool flag2 = !this.edition.isReadOnly && this.selection.isSelectable && this.selectingManipulator.RevealCursor();
				if (flag2)
				{
					this.DrawCaret(mgc);
				}
			}
			bool flag3 = this.ShouldElide() && this.uitkTextHandle.TextLibraryCanElide();
			if (flag3)
			{
				this.isElided = this.uitkTextHandle.IsElided();
			}
			this.UpdateTooltip();
		}

		// Token: 0x060026E4 RID: 9956 RVA: 0x0008F414 File Offset: 0x0008D614
		internal void OnGenerateTextOverNative(MeshGenerationContext mgc)
		{
			bool flag = this.selection.HasSelection() && this.selectingManipulator.HasFocus();
			if (flag)
			{
				this.DrawNativeHighlighting(mgc);
			}
			else
			{
				bool flag2 = !this.edition.isReadOnly && this.selection.isSelectable && this.selectingManipulator.RevealCursor();
				if (flag2)
				{
					this.DrawCaret(mgc);
				}
			}
			bool flag3 = this.ShouldElide() && this.uitkTextHandle.TextLibraryCanElide();
			if (flag3)
			{
				this.isElided = this.uitkTextHandle.IsElided();
			}
		}

		// Token: 0x060026E5 RID: 9957 RVA: 0x0008F4AC File Offset: 0x0008D6AC
		internal string ElideText(string drawText, string ellipsisText, float width, TextOverflowPosition textOverflowPosition)
		{
			float num = base.resolvedStyle.paddingRight;
			bool flag = float.IsNaN(num);
			if (flag)
			{
				num = 0f;
			}
			float num2 = Mathf.Clamp(num, 1f / base.scaledPixelsPerPoint, 1f);
			Vector2 vector = this.MeasureTextSize(drawText, 0f, VisualElement.MeasureMode.Undefined, 0f, VisualElement.MeasureMode.Undefined);
			bool flag2 = vector.x <= width + num2 || string.IsNullOrEmpty(ellipsisText);
			string text;
			if (flag2)
			{
				text = drawText;
			}
			else
			{
				string text2 = ((drawText.Length > 1) ? ellipsisText : drawText);
				Vector2 vector2 = this.MeasureTextSize(text2, 0f, VisualElement.MeasureMode.Undefined, 0f, VisualElement.MeasureMode.Undefined);
				bool flag3 = vector2.x >= width;
				if (flag3)
				{
					text = text2;
				}
				else
				{
					int num3 = drawText.Length - 1;
					int num4 = -1;
					string text3 = drawText;
					int i = ((textOverflowPosition == TextOverflowPosition.Start) ? 1 : 0);
					int num5 = ((textOverflowPosition == TextOverflowPosition.Start || textOverflowPosition == TextOverflowPosition.Middle) ? num3 : (num3 - 1));
					int num6 = (i + num5) / 2;
					while (i <= num5)
					{
						bool flag4 = textOverflowPosition == TextOverflowPosition.Start;
						if (flag4)
						{
							text3 = ellipsisText + drawText.Substring(num6, num3 - (num6 - 1));
						}
						else
						{
							bool flag5 = textOverflowPosition == TextOverflowPosition.End;
							if (flag5)
							{
								text3 = drawText.Substring(0, num6) + ellipsisText;
							}
							else
							{
								bool flag6 = textOverflowPosition == TextOverflowPosition.Middle;
								if (flag6)
								{
									text3 = ((num6 - 1 <= 0) ? "" : drawText.Substring(0, num6 - 1)) + ellipsisText + ((num3 - (num6 - 1) <= 0) ? "" : drawText.Substring(num3 - (num6 - 1)));
								}
							}
						}
						vector = this.MeasureTextSize(text3, 0f, VisualElement.MeasureMode.Undefined, 0f, VisualElement.MeasureMode.Undefined);
						bool flag7 = Math.Abs(vector.x - width) < 1E-30f;
						if (flag7)
						{
							return text3;
						}
						bool flag8 = textOverflowPosition == TextOverflowPosition.Start;
						if (flag8)
						{
							bool flag9 = vector.x > width;
							if (flag9)
							{
								bool flag10 = num4 == num6 - 1;
								if (flag10)
								{
									return ellipsisText + drawText.Substring(num4, num3 - (num4 - 1));
								}
								i = num6 + 1;
							}
							else
							{
								num5 = num6 - 1;
								num4 = num6;
							}
						}
						else
						{
							bool flag11 = textOverflowPosition == TextOverflowPosition.End || textOverflowPosition == TextOverflowPosition.Middle;
							if (flag11)
							{
								bool flag12 = vector.x > width;
								if (flag12)
								{
									bool flag13 = num4 == num6 - 1;
									if (flag13)
									{
										bool flag14 = textOverflowPosition == TextOverflowPosition.End;
										if (flag14)
										{
											return drawText.Substring(0, num4) + ellipsisText;
										}
										return drawText.Substring(0, Mathf.Max(num4 - 1, 0)) + ellipsisText + drawText.Substring(num3 - Mathf.Max(num4 - 1, 0));
									}
									else
									{
										num5 = num6 - 1;
									}
								}
								else
								{
									i = num6 + 1;
									num4 = num6;
								}
							}
						}
						num6 = (i + num5) / 2;
					}
					text = text3;
				}
			}
			return text;
		}

		// Token: 0x060026E6 RID: 9958 RVA: 0x0008F784 File Offset: 0x0008D984
		private void UpdateTooltip()
		{
			bool flag = this.displayTooltipWhenElided && this.isElided;
			bool flag2 = flag;
			if (flag2)
			{
				base.tooltip = this.text;
				this.m_WasElided = true;
			}
			else
			{
				bool wasElided = this.m_WasElided;
				if (wasElided)
				{
					base.tooltip = null;
					this.m_WasElided = false;
				}
			}
		}

		// Token: 0x060026E7 RID: 9959 RVA: 0x0008F7DC File Offset: 0x0008D9DC
		private void UpdateVisibleText()
		{
			bool flag = this.ShouldElide();
			bool flag2 = flag && this.uitkTextHandle.TextLibraryCanElide();
			if (!flag2)
			{
				bool flag3 = flag;
				if (flag3)
				{
					this.elidedText = this.ElideText(this.text, TextElement.k_EllipsisText, base.contentRect.width, base.computedStyle.unityTextOverflowPosition);
					this.isElided = flag && !string.Equals(this.elidedText, this.text, StringComparison.Ordinal);
				}
				else
				{
					this.isElided = false;
				}
			}
		}

		// Token: 0x060026E8 RID: 9960 RVA: 0x0008F870 File Offset: 0x0008DA70
		private bool ShouldElide()
		{
			return base.computedStyle.textOverflow == TextOverflow.Ellipsis && base.computedStyle.overflow == OverflowInternal.Hidden;
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x060026E9 RID: 9961 RVA: 0x0008F8A1 File Offset: 0x0008DAA1
		internal bool hasFocus
		{
			get
			{
				bool flag;
				if (base.elementPanel != null)
				{
					FocusController focusController = base.elementPanel.focusController;
					flag = ((focusController != null) ? focusController.GetLeafFocusedElement() : null) == this;
				}
				else
				{
					flag = false;
				}
				return flag;
			}
		}

		// Token: 0x060026EA RID: 9962 RVA: 0x0008F8C8 File Offset: 0x0008DAC8
		public Vector2 MeasureTextSize(string textToMeasure, float width, VisualElement.MeasureMode widthMode, float height, VisualElement.MeasureMode heightMode)
		{
			RenderedText renderedText = new RenderedText(textToMeasure);
			return TextUtilities.MeasureVisualElementTextSize(this, in renderedText, width, widthMode, height, heightMode);
		}

		// Token: 0x060026EB RID: 9963 RVA: 0x0008F8F0 File Offset: 0x0008DAF0
		protected internal override Vector2 DoMeasure(float desiredWidth, VisualElement.MeasureMode widthMode, float desiredHeight, VisualElement.MeasureMode heightMode)
		{
			RenderedText renderedText = this.renderedText;
			return TextUtilities.MeasureVisualElementTextSize(this, in renderedText, desiredWidth, widthMode, desiredHeight, heightMode);
		}

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x060026EC RID: 9964 RVA: 0x0008F916 File Offset: 0x0008DB16
		// (set) Token: 0x060026ED RID: 9965 RVA: 0x0008F928 File Offset: 0x0008DB28
		string INotifyValueChanged<string>.value
		{
			get
			{
				return this.m_Text ?? string.Empty;
			}
			set
			{
				bool flag = this.m_Text != value;
				if (flag)
				{
					bool flag2 = base.panel != null;
					if (flag2)
					{
						using (ChangeEvent<string> pooled = ChangeEvent<string>.GetPooled(this.text, value))
						{
							pooled.elementTarget = this;
							((INotifyValueChanged<string>)this).SetValueWithoutNotify(value);
							this.SendEvent(pooled);
							base.NotifyPropertyChanged(in TextElement.valueProperty);
							base.NotifyPropertyChanged(in TextElement.textProperty);
						}
					}
					else
					{
						((INotifyValueChanged<string>)this).SetValueWithoutNotify(value);
					}
				}
			}
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x060026EE RID: 9966 RVA: 0x0008F1EE File Offset: 0x0008D3EE
		// (set) Token: 0x060026EF RID: 9967 RVA: 0x0008F1F6 File Offset: 0x0008D3F6
		[CreateProperty]
		private string value
		{
			get
			{
				return ((INotifyValueChanged<string>)this).value;
			}
			set
			{
				((INotifyValueChanged<string>)this).value = value;
			}
		}

		// Token: 0x060026F0 RID: 9968 RVA: 0x0008F9C0 File Offset: 0x0008DBC0
		void INotifyValueChanged<string>.SetValueWithoutNotify(string newValue)
		{
			newValue = ((ITextEdition)this).CullString(newValue);
			bool flag = this.m_Text != newValue;
			if (flag)
			{
				this.SetRenderedText(newValue);
				this.m_Text = newValue;
				bool flag2 = base.computedStyle.height.IsAuto() || base.computedStyle.height.IsNone() || base.computedStyle.width.IsAuto() || base.computedStyle.width.IsNone();
				if (flag2)
				{
					base.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Repaint);
				}
				else
				{
					base.IncrementVersion(VersionChangeType.Repaint);
				}
				bool flag3 = !string.IsNullOrEmpty(base.viewDataKey);
				if (flag3)
				{
					base.SaveViewData();
				}
			}
			bool flag4 = this.editingManipulator != null;
			if (flag4)
			{
				this.editingManipulator.editingUtilities.text = newValue;
			}
		}

		// Token: 0x060026F1 RID: 9969 RVA: 0x0008FAAB File Offset: 0x0008DCAB
		public void MarkDirtyText()
		{
			base.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Repaint);
			this.uitkTextHandle.SetDirty();
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x060026F2 RID: 9970 RVA: 0x000435E1 File Offset: 0x000417E1
		internal ITextEdition edition
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x060026F3 RID: 9971 RVA: 0x0008FAC6 File Offset: 0x0008DCC6
		// (set) Token: 0x060026F4 RID: 9972 RVA: 0x0008FACE File Offset: 0x0008DCCE
		internal TextEditingManipulator editingManipulator { get; private set; }

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x060026F5 RID: 9973 RVA: 0x0008FAD7 File Offset: 0x0008DCD7
		// (set) Token: 0x060026F6 RID: 9974 RVA: 0x0008FAE0 File Offset: 0x0008DCE0
		bool ITextEdition.multiline
		{
			get
			{
				return this.m_Multiline;
			}
			set
			{
				bool flag = value != this.m_Multiline;
				if (flag)
				{
					bool flag2 = !this.edition.isReadOnly;
					if (flag2)
					{
						this.editingManipulator.editingUtilities.multiline = value;
					}
					this.m_Multiline = value;
				}
			}
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x060026F7 RID: 9975 RVA: 0x0008FB2A File Offset: 0x0008DD2A
		TouchScreenKeyboard ITextEdition.touchScreenKeyboard
		{
			get
			{
				return this.m_TouchScreenKeyboard;
			}
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x060026F8 RID: 9976 RVA: 0x0008FB32 File Offset: 0x0008DD32
		// (set) Token: 0x060026F9 RID: 9977 RVA: 0x0008FB3C File Offset: 0x0008DD3C
		TouchScreenKeyboardType ITextEdition.keyboardType
		{
			get
			{
				return this.m_KeyboardType;
			}
			set
			{
				bool flag = this.m_KeyboardType == value;
				if (!flag)
				{
					this.m_KeyboardType = value;
					base.NotifyPropertyChanged(in TextElement.keyboardTypeProperty);
				}
			}
		}

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x060026FA RID: 9978 RVA: 0x0008FB6C File Offset: 0x0008DD6C
		// (set) Token: 0x060026FB RID: 9979 RVA: 0x0008FB79 File Offset: 0x0008DD79
		[CreateProperty]
		private TouchScreenKeyboardType keyboardType
		{
			get
			{
				return this.edition.keyboardType;
			}
			set
			{
				this.edition.keyboardType = value;
			}
		}

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x060026FC RID: 9980 RVA: 0x0008FB88 File Offset: 0x0008DD88
		// (set) Token: 0x060026FD RID: 9981 RVA: 0x0008FB90 File Offset: 0x0008DD90
		bool ITextEdition.hideSoftKeyboard
		{
			get
			{
				return this.m_HideSoftKeyboard;
			}
			set
			{
				bool flag = this.m_HideSoftKeyboard == value;
				if (!flag)
				{
					this.m_HideSoftKeyboard = value;
					base.NotifyPropertyChanged(in TextElement.hideSoftKeyboardProperty);
				}
			}
		}

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x060026FE RID: 9982 RVA: 0x0008FBC0 File Offset: 0x0008DDC0
		// (set) Token: 0x060026FF RID: 9983 RVA: 0x0008FBCD File Offset: 0x0008DDCD
		[CreateProperty]
		private bool hideSoftKeyboard
		{
			get
			{
				return this.edition.hideSoftKeyboard;
			}
			set
			{
				this.edition.hideSoftKeyboard = value;
			}
		}

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06002700 RID: 9984 RVA: 0x0008FBDC File Offset: 0x0008DDDC
		// (set) Token: 0x06002701 RID: 9985 RVA: 0x0008FC1C File Offset: 0x0008DE1C
		bool ITextEdition.hideMobileInput
		{
			get
			{
				TouchScreenKeyboard.InputFieldAppearance inputFieldAppearance = TouchScreenKeyboard.inputFieldAppearance;
				if (!true)
				{
				}
				bool flag = inputFieldAppearance != TouchScreenKeyboard.InputFieldAppearance.AlwaysVisible && (inputFieldAppearance == TouchScreenKeyboard.InputFieldAppearance.AlwaysHidden || this.m_HideMobileInput);
				if (!true)
				{
				}
				return flag;
			}
			set
			{
				bool flag = TouchScreenKeyboard.inputFieldAppearance > TouchScreenKeyboard.InputFieldAppearance.Customizable;
				if (!flag)
				{
					bool flag2 = this.m_HideMobileInput == value;
					if (!flag2)
					{
						this.m_HideMobileInput = value;
						base.NotifyPropertyChanged(in TextElement.hideMobileInputProperty);
					}
				}
			}
		}

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x06002702 RID: 9986 RVA: 0x0008FC5A File Offset: 0x0008DE5A
		// (set) Token: 0x06002703 RID: 9987 RVA: 0x0008FC67 File Offset: 0x0008DE67
		[CreateProperty]
		private bool hideMobileInput
		{
			get
			{
				return this.edition.hideMobileInput;
			}
			set
			{
				this.edition.hideMobileInput = value;
			}
		}

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06002704 RID: 9988 RVA: 0x0008FC76 File Offset: 0x0008DE76
		// (set) Token: 0x06002705 RID: 9989 RVA: 0x0008FC8C File Offset: 0x0008DE8C
		bool ITextEdition.isReadOnly
		{
			get
			{
				return this.m_IsReadOnly || !base.enabledInHierarchy;
			}
			set
			{
				bool flag = value == this.m_IsReadOnly;
				if (!flag)
				{
					TextEditingManipulator editingManipulator = this.editingManipulator;
					if (editingManipulator != null)
					{
						editingManipulator.Reset();
					}
					this.editingManipulator = (value ? null : new TextEditingManipulator(this));
					this.m_IsReadOnly = value;
					Action<bool> action = this.onIsReadOnlyChanged;
					if (action != null)
					{
						action(value);
					}
					base.NotifyPropertyChanged(in TextElement.isReadOnlyProperty);
				}
			}
		}

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x06002706 RID: 9990 RVA: 0x0008FCF4 File Offset: 0x0008DEF4
		// (set) Token: 0x06002707 RID: 9991 RVA: 0x0008FD01 File Offset: 0x0008DF01
		[CreateProperty]
		private bool isReadOnly
		{
			get
			{
				return this.edition.isReadOnly;
			}
			set
			{
				this.edition.isReadOnly = value;
			}
		}

		// Token: 0x06002708 RID: 9992 RVA: 0x0008FD10 File Offset: 0x0008DF10
		private void ProcessMenuCommand(string command)
		{
			this.Focus();
			using (ExecuteCommandEvent pooled = CommandEventBase<ExecuteCommandEvent>.GetPooled(command))
			{
				pooled.elementTarget = this;
				this.SendEvent(pooled);
			}
		}

		// Token: 0x06002709 RID: 9993 RVA: 0x0008FD5C File Offset: 0x0008DF5C
		private void Cut(DropdownMenuAction a)
		{
			this.ProcessMenuCommand("Cut");
		}

		// Token: 0x0600270A RID: 9994 RVA: 0x0008FD6B File Offset: 0x0008DF6B
		private void Copy(DropdownMenuAction a)
		{
			this.ProcessMenuCommand("Copy");
		}

		// Token: 0x0600270B RID: 9995 RVA: 0x0008FD7A File Offset: 0x0008DF7A
		private void Paste(DropdownMenuAction a)
		{
			this.ProcessMenuCommand("Paste");
		}

		// Token: 0x0600270C RID: 9996 RVA: 0x0008FD8C File Offset: 0x0008DF8C
		private void BuildContextualMenu(ContextualMenuPopulateEvent evt)
		{
			bool flag = ((evt != null) ? evt.target : null) is TextElement;
			if (flag)
			{
				bool flag2 = !this.edition.isReadOnly;
				if (flag2)
				{
					evt.menu.AppendAction("Cut", new Action<DropdownMenuAction>(this.Cut), new Func<DropdownMenuAction, DropdownMenuAction.Status>(this.CutActionStatus), null);
					evt.menu.AppendAction("Copy", new Action<DropdownMenuAction>(this.Copy), new Func<DropdownMenuAction, DropdownMenuAction.Status>(this.CopyActionStatus), null);
					evt.menu.AppendAction("Paste", new Action<DropdownMenuAction>(this.Paste), new Func<DropdownMenuAction, DropdownMenuAction.Status>(this.PasteActionStatus), null);
				}
				else
				{
					evt.menu.AppendAction("Copy", new Action<DropdownMenuAction>(this.Copy), new Func<DropdownMenuAction, DropdownMenuAction.Status>(this.CopyActionStatus), null);
				}
			}
		}

		// Token: 0x0600270D RID: 9997 RVA: 0x0008FE78 File Offset: 0x0008E078
		private DropdownMenuAction.Status CutActionStatus(DropdownMenuAction a)
		{
			return (base.enabledInHierarchy && this.selection.HasSelection() && !this.edition.isPassword) ? DropdownMenuAction.Status.Normal : DropdownMenuAction.Status.Disabled;
		}

		// Token: 0x0600270E RID: 9998 RVA: 0x0008FEB0 File Offset: 0x0008E0B0
		private DropdownMenuAction.Status CopyActionStatus(DropdownMenuAction a)
		{
			return ((!base.enabledInHierarchy || this.selection.HasSelection()) && !this.edition.isPassword) ? DropdownMenuAction.Status.Normal : DropdownMenuAction.Status.Disabled;
		}

		// Token: 0x0600270F RID: 9999 RVA: 0x0008FEE8 File Offset: 0x0008E0E8
		private DropdownMenuAction.Status PasteActionStatus(DropdownMenuAction a)
		{
			bool flag = this.editingManipulator.editingUtilities.CanPaste();
			return base.enabledInHierarchy ? (flag ? DropdownMenuAction.Status.Normal : DropdownMenuAction.Status.Disabled) : DropdownMenuAction.Status.Hidden;
		}

		// Token: 0x06002710 RID: 10000 RVA: 0x0008FF20 File Offset: 0x0008E120
		private void EditionHandleEvent(EventBase evt)
		{
			bool isSelectable = this.selection.isSelectable;
			if (isSelectable)
			{
				TextEditingManipulator editingManipulator = this.editingManipulator;
				bool flag = editingManipulator != null && editingManipulator.editingUtilities.TouchScreenKeyboardCanBeUsed();
				bool flag2 = !flag || this.edition.hideMobileInput || this.edition.hideSoftKeyboard;
				if (flag2)
				{
					TextSelectingManipulator selectingManipulator = this.selectingManipulator;
					if (selectingManipulator != null)
					{
						selectingManipulator.HandleEventBubbleUp(evt);
					}
				}
				bool flag3 = !this.edition.isReadOnly;
				if (flag3)
				{
					TextEditingManipulator editingManipulator2 = this.editingManipulator;
					if (editingManipulator2 != null)
					{
						editingManipulator2.HandleEventBubbleUp(evt);
					}
				}
				BaseVisualElementPanel elementPanel = base.elementPanel;
				if (elementPanel != null)
				{
					ContextualMenuManager contextualMenuManager = elementPanel.contextualMenuManager;
					if (contextualMenuManager != null)
					{
						contextualMenuManager.DisplayMenuIfEventMatches(evt, this);
					}
				}
				long? num = ((evt != null) ? new long?(evt.eventTypeId) : null);
				long num2 = EventBase<ContextualMenuPopulateEvent>.TypeId();
				bool flag4 = (num.GetValueOrDefault() == num2) & (num != null);
				if (flag4)
				{
					ContextualMenuPopulateEvent contextualMenuPopulateEvent = evt as ContextualMenuPopulateEvent;
					int count = contextualMenuPopulateEvent.menu.MenuItems().Count;
					this.BuildContextualMenu(contextualMenuPopulateEvent);
					bool flag5 = count > 0 && contextualMenuPopulateEvent.menu.MenuItems().Count > count;
					if (flag5)
					{
						contextualMenuPopulateEvent.menu.InsertSeparator(null, count);
					}
				}
			}
		}

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x06002711 RID: 10001 RVA: 0x0009006A File Offset: 0x0008E26A
		// (set) Token: 0x06002712 RID: 10002 RVA: 0x00090074 File Offset: 0x0008E274
		int ITextEdition.maxLength
		{
			get
			{
				return this.m_MaxLength;
			}
			set
			{
				bool flag = this.m_MaxLength == value;
				if (!flag)
				{
					this.m_MaxLength = value;
					this.text = this.edition.CullString(this.text);
					base.NotifyPropertyChanged(in TextElement.maxLengthProperty);
				}
			}
		}

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x06002713 RID: 10003 RVA: 0x000900BC File Offset: 0x0008E2BC
		// (set) Token: 0x06002714 RID: 10004 RVA: 0x000900C9 File Offset: 0x0008E2C9
		[CreateProperty]
		private int maxLength
		{
			get
			{
				return this.edition.maxLength;
			}
			set
			{
				this.edition.maxLength = value;
			}
		}

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x06002715 RID: 10005 RVA: 0x000900D8 File Offset: 0x0008E2D8
		// (set) Token: 0x06002716 RID: 10006 RVA: 0x000900E0 File Offset: 0x0008E2E0
		string ITextEdition.placeholder
		{
			get
			{
				return this.m_PlaceholderText;
			}
			set
			{
				bool flag = value == this.m_PlaceholderText;
				if (!flag)
				{
					bool flag2 = !string.IsNullOrEmpty(value) && (this.text == null || this.text.Equals(this.edition.GetDefaultValueType()));
					if (flag2)
					{
						this.text = "";
					}
					this.m_PlaceholderText = value;
					Action onPlaceholderChanged = this.OnPlaceholderChanged;
					if (onPlaceholderChanged != null)
					{
						onPlaceholderChanged();
					}
					base.MarkDirtyRepaint();
				}
			}
		}

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x06002717 RID: 10007 RVA: 0x00090161 File Offset: 0x0008E361
		// (set) Token: 0x06002718 RID: 10008 RVA: 0x00090169 File Offset: 0x0008E369
		bool ITextEdition.isDelayed { get; set; }

		// Token: 0x06002719 RID: 10009 RVA: 0x00090174 File Offset: 0x0008E374
		void ITextEdition.ResetValueAndText()
		{
			this.m_OriginalText = (this.text = null);
		}

		// Token: 0x0600271A RID: 10010 RVA: 0x00090193 File Offset: 0x0008E393
		void ITextEdition.SaveValueAndText()
		{
			this.m_OriginalText = this.text;
		}

		// Token: 0x0600271B RID: 10011 RVA: 0x000901A2 File Offset: 0x0008E3A2
		void ITextEdition.RestoreValueAndText()
		{
			this.text = this.m_OriginalText;
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x0600271C RID: 10012 RVA: 0x000901B2 File Offset: 0x0008E3B2
		// (set) Token: 0x0600271D RID: 10013 RVA: 0x000901BA File Offset: 0x0008E3BA
		Func<char, bool> ITextEdition.AcceptCharacter { get; set; }

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x0600271E RID: 10014 RVA: 0x000901C3 File Offset: 0x0008E3C3
		// (set) Token: 0x0600271F RID: 10015 RVA: 0x000901CB File Offset: 0x0008E3CB
		Action<bool> ITextEdition.UpdateScrollOffset { get; set; }

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06002720 RID: 10016 RVA: 0x000901D4 File Offset: 0x0008E3D4
		// (set) Token: 0x06002721 RID: 10017 RVA: 0x000901DC File Offset: 0x0008E3DC
		Action ITextEdition.UpdateValueFromText { get; set; }

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x06002722 RID: 10018 RVA: 0x000901E5 File Offset: 0x0008E3E5
		// (set) Token: 0x06002723 RID: 10019 RVA: 0x000901ED File Offset: 0x0008E3ED
		Action ITextEdition.UpdateTextFromValue { get; set; }

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x06002724 RID: 10020 RVA: 0x000901F6 File Offset: 0x0008E3F6
		// (set) Token: 0x06002725 RID: 10021 RVA: 0x000901FE File Offset: 0x0008E3FE
		Action ITextEdition.MoveFocusToCompositeRoot { get; set; }

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x06002726 RID: 10022 RVA: 0x00090207 File Offset: 0x0008E407
		// (set) Token: 0x06002727 RID: 10023 RVA: 0x0009020F File Offset: 0x0008E40F
		internal Action OnPlaceholderChanged { get; set; }

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x06002728 RID: 10024 RVA: 0x00090218 File Offset: 0x0008E418
		// (set) Token: 0x06002729 RID: 10025 RVA: 0x00090220 File Offset: 0x0008E420
		Func<string> ITextEdition.GetDefaultValueType { get; set; }

		// Token: 0x0600272A RID: 10026 RVA: 0x0009022C File Offset: 0x0008E42C
		void ITextEdition.UpdateText(string value)
		{
			bool flag = this.m_TouchScreenKeyboard != null && this.m_TouchScreenKeyboard.text != value;
			if (flag)
			{
				this.m_TouchScreenKeyboard.text = value;
			}
			bool flag2 = this.text != value;
			if (flag2)
			{
				using (InputEvent pooled = InputEvent.GetPooled(this.text, value))
				{
					pooled.elementTarget = base.parent;
					((INotifyValueChanged<string>)this).SetValueWithoutNotify(value);
					VisualElement parent = base.parent;
					if (parent != null)
					{
						parent.SendEvent(pooled);
					}
				}
			}
		}

		// Token: 0x0600272B RID: 10027 RVA: 0x000902D0 File Offset: 0x0008E4D0
		string ITextEdition.CullString(string s)
		{
			int maxLength = this.edition.maxLength;
			bool flag = maxLength >= 0 && s != null && s.Length > maxLength;
			string text;
			if (flag)
			{
				text = s.Substring(0, maxLength);
			}
			else
			{
				text = s;
			}
			return text;
		}

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x0600272C RID: 10028 RVA: 0x00090311 File Offset: 0x0008E511
		// (set) Token: 0x0600272D RID: 10029 RVA: 0x0009031C File Offset: 0x0008E51C
		char ITextEdition.maskChar
		{
			get
			{
				return this.m_MaskChar;
			}
			set
			{
				bool flag = this.m_MaskChar != value;
				if (flag)
				{
					this.m_MaskChar = value;
					bool isPassword = this.edition.isPassword;
					if (isPassword)
					{
						base.IncrementVersion(VersionChangeType.Repaint);
					}
					base.NotifyPropertyChanged(in TextElement.maskCharProperty);
				}
			}
		}

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x0600272E RID: 10030 RVA: 0x0009036C File Offset: 0x0008E56C
		// (set) Token: 0x0600272F RID: 10031 RVA: 0x00090379 File Offset: 0x0008E579
		[CreateProperty]
		private char maskChar
		{
			get
			{
				return this.edition.maskChar;
			}
			set
			{
				this.edition.maskChar = value;
			}
		}

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x06002730 RID: 10032 RVA: 0x00090388 File Offset: 0x0008E588
		private char effectiveMaskChar
		{
			get
			{
				return this.edition.isPassword ? this.m_MaskChar : '\0';
			}
		}

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x06002731 RID: 10033 RVA: 0x000903A0 File Offset: 0x0008E5A0
		// (set) Token: 0x06002732 RID: 10034 RVA: 0x000903A8 File Offset: 0x0008E5A8
		bool ITextEdition.isPassword
		{
			get
			{
				return this.m_IsPassword;
			}
			set
			{
				bool flag = this.m_IsPassword != value;
				if (flag)
				{
					this.m_IsPassword = value;
					base.IncrementVersion(VersionChangeType.Repaint);
					base.NotifyPropertyChanged(in TextElement.isPasswordProperty);
				}
			}
		}

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x06002733 RID: 10035 RVA: 0x000903E7 File Offset: 0x0008E5E7
		// (set) Token: 0x06002734 RID: 10036 RVA: 0x000903F4 File Offset: 0x0008E5F4
		[CreateProperty]
		private bool isPassword
		{
			get
			{
				return this.edition.isPassword;
			}
			set
			{
				this.edition.isPassword = value;
			}
		}

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x06002735 RID: 10037 RVA: 0x00090403 File Offset: 0x0008E603
		// (set) Token: 0x06002736 RID: 10038 RVA: 0x0009040B File Offset: 0x0008E60B
		bool ITextEdition.hidePlaceholderOnFocus
		{
			get
			{
				return this.m_HidePlaceholderTextOnFocus;
			}
			set
			{
				this.m_HidePlaceholderTextOnFocus = value;
			}
		}

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x06002737 RID: 10039 RVA: 0x00090414 File Offset: 0x0008E614
		internal bool needsPlaceholderIfTextIsEmpty
		{
			get
			{
				bool flag = this.m_PlaceholderText.Length > 0;
				bool flag2 = this.edition.hidePlaceholderOnFocus && this.hasFocus;
				return flag && !flag2;
			}
		}

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x06002738 RID: 10040 RVA: 0x00090458 File Offset: 0x0008E658
		internal bool showPlaceholderText
		{
			get
			{
				bool flag = !this.needsPlaceholderIfTextIsEmpty;
				bool flag2;
				if (flag)
				{
					flag2 = false;
				}
				else
				{
					bool flag3 = string.IsNullOrEmpty(this.text);
					flag2 = flag3;
				}
				return flag2;
			}
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06002739 RID: 10041 RVA: 0x00090488 File Offset: 0x0008E688
		// (set) Token: 0x0600273A RID: 10042 RVA: 0x00090490 File Offset: 0x0008E690
		bool ITextEdition.autoCorrection
		{
			get
			{
				return this.m_AutoCorrection;
			}
			set
			{
				bool flag = this.m_AutoCorrection == value;
				if (!flag)
				{
					this.m_AutoCorrection = value;
					base.NotifyPropertyChanged(in TextElement.autoCorrectionProperty);
				}
			}
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x0600273B RID: 10043 RVA: 0x000904C0 File Offset: 0x0008E6C0
		// (set) Token: 0x0600273C RID: 10044 RVA: 0x000904CD File Offset: 0x0008E6CD
		[CreateProperty]
		private bool autoCorrection
		{
			get
			{
				return this.edition.autoCorrection;
			}
			set
			{
				this.edition.autoCorrection = value;
			}
		}

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x0600273D RID: 10045 RVA: 0x000904DC File Offset: 0x0008E6DC
		internal RenderedText renderedText
		{
			get
			{
				bool showPlaceholderText = this.showPlaceholderText;
				RenderedText renderedText;
				if (showPlaceholderText)
				{
					renderedText = (TextUtilities.IsAdvancedTextEnabledForElement(this) ? new RenderedText(this.m_PlaceholderText) : new RenderedText(this.m_PlaceholderText, "\u200b"));
				}
				else
				{
					bool flag = this.effectiveMaskChar > '\0';
					if (flag)
					{
						RenderedText renderedText3;
						if (!TextUtilities.IsAdvancedTextEnabledForElement(this))
						{
							char effectiveMaskChar = this.effectiveMaskChar;
							string renderedText2 = this.m_RenderedText;
							renderedText3 = new RenderedText(effectiveMaskChar, (renderedText2 != null) ? renderedText2.Length : 0, "\u200b");
						}
						else
						{
							char effectiveMaskChar2 = this.effectiveMaskChar;
							string renderedText4 = this.m_RenderedText;
							renderedText3 = new RenderedText(effectiveMaskChar2, (renderedText4 != null) ? renderedText4.Length : 0, null);
						}
						renderedText = renderedText3;
					}
					else
					{
						bool flag2 = !TextUtilities.IsAdvancedTextEnabledForElement(this) && (!this.isReadOnly || ((base.pseudoStates & PseudoStates.Disabled) != (PseudoStates)0 && this.isSelectable));
						if (flag2)
						{
							renderedText = new RenderedText(this.m_RenderedText, "\u200b");
						}
						else
						{
							renderedText = new RenderedText(this.m_RenderedText);
						}
					}
				}
				return renderedText;
			}
		}

		// Token: 0x0600273E RID: 10046 RVA: 0x000905CD File Offset: 0x0008E7CD
		private void SetRenderedText(string value)
		{
			this.m_RenderedText = value;
		}

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x0600273F RID: 10047 RVA: 0x000905D7 File Offset: 0x0008E7D7
		internal string originalText
		{
			get
			{
				return this.m_OriginalText;
			}
		}

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x06002740 RID: 10048 RVA: 0x000435E1 File Offset: 0x000417E1
		public new ITextElementExperimentalFeatures experimental
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06002741 RID: 10049 RVA: 0x000905DF File Offset: 0x0008E7DF
		void ITextElementExperimentalFeatures.SetRenderedText(string renderedText)
		{
			this.SetRenderedText(renderedText);
		}

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x06002742 RID: 10050 RVA: 0x000435E1 File Offset: 0x000417E1
		[CreateProperty(ReadOnly = true)]
		public ITextSelection selection
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x06002743 RID: 10051 RVA: 0x000905EA File Offset: 0x0008E7EA
		// (set) Token: 0x06002744 RID: 10052 RVA: 0x00090600 File Offset: 0x0008E800
		bool ITextSelection.isSelectable
		{
			get
			{
				return this.m_IsSelectable && this.focusable;
			}
			set
			{
				bool flag = value == this.m_IsSelectable;
				if (!flag)
				{
					this.focusable = value;
					this.m_IsSelectable = value;
					base.EnableInClassList(TextElement.selectableUssClassName, value);
					base.NotifyPropertyChanged(in TextElement.isSelectableProperty);
				}
			}
		}

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x06002745 RID: 10053 RVA: 0x00090645 File Offset: 0x0008E845
		// (set) Token: 0x06002746 RID: 10054 RVA: 0x00090652 File Offset: 0x0008E852
		[CreateProperty]
		internal bool isSelectable
		{
			get
			{
				return this.selection.isSelectable;
			}
			set
			{
				this.selection.isSelectable = value;
			}
		}

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x06002747 RID: 10055 RVA: 0x00090661 File Offset: 0x0008E861
		// (set) Token: 0x06002748 RID: 10056 RVA: 0x00090680 File Offset: 0x0008E880
		int ITextSelection.cursorIndex
		{
			get
			{
				return this.selection.isSelectable ? this.selectingManipulator.cursorIndex : (-1);
			}
			set
			{
				int cursorIndex = this.selection.cursorIndex;
				bool isSelectable = this.selection.isSelectable;
				if (isSelectable)
				{
					this.selectingManipulator.cursorIndex = value;
				}
				bool flag = cursorIndex != this.selection.cursorIndex;
				if (flag)
				{
					base.NotifyPropertyChanged(in TextElement.cursorIndexProperty);
				}
			}
		}

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x06002749 RID: 10057 RVA: 0x000906D7 File Offset: 0x0008E8D7
		// (set) Token: 0x0600274A RID: 10058 RVA: 0x000906E4 File Offset: 0x0008E8E4
		[CreateProperty]
		private int cursorIndex
		{
			get
			{
				return this.selection.cursorIndex;
			}
			set
			{
				this.selection.cursorIndex = value;
			}
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x0600274B RID: 10059 RVA: 0x000906F3 File Offset: 0x0008E8F3
		// (set) Token: 0x0600274C RID: 10060 RVA: 0x00090710 File Offset: 0x0008E910
		int ITextSelection.selectIndex
		{
			get
			{
				return this.selection.isSelectable ? this.selectingManipulator.selectIndex : (-1);
			}
			set
			{
				int selectIndex = this.selection.selectIndex;
				bool isSelectable = this.selection.isSelectable;
				if (isSelectable)
				{
					this.selectingManipulator.selectIndex = value;
				}
				bool flag = selectIndex != this.selection.selectIndex;
				if (flag)
				{
					base.NotifyPropertyChanged(in TextElement.selectIndexProperty);
				}
			}
		}

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x0600274D RID: 10061 RVA: 0x00090767 File Offset: 0x0008E967
		// (set) Token: 0x0600274E RID: 10062 RVA: 0x00090774 File Offset: 0x0008E974
		[CreateProperty]
		private int selectIndex
		{
			get
			{
				return this.selection.selectIndex;
			}
			set
			{
				this.selection.selectIndex = value;
			}
		}

		// Token: 0x0600274F RID: 10063 RVA: 0x00090784 File Offset: 0x0008E984
		void ITextSelection.SelectAll()
		{
			bool isSelectable = this.selection.isSelectable;
			if (isSelectable)
			{
				this.selectingManipulator.m_SelectingUtilities.SelectAll();
			}
		}

		// Token: 0x06002750 RID: 10064 RVA: 0x000907B4 File Offset: 0x0008E9B4
		void ITextSelection.SelectNone()
		{
			bool isSelectable = this.selection.isSelectable;
			if (isSelectable)
			{
				this.selectingManipulator.m_SelectingUtilities.SelectNone();
			}
		}

		// Token: 0x06002751 RID: 10065 RVA: 0x000907E4 File Offset: 0x0008E9E4
		void ITextSelection.SelectRange(int cursorIndex, int selectionIndex)
		{
			bool isSelectable = this.selection.isSelectable;
			if (isSelectable)
			{
				this.selectingManipulator.m_SelectingUtilities.cursorIndex = cursorIndex;
				this.selectingManipulator.m_SelectingUtilities.selectIndex = selectionIndex;
				bool flag = this.m_TouchScreenKeyboard != null;
				if (flag)
				{
					this.m_TouchScreenKeyboard.selection = new RangeInt(Mathf.Min(cursorIndex, selectionIndex), Mathf.Abs(selectionIndex - cursorIndex));
				}
			}
		}

		// Token: 0x06002752 RID: 10066 RVA: 0x00090854 File Offset: 0x0008EA54
		bool ITextSelection.HasSelection()
		{
			return this.selection.isSelectable && this.selectingManipulator.HasSelection();
		}

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06002753 RID: 10067 RVA: 0x00090881 File Offset: 0x0008EA81
		// (set) Token: 0x06002754 RID: 10068 RVA: 0x0009088C File Offset: 0x0008EA8C
		bool ITextSelection.doubleClickSelectsWord
		{
			get
			{
				return this.m_DoubleClickSelectsWord;
			}
			set
			{
				bool flag = this.m_DoubleClickSelectsWord == value;
				if (!flag)
				{
					this.m_DoubleClickSelectsWord = value;
					base.NotifyPropertyChanged(in TextElement.doubleClickSelectsWordProperty);
				}
			}
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06002755 RID: 10069 RVA: 0x000908BC File Offset: 0x0008EABC
		// (set) Token: 0x06002756 RID: 10070 RVA: 0x000908C9 File Offset: 0x0008EAC9
		[CreateProperty]
		internal bool doubleClickSelectsWord
		{
			get
			{
				return this.selection.doubleClickSelectsWord;
			}
			set
			{
				this.selection.doubleClickSelectsWord = value;
			}
		}

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06002757 RID: 10071 RVA: 0x000908D8 File Offset: 0x0008EAD8
		// (set) Token: 0x06002758 RID: 10072 RVA: 0x000908E0 File Offset: 0x0008EAE0
		bool ITextSelection.tripleClickSelectsLine
		{
			get
			{
				return this.m_TripleClickSelectsLine;
			}
			set
			{
				bool flag = this.m_TripleClickSelectsLine == value;
				if (!flag)
				{
					this.m_TripleClickSelectsLine = value;
					base.NotifyPropertyChanged(in TextElement.tripleClickSelectsLineProperty);
				}
			}
		}

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x06002759 RID: 10073 RVA: 0x00090910 File Offset: 0x0008EB10
		// (set) Token: 0x0600275A RID: 10074 RVA: 0x0009091D File Offset: 0x0008EB1D
		[CreateProperty]
		internal bool tripleClickSelectsLine
		{
			get
			{
				return this.selection.tripleClickSelectsLine;
			}
			set
			{
				this.selection.tripleClickSelectsLine = value;
			}
		}

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x0600275B RID: 10075 RVA: 0x0009092C File Offset: 0x0008EB2C
		// (set) Token: 0x0600275C RID: 10076 RVA: 0x00090934 File Offset: 0x0008EB34
		bool ITextSelection.selectAllOnFocus
		{
			get
			{
				return this.m_SelectAllOnFocus;
			}
			set
			{
				bool flag = this.m_SelectAllOnFocus == value;
				if (!flag)
				{
					this.m_SelectAllOnFocus = value;
					base.NotifyPropertyChanged(in TextElement.selectAllOnFocusProperty);
				}
			}
		}

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x0600275D RID: 10077 RVA: 0x00090964 File Offset: 0x0008EB64
		// (set) Token: 0x0600275E RID: 10078 RVA: 0x00090971 File Offset: 0x0008EB71
		[CreateProperty]
		private bool selectAllOnFocus
		{
			get
			{
				return this.selection.selectAllOnFocus;
			}
			set
			{
				this.selection.selectAllOnFocus = value;
			}
		}

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x0600275F RID: 10079 RVA: 0x00090980 File Offset: 0x0008EB80
		// (set) Token: 0x06002760 RID: 10080 RVA: 0x00090988 File Offset: 0x0008EB88
		bool ITextSelection.selectAllOnMouseUp
		{
			get
			{
				return this.m_SelectAllOnMouseUp;
			}
			set
			{
				bool flag = this.m_SelectAllOnMouseUp == value;
				if (!flag)
				{
					this.m_SelectAllOnMouseUp = value;
					base.NotifyPropertyChanged(in TextElement.selectAllOnMouseUpProperty);
				}
			}
		}

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x06002761 RID: 10081 RVA: 0x000909B8 File Offset: 0x0008EBB8
		// (set) Token: 0x06002762 RID: 10082 RVA: 0x000909C5 File Offset: 0x0008EBC5
		[CreateProperty]
		private bool selectAllOnMouseUp
		{
			get
			{
				return this.selection.selectAllOnMouseUp;
			}
			set
			{
				this.selection.selectAllOnMouseUp = value;
			}
		}

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x06002763 RID: 10083 RVA: 0x000909D4 File Offset: 0x0008EBD4
		Vector2 ITextSelection.cursorPosition
		{
			get
			{
				this.uitkTextHandle.AddTextInfoToPermanentCache();
				return this.uitkTextHandle.GetCursorPositionFromStringIndexUsingLineHeight(this.selection.cursorIndex, false, true) + base.contentRect.min;
			}
		}

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06002764 RID: 10084 RVA: 0x00090A1D File Offset: 0x0008EC1D
		[CreateProperty(ReadOnly = true)]
		private Vector2 cursorPosition
		{
			get
			{
				return this.selection.cursorPosition;
			}
		}

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x06002765 RID: 10085 RVA: 0x00090A2C File Offset: 0x0008EC2C
		float ITextSelection.lineHeightAtCursorPosition
		{
			get
			{
				this.uitkTextHandle.AddTextInfoToPermanentCache();
				return this.uitkTextHandle.GetLineHeightFromCharacterIndex(this.selection.cursorIndex);
			}
		}

		// Token: 0x06002766 RID: 10086 RVA: 0x00090A60 File Offset: 0x0008EC60
		void ITextSelection.MoveTextEnd()
		{
			bool isSelectable = this.selection.isSelectable;
			if (isSelectable)
			{
				this.selectingManipulator.m_SelectingUtilities.MoveTextEnd();
			}
		}

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x06002767 RID: 10087 RVA: 0x00090A8E File Offset: 0x0008EC8E
		// (set) Token: 0x06002768 RID: 10088 RVA: 0x00090A98 File Offset: 0x0008EC98
		Color ITextSelection.selectionColor
		{
			get
			{
				return this.m_SelectionColor;
			}
			set
			{
				bool flag = this.m_SelectionColor == value;
				if (!flag)
				{
					this.m_SelectionColor = value;
					base.MarkDirtyRepaint();
				}
			}
		}

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x06002769 RID: 10089 RVA: 0x00090A8E File Offset: 0x0008EC8E
		// (set) Token: 0x0600276A RID: 10090 RVA: 0x00090AC8 File Offset: 0x0008ECC8
		internal Color selectionColor
		{
			get
			{
				return this.m_SelectionColor;
			}
			set
			{
				bool flag = this.m_SelectionColor == value;
				if (!flag)
				{
					this.m_SelectionColor = value;
					base.MarkDirtyRepaint();
				}
			}
		}

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x0600276B RID: 10091 RVA: 0x00090AF6 File Offset: 0x0008ECF6
		// (set) Token: 0x0600276C RID: 10092 RVA: 0x00090B00 File Offset: 0x0008ED00
		Color ITextSelection.cursorColor
		{
			get
			{
				return this.m_CursorColor;
			}
			set
			{
				bool flag = this.m_CursorColor == value;
				if (!flag)
				{
					this.m_CursorColor = value;
					base.MarkDirtyRepaint();
				}
			}
		}

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x0600276D RID: 10093 RVA: 0x00090AF6 File Offset: 0x0008ECF6
		// (set) Token: 0x0600276E RID: 10094 RVA: 0x00090B30 File Offset: 0x0008ED30
		internal Color cursorColor
		{
			get
			{
				return this.m_CursorColor;
			}
			set
			{
				bool flag = this.m_CursorColor == value;
				if (!flag)
				{
					this.m_CursorColor = value;
					base.MarkDirtyRepaint();
				}
			}
		}

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x0600276F RID: 10095 RVA: 0x00090B5E File Offset: 0x0008ED5E
		// (set) Token: 0x06002770 RID: 10096 RVA: 0x00090B68 File Offset: 0x0008ED68
		float ITextSelection.cursorWidth
		{
			get
			{
				return this.m_CursorWidth;
			}
			set
			{
				bool flag = Mathf.Approximately(this.m_CursorWidth, value);
				if (!flag)
				{
					this.m_CursorWidth = value;
					base.MarkDirtyRepaint();
				}
			}
		}

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x06002771 RID: 10097 RVA: 0x00090B98 File Offset: 0x0008ED98
		internal TextSelectingManipulator selectingManipulator
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				TextSelectingManipulator textSelectingManipulator;
				if ((textSelectingManipulator = this.m_SelectingManipulator) == null)
				{
					textSelectingManipulator = (this.m_SelectingManipulator = new TextSelectingManipulator(this));
				}
				return textSelectingManipulator;
			}
		}

		// Token: 0x06002772 RID: 10098 RVA: 0x00090BC0 File Offset: 0x0008EDC0
		private void DrawHighlighting(MeshGenerationContext mgc)
		{
			VisualElement visualElement = mgc.visualElement;
			Color color = ((visualElement != null) ? visualElement.playModeTintColor : Color.white);
			int num = Math.Min(this.selection.cursorIndex, this.selection.selectIndex);
			int num2 = Math.Max(this.selection.cursorIndex, this.selection.selectIndex);
			Vector2 vector = this.uitkTextHandle.GetCursorPositionFromStringIndexUsingLineHeight(num, false, true);
			Vector2 vector2 = this.uitkTextHandle.GetCursorPositionFromStringIndexUsingLineHeight(num2, false, true);
			int lineNumber = this.uitkTextHandle.GetLineNumber(num);
			int lineNumber2 = this.uitkTextHandle.GetLineNumber(num2);
			float lineHeight = this.uitkTextHandle.GetLineHeight(lineNumber);
			Vector2 min = base.contentRect.min;
			bool flag = this.m_TouchScreenKeyboard != null && this.hideMobileInput;
			if (flag)
			{
				TextInfo textInfo = this.uitkTextHandle.textInfo;
				int num3 = ((this.selection.selectIndex < this.selection.cursorIndex) ? textInfo.textElementInfo[this.selection.selectIndex].index : textInfo.textElementInfo[this.selection.cursorIndex].index);
				int num4 = ((this.selection.selectIndex < this.selection.cursorIndex) ? (this.selection.cursorIndex - num3) : (this.selection.selectIndex - num3));
				this.m_TouchScreenKeyboard.selection = new RangeInt(num3, num4);
			}
			bool flag2 = lineNumber == lineNumber2;
			if (flag2)
			{
				vector += min;
				vector2 += min;
				mgc.meshGenerator.DrawRectangle(new MeshGenerator.RectangleParams
				{
					rect = new Rect(vector.x, vector.y - lineHeight, vector2.x - vector.x, lineHeight),
					color = this.selectionColor,
					playmodeTintColor = color
				});
			}
			else
			{
				for (int i = lineNumber; i <= lineNumber2; i++)
				{
					bool flag3 = i == lineNumber;
					if (flag3)
					{
						int num5 = this.GetLastCharacterAt(i);
						vector2 = this.uitkTextHandle.GetCursorPositionFromStringIndexUsingLineHeight(num5, true, true);
					}
					else
					{
						bool flag4 = i == lineNumber2;
						if (flag4)
						{
							int num6 = this.uitkTextHandle.textInfo.lineInfo[i].firstCharacterIndex;
							vector = this.uitkTextHandle.GetCursorPositionFromStringIndexUsingLineHeight(num6, false, true);
							vector2 = this.uitkTextHandle.GetCursorPositionFromStringIndexUsingLineHeight(num2, true, true);
						}
						else
						{
							bool flag5 = i != lineNumber && i != lineNumber2;
							if (flag5)
							{
								int num6 = this.uitkTextHandle.textInfo.lineInfo[i].firstCharacterIndex;
								vector = this.uitkTextHandle.GetCursorPositionFromStringIndexUsingLineHeight(num6, false, true);
								int num5 = this.GetLastCharacterAt(i);
								vector2 = this.uitkTextHandle.GetCursorPositionFromStringIndexUsingLineHeight(num5, true, true);
							}
						}
					}
					vector += min;
					vector2 += min;
					mgc.meshGenerator.DrawRectangle(new MeshGenerator.RectangleParams
					{
						rect = new Rect(vector.x, vector.y - lineHeight, vector2.x - vector.x, lineHeight),
						color = this.selectionColor,
						playmodeTintColor = color
					});
				}
			}
		}

		// Token: 0x06002773 RID: 10099 RVA: 0x00090F30 File Offset: 0x0008F130
		private void DrawNativeHighlighting(MeshGenerationContext mgc)
		{
			VisualElement visualElement = mgc.visualElement;
			Color color = ((visualElement != null) ? visualElement.playModeTintColor : Color.white);
			int num = Math.Min(this.selection.cursorIndex, this.selection.selectIndex);
			int num2 = Math.Max(this.selection.cursorIndex, this.selection.selectIndex);
			Rect[] highlightRectangles = this.uitkTextHandle.GetHighlightRectangles(num, num2);
			for (int i = 0; i < highlightRectangles.Length; i++)
			{
				mgc.meshGenerator.DrawRectangle(new MeshGenerator.RectangleParams
				{
					rect = new Rect(highlightRectangles[i].position + base.contentRect.min, highlightRectangles[i].size),
					color = this.selectionColor,
					playmodeTintColor = color
				});
			}
			bool flag = this.m_TouchScreenKeyboard != null && this.hideMobileInput;
			if (flag)
			{
				this.m_TouchScreenKeyboard.selection = new RangeInt(num, num2 - num);
			}
		}

		// Token: 0x06002774 RID: 10100 RVA: 0x00091048 File Offset: 0x0008F248
		internal void DrawCaret(MeshGenerationContext mgc)
		{
			VisualElement visualElement = mgc.visualElement;
			Color color = ((visualElement != null) ? visualElement.playModeTintColor : Color.white);
			float characterHeightFromIndex = this.uitkTextHandle.GetCharacterHeightFromIndex(this.selection.cursorIndex);
			float num = AlignmentUtils.CeilToPixelGrid(this.selection.cursorWidth, base.scaledPixelsPerPoint, -0.02f);
			mgc.meshGenerator.DrawRectangle(new MeshGenerator.RectangleParams
			{
				rect = new Rect(this.selection.cursorPosition.x, this.selection.cursorPosition.y - characterHeightFromIndex, num, characterHeightFromIndex),
				color = this.cursorColor,
				playmodeTintColor = color
			});
		}

		// Token: 0x06002775 RID: 10101 RVA: 0x000910FC File Offset: 0x0008F2FC
		private int GetLastCharacterAt(int lineIndex)
		{
			int num = this.uitkTextHandle.textInfo.lineInfo[lineIndex].lastCharacterIndex;
			int firstCharacterIndex = this.uitkTextHandle.textInfo.lineInfo[lineIndex].firstCharacterIndex;
			TextElementInfo textElementInfo = this.uitkTextHandle.textInfo.textElementInfo[num];
			for (;;)
			{
				uint character = textElementInfo.character;
				if ((character != 10U && character != 13U) || num <= firstCharacterIndex)
				{
					break;
				}
				textElementInfo = this.uitkTextHandle.textInfo.textElementInfo[--num];
			}
			return num;
		}

		// Token: 0x040011A5 RID: 4517
		internal static readonly BindingId displayTooltipWhenElidedProperty = "displayTooltipWhenElided";

		// Token: 0x040011A6 RID: 4518
		internal static readonly BindingId emojiFallbackSupportProperty = "emojiFallbackSupport";

		// Token: 0x040011A7 RID: 4519
		internal static readonly BindingId enableRichTextProperty = "enableRichText";

		// Token: 0x040011A8 RID: 4520
		internal static readonly BindingId isElidedProperty = "isElided";

		// Token: 0x040011A9 RID: 4521
		internal static readonly BindingId parseEscapeSequencesProperty = "parseEscapeSequences";

		// Token: 0x040011AA RID: 4522
		internal static readonly BindingId textProperty = "text";

		// Token: 0x040011AB RID: 4523
		internal static readonly BindingId valueProperty = "value";

		// Token: 0x040011AC RID: 4524
		public static readonly string ussClassName = "unity-text-element";

		// Token: 0x040011AD RID: 4525
		public static readonly string selectableUssClassName = TextElement.ussClassName + "__selectable";

		// Token: 0x040011AF RID: 4527
		private string m_Text = string.Empty;

		// Token: 0x040011B0 RID: 4528
		private bool m_EnableRichText = true;

		// Token: 0x040011B1 RID: 4529
		private bool m_EmojiFallbackSupport = true;

		// Token: 0x040011B2 RID: 4530
		private bool m_ParseEscapeSequences;

		// Token: 0x040011B3 RID: 4531
		private bool m_DisplayTooltipWhenElided = true;

		// Token: 0x040011B5 RID: 4533
		internal static readonly string k_EllipsisText = "...";

		// Token: 0x040011B6 RID: 4534
		internal string elidedText;

		// Token: 0x040011B7 RID: 4535
		private bool m_WasElided;

		// Token: 0x040011B8 RID: 4536
		internal static readonly BindingId autoCorrectionProperty = "autoCorrection";

		// Token: 0x040011B9 RID: 4537
		internal static readonly BindingId hideSoftKeyboardProperty = "hideSoftKeyboard";

		// Token: 0x040011BA RID: 4538
		internal static readonly BindingId hideMobileInputProperty = "hideMobileInput";

		// Token: 0x040011BB RID: 4539
		internal static readonly BindingId keyboardTypeProperty = "keyboardType";

		// Token: 0x040011BC RID: 4540
		internal static readonly BindingId isReadOnlyProperty = "isReadOnly";

		// Token: 0x040011BD RID: 4541
		internal static readonly BindingId isPasswordProperty = "isPassword";

		// Token: 0x040011BE RID: 4542
		internal static readonly BindingId maxLengthProperty = "maxLength";

		// Token: 0x040011BF RID: 4543
		internal static readonly BindingId maskCharProperty = "maskChar";

		// Token: 0x040011C1 RID: 4545
		internal bool isInputField = false;

		// Token: 0x040011C2 RID: 4546
		private bool m_Multiline;

		// Token: 0x040011C3 RID: 4547
		internal TouchScreenKeyboard m_TouchScreenKeyboard;

		// Token: 0x040011C4 RID: 4548
		internal Action<bool> onIsReadOnlyChanged;

		// Token: 0x040011C5 RID: 4549
		internal TouchScreenKeyboardType m_KeyboardType = TouchScreenKeyboardType.Default;

		// Token: 0x040011C6 RID: 4550
		private bool m_HideSoftKeyboard;

		// Token: 0x040011C7 RID: 4551
		private bool m_HideMobileInput;

		// Token: 0x040011C8 RID: 4552
		private bool m_IsReadOnly = true;

		// Token: 0x040011C9 RID: 4553
		private int m_MaxLength = -1;

		// Token: 0x040011CA RID: 4554
		private string m_PlaceholderText = "";

		// Token: 0x040011D3 RID: 4563
		private const string ZeroWidthSpace = "\u200b";

		// Token: 0x040011D4 RID: 4564
		private string m_RenderedText;

		// Token: 0x040011D5 RID: 4565
		private string m_OriginalText;

		// Token: 0x040011D6 RID: 4566
		private char m_MaskChar;

		// Token: 0x040011D7 RID: 4567
		private bool m_IsPassword;

		// Token: 0x040011D8 RID: 4568
		private bool m_HidePlaceholderTextOnFocus;

		// Token: 0x040011D9 RID: 4569
		private bool m_AutoCorrection;

		// Token: 0x040011DA RID: 4570
		internal static readonly BindingId isSelectableProperty = "isSelectable";

		// Token: 0x040011DB RID: 4571
		internal static readonly BindingId cursorIndexProperty = "cursorIndex";

		// Token: 0x040011DC RID: 4572
		internal static readonly BindingId selectIndexProperty = "selectIndex";

		// Token: 0x040011DD RID: 4573
		internal static readonly BindingId doubleClickSelectsWordProperty = "doubleClickSelectsWord";

		// Token: 0x040011DE RID: 4574
		internal static readonly BindingId tripleClickSelectsLineProperty = "tripleClickSelectsLine";

		// Token: 0x040011DF RID: 4575
		internal static readonly BindingId cursorPositionProperty = "cursorPosition";

		// Token: 0x040011E0 RID: 4576
		internal static readonly BindingId selectAllOnFocusProperty = "selectAllOnFocus";

		// Token: 0x040011E1 RID: 4577
		internal static readonly BindingId selectAllOnMouseUpProperty = "selectAllOnMouseUp";

		// Token: 0x040011E2 RID: 4578
		internal static readonly BindingId selectionProperty = "selection";

		// Token: 0x040011E3 RID: 4579
		private TextSelectingManipulator m_SelectingManipulator;

		// Token: 0x040011E4 RID: 4580
		private bool m_IsSelectable;

		// Token: 0x040011E5 RID: 4581
		private bool m_DoubleClickSelectsWord = true;

		// Token: 0x040011E6 RID: 4582
		private bool m_TripleClickSelectsLine = true;

		// Token: 0x040011E7 RID: 4583
		private bool m_SelectAllOnFocus = false;

		// Token: 0x040011E8 RID: 4584
		private bool m_SelectAllOnMouseUp = false;

		// Token: 0x040011E9 RID: 4585
		private Color m_SelectionColor = new Color(0.239f, 0.502f, 0.875f, 0.65f);

		// Token: 0x040011EA RID: 4586
		private Color m_CursorColor = new Color(0.706f, 0.706f, 0.706f, 1f);

		// Token: 0x040011EB RID: 4587
		private float m_CursorWidth = 1f;

		// Token: 0x020004F9 RID: 1273
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : BindableElement.UxmlSerializedData
		{
			// Token: 0x06002777 RID: 10103 RVA: 0x0009133C File Offset: 0x0008F53C
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				UxmlDescriptionCache.RegisterType(typeof(TextElement.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("text", "text", null, Array.Empty<string>()),
					new UxmlAttributeNames("enableRichText", "enable-rich-text", null, Array.Empty<string>()),
					new UxmlAttributeNames("emojiFallbackSupport", "emoji-fallback-support", null, Array.Empty<string>()),
					new UxmlAttributeNames("parseEscapeSequences", "parse-escape-sequences", null, Array.Empty<string>()),
					new UxmlAttributeNames("isSelectable", "selectable", null, new string[] { "selectable" }),
					new UxmlAttributeNames("doubleClickSelectsWord", "double-click-selects-word", null, new string[] { "selectWordByDoubleClick", "select-word-by-double-click" }),
					new UxmlAttributeNames("tripleClickSelectsLine", "triple-click-selects-line", null, new string[] { "selectLineByTripleClick", "select-line-by-triple-click" }),
					new UxmlAttributeNames("displayTooltipWhenElided", "display-tooltip-when-elided", null, Array.Empty<string>())
				});
			}

			// Token: 0x06002778 RID: 10104 RVA: 0x0009146B File Offset: 0x0008F66B
			public override object CreateInstance()
			{
				return new TextElement();
			}

			// Token: 0x06002779 RID: 10105 RVA: 0x00091474 File Offset: 0x0008F674
			public override void Deserialize(object obj)
			{
				base.Deserialize(obj);
				TextElement textElement = (TextElement)obj;
				bool flag = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.text_UxmlAttributeFlags);
				if (flag)
				{
					textElement.text = this.text;
				}
				bool flag2 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.enableRichText_UxmlAttributeFlags);
				if (flag2)
				{
					textElement.enableRichText = this.enableRichText;
				}
				bool flag3 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.emojiFallbackSupport_UxmlAttributeFlags);
				if (flag3)
				{
					textElement.emojiFallbackSupport = this.emojiFallbackSupport;
				}
				bool flag4 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.parseEscapeSequences_UxmlAttributeFlags);
				if (flag4)
				{
					textElement.parseEscapeSequences = this.parseEscapeSequences;
				}
				bool flag5 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.isSelectable_UxmlAttributeFlags);
				if (flag5)
				{
					textElement.isSelectable = this.isSelectable;
				}
				bool flag6 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.doubleClickSelectsWord_UxmlAttributeFlags);
				if (flag6)
				{
					textElement.doubleClickSelectsWord = this.doubleClickSelectsWord;
				}
				bool flag7 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.tripleClickSelectsLine_UxmlAttributeFlags);
				if (flag7)
				{
					textElement.tripleClickSelectsLine = this.tripleClickSelectsLine;
				}
				bool flag8 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.displayTooltipWhenElided_UxmlAttributeFlags);
				if (flag8)
				{
					textElement.displayTooltipWhenElided = this.displayTooltipWhenElided;
				}
			}

			// Token: 0x040011EC RID: 4588
			[MultilineTextField]
			[SerializeField]
			private string text;

			// Token: 0x040011ED RID: 4589
			[HideInInspector]
			[UxmlIgnore]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags text_UxmlAttributeFlags;

			// Token: 0x040011EE RID: 4590
			[SerializeField]
			private bool enableRichText;

			// Token: 0x040011EF RID: 4591
			[SerializeField]
			[HideInInspector]
			[UxmlIgnore]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags enableRichText_UxmlAttributeFlags;

			// Token: 0x040011F0 RID: 4592
			[SerializeField]
			private bool emojiFallbackSupport;

			// Token: 0x040011F1 RID: 4593
			[HideInInspector]
			[UxmlIgnore]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags emojiFallbackSupport_UxmlAttributeFlags;

			// Token: 0x040011F2 RID: 4594
			[SerializeField]
			private bool parseEscapeSequences;

			// Token: 0x040011F3 RID: 4595
			[SerializeField]
			[HideInInspector]
			[UxmlIgnore]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags parseEscapeSequences_UxmlAttributeFlags;

			// Token: 0x040011F4 RID: 4596
			[SerializeField]
			[FormerlySerializedAs("selectable")]
			[UxmlAttribute("selectable")]
			private bool isSelectable;

			// Token: 0x040011F5 RID: 4597
			[UxmlIgnore]
			[SerializeField]
			[FormerlySerializedAs("selectable_UxmlAttributeFlags")]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags isSelectable_UxmlAttributeFlags;

			// Token: 0x040011F6 RID: 4598
			[FormerlySerializedAs("selectWordByDoubleClick")]
			[SerializeField]
			[UxmlAttribute("double-click-selects-word", new string[] { "select-word-by-double-click" })]
			private bool doubleClickSelectsWord;

			// Token: 0x040011F7 RID: 4599
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			[FormerlySerializedAs("selectWordByDoubleClick_UxmlAttributeFlags")]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags doubleClickSelectsWord_UxmlAttributeFlags;

			// Token: 0x040011F8 RID: 4600
			[UxmlAttribute("triple-click-selects-line", new string[] { "select-line-by-triple-click" })]
			[FormerlySerializedAs("selectLineByTripleClick")]
			[SerializeField]
			private bool tripleClickSelectsLine;

			// Token: 0x040011F9 RID: 4601
			[HideInInspector]
			[UxmlIgnore]
			[FormerlySerializedAs("selectLineByTripleClick_UxmlAttributeFlags")]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags tripleClickSelectsLine_UxmlAttributeFlags;

			// Token: 0x040011FA RID: 4602
			[SerializeField]
			private bool displayTooltipWhenElided;

			// Token: 0x040011FB RID: 4603
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags displayTooltipWhenElided_UxmlAttributeFlags;
		}

		// Token: 0x020004FA RID: 1274
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<TextElement, TextElement.UxmlTraits>
		{
		}

		// Token: 0x020004FB RID: 1275
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BindableElement.UxmlTraits
		{
			// Token: 0x170009FE RID: 2558
			// (get) Token: 0x0600277C RID: 10108 RVA: 0x00091584 File Offset: 0x0008F784
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get
				{
					yield break;
				}
			}

			// Token: 0x0600277D RID: 10109 RVA: 0x000915A4 File Offset: 0x0008F7A4
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				TextElement textElement = (TextElement)ve;
				textElement.text = this.m_Text.GetValueFromBag(bag, cc);
				textElement.enableRichText = this.m_EnableRichText.GetValueFromBag(bag, cc);
				textElement.emojiFallbackSupport = this.m_EmojiFallbackSupport.GetValueFromBag(bag, cc);
				textElement.isSelectable = this.m_Selectable.GetValueFromBag(bag, cc);
				textElement.parseEscapeSequences = this.m_ParseEscapeSequences.GetValueFromBag(bag, cc);
				textElement.selection.doubleClickSelectsWord = this.m_SelectWordByDoubleClick.GetValueFromBag(bag, cc);
				textElement.selection.tripleClickSelectsLine = this.m_SelectLineByTripleClick.GetValueFromBag(bag, cc);
				textElement.displayTooltipWhenElided = this.m_DisplayTooltipWhenElided.GetValueFromBag(bag, cc);
			}

			// Token: 0x040011FC RID: 4604
			private UxmlStringAttributeDescription m_Text = new UxmlStringAttributeDescription
			{
				name = "text"
			};

			// Token: 0x040011FD RID: 4605
			private UxmlBoolAttributeDescription m_EnableRichText = new UxmlBoolAttributeDescription
			{
				name = "enable-rich-text",
				defaultValue = true
			};

			// Token: 0x040011FE RID: 4606
			private UxmlBoolAttributeDescription m_EmojiFallbackSupport = new UxmlBoolAttributeDescription
			{
				name = "emoji-fallback-support",
				defaultValue = true
			};

			// Token: 0x040011FF RID: 4607
			private UxmlBoolAttributeDescription m_ParseEscapeSequences = new UxmlBoolAttributeDescription
			{
				name = "parse-escape-sequences"
			};

			// Token: 0x04001200 RID: 4608
			private UxmlBoolAttributeDescription m_Selectable = new UxmlBoolAttributeDescription
			{
				name = "selectable"
			};

			// Token: 0x04001201 RID: 4609
			private UxmlBoolAttributeDescription m_SelectWordByDoubleClick = new UxmlBoolAttributeDescription
			{
				name = "select-word-by-double-click"
			};

			// Token: 0x04001202 RID: 4610
			private UxmlBoolAttributeDescription m_SelectLineByTripleClick = new UxmlBoolAttributeDescription
			{
				name = "select-line-by-triple-click"
			};

			// Token: 0x04001203 RID: 4611
			private UxmlBoolAttributeDescription m_DisplayTooltipWhenElided = new UxmlBoolAttributeDescription
			{
				name = "display-tooltip-when-elided"
			};
		}
	}
}
