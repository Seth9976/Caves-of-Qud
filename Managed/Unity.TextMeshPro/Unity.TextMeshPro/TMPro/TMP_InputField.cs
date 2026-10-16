using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TMPro
{
	// Token: 0x02000036 RID: 54
	[AddComponentMenu("UI/TextMeshPro - Input Field", 11)]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.ugui@2.0/manual/TextMeshPro/index.html")]
	public class TMP_InputField : Selectable, IUpdateSelectedHandler, IEventSystemHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler, ISubmitHandler, ICancelHandler, ICanvasElement, ILayoutElement, IScrollHandler
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00009C47 File Offset: 0x00007E47
		private BaseInput inputSystem
		{
			get
			{
				if (EventSystem.current && EventSystem.current.currentInputModule)
				{
					return EventSystem.current.currentInputModule.input;
				}
				return null;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00009C77 File Offset: 0x00007E77
		private string compositionString
		{
			get
			{
				if (!(this.inputSystem != null))
				{
					return Input.compositionString;
				}
				return this.inputSystem.compositionString;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00009C98 File Offset: 0x00007E98
		private int compositionLength
		{
			get
			{
				if (this.m_ReadOnly)
				{
					return 0;
				}
				return this.compositionString.Length;
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00009CB0 File Offset: 0x00007EB0
		protected TMP_InputField()
		{
			this.SetTextComponentWrapMode();
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00009DE9 File Offset: 0x00007FE9
		protected Mesh mesh
		{
			get
			{
				if (this.m_Mesh == null)
				{
					this.m_Mesh = new Mesh();
				}
				return this.m_Mesh;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00009E13 File Offset: 0x00008013
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x00009E0A File Offset: 0x0000800A
		public virtual bool shouldActivateOnSelect
		{
			get
			{
				return this.m_ShouldActivateOnSelect && Application.platform != RuntimePlatform.tvOS;
			}
			set
			{
				this.m_ShouldActivateOnSelect = value;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00009E2C File Offset: 0x0000802C
		// (set) Token: 0x060001CA RID: 458 RVA: 0x00009E64 File Offset: 0x00008064
		public bool shouldHideMobileInput
		{
			get
			{
				RuntimePlatform platform = Application.platform;
				if (platform <= RuntimePlatform.Android)
				{
					if (platform != RuntimePlatform.IPhonePlayer && platform != RuntimePlatform.Android)
					{
						return true;
					}
				}
				else if (platform != RuntimePlatform.WebGLPlayer && platform != RuntimePlatform.tvOS)
				{
					return true;
				}
				return this.m_HideMobileInput;
			}
			set
			{
				RuntimePlatform platform = Application.platform;
				if (platform <= RuntimePlatform.Android)
				{
					if (platform != RuntimePlatform.IPhonePlayer && platform != RuntimePlatform.Android)
					{
						goto IL_002E;
					}
				}
				else if (platform != RuntimePlatform.WebGLPlayer && platform != RuntimePlatform.tvOS)
				{
					goto IL_002E;
				}
				SetPropertyUtility.SetStruct<bool>(ref this.m_HideMobileInput, value);
				return;
				IL_002E:
				this.m_HideMobileInput = true;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00009EA8 File Offset: 0x000080A8
		// (set) Token: 0x060001CC RID: 460 RVA: 0x00009EFC File Offset: 0x000080FC
		public bool shouldHideSoftKeyboard
		{
			get
			{
				RuntimePlatform platform = Application.platform;
				if (platform <= RuntimePlatform.MetroPlayerARM)
				{
					if (platform != RuntimePlatform.IPhonePlayer && platform != RuntimePlatform.Android && platform - RuntimePlatform.WebGLPlayer > 3)
					{
						return true;
					}
				}
				else if (platform <= RuntimePlatform.Switch)
				{
					if (platform != RuntimePlatform.PS4 && platform - RuntimePlatform.tvOS > 1)
					{
						return true;
					}
				}
				else if (platform - RuntimePlatform.GameCoreXboxSeries > 2 && platform != RuntimePlatform.VisionOS)
				{
					return true;
				}
				return this.m_HideSoftKeyboard;
			}
			set
			{
				RuntimePlatform platform = Application.platform;
				if (platform <= RuntimePlatform.MetroPlayerARM)
				{
					if (platform != RuntimePlatform.IPhonePlayer && platform != RuntimePlatform.Android && platform - RuntimePlatform.WebGLPlayer > 3)
					{
						goto IL_004B;
					}
				}
				else if (platform <= RuntimePlatform.Switch)
				{
					if (platform != RuntimePlatform.PS4 && platform - RuntimePlatform.tvOS > 1)
					{
						goto IL_004B;
					}
				}
				else if (platform - RuntimePlatform.GameCoreXboxSeries > 2 && platform != RuntimePlatform.VisionOS)
				{
					goto IL_004B;
				}
				SetPropertyUtility.SetStruct<bool>(ref this.m_HideSoftKeyboard, value);
				goto IL_0052;
				IL_004B:
				this.m_HideSoftKeyboard = true;
				IL_0052:
				if (this.m_HideSoftKeyboard && this.m_SoftKeyboard != null && TouchScreenKeyboard.isSupported && this.m_SoftKeyboard.active)
				{
					this.m_SoftKeyboard.active = false;
					this.m_SoftKeyboard = null;
				}
			}
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00009F94 File Offset: 0x00008194
		private bool isKeyboardUsingEvents()
		{
			RuntimePlatform platform = Application.platform;
			if (platform > RuntimePlatform.WebGLPlayer)
			{
				if (platform != RuntimePlatform.PS4)
				{
					switch (platform)
					{
					case RuntimePlatform.tvOS:
						goto IL_0061;
					case RuntimePlatform.Switch:
					case RuntimePlatform.GameCoreXboxSeries:
					case RuntimePlatform.GameCoreXboxOne:
					case RuntimePlatform.PS5:
						break;
					case RuntimePlatform.Lumin:
					case RuntimePlatform.Stadia:
					case RuntimePlatform.LinuxHeadlessSimulation:
						return true;
					default:
						if (platform != RuntimePlatform.VisionOS)
						{
							return true;
						}
						goto IL_0061;
					}
				}
				return false;
			}
			if (platform != RuntimePlatform.IPhonePlayer)
			{
				if (platform == RuntimePlatform.Android)
				{
					return this.InPlaceEditing() && this.m_HideSoftKeyboard;
				}
				if (platform != RuntimePlatform.WebGLPlayer)
				{
					return true;
				}
				return this.m_SoftKeyboard == null || !this.m_SoftKeyboard.active;
			}
			IL_0061:
			return this.m_HideSoftKeyboard;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000A025 File Offset: 0x00008225
		private bool isUWP()
		{
			return Application.platform == RuntimePlatform.MetroPlayerX86 || Application.platform == RuntimePlatform.MetroPlayerX64 || Application.platform == RuntimePlatform.MetroPlayerARM;
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001CF RID: 463 RVA: 0x0000A044 File Offset: 0x00008244
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x0000A04C File Offset: 0x0000824C
		public string text
		{
			get
			{
				return this.m_Text;
			}
			set
			{
				this.SetText(value, true);
			}
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000A056 File Offset: 0x00008256
		public void SetTextWithoutNotify(string input)
		{
			this.SetText(input, false);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000A060 File Offset: 0x00008260
		private void SetText(string value, bool sendCallback = true)
		{
			if (this.text == value)
			{
				return;
			}
			if (value == null)
			{
				value = "";
			}
			value = value.Replace("\0", string.Empty);
			this.m_Text = value;
			if (this.m_SoftKeyboard != null)
			{
				this.m_SoftKeyboard.text = this.m_Text;
			}
			if (this.m_StringPosition > this.m_Text.Length)
			{
				this.m_StringPosition = (this.m_StringSelectPosition = this.m_Text.Length);
			}
			else if (this.m_StringSelectPosition > this.m_Text.Length)
			{
				this.m_StringSelectPosition = this.m_Text.Length;
			}
			this.m_forceRectTransformAdjustment = true;
			this.m_IsTextComponentUpdateRequired = true;
			this.UpdateLabel();
			if (sendCallback)
			{
				this.SendOnValueChanged();
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000A128 File Offset: 0x00008328
		public bool isFocused
		{
			get
			{
				return this.m_AllowInput;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x0000A130 File Offset: 0x00008330
		// (set) Token: 0x060001D5 RID: 469 RVA: 0x0000A138 File Offset: 0x00008338
		public float caretBlinkRate
		{
			get
			{
				return this.m_CaretBlinkRate;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_CaretBlinkRate, value) && this.m_AllowInput)
				{
					this.SetCaretActive();
				}
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x0000A156 File Offset: 0x00008356
		// (set) Token: 0x060001D7 RID: 471 RVA: 0x0000A15E File Offset: 0x0000835E
		public int caretWidth
		{
			get
			{
				return this.m_CaretWidth;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<int>(ref this.m_CaretWidth, value))
				{
					this.MarkGeometryAsDirty();
				}
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x0000A174 File Offset: 0x00008374
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x0000A17C File Offset: 0x0000837C
		public RectTransform textViewport
		{
			get
			{
				return this.m_TextViewport;
			}
			set
			{
				SetPropertyUtility.SetClass<RectTransform>(ref this.m_TextViewport, value);
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001DA RID: 474 RVA: 0x0000A18B File Offset: 0x0000838B
		// (set) Token: 0x060001DB RID: 475 RVA: 0x0000A193 File Offset: 0x00008393
		public TMP_Text textComponent
		{
			get
			{
				return this.m_TextComponent;
			}
			set
			{
				if (SetPropertyUtility.SetClass<TMP_Text>(ref this.m_TextComponent, value))
				{
					this.SetTextComponentWrapMode();
				}
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001DC RID: 476 RVA: 0x0000A1A9 File Offset: 0x000083A9
		// (set) Token: 0x060001DD RID: 477 RVA: 0x0000A1B1 File Offset: 0x000083B1
		public Graphic placeholder
		{
			get
			{
				return this.m_Placeholder;
			}
			set
			{
				SetPropertyUtility.SetClass<Graphic>(ref this.m_Placeholder, value);
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001DE RID: 478 RVA: 0x0000A1C0 File Offset: 0x000083C0
		// (set) Token: 0x060001DF RID: 479 RVA: 0x0000A1C8 File Offset: 0x000083C8
		public Scrollbar verticalScrollbar
		{
			get
			{
				return this.m_VerticalScrollbar;
			}
			set
			{
				if (this.m_VerticalScrollbar != null)
				{
					this.m_VerticalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.OnScrollbarValueChange));
				}
				SetPropertyUtility.SetClass<Scrollbar>(ref this.m_VerticalScrollbar, value);
				if (this.m_VerticalScrollbar)
				{
					this.m_VerticalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.OnScrollbarValueChange));
				}
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x0000A235 File Offset: 0x00008435
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x0000A23D File Offset: 0x0000843D
		public float scrollSensitivity
		{
			get
			{
				return this.m_ScrollSensitivity;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_ScrollSensitivity, value))
				{
					this.MarkGeometryAsDirty();
				}
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000A253 File Offset: 0x00008453
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x0000A26F File Offset: 0x0000846F
		public Color caretColor
		{
			get
			{
				if (!this.customCaretColor)
				{
					return this.textComponent.color;
				}
				return this.m_CaretColor;
			}
			set
			{
				if (SetPropertyUtility.SetColor(ref this.m_CaretColor, value))
				{
					this.MarkGeometryAsDirty();
				}
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x0000A285 File Offset: 0x00008485
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x0000A28D File Offset: 0x0000848D
		public bool customCaretColor
		{
			get
			{
				return this.m_CustomCaretColor;
			}
			set
			{
				if (this.m_CustomCaretColor != value)
				{
					this.m_CustomCaretColor = value;
					this.MarkGeometryAsDirty();
				}
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x0000A2A5 File Offset: 0x000084A5
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x0000A2AD File Offset: 0x000084AD
		public Color selectionColor
		{
			get
			{
				return this.m_SelectionColor;
			}
			set
			{
				if (SetPropertyUtility.SetColor(ref this.m_SelectionColor, value))
				{
					this.MarkGeometryAsDirty();
				}
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000A2C3 File Offset: 0x000084C3
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x0000A2CB File Offset: 0x000084CB
		public TMP_InputField.SubmitEvent onEndEdit
		{
			get
			{
				return this.m_OnEndEdit;
			}
			set
			{
				SetPropertyUtility.SetClass<TMP_InputField.SubmitEvent>(ref this.m_OnEndEdit, value);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000A2DA File Offset: 0x000084DA
		// (set) Token: 0x060001EB RID: 491 RVA: 0x0000A2E2 File Offset: 0x000084E2
		public TMP_InputField.SubmitEvent onSubmit
		{
			get
			{
				return this.m_OnSubmit;
			}
			set
			{
				SetPropertyUtility.SetClass<TMP_InputField.SubmitEvent>(ref this.m_OnSubmit, value);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001EC RID: 492 RVA: 0x0000A2F1 File Offset: 0x000084F1
		// (set) Token: 0x060001ED RID: 493 RVA: 0x0000A2F9 File Offset: 0x000084F9
		public TMP_InputField.SelectionEvent onSelect
		{
			get
			{
				return this.m_OnSelect;
			}
			set
			{
				SetPropertyUtility.SetClass<TMP_InputField.SelectionEvent>(ref this.m_OnSelect, value);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0000A308 File Offset: 0x00008508
		// (set) Token: 0x060001EF RID: 495 RVA: 0x0000A310 File Offset: 0x00008510
		public TMP_InputField.SelectionEvent onDeselect
		{
			get
			{
				return this.m_OnDeselect;
			}
			set
			{
				SetPropertyUtility.SetClass<TMP_InputField.SelectionEvent>(ref this.m_OnDeselect, value);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0000A31F File Offset: 0x0000851F
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x0000A327 File Offset: 0x00008527
		public TMP_InputField.TextSelectionEvent onTextSelection
		{
			get
			{
				return this.m_OnTextSelection;
			}
			set
			{
				SetPropertyUtility.SetClass<TMP_InputField.TextSelectionEvent>(ref this.m_OnTextSelection, value);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000A336 File Offset: 0x00008536
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x0000A33E File Offset: 0x0000853E
		public TMP_InputField.TextSelectionEvent onEndTextSelection
		{
			get
			{
				return this.m_OnEndTextSelection;
			}
			set
			{
				SetPropertyUtility.SetClass<TMP_InputField.TextSelectionEvent>(ref this.m_OnEndTextSelection, value);
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000A34D File Offset: 0x0000854D
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x0000A355 File Offset: 0x00008555
		public TMP_InputField.OnChangeEvent onValueChanged
		{
			get
			{
				return this.m_OnValueChanged;
			}
			set
			{
				SetPropertyUtility.SetClass<TMP_InputField.OnChangeEvent>(ref this.m_OnValueChanged, value);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000A364 File Offset: 0x00008564
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x0000A36C File Offset: 0x0000856C
		public TMP_InputField.TouchScreenKeyboardEvent onTouchScreenKeyboardStatusChanged
		{
			get
			{
				return this.m_OnTouchScreenKeyboardStatusChanged;
			}
			set
			{
				SetPropertyUtility.SetClass<TMP_InputField.TouchScreenKeyboardEvent>(ref this.m_OnTouchScreenKeyboardStatusChanged, value);
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x0000A37B File Offset: 0x0000857B
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x0000A383 File Offset: 0x00008583
		public TMP_InputField.OnValidateInput onValidateInput
		{
			get
			{
				return this.m_OnValidateInput;
			}
			set
			{
				SetPropertyUtility.SetClass<TMP_InputField.OnValidateInput>(ref this.m_OnValidateInput, value);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001FA RID: 506 RVA: 0x0000A392 File Offset: 0x00008592
		// (set) Token: 0x060001FB RID: 507 RVA: 0x0000A39A File Offset: 0x0000859A
		public int characterLimit
		{
			get
			{
				return this.m_CharacterLimit;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<int>(ref this.m_CharacterLimit, Math.Max(0, value)))
				{
					this.UpdateLabel();
					if (this.m_SoftKeyboard != null)
					{
						this.m_SoftKeyboard.characterLimit = value;
					}
				}
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001FC RID: 508 RVA: 0x0000A3CA File Offset: 0x000085CA
		// (set) Token: 0x060001FD RID: 509 RVA: 0x0000A3D2 File Offset: 0x000085D2
		public float pointSize
		{
			get
			{
				return this.m_GlobalPointSize;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_GlobalPointSize, Math.Max(0f, value)))
				{
					this.SetGlobalPointSize(this.m_GlobalPointSize);
					this.UpdateLabel();
				}
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001FE RID: 510 RVA: 0x0000A3FE File Offset: 0x000085FE
		// (set) Token: 0x060001FF RID: 511 RVA: 0x0000A406 File Offset: 0x00008606
		public TMP_FontAsset fontAsset
		{
			get
			{
				return this.m_GlobalFontAsset;
			}
			set
			{
				if (SetPropertyUtility.SetClass<TMP_FontAsset>(ref this.m_GlobalFontAsset, value))
				{
					this.SetGlobalFontAsset(this.m_GlobalFontAsset);
					this.UpdateLabel();
				}
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000200 RID: 512 RVA: 0x0000A428 File Offset: 0x00008628
		// (set) Token: 0x06000201 RID: 513 RVA: 0x0000A430 File Offset: 0x00008630
		public bool onFocusSelectAll
		{
			get
			{
				return this.m_OnFocusSelectAll;
			}
			set
			{
				this.m_OnFocusSelectAll = value;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000202 RID: 514 RVA: 0x0000A439 File Offset: 0x00008639
		// (set) Token: 0x06000203 RID: 515 RVA: 0x0000A441 File Offset: 0x00008641
		public bool resetOnDeActivation
		{
			get
			{
				return this.m_ResetOnDeActivation;
			}
			set
			{
				this.m_ResetOnDeActivation = value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000204 RID: 516 RVA: 0x0000A44A File Offset: 0x0000864A
		// (set) Token: 0x06000205 RID: 517 RVA: 0x0000A452 File Offset: 0x00008652
		public bool keepTextSelectionVisible
		{
			get
			{
				return this.m_KeepTextSelectionVisible;
			}
			set
			{
				this.m_KeepTextSelectionVisible = value;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000206 RID: 518 RVA: 0x0000A45B File Offset: 0x0000865B
		// (set) Token: 0x06000207 RID: 519 RVA: 0x0000A463 File Offset: 0x00008663
		public bool restoreOriginalTextOnEscape
		{
			get
			{
				return this.m_RestoreOriginalTextOnEscape;
			}
			set
			{
				this.m_RestoreOriginalTextOnEscape = value;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000208 RID: 520 RVA: 0x0000A46C File Offset: 0x0000866C
		// (set) Token: 0x06000209 RID: 521 RVA: 0x0000A474 File Offset: 0x00008674
		public bool isRichTextEditingAllowed
		{
			get
			{
				return this.m_isRichTextEditingAllowed;
			}
			set
			{
				this.m_isRichTextEditingAllowed = value;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600020A RID: 522 RVA: 0x0000A47D File Offset: 0x0000867D
		// (set) Token: 0x0600020B RID: 523 RVA: 0x0000A485 File Offset: 0x00008685
		public TMP_InputField.ContentType contentType
		{
			get
			{
				return this.m_ContentType;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<TMP_InputField.ContentType>(ref this.m_ContentType, value))
				{
					this.EnforceContentType();
				}
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600020C RID: 524 RVA: 0x0000A49B File Offset: 0x0000869B
		// (set) Token: 0x0600020D RID: 525 RVA: 0x0000A4A3 File Offset: 0x000086A3
		public TMP_InputField.LineType lineType
		{
			get
			{
				return this.m_LineType;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<TMP_InputField.LineType>(ref this.m_LineType, value))
				{
					this.SetToCustomIfContentTypeIsNot(new TMP_InputField.ContentType[]
					{
						TMP_InputField.ContentType.Standard,
						TMP_InputField.ContentType.Autocorrected
					});
					this.SetTextComponentWrapMode();
				}
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600020E RID: 526 RVA: 0x0000A4C9 File Offset: 0x000086C9
		// (set) Token: 0x0600020F RID: 527 RVA: 0x0000A4D1 File Offset: 0x000086D1
		public int lineLimit
		{
			get
			{
				return this.m_LineLimit;
			}
			set
			{
				if (this.m_LineType == TMP_InputField.LineType.SingleLine)
				{
					this.m_LineLimit = 1;
					return;
				}
				SetPropertyUtility.SetStruct<int>(ref this.m_LineLimit, value);
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000A4F0 File Offset: 0x000086F0
		// (set) Token: 0x06000211 RID: 529 RVA: 0x0000A4F8 File Offset: 0x000086F8
		public TMP_InputField.InputType inputType
		{
			get
			{
				return this.m_InputType;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<TMP_InputField.InputType>(ref this.m_InputType, value))
				{
					this.SetToCustom();
				}
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000A50E File Offset: 0x0000870E
		public TouchScreenKeyboard touchScreenKeyboard
		{
			get
			{
				return this.m_SoftKeyboard;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000A516 File Offset: 0x00008716
		// (set) Token: 0x06000214 RID: 532 RVA: 0x0000A51E File Offset: 0x0000871E
		public TouchScreenKeyboardType keyboardType
		{
			get
			{
				return this.m_KeyboardType;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<TouchScreenKeyboardType>(ref this.m_KeyboardType, value))
				{
					this.SetToCustom();
				}
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000A534 File Offset: 0x00008734
		// (set) Token: 0x06000216 RID: 534 RVA: 0x0000A53C File Offset: 0x0000873C
		public TMP_InputField.CharacterValidation characterValidation
		{
			get
			{
				return this.m_CharacterValidation;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<TMP_InputField.CharacterValidation>(ref this.m_CharacterValidation, value))
				{
					this.SetToCustom();
				}
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000217 RID: 535 RVA: 0x0000A552 File Offset: 0x00008752
		// (set) Token: 0x06000218 RID: 536 RVA: 0x0000A55A File Offset: 0x0000875A
		public TMP_InputValidator inputValidator
		{
			get
			{
				return this.m_InputValidator;
			}
			set
			{
				if (SetPropertyUtility.SetClass<TMP_InputValidator>(ref this.m_InputValidator, value))
				{
					this.SetToCustom(TMP_InputField.CharacterValidation.CustomValidator);
				}
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000219 RID: 537 RVA: 0x0000A571 File Offset: 0x00008771
		// (set) Token: 0x0600021A RID: 538 RVA: 0x0000A579 File Offset: 0x00008779
		public bool readOnly
		{
			get
			{
				return this.m_ReadOnly;
			}
			set
			{
				this.m_ReadOnly = value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600021B RID: 539 RVA: 0x0000A582 File Offset: 0x00008782
		// (set) Token: 0x0600021C RID: 540 RVA: 0x0000A58A File Offset: 0x0000878A
		public bool richText
		{
			get
			{
				return this.m_RichText;
			}
			set
			{
				this.m_RichText = value;
				this.SetTextComponentRichTextMode();
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000A599 File Offset: 0x00008799
		public bool multiLine
		{
			get
			{
				return this.m_LineType == TMP_InputField.LineType.MultiLineNewline || this.lineType == TMP_InputField.LineType.MultiLineSubmit;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000A5AF File Offset: 0x000087AF
		// (set) Token: 0x0600021F RID: 543 RVA: 0x0000A5B7 File Offset: 0x000087B7
		public char asteriskChar
		{
			get
			{
				return this.m_AsteriskChar;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<char>(ref this.m_AsteriskChar, value))
				{
					this.UpdateLabel();
				}
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000220 RID: 544 RVA: 0x0000A5CD File Offset: 0x000087CD
		public bool wasCanceled
		{
			get
			{
				return this.m_WasCanceled;
			}
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000A5D5 File Offset: 0x000087D5
		protected void ClampStringPos(ref int pos)
		{
			if (pos <= 0)
			{
				pos = 0;
				return;
			}
			if (pos > this.text.Length)
			{
				pos = this.text.Length;
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000A5FC File Offset: 0x000087FC
		protected void ClampCaretPos(ref int pos)
		{
			if (pos > this.m_TextComponent.textInfo.characterCount - 1)
			{
				pos = this.m_TextComponent.textInfo.characterCount - 1;
			}
			if (pos <= 0)
			{
				pos = 0;
			}
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000A630 File Offset: 0x00008830
		private int ClampArrayIndex(int index)
		{
			if (index < 0)
			{
				return 0;
			}
			return index;
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000A639 File Offset: 0x00008839
		// (set) Token: 0x06000225 RID: 549 RVA: 0x0000A648 File Offset: 0x00008848
		protected int caretPositionInternal
		{
			get
			{
				return this.m_CaretPosition + this.compositionLength;
			}
			set
			{
				this.m_CaretPosition = value;
				this.ClampCaretPos(ref this.m_CaretPosition);
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000A65D File Offset: 0x0000885D
		// (set) Token: 0x06000227 RID: 551 RVA: 0x0000A66C File Offset: 0x0000886C
		protected int stringPositionInternal
		{
			get
			{
				return this.m_StringPosition + this.compositionLength;
			}
			set
			{
				this.m_StringPosition = value;
				this.ClampStringPos(ref this.m_StringPosition);
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000A681 File Offset: 0x00008881
		// (set) Token: 0x06000229 RID: 553 RVA: 0x0000A690 File Offset: 0x00008890
		protected int caretSelectPositionInternal
		{
			get
			{
				return this.m_CaretSelectPosition + this.compositionLength;
			}
			set
			{
				this.m_CaretSelectPosition = value;
				this.ClampCaretPos(ref this.m_CaretSelectPosition);
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000A6A5 File Offset: 0x000088A5
		// (set) Token: 0x0600022B RID: 555 RVA: 0x0000A6B4 File Offset: 0x000088B4
		protected int stringSelectPositionInternal
		{
			get
			{
				return this.m_StringSelectPosition + this.compositionLength;
			}
			set
			{
				this.m_StringSelectPosition = value;
				this.ClampStringPos(ref this.m_StringSelectPosition);
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000A6C9 File Offset: 0x000088C9
		private bool hasSelection
		{
			get
			{
				return this.stringPositionInternal != this.stringSelectPositionInternal;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000A6DC File Offset: 0x000088DC
		// (set) Token: 0x0600022E RID: 558 RVA: 0x0000A6E4 File Offset: 0x000088E4
		public int caretPosition
		{
			get
			{
				return this.caretSelectPositionInternal;
			}
			set
			{
				this.selectionAnchorPosition = value;
				this.selectionFocusPosition = value;
				this.UpdateStringIndexFromCaretPosition();
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000A6FA File Offset: 0x000088FA
		// (set) Token: 0x06000230 RID: 560 RVA: 0x0000A702 File Offset: 0x00008902
		public int selectionAnchorPosition
		{
			get
			{
				return this.caretPositionInternal;
			}
			set
			{
				if (this.compositionLength != 0)
				{
					return;
				}
				this.caretPositionInternal = value;
				this.m_IsStringPositionDirty = true;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0000A71B File Offset: 0x0000891B
		// (set) Token: 0x06000232 RID: 562 RVA: 0x0000A723 File Offset: 0x00008923
		public int selectionFocusPosition
		{
			get
			{
				return this.caretSelectPositionInternal;
			}
			set
			{
				if (this.compositionLength != 0)
				{
					return;
				}
				this.caretSelectPositionInternal = value;
				this.m_IsStringPositionDirty = true;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000233 RID: 563 RVA: 0x0000A73C File Offset: 0x0000893C
		// (set) Token: 0x06000234 RID: 564 RVA: 0x0000A744 File Offset: 0x00008944
		public int stringPosition
		{
			get
			{
				return this.stringSelectPositionInternal;
			}
			set
			{
				this.selectionStringAnchorPosition = value;
				this.selectionStringFocusPosition = value;
				this.UpdateCaretPositionFromStringIndex();
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000235 RID: 565 RVA: 0x0000A75A File Offset: 0x0000895A
		// (set) Token: 0x06000236 RID: 566 RVA: 0x0000A762 File Offset: 0x00008962
		public int selectionStringAnchorPosition
		{
			get
			{
				return this.stringPositionInternal;
			}
			set
			{
				if (this.compositionLength != 0)
				{
					return;
				}
				this.stringPositionInternal = value;
				this.m_IsCaretPositionDirty = true;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0000A77B File Offset: 0x0000897B
		// (set) Token: 0x06000238 RID: 568 RVA: 0x0000A783 File Offset: 0x00008983
		public int selectionStringFocusPosition
		{
			get
			{
				return this.stringSelectPositionInternal;
			}
			set
			{
				if (this.compositionLength != 0)
				{
					return;
				}
				this.stringSelectPositionInternal = value;
				this.m_IsCaretPositionDirty = true;
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000A79C File Offset: 0x0000899C
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.m_Text == null)
			{
				this.m_Text = string.Empty;
			}
			this.m_IsApplePlatform = SystemInfo.operatingSystemFamily == OperatingSystemFamily.MacOSX || SystemInfo.operatingSystem.Contains("iOS") || SystemInfo.operatingSystem.Contains("tvOS");
			if (base.GetComponent<ILayoutController>() != null)
			{
				this.m_IsDrivenByLayoutComponents = true;
				this.m_LayoutGroup = base.GetComponent<LayoutGroup>();
			}
			else
			{
				this.m_IsDrivenByLayoutComponents = false;
			}
			if (Application.isPlaying && this.m_CachedInputRenderer == null && this.m_TextComponent != null)
			{
				GameObject gameObject = new GameObject("Caret", new Type[] { typeof(TMP_SelectionCaret) });
				gameObject.hideFlags = HideFlags.DontSave;
				gameObject.transform.SetParent(this.m_TextComponent.transform.parent);
				gameObject.transform.SetAsFirstSibling();
				gameObject.layer = base.gameObject.layer;
				this.caretRectTrans = gameObject.GetComponent<RectTransform>();
				this.m_CachedInputRenderer = gameObject.GetComponent<CanvasRenderer>();
				this.m_CachedInputRenderer.SetMaterial(Graphic.defaultGraphicMaterial, Texture2D.whiteTexture);
				gameObject.AddComponent<LayoutElement>().ignoreLayout = true;
				this.AssignPositioningIfNeeded();
			}
			this.m_RectTransform = base.GetComponent<RectTransform>();
			IScrollHandler[] componentsInParent = base.GetComponentsInParent<IScrollHandler>();
			if (componentsInParent.Length > 1)
			{
				this.m_IScrollHandlerParent = componentsInParent[1] as ScrollRect;
			}
			if (this.m_TextViewport != null)
			{
				this.m_TextViewportRectMask = this.m_TextViewport.GetComponent<RectMask2D>();
				this.UpdateMaskRegions();
			}
			if (this.m_CachedInputRenderer != null)
			{
				this.m_CachedInputRenderer.SetMaterial(Graphic.defaultGraphicMaterial, Texture2D.whiteTexture);
			}
			if (this.m_TextComponent != null)
			{
				this.m_TextComponent.RegisterDirtyVerticesCallback(new UnityAction(this.MarkGeometryAsDirty));
				this.m_TextComponent.RegisterDirtyVerticesCallback(new UnityAction(this.UpdateLabel));
				if (this.m_VerticalScrollbar != null)
				{
					this.m_VerticalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.OnScrollbarValueChange));
				}
				this.UpdateLabel();
			}
			this.m_TouchKeyboardAllowsInPlaceEditing = TouchScreenKeyboard.isInPlaceEditingAllowed;
			TMPro_EventManager.TEXT_CHANGED_EVENT.Add(new Action<global::UnityEngine.Object>(this.ON_TEXT_CHANGED));
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000A9DC File Offset: 0x00008BDC
		protected override void OnDisable()
		{
			this.m_BlinkCoroutine = null;
			this.DeactivateInputField(false);
			if (this.m_TextComponent != null)
			{
				this.m_TextComponent.UnregisterDirtyVerticesCallback(new UnityAction(this.MarkGeometryAsDirty));
				this.m_TextComponent.UnregisterDirtyVerticesCallback(new UnityAction(this.UpdateLabel));
				if (this.m_VerticalScrollbar != null)
				{
					this.m_VerticalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.OnScrollbarValueChange));
				}
			}
			CanvasUpdateRegistry.UnRegisterCanvasElementForRebuild(this);
			if (this.m_CachedInputRenderer != null)
			{
				this.m_CachedInputRenderer.Clear();
			}
			if (this.m_Mesh != null)
			{
				global::UnityEngine.Object.DestroyImmediate(this.m_Mesh);
			}
			this.m_Mesh = null;
			TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(new Action<global::UnityEngine.Object>(this.ON_TEXT_CHANGED));
			base.OnDisable();
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000AAB8 File Offset: 0x00008CB8
		private void ON_TEXT_CHANGED(global::UnityEngine.Object obj)
		{
			if (obj == this.m_TextComponent && !this.m_IsStringPositionDirty)
			{
				if (Application.isPlaying && this.compositionLength == 0)
				{
					this.UpdateCaretPositionFromStringIndex();
				}
				if (this.m_VerticalScrollbar)
				{
					this.UpdateScrollbar();
				}
			}
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000AAF8 File Offset: 0x00008CF8
		private IEnumerator CaretBlink()
		{
			this.m_CaretVisible = true;
			yield return null;
			while ((this.isFocused || this.m_SelectionStillActive) && this.m_CaretBlinkRate > 0f)
			{
				float num = 1f / this.m_CaretBlinkRate;
				bool flag = (Time.unscaledTime - this.m_BlinkStartTime) % num < num / 2f;
				if (this.m_CaretVisible != flag)
				{
					this.m_CaretVisible = flag;
					if (!this.hasSelection)
					{
						this.MarkGeometryAsDirty();
					}
				}
				yield return null;
			}
			this.m_BlinkCoroutine = null;
			yield break;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000AB07 File Offset: 0x00008D07
		private void SetCaretVisible()
		{
			if (!this.m_AllowInput)
			{
				return;
			}
			this.m_CaretVisible = true;
			this.m_BlinkStartTime = Time.unscaledTime;
			this.SetCaretActive();
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000AB2A File Offset: 0x00008D2A
		private void SetCaretActive()
		{
			if (!this.m_AllowInput)
			{
				return;
			}
			if (this.m_CaretBlinkRate > 0f)
			{
				if (this.m_BlinkCoroutine == null)
				{
					this.m_BlinkCoroutine = base.StartCoroutine(this.CaretBlink());
					return;
				}
			}
			else
			{
				this.m_CaretVisible = true;
			}
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000AB64 File Offset: 0x00008D64
		protected void OnFocus()
		{
			if (this.m_OnFocusSelectAll)
			{
				this.SelectAll();
			}
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000AB74 File Offset: 0x00008D74
		protected void SelectAll()
		{
			this.m_isSelectAll = true;
			this.stringPositionInternal = this.text.Length;
			this.stringSelectPositionInternal = 0;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000AB98 File Offset: 0x00008D98
		public void MoveTextEnd(bool shift)
		{
			if (this.m_isRichTextEditingAllowed)
			{
				int length = this.text.Length;
				if (shift)
				{
					this.stringSelectPositionInternal = length;
				}
				else
				{
					this.stringPositionInternal = length;
					this.stringSelectPositionInternal = this.stringPositionInternal;
				}
			}
			else
			{
				int num = this.m_TextComponent.textInfo.characterCount - 1;
				if (shift)
				{
					this.caretSelectPositionInternal = num;
					this.stringSelectPositionInternal = this.GetStringIndexFromCaretPosition(num);
				}
				else
				{
					this.caretPositionInternal = (this.caretSelectPositionInternal = num);
					this.stringSelectPositionInternal = (this.stringPositionInternal = this.GetStringIndexFromCaretPosition(num));
				}
			}
			this.UpdateLabel();
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000AC34 File Offset: 0x00008E34
		public void MoveTextStart(bool shift)
		{
			if (this.m_isRichTextEditingAllowed)
			{
				int num = 0;
				if (shift)
				{
					this.stringSelectPositionInternal = num;
				}
				else
				{
					this.stringPositionInternal = num;
					this.stringSelectPositionInternal = this.stringPositionInternal;
				}
			}
			else
			{
				int num2 = 0;
				if (shift)
				{
					this.caretSelectPositionInternal = num2;
					this.stringSelectPositionInternal = this.GetStringIndexFromCaretPosition(num2);
				}
				else
				{
					this.caretPositionInternal = (this.caretSelectPositionInternal = num2);
					this.stringSelectPositionInternal = (this.stringPositionInternal = this.GetStringIndexFromCaretPosition(num2));
				}
			}
			this.UpdateLabel();
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000ACB4 File Offset: 0x00008EB4
		public void MoveToEndOfLine(bool shift, bool ctrl)
		{
			int lineNumber = this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal].lineNumber;
			int num = (ctrl ? (this.m_TextComponent.textInfo.characterCount - 1) : this.m_TextComponent.textInfo.lineInfo[lineNumber].lastCharacterIndex);
			int index = this.m_TextComponent.textInfo.characterInfo[num].index;
			if (shift)
			{
				this.stringSelectPositionInternal = index;
				this.caretSelectPositionInternal = num;
			}
			else
			{
				this.stringPositionInternal = index;
				this.stringSelectPositionInternal = this.stringPositionInternal;
				this.caretSelectPositionInternal = (this.caretPositionInternal = num);
			}
			this.UpdateLabel();
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000AD70 File Offset: 0x00008F70
		public void MoveToStartOfLine(bool shift, bool ctrl)
		{
			int lineNumber = this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal].lineNumber;
			int num = (ctrl ? 0 : this.m_TextComponent.textInfo.lineInfo[lineNumber].firstCharacterIndex);
			int num2 = 0;
			if (num > 0)
			{
				num2 = this.m_TextComponent.textInfo.characterInfo[num - 1].index + this.m_TextComponent.textInfo.characterInfo[num - 1].stringLength;
			}
			if (shift)
			{
				this.stringSelectPositionInternal = num2;
				this.caretSelectPositionInternal = num;
			}
			else
			{
				this.stringPositionInternal = num2;
				this.stringSelectPositionInternal = this.stringPositionInternal;
				this.caretSelectPositionInternal = (this.caretPositionInternal = num);
			}
			this.UpdateLabel();
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000AE3E File Offset: 0x0000903E
		// (set) Token: 0x06000246 RID: 582 RVA: 0x0000AE45 File Offset: 0x00009045
		private static string clipboard
		{
			get
			{
				return GUIUtility.systemCopyBuffer;
			}
			set
			{
				GUIUtility.systemCopyBuffer = value;
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000AE50 File Offset: 0x00009050
		private bool InPlaceEditing()
		{
			if (this.m_TouchKeyboardAllowsInPlaceEditing)
			{
				return true;
			}
			if (this.isUWP())
			{
				return !TouchScreenKeyboard.isSupported;
			}
			return (TouchScreenKeyboard.isSupported && this.shouldHideSoftKeyboard) || !TouchScreenKeyboard.isSupported || this.shouldHideSoftKeyboard || this.shouldHideMobileInput;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000AEA3 File Offset: 0x000090A3
		private bool InPlaceEditingChanged()
		{
			return !TMP_InputField.s_IsQuestDevice && this.m_TouchKeyboardAllowsInPlaceEditing != TouchScreenKeyboard.isInPlaceEditingAllowed;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000AEC0 File Offset: 0x000090C0
		private bool TouchScreenKeyboardShouldBeUsed()
		{
			RuntimePlatform platform = Application.platform;
			if (platform != RuntimePlatform.Android && platform != RuntimePlatform.WebGLPlayer)
			{
				return TouchScreenKeyboard.isSupported;
			}
			if (TMP_InputField.s_IsQuestDevice)
			{
				return TouchScreenKeyboard.isSupported;
			}
			return !TouchScreenKeyboard.isInPlaceEditingAllowed;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000AEF8 File Offset: 0x000090F8
		private void UpdateKeyboardStringPosition()
		{
			if (this.m_HideMobileInput && this.m_SoftKeyboard != null && this.m_SoftKeyboard.canSetSelection && (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS))
			{
				int num = Mathf.Min(this.stringSelectPositionInternal, this.stringPositionInternal);
				int num2 = Mathf.Abs(this.stringSelectPositionInternal - this.stringPositionInternal);
				this.m_SoftKeyboard.selection = new RangeInt(num, num2);
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000AF6C File Offset: 0x0000916C
		private void UpdateStringPositionFromKeyboard()
		{
			RangeInt selection = this.m_SoftKeyboard.selection;
			int start = selection.start;
			int end = selection.end;
			bool flag = false;
			if (this.stringPositionInternal != start)
			{
				flag = true;
				this.stringPositionInternal = start;
				this.caretPositionInternal = this.GetCaretPositionFromStringIndex(this.stringPositionInternal);
			}
			if (this.stringSelectPositionInternal != end)
			{
				this.stringSelectPositionInternal = end;
				flag = true;
				this.caretSelectPositionInternal = this.GetCaretPositionFromStringIndex(this.stringSelectPositionInternal);
			}
			if (flag)
			{
				this.m_BlinkStartTime = Time.unscaledTime;
				this.UpdateLabel();
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000AFF4 File Offset: 0x000091F4
		protected virtual void LateUpdate()
		{
			if (this.m_ShouldActivateNextUpdate)
			{
				if (!this.isFocused)
				{
					this.ActivateInputFieldInternal();
					this.m_ShouldActivateNextUpdate = false;
					return;
				}
				this.m_ShouldActivateNextUpdate = false;
			}
			if (this.isFocused && this.InPlaceEditingChanged())
			{
				this.DeactivateInputField(false);
			}
			if (!this.isFocused && this.m_SelectionStillActive)
			{
				GameObject gameObject = ((EventSystem.current != null) ? EventSystem.current.currentSelectedGameObject : null);
				if (gameObject == null && this.m_ResetOnDeActivation)
				{
					this.ReleaseSelection();
					return;
				}
				if (gameObject != null && gameObject != base.gameObject)
				{
					if (gameObject == this.m_PreviouslySelectedObject)
					{
						return;
					}
					this.m_PreviouslySelectedObject = gameObject;
					if (this.m_VerticalScrollbar && gameObject == this.m_VerticalScrollbar.gameObject)
					{
						return;
					}
					if (this.m_ResetOnDeActivation)
					{
						this.ReleaseSelection();
						return;
					}
					if (!this.m_KeepTextSelectionVisible && gameObject.GetComponent<TMP_InputField>() != null)
					{
						this.ReleaseSelection();
					}
					return;
				}
				else if (this.m_ProcessingEvent != null && this.m_ProcessingEvent.rawType == EventType.MouseDown && this.m_ProcessingEvent.button == 0)
				{
					bool flag = false;
					float unscaledTime = Time.unscaledTime;
					if (this.m_KeyDownStartTime + this.m_DoubleClickDelay > unscaledTime)
					{
						flag = true;
					}
					this.m_KeyDownStartTime = unscaledTime;
					if (flag)
					{
						this.ReleaseSelection();
						return;
					}
				}
			}
			this.UpdateMaskRegions();
			if ((this.InPlaceEditing() && this.isKeyboardUsingEvents()) || !this.isFocused)
			{
				return;
			}
			this.AssignPositioningIfNeeded();
			if (this.m_SoftKeyboard == null || this.m_SoftKeyboard.status != TouchScreenKeyboard.Status.Visible)
			{
				if (this.m_SoftKeyboard != null)
				{
					if (!this.m_ReadOnly)
					{
						this.text = this.m_SoftKeyboard.text;
					}
					TouchScreenKeyboard.Status status = this.m_SoftKeyboard.status;
					if (this.m_LastKeyCode != KeyCode.Return && status == TouchScreenKeyboard.Status.Done && this.isUWP())
					{
						status = TouchScreenKeyboard.Status.Canceled;
						this.m_IsKeyboardBeingClosedInHoloLens = true;
					}
					switch (status)
					{
					case TouchScreenKeyboard.Status.Done:
						this.m_ReleaseSelection = true;
						this.SendTouchScreenKeyboardStatusChanged();
						this.OnSubmit(null);
						break;
					case TouchScreenKeyboard.Status.Canceled:
						this.m_ReleaseSelection = true;
						this.m_WasCanceled = true;
						this.SendTouchScreenKeyboardStatusChanged();
						break;
					case TouchScreenKeyboard.Status.LostFocus:
						this.SendTouchScreenKeyboardStatusChanged();
						break;
					}
				}
				this.OnDeselect(null);
				return;
			}
			string text = this.m_SoftKeyboard.text;
			if (this.m_Text != text)
			{
				if (this.m_ReadOnly)
				{
					this.m_SoftKeyboard.text = this.m_Text;
				}
				else
				{
					this.m_Text = "";
					foreach (char c in text)
					{
						bool flag2 = false;
						if (c == '\r' || c == '\u0003')
						{
							c = '\n';
						}
						if (this.onValidateInput != null)
						{
							c = this.onValidateInput(this.m_Text, this.m_Text.Length, c);
						}
						else if (this.characterValidation != TMP_InputField.CharacterValidation.None)
						{
							string text2 = this.m_Text;
							c = this.Validate(this.m_Text, this.m_Text.Length, c);
							flag2 = text2 != this.m_Text;
						}
						if (this.lineType != TMP_InputField.LineType.MultiLineNewline && c == '\n')
						{
							this.UpdateLabel();
							this.OnSubmit(null);
							this.OnDeselect(null);
							return;
						}
						if (c != '\0' && (this.characterValidation != TMP_InputField.CharacterValidation.CustomValidator || !flag2))
						{
							this.m_Text += c.ToString();
						}
					}
					if (this.characterLimit > 0 && this.m_Text.Length > this.characterLimit)
					{
						this.m_Text = this.m_Text.Substring(0, this.characterLimit);
					}
					this.UpdateStringPositionFromKeyboard();
					if (this.m_Text != text)
					{
						this.m_SoftKeyboard.text = this.m_Text;
					}
					this.SendOnValueChangedAndUpdateLabel();
				}
			}
			else if (this.m_HideMobileInput && this.m_SoftKeyboard != null && this.m_SoftKeyboard.canSetSelection && Application.platform != RuntimePlatform.IPhonePlayer && Application.platform != RuntimePlatform.tvOS)
			{
				int num = Mathf.Min(this.stringSelectPositionInternal, this.stringPositionInternal);
				int num2 = Mathf.Abs(this.stringSelectPositionInternal - this.stringPositionInternal);
				this.m_SoftKeyboard.selection = new RangeInt(num, num2);
			}
			else if ((this.m_HideMobileInput && Application.platform == RuntimePlatform.Android) || (this.m_SoftKeyboard.canSetSelection && (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS)))
			{
				this.UpdateStringPositionFromKeyboard();
			}
			if (this.m_SoftKeyboard != null && this.m_SoftKeyboard.status != TouchScreenKeyboard.Status.Visible)
			{
				if (this.m_SoftKeyboard.status == TouchScreenKeyboard.Status.Canceled)
				{
					this.m_WasCanceled = true;
				}
				this.OnDeselect(null);
			}
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000B48C File Offset: 0x0000968C
		private bool MayDrag(PointerEventData eventData)
		{
			return this.IsActive() && this.IsInteractable() && eventData.button == PointerEventData.InputButton.Left && this.m_TextComponent != null && (this.m_SoftKeyboard == null || this.shouldHideSoftKeyboard || this.shouldHideMobileInput);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000B4D9 File Offset: 0x000096D9
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			this.m_UpdateDrag = true;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000B4EC File Offset: 0x000096EC
		public virtual void OnDrag(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			CaretPosition caretPosition;
			int cursorIndexFromPosition = TMP_TextUtilities.GetCursorIndexFromPosition(this.m_TextComponent, eventData.position, eventData.pressEventCamera, out caretPosition);
			if (this.m_isRichTextEditingAllowed)
			{
				if (caretPosition == CaretPosition.Left)
				{
					this.stringSelectPositionInternal = this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index;
				}
				else if (caretPosition == CaretPosition.Right)
				{
					this.stringSelectPositionInternal = this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index + this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].stringLength;
				}
			}
			else if (caretPosition == CaretPosition.Left)
			{
				this.stringSelectPositionInternal = ((cursorIndexFromPosition == 0) ? this.m_TextComponent.textInfo.characterInfo[0].index : (this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition - 1].index + this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition - 1].stringLength));
			}
			else if (caretPosition == CaretPosition.Right)
			{
				this.stringSelectPositionInternal = this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index + this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].stringLength;
			}
			this.caretSelectPositionInternal = this.GetCaretPositionFromStringIndex(this.stringSelectPositionInternal);
			this.MarkGeometryAsDirty();
			this.m_DragPositionOutOfBounds = !RectTransformUtility.RectangleContainsScreenPoint(this.textViewport, eventData.position, eventData.pressEventCamera);
			if (this.m_DragPositionOutOfBounds && this.m_DragCoroutine == null)
			{
				this.m_DragCoroutine = base.StartCoroutine(this.MouseDragOutsideRect(eventData));
			}
			this.UpdateKeyboardStringPosition();
			eventData.Use();
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000B6AC File Offset: 0x000098AC
		private IEnumerator MouseDragOutsideRect(PointerEventData eventData)
		{
			while (this.m_UpdateDrag && this.m_DragPositionOutOfBounds)
			{
				Vector2 vector;
				RectTransformUtility.ScreenPointToLocalPointInRectangle(this.textViewport, eventData.position, eventData.pressEventCamera, out vector);
				Rect rect = this.textViewport.rect;
				if (this.multiLine)
				{
					if (vector.y > rect.yMax)
					{
						this.MoveUp(true, true);
					}
					else if (vector.y < rect.yMin)
					{
						this.MoveDown(true, true);
					}
				}
				else if (vector.x < rect.xMin)
				{
					this.MoveLeft(true, false);
				}
				else if (vector.x > rect.xMax)
				{
					this.MoveRight(true, false);
				}
				this.UpdateLabel();
				float num = (this.multiLine ? 0.1f : 0.05f);
				if (this.m_WaitForSecondsRealtime == null)
				{
					this.m_WaitForSecondsRealtime = new WaitForSecondsRealtime(num);
				}
				else
				{
					this.m_WaitForSecondsRealtime.waitTime = num;
				}
				yield return this.m_WaitForSecondsRealtime;
			}
			this.m_DragCoroutine = null;
			yield break;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000B6C2 File Offset: 0x000098C2
		public virtual void OnEndDrag(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			this.m_UpdateDrag = false;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000B6D8 File Offset: 0x000098D8
		public override void OnPointerDown(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			EventSystem.current.SetSelectedGameObject(base.gameObject, eventData);
			bool allowInput = this.m_AllowInput;
			base.OnPointerDown(eventData);
			if (!this.InPlaceEditing() && (this.m_SoftKeyboard == null || !this.m_SoftKeyboard.active))
			{
				this.OnSelect(eventData);
				return;
			}
			Event.PopEvent(this.m_ProcessingEvent);
			bool flag = this.m_ProcessingEvent != null && (this.m_ProcessingEvent.modifiers & EventModifiers.Shift) > EventModifiers.None;
			bool flag2 = false;
			float unscaledTime = Time.unscaledTime;
			if (this.m_PointerDownClickStartTime + this.m_DoubleClickDelay > unscaledTime)
			{
				flag2 = true;
			}
			this.m_PointerDownClickStartTime = unscaledTime;
			if (allowInput || !this.m_OnFocusSelectAll)
			{
				CaretPosition caretPosition;
				int cursorIndexFromPosition = TMP_TextUtilities.GetCursorIndexFromPosition(this.m_TextComponent, eventData.position, eventData.pressEventCamera, out caretPosition);
				if (flag)
				{
					if (this.m_isRichTextEditingAllowed)
					{
						if (caretPosition == CaretPosition.Left)
						{
							this.stringSelectPositionInternal = this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index;
						}
						else if (caretPosition == CaretPosition.Right)
						{
							this.stringSelectPositionInternal = this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index + this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].stringLength;
						}
					}
					else if (caretPosition == CaretPosition.Left)
					{
						this.stringSelectPositionInternal = ((cursorIndexFromPosition == 0) ? this.m_TextComponent.textInfo.characterInfo[0].index : (this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition - 1].index + this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition - 1].stringLength));
					}
					else if (caretPosition == CaretPosition.Right)
					{
						this.stringSelectPositionInternal = this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index + this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].stringLength;
					}
				}
				else if (this.m_isRichTextEditingAllowed)
				{
					if (caretPosition == CaretPosition.Left)
					{
						this.stringPositionInternal = (this.stringSelectPositionInternal = this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index);
					}
					else if (caretPosition == CaretPosition.Right)
					{
						this.stringPositionInternal = (this.stringSelectPositionInternal = this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index + this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].stringLength);
					}
				}
				else if (caretPosition == CaretPosition.Left)
				{
					this.stringPositionInternal = (this.stringSelectPositionInternal = ((cursorIndexFromPosition == 0) ? this.m_TextComponent.textInfo.characterInfo[0].index : (this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition - 1].index + this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition - 1].stringLength)));
				}
				else if (caretPosition == CaretPosition.Right)
				{
					this.stringPositionInternal = (this.stringSelectPositionInternal = this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index + this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].stringLength);
				}
				if (flag2)
				{
					int num = TMP_TextUtilities.FindIntersectingWord(this.m_TextComponent, eventData.position, eventData.pressEventCamera);
					if (num != -1)
					{
						this.caretPositionInternal = this.m_TextComponent.textInfo.wordInfo[num].firstCharacterIndex;
						this.caretSelectPositionInternal = this.m_TextComponent.textInfo.wordInfo[num].lastCharacterIndex + 1;
						this.stringPositionInternal = this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal].index;
						this.stringSelectPositionInternal = this.m_TextComponent.textInfo.characterInfo[this.caretSelectPositionInternal - 1].index + this.m_TextComponent.textInfo.characterInfo[this.caretSelectPositionInternal - 1].stringLength;
					}
					else
					{
						this.caretPositionInternal = cursorIndexFromPosition;
						this.caretSelectPositionInternal = this.caretPositionInternal + 1;
						this.stringPositionInternal = this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index;
						this.stringSelectPositionInternal = this.stringPositionInternal + this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].stringLength;
					}
				}
				else
				{
					this.caretPositionInternal = (this.caretSelectPositionInternal = this.GetCaretPositionFromStringIndex(this.stringPositionInternal));
				}
				this.m_isSelectAll = false;
			}
			this.UpdateLabel();
			this.UpdateKeyboardStringPosition();
			eventData.Use();
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000BBCC File Offset: 0x00009DCC
		protected TMP_InputField.EditState KeyPressed(Event evt)
		{
			EventModifiers modifiers = evt.modifiers;
			bool flag = (this.m_IsApplePlatform ? ((modifiers & EventModifiers.Command) > EventModifiers.None) : ((modifiers & EventModifiers.Control) > EventModifiers.None));
			bool flag2 = (modifiers & EventModifiers.Shift) > EventModifiers.None;
			bool flag3 = (modifiers & EventModifiers.Alt) > EventModifiers.None;
			bool flag4 = flag && !flag3 && !flag2;
			this.m_LastKeyCode = evt.keyCode;
			KeyCode keyCode = evt.keyCode;
			if (keyCode <= KeyCode.A)
			{
				if (keyCode <= KeyCode.Return)
				{
					if (keyCode == KeyCode.Backspace)
					{
						this.Backspace();
						return TMP_InputField.EditState.Continue;
					}
					if (keyCode != KeyCode.Return)
					{
						goto IL_0229;
					}
				}
				else
				{
					if (keyCode == KeyCode.Escape)
					{
						this.m_ReleaseSelection = true;
						this.m_WasCanceled = true;
						return TMP_InputField.EditState.Finish;
					}
					if (keyCode != KeyCode.A)
					{
						goto IL_0229;
					}
					if (flag4)
					{
						this.SelectAll();
						return TMP_InputField.EditState.Continue;
					}
					goto IL_0229;
				}
			}
			else if (keyCode <= KeyCode.V)
			{
				if (keyCode != KeyCode.C)
				{
					if (keyCode != KeyCode.V)
					{
						goto IL_0229;
					}
					if (flag4)
					{
						this.Append(TMP_InputField.clipboard);
						return TMP_InputField.EditState.Continue;
					}
					goto IL_0229;
				}
				else
				{
					if (flag4)
					{
						if (this.inputType != TMP_InputField.InputType.Password)
						{
							TMP_InputField.clipboard = this.GetSelectedString();
						}
						else
						{
							TMP_InputField.clipboard = "";
						}
						return TMP_InputField.EditState.Continue;
					}
					goto IL_0229;
				}
			}
			else if (keyCode != KeyCode.X)
			{
				if (keyCode == KeyCode.Delete)
				{
					this.DeleteKey();
					return TMP_InputField.EditState.Continue;
				}
				switch (keyCode)
				{
				case KeyCode.KeypadEnter:
					break;
				case KeyCode.KeypadEquals:
				case KeyCode.Insert:
					goto IL_0229;
				case KeyCode.UpArrow:
					this.MoveUp(flag2);
					return TMP_InputField.EditState.Continue;
				case KeyCode.DownArrow:
					this.MoveDown(flag2);
					return TMP_InputField.EditState.Continue;
				case KeyCode.RightArrow:
					this.MoveRight(flag2, flag);
					return TMP_InputField.EditState.Continue;
				case KeyCode.LeftArrow:
					this.MoveLeft(flag2, flag);
					return TMP_InputField.EditState.Continue;
				case KeyCode.Home:
					this.MoveToStartOfLine(flag2, flag);
					return TMP_InputField.EditState.Continue;
				case KeyCode.End:
					this.MoveToEndOfLine(flag2, flag);
					return TMP_InputField.EditState.Continue;
				case KeyCode.PageUp:
					this.MovePageUp(flag2);
					return TMP_InputField.EditState.Continue;
				case KeyCode.PageDown:
					this.MovePageDown(flag2);
					return TMP_InputField.EditState.Continue;
				default:
					goto IL_0229;
				}
			}
			else
			{
				if (flag4)
				{
					if (this.inputType != TMP_InputField.InputType.Password)
					{
						TMP_InputField.clipboard = this.GetSelectedString();
					}
					else
					{
						TMP_InputField.clipboard = "";
					}
					this.Delete();
					this.UpdateTouchKeyboardFromEditChanges();
					this.SendOnValueChangedAndUpdateLabel();
					return TMP_InputField.EditState.Continue;
				}
				goto IL_0229;
			}
			if (this.lineType != TMP_InputField.LineType.MultiLineNewline)
			{
				this.m_ReleaseSelection = true;
				return TMP_InputField.EditState.Finish;
			}
			TMP_TextInfo textInfo = this.m_TextComponent.textInfo;
			if (this.m_LineLimit > 0 && textInfo != null && textInfo.lineCount >= this.m_LineLimit)
			{
				this.m_ReleaseSelection = true;
				return TMP_InputField.EditState.Finish;
			}
			IL_0229:
			char c = evt.character;
			if (TMP_InputField.enableHorribleDeckWorkaround && TMP_InputField.keycodeMapper != null && (evt.modifiers & EventModifiers.Shift) != EventModifiers.None && evt.keyCode != KeyCode.None)
			{
				c = TMP_InputField.keycodeMapper(evt.keyCode);
			}
			if (!this.multiLine && (c == '\t' || c == '\r' || c == '\n'))
			{
				return TMP_InputField.EditState.Continue;
			}
			if (c == '\r' || c == '\u0003')
			{
				c = '\n';
			}
			if (flag2 && c == '\n')
			{
				c = '\v';
			}
			if (this.IsValidChar(c))
			{
				this.Append(c);
			}
			if (c == '\0' && this.compositionLength > 0)
			{
				this.UpdateLabel();
			}
			return TMP_InputField.EditState.Continue;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000BE9A File Offset: 0x0000A09A
		protected virtual bool IsValidChar(char c)
		{
			return c != '\u007f' && (c == '\t' || c == '\n' || c >= ' ');
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000BEB7 File Offset: 0x0000A0B7
		public void ProcessEvent(Event e)
		{
			this.KeyPressed(e);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000BEC4 File Offset: 0x0000A0C4
		public virtual void OnUpdateSelected(BaseEventData eventData)
		{
			if (!this.isFocused)
			{
				return;
			}
			bool flag = false;
			while (Event.PopEvent(this.m_ProcessingEvent))
			{
				EventType rawType = this.m_ProcessingEvent.rawType;
				if (rawType != EventType.KeyUp)
				{
					if (rawType == EventType.KeyDown)
					{
						flag = true;
						if (!this.m_IsCompositionActive || this.compositionLength != 0 || this.m_ProcessingEvent.character != '\0' || this.m_ProcessingEvent.modifiers != EventModifiers.None)
						{
							if (this.KeyPressed(this.m_ProcessingEvent) == TMP_InputField.EditState.Finish)
							{
								if (!this.m_WasCanceled)
								{
									this.SendOnSubmit();
								}
								this.DeactivateInputField(false);
								break;
							}
							this.m_IsTextComponentUpdateRequired = true;
							this.UpdateLabel();
						}
					}
					else if (rawType - EventType.ValidateCommand <= 1 && this.m_ProcessingEvent.commandName == "SelectAll")
					{
						this.SelectAll();
						flag = true;
					}
				}
			}
			if (flag)
			{
				this.UpdateLabel();
				eventData.Use();
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000BFA0 File Offset: 0x0000A1A0
		public virtual void OnScroll(PointerEventData eventData)
		{
			if (this.m_LineType == TMP_InputField.LineType.SingleLine)
			{
				if (this.m_IScrollHandlerParent != null)
				{
					this.m_IScrollHandlerParent.OnScroll(eventData);
				}
				return;
			}
			if (this.m_TextComponent.preferredHeight < this.m_TextViewport.rect.height)
			{
				return;
			}
			float num = -eventData.scrollDelta.y;
			this.m_ScrollPosition = this.GetScrollPositionRelativeToViewport();
			this.m_ScrollPosition += 1f / (float)this.m_TextComponent.textInfo.lineCount * num * this.m_ScrollSensitivity;
			this.m_ScrollPosition = Mathf.Clamp01(this.m_ScrollPosition);
			this.AdjustTextPositionRelativeToViewport(this.m_ScrollPosition);
			if (this.m_VerticalScrollbar)
			{
				this.m_VerticalScrollbar.value = this.m_ScrollPosition;
			}
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000C06C File Offset: 0x0000A26C
		private float GetScrollPositionRelativeToViewport()
		{
			Rect rect = this.m_TextViewport.rect;
			return (float)((int)((this.m_TextComponent.textInfo.lineInfo[0].ascender + this.m_TextComponent.margin.y + this.m_TextComponent.margin.w - rect.yMax + this.m_TextComponent.rectTransform.anchoredPosition.y) / (this.m_TextComponent.preferredHeight - rect.height) * 1000f + 0.5f)) / 1000f;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000C108 File Offset: 0x0000A308
		private string GetSelectedString()
		{
			if (!this.hasSelection)
			{
				return "";
			}
			int num = this.stringPositionInternal;
			int num2 = this.stringSelectPositionInternal;
			if (num > num2)
			{
				int num3 = num;
				num = num2;
				num2 = num3;
			}
			return this.text.Substring(num, num2 - num);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000C148 File Offset: 0x0000A348
		private int FindNextWordBegin()
		{
			if (this.stringSelectPositionInternal + 1 >= this.text.Length)
			{
				return this.text.Length;
			}
			int num = this.text.IndexOfAny(TMP_InputField.kSeparators, this.stringSelectPositionInternal + 1);
			if (num == -1)
			{
				num = this.text.Length;
			}
			else
			{
				num++;
			}
			return num;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000C1A8 File Offset: 0x0000A3A8
		private void MoveRight(bool shift, bool ctrl)
		{
			if (this.hasSelection && !shift)
			{
				this.stringPositionInternal = (this.stringSelectPositionInternal = Mathf.Max(this.stringPositionInternal, this.stringSelectPositionInternal));
				this.caretPositionInternal = (this.caretSelectPositionInternal = this.GetCaretPositionFromStringIndex(this.stringSelectPositionInternal));
				return;
			}
			int num;
			if (ctrl)
			{
				num = this.FindNextWordBegin();
			}
			else if (this.m_isRichTextEditingAllowed)
			{
				if (this.stringSelectPositionInternal < this.text.Length && char.IsHighSurrogate(this.text[this.stringSelectPositionInternal]))
				{
					num = this.stringSelectPositionInternal + 2;
				}
				else
				{
					num = this.stringSelectPositionInternal + 1;
				}
			}
			else if (this.m_TextComponent.textInfo.characterInfo[this.caretSelectPositionInternal].character == '\r' && this.m_TextComponent.textInfo.characterInfo[this.caretSelectPositionInternal + 1].character == '\n')
			{
				num = this.m_TextComponent.textInfo.characterInfo[this.caretSelectPositionInternal + 1].index + this.m_TextComponent.textInfo.characterInfo[this.caretSelectPositionInternal + 1].stringLength;
			}
			else
			{
				num = this.m_TextComponent.textInfo.characterInfo[this.caretSelectPositionInternal].index + this.m_TextComponent.textInfo.characterInfo[this.caretSelectPositionInternal].stringLength;
			}
			if (shift)
			{
				this.stringSelectPositionInternal = num;
				this.caretSelectPositionInternal = this.GetCaretPositionFromStringIndex(this.stringSelectPositionInternal);
				return;
			}
			this.stringSelectPositionInternal = (this.stringPositionInternal = num);
			if (this.stringPositionInternal >= this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal].index + this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal].stringLength)
			{
				this.caretSelectPositionInternal = (this.caretPositionInternal = this.GetCaretPositionFromStringIndex(this.stringSelectPositionInternal));
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000C3C0 File Offset: 0x0000A5C0
		private int FindPrevWordBegin()
		{
			if (this.stringSelectPositionInternal - 2 < 0)
			{
				return 0;
			}
			int num = this.text.LastIndexOfAny(TMP_InputField.kSeparators, this.stringSelectPositionInternal - 2);
			if (num == -1)
			{
				num = 0;
			}
			else
			{
				num++;
			}
			return num;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000C400 File Offset: 0x0000A600
		private void MoveLeft(bool shift, bool ctrl)
		{
			if (this.hasSelection && !shift)
			{
				this.stringPositionInternal = (this.stringSelectPositionInternal = Mathf.Min(this.stringPositionInternal, this.stringSelectPositionInternal));
				this.caretPositionInternal = (this.caretSelectPositionInternal = this.GetCaretPositionFromStringIndex(this.stringSelectPositionInternal));
				return;
			}
			int num;
			if (ctrl)
			{
				num = this.FindPrevWordBegin();
			}
			else if (this.m_isRichTextEditingAllowed)
			{
				if (this.stringSelectPositionInternal > 0 && char.IsLowSurrogate(this.text[this.stringSelectPositionInternal - 1]))
				{
					num = this.stringSelectPositionInternal - 2;
				}
				else
				{
					num = this.stringSelectPositionInternal - 1;
				}
			}
			else
			{
				num = ((this.caretSelectPositionInternal < 1) ? this.m_TextComponent.textInfo.characterInfo[0].index : this.m_TextComponent.textInfo.characterInfo[this.caretSelectPositionInternal - 1].index);
				if (num > 0 && this.m_TextComponent.textInfo.characterInfo[this.caretSelectPositionInternal - 1].character == '\n' && this.m_TextComponent.textInfo.characterInfo[this.caretSelectPositionInternal - 2].character == '\r')
				{
					num = this.m_TextComponent.textInfo.characterInfo[this.caretSelectPositionInternal - 2].index;
				}
			}
			if (shift)
			{
				this.stringSelectPositionInternal = num;
				this.caretSelectPositionInternal = this.GetCaretPositionFromStringIndex(this.stringSelectPositionInternal);
				return;
			}
			this.stringSelectPositionInternal = (this.stringPositionInternal = num);
			if (this.caretPositionInternal > 0 && this.stringPositionInternal <= this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal - 1].index)
			{
				this.caretSelectPositionInternal = (this.caretPositionInternal = this.GetCaretPositionFromStringIndex(this.stringSelectPositionInternal));
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000C5E4 File Offset: 0x0000A7E4
		private int LineUpCharacterPosition(int originalPos, bool goToFirstChar)
		{
			if (originalPos >= this.m_TextComponent.textInfo.characterCount)
			{
				originalPos--;
			}
			TMP_CharacterInfo tmp_CharacterInfo = this.m_TextComponent.textInfo.characterInfo[originalPos];
			int lineNumber = tmp_CharacterInfo.lineNumber;
			if (lineNumber - 1 < 0)
			{
				if (!goToFirstChar)
				{
					return originalPos;
				}
				return 0;
			}
			else
			{
				int num = this.m_TextComponent.textInfo.lineInfo[lineNumber].firstCharacterIndex - 1;
				int num2 = -1;
				float num3 = 32767f;
				float num4 = 0f;
				int i = this.m_TextComponent.textInfo.lineInfo[lineNumber - 1].firstCharacterIndex;
				while (i < num)
				{
					TMP_CharacterInfo tmp_CharacterInfo2 = this.m_TextComponent.textInfo.characterInfo[i];
					float num5 = tmp_CharacterInfo.origin - tmp_CharacterInfo2.origin;
					float num6 = num5 / (tmp_CharacterInfo2.xAdvance - tmp_CharacterInfo2.origin);
					if (num6 >= 0f && num6 <= 1f)
					{
						if (num6 < 0.5f)
						{
							return i;
						}
						return i + 1;
					}
					else
					{
						num5 = Mathf.Abs(num5);
						if (num5 < num3)
						{
							num2 = i;
							num3 = num5;
							num4 = num6;
						}
						i++;
					}
				}
				if (num2 == -1)
				{
					return num;
				}
				if (num4 < 0.5f)
				{
					return num2;
				}
				return num2 + 1;
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000C724 File Offset: 0x0000A924
		private int LineDownCharacterPosition(int originalPos, bool goToLastChar)
		{
			if (originalPos >= this.m_TextComponent.textInfo.characterCount)
			{
				return this.m_TextComponent.textInfo.characterCount - 1;
			}
			TMP_CharacterInfo tmp_CharacterInfo = this.m_TextComponent.textInfo.characterInfo[originalPos];
			int lineNumber = tmp_CharacterInfo.lineNumber;
			if (lineNumber + 1 >= this.m_TextComponent.textInfo.lineCount)
			{
				if (!goToLastChar)
				{
					return originalPos;
				}
				return this.m_TextComponent.textInfo.characterCount - 1;
			}
			else
			{
				int lastCharacterIndex = this.m_TextComponent.textInfo.lineInfo[lineNumber + 1].lastCharacterIndex;
				int num = -1;
				float num2 = 32767f;
				float num3 = 0f;
				int i = this.m_TextComponent.textInfo.lineInfo[lineNumber + 1].firstCharacterIndex;
				while (i < lastCharacterIndex)
				{
					TMP_CharacterInfo tmp_CharacterInfo2 = this.m_TextComponent.textInfo.characterInfo[i];
					float num4 = tmp_CharacterInfo.origin - tmp_CharacterInfo2.origin;
					float num5 = num4 / (tmp_CharacterInfo2.xAdvance - tmp_CharacterInfo2.origin);
					if (num5 >= 0f && num5 <= 1f)
					{
						if (num5 < 0.5f)
						{
							return i;
						}
						return i + 1;
					}
					else
					{
						num4 = Mathf.Abs(num4);
						if (num4 < num2)
						{
							num = i;
							num2 = num4;
							num3 = num5;
						}
						i++;
					}
				}
				if (num == -1)
				{
					return lastCharacterIndex;
				}
				if (num3 < 0.5f)
				{
					return num;
				}
				return num + 1;
			}
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000C890 File Offset: 0x0000AA90
		private int PageUpCharacterPosition(int originalPos, bool goToFirstChar)
		{
			if (originalPos >= this.m_TextComponent.textInfo.characterCount)
			{
				originalPos--;
			}
			TMP_CharacterInfo tmp_CharacterInfo = this.m_TextComponent.textInfo.characterInfo[originalPos];
			int lineNumber = tmp_CharacterInfo.lineNumber;
			if (lineNumber - 1 < 0)
			{
				if (!goToFirstChar)
				{
					return originalPos;
				}
				return 0;
			}
			else
			{
				float height = this.m_TextViewport.rect.height;
				int num = lineNumber - 1;
				while (num > 0 && this.m_TextComponent.textInfo.lineInfo[num].baseline <= this.m_TextComponent.textInfo.lineInfo[lineNumber].baseline + height)
				{
					num--;
				}
				int lastCharacterIndex = this.m_TextComponent.textInfo.lineInfo[num].lastCharacterIndex;
				int num2 = -1;
				float num3 = 32767f;
				float num4 = 0f;
				int i = this.m_TextComponent.textInfo.lineInfo[num].firstCharacterIndex;
				while (i < lastCharacterIndex)
				{
					TMP_CharacterInfo tmp_CharacterInfo2 = this.m_TextComponent.textInfo.characterInfo[i];
					float num5 = tmp_CharacterInfo.origin - tmp_CharacterInfo2.origin;
					float num6 = num5 / (tmp_CharacterInfo2.xAdvance - tmp_CharacterInfo2.origin);
					if (num6 >= 0f && num6 <= 1f)
					{
						if (num6 < 0.5f)
						{
							return i;
						}
						return i + 1;
					}
					else
					{
						num5 = Mathf.Abs(num5);
						if (num5 < num3)
						{
							num2 = i;
							num3 = num5;
							num4 = num6;
						}
						i++;
					}
				}
				if (num2 == -1)
				{
					return lastCharacterIndex;
				}
				if (num4 < 0.5f)
				{
					return num2;
				}
				return num2 + 1;
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000CA30 File Offset: 0x0000AC30
		private int PageDownCharacterPosition(int originalPos, bool goToLastChar)
		{
			if (originalPos >= this.m_TextComponent.textInfo.characterCount)
			{
				return this.m_TextComponent.textInfo.characterCount - 1;
			}
			TMP_CharacterInfo tmp_CharacterInfo = this.m_TextComponent.textInfo.characterInfo[originalPos];
			int lineNumber = tmp_CharacterInfo.lineNumber;
			if (lineNumber + 1 >= this.m_TextComponent.textInfo.lineCount)
			{
				if (!goToLastChar)
				{
					return originalPos;
				}
				return this.m_TextComponent.textInfo.characterCount - 1;
			}
			else
			{
				float height = this.m_TextViewport.rect.height;
				int num = lineNumber + 1;
				while (num < this.m_TextComponent.textInfo.lineCount - 1 && this.m_TextComponent.textInfo.lineInfo[num].baseline >= this.m_TextComponent.textInfo.lineInfo[lineNumber].baseline - height)
				{
					num++;
				}
				int lastCharacterIndex = this.m_TextComponent.textInfo.lineInfo[num].lastCharacterIndex;
				int num2 = -1;
				float num3 = 32767f;
				float num4 = 0f;
				int i = this.m_TextComponent.textInfo.lineInfo[num].firstCharacterIndex;
				while (i < lastCharacterIndex)
				{
					TMP_CharacterInfo tmp_CharacterInfo2 = this.m_TextComponent.textInfo.characterInfo[i];
					float num5 = tmp_CharacterInfo.origin - tmp_CharacterInfo2.origin;
					float num6 = num5 / (tmp_CharacterInfo2.xAdvance - tmp_CharacterInfo2.origin);
					if (num6 >= 0f && num6 <= 1f)
					{
						if (num6 < 0.5f)
						{
							return i;
						}
						return i + 1;
					}
					else
					{
						num5 = Mathf.Abs(num5);
						if (num5 < num3)
						{
							num2 = i;
							num3 = num5;
							num4 = num6;
						}
						i++;
					}
				}
				if (num2 == -1)
				{
					return lastCharacterIndex;
				}
				if (num4 < 0.5f)
				{
					return num2;
				}
				return num2 + 1;
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000CC0E File Offset: 0x0000AE0E
		private void MoveDown(bool shift)
		{
			this.MoveDown(shift, true);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000CC18 File Offset: 0x0000AE18
		private void MoveDown(bool shift, bool goToLastChar)
		{
			if (this.hasSelection && !shift)
			{
				this.caretPositionInternal = (this.caretSelectPositionInternal = Mathf.Max(this.caretPositionInternal, this.caretSelectPositionInternal));
			}
			int num = (this.multiLine ? this.LineDownCharacterPosition(this.caretSelectPositionInternal, goToLastChar) : (this.m_TextComponent.textInfo.characterCount - 1));
			if (shift)
			{
				this.caretSelectPositionInternal = num;
				this.stringSelectPositionInternal = this.GetStringIndexFromCaretPosition(this.caretSelectPositionInternal);
				return;
			}
			this.caretSelectPositionInternal = (this.caretPositionInternal = num);
			this.stringSelectPositionInternal = (this.stringPositionInternal = this.GetStringIndexFromCaretPosition(this.caretSelectPositionInternal));
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000CCC2 File Offset: 0x0000AEC2
		private void MoveUp(bool shift)
		{
			this.MoveUp(shift, true);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000CCCC File Offset: 0x0000AECC
		private void MoveUp(bool shift, bool goToFirstChar)
		{
			if (this.hasSelection && !shift)
			{
				this.caretPositionInternal = (this.caretSelectPositionInternal = Mathf.Min(this.caretPositionInternal, this.caretSelectPositionInternal));
			}
			int num = (this.multiLine ? this.LineUpCharacterPosition(this.caretSelectPositionInternal, goToFirstChar) : 0);
			if (shift)
			{
				this.caretSelectPositionInternal = num;
				this.stringSelectPositionInternal = this.GetStringIndexFromCaretPosition(this.caretSelectPositionInternal);
				return;
			}
			this.caretSelectPositionInternal = (this.caretPositionInternal = num);
			this.stringSelectPositionInternal = (this.stringPositionInternal = this.GetStringIndexFromCaretPosition(this.caretSelectPositionInternal));
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000CD65 File Offset: 0x0000AF65
		private void MovePageUp(bool shift)
		{
			this.MovePageUp(shift, true);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000CD70 File Offset: 0x0000AF70
		private void MovePageUp(bool shift, bool goToFirstChar)
		{
			if (this.hasSelection && !shift)
			{
				this.caretPositionInternal = (this.caretSelectPositionInternal = Mathf.Min(this.caretPositionInternal, this.caretSelectPositionInternal));
			}
			int num = (this.multiLine ? this.PageUpCharacterPosition(this.caretSelectPositionInternal, goToFirstChar) : 0);
			if (shift)
			{
				this.caretSelectPositionInternal = num;
				this.stringSelectPositionInternal = this.GetStringIndexFromCaretPosition(this.caretSelectPositionInternal);
			}
			else
			{
				this.caretSelectPositionInternal = (this.caretPositionInternal = num);
				this.stringSelectPositionInternal = (this.stringPositionInternal = this.GetStringIndexFromCaretPosition(this.caretSelectPositionInternal));
			}
			if (this.m_LineType != TMP_InputField.LineType.SingleLine)
			{
				float num2 = this.m_TextViewport.rect.height;
				float num3 = this.m_TextComponent.rectTransform.position.y + this.m_TextComponent.textBounds.max.y;
				float num4 = this.m_TextViewport.position.y + this.m_TextViewport.rect.yMax;
				num2 = ((num4 > num3 + num2) ? num2 : (num4 - num3));
				this.m_TextComponent.rectTransform.anchoredPosition += new Vector2(0f, num2);
				this.AssignPositioningIfNeeded();
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000CEBC File Offset: 0x0000B0BC
		private void MovePageDown(bool shift)
		{
			this.MovePageDown(shift, true);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000CEC8 File Offset: 0x0000B0C8
		private void MovePageDown(bool shift, bool goToLastChar)
		{
			if (this.hasSelection && !shift)
			{
				this.caretPositionInternal = (this.caretSelectPositionInternal = Mathf.Max(this.caretPositionInternal, this.caretSelectPositionInternal));
			}
			int num = (this.multiLine ? this.PageDownCharacterPosition(this.caretSelectPositionInternal, goToLastChar) : (this.m_TextComponent.textInfo.characterCount - 1));
			if (shift)
			{
				this.caretSelectPositionInternal = num;
				this.stringSelectPositionInternal = this.GetStringIndexFromCaretPosition(this.caretSelectPositionInternal);
			}
			else
			{
				this.caretSelectPositionInternal = (this.caretPositionInternal = num);
				this.stringSelectPositionInternal = (this.stringPositionInternal = this.GetStringIndexFromCaretPosition(this.caretSelectPositionInternal));
			}
			if (this.m_LineType != TMP_InputField.LineType.SingleLine)
			{
				float num2 = this.m_TextViewport.rect.height;
				float num3 = this.m_TextComponent.rectTransform.position.y + this.m_TextComponent.textBounds.min.y;
				float num4 = this.m_TextViewport.position.y + this.m_TextViewport.rect.yMin;
				num2 = ((num4 > num3 + num2) ? num2 : (num4 - num3));
				this.m_TextComponent.rectTransform.anchoredPosition += new Vector2(0f, num2);
				this.AssignPositioningIfNeeded();
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000D028 File Offset: 0x0000B228
		private void Delete()
		{
			if (this.m_ReadOnly)
			{
				return;
			}
			if (this.m_StringPosition == this.m_StringSelectPosition)
			{
				return;
			}
			if (this.m_isRichTextEditingAllowed || this.m_isSelectAll)
			{
				if (this.m_StringPosition < this.m_StringSelectPosition)
				{
					this.m_Text = this.text.Remove(this.m_StringPosition, this.m_StringSelectPosition - this.m_StringPosition);
					this.m_StringSelectPosition = this.m_StringPosition;
				}
				else
				{
					this.m_Text = this.text.Remove(this.m_StringSelectPosition, this.m_StringPosition - this.m_StringSelectPosition);
					this.m_StringPosition = this.m_StringSelectPosition;
				}
				if (this.m_isSelectAll)
				{
					this.m_CaretPosition = (this.m_CaretSelectPosition = 0);
					this.m_isSelectAll = false;
					return;
				}
			}
			else
			{
				if (this.m_CaretPosition < this.m_CaretSelectPosition)
				{
					int num = this.ClampArrayIndex(this.m_CaretSelectPosition - 1);
					this.m_StringPosition = this.m_TextComponent.textInfo.characterInfo[this.m_CaretPosition].index;
					this.m_StringSelectPosition = this.m_TextComponent.textInfo.characterInfo[num].index + this.m_TextComponent.textInfo.characterInfo[num].stringLength;
					this.m_Text = this.text.Remove(this.m_StringPosition, this.m_StringSelectPosition - this.m_StringPosition);
					this.m_StringSelectPosition = this.m_StringPosition;
					this.m_CaretSelectPosition = this.m_CaretPosition;
					return;
				}
				int num2 = this.ClampArrayIndex(this.m_CaretPosition - 1);
				this.m_StringPosition = this.m_TextComponent.textInfo.characterInfo[num2].index + this.m_TextComponent.textInfo.characterInfo[num2].stringLength;
				this.m_StringSelectPosition = this.m_TextComponent.textInfo.characterInfo[this.m_CaretSelectPosition].index;
				this.m_Text = this.text.Remove(this.m_StringSelectPosition, this.m_StringPosition - this.m_StringSelectPosition);
				this.m_StringPosition = this.m_StringSelectPosition;
				this.m_CaretPosition = this.m_CaretSelectPosition;
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000D264 File Offset: 0x0000B464
		private void DeleteKey()
		{
			if (this.m_ReadOnly)
			{
				return;
			}
			if (this.hasSelection)
			{
				this.m_HasTextBeenRemoved = true;
				this.Delete();
				this.UpdateTouchKeyboardFromEditChanges();
				this.SendOnValueChangedAndUpdateLabel();
				return;
			}
			if (this.m_isRichTextEditingAllowed)
			{
				if (this.stringPositionInternal < this.text.Length)
				{
					if (char.IsHighSurrogate(this.text[this.stringPositionInternal]))
					{
						this.m_Text = this.text.Remove(this.stringPositionInternal, 2);
					}
					else
					{
						this.m_Text = this.text.Remove(this.stringPositionInternal, 1);
					}
					this.m_HasTextBeenRemoved = true;
					this.UpdateTouchKeyboardFromEditChanges();
					this.SendOnValueChangedAndUpdateLabel();
					return;
				}
			}
			else if (this.caretPositionInternal < this.m_TextComponent.textInfo.characterCount - 1)
			{
				int num = this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal].stringLength;
				if (this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal].character == '\r' && this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal + 1].character == '\n')
				{
					num += this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal + 1].stringLength;
				}
				int index = this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal].index;
				this.m_Text = this.text.Remove(index, num);
				this.m_HasTextBeenRemoved = true;
				this.SendOnValueChangedAndUpdateLabel();
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000D408 File Offset: 0x0000B608
		private void Backspace()
		{
			if (this.m_ReadOnly)
			{
				return;
			}
			if (this.hasSelection)
			{
				this.m_HasTextBeenRemoved = true;
				this.Delete();
				this.UpdateTouchKeyboardFromEditChanges();
				this.SendOnValueChangedAndUpdateLabel();
				return;
			}
			if (this.m_isRichTextEditingAllowed)
			{
				if (this.stringPositionInternal > 0)
				{
					int num = 1;
					if (char.IsLowSurrogate(this.text[this.stringPositionInternal - 1]))
					{
						num = 2;
					}
					this.stringSelectPositionInternal = (this.stringPositionInternal -= num);
					this.m_Text = this.text.Remove(this.stringPositionInternal, num);
					this.caretSelectPositionInternal = --this.caretPositionInternal;
					this.m_HasTextBeenRemoved = true;
					this.UpdateTouchKeyboardFromEditChanges();
					this.SendOnValueChangedAndUpdateLabel();
					return;
				}
			}
			else
			{
				if (this.caretPositionInternal > 0)
				{
					int num2 = this.caretPositionInternal - 1;
					int num3 = this.m_TextComponent.textInfo.characterInfo[num2].stringLength;
					if (num2 > 0 && this.m_TextComponent.textInfo.characterInfo[num2].character == '\n' && this.m_TextComponent.textInfo.characterInfo[num2 - 1].character == '\r')
					{
						num3 += this.m_TextComponent.textInfo.characterInfo[num2 - 1].stringLength;
						num2--;
					}
					this.m_Text = this.text.Remove(this.m_TextComponent.textInfo.characterInfo[num2].index, num3);
					this.stringSelectPositionInternal = (this.stringPositionInternal = ((this.caretPositionInternal < 1) ? this.m_TextComponent.textInfo.characterInfo[0].index : this.m_TextComponent.textInfo.characterInfo[num2].index));
					this.caretSelectPositionInternal = (this.caretPositionInternal = num2);
				}
				this.m_HasTextBeenRemoved = true;
				this.UpdateTouchKeyboardFromEditChanges();
				this.SendOnValueChangedAndUpdateLabel();
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000D608 File Offset: 0x0000B808
		protected virtual void Append(string input)
		{
			if (this.m_ReadOnly)
			{
				return;
			}
			if (!this.InPlaceEditing())
			{
				return;
			}
			int i = 0;
			int length = input.Length;
			while (i < length)
			{
				char c = input[i];
				if (c >= ' ' || c == '\t' || c == '\r' || c == '\n')
				{
					this.Append(c);
				}
				i++;
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000D660 File Offset: 0x0000B860
		protected virtual void Append(char input)
		{
			if (this.m_ReadOnly)
			{
				return;
			}
			if (!this.InPlaceEditing())
			{
				return;
			}
			int num = Mathf.Min(this.stringPositionInternal, this.stringSelectPositionInternal);
			string text = this.text;
			if (this.selectionFocusPosition != this.selectionAnchorPosition)
			{
				this.m_HasTextBeenRemoved = true;
				if (this.m_isRichTextEditingAllowed || this.m_isSelectAll)
				{
					if (this.m_StringPosition < this.m_StringSelectPosition)
					{
						text = this.text.Remove(this.m_StringPosition, this.m_StringSelectPosition - this.m_StringPosition);
					}
					else
					{
						text = this.text.Remove(this.m_StringSelectPosition, this.m_StringPosition - this.m_StringSelectPosition);
					}
				}
				else if (this.m_CaretPosition < this.m_CaretSelectPosition)
				{
					this.m_StringPosition = this.m_TextComponent.textInfo.characterInfo[this.m_CaretPosition].index;
					this.m_StringSelectPosition = this.m_TextComponent.textInfo.characterInfo[this.m_CaretSelectPosition - 1].index + this.m_TextComponent.textInfo.characterInfo[this.m_CaretSelectPosition - 1].stringLength;
					text = this.text.Remove(this.m_StringPosition, this.m_StringSelectPosition - this.m_StringPosition);
				}
				else
				{
					this.m_StringPosition = this.m_TextComponent.textInfo.characterInfo[this.m_CaretPosition - 1].index + this.m_TextComponent.textInfo.characterInfo[this.m_CaretPosition - 1].stringLength;
					this.m_StringSelectPosition = this.m_TextComponent.textInfo.characterInfo[this.m_CaretSelectPosition].index;
					text = this.text.Remove(this.m_StringSelectPosition, this.m_StringPosition - this.m_StringSelectPosition);
				}
			}
			if (this.onValidateInput != null)
			{
				input = this.onValidateInput(text, num, input);
			}
			else if (this.characterValidation == TMP_InputField.CharacterValidation.CustomValidator)
			{
				input = this.Validate(text, num, input);
				if (input == '\0')
				{
					return;
				}
				if (!char.IsHighSurrogate(input))
				{
					this.m_CaretSelectPosition = ++this.m_CaretPosition;
				}
				this.SendOnValueChanged();
				this.UpdateLabel();
				return;
			}
			else if (this.characterValidation != TMP_InputField.CharacterValidation.None)
			{
				input = this.Validate(text, num, input);
			}
			if (input == '\0')
			{
				return;
			}
			this.Insert(input);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000D8C8 File Offset: 0x0000BAC8
		private void Insert(char c)
		{
			if (this.m_ReadOnly)
			{
				return;
			}
			string text = c.ToString();
			this.Delete();
			if (this.characterLimit > 0 && this.text.Length >= this.characterLimit)
			{
				return;
			}
			this.m_Text = this.text.Insert(this.m_StringPosition, text);
			if (!char.IsHighSurrogate(c))
			{
				this.m_CaretSelectPosition = ++this.m_CaretPosition;
			}
			this.m_StringSelectPosition = ++this.m_StringPosition;
			this.UpdateTouchKeyboardFromEditChanges();
			this.SendOnValueChanged();
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000D963 File Offset: 0x0000BB63
		private void UpdateTouchKeyboardFromEditChanges()
		{
			if (this.m_SoftKeyboard != null && this.InPlaceEditing())
			{
				this.m_SoftKeyboard.text = this.m_Text;
			}
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000D986 File Offset: 0x0000BB86
		private void SendOnValueChangedAndUpdateLabel()
		{
			this.UpdateLabel();
			this.SendOnValueChanged();
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000D994 File Offset: 0x0000BB94
		private void SendOnValueChanged()
		{
			if (this.onValueChanged != null)
			{
				this.onValueChanged.Invoke(this.text);
			}
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000D9AF File Offset: 0x0000BBAF
		protected void SendOnEndEdit()
		{
			if (this.onEndEdit != null)
			{
				this.onEndEdit.Invoke(this.m_Text);
			}
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000D9CA File Offset: 0x0000BBCA
		protected void SendOnSubmit()
		{
			if (this.onSubmit != null)
			{
				this.onSubmit.Invoke(this.m_Text);
			}
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000D9E5 File Offset: 0x0000BBE5
		protected void SendOnFocus()
		{
			if (this.onSelect != null)
			{
				this.onSelect.Invoke(this.m_Text);
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000DA00 File Offset: 0x0000BC00
		protected void SendOnFocusLost()
		{
			if (this.onDeselect != null)
			{
				this.onDeselect.Invoke(this.m_Text);
			}
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000DA1B File Offset: 0x0000BC1B
		protected void SendOnTextSelection()
		{
			this.m_isSelected = true;
			if (this.onTextSelection != null)
			{
				this.onTextSelection.Invoke(this.m_Text, this.stringPositionInternal, this.stringSelectPositionInternal);
			}
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000DA49 File Offset: 0x0000BC49
		protected void SendOnEndTextSelection()
		{
			if (!this.m_isSelected)
			{
				return;
			}
			if (this.onEndTextSelection != null)
			{
				this.onEndTextSelection.Invoke(this.m_Text, this.stringPositionInternal, this.stringSelectPositionInternal);
			}
			this.m_isSelected = false;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000DA80 File Offset: 0x0000BC80
		protected void SendTouchScreenKeyboardStatusChanged()
		{
			if (this.m_SoftKeyboard != null && this.onTouchScreenKeyboardStatusChanged != null)
			{
				this.onTouchScreenKeyboardStatusChanged.Invoke(this.m_SoftKeyboard.status);
			}
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000DAA8 File Offset: 0x0000BCA8
		protected void UpdateLabel()
		{
			if (this.m_TextComponent != null && this.m_TextComponent.font != null && !this.m_PreventCallback)
			{
				this.m_PreventCallback = true;
				string text;
				if (this.compositionLength > 0 && !this.m_ReadOnly)
				{
					this.Delete();
					if (this.m_RichText)
					{
						text = string.Concat(new string[]
						{
							this.text.Substring(0, this.m_StringPosition),
							"<u>",
							this.compositionString,
							"</u>",
							this.text.Substring(this.m_StringPosition)
						});
					}
					else
					{
						text = this.text.Substring(0, this.m_StringPosition) + this.compositionString + this.text.Substring(this.m_StringPosition);
					}
					this.m_IsCompositionActive = true;
				}
				else
				{
					text = this.text;
					this.m_IsCompositionActive = false;
					this.m_ShouldUpdateIMEWindowPosition = true;
				}
				string text2;
				if (this.inputType == TMP_InputField.InputType.Password)
				{
					text2 = new string(this.asteriskChar, text.Length);
				}
				else
				{
					text2 = text;
				}
				bool flag = string.IsNullOrEmpty(text);
				if (this.m_Placeholder != null)
				{
					this.m_Placeholder.enabled = flag;
				}
				if (!flag && !this.m_ReadOnly)
				{
					this.SetCaretVisible();
				}
				this.m_TextComponent.text = text2 + "\u200b";
				if (this.m_IsDrivenByLayoutComponents)
				{
					LayoutRebuilder.MarkLayoutForRebuild(this.m_RectTransform);
				}
				if (this.m_LineLimit > 0)
				{
					this.m_TextComponent.ForceMeshUpdate(false, false);
					TMP_TextInfo textInfo = this.m_TextComponent.textInfo;
					if (textInfo != null && textInfo.lineCount > this.m_LineLimit)
					{
						int lastCharacterIndex = textInfo.lineInfo[this.m_LineLimit - 1].lastCharacterIndex;
						int num = textInfo.characterInfo[lastCharacterIndex].index + textInfo.characterInfo[lastCharacterIndex].stringLength;
						this.text = text2.Remove(num, text2.Length - num);
						this.m_TextComponent.text = this.text + "\u200b";
					}
				}
				if (this.m_IsTextComponentUpdateRequired || (this.m_VerticalScrollbar && (!this.m_IsCaretPositionDirty || !this.m_IsStringPositionDirty)))
				{
					this.m_IsTextComponentUpdateRequired = false;
					this.m_TextComponent.ForceMeshUpdate(false, false);
				}
				this.MarkGeometryAsDirty();
				this.m_PreventCallback = false;
			}
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000DD1C File Offset: 0x0000BF1C
		private void UpdateScrollbar()
		{
			if (this.m_VerticalScrollbar)
			{
				float num = this.m_TextViewport.rect.height / this.m_TextComponent.preferredHeight;
				this.m_VerticalScrollbar.size = num;
				this.m_VerticalScrollbar.value = this.GetScrollPositionRelativeToViewport();
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000DD73 File Offset: 0x0000BF73
		private void OnScrollbarValueChange(float value)
		{
			if (value < 0f || value > 1f)
			{
				return;
			}
			this.AdjustTextPositionRelativeToViewport(value);
			this.m_ScrollPosition = value;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000DD94 File Offset: 0x0000BF94
		private void UpdateMaskRegions()
		{
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000DD98 File Offset: 0x0000BF98
		private void AdjustTextPositionRelativeToViewport(float relativePosition)
		{
			if (this.m_TextViewport == null)
			{
				return;
			}
			TMP_TextInfo textInfo = this.m_TextComponent.textInfo;
			if (textInfo == null || textInfo.lineInfo == null || textInfo.lineCount == 0 || textInfo.lineCount > textInfo.lineInfo.Length)
			{
				return;
			}
			float num = 0f;
			float num2 = this.m_TextComponent.preferredHeight;
			VerticalAlignmentOptions verticalAlignment = this.m_TextComponent.verticalAlignment;
			if (verticalAlignment <= VerticalAlignmentOptions.Bottom)
			{
				if (verticalAlignment != VerticalAlignmentOptions.Top)
				{
					if (verticalAlignment != VerticalAlignmentOptions.Middle)
					{
						if (verticalAlignment == VerticalAlignmentOptions.Bottom)
						{
							num = 1f;
						}
					}
					else
					{
						num = 0.5f;
					}
				}
				else
				{
					num = 0f;
				}
			}
			else if (verticalAlignment != VerticalAlignmentOptions.Baseline)
			{
				if (verticalAlignment != VerticalAlignmentOptions.Geometry)
				{
					if (verticalAlignment == VerticalAlignmentOptions.Capline)
					{
						num = 0.5f;
					}
				}
				else
				{
					num = 0.5f;
					num2 = this.m_TextComponent.bounds.size.y;
				}
			}
			this.m_TextComponent.rectTransform.anchoredPosition = new Vector2(this.m_TextComponent.rectTransform.anchoredPosition.x, (num2 - this.m_TextViewport.rect.height) * (relativePosition - num));
			this.AssignPositioningIfNeeded();
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000DEC8 File Offset: 0x0000C0C8
		private int GetCaretPositionFromStringIndex(int stringIndex)
		{
			int characterCount = this.m_TextComponent.textInfo.characterCount;
			for (int i = 0; i < characterCount; i++)
			{
				if (this.m_TextComponent.textInfo.characterInfo[i].index >= stringIndex)
				{
					return i;
				}
			}
			return characterCount;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000DF14 File Offset: 0x0000C114
		private int GetMinCaretPositionFromStringIndex(int stringIndex)
		{
			int characterCount = this.m_TextComponent.textInfo.characterCount;
			for (int i = 0; i < characterCount; i++)
			{
				if (stringIndex < this.m_TextComponent.textInfo.characterInfo[i].index + this.m_TextComponent.textInfo.characterInfo[i].stringLength)
				{
					return i;
				}
			}
			return characterCount;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000DF7C File Offset: 0x0000C17C
		private int GetMaxCaretPositionFromStringIndex(int stringIndex)
		{
			int characterCount = this.m_TextComponent.textInfo.characterCount;
			for (int i = 0; i < characterCount; i++)
			{
				if (this.m_TextComponent.textInfo.characterInfo[i].index >= stringIndex)
				{
					return i;
				}
			}
			return characterCount;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000DFC7 File Offset: 0x0000C1C7
		private int GetStringIndexFromCaretPosition(int caretPosition)
		{
			this.ClampCaretPos(ref caretPosition);
			return this.m_TextComponent.textInfo.characterInfo[caretPosition].index;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000DFEC File Offset: 0x0000C1EC
		private void UpdateStringIndexFromCaretPosition()
		{
			this.stringPositionInternal = this.GetStringIndexFromCaretPosition(this.m_CaretPosition);
			this.stringSelectPositionInternal = this.GetStringIndexFromCaretPosition(this.m_CaretSelectPosition);
			this.m_IsStringPositionDirty = false;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000E019 File Offset: 0x0000C219
		private void UpdateCaretPositionFromStringIndex()
		{
			this.caretPositionInternal = this.GetCaretPositionFromStringIndex(this.stringPositionInternal);
			this.caretSelectPositionInternal = this.GetCaretPositionFromStringIndex(this.stringSelectPositionInternal);
			this.m_IsCaretPositionDirty = false;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000E046 File Offset: 0x0000C246
		public void ForceLabelUpdate()
		{
			this.UpdateLabel();
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000E04E File Offset: 0x0000C24E
		private void MarkGeometryAsDirty()
		{
			CanvasUpdateRegistry.RegisterCanvasElementForGraphicRebuild(this);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000E056 File Offset: 0x0000C256
		public virtual void Rebuild(CanvasUpdate update)
		{
			if (update == CanvasUpdate.LatePreRender)
			{
				this.UpdateGeometry();
			}
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000E062 File Offset: 0x0000C262
		public virtual void LayoutComplete()
		{
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000E064 File Offset: 0x0000C264
		public virtual void GraphicUpdateComplete()
		{
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000E066 File Offset: 0x0000C266
		private void UpdateGeometry()
		{
			if (!this.InPlaceEditing() && !this.isUWP())
			{
				return;
			}
			if (this.m_CachedInputRenderer == null)
			{
				return;
			}
			this.OnFillVBO(this.mesh);
			this.m_CachedInputRenderer.SetMesh(this.mesh);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000E0A8 File Offset: 0x0000C2A8
		private void AssignPositioningIfNeeded()
		{
			if (this.m_TextComponent != null && this.caretRectTrans != null && (this.caretRectTrans.localPosition != this.m_TextComponent.rectTransform.localPosition || this.caretRectTrans.localRotation != this.m_TextComponent.rectTransform.localRotation || this.caretRectTrans.localScale != this.m_TextComponent.rectTransform.localScale || this.caretRectTrans.anchorMin != this.m_TextComponent.rectTransform.anchorMin || this.caretRectTrans.anchorMax != this.m_TextComponent.rectTransform.anchorMax || this.caretRectTrans.anchoredPosition != this.m_TextComponent.rectTransform.anchoredPosition || this.caretRectTrans.sizeDelta != this.m_TextComponent.rectTransform.sizeDelta || this.caretRectTrans.pivot != this.m_TextComponent.rectTransform.pivot))
			{
				this.caretRectTrans.localPosition = this.m_TextComponent.rectTransform.localPosition;
				this.caretRectTrans.localRotation = this.m_TextComponent.rectTransform.localRotation;
				this.caretRectTrans.localScale = this.m_TextComponent.rectTransform.localScale;
				this.caretRectTrans.anchorMin = this.m_TextComponent.rectTransform.anchorMin;
				this.caretRectTrans.anchorMax = this.m_TextComponent.rectTransform.anchorMax;
				this.caretRectTrans.anchoredPosition = this.m_TextComponent.rectTransform.anchoredPosition;
				this.caretRectTrans.sizeDelta = this.m_TextComponent.rectTransform.sizeDelta;
				this.caretRectTrans.pivot = this.m_TextComponent.rectTransform.pivot;
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000E2D0 File Offset: 0x0000C4D0
		private void OnFillVBO(Mesh vbo)
		{
			using (VertexHelper vertexHelper = new VertexHelper())
			{
				if (!this.isFocused && !this.m_SelectionStillActive)
				{
					vertexHelper.FillMesh(vbo);
				}
				else
				{
					if (this.m_IsStringPositionDirty)
					{
						this.UpdateStringIndexFromCaretPosition();
					}
					if (this.m_IsCaretPositionDirty)
					{
						this.UpdateCaretPositionFromStringIndex();
					}
					if (!this.hasSelection)
					{
						this.GenerateCaret(vertexHelper, Vector2.zero);
						this.SendOnEndTextSelection();
					}
					else
					{
						this.GenerateHighlight(vertexHelper, Vector2.zero);
						this.SendOnTextSelection();
					}
					vertexHelper.FillMesh(vbo);
				}
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000E36C File Offset: 0x0000C56C
		private void GenerateCaret(VertexHelper vbo, Vector2 roundingOffset)
		{
			if (!this.m_CaretVisible || this.m_TextComponent.canvas == null || this.m_ReadOnly)
			{
				return;
			}
			if (this.m_CursorVerts == null)
			{
				this.CreateCursorVerts();
			}
			Vector2 zero = Vector2.zero;
			if (this.caretPositionInternal >= this.m_TextComponent.textInfo.characterInfo.Length || this.caretPositionInternal < 0)
			{
				return;
			}
			int lineNumber = this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal].lineNumber;
			TMP_CharacterInfo tmp_CharacterInfo;
			float num;
			if (this.caretPositionInternal == this.m_TextComponent.textInfo.lineInfo[lineNumber].firstCharacterIndex)
			{
				tmp_CharacterInfo = this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal];
				num = tmp_CharacterInfo.ascender - tmp_CharacterInfo.descender;
				if (this.m_TextComponent.verticalAlignment == VerticalAlignmentOptions.Geometry)
				{
					zero = new Vector2(tmp_CharacterInfo.origin, 0f - num / 2f);
				}
				else
				{
					zero = new Vector2(tmp_CharacterInfo.origin, tmp_CharacterInfo.descender);
				}
			}
			else
			{
				tmp_CharacterInfo = this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal - 1];
				num = tmp_CharacterInfo.ascender - tmp_CharacterInfo.descender;
				if (this.m_TextComponent.verticalAlignment == VerticalAlignmentOptions.Geometry)
				{
					zero = new Vector2(tmp_CharacterInfo.xAdvance, 0f - num / 2f);
				}
				else
				{
					zero = new Vector2(tmp_CharacterInfo.xAdvance, tmp_CharacterInfo.descender);
				}
			}
			if (this.m_SoftKeyboard != null && this.compositionLength == 0)
			{
				int num2 = this.m_StringPosition;
				int num3 = ((this.m_SoftKeyboard.text == null) ? 0 : this.m_SoftKeyboard.text.Length);
				if (num2 < 0)
				{
					num2 = 0;
				}
				if (num2 > num3)
				{
					num2 = num3;
				}
				this.m_SoftKeyboard.selection = new RangeInt(num2, 0);
			}
			if ((this.isFocused && zero != this.m_LastPosition) || this.m_forceRectTransformAdjustment || this.m_HasTextBeenRemoved)
			{
				this.AdjustRectTransformRelativeToViewport(zero, num, tmp_CharacterInfo.isVisible);
			}
			this.m_LastPosition = zero;
			float num4 = zero.y + num;
			float num5 = num4 - num;
			TMP_FontAsset font = this.m_TextComponent.font;
			float num6 = this.m_TextComponent.fontSize / font.m_FaceInfo.pointSize * font.m_FaceInfo.scale;
			float num7 = (float)this.m_CaretWidth * font.faceInfo.lineHeight * num6 * 0.05f;
			this.m_CursorVerts[0].position = new Vector3(zero.x, num5, 0f);
			this.m_CursorVerts[1].position = new Vector3(zero.x, num4, 0f);
			this.m_CursorVerts[2].position = new Vector3(zero.x + num7, num4, 0f);
			this.m_CursorVerts[3].position = new Vector3(zero.x + num7, num5, 0f);
			this.m_CursorVerts[0].color = this.caretColor;
			this.m_CursorVerts[1].color = this.caretColor;
			this.m_CursorVerts[2].color = this.caretColor;
			this.m_CursorVerts[3].color = this.caretColor;
			vbo.AddUIVertexQuad(this.m_CursorVerts);
			if (this.m_ShouldUpdateIMEWindowPosition || lineNumber != this.m_PreviousIMEInsertionLine)
			{
				this.m_ShouldUpdateIMEWindowPosition = false;
				this.m_PreviousIMEInsertionLine = lineNumber;
				Camera camera;
				if (this.m_TextComponent.canvas.renderMode == RenderMode.ScreenSpaceOverlay)
				{
					camera = null;
				}
				else
				{
					camera = this.m_TextComponent.canvas.worldCamera;
					if (camera == null)
					{
						camera = Camera.current;
					}
				}
				Vector3 vector = this.m_CachedInputRenderer.gameObject.transform.TransformPoint(this.m_CursorVerts[0].position);
				Vector2 vector2 = RectTransformUtility.WorldToScreenPoint(camera, vector);
				vector2.y = (float)Screen.height - vector2.y;
				if (this.inputSystem != null)
				{
					this.inputSystem.compositionCursorPos = vector2;
				}
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000E7CC File Offset: 0x0000C9CC
		private void CreateCursorVerts()
		{
			this.m_CursorVerts = new UIVertex[4];
			for (int i = 0; i < this.m_CursorVerts.Length; i++)
			{
				this.m_CursorVerts[i] = UIVertex.simpleVert;
				this.m_CursorVerts[i].uv0 = Vector2.zero;
			}
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000E824 File Offset: 0x0000CA24
		private void GenerateHighlight(VertexHelper vbo, Vector2 roundingOffset)
		{
			this.UpdateMaskRegions();
			TMP_TextInfo textInfo = this.m_TextComponent.textInfo;
			if (textInfo.characterCount == 0)
			{
				return;
			}
			this.m_CaretPosition = this.GetCaretPositionFromStringIndex(this.stringPositionInternal);
			this.m_CaretSelectPosition = this.GetCaretPositionFromStringIndex(this.stringSelectPositionInternal);
			if (this.m_SoftKeyboard != null && this.compositionLength == 0)
			{
				int num = ((this.m_CaretPosition < this.m_CaretSelectPosition) ? textInfo.characterInfo[this.m_CaretPosition].index : textInfo.characterInfo[this.m_CaretSelectPosition].index);
				int num2 = ((this.m_CaretPosition < this.m_CaretSelectPosition) ? (this.stringSelectPositionInternal - num) : (this.stringPositionInternal - num));
				this.m_SoftKeyboard.selection = new RangeInt(num, num2);
			}
			Vector2 vector;
			float num3;
			if (this.m_CaretSelectPosition < textInfo.characterCount)
			{
				vector = new Vector2(textInfo.characterInfo[this.m_CaretSelectPosition].origin, textInfo.characterInfo[this.m_CaretSelectPosition].descender);
				num3 = textInfo.characterInfo[this.m_CaretSelectPosition].ascender - textInfo.characterInfo[this.m_CaretSelectPosition].descender;
			}
			else
			{
				vector = new Vector2(textInfo.characterInfo[this.m_CaretSelectPosition - 1].xAdvance, textInfo.characterInfo[this.m_CaretSelectPosition - 1].descender);
				num3 = textInfo.characterInfo[this.m_CaretSelectPosition - 1].ascender - textInfo.characterInfo[this.m_CaretSelectPosition - 1].descender;
			}
			this.AdjustRectTransformRelativeToViewport(vector, num3, true);
			int num4 = Mathf.Max(0, this.m_CaretPosition);
			int num5 = Mathf.Max(0, this.m_CaretSelectPosition);
			if (num4 > num5)
			{
				int num6 = num4;
				num4 = num5;
				num5 = num6;
			}
			num5--;
			int num7 = textInfo.characterInfo[num4].lineNumber;
			int num8 = textInfo.lineInfo[num7].lastCharacterIndex;
			UIVertex simpleVert = UIVertex.simpleVert;
			simpleVert.uv0 = Vector2.zero;
			simpleVert.color = this.selectionColor;
			int num9 = num4;
			while (num9 <= num5 && num9 < textInfo.characterCount)
			{
				if (num9 == num8 || num9 == num5)
				{
					TMP_CharacterInfo tmp_CharacterInfo = textInfo.characterInfo[num4];
					TMP_CharacterInfo tmp_CharacterInfo2 = textInfo.characterInfo[num9];
					if (num9 > 0 && tmp_CharacterInfo2.character == '\n' && textInfo.characterInfo[num9 - 1].character == '\r')
					{
						tmp_CharacterInfo2 = textInfo.characterInfo[num9 - 1];
					}
					Vector2 vector2 = new Vector2(tmp_CharacterInfo.origin, textInfo.lineInfo[num7].ascender);
					Vector2 vector3 = new Vector2(tmp_CharacterInfo2.xAdvance, textInfo.lineInfo[num7].descender);
					int currentVertCount = vbo.currentVertCount;
					simpleVert.position = new Vector3(vector2.x, vector3.y, 0f);
					vbo.AddVert(simpleVert);
					simpleVert.position = new Vector3(vector3.x, vector3.y, 0f);
					vbo.AddVert(simpleVert);
					simpleVert.position = new Vector3(vector3.x, vector2.y, 0f);
					vbo.AddVert(simpleVert);
					simpleVert.position = new Vector3(vector2.x, vector2.y, 0f);
					vbo.AddVert(simpleVert);
					vbo.AddTriangle(currentVertCount, currentVertCount + 1, currentVertCount + 2);
					vbo.AddTriangle(currentVertCount + 2, currentVertCount + 3, currentVertCount);
					num4 = num9 + 1;
					num7++;
					if (num7 < textInfo.lineCount)
					{
						num8 = textInfo.lineInfo[num7].lastCharacterIndex;
					}
				}
				num9++;
			}
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000EC10 File Offset: 0x0000CE10
		private void AdjustRectTransformRelativeToViewport(Vector2 startPosition, float height, bool isCharVisible)
		{
			if (this.m_TextViewport == null)
			{
				return;
			}
			Vector3 localPosition = base.transform.localPosition;
			Vector3 localPosition2 = this.m_TextComponent.rectTransform.localPosition;
			Vector3 localPosition3 = this.m_TextViewport.localPosition;
			Rect rect = this.m_TextViewport.rect;
			Vector2 vector = new Vector2(startPosition.x + localPosition2.x + localPosition3.x + localPosition.x, startPosition.y + localPosition2.y + localPosition3.y + localPosition.y);
			Rect rect2 = new Rect(localPosition.x + localPosition3.x + rect.x, localPosition.y + localPosition3.y + rect.y, rect.width, rect.height);
			float num = rect2.xMax - (vector.x + this.m_TextComponent.margin.z + (float)this.m_CaretWidth);
			if (num < 0f && (!this.multiLine || (this.multiLine && isCharVisible)))
			{
				this.m_TextComponent.rectTransform.anchoredPosition += new Vector2(num, 0f);
				this.AssignPositioningIfNeeded();
			}
			float num2 = vector.x - this.m_TextComponent.margin.x - rect2.xMin;
			if (num2 < 0f)
			{
				this.m_TextComponent.rectTransform.anchoredPosition += new Vector2(-num2, 0f);
				this.AssignPositioningIfNeeded();
			}
			if (this.m_LineType != TMP_InputField.LineType.SingleLine)
			{
				float num3 = rect2.yMax - (vector.y + height);
				if (num3 < -0.0001f)
				{
					this.m_TextComponent.rectTransform.anchoredPosition += new Vector2(0f, num3);
					this.AssignPositioningIfNeeded();
				}
				float num4 = vector.y - rect2.yMin;
				if (num4 < 0f)
				{
					this.m_TextComponent.rectTransform.anchoredPosition -= new Vector2(0f, num4);
					this.AssignPositioningIfNeeded();
				}
			}
			if (this.m_HasTextBeenRemoved)
			{
				float x = this.m_TextComponent.rectTransform.anchoredPosition.x;
				float num5 = localPosition.x + localPosition3.x + localPosition2.x + this.m_TextComponent.textInfo.characterInfo[0].origin - this.m_TextComponent.margin.x;
				int num6 = this.ClampArrayIndex(this.m_TextComponent.textInfo.characterCount - 1);
				float num7 = localPosition.x + localPosition3.x + localPosition2.x + this.m_TextComponent.textInfo.characterInfo[num6].origin + this.m_TextComponent.margin.z + (float)this.m_CaretWidth;
				if (x > 0.0001f && num5 > rect2.xMin)
				{
					float num8 = rect2.xMin - num5;
					if (x < -num8)
					{
						num8 = -x;
					}
					this.m_TextComponent.rectTransform.anchoredPosition += new Vector2(num8, 0f);
					this.AssignPositioningIfNeeded();
				}
				else if (x < -0.0001f && num7 < rect2.xMax)
				{
					float num9 = rect2.xMax - num7;
					if (-x < num9)
					{
						num9 = -x;
					}
					this.m_TextComponent.rectTransform.anchoredPosition += new Vector2(num9, 0f);
					this.AssignPositioningIfNeeded();
				}
				this.m_HasTextBeenRemoved = false;
			}
			this.m_forceRectTransformAdjustment = false;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000EFD0 File Offset: 0x0000D1D0
		protected char Validate(string text, int pos, char ch)
		{
			if (this.characterValidation == TMP_InputField.CharacterValidation.None || !base.enabled)
			{
				return ch;
			}
			if (this.characterValidation == TMP_InputField.CharacterValidation.Integer || this.characterValidation == TMP_InputField.CharacterValidation.Decimal)
			{
				bool flag = pos == 0 && text.Length > 0 && text[0] == '-';
				bool flag2 = this.stringPositionInternal == 0 || this.stringSelectPositionInternal == 0;
				if (!flag)
				{
					if (ch >= '0' && ch <= '9')
					{
						return ch;
					}
					if (ch == '-' && (pos == 0 || flag2))
					{
						return ch;
					}
					string numberDecimalSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
					if (ch == Convert.ToChar(numberDecimalSeparator) && this.characterValidation == TMP_InputField.CharacterValidation.Decimal && !text.Contains(numberDecimalSeparator))
					{
						return ch;
					}
				}
			}
			else if (this.characterValidation == TMP_InputField.CharacterValidation.Digit)
			{
				if (ch >= '0' && ch <= '9')
				{
					return ch;
				}
			}
			else if (this.characterValidation == TMP_InputField.CharacterValidation.Alphanumeric)
			{
				if (ch >= 'A' && ch <= 'Z')
				{
					return ch;
				}
				if (ch >= 'a' && ch <= 'z')
				{
					return ch;
				}
				if (ch >= '0' && ch <= '9')
				{
					return ch;
				}
			}
			else if (this.characterValidation == TMP_InputField.CharacterValidation.Name)
			{
				char c = ((text.Length > 0) ? text[Mathf.Clamp(pos - 1, 0, text.Length - 1)] : ' ');
				char c2 = ((text.Length > 0) ? text[Mathf.Clamp(pos, 0, text.Length - 1)] : ' ');
				char c3 = ((text.Length > 0) ? text[Mathf.Clamp(pos + 1, 0, text.Length - 1)] : '\n');
				if (char.IsLetter(ch))
				{
					if (char.IsLower(ch) && pos == 0)
					{
						return char.ToUpper(ch);
					}
					if (char.IsLower(ch) && (c == ' ' || c == '-'))
					{
						return char.ToUpper(ch);
					}
					if (char.IsUpper(ch) && pos > 0 && c != ' ' && c != '\'' && c != '-' && !char.IsLower(c))
					{
						return char.ToLower(ch);
					}
					if (char.IsUpper(ch) && char.IsUpper(c2))
					{
						return '\0';
					}
					return ch;
				}
				else
				{
					if (ch == '\'' && c2 != ' ' && c2 != '\'' && c3 != '\'' && !text.Contains("'"))
					{
						return ch;
					}
					if (char.IsLetter(c) && ch == '-' && c2 != '-')
					{
						return ch;
					}
					if ((ch == ' ' || ch == '-') && pos != 0 && c != ' ' && c != '\'' && c != '-' && c2 != ' ' && c2 != '\'' && c2 != '-' && c3 != ' ' && c3 != '\'' && c3 != '-')
					{
						return ch;
					}
				}
			}
			else if (this.characterValidation == TMP_InputField.CharacterValidation.EmailAddress)
			{
				if (ch >= 'A' && ch <= 'Z')
				{
					return ch;
				}
				if (ch >= 'a' && ch <= 'z')
				{
					return ch;
				}
				if (ch >= '0' && ch <= '9')
				{
					return ch;
				}
				if (ch == '@' && text.IndexOf('@') == -1)
				{
					return ch;
				}
				if ("!#$%&'*+-/=?^_`{|}~".IndexOf(ch) != -1)
				{
					return ch;
				}
				if (ch == '.')
				{
					int num = (int)((text.Length > 0) ? text[Mathf.Clamp(pos, 0, text.Length - 1)] : ' ');
					char c4 = ((text.Length > 0) ? text[Mathf.Clamp(pos + 1, 0, text.Length - 1)] : '\n');
					if (num != 46 && c4 != '.')
					{
						return ch;
					}
				}
			}
			else if (this.characterValidation == TMP_InputField.CharacterValidation.Regex)
			{
				if (Regex.IsMatch(ch.ToString(), this.m_RegexValue))
				{
					return ch;
				}
			}
			else if (this.characterValidation == TMP_InputField.CharacterValidation.CustomValidator && this.m_InputValidator != null)
			{
				char c5 = this.m_InputValidator.Validate(ref text, ref pos, ch);
				this.m_Text = text;
				this.stringSelectPositionInternal = (this.stringPositionInternal = pos);
				return c5;
			}
			return '\0';
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000F370 File Offset: 0x0000D570
		public void ActivateInputField()
		{
			if (this.m_TextComponent == null || this.m_TextComponent.font == null || !this.IsActive() || !this.IsInteractable())
			{
				return;
			}
			if (this.isFocused && this.m_SoftKeyboard != null && !this.m_SoftKeyboard.active)
			{
				this.m_SoftKeyboard.active = true;
				this.m_SoftKeyboard.text = this.m_Text;
			}
			this.m_ShouldActivateNextUpdate = true;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000F3F0 File Offset: 0x0000D5F0
		private void ActivateInputFieldInternal()
		{
			if (EventSystem.current == null)
			{
				return;
			}
			if (EventSystem.current.currentSelectedGameObject != base.gameObject)
			{
				EventSystem.current.SetSelectedGameObject(base.gameObject);
			}
			this.m_TouchKeyboardAllowsInPlaceEditing = !TMP_InputField.s_IsQuestDevice && TouchScreenKeyboard.isInPlaceEditingAllowed;
			if (this.TouchScreenKeyboardShouldBeUsed() && !this.shouldHideSoftKeyboard)
			{
				if (this.inputSystem != null && this.inputSystem.touchSupported)
				{
					TouchScreenKeyboard.hideInput = this.shouldHideMobileInput;
				}
				if (!this.shouldHideSoftKeyboard && !this.m_ReadOnly)
				{
					this.m_SoftKeyboard = ((this.inputType == TMP_InputField.InputType.Password) ? TouchScreenKeyboard.Open(this.m_Text, this.keyboardType, false, this.multiLine, true, this.isAlert, "", this.characterLimit) : TouchScreenKeyboard.Open(this.m_Text, this.keyboardType, this.inputType == TMP_InputField.InputType.AutoCorrect, this.multiLine, false, this.isAlert, "", this.characterLimit));
					this.OnFocus();
					if (this.m_SoftKeyboard != null && this.m_SoftKeyboard.canSetSelection)
					{
						int num = ((this.stringPositionInternal < this.stringSelectPositionInternal) ? (this.stringSelectPositionInternal - this.stringPositionInternal) : (this.stringPositionInternal - this.stringSelectPositionInternal));
						this.m_SoftKeyboard.selection = new RangeInt((this.stringPositionInternal < this.stringSelectPositionInternal) ? this.stringPositionInternal : this.stringSelectPositionInternal, num);
					}
				}
			}
			else
			{
				if (!this.TouchScreenKeyboardShouldBeUsed() && !this.m_ReadOnly && this.inputSystem != null)
				{
					this.inputSystem.imeCompositionMode = IMECompositionMode.On;
				}
				this.OnFocus();
			}
			this.m_AllowInput = true;
			this.m_OriginalText = this.text;
			this.m_WasCanceled = false;
			this.SetCaretVisible();
			this.UpdateLabel();
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000F5D7 File Offset: 0x0000D7D7
		public override void OnSelect(BaseEventData eventData)
		{
			base.OnSelect(eventData);
			this.SendOnFocus();
			if (this.shouldActivateOnSelect)
			{
				this.ActivateInputField();
			}
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000F5F4 File Offset: 0x0000D7F4
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.ActivateInputField();
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000F605 File Offset: 0x0000D805
		public void OnControlClick()
		{
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000F607 File Offset: 0x0000D807
		public void ReleaseSelection()
		{
			this.m_SelectionStillActive = false;
			this.m_ReleaseSelection = false;
			this.m_PreviouslySelectedObject = null;
			this.MarkGeometryAsDirty();
			this.SendOnEndEdit();
			this.SendOnEndTextSelection();
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000F630 File Offset: 0x0000D830
		public void DeactivateInputField(bool clearSelection = false)
		{
			if (!this.m_AllowInput)
			{
				return;
			}
			this.m_HasDoneFocusTransition = false;
			this.m_AllowInput = false;
			if (this.m_Placeholder != null)
			{
				this.m_Placeholder.enabled = string.IsNullOrEmpty(this.m_Text);
			}
			if (this.m_TextComponent != null && this.IsInteractable())
			{
				if (this.m_WasCanceled && this.m_RestoreOriginalTextOnEscape && !this.m_IsKeyboardBeingClosedInHoloLens)
				{
					this.text = this.m_OriginalText;
				}
				if (this.m_SoftKeyboard != null)
				{
					this.m_SoftKeyboard.active = false;
					this.m_SoftKeyboard = null;
				}
				this.m_SelectionStillActive = true;
				if ((this.m_ResetOnDeActivation || this.m_ReleaseSelection || clearSelection) && this.m_VerticalScrollbar == null)
				{
					this.ReleaseSelection();
				}
				if (this.inputSystem != null)
				{
					this.inputSystem.imeCompositionMode = IMECompositionMode.Auto;
				}
				this.m_IsKeyboardBeingClosedInHoloLens = false;
			}
			this.MarkGeometryAsDirty();
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000F72A File Offset: 0x0000D92A
		public override void OnDeselect(BaseEventData eventData)
		{
			this.DeactivateInputField(false);
			base.OnDeselect(eventData);
			this.SendOnFocusLost();
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000F740 File Offset: 0x0000D940
		public virtual void OnSubmit(BaseEventData eventData)
		{
			if (!this.IsActive() || !this.IsInteractable())
			{
				return;
			}
			if (!this.isFocused)
			{
				this.m_ShouldActivateNextUpdate = true;
			}
			this.SendOnSubmit();
			this.DeactivateInputField(false);
			if (eventData != null)
			{
				eventData.Use();
			}
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000F778 File Offset: 0x0000D978
		public virtual void OnCancel(BaseEventData eventData)
		{
			if (!this.IsActive() || !this.IsInteractable())
			{
				return;
			}
			if (!this.isFocused)
			{
				this.m_ShouldActivateNextUpdate = true;
			}
			this.m_WasCanceled = true;
			this.DeactivateInputField(false);
			eventData.Use();
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000F7AE File Offset: 0x0000D9AE
		public override void OnMove(AxisEventData eventData)
		{
			if (!this.m_AllowInput)
			{
				base.OnMove(eventData);
			}
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000F7C0 File Offset: 0x0000D9C0
		private void EnforceContentType()
		{
			switch (this.contentType)
			{
			case TMP_InputField.ContentType.Standard:
				this.m_InputType = TMP_InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.Default;
				this.m_CharacterValidation = TMP_InputField.CharacterValidation.None;
				break;
			case TMP_InputField.ContentType.Autocorrected:
				this.m_InputType = TMP_InputField.InputType.AutoCorrect;
				this.m_KeyboardType = TouchScreenKeyboardType.Default;
				this.m_CharacterValidation = TMP_InputField.CharacterValidation.None;
				break;
			case TMP_InputField.ContentType.IntegerNumber:
				this.m_LineType = TMP_InputField.LineType.SingleLine;
				this.m_InputType = TMP_InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.NumberPad;
				this.m_CharacterValidation = TMP_InputField.CharacterValidation.Integer;
				break;
			case TMP_InputField.ContentType.DecimalNumber:
				this.m_LineType = TMP_InputField.LineType.SingleLine;
				this.m_InputType = TMP_InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.NumbersAndPunctuation;
				this.m_CharacterValidation = TMP_InputField.CharacterValidation.Decimal;
				break;
			case TMP_InputField.ContentType.Alphanumeric:
				this.m_LineType = TMP_InputField.LineType.SingleLine;
				this.m_InputType = TMP_InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.ASCIICapable;
				this.m_CharacterValidation = TMP_InputField.CharacterValidation.Alphanumeric;
				break;
			case TMP_InputField.ContentType.Name:
				this.m_LineType = TMP_InputField.LineType.SingleLine;
				this.m_InputType = TMP_InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.Default;
				this.m_CharacterValidation = TMP_InputField.CharacterValidation.Name;
				break;
			case TMP_InputField.ContentType.EmailAddress:
				this.m_LineType = TMP_InputField.LineType.SingleLine;
				this.m_InputType = TMP_InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.EmailAddress;
				this.m_CharacterValidation = TMP_InputField.CharacterValidation.EmailAddress;
				break;
			case TMP_InputField.ContentType.Password:
				this.m_LineType = TMP_InputField.LineType.SingleLine;
				this.m_InputType = TMP_InputField.InputType.Password;
				this.m_KeyboardType = TouchScreenKeyboardType.Default;
				this.m_CharacterValidation = TMP_InputField.CharacterValidation.None;
				break;
			case TMP_InputField.ContentType.Pin:
				this.m_LineType = TMP_InputField.LineType.SingleLine;
				this.m_InputType = TMP_InputField.InputType.Password;
				this.m_KeyboardType = TouchScreenKeyboardType.NumberPad;
				this.m_CharacterValidation = TMP_InputField.CharacterValidation.Digit;
				break;
			}
			this.SetTextComponentWrapMode();
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000F913 File Offset: 0x0000DB13
		private void SetTextComponentWrapMode()
		{
			if (this.m_TextComponent == null)
			{
				return;
			}
			if (this.multiLine)
			{
				this.m_TextComponent.textWrappingMode = TextWrappingModes.Normal;
				return;
			}
			this.m_TextComponent.textWrappingMode = TextWrappingModes.PreserveWhitespaceNoWrap;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000F945 File Offset: 0x0000DB45
		private void SetTextComponentRichTextMode()
		{
			if (this.m_TextComponent == null)
			{
				return;
			}
			this.m_TextComponent.richText = this.m_RichText;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000F968 File Offset: 0x0000DB68
		private void SetToCustomIfContentTypeIsNot(params TMP_InputField.ContentType[] allowedContentTypes)
		{
			if (this.contentType == TMP_InputField.ContentType.Custom)
			{
				return;
			}
			for (int i = 0; i < allowedContentTypes.Length; i++)
			{
				if (this.contentType == allowedContentTypes[i])
				{
					return;
				}
			}
			this.contentType = TMP_InputField.ContentType.Custom;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000F9A2 File Offset: 0x0000DBA2
		private void SetToCustom()
		{
			if (this.contentType == TMP_InputField.ContentType.Custom)
			{
				return;
			}
			this.contentType = TMP_InputField.ContentType.Custom;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000F9B7 File Offset: 0x0000DBB7
		private void SetToCustom(TMP_InputField.CharacterValidation characterValidation)
		{
			if (this.contentType == TMP_InputField.ContentType.Custom)
			{
				return;
			}
			this.contentType = TMP_InputField.ContentType.Custom;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000F9D2 File Offset: 0x0000DBD2
		protected override void DoStateTransition(Selectable.SelectionState state, bool instant)
		{
			if (this.m_HasDoneFocusTransition)
			{
				state = Selectable.SelectionState.Selected;
			}
			else if (state == Selectable.SelectionState.Pressed)
			{
				this.m_HasDoneFocusTransition = true;
			}
			base.DoStateTransition(state, instant);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000F9F4 File Offset: 0x0000DBF4
		public virtual void CalculateLayoutInputHorizontal()
		{
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000F9F6 File Offset: 0x0000DBF6
		public virtual void CalculateLayoutInputVertical()
		{
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000F9F8 File Offset: 0x0000DBF8
		public virtual float minWidth
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000FA00 File Offset: 0x0000DC00
		public virtual float preferredWidth
		{
			get
			{
				if (this.textComponent == null)
				{
					return 0f;
				}
				float num = 0f;
				if (this.m_LayoutGroup != null)
				{
					num = (float)this.m_LayoutGroup.padding.horizontal;
				}
				if (this.m_TextViewport != null)
				{
					num += this.m_TextViewport.offsetMin.x - this.m_TextViewport.offsetMax.x;
				}
				return this.m_TextComponent.preferredWidth + num;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x0000FA86 File Offset: 0x0000DC86
		public virtual float flexibleWidth
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000FA8D File Offset: 0x0000DC8D
		public virtual float minHeight
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002AA RID: 682 RVA: 0x0000FA94 File Offset: 0x0000DC94
		public virtual float preferredHeight
		{
			get
			{
				if (this.textComponent == null)
				{
					return 0f;
				}
				float num = 0f;
				if (this.m_LayoutGroup != null)
				{
					num = (float)this.m_LayoutGroup.padding.vertical;
				}
				if (this.m_TextViewport != null)
				{
					num += this.m_TextViewport.offsetMin.y - this.m_TextViewport.offsetMax.y;
				}
				return this.m_TextComponent.preferredHeight + num;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000FB1A File Offset: 0x0000DD1A
		public virtual float flexibleHeight
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000FB21 File Offset: 0x0000DD21
		public virtual int layoutPriority
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000FB24 File Offset: 0x0000DD24
		public void SetGlobalPointSize(float pointSize)
		{
			TMP_Text tmp_Text = this.m_Placeholder as TMP_Text;
			if (tmp_Text != null)
			{
				tmp_Text.fontSize = pointSize;
			}
			this.textComponent.fontSize = pointSize;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000FB5C File Offset: 0x0000DD5C
		public void SetGlobalFontAsset(TMP_FontAsset fontAsset)
		{
			TMP_Text tmp_Text = this.m_Placeholder as TMP_Text;
			if (tmp_Text != null)
			{
				tmp_Text.font = fontAsset;
			}
			this.textComponent.font = fontAsset;
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000FBB5 File Offset: 0x0000DDB5
		Transform ICanvasElement.get_transform()
		{
			return base.transform;
		}

		// Token: 0x04000140 RID: 320
		protected TouchScreenKeyboard m_SoftKeyboard;

		// Token: 0x04000141 RID: 321
		private static readonly char[] kSeparators = new char[] { ' ', '.', ',', '\t', '\r', '\n' };

		// Token: 0x04000142 RID: 322
		private static bool s_IsQuestDevice = false;

		// Token: 0x04000143 RID: 323
		protected RectTransform m_RectTransform;

		// Token: 0x04000144 RID: 324
		[SerializeField]
		protected RectTransform m_TextViewport;

		// Token: 0x04000145 RID: 325
		protected RectMask2D m_TextComponentRectMask;

		// Token: 0x04000146 RID: 326
		protected RectMask2D m_TextViewportRectMask;

		// Token: 0x04000147 RID: 327
		[SerializeField]
		protected TMP_Text m_TextComponent;

		// Token: 0x04000148 RID: 328
		protected RectTransform m_TextComponentRectTransform;

		// Token: 0x04000149 RID: 329
		[SerializeField]
		protected Graphic m_Placeholder;

		// Token: 0x0400014A RID: 330
		[SerializeField]
		protected Scrollbar m_VerticalScrollbar;

		// Token: 0x0400014B RID: 331
		[SerializeField]
		protected TMP_ScrollbarEventHandler m_VerticalScrollbarEventHandler;

		// Token: 0x0400014C RID: 332
		private bool m_IsDrivenByLayoutComponents;

		// Token: 0x0400014D RID: 333
		[SerializeField]
		private LayoutGroup m_LayoutGroup;

		// Token: 0x0400014E RID: 334
		private IScrollHandler m_IScrollHandlerParent;

		// Token: 0x0400014F RID: 335
		private float m_ScrollPosition;

		// Token: 0x04000150 RID: 336
		[SerializeField]
		protected float m_ScrollSensitivity = 1f;

		// Token: 0x04000151 RID: 337
		[SerializeField]
		private TMP_InputField.ContentType m_ContentType;

		// Token: 0x04000152 RID: 338
		[SerializeField]
		private TMP_InputField.InputType m_InputType;

		// Token: 0x04000153 RID: 339
		[SerializeField]
		private char m_AsteriskChar = '*';

		// Token: 0x04000154 RID: 340
		[SerializeField]
		private TouchScreenKeyboardType m_KeyboardType;

		// Token: 0x04000155 RID: 341
		[SerializeField]
		private TMP_InputField.LineType m_LineType;

		// Token: 0x04000156 RID: 342
		[SerializeField]
		private bool m_HideMobileInput;

		// Token: 0x04000157 RID: 343
		[SerializeField]
		private bool m_HideSoftKeyboard;

		// Token: 0x04000158 RID: 344
		[SerializeField]
		private TMP_InputField.CharacterValidation m_CharacterValidation;

		// Token: 0x04000159 RID: 345
		[SerializeField]
		private string m_RegexValue = string.Empty;

		// Token: 0x0400015A RID: 346
		[SerializeField]
		private float m_GlobalPointSize = 14f;

		// Token: 0x0400015B RID: 347
		[SerializeField]
		private int m_CharacterLimit;

		// Token: 0x0400015C RID: 348
		[SerializeField]
		private TMP_InputField.SubmitEvent m_OnEndEdit = new TMP_InputField.SubmitEvent();

		// Token: 0x0400015D RID: 349
		[SerializeField]
		private TMP_InputField.SubmitEvent m_OnSubmit = new TMP_InputField.SubmitEvent();

		// Token: 0x0400015E RID: 350
		[SerializeField]
		private TMP_InputField.SelectionEvent m_OnSelect = new TMP_InputField.SelectionEvent();

		// Token: 0x0400015F RID: 351
		[SerializeField]
		private TMP_InputField.SelectionEvent m_OnDeselect = new TMP_InputField.SelectionEvent();

		// Token: 0x04000160 RID: 352
		[SerializeField]
		private TMP_InputField.TextSelectionEvent m_OnTextSelection = new TMP_InputField.TextSelectionEvent();

		// Token: 0x04000161 RID: 353
		[SerializeField]
		private TMP_InputField.TextSelectionEvent m_OnEndTextSelection = new TMP_InputField.TextSelectionEvent();

		// Token: 0x04000162 RID: 354
		[SerializeField]
		private TMP_InputField.OnChangeEvent m_OnValueChanged = new TMP_InputField.OnChangeEvent();

		// Token: 0x04000163 RID: 355
		[SerializeField]
		private TMP_InputField.TouchScreenKeyboardEvent m_OnTouchScreenKeyboardStatusChanged = new TMP_InputField.TouchScreenKeyboardEvent();

		// Token: 0x04000164 RID: 356
		[SerializeField]
		private TMP_InputField.OnValidateInput m_OnValidateInput;

		// Token: 0x04000165 RID: 357
		[SerializeField]
		private Color m_CaretColor = new Color(0.19607843f, 0.19607843f, 0.19607843f, 1f);

		// Token: 0x04000166 RID: 358
		[SerializeField]
		private bool m_CustomCaretColor;

		// Token: 0x04000167 RID: 359
		[SerializeField]
		private Color m_SelectionColor = new Color(0.65882355f, 0.80784315f, 1f, 0.7529412f);

		// Token: 0x04000168 RID: 360
		[SerializeField]
		[TextArea(5, 10)]
		protected string m_Text = string.Empty;

		// Token: 0x04000169 RID: 361
		[SerializeField]
		[Range(0f, 4f)]
		private float m_CaretBlinkRate = 0.85f;

		// Token: 0x0400016A RID: 362
		[SerializeField]
		[Range(1f, 5f)]
		private int m_CaretWidth = 1;

		// Token: 0x0400016B RID: 363
		[SerializeField]
		private bool m_ReadOnly;

		// Token: 0x0400016C RID: 364
		[SerializeField]
		private bool m_RichText = true;

		// Token: 0x0400016D RID: 365
		protected int m_StringPosition;

		// Token: 0x0400016E RID: 366
		protected int m_StringSelectPosition;

		// Token: 0x0400016F RID: 367
		protected int m_CaretPosition;

		// Token: 0x04000170 RID: 368
		protected int m_CaretSelectPosition;

		// Token: 0x04000171 RID: 369
		private RectTransform caretRectTrans;

		// Token: 0x04000172 RID: 370
		protected UIVertex[] m_CursorVerts;

		// Token: 0x04000173 RID: 371
		private CanvasRenderer m_CachedInputRenderer;

		// Token: 0x04000174 RID: 372
		private Vector2 m_LastPosition;

		// Token: 0x04000175 RID: 373
		[NonSerialized]
		protected Mesh m_Mesh;

		// Token: 0x04000176 RID: 374
		private bool m_AllowInput;

		// Token: 0x04000177 RID: 375
		private bool m_ShouldActivateNextUpdate;

		// Token: 0x04000178 RID: 376
		private bool m_UpdateDrag;

		// Token: 0x04000179 RID: 377
		private bool m_DragPositionOutOfBounds;

		// Token: 0x0400017A RID: 378
		private const float kHScrollSpeed = 0.05f;

		// Token: 0x0400017B RID: 379
		private const float kVScrollSpeed = 0.1f;

		// Token: 0x0400017C RID: 380
		protected bool m_CaretVisible;

		// Token: 0x0400017D RID: 381
		private Coroutine m_BlinkCoroutine;

		// Token: 0x0400017E RID: 382
		private float m_BlinkStartTime;

		// Token: 0x0400017F RID: 383
		private Coroutine m_DragCoroutine;

		// Token: 0x04000180 RID: 384
		private string m_OriginalText = "";

		// Token: 0x04000181 RID: 385
		private bool m_WasCanceled;

		// Token: 0x04000182 RID: 386
		private bool m_HasDoneFocusTransition;

		// Token: 0x04000183 RID: 387
		private WaitForSecondsRealtime m_WaitForSecondsRealtime;

		// Token: 0x04000184 RID: 388
		private bool m_PreventCallback;

		// Token: 0x04000185 RID: 389
		private bool m_TouchKeyboardAllowsInPlaceEditing;

		// Token: 0x04000186 RID: 390
		private bool m_IsTextComponentUpdateRequired;

		// Token: 0x04000187 RID: 391
		private bool m_HasTextBeenRemoved;

		// Token: 0x04000188 RID: 392
		private float m_PointerDownClickStartTime;

		// Token: 0x04000189 RID: 393
		private float m_KeyDownStartTime;

		// Token: 0x0400018A RID: 394
		private float m_DoubleClickDelay = 0.5f;

		// Token: 0x0400018B RID: 395
		private bool m_IsApplePlatform;

		// Token: 0x0400018C RID: 396
		private const string kEmailSpecialCharacters = "!#$%&'*+-/=?^_`{|}~";

		// Token: 0x0400018D RID: 397
		private const string kOculusQuestDeviceModel = "Oculus Quest";

		// Token: 0x0400018E RID: 398
		private bool m_IsCompositionActive;

		// Token: 0x0400018F RID: 399
		private bool m_ShouldUpdateIMEWindowPosition;

		// Token: 0x04000190 RID: 400
		private int m_PreviousIMEInsertionLine;

		// Token: 0x04000191 RID: 401
		[SerializeField]
		protected TMP_FontAsset m_GlobalFontAsset;

		// Token: 0x04000192 RID: 402
		[SerializeField]
		protected bool m_OnFocusSelectAll = true;

		// Token: 0x04000193 RID: 403
		protected bool m_isSelectAll;

		// Token: 0x04000194 RID: 404
		[SerializeField]
		protected bool m_ResetOnDeActivation = true;

		// Token: 0x04000195 RID: 405
		private bool m_SelectionStillActive;

		// Token: 0x04000196 RID: 406
		private bool m_ReleaseSelection;

		// Token: 0x04000197 RID: 407
		private KeyCode m_LastKeyCode;

		// Token: 0x04000198 RID: 408
		private GameObject m_PreviouslySelectedObject;

		// Token: 0x04000199 RID: 409
		[SerializeField]
		private bool m_KeepTextSelectionVisible;

		// Token: 0x0400019A RID: 410
		[SerializeField]
		private bool m_RestoreOriginalTextOnEscape = true;

		// Token: 0x0400019B RID: 411
		[SerializeField]
		protected bool m_isRichTextEditingAllowed;

		// Token: 0x0400019C RID: 412
		[SerializeField]
		protected int m_LineLimit;

		// Token: 0x0400019D RID: 413
		public bool isAlert;

		// Token: 0x0400019E RID: 414
		[SerializeField]
		protected TMP_InputValidator m_InputValidator;

		// Token: 0x0400019F RID: 415
		[SerializeField]
		private bool m_ShouldActivateOnSelect = true;

		// Token: 0x040001A0 RID: 416
		private bool m_isSelected;

		// Token: 0x040001A1 RID: 417
		private bool m_IsStringPositionDirty;

		// Token: 0x040001A2 RID: 418
		private bool m_IsCaretPositionDirty;

		// Token: 0x040001A3 RID: 419
		private bool m_forceRectTransformAdjustment;

		// Token: 0x040001A4 RID: 420
		private bool m_IsKeyboardBeingClosedInHoloLens;

		// Token: 0x040001A5 RID: 421
		public static bool enableHorribleDeckWorkaround = false;

		// Token: 0x040001A6 RID: 422
		public static Func<KeyCode, char> keycodeMapper;

		// Token: 0x040001A7 RID: 423
		private Event m_ProcessingEvent = new Event();

		// Token: 0x0200009C RID: 156
		public enum ContentType
		{
			// Token: 0x04000639 RID: 1593
			Standard,
			// Token: 0x0400063A RID: 1594
			Autocorrected,
			// Token: 0x0400063B RID: 1595
			IntegerNumber,
			// Token: 0x0400063C RID: 1596
			DecimalNumber,
			// Token: 0x0400063D RID: 1597
			Alphanumeric,
			// Token: 0x0400063E RID: 1598
			Name,
			// Token: 0x0400063F RID: 1599
			EmailAddress,
			// Token: 0x04000640 RID: 1600
			Password,
			// Token: 0x04000641 RID: 1601
			Pin,
			// Token: 0x04000642 RID: 1602
			Custom
		}

		// Token: 0x0200009D RID: 157
		public enum InputType
		{
			// Token: 0x04000644 RID: 1604
			Standard,
			// Token: 0x04000645 RID: 1605
			AutoCorrect,
			// Token: 0x04000646 RID: 1606
			Password
		}

		// Token: 0x0200009E RID: 158
		public enum CharacterValidation
		{
			// Token: 0x04000648 RID: 1608
			None,
			// Token: 0x04000649 RID: 1609
			Digit,
			// Token: 0x0400064A RID: 1610
			Integer,
			// Token: 0x0400064B RID: 1611
			Decimal,
			// Token: 0x0400064C RID: 1612
			Alphanumeric,
			// Token: 0x0400064D RID: 1613
			Name,
			// Token: 0x0400064E RID: 1614
			Regex,
			// Token: 0x0400064F RID: 1615
			EmailAddress,
			// Token: 0x04000650 RID: 1616
			CustomValidator
		}

		// Token: 0x0200009F RID: 159
		public enum LineType
		{
			// Token: 0x04000652 RID: 1618
			SingleLine,
			// Token: 0x04000653 RID: 1619
			MultiLineSubmit,
			// Token: 0x04000654 RID: 1620
			MultiLineNewline
		}

		// Token: 0x020000A0 RID: 160
		// (Invoke) Token: 0x060006CC RID: 1740
		public delegate char OnValidateInput(string text, int charIndex, char addedChar);

		// Token: 0x020000A1 RID: 161
		[Serializable]
		public class SubmitEvent : UnityEvent<string>
		{
		}

		// Token: 0x020000A2 RID: 162
		[Serializable]
		public class OnChangeEvent : UnityEvent<string>
		{
		}

		// Token: 0x020000A3 RID: 163
		[Serializable]
		public class SelectionEvent : UnityEvent<string>
		{
		}

		// Token: 0x020000A4 RID: 164
		[Serializable]
		public class TextSelectionEvent : UnityEvent<string, int, int>
		{
		}

		// Token: 0x020000A5 RID: 165
		[Serializable]
		public class TouchScreenKeyboardEvent : UnityEvent<TouchScreenKeyboard.Status>
		{
		}

		// Token: 0x020000A6 RID: 166
		protected enum EditState
		{
			// Token: 0x04000656 RID: 1622
			Continue,
			// Token: 0x04000657 RID: 1623
			Finish
		}
	}
}
