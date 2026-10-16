using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200005E RID: 94
	[NativeHeader("Modules/TextCoreTextEngine/Native/TextSelectionService.h")]
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule", "Unity.UIElements.PlayModeTests" })]
	internal class TextSelectionService
	{
		// Token: 0x060002AB RID: 683 RVA: 0x00022B94 File Offset: 0x00020D94
		[NativeMethod(Name = "TextSelectionService::Substring")]
		internal static string Substring(IntPtr textGenerationInfo, int startIndex, int endIndex)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				TextSelectionService.Substring_Injected(textGenerationInfo, startIndex, endIndex, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x060002AC RID: 684
		[NativeMethod(Name = "TextSelectionService::SelectCurrentWord")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SelectCurrentWord(IntPtr textGenerationInfo, int currentIndex, ref int startIndex, ref int endIndex);

		// Token: 0x060002AD RID: 685
		[NativeMethod(Name = "TextSelectionService::PreviousCodePointIndex")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int PreviousCodePointIndex(IntPtr textGenerationInfo, int currentIndex);

		// Token: 0x060002AE RID: 686
		[NativeMethod(Name = "TextSelectionService::NextCodePointIndex")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int NextCodePointIndex(IntPtr textGenerationInfo, int currentIndex);

		// Token: 0x060002AF RID: 687 RVA: 0x00022BC8 File Offset: 0x00020DC8
		[NativeMethod(Name = "TextSelectionService::GetCursorLogicalIndexFromPosition")]
		internal static int GetCursorLogicalIndexFromPosition(IntPtr textGenerationInfo, Vector2 position)
		{
			return TextSelectionService.GetCursorLogicalIndexFromPosition_Injected(textGenerationInfo, ref position);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00022BE0 File Offset: 0x00020DE0
		[NativeMethod(Name = "TextSelectionService::GetCursorPositionFromLogicalIndex")]
		internal static Vector2 GetCursorPositionFromLogicalIndex(IntPtr textGenerationInfo, int logicalIndex)
		{
			Vector2 vector;
			TextSelectionService.GetCursorPositionFromLogicalIndex_Injected(textGenerationInfo, logicalIndex, out vector);
			return vector;
		}

		// Token: 0x060002B1 RID: 689
		[NativeMethod(Name = "TextSelectionService::LineUpCharacterPosition")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int LineUpCharacterPosition(IntPtr textGenerationInfo, int originalPos);

		// Token: 0x060002B2 RID: 690
		[NativeMethod(Name = "TextSelectionService::LineDownCharacterPosition")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int LineDownCharacterPosition(IntPtr textGenerationInfo, int originalPos);

		// Token: 0x060002B3 RID: 691 RVA: 0x00022BF8 File Offset: 0x00020DF8
		[NativeMethod(Name = "TextSelectionService::GetHighlightRectangles")]
		internal static Rect[] GetHighlightRectangles(IntPtr textGenerationInfo, int cursorIndex, int selectIndex)
		{
			Rect[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				TextSelectionService.GetHighlightRectangles_Injected(textGenerationInfo, cursorIndex, selectIndex, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				Rect[] array;
				blittableArrayWrapper.Unmarshal<Rect>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x060002B4 RID: 692
		[NativeMethod(Name = "TextSelectionService::GetCharacterHeightFromIndex")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern float GetCharacterHeightFromIndex(IntPtr textGenerationInfo, int index);

		// Token: 0x060002B5 RID: 693
		[NativeMethod(Name = "TextSelectionService::GetStartOfNextWord")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetStartOfNextWord(IntPtr textGenerationInfo, int currentIndex);

		// Token: 0x060002B6 RID: 694
		[NativeMethod(Name = "TextSelectionService::GetEndOfPreviousWord")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetEndOfPreviousWord(IntPtr textGenerationInfo, int currentIndex);

		// Token: 0x060002B7 RID: 695
		[NativeMethod(Name = "TextSelectionService::GetFirstCharacterIndexOnLine")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetFirstCharacterIndexOnLine(IntPtr textGenerationInfo, int currentIndex);

		// Token: 0x060002B8 RID: 696
		[NativeMethod(Name = "TextSelectionService::GetLastCharacterIndexOnLine")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetLastCharacterIndexOnLine(IntPtr textGenerationInfo, int currentIndex);

		// Token: 0x060002B9 RID: 697
		[NativeMethod(Name = "TextSelectionService::GetLineHeight")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern float GetLineHeight(IntPtr textGenerationInfo, int lineIndex);

		// Token: 0x060002BA RID: 698
		[NativeMethod(Name = "TextSelectionService::GetLineNumberFromLogicalIndex")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetLineNumber(IntPtr textGenerationInfo, int logicalIndex);

		// Token: 0x060002BB RID: 699
		[NativeMethod(Name = "TextSelectionService::SelectToPreviousParagraph")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SelectToPreviousParagraph(IntPtr textGenerationInfo, ref int cursorIndex);

		// Token: 0x060002BC RID: 700
		[NativeMethod(Name = "TextSelectionService::SelectToStartOfParagraph")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SelectToStartOfParagraph(IntPtr textGenerationInfo, ref int cursorIndex);

		// Token: 0x060002BD RID: 701
		[NativeMethod(Name = "TextSelectionService::SelectToEndOfParagraph")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SelectToEndOfParagraph(IntPtr textGenerationInfo, ref int cursorIndex);

		// Token: 0x060002BE RID: 702
		[NativeMethod(Name = "TextSelectionService::SelectToNextParagraph")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SelectToNextParagraph(IntPtr textGenerationInfo, ref int cursorIndex);

		// Token: 0x060002BF RID: 703
		[NativeMethod(Name = "TextSelectionService::SelectCurrentParagraph")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SelectCurrentParagraph(IntPtr textGenerationInfo, ref int cursorIndex, ref int selectIndex);

		// Token: 0x060002C1 RID: 705
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Substring_Injected(IntPtr textGenerationInfo, int startIndex, int endIndex, out ManagedSpanWrapper ret);

		// Token: 0x060002C2 RID: 706
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetCursorLogicalIndexFromPosition_Injected(IntPtr textGenerationInfo, [In] ref Vector2 position);

		// Token: 0x060002C3 RID: 707
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetCursorPositionFromLogicalIndex_Injected(IntPtr textGenerationInfo, int logicalIndex, out Vector2 ret);

		// Token: 0x060002C4 RID: 708
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetHighlightRectangles_Injected(IntPtr textGenerationInfo, int cursorIndex, int selectIndex, out BlittableArrayWrapper ret);
	}
}
