using System;
using System.Linq;

namespace NUnit.Framework.Internal
{
	// Token: 0x02000072 RID: 114
	public class PlatformHelper
	{
		// Token: 0x060003FE RID: 1022 RVA: 0x00008ACA File Offset: 0x00006CCA
		public PlatformHelper()
		{
			this._os = OSPlatform.CurrentPlatform;
			this._rt = RuntimeFramework.CurrentFramework;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00008AF3 File Offset: 0x00006CF3
		public PlatformHelper(OSPlatform os, RuntimeFramework rt)
		{
			this._os = os;
			this._rt = rt;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00008B14 File Offset: 0x00006D14
		public bool IsPlatformSupported(string[] platforms)
		{
			return platforms.Any<string>(new Func<string, bool>(this.IsPlatformSupported));
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00008B28 File Offset: 0x00006D28
		public bool IsPlatformSupported(PlatformAttribute platformAttribute)
		{
			string include = platformAttribute.Include;
			string exclude = platformAttribute.Exclude;
			return this.IsPlatformSupported(include, exclude);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00008B4C File Offset: 0x00006D4C
		public bool IsPlatformSupported(TestCaseAttribute testCaseAttribute)
		{
			string includePlatform = testCaseAttribute.IncludePlatform;
			string excludePlatform = testCaseAttribute.ExcludePlatform;
			return this.IsPlatformSupported(includePlatform, excludePlatform);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00008B70 File Offset: 0x00006D70
		private bool IsPlatformSupported(string include, string exclude)
		{
			try
			{
				if (include != null && !this.IsPlatformSupported(include))
				{
					this._reason = string.Format("Only supported on {0}", include);
					return false;
				}
				if (exclude != null && this.IsPlatformSupported(exclude))
				{
					this._reason = string.Format("Not supported on {0}", exclude);
					return false;
				}
			}
			catch (Exception ex)
			{
				this._reason = ex.Message;
				return false;
			}
			return true;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00008BE8 File Offset: 0x00006DE8
		public bool IsPlatformSupported(string platform)
		{
			if (platform.IndexOf(',') >= 0)
			{
				return this.IsPlatformSupported(platform.Split(new char[] { ',' }));
			}
			string text = platform.Trim();
			string text2 = text.ToUpper();
			bool flag;
			if (text2 != null)
			{
				switch (text2.Length)
				{
				case 3:
				{
					char c = text2[2];
					switch (c)
					{
					case '3':
						if (!(text2 == "NT3"))
						{
							goto IL_0774;
						}
						flag = this._os.IsNT3;
						goto IL_077C;
					case '4':
						if (!(text2 == "NT4"))
						{
							goto IL_0774;
						}
						flag = this._os.IsNT4;
						goto IL_077C;
					case '5':
						if (!(text2 == "NT5"))
						{
							goto IL_0774;
						}
						flag = this._os.IsNT5;
						goto IL_077C;
					case '6':
						if (!(text2 == "NT6"))
						{
							goto IL_0774;
						}
						flag = this._os.IsNT6;
						goto IL_077C;
					default:
						if (c != 'N')
						{
							goto IL_0774;
						}
						if (!(text2 == "WIN"))
						{
							goto IL_0774;
						}
						break;
					}
					break;
				}
				case 4:
					switch (text2[0])
					{
					case 'U':
						if (!(text2 == "UNIX"))
						{
							goto IL_0774;
						}
						goto IL_0721;
					case 'V':
						goto IL_0774;
					case 'W':
						if (text2 == "WIN7")
						{
							goto IL_06D5;
						}
						if (!(text2 == "WIN8"))
						{
							goto IL_0774;
						}
						goto IL_06E6;
					case 'X':
						if (!(text2 == "XBOX"))
						{
							goto IL_0774;
						}
						flag = this._os.IsXbox;
						goto IL_077C;
					default:
						goto IL_0774;
					}
					break;
				case 5:
				{
					char c = text2[3];
					if (c <= '9')
					{
						switch (c)
						{
						case '1':
							if (!(text2 == "WIN10"))
							{
								goto IL_0774;
							}
							goto IL_0705;
						case '2':
							if (!(text2 == "WIN2K"))
							{
								goto IL_0774;
							}
							flag = this._os.IsWin2K;
							goto IL_077C;
						case '3':
							if (!(text2 == "WIN32"))
							{
								goto IL_0774;
							}
							break;
						default:
							if (c != '9')
							{
								goto IL_0774;
							}
							if (text2 == "WIN95")
							{
								flag = this._os.IsWin95;
								goto IL_077C;
							}
							if (!(text2 == "WIN98"))
							{
								goto IL_0774;
							}
							flag = this._os.IsWin98;
							goto IL_077C;
						}
					}
					else if (c != 'C')
					{
						if (c != 'M')
						{
							switch (c)
							{
							case 'T':
								if (!(text2 == "VISTA"))
								{
									goto IL_0774;
								}
								flag = this._os.IsVista;
								goto IL_077C;
							case 'U':
								if (!(text2 == "LINUX"))
								{
									goto IL_0774;
								}
								goto IL_0721;
							case 'V':
							case 'W':
								goto IL_0774;
							case 'X':
								if (!(text2 == "WINXP"))
								{
									goto IL_0774;
								}
								flag = this._os.IsWinXP;
								goto IL_077C;
							default:
								goto IL_0774;
							}
						}
						else
						{
							if (!(text2 == "WINME"))
							{
								goto IL_0774;
							}
							flag = this._os.IsWinME;
							goto IL_077C;
						}
					}
					else
					{
						if (!(text2 == "WINCE"))
						{
							goto IL_0774;
						}
						flag = this._os.IsWinCE;
						goto IL_077C;
					}
					break;
				}
				case 6:
				{
					char c = text2[0];
					if (c <= '6')
					{
						if (c != '3')
						{
							if (c != '6')
							{
								goto IL_0774;
							}
							if (!(text2 == "64-BIT"))
							{
								goto IL_0774;
							}
							goto IL_074B;
						}
						else
						{
							if (!(text2 == "32-BIT"))
							{
								goto IL_0774;
							}
							goto IL_0756;
						}
					}
					else if (c != 'M')
					{
						if (c != 'W')
						{
							goto IL_0774;
						}
						if (text2 == "WIN32S")
						{
							flag = this._os.IsWin32S;
							goto IL_077C;
						}
						if (!(text2 == "WIN8.1"))
						{
							goto IL_0774;
						}
						goto IL_06F7;
					}
					else
					{
						if (!(text2 == "MACOSX"))
						{
							goto IL_0774;
						}
						flag = this._os.IsMacOSX;
						goto IL_077C;
					}
					break;
				}
				case 7:
					if (!(text2 == "WIN32NT"))
					{
						goto IL_0774;
					}
					flag = this._os.IsWin32NT;
					goto IL_077C;
				case 8:
				{
					char c = text2[7];
					if (c != '7')
					{
						if (c != '8')
						{
							goto IL_0774;
						}
						if (!(text2 == "WINDOWS8"))
						{
							goto IL_0774;
						}
						goto IL_06E6;
					}
					else
					{
						if (!(text2 == "WINDOWS7"))
						{
							goto IL_0774;
						}
						goto IL_06D5;
					}
					break;
				}
				case 9:
				{
					char c = text2[0];
					if (c != '3')
					{
						if (c != '6')
						{
							if (c != 'W')
							{
								goto IL_0774;
							}
							if (!(text2 == "WINDOWS10"))
							{
								goto IL_0774;
							}
							goto IL_0705;
						}
						else
						{
							if (!(text2 == "64-BIT-OS"))
							{
								goto IL_0774;
							}
							flag = Environment.Is64BitOperatingSystem;
							goto IL_077C;
						}
					}
					else
					{
						if (!(text2 == "32-BIT-OS"))
						{
							goto IL_0774;
						}
						flag = !Environment.Is64BitOperatingSystem;
						goto IL_077C;
					}
					break;
				}
				case 10:
					if (!(text2 == "WINDOWS8.1"))
					{
						goto IL_0774;
					}
					goto IL_06F7;
				case 11:
					goto IL_0774;
				case 12:
					if (!(text2 == "WIN32WINDOWS"))
					{
						goto IL_0774;
					}
					flag = this._os.IsWin32Windows;
					goto IL_077C;
				case 13:
				{
					char c = text2[6];
					if (c != '2')
					{
						if (c != '3')
						{
							if (c != '8')
							{
								goto IL_0774;
							}
							if (!(text2 == "WIN2008SERVER"))
							{
								goto IL_0774;
							}
							flag = this._os.IsWin2008Server;
							goto IL_077C;
						}
						else
						{
							if (!(text2 == "WIN2003SERVER"))
							{
								goto IL_0774;
							}
							flag = this._os.IsWin2003Server;
							goto IL_077C;
						}
					}
					else
					{
						if (!(text2 == "WIN2012SERVER"))
						{
							goto IL_0774;
						}
						flag = this._os.IsWin2012ServerR1 || this._os.IsWin2012ServerR2;
						goto IL_077C;
					}
					break;
				}
				case 14:
				{
					char c = text2[0];
					if (c != '3')
					{
						if (c != '6')
						{
							goto IL_0774;
						}
						if (!(text2 == "64-BIT-PROCESS"))
						{
							goto IL_0774;
						}
						goto IL_074B;
					}
					else
					{
						if (!(text2 == "32-BIT-PROCESS"))
						{
							goto IL_0774;
						}
						goto IL_0756;
					}
					break;
				}
				case 15:
				{
					char c = text2[5];
					if (c != '0')
					{
						if (c != '1')
						{
							if (c != 'W')
							{
								goto IL_0774;
							}
							if (!(text2 == "WINDOWSSERVER10"))
							{
								goto IL_0774;
							}
							flag = this._os.IsWindowsServer10;
							goto IL_077C;
						}
						else
						{
							if (!(text2 == "WIN2012SERVERR2"))
							{
								goto IL_0774;
							}
							flag = this._os.IsWin2012ServerR2;
							goto IL_077C;
						}
					}
					else
					{
						if (!(text2 == "WIN2008SERVERR2"))
						{
							goto IL_0774;
						}
						flag = this._os.IsWin2008ServerR2;
						goto IL_077C;
					}
					break;
				}
				default:
					goto IL_0774;
				}
				flag = this._os.IsWindows;
				goto IL_077C;
				IL_06D5:
				flag = this._os.IsWindows7;
				goto IL_077C;
				IL_06E6:
				flag = this._os.IsWindows8;
				goto IL_077C;
				IL_06F7:
				flag = this._os.IsWindows81;
				goto IL_077C;
				IL_0705:
				flag = this._os.IsWindows10;
				goto IL_077C;
				IL_0721:
				flag = this._os.IsUnix;
				goto IL_077C;
				IL_074B:
				flag = IntPtr.Size == 8;
				goto IL_077C;
				IL_0756:
				flag = IntPtr.Size == 4;
				goto IL_077C;
			}
			IL_0774:
			flag = this.IsRuntimeSupported(text);
			IL_077C:
			if (!flag)
			{
				this._reason = "Only supported on " + platform;
			}
			return flag;
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x00009386 File Offset: 0x00007586
		public string Reason
		{
			get
			{
				return this._reason;
			}
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00009390 File Offset: 0x00007590
		private bool IsRuntimeSupported(string platformName)
		{
			string text = null;
			string[] array = platformName.Split(new char[] { '-' });
			if (array.Length == 2)
			{
				platformName = array[0];
				text = array[1];
			}
			string text2 = platformName.ToUpper();
			if (text2 != null)
			{
				switch (text2.Length)
				{
				case 2:
					if (!(text2 == "SL"))
					{
						goto IL_0143;
					}
					break;
				case 3:
					if (!(text2 == "NET"))
					{
						goto IL_0143;
					}
					return this.IsRuntimeSupported(RuntimeType.Net, text);
				case 4:
					if (!(text2 == "MONO"))
					{
						goto IL_0143;
					}
					return this.IsRuntimeSupported(RuntimeType.Mono, text);
				case 5:
				{
					char c = text2[0];
					if (c != 'N')
					{
						if (c != 'R')
						{
							if (c != 'S')
							{
								goto IL_0143;
							}
							if (!(text2 == "SSCLI"))
							{
								goto IL_0143;
							}
						}
						else if (!(text2 == "ROTOR"))
						{
							goto IL_0143;
						}
						return this.IsRuntimeSupported(RuntimeType.SSCLI, text);
					}
					if (!(text2 == "NETCF"))
					{
						goto IL_0143;
					}
					return this.IsRuntimeSupported(RuntimeType.NetCF, text);
				}
				case 6:
				case 7:
				case 8:
				case 10:
					goto IL_0143;
				case 9:
					if (!(text2 == "MONOTOUCH"))
					{
						goto IL_0143;
					}
					return this.IsRuntimeSupported(RuntimeType.MonoTouch, text);
				case 11:
					if (!(text2 == "SILVERLIGHT"))
					{
						goto IL_0143;
					}
					break;
				default:
					goto IL_0143;
				}
				return this.IsRuntimeSupported(RuntimeType.Silverlight, text);
			}
			IL_0143:
			throw new ArgumentException("Invalid platform name", platformName);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x000094EC File Offset: 0x000076EC
		private bool IsRuntimeSupported(RuntimeType runtime, string versionSpecification)
		{
			Version version = ((versionSpecification == null) ? RuntimeFramework.DefaultVersion : new Version(versionSpecification));
			RuntimeFramework runtimeFramework = new RuntimeFramework(runtime, version);
			return this._rt.Supports(runtimeFramework);
		}

		// Token: 0x04000095 RID: 149
		private readonly OSPlatform _os;

		// Token: 0x04000096 RID: 150
		private readonly RuntimeFramework _rt;

		// Token: 0x04000097 RID: 151
		private string _reason = string.Empty;

		// Token: 0x04000098 RID: 152
		private const string CommonOSPlatforms = "Win,Win32,Win32S,Win32NT,Win32Windows,WinCE,Win95,Win98,WinMe,NT3,NT4,NT5,NT6,Win2008Server,Win2008ServerR2,Win2012Server,Win2012ServerR2,Win2K,WinXP,Win2003Server,Vista,Win7,Windows7,Win8,Windows8,Win8.1,Windows8.1,Win10,Windows10,WindowsServer10,Unix,Linux";

		// Token: 0x04000099 RID: 153
		public const string OSPlatforms = "Win,Win32,Win32S,Win32NT,Win32Windows,WinCE,Win95,Win98,WinMe,NT3,NT4,NT5,NT6,Win2008Server,Win2008ServerR2,Win2012Server,Win2012ServerR2,Win2K,WinXP,Win2003Server,Vista,Win7,Windows7,Win8,Windows8,Win8.1,Windows8.1,Win10,Windows10,WindowsServer10,Unix,Linux,Xbox,MacOSX";

		// Token: 0x0400009A RID: 154
		public static readonly string RuntimePlatforms = "Net,NetCF,SSCLI,Rotor,Mono,MonoTouch";
	}
}
