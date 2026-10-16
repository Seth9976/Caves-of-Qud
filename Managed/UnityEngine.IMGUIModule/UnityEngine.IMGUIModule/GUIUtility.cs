using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000034 RID: 52
	[NativeHeader("Modules/IMGUI/GUIUtility.h")]
	[NativeHeader("Runtime/Input/InputBindings.h")]
	[NativeHeader("Runtime/Input/InputManager.h")]
	[NativeHeader("Runtime/Camera/RenderLayers/GUITexture.h")]
	[NativeHeader("Runtime/Utilities/CopyPaste.h")]
	[NativeHeader("Modules/IMGUI/GUIManager.h")]
	public class GUIUtility
	{
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060003E4 RID: 996
		public static extern bool hasModalWindow
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060003E5 RID: 997
		// (set) Token: 0x060003E6 RID: 998
		[NativeProperty("GetGUIState().m_PixelsPerPoint", true, TargetType.Field)]
		internal static extern float pixelsPerPoint
		{
			[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060003E7 RID: 999
		[NativeProperty("GetGUIState().m_OnGUIDepth", true, TargetType.Field)]
		internal static extern int guiDepth
		{
			[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x0000DB00 File Offset: 0x0000BD00
		// (set) Token: 0x060003E9 RID: 1001 RVA: 0x0000DB18 File Offset: 0x0000BD18
		internal static Vector2 s_EditorScreenPointOffset
		{
			[NativeMethod("GetGUIState().GetGUIPixelOffset", true)]
			get
			{
				Vector2 vector;
				GUIUtility.get_s_EditorScreenPointOffset_Injected(out vector);
				return vector;
			}
			[NativeMethod("GetGUIState().SetGUIPixelOffset", true)]
			set
			{
				GUIUtility.set_s_EditorScreenPointOffset_Injected(ref value);
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060003EA RID: 1002
		// (set) Token: 0x060003EB RID: 1003
		[NativeProperty("GetGUIState().m_CanvasGUIState.m_IsMouseUsed", true, TargetType.Field)]
		internal static extern bool mouseUsed
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060003EC RID: 1004
		// (set) Token: 0x060003ED RID: 1005
		[StaticAccessor("GetInputManager()", StaticAccessorType.Dot)]
		internal static extern bool textFieldInput
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060003EE RID: 1006
		// (set) Token: 0x060003EF RID: 1007
		internal static extern bool manualTex2SRGBEnabled
		{
			[FreeFunction("GUITexture::IsManualTex2SRGBEnabled")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction("GUITexture::SetManualTex2SRGBEnabled")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0000DB2C File Offset: 0x0000BD2C
		// (set) Token: 0x060003F1 RID: 1009 RVA: 0x0000DB5C File Offset: 0x0000BD5C
		public unsafe static string systemCopyBuffer
		{
			[FreeFunction("GetCopyBuffer")]
			get
			{
				string stringAndDispose;
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					GUIUtility.get_systemCopyBuffer_Injected(out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
			[FreeFunction("SetCopyBuffer")]
			set
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
					GUIUtility.set_systemCopyBuffer_Injected(ref managedSpanWrapper);
				}
				finally
				{
					char* ptr = null;
				}
			}
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000DBB0 File Offset: 0x0000BDB0
		[FreeFunction("GetGUIState().GetControlID")]
		private static int Internal_GetControlID(int hint, FocusType focusType, Rect rect)
		{
			return GUIUtility.Internal_GetControlID_Injected(hint, focusType, ref rect);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000DBC8 File Offset: 0x0000BDC8
		public static int GetControlID(int hint, FocusType focusType, Rect rect)
		{
			GUIUtility.s_ControlCount++;
			return GUIUtility.Internal_GetControlID(hint, focusType, rect);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000DBF0 File Offset: 0x0000BDF0
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static void BeginContainerFromOwner(ScriptableObject owner)
		{
			GUIUtility.BeginContainerFromOwner_Injected(Object.MarshalledUnityObject.Marshal<ScriptableObject>(owner));
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000DC08 File Offset: 0x0000BE08
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static void BeginContainer(ObjectGUIState objectGUIState)
		{
			GUIUtility.BeginContainer_Injected((objectGUIState == null) ? ((IntPtr)0) : ObjectGUIState.BindingsMarshaller.ConvertToNative(objectGUIState));
		}

		// Token: 0x060003F6 RID: 1014
		[NativeMethod("EndContainer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_EndContainer();

		// Token: 0x060003F7 RID: 1015
		[FreeFunction("GetSpecificGUIState(0).m_EternalGUIState->GetNextUniqueID")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetPermanentControlID();

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000DC2C File Offset: 0x0000BE2C
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static int CheckForTabEvent(Event evt)
		{
			return GUIUtility.CheckForTabEvent_Injected((evt == null) ? ((IntPtr)0) : Event.BindingsMarshaller.ConvertToNative(evt));
		}

		// Token: 0x060003F9 RID: 1017
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetKeyboardControlToFirstControlId();

		// Token: 0x060003FA RID: 1018
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetKeyboardControlToLastControlId();

		// Token: 0x060003FB RID: 1019
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool HasFocusableControls();

		// Token: 0x060003FC RID: 1020
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool OwnsId(int id);

		// Token: 0x060003FD RID: 1021 RVA: 0x0000DC50 File Offset: 0x0000BE50
		public static Rect AlignRectToDevice(Rect rect, out int widthInPixels, out int heightInPixels)
		{
			Rect rect2;
			GUIUtility.AlignRectToDevice_Injected(ref rect, out widthInPixels, out heightInPixels, out rect2);
			return rect2;
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x0000DC6C File Offset: 0x0000BE6C
		[StaticAccessor("InputBindings", StaticAccessorType.DoubleColon)]
		internal static string compositionString
		{
			get
			{
				string stringAndDispose;
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					GUIUtility.get_compositionString_Injected(out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060003FF RID: 1023
		// (set) Token: 0x06000400 RID: 1024
		[StaticAccessor("InputBindings", StaticAccessorType.DoubleColon)]
		internal static extern IMECompositionMode imeCompositionMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x0000DC9C File Offset: 0x0000BE9C
		// (set) Token: 0x06000402 RID: 1026 RVA: 0x0000DCB4 File Offset: 0x0000BEB4
		[StaticAccessor("InputBindings", StaticAccessorType.DoubleColon)]
		internal static Vector2 compositionCursorPos
		{
			get
			{
				Vector2 vector;
				GUIUtility.get_compositionCursorPos_Injected(out vector);
				return vector;
			}
			set
			{
				GUIUtility.set_compositionCursorPos_Injected(ref value);
			}
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000DCC8 File Offset: 0x0000BEC8
		internal static Vector3 Internal_MultiplyPoint(Vector3 point, Matrix4x4 transform)
		{
			Vector3 vector;
			GUIUtility.Internal_MultiplyPoint_Injected(ref point, ref transform, out vector);
			return vector;
		}

		// Token: 0x06000404 RID: 1028
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool GetChanged();

		// Token: 0x06000405 RID: 1029
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetChanged(bool changed);

		// Token: 0x06000406 RID: 1030
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetDidGUIWindowsEatLastEvent(bool value);

		// Token: 0x06000407 RID: 1031
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetHotControl();

		// Token: 0x06000408 RID: 1032
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetKeyboardControl();

		// Token: 0x06000409 RID: 1033
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetHotControl(int value);

		// Token: 0x0600040A RID: 1034
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetKeyboardControl(int value);

		// Token: 0x0600040B RID: 1035
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object Internal_GetDefaultSkin(int skinMode);

		// Token: 0x0600040C RID: 1036 RVA: 0x0000DCE4 File Offset: 0x0000BEE4
		private static Object Internal_GetBuiltinSkin(int skin)
		{
			return Unmarshal.UnmarshalUnityObject<Object>(GUIUtility.Internal_GetBuiltinSkin_Injected(skin));
		}

		// Token: 0x0600040D RID: 1037
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_ExitGUI();

		// Token: 0x0600040E RID: 1038 RVA: 0x0000DCFC File Offset: 0x0000BEFC
		private static Vector2 InternalWindowToScreenPoint(Vector2 windowPoint)
		{
			Vector2 vector;
			GUIUtility.InternalWindowToScreenPoint_Injected(ref windowPoint, out vector);
			return vector;
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000DD14 File Offset: 0x0000BF14
		private static Vector2 InternalScreenToWindowPoint(Vector2 screenPoint)
		{
			Vector2 vector;
			GUIUtility.InternalScreenToWindowPoint_Injected(ref screenPoint, out vector);
			return vector;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000DD2B File Offset: 0x0000BF2B
		[RequiredByNativeCode]
		private static void MarkGUIChanged()
		{
			Action action = GUIUtility.guiChanged;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000DD40 File Offset: 0x0000BF40
		public static int GetControlID(FocusType focus)
		{
			return GUIUtility.GetControlID(0, focus);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000DD5C File Offset: 0x0000BF5C
		public static int GetControlID(GUIContent contents, FocusType focus)
		{
			return GUIUtility.GetControlID(contents.hash, focus);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000DD7C File Offset: 0x0000BF7C
		public static int GetControlID(FocusType focus, Rect position)
		{
			return GUIUtility.GetControlID(0, focus, position);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000DD98 File Offset: 0x0000BF98
		public static int GetControlID(GUIContent contents, FocusType focus, Rect position)
		{
			return GUIUtility.GetControlID(contents.hash, focus, position);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000DDB8 File Offset: 0x0000BFB8
		public static int GetControlID(int hint, FocusType focus)
		{
			return GUIUtility.GetControlID(hint, focus, Rect.zero);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000DDD8 File Offset: 0x0000BFD8
		public static object GetStateObject(Type t, int controlID)
		{
			return GUIStateObjects.GetStateObject(t, controlID);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000DDF4 File Offset: 0x0000BFF4
		public static object QueryStateObject(Type t, int controlID)
		{
			return GUIStateObjects.QueryStateObject(t, controlID);
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x0000DE0D File Offset: 0x0000C00D
		// (set) Token: 0x06000419 RID: 1049 RVA: 0x0000DE14 File Offset: 0x0000C014
		internal static bool guiIsExiting { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x0000DE1C File Offset: 0x0000C01C
		// (set) Token: 0x0600041B RID: 1051 RVA: 0x0000DE33 File Offset: 0x0000C033
		public static int hotControl
		{
			get
			{
				return GUIUtility.Internal_GetHotControl();
			}
			set
			{
				GUIUtility.Internal_SetHotControl(value);
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000DE3D File Offset: 0x0000C03D
		[RequiredByNativeCode]
		internal static void TakeCapture()
		{
			Action action = GUIUtility.takeCapture;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000DE51 File Offset: 0x0000C051
		[RequiredByNativeCode]
		internal static void RemoveCapture()
		{
			Action action = GUIUtility.releaseCapture;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x0000DE68 File Offset: 0x0000C068
		// (set) Token: 0x0600041F RID: 1055 RVA: 0x0000DE7F File Offset: 0x0000C07F
		public static int keyboardControl
		{
			get
			{
				return GUIUtility.Internal_GetKeyboardControl();
			}
			set
			{
				GUIUtility.Internal_SetKeyboardControl(value);
			}
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000DE8C File Offset: 0x0000C08C
		internal static bool HasKeyFocus(int controlID)
		{
			return controlID == GUIUtility.keyboardControl && (GUIUtility.s_HasCurrentWindowKeyFocusFunc == null || GUIUtility.s_HasCurrentWindowKeyFocusFunc());
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000DEBD File Offset: 0x0000C0BD
		public static void ExitGUI()
		{
			throw new ExitGUIException();
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000DEC8 File Offset: 0x0000C0C8
		internal static GUISkin GetDefaultSkin(int skinMode)
		{
			return GUIUtility.Internal_GetDefaultSkin(skinMode) as GUISkin;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000DEE8 File Offset: 0x0000C0E8
		internal static GUISkin GetDefaultSkin()
		{
			return GUIUtility.Internal_GetDefaultSkin(GUIUtility.s_SkinMode) as GUISkin;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000DF0C File Offset: 0x0000C10C
		internal static GUISkin GetBuiltinSkin(int skin)
		{
			return GUIUtility.Internal_GetBuiltinSkin(skin) as GUISkin;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000DF2C File Offset: 0x0000C12C
		[RequiredByNativeCode]
		internal static void ProcessEvent(int instanceID, IntPtr nativeEventPtr, out bool result)
		{
			bool flag = GUIUtility.beforeEventProcessed != null;
			if (flag)
			{
				GUIUtility.m_Event.CopyFromPtr(nativeEventPtr);
				GUIUtility.beforeEventProcessed(GUIUtility.m_Event.type, GUIUtility.m_Event.keyCode, GUIUtility.m_Event.modifiers);
			}
			result = false;
			bool flag2 = GUIUtility.processEvent != null;
			if (flag2)
			{
				foreach (Delegate @delegate in GUIUtility.processEvent.GetInvocationList())
				{
					Func<int, IntPtr, bool> func = @delegate as Func<int, IntPtr, bool>;
					bool flag3 = func == null;
					if (!flag3)
					{
						result |= func(instanceID, nativeEventPtr);
					}
				}
			}
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000DFD6 File Offset: 0x0000C1D6
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static void EndContainer()
		{
			GUIUtility.Internal_EndContainer();
			GUIUtility.Internal_ExitGUI();
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000DFE5 File Offset: 0x0000C1E5
		internal static void CleanupRoots()
		{
			Action action = GUIUtility.cleanupRoots;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000DFFC File Offset: 0x0000C1FC
		[RequiredByNativeCode]
		internal static void BeginGUI(int skinMode, int instanceID, int useGUILayout)
		{
			GUIUtility.s_SkinMode = skinMode;
			GUIUtility.s_OriginalID = instanceID;
			GUIUtility.ResetGlobalState();
			bool flag = useGUILayout != 0;
			if (flag)
			{
				GUILayoutUtility.Begin(instanceID);
			}
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0000E02D File Offset: 0x0000C22D
		[RequiredByNativeCode]
		internal static void DestroyGUI(int instanceID)
		{
			GUILayoutUtility.RemoveSelectedIdList(instanceID, false);
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000E038 File Offset: 0x0000C238
		[RequiredByNativeCode]
		internal static void EndGUI(int layoutType)
		{
			try
			{
				bool flag = Event.current.type == EventType.Layout;
				if (flag)
				{
					switch (layoutType)
					{
					case 1:
						GUILayoutUtility.Layout();
						break;
					case 2:
						GUILayoutUtility.LayoutFromEditorWindow();
						break;
					}
				}
				GUILayoutUtility.SelectIDList(GUIUtility.s_OriginalID, false);
				GUIContent.ClearStaticCache();
			}
			finally
			{
				GUIUtility.Internal_ExitGUI();
			}
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000E0B4 File Offset: 0x0000C2B4
		[RequiredByNativeCode]
		internal static bool EndGUIFromException(Exception exception)
		{
			GUIUtility.Internal_ExitGUI();
			return GUIUtility.ShouldRethrowException(exception);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000E0D4 File Offset: 0x0000C2D4
		[RequiredByNativeCode]
		internal static bool EndContainerGUIFromException(Exception exception)
		{
			bool flag = GUIUtility.endContainerGUIFromException != null;
			return flag && GUIUtility.endContainerGUIFromException(exception);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000E101 File Offset: 0x0000C301
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static void ResetGlobalState()
		{
			GUI.skin = null;
			GUIUtility.guiIsExiting = false;
			GUI.changed = false;
			GUI.scrollViewStates.Clear();
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000E124 File Offset: 0x0000C324
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static bool IsExitGUIException(Exception exception)
		{
			while (exception is TargetInvocationException && exception.InnerException != null)
			{
				exception = exception.InnerException;
			}
			return exception is ExitGUIException;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000E164 File Offset: 0x0000C364
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static bool ShouldRethrowException(Exception exception)
		{
			return GUIUtility.IsExitGUIException(exception);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000E17C File Offset: 0x0000C37C
		internal static void CheckOnGUI()
		{
			bool flag = GUIUtility.guiDepth <= 0;
			if (flag)
			{
				throw new ArgumentException("You can only call GUI functions from inside OnGUI.");
			}
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000E1A4 File Offset: 0x0000C3A4
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static float RoundToPixelGrid(float v)
		{
			return Mathf.Floor(v * GUIUtility.pixelsPerPoint + 0.48f) / GUIUtility.pixelsPerPoint;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0000E1D0 File Offset: 0x0000C3D0
		internal static float RoundToPixelGrid(float v, float scale)
		{
			return Mathf.Floor(v * scale + 0.48f) / scale;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0000E1F4 File Offset: 0x0000C3F4
		public static Vector2 GUIToScreenPoint(Vector2 guiPoint)
		{
			return GUIUtility.InternalWindowToScreenPoint(GUIClip.UnclipToWindow(guiPoint));
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0000E214 File Offset: 0x0000C414
		public static Rect GUIToScreenRect(Rect guiRect)
		{
			Vector2 vector = GUIUtility.GUIToScreenPoint(new Vector2(guiRect.x, guiRect.y));
			guiRect.x = vector.x;
			guiRect.y = vector.y;
			return guiRect;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0000E25C File Offset: 0x0000C45C
		public static Vector2 ScreenToGUIPoint(Vector2 screenPoint)
		{
			return GUIClip.ClipToWindow(GUIUtility.InternalScreenToWindowPoint(screenPoint));
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0000E27C File Offset: 0x0000C47C
		public static Rect ScreenToGUIRect(Rect screenRect)
		{
			Vector2 vector = GUIUtility.ScreenToGUIPoint(new Vector2(screenRect.x, screenRect.y));
			screenRect.x = vector.x;
			screenRect.y = vector.y;
			return screenRect;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0000E2C4 File Offset: 0x0000C4C4
		public static void RotateAroundPivot(float angle, Vector2 pivotPoint)
		{
			Matrix4x4 matrix = GUI.matrix;
			GUI.matrix = Matrix4x4.identity;
			Vector2 vector = GUIClip.Unclip(pivotPoint);
			Matrix4x4 matrix4x = Matrix4x4.TRS(vector, Quaternion.Euler(0f, 0f, angle), Vector3.one) * Matrix4x4.TRS(-vector, Quaternion.identity, Vector3.one);
			GUI.matrix = matrix4x * matrix;
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0000E338 File Offset: 0x0000C538
		public static void ScaleAroundPivot(Vector2 scale, Vector2 pivotPoint)
		{
			Matrix4x4 matrix = GUI.matrix;
			Vector2 vector = GUIClip.Unclip(pivotPoint);
			Matrix4x4 matrix4x = Matrix4x4.TRS(vector, Quaternion.identity, new Vector3(scale.x, scale.y, 1f)) * Matrix4x4.TRS(-vector, Quaternion.identity, Vector3.one);
			GUI.matrix = matrix4x * matrix;
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0000E3A8 File Offset: 0x0000C5A8
		public static Rect AlignRectToDevice(Rect rect)
		{
			int num;
			int num2;
			return GUIUtility.AlignRectToDevice(rect, out num, out num2);
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0000E3C4 File Offset: 0x0000C5C4
		internal static bool HitTest(Rect rect, Vector2 point, int offset)
		{
			return point.x >= rect.xMin - (float)offset && point.x < rect.xMax + (float)offset && point.y >= rect.yMin - (float)offset && point.y < rect.yMax + (float)offset;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0000E424 File Offset: 0x0000C624
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static bool HitTest(Rect rect, Vector2 point, bool isDirectManipulationDevice)
		{
			int num = 0;
			return GUIUtility.HitTest(rect, point, num);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0000E440 File Offset: 0x0000C640
		internal static bool HitTest(Rect rect, Event evt)
		{
			return GUIUtility.HitTest(rect, evt.mousePosition, evt.isDirectManipulationDevice);
		}

		// Token: 0x0600043F RID: 1087
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_s_EditorScreenPointOffset_Injected(out Vector2 ret);

		// Token: 0x06000440 RID: 1088
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_s_EditorScreenPointOffset_Injected([In] ref Vector2 value);

		// Token: 0x06000441 RID: 1089
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_systemCopyBuffer_Injected(out ManagedSpanWrapper ret);

		// Token: 0x06000442 RID: 1090
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_systemCopyBuffer_Injected(ref ManagedSpanWrapper value);

		// Token: 0x06000443 RID: 1091
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetControlID_Injected(int hint, FocusType focusType, [In] ref Rect rect);

		// Token: 0x06000444 RID: 1092
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BeginContainerFromOwner_Injected(IntPtr owner);

		// Token: 0x06000445 RID: 1093
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BeginContainer_Injected(IntPtr objectGUIState);

		// Token: 0x06000446 RID: 1094
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int CheckForTabEvent_Injected(IntPtr evt);

		// Token: 0x06000447 RID: 1095
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AlignRectToDevice_Injected([In] ref Rect rect, out int widthInPixels, out int heightInPixels, out Rect ret);

		// Token: 0x06000448 RID: 1096
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_compositionString_Injected(out ManagedSpanWrapper ret);

		// Token: 0x06000449 RID: 1097
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_compositionCursorPos_Injected(out Vector2 ret);

		// Token: 0x0600044A RID: 1098
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_compositionCursorPos_Injected([In] ref Vector2 value);

		// Token: 0x0600044B RID: 1099
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_MultiplyPoint_Injected([In] ref Vector3 point, [In] ref Matrix4x4 transform, out Vector3 ret);

		// Token: 0x0600044C RID: 1100
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_GetBuiltinSkin_Injected(int skin);

		// Token: 0x0600044D RID: 1101
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalWindowToScreenPoint_Injected([In] ref Vector2 windowPoint, out Vector2 ret);

		// Token: 0x0600044E RID: 1102
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalScreenToWindowPoint_Injected([In] ref Vector2 screenPoint, out Vector2 ret);

		// Token: 0x040000EC RID: 236
		internal static int s_ControlCount = 0;

		// Token: 0x040000ED RID: 237
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static int s_SkinMode;

		// Token: 0x040000EE RID: 238
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static int s_OriginalID;

		// Token: 0x040000EF RID: 239
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static Action takeCapture;

		// Token: 0x040000F0 RID: 240
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static Action releaseCapture;

		// Token: 0x040000F1 RID: 241
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static Func<int, IntPtr, bool> processEvent;

		// Token: 0x040000F2 RID: 242
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static Action cleanupRoots;

		// Token: 0x040000F3 RID: 243
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static Func<Exception, bool> endContainerGUIFromException;

		// Token: 0x040000F4 RID: 244
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static Action guiChanged;

		// Token: 0x040000F5 RID: 245
		internal static Action<EventType, KeyCode, EventModifiers> beforeEventProcessed;

		// Token: 0x040000F6 RID: 246
		private static Event m_Event = new Event();

		// Token: 0x040000F8 RID: 248
		internal static Func<bool> s_HasCurrentWindowKeyFocusFunc;
	}
}
