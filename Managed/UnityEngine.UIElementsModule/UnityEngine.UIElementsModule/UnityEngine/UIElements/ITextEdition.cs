using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020004FD RID: 1277
	public interface ITextEdition
	{
		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x06002787 RID: 10119
		// (set) Token: 0x06002788 RID: 10120
		bool multiline { get; set; }

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x06002789 RID: 10121
		// (set) Token: 0x0600278A RID: 10122
		bool isReadOnly { get; set; }

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x0600278B RID: 10123
		// (set) Token: 0x0600278C RID: 10124
		int maxLength { get; set; }

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x0600278D RID: 10125
		// (set) Token: 0x0600278E RID: 10126
		string placeholder { get; set; }

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x0600278F RID: 10127
		// (set) Token: 0x06002790 RID: 10128
		bool isDelayed { get; set; }

		// Token: 0x06002791 RID: 10129
		void ResetValueAndText();

		// Token: 0x06002792 RID: 10130
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		void SaveValueAndText();

		// Token: 0x06002793 RID: 10131
		void RestoreValueAndText();

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x06002794 RID: 10132
		// (set) Token: 0x06002795 RID: 10133
		Func<char, bool> AcceptCharacter { get; set; }

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x06002796 RID: 10134
		// (set) Token: 0x06002797 RID: 10135
		Action<bool> UpdateScrollOffset { get; set; }

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06002798 RID: 10136
		// (set) Token: 0x06002799 RID: 10137
		Action UpdateValueFromText { get; set; }

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x0600279A RID: 10138
		// (set) Token: 0x0600279B RID: 10139
		Action UpdateTextFromValue { get; set; }

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x0600279C RID: 10140
		// (set) Token: 0x0600279D RID: 10141
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		Action MoveFocusToCompositeRoot { get; set; }

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x0600279E RID: 10142
		// (set) Token: 0x0600279F RID: 10143
		Func<string> GetDefaultValueType { get; set; }

		// Token: 0x060027A0 RID: 10144
		void UpdateText(string value);

		// Token: 0x060027A1 RID: 10145
		string CullString(string s);

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x060027A2 RID: 10146
		// (set) Token: 0x060027A3 RID: 10147
		char maskChar { get; set; }

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x060027A4 RID: 10148
		// (set) Token: 0x060027A5 RID: 10149
		bool isPassword { get; set; }

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x060027A6 RID: 10150
		// (set) Token: 0x060027A7 RID: 10151
		bool hidePlaceholderOnFocus { get; set; }

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x060027A8 RID: 10152 RVA: 0x000917E4 File Offset: 0x0008F9E4
		// (set) Token: 0x060027A9 RID: 10153 RVA: 0x00091817 File Offset: 0x0008FA17
		bool autoCorrection
		{
			get
			{
				Debug.Log("Type " + base.GetType().Name + " implementing interface ITextEdition is missing the implementation for autoCorrection. Calling ITextEdition.autoCorrection of this type will always return false.");
				return false;
			}
			set
			{
				Debug.Log("Type " + base.GetType().Name + " implementing interface ITextEdition is missing the implementation for autoCorrection. Assigning a value to ITextEdition.autoCorrection will not update its value.");
			}
		}

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x060027AA RID: 10154 RVA: 0x0009183C File Offset: 0x0008FA3C
		// (set) Token: 0x060027AB RID: 10155 RVA: 0x0009186F File Offset: 0x0008FA6F
		bool hideSoftKeyboard
		{
			get
			{
				Debug.Log("Type " + base.GetType().Name + " implementing interface ITextEdition is missing the implementation for hideSoftKeyboard. Calling ITextEdition.hideSoftKeyboard of this type will always return false.");
				return false;
			}
			set
			{
				Debug.Log("Type " + base.GetType().Name + " implementing interface ITextEdition is missing the implementation for hideSoftKeyboard. Assigning a value to ITextEdition.hideSoftKeyboard will not update its value.");
			}
		}

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x060027AC RID: 10156 RVA: 0x00091894 File Offset: 0x0008FA94
		// (set) Token: 0x060027AD RID: 10157 RVA: 0x000918C7 File Offset: 0x0008FAC7
		bool hideMobileInput
		{
			get
			{
				Debug.Log("Type " + base.GetType().Name + " implementing interface ITextEdition is missing the implementation for hideMobileInput. Calling ITextEdition.hideMobileInput of this type will always return false.");
				return false;
			}
			set
			{
				Debug.Log("Type " + base.GetType().Name + " implementing interface ITextEdition is missing the implementation for hideMobileInput. Assigning a value to ITextEdition.hideMobileInput will not update its value.");
			}
		}

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x060027AE RID: 10158 RVA: 0x000918EC File Offset: 0x0008FAEC
		TouchScreenKeyboard touchScreenKeyboard
		{
			get
			{
				Debug.Log("Type " + base.GetType().Name + " implementing interface ITextEdition is missing the implementation for touchScreenKeyboard. Calling ITextEdition.touchScreenKeyboard of this type will always return null.");
				return null;
			}
		}

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x060027AF RID: 10159 RVA: 0x00091920 File Offset: 0x0008FB20
		// (set) Token: 0x060027B0 RID: 10160 RVA: 0x00091953 File Offset: 0x0008FB53
		TouchScreenKeyboardType keyboardType
		{
			get
			{
				Debug.Log("Type " + base.GetType().Name + " implementing interface ITextEdition is missing the implementation for keyboardType. Calling ITextEdition.keyboardType of this type will always return Default.");
				return TouchScreenKeyboardType.Default;
			}
			set
			{
				Debug.Log("Type " + base.GetType().Name + " implementing interface ITextEdition is missing the implementation for keyboardType. Assigning a value to ITextEdition.keyboardType will not update its value.");
			}
		}
	}
}
