using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.TextCore;
using UnityEngine.TextCore.Text;

namespace UnityEngine.UIElements
{
	// Token: 0x020004E7 RID: 1255
	internal class UITKTextHandle : TextHandle
	{
		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x0600263C RID: 9788 RVA: 0x0008ADF8 File Offset: 0x00088FF8
		private List<ValueTuple<int, RichTextTagParser.TagType, string>> Links
		{
			get
			{
				List<ValueTuple<int, RichTextTagParser.TagType, string>> list;
				if ((list = this.m_Links) == null)
				{
					list = (this.m_Links = new List<ValueTuple<int, RichTextTagParser.TagType, string>>());
				}
				return list;
			}
		}

		// Token: 0x0600263D RID: 9789 RVA: 0x0008AE20 File Offset: 0x00089020
		public void ComputeNativeTextSize(in RenderedText textToMeasure, float width, float height)
		{
			bool flag = !this.ConvertUssToNativeTextGenerationSettings();
			if (!flag)
			{
				this.nativeSettings.text = textToMeasure.CreateString();
				this.nativeSettings.screenWidth = (float.IsNaN(width) ? int.MaxValue : ((int)(width * 64f)));
				this.nativeSettings.screenHeight = (float.IsNaN(height) ? int.MaxValue : ((int)(height * 64f)));
				bool flag2 = this.m_TextElement.enableRichText && !string.IsNullOrEmpty(this.nativeSettings.text);
				if (flag2)
				{
					RichTextTagParser.CreateTextGenerationSettingsArray(ref this.nativeSettings, this.Links, this.atgHyperlinkColor);
				}
				else
				{
					this.nativeSettings.textSpans = null;
				}
				base.preferredSize = this.textLib.MeasureText(this.nativeSettings, IntPtr.Zero);
			}
		}

		// Token: 0x0600263E RID: 9790 RVA: 0x0008AF04 File Offset: 0x00089104
		public ValueTuple<NativeTextInfo, bool> UpdateNative(bool generateNativeSettings = true)
		{
			bool flag = generateNativeSettings && !this.ConvertUssToNativeTextGenerationSettings();
			ValueTuple<NativeTextInfo, bool> valueTuple;
			if (flag)
			{
				valueTuple = new ValueTuple<NativeTextInfo, bool>(default(NativeTextInfo), false);
			}
			else
			{
				bool flag2 = this.m_TextElement.enableRichText && !string.IsNullOrEmpty(this.nativeSettings.text);
				if (flag2)
				{
					RichTextTagParser.CreateTextGenerationSettingsArray(ref this.nativeSettings, this.Links, this.atgHyperlinkColor);
				}
				else
				{
					this.nativeSettings.textSpans = null;
				}
				bool flag3 = this.nativeSettings.hasLink && this.textGenerationInfo == IntPtr.Zero;
				if (flag3)
				{
					this.textGenerationInfo = TextGenerationInfo.Create();
					if (this.m_ATGTextEventHandler == null)
					{
						this.m_ATGTextEventHandler = new ATGTextEventHandler(this.m_TextElement);
					}
				}
				NativeTextInfo nativeTextInfo = this.textLib.GenerateText(this.nativeSettings, this.textGenerationInfo);
				this.m_IsEllided = nativeTextInfo.isElided;
				valueTuple = new ValueTuple<NativeTextInfo, bool>(nativeTextInfo, true);
			}
			return valueTuple;
		}

		// Token: 0x0600263F RID: 9791 RVA: 0x0008B006 File Offset: 0x00089206
		public void ProcessMeshInfos(NativeTextInfo textInfo)
		{
			this.textLib.ProcessMeshInfos(textInfo, this.nativeSettings);
		}

		// Token: 0x06002640 RID: 9792 RVA: 0x0008B01C File Offset: 0x0008921C
		private ValueTuple<bool, bool> hasLinkAndHyperlink()
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = this.m_Links != null;
			if (flag3)
			{
				foreach (ValueTuple<int, RichTextTagParser.TagType, string> valueTuple in this.Links)
				{
					RichTextTagParser.TagType item = valueTuple.Item2;
					flag = flag || item == RichTextTagParser.TagType.Link;
					flag2 = flag2 || item == RichTextTagParser.TagType.Hyperlink;
					bool flag4 = flag && flag2;
					if (flag4)
					{
						break;
					}
				}
			}
			return new ValueTuple<bool, bool>(flag, flag2);
		}

