using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x0200000E RID: 14
	[NativeHeader("Modules/TextCoreFontEngine/Native/FontEngine.h")]
	public sealed class FontEngine
	{
		// Token: 0x06000060 RID: 96 RVA: 0x00002C1D File Offset: 0x00000E1D
		internal FontEngine()
		{
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002C28 File Offset: 0x00000E28
		public static FontEngineError InitializeFontEngine()
		{
			return (FontEngineError)FontEngine.InitializeFontEngine_Internal();
		}

		// Token: 0x06000062 RID: 98
		[NativeMethod(Name = "TextCore::FontEngine::InitFontEngine", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int InitializeFontEngine_Internal();

		// Token: 0x06000063 RID: 99 RVA: 0x00002C40 File Offset: 0x00000E40
		public static FontEngineError DestroyFontEngine()
		{
			return (FontEngineError)FontEngine.DestroyFontEngine_Internal();
		}

		// Token: 0x06000064 RID: 100
		[NativeMethod(Name = "TextCore::FontEngine::DestroyFontEngine", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int DestroyFontEngine_Internal();

		// Token: 0x06000065 RID: 101 RVA: 0x00002C57 File Offset: 0x00000E57
		internal static void SendCancellationRequest()
		{
			FontEngine.SendCancellationRequest_Internal();
		}

		// Token: 0x06000066 RID: 102
		[NativeMethod(Name = "TextCore::FontEngine::SendCancellationRequest", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SendCancellationRequest_Internal();

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000067 RID: 103
		internal static extern bool isProcessingDone
		{
			[NativeMethod(Name = "TextCore::FontEngine::GetIsProcessingDone", IsFreeFunction = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000068 RID: 104
		internal static extern float generationProgress
		{
			[NativeMethod(Name = "TextCore::FontEngine::GetGenerationProgress", IsFreeFunction = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002C60 File Offset: 0x00000E60
		public static FontEngineError LoadFontFace(string filePath)
		{
			return (FontEngineError)FontEngine.LoadFontFace_Internal(filePath);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002C78 File Offset: 0x00000E78
		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		private unsafe static int LoadFontFace_Internal(string filePath)
		{
			int num;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(filePath, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = filePath.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				num = FontEngine.LoadFontFace_Internal_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return num;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002CD0 File Offset: 0x00000ED0
		public static FontEngineError LoadFontFace(string filePath, int pointSize)
		{
			return (FontEngineError)FontEngine.LoadFontFace_With_Size_Internal(filePath, pointSize);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002CEC File Offset: 0x00000EEC
		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		private unsafe static int LoadFontFace_With_Size_Internal(string filePath, int pointSize)
		{
			int num;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(filePath, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = filePath.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				num = FontEngine.LoadFontFace_With_Size_Internal_Injected(ref managedSpanWrapper, pointSize);
			}
			finally
			{
				char* ptr = null;
			}
			return num;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002D44 File Offset: 0x00000F44
		public static FontEngineError LoadFontFace(string filePath, float pointSize, int faceIndex)
		{
			return (FontEngineError)FontEngine.LoadFontFace_With_Size_And_FaceIndex_Internal(filePath, (int)Math.Round((double)pointSize, MidpointRounding.AwayFromZero), faceIndex);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002D68 File Offset: 0x00000F68
		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		private unsafe static int LoadFontFace_With_Size_And_FaceIndex_Internal(string filePath, int pointSize, int faceIndex)
		{
			int num;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(filePath, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = filePath.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				num = FontEngine.LoadFontFace_With_Size_And_FaceIndex_Internal_Injected(ref managedSpanWrapper, pointSize, faceIndex);
			}
			finally
			{
				char* ptr = null;
			}
			return num;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002DC0 File Offset: 0x00000FC0
		public static FontEngineError LoadFontFace(byte[] sourceFontFile)
		{
			bool flag = sourceFontFile.Length == 0;
			FontEngineError fontEngineError;
			if (flag)
			{
				fontEngineError = FontEngineError.Invalid_File;
			}
			else
			{
				fontEngineError = (FontEngineError)FontEngine.LoadFontFace_FromSourceFontFile_Internal(sourceFontFile);
			}
			return fontEngineError;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002DE8 File Offset: 0x00000FE8
		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		private unsafe static int LoadFontFace_FromSourceFontFile_Internal(byte[] sourceFontFile)
		{
			Span<byte> span = new Span<byte>(sourceFontFile);
			int num;
			fixed (byte* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				num = FontEngine.LoadFontFace_FromSourceFontFile_Internal_Injected(ref managedSpanWrapper);
			}
			return num;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002E20 File Offset: 0x00001020
		public static FontEngineError LoadFontFace(byte[] sourceFontFile, int pointSize)
		{
			bool flag = sourceFontFile.Length == 0;
			FontEngineError fontEngineError;
			if (flag)
			{
				fontEngineError = FontEngineError.Invalid_File;
			}
			else
			{
				fontEngineError = (FontEngineError)FontEngine.LoadFontFace_With_Size_FromSourceFontFile_Internal(sourceFontFile, pointSize);
			}
			return fontEngineError;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002E48 File Offset: 0x00001048
		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		private unsafe static int LoadFontFace_With_Size_FromSourceFontFile_Internal(byte[] sourceFontFile, int pointSize)
		{
			Span<byte> span = new Span<byte>(sourceFontFile);
			int num;
			fixed (byte* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				num = FontEngine.LoadFontFace_With_Size_FromSourceFontFile_Internal_Injected(ref managedSpanWrapper, pointSize);
			}
			return num;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002E84 File Offset: 0x00001084
		public static FontEngineError LoadFontFace(byte[] sourceFontFile, float pointSize, int faceIndex)
		{
			bool flag = sourceFontFile.Length == 0;
			FontEngineError fontEngineError;
			if (flag)
			{
				fontEngineError = FontEngineError.Invalid_File;
			}
			else
			{
				fontEngineError = (FontEngineError)FontEngine.LoadFontFace_With_Size_And_FaceIndex_FromSourceFontFile_Internal(sourceFontFile, (int)Math.Round((double)pointSize, MidpointRounding.AwayFromZero), faceIndex);
			}
			return fontEngineError;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002EB4 File Offset: 0x000010B4
		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		private unsafe static int LoadFontFace_With_Size_And_FaceIndex_FromSourceFontFile_Internal(byte[] sourceFontFile, int pointSize, int faceIndex)
		{
			Span<byte> span = new Span<byte>(sourceFontFile);
			int num;
			fixed (byte* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				num = FontEngine.LoadFontFace_With_Size_And_FaceIndex_FromSourceFontFile_Internal_Injected(ref managedSpanWrapper, pointSize, faceIndex);
			}
			return num;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002EF0 File Offset: 0x000010F0
		public static FontEngineError LoadFontFace(Font font)
		{
			return (FontEngineError)FontEngine.LoadFontFace_FromFont_Internal(font);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002F08 File Offset: 0x00001108
		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		private static int LoadFontFace_FromFont_Internal(Font font)
		{
			return FontEngine.LoadFontFace_FromFont_Internal_Injected(Object.MarshalledUnityObject.Marshal<Font>(font));
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002F20 File Offset: 0x00001120
		public static FontEngineError LoadFontFace(Font font, int pointSize)
		{
			return (FontEngineError)FontEngine.LoadFontFace_With_Size_FromFont_Internal(font, pointSize);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002F3C File Offset: 0x0000113C
		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		private static int LoadFontFace_With_Size_FromFont_Internal(Font font, int pointSize)
		{
			return FontEngine.LoadFontFace_With_Size_FromFont_Internal_Injected(Object.MarshalledUnityObject.Marshal<Font>(font), pointSize);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002F58 File Offset: 0x00001158
		public static FontEngineError LoadFontFace(Font font, float pointSize, int faceIndex)
		{
			return (FontEngineError)FontEngine.LoadFontFace_With_Size_and_FaceIndex_FromFont_Internal(font, (int)Math.Round((double)pointSize, MidpointRounding.AwayFromZero), faceIndex);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002F7C File Offset: 0x0000117C
		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		private static int LoadFontFace_With_Size_and_FaceIndex_FromFont_Internal(Font font, int pointSize, int faceIndex)
		{
			return FontEngine.LoadFontFace_With_Size_and_FaceIndex_FromFont_Internal_Injected(Object.MarshalledUnityObject.Marshal<Font>(font), pointSize, faceIndex);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002F98 File Offset: 0x00001198
		public static FontEngineError LoadFontFace(string familyName, string styleName)
		{
			return (FontEngineError)FontEngine.LoadFontFace_by_FamilyName_and_StyleName_Internal(familyName, styleName);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002FB4 File Offset: 0x000011B4
		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		private unsafe static int LoadFontFace_by_FamilyName_and_StyleName_Internal(string familyName, string styleName)
		{
			int num;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(familyName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = familyName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(styleName, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = styleName.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				num = FontEngine.LoadFontFace_by_FamilyName_and_StyleName_Internal_Injected(ref managedSpanWrapper, ref managedSpanWrapper2);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
			return num;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003040 File Offset: 0x00001240
		public static FontEngineError LoadFontFace(string familyName, string styleName, float pointSize)
		{
			return (FontEngineError)FontEngine.LoadFontFace_With_Size_by_FamilyName_and_StyleName_Internal(familyName, styleName, (int)Math.Round((double)pointSize, MidpointRounding.AwayFromZero));
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003064 File Offset: 0x00001264
		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		private unsafe static int LoadFontFace_With_Size_by_FamilyName_and_StyleName_Internal(string familyName, string styleName, int pointSize)
		{
			int num;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(familyName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = familyName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(styleName, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = styleName.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				num = FontEngine.LoadFontFace_With_Size_by_FamilyName_and_StyleName_Internal_Injected(ref managedSpanWrapper, ref managedSpanWrapper2, pointSize);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
			return num;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000030F0 File Offset: 0x000012F0
		public static FontEngineError UnloadFontFace()
		{
			return (FontEngineError)FontEngine.UnloadFontFace_Internal();
		}

		// Token: 0x06000080 RID: 128
		[NativeMethod(Name = "TextCore::FontEngine::UnloadFontFace", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int UnloadFontFace_Internal();

		// Token: 0x06000081 RID: 129 RVA: 0x00003108 File Offset: 0x00001308
		public static FontEngineError UnloadAllFontFaces()
		{
			return (FontEngineError)FontEngine.UnloadAllFontFaces_Internal();
		}

		// Token: 0x06000082 RID: 130
		[NativeMethod(Name = "TextCore::FontEngine::UnloadAllFontFaces", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int UnloadAllFontFaces_Internal();

		// Token: 0x06000083 RID: 131 RVA: 0x00003120 File Offset: 0x00001320
		public static string[] GetSystemFontNames()
		{
			string[] systemFontNames_Internal = FontEngine.GetSystemFontNames_Internal();
			bool flag = systemFontNames_Internal != null && systemFontNames_Internal.Length == 0;
			string[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				array = systemFontNames_Internal;
			}
			return array;
		}

		// Token: 0x06000084 RID: 132
		[NativeMethod(Name = "TextCore::FontEngine::GetSystemFontNames", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] GetSystemFontNames_Internal();

		// Token: 0x06000085 RID: 133
		[NativeMethod(Name = "TextCore::FontEngine::GetSystemFontReferences", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern FontReference[] GetSystemFontReferences();

		// Token: 0x06000086 RID: 134
		[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsColorFontFace();

		// Token: 0x06000087 RID: 135 RVA: 0x0000314C File Offset: 0x0000134C
		[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
		internal static bool TryGetSystemFontReference(string familyName, string styleName, out FontReference fontRef)
		{
			return FontEngine.TryGetSystemFontReference_Internal(familyName, styleName, out fontRef);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003168 File Offset: 0x00001368
		[NativeMethod(Name = "TextCore::FontEngine::TryGetSystemFontReference", IsThreadSafe = true, IsFreeFunction = true)]
		private unsafe static bool TryGetSystemFontReference_Internal(string familyName, string styleName, out FontReference fontRef)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(familyName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = familyName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(styleName, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = styleName.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				flag = FontEngine.TryGetSystemFontReference_Internal_Injected(ref managedSpanWrapper, ref managedSpanWrapper2, out fontRef);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
			return flag;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000031F4 File Offset: 0x000013F4
		public static FontEngineError SetFaceSize(int pointSize)
		{
			return (FontEngineError)FontEngine.SetFaceSize_Internal(pointSize);
		}

		// Token: 0x0600008A RID: 138
		[NativeMethod(Name = "TextCore::FontEngine::SetFaceSize", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int SetFaceSize_Internal(int pointSize);

		// Token: 0x0600008B RID: 139 RVA: 0x0000320C File Offset: 0x0000140C
		public static FaceInfo GetFaceInfo()
		{
			FaceInfo faceInfo = default(FaceInfo);
			FontEngine.GetFaceInfo_Internal(ref faceInfo);
			return faceInfo;
		}

		// Token: 0x0600008C RID: 140
		[NativeMethod(Name = "TextCore::FontEngine::GetFaceInfo", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetFaceInfo_Internal(ref FaceInfo faceInfo);

		// Token: 0x0600008D RID: 141
		[NativeMethod(Name = "TextCore::FontEngine::GetFaceCount", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetFaceCount();

		// Token: 0x0600008E RID: 142 RVA: 0x00003230 File Offset: 0x00001430
		public static string[] GetFontFaces()
		{
			string[] fontFaces_Internal = FontEngine.GetFontFaces_Internal();
			bool flag = fontFaces_Internal != null && fontFaces_Internal.Length == 0;
			string[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				array = fontFaces_Internal;
			}
			return array;
		}

		// Token: 0x0600008F RID: 143
		[NativeMethod(Name = "TextCore::FontEngine::GetFontFaces", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] GetFontFaces_Internal();

		// Token: 0x06000090 RID: 144
		[NativeMethod(Name = "TextCore::FontEngine::GetVariantGlyphIndex", IsThreadSafe = true, IsFreeFunction = true)]
		[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern uint GetVariantGlyphIndex(uint unicode, uint variantSelectorUnicode);

		// Token: 0x06000091 RID: 145
		[NativeMethod(Name = "TextCore::FontEngine::GetGlyphIndex", IsThreadSafe = true, IsFreeFunction = true)]
		[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern uint GetGlyphIndex(uint unicode);

		// Token: 0x06000092 RID: 146
		[NativeMethod(Name = "TextCore::FontEngine::TryGetGlyphIndex", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool TryGetGlyphIndex(uint unicode, out uint glyphIndex);

		// Token: 0x06000093 RID: 147 RVA: 0x0000325C File Offset: 0x0000145C
		public static bool TryGetGlyphWithUnicodeValue(uint unicode, GlyphLoadFlags flags, out Glyph glyph)
		{
			GlyphMarshallingStruct glyphMarshallingStruct = default(GlyphMarshallingStruct);
			bool flag = FontEngine.TryGetGlyphWithUnicodeValue_Internal(unicode, flags, ref glyphMarshallingStruct);
			bool flag2;
			if (flag)
			{
				glyph = new Glyph(glyphMarshallingStruct);
				flag2 = true;
			}
			else
			{
				glyph = null;
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06000094 RID: 148
		[NativeMethod(Name = "TextCore::FontEngine::TryGetGlyphWithUnicodeValue", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryGetGlyphWithUnicodeValue_Internal(uint unicode, GlyphLoadFlags loadFlags, ref GlyphMarshallingStruct glyphStruct);

		// Token: 0x06000095 RID: 149 RVA: 0x00003294 File Offset: 0x00001494
		public static bool TryGetGlyphWithIndexValue(uint glyphIndex, GlyphLoadFlags flags, out Glyph glyph)
		{
			GlyphMarshallingStruct glyphMarshallingStruct = default(GlyphMarshallingStruct);
			bool flag = FontEngine.TryGetGlyphWithIndexValue_Internal(glyphIndex, flags, ref glyphMarshallingStruct);
			bool flag2;
			if (flag)
			{
				glyph = new Glyph(glyphMarshallingStruct);
				flag2 = true;
			}
			else
			{
				glyph = null;
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06000096 RID: 150
		[NativeMethod(Name = "TextCore::FontEngine::TryGetGlyphWithIndexValue", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryGetGlyphWithIndexValue_Internal(uint glyphIndex, GlyphLoadFlags loadFlags, ref GlyphMarshallingStruct glyphStruct);

		// Token: 0x06000097 RID: 151 RVA: 0x000032CC File Offset: 0x000014CC
		internal static bool TryPackGlyphInAtlas(Glyph glyph, int padding, GlyphPackingMode packingMode, GlyphRenderMode renderMode, int width, int height, List<GlyphRect> freeGlyphRects, List<GlyphRect> usedGlyphRects)
		{
			GlyphMarshallingStruct glyphMarshallingStruct = new GlyphMarshallingStruct(glyph);
			int count = freeGlyphRects.Count;
			int count2 = usedGlyphRects.Count;
			int num = count + count2;
			bool flag = FontEngine.s_FreeGlyphRects.Length < num || FontEngine.s_UsedGlyphRects.Length < num;
			if (flag)
			{
				int num2 = Mathf.NextPowerOfTwo(num + 1);
				FontEngine.s_FreeGlyphRects = new GlyphRect[num2];
				FontEngine.s_UsedGlyphRects = new GlyphRect[num2];
			}
			int num3 = Mathf.Max(count, count2);
			for (int i = 0; i < num3; i++)
			{
				bool flag2 = i < count;
				if (flag2)
				{
					FontEngine.s_FreeGlyphRects[i] = freeGlyphRects[i];
				}
				bool flag3 = i < count2;
				if (flag3)
				{
					FontEngine.s_UsedGlyphRects[i] = usedGlyphRects[i];
				}
			}
			bool flag4 = FontEngine.TryPackGlyphInAtlas_Internal(ref glyphMarshallingStruct, padding, packingMode, renderMode, width, height, FontEngine.s_FreeGlyphRects, ref count, FontEngine.s_UsedGlyphRects, ref count2);
			bool flag7;
			if (flag4)
			{
				glyph.glyphRect = glyphMarshallingStruct.glyphRect;
				freeGlyphRects.Clear();
				usedGlyphRects.Clear();
				num3 = Mathf.Max(count, count2);
				for (int j = 0; j < num3; j++)
				{
					bool flag5 = j < count;
					if (flag5)
					{
						freeGlyphRects.Add(FontEngine.s_FreeGlyphRects[j]);
					}
					bool flag6 = j < count2;
					if (flag6)
					{
						usedGlyphRects.Add(FontEngine.s_UsedGlyphRects[j]);
					}
				}
				flag7 = true;
			}
			else
			{
				flag7 = false;
			}
			return flag7;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003448 File Offset: 0x00001648
		[NativeMethod(Name = "TextCore::FontEngine::TryPackGlyph", IsThreadSafe = true, IsFreeFunction = true)]
		private unsafe static bool TryPackGlyphInAtlas_Internal(ref GlyphMarshallingStruct glyph, int padding, GlyphPackingMode packingMode, GlyphRenderMode renderMode, int width, int height, [Out] GlyphRect[] freeGlyphRects, ref int freeGlyphRectCount, [Out] GlyphRect[] usedGlyphRects, ref int usedGlyphRectCount)
		{
			bool flag;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				if (freeGlyphRects != null)
				{
					fixed (GlyphRect[] array = freeGlyphRects)
					{
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
					}
				}
				BlittableArrayWrapper blittableArrayWrapper2;
				if (usedGlyphRects != null)
				{
					fixed (GlyphRect[] array2 = usedGlyphRects)
					{
						if (array2.Length != 0)
						{
							blittableArrayWrapper2 = new BlittableArrayWrapper((void*)(&array2[0]), array2.Length);
						}
					}
				}
				flag = FontEngine.TryPackGlyphInAtlas_Internal_Injected(ref glyph, padding, packingMode, renderMode, width, height, out blittableArrayWrapper, ref freeGlyphRectCount, out blittableArrayWrapper2, ref usedGlyphRectCount);
			}
			finally
			{
				GlyphRect[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<GlyphRect>(ref array);
				GlyphRect[] array2;
				BlittableArrayWrapper blittableArrayWrapper2;
				blittableArrayWrapper2.Unmarshal<GlyphRect>(ref array2);
			}
			return flag;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000034D4 File Offset: 0x000016D4
		internal static bool TryPackGlyphsInAtlas(List<Glyph> glyphsToAdd, List<Glyph> glyphsAdded, int padding, GlyphPackingMode packingMode, GlyphRenderMode renderMode, int width, int height, List<GlyphRect> freeGlyphRects, List<GlyphRect> usedGlyphRects)
		{
			int count = glyphsToAdd.Count;
			int count2 = glyphsAdded.Count;
			int count3 = freeGlyphRects.Count;
			int count4 = usedGlyphRects.Count;
			int num = count + count2 + count3 + count4;
			bool flag = FontEngine.s_GlyphMarshallingStruct_IN.Length < num || FontEngine.s_GlyphMarshallingStruct_OUT.Length < num || FontEngine.s_FreeGlyphRects.Length < num || FontEngine.s_UsedGlyphRects.Length < num;
			if (flag)
			{
				int num2 = Mathf.NextPowerOfTwo(num + 1);
				FontEngine.s_GlyphMarshallingStruct_IN = new GlyphMarshallingStruct[num2];
				FontEngine.s_GlyphMarshallingStruct_OUT = new GlyphMarshallingStruct[num2];
				FontEngine.s_FreeGlyphRects = new GlyphRect[num2];
				FontEngine.s_UsedGlyphRects = new GlyphRect[num2];
			}
			FontEngine.s_GlyphLookupDictionary.Clear();
			for (int i = 0; i < num; i++)
			{
				bool flag2 = i < count;
				if (flag2)
				{
					GlyphMarshallingStruct glyphMarshallingStruct = new GlyphMarshallingStruct(glyphsToAdd[i]);
					FontEngine.s_GlyphMarshallingStruct_IN[i] = glyphMarshallingStruct;
					bool flag3 = !FontEngine.s_GlyphLookupDictionary.ContainsKey(glyphMarshallingStruct.index);
					if (flag3)
					{
						FontEngine.s_GlyphLookupDictionary.Add(glyphMarshallingStruct.index, glyphsToAdd[i]);
					}
				}
				bool flag4 = i < count2;
				if (flag4)
				{
					GlyphMarshallingStruct glyphMarshallingStruct2 = new GlyphMarshallingStruct(glyphsAdded[i]);
					FontEngine.s_GlyphMarshallingStruct_OUT[i] = glyphMarshallingStruct2;
					bool flag5 = !FontEngine.s_GlyphLookupDictionary.ContainsKey(glyphMarshallingStruct2.index);
					if (flag5)
					{
						FontEngine.s_GlyphLookupDictionary.Add(glyphMarshallingStruct2.index, glyphsAdded[i]);
					}
				}
				bool flag6 = i < count3;
				if (flag6)
				{
					FontEngine.s_FreeGlyphRects[i] = freeGlyphRects[i];
				}
				bool flag7 = i < count4;
				if (flag7)
				{
					FontEngine.s_UsedGlyphRects[i] = usedGlyphRects[i];
				}
			}
			bool flag8 = FontEngine.TryPackGlyphsInAtlas_Internal(FontEngine.s_GlyphMarshallingStruct_IN, ref count, FontEngine.s_GlyphMarshallingStruct_OUT, ref count2, padding, packingMode, renderMode, width, height, FontEngine.s_FreeGlyphRects, ref count3, FontEngine.s_UsedGlyphRects, ref count4);
			glyphsToAdd.Clear();
			glyphsAdded.Clear();
			freeGlyphRects.Clear();
			usedGlyphRects.Clear();
			for (int j = 0; j < num; j++)
			{
				bool flag9 = j < count;
				if (flag9)
				{
					GlyphMarshallingStruct glyphMarshallingStruct3 = FontEngine.s_GlyphMarshallingStruct_IN[j];
					Glyph glyph = FontEngine.s_GlyphLookupDictionary[glyphMarshallingStruct3.index];
					glyph.metrics = glyphMarshallingStruct3.metrics;
					glyph.glyphRect = glyphMarshallingStruct3.glyphRect;
					glyph.scale = glyphMarshallingStruct3.scale;
					glyph.atlasIndex = glyphMarshallingStruct3.atlasIndex;
					glyphsToAdd.Add(glyph);
				}
				bool flag10 = j < count2;
				if (flag10)
				{
					GlyphMarshallingStruct glyphMarshallingStruct4 = FontEngine.s_GlyphMarshallingStruct_OUT[j];
					Glyph glyph2 = FontEngine.s_GlyphLookupDictionary[glyphMarshallingStruct4.index];
					glyph2.metrics = glyphMarshallingStruct4.metrics;
					glyph2.glyphRect = glyphMarshallingStruct4.glyphRect;
					glyph2.scale = glyphMarshallingStruct4.scale;
					glyph2.atlasIndex = glyphMarshallingStruct4.atlasIndex;
					glyphsAdded.Add(glyph2);
				}
				bool flag11 = j < count3;
				if (flag11)
				{
					freeGlyphRects.Add(FontEngine.s_FreeGlyphRects[j]);
				}
				bool flag12 = j < count4;
				if (flag12)
				{
					usedGlyphRects.Add(FontEngine.s_UsedGlyphRects[j]);
				}
			}
			return flag8;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000383C File Offset: 0x00001A3C
		[NativeMethod(Name = "TextCore::FontEngine::TryPackGlyphs", IsThreadSafe = true, IsFreeFunction = true)]
		private unsafe static bool TryPackGlyphsInAtlas_Internal([Out] GlyphMarshallingStruct[] glyphsToAdd, ref int glyphsToAddCount, [Out] GlyphMarshallingStruct[] glyphsAdded, ref int glyphsAddedCount, int padding, GlyphPackingMode packingMode, GlyphRenderMode renderMode, int width, int height, [Out] GlyphRect[] freeGlyphRects, ref int freeGlyphRectCount, [Out] GlyphRect[] usedGlyphRects, ref int usedGlyphRectCount)
		{
			bool flag;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				if (glyphsToAdd != null)
				{
					fixed (GlyphMarshallingStruct[] array = glyphsToAdd)
					{
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
					}
				}
				BlittableArrayWrapper blittableArrayWrapper2;
				if (glyphsAdded != null)
				{
					fixed (GlyphMarshallingStruct[] array2 = glyphsAdded)
					{
						if (array2.Length != 0)
						{
							blittableArrayWrapper2 = new BlittableArrayWrapper((void*)(&array2[0]), array2.Length);
						}
					}
				}
				BlittableArrayWrapper blittableArrayWrapper3;
				if (freeGlyphRects != null)
				{
					fixed (GlyphRect[] array3 = freeGlyphRects)
					{
						if (array3.Length != 0)
						{
							blittableArrayWrapper3 = new BlittableArrayWrapper((void*)(&array3[0]), array3.Length);
						}
					}
				}
				BlittableArrayWrapper blittableArrayWrapper4;
				if (usedGlyphRects != null)
				{
					fixed (GlyphRect[] array4 = usedGlyphRects)
					{
						if (array4.Length != 0)
						{
							blittableArrayWrapper4 = new BlittableArrayWrapper((void*)(&array4[0]), array4.Length);
						}
					}
				}
				flag = FontEngine.TryPackGlyphsInAtlas_Internal_Injected(out blittableArrayWrapper, ref glyphsToAddCount, out blittableArrayWrapper2, ref glyphsAddedCount, padding, packingMode, renderMode, width, height, out blittableArrayWrapper3, ref freeGlyphRectCount, out blittableArrayWrapper4, ref usedGlyphRectCount);
			}
			finally
			{
				GlyphMarshallingStruct[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<GlyphMarshallingStruct>(ref array);
				GlyphMarshallingStruct[] array2;
				BlittableArrayWrapper blittableArrayWrapper2;
				blittableArrayWrapper2.Unmarshal<GlyphMarshallingStruct>(ref array2);
				GlyphRect[] array3;
				BlittableArrayWrapper blittableArrayWrapper3;
				blittableArrayWrapper3.Unmarshal<GlyphRect>(ref array3);
				GlyphRect[] array4;
				BlittableArrayWrapper blittableArrayWrapper4;
				blittableArrayWrapper4.Unmarshal<GlyphRect>(ref array4);
			}
			return flag;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003928 File Offset: 0x00001B28
		internal static FontEngineError RenderGlyphToTexture(Glyph glyph, int padding, GlyphRenderMode renderMode, Texture2D texture)
		{
			GlyphMarshallingStruct glyphMarshallingStruct = new GlyphMarshallingStruct(glyph);
			return (FontEngineError)FontEngine.RenderGlyphToTexture_Internal(glyphMarshallingStruct, padding, renderMode, texture);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000394C File Offset: 0x00001B4C
		[NativeMethod(Name = "TextCore::FontEngine::RenderGlyphToTexture", IsFreeFunction = true)]
		private static int RenderGlyphToTexture_Internal(GlyphMarshallingStruct glyphStruct, int padding, GlyphRenderMode renderMode, Texture2D texture)
		{
			return FontEngine.RenderGlyphToTexture_Internal_Injected(ref glyphStruct, padding, renderMode, Object.MarshalledUnityObject.Marshal<Texture2D>(texture));
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003968 File Offset: 0x00001B68
		internal static FontEngineError RenderGlyphsToTexture(List<Glyph> glyphs, int padding, GlyphRenderMode renderMode, Texture2D texture)
		{
			int count = glyphs.Count;
			bool flag = FontEngine.s_GlyphMarshallingStruct_IN.Length < count;
			if (flag)
			{
				int num = Mathf.NextPowerOfTwo(count + 1);
				FontEngine.s_GlyphMarshallingStruct_IN = new GlyphMarshallingStruct[num];
			}
			for (int i = 0; i < count; i++)
			{
				FontEngine.s_GlyphMarshallingStruct_IN[i] = new GlyphMarshallingStruct(glyphs[i]);
			}
			return (FontEngineError)FontEngine.RenderGlyphsToTexture_Internal(FontEngine.s_GlyphMarshallingStruct_IN, count, padding, renderMode, texture);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000039E8 File Offset: 0x00001BE8
		[NativeMethod(Name = "TextCore::FontEngine::RenderGlyphsToTexture", IsFreeFunction = true)]
		private unsafe static int RenderGlyphsToTexture_Internal(GlyphMarshallingStruct[] glyphs, int glyphCount, int padding, GlyphRenderMode renderMode, Texture2D texture)
		{
			Span<GlyphMarshallingStruct> span = new Span<GlyphMarshallingStruct>(glyphs);
			int num;
			fixed (GlyphMarshallingStruct* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				num = FontEngine.RenderGlyphsToTexture_Internal_Injected(ref managedSpanWrapper, glyphCount, padding, renderMode, Object.MarshalledUnityObject.Marshal<Texture2D>(texture));
			}
			return num;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003A2C File Offset: 0x00001C2C
		internal static FontEngineError RenderGlyphsToTexture(List<Glyph> glyphs, int padding, GlyphRenderMode renderMode, byte[] texBuffer, int texWidth, int texHeight)
		{
			int count = glyphs.Count;
			bool flag = FontEngine.s_GlyphMarshallingStruct_IN.Length < count;
			if (flag)
			{
				int num = Mathf.NextPowerOfTwo(count + 1);
				FontEngine.s_GlyphMarshallingStruct_IN = new GlyphMarshallingStruct[num];
			}
			for (int i = 0; i < count; i++)
			{
				FontEngine.s_GlyphMarshallingStruct_IN[i] = new GlyphMarshallingStruct(glyphs[i]);
			}
			return (FontEngineError)FontEngine.RenderGlyphsToTextureBuffer_Internal(FontEngine.s_GlyphMarshallingStruct_IN, count, padding, renderMode, texBuffer, texWidth, texHeight);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003AB0 File Offset: 0x00001CB0
		[NativeMethod(Name = "TextCore::FontEngine::RenderGlyphsToTextureBuffer", IsThreadSafe = true, IsFreeFunction = true)]
		private unsafe static int RenderGlyphsToTextureBuffer_Internal(GlyphMarshallingStruct[] glyphs, int glyphCount, int padding, GlyphRenderMode renderMode, [Out] byte[] texBuffer, int texWidth, int texHeight)
		{
			int num;
			try
			{
				Span<GlyphMarshallingStruct> span = new Span<GlyphMarshallingStruct>(glyphs);
				fixed (GlyphMarshallingStruct* ptr = span.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, span.Length);
					BlittableArrayWrapper blittableArrayWrapper;
					if (texBuffer != null)
					{
						fixed (byte[] array = texBuffer)
						{
							if (array.Length != 0)
							{
								blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
							}
						}
					}
					num = FontEngine.RenderGlyphsToTextureBuffer_Internal_Injected(ref managedSpanWrapper, glyphCount, padding, renderMode, out blittableArrayWrapper, texWidth, texHeight);
				}
			}
			finally
			{
				GlyphMarshallingStruct* ptr = null;
				byte[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<byte>(ref array);
			}
			return num;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003B38 File Offset: 0x00001D38
		internal static FontEngineError RenderGlyphsToSharedTexture(List<Glyph> glyphs, int padding, GlyphRenderMode renderMode)
		{
			int count = glyphs.Count;
			bool flag = FontEngine.s_GlyphMarshallingStruct_IN.Length < count;
			if (flag)
			{
				int num = Mathf.NextPowerOfTwo(count + 1);
				FontEngine.s_GlyphMarshallingStruct_IN = new GlyphMarshallingStruct[num];
			}
			for (int i = 0; i < count; i++)
			{
				FontEngine.s_GlyphMarshallingStruct_IN[i] = new GlyphMarshallingStruct(glyphs[i]);
			}
			return (FontEngineError)FontEngine.RenderGlyphsToSharedTexture_Internal(FontEngine.s_GlyphMarshallingStruct_IN, count, padding, renderMode);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003BB8 File Offset: 0x00001DB8
		[NativeMethod(Name = "TextCore::FontEngine::RenderGlyphsToSharedTexture", IsThreadSafe = true, IsFreeFunction = true)]
		private unsafe static int RenderGlyphsToSharedTexture_Internal(GlyphMarshallingStruct[] glyphs, int glyphCount, int padding, GlyphRenderMode renderMode)
		{
			Span<GlyphMarshallingStruct> span = new Span<GlyphMarshallingStruct>(glyphs);
			int num;
			fixed (GlyphMarshallingStruct* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				num = FontEngine.RenderGlyphsToSharedTexture_Internal_Injected(ref managedSpanWrapper, glyphCount, padding, renderMode);
			}
			return num;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003BF4 File Offset: 0x00001DF4
		[NativeMethod(Name = "TextCore::FontEngine::SetSharedTextureData", IsFreeFunction = true)]
		internal static void SetSharedTexture(Texture2D texture)
		{
			FontEngine.SetSharedTexture_Injected(Object.MarshalledUnityObject.Marshal<Texture2D>(texture));
		}

		// Token: 0x060000A4 RID: 164
		[NativeMethod(Name = "TextCore::FontEngine::ReleaseSharedTextureData", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ReleaseSharedTexture();

		// Token: 0x060000A5 RID: 165
		[NativeMethod(Name = "TextCore::FontEngine::SetTextureUploadMode", IsThreadSafe = true, IsFreeFunction = true)]
		[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetTextureUploadMode(bool shouldUploadImmediately);

		// Token: 0x060000A6 RID: 166 RVA: 0x00003C0C File Offset: 0x00001E0C
		[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
		internal static bool TryAddGlyphToTexture(uint glyphIndex, int padding, GlyphPackingMode packingMode, List<GlyphRect> freeGlyphRects, List<GlyphRect> usedGlyphRects, GlyphRenderMode renderMode, Texture2D texture, out Glyph glyph)
		{
			int count = freeGlyphRects.Count;
			int count2 = usedGlyphRects.Count;
			int num = count + count2;
			bool flag = FontEngine.s_FreeGlyphRects.Length < num || FontEngine.s_UsedGlyphRects.Length < num;
			if (flag)
			{
				int num2 = Mathf.NextPowerOfTwo(num + 1);
				FontEngine.s_FreeGlyphRects = new GlyphRect[num2];
				FontEngine.s_UsedGlyphRects = new GlyphRect[num2];
			}
			int num3 = Mathf.Max(count, count2);
			for (int i = 0; i < num3; i++)
			{
				bool flag2 = i < count;
				if (flag2)
				{
					FontEngine.s_FreeGlyphRects[i] = freeGlyphRects[i];
				}
				bool flag3 = i < count2;
				if (flag3)
				{
					FontEngine.s_UsedGlyphRects[i] = usedGlyphRects[i];
				}
			}
			GlyphMarshallingStruct glyphMarshallingStruct;
			bool flag4 = FontEngine.TryAddGlyphToTexture_Internal(glyphIndex, padding, packingMode, FontEngine.s_FreeGlyphRects, ref count, FontEngine.s_UsedGlyphRects, ref count2, renderMode, texture, out glyphMarshallingStruct);
			bool flag7;
			if (flag4)
			{
				glyph = new Glyph(glyphMarshallingStruct);
				freeGlyphRects.Clear();
				usedGlyphRects.Clear();
				num3 = Mathf.Max(count, count2);
				for (int j = 0; j < num3; j++)
				{
					bool flag5 = j < count;
					if (flag5)
					{
						freeGlyphRects.Add(FontEngine.s_FreeGlyphRects[j]);
					}
					bool flag6 = j < count2;
					if (flag6)
					{
						usedGlyphRects.Add(FontEngine.s_UsedGlyphRects[j]);
					}
				}
				flag7 = true;
			}
			else
			{
				glyph = null;
				flag7 = false;
			}
			return flag7;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003D78 File Offset: 0x00001F78
		[NativeMethod(Name = "TextCore::FontEngine::TryAddGlyphToTexture", IsThreadSafe = true, IsFreeFunction = true)]
		private unsafe static bool TryAddGlyphToTexture_Internal(uint glyphIndex, int padding, GlyphPackingMode packingMode, [Out] GlyphRect[] freeGlyphRects, ref int freeGlyphRectCount, [Out] GlyphRect[] usedGlyphRects, ref int usedGlyphRectCount, GlyphRenderMode renderMode, Texture2D texture, out GlyphMarshallingStruct glyph)
		{
			bool flag;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				if (freeGlyphRects != null)
				{
					fixed (GlyphRect[] array = freeGlyphRects)
					{
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
					}
				}
				BlittableArrayWrapper blittableArrayWrapper2;
				if (usedGlyphRects != null)
				{
					fixed (GlyphRect[] array2 = usedGlyphRects)
					{
						if (array2.Length != 0)
						{
							blittableArrayWrapper2 = new BlittableArrayWrapper((void*)(&array2[0]), array2.Length);
						}
					}
				}
				flag = FontEngine.TryAddGlyphToTexture_Internal_Injected(glyphIndex, padding, packingMode, out blittableArrayWrapper, ref freeGlyphRectCount, out blittableArrayWrapper2, ref usedGlyphRectCount, renderMode, Object.MarshalledUnityObject.Marshal<Texture2D>(texture), out glyph);
			}
			finally
			{
				GlyphRect[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<GlyphRect>(ref array);
				GlyphRect[] array2;
				BlittableArrayWrapper blittableArrayWrapper2;
				blittableArrayWrapper2.Unmarshal<GlyphRect>(ref array2);
			}
			return flag;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003E08 File Offset: 0x00002008
		[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
		internal static bool TryAddGlyphsToTexture(List<Glyph> glyphsToAdd, List<Glyph> glyphsAdded, int padding, GlyphPackingMode packingMode, List<GlyphRect> freeGlyphRects, List<GlyphRect> usedGlyphRects, GlyphRenderMode renderMode, Texture2D texture)
		{
			int count = glyphsToAdd.Count;
			int num = 0;
			bool flag = FontEngine.s_GlyphMarshallingStruct_IN.Length < count || FontEngine.s_GlyphMarshallingStruct_OUT.Length < count;
			if (flag)
			{
				int num2 = Mathf.NextPowerOfTwo(count + 1);
				bool flag2 = FontEngine.s_GlyphMarshallingStruct_IN.Length < count;
				if (flag2)
				{
					Array.Resize<GlyphMarshallingStruct>(ref FontEngine.s_GlyphMarshallingStruct_IN, num2);
				}
				bool flag3 = FontEngine.s_GlyphMarshallingStruct_OUT.Length < count;
				if (flag3)
				{
					Array.Resize<GlyphMarshallingStruct>(ref FontEngine.s_GlyphMarshallingStruct_OUT, num2);
				}
			}
			int count2 = freeGlyphRects.Count;
			int count3 = usedGlyphRects.Count;
			int num3 = count2 + count3 + count;
			bool flag4 = FontEngine.s_FreeGlyphRects.Length < num3 || FontEngine.s_UsedGlyphRects.Length < num3;
			if (flag4)
			{
				int num4 = Mathf.NextPowerOfTwo(num3 + 1);
				bool flag5 = FontEngine.s_FreeGlyphRects.Length < num3;
				if (flag5)
				{
					Array.Resize<GlyphRect>(ref FontEngine.s_FreeGlyphRects, num4);
				}
				bool flag6 = FontEngine.s_UsedGlyphRects.Length < num3;
				if (flag6)
				{
					Array.Resize<GlyphRect>(ref FontEngine.s_UsedGlyphRects, num4);
				}
			}
			FontEngine.s_GlyphLookupDictionary.Clear();
			int num5 = 0;
			bool flag7 = true;
			while (flag7)
			{
				flag7 = false;
				bool flag8 = num5 < count;
				if (flag8)
				{
					Glyph glyph = glyphsToAdd[num5];
					FontEngine.s_GlyphMarshallingStruct_IN[num5] = new GlyphMarshallingStruct(glyph);
					FontEngine.s_GlyphLookupDictionary.Add(glyph.index, glyph);
					flag7 = true;
				}
				bool flag9 = num5 < count2;
				if (flag9)
				{
					FontEngine.s_FreeGlyphRects[num5] = freeGlyphRects[num5];
					flag7 = true;
				}
				bool flag10 = num5 < count3;
				if (flag10)
				{
					FontEngine.s_UsedGlyphRects[num5] = usedGlyphRects[num5];
					flag7 = true;
				}
				num5++;
			}
			bool flag11 = FontEngine.TryAddGlyphsToTexture_Internal_MultiThread(FontEngine.s_GlyphMarshallingStruct_IN, ref count, FontEngine.s_GlyphMarshallingStruct_OUT, ref num, padding, packingMode, FontEngine.s_FreeGlyphRects, ref count2, FontEngine.s_UsedGlyphRects, ref count3, renderMode, texture);
			glyphsToAdd.Clear();
			glyphsAdded.Clear();
			freeGlyphRects.Clear();
			usedGlyphRects.Clear();
			num5 = 0;
			flag7 = true;
			while (flag7)
			{
				flag7 = false;
				bool flag12 = num5 < count;
				if (flag12)
				{
					uint index = FontEngine.s_GlyphMarshallingStruct_IN[num5].index;
					glyphsToAdd.Add(FontEngine.s_GlyphLookupDictionary[index]);
					flag7 = true;
				}
				bool flag13 = num5 < num;
				if (flag13)
				{
					uint index2 = FontEngine.s_GlyphMarshallingStruct_OUT[num5].index;
					Glyph glyph2 = FontEngine.s_GlyphLookupDictionary[index2];
					glyph2.atlasIndex = FontEngine.s_GlyphMarshallingStruct_OUT[num5].atlasIndex;
					glyph2.scale = FontEngine.s_GlyphMarshallingStruct_OUT[num5].scale;
					glyph2.glyphRect = FontEngine.s_GlyphMarshallingStruct_OUT[num5].glyphRect;
					glyph2.metrics = FontEngine.s_GlyphMarshallingStruct_OUT[num5].metrics;
					glyphsAdded.Add(glyph2);
					flag7 = true;
				}
				bool flag14 = num5 < count2;
				if (flag14)
				{
					freeGlyphRects.Add(FontEngine.s_FreeGlyphRects[num5]);
					flag7 = true;
				}
				bool flag15 = num5 < count3;
				if (flag15)
				{
					usedGlyphRects.Add(FontEngine.s_UsedGlyphRects[num5]);
					flag7 = true;
				}
				num5++;
			}
			return flag11;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000412C File Offset: 0x0000232C
		[NativeMethod(Name = "TextCore::FontEngine::TryAddGlyphsToTexture", IsThreadSafe = true, IsFreeFunction = true)]
		private unsafe static bool TryAddGlyphsToTexture_Internal_MultiThread([Out] GlyphMarshallingStruct[] glyphsToAdd, ref int glyphsToAddCount, [Out] GlyphMarshallingStruct[] glyphsAdded, ref int glyphsAddedCount, int padding, GlyphPackingMode packingMode, [Out] GlyphRect[] freeGlyphRects, ref int freeGlyphRectCount, [Out] GlyphRect[] usedGlyphRects, ref int usedGlyphRectCount, GlyphRenderMode renderMode, Texture2D texture)
		{
			bool flag;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				if (glyphsToAdd != null)
				{
					fixed (GlyphMarshallingStruct[] array = glyphsToAdd)
					{
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
					}
				}
				BlittableArrayWrapper blittableArrayWrapper2;
				if (glyphsAdded != null)
				{
					fixed (GlyphMarshallingStruct[] array2 = glyphsAdded)
					{
						if (array2.Length != 0)
						{
							blittableArrayWrapper2 = new BlittableArrayWrapper((void*)(&array2[0]), array2.Length);
						}
					}
				}
				BlittableArrayWrapper blittableArrayWrapper3;
				if (freeGlyphRects != null)
				{
					fixed (GlyphRect[] array3 = freeGlyphRects)
					{
						if (array3.Length != 0)
						{
							blittableArrayWrapper3 = new BlittableArrayWrapper((void*)(&array3[0]), array3.Length);
						}
					}
				}
				BlittableArrayWrapper blittableArrayWrapper4;
				if (usedGlyphRects != null)
				{
					fixed (GlyphRect[] array4 = usedGlyphRects)
					{
						if (array4.Length != 0)
						{
							blittableArrayWrapper4 = new BlittableArrayWrapper((void*)(&array4[0]), array4.Length);
						}
					}
				}
				flag = FontEngine.TryAddGlyphsToTexture_Internal_MultiThread_Injected(out blittableArrayWrapper, ref glyphsToAddCount, out blittableArrayWrapper2, ref glyphsAddedCount, padding, packingMode, out blittableArrayWrapper3, ref freeGlyphRectCount, out blittableArrayWrapper4, ref usedGlyphRectCount, renderMode, Object.MarshalledUnityObject.Marshal<Texture2D>(texture));
			}
			finally
			{
				GlyphMarshallingStruct[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<GlyphMarshallingStruct>(ref array);
				GlyphMarshallingStruct[] array2;
				BlittableArrayWrapper blittableArrayWrapper2;
				blittableArrayWrapper2.Unmarshal<GlyphMarshallingStruct>(ref array2);
				GlyphRect[] array3;
				BlittableArrayWrapper blittableArrayWrapper3;
				blittableArrayWrapper3.Unmarshal<GlyphRect>(ref array3);
				GlyphRect[] array4;
				BlittableArrayWrapper blittableArrayWrapper4;
				blittableArrayWrapper4.Unmarshal<GlyphRect>(ref array4);
			}
			return flag;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000421C File Offset: 0x0000241C
		[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
		internal static bool TryAddGlyphsToTexture(List<uint> glyphIndexes, int padding, GlyphPackingMode packingMode, List<GlyphRect> freeGlyphRects, List<GlyphRect> usedGlyphRects, GlyphRenderMode renderMode, Texture2D texture, out Glyph[] glyphs)
		{
			glyphs = null;
			bool flag = glyphIndexes == null || glyphIndexes.Count == 0;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				int count = glyphIndexes.Count;
				bool flag3 = FontEngine.s_GlyphIndexes_MarshallingArray_A == null || FontEngine.s_GlyphIndexes_MarshallingArray_A.Length < count;
				if (flag3)
				{
					FontEngine.s_GlyphIndexes_MarshallingArray_A = new uint[Mathf.NextPowerOfTwo(count + 1)];
				}
				int count2 = freeGlyphRects.Count;
				int count3 = usedGlyphRects.Count;
				int num = count2 + count3 + count;
				bool flag4 = FontEngine.s_FreeGlyphRects.Length < num || FontEngine.s_UsedGlyphRects.Length < num;
				if (flag4)
				{
					int num2 = Mathf.NextPowerOfTwo(num + 1);
					FontEngine.s_FreeGlyphRects = new GlyphRect[num2];
					FontEngine.s_UsedGlyphRects = new GlyphRect[num2];
				}
				bool flag5 = FontEngine.s_GlyphMarshallingStruct_OUT.Length < count;
				if (flag5)
				{
					int num3 = Mathf.NextPowerOfTwo(count + 1);
					FontEngine.s_GlyphMarshallingStruct_OUT = new GlyphMarshallingStruct[num3];
				}
				int num4 = FontEngineUtilities.MaxValue(count2, count3, count);
				for (int i = 0; i < num4; i++)
				{
					bool flag6 = i < count;
					if (flag6)
					{
						FontEngine.s_GlyphIndexes_MarshallingArray_A[i] = glyphIndexes[i];
					}
					bool flag7 = i < count2;
					if (flag7)
					{
						FontEngine.s_FreeGlyphRects[i] = freeGlyphRects[i];
					}
					bool flag8 = i < count3;
					if (flag8)
					{
						FontEngine.s_UsedGlyphRects[i] = usedGlyphRects[i];
					}
				}
				bool flag9 = FontEngine.TryAddGlyphsToTexture_Internal(FontEngine.s_GlyphIndexes_MarshallingArray_A, padding, packingMode, FontEngine.s_FreeGlyphRects, ref count2, FontEngine.s_UsedGlyphRects, ref count3, renderMode, texture, FontEngine.s_GlyphMarshallingStruct_OUT, ref count);
				bool flag10 = FontEngine.s_Glyphs == null || FontEngine.s_Glyphs.Length <= count;
				if (flag10)
				{
					FontEngine.s_Glyphs = new Glyph[Mathf.NextPowerOfTwo(count + 1)];
				}
				FontEngine.s_Glyphs[count] = null;
				freeGlyphRects.Clear();
				usedGlyphRects.Clear();
				num4 = FontEngineUtilities.MaxValue(count2, count3, count);
				for (int j = 0; j < num4; j++)
				{
					bool flag11 = j < count;
					if (flag11)
					{
						FontEngine.s_Glyphs[j] = new Glyph(FontEngine.s_GlyphMarshallingStruct_OUT[j]);
					}
					bool flag12 = j < count2;
					if (flag12)
					{
						freeGlyphRects.Add(FontEngine.s_FreeGlyphRects[j]);
					}
					bool flag13 = j < count3;
					if (flag13)
					{
						usedGlyphRects.Add(FontEngine.s_UsedGlyphRects[j]);
					}
				}
				glyphs = FontEngine.s_Glyphs;
				flag2 = flag9;
			}
			return flag2;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000447C File Offset: 0x0000267C
		[NativeMethod(Name = "TextCore::FontEngine::TryAddGlyphsToTexture", IsThreadSafe = true, IsFreeFunction = true)]
		private unsafe static bool TryAddGlyphsToTexture_Internal(uint[] glyphIndex, int padding, GlyphPackingMode packingMode, [Out] GlyphRect[] freeGlyphRects, ref int freeGlyphRectCount, [Out] GlyphRect[] usedGlyphRects, ref int usedGlyphRectCount, GlyphRenderMode renderMode, Texture2D texture, [Out] GlyphMarshallingStruct[] glyphs, ref int glyphCount)
		{
			bool flag;
			try
			{
				Span<uint> span = new Span<uint>(glyphIndex);
				fixed (uint* ptr = span.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, span.Length);
					BlittableArrayWrapper blittableArrayWrapper;
					if (freeGlyphRects != null)
					{
						fixed (GlyphRect[] array = freeGlyphRects)
						{
							if (array.Length != 0)
							{
								blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
							}
						}
					}
					BlittableArrayWrapper blittableArrayWrapper2;
					if (usedGlyphRects != null)
					{
						fixed (GlyphRect[] array2 = usedGlyphRects)
						{
							if (array2.Length != 0)
							{
								blittableArrayWrapper2 = new BlittableArrayWrapper((void*)(&array2[0]), array2.Length);
							}
						}
					}
					IntPtr intPtr = Object.MarshalledUnityObject.Marshal<Texture2D>(texture);
					BlittableArrayWrapper blittableArrayWrapper3;
					if (glyphs != null)
					{
						fixed (GlyphMarshallingStruct[] array3 = glyphs)
						{
							if (array3.Length != 0)
							{
								blittableArrayWrapper3 = new BlittableArrayWrapper((void*)(&array3[0]), array3.Length);
							}
						}
					}
					flag = FontEngine.TryAddGlyphsToTexture_Internal_Injected(ref managedSpanWrapper, padding, packingMode, out blittableArrayWrapper, ref freeGlyphRectCount, out blittableArrayWrapper2, ref usedGlyphRectCount, renderMode, intPtr, out blittableArrayWrapper3, ref glyphCount);
				}
			}
			finally
			{
				uint* ptr = null;
				GlyphRect[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<GlyphRect>(ref array);
				GlyphRect[] array2;
				BlittableArrayWrapper blittableArrayWrapper2;
				blittableArrayWrapper2.Unmarshal<GlyphRect>(ref array2);
				GlyphMarshallingStruct[] array3;
				BlittableArrayWrapper blittableArrayWrapper3;
				blittableArrayWrapper3.Unmarshal<GlyphMarshallingStruct>(ref array3);
			}
			return flag;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000456C File Offset: 0x0000276C
		[NativeMethod(Name = "TextCore::FontEngine::GetOpenTypeLayoutTable", IsFreeFunction = true)]
		internal static OTL_Table GetOpenTypeLayoutTable(OTL_TableType type)
		{
			OTL_Table otl_Table;
			FontEngine.GetOpenTypeLayoutTable_Injected(type, out otl_Table);
			return otl_Table;
		}

		// Token: 0x060000AD RID: 173
		[NativeMethod(Name = "TextCore::FontEngine::GetOpenTypeLayoutScripts", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern OTL_Script[] GetOpenTypeLayoutScripts();

		// Token: 0x060000AE RID: 174
		[NativeMethod(Name = "TextCore::FontEngine::GetOpenTypeLayoutFeatures", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern OTL_Feature[] GetOpenTypeLayoutFeatures();

		// Token: 0x060000AF RID: 175 RVA: 0x00004584 File Offset: 0x00002784
		[NativeMethod(Name = "TextCore::FontEngine::GetOpenTypeLayoutLookups", IsFreeFunction = true)]
		internal static OTL_Lookup[] GetOpenTypeLayoutLookups()
		{
			OTL_Lookup[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				FontEngine.GetOpenTypeLayoutLookups_Injected(out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				OTL_Lookup[] array;
				blittableArrayWrapper.Unmarshal<OTL_Lookup>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000045B8 File Offset: 0x000027B8
		internal static OpenTypeFeature[] GetOpenTypeFontFeatureList()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000045C0 File Offset: 0x000027C0
		[NativeMethod(Name = "TextCore::FontEngine::GetAllSingleSubstitutionRecords", IsThreadSafe = true, IsFreeFunction = true)]
		internal static SingleSubstitutionRecord[] GetAllSingleSubstitutionRecords()
		{
			SingleSubstitutionRecord[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				FontEngine.GetAllSingleSubstitutionRecords_Injected(out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				SingleSubstitutionRecord[] array;
				blittableArrayWrapper.Unmarshal<SingleSubstitutionRecord>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000045F4 File Offset: 0x000027F4
		internal static SingleSubstitutionRecord[] GetSingleSubstitutionRecords(int lookupIndex, uint glyphIndex)
		{
			FontEngine.GlyphIndexToMarshallingArray(glyphIndex, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetSingleSubstitutionRecords(lookupIndex, FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004620 File Offset: 0x00002820
		internal static SingleSubstitutionRecord[] GetSingleSubstitutionRecords(int lookupIndex, List<uint> glyphIndexes)
		{
			FontEngine.GenericListToMarshallingArray<uint>(ref glyphIndexes, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetSingleSubstitutionRecords(lookupIndex, FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000464C File Offset: 0x0000284C
		private static SingleSubstitutionRecord[] GetSingleSubstitutionRecords(int lookupIndex, uint[] glyphIndexes)
		{
			int num;
			FontEngine.PopulateSingleSubstitutionRecordMarshallingArray_from_GlyphIndexes(glyphIndexes, lookupIndex, out num);
			bool flag = num == 0;
			SingleSubstitutionRecord[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				FontEngine.SetMarshallingArraySize<SingleSubstitutionRecord>(ref FontEngine.s_SingleSubstitutionRecords_MarshallingArray, num);
				FontEngine.GetSingleSubstitutionRecordsFromMarshallingArray(FontEngine.s_SingleSubstitutionRecords_MarshallingArray.AsSpan<SingleSubstitutionRecord>());
				FontEngine.s_SingleSubstitutionRecords_MarshallingArray[num] = default(SingleSubstitutionRecord);
				array = FontEngine.s_SingleSubstitutionRecords_MarshallingArray;
			}
			return array;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000046A8 File Offset: 0x000028A8
		[NativeMethod(Name = "TextCore::FontEngine::PopulateSingleSubstitutionRecordMarshallingArray", IsFreeFunction = true)]
		private unsafe static int PopulateSingleSubstitutionRecordMarshallingArray_from_GlyphIndexes(uint[] glyphIndexes, int lookupIndex, out int recordCount)
		{
			Span<uint> span = new Span<uint>(glyphIndexes);
			int num;
			fixed (uint* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				num = FontEngine.PopulateSingleSubstitutionRecordMarshallingArray_from_GlyphIndexes_Injected(ref managedSpanWrapper, lookupIndex, out recordCount);
			}
			return num;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000046E4 File Offset: 0x000028E4
		[NativeMethod(Name = "TextCore::FontEngine::GetSingleSubstitutionRecordsFromMarshallingArray", IsFreeFunction = true)]
		private unsafe static int GetSingleSubstitutionRecordsFromMarshallingArray(Span<SingleSubstitutionRecord> singleSubstitutionRecords)
		{
			Span<SingleSubstitutionRecord> span = singleSubstitutionRecords;
			int singleSubstitutionRecordsFromMarshallingArray_Injected;
			fixed (SingleSubstitutionRecord* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				singleSubstitutionRecordsFromMarshallingArray_Injected = FontEngine.GetSingleSubstitutionRecordsFromMarshallingArray_Injected(ref managedSpanWrapper);
			}
			return singleSubstitutionRecordsFromMarshallingArray_Injected;
		}

		// Token: 0x060000B7 RID: 183
		[NativeMethod(Name = "TextCore::FontEngine::GetAllMultipleSubstitutionRecords", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern MultipleSubstitutionRecord[] GetAllMultipleSubstitutionRecords();

		// Token: 0x060000B8 RID: 184 RVA: 0x00004718 File Offset: 0x00002918
		internal static MultipleSubstitutionRecord[] GetMultipleSubstitutionRecords(int lookupIndex, uint glyphIndex)
		{
			FontEngine.GlyphIndexToMarshallingArray(glyphIndex, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetMultipleSubstitutionRecords(lookupIndex, FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004744 File Offset: 0x00002944
		internal static MultipleSubstitutionRecord[] GetMultipleSubstitutionRecords(int lookupIndex, List<uint> glyphIndexes)
		{
			FontEngine.GenericListToMarshallingArray<uint>(ref glyphIndexes, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetMultipleSubstitutionRecords(lookupIndex, FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004770 File Offset: 0x00002970
		private static MultipleSubstitutionRecord[] GetMultipleSubstitutionRecords(int lookupIndex, uint[] glyphIndexes)
		{
			int num;
			FontEngine.PopulateMultipleSubstitutionRecordMarshallingArray_from_GlyphIndexes(glyphIndexes, lookupIndex, out num);
			bool flag = num == 0;
			MultipleSubstitutionRecord[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				FontEngine.SetMarshallingArraySize<MultipleSubstitutionRecord>(ref FontEngine.s_MultipleSubstitutionRecords_MarshallingArray, num);
				FontEngine.GetMultipleSubstitutionRecordsFromMarshallingArray(FontEngine.s_MultipleSubstitutionRecords_MarshallingArray);
				FontEngine.s_MultipleSubstitutionRecords_MarshallingArray[num] = default(MultipleSubstitutionRecord);
				array = FontEngine.s_MultipleSubstitutionRecords_MarshallingArray;
			}
			return array;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000047C8 File Offset: 0x000029C8
		[NativeMethod(Name = "TextCore::FontEngine::PopulateMultipleSubstitutionRecordMarshallingArray", IsFreeFunction = true)]
		private unsafe static int PopulateMultipleSubstitutionRecordMarshallingArray_from_GlyphIndexes(uint[] glyphIndexes, int lookupIndex, out int recordCount)
		{
			Span<uint> span = new Span<uint>(glyphIndexes);
			int num;
			fixed (uint* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				num = FontEngine.PopulateMultipleSubstitutionRecordMarshallingArray_from_GlyphIndexes_Injected(ref managedSpanWrapper, lookupIndex, out recordCount);
			}
			return num;
		}

		// Token: 0x060000BC RID: 188
		[NativeMethod(Name = "TextCore::FontEngine::GetMultipleSubstitutionRecordsFromMarshallingArray", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMultipleSubstitutionRecordsFromMarshallingArray([Out] MultipleSubstitutionRecord[] substitutionRecords);

		// Token: 0x060000BD RID: 189
		[NativeMethod(Name = "TextCore::FontEngine::GetAllAlternateSubstitutionRecords", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AlternateSubstitutionRecord[] GetAllAlternateSubstitutionRecords();

		// Token: 0x060000BE RID: 190 RVA: 0x00004804 File Offset: 0x00002A04
		internal static AlternateSubstitutionRecord[] GetAlternateSubstitutionRecords(int lookupIndex, uint glyphIndex)
		{
			FontEngine.GlyphIndexToMarshallingArray(glyphIndex, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetAlternateSubstitutionRecords(lookupIndex, FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004830 File Offset: 0x00002A30
		internal static AlternateSubstitutionRecord[] GetAlternateSubstitutionRecords(int lookupIndex, List<uint> glyphIndexes)
		{
			FontEngine.GenericListToMarshallingArray<uint>(ref glyphIndexes, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetAlternateSubstitutionRecords(lookupIndex, FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000485C File Offset: 0x00002A5C
		private static AlternateSubstitutionRecord[] GetAlternateSubstitutionRecords(int lookupIndex, uint[] glyphIndexes)
		{
			int num;
			FontEngine.PopulateAlternateSubstitutionRecordMarshallingArray_from_GlyphIndexes(glyphIndexes, lookupIndex, out num);
			bool flag = num == 0;
			AlternateSubstitutionRecord[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				FontEngine.SetMarshallingArraySize<AlternateSubstitutionRecord>(ref FontEngine.s_AlternateSubstitutionRecords_MarshallingArray, num);
				FontEngine.GetAlternateSubstitutionRecordsFromMarshallingArray(FontEngine.s_AlternateSubstitutionRecords_MarshallingArray);
				FontEngine.s_AlternateSubstitutionRecords_MarshallingArray[num] = default(AlternateSubstitutionRecord);
				array = FontEngine.s_AlternateSubstitutionRecords_MarshallingArray;
			}
			return array;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000048B4 File Offset: 0x00002AB4
		[NativeMethod(Name = "TextCore::FontEngine::PopulateAlternateSubstitutionRecordMarshallingArray", IsFreeFunction = true)]
		private unsafe static int PopulateAlternateSubstitutionRecordMarshallingArray_from_GlyphIndexes(uint[] glyphIndexes, int lookupIndex, out int recordCount)
		{
			Span<uint> span = new Span<uint>(glyphIndexes);
			int num;
			fixed (uint* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				num = FontEngine.PopulateAlternateSubstitutionRecordMarshallingArray_from_GlyphIndexes_Injected(ref managedSpanWrapper, lookupIndex, out recordCount);
			}
			return num;
		}

		// Token: 0x060000C2 RID: 194
		[NativeMethod(Name = "TextCore::FontEngine::GetAlternateSubstitutionRecordsFromMarshallingArray", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetAlternateSubstitutionRecordsFromMarshallingArray([Out] AlternateSubstitutionRecord[] singleSubstitutionRecords);

		// Token: 0x060000C3 RID: 195
		[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
		[NativeMethod(Name = "TextCore::FontEngine::GetAllLigatureSubstitutionRecords", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern LigatureSubstitutionRecord[] GetAllLigatureSubstitutionRecords();

		// Token: 0x060000C4 RID: 196 RVA: 0x000048F0 File Offset: 0x00002AF0
		[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
		internal static LigatureSubstitutionRecord[] GetLigatureSubstitutionRecords(uint glyphIndex)
		{
			FontEngine.GlyphIndexToMarshallingArray(glyphIndex, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetLigatureSubstitutionRecords(FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00004918 File Offset: 0x00002B18
		[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
		internal static LigatureSubstitutionRecord[] GetLigatureSubstitutionRecords(List<uint> glyphIndexes)
		{
			FontEngine.GenericListToMarshallingArray<uint>(ref glyphIndexes, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetLigatureSubstitutionRecords(FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004944 File Offset: 0x00002B44
		internal static LigatureSubstitutionRecord[] GetLigatureSubstitutionRecords(int lookupIndex, uint glyphIndex)
		{
			FontEngine.GlyphIndexToMarshallingArray(glyphIndex, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetLigatureSubstitutionRecords(lookupIndex, FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004970 File Offset: 0x00002B70
		internal static LigatureSubstitutionRecord[] GetLigatureSubstitutionRecords(int lookupIndex, List<uint> glyphIndexes)
		{
			FontEngine.GenericListToMarshallingArray<uint>(ref glyphIndexes, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetLigatureSubstitutionRecords(lookupIndex, FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000499C File Offset: 0x00002B9C
		private static LigatureSubstitutionRecord[] GetLigatureSubstitutionRecords(uint[] glyphIndexes)
		{
			int num;
			FontEngine.PopulateLigatureSubstitutionRecordMarshallingArray(glyphIndexes, out num);
			bool flag = num == 0;
			LigatureSubstitutionRecord[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				FontEngine.SetMarshallingArraySize<LigatureSubstitutionRecord>(ref FontEngine.s_LigatureSubstitutionRecords_MarshallingArray, num);
				FontEngine.GetLigatureSubstitutionRecordsFromMarshallingArray(FontEngine.s_LigatureSubstitutionRecords_MarshallingArray);
				FontEngine.s_LigatureSubstitutionRecords_MarshallingArray[num] = default(LigatureSubstitutionRecord);
				array = FontEngine.s_LigatureSubstitutionRecords_MarshallingArray;
			}
			return array;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000049F0 File Offset: 0x00002BF0
		private static LigatureSubstitutionRecord[] GetLigatureSubstitutionRecords(int lookupIndex, uint[] glyphIndexes)
		{
			int num;
			FontEngine.PopulateLigatureSubstitutionRecordMarshallingArray_for_LookupIndex(glyphIndexes, lookupIndex, out num);
			bool flag = num == 0;
			LigatureSubstitutionRecord[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				FontEngine.SetMarshallingArraySize<LigatureSubstitutionRecord>(ref FontEngine.s_LigatureSubstitutionRecords_MarshallingArray, num);
				FontEngine.GetLigatureSubstitutionRecordsFromMarshallingArray(FontEngine.s_LigatureSubstitutionRecords_MarshallingArray);
				FontEngine.s_LigatureSubstitutionRecords_MarshallingArray[num] = default(LigatureSubstitutionRecord);
				array = FontEngine.s_LigatureSubstitutionRecords_MarshallingArray;
			}
			return array;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004A48 File Offset: 0x00002C48
		[NativeMethod(Name = "TextCore::FontEngine::PopulateLigatureSubstitutionRecordMarshallingArray", IsFreeFunction = true)]
		private unsafe static int PopulateLigatureSubstitutionRecordMarshallingArray(uint[] glyphIndexes, out int recordCount)
		{
			Span<uint> span = new Span<uint>(glyphIndexes);
			int num;
			fixed (uint* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				num = FontEngine.PopulateLigatureSubstitutionRecordMarshallingArray_Injected(ref managedSpanWrapper, out recordCount);
			}
			return num;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004A84 File Offset: 0x00002C84
		[NativeMethod(Name = "TextCore::FontEngine::PopulateLigatureSubstitutionRecordMarshallingArray", IsFreeFunction = true)]
		private unsafe static int PopulateLigatureSubstitutionRecordMarshallingArray_for_LookupIndex(uint[] glyphIndexes, int lookupIndex, out int recordCount)
		{
			Span<uint> span = new Span<uint>(glyphIndexes);
			int num;
			fixed (uint* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				num = FontEngine.PopulateLigatureSubstitutionRecordMarshallingArray_for_LookupIndex_Injected(ref managedSpanWrapper, lookupIndex, out recordCount);
			}
			return num;
		}

		// Token: 0x060000CC RID: 204
		[NativeMethod(Name = "TextCore::FontEngine::GetLigatureSubstitutionRecordsFromMarshallingArray", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetLigatureSubstitutionRecordsFromMarshallingArray([Out] LigatureSubstitutionRecord[] ligatureSubstitutionRecords);

		// Token: 0x060000CD RID: 205
		[NativeMethod(Name = "TextCore::FontEngine::GetAllContextualSubstitutionRecords", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ContextualSubstitutionRecord[] GetAllContextualSubstitutionRecords();

		// Token: 0x060000CE RID: 206 RVA: 0x00004AC0 File Offset: 0x00002CC0
		internal static ContextualSubstitutionRecord[] GetContextualSubstitutionRecords(int lookupIndex, uint glyphIndex)
		{
			FontEngine.GlyphIndexToMarshallingArray(glyphIndex, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetContextualSubstitutionRecords(lookupIndex, FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004AEC File Offset: 0x00002CEC
		internal static ContextualSubstitutionRecord[] GetContextualSubstitutionRecords(int lookupIndex, List<uint> glyphIndexes)
		{
			FontEngine.GenericListToMarshallingArray<uint>(ref glyphIndexes, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetContextualSubstitutionRecords(lookupIndex, FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004B18 File Offset: 0x00002D18
		private static ContextualSubstitutionRecord[] GetContextualSubstitutionRecords(int lookupIndex, uint[] glyphIndexes)
		{
			int num;
			FontEngine.PopulateContextualSubstitutionRecordMarshallingArray_from_GlyphIndexes(glyphIndexes, lookupIndex, out num);
			bool flag = num == 0;
			ContextualSubstitutionRecord[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				FontEngine.SetMarshallingArraySize<ContextualSubstitutionRecord>(ref FontEngine.s_ContextualSubstitutionRecords_MarshallingArray, num);
				FontEngine.GetContextualSubstitutionRecordsFromMarshallingArray(FontEngine.s_ContextualSubstitutionRecords_MarshallingArray);
				FontEngine.s_ContextualSubstitutionRecords_MarshallingArray[num] = default(ContextualSubstitutionRecord);
				array = FontEngine.s_ContextualSubstitutionRecords_MarshallingArray;
			}
			return array;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004B70 File Offset: 0x00002D70
		[NativeMethod(Name = "TextCore::FontEngine::PopulateContextualSubstitutionRecordMarshallingArray", IsFreeFunction = true)]
		private unsafe static int PopulateContextualSubstitutionRecordMarshallingArray_from_GlyphIndexes(uint[] glyphIndexes, int lookupIndex, out int recordCount)
		{
			Span<uint> span = new Span<uint>(glyphIndexes);
			int num;
			fixed (uint* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				num = FontEngine.PopulateContextualSubstitutionRecordMarshallingArray_from_GlyphIndexes_Injected(ref managedSpanWrapper, lookupIndex, out recordCount);
			}
			return num;
		}

		// Token: 0x060000D2 RID: 210
		[NativeMethod(Name = "TextCore::FontEngine::GetContextualSubstitutionRecordsFromMarshallingArray", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetContextualSubstitutionRecordsFromMarshallingArray([Out] ContextualSubstitutionRecord[] substitutionRecords);

		// Token: 0x060000D3 RID: 211
		[NativeMethod(Name = "TextCore::FontEngine::GetAllChainingContextualSubstitutionRecords", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ChainingContextualSubstitutionRecord[] GetAllChainingContextualSubstitutionRecords();

		// Token: 0x060000D4 RID: 212 RVA: 0x00004BAC File Offset: 0x00002DAC
		internal static ChainingContextualSubstitutionRecord[] GetChainingContextualSubstitutionRecords(int lookupIndex, uint glyphIndex)
		{
			FontEngine.GlyphIndexToMarshallingArray(glyphIndex, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetChainingContextualSubstitutionRecords(lookupIndex, FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004BD8 File Offset: 0x00002DD8
		internal static ChainingContextualSubstitutionRecord[] GetChainingContextualSubstitutionRecords(int lookupIndex, List<uint> glyphIndexes)
		{
			FontEngine.GenericListToMarshallingArray<uint>(ref glyphIndexes, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetChainingContextualSubstitutionRecords(lookupIndex, FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004C04 File Offset: 0x00002E04
		private static ChainingContextualSubstitutionRecord[] GetChainingContextualSubstitutionRecords(int lookupIndex, uint[] glyphIndexes)
		{
			int num;
			FontEngine.PopulateChainingContextualSubstitutionRecordMarshallingArray_from_GlyphIndexes(glyphIndexes, lookupIndex, out num);
			bool flag = num == 0;
			ChainingContextualSubstitutionRecord[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				FontEngine.SetMarshallingArraySize<ChainingContextualSubstitutionRecord>(ref FontEngine.s_ChainingContextualSubstitutionRecords_MarshallingArray, num);
				FontEngine.GetChainingContextualSubstitutionRecordsFromMarshallingArray(FontEngine.s_ChainingContextualSubstitutionRecords_MarshallingArray);
				FontEngine.s_ChainingContextualSubstitutionRecords_MarshallingArray[num] = default(ChainingContextualSubstitutionRecord);
				array = FontEngine.s_ChainingContextualSubstitutionRecords_MarshallingArray;
			}
			return array;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004C5C File Offset: 0x00002E5C
		[NativeMethod(Name = "TextCore::FontEngine::PopulateChainingContextualSubstitutionRecordMarshallingArray", IsFreeFunction = true)]
		private unsafe static int PopulateChainingContextualSubstitutionRecordMarshallingArray_from_GlyphIndexes(uint[] glyphIndexes, int lookupIndex, out int recordCount)
		{
			Span<uint> span = new Span<uint>(glyphIndexes);
			int num;
			fixed (uint* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				num = FontEngine.PopulateChainingContextualSubstitutionRecordMarshallingArray_from_GlyphIndexes_Injected(ref managedSpanWrapper, lookupIndex, out recordCount);
			}
			return num;
		}

		// Token: 0x060000D8 RID: 216
		[NativeMethod(Name = "TextCore::FontEngine::GetChainingContextualSubstitutionRecordsFromMarshallingArray", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetChainingContextualSubstitutionRecordsFromMarshallingArray([Out] ChainingContextualSubstitutionRecord[] substitutionRecords);

		// Token: 0x060000D9 RID: 217 RVA: 0x00004C98 File Offset: 0x00002E98
		[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
		internal static GlyphPairAdjustmentRecord[] GetGlyphPairAdjustmentTable(uint[] glyphIndexes)
		{
			int num;
			FontEngine.PopulatePairAdjustmentRecordMarshallingArray_from_KernTable(glyphIndexes, out num);
			bool flag = num == 0;
			GlyphPairAdjustmentRecord[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				FontEngine.SetMarshallingArraySize<GlyphPairAdjustmentRecord>(ref FontEngine.s_PairAdjustmentRecords_MarshallingArray, num);
				FontEngine.GetPairAdjustmentRecordsFromMarshallingArray(FontEngine.s_PairAdjustmentRecords_MarshallingArray);
				FontEngine.s_PairAdjustmentRecords_MarshallingArray[num] = default(GlyphPairAdjustmentRecord);
				array = FontEngine.s_PairAdjustmentRecords_MarshallingArray;
			}
			return array;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004CF4 File Offset: 0x00002EF4
		internal static GlyphPairAdjustmentRecord[] GetGlyphPairAdjustmentRecords(List<uint> glyphIndexes, out int recordCount)
		{
			FontEngine.GenericListToMarshallingArray<uint>(ref glyphIndexes, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			FontEngine.PopulatePairAdjustmentRecordMarshallingArray_from_KernTable(FontEngine.s_GlyphIndexes_MarshallingArray_A, out recordCount);
			bool flag = recordCount == 0;
			GlyphPairAdjustmentRecord[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				FontEngine.SetMarshallingArraySize<GlyphPairAdjustmentRecord>(ref FontEngine.s_PairAdjustmentRecords_MarshallingArray, recordCount);
				FontEngine.GetPairAdjustmentRecordsFromMarshallingArray(FontEngine.s_PairAdjustmentRecords_MarshallingArray);
				FontEngine.s_PairAdjustmentRecords_MarshallingArray[recordCount] = default(GlyphPairAdjustmentRecord);
				array = FontEngine.s_PairAdjustmentRecords_MarshallingArray;
			}
			return array;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004D60 File Offset: 0x00002F60
		[NativeMethod(Name = "TextCore::FontEngine::PopulatePairAdjustmentRecordMarshallingArrayFromKernTable", IsFreeFunction = true)]
		private unsafe static int PopulatePairAdjustmentRecordMarshallingArray_from_KernTable(uint[] glyphIndexes, out int recordCount)
		{
			Span<uint> span = new Span<uint>(glyphIndexes);
			int num;
			fixed (uint* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				num = FontEngine.PopulatePairAdjustmentRecordMarshallingArray_from_KernTable_Injected(ref managedSpanWrapper, out recordCount);
			}
			return num;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004D9C File Offset: 0x00002F9C
		internal static GlyphPairAdjustmentRecord[] GetGlyphPairAdjustmentRecords(uint glyphIndex, out int recordCount)
		{
			FontEngine.PopulatePairAdjustmentRecordMarshallingArray_from_GlyphIndex(glyphIndex, out recordCount);
			bool flag = recordCount == 0;
			GlyphPairAdjustmentRecord[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				FontEngine.SetMarshallingArraySize<GlyphPairAdjustmentRecord>(ref FontEngine.s_PairAdjustmentRecords_MarshallingArray, recordCount);
				FontEngine.GetPairAdjustmentRecordsFromMarshallingArray(FontEngine.s_PairAdjustmentRecords_MarshallingArray);
				FontEngine.s_PairAdjustmentRecords_MarshallingArray[recordCount] = default(GlyphPairAdjustmentRecord);
				array = FontEngine.s_PairAdjustmentRecords_MarshallingArray;
			}
			return array;
		}

		// Token: 0x060000DD RID: 221
		[NativeMethod(Name = "TextCore::FontEngine::PopulatePairAdjustmentRecordMarshallingArrayFromKernTable", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PopulatePairAdjustmentRecordMarshallingArray_from_GlyphIndex(uint glyphIndex, out int recordCount);

		// Token: 0x060000DE RID: 222 RVA: 0x00004DF8 File Offset: 0x00002FF8
		internal static GlyphPairAdjustmentRecord[] GetGlyphPairAdjustmentRecords(List<uint> newGlyphIndexes, List<uint> allGlyphIndexes)
		{
			FontEngine.GenericListToMarshallingArray<uint>(ref newGlyphIndexes, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			FontEngine.GenericListToMarshallingArray<uint>(ref allGlyphIndexes, ref FontEngine.s_GlyphIndexes_MarshallingArray_B);
			int num;
			FontEngine.PopulatePairAdjustmentRecordMarshallingArray_for_NewlyAddedGlyphIndexes(FontEngine.s_GlyphIndexes_MarshallingArray_A, FontEngine.s_GlyphIndexes_MarshallingArray_B, out num);
			bool flag = num == 0;
			GlyphPairAdjustmentRecord[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				FontEngine.SetMarshallingArraySize<GlyphPairAdjustmentRecord>(ref FontEngine.s_PairAdjustmentRecords_MarshallingArray, num);
				FontEngine.GetPairAdjustmentRecordsFromMarshallingArray(FontEngine.s_PairAdjustmentRecords_MarshallingArray);
				FontEngine.s_PairAdjustmentRecords_MarshallingArray[num] = default(GlyphPairAdjustmentRecord);
				array = FontEngine.s_PairAdjustmentRecords_MarshallingArray;
			}
			return array;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004E74 File Offset: 0x00003074
		[NativeMethod(Name = "TextCore::FontEngine::PopulatePairAdjustmentRecordMarshallingArrayFromKernTable", IsFreeFunction = true)]
		private unsafe static int PopulatePairAdjustmentRecordMarshallingArray_for_NewlyAddedGlyphIndexes(uint[] newGlyphIndexes, uint[] allGlyphIndexes, out int recordCount)
		{
			Span<uint> span = new Span<uint>(newGlyphIndexes);
			fixed (uint* ptr = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, span.Length);
				Span<uint> span2 = new Span<uint>(allGlyphIndexes);
				int num;
				fixed (uint* pinnableReference = span2.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper2 = new ManagedSpanWrapper((void*)pinnableReference, span2.Length);
					num = FontEngine.PopulatePairAdjustmentRecordMarshallingArray_for_NewlyAddedGlyphIndexes_Injected(ref managedSpanWrapper, ref managedSpanWrapper2, out recordCount);
					ptr = null;
				}
				return num;
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004ED8 File Offset: 0x000030D8
		[NativeMethod(Name = "TextCore::FontEngine::GetGlyphPairAdjustmentRecord", IsFreeFunction = true)]
		internal static GlyphPairAdjustmentRecord GetGlyphPairAdjustmentRecord(uint firstGlyphIndex, uint secondGlyphIndex)
		{
			GlyphPairAdjustmentRecord glyphPairAdjustmentRecord;
			FontEngine.GetGlyphPairAdjustmentRecord_Injected(firstGlyphIndex, secondGlyphIndex, out glyphPairAdjustmentRecord);
			return glyphPairAdjustmentRecord;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004EF0 File Offset: 0x000030F0
		internal static GlyphAdjustmentRecord[] GetSingleAdjustmentRecords(int lookupIndex, uint glyphIndex)
		{
			bool flag = FontEngine.s_GlyphIndexes_MarshallingArray_A == null;
			if (flag)
			{
				FontEngine.s_GlyphIndexes_MarshallingArray_A = new uint[8];
			}
			FontEngine.s_GlyphIndexes_MarshallingArray_A[0] = glyphIndex;
			FontEngine.s_GlyphIndexes_MarshallingArray_A[1] = 0U;
			return FontEngine.GetSingleAdjustmentRecords(lookupIndex, FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004F34 File Offset: 0x00003134
		internal static GlyphAdjustmentRecord[] GetSingleAdjustmentRecords(int lookupIndex, List<uint> glyphIndexes)
		{
			FontEngine.GenericListToMarshallingArray<uint>(ref glyphIndexes, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetSingleAdjustmentRecords(lookupIndex, FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004F60 File Offset: 0x00003160
		private static GlyphAdjustmentRecord[] GetSingleAdjustmentRecords(int lookupIndex, uint[] glyphIndexes)
		{
			int num;
			FontEngine.PopulateSingleAdjustmentRecordMarshallingArray_from_GlyphIndexes(glyphIndexes, lookupIndex, out num);
			bool flag = num == 0;
			GlyphAdjustmentRecord[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				FontEngine.SetMarshallingArraySize<GlyphAdjustmentRecord>(ref FontEngine.s_SingleAdjustmentRecords_MarshallingArray, num);
				FontEngine.GetSingleAdjustmentRecordsFromMarshallingArray(FontEngine.s_SingleAdjustmentRecords_MarshallingArray.AsSpan<GlyphAdjustmentRecord>());
				FontEngine.s_SingleAdjustmentRecords_MarshallingArray[num] = default(GlyphAdjustmentRecord);
				array = FontEngine.s_SingleAdjustmentRecords_MarshallingArray;
			}
			return array;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004FBC File Offset: 0x000031BC
		[NativeMethod(Name = "TextCore::FontEngine::PopulateSingleAdjustmentRecordMarshallingArray", IsFreeFunction = true)]
		private unsafe static int PopulateSingleAdjustmentRecordMarshallingArray_from_GlyphIndexes(uint[] glyphIndexes, int lookupIndex, out int recordCount)
		{
			Span<uint> span = new Span<uint>(glyphIndexes);
			int num;
			fixed (uint* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				num = FontEngine.PopulateSingleAdjustmentRecordMarshallingArray_from_GlyphIndexes_Injected(ref managedSpanWrapper, lookupIndex, out recordCount);
			}
			return num;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004FF8 File Offset: 0x000031F8
		[NativeMethod(Name = "TextCore::FontEngine::GetSingleAdjustmentRecordsFromMarshallingArray", IsFreeFunction = true)]
		private unsafe static int GetSingleAdjustmentRecordsFromMarshallingArray(Span<GlyphAdjustmentRecord> singleSubstitutionRecords)
		{
			Span<GlyphAdjustmentRecord> span = singleSubstitutionRecords;
			int singleAdjustmentRecordsFromMarshallingArray_Injected;
			fixed (GlyphAdjustmentRecord* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				singleAdjustmentRecordsFromMarshallingArray_Injected = FontEngine.GetSingleAdjustmentRecordsFromMarshallingArray_Injected(ref managedSpanWrapper);
			}
			return singleAdjustmentRecordsFromMarshallingArray_Injected;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000502C File Offset: 0x0000322C
		[NativeMethod(Name = "TextCore::FontEngine::GetPairAdjustmentRecords", IsThreadSafe = true, IsFreeFunction = true)]
		[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
		internal static GlyphPairAdjustmentRecord[] GetPairAdjustmentRecords(uint glyphIndex)
		{
			GlyphPairAdjustmentRecord[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				FontEngine.GetPairAdjustmentRecords_Injected(glyphIndex, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				GlyphPairAdjustmentRecord[] array;
				blittableArrayWrapper.Unmarshal<GlyphPairAdjustmentRecord>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00005060 File Offset: 0x00003260
		[NativeMethod(Name = "TextCore::FontEngine::GetPairAdjustmentRecord", IsThreadSafe = true, IsFreeFunction = true)]
		internal static GlyphPairAdjustmentRecord GetPairAdjustmentRecord(uint firstGlyphIndex, uint secondGlyphIndex)
		{
			GlyphPairAdjustmentRecord glyphPairAdjustmentRecord;
			FontEngine.GetPairAdjustmentRecord_Injected(firstGlyphIndex, secondGlyphIndex, out glyphPairAdjustmentRecord);
			return glyphPairAdjustmentRecord;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00005078 File Offset: 0x00003278
		[NativeMethod(Name = "TextCore::FontEngine::GetAllPairAdjustmentRecords", IsThreadSafe = true, IsFreeFunction = true)]
		[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
		internal static GlyphPairAdjustmentRecord[] GetAllPairAdjustmentRecords()
		{
			GlyphPairAdjustmentRecord[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				FontEngine.GetAllPairAdjustmentRecords_Injected(out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				GlyphPairAdjustmentRecord[] array;
				blittableArrayWrapper.Unmarshal<GlyphPairAdjustmentRecord>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000050AC File Offset: 0x000032AC
		[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
		internal static GlyphPairAdjustmentRecord[] GetPairAdjustmentRecords(List<uint> glyphIndexes)
		{
			FontEngine.GenericListToMarshallingArray<uint>(ref glyphIndexes, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetPairAdjustmentRecords(FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000050D8 File Offset: 0x000032D8
		internal static GlyphPairAdjustmentRecord[] GetPairAdjustmentRecords(int lookupIndex, uint glyphIndex)
		{
			FontEngine.GlyphIndexToMarshallingArray(glyphIndex, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetPairAdjustmentRecords(lookupIndex, FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00005104 File Offset: 0x00003304
		internal static GlyphPairAdjustmentRecord[] GetPairAdjustmentRecords(int lookupIndex, List<uint> glyphIndexes)
		{
			FontEngine.GenericListToMarshallingArray<uint>(ref glyphIndexes, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetPairAdjustmentRecords(lookupIndex, FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00005130 File Offset: 0x00003330
		private static GlyphPairAdjustmentRecord[] GetPairAdjustmentRecords(uint[] glyphIndexes)
		{
			int num;
			FontEngine.PopulatePairAdjustmentRecordMarshallingArray(glyphIndexes, out num);
			bool flag = num == 0;
			GlyphPairAdjustmentRecord[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				FontEngine.SetMarshallingArraySize<GlyphPairAdjustmentRecord>(ref FontEngine.s_PairAdjustmentRecords_MarshallingArray, num);
				FontEngine.GetPairAdjustmentRecordsFromMarshallingArray(FontEngine.s_PairAdjustmentRecords_MarshallingArray);
				FontEngine.s_PairAdjustmentRecords_MarshallingArray[num] = default(GlyphPairAdjustmentRecord);
				array = FontEngine.s_PairAdjustmentRecords_MarshallingArray;
			}
			return array;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000518C File Offset: 0x0000338C
		private static GlyphPairAdjustmentRecord[] GetPairAdjustmentRecords(int lookupIndex, uint[] glyphIndexes)
		{
			int num;
			FontEngine.PopulatePairAdjustmentRecordMarshallingArray_for_LookupIndex(glyphIndexes, lookupIndex, out num);
			bool flag = num == 0;
			GlyphPairAdjustmentRecord[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				FontEngine.SetMarshallingArraySize<GlyphPairAdjustmentRecord>(ref FontEngine.s_PairAdjustmentRecords_MarshallingArray, num);
				FontEngine.GetPairAdjustmentRecordsFromMarshallingArray(FontEngine.s_PairAdjustmentRecords_MarshallingArray);
				FontEngine.s_PairAdjustmentRecords_MarshallingArray[num] = default(GlyphPairAdjustmentRecord);
				array = FontEngine.s_PairAdjustmentRecords_MarshallingArray;
			}
			return array;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000051E8 File Offset: 0x000033E8
		[NativeMethod(Name = "TextCore::FontEngine::PopulatePairAdjustmentRecordMarshallingArray", IsFreeFunction = true)]
		private unsafe static int PopulatePairAdjustmentRecordMarshallingArray(uint[] glyphIndexes, out int recordCount)
		{
			Span<uint> span = new Span<uint>(glyphIndexes);
			int num;
			fixed (uint* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				num = FontEngine.PopulatePairAdjustmentRecordMarshallingArray_Injected(ref managedSpanWrapper, out recordCount);
			}
			return num;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00005224 File Offset: 0x00003424
		[NativeMethod(Name = "TextCore::FontEngine::PopulatePairAdjustmentRecordMarshallingArray", IsFreeFunction = true)]
		private unsafe static int PopulatePairAdjustmentRecordMarshallingArray_for_LookupIndex(uint[] glyphIndexes, int lookupIndex, out int recordCount)
		{
			Span<uint> span = new Span<uint>(glyphIndexes);
			int num;
			fixed (uint* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				num = FontEngine.PopulatePairAdjustmentRecordMarshallingArray_for_LookupIndex_Injected(ref managedSpanWrapper, lookupIndex, out recordCount);
			}
			return num;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00005260 File Offset: 0x00003460
		[NativeMethod(Name = "TextCore::FontEngine::GetGlyphPairAdjustmentRecordsFromMarshallingArray", IsFreeFunction = true)]
		private unsafe static int GetPairAdjustmentRecordsFromMarshallingArray(Span<GlyphPairAdjustmentRecord> glyphPairAdjustmentRecords)
		{
			Span<GlyphPairAdjustmentRecord> span = glyphPairAdjustmentRecords;
			int pairAdjustmentRecordsFromMarshallingArray_Injected;
			fixed (GlyphPairAdjustmentRecord* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				pairAdjustmentRecordsFromMarshallingArray_Injected = FontEngine.GetPairAdjustmentRecordsFromMarshallingArray_Injected(ref managedSpanWrapper);
			}
			return pairAdjustmentRecordsFromMarshallingArray_Injected;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00005294 File Offset: 0x00003494
		[NativeMethod(Name = "TextCore::FontEngine::GetAllMarkToBaseAdjustmentRecords", IsThreadSafe = true, IsFreeFunction = true)]
		[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
		internal static MarkToBaseAdjustmentRecord[] GetAllMarkToBaseAdjustmentRecords()
		{
			MarkToBaseAdjustmentRecord[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				FontEngine.GetAllMarkToBaseAdjustmentRecords_Injected(out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				MarkToBaseAdjustmentRecord[] array;
				blittableArrayWrapper.Unmarshal<MarkToBaseAdjustmentRecord>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000052C8 File Offset: 0x000034C8
		[NativeMethod(Name = "TextCore::FontEngine::GetMarkToBaseAdjustmentRecords", IsThreadSafe = true, IsFreeFunction = true)]
		internal static MarkToBaseAdjustmentRecord[] GetMarkToBaseAdjustmentRecords(uint baseGlyphIndex)
		{
			MarkToBaseAdjustmentRecord[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				FontEngine.GetMarkToBaseAdjustmentRecords_Injected(baseGlyphIndex, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				MarkToBaseAdjustmentRecord[] array;
				blittableArrayWrapper.Unmarshal<MarkToBaseAdjustmentRecord>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000052FC File Offset: 0x000034FC
		[NativeMethod(Name = "TextCore::FontEngine::GetMarkToBaseAdjustmentRecord", IsFreeFunction = true)]
		internal static MarkToBaseAdjustmentRecord GetMarkToBaseAdjustmentRecord(uint baseGlyphIndex, uint markGlyphIndex)
		{
			MarkToBaseAdjustmentRecord markToBaseAdjustmentRecord;
			FontEngine.GetMarkToBaseAdjustmentRecord_Injected(baseGlyphIndex, markGlyphIndex, out markToBaseAdjustmentRecord);
			return markToBaseAdjustmentRecord;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00005314 File Offset: 0x00003514
		[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
		internal static MarkToBaseAdjustmentRecord[] GetMarkToBaseAdjustmentRecords(List<uint> glyphIndexes)
		{
			FontEngine.GenericListToMarshallingArray<uint>(ref glyphIndexes, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetMarkToBaseAdjustmentRecords(FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00005340 File Offset: 0x00003540
		internal static MarkToBaseAdjustmentRecord[] GetMarkToBaseAdjustmentRecords(int lookupIndex, List<uint> glyphIndexes)
		{
			FontEngine.GenericListToMarshallingArray<uint>(ref glyphIndexes, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetMarkToBaseAdjustmentRecords(lookupIndex, FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000536C File Offset: 0x0000356C
		private static MarkToBaseAdjustmentRecord[] GetMarkToBaseAdjustmentRecords(uint[] glyphIndexes)
		{
			int num;
			FontEngine.PopulateMarkToBaseAdjustmentRecordMarshallingArray(glyphIndexes, out num);
			bool flag = num == 0;
			MarkToBaseAdjustmentRecord[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				FontEngine.SetMarshallingArraySize<MarkToBaseAdjustmentRecord>(ref FontEngine.s_MarkToBaseAdjustmentRecords_MarshallingArray, num);
				FontEngine.GetMarkToBaseAdjustmentRecordsFromMarshallingArray(FontEngine.s_MarkToBaseAdjustmentRecords_MarshallingArray);
				FontEngine.s_MarkToBaseAdjustmentRecords_MarshallingArray[num] = default(MarkToBaseAdjustmentRecord);
				array = FontEngine.s_MarkToBaseAdjustmentRecords_MarshallingArray;
			}
			return array;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000053C8 File Offset: 0x000035C8
		private static MarkToBaseAdjustmentRecord[] GetMarkToBaseAdjustmentRecords(int lookupIndex, uint[] glyphIndexes)
		{
			int num;
			FontEngine.PopulateMarkToBaseAdjustmentRecordMarshallingArray_for_LookupIndex(glyphIndexes, lookupIndex, out num);
			bool flag = num == 0;
			MarkToBaseAdjustmentRecord[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				FontEngine.SetMarshallingArraySize<MarkToBaseAdjustmentRecord>(ref FontEngine.s_MarkToBaseAdjustmentRecords_MarshallingArray, num);
				FontEngine.GetMarkToBaseAdjustmentRecordsFromMarshallingArray(FontEngine.s_MarkToBaseAdjustmentRecords_MarshallingArray);
				FontEngine.s_MarkToBaseAdjustmentRecords_MarshallingArray[num] = default(MarkToBaseAdjustmentRecord);
				array = FontEngine.s_MarkToBaseAdjustmentRecords_MarshallingArray;
			}
			return array;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00005424 File Offset: 0x00003624
		[NativeMethod(Name = "TextCore::FontEngine::PopulateMarkToBaseAdjustmentRecordMarshallingArray", IsFreeFunction = true)]
		private unsafe static int PopulateMarkToBaseAdjustmentRecordMarshallingArray(uint[] glyphIndexes, out int recordCount)
		{
			Span<uint> span = new Span<uint>(glyphIndexes);
			int num;
			fixed (uint* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				num = FontEngine.PopulateMarkToBaseAdjustmentRecordMarshallingArray_Injected(ref managedSpanWrapper, out recordCount);
			}
			return num;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00005460 File Offset: 0x00003660
		[NativeMethod(Name = "TextCore::FontEngine::PopulateMarkToBaseAdjustmentRecordMarshallingArray", IsFreeFunction = true)]
		private unsafe static int PopulateMarkToBaseAdjustmentRecordMarshallingArray_for_LookupIndex(uint[] glyphIndexes, int lookupIndex, out int recordCount)
		{
			Span<uint> span = new Span<uint>(glyphIndexes);
			int num;
			fixed (uint* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				num = FontEngine.PopulateMarkToBaseAdjustmentRecordMarshallingArray_for_LookupIndex_Injected(ref managedSpanWrapper, lookupIndex, out recordCount);
			}
			return num;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000549C File Offset: 0x0000369C
		[NativeMethod(Name = "TextCore::FontEngine::GetMarkToBaseAdjustmentRecordsFromMarshallingArray", IsFreeFunction = true)]
		private unsafe static int GetMarkToBaseAdjustmentRecordsFromMarshallingArray(Span<MarkToBaseAdjustmentRecord> adjustmentRecords)
		{
			Span<MarkToBaseAdjustmentRecord> span = adjustmentRecords;
			int markToBaseAdjustmentRecordsFromMarshallingArray_Injected;
			fixed (MarkToBaseAdjustmentRecord* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				markToBaseAdjustmentRecordsFromMarshallingArray_Injected = FontEngine.GetMarkToBaseAdjustmentRecordsFromMarshallingArray_Injected(ref managedSpanWrapper);
			}
			return markToBaseAdjustmentRecordsFromMarshallingArray_Injected;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000054D0 File Offset: 0x000036D0
		[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
		[NativeMethod(Name = "TextCore::FontEngine::GetAllMarkToMarkAdjustmentRecords", IsThreadSafe = true, IsFreeFunction = true)]
		internal static MarkToMarkAdjustmentRecord[] GetAllMarkToMarkAdjustmentRecords()
		{
			MarkToMarkAdjustmentRecord[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				FontEngine.GetAllMarkToMarkAdjustmentRecords_Injected(out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				MarkToMarkAdjustmentRecord[] array;
				blittableArrayWrapper.Unmarshal<MarkToMarkAdjustmentRecord>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005504 File Offset: 0x00003704
		[NativeMethod(Name = "TextCore::FontEngine::GetMarkToMarkAdjustmentRecords", IsThreadSafe = true, IsFreeFunction = true)]
		internal static MarkToMarkAdjustmentRecord[] GetMarkToMarkAdjustmentRecords(uint baseMarkGlyphIndex)
		{
			MarkToMarkAdjustmentRecord[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				FontEngine.GetMarkToMarkAdjustmentRecords_Injected(baseMarkGlyphIndex, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				MarkToMarkAdjustmentRecord[] array;
				blittableArrayWrapper.Unmarshal<MarkToMarkAdjustmentRecord>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005538 File Offset: 0x00003738
		[NativeMethod(Name = "TextCore::FontEngine::GetMarkToMarkAdjustmentRecord", IsFreeFunction = true)]
		internal static MarkToMarkAdjustmentRecord GetMarkToMarkAdjustmentRecord(uint firstGlyphIndex, uint secondGlyphIndex)
		{
			MarkToMarkAdjustmentRecord markToMarkAdjustmentRecord;
			FontEngine.GetMarkToMarkAdjustmentRecord_Injected(firstGlyphIndex, secondGlyphIndex, out markToMarkAdjustmentRecord);
			return markToMarkAdjustmentRecord;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005550 File Offset: 0x00003750
		[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
		internal static MarkToMarkAdjustmentRecord[] GetMarkToMarkAdjustmentRecords(List<uint> glyphIndexes)
		{
			FontEngine.GenericListToMarshallingArray<uint>(ref glyphIndexes, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetMarkToMarkAdjustmentRecords(FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000557C File Offset: 0x0000377C
		internal static MarkToMarkAdjustmentRecord[] GetMarkToMarkAdjustmentRecords(int lookupIndex, List<uint> glyphIndexes)
		{
			FontEngine.GenericListToMarshallingArray<uint>(ref glyphIndexes, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetMarkToMarkAdjustmentRecords(lookupIndex, FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000055A8 File Offset: 0x000037A8
		private static MarkToMarkAdjustmentRecord[] GetMarkToMarkAdjustmentRecords(uint[] glyphIndexes)
		{
			int num;
			FontEngine.PopulateMarkToMarkAdjustmentRecordMarshallingArray(FontEngine.s_GlyphIndexes_MarshallingArray_A, out num);
			bool flag = num == 0;
			MarkToMarkAdjustmentRecord[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				FontEngine.SetMarshallingArraySize<MarkToMarkAdjustmentRecord>(ref FontEngine.s_MarkToMarkAdjustmentRecords_MarshallingArray, num);
				FontEngine.GetMarkToMarkAdjustmentRecordsFromMarshallingArray(FontEngine.s_MarkToMarkAdjustmentRecords_MarshallingArray);
				FontEngine.s_MarkToMarkAdjustmentRecords_MarshallingArray[num] = default(MarkToMarkAdjustmentRecord);
				array = FontEngine.s_MarkToMarkAdjustmentRecords_MarshallingArray;
			}
			return array;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005608 File Offset: 0x00003808
		private static MarkToMarkAdjustmentRecord[] GetMarkToMarkAdjustmentRecords(int lookupIndex, uint[] glyphIndexes)
		{
			int num;
			FontEngine.PopulateMarkToMarkAdjustmentRecordMarshallingArray_for_LookupIndex(FontEngine.s_GlyphIndexes_MarshallingArray_A, lookupIndex, out num);
			bool flag = num == 0;
			MarkToMarkAdjustmentRecord[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				FontEngine.SetMarshallingArraySize<MarkToMarkAdjustmentRecord>(ref FontEngine.s_MarkToMarkAdjustmentRecords_MarshallingArray, num);
				FontEngine.GetMarkToMarkAdjustmentRecordsFromMarshallingArray(FontEngine.s_MarkToMarkAdjustmentRecords_MarshallingArray);
				FontEngine.s_MarkToMarkAdjustmentRecords_MarshallingArray[num] = default(MarkToMarkAdjustmentRecord);
				array = FontEngine.s_MarkToMarkAdjustmentRecords_MarshallingArray;
			}
			return array;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005668 File Offset: 0x00003868
		[NativeMethod(Name = "TextCore::FontEngine::PopulateMarkToMarkAdjustmentRecordMarshallingArray", IsFreeFunction = true)]
		private unsafe static int PopulateMarkToMarkAdjustmentRecordMarshallingArray(uint[] glyphIndexes, out int recordCount)
		{
			Span<uint> span = new Span<uint>(glyphIndexes);
			int num;
			fixed (uint* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				num = FontEngine.PopulateMarkToMarkAdjustmentRecordMarshallingArray_Injected(ref managedSpanWrapper, out recordCount);
			}
			return num;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000056A4 File Offset: 0x000038A4
		[NativeMethod(Name = "TextCore::FontEngine::PopulateMarkToMarkAdjustmentRecordMarshallingArray", IsFreeFunction = true)]
		private unsafe static int PopulateMarkToMarkAdjustmentRecordMarshallingArray_for_LookupIndex(uint[] glyphIndexes, int lookupIndex, out int recordCount)
		{
			Span<uint> span = new Span<uint>(glyphIndexes);
			int num;
			fixed (uint* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				num = FontEngine.PopulateMarkToMarkAdjustmentRecordMarshallingArray_for_LookupIndex_Injected(ref managedSpanWrapper, lookupIndex, out recordCount);
			}
			return num;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000056E0 File Offset: 0x000038E0
		[NativeMethod(Name = "TextCore::FontEngine::GetMarkToMarkAdjustmentRecordsFromMarshallingArray", IsFreeFunction = true)]
		private unsafe static int GetMarkToMarkAdjustmentRecordsFromMarshallingArray(Span<MarkToMarkAdjustmentRecord> adjustmentRecords)
		{
			Span<MarkToMarkAdjustmentRecord> span = adjustmentRecords;
			int markToMarkAdjustmentRecordsFromMarshallingArray_Injected;
			fixed (MarkToMarkAdjustmentRecord* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				markToMarkAdjustmentRecordsFromMarshallingArray_Injected = FontEngine.GetMarkToMarkAdjustmentRecordsFromMarshallingArray_Injected(ref managedSpanWrapper);
			}
			return markToMarkAdjustmentRecordsFromMarshallingArray_Injected;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005714 File Offset: 0x00003914
		private static void GlyphIndexToMarshallingArray(uint glyphIndex, ref uint[] dstArray)
		{
			bool flag = dstArray == null || dstArray.Length == 1;
			if (flag)
			{
				dstArray = new uint[8];
			}
			dstArray[0] = glyphIndex;
			dstArray[1] = 0U;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00005748 File Offset: 0x00003948
		private static void GenericListToMarshallingArray<T>(ref List<T> srcList, ref T[] dstArray)
		{
			int count = srcList.Count;
			bool flag = dstArray == null || dstArray.Length <= count;
			if (flag)
			{
				int num = Mathf.NextPowerOfTwo(count + 1);
				bool flag2 = dstArray == null;
				if (flag2)
				{
					dstArray = new T[num];
				}
				else
				{
					Array.Resize<T>(ref dstArray, num);
				}
			}
			for (int i = 0; i < count; i++)
			{
				dstArray[i] = srcList[i];
			}
			dstArray[count] = default(T);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000057D4 File Offset: 0x000039D4
		private static void SetMarshallingArraySize<T>(ref T[] marshallingArray, int recordCount)
		{
			bool flag = marshallingArray == null || marshallingArray.Length <= recordCount;
			if (flag)
			{
				int num = Mathf.NextPowerOfTwo(recordCount + 1);
				bool flag2 = marshallingArray == null;
				if (flag2)
				{
					marshallingArray = new T[num];
				}
				else
				{
					Array.Resize<T>(ref marshallingArray, num);
				}
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005820 File Offset: 0x00003A20
		[NativeMethod(Name = "TextCore::FontEngine::ResetAtlasTexture", IsFreeFunction = true)]
		[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
		internal static void ResetAtlasTexture(Texture2D texture)
		{
			FontEngine.ResetAtlasTexture_Injected(Object.MarshalledUnityObject.Marshal<Texture2D>(texture));
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005838 File Offset: 0x00003A38
		[NativeMethod(Name = "TextCore::FontEngine::RenderToTexture", IsFreeFunction = true)]
		internal static void RenderBufferToTexture(Texture2D srcTexture, int padding, GlyphRenderMode renderMode, Texture2D dstTexture)
		{
			FontEngine.RenderBufferToTexture_Injected(Object.MarshalledUnityObject.Marshal<Texture2D>(srcTexture), padding, renderMode, Object.MarshalledUnityObject.Marshal<Texture2D>(dstTexture));
		}

		// Token: 0x0600010B RID: 267
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int LoadFontFace_Internal_Injected(ref ManagedSpanWrapper filePath);

		// Token: 0x0600010C RID: 268
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int LoadFontFace_With_Size_Internal_Injected(ref ManagedSpanWrapper filePath, int pointSize);

		// Token: 0x0600010D RID: 269
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int LoadFontFace_With_Size_And_FaceIndex_Internal_Injected(ref ManagedSpanWrapper filePath, int pointSize, int faceIndex);

		// Token: 0x0600010E RID: 270
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int LoadFontFace_FromSourceFontFile_Internal_Injected(ref ManagedSpanWrapper sourceFontFile);

		// Token: 0x0600010F RID: 271
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int LoadFontFace_With_Size_FromSourceFontFile_Internal_Injected(ref ManagedSpanWrapper sourceFontFile, int pointSize);

		// Token: 0x06000110 RID: 272
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int LoadFontFace_With_Size_And_FaceIndex_FromSourceFontFile_Internal_Injected(ref ManagedSpanWrapper sourceFontFile, int pointSize, int faceIndex);

		// Token: 0x06000111 RID: 273
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int LoadFontFace_FromFont_Internal_Injected(IntPtr font);

		// Token: 0x06000112 RID: 274
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int LoadFontFace_With_Size_FromFont_Internal_Injected(IntPtr font, int pointSize);

		// Token: 0x06000113 RID: 275
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int LoadFontFace_With_Size_and_FaceIndex_FromFont_Internal_Injected(IntPtr font, int pointSize, int faceIndex);

		// Token: 0x06000114 RID: 276
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int LoadFontFace_by_FamilyName_and_StyleName_Internal_Injected(ref ManagedSpanWrapper familyName, ref ManagedSpanWrapper styleName);

		// Token: 0x06000115 RID: 277
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int LoadFontFace_With_Size_by_FamilyName_and_StyleName_Internal_Injected(ref ManagedSpanWrapper familyName, ref ManagedSpanWrapper styleName, int pointSize);

		// Token: 0x06000116 RID: 278
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryGetSystemFontReference_Internal_Injected(ref ManagedSpanWrapper familyName, ref ManagedSpanWrapper styleName, out FontReference fontRef);

		// Token: 0x06000117 RID: 279
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryPackGlyphInAtlas_Internal_Injected(ref GlyphMarshallingStruct glyph, int padding, GlyphPackingMode packingMode, GlyphRenderMode renderMode, int width, int height, out BlittableArrayWrapper freeGlyphRects, ref int freeGlyphRectCount, out BlittableArrayWrapper usedGlyphRects, ref int usedGlyphRectCount);

		// Token: 0x06000118 RID: 280
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryPackGlyphsInAtlas_Internal_Injected(out BlittableArrayWrapper glyphsToAdd, ref int glyphsToAddCount, out BlittableArrayWrapper glyphsAdded, ref int glyphsAddedCount, int padding, GlyphPackingMode packingMode, GlyphRenderMode renderMode, int width, int height, out BlittableArrayWrapper freeGlyphRects, ref int freeGlyphRectCount, out BlittableArrayWrapper usedGlyphRects, ref int usedGlyphRectCount);

		// Token: 0x06000119 RID: 281
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int RenderGlyphToTexture_Internal_Injected([In] ref GlyphMarshallingStruct glyphStruct, int padding, GlyphRenderMode renderMode, IntPtr texture);

		// Token: 0x0600011A RID: 282
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int RenderGlyphsToTexture_Internal_Injected(ref ManagedSpanWrapper glyphs, int glyphCount, int padding, GlyphRenderMode renderMode, IntPtr texture);

		// Token: 0x0600011B RID: 283
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int RenderGlyphsToTextureBuffer_Internal_Injected(ref ManagedSpanWrapper glyphs, int glyphCount, int padding, GlyphRenderMode renderMode, out BlittableArrayWrapper texBuffer, int texWidth, int texHeight);

		// Token: 0x0600011C RID: 284
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int RenderGlyphsToSharedTexture_Internal_Injected(ref ManagedSpanWrapper glyphs, int glyphCount, int padding, GlyphRenderMode renderMode);

		// Token: 0x0600011D RID: 285
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSharedTexture_Injected(IntPtr texture);

		// Token: 0x0600011E RID: 286
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryAddGlyphToTexture_Internal_Injected(uint glyphIndex, int padding, GlyphPackingMode packingMode, out BlittableArrayWrapper freeGlyphRects, ref int freeGlyphRectCount, out BlittableArrayWrapper usedGlyphRects, ref int usedGlyphRectCount, GlyphRenderMode renderMode, IntPtr texture, out GlyphMarshallingStruct glyph);

		// Token: 0x0600011F RID: 287
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryAddGlyphsToTexture_Internal_MultiThread_Injected(out BlittableArrayWrapper glyphsToAdd, ref int glyphsToAddCount, out BlittableArrayWrapper glyphsAdded, ref int glyphsAddedCount, int padding, GlyphPackingMode packingMode, out BlittableArrayWrapper freeGlyphRects, ref int freeGlyphRectCount, out BlittableArrayWrapper usedGlyphRects, ref int usedGlyphRectCount, GlyphRenderMode renderMode, IntPtr texture);

		// Token: 0x06000120 RID: 288
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryAddGlyphsToTexture_Internal_Injected(ref ManagedSpanWrapper glyphIndex, int padding, GlyphPackingMode packingMode, out BlittableArrayWrapper freeGlyphRects, ref int freeGlyphRectCount, out BlittableArrayWrapper usedGlyphRects, ref int usedGlyphRectCount, GlyphRenderMode renderMode, IntPtr texture, out BlittableArrayWrapper glyphs, ref int glyphCount);

		// Token: 0x06000121 RID: 289
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetOpenTypeLayoutTable_Injected(OTL_TableType type, out OTL_Table ret);

		// Token: 0x06000122 RID: 290
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetOpenTypeLayoutLookups_Injected(out BlittableArrayWrapper ret);

		// Token: 0x06000123 RID: 291
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetAllSingleSubstitutionRecords_Injected(out BlittableArrayWrapper ret);

		// Token: 0x06000124 RID: 292
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PopulateSingleSubstitutionRecordMarshallingArray_from_GlyphIndexes_Injected(ref ManagedSpanWrapper glyphIndexes, int lookupIndex, out int recordCount);

		// Token: 0x06000125 RID: 293
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetSingleSubstitutionRecordsFromMarshallingArray_Injected(ref ManagedSpanWrapper singleSubstitutionRecords);

		// Token: 0x06000126 RID: 294
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PopulateMultipleSubstitutionRecordMarshallingArray_from_GlyphIndexes_Injected(ref ManagedSpanWrapper glyphIndexes, int lookupIndex, out int recordCount);

		// Token: 0x06000127 RID: 295
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PopulateAlternateSubstitutionRecordMarshallingArray_from_GlyphIndexes_Injected(ref ManagedSpanWrapper glyphIndexes, int lookupIndex, out int recordCount);

		// Token: 0x06000128 RID: 296
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PopulateLigatureSubstitutionRecordMarshallingArray_Injected(ref ManagedSpanWrapper glyphIndexes, out int recordCount);

		// Token: 0x06000129 RID: 297
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PopulateLigatureSubstitutionRecordMarshallingArray_for_LookupIndex_Injected(ref ManagedSpanWrapper glyphIndexes, int lookupIndex, out int recordCount);

		// Token: 0x0600012A RID: 298
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PopulateContextualSubstitutionRecordMarshallingArray_from_GlyphIndexes_Injected(ref ManagedSpanWrapper glyphIndexes, int lookupIndex, out int recordCount);

		// Token: 0x0600012B RID: 299
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PopulateChainingContextualSubstitutionRecordMarshallingArray_from_GlyphIndexes_Injected(ref ManagedSpanWrapper glyphIndexes, int lookupIndex, out int recordCount);

		// Token: 0x0600012C RID: 300
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PopulatePairAdjustmentRecordMarshallingArray_from_KernTable_Injected(ref ManagedSpanWrapper glyphIndexes, out int recordCount);

		// Token: 0x0600012D RID: 301
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PopulatePairAdjustmentRecordMarshallingArray_for_NewlyAddedGlyphIndexes_Injected(ref ManagedSpanWrapper newGlyphIndexes, ref ManagedSpanWrapper allGlyphIndexes, out int recordCount);

		// Token: 0x0600012E RID: 302
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGlyphPairAdjustmentRecord_Injected(uint firstGlyphIndex, uint secondGlyphIndex, out GlyphPairAdjustmentRecord ret);

		// Token: 0x0600012F RID: 303
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PopulateSingleAdjustmentRecordMarshallingArray_from_GlyphIndexes_Injected(ref ManagedSpanWrapper glyphIndexes, int lookupIndex, out int recordCount);

		// Token: 0x06000130 RID: 304
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetSingleAdjustmentRecordsFromMarshallingArray_Injected(ref ManagedSpanWrapper singleSubstitutionRecords);

		// Token: 0x06000131 RID: 305
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPairAdjustmentRecords_Injected(uint glyphIndex, out BlittableArrayWrapper ret);

		// Token: 0x06000132 RID: 306
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPairAdjustmentRecord_Injected(uint firstGlyphIndex, uint secondGlyphIndex, out GlyphPairAdjustmentRecord ret);

		// Token: 0x06000133 RID: 307
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetAllPairAdjustmentRecords_Injected(out BlittableArrayWrapper ret);

		// Token: 0x06000134 RID: 308
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PopulatePairAdjustmentRecordMarshallingArray_Injected(ref ManagedSpanWrapper glyphIndexes, out int recordCount);

		// Token: 0x06000135 RID: 309
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PopulatePairAdjustmentRecordMarshallingArray_for_LookupIndex_Injected(ref ManagedSpanWrapper glyphIndexes, int lookupIndex, out int recordCount);

		// Token: 0x06000136 RID: 310
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetPairAdjustmentRecordsFromMarshallingArray_Injected(ref ManagedSpanWrapper glyphPairAdjustmentRecords);

		// Token: 0x06000137 RID: 311
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetAllMarkToBaseAdjustmentRecords_Injected(out BlittableArrayWrapper ret);

		// Token: 0x06000138 RID: 312
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetMarkToBaseAdjustmentRecords_Injected(uint baseGlyphIndex, out BlittableArrayWrapper ret);

		// Token: 0x06000139 RID: 313
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetMarkToBaseAdjustmentRecord_Injected(uint baseGlyphIndex, uint markGlyphIndex, out MarkToBaseAdjustmentRecord ret);

		// Token: 0x0600013A RID: 314
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PopulateMarkToBaseAdjustmentRecordMarshallingArray_Injected(ref ManagedSpanWrapper glyphIndexes, out int recordCount);

		// Token: 0x0600013B RID: 315
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PopulateMarkToBaseAdjustmentRecordMarshallingArray_for_LookupIndex_Injected(ref ManagedSpanWrapper glyphIndexes, int lookupIndex, out int recordCount);

		// Token: 0x0600013C RID: 316
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMarkToBaseAdjustmentRecordsFromMarshallingArray_Injected(ref ManagedSpanWrapper adjustmentRecords);

		// Token: 0x0600013D RID: 317
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetAllMarkToMarkAdjustmentRecords_Injected(out BlittableArrayWrapper ret);

		// Token: 0x0600013E RID: 318
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetMarkToMarkAdjustmentRecords_Injected(uint baseMarkGlyphIndex, out BlittableArrayWrapper ret);

		// Token: 0x0600013F RID: 319
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetMarkToMarkAdjustmentRecord_Injected(uint firstGlyphIndex, uint secondGlyphIndex, out MarkToMarkAdjustmentRecord ret);

		// Token: 0x06000140 RID: 320
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PopulateMarkToMarkAdjustmentRecordMarshallingArray_Injected(ref ManagedSpanWrapper glyphIndexes, out int recordCount);

		// Token: 0x06000141 RID: 321
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PopulateMarkToMarkAdjustmentRecordMarshallingArray_for_LookupIndex_Injected(ref ManagedSpanWrapper glyphIndexes, int lookupIndex, out int recordCount);

		// Token: 0x06000142 RID: 322
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMarkToMarkAdjustmentRecordsFromMarshallingArray_Injected(ref ManagedSpanWrapper adjustmentRecords);

		// Token: 0x06000143 RID: 323
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResetAtlasTexture_Injected(IntPtr texture);

		// Token: 0x06000144 RID: 324
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RenderBufferToTexture_Injected(IntPtr srcTexture, int padding, GlyphRenderMode renderMode, IntPtr dstTexture);

		// Token: 0x0400007C RID: 124
		private static Glyph[] s_Glyphs = new Glyph[16];

		// Token: 0x0400007D RID: 125
		private static uint[] s_GlyphIndexes_MarshallingArray_A;

		// Token: 0x0400007E RID: 126
		private static uint[] s_GlyphIndexes_MarshallingArray_B;

		// Token: 0x0400007F RID: 127
		private static GlyphMarshallingStruct[] s_GlyphMarshallingStruct_IN = new GlyphMarshallingStruct[16];

		// Token: 0x04000080 RID: 128
		private static GlyphMarshallingStruct[] s_GlyphMarshallingStruct_OUT = new GlyphMarshallingStruct[16];

		// Token: 0x04000081 RID: 129
		private static GlyphRect[] s_FreeGlyphRects = new GlyphRect[16];

		// Token: 0x04000082 RID: 130
		private static GlyphRect[] s_UsedGlyphRects = new GlyphRect[16];

		// Token: 0x04000083 RID: 131
		private static GlyphAdjustmentRecord[] s_SingleAdjustmentRecords_MarshallingArray;

		// Token: 0x04000084 RID: 132
		private static SingleSubstitutionRecord[] s_SingleSubstitutionRecords_MarshallingArray;

		// Token: 0x04000085 RID: 133
		private static MultipleSubstitutionRecord[] s_MultipleSubstitutionRecords_MarshallingArray;

		// Token: 0x04000086 RID: 134
		private static AlternateSubstitutionRecord[] s_AlternateSubstitutionRecords_MarshallingArray;

		// Token: 0x04000087 RID: 135
		private static LigatureSubstitutionRecord[] s_LigatureSubstitutionRecords_MarshallingArray;

		// Token: 0x04000088 RID: 136
		private static ContextualSubstitutionRecord[] s_ContextualSubstitutionRecords_MarshallingArray;

		// Token: 0x04000089 RID: 137
		private static ChainingContextualSubstitutionRecord[] s_ChainingContextualSubstitutionRecords_MarshallingArray;

		// Token: 0x0400008A RID: 138
		private static GlyphPairAdjustmentRecord[] s_PairAdjustmentRecords_MarshallingArray;

		// Token: 0x0400008B RID: 139
		private static MarkToBaseAdjustmentRecord[] s_MarkToBaseAdjustmentRecords_MarshallingArray;

		// Token: 0x0400008C RID: 140
		private static MarkToMarkAdjustmentRecord[] s_MarkToMarkAdjustmentRecords_MarshallingArray;

		// Token: 0x0400008D RID: 141
		private static Dictionary<uint, Glyph> s_GlyphLookupDictionary = new Dictionary<uint, Glyph>();
	}
}
