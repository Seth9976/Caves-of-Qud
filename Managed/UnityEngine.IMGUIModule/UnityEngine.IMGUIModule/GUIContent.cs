using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000015 RID: 21
	[NativeHeader("Modules/IMGUI/GUIContent.h")]
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class GUIContent
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060001BA RID: 442 RVA: 0x000083C4 File Offset: 0x000065C4
		// (remove) Token: 0x060001BB RID: 443 RVA: 0x000083FC File Offset: 0x000065FC
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action OnTextChanged;

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00008434 File Offset: 0x00006634
		// (set) Token: 0x060001BD RID: 445 RVA: 0x0000844C File Offset: 0x0000664C
		public string text
		{
			get
			{
				return this.m_Text;
			}
			set
			{
				bool flag = this.m_Text == value;
				if (!flag)
				{
					this.m_Text = value;
					this.textWithWhitespace = value;
					Action onTextChanged = this.OnTextChanged;
					if (onTextChanged != null)
					{
						onTextChanged();
					}
				}
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00008490 File Offset: 0x00006690
		// (set) Token: 0x060001BF RID: 447 RVA: 0x000084BC File Offset: 0x000066BC
		internal string textWithWhitespace
		{
			get
			{
				return string.IsNullOrEmpty(this.m_TextWithWhitespace) ? GUIContent.k_ZeroWidthSpace : this.m_TextWithWhitespace;
			}
			set
			{
				this.m_TextWithWhitespace = value + GUIContent.k_ZeroWidthSpace;
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x000084CF File Offset: 0x000066CF
		internal void SetTextWithoutNotify(string value)
		{
			this.m_Text = value;
			this.textWithWhitespace = value;
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x000084E4 File Offset: 0x000066E4
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x000084FC File Offset: 0x000066FC
		public Texture image
		{
			get
			{
				return this.m_Image;
			}
			set
			{
				this.m_Image = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00008508 File Offset: 0x00006708
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x00008520 File Offset: 0x00006720
		public string tooltip
		{
			get
			{
				return this.m_Tooltip;
			}
			set
			{
				this.m_Tooltip = value;
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000852A File Offset: 0x0000672A
		public GUIContent()
		{
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00008555 File Offset: 0x00006755
		public GUIContent(string text)
			: this(text, null, string.Empty)
		{
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00008566 File Offset: 0x00006766
		public GUIContent(Texture image)
			: this(string.Empty, image, string.Empty)
		{
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000857B File Offset: 0x0000677B
		public GUIContent(string text, Texture image)
			: this(text, image, string.Empty)
		{
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000858C File Offset: 0x0000678C
		public GUIContent(string text, string tooltip)
			: this(text, null, tooltip)
		{
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00008599 File Offset: 0x00006799
		public GUIContent(Texture image, string tooltip)
			: this(string.Empty, image, tooltip)
		{
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000085AC File Offset: 0x000067AC
		public GUIContent(string text, Texture image, string tooltip)
		{
			this.text = text;
			this.image = image;
			this.tooltip = tooltip;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x000085FC File Offset: 0x000067FC
		public GUIContent(GUIContent src)
		{
			this.text = src.m_Text;
			this.image = src.m_Image;
			this.tooltip = src.m_Tooltip;
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001CD RID: 461 RVA: 0x0000865C File Offset: 0x0000685C
		internal int hash
		{
			get
			{
				int num = 0;
				bool flag = !string.IsNullOrEmpty(this.m_Text);
				if (flag)
				{
					num = this.m_Text.GetHashCode() * 37;
				}
				return num;
			}
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00008694 File Offset: 0x00006894
		internal static GUIContent Temp(string t)
		{
			GUIContent.s_Text.m_Text = t;
			GUIContent.s_Text.textWithWhitespace = t;
			GUIContent.s_Text.m_Tooltip = string.Empty;
			return GUIContent.s_Text;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x000086D4 File Offset: 0x000068D4
		internal static GUIContent Temp(string t, string tooltip)
		{
			GUIContent.s_Text.m_Text = t;
			GUIContent.s_Text.textWithWhitespace = t;
			GUIContent.s_Text.m_Tooltip = tooltip;
			return GUIContent.s_Text;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00008710 File Offset: 0x00006910
		internal static GUIContent Temp(Texture i)
		{
			GUIContent.s_Image.m_Image = i;
			GUIContent.s_Image.m_Tooltip = string.Empty;
			return GUIContent.s_Image;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00008744 File Offset: 0x00006944
		internal static GUIContent Temp(Texture i, string tooltip)
		{
			GUIContent.s_Image.m_Image = i;
			GUIContent.s_Image.m_Tooltip = tooltip;
			return GUIContent.s_Image;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00008774 File Offset: 0x00006974
		internal static GUIContent Temp(string t, Texture i)
		{
			GUIContent.s_TextImage.m_Text = t;
			GUIContent.s_Text.textWithWhitespace = t;
			GUIContent.s_TextImage.m_Image = i;
			return GUIContent.s_TextImage;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x000087B0 File Offset: 0x000069B0
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static void ClearStaticCache()
		{
			GUIContent.s_Text.m_Text = null;
			GUIContent.s_Text.m_TextWithWhitespace = null;
			GUIContent.s_Text.m_Tooltip = string.Empty;
			GUIContent.s_Image.m_Image = null;
			GUIContent.s_Image.m_Tooltip = string.Empty;
			GUIContent.s_Image.m_TextWithWhitespace = null;
			GUIContent.s_TextImage.m_Text = null;
			GUIContent.s_TextImage.m_Image = null;
			GUIContent.s_TextImage.m_TextWithWhitespace = null;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000882C File Offset: 0x00006A2C
		internal static GUIContent[] Temp(string[] texts)
		{
			GUIContent[] array = new GUIContent[texts.Length];
			for (int i = 0; i < texts.Length; i++)
			{
				array[i] = new GUIContent(texts[i]);
			}
			return array;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00008868 File Offset: 0x00006A68
		internal static GUIContent[] Temp(Texture[] images)
		{
			GUIContent[] array = new GUIContent[images.Length];
			for (int i = 0; i < images.Length; i++)
			{
				array[i] = new GUIContent(images[i]);
			}
			return array;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x000088A4 File Offset: 0x00006AA4
		public override string ToString()
		{
			string text;
			if ((text = this.text) == null)
			{
				text = this.tooltip ?? base.ToString();
			}
			return text;
		}

		// Token: 0x0400006F RID: 111
		[SerializeField]
		private string m_Text = string.Empty;

		// Token: 0x04000070 RID: 112
		[SerializeField]
		private Texture m_Image;

		// Token: 0x04000071 RID: 113
		[SerializeField]
		private string m_Tooltip = string.Empty;

		// Token: 0x04000072 RID: 114
		[SerializeField]
		private string m_TextWithWhitespace = string.Empty;

		// Token: 0x04000074 RID: 116
		private static readonly GUIContent s_Text = new GUIContent();

		// Token: 0x04000075 RID: 117
		private static readonly GUIContent s_Image = new GUIContent();

		// Token: 0x04000076 RID: 118
		private static readonly GUIContent s_TextImage = new GUIContent();

		// Token: 0x04000077 RID: 119
		internal static string k_ZeroWidthSpace = "\u200b";

		// Token: 0x04000078 RID: 120
		public static GUIContent none = new GUIContent("");
	}
}