		// Token: 0x06002641 RID: 9793 RVA: 0x0008B0B8 File Offset: 0x000892B8
		internal ValueTuple<RichTextTagParser.TagType, string> ATGFindIntersectingLink(Vector2 point)
		{
			Debug.Assert(base.useAdvancedText);
			bool flag = this.textGenerationInfo == IntPtr.Zero;
			ValueTuple<RichTextTagParser.TagType, string> valueTuple;
			if (flag)
			{
				Debug.LogError("TextGenerationInfo pointer is null.");
				valueTuple = new ValueTuple<RichTextTagParser.TagType, string>(RichTextTagParser.TagType.Unknown, null);
			}
			else
			{
				int num = TextLib.FindIntersectingLink(point, this.textGenerationInfo);
				bool flag2 = num == -1;
				if (flag2)
				{
					valueTuple = new ValueTuple<RichTextTagParser.TagType, string>(RichTextTagParser.TagType.Unknown, null);
				}
				else
				{
					valueTuple = new ValueTuple<RichTextTagParser.TagType, string>(this.m_Links[num].Item2, this.m_Links[num].Item3);
				}
			}
			return valueTuple;
		}

		// Token: 0x06002642 RID: 9794 RVA: 0x0008B148 File Offset: 0x00089348
		internal void UpdateATGTextEventHandler()
		{
			bool flag = this.m_ATGTextEventHandler == null;
			if (!flag)
			{
				ValueTuple<bool, bool> valueTuple = this.hasLinkAndHyperlink();
				bool item = valueTuple.Item1;
				bool item2 = valueTuple.Item2;
				bool flag2 = item;
				if (flag2)
				{
					this.m_ATGTextEventHandler.RegisterLinkTagCallbacks();
				}
				else
				{
					this.m_ATGTextEventHandler.UnRegisterLinkTagCallbacks();
				}
				bool flag3 = item2;
				if (flag3)
				{
					this.m_ATGTextEventHandler.RegisterHyperlinkCallbacks();
				}
				else
				{
					this.m_ATGTextEventHandler.UnRegisterHyperlinkCallbacks();
				}
			}
		}

