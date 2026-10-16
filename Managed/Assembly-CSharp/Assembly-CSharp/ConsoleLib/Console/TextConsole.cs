using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleLib.Console
{
	// Token: 0x0200154A RID: 5450
	public class TextConsole : IDisposable
	{
		// Token: 0x0600D47E RID: 54398 RVA: 0x004EFF0A File Offset: 0x004EE10A
		public static void LoadScrapBuffers()
		{
			TextConsole.ScrapBuffer.Copy(TextConsole.CurrentBuffer);
			TextConsole.ScrapBuffer2.Copy(TextConsole.CurrentBuffer);
		}

		// Token: 0x0600D47F RID: 54399 RVA: 0x004EFF2C File Offset: 0x004EE12C
		public static ScreenBuffer GetScrapBuffer1(bool bLoadFromCurrent = false)
		{
			if (bLoadFromCurrent)
			{
				object bufferCS = TextConsole.BufferCS;
				lock (bufferCS)
				{
					TextConsole.ScrapBuffer.Copy(TextConsole.CurrentBuffer);
				}
			}
			return TextConsole.ScrapBuffer;
		}

		// Token: 0x0600D480 RID: 54400 RVA: 0x004EFF80 File Offset: 0x004EE180
		public static ScreenBuffer GetScrapBuffer2(bool bLoadFromCurrent = false)
		{
			if (bLoadFromCurrent)
			{
				object bufferCS = TextConsole.BufferCS;
				lock (bufferCS)
				{
					TextConsole.ScrapBuffer2.Copy(TextConsole.CurrentBuffer);
				}
			}
			return TextConsole.ScrapBuffer2;
		}

		// Token: 0x0600D481 RID: 54401 RVA: 0x004EFFD4 File Offset: 0x004EE1D4
		public static void ConsoleUIThread()
		{
		}

		// Token: 0x0600D482 RID: 54402 RVA: 0x004EFFD6 File Offset: 0x004EE1D6
		public void Hide()
		{
		}

		// Token: 0x0600D483 RID: 54403 RVA: 0x004EFFD8 File Offset: 0x004EE1D8
		public TextConsole()
		{
			TextConsole.CurrentBuffer = ScreenBuffer.create(80, 25);
			TextConsole.ScrapBuffer = ScreenBuffer.create(80, 25);
			TextConsole.ScrapBuffer2 = ScreenBuffer.create(80, 25);
			Keyboard.Init();
		}

		// Token: 0x0600D484 RID: 54404 RVA: 0x004F000F File Offset: 0x004EE20F
		public void Dispose()
		{
		}

		// Token: 0x0600D485 RID: 54405 RVA: 0x004F0014 File Offset: 0x004EE214
		public static void StopUI()
		{
			try
			{
				if (TextConsole.UIThread != null)
				{
					TextConsole.UIThread.Abort();
				}
			}
			catch
			{
			}
			Keyboard.KeyEvent.Set();
		}

		// Token: 0x0600D486 RID: 54406 RVA: 0x004F0054 File Offset: 0x004EE254
		public void FocusUI()
		{
		}

		// Token: 0x0600D487 RID: 54407 RVA: 0x004F0056 File Offset: 0x004EE256
		public ScreenBuffer GetCopy()
		{
			ScreenBuffer screenBuffer = ScreenBuffer.create(80, 25);
			screenBuffer.Copy(TextConsole.CurrentBuffer);
			return screenBuffer;
		}

		// Token: 0x0600D488 RID: 54408 RVA: 0x004F006D File Offset: 0x004EE26D
		public void ShowCursor(bool bShow)
		{
		}

		// Token: 0x0600D489 RID: 54409 RVA: 0x004F006F File Offset: 0x004EE26F
		public void Close()
		{
		}

		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x0600D48A RID: 54410 RVA: 0x004F0071 File Offset: 0x004EE271
		// (set) Token: 0x0600D48B RID: 54411 RVA: 0x004F0078 File Offset: 0x004EE278
		public static bool BufferUpdated
		{
			get
			{
				return TextConsole._BufferUpdated;
			}
			set
			{
				TextConsole._BufferUpdated = value;
			}
		}

		// Token: 0x0600D48C RID: 54412 RVA: 0x004F0080 File Offset: 0x004EE280
		public void WaitFrame()
		{
			while (TextConsole.BufferUpdated)
			{
			}
		}

		// Token: 0x0600D48D RID: 54413 RVA: 0x004F008C File Offset: 0x004EE28C
		public void DrawBuffer(ScreenBuffer Buffer, IScreenBufferExtra BufferExtra = null, bool bSkipIfOverlay = false)
		{
			if (Keyboard.Closed)
			{
				Thread.CurrentThread.Abort();
				throw new Exception("Stopping game thread with an exception!");
			}
			object bufferCS = TextConsole.BufferCS;
			lock (bufferCS)
			{
				if (!bSkipIfOverlay || !GameManager.Instance.ModernUI)
				{
					TextConsole.CurrentBuffer.Copy(Buffer);
					ScreenBuffer currentBuffer = TextConsole.CurrentBuffer;
					GameManager instance = GameManager.Instance;
					currentBuffer.ViewTag = ((instance != null) ? instance.CurrentGameView : null);
					if (BufferExtra != null)
					{
						TextConsole.bufferExtras.Enqueue(BufferExtra);
					}
				}
				TextConsole.BufferUpdated = true;
			}
			TextConsole.DrawBufferEvent.Set();
		}

		// Token: 0x0600D48E RID: 54414 RVA: 0x004F0138 File Offset: 0x004EE338
		public static void WaitForDraw(int Timeout)
		{
			TextConsole.DrawBufferEvent.Reset();
			TextConsole.DrawBufferEvent.WaitOne(Timeout);
		}

		// Token: 0x0600D48F RID: 54415 RVA: 0x004F0151 File Offset: 0x004EE351
		public static void Constrain(ref int X, ref int Y)
		{
			if (X < 0)
			{
				X = 0;
			}
			else if (X > 79)
			{
				X = 79;
			}
			if (Y < 0)
			{
				Y = 0;
				return;
			}
			if (Y > 24)
			{
				Y = 24;
			}
		}

		// Token: 0x0600D490 RID: 54416 RVA: 0x004F017A File Offset: 0x004EE37A
		public static void Constrain(ref int Left, ref int Right, ref int Top, ref int Bottom)
		{
			if (Left < 0)
			{
				Left = 0;
			}
			if (Right > 79)
			{
				Right = 79;
			}
			if (Top < 0)
			{
				Top = 0;
			}
			if (Bottom > 24)
			{
				Bottom = 24;
			}
		}

		// Token: 0x0400553C RID: 21820
		public static bool bExtended = false;

		// Token: 0x0400553D RID: 21821
		public static ScreenBuffer ScrapBuffer;

		// Token: 0x0400553E RID: 21822
		public static ScreenBuffer ScrapBuffer2;

		// Token: 0x0400553F RID: 21823
		public static TerminalMode Mode = TerminalMode.Unity;

		// Token: 0x04005540 RID: 21824
		public static FilterMode TexFilterMode = FilterMode.Nearest;

		// Token: 0x04005541 RID: 21825
		public static TerminalFont TexFont = TerminalFont.Nice;

		// Token: 0x04005542 RID: 21826
		public static Thread UIThread = null;

		// Token: 0x04005543 RID: 21827
		private IntPtr hStdout;

		// Token: 0x04005544 RID: 21828
		private IntPtr hStdin;

		// Token: 0x04005545 RID: 21829
		public static string ThinkString;

		// Token: 0x04005546 RID: 21830
		public static object BufferCS = new object();

		// Token: 0x04005547 RID: 21831
		public static ScreenBuffer CurrentBuffer;

		// Token: 0x04005548 RID: 21832
		public static bool _BufferUpdated = false;

		// Token: 0x04005549 RID: 21833
		public static Queue<IScreenBufferExtra> bufferExtras = new Queue<IScreenBufferExtra>();

		// Token: 0x0400554A RID: 21834
		public static AutoResetEvent DrawBufferEvent = new AutoResetEvent(false);
	}
}
