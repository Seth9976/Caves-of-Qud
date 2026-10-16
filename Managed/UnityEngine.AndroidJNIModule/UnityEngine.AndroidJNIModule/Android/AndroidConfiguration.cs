using System;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Android
{
	// Token: 0x02000031 RID: 49
	[NativeAsStruct]
	[NativeType(Header = "Modules/AndroidJNI/Public/AndroidConfiguration.bindings.h")]
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class AndroidConfiguration
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000AFD7 File Offset: 0x000091D7
		// (set) Token: 0x060002BC RID: 700 RVA: 0x0000AFDF File Offset: 0x000091DF
		private int colorMode { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0000AFE8 File Offset: 0x000091E8
		// (set) Token: 0x060002BE RID: 702 RVA: 0x0000AFF0 File Offset: 0x000091F0
		public int densityDpi { get; private set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000AFF9 File Offset: 0x000091F9
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x0000B001 File Offset: 0x00009201
		public float fontScale { get; private set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000B00A File Offset: 0x0000920A
		// (set) Token: 0x060002C2 RID: 706 RVA: 0x0000B012 File Offset: 0x00009212
		public int fontWeightAdjustment { get; private set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000B01B File Offset: 0x0000921B
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x0000B023 File Offset: 0x00009223
		public AndroidKeyboard keyboard { get; private set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000B02C File Offset: 0x0000922C
		// (set) Token: 0x060002C6 RID: 710 RVA: 0x0000B034 File Offset: 0x00009234
		public AndroidHardwareKeyboardHidden hardKeyboardHidden { get; private set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000B03D File Offset: 0x0000923D
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x0000B045 File Offset: 0x00009245
		public AndroidKeyboardHidden keyboardHidden { get; private set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0000B04E File Offset: 0x0000924E
		// (set) Token: 0x060002CA RID: 714 RVA: 0x0000B056 File Offset: 0x00009256
		public int mobileCountryCode { get; private set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0000B05F File Offset: 0x0000925F
		// (set) Token: 0x060002CC RID: 716 RVA: 0x0000B067 File Offset: 0x00009267
		public int mobileNetworkCode { get; private set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0000B070 File Offset: 0x00009270
		// (set) Token: 0x060002CE RID: 718 RVA: 0x0000B078 File Offset: 0x00009278
		public AndroidNavigation navigation { get; private set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060002CF RID: 719 RVA: 0x0000B081 File Offset: 0x00009281
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x0000B089 File Offset: 0x00009289
		public AndroidNavigationHidden navigationHidden { get; private set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000B092 File Offset: 0x00009292
		// (set) Token: 0x060002D2 RID: 722 RVA: 0x0000B09A File Offset: 0x0000929A
		public AndroidOrientation orientation { get; private set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000B0A3 File Offset: 0x000092A3
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x0000B0AB File Offset: 0x000092AB
		public int screenHeightDp { get; private set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000B0B4 File Offset: 0x000092B4
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x0000B0BC File Offset: 0x000092BC
		public int screenWidthDp { get; private set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000B0C5 File Offset: 0x000092C5
		// (set) Token: 0x060002D8 RID: 728 RVA: 0x0000B0CD File Offset: 0x000092CD
		public int smallestScreenWidthDp { get; private set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000B0D6 File Offset: 0x000092D6
		// (set) Token: 0x060002DA RID: 730 RVA: 0x0000B0DE File Offset: 0x000092DE
		private int screenLayout { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060002DB RID: 731 RVA: 0x0000B0E7 File Offset: 0x000092E7
		// (set) Token: 0x060002DC RID: 732 RVA: 0x0000B0EF File Offset: 0x000092EF
		public AndroidTouchScreen touchScreen { get; private set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060002DD RID: 733 RVA: 0x0000B0F8 File Offset: 0x000092F8
		// (set) Token: 0x060002DE RID: 734 RVA: 0x0000B100 File Offset: 0x00009300
		private int uiMode { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0000B109 File Offset: 0x00009309
		// (set) Token: 0x060002E0 RID: 736 RVA: 0x0000B111 File Offset: 0x00009311
		private string primaryLocaleCountry { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x0000B11A File Offset: 0x0000931A
		// (set) Token: 0x060002E2 RID: 738 RVA: 0x0000B122 File Offset: 0x00009322
		private string primaryLocaleLanguage { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x0000B12C File Offset: 0x0000932C
		public AndroidLocale[] locales
		{
			get
			{
				bool flag = this.primaryLocaleCountry == null && this.primaryLocaleLanguage == null;
				AndroidLocale[] array;
				if (flag)
				{
					array = new AndroidLocale[0];
				}
				else
				{
					array = new AndroidLocale[]
					{
						new AndroidLocale(this.primaryLocaleCountry, this.primaryLocaleLanguage)
					};
				}
				return array;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x0000B179 File Offset: 0x00009379
		public AndroidColorModeHdr colorModeHdr
		{
			get
			{
				return (AndroidColorModeHdr)(this.colorMode & 12);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x0000B184 File Offset: 0x00009384
		public AndroidColorModeWideColorGamut colorModeWideColorGamut
		{
			get
			{
				return (AndroidColorModeWideColorGamut)(this.colorMode & 3);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x0000B18E File Offset: 0x0000938E
		public AndroidScreenLayoutDirection screenLayoutDirection
		{
			get
			{
				return (AndroidScreenLayoutDirection)(this.screenLayout & 192);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x0000B19C File Offset: 0x0000939C
		public AndroidScreenLayoutLong screenLayoutLong
		{
			get
			{
				return (AndroidScreenLayoutLong)(this.screenLayout & 48);
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000B1A7 File Offset: 0x000093A7
		public AndroidScreenLayoutRound screenLayoutRound
		{
			get
			{
				return (AndroidScreenLayoutRound)(this.screenLayout & 768);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000B1B5 File Offset: 0x000093B5
		public AndroidScreenLayoutSize screenLayoutSize
		{
			get
			{
				return (AndroidScreenLayoutSize)(this.screenLayout & 15);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000B1C0 File Offset: 0x000093C0
		public AndroidUIModeNight uiModeNight
		{
			get
			{
				return (AndroidUIModeNight)(this.uiMode & 48);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000B1CB File Offset: 0x000093CB
		public AndroidUIModeType uiModeType
		{
			get
			{
				return (AndroidUIModeType)(this.uiMode & 15);
			}
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00002DE0 File Offset: 0x00000FE0
		public AndroidConfiguration()
		{
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000B1D6 File Offset: 0x000093D6
		public AndroidConfiguration(AndroidConfiguration otherConfiguration)
		{
			this.CopyFrom(otherConfiguration);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000B1E8 File Offset: 0x000093E8
		public void CopyFrom(AndroidConfiguration otherConfiguration)
		{
			this.colorMode = otherConfiguration.colorMode;
			this.densityDpi = otherConfiguration.densityDpi;
			this.fontScale = otherConfiguration.fontScale;
			this.fontWeightAdjustment = otherConfiguration.fontWeightAdjustment;
			this.keyboard = otherConfiguration.keyboard;
			this.hardKeyboardHidden = otherConfiguration.hardKeyboardHidden;
			this.keyboardHidden = otherConfiguration.keyboardHidden;
			this.mobileCountryCode = otherConfiguration.mobileCountryCode;
			this.mobileNetworkCode = otherConfiguration.mobileNetworkCode;
			this.navigation = otherConfiguration.navigation;
			this.navigationHidden = otherConfiguration.navigationHidden;
			this.orientation = otherConfiguration.orientation;
			this.screenHeightDp = otherConfiguration.screenHeightDp;
			this.screenWidthDp = otherConfiguration.screenWidthDp;
			this.smallestScreenWidthDp = otherConfiguration.smallestScreenWidthDp;
			this.screenLayout = otherConfiguration.screenLayout;
			this.touchScreen = otherConfiguration.touchScreen;
			this.uiMode = otherConfiguration.uiMode;
			this.primaryLocaleCountry = otherConfiguration.primaryLocaleCountry;
			this.primaryLocaleLanguage = otherConfiguration.primaryLocaleLanguage;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000B2FC File Offset: 0x000094FC
		[Preserve]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Format("* ColorMode, Hdr: {0}", this.colorModeHdr));
			stringBuilder.AppendLine(string.Format("* ColorMode, Gamut: {0}", this.colorModeWideColorGamut));
			stringBuilder.AppendLine(string.Format("* DensityDpi: {0}", this.densityDpi));
			stringBuilder.AppendLine(string.Format("* FontScale: {0}", this.fontScale));
			stringBuilder.AppendLine(string.Format("* FontWeightAdj: {0}", this.fontWeightAdjustment));
			stringBuilder.AppendLine(string.Format("* Keyboard: {0}", this.keyboard));
			stringBuilder.AppendLine(string.Format("* Keyboard Hidden, Hard: {0}", this.hardKeyboardHidden));
			stringBuilder.AppendLine(string.Format("* Keyboard Hidden, Normal: {0}", this.keyboardHidden));
			stringBuilder.AppendLine(string.Format("* Mcc: {0}", this.mobileCountryCode));
			stringBuilder.AppendLine(string.Format("* Mnc: {0}", this.mobileNetworkCode));
			stringBuilder.AppendLine(string.Format("* Navigation: {0}", this.navigation));
			stringBuilder.AppendLine(string.Format("* NavigationHidden: {0}", this.navigationHidden));
			stringBuilder.AppendLine(string.Format("* Orientation: {0}", this.orientation));
			stringBuilder.AppendLine(string.Format("* ScreenHeightDp: {0}", this.screenHeightDp));
			stringBuilder.AppendLine(string.Format("* ScreenWidthDp: {0}", this.screenWidthDp));
			stringBuilder.AppendLine(string.Format("* SmallestScreenWidthDp: {0}", this.smallestScreenWidthDp));
			stringBuilder.AppendLine(string.Format("* ScreenLayout, Direction: {0}", this.screenLayoutDirection));
			stringBuilder.AppendLine(string.Format("* ScreenLayout, Size: {0}", this.screenLayoutSize));
			stringBuilder.AppendLine(string.Format("* ScreenLayout, Long: {0}", this.screenLayoutLong));
			stringBuilder.AppendLine(string.Format("* ScreenLayout, Round: {0}", this.screenLayoutRound));
			stringBuilder.AppendLine(string.Format("* TouchScreen: {0}", this.touchScreen));
			stringBuilder.AppendLine(string.Format("* UiMode, Night: {0}", this.uiModeNight));
			stringBuilder.AppendLine(string.Format("* UiMode, Type: {0}", this.uiModeType));
			stringBuilder.AppendLine(string.Format("* Locales ({0}):", this.locales.Length));
			for (int i = 0; i < this.locales.Length; i++)
			{
				AndroidLocale androidLocale = this.locales[i];
				stringBuilder.AppendLine(string.Format("* Locale[{0}] {1}-{2}", i, androidLocale.country, androidLocale.language));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040000A4 RID: 164
		private const int UiModeNightMask = 48;

		// Token: 0x040000A5 RID: 165
		private const int UiModeTypeMask = 15;

		// Token: 0x040000A6 RID: 166
		private const int ScreenLayoutDirectionMask = 192;

		// Token: 0x040000A7 RID: 167
		private const int ScreenLayoutLongMask = 48;

		// Token: 0x040000A8 RID: 168
		private const int ScreenLayoutRoundMask = 768;

		// Token: 0x040000A9 RID: 169
		private const int ScreenLayoutSizeMask = 15;

		// Token: 0x040000AA RID: 170
		private const int ColorModeHdrMask = 12;

		// Token: 0x040000AB RID: 171
		private const int ColorModeWideColorGamutMask = 3;
	}
}