		// Token: 0x06002643 RID: 9795 RVA: 0x0008B1B8 File Offset: 0x000893B8
		internal unsafe bool ConvertUssToNativeTextGenerationSettings()
		{
			FontAsset fontAsset = TextUtilities.GetFontAsset(this.m_TextElement);
			bool flag = fontAsset.atlasPopulationMode == AtlasPopulationMode.Static;
			bool flag2;
			if (flag)
			{
				Debug.LogError("Advanced text system cannot render using static font asset " + fontAsset.faceInfo.familyName);
				flag2 = false;
			}
			else
			{
				ComputedStyle computedStyle = *this.m_TextElement.computedStyle;
				this.nativeSettings.textSettings = TextUtilities.GetTextSettingsFrom(this.m_TextElement).nativeTextSettings;
				this.nativeSettings.text = ((this.m_TextElement.isElided && !this.TextLibraryCanElide()) ? new RenderedText(this.m_TextElement.elidedText) : this.m_TextElement.renderedText).CreateString();
				this.nativeSettings.fontSize = (int)(computedStyle.fontSize.value * 64f);
				this.nativeSettings.wordWrap = computedStyle.whiteSpace.toTextCore(this.m_TextElement.isInputField);
				this.nativeSettings.overflow = computedStyle.textOverflow.toTextCore(computedStyle.overflow);
				this.nativeSettings.horizontalAlignment = TextGeneratorUtilities.GetHorizontalAlignment(computedStyle.unityTextAlign);
				this.nativeSettings.verticalAlignment = TextGeneratorUtilities.GetVerticalAlignment(computedStyle.unityTextAlign);
				this.nativeSettings.characterSpacing = (int)(computedStyle.letterSpacing.value * 64f);
				this.nativeSettings.wordSpacing = (int)(computedStyle.wordSpacing.value * 64f);
				this.nativeSettings.paragraphSpacing = (int)(computedStyle.unityParagraphSpacing.value * 64f);
				this.nativeSettings.color = computedStyle.color;
				this.nativeSettings.fontAsset = fontAsset.nativeFontAsset;
				this.nativeSettings.languageDirection = this.m_TextElement.localLanguageDirection.toTextCore();
				this.nativeSettings.vertexPadding = (int)(this.GetVertexPadding(fontAsset) * 64f);
				FontStyles fontStyles = TextGeneratorUtilities.LegacyStyleToNewStyle(computedStyle.unityFontStyleAndWeight);
				this.nativeSettings.fontStyle = fontStyles & ~FontStyles.Bold;
				this.nativeSettings.fontWeight = (((fontStyles & FontStyles.Bold) == FontStyles.Bold) ? TextFontWeight.Bold : TextFontWeight.Regular);
				Vector2 vector = this.m_TextElement.contentRect.size;
				bool flag3 = Mathf.Abs(vector.x - this.ATGRoundedSizes.x) < 0.01f && Mathf.Abs(vector.y - this.ATGRoundedSizes.y) < 0.01f;
				if (flag3)
				{
					vector = this.ATGMeasuredSizes;
				}
				else
				{
					this.ATGRoundedSizes = vector;
					this.ATGMeasuredSizes = vector;
				}
				this.nativeSettings.screenWidth = (int)(vector.x * 64f);
				this.nativeSettings.screenHeight = (int)(vector.y * 64f);
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06002644 RID: 9796 RVA: 0x0008B4BC File Offset: 0x000896BC
		private TextAsset GetICUAsset()
		{
			bool flag = this.m_TextElement.panel == null;
			if (flag)
			{
				throw new InvalidOperationException("Text cannot be processed on elements not in a panel");
			}
			TextAsset textAsset = ((PanelSettings)((RuntimePanel)this.m_TextElement.panel).ownerObject).m_ICUDataAsset;
			bool flag2 = textAsset != null;
			TextAsset textAsset2;
			if (flag2)
			{
				textAsset2 = textAsset;
			}
			else
			{
				textAsset = UITKTextHandle.GetICUAssetStaticFalback();
				bool flag3 = textAsset != null;
				if (flag3)
				{
					textAsset2 = textAsset;
				}
				else
				{
					Debug.LogError("ICU Data not available. The data should be automatically assigned to the PanelSettings in the editor if the advanced text option is enable in the project settings. It will not be present on PanelSettings created at runtime, so make sure the build contains at least one PanelSettings asset");
					textAsset2 = null;
				}
			}
			return textAsset2;
		}

		// Token: 0x06002645 RID: 9797 RVA: 0x0008B540 File Offset: 0x00089740
		internal static TextAsset GetICUAssetStaticFalback()
		{
			foreach (TextAsset textAsset in Resources.FindObjectsOfTypeAll<TextAsset>())
			{
				bool flag = textAsset.name == "icudt73l";
				if (flag)
				{
					return textAsset;
				}
			}
			return null;
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x06002646 RID: 9798 RVA: 0x0008B58C File Offset: 0x0008978C
		protected internal TextLib textLib
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				this.InitTextLib();
				return UITKTextHandle.s_TextLib;
			}
		}

		// Token: 0x06002647 RID: 9799 RVA: 0x0008B5AA File Offset: 0x000897AA
		protected internal void InitTextLib()
		{
			if (UITKTextHandle.s_TextLib == null)
			{
				UITKTextHandle.s_TextLib = new TextLib(this.GetICUAsset().bytes);
			}
		}

		// Token: 0x06002648 RID: 9800 RVA: 0x0008B5C9 File Offset: 0x000897C9
		public UITKTextHandle(TextElement te)
		{
			this.m_TextElement = te;
			this.m_TextEventHandler = new TextEventHandler(te);
		}

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x06002649 RID: 9801 RVA: 0x0008B5F1 File Offset: 0x000897F1
		// (set) Token: 0x0600264A RID: 9802 RVA: 0x0008B5F9 File Offset: 0x000897F9
		public Vector2 MeasuredSizes { get; set; }

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x0600264B RID: 9803 RVA: 0x0008B602 File Offset: 0x00089802
		// (set) Token: 0x0600264C RID: 9804 RVA: 0x0008B60A File Offset: 0x0008980A
		public Vector2 RoundedSizes { get; set; }

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x0600264D RID: 9805 RVA: 0x0008B613 File Offset: 0x00089813
		// (set) Token: 0x0600264E RID: 9806 RVA: 0x0008B61B File Offset: 0x0008981B
		public Vector2 ATGMeasuredSizes { get; set; }

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x0600264F RID: 9807 RVA: 0x0008B624 File Offset: 0x00089824
		// (set) Token: 0x06002650 RID: 9808 RVA: 0x0008B62C File Offset: 0x0008982C
		public Vector2 ATGRoundedSizes { get; set; }

		// Token: 0x06002651 RID: 9809 RVA: 0x0008B638 File Offset: 0x00089838
		public Vector2 ComputeTextSize(in RenderedText textToMeasure, float width, float height)
		{
			bool flag = TextUtilities.IsAdvancedTextEnabledForElement(this.m_TextElement);
			if (flag)
			{
				this.ComputeNativeTextSize(in textToMeasure, width, height);
			}
			else
			{
				this.ConvertUssToTextGenerationSettings();
				TextHandle.settings.renderedText = textToMeasure;
				TextHandle.settings.screenRect = new Rect(0f, 0f, width, height);
				base.UpdatePreferredValues(TextHandle.settings);
			}
			return base.preferredSize;
		}

		// Token: 0x06002652 RID: 9810 RVA: 0x0008B6AE File Offset: 0x000898AE
		public void ComputeSettingsAndUpdate()
		{
			this.UpdateMesh();
			this.HandleATag();
			this.HandleLinkTag();
			this.HandleLinkAndATagCallbacks();
		}

		// Token: 0x06002653 RID: 9811 RVA: 0x0008B6CD File Offset: 0x000898CD
		public void HandleATag()
		{
			TextEventHandler textEventHandler = this.m_TextEventHandler;
			if (textEventHandler != null)
			{
				textEventHandler.HandleATag();
			}
		}

		// Token: 0x06002654 RID: 9812 RVA: 0x0008B6E2 File Offset: 0x000898E2
		public void HandleLinkTag()
		{
			TextEventHandler textEventHandler = this.m_TextEventHandler;
			if (textEventHandler != null)
			{
				textEventHandler.HandleLinkTag();
			}
		}

		// Token: 0x06002655 RID: 9813 RVA: 0x0008B6F7 File Offset: 0x000898F7
		public void HandleLinkAndATagCallbacks()
		{
			TextEventHandler textEventHandler = this.m_TextEventHandler;
			if (textEventHandler != null)
			{
				textEventHandler.HandleLinkAndATagCallbacks();
			}
		}

		// Token: 0x06002656 RID: 9814 RVA: 0x0008B70C File Offset: 0x0008990C
		public void UpdateMesh()
		{
			this.ConvertUssToTextGenerationSettings();
			int hashCode = TextHandle.settings.GetHashCode();
			bool flag = this.m_PreviousGenerationSettingsHash == hashCode && !this.isDirty;
			if (flag)
			{
				base.AddTextInfoToTemporaryCache(hashCode);
			}
			else
			{
				base.RemoveTextInfoFromTemporaryCache();
				base.UpdateWithHash(hashCode);
			}
		}

		// Token: 0x06002657 RID: 9815 RVA: 0x0008B760 File Offset: 0x00089960
		public override void AddTextInfoToPermanentCache()
		{
			bool useAdvancedText = base.useAdvancedText;
			if (useAdvancedText)
			{
				bool flag = this.textGenerationInfo == IntPtr.Zero;
				if (flag)
				{
					this.textGenerationInfo = TextGenerationInfo.Create();
				}
				this.UpdateNative(true);
				this.UpdateATGTextEventHandler();
			}
			else
			{
				bool flag2 = this.ConvertUssToTextGenerationSettings();
				if (flag2)
				{
					base.AddTextInfoToPermanentCache();
				}
			}
		}

		// Token: 0x06002658 RID: 9816 RVA: 0x0008B7BC File Offset: 0x000899BC
		private unsafe TextOverflowMode GetTextOverflowMode()
		{
			ComputedStyle computedStyle = *this.m_TextElement.computedStyle;
			bool flag = computedStyle.textOverflow == TextOverflow.Clip;
			TextOverflowMode textOverflowMode;
			if (flag)
			{
				textOverflowMode = TextOverflowMode.Masking;
			}
			else
			{
				bool flag2 = computedStyle.textOverflow != TextOverflow.Ellipsis;
				if (flag2)
				{
					textOverflowMode = TextOverflowMode.Overflow;
				}
				else
				{
					bool flag3 = !this.TextLibraryCanElide();
					if (flag3)
					{
						textOverflowMode = TextOverflowMode.Masking;
					}
					else
					{
						bool flag4 = computedStyle.overflow == OverflowInternal.Hidden;
						if (flag4)
						{
							textOverflowMode = TextOverflowMode.Ellipsis;
						}
						else
						{
							textOverflowMode = TextOverflowMode.Overflow;
						}
					}
				}
			}
			return textOverflowMode;
		}

		// Token: 0x06002659 RID: 9817 RVA: 0x0008B830 File Offset: 0x00089A30
		internal unsafe virtual bool ConvertUssToTextGenerationSettings()
		{
			ComputedStyle computedStyle = *this.m_TextElement.computedStyle;
			TextGenerationSettings settings = TextHandle.settings;
			settings.text = string.Empty;
			settings.isIMGUI = false;
			settings.textSettings = TextUtilities.GetTextSettingsFrom(this.m_TextElement);
			bool flag = settings.textSettings == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				settings.fontAsset = TextUtilities.GetFontAsset(this.m_TextElement);
				bool flag3 = settings.fontAsset == null;
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					settings.screenRect = new Rect(0f, 0f, this.m_TextElement.contentRect.width, this.m_TextElement.contentRect.height);
					settings.extraPadding = this.GetVertexPadding(settings.fontAsset);
					settings.renderedText = ((this.m_TextElement.isElided && !this.TextLibraryCanElide()) ? new RenderedText(this.m_TextElement.elidedText) : this.m_TextElement.renderedText);
					settings.isPlaceholder = this.m_TextElement.showPlaceholderText;
					settings.fontSize = computedStyle.fontSize.value;
					settings.fontStyle = TextGeneratorUtilities.LegacyStyleToNewStyle(computedStyle.unityFontStyleAndWeight);
					settings.textAlignment = TextGeneratorUtilities.LegacyAlignmentToNewAlignment(computedStyle.unityTextAlign);
					settings.textWrappingMode = computedStyle.whiteSpace.toTextWrappingMode(this.m_TextElement.isInputField && !this.m_TextElement.edition.multiline);
					settings.richText = this.m_TextElement.enableRichText;
					settings.overflowMode = this.GetTextOverflowMode();
					settings.characterSpacing = computedStyle.letterSpacing.value;
					settings.wordSpacing = computedStyle.wordSpacing.value;
					settings.paragraphSpacing = computedStyle.unityParagraphSpacing.value;
					settings.color = computedStyle.color;
					settings.color *= this.m_TextElement.playModeTintColor;
					settings.shouldConvertToLinearSpace = false;
					settings.parseControlCharacters = this.m_TextElement.parseEscapeSequences;
					settings.isRightToLeft = this.m_TextElement.localLanguageDirection == LanguageDirection.RTL;
					settings.emojiFallbackSupport = this.m_TextElement.emojiFallbackSupport;
					Vector2 vector = this.m_TextElement.contentRect.size;
					bool flag4 = Mathf.Abs(vector.x - this.RoundedSizes.x) < 0.01f && Mathf.Abs(vector.y - this.RoundedSizes.y) < 0.01f;
					if (flag4)
					{
						vector = this.MeasuredSizes;
					}
					else
					{
						this.RoundedSizes = vector;
						this.MeasuredSizes = vector;
					}
					settings.screenRect = new Rect(Vector2.zero, vector);
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x0600265A RID: 9818 RVA: 0x0008BB18 File Offset: 0x00089D18
		internal bool TextLibraryCanElide()
		{
			return this.m_TextElement.computedStyle.unityTextOverflowPosition == TextOverflowPosition.End;
		}

		// Token: 0x0600265B RID: 9819 RVA: 0x0008BB40 File Offset: 0x00089D40
		internal unsafe float GetVertexPadding(FontAsset fontAsset)
		{
			ComputedStyle computedStyle = *this.m_TextElement.computedStyle;
			float num = computedStyle.unityTextOutlineWidth / 2f;
			float num2 = Mathf.Abs(computedStyle.textShadow.offset.x);
			float num3 = Mathf.Abs(computedStyle.textShadow.offset.y);
			float num4 = Mathf.Abs(computedStyle.textShadow.blurRadius);
			bool flag = num <= 0f && num2 <= 0f && num3 <= 0f && num4 <= 0f;
			float num5;
			if (flag)
			{
				num5 = UITKTextHandle.k_MinPadding;
			}
			else
			{
				float num6 = Mathf.Max(num2 + num4, num);
				float num7 = Mathf.Max(num3 + num4, num);
				float num8 = Mathf.Max(num6, num7) + UITKTextHandle.k_MinPadding;
				float num9 = TextHandle.ConvertPixelUnitsToTextCoreRelativeUnits(computedStyle.fontSize.value, fontAsset);
				int num10 = fontAsset.atlasPadding + 1;
				num5 = Mathf.Min(num8 * num9 * (float)num10, (float)num10);
			}
			return num5;
		}

		// Token: 0x0600265C RID: 9820 RVA: 0x0008BC48 File Offset: 0x00089E48
		internal override bool IsAdvancedTextEnabledForElement()
		{
			return TextUtilities.IsAdvancedTextEnabledForElement(this.m_TextElement);
		}

		// Token: 0x0600265D RID: 9821 RVA: 0x0008BC68 File Offset: 0x00089E68
		internal void ReleaseResourcesIfPossible()
		{
			bool flag = TextUtilities.IsAdvancedTextEnabledForElement(this.m_TextElement);
			bool flag2 = this.wasAdvancedTextEnabledForElement && !flag && this.textGenerationInfo != IntPtr.Zero;
			if (flag2)
			{
				TextGenerationInfo.Destroy(this.textGenerationInfo);
				this.textGenerationInfo = IntPtr.Zero;
				ATGTextEventHandler atgtextEventHandler = this.m_ATGTextEventHandler;
				if (atgtextEventHandler != null)
				{
					atgtextEventHandler.OnDestroy();
				}
				this.m_ATGTextEventHandler = null;
				this.m_TextEventHandler = new TextEventHandler(this.m_TextElement);
			}
			else
			{
				bool flag3 = !this.wasAdvancedTextEnabledForElement && flag;
				if (flag3)
				{
					TextHandle.s_PermanentCache.RemoveTextInfoFromCache(this);
					TextHandle.s_TemporaryCache.RemoveTextInfoFromCache(this);
					TextEventHandler textEventHandler = this.m_TextEventHandler;
					if (textEventHandler != null)
					{
						textEventHandler.OnDestroy();
					}
					this.m_TextEventHandler = null;
					this.m_ATGTextEventHandler = new ATGTextEventHandler(this.m_TextElement);
				}
			}
			this.wasAdvancedTextEnabledForElement = flag;
		}

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x0600265E RID: 9822 RVA: 0x0008BD43 File Offset: 0x00089F43
		public override bool IsPlaceholder
		{
			get
			{
				return base.useAdvancedText ? this.m_TextElement.showPlaceholderText : base.IsPlaceholder;
			}
		}

		// Token: 0x0600265F RID: 9823 RVA: 0x0008BD60 File Offset: 0x00089F60
		public bool IsElided()
		{
			bool flag = string.IsNullOrEmpty(this.m_TextElement.text);
			return flag || this.m_IsEllided;
		}

		// Token: 0x04001149 RID: 4425
		internal ATGTextEventHandler m_ATGTextEventHandler;

		// Token: 0x0400114A RID: 4426
		private List<ValueTuple<int, RichTextTagParser.TagType, string>> m_Links;

		// Token: 0x0400114B RID: 4427
		internal Color atgHyperlinkColor = Color.blue;

		// Token: 0x0400114C RID: 4428
		private static TextLib s_TextLib;

		// Token: 0x04001151 RID: 4433
		internal TextEventHandler m_TextEventHandler;

		// Token: 0x04001152 RID: 4434
		protected TextElement m_TextElement;

		// Token: 0x04001153 RID: 4435
		internal static readonly float k_MinPadding = 6f;

		// Token: 0x04001154 RID: 4436
		private bool wasAdvancedTextEnabledForElement;
	}
}
