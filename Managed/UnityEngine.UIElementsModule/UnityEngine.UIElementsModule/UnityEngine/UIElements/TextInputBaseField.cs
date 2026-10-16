using System;
using Unity.Properties;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.TextCore.Text;

namespace UnityEngine.UIElements
{
	// Token: 0x02000123 RID: 291
	public abstract class TextInputBaseField<TValueType> : BaseField<TValueType>, IDelayedField
	{
		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x00027F5D File Offset: 0x0002615D
		// (set) Token: 0x0600091F RID: 2335 RVA: 0x00027F6A File Offset: 0x0002616A
		internal bool password
		{
			get
			{
				return this.textEdition.isPassword;
			}
			set
			{
				this.textEdition.isPassword = value;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x00027F79 File Offset: 0x00026179
		// (set) Token: 0x06000921 RID: 2337 RVA: 0x00027F86 File Offset: 0x00026186
		internal bool selectWordByDoubleClick
		{
			get
			{
				return this.textSelection.doubleClickSelectsWord;
			}
			set
			{
				this.textSelection.doubleClickSelectsWord = value;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x00027F95 File Offset: 0x00026195
		// (set) Token: 0x06000923 RID: 2339 RVA: 0x00027FA2 File Offset: 0x000261A2
		internal bool selectLineByTripleClick
		{
			get
			{
				return this.textSelection.tripleClickSelectsLine;
			}
			set
			{
				this.textSelection.tripleClickSelectsLine = value;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000924 RID: 2340 RVA: 0x00027FB1 File Offset: 0x000261B1
		// (set) Token: 0x06000925 RID: 2341 RVA: 0x00027FB9 File Offset: 0x000261B9
		internal bool readOnly
		{
			get
			{
				return this.isReadOnly;
			}
			set
			{
				this.isReadOnly = value;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000926 RID: 2342 RVA: 0x00027FC3 File Offset: 0x000261C3
		// (set) Token: 0x06000927 RID: 2343 RVA: 0x00027FD0 File Offset: 0x000261D0
		[CreateProperty]
		internal string placeholderText
		{
			get
			{
				return this.textEdition.placeholder;
			}
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			set
			{
				bool flag = this.textEdition.placeholder == value;
				if (!flag)
				{
					this.textEdition.placeholder = value;
					base.NotifyPropertyChanged(in TextInputBaseField<TValueType>.placeholderTextProperty);
				}
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x0002800E File Offset: 0x0002620E
		// (set) Token: 0x06000929 RID: 2345 RVA: 0x0002801C File Offset: 0x0002621C
		[CreateProperty]
		internal bool hidePlaceholderOnFocus
		{
			get
			{
				return this.textEdition.hidePlaceholderOnFocus;
			}
			set
			{
				bool flag = this.textEdition.hidePlaceholderOnFocus == value;
				if (!flag)
				{
					this.textEdition.hidePlaceholderOnFocus = value;
					base.NotifyPropertyChanged(in TextInputBaseField<TValueType>.hidePlaceholderOnFocusProperty);
				}
			}
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x00028057 File Offset: 0x00026257
		protected TextInputBaseField(int maxLength, char maskChar, TextInputBaseField<TValueType>.TextInputBase textInputBase)
			: this(null, maxLength, maskChar, textInputBase)
		{
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x00028068 File Offset: 0x00026268
		protected TextInputBaseField(string label, int maxLength, char maskChar, TextInputBaseField<TValueType>.TextInputBase textInputBase)
			: base(label, textInputBase)
		{
			base.tabIndex = 0;
			base.delegatesFocus = true;
			base.labelElement.tabIndex = -1;
			base.AddToClassList(TextInputBaseField<TValueType>.ussClassName);
			base.labelElement.AddToClassList(TextInputBaseField<TValueType>.labelUssClassName);
			base.visualInput.AddToClassList(TextInputBaseField<TValueType>.inputUssClassName);
			base.visualInput.AddToClassList(TextInputBaseField<TValueType>.singleLineInputUssClassName);
			this.m_TextInputBase = textInputBase;
			this.m_TextInputBase.textEdition.maxLength = maxLength;
			this.m_TextInputBase.textEdition.maskChar = maskChar;
			base.RegisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnFieldCustomStyleResolved), TrickleDown.NoTrickleDown);
			TextElement textElement = textInputBase.textElement;
			textElement.OnPlaceholderChanged = (Action)Delegate.Combine(textElement.OnPlaceholderChanged, new Action(this.OnPlaceholderChanged));
			this.m_UpdateTextFromValue = true;
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x0002814C File Offset: 0x0002634C
		protected internal TextInputBaseField<TValueType>.TextInputBase textInputBase
		{
			get
			{
				return this.m_TextInputBase;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x00028154 File Offset: 0x00026354
		[CreateProperty(ReadOnly = true)]
		public ITextSelection textSelection
		{
			get
			{
				return this.m_TextInputBase.textElement.selection;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x00028166 File Offset: 0x00026366
		[CreateProperty(ReadOnly = true)]
		public ITextEdition textEdition
		{
			get
			{
				return this.m_TextInputBase.textElement.edition;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x00028178 File Offset: 0x00026378
		// (set) Token: 0x06000930 RID: 2352 RVA: 0x0002818A File Offset: 0x0002638A
		protected Action<bool> onIsReadOnlyChanged
		{
			get
			{
				return this.m_TextInputBase.textElement.onIsReadOnlyChanged;
			}
			set
			{
				this.m_TextInputBase.textElement.onIsReadOnlyChanged = value;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x0002819D File Offset: 0x0002639D
		// (set) Token: 0x06000932 RID: 2354 RVA: 0x000281AC File Offset: 0x000263AC
		[CreateProperty]
		public bool isReadOnly
		{
			get
			{
				return this.textEdition.isReadOnly;
			}
			set
			{
				bool flag = this.textEdition.isReadOnly == value;
				if (!flag)
				{
					this.textEdition.isReadOnly = value;
					base.NotifyPropertyChanged(in TextInputBaseField<TValueType>.isReadOnlyProperty);
				}
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x00027F5D File Offset: 0x0002615D
		// (set) Token: 0x06000934 RID: 2356 RVA: 0x000281E8 File Offset: 0x000263E8
		[CreateProperty]
		public bool isPasswordField
		{
			get
			{
				return this.textEdition.isPassword;
			}
			set
			{
				bool flag = this.textEdition.isPassword == value;
				if (!flag)
				{
					this.textEdition.isPassword = value;
					this.m_TextInputBase.IncrementVersion(VersionChangeType.Repaint);
					base.NotifyPropertyChanged(in TextInputBaseField<TValueType>.isPasswordFieldProperty);
				}
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x00028234 File Offset: 0x00026434
		// (set) Token: 0x06000936 RID: 2358 RVA: 0x00028244 File Offset: 0x00026444
		[CreateProperty]
		public bool autoCorrection
		{
			get
			{
				return this.textEdition.autoCorrection;
			}
			set
			{
				bool flag = this.textEdition.autoCorrection == value;
				if (!flag)
				{
					this.textEdition.autoCorrection = value;
					base.NotifyPropertyChanged(in TextInputBaseField<TValueType>.autoCorrectionProperty);
				}
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x0002827F File Offset: 0x0002647F
		// (set) Token: 0x06000938 RID: 2360 RVA: 0x0002828C File Offset: 0x0002648C
		[CreateProperty]
		public bool hideSoftKeyboard
		{
			get
			{
				return this.textEdition.hideSoftKeyboard;
			}
			set
			{
				bool flag = this.textEdition.hideSoftKeyboard == value;
				if (!flag)
				{
					this.textEdition.hideSoftKeyboard = value;
					base.NotifyPropertyChanged(in TextInputBaseField<TValueType>.hideSoftKeyboardProperty);
				}
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000939 RID: 2361 RVA: 0x000282C7 File Offset: 0x000264C7
		// (set) Token: 0x0600093A RID: 2362 RVA: 0x000282D4 File Offset: 0x000264D4
		[CreateProperty]
		public bool hideMobileInput
		{
			get
			{
				return this.textEdition.hideMobileInput;
			}
			set
			{
				bool flag = this.textEdition.hideMobileInput == value;
				if (!flag)
				{
					this.textEdition.hideMobileInput = value;
					base.NotifyPropertyChanged(in TextInputBaseField<TValueType>.hideMobileInputProperty);
				}
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x0002830F File Offset: 0x0002650F
		// (set) Token: 0x0600093C RID: 2364 RVA: 0x0002831C File Offset: 0x0002651C
		[CreateProperty]
		public TouchScreenKeyboardType keyboardType
		{
			get
			{
				return this.textEdition.keyboardType;
			}
			set
			{
				bool flag = this.textEdition.keyboardType == value;
				if (!flag)
				{
					this.textEdition.keyboardType = value;
					base.NotifyPropertyChanged(in TextInputBaseField<TValueType>.keyboardTypeProperty);
				}
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x00028357 File Offset: 0x00026557
		public TouchScreenKeyboard touchScreenKeyboard
		{
			get
			{
				return this.textEdition.touchScreenKeyboard;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x00028364 File Offset: 0x00026564
		// (set) Token: 0x0600093F RID: 2367 RVA: 0x00028374 File Offset: 0x00026574
		[CreateProperty]
		public int maxLength
		{
			get
			{
				return this.textEdition.maxLength;
			}
			set
			{
				bool flag = this.textEdition.maxLength == value;
				if (!flag)
				{
					this.textEdition.maxLength = value;
					this.textEdition.UpdateText(this.ValueToString(this.value));
					base.NotifyPropertyChanged(in TextInputBaseField<TValueType>.maxLengthProperty);
				}
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x000283C7 File Offset: 0x000265C7
		// (set) Token: 0x06000941 RID: 2369 RVA: 0x000283D4 File Offset: 0x000265D4
		[CreateProperty]
		public bool isDelayed
		{
			get
			{
				return this.textEdition.isDelayed;
			}
			set
			{
				bool flag = this.textEdition.isDelayed == value;
				if (!flag)
				{
					this.textEdition.isDelayed = value;
					base.NotifyPropertyChanged(in TextInputBaseField<TValueType>.isDelayedProperty);
				}
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x0002840F File Offset: 0x0002660F
		// (set) Token: 0x06000943 RID: 2371 RVA: 0x0002841C File Offset: 0x0002661C
		[CreateProperty]
		public char maskChar
		{
			get
			{
				return this.textEdition.maskChar;
			}
			set
			{
				bool flag = this.textEdition.maskChar == value;
				if (!flag)
				{
					this.textEdition.maskChar = value;
					base.NotifyPropertyChanged(in TextInputBaseField<TValueType>.maskCharProperty);
				}
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x00028457 File Offset: 0x00026657
		[Obsolete("cursorColor is deprecated. Please use the corresponding USS property (--unity-cursor-color) instead.")]
		public Color selectionColor
		{
			get
			{
				return this.textSelection.selectionColor;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x00028464 File Offset: 0x00026664
		[Obsolete("cursorColor is deprecated. Please use the corresponding USS property (--unity-cursor-color) instead.")]
		public Color cursorColor
		{
			get
			{
				return this.textSelection.cursorColor;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000946 RID: 2374 RVA: 0x00028471 File Offset: 0x00026671
		// (set) Token: 0x06000947 RID: 2375 RVA: 0x00028480 File Offset: 0x00026680
		[CreateProperty]
		public int cursorIndex
		{
			get
			{
				return this.textSelection.cursorIndex;
			}
			set
			{
				bool flag = this.textSelection.cursorIndex == value;
				if (!flag)
				{
					this.textSelection.cursorIndex = value;
					base.NotifyPropertyChanged(in TextInputBaseField<TValueType>.cursorIndexProperty);
				}
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000948 RID: 2376 RVA: 0x000284BB File Offset: 0x000266BB
		[CreateProperty(ReadOnly = true)]
		public Vector2 cursorPosition
		{
			get
			{
				return this.textSelection.cursorPosition;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x000284C8 File Offset: 0x000266C8
		// (set) Token: 0x0600094A RID: 2378 RVA: 0x000284D8 File Offset: 0x000266D8
		[CreateProperty]
		public int selectIndex
		{
			get
			{
				return this.textSelection.selectIndex;
			}
			set
			{
				bool flag = this.textSelection.selectIndex == value;
				if (!flag)
				{
					this.textSelection.selectIndex = value;
					base.NotifyPropertyChanged(in TextInputBaseField<TValueType>.selectIndexProperty);
				}
			}
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x00028513 File Offset: 0x00026713
		public void SelectAll()
		{
			this.textSelection.SelectAll();
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x00028522 File Offset: 0x00026722
		public void SelectNone()
		{
			this.textSelection.SelectNone();
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x00028531 File Offset: 0x00026731
		public void SelectRange(int cursorIndex, int selectionIndex)
		{
			this.textSelection.SelectRange(cursorIndex, selectionIndex);
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600094E RID: 2382 RVA: 0x00028542 File Offset: 0x00026742
		// (set) Token: 0x0600094F RID: 2383 RVA: 0x00028550 File Offset: 0x00026750
		[CreateProperty]
		public bool selectAllOnFocus
		{
			get
			{
				return this.textSelection.selectAllOnFocus;
			}
			set
			{
				bool flag = this.textSelection.selectAllOnFocus == value;
				if (!flag)
				{
					this.textSelection.selectAllOnFocus = value;
					base.NotifyPropertyChanged(in TextInputBaseField<TValueType>.selectAllOnFocusProperty);
				}
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000950 RID: 2384 RVA: 0x0002858B File Offset: 0x0002678B
		// (set) Token: 0x06000951 RID: 2385 RVA: 0x00028598 File Offset: 0x00026798
		[CreateProperty]
		public bool selectAllOnMouseUp
		{
			get
			{
				return this.textSelection.selectAllOnMouseUp;
			}
			set
			{
				bool flag = this.textSelection.selectAllOnMouseUp == value;
				if (!flag)
				{
					this.textSelection.selectAllOnMouseUp = value;
					base.NotifyPropertyChanged(in TextInputBaseField<TValueType>.selectAllOnMouseUpProperty);
				}
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000952 RID: 2386 RVA: 0x00027F79 File Offset: 0x00026179
		// (set) Token: 0x06000953 RID: 2387 RVA: 0x000285D4 File Offset: 0x000267D4
		[CreateProperty]
		public bool doubleClickSelectsWord
		{
			get
			{
				return this.textSelection.doubleClickSelectsWord;
			}
			set
			{
				bool flag = this.textSelection.doubleClickSelectsWord == value;
				if (!flag)
				{
					this.textSelection.doubleClickSelectsWord = value;
					base.NotifyPropertyChanged(in TextInputBaseField<TValueType>.doubleClickSelectsWordProperty);
				}
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000954 RID: 2388 RVA: 0x00027F95 File Offset: 0x00026195
		// (set) Token: 0x06000955 RID: 2389 RVA: 0x00028610 File Offset: 0x00026810
		[CreateProperty]
		public bool tripleClickSelectsLine
		{
			get
			{
				return this.textSelection.tripleClickSelectsLine;
			}
			set
			{
				bool flag = this.textSelection.tripleClickSelectsLine == value;
				if (!flag)
				{
					this.textSelection.tripleClickSelectsLine = value;
					base.NotifyPropertyChanged(in TextInputBaseField<TValueType>.tripleClickSelectsLineProperty);
				}
			}
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x0002864C File Offset: 0x0002684C
		[Obsolete("SetVerticalScrollerVisibility is deprecated. Use TextField.verticalScrollerVisibility instead.")]
		public bool SetVerticalScrollerVisibility(ScrollerVisibility sv)
		{
			return this.textInputBase.SetVerticalScrollerVisibility(sv);
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x0002866A File Offset: 0x0002686A
		// (set) Token: 0x06000958 RID: 2392 RVA: 0x00028677 File Offset: 0x00026877
		public string text
		{
			get
			{
				return this.m_TextInputBase.text;
			}
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			protected internal set
			{
				this.m_TextInputBase.text = value;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000959 RID: 2393 RVA: 0x00028686 File Offset: 0x00026886
		// (set) Token: 0x0600095A RID: 2394 RVA: 0x00028698 File Offset: 0x00026898
		[CreateProperty]
		public bool emojiFallbackSupport
		{
			get
			{
				return this.m_TextInputBase.textElement.emojiFallbackSupport;
			}
			set
			{
				bool flag = this.m_TextInputBase.textElement.emojiFallbackSupport == value;
				if (!flag)
				{
					base.labelElement.emojiFallbackSupport = value;
					this.m_TextInputBase.textElement.emojiFallbackSupport = value;
					base.NotifyPropertyChanged(in TextInputBaseField<TValueType>.emojiFallbackSupportProperty);
				}
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600095B RID: 2395 RVA: 0x000286EA File Offset: 0x000268EA
		// (set) Token: 0x0600095C RID: 2396 RVA: 0x000286F8 File Offset: 0x000268F8
		[CreateProperty]
		public ScrollerVisibility verticalScrollerVisibility
		{
			get
			{
				return this.textInputBase.verticalScrollerVisibility;
			}
			set
			{
				bool flag = this.textInputBase.verticalScrollerVisibility == value;
				if (!flag)
				{
					this.textInputBase.SetVerticalScrollerVisibility(value);
					base.NotifyPropertyChanged(in TextInputBaseField<TValueType>.verticalScrollerVisibilityProperty);
				}
			}
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x00028734 File Offset: 0x00026934
		public Vector2 MeasureTextSize(string textToMeasure, float width, VisualElement.MeasureMode widthMode, float height, VisualElement.MeasureMode heightMode)
		{
			TextElement textElement = this.m_TextInputBase.textElement;
			RenderedText renderedText = new RenderedText(textToMeasure);
			return TextUtilities.MeasureVisualElementTextSize(textElement, in renderedText, width, widthMode, height, heightMode);
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x00028768 File Offset: 0x00026968
		[EventInterest(new Type[]
		{
			typeof(NavigationSubmitEvent),
			typeof(FocusInEvent),
			typeof(FocusEvent),
			typeof(FocusOutEvent),
			typeof(BlurEvent)
		})]
		protected override void HandleEventBubbleUp(EventBase evt)
		{
			base.HandleEventBubbleUp(evt);
			bool isReadOnly = this.textEdition.isReadOnly;
			if (!isReadOnly)
			{
				bool flag = evt.eventTypeId == EventBase<NavigationSubmitEvent>.TypeId() && evt.target != this.textInputBase.textElement;
				if (flag)
				{
					this.textInputBase.textElement.Focus();
				}
				else
				{
					bool flag2 = evt.eventTypeId == EventBase<NavigationMoveEvent>.TypeId() && evt.target != this.textInputBase.textElement;
					if (flag2)
					{
						this.focusController.SwitchFocusOnEvent(this.textInputBase.textElement, evt);
					}
					else
					{
						bool flag3 = evt.eventTypeId == EventBase<FocusInEvent>.TypeId();
						if (flag3)
						{
							bool showMixedValue = base.showMixedValue;
							if (showMixedValue)
							{
								((INotifyValueChanged<string>)this.textInputBase.textElement).SetValueWithoutNotify(null);
							}
						}
						else
						{
							bool flag4 = evt.eventTypeId == EventBase<FocusEvent>.TypeId();
							if (flag4)
							{
								this.UpdatePlaceholderClassList(null);
							}
							else
							{
								bool flag5 = evt.eventTypeId == EventBase<BlurEvent>.TypeId();
								if (flag5)
								{
									bool showMixedValue2 = base.showMixedValue;
									if (showMixedValue2)
									{
										this.UpdateMixedValueContent();
									}
									this.UpdatePlaceholderClassList(null);
									this.textInputBase.UpdateScrollOffset(false);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x000288AC File Offset: 0x00026AAC
		public override void SetValueWithoutNotify(TValueType newValue)
		{
			base.SetValueWithoutNotify(newValue);
			bool flag = this.textInputBase.textElement.needsPlaceholderIfTextIsEmpty && string.IsNullOrEmpty(this.ValueToString(newValue));
			if (flag)
			{
				base.visualInput.AddToClassList(TextInputBaseField<TValueType>.placeholderUssClassName);
			}
		}

		// Token: 0x06000960 RID: 2400
		protected abstract string ValueToString(TValueType value);

		// Token: 0x06000961 RID: 2401
		protected abstract TValueType StringToValue(string str);

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x000288FA File Offset: 0x00026AFA
		private protected override bool canSwitchToMixedValue
		{
			get
			{
				return !this.textInputBase.textElement.hasFocus;
			}
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x00028910 File Offset: 0x00026B10
		protected override void UpdateMixedValueContent()
		{
			bool showMixedValue = base.showMixedValue;
			if (showMixedValue)
			{
				bool updateTextFromValue = this.m_UpdateTextFromValue;
				if (updateTextFromValue)
				{
					((INotifyValueChanged<string>)this.textInputBase.textElement).SetValueWithoutNotify(BaseField<TValueType>.mixedValueString);
				}
				base.AddToClassList(BaseField<TValueType>.mixedValueLabelUssClassName);
				VisualElement visualInput = base.visualInput;
				if (visualInput != null)
				{
					visualInput.AddToClassList(BaseField<TValueType>.mixedValueLabelUssClassName);
				}
			}
			else
			{
				this.UpdateTextFromValue();
				VisualElement visualInput2 = base.visualInput;
				if (visualInput2 != null)
				{
					visualInput2.RemoveFromClassList(BaseField<TValueType>.mixedValueLabelUssClassName);
				}
				base.RemoveFromClassList(BaseField<TValueType>.mixedValueLabelUssClassName);
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x0002899D File Offset: 0x00026B9D
		internal bool hasFocus
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				return this.textInputBase.textElement.hasFocus;
			}
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x000289B0 File Offset: 0x00026BB0
		internal void OnPlaceholderChanged()
		{
			bool flag = !string.IsNullOrEmpty(this.textEdition.placeholder);
			if (flag)
			{
				base.RegisterCallback<ChangeEvent<TValueType>>(new EventCallback<ChangeEvent<TValueType>>(this.UpdatePlaceholderClassList), TrickleDown.NoTrickleDown);
			}
			else
			{
				base.UnregisterCallback<ChangeEvent<TValueType>>(new EventCallback<ChangeEvent<TValueType>>(this.UpdatePlaceholderClassList), TrickleDown.NoTrickleDown);
			}
			this.UpdatePlaceholderClassList(null);
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x00028A08 File Offset: 0x00026C08
		internal void UpdatePlaceholderClassList(ChangeEvent<TValueType> evt = null)
		{
			bool showPlaceholderText = this.textInputBase.textElement.showPlaceholderText;
			if (showPlaceholderText)
			{
				base.visualInput.AddToClassList(TextInputBaseField<TValueType>.placeholderUssClassName);
			}
			else
			{
				base.visualInput.RemoveFromClassList(TextInputBaseField<TValueType>.placeholderUssClassName);
			}
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x00028A4E File Offset: 0x00026C4E
		internal virtual void UpdateValueFromText()
		{
			this.value = this.StringToValue(this.text);
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x00002152 File Offset: 0x00000352
		internal virtual void UpdateTextFromValue()
		{
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x00028A64 File Offset: 0x00026C64
		private void OnFieldCustomStyleResolved(CustomStyleResolvedEvent e)
		{
			this.m_TextInputBase.OnInputCustomStyleResolved(e);
		}

		// Token: 0x04000520 RID: 1312
		internal static readonly BindingId autoCorrectionProperty = "autoCorrection";

		// Token: 0x04000521 RID: 1313
		internal static readonly BindingId hideMobileInputProperty = "hideMobileInput";

		// Token: 0x04000522 RID: 1314
		internal static readonly BindingId hideSoftKeyboardProperty = "hideSoftKeyboard";

		// Token: 0x04000523 RID: 1315
		internal static readonly BindingId hidePlaceholderOnFocusProperty = "hidePlaceholderOnFocus";

		// Token: 0x04000524 RID: 1316
		internal static readonly BindingId keyboardTypeProperty = "keyboardType";

		// Token: 0x04000525 RID: 1317
		internal static readonly BindingId isReadOnlyProperty = "isReadOnly";

		// Token: 0x04000526 RID: 1318
		internal static readonly BindingId isPasswordFieldProperty = "isPasswordField";

		// Token: 0x04000527 RID: 1319
		internal static readonly BindingId textSelectionProperty = "textSelection";

		// Token: 0x04000528 RID: 1320
		internal static readonly BindingId textEditionProperty = "textEdition";

		// Token: 0x04000529 RID: 1321
		internal static readonly BindingId placeholderTextProperty = "placeholderText";

		// Token: 0x0400052A RID: 1322
		internal static readonly BindingId cursorIndexProperty = "cursorIndex";

		// Token: 0x0400052B RID: 1323
		internal static readonly BindingId cursorPositionProperty = "cursorPosition";

		// Token: 0x0400052C RID: 1324
		internal static readonly BindingId selectIndexProperty = "selectIndex";

		// Token: 0x0400052D RID: 1325
		internal static readonly BindingId selectAllOnFocusProperty = "selectAllOnFocus";

		// Token: 0x0400052E RID: 1326
		internal static readonly BindingId selectAllOnMouseUpProperty = "selectAllOnMouseUp";

		// Token: 0x0400052F RID: 1327
		internal static readonly BindingId maxLengthProperty = "maxLength";

		// Token: 0x04000530 RID: 1328
		internal static readonly BindingId doubleClickSelectsWordProperty = "doubleClickSelectsWord";

		// Token: 0x04000531 RID: 1329
		internal static readonly BindingId tripleClickSelectsLineProperty = "tripleClickSelectsLine";

		// Token: 0x04000532 RID: 1330
		internal static readonly BindingId emojiFallbackSupportProperty = "emojiFallbackSupport";

		// Token: 0x04000533 RID: 1331
		internal static readonly BindingId isDelayedProperty = "isDelayed";

		// Token: 0x04000534 RID: 1332
		internal static readonly BindingId maskCharProperty = "maskChar";

		// Token: 0x04000535 RID: 1333
		internal static readonly BindingId verticalScrollerVisibilityProperty = "verticalScrollerVisibility";

		// Token: 0x04000536 RID: 1334
		private static CustomStyleProperty<Color> s_SelectionColorProperty = new CustomStyleProperty<Color>("--unity-selection-color");

		// Token: 0x04000537 RID: 1335
		private static CustomStyleProperty<Color> s_CursorColorProperty = new CustomStyleProperty<Color>("--unity-cursor-color");

		// Token: 0x04000538 RID: 1336
		internal const int kMaxLengthNone = -1;

		// Token: 0x04000539 RID: 1337
		internal const char kMaskCharDefault = '*';

		// Token: 0x0400053A RID: 1338
		public new static readonly string ussClassName = "unity-base-text-field";

		// Token: 0x0400053B RID: 1339
		public new static readonly string labelUssClassName = TextInputBaseField<TValueType>.ussClassName + "__label";

		// Token: 0x0400053C RID: 1340
		public new static readonly string inputUssClassName = TextInputBaseField<TValueType>.ussClassName + "__input";

		// Token: 0x0400053D RID: 1341
		internal static readonly string multilineContainerClassName = TextInputBaseField<TValueType>.ussClassName + "__multiline-container";

		// Token: 0x0400053E RID: 1342
		public static readonly string singleLineInputUssClassName = TextInputBaseField<TValueType>.inputUssClassName + "--single-line";

		// Token: 0x0400053F RID: 1343
		public static readonly string multilineInputUssClassName = TextInputBaseField<TValueType>.inputUssClassName + "--multiline";

		// Token: 0x04000540 RID: 1344
		public static readonly string placeholderUssClassName = TextInputBaseField<TValueType>.inputUssClassName + "--placeholder";

		// Token: 0x04000541 RID: 1345
		internal static readonly string multilineInputWithScrollViewUssClassName = TextInputBaseField<TValueType>.multilineInputUssClassName + "--scroll-view";

		// Token: 0x04000542 RID: 1346
		public static readonly string textInputUssName = "unity-text-input";

		// Token: 0x04000543 RID: 1347
		private TextInputBaseField<TValueType>.TextInputBase m_TextInputBase;

		// Token: 0x04000544 RID: 1348
		internal bool m_UpdateTextFromValue;

		// Token: 0x02000124 RID: 292
		[ExcludeFromDocs]
		[Serializable]
		public new abstract class UxmlSerializedData : BaseField<TValueType>.UxmlSerializedData
		{
			// Token: 0x0600096B RID: 2411 RVA: 0x00028C8C File Offset: 0x00026E8C
			public new static void Register()
			{
				BaseField<TValueType>.UxmlSerializedData.Register();
				UxmlDescriptionCache.RegisterType(typeof(TextInputBaseField<TValueType>.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("maxLength", "max-length", null, new string[] { "maxLength" }),
					new UxmlAttributeNames("isPasswordField", "password", null, Array.Empty<string>()),
					new UxmlAttributeNames("maskChar", "mask-character", null, new string[] { "maskCharacter" }),
					new UxmlAttributeNames("placeholderText", "placeholder-text", null, Array.Empty<string>()),
					new UxmlAttributeNames("hidePlaceholderOnFocus", "hide-placeholder-on-focus", null, Array.Empty<string>()),
					new UxmlAttributeNames("isReadOnly", "readonly", null, Array.Empty<string>()),
					new UxmlAttributeNames("isDelayed", "is-delayed", null, Array.Empty<string>()),
					new UxmlAttributeNames("verticalScrollerVisibility", "vertical-scroller-visibility", null, Array.Empty<string>()),
					new UxmlAttributeNames("selectAllOnMouseUp", "select-all-on-mouse-up", null, Array.Empty<string>()),
					new UxmlAttributeNames("selectAllOnFocus", "select-all-on-focus", null, Array.Empty<string>()),
					new UxmlAttributeNames("doubleClickSelectsWord", "select-word-by-double-click", null, Array.Empty<string>()),
					new UxmlAttributeNames("tripleClickSelectsLine", "select-line-by-triple-click", null, Array.Empty<string>()),
					new UxmlAttributeNames("emojiFallbackSupport", "emoji-fallback-support", null, Array.Empty<string>()),
					new UxmlAttributeNames("hideSoftKeyboard", "hide-soft-keyboard", null, Array.Empty<string>()),
					new UxmlAttributeNames("hideMobileInput", "hide-mobile-input", null, Array.Empty<string>()),
					new UxmlAttributeNames("keyboardType", "keyboard-type", null, Array.Empty<string>()),
					new UxmlAttributeNames("autoCorrection", "auto-correction", null, Array.Empty<string>())
				});
			}

			// Token: 0x0600096C RID: 2412 RVA: 0x00028EAD File Offset: 0x000270AD
			public override object CreateInstance()
			{
				throw new MissingMethodException();
			}

			// Token: 0x0600096D RID: 2413 RVA: 0x00028EB4 File Offset: 0x000270B4
			public override void Deserialize(object obj)
			{
				base.Deserialize(obj);
				TextInputBaseField<TValueType> textInputBaseField = (TextInputBaseField<TValueType>)obj;
				bool flag = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.maxLength_UxmlAttributeFlags);
				if (flag)
				{
					textInputBaseField.maxLength = this.maxLength;
				}
				bool flag2 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.isPasswordField_UxmlAttributeFlags);
				if (flag2)
				{
					textInputBaseField.isPasswordField = this.isPasswordField;
				}
				bool flag3 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.maskChar_UxmlAttributeFlags);
				if (flag3)
				{
					textInputBaseField.maskChar = this.maskChar;
				}
				bool flag4 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.placeholderText_UxmlAttributeFlags);
				if (flag4)
				{
					textInputBaseField.placeholderText = this.placeholderText;
				}
				bool flag5 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.hidePlaceholderOnFocus_UxmlAttributeFlags);
				if (flag5)
				{
					textInputBaseField.hidePlaceholderOnFocus = this.hidePlaceholderOnFocus;
				}
				bool flag6 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.isReadOnly_UxmlAttributeFlags);
				if (flag6)
				{
					textInputBaseField.isReadOnly = this.isReadOnly;
				}
				bool flag7 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.isDelayed_UxmlAttributeFlags);
				if (flag7)
				{
					textInputBaseField.isDelayed = this.isDelayed;
				}
				bool flag8 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.verticalScrollerVisibility_UxmlAttributeFlags);
				if (flag8)
				{
					textInputBaseField.verticalScrollerVisibility = this.verticalScrollerVisibility;
				}
				bool flag9 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.selectAllOnMouseUp_UxmlAttributeFlags);
				if (flag9)
				{
					textInputBaseField.textSelection.selectAllOnMouseUp = this.selectAllOnMouseUp;
				}
				bool flag10 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.selectAllOnFocus_UxmlAttributeFlags);
				if (flag10)
				{
					textInputBaseField.textSelection.selectAllOnFocus = this.selectAllOnFocus;
				}
				bool flag11 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.doubleClickSelectsWord_UxmlAttributeFlags);
				if (flag11)
				{
					textInputBaseField.doubleClickSelectsWord = this.doubleClickSelectsWord;
				}
				bool flag12 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.tripleClickSelectsLine_UxmlAttributeFlags);
				if (flag12)
				{
					textInputBaseField.tripleClickSelectsLine = this.tripleClickSelectsLine;
				}
				bool flag13 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.emojiFallbackSupport_UxmlAttributeFlags);
				if (flag13)
				{
					textInputBaseField.emojiFallbackSupport = this.emojiFallbackSupport;
				}
				bool flag14 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.hideSoftKeyboard_UxmlAttributeFlags);
				if (flag14)
				{
					textInputBaseField.hideSoftKeyboard = this.hideSoftKeyboard;
				}
				bool flag15 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.hideMobileInput_UxmlAttributeFlags);
				if (flag15)
				{
					textInputBaseField.hideMobileInput = this.hideMobileInput;
				}
				bool flag16 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.keyboardType_UxmlAttributeFlags);
				if (flag16)
				{
					textInputBaseField.keyboardType = this.keyboardType;
				}
				bool flag17 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.autoCorrection_UxmlAttributeFlags);
				if (flag17)
				{
					textInputBaseField.autoCorrection = this.autoCorrection;
				}
			}

			// Token: 0x04000545 RID: 1349
			[SerializeField]
			private string placeholderText;

			// Token: 0x04000546 RID: 1350
			[SerializeField]
			[UxmlAttribute(obsoleteNames = new string[] { "maxLength" })]
			[Delayed]
			private int maxLength;

			// Token: 0x04000547 RID: 1351
			[SerializeField]
			private TouchScreenKeyboardType keyboardType;

			// Token: 0x04000548 RID: 1352
			[SerializeField]
			private protected ScrollerVisibility verticalScrollerVisibility;

			// Token: 0x04000549 RID: 1353
			[UxmlAttribute("password")]
			[SerializeField]
			private bool isPasswordField;

			// Token: 0x0400054A RID: 1354
			[UxmlAttribute("mask-character", obsoleteNames = new string[] { "maskCharacter" })]
			[SerializeField]
			private char maskChar;

			// Token: 0x0400054B RID: 1355
			[SerializeField]
			private bool hidePlaceholderOnFocus;

			// Token: 0x0400054C RID: 1356
			[UxmlAttribute("readonly")]
			[SerializeField]
			private bool isReadOnly;

			// Token: 0x0400054D RID: 1357
			[SerializeField]
			private bool isDelayed;

			// Token: 0x0400054E RID: 1358
			[SerializeField]
			private bool selectAllOnMouseUp;

			// Token: 0x0400054F RID: 1359
			[SerializeField]
			private bool selectAllOnFocus;

			// Token: 0x04000550 RID: 1360
			[UxmlAttribute("select-word-by-double-click")]
			[SerializeField]
			private bool doubleClickSelectsWord;

			// Token: 0x04000551 RID: 1361
			[UxmlAttribute("select-line-by-triple-click")]
			[SerializeField]
			private bool tripleClickSelectsLine;

			// Token: 0x04000552 RID: 1362
			[SerializeField]
			private bool emojiFallbackSupport;

			// Token: 0x04000553 RID: 1363
			[SerializeField]
			private bool hideSoftKeyboard;

			// Token: 0x04000554 RID: 1364
			[SerializeField]
			private bool hideMobileInput;

			// Token: 0x04000555 RID: 1365
			[SerializeField]
			private bool autoCorrection;

			// Token: 0x04000556 RID: 1366
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags maxLength_UxmlAttributeFlags;

			// Token: 0x04000557 RID: 1367
			[SerializeField]
			[HideInInspector]
			[UxmlIgnore]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags isPasswordField_UxmlAttributeFlags;

			// Token: 0x04000558 RID: 1368
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags maskChar_UxmlAttributeFlags;

			// Token: 0x04000559 RID: 1369
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags placeholderText_UxmlAttributeFlags;

			// Token: 0x0400055A RID: 1370
			[SerializeField]
			[HideInInspector]
			[UxmlIgnore]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags hidePlaceholderOnFocus_UxmlAttributeFlags;

			// Token: 0x0400055B RID: 1371
			[UxmlIgnore]
			[SerializeField]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags isReadOnly_UxmlAttributeFlags;

			// Token: 0x0400055C RID: 1372
			[HideInInspector]
			[UxmlIgnore]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags isDelayed_UxmlAttributeFlags;

			// Token: 0x0400055D RID: 1373
			[UxmlIgnore]
			[HideInInspector]
			[SerializeField]
			private protected UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags verticalScrollerVisibility_UxmlAttributeFlags;

			// Token: 0x0400055E RID: 1374
			[UxmlIgnore]
			[SerializeField]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags selectAllOnMouseUp_UxmlAttributeFlags;

			// Token: 0x0400055F RID: 1375
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags selectAllOnFocus_UxmlAttributeFlags;

			// Token: 0x04000560 RID: 1376
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags doubleClickSelectsWord_UxmlAttributeFlags;

			// Token: 0x04000561 RID: 1377
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags tripleClickSelectsLine_UxmlAttributeFlags;

			// Token: 0x04000562 RID: 1378
			[UxmlIgnore]
			[SerializeField]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags emojiFallbackSupport_UxmlAttributeFlags;

			// Token: 0x04000563 RID: 1379
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags hideSoftKeyboard_UxmlAttributeFlags;

			// Token: 0x04000564 RID: 1380
			[SerializeField]
			[HideInInspector]
			[UxmlIgnore]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags hideMobileInput_UxmlAttributeFlags;

			// Token: 0x04000565 RID: 1381
			[HideInInspector]
			[UxmlIgnore]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags keyboardType_UxmlAttributeFlags;

			// Token: 0x04000566 RID: 1382
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags autoCorrection_UxmlAttributeFlags;
		}

		// Token: 0x02000125 RID: 293
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseFieldTraits<string, UxmlStringAttributeDescription>
		{
			// Token: 0x0600096F RID: 2415 RVA: 0x000290D4 File Offset: 0x000272D4
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				TextInputBaseField<TValueType> textInputBaseField = (TextInputBaseField<TValueType>)ve;
				textInputBaseField.maxLength = this.m_MaxLength.GetValueFromBag(bag, cc);
				textInputBaseField.password = this.m_Password.GetValueFromBag(bag, cc);
				textInputBaseField.readOnly = this.m_IsReadOnly.GetValueFromBag(bag, cc);
				textInputBaseField.isDelayed = this.m_IsDelayed.GetValueFromBag(bag, cc);
				textInputBaseField.textSelection.selectAllOnFocus = this.m_SelectAllOnFocus.GetValueFromBag(bag, cc);
				textInputBaseField.textSelection.selectAllOnMouseUp = this.m_SelectAllOnMouseUp.GetValueFromBag(bag, cc);
				textInputBaseField.doubleClickSelectsWord = this.m_SelectWordByDoubleClick.GetValueFromBag(bag, cc);
				textInputBaseField.tripleClickSelectsLine = this.m_SelectLineByTripleClick.GetValueFromBag(bag, cc);
				textInputBaseField.emojiFallbackSupport = this.m_EmojiFallbackSupport.GetValueFromBag(bag, cc);
				ScrollerVisibility scrollerVisibility = ScrollerVisibility.Hidden;
				this.m_VerticalScrollerVisibility.TryGetValueFromBag(bag, cc, ref scrollerVisibility);
				textInputBaseField.verticalScrollerVisibility = scrollerVisibility;
				textInputBaseField.hideMobileInput = this.m_HideMobileInput.GetValueFromBag(bag, cc);
				textInputBaseField.keyboardType = this.m_KeyboardType.GetValueFromBag(bag, cc);
				textInputBaseField.autoCorrection = this.m_AutoCorrection.GetValueFromBag(bag, cc);
				string valueFromBag = this.m_MaskCharacter.GetValueFromBag(bag, cc);
				textInputBaseField.maskChar = (string.IsNullOrEmpty(valueFromBag) ? '*' : valueFromBag[0]);
				textInputBaseField.placeholderText = this.m_PlaceholderText.GetValueFromBag(bag, cc);
				textInputBaseField.hidePlaceholderOnFocus = this.m_HidePlaceholderOnFocus.GetValueFromBag(bag, cc);
			}

			// Token: 0x04000567 RID: 1383
			private UxmlIntAttributeDescription m_MaxLength = new UxmlIntAttributeDescription
			{
				name = "max-length",
				obsoleteNames = new string[] { "maxLength" },
				defaultValue = -1
			};

			// Token: 0x04000568 RID: 1384
			private UxmlBoolAttributeDescription m_Password = new UxmlBoolAttributeDescription
			{
				name = "password"
			};

			// Token: 0x04000569 RID: 1385
			private UxmlStringAttributeDescription m_MaskCharacter = new UxmlStringAttributeDescription
			{
				name = "mask-character",
				obsoleteNames = new string[] { "maskCharacter" },
				defaultValue = '*'.ToString()
			};

			// Token: 0x0400056A RID: 1386
			private UxmlStringAttributeDescription m_PlaceholderText = new UxmlStringAttributeDescription
			{
				name = "placeholder-text"
			};

			// Token: 0x0400056B RID: 1387
			private UxmlBoolAttributeDescription m_HidePlaceholderOnFocus = new UxmlBoolAttributeDescription
			{
				name = "hide-placeholder-on-focus"
			};

			// Token: 0x0400056C RID: 1388
			private UxmlBoolAttributeDescription m_IsReadOnly = new UxmlBoolAttributeDescription
			{
				name = "readonly"
			};

			// Token: 0x0400056D RID: 1389
			private UxmlBoolAttributeDescription m_IsDelayed = new UxmlBoolAttributeDescription
			{
				name = "is-delayed"
			};

			// Token: 0x0400056E RID: 1390
			private UxmlEnumAttributeDescription<ScrollerVisibility> m_VerticalScrollerVisibility = new UxmlEnumAttributeDescription<ScrollerVisibility>
			{
				name = "vertical-scroller-visibility",
				defaultValue = ScrollerVisibility.Hidden
			};

			// Token: 0x0400056F RID: 1391
			private UxmlBoolAttributeDescription m_SelectAllOnMouseUp = new UxmlBoolAttributeDescription
			{
				name = "select-all-on-mouse-up",
				defaultValue = true
			};

			// Token: 0x04000570 RID: 1392
			private UxmlBoolAttributeDescription m_SelectAllOnFocus = new UxmlBoolAttributeDescription
			{
				name = "select-all-on-focus",
				defaultValue = true
			};

			// Token: 0x04000571 RID: 1393
			private UxmlBoolAttributeDescription m_SelectWordByDoubleClick = new UxmlBoolAttributeDescription
			{
				name = "select-word-by-double-click",
				defaultValue = true
			};

			// Token: 0x04000572 RID: 1394
			private UxmlBoolAttributeDescription m_SelectLineByTripleClick = new UxmlBoolAttributeDescription
			{
				name = "select-line-by-triple-click",
				defaultValue = true
			};

			// Token: 0x04000573 RID: 1395
			private UxmlBoolAttributeDescription m_EmojiFallbackSupport = new UxmlBoolAttributeDescription
			{
				name = "emoji-fallback-support",
				defaultValue = true
			};

			// Token: 0x04000574 RID: 1396
			private UxmlBoolAttributeDescription m_HideMobileInput = new UxmlBoolAttributeDescription
			{
				name = "hide-mobile-input"
			};

			// Token: 0x04000575 RID: 1397
			private UxmlEnumAttributeDescription<TouchScreenKeyboardType> m_KeyboardType = new UxmlEnumAttributeDescription<TouchScreenKeyboardType>
			{
				name = "keyboard-type"
			};

			// Token: 0x04000576 RID: 1398
			private UxmlBoolAttributeDescription m_AutoCorrection = new UxmlBoolAttributeDescription
			{
				name = "auto-correction"
			};
		}

		// Token: 0x02000126 RID: 294
		protected internal abstract class TextInputBase : VisualElement
		{
			// Token: 0x1700017D RID: 381
			// (get) Token: 0x06000971 RID: 2417 RVA: 0x00029453 File Offset: 0x00027653
			// (set) Token: 0x06000972 RID: 2418 RVA: 0x0002945B File Offset: 0x0002765B
			internal TextElement textElement { get; private set; }

			// Token: 0x06000973 RID: 2419 RVA: 0x00029464 File Offset: 0x00027664
			internal TextInputBase()
			{
				base.delegatesFocus = true;
				this.textElement = new TextElement();
				this.textElement.isInputField = true;
				this.textElement.selection.isSelectable = true;
				this.textEdition.isReadOnly = false;
				this.textSelection.isSelectable = true;
				this.textSelection.selectAllOnFocus = true;
				this.textSelection.selectAllOnMouseUp = true;
				this.textElement.enableRichText = false;
				this.textElement.tabIndex = 0;
				ITextEdition textEdition = this.textEdition;
				textEdition.AcceptCharacter = (Func<char, bool>)Delegate.Combine(textEdition.AcceptCharacter, new Func<char, bool>(this.AcceptCharacter));
				ITextEdition textEdition2 = this.textEdition;
				textEdition2.UpdateScrollOffset = (Action<bool>)Delegate.Combine(textEdition2.UpdateScrollOffset, new Action<bool>(this.UpdateScrollOffset));
				ITextEdition textEdition3 = this.textEdition;
				textEdition3.UpdateValueFromText = (Action)Delegate.Combine(textEdition3.UpdateValueFromText, new Action(this.UpdateValueFromText));
				ITextEdition textEdition4 = this.textEdition;
				textEdition4.UpdateTextFromValue = (Action)Delegate.Combine(textEdition4.UpdateTextFromValue, new Action(this.UpdateTextFromValue));
				ITextEdition textEdition5 = this.textEdition;
				textEdition5.MoveFocusToCompositeRoot = (Action)Delegate.Combine(textEdition5.MoveFocusToCompositeRoot, new Action(this.MoveFocusToCompositeRoot));
				this.textEdition.GetDefaultValueType = new Func<string>(this.GetDefaultValueType);
				base.AddToClassList(TextInputBaseField<TValueType>.inputUssClassName);
				base.name = TextInputBaseField<string>.textInputUssName;
				this.SetSingleLine();
				base.RegisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnInputCustomStyleResolved), TrickleDown.NoTrickleDown);
				base.tabIndex = -1;
			}

			// Token: 0x1700017E RID: 382
			// (get) Token: 0x06000974 RID: 2420 RVA: 0x00029632 File Offset: 0x00027832
			public ITextSelection textSelection
			{
				get
				{
					return this.textElement.selection;
				}
			}

			// Token: 0x1700017F RID: 383
			// (get) Token: 0x06000975 RID: 2421 RVA: 0x0002963F File Offset: 0x0002783F
			public ITextEdition textEdition
			{
				get
				{
					return this.textElement.edition;
				}
			}

			// Token: 0x17000180 RID: 384
			// (get) Token: 0x06000976 RID: 2422 RVA: 0x0002964C File Offset: 0x0002784C
			// (set) Token: 0x06000977 RID: 2423 RVA: 0x00029654 File Offset: 0x00027854
			internal bool isDragging { get; set; }

			// Token: 0x17000181 RID: 385
			// (get) Token: 0x06000978 RID: 2424 RVA: 0x0002965D File Offset: 0x0002785D
			// (set) Token: 0x06000979 RID: 2425 RVA: 0x0002966C File Offset: 0x0002786C
			public string text
			{
				get
				{
					return this.textElement.text;
				}
				set
				{
					bool flag = this.textElement.text == value;
					if (!flag)
					{
						this.textElement.text = value;
					}
				}
			}

			// Token: 0x17000182 RID: 386
			// (get) Token: 0x0600097A RID: 2426 RVA: 0x0002969E File Offset: 0x0002789E
			internal string originalText
			{
				get
				{
					return this.textElement.originalText;
				}
			}

			// Token: 0x0600097B RID: 2427 RVA: 0x000296AB File Offset: 0x000278AB
			protected virtual TValueType StringToValue(string str)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600097C RID: 2428 RVA: 0x000296B4 File Offset: 0x000278B4
			internal void UpdateValueFromText()
			{
				TextInputBaseField<TValueType> textInputBaseField = (TextInputBaseField<TValueType>)base.parent;
				textInputBaseField.UpdateValueFromText();
			}

			// Token: 0x0600097D RID: 2429 RVA: 0x000296D8 File Offset: 0x000278D8
			internal void UpdateTextFromValue()
			{
				TextInputBaseField<TValueType> textInputBaseField = (TextInputBaseField<TValueType>)base.parent;
				textInputBaseField.UpdateTextFromValue();
			}

			// Token: 0x0600097E RID: 2430 RVA: 0x000296FC File Offset: 0x000278FC
			internal void MoveFocusToCompositeRoot()
			{
				TextInputBaseField<TValueType> textInputBaseField = (TextInputBaseField<TValueType>)base.parent;
				this.focusController.SwitchFocus(textInputBaseField, false, DispatchMode.Default);
				this.textEdition.keyboardType = TouchScreenKeyboardType.Default;
				this.textEdition.autoCorrection = false;
			}

			// Token: 0x0600097F RID: 2431 RVA: 0x0002973F File Offset: 0x0002793F
			private void MakeSureScrollViewDoesNotLeakEvents(ChangeEvent<float> evt)
			{
				evt.StopPropagation();
			}

			// Token: 0x06000980 RID: 2432 RVA: 0x0002974C File Offset: 0x0002794C
			internal void SetSingleLine()
			{
				base.hierarchy.Clear();
				this.RemoveMultilineComponents();
				base.Add(this.textElement);
				base.AddToClassList(TextInputBaseField<TValueType>.singleLineInputUssClassName);
				this.textElement.AddToClassList(TextInputBaseField<TValueType>.TextInputBase.innerTextElementUssClassName);
				this.textElement.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.TextElementOnGeometryChangedEvent), TrickleDown.NoTrickleDown);
				bool flag = this.scrollOffset != Vector2.zero;
				if (flag)
				{
					this.scrollOffset.y = 0f;
					this.UpdateScrollOffset(false);
				}
				bool hasFocus = this.textElement.hasFocus;
				if (hasFocus)
				{
					this.textElement.uitkTextHandle.AddTextInfoToPermanentCache();
				}
			}

			// Token: 0x06000981 RID: 2433 RVA: 0x00029804 File Offset: 0x00027A04
			internal void SetMultiline()
			{
				bool flag = !this.textEdition.multiline;
				if (!flag)
				{
					this.RemoveSingleLineComponents();
					this.RemoveMultilineComponents();
					bool flag2 = this.verticalScrollerVisibility != ScrollerVisibility.Hidden && this.scrollView == null;
					if (flag2)
					{
						this.scrollView = new ScrollView();
						this.scrollView.Add(this.textElement);
						base.Add(this.scrollView);
						this.SetScrollViewMode();
						this.scrollView.horizontalScrollerVisibility = ScrollerVisibility.Hidden;
						this.scrollView.verticalScrollerVisibility = this.verticalScrollerVisibility;
						this.scrollView.AddToClassList(TextInputBaseField<TValueType>.TextInputBase.innerScrollviewUssClassName);
						this.scrollView.contentViewport.AddToClassList(TextInputBaseField<TValueType>.TextInputBase.innerViewportUssClassName);
						this.scrollView.contentContainer.AddToClassList(TextInputBaseField<TValueType>.TextInputBase.innerContentContainerUssClassName);
						this.scrollView.contentContainer.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.ScrollViewOnGeometryChangedEvent), TrickleDown.NoTrickleDown);
						this.scrollView.verticalScroller.slider.RegisterValueChangedCallback<float>(new EventCallback<ChangeEvent<float>>(this.MakeSureScrollViewDoesNotLeakEvents));
						this.scrollView.verticalScroller.slider.focusable = false;
						this.scrollView.horizontalScroller.slider.RegisterValueChangedCallback<float>(new EventCallback<ChangeEvent<float>>(this.MakeSureScrollViewDoesNotLeakEvents));
						this.scrollView.horizontalScroller.slider.focusable = false;
						base.AddToClassList(TextInputBaseField<TValueType>.multilineInputWithScrollViewUssClassName);
						this.textElement.AddToClassList(TextInputBaseField<TValueType>.TextInputBase.innerTextElementWithScrollViewUssClassName);
					}
					else
					{
						bool flag3 = this.multilineContainer == null;
						if (flag3)
						{
							this.textElement.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.TextElementOnGeometryChangedEvent), TrickleDown.NoTrickleDown);
							this.multilineContainer = new VisualElement
							{
								classList = { TextInputBaseField<TValueType>.multilineContainerClassName }
							};
							this.multilineContainer.Add(this.textElement);
							base.Add(this.multilineContainer);
							this.SetMultilineContainerStyle();
							base.AddToClassList(TextInputBaseField<TValueType>.multilineInputUssClassName);
							this.textElement.AddToClassList(TextInputBaseField<TValueType>.TextInputBase.innerTextElementUssClassName);
						}
					}
					bool hasFocus = this.textElement.hasFocus;
					if (hasFocus)
					{
						this.textElement.uitkTextHandle.AddTextInfoToPermanentCache();
					}
				}
			}

			// Token: 0x06000982 RID: 2434 RVA: 0x00029A3C File Offset: 0x00027C3C
			private void ScrollViewOnGeometryChangedEvent(GeometryChangedEvent e)
			{
				bool flag = e.oldRect.size == e.newRect.size;
				if (!flag)
				{
					this.UpdateScrollOffset(false);
				}
			}

			// Token: 0x06000983 RID: 2435 RVA: 0x00029A7C File Offset: 0x00027C7C
			private void TextElementOnGeometryChangedEvent(GeometryChangedEvent e)
			{
				bool flag = e.oldRect.size == e.newRect.size;
				if (!flag)
				{
					bool flag2 = Math.Abs(e.oldRect.size.x - e.newRect.size.x) > 1E-30f;
					this.UpdateScrollOffset(false, flag2);
				}
			}

			// Token: 0x06000984 RID: 2436 RVA: 0x00029AF0 File Offset: 0x00027CF0
			internal void OnInputCustomStyleResolved(CustomStyleResolvedEvent e)
			{
				ICustomStyle customStyle = e.customStyle;
				Color color;
				bool flag = customStyle.TryGetValue(TextInputBaseField<TValueType>.s_SelectionColorProperty, out color);
				if (flag)
				{
					this.textElement.selectionColor = color;
				}
				Color color2;
				bool flag2 = customStyle.TryGetValue(TextInputBaseField<TValueType>.s_CursorColorProperty, out color2);
				if (flag2)
				{
					this.textElement.cursorColor = color2;
				}
				this.SetScrollViewMode();
				this.SetMultilineContainerStyle();
			}

			// Token: 0x06000985 RID: 2437 RVA: 0x00029B54 File Offset: 0x00027D54
			private string GetDefaultValueType()
			{
				TValueType tvalueType = default(TValueType);
				string text;
				if (tvalueType != null)
				{
					tvalueType = default(TValueType);
					text = tvalueType.ToString();
				}
				else
				{
					text = "";
				}
				return text;
			}

			// Token: 0x06000986 RID: 2438 RVA: 0x00029B94 File Offset: 0x00027D94
			internal virtual bool AcceptCharacter(char c)
			{
				return !this.textEdition.isReadOnly && base.enabledInHierarchy;
			}

			// Token: 0x06000987 RID: 2439 RVA: 0x00029BBC File Offset: 0x00027DBC
			internal void UpdateScrollOffset(bool isBackspace = false)
			{
				this.UpdateScrollOffset(isBackspace, false);
			}

			// Token: 0x06000988 RID: 2440 RVA: 0x00029BC8 File Offset: 0x00027DC8
			internal void UpdateScrollOffset(bool isBackspace, bool widthChanged)
			{
				ITextSelection textSelection = this.textSelection;
				bool flag = textSelection.cursorIndex < 0 || (textSelection.cursorIndex <= 0 && textSelection.selectIndex <= 0 && this.scrollOffset == Vector2.zero);
				if (!flag)
				{
					bool flag2 = this.scrollView != null;
					if (flag2)
					{
						this.scrollOffset = this.GetScrollOffset(this.scrollView.scrollOffset.x, this.scrollView.scrollOffset.y, this.scrollView.contentViewport.layout.width, isBackspace, widthChanged);
						this.scrollView.scrollOffset = this.scrollOffset;
						this.m_ScrollViewWasClamped = this.scrollOffset.x > this.scrollView.scrollOffset.x || this.scrollOffset.y > this.scrollView.scrollOffset.y;
					}
					else
					{
						Vector3 position = this.textElement.transform.position;
						this.scrollOffset = this.GetScrollOffset(this.scrollOffset.x, this.scrollOffset.y, base.contentRect.width, isBackspace, widthChanged);
						position.y = -Mathf.Min(this.scrollOffset.y, Math.Abs(this.textElement.contentRect.height - base.contentRect.height));
						position.x = -this.scrollOffset.x;
						bool flag3 = !position.Equals(this.textElement.transform.position);
						if (flag3)
						{
							this.textElement.transform.position = position;
						}
					}
				}
			}

			// Token: 0x06000989 RID: 2441 RVA: 0x00029D94 File Offset: 0x00027F94
			private Vector2 GetScrollOffset(float xOffset, float yOffset, float contentViewportWidth, bool isBackspace, bool widthChanged)
			{
				bool flag = !this.textElement.hasFocus;
				Vector2 vector;
				if (flag)
				{
					vector = Vector2.zero;
				}
				else
				{
					Vector2 cursorPosition = this.textSelection.cursorPosition;
					float cursorWidth = this.textSelection.cursorWidth;
					float num = xOffset;
					float num2 = yOffset;
					bool flag2 = Math.Abs(this.lastCursorPos.x - cursorPosition.x) > 0.05f || this.m_ScrollViewWasClamped || widthChanged;
					if (flag2)
					{
						bool flag3 = cursorPosition.x > xOffset + contentViewportWidth - cursorWidth || (xOffset > 0f && widthChanged);
						if (flag3)
						{
							float num3 = Mathf.Ceil(cursorPosition.x + cursorWidth - contentViewportWidth);
							num = Mathf.Max(num3, 0f);
						}
						else
						{
							bool flag4 = cursorPosition.x < xOffset + 5f;
							if (flag4)
							{
								num = Mathf.Max(cursorPosition.x - 5f, 0f);
							}
						}
					}
					bool flag5 = this.textEdition.multiline && (Math.Abs(this.lastCursorPos.y - cursorPosition.y) > 0.05f || this.m_ScrollViewWasClamped);
					if (flag5)
					{
						bool flag6 = cursorPosition.y > base.contentRect.height + yOffset;
						if (flag6)
						{
							num2 = cursorPosition.y - base.contentRect.height;
						}
						else
						{
							bool flag7 = cursorPosition.y < this.textSelection.lineHeightAtCursorPosition + yOffset + 0.05f;
							if (flag7)
							{
								num2 = cursorPosition.y - this.textSelection.lineHeightAtCursorPosition;
							}
						}
					}
					this.lastCursorPos = cursorPosition;
					bool flag8 = Math.Abs(xOffset - num) > 0.05f || Math.Abs(yOffset - num2) > 0.05f;
					if (flag8)
					{
						vector = new Vector2(num, num2);
					}
					else
					{
						vector = ((this.scrollView != null) ? this.scrollView.scrollOffset : this.scrollOffset);
					}
				}
				return vector;
			}

			// Token: 0x0600098A RID: 2442 RVA: 0x00029F88 File Offset: 0x00028188
			internal void SetScrollViewMode()
			{
				bool flag = this.scrollView == null;
				if (!flag)
				{
					this.textElement.RemoveFromClassList(TextInputBaseField<TValueType>.TextInputBase.verticalVariantInnerTextElementUssClassName);
					this.textElement.RemoveFromClassList(TextInputBaseField<TValueType>.TextInputBase.verticalHorizontalVariantInnerTextElementUssClassName);
					this.textElement.RemoveFromClassList(TextInputBaseField<TValueType>.TextInputBase.horizontalVariantInnerTextElementUssClassName);
					bool flag2 = this.textEdition.multiline && (base.computedStyle.whiteSpace == WhiteSpace.Normal || base.computedStyle.whiteSpace == WhiteSpace.PreWrap);
					if (flag2)
					{
						this.textElement.AddToClassList(TextInputBaseField<TValueType>.TextInputBase.verticalVariantInnerTextElementUssClassName);
						this.scrollView.mode = ScrollViewMode.Vertical;
					}
					else
					{
						bool multiline = this.textEdition.multiline;
						if (multiline)
						{
							this.textElement.AddToClassList(TextInputBaseField<TValueType>.TextInputBase.verticalHorizontalVariantInnerTextElementUssClassName);
							this.scrollView.mode = ScrollViewMode.VerticalAndHorizontal;
						}
						else
						{
							this.textElement.AddToClassList(TextInputBaseField<TValueType>.TextInputBase.horizontalVariantInnerTextElementUssClassName);
							this.scrollView.mode = ScrollViewMode.Horizontal;
						}
					}
				}
			}

			// Token: 0x0600098B RID: 2443 RVA: 0x0002A080 File Offset: 0x00028280
			private void SetMultilineContainerStyle()
			{
				bool flag = this.multilineContainer != null;
				if (flag)
				{
					bool flag2 = base.computedStyle.whiteSpace == WhiteSpace.Normal || base.computedStyle.whiteSpace == WhiteSpace.PreWrap;
					if (flag2)
					{
						base.style.overflow = Overflow.Hidden;
					}
					else
					{
						base.style.overflow = (Overflow)2;
					}
				}
			}

			// Token: 0x0600098C RID: 2444 RVA: 0x0002A0E8 File Offset: 0x000282E8
			private void RemoveSingleLineComponents()
			{
				base.RemoveFromClassList(TextInputBaseField<TValueType>.singleLineInputUssClassName);
				this.textElement.RemoveFromClassList(TextInputBaseField<TValueType>.TextInputBase.innerTextElementUssClassName);
				this.textElement.RemoveFromHierarchy();
				this.textElement.UnregisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.TextElementOnGeometryChangedEvent), TrickleDown.NoTrickleDown);
			}

			// Token: 0x0600098D RID: 2445 RVA: 0x0002A138 File Offset: 0x00028338
			private void RemoveMultilineComponents()
			{
				bool flag = this.scrollView != null;
				if (flag)
				{
					this.scrollView.RemoveFromHierarchy();
					this.scrollView.contentContainer.UnregisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.ScrollViewOnGeometryChangedEvent), TrickleDown.NoTrickleDown);
					this.scrollView.verticalScroller.slider.UnregisterValueChangedCallback<float>(new EventCallback<ChangeEvent<float>>(this.MakeSureScrollViewDoesNotLeakEvents));
					this.scrollView.horizontalScroller.slider.UnregisterValueChangedCallback<float>(new EventCallback<ChangeEvent<float>>(this.MakeSureScrollViewDoesNotLeakEvents));
					this.scrollView = null;
					this.textElement.RemoveFromClassList(TextInputBaseField<TValueType>.TextInputBase.verticalVariantInnerTextElementUssClassName);
					this.textElement.RemoveFromClassList(TextInputBaseField<TValueType>.TextInputBase.verticalHorizontalVariantInnerTextElementUssClassName);
					this.textElement.RemoveFromClassList(TextInputBaseField<TValueType>.TextInputBase.horizontalVariantInnerTextElementUssClassName);
					base.RemoveFromClassList(TextInputBaseField<TValueType>.multilineInputWithScrollViewUssClassName);
					this.textElement.RemoveFromClassList(TextInputBaseField<TValueType>.TextInputBase.innerTextElementWithScrollViewUssClassName);
				}
				bool flag2 = this.multilineContainer != null;
				if (flag2)
				{
					this.textElement.transform.position = Vector3.zero;
					this.multilineContainer.RemoveFromHierarchy();
					this.textElement.UnregisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.TextElementOnGeometryChangedEvent), TrickleDown.NoTrickleDown);
					this.multilineContainer = null;
					base.RemoveFromClassList(TextInputBaseField<TValueType>.multilineInputUssClassName);
				}
			}

			// Token: 0x0600098E RID: 2446 RVA: 0x0002A27C File Offset: 0x0002847C
			internal bool SetVerticalScrollerVisibility(ScrollerVisibility sv)
			{
				bool multiline = this.textEdition.multiline;
				bool flag2;
				if (multiline)
				{
					this.verticalScrollerVisibility = sv;
					bool flag = this.scrollView == null;
					if (flag)
					{
						this.SetMultiline();
					}
					else
					{
						this.scrollView.verticalScrollerVisibility = this.verticalScrollerVisibility;
					}
					flag2 = true;
				}
				else
				{
					flag2 = false;
				}
				return flag2;
			}

			// Token: 0x0600098F RID: 2447 RVA: 0x0002A2D2 File Offset: 0x000284D2
			[Obsolete("SelectAll() is deprecated. Use textSelection.SelectAll() instead.")]
			public void SelectAll()
			{
				this.textSelection.SelectAll();
			}

			// Token: 0x17000183 RID: 387
			// (get) Token: 0x06000990 RID: 2448 RVA: 0x0002A2E1 File Offset: 0x000284E1
			// (set) Token: 0x06000991 RID: 2449 RVA: 0x0002A2EE File Offset: 0x000284EE
			[Obsolete("isReadOnly is deprecated. Use textEdition.isReadOnly instead.")]
			public bool isReadOnly
			{
				get
				{
					return this.textEdition.isReadOnly;
				}
				set
				{
					this.textEdition.isReadOnly = value;
				}
			}

			// Token: 0x17000184 RID: 388
			// (get) Token: 0x06000992 RID: 2450 RVA: 0x0002A2FD File Offset: 0x000284FD
			// (set) Token: 0x06000993 RID: 2451 RVA: 0x0002A30A File Offset: 0x0002850A
			[Obsolete("maxLength is deprecated. Use textEdition.maxLength instead.")]
			public int maxLength
			{
				get
				{
					return this.textEdition.maxLength;
				}
				set
				{
					this.textEdition.maxLength = value;
				}
			}

			// Token: 0x17000185 RID: 389
			// (get) Token: 0x06000994 RID: 2452 RVA: 0x0002A319 File Offset: 0x00028519
			// (set) Token: 0x06000995 RID: 2453 RVA: 0x0002A326 File Offset: 0x00028526
			[Obsolete("maskChar is deprecated. Use textEdition.maskChar instead.")]
			public char maskChar
			{
				get
				{
					return this.textEdition.maskChar;
				}
				set
				{
					this.textEdition.maskChar = value;
				}
			}

			// Token: 0x17000186 RID: 390
			// (get) Token: 0x06000996 RID: 2454 RVA: 0x0002A335 File Offset: 0x00028535
			// (set) Token: 0x06000997 RID: 2455 RVA: 0x0002A342 File Offset: 0x00028542
			[Obsolete("isPasswordField is deprecated. Use textEdition.isPassword instead.")]
			public virtual bool isPasswordField
			{
				get
				{
					return this.textEdition.isPassword;
				}
				set
				{
					this.textEdition.isPassword = value;
				}
			}

			// Token: 0x17000187 RID: 391
			// (get) Token: 0x06000998 RID: 2456 RVA: 0x0002A351 File Offset: 0x00028551
			// (set) Token: 0x06000999 RID: 2457 RVA: 0x0002A35E File Offset: 0x0002855E
			[Obsolete("selectionColor is deprecated. Use textSelection.selectionColor instead.")]
			public Color selectionColor
			{
				get
				{
					return this.textSelection.selectionColor;
				}
				set
				{
					this.textSelection.selectionColor = value;
				}
			}

			// Token: 0x17000188 RID: 392
			// (get) Token: 0x0600099A RID: 2458 RVA: 0x0002A36D File Offset: 0x0002856D
			// (set) Token: 0x0600099B RID: 2459 RVA: 0x0002A37A File Offset: 0x0002857A
			[Obsolete("cursorColor is deprecated. Use textSelection.cursorColor instead.")]
			public Color cursorColor
			{
				get
				{
					return this.textSelection.cursorColor;
				}
				set
				{
					this.textSelection.cursorColor = value;
				}
			}

			// Token: 0x17000189 RID: 393
			// (get) Token: 0x0600099C RID: 2460 RVA: 0x0002A389 File Offset: 0x00028589
			[Obsolete("cursorIndex is deprecated. Use textSelection.cursorIndex instead.")]
			public int cursorIndex
			{
				get
				{
					return this.textSelection.cursorIndex;
				}
			}

			// Token: 0x1700018A RID: 394
			// (get) Token: 0x0600099D RID: 2461 RVA: 0x0002A396 File Offset: 0x00028596
			[Obsolete("selectIndex is deprecated. Use textSelection.selectIndex instead.")]
			public int selectIndex
			{
				get
				{
					return this.textSelection.selectIndex;
				}
			}

			// Token: 0x1700018B RID: 395
			// (get) Token: 0x0600099E RID: 2462 RVA: 0x0002A3A3 File Offset: 0x000285A3
			// (set) Token: 0x0600099F RID: 2463 RVA: 0x0002A3B0 File Offset: 0x000285B0
			[Obsolete("doubleClickSelectsWord is deprecated. Use textSelection.doubleClickSelectsWord instead.")]
			public bool doubleClickSelectsWord
			{
				get
				{
					return this.textSelection.doubleClickSelectsWord;
				}
				set
				{
					this.textSelection.doubleClickSelectsWord = value;
				}
			}

			// Token: 0x1700018C RID: 396
			// (get) Token: 0x060009A0 RID: 2464 RVA: 0x0002A3BF File Offset: 0x000285BF
			// (set) Token: 0x060009A1 RID: 2465 RVA: 0x0002A3CC File Offset: 0x000285CC
			[Obsolete("tripleClickSelectsLine is deprecated. Use textSelection.tripleClickSelectsLine instead.")]
			public bool tripleClickSelectsLine
			{
				get
				{
					return this.textSelection.tripleClickSelectsLine;
				}
				set
				{
					this.textSelection.tripleClickSelectsLine = value;
				}
			}

			// Token: 0x04000578 RID: 1400
			internal ScrollView scrollView;

			// Token: 0x04000579 RID: 1401
			internal VisualElement multilineContainer;

			// Token: 0x0400057A RID: 1402
			public static readonly string innerComponentsModifierName = "--inner-input-field-component";

			// Token: 0x0400057B RID: 1403
			public static readonly string innerTextElementUssClassName = TextElement.ussClassName + TextInputBaseField<TValueType>.TextInputBase.innerComponentsModifierName;

			// Token: 0x0400057C RID: 1404
			internal static readonly string innerTextElementWithScrollViewUssClassName = TextElement.ussClassName + TextInputBaseField<TValueType>.TextInputBase.innerComponentsModifierName + "--scroll-view";

			// Token: 0x0400057D RID: 1405
			public static readonly string horizontalVariantInnerTextElementUssClassName = TextElement.ussClassName + TextInputBaseField<TValueType>.TextInputBase.innerComponentsModifierName + "--horizontal";

			// Token: 0x0400057E RID: 1406
			public static readonly string verticalVariantInnerTextElementUssClassName = TextElement.ussClassName + TextInputBaseField<TValueType>.TextInputBase.innerComponentsModifierName + "--vertical";

			// Token: 0x0400057F RID: 1407
			public static readonly string verticalHorizontalVariantInnerTextElementUssClassName = TextElement.ussClassName + TextInputBaseField<TValueType>.TextInputBase.innerComponentsModifierName + "--vertical-horizontal";

			// Token: 0x04000580 RID: 1408
			public static readonly string innerScrollviewUssClassName = ScrollView.ussClassName + TextInputBaseField<TValueType>.TextInputBase.innerComponentsModifierName;

			// Token: 0x04000581 RID: 1409
			public static readonly string innerViewportUssClassName = ScrollView.viewportUssClassName + TextInputBaseField<TValueType>.TextInputBase.innerComponentsModifierName;

			// Token: 0x04000582 RID: 1410
			public static readonly string innerContentContainerUssClassName = ScrollView.contentUssClassName + TextInputBaseField<TValueType>.TextInputBase.innerComponentsModifierName;

			// Token: 0x04000584 RID: 1412
			internal Vector2 scrollOffset = Vector2.zero;

			// Token: 0x04000585 RID: 1413
			private bool m_ScrollViewWasClamped;

			// Token: 0x04000586 RID: 1414
			private Vector2 lastCursorPos = Vector2.zero;

			// Token: 0x04000587 RID: 1415
			internal ScrollerVisibility verticalScrollerVisibility = ScrollerVisibility.Hidden;
		}
	}
}
