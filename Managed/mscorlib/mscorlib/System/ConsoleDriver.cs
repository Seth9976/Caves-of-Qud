using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x0200022E RID: 558
	internal static class ConsoleDriver
	{
		// Token: 0x06001958 RID: 6488 RVA: 0x0005EAFC File Offset: 0x0005CCFC
		static ConsoleDriver()
		{
			if (!ConsoleDriver.IsConsole)
			{
				ConsoleDriver.driver = ConsoleDriver.CreateNullConsoleDriver();
				return;
			}
			if (Environment.IsRunningOnWindows)
			{
				ConsoleDriver.driver = ConsoleDriver.CreateWindowsConsoleDriver();
				return;
			}
			string environmentVariable = Environment.GetEnvironmentVariable("TERM");
			if (environmentVariable == "dumb")
			{
				ConsoleDriver.is_console = false;
				ConsoleDriver.driver = ConsoleDriver.CreateNullConsoleDriver();
				return;
			}
			ConsoleDriver.driver = ConsoleDriver.CreateTermInfoDriver(environmentVariable);
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x0005EB61 File Offset: 0x0005CD61
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static IConsoleDriver CreateNullConsoleDriver()
		{
			return new NullConsoleDriver();
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x0005EB68 File Offset: 0x0005CD68
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static IConsoleDriver CreateWindowsConsoleDriver()
		{
			return new WindowsConsoleDriver();
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x0005EB6F File Offset: 0x0005CD6F
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static IConsoleDriver CreateTermInfoDriver(string term)
		{
			return new TermInfoDriver(term);
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x0600195C RID: 6492 RVA: 0x0005EB77 File Offset: 0x0005CD77
		public static bool Initialized
		{
			get
			{
				return ConsoleDriver.driver.Initialized;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x0600195D RID: 6493 RVA: 0x0005EB83 File Offset: 0x0005CD83
		// (set) Token: 0x0600195E RID: 6494 RVA: 0x0005EB8F File Offset: 0x0005CD8F
		public static ConsoleColor BackgroundColor
		{
			get
			{
				return ConsoleDriver.driver.BackgroundColor;
			}
			set
			{
				if (value < ConsoleColor.Black || value > ConsoleColor.White)
				{
					throw new ArgumentOutOfRangeException("value", "Not a ConsoleColor value.");
				}
				ConsoleDriver.driver.BackgroundColor = value;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x0600195F RID: 6495 RVA: 0x0005EBB5 File Offset: 0x0005CDB5
		// (set) Token: 0x06001960 RID: 6496 RVA: 0x0005EBC1 File Offset: 0x0005CDC1
		public static int BufferHeight
		{
			get
			{
				return ConsoleDriver.driver.BufferHeight;
			}
			set
			{
				ConsoleDriver.driver.BufferHeight = value;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06001961 RID: 6497 RVA: 0x0005EBCE File Offset: 0x0005CDCE
		// (set) Token: 0x06001962 RID: 6498 RVA: 0x0005EBDA File Offset: 0x0005CDDA
		public static int BufferWidth
		{
			get
			{
				return ConsoleDriver.driver.BufferWidth;
			}
			set
			{
				ConsoleDriver.driver.BufferWidth = value;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06001963 RID: 6499 RVA: 0x0005EBE7 File Offset: 0x0005CDE7
		public static bool CapsLock
		{
			get
			{
				return ConsoleDriver.driver.CapsLock;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06001964 RID: 6500 RVA: 0x0005EBF3 File Offset: 0x0005CDF3
		// (set) Token: 0x06001965 RID: 6501 RVA: 0x0005EBFF File Offset: 0x0005CDFF
		public static int CursorLeft
		{
			get
			{
				return ConsoleDriver.driver.CursorLeft;
			}
			set
			{
				ConsoleDriver.driver.CursorLeft = value;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06001966 RID: 6502 RVA: 0x0005EC0C File Offset: 0x0005CE0C
		// (set) Token: 0x06001967 RID: 6503 RVA: 0x0005EC18 File Offset: 0x0005CE18
		public static int CursorSize
		{
			get
			{
				return ConsoleDriver.driver.CursorSize;
			}
			set
			{
				ConsoleDriver.driver.CursorSize = value;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06001968 RID: 6504 RVA: 0x0005EC25 File Offset: 0x0005CE25
		// (set) Token: 0x06001969 RID: 6505 RVA: 0x0005EC31 File Offset: 0x0005CE31
		public static int CursorTop
		{
			get
			{
				return ConsoleDriver.driver.CursorTop;
			}
			set
			{
				ConsoleDriver.driver.CursorTop = value;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x0600196A RID: 6506 RVA: 0x0005EC3E File Offset: 0x0005CE3E
		// (set) Token: 0x0600196B RID: 6507 RVA: 0x0005EC4A File Offset: 0x0005CE4A
		public static bool CursorVisible
		{
			get
			{
				return ConsoleDriver.driver.CursorVisible;
			}
			set
			{
				ConsoleDriver.driver.CursorVisible = value;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x0600196C RID: 6508 RVA: 0x0005EC57 File Offset: 0x0005CE57
		public static bool KeyAvailable
		{
			get
			{
				return ConsoleDriver.driver.KeyAvailable;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x0600196D RID: 6509 RVA: 0x0005EC63 File Offset: 0x0005CE63
		// (set) Token: 0x0600196E RID: 6510 RVA: 0x0005EC6F File Offset: 0x0005CE6F
		public static ConsoleColor ForegroundColor
		{
			get
			{
				return ConsoleDriver.driver.ForegroundColor;
			}
			set
			{
				if (value < ConsoleColor.Black || value > ConsoleColor.White)
				{
					throw new ArgumentOutOfRangeException("value", "Not a ConsoleColor value.");
				}
				ConsoleDriver.driver.ForegroundColor = value;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x0600196F RID: 6511 RVA: 0x0005EC95 File Offset: 0x0005CE95
		public static int LargestWindowHeight
		{
			get
			{
				return ConsoleDriver.driver.LargestWindowHeight;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06001970 RID: 6512 RVA: 0x0005ECA1 File Offset: 0x0005CEA1
		public static int LargestWindowWidth
		{
			get
			{
				return ConsoleDriver.driver.LargestWindowWidth;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06001971 RID: 6513 RVA: 0x0005ECAD File Offset: 0x0005CEAD
		public static bool NumberLock
		{
			get
			{
				return ConsoleDriver.driver.NumberLock;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06001972 RID: 6514 RVA: 0x0005ECB9 File Offset: 0x0005CEB9
		// (set) Token: 0x06001973 RID: 6515 RVA: 0x0005ECC5 File Offset: 0x0005CEC5
		public static string Title
		{
			get
			{
				return ConsoleDriver.driver.Title;
			}
			set
			{
				ConsoleDriver.driver.Title = value;
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06001974 RID: 6516 RVA: 0x0005ECD2 File Offset: 0x0005CED2
		// (set) Token: 0x06001975 RID: 6517 RVA: 0x0005ECDE File Offset: 0x0005CEDE
		public static bool TreatControlCAsInput
		{
			get
			{
				return ConsoleDriver.driver.TreatControlCAsInput;
			}
			set
			{
				ConsoleDriver.driver.TreatControlCAsInput = value;
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06001976 RID: 6518 RVA: 0x0005ECEB File Offset: 0x0005CEEB
		// (set) Token: 0x06001977 RID: 6519 RVA: 0x0005ECF7 File Offset: 0x0005CEF7
		public static int WindowHeight
		{
			get
			{
				return ConsoleDriver.driver.WindowHeight;
			}
			set
			{
				ConsoleDriver.driver.WindowHeight = value;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06001978 RID: 6520 RVA: 0x0005ED04 File Offset: 0x0005CF04
		// (set) Token: 0x06001979 RID: 6521 RVA: 0x0005ED10 File Offset: 0x0005CF10
		public static int WindowLeft
		{
			get
			{
				return ConsoleDriver.driver.WindowLeft;
			}
			set
			{
				ConsoleDriver.driver.WindowLeft = value;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x0600197A RID: 6522 RVA: 0x0005ED1D File Offset: 0x0005CF1D
		// (set) Token: 0x0600197B RID: 6523 RVA: 0x0005ED29 File Offset: 0x0005CF29
		public static int WindowTop
		{
			get
			{
				return ConsoleDriver.driver.WindowTop;
			}
			set
			{
				ConsoleDriver.driver.WindowTop = value;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x0600197C RID: 6524 RVA: 0x0005ED36 File Offset: 0x0005CF36
		// (set) Token: 0x0600197D RID: 6525 RVA: 0x0005ED42 File Offset: 0x0005CF42
		public static int WindowWidth
		{
			get
			{
				return ConsoleDriver.driver.WindowWidth;
			}
			set
			{
				ConsoleDriver.driver.WindowWidth = value;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x0600197E RID: 6526 RVA: 0x0005ED4F File Offset: 0x0005CF4F
		public static bool IsErrorRedirected
		{
			get
			{
				return !ConsoleDriver.Isatty(MonoIO.ConsoleError);
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x0600197F RID: 6527 RVA: 0x0005ED5E File Offset: 0x0005CF5E
		public static bool IsOutputRedirected
		{
			get
			{
				return !ConsoleDriver.Isatty(MonoIO.ConsoleOutput);
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06001980 RID: 6528 RVA: 0x0005ED6D File Offset: 0x0005CF6D
		public static bool IsInputRedirected
		{
			get
			{
				return !ConsoleDriver.Isatty(MonoIO.ConsoleInput);
			}
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x0005ED7C File Offset: 0x0005CF7C
		public static void Beep(int frequency, int duration)
		{
			ConsoleDriver.driver.Beep(frequency, duration);
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x0005ED8A File Offset: 0x0005CF8A
		public static void Clear()
		{
			ConsoleDriver.driver.Clear();
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x0005ED98 File Offset: 0x0005CF98
		public static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop)
		{
			ConsoleDriver.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop, ' ', ConsoleColor.Black, ConsoleColor.Black);
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x0005EDB8 File Offset: 0x0005CFB8
		public static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop, char sourceChar, ConsoleColor sourceForeColor, ConsoleColor sourceBackColor)
		{
			ConsoleDriver.driver.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop, sourceChar, sourceForeColor, sourceBackColor);
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x0005EDDD File Offset: 0x0005CFDD
		public static void Init()
		{
			ConsoleDriver.driver.Init();
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x0005EDEC File Offset: 0x0005CFEC
		public static int Read()
		{
			return (int)ConsoleDriver.ReadKey(false).KeyChar;
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x0005EE07 File Offset: 0x0005D007
		public static string ReadLine()
		{
			return ConsoleDriver.driver.ReadLine();
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x0005EE13 File Offset: 0x0005D013
		public static ConsoleKeyInfo ReadKey(bool intercept)
		{
			return ConsoleDriver.driver.ReadKey(intercept);
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x0005EE20 File Offset: 0x0005D020
		public static void ResetColor()
		{
			ConsoleDriver.driver.ResetColor();
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x0005EE2C File Offset: 0x0005D02C
		public static void SetBufferSize(int width, int height)
		{
			ConsoleDriver.driver.SetBufferSize(width, height);
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x0005EE3A File Offset: 0x0005D03A
		public static void SetCursorPosition(int left, int top)
		{
			ConsoleDriver.driver.SetCursorPosition(left, top);
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x0005EE48 File Offset: 0x0005D048
		public static void SetWindowPosition(int left, int top)
		{
			ConsoleDriver.driver.SetWindowPosition(left, top);
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x0005EE56 File Offset: 0x0005D056
		public static void SetWindowSize(int width, int height)
		{
			ConsoleDriver.driver.SetWindowSize(width, height);
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x0600198E RID: 6542 RVA: 0x0005EE64 File Offset: 0x0005D064
		public static bool IsConsole
		{
			get
			{
				if (ConsoleDriver.called_isatty)
				{
					return ConsoleDriver.is_console;
				}
				ConsoleDriver.is_console = ConsoleDriver.Isatty(MonoIO.ConsoleOutput) && ConsoleDriver.Isatty(MonoIO.ConsoleInput);
				ConsoleDriver.called_isatty = true;
				return ConsoleDriver.is_console;
			}
		}

		// Token: 0x0600198F RID: 6543
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Isatty(IntPtr handle);

		// Token: 0x06001990 RID: 6544
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int InternalKeyAvailable(int ms_timeout);

		// Token: 0x06001991 RID: 6545
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern bool TtySetup(string keypadXmit, string teardown, out byte[] control_characters, out int* address);

		// Token: 0x06001992 RID: 6546
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool SetEcho(bool wantEcho);

		// Token: 0x06001993 RID: 6547
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool SetBreak(bool wantBreak);

		// Token: 0x040016E8 RID: 5864
		internal static IConsoleDriver driver;

		// Token: 0x040016E9 RID: 5865
		private static bool is_console;

		// Token: 0x040016EA RID: 5866
		private static bool called_isatty;
	}
}
