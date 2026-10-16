using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000012 RID: 18
	[StaticAccessor("TextRenderingPrivate", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/TextRendering/Public/FontImpl.h")]
	[NativeHeader("Modules/TextRendering/Public/Font.h")]
	[NativeClass("TextRendering::Font")]
	public sealed class Font : Object
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600009F RID: 159 RVA: 0x000035C0 File Offset: 0x000017C0
		// (remove) Token: 0x060000A0 RID: 160 RVA: 0x000035F4 File Offset: 0x000017F4
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<Font> textureRebuilt;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060000A1 RID: 161 RVA: 0x00003628 File Offset: 0x00001828
		// (remove) Token: 0x060000A2 RID: 162 RVA: 0x00003660 File Offset: 0x00001860
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private event Font.FontTextureRebuildCallback m_FontTextureRebuildCallback;

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00003698 File Offset: 0x00001898
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x000036C0 File Offset: 0x000018C0
		public Material material
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Font>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Material>(Font.get_material_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Font>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Font.set_material_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Material>(value));
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x000036E8 File Offset: 0x000018E8
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x0000370C File Offset: 0x0000190C
		public string[] fontNames
		{
			[return: Unmarshalled]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Font>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Font.get_fontNames_Injected(intPtr);
			}
			[param: Unmarshalled]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Font>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Font.set_fontNames_Injected(intPtr, value);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00003730 File Offset: 0x00001930
		public bool dynamic
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Font>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Font.get_dynamic_Injected(intPtr);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00003754 File Offset: 0x00001954
		public int ascent
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Font>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Font.get_ascent_Injected(intPtr);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00003778 File Offset: 0x00001978
		public int fontSize
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Font>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Font.get_fontSize_Injected(intPtr);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000AA RID: 170 RVA: 0x0000379C File Offset: 0x0000199C
		// (set) Token: 0x060000AB RID: 171 RVA: 0x000037E0 File Offset: 0x000019E0
		public unsafe CharacterInfo[] characterInfo
		{
			[FreeFunction("TextRenderingPrivate::GetFontCharacterInfo", HasExplicitThis = true)]
			get
			{
				CharacterInfo[] array2;
				try
				{
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Font>(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					BlittableArrayWrapper blittableArrayWrapper;
					Font.get_characterInfo_Injected(intPtr, out blittableArrayWrapper);
				}
				finally
				{
					BlittableArrayWrapper blittableArrayWrapper;
					CharacterInfo[] array;
					blittableArrayWrapper.Unmarshal<CharacterInfo>(ref array);
					array2 = array;
				}
				return array2;
			}
			[FreeFunction("TextRenderingPrivate::SetFontCharacterInfo", HasExplicitThis = true)]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Font>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Span<CharacterInfo> span = new Span<CharacterInfo>(value);
				fixed (CharacterInfo* pinnableReference = span.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
					Font.set_characterInfo_Injected(intPtr, ref managedSpanWrapper);
				}
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00003828 File Offset: 0x00001A28
		[NativeProperty("LineSpacing", false, TargetType.Function)]
		public int lineHeight
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Font>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Font.get_lineHeight_Injected(intPtr);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000AD RID: 173 RVA: 0x0000384C File Offset: 0x00001A4C
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00003864 File Offset: 0x00001A64
		[Obsolete("Font.textureRebuildCallback has been deprecated. Use Font.textureRebuilt instead.")]
		public Font.FontTextureRebuildCallback textureRebuildCallback
		{
			get
			{
				return this.m_FontTextureRebuildCallback;
			}
			set
			{
				this.m_FontTextureRebuildCallback = value;
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000386E File Offset: 0x00001A6E
		public Font()
		{
			Font.Internal_CreateFont(this, null);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003880 File Offset: 0x00001A80
		public Font(string name)
		{
			bool flag = Path.GetDirectoryName(name) == string.Empty;
			bool flag2 = flag;
			if (flag2)
			{
				Font.Internal_CreateFont(this, name);
			}
			else
			{
				Font.Internal_CreateFontFromPath(this, name);
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000038BD File Offset: 0x00001ABD
		private Font(string[] names, int size)
		{
			Font.Internal_CreateDynamicFont(this, names, size);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000038D0 File Offset: 0x00001AD0
		public static Font CreateDynamicFontFromOSFont(string fontname, int size)
		{
			return new Font(new string[] { fontname }, size);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000038F4 File Offset: 0x00001AF4
		public static Font CreateDynamicFontFromOSFont(string[] fontnames, int size)
		{
			return new Font(fontnames, size);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000390D File Offset: 0x00001B0D
		[RequiredByNativeCode]
		internal static void InvokeTextureRebuilt_Internal(Font font)
		{
			Action<Font> action = Font.textureRebuilt;
			if (action != null)
			{
				action(font);
			}
			Font.FontTextureRebuildCallback fontTextureRebuildCallback = font.m_FontTextureRebuildCallback;
			if (fontTextureRebuildCallback != null)
			{
				fontTextureRebuildCallback();
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003934 File Offset: 0x00001B34
		public static int GetMaxVertsForString(string str)
		{
			return str.Length * 4 + 4;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003950 File Offset: 0x00001B50
		internal static Font GetDefault()
		{
			return Unmarshal.UnmarshalUnityObject<Font>(Font.GetDefault_Injected());
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003968 File Offset: 0x00001B68
		public bool HasCharacter(char c)
		{
			return this.HasCharacter((int)c);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003984 File Offset: 0x00001B84
		private bool HasCharacter(int c)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Font>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Font.HasCharacter_Injected(intPtr, c);
		}

		// Token: 0x060000B9 RID: 185
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string[] GetOSInstalledFontNames();

		// Token: 0x060000BA RID: 186
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string[] GetPathsToOSFonts();

		// Token: 0x060000BB RID: 187
		[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string[] GetOSFallbacks();

		// Token: 0x060000BC RID: 188 RVA: 0x000039A8 File Offset: 0x00001BA8
		private unsafe static void Internal_CreateFont([Writable] Font self, string name)
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
				Font.Internal_CreateFont_Injected(self, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003A00 File Offset: 0x00001C00
		private unsafe static void Internal_CreateFontFromPath([Writable] Font self, string fontPath)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(fontPath, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = fontPath.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Font.Internal_CreateFontFromPath_Injected(self, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x060000BE RID: 190
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateDynamicFont([Writable] Font self, [Unmarshalled] string[] _names, int size);

		// Token: 0x060000BF RID: 191 RVA: 0x00003A58 File Offset: 0x00001C58
		[FreeFunction("TextRenderingPrivate::GetCharacterInfo", HasExplicitThis = true)]
		public bool GetCharacterInfo(char ch, out CharacterInfo info, [DefaultValue("0")] int size, [DefaultValue("FontStyle.Normal")] FontStyle style)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Font>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Font.GetCharacterInfo_Injected(intPtr, ch, out info, size, style);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003A80 File Offset: 0x00001C80
		[ExcludeFromDocs]
		public bool GetCharacterInfo(char ch, out CharacterInfo info, int size)
		{
			return this.GetCharacterInfo(ch, out info, size, FontStyle.Normal);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003A9C File Offset: 0x00001C9C
		[ExcludeFromDocs]
		public bool GetCharacterInfo(char ch, out CharacterInfo info)
		{
			return this.GetCharacterInfo(ch, out info, 0, FontStyle.Normal);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003AB8 File Offset: 0x00001CB8
		public unsafe void RequestCharactersInTexture(string characters, [DefaultValue("0")] int size, [DefaultValue("FontStyle.Normal")] FontStyle style)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Font>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(characters, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = characters.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Font.RequestCharactersInTexture_Injected(intPtr, ref managedSpanWrapper, size, style);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003B20 File Offset: 0x00001D20
		[ExcludeFromDocs]
		public void RequestCharactersInTexture(string characters, int size)
		{
			this.RequestCharactersInTexture(characters, size, FontStyle.Normal);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003B2D File Offset: 0x00001D2D
		[ExcludeFromDocs]
		public void RequestCharactersInTexture(string characters)
		{
			this.RequestCharactersInTexture(characters, 0, FontStyle.Normal);
		}

		// Token: 0x060000C5 RID: 197
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_material_Injected(IntPtr _unity_self);

		// Token: 0x060000C6 RID: 198
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_material_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x060000C7 RID: 199
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] get_fontNames_Injected(IntPtr _unity_self);

		// Token: 0x060000C8 RID: 200
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_fontNames_Injected(IntPtr _unity_self, string[] value);

		// Token: 0x060000C9 RID: 201
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_dynamic_Injected(IntPtr _unity_self);

		// Token: 0x060000CA RID: 202
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_ascent_Injected(IntPtr _unity_self);

		// Token: 0x060000CB RID: 203
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_fontSize_Injected(IntPtr _unity_self);

		// Token: 0x060000CC RID: 204
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_characterInfo_Injected(IntPtr _unity_self, out BlittableArrayWrapper ret);

		// Token: 0x060000CD RID: 205
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_characterInfo_Injected(IntPtr _unity_self, ref ManagedSpanWrapper value);

		// Token: 0x060000CE RID: 206
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_lineHeight_Injected(IntPtr _unity_self);

		// Token: 0x060000CF RID: 207
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetDefault_Injected();

		// Token: 0x060000D0 RID: 208
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasCharacter_Injected(IntPtr _unity_self, int c);

		// Token: 0x060000D1 RID: 209
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateFont_Injected([Writable] Font self, ref ManagedSpanWrapper name);

		// Token: 0x060000D2 RID: 210
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateFontFromPath_Injected([Writable] Font self, ref ManagedSpanWrapper fontPath);

		// Token: 0x060000D3 RID: 211
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetCharacterInfo_Injected(IntPtr _unity_self, char ch, out CharacterInfo info, [DefaultValue("0")] int size, [DefaultValue("FontStyle.Normal")] FontStyle style);

		// Token: 0x060000D4 RID: 212
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RequestCharactersInTexture_Injected(IntPtr _unity_self, ref ManagedSpanWrapper characters, [DefaultValue("0")] int size, [DefaultValue("FontStyle.Normal")] FontStyle style);

		// Token: 0x02000013 RID: 19
		// (Invoke) Token: 0x060000D6 RID: 214
		public delegate void FontTextureRebuildCallback();
	}
}
