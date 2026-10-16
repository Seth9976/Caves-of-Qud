using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x02000009 RID: 9
	[NativeHeader("Modules/IMGUI/GUISkin.bindings.h")]
	[NativeHeader("Modules/IMGUI/GUI.bindings.h")]
	public class GUI
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00004100 File Offset: 0x00002300
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00004118 File Offset: 0x00002318
		public static Color color
		{
			get
			{
				Color color;
				GUI.get_color_Injected(out color);
				return color;
			}
			set
			{
				GUI.set_color_Injected(ref value);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000080 RID: 128 RVA: 0x0000412C File Offset: 0x0000232C
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00004144 File Offset: 0x00002344
		public static Color backgroundColor
		{
			get
			{
				Color color;
				GUI.get_backgroundColor_Injected(out color);
				return color;
			}
			set
			{
				GUI.set_backgroundColor_Injected(ref value);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00004158 File Offset: 0x00002358
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00004170 File Offset: 0x00002370
		public static Color contentColor
		{
			get
			{
				Color color;
				GUI.get_contentColor_Injected(out color);
				return color;
			}
			set
			{
				GUI.set_contentColor_Injected(ref value);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000084 RID: 132
		// (set) Token: 0x06000085 RID: 133
		public static extern bool changed
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000086 RID: 134
		// (set) Token: 0x06000087 RID: 135
		public static extern bool enabled
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000088 RID: 136
		// (set) Token: 0x06000089 RID: 137
		public static extern int depth
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600008A RID: 138
		internal static extern bool usePageScrollbars
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600008B RID: 139
		// (set) Token: 0x0600008C RID: 140
		internal static extern bool isInsideList
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00004184 File Offset: 0x00002384
		internal static Material blendMaterial
		{
			[FreeFunction("GetGUIBlendMaterial")]
			get
			{
				return Unmarshal.UnmarshalUnityObject<Material>(GUI.get_blendMaterial_Injected());
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600008E RID: 142 RVA: 0x0000419C File Offset: 0x0000239C
		internal static Material blitMaterial
		{
			[FreeFunction("GetGUIBlitMaterial")]
			get
			{
				return Unmarshal.UnmarshalUnityObject<Material>(GUI.get_blitMaterial_Injected());
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600008F RID: 143 RVA: 0x000041B4 File Offset: 0x000023B4
		internal static Material roundedRectMaterial
		{
			[FreeFunction("GetGUIRoundedRectMaterial")]
			get
			{
				return Unmarshal.UnmarshalUnityObject<Material>(GUI.get_roundedRectMaterial_Injected());
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000090 RID: 144 RVA: 0x000041CC File Offset: 0x000023CC
		internal static Material roundedRectWithColorPerBorderMaterial
		{
			[FreeFunction("GetGUIRoundedRectWithColorPerBorderMaterial")]
			get
			{
				return Unmarshal.UnmarshalUnityObject<Material>(GUI.get_roundedRectWithColorPerBorderMaterial_Injected());
			}
		}

		// Token: 0x06000091 RID: 145
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void GrabMouseControl(int id);

		// Token: 0x06000092 RID: 146
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool HasMouseControl(int id);

		// Token: 0x06000093 RID: 147
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ReleaseMouseControl();

		// Token: 0x06000094 RID: 148 RVA: 0x000041E4 File Offset: 0x000023E4
		[FreeFunction("GetGUIState().SetNameOfNextControl")]
		public unsafe static void SetNextControlName(string name)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				GUI.SetNextControlName_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004238 File Offset: 0x00002438
		[FreeFunction("GetGUIState().GetNameOfFocusedControl")]
		public static string GetNameOfFocusedControl()
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				GUI.GetNameOfFocusedControl_Injected(out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004268 File Offset: 0x00002468
		[FreeFunction("GetGUIState().FocusKeyboardControl")]
		public unsafe static void FocusControl(string name)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				GUI.FocusControl_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000097 RID: 151
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalRepaintEditorWindow();

		// Token: 0x06000098 RID: 152 RVA: 0x000042BC File Offset: 0x000024BC
		private static string Internal_GetTooltip()
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				GUI.Internal_GetTooltip_Injected(out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000042EC File Offset: 0x000024EC
		private unsafe static void Internal_SetTooltip(string value)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(value, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = value.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				GUI.Internal_SetTooltip_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004340 File Offset: 0x00002540
		private static string Internal_GetMouseTooltip()
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				GUI.Internal_GetMouseTooltip_Injected(out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004370 File Offset: 0x00002570
		private static Rect Internal_DoModalWindow(int id, int instanceID, Rect clientRect, GUI.WindowFunction func, GUIContent content, [Unmarshalled] GUIStyle style, object skin)
		{
			Rect rect;
			GUI.Internal_DoModalWindow_Injected(id, instanceID, ref clientRect, func, content, style, skin, out rect);
			return rect;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004390 File Offset: 0x00002590
		private static Rect Internal_DoWindow(int id, int instanceID, Rect clientRect, GUI.WindowFunction func, GUIContent title, [Unmarshalled] GUIStyle style, object skin, bool forceRectOnLayout)
		{
			Rect rect;
			GUI.Internal_DoWindow_Injected(id, instanceID, ref clientRect, func, title, style, skin, forceRectOnLayout, out rect);
			return rect;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000043B4 File Offset: 0x000025B4
		public static void DragWindow(Rect position)
		{
			GUI.DragWindow_Injected(ref position);
		}

		// Token: 0x0600009E RID: 158
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void BringWindowToFront(int windowID);

		// Token: 0x0600009F RID: 159
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void BringWindowToBack(int windowID);

		// Token: 0x060000A0 RID: 160
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void FocusWindow(int windowID);

		// Token: 0x060000A1 RID: 161
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void UnfocusWindow();

		// Token: 0x060000A2 RID: 162
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_BeginWindows();

		// Token: 0x060000A3 RID: 163
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_EndWindows();

		// Token: 0x060000A4 RID: 164 RVA: 0x000043C8 File Offset: 0x000025C8
		internal static string Internal_Concatenate(GUIContent first, GUIContent second)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				GUI.Internal_Concatenate_Injected(first, second, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x0000449D File Offset: 0x0000269D
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x000044A4 File Offset: 0x000026A4
		internal static int scrollTroughSide { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x000044AC File Offset: 0x000026AC
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x000044B3 File Offset: 0x000026B3
		internal static DateTime nextScrollStepTime { get; set; } = DateTime.Now;

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000AB RID: 171 RVA: 0x000044CC File Offset: 0x000026CC
		// (set) Token: 0x060000AA RID: 170 RVA: 0x000044BB File Offset: 0x000026BB
		public static GUISkin skin
		{
			get
			{
				GUIUtility.CheckOnGUI();
				return GUI.s_Skin;
			}
			set
			{
				GUIUtility.CheckOnGUI();
				GUI.DoSetSkin(value);
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000044EC File Offset: 0x000026EC
		internal static void DoSetSkin(GUISkin newSkin)
		{
			bool flag = !newSkin;
			if (flag)
			{
				newSkin = GUIUtility.GetDefaultSkin();
			}
			GUI.s_Skin = newSkin;
			newSkin.MakeCurrent();
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000451B File Offset: 0x0000271B
		internal static void CleanupRoots()
		{
			GUI.s_Skin = null;
			GUIUtility.CleanupRoots();
			GUILayoutUtility.CleanupRoots();
			GUISkin.CleanupRoots();
			GUIStyle.CleanupRoots();
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000AE RID: 174 RVA: 0x0000453C File Offset: 0x0000273C
		// (set) Token: 0x060000AF RID: 175 RVA: 0x00004553 File Offset: 0x00002753
		public static Matrix4x4 matrix
		{
			get
			{
				return GUIClip.GetMatrix();
			}
			set
			{
				GUIClip.SetMatrix(value);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00004560 File Offset: 0x00002760
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00004577 File Offset: 0x00002777
		public static string tooltip
		{
			get
			{
				return GUI.Internal_GetTooltip();
			}
			set
			{
				GUI.Internal_SetTooltip(value);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00004581 File Offset: 0x00002781
		protected static string mouseTooltip
		{
			get
			{
				return GUI.Internal_GetMouseTooltip();
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00004588 File Offset: 0x00002788
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x0000459F File Offset: 0x0000279F
		protected static Rect tooltipRect
		{
			get
			{
				return GUI.s_ToolTipRect;
			}
			set
			{
				GUI.s_ToolTipRect = value;
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000045A8 File Offset: 0x000027A8
		public static void Label(Rect position, string text)
		{
			GUI.Label(position, GUIContent.Temp(text), GUI.s_Skin.label);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000045C2 File Offset: 0x000027C2
		public static void Label(Rect position, Texture image)
		{
			GUI.Label(position, GUIContent.Temp(image), GUI.s_Skin.label);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000045DC File Offset: 0x000027DC
		public static void Label(Rect position, GUIContent content)
		{
			GUI.Label(position, content, GUI.s_Skin.label);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000045F1 File Offset: 0x000027F1
		public static void Label(Rect position, string text, GUIStyle style)
		{
			GUI.Label(position, GUIContent.Temp(text), style);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004602 File Offset: 0x00002802
		public static void Label(Rect position, Texture image, GUIStyle style)
		{
			GUI.Label(position, GUIContent.Temp(image), style);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004613 File Offset: 0x00002813
		public static void Label(Rect position, GUIContent content, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			GUI.DoLabel(position, content, style);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004625 File Offset: 0x00002825
		public static void DrawTexture(Rect position, Texture image)
		{
			GUI.DrawTexture(position, image, ScaleMode.StretchToFill);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004631 File Offset: 0x00002831
		public static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode)
		{
			GUI.DrawTexture(position, image, scaleMode, true);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000463E File Offset: 0x0000283E
		public static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode, bool alphaBlend)
		{
			GUI.DrawTexture(position, image, scaleMode, alphaBlend, 0f);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004650 File Offset: 0x00002850
		public static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode, bool alphaBlend, float imageAspect)
		{
			GUI.DrawTexture(position, image, scaleMode, alphaBlend, imageAspect, GUI.color, 0f, 0f);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004670 File Offset: 0x00002870
		public static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode, bool alphaBlend, float imageAspect, Color color, float borderWidth, float borderRadius)
		{
			Vector4 vector = Vector4.one * borderWidth;
			GUI.DrawTexture(position, image, scaleMode, alphaBlend, imageAspect, color, vector, borderRadius);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000469C File Offset: 0x0000289C
		public static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode, bool alphaBlend, float imageAspect, Color color, Vector4 borderWidths, float borderRadius)
		{
			Vector4 vector = Vector4.one * borderRadius;
			GUI.DrawTexture(position, image, scaleMode, alphaBlend, imageAspect, color, borderWidths, vector);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000046C8 File Offset: 0x000028C8
		public static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode, bool alphaBlend, float imageAspect, Color color, Vector4 borderWidths, Vector4 borderRadiuses)
		{
			GUI.DrawTexture(position, image, scaleMode, alphaBlend, imageAspect, color, borderWidths, borderRadiuses, true);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000046EC File Offset: 0x000028EC
		internal static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode, bool alphaBlend, float imageAspect, Color color, Vector4 borderWidths, Vector4 borderRadiuses, bool drawSmoothCorners)
		{
			GUI.DrawTexture(position, image, scaleMode, alphaBlend, imageAspect, color, color, color, color, borderWidths, borderRadiuses, drawSmoothCorners);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004714 File Offset: 0x00002914
		internal static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode, bool alphaBlend, float imageAspect, Color leftColor, Color topColor, Color rightColor, Color bottomColor, Vector4 borderWidths, Vector4 borderRadiuses)
		{
			GUI.DrawTexture(position, image, scaleMode, alphaBlend, imageAspect, leftColor, topColor, rightColor, bottomColor, borderWidths, borderRadiuses, true);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000473C File Offset: 0x0000293C
		internal static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode, bool alphaBlend, float imageAspect, Color leftColor, Color topColor, Color rightColor, Color bottomColor, Vector4 borderWidths, Vector4 borderRadiuses, bool drawSmoothCorners)
		{
			GUIUtility.CheckOnGUI();
			bool flag = Event.current.type == EventType.Repaint;
			if (flag)
			{
				bool flag2 = image == null;
				if (flag2)
				{
					Debug.LogWarning("null texture passed to GUI.DrawTexture");
				}
				else
				{
					bool flag3 = imageAspect == 0f;
					if (flag3)
					{
						imageAspect = (float)image.width / (float)image.height;
					}
					bool flag4 = borderWidths != Vector4.zero;
					Material material;
					if (flag4)
					{
						bool flag5 = leftColor != topColor || leftColor != rightColor || leftColor != bottomColor;
						if (flag5)
						{
							material = GUI.roundedRectWithColorPerBorderMaterial;
						}
						else
						{
							material = GUI.roundedRectMaterial;
						}
					}
					else
					{
						bool flag6 = borderRadiuses != Vector4.zero;
						if (flag6)
						{
							material = GUI.roundedRectMaterial;
						}
						else
						{
							material = (alphaBlend ? GUI.blendMaterial : GUI.blitMaterial);
						}
					}
					Internal_DrawTextureArguments internal_DrawTextureArguments = new Internal_DrawTextureArguments
					{
						leftBorder = 0,
						rightBorder = 0,
						topBorder = 0,
						bottomBorder = 0,
						color = leftColor,
						leftBorderColor = leftColor,
						topBorderColor = topColor,
						rightBorderColor = rightColor,
						bottomBorderColor = bottomColor,
						borderWidths = borderWidths,
						cornerRadiuses = borderRadiuses,
						texture = image,
						smoothCorners = drawSmoothCorners,
						mat = material
					};
					GUI.CalculateScaledTextureRects(position, scaleMode, imageAspect, ref internal_DrawTextureArguments.screenRect, ref internal_DrawTextureArguments.sourceRect);
					Graphics.Internal_DrawTexture(ref internal_DrawTextureArguments);
				}
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000048C8 File Offset: 0x00002AC8
		internal static bool CalculateScaledTextureRects(Rect position, ScaleMode scaleMode, float imageAspect, ref Rect outScreenRect, ref Rect outSourceRect)
		{
			float num = position.width / position.height;
			bool flag = false;
			switch (scaleMode)
			{
			case ScaleMode.StretchToFill:
				outScreenRect = position;
				outSourceRect = new Rect(0f, 0f, 1f, 1f);
				flag = true;
				break;
			case ScaleMode.ScaleAndCrop:
			{
				bool flag2 = num > imageAspect;
				if (flag2)
				{
					float num2 = imageAspect / num;
					outScreenRect = position;
					outSourceRect = new Rect(0f, (1f - num2) * 0.5f, 1f, num2);
					flag = true;
				}
				else
				{
					float num3 = num / imageAspect;
					outScreenRect = position;
					outSourceRect = new Rect(0.5f - num3 * 0.5f, 0f, num3, 1f);
					flag = true;
				}
				break;
			}
			case ScaleMode.ScaleToFit:
			{
				bool flag3 = num > imageAspect;
				if (flag3)
				{
					float num4 = imageAspect / num;
					outScreenRect = new Rect(position.xMin + position.width * (1f - num4) * 0.5f, position.yMin, num4 * position.width, position.height);
					outSourceRect = new Rect(0f, 0f, 1f, 1f);
					flag = true;
				}
				else
				{
					float num5 = num / imageAspect;
					outScreenRect = new Rect(position.xMin, position.yMin + position.height * (1f - num5) * 0.5f, position.width, num5 * position.height);
					outSourceRect = new Rect(0f, 0f, 1f, 1f);
					flag = true;
				}
				break;
			}
			}
			return flag;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004A97 File Offset: 0x00002C97
		public static void DrawTextureWithTexCoords(Rect position, Texture image, Rect texCoords)
		{
			GUI.DrawTextureWithTexCoords(position, image, texCoords, true);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004AA4 File Offset: 0x00002CA4
		public static void DrawTextureWithTexCoords(Rect position, Texture image, Rect texCoords, bool alphaBlend)
		{
			GUIUtility.CheckOnGUI();
			bool flag = Event.current.type == EventType.Repaint;
			if (flag)
			{
				Material material = (alphaBlend ? GUI.blendMaterial : GUI.blitMaterial);
				Internal_DrawTextureArguments internal_DrawTextureArguments = default(Internal_DrawTextureArguments);
				internal_DrawTextureArguments.texture = image;
				internal_DrawTextureArguments.mat = material;
				internal_DrawTextureArguments.leftBorder = 0;
				internal_DrawTextureArguments.rightBorder = 0;
				internal_DrawTextureArguments.topBorder = 0;
				internal_DrawTextureArguments.bottomBorder = 0;
				internal_DrawTextureArguments.color = GUI.color;
				internal_DrawTextureArguments.leftBorderColor = GUI.color;
				internal_DrawTextureArguments.topBorderColor = GUI.color;
				internal_DrawTextureArguments.rightBorderColor = GUI.color;
				internal_DrawTextureArguments.bottomBorderColor = GUI.color;
				internal_DrawTextureArguments.screenRect = position;
				internal_DrawTextureArguments.sourceRect = texCoords;
				Graphics.Internal_DrawTexture(ref internal_DrawTextureArguments);
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004B6A File Offset: 0x00002D6A
		public static void Box(Rect position, string text)
		{
			GUI.Box(position, GUIContent.Temp(text), GUI.s_Skin.box);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004B84 File Offset: 0x00002D84
		public static void Box(Rect position, Texture image)
		{
			GUI.Box(position, GUIContent.Temp(image), GUI.s_Skin.box);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004B9E File Offset: 0x00002D9E
		public static void Box(Rect position, GUIContent content)
		{
			GUI.Box(position, content, GUI.s_Skin.box);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004BB3 File Offset: 0x00002DB3
		public static void Box(Rect position, string text, GUIStyle style)
		{
			GUI.Box(position, GUIContent.Temp(text), style);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004BC4 File Offset: 0x00002DC4
		public static void Box(Rect position, Texture image, GUIStyle style)
		{
			GUI.Box(position, GUIContent.Temp(image), style);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004BD8 File Offset: 0x00002DD8
		public static void Box(Rect position, GUIContent content, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			int controlID = GUIUtility.GetControlID(GUI.s_BoxHash, FocusType.Passive);
			bool flag = Event.current.type == EventType.Repaint;
			if (flag)
			{
				style.Draw(position, content, controlID, false, position.Contains(Event.current.mousePosition));
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004C28 File Offset: 0x00002E28
		public static bool Button(Rect position, string text)
		{
			return GUI.Button(position, GUIContent.Temp(text), GUI.s_Skin.button);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004C50 File Offset: 0x00002E50
		public static bool Button(Rect position, Texture image)
		{
			return GUI.Button(position, GUIContent.Temp(image), GUI.s_Skin.button);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004C78 File Offset: 0x00002E78
		public static bool Button(Rect position, GUIContent content)
		{
			return GUI.Button(position, content, GUI.s_Skin.button);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004C9C File Offset: 0x00002E9C
		public static bool Button(Rect position, string text, GUIStyle style)
		{
			return GUI.Button(position, GUIContent.Temp(text), style);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004CBC File Offset: 0x00002EBC
		public static bool Button(Rect position, Texture image, GUIStyle style)
		{
			return GUI.Button(position, GUIContent.Temp(image), style);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004CDC File Offset: 0x00002EDC
		public static bool Button(Rect position, GUIContent content, GUIStyle style)
		{
			int controlID = GUIUtility.GetControlID(GUI.s_ButonHash, FocusType.Passive, position);
			return GUI.Button(position, controlID, content, style);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004D04 File Offset: 0x00002F04
		internal static bool Button(Rect position, int id, GUIContent content, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoButton(position, id, content, style);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004D28 File Offset: 0x00002F28
		public static bool RepeatButton(Rect position, string text)
		{
			return GUI.DoRepeatButton(position, GUIContent.Temp(text), GUI.s_Skin.button, FocusType.Passive);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004D54 File Offset: 0x00002F54
		public static bool RepeatButton(Rect position, Texture image)
		{
			return GUI.DoRepeatButton(position, GUIContent.Temp(image), GUI.s_Skin.button, FocusType.Passive);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004D80 File Offset: 0x00002F80
		public static bool RepeatButton(Rect position, GUIContent content)
		{
			return GUI.DoRepeatButton(position, content, GUI.s_Skin.button, FocusType.Passive);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004DA4 File Offset: 0x00002FA4
		public static bool RepeatButton(Rect position, string text, GUIStyle style)
		{
			return GUI.DoRepeatButton(position, GUIContent.Temp(text), style, FocusType.Passive);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004DC4 File Offset: 0x00002FC4
		public static bool RepeatButton(Rect position, Texture image, GUIStyle style)
		{
			return GUI.DoRepeatButton(position, GUIContent.Temp(image), style, FocusType.Passive);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004DE4 File Offset: 0x00002FE4
		public static bool RepeatButton(Rect position, GUIContent content, GUIStyle style)
		{
			return GUI.DoRepeatButton(position, content, style, FocusType.Passive);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004E00 File Offset: 0x00003000
		private static bool DoRepeatButton(Rect position, GUIContent content, GUIStyle style, FocusType focusType)
		{
			GUIUtility.CheckOnGUI();
			int controlID = GUIUtility.GetControlID(GUI.s_RepeatButtonHash, focusType, position);
			EventType typeForControl = Event.current.GetTypeForControl(controlID);
			EventType eventType = typeForControl;
			bool flag;
			if (eventType != EventType.MouseDown)
			{
				if (eventType != EventType.MouseUp)
				{
					if (eventType != EventType.Repaint)
					{
						flag = false;
					}
					else
					{
						style.Draw(position, content, controlID, false, position.Contains(Event.current.mousePosition));
						flag = controlID == GUIUtility.hotControl && position.Contains(Event.current.mousePosition);
					}
				}
				else
				{
					bool flag2 = GUIUtility.hotControl == controlID;
					if (flag2)
					{
						GUIUtility.hotControl = 0;
						Event.current.Use();
						flag = position.Contains(Event.current.mousePosition);
					}
					else
					{
						flag = false;
					}
				}
			}
			else
			{
				bool flag3 = position.Contains(Event.current.mousePosition);
				if (flag3)
				{
					GUIUtility.hotControl = controlID;
					Event.current.Use();
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004EF0 File Offset: 0x000030F0
		public static string TextField(Rect position, string text)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, false, -1, GUI.skin.textField);
			return guicontent.text;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004F2C File Offset: 0x0000312C
		public static string TextField(Rect position, string text, int maxLength)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, false, maxLength, GUI.skin.textField);
			return guicontent.text;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004F68 File Offset: 0x00003168
		public static string TextField(Rect position, string text, GUIStyle style)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, false, -1, style);
			return guicontent.text;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004F9C File Offset: 0x0000319C
		public static string TextField(Rect position, string text, int maxLength, GUIStyle style)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, false, maxLength, style);
			return guicontent.text;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004FD0 File Offset: 0x000031D0
		public static string PasswordField(Rect position, string password, char maskChar)
		{
			return GUI.PasswordField(position, password, maskChar, -1, GUI.skin.textField);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004FF8 File Offset: 0x000031F8
		public static string PasswordField(Rect position, string password, char maskChar, int maxLength)
		{
			return GUI.PasswordField(position, password, maskChar, maxLength, GUI.skin.textField);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00005020 File Offset: 0x00003220
		public static string PasswordField(Rect position, string password, char maskChar, GUIStyle style)
		{
			return GUI.PasswordField(position, password, maskChar, -1, style);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000503C File Offset: 0x0000323C
		public static string PasswordField(Rect position, string password, char maskChar, int maxLength, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			string text = GUI.PasswordFieldGetStrToShow(password, maskChar);
			GUIContent guicontent = GUIContent.Temp(text);
			bool changed = GUI.changed;
			GUI.changed = false;
			bool flag = TouchScreenKeyboard.isSupported && !TouchScreenKeyboard.isInPlaceEditingAllowed;
			if (flag)
			{
				GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard), guicontent, false, maxLength, style, password, maskChar);
			}
			else
			{
				GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, false, maxLength, style);
			}
			text = (GUI.changed ? guicontent.text : password);
			GUI.changed = GUI.changed || changed;
			return text;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000050D0 File Offset: 0x000032D0
		internal static string PasswordFieldGetStrToShow(string password, char maskChar)
		{
			return (Event.current.type == EventType.Repaint || Event.current.type == EventType.MouseDown) ? "".PadRight(password.Length, maskChar) : password;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00005110 File Offset: 0x00003310
		public static string TextArea(Rect position, string text)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, true, -1, GUI.skin.textArea);
			return guicontent.text;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000514C File Offset: 0x0000334C
		public static string TextArea(Rect position, string text, int maxLength)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, true, maxLength, GUI.skin.textArea);
			return guicontent.text;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00005188 File Offset: 0x00003388
		public static string TextArea(Rect position, string text, GUIStyle style)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, true, -1, style);
			return guicontent.text;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000051BC File Offset: 0x000033BC
		public static string TextArea(Rect position, string text, int maxLength, GUIStyle style)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, true, maxLength, style);
			return guicontent.text;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000051ED File Offset: 0x000033ED
		internal static void DoTextField(Rect position, int id, GUIContent content, bool multiline, int maxLength, GUIStyle style)
		{
			GUI.DoTextField(position, id, content, multiline, maxLength, style, null);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000051FF File Offset: 0x000033FF
		internal static void DoTextField(Rect position, int id, GUIContent content, bool multiline, int maxLength, GUIStyle style, string secureText)
		{
			GUI.DoTextField(position, id, content, multiline, maxLength, style, secureText, '\0');
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00005214 File Offset: 0x00003414
		internal static void DoTextField(Rect position, int id, GUIContent content, bool multiline, int maxLength, GUIStyle style, string secureText, char maskChar)
		{
			GUIUtility.CheckOnGUI();
			bool flag = maxLength >= 0 && content.text.Length > maxLength;
			if (flag)
			{
				content.text = content.text.Substring(0, maxLength);
			}
			TextEditor textEditor = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), id);
			textEditor.text = content.text;
			textEditor.SaveBackup();
			textEditor.position = position;
			textEditor.style = style;
			textEditor.isMultiline = multiline;
			textEditor.controlID = id;
			textEditor.DetectFocusChange();
			bool flag2 = TouchScreenKeyboard.isSupported && !TouchScreenKeyboard.isInPlaceEditingAllowed;
			if (flag2)
			{
				GUI.HandleTextFieldEventForTouchscreen(position, id, content, multiline, maxLength, style, secureText, maskChar, textEditor);
			}
			else
			{
				GUI.HandleTextFieldEventForDesktop(position, id, content, multiline, maxLength, style, textEditor);
			}
			textEditor.UpdateScrollOffsetIfNeeded(Event.current);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000052F4 File Offset: 0x000034F4
		private static void HandleTextFieldEventForTouchscreen(Rect position, int id, GUIContent content, bool multiline, int maxLength, GUIStyle style, string secureText, char maskChar, TextEditor editor)
		{
			Event current = Event.current;
			EventType type = current.type;
			EventType eventType = type;
			if (eventType != EventType.MouseDown)
			{
				if (eventType == EventType.Repaint)
				{
					bool flag = editor.keyboardOnScreen != null;
					if (flag)
					{
						content.text = editor.keyboardOnScreen.text;
						bool flag2 = maxLength >= 0 && content.text.Length > maxLength;
						if (flag2)
						{
							content.text = content.text.Substring(0, maxLength);
						}
						bool flag3 = editor.keyboardOnScreen.status > TouchScreenKeyboard.Status.Visible;
						if (flag3)
						{
							editor.keyboardOnScreen = null;
							GUI.changed = true;
						}
					}
					string text = content.text;
					bool flag4 = secureText != null;
					if (flag4)
					{
						content.text = GUI.PasswordFieldGetStrToShow(text, maskChar);
					}
					style.Draw(position, content, id, false);
					content.text = text;
				}
			}
			else
			{
				bool flag5 = position.Contains(current.mousePosition);
				if (flag5)
				{
					GUIUtility.hotControl = id;
					bool flag6 = GUI.s_HotTextField != -1 && GUI.s_HotTextField != id;
					if (flag6)
					{
						TextEditor textEditor = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), GUI.s_HotTextField);
						textEditor.keyboardOnScreen = null;
					}
					GUI.s_HotTextField = id;
					bool flag7 = GUIUtility.keyboardControl != id;
					if (flag7)
					{
						GUIUtility.keyboardControl = id;
					}
					editor.keyboardOnScreen = TouchScreenKeyboard.Open(secureText ?? content.text, TouchScreenKeyboardType.Default, true, multiline, secureText != null);
					current.Use();
				}
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00005480 File Offset: 0x00003680
		private static void HandleTextFieldEventForDesktop(Rect position, int id, GUIContent content, bool multiline, int maxLength, GUIStyle style, TextEditor editor)
		{
			Event current = Event.current;
			bool flag = false;
			switch (current.type)
			{
			case EventType.MouseDown:
			{
				bool flag2 = position.Contains(current.mousePosition);
				if (flag2)
				{
					GUIUtility.hotControl = id;
					GUIUtility.keyboardControl = id;
					editor.m_HasFocus = true;
					editor.MoveCursorToPosition(Event.current.mousePosition);
					bool flag3 = Event.current.clickCount == 2 && GUI.skin.settings.doubleClickSelectsWord;
					if (flag3)
					{
						editor.SelectCurrentWord();
						editor.DblClickSnap(TextEditor.DblClickSnapping.WORDS);
						editor.MouseDragSelectsWholeWords(true);
					}
					bool flag4 = Event.current.clickCount == 3 && GUI.skin.settings.tripleClickSelectsLine;
					if (flag4)
					{
						editor.SelectCurrentParagraph();
						editor.MouseDragSelectsWholeWords(true);
						editor.DblClickSnap(TextEditor.DblClickSnapping.PARAGRAPHS);
					}
					current.Use();
				}
				break;
			}
			case EventType.MouseUp:
			{
				bool flag5 = GUIUtility.hotControl == id;
				if (flag5)
				{
					editor.MouseDragSelectsWholeWords(false);
					GUIUtility.hotControl = 0;
					current.Use();
				}
				break;
			}
			case EventType.MouseDrag:
			{
				bool flag6 = GUIUtility.hotControl == id;
				if (flag6)
				{
					bool shift = current.shift;
					if (shift)
					{
						editor.MoveCursorToPosition(Event.current.mousePosition);
					}
					else
					{
						editor.SelectToPosition(Event.current.mousePosition);
					}
					current.Use();
				}
				break;
			}
			case EventType.KeyDown:
			{
				bool flag7 = GUIUtility.keyboardControl != id;
				if (flag7)
				{
					return;
				}
				bool flag8 = editor.HandleKeyEvent(current);
				if (flag8)
				{
					current.Use();
					flag = true;
					content.text = editor.text;
				}
				else
				{
					bool flag9 = current.keyCode == KeyCode.Tab || current.character == '\t';
					if (flag9)
					{
						return;
					}
					char character = current.character;
					bool flag10 = character == '\n' && !multiline && !current.alt;
					if (flag10)
					{
						return;
					}
					Font font = style.font;
					bool flag11 = !font;
					if (flag11)
					{
						font = GUI.skin.font;
					}
					bool flag12 = font.HasCharacter(character) || character == '\n';
					if (flag12)
					{
						editor.Insert(character);
						flag = true;
					}
					else
					{
						bool flag13 = character == '\0';
						if (flag13)
						{
							bool flag14 = GUIUtility.compositionString.Length > 0;
							if (flag14)
							{
								editor.ReplaceSelection("");
								flag = true;
							}
							current.Use();
						}
					}
				}
				break;
			}
			case EventType.Repaint:
			{
				editor.UpdateTextHandle();
				bool flag15 = GUIUtility.keyboardControl != id;
				if (flag15)
				{
					style.Draw(position, content, id, false);
				}
				else
				{
					editor.DrawCursor(content.text);
				}
				break;
			}
			}
			bool flag16 = GUIUtility.keyboardControl == id;
			if (flag16)
			{
				GUIUtility.textFieldInput = true;
			}
			bool flag17 = flag;
			if (flag17)
			{
				GUI.changed = true;
				content.text = editor.text;
				bool flag18 = maxLength >= 0 && content.text.Length > maxLength;
				if (flag18)
				{
					content.text = content.text.Substring(0, maxLength);
				}
				current.Use();
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000057C8 File Offset: 0x000039C8
		public static bool Toggle(Rect position, bool value, string text)
		{
			return GUI.Toggle(position, value, GUIContent.Temp(text), GUI.s_Skin.toggle);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000057F4 File Offset: 0x000039F4
		public static bool Toggle(Rect position, bool value, Texture image)
		{
			return GUI.Toggle(position, value, GUIContent.Temp(image), GUI.s_Skin.toggle);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00005820 File Offset: 0x00003A20
		public static bool Toggle(Rect position, bool value, GUIContent content)
		{
			return GUI.Toggle(position, value, content, GUI.s_Skin.toggle);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00005844 File Offset: 0x00003A44
		public static bool Toggle(Rect position, bool value, string text, GUIStyle style)
		{
			return GUI.Toggle(position, value, GUIContent.Temp(text), style);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00005864 File Offset: 0x00003A64
		public static bool Toggle(Rect position, bool value, Texture image, GUIStyle style)
		{
			return GUI.Toggle(position, value, GUIContent.Temp(image), style);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00005884 File Offset: 0x00003A84
		public static bool Toggle(Rect position, bool value, GUIContent content, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoToggle(position, GUIUtility.GetControlID(GUI.s_ToggleHash, FocusType.Passive, position), value, content, style);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000058B4 File Offset: 0x00003AB4
		public static bool Toggle(Rect position, int id, bool value, GUIContent content, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoToggle(position, id, value, content, style);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000058D8 File Offset: 0x00003AD8
		public static int Toolbar(Rect position, int selected, string[] texts)
		{
			return GUI.Toolbar(position, selected, GUIContent.Temp(texts), GUI.s_Skin.button);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00005904 File Offset: 0x00003B04
		public static int Toolbar(Rect position, int selected, Texture[] images)
		{
			return GUI.Toolbar(position, selected, GUIContent.Temp(images), GUI.s_Skin.button);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00005930 File Offset: 0x00003B30
		public static int Toolbar(Rect position, int selected, GUIContent[] contents)
		{
			return GUI.Toolbar(position, selected, contents, GUI.s_Skin.button);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00005954 File Offset: 0x00003B54
		public static int Toolbar(Rect position, int selected, string[] texts, GUIStyle style)
		{
			return GUI.Toolbar(position, selected, GUIContent.Temp(texts), style);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00005974 File Offset: 0x00003B74
		public static int Toolbar(Rect position, int selected, Texture[] images, GUIStyle style)
		{
			return GUI.Toolbar(position, selected, GUIContent.Temp(images), style);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005994 File Offset: 0x00003B94
		public static int Toolbar(Rect position, int selected, GUIContent[] contents, GUIStyle style)
		{
			return GUI.Toolbar(position, selected, contents, null, style, GUI.ToolbarButtonSize.Fixed, null);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000059B4 File Offset: 0x00003BB4
		public static int Toolbar(Rect position, int selected, GUIContent[] contents, GUIStyle style, GUI.ToolbarButtonSize buttonSize)
		{
			return GUI.Toolbar(position, selected, contents, null, style, buttonSize, null);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000059D4 File Offset: 0x00003BD4
		internal static int Toolbar(Rect position, int selected, GUIContent[] contents, string[] controlNames, GUIStyle style, GUI.ToolbarButtonSize buttonSize, bool[] contentsEnabled = null)
		{
			GUIStyle guistyle;
			GUIStyle guistyle2;
			GUIStyle guistyle3;
			GUI.FindStyles(ref style, out guistyle, out guistyle2, out guistyle3, "left", "mid", "right");
			return GUI.Toolbar(position, selected, contents, controlNames, style, guistyle, guistyle2, guistyle3, buttonSize, contentsEnabled);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005A18 File Offset: 0x00003C18
		internal static int Toolbar(Rect position, int selected, GUIContent[] contents, string[] controlNames, GUIStyle style, GUIStyle firstStyle, GUIStyle midStyle, GUIStyle lastStyle, GUI.ToolbarButtonSize buttonSize, bool[] contentsEnabled = null)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoButtonGrid(position, selected, contents, controlNames, contents.Length, style, firstStyle, midStyle, lastStyle, buttonSize, contentsEnabled);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005A48 File Offset: 0x00003C48
		public static int SelectionGrid(Rect position, int selected, string[] texts, int xCount)
		{
			return GUI.SelectionGrid(position, selected, GUIContent.Temp(texts), xCount, null);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005A6C File Offset: 0x00003C6C
		public static int SelectionGrid(Rect position, int selected, Texture[] images, int xCount)
		{
			return GUI.SelectionGrid(position, selected, GUIContent.Temp(images), xCount, null);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005A90 File Offset: 0x00003C90
		public static int SelectionGrid(Rect position, int selected, GUIContent[] content, int xCount)
		{
			return GUI.SelectionGrid(position, selected, content, xCount, null);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005AAC File Offset: 0x00003CAC
		public static int SelectionGrid(Rect position, int selected, string[] texts, int xCount, GUIStyle style)
		{
			return GUI.SelectionGrid(position, selected, GUIContent.Temp(texts), xCount, style);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005AD0 File Offset: 0x00003CD0
		public static int SelectionGrid(Rect position, int selected, Texture[] images, int xCount, GUIStyle style)
		{
			return GUI.SelectionGrid(position, selected, GUIContent.Temp(images), xCount, style);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005AF4 File Offset: 0x00003CF4
		public static int SelectionGrid(Rect position, int selected, GUIContent[] contents, int xCount, GUIStyle style)
		{
			bool flag = style == null;
			if (flag)
			{
				style = GUI.s_Skin.button;
			}
			return GUI.DoButtonGrid(position, selected, contents, null, xCount, style, style, style, style, GUI.ToolbarButtonSize.Fixed, null);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005B30 File Offset: 0x00003D30
		internal static void FindStyles(ref GUIStyle style, out GUIStyle firstStyle, out GUIStyle midStyle, out GUIStyle lastStyle, string first, string mid, string last)
		{
			bool flag = style == null;
			if (flag)
			{
				style = GUI.skin.button;
			}
			string name = style.name;
			midStyle = GUI.skin.FindStyle(name + mid) ?? style;
			firstStyle = GUI.skin.FindStyle(name + first) ?? midStyle;
			lastStyle = GUI.skin.FindStyle(name + last) ?? midStyle;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005BAC File Offset: 0x00003DAC
		internal static int CalcTotalHorizSpacing(int xCount, GUIStyle style, GUIStyle firstStyle, GUIStyle midStyle, GUIStyle lastStyle)
		{
			bool flag = xCount < 2;
			int num;
			if (flag)
			{
				num = 0;
			}
			else
			{
				bool flag2 = xCount == 2;
				if (flag2)
				{
					num = Mathf.Max(firstStyle.margin.right, lastStyle.margin.left);
				}
				else
				{
					int num2 = Mathf.Max(midStyle.margin.left, midStyle.margin.right);
					num = Mathf.Max(firstStyle.margin.right, midStyle.margin.left) + Mathf.Max(midStyle.margin.right, lastStyle.margin.left) + num2 * (xCount - 3);
				}
			}
			return num;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00005C50 File Offset: 0x00003E50
		internal static bool DoControl(Rect position, int id, bool on, bool hover, GUIContent content, GUIStyle style)
		{
			Event current = Event.current;
			switch (current.type)
			{
			case EventType.MouseDown:
			{
				bool flag = GUIUtility.HitTest(position, current);
				if (flag)
				{
					GUI.GrabMouseControl(id);
					current.Use();
				}
				break;
			}
			case EventType.MouseUp:
			{
				bool flag2 = GUI.HasMouseControl(id);
				if (flag2)
				{
					GUI.ReleaseMouseControl();
					current.Use();
					bool flag3 = GUIUtility.HitTest(position, current);
					if (flag3)
					{
						GUI.changed = true;
						return !on;
					}
				}
				break;
			}
			case EventType.MouseDrag:
			{
				bool flag4 = GUI.HasMouseControl(id);
				if (flag4)
				{
					current.Use();
				}
				break;
			}
			case EventType.KeyDown:
			{
				bool flag5 = current.alt || current.shift || current.command || current.control;
				bool flag6 = (current.keyCode == KeyCode.Space || current.keyCode == KeyCode.Return || current.keyCode == KeyCode.KeypadEnter) && !flag5 && GUIUtility.keyboardControl == id;
				if (flag6)
				{
					current.Use();
					GUI.changed = true;
					return !on;
				}
				break;
			}
			case EventType.Repaint:
				style.Draw(position, content, id, on, hover);
				break;
			}
			return on;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00005D94 File Offset: 0x00003F94
		private static void DoLabel(Rect position, GUIContent content, GUIStyle style)
		{
			Event current = Event.current;
			bool flag = current.type != EventType.Repaint;
			if (!flag)
			{
				bool flag2 = position.Contains(current.mousePosition);
				style.Draw(position, content, flag2, false, false, false);
				bool flag3 = !string.IsNullOrEmpty(content.tooltip) && flag2 && GUIClip.visibleRect.Contains(current.mousePosition);
				if (flag3)
				{
					bool flag4 = !GUIStyle.IsTooltipActive(content.tooltip);
					if (flag4)
					{
						GUI.s_ToolTipRect = new Rect(current.mousePosition, Vector2.zero);
					}
					GUIStyle.SetMouseTooltip(content.tooltip, GUI.s_ToolTipRect);
				}
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005E44 File Offset: 0x00004044
		internal static bool DoToggle(Rect position, int id, bool value, GUIContent content, GUIStyle style)
		{
			return GUI.DoControl(position, id, value, position.Contains(Event.current.mousePosition), content, style);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005E74 File Offset: 0x00004074
		internal static bool DoButton(Rect position, int id, GUIContent content, GUIStyle style)
		{
			return GUI.DoControl(position, id, false, position.Contains(Event.current.mousePosition), content, style);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00005EA4 File Offset: 0x000040A4
		private static Rect[] CalcGridRectsFixedWidthFixedMargin(Rect position, int itemCount, int itemsPerRow, float elemWidth, float elemHeight, float spacingHorizontal, float spacingVertical)
		{
			int num = 0;
			float num2 = position.xMin;
			float num3 = position.yMin;
			Rect[] array = new Rect[itemCount];
			for (int i = 0; i < itemCount; i++)
			{
				array[i] = new Rect(num2, num3, elemWidth, elemHeight);
				array[i] = GUIUtility.AlignRectToDevice(array[i]);
				num2 = array[i].xMax + spacingHorizontal;
				bool flag = ++num >= itemsPerRow;
				if (flag)
				{
					num = 0;
					num3 += elemHeight + spacingVertical;
					num2 = position.xMin;
				}
			}
			return array;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00005F48 File Offset: 0x00004148
		internal static int DoCustomSelectionGrid(Rect position, int selected, int itemCount, GUI.CustomSelectionGridItemGUI itemGUI, int itemsPerRow, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			bool flag = itemCount == 0;
			int num;
			if (flag)
			{
				num = selected;
			}
			else
			{
				bool flag2 = itemsPerRow <= 0;
				if (flag2)
				{
					Debug.LogWarning("You are trying to create a SelectionGrid with zero or less elements to be displayed in the horizontal direction. Set itemsPerRow to a positive value.");
					num = selected;
				}
				else
				{
					int num2 = (itemCount + itemsPerRow - 1) / itemsPerRow;
					float num3 = (float)Mathf.Max(style.margin.left, style.margin.right);
					float num4 = (float)Mathf.Max(style.margin.top, style.margin.bottom);
					float num5 = ((style.fixedWidth != 0f) ? style.fixedWidth : ((position.width - (float)GUI.CalcTotalHorizSpacing(itemsPerRow, style, style, style, style)) / (float)itemsPerRow));
					float num6 = ((style.fixedHeight != 0f) ? style.fixedHeight : ((position.height - num4 * (float)(num2 - 1)) / (float)num2));
					Rect[] array = GUI.CalcGridRectsFixedWidthFixedMargin(position, itemCount, itemsPerRow, num5, num6, num3, num4);
					int num7 = 0;
					for (int i = 0; i < itemCount; i++)
					{
						Rect rect = array[i];
						int controlID = GUIUtility.GetControlID(GUI.s_ButtonGridHash, FocusType.Passive, rect);
						bool flag3 = i == selected;
						if (flag3)
						{
							num7 = controlID;
						}
						EventType typeForControl = Event.current.GetTypeForControl(controlID);
						EventType eventType = typeForControl;
						EventType eventType2 = eventType;
						switch (eventType2)
						{
						case EventType.MouseDown:
						{
							bool flag4 = GUIUtility.HitTest(rect, Event.current);
							if (flag4)
							{
								GUIUtility.hotControl = controlID;
								Event.current.Use();
							}
							break;
						}
						case EventType.MouseUp:
						{
							bool flag5 = GUIUtility.hotControl == controlID;
							if (flag5)
							{
								GUIUtility.hotControl = 0;
								Event.current.Use();
								GUI.changed = true;
								return i;
							}
							break;
						}
						case EventType.MouseMove:
							break;
						case EventType.MouseDrag:
						{
							bool flag6 = GUIUtility.hotControl == controlID;
							if (flag6)
							{
								Event.current.Use();
							}
							break;
						}
						default:
							if (eventType2 == EventType.Repaint)
							{
								bool flag7 = selected != i;
								if (flag7)
								{
									itemGUI(i, rect, style, controlID);
								}
							}
							break;
						}
						bool flag8 = typeForControl != EventType.Repaint || selected != i;
						if (flag8)
						{
							itemGUI(i, rect, style, controlID);
						}
					}
					bool flag9 = selected >= 0 && selected < itemCount && Event.current.type == EventType.Repaint;
					if (flag9)
					{
						itemGUI(selected, array[selected], style, num7);
					}
					num = selected;
				}
			}
			return num;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000061C0 File Offset: 0x000043C0
		private static int DoButtonGrid(Rect position, int selected, GUIContent[] contents, string[] controlNames, int itemsPerRow, GUIStyle style, GUIStyle firstStyle, GUIStyle midStyle, GUIStyle lastStyle, GUI.ToolbarButtonSize buttonSize, bool[] contentsEnabled = null)
		{
			GUIUtility.CheckOnGUI();
			int num = contents.Length;
			bool flag = num == 0;
			int num2;
			if (flag)
			{
				num2 = selected;
			}
			else
			{
				bool flag2 = itemsPerRow <= 0;
				if (flag2)
				{
					Debug.LogWarning("You are trying to create a SelectionGrid with zero or less elements to be displayed in the horizontal direction. Set itemsPerRow to a positive value.");
					num2 = selected;
				}
				else
				{
					bool flag3 = contentsEnabled != null && contentsEnabled.Length != num;
					if (flag3)
					{
						throw new ArgumentException("contentsEnabled");
					}
					int num3 = (num + itemsPerRow - 1) / itemsPerRow;
					float num4 = ((style.fixedWidth != 0f) ? style.fixedWidth : ((position.width - (float)GUI.CalcTotalHorizSpacing(itemsPerRow, style, firstStyle, midStyle, lastStyle)) / (float)itemsPerRow));
					float num5 = ((style.fixedHeight != 0f) ? style.fixedHeight : ((position.height - (float)(Mathf.Max(style.margin.top, style.margin.bottom) * (num3 - 1))) / (float)num3));
					Rect[] array = GUI.CalcGridRects(position, contents, itemsPerRow, num4, num5, style, firstStyle, midStyle, lastStyle, buttonSize);
					GUIStyle guistyle = null;
					int num6 = 0;
					for (int i = 0; i < num; i++)
					{
						bool enabled = GUI.enabled;
						GUI.enabled &= contentsEnabled == null || contentsEnabled[i];
						Rect rect = array[i];
						GUIContent guicontent = contents[i];
						bool flag4 = controlNames != null;
						if (flag4)
						{
							GUI.SetNextControlName(controlNames[i]);
						}
						int controlID = GUIUtility.GetControlID(GUI.s_ButtonGridHash, FocusType.Passive, rect);
						bool flag5 = i == selected;
						if (flag5)
						{
							num6 = controlID;
						}
						EventType typeForControl = Event.current.GetTypeForControl(controlID);
						EventType eventType = typeForControl;
						switch (eventType)
						{
						case EventType.MouseDown:
						{
							bool flag6 = GUIUtility.HitTest(rect, Event.current);
							if (flag6)
							{
								GUIUtility.hotControl = controlID;
								Event.current.Use();
							}
							break;
						}
						case EventType.MouseUp:
						{
							bool flag7 = GUIUtility.hotControl == controlID;
							if (flag7)
							{
								GUIUtility.hotControl = 0;
								Event.current.Use();
								GUI.changed = true;
								return i;
							}
							break;
						}
						case EventType.MouseMove:
							break;
						case EventType.MouseDrag:
						{
							bool flag8 = GUIUtility.hotControl == controlID;
							if (flag8)
							{
								Event.current.Use();
							}
							break;
						}
						default:
							if (eventType == EventType.Repaint)
							{
								GUIStyle guistyle2 = ((num == 1) ? style : ((i == 0) ? firstStyle : ((i == num - 1) ? lastStyle : midStyle)));
								bool flag9 = rect.Contains(Event.current.mousePosition);
								bool flag10 = GUIUtility.hotControl == controlID;
								bool flag11 = selected == i;
								bool flag12 = !flag11;
								if (flag12)
								{
									guistyle2.Draw(rect, guicontent, GUI.enabled && flag9 && (flag10 || GUIUtility.hotControl == 0), GUI.enabled && flag10, false, false);
								}
								else
								{
									guistyle = guistyle2;
								}
								bool flag13 = flag9;
								if (flag13)
								{
									GUIUtility.mouseUsed = true;
									bool flag14 = !string.IsNullOrEmpty(guicontent.tooltip);
									if (flag14)
									{
										GUIStyle.SetMouseTooltip(guicontent.tooltip, rect);
									}
								}
							}
							break;
						}
						GUI.enabled = enabled;
					}
					bool flag15 = guistyle != null;
					if (flag15)
					{
						Rect rect2 = array[selected];
						GUIContent guicontent2 = contents[selected];
						bool flag16 = rect2.Contains(Event.current.mousePosition);
						bool flag17 = GUIUtility.hotControl == num6;
						bool enabled2 = GUI.enabled;
						GUI.enabled &= contentsEnabled == null || contentsEnabled[selected];
						guistyle.Draw(rect2, guicontent2, GUI.enabled && flag16 && (flag17 || GUIUtility.hotControl == 0), GUI.enabled && flag17, true, false);
						GUI.enabled = enabled2;
					}
					num2 = selected;
				}
			}
			return num2;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000656C File Offset: 0x0000476C
		private static Rect[] CalcGridRects(Rect position, GUIContent[] contents, int xCount, float elemWidth, float elemHeight, GUIStyle style, GUIStyle firstStyle, GUIStyle midStyle, GUIStyle lastStyle, GUI.ToolbarButtonSize buttonSize)
		{
			int num = contents.Length;
			int num2 = 0;
			float num3 = position.xMin;
			float num4 = position.yMin;
			GUIStyle guistyle = style;
			Rect[] array = new Rect[num];
			bool flag = num > 1;
			if (flag)
			{
				guistyle = firstStyle;
			}
			for (int i = 0; i < num; i++)
			{
				float num5 = 0f;
				if (buttonSize != GUI.ToolbarButtonSize.Fixed)
				{
					if (buttonSize == GUI.ToolbarButtonSize.FitToContents)
					{
						num5 = guistyle.CalcSize(contents[i]).x;
					}
				}
				else
				{
					num5 = elemWidth;
				}
				array[i] = new Rect(num3, num4, num5, elemHeight);
				array[i] = GUIUtility.AlignRectToDevice(array[i]);
				GUIStyle guistyle2 = midStyle;
				bool flag2 = i == num - 2 || i == xCount - 2;
				if (flag2)
				{
					guistyle2 = lastStyle;
				}
				num3 = array[i].xMax + (float)Mathf.Max(guistyle.margin.right, guistyle2.margin.left);
				num2++;
				bool flag3 = num2 >= xCount;
				if (flag3)
				{
					num2 = 0;
					num4 += elemHeight + (float)Mathf.Max(style.margin.top, style.margin.bottom);
					num3 = position.xMin;
					guistyle2 = firstStyle;
				}
				guistyle = guistyle2;
			}
			return array;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000066C8 File Offset: 0x000048C8
		public static float HorizontalSlider(Rect position, float value, float leftValue, float rightValue)
		{
			return GUI.Slider(position, value, 0f, leftValue, rightValue, GUI.skin.horizontalSlider, GUI.skin.horizontalSliderThumb, true, 0, GUI.skin.horizontalSliderThumbExtent);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00006708 File Offset: 0x00004908
		public static float HorizontalSlider(Rect position, float value, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb)
		{
			return GUI.Slider(position, value, 0f, leftValue, rightValue, slider, thumb, true, 0, null);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00006730 File Offset: 0x00004930
		public static float HorizontalSlider(Rect position, float value, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb, GUIStyle thumbExtent)
		{
			return GUI.Slider(position, value, 0f, leftValue, rightValue, slider, thumb, true, 0, (thumbExtent == null && thumb == GUI.skin.horizontalSliderThumb) ? GUI.skin.horizontalSliderThumbExtent : thumbExtent);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00006778 File Offset: 0x00004978
		public static float VerticalSlider(Rect position, float value, float topValue, float bottomValue)
		{
			return GUI.Slider(position, value, 0f, topValue, bottomValue, GUI.skin.verticalSlider, GUI.skin.verticalSliderThumb, false, 0, GUI.skin.verticalSliderThumbExtent);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000067B8 File Offset: 0x000049B8
		public static float VerticalSlider(Rect position, float value, float topValue, float bottomValue, GUIStyle slider, GUIStyle thumb)
		{
			return GUI.Slider(position, value, 0f, topValue, bottomValue, slider, thumb, false, 0, null);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000067E0 File Offset: 0x000049E0
		public static float VerticalSlider(Rect position, float value, float topValue, float bottomValue, GUIStyle slider, GUIStyle thumb, GUIStyle thumbExtent)
		{
			return GUI.Slider(position, value, 0f, topValue, bottomValue, slider, thumb, false, 0, (thumbExtent == null && thumb == GUI.skin.verticalSliderThumb) ? GUI.skin.verticalSliderThumbExtent : thumbExtent);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00006828 File Offset: 0x00004A28
		public static float Slider(Rect position, float value, float size, float start, float end, GUIStyle slider, GUIStyle thumb, bool horiz, int id, GUIStyle thumbExtent = null)
		{
			GUIUtility.CheckOnGUI();
			bool flag = id == 0;
			if (flag)
			{
				id = GUIUtility.GetControlID(GUI.s_SliderHash, FocusType.Passive, position);
			}
			return new SliderHandler(position, value, size, start, end, slider, thumb, horiz, id, thumbExtent).Handle();
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00006878 File Offset: 0x00004A78
		public static float HorizontalScrollbar(Rect position, float value, float size, float leftValue, float rightValue)
		{
			return GUI.Scroller(position, value, size, leftValue, rightValue, GUI.skin.horizontalScrollbar, GUI.skin.horizontalScrollbarThumb, GUI.skin.horizontalScrollbarLeftButton, GUI.skin.horizontalScrollbarRightButton, true);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000068C0 File Offset: 0x00004AC0
		public static float HorizontalScrollbar(Rect position, float value, float size, float leftValue, float rightValue, GUIStyle style)
		{
			return GUI.Scroller(position, value, size, leftValue, rightValue, style, GUI.skin.GetStyle(style.name + "thumb"), GUI.skin.GetStyle(style.name + "leftbutton"), GUI.skin.GetStyle(style.name + "rightbutton"), true);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00006934 File Offset: 0x00004B34
		internal static bool ScrollerRepeatButton(int scrollerID, Rect rect, GUIStyle style)
		{
			bool flag = false;
			bool flag2 = GUI.DoRepeatButton(rect, GUIContent.none, style, FocusType.Passive);
			if (flag2)
			{
				bool flag3 = GUI.s_ScrollControlId != scrollerID;
				GUI.s_ScrollControlId = scrollerID;
				bool flag4 = flag3;
				if (flag4)
				{
					flag = true;
					GUI.nextScrollStepTime = DateTime.Now.AddMilliseconds(250.0);
				}
				else
				{
					bool flag5 = DateTime.Now >= GUI.nextScrollStepTime;
					if (flag5)
					{
						flag = true;
						GUI.nextScrollStepTime = DateTime.Now.AddMilliseconds(30.0);
					}
				}
				bool flag6 = Event.current.type == EventType.Repaint;
				if (flag6)
				{
					GUI.InternalRepaintEditorWindow();
				}
			}
			return flag;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000069EC File Offset: 0x00004BEC
		public static float VerticalScrollbar(Rect position, float value, float size, float topValue, float bottomValue)
		{
			return GUI.Scroller(position, value, size, topValue, bottomValue, GUI.skin.verticalScrollbar, GUI.skin.verticalScrollbarThumb, GUI.skin.verticalScrollbarUpButton, GUI.skin.verticalScrollbarDownButton, false);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00006A34 File Offset: 0x00004C34
		public static float VerticalScrollbar(Rect position, float value, float size, float topValue, float bottomValue, GUIStyle style)
		{
			return GUI.Scroller(position, value, size, topValue, bottomValue, style, GUI.skin.GetStyle(style.name + "thumb"), GUI.skin.GetStyle(style.name + "upbutton"), GUI.skin.GetStyle(style.name + "downbutton"), false);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00006AA8 File Offset: 0x00004CA8
		internal static float Scroller(Rect position, float value, float size, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb, GUIStyle leftButton, GUIStyle rightButton, bool horiz)
		{
			GUIUtility.CheckOnGUI();
			int controlID = GUIUtility.GetControlID(GUI.s_SliderHash, FocusType.Passive, position);
			Rect rect;
			Rect rect2;
			Rect rect3;
			if (horiz)
			{
				rect = new Rect(position.x + leftButton.fixedWidth, position.y, position.width - leftButton.fixedWidth - rightButton.fixedWidth, position.height);
				rect2 = new Rect(position.x, position.y, leftButton.fixedWidth, position.height);
				rect3 = new Rect(position.xMax - rightButton.fixedWidth, position.y, rightButton.fixedWidth, position.height);
			}
			else
			{
				rect = new Rect(position.x, position.y + leftButton.fixedHeight, position.width, position.height - leftButton.fixedHeight - rightButton.fixedHeight);
				rect2 = new Rect(position.x, position.y, position.width, leftButton.fixedHeight);
				rect3 = new Rect(position.x, position.yMax - rightButton.fixedHeight, position.width, rightButton.fixedHeight);
			}
			value = GUI.Slider(rect, value, size, leftValue, rightValue, slider, thumb, horiz, controlID, null);
			bool flag = Event.current.type == EventType.MouseUp;
			bool flag2 = GUI.ScrollerRepeatButton(controlID, rect2, leftButton);
			if (flag2)
			{
				value -= 10f * ((leftValue < rightValue) ? 1f : (-1f));
			}
			bool flag3 = GUI.ScrollerRepeatButton(controlID, rect3, rightButton);
			if (flag3)
			{
				value += 10f * ((leftValue < rightValue) ? 1f : (-1f));
			}
			bool flag4 = flag && Event.current.type == EventType.Used;
			if (flag4)
			{
				GUI.s_ScrollControlId = 0;
			}
			bool flag5 = leftValue < rightValue;
			if (flag5)
			{
				value = Mathf.Clamp(value, leftValue, rightValue - size);
			}
			else
			{
				value = Mathf.Clamp(value, rightValue, leftValue - size);
			}
			return value;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00006CB6 File Offset: 0x00004EB6
		public static void BeginClip(Rect position, Vector2 scrollOffset, Vector2 renderOffset, bool resetOffset)
		{
			GUIUtility.CheckOnGUI();
			GUIClip.Push(position, scrollOffset, renderOffset, resetOffset);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00006CC9 File Offset: 0x00004EC9
		public static void BeginGroup(Rect position)
		{
			GUI.BeginGroup(position, GUIContent.none, GUIStyle.none);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00006CDD File Offset: 0x00004EDD
		public static void BeginGroup(Rect position, string text)
		{
			GUI.BeginGroup(position, GUIContent.Temp(text), GUIStyle.none);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00006CF2 File Offset: 0x00004EF2
		public static void BeginGroup(Rect position, Texture image)
		{
			GUI.BeginGroup(position, GUIContent.Temp(image), GUIStyle.none);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00006D07 File Offset: 0x00004F07
		public static void BeginGroup(Rect position, GUIContent content)
		{
			GUI.BeginGroup(position, content, GUIStyle.none);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00006D17 File Offset: 0x00004F17
		public static void BeginGroup(Rect position, GUIStyle style)
		{
			GUI.BeginGroup(position, GUIContent.none, style);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00006D27 File Offset: 0x00004F27
		public static void BeginGroup(Rect position, string text, GUIStyle style)
		{
			GUI.BeginGroup(position, GUIContent.Temp(text), style);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00006D38 File Offset: 0x00004F38
		public static void BeginGroup(Rect position, Texture image, GUIStyle style)
		{
			GUI.BeginGroup(position, GUIContent.Temp(image), style);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00006D49 File Offset: 0x00004F49
		public static void BeginGroup(Rect position, GUIContent content, GUIStyle style)
		{
			GUI.BeginGroup(position, content, style, Vector2.zero);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00006D5C File Offset: 0x00004F5C
		internal static void BeginGroup(Rect position, GUIContent content, GUIStyle style, Vector2 scrollOffset)
		{
			GUIUtility.CheckOnGUI();
			int controlID = GUIUtility.GetControlID(GUI.s_BeginGroupHash, FocusType.Passive);
			bool flag = content != GUIContent.none || style != GUIStyle.none;
			if (flag)
			{
				EventType type = Event.current.type;
				EventType eventType = type;
				if (eventType != EventType.Repaint)
				{
					bool flag2 = position.Contains(Event.current.mousePosition);
					if (flag2)
					{
						GUIUtility.mouseUsed = true;
					}
				}
				else
				{
					style.Draw(position, content, controlID);
				}
			}
			GUIClip.Push(position, scrollOffset, Vector2.zero, false);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00006DE5 File Offset: 0x00004FE5
		public static void EndGroup()
		{
			GUIUtility.CheckOnGUI();
			GUIClip.Internal_Pop();
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00006DF4 File Offset: 0x00004FF4
		public static void BeginClip(Rect position)
		{
			GUIUtility.CheckOnGUI();
			GUIClip.Push(position, Vector2.zero, Vector2.zero, false);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00006E0F File Offset: 0x0000500F
		public static void EndClip()
		{
			GUIUtility.CheckOnGUI();
			GUIClip.Pop();
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00006E1E File Offset: 0x0000501E
		// (set) Token: 0x06000129 RID: 297 RVA: 0x00006E25 File Offset: 0x00005025
		internal static GenericStack scrollViewStates { get; set; } = new GenericStack();

		// Token: 0x0600012A RID: 298 RVA: 0x00006E30 File Offset: 0x00005030
		public static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect)
		{
			return GUI.BeginScrollView(position, scrollPosition, viewRect, false, false, GUI.skin.horizontalScrollbar, GUI.skin.verticalScrollbar, GUI.skin.scrollView);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00006E6C File Offset: 0x0000506C
		public static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical)
		{
			return GUI.BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical, GUI.skin.horizontalScrollbar, GUI.skin.verticalScrollbar, GUI.skin.scrollView);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00006EA8 File Offset: 0x000050A8
		public static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar)
		{
			return GUI.BeginScrollView(position, scrollPosition, viewRect, false, false, horizontalScrollbar, verticalScrollbar, GUI.skin.scrollView);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00006ED4 File Offset: 0x000050D4
		public static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar)
		{
			return GUI.BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, GUI.skin.scrollView);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00006F00 File Offset: 0x00005100
		protected static Vector2 DoBeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, GUIStyle background)
		{
			return GUI.BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, background);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00006F24 File Offset: 0x00005124
		internal static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, GUIStyle background)
		{
			GUIUtility.CheckOnGUI();
			int controlID = GUIUtility.GetControlID(GUI.s_ScrollviewHash, FocusType.Passive);
			ScrollViewState scrollViewState = (ScrollViewState)GUIUtility.GetStateObject(typeof(ScrollViewState), controlID);
			bool apply = scrollViewState.apply;
			if (apply)
			{
				scrollPosition = scrollViewState.scrollPosition;
				scrollViewState.apply = false;
			}
			scrollViewState.position = position;
			scrollViewState.scrollPosition = scrollPosition;
			scrollViewState.visibleRect = (scrollViewState.viewRect = viewRect);
			scrollViewState.visibleRect.width = position.width;
			scrollViewState.visibleRect.height = position.height;
			GUI.scrollViewStates.Push(scrollViewState);
			Rect rect = new Rect(position);
			EventType type = Event.current.type;
			EventType eventType = type;
			if (eventType != EventType.Layout)
			{
				if (eventType != EventType.Used)
				{
					bool flag = alwaysShowVertical;
					bool flag2 = alwaysShowHorizontal;
					bool flag3 = flag2 || viewRect.width > rect.width;
					if (flag3)
					{
						scrollViewState.visibleRect.height = position.height - horizontalScrollbar.fixedHeight + (float)horizontalScrollbar.margin.top;
						rect.height -= horizontalScrollbar.fixedHeight + (float)horizontalScrollbar.margin.top;
						flag2 = true;
					}
					bool flag4 = flag || viewRect.height > rect.height;
					if (flag4)
					{
						scrollViewState.visibleRect.width = position.width - verticalScrollbar.fixedWidth + (float)verticalScrollbar.margin.left;
						rect.width -= verticalScrollbar.fixedWidth + (float)verticalScrollbar.margin.left;
						flag = true;
						bool flag5 = !flag2 && viewRect.width > rect.width;
						if (flag5)
						{
							scrollViewState.visibleRect.height = position.height - horizontalScrollbar.fixedHeight + (float)horizontalScrollbar.margin.top;
							rect.height -= horizontalScrollbar.fixedHeight + (float)horizontalScrollbar.margin.top;
							flag2 = true;
						}
					}
					bool flag6 = Event.current.type == EventType.Repaint && background != GUIStyle.none;
					if (flag6)
					{
						background.Draw(position, position.Contains(Event.current.mousePosition), false, flag2 && flag, false);
					}
					bool flag7 = flag2 && horizontalScrollbar != GUIStyle.none;
					if (flag7)
					{
						scrollPosition.x = GUI.HorizontalScrollbar(new Rect(position.x, position.yMax - horizontalScrollbar.fixedHeight, rect.width, horizontalScrollbar.fixedHeight), scrollPosition.x, Mathf.Min(rect.width, viewRect.width), 0f, viewRect.width, horizontalScrollbar);
					}
					else
					{
						GUIUtility.GetControlID(GUI.s_SliderHash, FocusType.Passive);
						GUIUtility.GetControlID(GUI.s_RepeatButtonHash, FocusType.Passive);
						GUIUtility.GetControlID(GUI.s_RepeatButtonHash, FocusType.Passive);
						scrollPosition.x = ((horizontalScrollbar != GUIStyle.none) ? 0f : Mathf.Clamp(scrollPosition.x, 0f, Mathf.Max(viewRect.width - position.width, 0f)));
					}
					bool flag8 = flag && verticalScrollbar != GUIStyle.none;
					if (flag8)
					{
						scrollPosition.y = GUI.VerticalScrollbar(new Rect(rect.xMax + (float)verticalScrollbar.margin.left, rect.y, verticalScrollbar.fixedWidth, rect.height), scrollPosition.y, Mathf.Min(rect.height, viewRect.height), 0f, viewRect.height, verticalScrollbar);
					}
					else
					{
						GUIUtility.GetControlID(GUI.s_SliderHash, FocusType.Passive);
						GUIUtility.GetControlID(GUI.s_RepeatButtonHash, FocusType.Passive);
						GUIUtility.GetControlID(GUI.s_RepeatButtonHash, FocusType.Passive);
						scrollPosition.y = ((verticalScrollbar != GUIStyle.none) ? 0f : Mathf.Clamp(scrollPosition.y, 0f, Mathf.Max(viewRect.height - position.height, 0f)));
					}
				}
			}
			else
			{
				GUIUtility.GetControlID(GUI.s_SliderHash, FocusType.Passive);
				GUIUtility.GetControlID(GUI.s_RepeatButtonHash, FocusType.Passive);
				GUIUtility.GetControlID(GUI.s_RepeatButtonHash, FocusType.Passive);
				GUIUtility.GetControlID(GUI.s_SliderHash, FocusType.Passive);
				GUIUtility.GetControlID(GUI.s_RepeatButtonHash, FocusType.Passive);
				GUIUtility.GetControlID(GUI.s_RepeatButtonHash, FocusType.Passive);
			}
			GUIClip.Push(rect, new Vector2(Mathf.Round(-scrollPosition.x - viewRect.x), Mathf.Round(-scrollPosition.y - viewRect.y)), Vector2.zero, false);
			return scrollPosition;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000073E3 File Offset: 0x000055E3
		public static void EndScrollView()
		{
			GUI.EndScrollView(true);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000073F0 File Offset: 0x000055F0
		public static void EndScrollView(bool handleScrollWheel)
		{
			GUIUtility.CheckOnGUI();
			bool flag = GUI.scrollViewStates.Count == 0;
			if (!flag)
			{
				ScrollViewState scrollViewState = (ScrollViewState)GUI.scrollViewStates.Peek();
				GUIClip.Pop();
				GUI.scrollViewStates.Pop();
				bool flag2 = false;
				float num = Time.realtimeSinceStartup - scrollViewState.previousTimeSinceStartup;
				scrollViewState.previousTimeSinceStartup = Time.realtimeSinceStartup;
				bool flag3 = Event.current.type == EventType.Repaint && scrollViewState.velocity != Vector2.zero;
				if (flag3)
				{
					for (int i = 0; i < 2; i++)
					{
						ref Vector2 ptr = ref scrollViewState.velocity;
						int num2 = i;
						ptr[num2] *= Mathf.Pow(0.1f, num);
						float num3 = 0.1f / num;
						bool flag4 = Mathf.Abs(scrollViewState.velocity[i]) < num3;
						if (flag4)
						{
							scrollViewState.velocity[i] = 0f;
						}
						else
						{
							ptr = ref scrollViewState.velocity;
							num2 = i;
							ptr[num2] += ((scrollViewState.velocity[i] < 0f) ? num3 : (-num3));
							ptr = ref scrollViewState.scrollPosition;
							num2 = i;
							ptr[num2] += scrollViewState.velocity[i] * num;
							flag2 = true;
							scrollViewState.touchScrollStartMousePosition = Event.current.mousePosition;
							scrollViewState.touchScrollStartPosition = scrollViewState.scrollPosition;
						}
					}
					bool flag5 = scrollViewState.velocity != Vector2.zero;
					if (flag5)
					{
						GUI.InternalRepaintEditorWindow();
					}
				}
				bool flag6 = handleScrollWheel && (Event.current.type == EventType.ScrollWheel || Event.current.type == EventType.TouchDown || Event.current.type == EventType.TouchUp || Event.current.type == EventType.TouchMove);
				if (flag6)
				{
					bool flag7 = Event.current.type == EventType.ScrollWheel && scrollViewState.position.Contains(Event.current.mousePosition);
					if (flag7)
					{
						scrollViewState.scrollPosition.x = Mathf.Clamp(scrollViewState.scrollPosition.x + Event.current.delta.x * 20f, 0f, scrollViewState.viewRect.width - scrollViewState.visibleRect.width);
						scrollViewState.scrollPosition.y = Mathf.Clamp(scrollViewState.scrollPosition.y + Event.current.delta.y * 20f, 0f, scrollViewState.viewRect.height - scrollViewState.visibleRect.height);
						Event.current.Use();
						flag2 = true;
					}
					else
					{
						bool flag8 = Event.current.type == EventType.TouchDown && (Event.current.modifiers & EventModifiers.Alt) == EventModifiers.Alt && scrollViewState.position.Contains(Event.current.mousePosition);
						if (flag8)
						{
							scrollViewState.isDuringTouchScroll = true;
							scrollViewState.touchScrollStartMousePosition = Event.current.mousePosition;
							scrollViewState.touchScrollStartPosition = scrollViewState.scrollPosition;
							GUIUtility.hotControl = GUIUtility.GetControlID(GUI.s_ScrollviewHash, FocusType.Passive, scrollViewState.position);
							Event.current.Use();
						}
						else
						{
							bool flag9 = scrollViewState.isDuringTouchScroll && Event.current.type == EventType.TouchUp;
							if (flag9)
							{
								scrollViewState.isDuringTouchScroll = false;
							}
							else
							{
								bool flag10 = scrollViewState.isDuringTouchScroll && Event.current.type == EventType.TouchMove;
								if (flag10)
								{
									Vector2 scrollPosition = scrollViewState.scrollPosition;
									scrollViewState.scrollPosition.x = Mathf.Clamp(scrollViewState.touchScrollStartPosition.x - (Event.current.mousePosition.x - scrollViewState.touchScrollStartMousePosition.x), 0f, scrollViewState.viewRect.width - scrollViewState.visibleRect.width);
									scrollViewState.scrollPosition.y = Mathf.Clamp(scrollViewState.touchScrollStartPosition.y - (Event.current.mousePosition.y - scrollViewState.touchScrollStartMousePosition.y), 0f, scrollViewState.viewRect.height - scrollViewState.visibleRect.height);
									Event.current.Use();
									Vector2 vector = (scrollViewState.scrollPosition - scrollPosition) / num;
									scrollViewState.velocity = Vector2.Lerp(scrollViewState.velocity, vector, num * 10f);
									flag2 = true;
								}
							}
						}
					}
				}
				bool flag11 = flag2;
				if (flag11)
				{
					bool flag12 = scrollViewState.scrollPosition.x < 0f;
					if (flag12)
					{
						scrollViewState.scrollPosition.x = 0f;
					}
					bool flag13 = scrollViewState.scrollPosition.y < 0f;
					if (flag13)
					{
						scrollViewState.scrollPosition.y = 0f;
					}
					scrollViewState.apply = true;
				}
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000078F4 File Offset: 0x00005AF4
		internal static ScrollViewState GetTopScrollView()
		{
			bool flag = GUI.scrollViewStates.Count != 0;
			ScrollViewState scrollViewState;
			if (flag)
			{
				scrollViewState = (ScrollViewState)GUI.scrollViewStates.Peek();
			}
			else
			{
				scrollViewState = null;
			}
			return scrollViewState;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000792C File Offset: 0x00005B2C
		public static void ScrollTo(Rect position)
		{
			ScrollViewState topScrollView = GUI.GetTopScrollView();
			if (topScrollView != null)
			{
				topScrollView.ScrollTo(position);
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00007950 File Offset: 0x00005B50
		public static bool ScrollTowards(Rect position, float maxDelta)
		{
			ScrollViewState topScrollView = GUI.GetTopScrollView();
			bool flag = topScrollView == null;
			return !flag && topScrollView.ScrollTowards(position, maxDelta);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000797C File Offset: 0x00005B7C
		public static Rect Window(int id, Rect clientRect, GUI.WindowFunction func, string text)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoWindow(id, clientRect, func, GUIContent.Temp(text), GUI.skin.window, GUI.skin, true);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000079B4 File Offset: 0x00005BB4
		public static Rect Window(int id, Rect clientRect, GUI.WindowFunction func, Texture image)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoWindow(id, clientRect, func, GUIContent.Temp(image), GUI.skin.window, GUI.skin, true);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000079EC File Offset: 0x00005BEC
		public static Rect Window(int id, Rect clientRect, GUI.WindowFunction func, GUIContent content)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoWindow(id, clientRect, func, content, GUI.skin.window, GUI.skin, true);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00007A20 File Offset: 0x00005C20
		public static Rect Window(int id, Rect clientRect, GUI.WindowFunction func, string text, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoWindow(id, clientRect, func, GUIContent.Temp(text), style, GUI.skin, true);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00007A50 File Offset: 0x00005C50
		public static Rect Window(int id, Rect clientRect, GUI.WindowFunction func, Texture image, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoWindow(id, clientRect, func, GUIContent.Temp(image), style, GUI.skin, true);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00007A80 File Offset: 0x00005C80
		public static Rect Window(int id, Rect clientRect, GUI.WindowFunction func, GUIContent title, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoWindow(id, clientRect, func, title, style, GUI.skin, true);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00007AAC File Offset: 0x00005CAC
		public static Rect ModalWindow(int id, Rect clientRect, GUI.WindowFunction func, string text)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoModalWindow(id, clientRect, func, GUIContent.Temp(text), GUI.skin.window, GUI.skin);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00007AE4 File Offset: 0x00005CE4
		public static Rect ModalWindow(int id, Rect clientRect, GUI.WindowFunction func, Texture image)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoModalWindow(id, clientRect, func, GUIContent.Temp(image), GUI.skin.window, GUI.skin);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00007B1C File Offset: 0x00005D1C
		public static Rect ModalWindow(int id, Rect clientRect, GUI.WindowFunction func, GUIContent content)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoModalWindow(id, clientRect, func, content, GUI.skin.window, GUI.skin);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00007B4C File Offset: 0x00005D4C
		public static Rect ModalWindow(int id, Rect clientRect, GUI.WindowFunction func, string text, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoModalWindow(id, clientRect, func, GUIContent.Temp(text), style, GUI.skin);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00007B7C File Offset: 0x00005D7C
		public static Rect ModalWindow(int id, Rect clientRect, GUI.WindowFunction func, Texture image, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoModalWindow(id, clientRect, func, GUIContent.Temp(image), style, GUI.skin);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00007BAC File Offset: 0x00005DAC
		public static Rect ModalWindow(int id, Rect clientRect, GUI.WindowFunction func, GUIContent content, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoModalWindow(id, clientRect, func, content, style, GUI.skin);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00007BD4 File Offset: 0x00005DD4
		private static Rect DoWindow(int id, Rect clientRect, GUI.WindowFunction func, GUIContent title, GUIStyle style, GUISkin skin, bool forceRectOnLayout)
		{
			return GUI.Internal_DoWindow(id, GUIUtility.s_OriginalID, clientRect, func, title, style, skin, forceRectOnLayout);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00007BFC File Offset: 0x00005DFC
		private static Rect DoModalWindow(int id, Rect clientRect, GUI.WindowFunction func, GUIContent content, GUIStyle style, GUISkin skin)
		{
			return GUI.Internal_DoModalWindow(id, GUIUtility.s_OriginalID, clientRect, func, content, style, skin);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00007C20 File Offset: 0x00005E20
		[RequiredByNativeCode]
		internal static void CallWindowDelegate(GUI.WindowFunction func, int id, int instanceID, GUISkin _skin, int forceRect, float width, float height, GUIStyle style)
		{
			GUILayoutUtility.SelectIDList(id, true);
			GUISkin skin = GUI.skin;
			bool flag = Event.current.type == EventType.Layout;
			if (flag)
			{
				bool flag2 = forceRect != 0;
				if (flag2)
				{
					GUILayoutOption[] array = new GUILayoutOption[]
					{
						GUILayout.Width(width),
						GUILayout.Height(height)
					};
					GUILayoutUtility.BeginWindow(id, style, array);
				}
				else
				{
					GUILayoutUtility.BeginWindow(id, style, null);
				}
			}
			else
			{
				GUILayoutUtility.BeginWindow(id, GUIStyle.none, null);
			}
			GUI.skin = _skin;
			if (func != null)
			{
				func(id);
			}
			bool flag3 = Event.current.type == EventType.Layout;
			if (flag3)
			{
				GUILayoutUtility.Layout();
			}
			GUI.skin = skin;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00007CD4 File Offset: 0x00005ED4
		public static void DragWindow()
		{
			GUI.DragWindow(new Rect(0f, 0f, 10000f, 10000f));
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00007CF8 File Offset: 0x00005EF8
		internal static void BeginWindows(int skinMode, int editorWindowInstanceID)
		{
			GUILayoutGroup topLevel = GUILayoutUtility.current.topLevel;
			GenericStack layoutGroups = GUILayoutUtility.current.layoutGroups;
			GUILayoutGroup windows = GUILayoutUtility.current.windows;
			Matrix4x4 matrix = GUI.matrix;
			GUI.Internal_BeginWindows();
			GUI.matrix = matrix;
			GUILayoutUtility.current.topLevel = topLevel;
			GUILayoutUtility.current.layoutGroups = layoutGroups;
			GUILayoutUtility.current.windows = windows;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00007D5C File Offset: 0x00005F5C
		internal static void EndWindows()
		{
			GUILayoutGroup topLevel = GUILayoutUtility.current.topLevel;
			GenericStack layoutGroups = GUILayoutUtility.current.layoutGroups;
			GUILayoutGroup windows = GUILayoutUtility.current.windows;
			GUI.Internal_EndWindows();
			GUILayoutUtility.current.topLevel = topLevel;
			GUILayoutUtility.current.layoutGroups = layoutGroups;
			GUILayoutUtility.current.windows = windows;
		}

		// Token: 0x06000148 RID: 328
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_color_Injected(out Color ret);

		// Token: 0x06000149 RID: 329
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_color_Injected([In] ref Color value);

		// Token: 0x0600014A RID: 330
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_backgroundColor_Injected(out Color ret);

		// Token: 0x0600014B RID: 331
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_backgroundColor_Injected([In] ref Color value);

		// Token: 0x0600014C RID: 332
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_contentColor_Injected(out Color ret);

		// Token: 0x0600014D RID: 333
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_contentColor_Injected([In] ref Color value);

		// Token: 0x0600014E RID: 334
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_blendMaterial_Injected();

		// Token: 0x0600014F RID: 335
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_blitMaterial_Injected();

		// Token: 0x06000150 RID: 336
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_roundedRectMaterial_Injected();

		// Token: 0x06000151 RID: 337
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_roundedRectWithColorPerBorderMaterial_Injected();

		// Token: 0x06000152 RID: 338
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetNextControlName_Injected(ref ManagedSpanWrapper name);

		// Token: 0x06000153 RID: 339
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetNameOfFocusedControl_Injected(out ManagedSpanWrapper ret);

		// Token: 0x06000154 RID: 340
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FocusControl_Injected(ref ManagedSpanWrapper name);

		// Token: 0x06000155 RID: 341
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetTooltip_Injected(out ManagedSpanWrapper ret);

		// Token: 0x06000156 RID: 342
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetTooltip_Injected(ref ManagedSpanWrapper value);

		// Token: 0x06000157 RID: 343
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetMouseTooltip_Injected(out ManagedSpanWrapper ret);

		// Token: 0x06000158 RID: 344
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DoModalWindow_Injected(int id, int instanceID, [In] ref Rect clientRect, GUI.WindowFunction func, GUIContent content, GUIStyle style, object skin, out Rect ret);

		// Token: 0x06000159 RID: 345
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DoWindow_Injected(int id, int instanceID, [In] ref Rect clientRect, GUI.WindowFunction func, GUIContent title, GUIStyle style, object skin, bool forceRectOnLayout, out Rect ret);

		// Token: 0x0600015A RID: 346
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DragWindow_Injected([In] ref Rect position);

		// Token: 0x0600015B RID: 347
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Concatenate_Injected(GUIContent first, GUIContent second, out ManagedSpanWrapper ret);

		// Token: 0x04000054 RID: 84
		private const float s_ScrollStepSize = 10f;

		// Token: 0x04000055 RID: 85
		private static int s_ScrollControlId;

		// Token: 0x04000056 RID: 86
		private static int s_HotTextField = -1;

		// Token: 0x04000057 RID: 87
		private static readonly int s_BoxHash = "Box".GetHashCode();

		// Token: 0x04000058 RID: 88
		private static readonly int s_ButonHash = "Button".GetHashCode();

		// Token: 0x04000059 RID: 89
		private static readonly int s_RepeatButtonHash = "repeatButton".GetHashCode();

		// Token: 0x0400005A RID: 90
		private static readonly int s_ToggleHash = "Toggle".GetHashCode();

		// Token: 0x0400005B RID: 91
		private static readonly int s_ButtonGridHash = "ButtonGrid".GetHashCode();

		// Token: 0x0400005C RID: 92
		private static readonly int s_SliderHash = "Slider".GetHashCode();

		// Token: 0x0400005D RID: 93
		private static readonly int s_BeginGroupHash = "BeginGroup".GetHashCode();

		// Token: 0x0400005E RID: 94
		private static readonly int s_ScrollviewHash = "scrollView".GetHashCode();

		// Token: 0x04000061 RID: 97
		private static GUISkin s_Skin;

		// Token: 0x04000062 RID: 98
		internal static Rect s_ToolTipRect;

		// Token: 0x0200000A RID: 10
		public enum ToolbarButtonSize
		{
			// Token: 0x04000065 RID: 101
			Fixed,
			// Token: 0x04000066 RID: 102
			FitToContents
		}

		// Token: 0x0200000B RID: 11
		// (Invoke) Token: 0x0600015D RID: 349
		internal delegate void CustomSelectionGridItemGUI(int item, Rect rect, GUIStyle style, int controlID);

		// Token: 0x0200000C RID: 12
		// (Invoke) Token: 0x06000161 RID: 353
		public delegate void WindowFunction(int id);

		// Token: 0x0200000D RID: 13
		public abstract class Scope : IDisposable
		{
			// Token: 0x06000164 RID: 356 RVA: 0x00007DBC File Offset: 0x00005FBC
			internal virtual void Dispose(bool disposing)
			{
				bool disposed = this.m_Disposed;
				if (!disposed)
				{
					bool flag = disposing && !GUIUtility.guiIsExiting;
					if (flag)
					{
						this.CloseScope();
					}
					this.m_Disposed = true;
				}
			}

			// Token: 0x06000165 RID: 357 RVA: 0x00007DF8 File Offset: 0x00005FF8
			protected override void Finalize()
			{
				try
				{
					bool flag = !this.m_Disposed && !GUIUtility.guiIsExiting;
					if (flag)
					{
						Console.WriteLine(base.GetType().Name + " was not disposed! You should use the 'using' keyword or manually call Dispose.");
					}
					this.Dispose(false);
				}
				finally
				{
					base.Finalize();
				}
			}

			// Token: 0x06000166 RID: 358 RVA: 0x00007E5C File Offset: 0x0000605C
			public void Dispose()
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}

			// Token: 0x06000167 RID: 359
			protected abstract void CloseScope();

			// Token: 0x04000067 RID: 103
			private bool m_Disposed;
		}

		// Token: 0x0200000E RID: 14
		public class GroupScope : GUI.Scope
		{
			// Token: 0x06000169 RID: 361 RVA: 0x00007E6E File Offset: 0x0000606E
			public GroupScope(Rect position)
			{
				GUI.BeginGroup(position);
			}

			// Token: 0x0600016A RID: 362 RVA: 0x00007E7F File Offset: 0x0000607F
			public GroupScope(Rect position, string text)
			{
				GUI.BeginGroup(position, text);
			}

			// Token: 0x0600016B RID: 363 RVA: 0x00007E91 File Offset: 0x00006091
			public GroupScope(Rect position, Texture image)
			{
				GUI.BeginGroup(position, image);
			}

			// Token: 0x0600016C RID: 364 RVA: 0x00007EA3 File Offset: 0x000060A3
			public GroupScope(Rect position, GUIContent content)
			{
				GUI.BeginGroup(position, content);
			}

			// Token: 0x0600016D RID: 365 RVA: 0x00007EB5 File Offset: 0x000060B5
			public GroupScope(Rect position, GUIStyle style)
			{
				GUI.BeginGroup(position, style);
			}

			// Token: 0x0600016E RID: 366 RVA: 0x00007EC7 File Offset: 0x000060C7
			public GroupScope(Rect position, string text, GUIStyle style)
			{
				GUI.BeginGroup(position, text, style);
			}

			// Token: 0x0600016F RID: 367 RVA: 0x00007EDA File Offset: 0x000060DA
			public GroupScope(Rect position, Texture image, GUIStyle style)
			{
				GUI.BeginGroup(position, image, style);
			}

			// Token: 0x06000170 RID: 368 RVA: 0x00007EED File Offset: 0x000060ED
			protected override void CloseScope()
			{
				GUI.EndGroup();
			}
		}

		// Token: 0x0200000F RID: 15
		public class ScrollViewScope : GUI.Scope
		{
			// Token: 0x17000036 RID: 54
			// (get) Token: 0x06000171 RID: 369 RVA: 0x00007EF6 File Offset: 0x000060F6
			// (set) Token: 0x06000172 RID: 370 RVA: 0x00007EFE File Offset: 0x000060FE
			public Vector2 scrollPosition { get; private set; }

			// Token: 0x17000037 RID: 55
			// (get) Token: 0x06000173 RID: 371 RVA: 0x00007F07 File Offset: 0x00006107
			// (set) Token: 0x06000174 RID: 372 RVA: 0x00007F0F File Offset: 0x0000610F
			public bool handleScrollWheel { get; set; }

			// Token: 0x06000175 RID: 373 RVA: 0x00007F18 File Offset: 0x00006118
			public ScrollViewScope(Rect position, Vector2 scrollPosition, Rect viewRect)
			{
				this.handleScrollWheel = true;
				this.scrollPosition = GUI.BeginScrollView(position, scrollPosition, viewRect);
			}

			// Token: 0x06000176 RID: 374 RVA: 0x00007F39 File Offset: 0x00006139
			public ScrollViewScope(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical)
			{
				this.handleScrollWheel = true;
				this.scrollPosition = GUI.BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical);
			}

			// Token: 0x06000177 RID: 375 RVA: 0x00007F5E File Offset: 0x0000615E
			public ScrollViewScope(Rect position, Vector2 scrollPosition, Rect viewRect, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar)
			{
				this.handleScrollWheel = true;
				this.scrollPosition = GUI.BeginScrollView(position, scrollPosition, viewRect, horizontalScrollbar, verticalScrollbar);
			}

			// Token: 0x06000178 RID: 376 RVA: 0x00007F83 File Offset: 0x00006183
			public ScrollViewScope(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar)
			{
				this.handleScrollWheel = true;
				this.scrollPosition = GUI.BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar);
			}

			// Token: 0x06000179 RID: 377 RVA: 0x00007FAC File Offset: 0x000061AC
			internal ScrollViewScope(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, GUIStyle background)
			{
				this.handleScrollWheel = true;
				this.scrollPosition = GUI.BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, background);
			}

			// Token: 0x0600017A RID: 378 RVA: 0x00007FE2 File Offset: 0x000061E2
			protected override void CloseScope()
			{
				GUI.EndScrollView(this.handleScrollWheel);
			}
		}

		// Token: 0x02000010 RID: 16
		public class ClipScope : GUI.Scope
		{
			// Token: 0x0600017B RID: 379 RVA: 0x00007FF1 File Offset: 0x000061F1
			public ClipScope(Rect position)
			{
				GUI.BeginClip(position);
			}

			// Token: 0x0600017C RID: 380 RVA: 0x00008004 File Offset: 0x00006204
			internal ClipScope(Rect position, Vector2 scrollOffset)
			{
				GUI.BeginClip(position, scrollOffset, default(Vector2), false);
			}

			// Token: 0x0600017D RID: 381 RVA: 0x0000802B File Offset: 0x0000622B
			protected override void CloseScope()
			{
				GUI.EndClip();
			}
		}

		// Token: 0x02000011 RID: 17
		internal struct ColorScope : IDisposable
		{
			// Token: 0x0600017E RID: 382 RVA: 0x00008034 File Offset: 0x00006234
			public ColorScope(Color newColor)
			{
				this.m_Disposed = false;
				this.m_PreviousColor = GUI.color;
				GUI.color = newColor;
			}

			// Token: 0x0600017F RID: 383 RVA: 0x00008050 File Offset: 0x00006250
			public ColorScope(float r, float g, float b, float a = 1f)
			{
				this = new GUI.ColorScope(new Color(r, g, b, a));
			}

			// Token: 0x06000180 RID: 384 RVA: 0x00008064 File Offset: 0x00006264
			public void Dispose()
			{
				bool disposed = this.m_Disposed;
				if (!disposed)
				{
					this.m_Disposed = true;
					GUI.color = this.m_PreviousColor;
				}
			}

			// Token: 0x0400006A RID: 106
			private bool m_Disposed;

			// Token: 0x0400006B RID: 107
			private Color m_PreviousColor;
		}

		// Token: 0x02000012 RID: 18
		internal struct BackgroundColorScope : IDisposable
		{
			// Token: 0x06000181 RID: 385 RVA: 0x00008091 File Offset: 0x00006291
			public BackgroundColorScope(Color newColor)
			{
				this.m_Disposed = false;
				this.m_PreviousColor = GUI.backgroundColor;
				GUI.backgroundColor = newColor;
			}

			// Token: 0x06000182 RID: 386 RVA: 0x000080AD File Offset: 0x000062AD
			public BackgroundColorScope(float r, float g, float b, float a = 1f)
			{
				this = new GUI.BackgroundColorScope(new Color(r, g, b, a));
			}

			// Token: 0x06000183 RID: 387 RVA: 0x000080C4 File Offset: 0x000062C4
			public void Dispose()
			{
				bool disposed = this.m_Disposed;
				if (!disposed)
				{
					this.m_Disposed = true;
					GUI.backgroundColor = this.m_PreviousColor;
				}
			}

			// Token: 0x0400006C RID: 108
			private bool m_Disposed;

			// Token: 0x0400006D RID: 109
			private Color m_PreviousColor;
		}
	}
}
