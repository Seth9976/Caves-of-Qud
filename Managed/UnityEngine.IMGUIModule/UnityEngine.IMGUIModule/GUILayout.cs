using System;

namespace UnityEngine
{
	// Token: 0x0200001B RID: 27
	public class GUILayout
	{
		// Token: 0x060001EA RID: 490 RVA: 0x00008AA6 File Offset: 0x00006CA6
		public static void Label(Texture image, params GUILayoutOption[] options)
		{
			GUILayout.DoLabel(GUIContent.Temp(image), GUI.skin.label, options);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00008AC0 File Offset: 0x00006CC0
		public static void Label(string text, params GUILayoutOption[] options)
		{
			GUILayout.DoLabel(GUIContent.Temp(text), GUI.skin.label, options);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00008ADA File Offset: 0x00006CDA
		public static void Label(GUIContent content, params GUILayoutOption[] options)
		{
			GUILayout.DoLabel(content, GUI.skin.label, options);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00008AEF File Offset: 0x00006CEF
		public static void Label(Texture image, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.DoLabel(GUIContent.Temp(image), style, options);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00008B00 File Offset: 0x00006D00
		public static void Label(string text, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.DoLabel(GUIContent.Temp(text), style, options);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00008B11 File Offset: 0x00006D11
		public static void Label(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.DoLabel(content, style, options);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00008B1D File Offset: 0x00006D1D
		private static void DoLabel(GUIContent content, GUIStyle style, GUILayoutOption[] options)
		{
			GUI.Label(GUILayoutUtility.GetRect(content, style, options), content, style);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00008B30 File Offset: 0x00006D30
		public static void Box(Texture image, params GUILayoutOption[] options)
		{
			GUILayout.DoBox(GUIContent.Temp(image), GUI.skin.box, options);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00008B4A File Offset: 0x00006D4A
		public static void Box(string text, params GUILayoutOption[] options)
		{
			GUILayout.DoBox(GUIContent.Temp(text), GUI.skin.box, options);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00008B64 File Offset: 0x00006D64
		public static void Box(GUIContent content, params GUILayoutOption[] options)
		{
			GUILayout.DoBox(content, GUI.skin.box, options);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00008B79 File Offset: 0x00006D79
		public static void Box(Texture image, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.DoBox(GUIContent.Temp(image), style, options);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00008B8A File Offset: 0x00006D8A
		public static void Box(string text, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.DoBox(GUIContent.Temp(text), style, options);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00008B9B File Offset: 0x00006D9B
		public static void Box(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.DoBox(content, style, options);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00008BA7 File Offset: 0x00006DA7
		private static void DoBox(GUIContent content, GUIStyle style, GUILayoutOption[] options)
		{
			GUI.Box(GUILayoutUtility.GetRect(content, style, options), content, style);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00008BBC File Offset: 0x00006DBC
		public static bool Button(Texture image, params GUILayoutOption[] options)
		{
			return GUILayout.DoButton(GUIContent.Temp(image), GUI.skin.button, options);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00008BE4 File Offset: 0x00006DE4
		public static bool Button(string text, params GUILayoutOption[] options)
		{
			return GUILayout.DoButton(GUIContent.Temp(text), GUI.skin.button, options);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00008C0C File Offset: 0x00006E0C
		public static bool Button(GUIContent content, params GUILayoutOption[] options)
		{
			return GUILayout.DoButton(content, GUI.skin.button, options);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00008C30 File Offset: 0x00006E30
		public static bool Button(Texture image, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoButton(GUIContent.Temp(image), style, options);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00008C50 File Offset: 0x00006E50
		public static bool Button(string text, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoButton(GUIContent.Temp(text), style, options);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00008C70 File Offset: 0x00006E70
		public static bool Button(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoButton(content, style, options);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00008C8C File Offset: 0x00006E8C
		private static bool DoButton(GUIContent content, GUIStyle style, GUILayoutOption[] options)
		{
			return GUI.Button(GUILayoutUtility.GetRect(content, style, options), content, style);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00008CB0 File Offset: 0x00006EB0
		public static bool RepeatButton(Texture image, params GUILayoutOption[] options)
		{
			return GUILayout.DoRepeatButton(GUIContent.Temp(image), GUI.skin.button, options);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00008CD8 File Offset: 0x00006ED8
		public static bool RepeatButton(string text, params GUILayoutOption[] options)
		{
			return GUILayout.DoRepeatButton(GUIContent.Temp(text), GUI.skin.button, options);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00008D00 File Offset: 0x00006F00
		public static bool RepeatButton(GUIContent content, params GUILayoutOption[] options)
		{
			return GUILayout.DoRepeatButton(content, GUI.skin.button, options);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00008D24 File Offset: 0x00006F24
		public static bool RepeatButton(Texture image, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoRepeatButton(GUIContent.Temp(image), style, options);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00008D44 File Offset: 0x00006F44
		public static bool RepeatButton(string text, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoRepeatButton(GUIContent.Temp(text), style, options);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00008D64 File Offset: 0x00006F64
		public static bool RepeatButton(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoRepeatButton(content, style, options);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00008D80 File Offset: 0x00006F80
		private static bool DoRepeatButton(GUIContent content, GUIStyle style, GUILayoutOption[] options)
		{
			return GUI.RepeatButton(GUILayoutUtility.GetRect(content, style, options), content, style);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00008DA4 File Offset: 0x00006FA4
		public static string TextField(string text, params GUILayoutOption[] options)
		{
			return GUILayout.DoTextField(text, -1, false, GUI.skin.textField, options);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00008DCC File Offset: 0x00006FCC
		public static string TextField(string text, int maxLength, params GUILayoutOption[] options)
		{
			return GUILayout.DoTextField(text, maxLength, false, GUI.skin.textField, options);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00008DF4 File Offset: 0x00006FF4
		public static string TextField(string text, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoTextField(text, -1, false, style, options);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00008E10 File Offset: 0x00007010
		public static string TextField(string text, int maxLength, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoTextField(text, maxLength, false, style, options);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00008E2C File Offset: 0x0000702C
		public static string PasswordField(string password, char maskChar, params GUILayoutOption[] options)
		{
			return GUILayout.PasswordField(password, maskChar, -1, GUI.skin.textField, options);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00008E54 File Offset: 0x00007054
		public static string PasswordField(string password, char maskChar, int maxLength, params GUILayoutOption[] options)
		{
			return GUILayout.PasswordField(password, maskChar, maxLength, GUI.skin.textField, options);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00008E7C File Offset: 0x0000707C
		public static string PasswordField(string password, char maskChar, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.PasswordField(password, maskChar, -1, style, options);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00008E98 File Offset: 0x00007098
		public static string PasswordField(string password, char maskChar, int maxLength, GUIStyle style, params GUILayoutOption[] options)
		{
			GUIContent guicontent = GUIContent.Temp(GUI.PasswordFieldGetStrToShow(password, maskChar));
			return GUI.PasswordField(GUILayoutUtility.GetRect(guicontent, GUI.skin.textField, options), password, maskChar, maxLength, style);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00008ED4 File Offset: 0x000070D4
		public static string TextArea(string text, params GUILayoutOption[] options)
		{
			return GUILayout.DoTextField(text, -1, true, GUI.skin.textArea, options);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00008EFC File Offset: 0x000070FC
		public static string TextArea(string text, int maxLength, params GUILayoutOption[] options)
		{
			return GUILayout.DoTextField(text, maxLength, true, GUI.skin.textArea, options);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00008F24 File Offset: 0x00007124
		public static string TextArea(string text, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoTextField(text, -1, true, style, options);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00008F40 File Offset: 0x00007140
		public static string TextArea(string text, int maxLength, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoTextField(text, maxLength, true, style, options);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00008F5C File Offset: 0x0000715C
		private static string DoTextField(string text, int maxLength, bool multiline, GUIStyle style, GUILayoutOption[] options)
		{
			int controlID = GUIUtility.GetControlID(FocusType.Keyboard);
			GUIContent guicontent = GUIContent.Temp(text);
			bool flag = GUIUtility.keyboardControl != controlID;
			if (flag)
			{
				guicontent = GUIContent.Temp(text);
			}
			else
			{
				guicontent = GUIContent.Temp(text + GUIUtility.compositionString);
			}
			Rect rect = GUILayoutUtility.GetRect(guicontent, style, options);
			bool flag2 = GUIUtility.keyboardControl == controlID;
			if (flag2)
			{
				guicontent = GUIContent.Temp(text);
			}
			GUI.DoTextField(rect, controlID, guicontent, multiline, maxLength, style);
			return guicontent.text;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00008FD8 File Offset: 0x000071D8
		public static bool Toggle(bool value, Texture image, params GUILayoutOption[] options)
		{
			return GUILayout.DoToggle(value, GUIContent.Temp(image), GUI.skin.toggle, options);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00009004 File Offset: 0x00007204
		public static bool Toggle(bool value, string text, params GUILayoutOption[] options)
		{
			return GUILayout.DoToggle(value, GUIContent.Temp(text), GUI.skin.toggle, options);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00009030 File Offset: 0x00007230
		public static bool Toggle(bool value, GUIContent content, params GUILayoutOption[] options)
		{
			return GUILayout.DoToggle(value, content, GUI.skin.toggle, options);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00009054 File Offset: 0x00007254
		public static bool Toggle(bool value, Texture image, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoToggle(value, GUIContent.Temp(image), style, options);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00009074 File Offset: 0x00007274
		public static bool Toggle(bool value, string text, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoToggle(value, GUIContent.Temp(text), style, options);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00009094 File Offset: 0x00007294
		public static bool Toggle(bool value, GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoToggle(value, content, style, options);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x000090B0 File Offset: 0x000072B0
		private static bool DoToggle(bool value, GUIContent content, GUIStyle style, GUILayoutOption[] options)
		{
			return GUI.Toggle(GUILayoutUtility.GetRect(content, style, options), value, content, style);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x000090D4 File Offset: 0x000072D4
		public static int Toolbar(int selected, string[] texts, params GUILayoutOption[] options)
		{
			return GUILayout.Toolbar(selected, GUIContent.Temp(texts), GUI.skin.button, options);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00009100 File Offset: 0x00007300
		public static int Toolbar(int selected, Texture[] images, params GUILayoutOption[] options)
		{
			return GUILayout.Toolbar(selected, GUIContent.Temp(images), GUI.skin.button, options);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000912C File Offset: 0x0000732C
		public static int Toolbar(int selected, GUIContent[] contents, params GUILayoutOption[] options)
		{
			return GUILayout.Toolbar(selected, contents, GUI.skin.button, options);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00009150 File Offset: 0x00007350
		public static int Toolbar(int selected, string[] texts, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.Toolbar(selected, GUIContent.Temp(texts), style, options);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00009170 File Offset: 0x00007370
		public static int Toolbar(int selected, Texture[] images, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.Toolbar(selected, GUIContent.Temp(images), style, options);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00009190 File Offset: 0x00007390
		public static int Toolbar(int selected, string[] texts, GUIStyle style, GUI.ToolbarButtonSize buttonSize, params GUILayoutOption[] options)
		{
			return GUILayout.Toolbar(selected, GUIContent.Temp(texts), style, buttonSize, options);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x000091B4 File Offset: 0x000073B4
		public static int Toolbar(int selected, Texture[] images, GUIStyle style, GUI.ToolbarButtonSize buttonSize, params GUILayoutOption[] options)
		{
			return GUILayout.Toolbar(selected, GUIContent.Temp(images), style, buttonSize, options);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x000091D8 File Offset: 0x000073D8
		public static int Toolbar(int selected, GUIContent[] contents, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.Toolbar(selected, contents, style, GUI.ToolbarButtonSize.Fixed, options);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x000091F4 File Offset: 0x000073F4
		public static int Toolbar(int selected, GUIContent[] contents, GUIStyle style, GUI.ToolbarButtonSize buttonSize, params GUILayoutOption[] options)
		{
			return GUILayout.Toolbar(selected, contents, null, style, buttonSize, options);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00009214 File Offset: 0x00007414
		public static int Toolbar(int selected, GUIContent[] contents, bool[] enabled, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.Toolbar(selected, contents, enabled, style, GUI.ToolbarButtonSize.Fixed, options);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00009234 File Offset: 0x00007434
		internal static int Toolbar(int selected, GUIContent[] contents, bool[] enabled, GUIStyle style, GUIStyle firstStyle, GUIStyle midStyle, GUIStyle lastStyle, params GUILayoutOption[] options)
		{
			return GUILayout.Toolbar(selected, contents, enabled, style, firstStyle, midStyle, lastStyle, GUI.ToolbarButtonSize.Fixed, options);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00009258 File Offset: 0x00007458
		public static int Toolbar(int selected, GUIContent[] contents, bool[] enabled, GUIStyle style, GUI.ToolbarButtonSize buttonSize, params GUILayoutOption[] options)
		{
			GUIStyle guistyle;
			GUIStyle guistyle2;
			GUIStyle guistyle3;
			GUI.FindStyles(ref style, out guistyle, out guistyle2, out guistyle3, "left", "mid", "right");
			return GUILayout.Toolbar(selected, contents, enabled, style, guistyle, guistyle2, guistyle3, buttonSize, options);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00009298 File Offset: 0x00007498
		internal static int Toolbar(int selected, GUIContent[] contents, bool[] enabled, GUIStyle style, GUIStyle firstStyle, GUIStyle midStyle, GUIStyle lastStyle, GUI.ToolbarButtonSize buttonSize, params GUILayoutOption[] options)
		{
			Vector2 vector = default(Vector2);
			int num = contents.Length;
			GUIStyle guistyle = ((num > 1) ? firstStyle : style);
			GUIStyle guistyle2 = ((num > 1) ? midStyle : style);
			GUIStyle guistyle3 = ((num > 1) ? lastStyle : style);
			float num2 = 0f;
			for (int i = 0; i < contents.Length; i++)
			{
				bool flag = i == num - 2;
				if (flag)
				{
					guistyle2 = guistyle3;
				}
				Vector2 vector2 = guistyle.CalcSize(contents[i]);
				if (buttonSize != GUI.ToolbarButtonSize.Fixed)
				{
					if (buttonSize == GUI.ToolbarButtonSize.FitToContents)
					{
						vector.x += vector2.x;
					}
				}
				else
				{
					bool flag2 = vector2.x > vector.x;
					if (flag2)
					{
						vector.x = vector2.x;
					}
				}
				bool flag3 = vector2.y > vector.y;
				if (flag3)
				{
					vector.y = vector2.y;
				}
				bool flag4 = i == num - 1;
				if (flag4)
				{
					num2 += (float)guistyle.margin.right;
				}
				else
				{
					num2 += (float)Mathf.Max(guistyle.margin.right, guistyle2.margin.left);
				}
				guistyle = guistyle2;
			}
			if (buttonSize != GUI.ToolbarButtonSize.Fixed)
			{
				if (buttonSize == GUI.ToolbarButtonSize.FitToContents)
				{
					vector.x += num2;
				}
			}
			else
			{
				vector.x = vector.x * (float)contents.Length + num2;
			}
			return GUI.Toolbar(GUILayoutUtility.GetRect(vector.x, vector.y, style, options), selected, contents, null, style, firstStyle, midStyle, lastStyle, buttonSize, enabled);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00009430 File Offset: 0x00007630
		public static int SelectionGrid(int selected, string[] texts, int xCount, params GUILayoutOption[] options)
		{
			return GUILayout.SelectionGrid(selected, GUIContent.Temp(texts), xCount, GUI.skin.button, options);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000945C File Offset: 0x0000765C
		public static int SelectionGrid(int selected, Texture[] images, int xCount, params GUILayoutOption[] options)
		{
			return GUILayout.SelectionGrid(selected, GUIContent.Temp(images), xCount, GUI.skin.button, options);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00009488 File Offset: 0x00007688
		public static int SelectionGrid(int selected, GUIContent[] content, int xCount, params GUILayoutOption[] options)
		{
			return GUILayout.SelectionGrid(selected, content, xCount, GUI.skin.button, options);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x000094B0 File Offset: 0x000076B0
		public static int SelectionGrid(int selected, string[] texts, int xCount, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.SelectionGrid(selected, GUIContent.Temp(texts), xCount, style, options);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x000094D4 File Offset: 0x000076D4
		public static int SelectionGrid(int selected, Texture[] images, int xCount, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.SelectionGrid(selected, GUIContent.Temp(images), xCount, style, options);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x000094F8 File Offset: 0x000076F8
		public static int SelectionGrid(int selected, GUIContent[] contents, int xCount, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUI.SelectionGrid(GUIGridSizer.GetRect(contents, xCount, style, options), selected, contents, xCount, style);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00009520 File Offset: 0x00007720
		public static float HorizontalSlider(float value, float leftValue, float rightValue, params GUILayoutOption[] options)
		{
			return GUILayout.DoHorizontalSlider(value, leftValue, rightValue, GUI.skin.horizontalSlider, GUI.skin.horizontalSliderThumb, options);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00009550 File Offset: 0x00007750
		public static float HorizontalSlider(float value, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb, params GUILayoutOption[] options)
		{
			return GUILayout.DoHorizontalSlider(value, leftValue, rightValue, slider, thumb, options);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00009570 File Offset: 0x00007770
		private static float DoHorizontalSlider(float value, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb, GUILayoutOption[] options)
		{
			return GUI.HorizontalSlider(GUILayoutUtility.GetRect(GUIContent.Temp("mmmm"), slider, options), value, leftValue, rightValue, slider, thumb);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x000095A0 File Offset: 0x000077A0
		public static float VerticalSlider(float value, float leftValue, float rightValue, params GUILayoutOption[] options)
		{
			return GUILayout.DoVerticalSlider(value, leftValue, rightValue, GUI.skin.verticalSlider, GUI.skin.verticalSliderThumb, options);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x000095D0 File Offset: 0x000077D0
		public static float VerticalSlider(float value, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb, params GUILayoutOption[] options)
		{
			return GUILayout.DoVerticalSlider(value, leftValue, rightValue, slider, thumb, options);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x000095F0 File Offset: 0x000077F0
		private static float DoVerticalSlider(float value, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb, params GUILayoutOption[] options)
		{
			return GUI.VerticalSlider(GUILayoutUtility.GetRect(GUIContent.Temp("\n\n\n\n\n"), slider, options), value, leftValue, rightValue, slider, thumb);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00009620 File Offset: 0x00007820
		public static float HorizontalScrollbar(float value, float size, float leftValue, float rightValue, params GUILayoutOption[] options)
		{
			return GUILayout.HorizontalScrollbar(value, size, leftValue, rightValue, GUI.skin.horizontalScrollbar, options);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00009648 File Offset: 0x00007848
		public static float HorizontalScrollbar(float value, float size, float leftValue, float rightValue, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUI.HorizontalScrollbar(GUILayoutUtility.GetRect(GUIContent.Temp("mmmm"), style, options), value, size, leftValue, rightValue, style);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00009678 File Offset: 0x00007878
		public static float VerticalScrollbar(float value, float size, float topValue, float bottomValue, params GUILayoutOption[] options)
		{
			return GUILayout.VerticalScrollbar(value, size, topValue, bottomValue, GUI.skin.verticalScrollbar, options);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x000096A0 File Offset: 0x000078A0
		public static float VerticalScrollbar(float value, float size, float topValue, float bottomValue, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUI.VerticalScrollbar(GUILayoutUtility.GetRect(GUIContent.Temp("\n\n\n\n"), style, options), value, size, topValue, bottomValue, style);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x000096D0 File Offset: 0x000078D0
		public static void Space(float pixels)
		{
			GUIUtility.CheckOnGUI();
			bool isVertical = GUILayoutUtility.current.topLevel.isVertical;
			if (isVertical)
			{
				GUILayoutUtility.GetRect(0f, pixels, GUILayoutUtility.spaceStyle, new GUILayoutOption[] { GUILayout.Height(pixels) });
			}
			else
			{
				GUILayoutUtility.GetRect(pixels, 0f, GUILayoutUtility.spaceStyle, new GUILayoutOption[] { GUILayout.Width(pixels) });
			}
			bool flag = Event.current.type == EventType.Layout;
			if (flag)
			{
				GUILayoutUtility.current.topLevel.entries[GUILayoutUtility.current.topLevel.entries.Count - 1].consideredForMargin = false;
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000977C File Offset: 0x0000797C
		public static void FlexibleSpace()
		{
			GUIUtility.CheckOnGUI();
			bool isVertical = GUILayoutUtility.current.topLevel.isVertical;
			GUILayoutOption guilayoutOption;
			if (isVertical)
			{
				guilayoutOption = GUILayout.ExpandHeight(true);
			}
			else
			{
				guilayoutOption = GUILayout.ExpandWidth(true);
			}
			guilayoutOption = new GUILayoutOption(guilayoutOption.type, 10000);
			GUILayoutUtility.GetRect(0f, 0f, GUILayoutUtility.spaceStyle, new GUILayoutOption[] { guilayoutOption });
			bool flag = Event.current.type == EventType.Layout;
			if (flag)
			{
				GUILayoutUtility.current.topLevel.entries[GUILayoutUtility.current.topLevel.entries.Count - 1].consideredForMargin = false;
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000982B File Offset: 0x00007A2B
		public static void BeginHorizontal(params GUILayoutOption[] options)
		{
			GUILayout.BeginHorizontal(GUIContent.none, GUIStyle.none, options);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000983F File Offset: 0x00007A3F
		public static void BeginHorizontal(GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.BeginHorizontal(GUIContent.none, style, options);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000984F File Offset: 0x00007A4F
		public static void BeginHorizontal(string text, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.BeginHorizontal(GUIContent.Temp(text), style, options);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00009860 File Offset: 0x00007A60
		public static void BeginHorizontal(Texture image, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.BeginHorizontal(GUIContent.Temp(image), style, options);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00009874 File Offset: 0x00007A74
		public static void BeginHorizontal(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayoutGroup guilayoutGroup = GUILayoutUtility.BeginLayoutGroup(style, options, typeof(GUILayoutGroup));
			guilayoutGroup.isVertical = false;
			bool flag = style != GUIStyle.none || content != GUIContent.none;
			if (flag)
			{
				GUI.Box(guilayoutGroup.rect, content, style);
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x000098C3 File Offset: 0x00007AC3
		public static void EndHorizontal()
		{
			GUILayoutUtility.EndLayoutGroup();
		}

		// Token: 0x0600023F RID: 575 RVA: 0x000098CC File Offset: 0x00007ACC
		public static void BeginVertical(params GUILayoutOption[] options)
		{
			GUILayout.BeginVertical(GUIContent.none, GUIStyle.none, options);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x000098E0 File Offset: 0x00007AE0
		public static void BeginVertical(GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.BeginVertical(GUIContent.none, style, options);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x000098F0 File Offset: 0x00007AF0
		public static void BeginVertical(string text, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.BeginVertical(GUIContent.Temp(text), style, options);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00009901 File Offset: 0x00007B01
		public static void BeginVertical(Texture image, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.BeginVertical(GUIContent.Temp(image), style, options);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00009914 File Offset: 0x00007B14
		public static void BeginVertical(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayoutGroup guilayoutGroup = GUILayoutUtility.BeginLayoutGroup(style, options, typeof(GUILayoutGroup));
			guilayoutGroup.isVertical = true;
			bool flag = style != GUIStyle.none || content != GUIContent.none;
			if (flag)
			{
				GUI.Box(guilayoutGroup.rect, content, style);
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000098C3 File Offset: 0x00007AC3
		public static void EndVertical()
		{
			GUILayoutUtility.EndLayoutGroup();
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00009963 File Offset: 0x00007B63
		public static void BeginArea(Rect screenRect)
		{
			GUILayout.BeginArea(screenRect, GUIContent.none, GUIStyle.none);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00009977 File Offset: 0x00007B77
		public static void BeginArea(Rect screenRect, string text)
		{
			GUILayout.BeginArea(screenRect, GUIContent.Temp(text), GUIStyle.none);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000998C File Offset: 0x00007B8C
		public static void BeginArea(Rect screenRect, Texture image)
		{
			GUILayout.BeginArea(screenRect, GUIContent.Temp(image), GUIStyle.none);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x000099A1 File Offset: 0x00007BA1
		public static void BeginArea(Rect screenRect, GUIContent content)
		{
			GUILayout.BeginArea(screenRect, content, GUIStyle.none);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x000099B1 File Offset: 0x00007BB1
		public static void BeginArea(Rect screenRect, GUIStyle style)
		{
			GUILayout.BeginArea(screenRect, GUIContent.none, style);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x000099C1 File Offset: 0x00007BC1
		public static void BeginArea(Rect screenRect, string text, GUIStyle style)
		{
			GUILayout.BeginArea(screenRect, GUIContent.Temp(text), style);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x000099D2 File Offset: 0x00007BD2
		public static void BeginArea(Rect screenRect, Texture image, GUIStyle style)
		{
			GUILayout.BeginArea(screenRect, GUIContent.Temp(image), style);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x000099E4 File Offset: 0x00007BE4
		public static void BeginArea(Rect screenRect, GUIContent content, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			GUILayoutGroup guilayoutGroup = GUILayoutUtility.BeginLayoutArea(style, typeof(GUILayoutGroup));
			bool flag = Event.current.type == EventType.Layout;
			if (flag)
			{
				guilayoutGroup.resetCoords = true;
				guilayoutGroup.minWidth = (guilayoutGroup.maxWidth = screenRect.width);
				guilayoutGroup.minHeight = (guilayoutGroup.maxHeight = screenRect.height);
				guilayoutGroup.rect = Rect.MinMaxRect(screenRect.xMin, screenRect.yMin, guilayoutGroup.rect.xMax, guilayoutGroup.rect.yMax);
			}
			GUI.BeginGroup(guilayoutGroup.rect, content, style);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00009A8C File Offset: 0x00007C8C
		public static void EndArea()
		{
			GUIUtility.CheckOnGUI();
			GUILayoutUtility.EndLayoutArea();
			bool flag = Event.current.type == EventType.Used;
			if (!flag)
			{
				GUI.EndGroup();
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00009AC0 File Offset: 0x00007CC0
		public static Vector2 BeginScrollView(Vector2 scrollPosition, params GUILayoutOption[] options)
		{
			return GUILayout.BeginScrollView(scrollPosition, false, false, GUI.skin.horizontalScrollbar, GUI.skin.verticalScrollbar, GUI.skin.scrollView, options);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00009AFC File Offset: 0x00007CFC
		public static Vector2 BeginScrollView(Vector2 scrollPosition, bool alwaysShowHorizontal, bool alwaysShowVertical, params GUILayoutOption[] options)
		{
			return GUILayout.BeginScrollView(scrollPosition, alwaysShowHorizontal, alwaysShowVertical, GUI.skin.horizontalScrollbar, GUI.skin.verticalScrollbar, GUI.skin.scrollView, options);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00009B38 File Offset: 0x00007D38
		public static Vector2 BeginScrollView(Vector2 scrollPosition, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, params GUILayoutOption[] options)
		{
			return GUILayout.BeginScrollView(scrollPosition, false, false, horizontalScrollbar, verticalScrollbar, GUI.skin.scrollView, options);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00009B60 File Offset: 0x00007D60
		public static Vector2 BeginScrollView(Vector2 scrollPosition, GUIStyle style)
		{
			GUILayoutOption[] array = null;
			return GUILayout.BeginScrollView(scrollPosition, style, array);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00009B7C File Offset: 0x00007D7C
		public static Vector2 BeginScrollView(Vector2 scrollPosition, GUIStyle style, params GUILayoutOption[] options)
		{
			string name = style.name;
			GUIStyle guistyle = GUI.skin.FindStyle(name + "VerticalScrollbar");
			bool flag = guistyle == null;
			if (flag)
			{
				guistyle = GUI.skin.verticalScrollbar;
			}
			GUIStyle guistyle2 = GUI.skin.FindStyle(name + "HorizontalScrollbar");
			bool flag2 = guistyle2 == null;
			if (flag2)
			{
				guistyle2 = GUI.skin.horizontalScrollbar;
			}
			return GUILayout.BeginScrollView(scrollPosition, false, false, guistyle2, guistyle, style, options);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00009BF8 File Offset: 0x00007DF8
		public static Vector2 BeginScrollView(Vector2 scrollPosition, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, params GUILayoutOption[] options)
		{
			return GUILayout.BeginScrollView(scrollPosition, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, GUI.skin.scrollView, options);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00009C24 File Offset: 0x00007E24
		public static Vector2 BeginScrollView(Vector2 scrollPosition, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, GUIStyle background, params GUILayoutOption[] options)
		{
			GUIUtility.CheckOnGUI();
			GUIScrollGroup guiscrollGroup = (GUIScrollGroup)GUILayoutUtility.BeginLayoutGroup(background, null, typeof(GUIScrollGroup));
			EventType type = Event.current.type;
			EventType eventType = type;
			if (eventType == EventType.Layout)
			{
				guiscrollGroup.resetCoords = true;
				guiscrollGroup.isVertical = true;
				guiscrollGroup.stretchWidth = 1;
				guiscrollGroup.stretchHeight = 1;
				guiscrollGroup.verticalScrollbar = verticalScrollbar;
				guiscrollGroup.horizontalScrollbar = horizontalScrollbar;
				guiscrollGroup.needsVerticalScrollbar = alwaysShowVertical;
				guiscrollGroup.needsHorizontalScrollbar = alwaysShowHorizontal;
				guiscrollGroup.ApplyOptions(options);
			}
			return GUI.BeginScrollView(guiscrollGroup.rect, scrollPosition, new Rect(0f, 0f, guiscrollGroup.clientWidth, guiscrollGroup.clientHeight), alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, background);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00009CDB File Offset: 0x00007EDB
		public static void EndScrollView()
		{
			GUILayout.EndScrollView(true);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00009CE5 File Offset: 0x00007EE5
		internal static void EndScrollView(bool handleScrollWheel)
		{
			GUILayoutUtility.EndLayoutGroup();
			GUI.EndScrollView(handleScrollWheel);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00009CF8 File Offset: 0x00007EF8
		public static Rect Window(int id, Rect screenRect, GUI.WindowFunction func, string text, params GUILayoutOption[] options)
		{
			return GUILayout.DoWindow(id, screenRect, func, GUIContent.Temp(text), GUI.skin.window, options);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00009D24 File Offset: 0x00007F24
		public static Rect Window(int id, Rect screenRect, GUI.WindowFunction func, Texture image, params GUILayoutOption[] options)
		{
			return GUILayout.DoWindow(id, screenRect, func, GUIContent.Temp(image), GUI.skin.window, options);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00009D50 File Offset: 0x00007F50
		public static Rect Window(int id, Rect screenRect, GUI.WindowFunction func, GUIContent content, params GUILayoutOption[] options)
		{
			return GUILayout.DoWindow(id, screenRect, func, content, GUI.skin.window, options);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00009D78 File Offset: 0x00007F78
		public static Rect Window(int id, Rect screenRect, GUI.WindowFunction func, string text, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoWindow(id, screenRect, func, GUIContent.Temp(text), style, options);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00009D9C File Offset: 0x00007F9C
		public static Rect Window(int id, Rect screenRect, GUI.WindowFunction func, Texture image, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoWindow(id, screenRect, func, GUIContent.Temp(image), style, options);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00009DC0 File Offset: 0x00007FC0
		public static Rect Window(int id, Rect screenRect, GUI.WindowFunction func, GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoWindow(id, screenRect, func, content, style, options);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00009DE0 File Offset: 0x00007FE0
		private static Rect DoWindow(int id, Rect screenRect, GUI.WindowFunction func, GUIContent content, GUIStyle style, GUILayoutOption[] options)
		{
			GUIUtility.CheckOnGUI();
			GUILayout.LayoutedWindow layoutedWindow = new GUILayout.LayoutedWindow(func, screenRect, content, options, style);
			return GUI.Window(id, screenRect, new GUI.WindowFunction(layoutedWindow.DoWindow), content, style);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00009E1C File Offset: 0x0000801C
		public static GUILayoutOption Width(float width)
		{
			return new GUILayoutOption(GUILayoutOption.Type.fixedWidth, width);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00009E3C File Offset: 0x0000803C
		public static GUILayoutOption MinWidth(float minWidth)
		{
			return new GUILayoutOption(GUILayoutOption.Type.minWidth, minWidth);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00009E5C File Offset: 0x0000805C
		public static GUILayoutOption MaxWidth(float maxWidth)
		{
			return new GUILayoutOption(GUILayoutOption.Type.maxWidth, maxWidth);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00009E7C File Offset: 0x0000807C
		public static GUILayoutOption Height(float height)
		{
			return new GUILayoutOption(GUILayoutOption.Type.fixedHeight, height);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00009E9C File Offset: 0x0000809C
		public static GUILayoutOption MinHeight(float minHeight)
		{
			return new GUILayoutOption(GUILayoutOption.Type.minHeight, minHeight);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00009EBC File Offset: 0x000080BC
		public static GUILayoutOption MaxHeight(float maxHeight)
		{
			return new GUILayoutOption(GUILayoutOption.Type.maxHeight, maxHeight);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00009EDC File Offset: 0x000080DC
		public static GUILayoutOption ExpandWidth(bool expand)
		{
			return new GUILayoutOption(GUILayoutOption.Type.stretchWidth, expand ? 1 : 0);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00009F00 File Offset: 0x00008100
		public static GUILayoutOption ExpandHeight(bool expand)
		{
			return new GUILayoutOption(GUILayoutOption.Type.stretchHeight, expand ? 1 : 0);
		}

		// Token: 0x0200001C RID: 28
		private sealed class LayoutedWindow
		{
			// Token: 0x06000267 RID: 615 RVA: 0x00009F24 File Offset: 0x00008124
			internal LayoutedWindow(GUI.WindowFunction f, Rect screenRect, GUIContent content, GUILayoutOption[] options, GUIStyle style)
			{
				this.m_Func = f;
				this.m_ScreenRect = screenRect;
				this.m_Options = options;
				this.m_Style = style;
			}

			// Token: 0x06000268 RID: 616 RVA: 0x00009F4C File Offset: 0x0000814C
			public void DoWindow(int windowID)
			{
				GUILayoutGroup topLevel = GUILayoutUtility.current.topLevel;
				EventType type = Event.current.type;
				EventType eventType = type;
				if (eventType != EventType.Layout)
				{
					topLevel.ResetCursor();
				}
				else
				{
					topLevel.resetCoords = true;
					topLevel.rect = this.m_ScreenRect;
					bool flag = this.m_Options != null;
					if (flag)
					{
						topLevel.ApplyOptions(this.m_Options);
					}
					topLevel.isWindow = true;
					topLevel.windowID = windowID;
					topLevel.style = this.m_Style;
				}
				this.m_Func(windowID);
			}

			// Token: 0x04000081 RID: 129
			private readonly GUI.WindowFunction m_Func;

			// Token: 0x04000082 RID: 130
			private readonly Rect m_ScreenRect;

			// Token: 0x04000083 RID: 131
			private readonly GUILayoutOption[] m_Options;

			// Token: 0x04000084 RID: 132
			private readonly GUIStyle m_Style;
		}

		// Token: 0x0200001D RID: 29
		public class HorizontalScope : GUI.Scope
		{
			// Token: 0x06000269 RID: 617 RVA: 0x00009FD8 File Offset: 0x000081D8
			public HorizontalScope(params GUILayoutOption[] options)
			{
				GUILayout.BeginHorizontal(options);
			}

			// Token: 0x0600026A RID: 618 RVA: 0x00009FE9 File Offset: 0x000081E9
			public HorizontalScope(GUIStyle style, params GUILayoutOption[] options)
			{
				GUILayout.BeginHorizontal(style, options);
			}

			// Token: 0x0600026B RID: 619 RVA: 0x00009FFB File Offset: 0x000081FB
			public HorizontalScope(string text, GUIStyle style, params GUILayoutOption[] options)
			{
				GUILayout.BeginHorizontal(text, style, options);
			}

			// Token: 0x0600026C RID: 620 RVA: 0x0000A00E File Offset: 0x0000820E
			public HorizontalScope(Texture image, GUIStyle style, params GUILayoutOption[] options)
			{
				GUILayout.BeginHorizontal(image, style, options);
			}

			// Token: 0x0600026D RID: 621 RVA: 0x0000A021 File Offset: 0x00008221
			public HorizontalScope(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
			{
				GUILayout.BeginHorizontal(content, style, options);
			}

			// Token: 0x0600026E RID: 622 RVA: 0x0000A034 File Offset: 0x00008234
			protected override void CloseScope()
			{
				GUILayout.EndHorizontal();
			}
		}

		// Token: 0x0200001E RID: 30
		public class VerticalScope : GUI.Scope
		{
			// Token: 0x0600026F RID: 623 RVA: 0x0000A03D File Offset: 0x0000823D
			public VerticalScope(params GUILayoutOption[] options)
			{
				GUILayout.BeginVertical(options);
			}

			// Token: 0x06000270 RID: 624 RVA: 0x0000A04E File Offset: 0x0000824E
			public VerticalScope(GUIStyle style, params GUILayoutOption[] options)
			{
				GUILayout.BeginVertical(style, options);
			}

			// Token: 0x06000271 RID: 625 RVA: 0x0000A060 File Offset: 0x00008260
			public VerticalScope(string text, GUIStyle style, params GUILayoutOption[] options)
			{
				GUILayout.BeginVertical(text, style, options);
			}

			// Token: 0x06000272 RID: 626 RVA: 0x0000A073 File Offset: 0x00008273
			public VerticalScope(Texture image, GUIStyle style, params GUILayoutOption[] options)
			{
				GUILayout.BeginVertical(image, style, options);
			}

			// Token: 0x06000273 RID: 627 RVA: 0x0000A086 File Offset: 0x00008286
			public VerticalScope(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
			{
				GUILayout.BeginVertical(content, style, options);
			}

			// Token: 0x06000274 RID: 628 RVA: 0x0000A099 File Offset: 0x00008299
			protected override void CloseScope()
			{
				GUILayout.EndVertical();
			}
		}

		// Token: 0x0200001F RID: 31
		public class AreaScope : GUI.Scope
		{
			// Token: 0x06000275 RID: 629 RVA: 0x0000A0A2 File Offset: 0x000082A2
			public AreaScope(Rect screenRect)
			{
				GUILayout.BeginArea(screenRect);
			}

			// Token: 0x06000276 RID: 630 RVA: 0x0000A0B3 File Offset: 0x000082B3
			public AreaScope(Rect screenRect, string text)
			{
				GUILayout.BeginArea(screenRect, text);
			}

			// Token: 0x06000277 RID: 631 RVA: 0x0000A0C5 File Offset: 0x000082C5
			public AreaScope(Rect screenRect, Texture image)
			{
				GUILayout.BeginArea(screenRect, image);
			}

			// Token: 0x06000278 RID: 632 RVA: 0x0000A0D7 File Offset: 0x000082D7
			public AreaScope(Rect screenRect, GUIContent content)
			{
				GUILayout.BeginArea(screenRect, content);
			}

			// Token: 0x06000279 RID: 633 RVA: 0x0000A0E9 File Offset: 0x000082E9
			public AreaScope(Rect screenRect, string text, GUIStyle style)
			{
				GUILayout.BeginArea(screenRect, text, style);
			}

			// Token: 0x0600027A RID: 634 RVA: 0x0000A0FC File Offset: 0x000082FC
			public AreaScope(Rect screenRect, Texture image, GUIStyle style)
			{
				GUILayout.BeginArea(screenRect, image, style);
			}

			// Token: 0x0600027B RID: 635 RVA: 0x0000A10F File Offset: 0x0000830F
			public AreaScope(Rect screenRect, GUIContent content, GUIStyle style)
			{
				GUILayout.BeginArea(screenRect, content, style);
			}

			// Token: 0x0600027C RID: 636 RVA: 0x0000A122 File Offset: 0x00008322
			protected override void CloseScope()
			{
				GUILayout.EndArea();
			}
		}

		// Token: 0x02000020 RID: 32
		public class ScrollViewScope : GUI.Scope
		{
			// Token: 0x17000041 RID: 65
			// (get) Token: 0x0600027D RID: 637 RVA: 0x0000A12B File Offset: 0x0000832B
			// (set) Token: 0x0600027E RID: 638 RVA: 0x0000A133 File Offset: 0x00008333
			public Vector2 scrollPosition { get; private set; }

			// Token: 0x17000042 RID: 66
			// (get) Token: 0x0600027F RID: 639 RVA: 0x0000A13C File Offset: 0x0000833C
			// (set) Token: 0x06000280 RID: 640 RVA: 0x0000A144 File Offset: 0x00008344
			public bool handleScrollWheel { get; set; }

			// Token: 0x06000281 RID: 641 RVA: 0x0000A14D File Offset: 0x0000834D
			public ScrollViewScope(Vector2 scrollPosition, params GUILayoutOption[] options)
			{
				this.handleScrollWheel = true;
				this.scrollPosition = GUILayout.BeginScrollView(scrollPosition, options);
			}

			// Token: 0x06000282 RID: 642 RVA: 0x0000A16D File Offset: 0x0000836D
			public ScrollViewScope(Vector2 scrollPosition, bool alwaysShowHorizontal, bool alwaysShowVertical, params GUILayoutOption[] options)
			{
				this.handleScrollWheel = true;
				this.scrollPosition = GUILayout.BeginScrollView(scrollPosition, alwaysShowHorizontal, alwaysShowVertical, options);
			}

			// Token: 0x06000283 RID: 643 RVA: 0x0000A190 File Offset: 0x00008390
			public ScrollViewScope(Vector2 scrollPosition, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, params GUILayoutOption[] options)
			{
				this.handleScrollWheel = true;
				this.scrollPosition = GUILayout.BeginScrollView(scrollPosition, horizontalScrollbar, verticalScrollbar, options);
			}

			// Token: 0x06000284 RID: 644 RVA: 0x0000A1B3 File Offset: 0x000083B3
			public ScrollViewScope(Vector2 scrollPosition, GUIStyle style, params GUILayoutOption[] options)
			{
				this.handleScrollWheel = true;
				this.scrollPosition = GUILayout.BeginScrollView(scrollPosition, style, options);
			}

			// Token: 0x06000285 RID: 645 RVA: 0x0000A1D4 File Offset: 0x000083D4
			public ScrollViewScope(Vector2 scrollPosition, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, params GUILayoutOption[] options)
			{
				this.handleScrollWheel = true;
				this.scrollPosition = GUILayout.BeginScrollView(scrollPosition, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, options);
			}

			// Token: 0x06000286 RID: 646 RVA: 0x0000A1FB File Offset: 0x000083FB
			public ScrollViewScope(Vector2 scrollPosition, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, GUIStyle background, params GUILayoutOption[] options)
			{
				this.handleScrollWheel = true;
				this.scrollPosition = GUILayout.BeginScrollView(scrollPosition, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, background, options);
			}

			// Token: 0x06000287 RID: 647 RVA: 0x0000A224 File Offset: 0x00008424
			protected override void CloseScope()
			{
				GUILayout.EndScrollView(this.handleScrollWheel);
			}
		}
	}
}
