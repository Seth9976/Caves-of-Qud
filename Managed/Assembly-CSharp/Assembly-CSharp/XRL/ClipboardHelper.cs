using System;
using UnityEngine;

namespace XRL
{
	// Token: 0x020004CA RID: 1226
	public static class ClipboardHelper
	{
		// Token: 0x060034EF RID: 13551 RVA: 0x000FFFBE File Offset: 0x000FE1BE
		public static string GetClipboardData()
		{
			ClipboardHelper.bUpdateClipboard = true;
			while (ClipboardHelper.bUpdateClipboard)
			{
			}
			return ClipboardHelper.ClipboardData;
		}

		// Token: 0x060034F0 RID: 13552 RVA: 0x000FFFD2 File Offset: 0x000FE1D2
		public static void SetClipboardData(string D)
		{
			if (ClipboardHelper.bPutClipboard)
			{
				return;
			}
			ClipboardHelper.bPutClipboard = true;
			ClipboardHelper.PutData = D;
		}

		// Token: 0x060034F1 RID: 13553 RVA: 0x000FFFE8 File Offset: 0x000FE1E8
		public static void UpdateFromMainThread()
		{
			if (ClipboardHelper.bPutClipboard)
			{
				ClipboardHelper.clipBoard = ClipboardHelper.PutData;
				ClipboardHelper.bPutClipboard = false;
			}
			if (ClipboardHelper.bUpdateClipboard)
			{
				ClipboardHelper.ClipboardData = ClipboardHelper.clipBoard;
				ClipboardHelper.bUpdateClipboard = false;
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x060034F2 RID: 13554 RVA: 0x00100018 File Offset: 0x000FE218
		// (set) Token: 0x060034F3 RID: 13555 RVA: 0x0010001F File Offset: 0x000FE21F
		public static string clipBoard
		{
			get
			{
				return GUIUtility.systemCopyBuffer;
			}
			set
			{
				GUIUtility.systemCopyBuffer = value;
			}
		}

		// Token: 0x04001ACA RID: 6858
		public static bool bPutClipboard;

		// Token: 0x04001ACB RID: 6859
		public static bool bUpdateClipboard;

		// Token: 0x04001ACC RID: 6860
		public static string ClipboardData;

		// Token: 0x04001ACD RID: 6861
		public static string PutData;
	}
}
