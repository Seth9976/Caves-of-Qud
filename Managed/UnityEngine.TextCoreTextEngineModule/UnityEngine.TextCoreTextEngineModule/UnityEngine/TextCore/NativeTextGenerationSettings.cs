using System;
using System.Text;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.TextCore.Text;

namespace UnityEngine.TextCore
{
	// Token: 0x02000005 RID: 5
	[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
	[UsedByNativeCode("TextGenerationSettings")]
	[NativeHeader("Modules/TextCoreTextEngine/Native/TextGenerationSettings.h")]
	internal struct NativeTextGenerationSettings
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002092 File Offset: 0x00000292
		public bool hasLink
		{
			get
			{
				bool flag;
				if (this.textSpans != null)
				{
					flag = Array.Exists<TextSpan>(this.textSpans, (TextSpan span) => span.linkID != -1);
				}
				else
				{
					flag = false;
				}
				return flag;
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020CC File Offset: 0x000002CC
		public readonly TextSpan CreateTextSpan()
		{
			return new TextSpan
			{
				fontAsset = this.fontAsset,
				fontSize = this.fontSize,
				color = this.color,
				fontStyle = this.fontStyle,
				fontWeight = this.fontWeight,
				linkID = -1
			};
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002130 File Offset: 0x00000330
		public string GetTextSpanContent(int spanIndex)
		{
			bool flag = string.IsNullOrEmpty(this.text);
			if (flag)
			{
				throw new InvalidOperationException("The text property is null or empty.");
			}
			bool flag2 = this.textSpans == null || spanIndex < 0 || spanIndex >= this.textSpans.Length;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("spanIndex", "Invalid span index.");
			}
			TextSpan textSpan = this.textSpans[spanIndex];
			bool flag3 = textSpan.startIndex < 0 || textSpan.startIndex >= this.text.Length || textSpan.startIndex + textSpan.length > this.text.Length;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("spanIndex", "Invalid startIndex or length for the current text.");
			}
			return this.text.Substring(textSpan.startIndex, textSpan.length);
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002204 File Offset: 0x00000404
		public static NativeTextGenerationSettings Default
		{
			get
			{
				return new NativeTextGenerationSettings
				{
					fontStyle = FontStyles.Normal,
					fontWeight = TextFontWeight.Regular,
					color = Color.black
				};
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002240 File Offset: 0x00000440
		internal NativeTextGenerationSettings(NativeTextGenerationSettings tgs)
		{
			this.text = tgs.text;
			this.fontSize = tgs.fontSize;
			this.screenWidth = tgs.screenWidth;
			this.screenHeight = tgs.screenHeight;
			this.wordWrap = tgs.wordWrap;
			this.horizontalAlignment = tgs.horizontalAlignment;
			this.verticalAlignment = tgs.verticalAlignment;
			this.color = tgs.color;
			this.fontAsset = tgs.fontAsset;
			this.textSettings = tgs.textSettings;
			this.fontStyle = tgs.fontStyle;
			this.fontWeight = tgs.fontWeight;
			this.languageDirection = tgs.languageDirection;
			this.vertexPadding = tgs.vertexPadding;
			this.overflow = tgs.overflow;
			this.textSpans = ((tgs.textSpans != null) ? ((TextSpan[])tgs.textSpans.Clone()) : null);
			this.characterSpacing = tgs.characterSpacing;
			this.wordSpacing = tgs.wordSpacing;
			this.paragraphSpacing = tgs.paragraphSpacing;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002348 File Offset: 0x00000548
		public override string ToString()
		{
			string text = "null";
			bool flag = this.textSpans != null;
			if (flag)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("[");
				for (int i = 0; i < this.textSpans.Length; i++)
				{
					bool flag2 = i > 0;
					if (flag2)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(this.textSpans[i].ToString());
				}
				stringBuilder.Append("]");
				text = stringBuilder.ToString();
			}
			return string.Concat(new string[]
			{
				string.Format("{0}: {1}\n", "fontAsset", this.fontAsset),
				string.Format("{0}: {1}\n", "textSettings", this.textSettings),
				"text: ",
				this.text,
				"\n",
				string.Format("{0}: {1}\n", "screenWidth", this.screenWidth),
				string.Format("{0}: {1}\n", "screenHeight", this.screenHeight),
				string.Format("{0}: {1}\n", "fontSize", this.fontSize),
				string.Format("{0}: {1}\n", "wordWrap", this.wordWrap),
				string.Format("{0}: {1}\n", "languageDirection", this.languageDirection),
				string.Format("{0}: {1}\n", "horizontalAlignment", this.horizontalAlignment),
				string.Format("{0}: {1}\n", "verticalAlignment", this.verticalAlignment),
				string.Format("{0}: {1}\n", "color", this.color),
				string.Format("{0}: {1}\n", "fontStyle", this.fontStyle),
				string.Format("{0}: {1}\n", "fontWeight", this.fontWeight),
				string.Format("{0}: {1}\n", "vertexPadding", this.vertexPadding),
				string.Format("{0}: {1}\n", "overflow", this.overflow),
				"textSpans: ",
				text,
				"\n",
				string.Format("{0}: {1}\n", "characterSpacing", this.characterSpacing),
				string.Format("{0}: {1}\n", "paragraphSpacing", this.paragraphSpacing),
				string.Format("{0}: {1}\n", "wordSpacing", this.wordSpacing)
			});
		}

		// Token: 0x04000003 RID: 3
		public IntPtr fontAsset;

		// Token: 0x04000004 RID: 4
		public IntPtr textSettings;

		// Token: 0x04000005 RID: 5
		public string text;

		// Token: 0x04000006 RID: 6
		public int screenWidth;

		// Token: 0x04000007 RID: 7
		public int screenHeight;

		// Token: 0x04000008 RID: 8
		public WhiteSpace wordWrap;

		// Token: 0x04000009 RID: 9
		public TextOverflow overflow;

		// Token: 0x0400000A RID: 10
		public LanguageDirection languageDirection;

		// Token: 0x0400000B RID: 11
		public int vertexPadding;

		// Token: 0x0400000C RID: 12
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal HorizontalAlignment horizontalAlignment;

		// Token: 0x0400000D RID: 13
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal VerticalAlignment verticalAlignment;

		// Token: 0x0400000E RID: 14
		public int fontSize;

		// Token: 0x0400000F RID: 15
		public FontStyles fontStyle;

		// Token: 0x04000010 RID: 16
		public TextFontWeight fontWeight;

		// Token: 0x04000011 RID: 17
		public TextSpan[] textSpans;

		// Token: 0x04000012 RID: 18
		public Color32 color;

		// Token: 0x04000013 RID: 19
		public int characterSpacing;

		// Token: 0x04000014 RID: 20
		public int wordSpacing;

		// Token: 0x04000015 RID: 21
		public int paragraphSpacing;
	}
}
