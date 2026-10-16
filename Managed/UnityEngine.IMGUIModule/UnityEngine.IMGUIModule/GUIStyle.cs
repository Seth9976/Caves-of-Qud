using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.TextCore.Text;

namespace UnityEngine
{
	// Token: 0x0200002D RID: 45
	[NativeHeader("IMGUIScriptingClasses.h")]
	[NativeHeader("Modules/IMGUI/GUIStyle.bindings.h")]
	[RequiredByNativeCode]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class GUIStyle
	{
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000326 RID: 806 RVA: 0x0000C3B0 File Offset: 0x0000A5B0
		// (set) Token: 0x06000327 RID: 807 RVA: 0x0000C3F0 File Offset: 0x0000A5F0
		[NativeProperty("Name", false, TargetType.Function)]
		internal unsafe string rawName
		{
			get
			{
				string stringAndDispose;
				try
				{
					IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					GUIStyle.get_rawName_Injected(intPtr, out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
			set
			{
				try
				{
					IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					if (!StringMarshaller.TryMarshalEmptyOrNullString(value, ref managedSpanWrapper))
					{
						ReadOnlySpan<char> readOnlySpan = value.AsSpan();
						fixed (char* ptr = readOnlySpan.GetPinnableReference())
						{
							managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
						}
					}
					GUIStyle.set_rawName_Injected(intPtr, ref managedSpanWrapper);
				}
				finally
				{
					char* ptr = null;
				}
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000C454 File Offset: 0x0000A654
		// (set) Token: 0x06000329 RID: 809 RVA: 0x0000C47C File Offset: 0x0000A67C
		[NativeProperty("Font", false, TargetType.Function)]
		public Font font
		{
			get
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Font>(GUIStyle.get_font_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				GUIStyle.set_font_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Font>(value));
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0000C4A4 File Offset: 0x0000A6A4
		// (set) Token: 0x0600032B RID: 811 RVA: 0x0000C4C8 File Offset: 0x0000A6C8
		[NativeProperty("m_ImagePosition", false, TargetType.Field)]
		public ImagePosition imagePosition
		{
			get
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GUIStyle.get_imagePosition_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				GUIStyle.set_imagePosition_Injected(intPtr, value);
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600032C RID: 812 RVA: 0x0000C4EC File Offset: 0x0000A6EC
		// (set) Token: 0x0600032D RID: 813 RVA: 0x0000C510 File Offset: 0x0000A710
		[NativeProperty("m_Alignment", false, TargetType.Field)]
		public TextAnchor alignment
		{
			get
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GUIStyle.get_alignment_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				GUIStyle.set_alignment_Injected(intPtr, value);
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0000C534 File Offset: 0x0000A734
		// (set) Token: 0x0600032F RID: 815 RVA: 0x0000C558 File Offset: 0x0000A758
		[NativeProperty("m_WordWrap", false, TargetType.Field)]
		public bool wordWrap
		{
			get
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GUIStyle.get_wordWrap_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				GUIStyle.set_wordWrap_Injected(intPtr, value);
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0000C57C File Offset: 0x0000A77C
		// (set) Token: 0x06000331 RID: 817 RVA: 0x0000C5A0 File Offset: 0x0000A7A0
		[NativeProperty("m_Clipping", false, TargetType.Field)]
		public TextClipping clipping
		{
			get
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GUIStyle.get_clipping_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				GUIStyle.set_clipping_Injected(intPtr, value);
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0000C5C4 File Offset: 0x0000A7C4
		// (set) Token: 0x06000333 RID: 819 RVA: 0x0000C5EC File Offset: 0x0000A7EC
		[NativeProperty("m_ContentOffset", false, TargetType.Field)]
		public Vector2 contentOffset
		{
			get
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				GUIStyle.get_contentOffset_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				GUIStyle.set_contentOffset_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000C610 File Offset: 0x0000A810
		// (set) Token: 0x06000335 RID: 821 RVA: 0x0000C634 File Offset: 0x0000A834
		[NativeProperty("m_FixedWidth", false, TargetType.Field)]
		public float fixedWidth
		{
			get
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GUIStyle.get_fixedWidth_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				GUIStyle.set_fixedWidth_Injected(intPtr, value);
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0000C658 File Offset: 0x0000A858
		// (set) Token: 0x06000337 RID: 823 RVA: 0x0000C67C File Offset: 0x0000A87C
		[NativeProperty("m_FixedHeight", false, TargetType.Field)]
		public float fixedHeight
		{
			get
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GUIStyle.get_fixedHeight_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				GUIStyle.set_fixedHeight_Injected(intPtr, value);
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000338 RID: 824 RVA: 0x0000C6A0 File Offset: 0x0000A8A0
		// (set) Token: 0x06000339 RID: 825 RVA: 0x0000C6C4 File Offset: 0x0000A8C4
		[NativeProperty("m_StretchWidth", false, TargetType.Field)]
		public bool stretchWidth
		{
			get
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GUIStyle.get_stretchWidth_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				GUIStyle.set_stretchWidth_Injected(intPtr, value);
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600033A RID: 826 RVA: 0x0000C6E8 File Offset: 0x0000A8E8
		// (set) Token: 0x0600033B RID: 827 RVA: 0x0000C70C File Offset: 0x0000A90C
		[NativeProperty("m_StretchHeight", false, TargetType.Field)]
		public bool stretchHeight
		{
			get
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GUIStyle.get_stretchHeight_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				GUIStyle.set_stretchHeight_Injected(intPtr, value);
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600033C RID: 828 RVA: 0x0000C730 File Offset: 0x0000A930
		// (set) Token: 0x0600033D RID: 829 RVA: 0x0000C754 File Offset: 0x0000A954
		[NativeProperty("m_FontSize", false, TargetType.Field)]
		public int fontSize
		{
			get
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GUIStyle.get_fontSize_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				GUIStyle.set_fontSize_Injected(intPtr, value);
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600033E RID: 830 RVA: 0x0000C778 File Offset: 0x0000A978
		// (set) Token: 0x0600033F RID: 831 RVA: 0x0000C79C File Offset: 0x0000A99C
		[NativeProperty("m_FontStyle", false, TargetType.Field)]
		public FontStyle fontStyle
		{
			get
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GUIStyle.get_fontStyle_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				GUIStyle.set_fontStyle_Injected(intPtr, value);
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0000C7C0 File Offset: 0x0000A9C0
		// (set) Token: 0x06000341 RID: 833 RVA: 0x0000C7E4 File Offset: 0x0000A9E4
		[NativeProperty("m_RichText", false, TargetType.Field)]
		public bool richText
		{
			get
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GUIStyle.get_richText_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				GUIStyle.set_richText_Injected(intPtr, value);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000342 RID: 834 RVA: 0x0000C808 File Offset: 0x0000AA08
		// (set) Token: 0x06000343 RID: 835 RVA: 0x0000C82C File Offset: 0x0000AA2C
		[NativeProperty("m_IsSDF", false, TargetType.Field)]
		internal bool isSDF
		{
			get
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GUIStyle.get_isSDF_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				GUIStyle.set_isSDF_Injected(intPtr, value);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000344 RID: 836 RVA: 0x0000C850 File Offset: 0x0000AA50
		// (set) Token: 0x06000345 RID: 837 RVA: 0x0000C878 File Offset: 0x0000AA78
		[Obsolete("Don't use clipOffset - put things inside BeginGroup instead. This functionality will be removed in a later version.", false)]
		[NativeProperty("m_ClipOffset", false, TargetType.Field)]
		public Vector2 clipOffset
		{
			get
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				GUIStyle.get_clipOffset_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				GUIStyle.set_clipOffset_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000346 RID: 838 RVA: 0x0000C89C File Offset: 0x0000AA9C
		// (set) Token: 0x06000347 RID: 839 RVA: 0x0000C8C4 File Offset: 0x0000AAC4
		[NativeProperty("m_ClipOffset", false, TargetType.Field)]
		internal Vector2 Internal_clipOffset
		{
			get
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				GUIStyle.get_Internal_clipOffset_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				GUIStyle.set_Internal_clipOffset_Injected(intPtr, ref value);
			}
		}

		// Token: 0x06000348 RID: 840
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_Create", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_Create([Unmarshalled] GUIStyle self);

		// Token: 0x06000349 RID: 841 RVA: 0x0000C8E8 File Offset: 0x0000AAE8
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_Copy", IsThreadSafe = true)]
		private static IntPtr Internal_Copy([Unmarshalled] GUIStyle self, GUIStyle other)
		{
			return GUIStyle.Internal_Copy_Injected(self, (other == null) ? ((IntPtr)0) : GUIStyle.BindingsMarshaller.ConvertToNative(other));
		}

		// Token: 0x0600034A RID: 842
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_Destroy", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Destroy(IntPtr self);

		// Token: 0x0600034B RID: 843 RVA: 0x0000C90C File Offset: 0x0000AB0C
		[FreeFunction(Name = "GUIStyle_Bindings::GetStyleStatePtr", IsThreadSafe = true, HasExplicitThis = true)]
		private IntPtr GetStyleStatePtr(int idx)
		{
			IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return GUIStyle.GetStyleStatePtr_Injected(intPtr, idx);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000C930 File Offset: 0x0000AB30
		[FreeFunction(Name = "GUIStyle_Bindings::AssignStyleState", HasExplicitThis = true)]
		private void AssignStyleState(int idx, IntPtr srcStyleState)
		{
			IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			GUIStyle.AssignStyleState_Injected(intPtr, idx, srcStyleState);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000C954 File Offset: 0x0000AB54
		[FreeFunction(Name = "GUIStyle_Bindings::GetRectOffsetPtr", HasExplicitThis = true)]
		private IntPtr GetRectOffsetPtr(int idx)
		{
			IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return GUIStyle.GetRectOffsetPtr_Injected(intPtr, idx);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000C978 File Offset: 0x0000AB78
		[FreeFunction(Name = "GUIStyle_Bindings::AssignRectOffset", HasExplicitThis = true)]
		private void AssignRectOffset(int idx, IntPtr srcRectOffset)
		{
			IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			GUIStyle.AssignRectOffset_Injected(intPtr, idx, srcRectOffset);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000C99C File Offset: 0x0000AB9C
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_Draw", HasExplicitThis = true)]
		private void Internal_Draw(Rect screenRect, GUIContent content, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			GUIStyle.Internal_Draw_Injected(intPtr, ref screenRect, content, isHover, isActive, on, hasKeyboardFocus);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000C9C8 File Offset: 0x0000ABC8
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_Draw2", HasExplicitThis = true)]
		private void Internal_Draw2(Rect position, GUIContent content, int controlID, bool on)
		{
			IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			GUIStyle.Internal_Draw2_Injected(intPtr, ref position, content, controlID, on);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000C9F0 File Offset: 0x0000ABF0
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_DrawCursor", HasExplicitThis = true)]
		private void Internal_DrawCursor(Rect position, GUIContent content, Vector2 pos, Color cursorColor)
		{
			IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			GUIStyle.Internal_DrawCursor_Injected(intPtr, ref position, content, ref pos, ref cursorColor);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000CA1C File Offset: 0x0000AC1C
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_DrawWithTextSelection", HasExplicitThis = true)]
		private void Internal_DrawWithTextSelection(Rect screenRect, GUIContent content, bool isHover, bool isActive, bool on, bool hasKeyboardFocus, bool drawSelectionAsComposition, Vector2 cursorFirstPosition, Vector2 cursorLastPosition, Color cursorColor, Color selectionColor)
		{
			IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			GUIStyle.Internal_DrawWithTextSelection_Injected(intPtr, ref screenRect, content, isHover, isActive, on, hasKeyboardFocus, drawSelectionAsComposition, ref cursorFirstPosition, ref cursorLastPosition, ref cursorColor, ref selectionColor);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000CA54 File Offset: 0x0000AC54
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_CalcSize", HasExplicitThis = true)]
		internal Vector2 Internal_CalcSize(GUIContent content)
		{
			IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector2 vector;
			GUIStyle.Internal_CalcSize_Injected(intPtr, content, out vector);
			return vector;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000CA7C File Offset: 0x0000AC7C
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_CalcSizeWithConstraints", HasExplicitThis = true)]
		internal Vector2 Internal_CalcSizeWithConstraints(GUIContent content, Vector2 maxSize)
		{
			IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector2 vector;
			GUIStyle.Internal_CalcSizeWithConstraints_Injected(intPtr, content, ref maxSize, out vector);
			return vector;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000CAA4 File Offset: 0x0000ACA4
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_CalcHeight", HasExplicitThis = true)]
		private float Internal_CalcHeight(GUIContent content, float width)
		{
			IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return GUIStyle.Internal_CalcHeight_Injected(intPtr, content, width);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000CAC8 File Offset: 0x0000ACC8
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_CalcMinMaxWidth", HasExplicitThis = true)]
		private Vector2 Internal_CalcMinMaxWidth(GUIContent content)
		{
			IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector2 vector;
			GUIStyle.Internal_CalcMinMaxWidth_Injected(intPtr, content, out vector);
			return vector;
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000CAF0 File Offset: 0x0000ACF0
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_GetTextRectOffset", HasExplicitThis = true)]
		internal Vector2 Internal_GetTextRectOffset(Rect screenRect, GUIContent content, Vector2 textSize)
		{
			IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector2 vector;
			GUIStyle.Internal_GetTextRectOffset_Injected(intPtr, ref screenRect, content, ref textSize, out vector);
			return vector;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000CB1C File Offset: 0x0000AD1C
		[FreeFunction(Name = "GUIStyle_Bindings::SetMouseTooltip")]
		internal unsafe static void SetMouseTooltip(string tooltip, Rect screenRect)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(tooltip, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = tooltip.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				GUIStyle.SetMouseTooltip_Injected(ref managedSpanWrapper, ref screenRect);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000CB74 File Offset: 0x0000AD74
		[FreeFunction(Name = "GUIStyle_Bindings::IsTooltipActive")]
		internal unsafe static bool IsTooltipActive(string tooltip)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(tooltip, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = tooltip.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = GUIStyle.IsTooltipActive_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x0600035A RID: 858
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_GetCursorFlashOffset")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float Internal_GetCursorFlashOffset();

		// Token: 0x0600035B RID: 859 RVA: 0x0000CBCC File Offset: 0x0000ADCC
		[FreeFunction(Name = "GUIStyle::SetDefaultFont")]
		internal static void SetDefaultFont(Font font)
		{
			GUIStyle.SetDefaultFont_Injected(Object.MarshalledUnityObject.Marshal<Font>(font));
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000CBE4 File Offset: 0x0000ADE4
		[FreeFunction(Name = "GUIStyle::GetDefaultFont")]
		internal static Font GetDefaultFont()
		{
			return Unmarshal.UnmarshalUnityObject<Font>(GUIStyle.GetDefaultFont_Injected());
		}

		// Token: 0x0600035D RID: 861
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_DestroyTextGenerator")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_DestroyTextGenerator(int meshInfoId);

		// Token: 0x0600035E RID: 862
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_CleanupAllTextGenerator")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_CleanupAllTextGenerator();

		// Token: 0x0600035F RID: 863 RVA: 0x0000CBFB File Offset: 0x0000ADFB
		public GUIStyle()
		{
			this.m_Ptr = GUIStyle.Internal_Create(this);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000CC14 File Offset: 0x0000AE14
		public GUIStyle(GUIStyle other)
		{
			bool flag = other == null;
			if (flag)
			{
				Debug.LogError("Copied style is null. Using StyleNotFound instead.");
				other = GUISkin.error;
			}
			this.m_Ptr = GUIStyle.Internal_Copy(this, other);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000CC54 File Offset: 0x0000AE54
		protected override void Finalize()
		{
			try
			{
				bool flag = this.m_Ptr != IntPtr.Zero;
				if (flag)
				{
					GUIStyle.Internal_Destroy(this.m_Ptr);
					this.m_Ptr = IntPtr.Zero;
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000CCAC File Offset: 0x0000AEAC
		internal static void CleanupRoots()
		{
			GUIStyle.s_None = null;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000CCB8 File Offset: 0x0000AEB8
		internal void InternalOnAfterDeserialize()
		{
			this.m_Normal = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(0));
			this.m_Hover = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(1));
			this.m_Active = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(2));
			this.m_Focused = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(3));
			this.m_OnNormal = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(4));
			this.m_OnHover = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(5));
			this.m_OnActive = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(6));
			this.m_OnFocused = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(7));
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000364 RID: 868 RVA: 0x0000CD60 File Offset: 0x0000AF60
		// (set) Token: 0x06000365 RID: 869 RVA: 0x0000CD8B File Offset: 0x0000AF8B
		public string name
		{
			get
			{
				string text;
				if ((text = this.m_Name) == null)
				{
					text = (this.m_Name = this.rawName);
				}
				return text;
			}
			set
			{
				this.m_Name = value;
				this.rawName = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000366 RID: 870 RVA: 0x0000CDA0 File Offset: 0x0000AFA0
		// (set) Token: 0x06000367 RID: 871 RVA: 0x0000CDD2 File Offset: 0x0000AFD2
		public GUIStyleState normal
		{
			get
			{
				GUIStyleState guistyleState;
				if ((guistyleState = this.m_Normal) == null)
				{
					guistyleState = (this.m_Normal = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(0)));
				}
				return guistyleState;
			}
			set
			{
				this.AssignStyleState(0, value.m_Ptr);
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000368 RID: 872 RVA: 0x0000CDE4 File Offset: 0x0000AFE4
		// (set) Token: 0x06000369 RID: 873 RVA: 0x0000CE16 File Offset: 0x0000B016
		public GUIStyleState hover
		{
			get
			{
				GUIStyleState guistyleState;
				if ((guistyleState = this.m_Hover) == null)
				{
					guistyleState = (this.m_Hover = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(1)));
				}
				return guistyleState;
			}
			set
			{
				this.AssignStyleState(1, value.m_Ptr);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600036A RID: 874 RVA: 0x0000CE28 File Offset: 0x0000B028
		// (set) Token: 0x0600036B RID: 875 RVA: 0x0000CE5A File Offset: 0x0000B05A
		public GUIStyleState active
		{
			get
			{
				GUIStyleState guistyleState;
				if ((guistyleState = this.m_Active) == null)
				{
					guistyleState = (this.m_Active = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(2)));
				}
				return guistyleState;
			}
			set
			{
				this.AssignStyleState(2, value.m_Ptr);
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600036C RID: 876 RVA: 0x0000CE6C File Offset: 0x0000B06C
		// (set) Token: 0x0600036D RID: 877 RVA: 0x0000CE9E File Offset: 0x0000B09E
		public GUIStyleState onNormal
		{
			get
			{
				GUIStyleState guistyleState;
				if ((guistyleState = this.m_OnNormal) == null)
				{
					guistyleState = (this.m_OnNormal = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(4)));
				}
				return guistyleState;
			}
			set
			{
				this.AssignStyleState(4, value.m_Ptr);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600036E RID: 878 RVA: 0x0000CEB0 File Offset: 0x0000B0B0
		// (set) Token: 0x0600036F RID: 879 RVA: 0x0000CEE2 File Offset: 0x0000B0E2
		public GUIStyleState onHover
		{
			get
			{
				GUIStyleState guistyleState;
				if ((guistyleState = this.m_OnHover) == null)
				{
					guistyleState = (this.m_OnHover = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(5)));
				}
				return guistyleState;
			}
			set
			{
				this.AssignStyleState(5, value.m_Ptr);
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000370 RID: 880 RVA: 0x0000CEF4 File Offset: 0x0000B0F4
		// (set) Token: 0x06000371 RID: 881 RVA: 0x0000CF26 File Offset: 0x0000B126
		public GUIStyleState onActive
		{
			get
			{
				GUIStyleState guistyleState;
				if ((guistyleState = this.m_OnActive) == null)
				{
					guistyleState = (this.m_OnActive = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(6)));
				}
				return guistyleState;
			}
			set
			{
				this.AssignStyleState(6, value.m_Ptr);
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000372 RID: 882 RVA: 0x0000CF38 File Offset: 0x0000B138
		// (set) Token: 0x06000373 RID: 883 RVA: 0x0000CF6A File Offset: 0x0000B16A
		public GUIStyleState focused
		{
			get
			{
				GUIStyleState guistyleState;
				if ((guistyleState = this.m_Focused) == null)
				{
					guistyleState = (this.m_Focused = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(3)));
				}
				return guistyleState;
			}
			set
			{
				this.AssignStyleState(3, value.m_Ptr);
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0000CF7C File Offset: 0x0000B17C
		// (set) Token: 0x06000375 RID: 885 RVA: 0x0000CFAE File Offset: 0x0000B1AE
		public GUIStyleState onFocused
		{
			get
			{
				GUIStyleState guistyleState;
				if ((guistyleState = this.m_OnFocused) == null)
				{
					guistyleState = (this.m_OnFocused = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(7)));
				}
				return guistyleState;
			}
			set
			{
				this.AssignStyleState(7, value.m_Ptr);
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0000CFC0 File Offset: 0x0000B1C0
		// (set) Token: 0x06000377 RID: 887 RVA: 0x0000CFF2 File Offset: 0x0000B1F2
		public RectOffset border
		{
			get
			{
				RectOffset rectOffset;
				if ((rectOffset = this.m_Border) == null)
				{
					rectOffset = (this.m_Border = new RectOffset(this, this.GetRectOffsetPtr(0)));
				}
				return rectOffset;
			}
			set
			{
				this.AssignRectOffset(0, value.m_Ptr);
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000378 RID: 888 RVA: 0x0000D004 File Offset: 0x0000B204
		// (set) Token: 0x06000379 RID: 889 RVA: 0x0000D036 File Offset: 0x0000B236
		public RectOffset margin
		{
			get
			{
				RectOffset rectOffset;
				if ((rectOffset = this.m_Margin) == null)
				{
					rectOffset = (this.m_Margin = new RectOffset(this, this.GetRectOffsetPtr(1)));
				}
				return rectOffset;
			}
			set
			{
				this.AssignRectOffset(1, value.m_Ptr);
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600037A RID: 890 RVA: 0x0000D048 File Offset: 0x0000B248
		// (set) Token: 0x0600037B RID: 891 RVA: 0x0000D07A File Offset: 0x0000B27A
		public RectOffset padding
		{
			get
			{
				RectOffset rectOffset;
				if ((rectOffset = this.m_Padding) == null)
				{
					rectOffset = (this.m_Padding = new RectOffset(this, this.GetRectOffsetPtr(2)));
				}
				return rectOffset;
			}
			set
			{
				this.AssignRectOffset(2, value.m_Ptr);
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600037C RID: 892 RVA: 0x0000D08C File Offset: 0x0000B28C
		// (set) Token: 0x0600037D RID: 893 RVA: 0x0000D0BE File Offset: 0x0000B2BE
		public RectOffset overflow
		{
			get
			{
				RectOffset rectOffset;
				if ((rectOffset = this.m_Overflow) == null)
				{
					rectOffset = (this.m_Overflow = new RectOffset(this, this.GetRectOffsetPtr(3)));
				}
				return rectOffset;
			}
			set
			{
				this.AssignRectOffset(3, value.m_Ptr);
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600037E RID: 894 RVA: 0x0000D0CF File Offset: 0x0000B2CF
		public float lineHeight
		{
			get
			{
				return Mathf.Round(IMGUITextHandle.GetLineHeight(this));
			}
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000D0DC File Offset: 0x0000B2DC
		public void Draw(Rect position, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			this.Draw(position, GUIContent.none, -1, isHover, isActive, on, hasKeyboardFocus);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000D0F3 File Offset: 0x0000B2F3
		public void Draw(Rect position, string text, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			this.Draw(position, GUIContent.Temp(text), -1, isHover, isActive, on, hasKeyboardFocus);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000D10C File Offset: 0x0000B30C
		public void Draw(Rect position, Texture image, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			this.Draw(position, GUIContent.Temp(image), -1, isHover, isActive, on, hasKeyboardFocus);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000D125 File Offset: 0x0000B325
		public void Draw(Rect position, GUIContent content, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			this.Draw(position, content, -1, isHover, isActive, on, hasKeyboardFocus);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000D139 File Offset: 0x0000B339
		public void Draw(Rect position, GUIContent content, int controlID)
		{
			this.Draw(position, content, controlID, false, false, false, false);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000D14A File Offset: 0x0000B34A
		public void Draw(Rect position, GUIContent content, int controlID, bool on)
		{
			this.Draw(position, content, controlID, false, false, on, false);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000D15C File Offset: 0x0000B35C
		public void Draw(Rect position, GUIContent content, int controlID, bool on, bool hover)
		{
			this.Draw(position, content, controlID, hover, GUIUtility.hotControl == controlID, on, GUIUtility.HasKeyFocus(controlID));
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000D17C File Offset: 0x0000B37C
		private void Draw(Rect position, GUIContent content, int controlId, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			bool flag = controlId == -1;
			if (flag)
			{
				this.Internal_Draw(position, content, isHover, isActive, on, hasKeyboardFocus);
			}
			else
			{
				this.Internal_Draw2(position, content, controlId, on);
			}
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000D1B4 File Offset: 0x0000B3B4
		public void DrawCursor(Rect position, GUIContent content, int controlID, int character)
		{
			Event current = Event.current;
			bool flag = current.type == EventType.Repaint;
			if (flag)
			{
				Color cursorColor = new Color(0f, 0f, 0f, 0f);
				float cursorFlashSpeed = GUI.skin.settings.cursorFlashSpeed;
				float num = (Time.realtimeSinceStartup - GUIStyle.Internal_GetCursorFlashOffset()) % cursorFlashSpeed / cursorFlashSpeed;
				bool flag2 = cursorFlashSpeed == 0f || num < 0.5f;
				if (flag2)
				{
					cursorColor = GUI.skin.settings.cursorColor;
				}
				this.Internal_DrawCursor(position, content, this.GetCursorPixelPosition(position, content, character), cursorColor);
			}
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000D254 File Offset: 0x0000B454
		internal void DrawWithTextSelection(Rect position, GUIContent content, bool isActive, bool hasKeyboardFocus, int firstSelectedCharacter, int lastSelectedCharacter, bool drawSelectionAsComposition, Color selectionColor)
		{
			bool flag = firstSelectedCharacter > lastSelectedCharacter;
			if (flag)
			{
				int num = lastSelectedCharacter;
				lastSelectedCharacter = firstSelectedCharacter;
				firstSelectedCharacter = num;
			}
			Vector2 vector = this.GetCursorPixelPosition(position, content, firstSelectedCharacter);
			Vector2 vector2 = this.GetCursorPixelPosition(position, content, lastSelectedCharacter);
			Vector2 vector3 = new Vector2(string.IsNullOrEmpty(content.text) ? 0f : 1f, 0f);
			vector -= vector3;
			vector2 -= vector3;
			Color cursorColor = new Color(0f, 0f, 0f, 0f);
			float cursorFlashSpeed = GUI.skin.settings.cursorFlashSpeed;
			float num2 = (Time.realtimeSinceStartup - GUIStyle.Internal_GetCursorFlashOffset()) % cursorFlashSpeed / cursorFlashSpeed;
			bool flag2 = cursorFlashSpeed == 0f || num2 < 0.5f;
			if (flag2)
			{
				cursorColor = GUI.skin.settings.cursorColor;
			}
			bool flag3 = position.Contains(Event.current.mousePosition);
			this.Internal_DrawWithTextSelection(position, content, flag3, isActive, false, hasKeyboardFocus, drawSelectionAsComposition, vector, vector2, cursorColor, selectionColor);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000D35C File Offset: 0x0000B55C
		internal void DrawWithTextSelection(Rect position, GUIContent content, int controlID, int firstSelectedCharacter, int lastSelectedCharacter, bool drawSelectionAsComposition)
		{
			this.DrawWithTextSelection(position, content, controlID == GUIUtility.hotControl, controlID == GUIUtility.keyboardControl && GUIStyle.showKeyboardFocus, firstSelectedCharacter, lastSelectedCharacter, drawSelectionAsComposition, GUI.skin.settings.selectionColor);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000D3A0 File Offset: 0x0000B5A0
		public void DrawWithTextSelection(Rect position, GUIContent content, int controlID, int firstSelectedCharacter, int lastSelectedCharacter)
		{
			this.DrawWithTextSelection(position, content, controlID, firstSelectedCharacter, lastSelectedCharacter, false);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000D3B4 File Offset: 0x0000B5B4
		public static implicit operator GUIStyle(string str)
		{
			bool flag = GUISkin.current == null;
			GUIStyle guistyle;
			if (flag)
			{
				Debug.LogError("Unable to use a named GUIStyle without a current skin. Most likely you need to move your GUIStyle initialization code to OnGUI");
				guistyle = GUISkin.error;
			}
			else
			{
				guistyle = GUISkin.current.GetStyle(str);
			}
			return guistyle;
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600038C RID: 908 RVA: 0x0000D3F4 File Offset: 0x0000B5F4
		public static GUIStyle none
		{
			get
			{
				GUIStyle guistyle;
				if ((guistyle = GUIStyle.s_None) == null)
				{
					guistyle = (GUIStyle.s_None = new GUIStyle());
				}
				return guistyle;
			}
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000D40C File Offset: 0x0000B60C
		public Vector2 GetCursorPixelPosition(Rect position, GUIContent content, int cursorStringIndex)
		{
			IMGUITextHandle textHandle = IMGUITextHandle.GetTextHandle(this, this.padding.Remove(position), content.textWithWhitespace, Color.white);
			Vector2 cursorPositionFromStringIndexUsingLineHeight = textHandle.GetCursorPositionFromStringIndexUsingLineHeight(cursorStringIndex, false, true);
			cursorPositionFromStringIndexUsingLineHeight = new Vector2(Mathf.Max(0f, cursorPositionFromStringIndexUsingLineHeight.x), cursorPositionFromStringIndexUsingLineHeight.y);
			Vector2 vector = this.Internal_GetTextRectOffset(position, content, new Vector2(textHandle.preferredSize.x, (textHandle.preferredSize.y > 0f) ? textHandle.preferredSize.y : this.lineHeight));
			return cursorPositionFromStringIndexUsingLineHeight + vector + this.Internal_clipOffset - new Vector2(0f, this.lineHeight);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000D4D0 File Offset: 0x0000B6D0
		internal Rect[] GetHyperlinkRects(IMGUITextHandle handle, Rect content)
		{
			content = this.padding.Remove(content);
			return handle.GetHyperlinkRects(content);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000D4F8 File Offset: 0x0000B6F8
		public int GetCursorStringIndex(Rect position, GUIContent content, Vector2 cursorPixelPosition)
		{
			return IMGUITextHandle.GetTextHandle(this, position, content.textWithWhitespace, Color.white).GetCursorIndexFromPosition(cursorPixelPosition, true);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000D528 File Offset: 0x0000B728
		internal int GetNumCharactersThatFitWithinWidth(string text, float width)
		{
			return IMGUITextHandle.GetTextHandle(this, new Rect(0f, 0f, width, 1f), text, Color.white).GetNumCharactersThatFitWithinWidth(width);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000D568 File Offset: 0x0000B768
		public Vector2 CalcSize(GUIContent content)
		{
			return this.Internal_CalcSize(content);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000D584 File Offset: 0x0000B784
		internal Vector2 CalcSizeWithConstraints(GUIContent content, Vector2 constraints)
		{
			Vector2 vector = this.Internal_CalcSizeWithConstraints(content, constraints);
			bool flag = constraints.x > 0f;
			if (flag)
			{
				vector.x = Mathf.Min(vector.x, constraints.x);
			}
			bool flag2 = constraints.y > 0f;
			if (flag2)
			{
				vector.y = Mathf.Min(vector.y, constraints.y);
			}
			return vector;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000D5F4 File Offset: 0x0000B7F4
		public Vector2 CalcScreenSize(Vector2 contentSize)
		{
			return new Vector2((this.fixedWidth != 0f) ? this.fixedWidth : Mathf.Ceil(contentSize.x + (float)this.padding.left + (float)this.padding.right), (this.fixedHeight != 0f) ? this.fixedHeight : Mathf.Ceil(contentSize.y + (float)this.padding.top + (float)this.padding.bottom));
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000D680 File Offset: 0x0000B880
		public float CalcHeight(GUIContent content, float width)
		{
			return this.Internal_CalcHeight(content, width);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000D69C File Offset: 0x0000B89C
		internal Vector2 GetPreferredSize(string content, Rect rect)
		{
			return IMGUITextHandle.GetTextHandle(this, this.padding.Remove(rect), content, Color.white).GetPreferredSize();
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000396 RID: 918 RVA: 0x0000D6D0 File Offset: 0x0000B8D0
		public bool isHeightDependantOnWidth
		{
			get
			{
				return this.fixedHeight == 0f && this.wordWrap && this.imagePosition != ImagePosition.ImageOnly;
			}
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000D6FC File Offset: 0x0000B8FC
		public void CalcMinMaxWidth(GUIContent content, out float minWidth, out float maxWidth)
		{
			Vector2 vector = this.Internal_CalcMinMaxWidth(content);
			minWidth = vector.x;
			maxWidth = vector.y;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000D724 File Offset: 0x0000B924
		public override string ToString()
		{
			return UnityString.Format("GUIStyle '{0}'", new object[] { this.name });
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000D750 File Offset: 0x0000B950
		[RequiredByNativeCode]
		internal static void GetMeshInfo(GUIStyle style, Color color, string content, Rect rect, ref MeshInfoBindings[] meshInfos, ref Vector2 dimensions, ref int generationId)
		{
			bool flag = false;
			IMGUITextHandle textHandle = IMGUITextHandle.GetTextHandle(style, rect, content, color, ref flag);
			generationId = TextHandle.settings.GetHashCode();
			bool flag2 = !flag;
			if (flag2)
			{
				TextInfo textInfo = textHandle.textInfo;
				meshInfos = new MeshInfoBindings[textInfo.materialCount];
				for (int i = 0; i < textInfo.materialCount; i++)
				{
					meshInfos[i].vertexData = new TextCoreVertex[textInfo.meshInfo[i].vertexCount];
					meshInfos[i].vertexCount = textInfo.meshInfo[i].vertexCount;
					meshInfos[i].material = textInfo.meshInfo[i].material;
					Array.Copy(textInfo.meshInfo[i].vertexData, meshInfos[i].vertexData, textInfo.meshInfo[i].vertexCount);
				}
			}
			dimensions = textHandle.preferredSize;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000D871 File Offset: 0x0000BA71
		[RequiredByNativeCode]
		internal static void GetDimensions(GUIStyle style, Color color, string content, Rect rect, ref Vector2 dimensions)
		{
			dimensions = style.GetPreferredSize(content, rect);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000D883 File Offset: 0x0000BA83
		[RequiredByNativeCode]
		internal static void GetLineHeight(GUIStyle style, ref float lineHeight)
		{
			lineHeight = style.lineHeight;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000D88E File Offset: 0x0000BA8E
		[RequiredByNativeCode]
		internal static void EmptyManagedCache()
		{
			IMGUITextHandle.EmptyManagedCache();
		}

		// Token: 0x0600039E RID: 926
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_rawName_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x0600039F RID: 927
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_rawName_Injected(IntPtr _unity_self, ref ManagedSpanWrapper value);

		// Token: 0x060003A0 RID: 928
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_font_Injected(IntPtr _unity_self);

		// Token: 0x060003A1 RID: 929
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_font_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x060003A2 RID: 930
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ImagePosition get_imagePosition_Injected(IntPtr _unity_self);

		// Token: 0x060003A3 RID: 931
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_imagePosition_Injected(IntPtr _unity_self, ImagePosition value);

		// Token: 0x060003A4 RID: 932
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TextAnchor get_alignment_Injected(IntPtr _unity_self);

		// Token: 0x060003A5 RID: 933
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_alignment_Injected(IntPtr _unity_self, TextAnchor value);

		// Token: 0x060003A6 RID: 934
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_wordWrap_Injected(IntPtr _unity_self);

		// Token: 0x060003A7 RID: 935
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_wordWrap_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060003A8 RID: 936
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TextClipping get_clipping_Injected(IntPtr _unity_self);

		// Token: 0x060003A9 RID: 937
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_clipping_Injected(IntPtr _unity_self, TextClipping value);

		// Token: 0x060003AA RID: 938
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_contentOffset_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x060003AB RID: 939
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_contentOffset_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x060003AC RID: 940
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_fixedWidth_Injected(IntPtr _unity_self);

		// Token: 0x060003AD RID: 941
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_fixedWidth_Injected(IntPtr _unity_self, float value);

		// Token: 0x060003AE RID: 942
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_fixedHeight_Injected(IntPtr _unity_self);

		// Token: 0x060003AF RID: 943
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_fixedHeight_Injected(IntPtr _unity_self, float value);

		// Token: 0x060003B0 RID: 944
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_stretchWidth_Injected(IntPtr _unity_self);

		// Token: 0x060003B1 RID: 945
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_stretchWidth_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060003B2 RID: 946
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_stretchHeight_Injected(IntPtr _unity_self);

		// Token: 0x060003B3 RID: 947
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_stretchHeight_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060003B4 RID: 948
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_fontSize_Injected(IntPtr _unity_self);

		// Token: 0x060003B5 RID: 949
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_fontSize_Injected(IntPtr _unity_self, int value);

		// Token: 0x060003B6 RID: 950
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern FontStyle get_fontStyle_Injected(IntPtr _unity_self);

		// Token: 0x060003B7 RID: 951
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_fontStyle_Injected(IntPtr _unity_self, FontStyle value);

		// Token: 0x060003B8 RID: 952
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_richText_Injected(IntPtr _unity_self);

		// Token: 0x060003B9 RID: 953
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_richText_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060003BA RID: 954
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isSDF_Injected(IntPtr _unity_self);

		// Token: 0x060003BB RID: 955
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_isSDF_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060003BC RID: 956
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_clipOffset_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x060003BD RID: 957
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_clipOffset_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x060003BE RID: 958
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_Internal_clipOffset_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x060003BF RID: 959
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_Internal_clipOffset_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x060003C0 RID: 960
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_Copy_Injected(GUIStyle self, IntPtr other);

		// Token: 0x060003C1 RID: 961
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetStyleStatePtr_Injected(IntPtr _unity_self, int idx);

		// Token: 0x060003C2 RID: 962
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AssignStyleState_Injected(IntPtr _unity_self, int idx, IntPtr srcStyleState);

		// Token: 0x060003C3 RID: 963
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetRectOffsetPtr_Injected(IntPtr _unity_self, int idx);

		// Token: 0x060003C4 RID: 964
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AssignRectOffset_Injected(IntPtr _unity_self, int idx, IntPtr srcRectOffset);

		// Token: 0x060003C5 RID: 965
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Draw_Injected(IntPtr _unity_self, [In] ref Rect screenRect, GUIContent content, bool isHover, bool isActive, bool on, bool hasKeyboardFocus);

		// Token: 0x060003C6 RID: 966
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Draw2_Injected(IntPtr _unity_self, [In] ref Rect position, GUIContent content, int controlID, bool on);

		// Token: 0x060003C7 RID: 967
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawCursor_Injected(IntPtr _unity_self, [In] ref Rect position, GUIContent content, [In] ref Vector2 pos, [In] ref Color cursorColor);

		// Token: 0x060003C8 RID: 968
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawWithTextSelection_Injected(IntPtr _unity_self, [In] ref Rect screenRect, GUIContent content, bool isHover, bool isActive, bool on, bool hasKeyboardFocus, bool drawSelectionAsComposition, [In] ref Vector2 cursorFirstPosition, [In] ref Vector2 cursorLastPosition, [In] ref Color cursorColor, [In] ref Color selectionColor);

		// Token: 0x060003C9 RID: 969
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CalcSize_Injected(IntPtr _unity_self, GUIContent content, out Vector2 ret);

		// Token: 0x060003CA RID: 970
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CalcSizeWithConstraints_Injected(IntPtr _unity_self, GUIContent content, [In] ref Vector2 maxSize, out Vector2 ret);

		// Token: 0x060003CB RID: 971
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float Internal_CalcHeight_Injected(IntPtr _unity_self, GUIContent content, float width);

		// Token: 0x060003CC RID: 972
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CalcMinMaxWidth_Injected(IntPtr _unity_self, GUIContent content, out Vector2 ret);

		// Token: 0x060003CD RID: 973
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetTextRectOffset_Injected(IntPtr _unity_self, [In] ref Rect screenRect, GUIContent content, [In] ref Vector2 textSize, out Vector2 ret);

		// Token: 0x060003CE RID: 974
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMouseTooltip_Injected(ref ManagedSpanWrapper tooltip, [In] ref Rect screenRect);

		// Token: 0x060003CF RID: 975
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsTooltipActive_Injected(ref ManagedSpanWrapper tooltip);

		// Token: 0x060003D0 RID: 976
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetDefaultFont_Injected(IntPtr font);

		// Token: 0x060003D1 RID: 977
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetDefaultFont_Injected();

		// Token: 0x040000D0 RID: 208
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x040000D1 RID: 209
		[NonSerialized]
		private GUIStyleState m_Normal;

		// Token: 0x040000D2 RID: 210
		[NonSerialized]
		private GUIStyleState m_Hover;

		// Token: 0x040000D3 RID: 211
		[NonSerialized]
		private GUIStyleState m_Active;

		// Token: 0x040000D4 RID: 212
		[NonSerialized]
		private GUIStyleState m_Focused;

		// Token: 0x040000D5 RID: 213
		[NonSerialized]
		private GUIStyleState m_OnNormal;

		// Token: 0x040000D6 RID: 214
		[NonSerialized]
		private GUIStyleState m_OnHover;

		// Token: 0x040000D7 RID: 215
		[NonSerialized]
		private GUIStyleState m_OnActive;

		// Token: 0x040000D8 RID: 216
		[NonSerialized]
		private GUIStyleState m_OnFocused;

		// Token: 0x040000D9 RID: 217
		[NonSerialized]
		private RectOffset m_Border;

		// Token: 0x040000DA RID: 218
		[NonSerialized]
		private RectOffset m_Padding;

		// Token: 0x040000DB RID: 219
		[NonSerialized]
		private RectOffset m_Margin;

		// Token: 0x040000DC RID: 220
		[NonSerialized]
		private RectOffset m_Overflow;

		// Token: 0x040000DD RID: 221
		[NonSerialized]
		private string m_Name;

		// Token: 0x040000DE RID: 222
		internal static bool showKeyboardFocus = true;

		// Token: 0x040000DF RID: 223
		private static GUIStyle s_None;

		// Token: 0x0200002E RID: 46
		internal static class BindingsMarshaller
		{
			// Token: 0x060003D2 RID: 978 RVA: 0x0000D89F File Offset: 0x0000BA9F
			public static IntPtr ConvertToNative(GUIStyle guiStyle)
			{
				return guiStyle.m_Ptr;
			}
		}
	}
}
