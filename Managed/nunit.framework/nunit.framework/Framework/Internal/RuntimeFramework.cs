using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Win32;

namespace NUnit.Framework.Internal
{
	// Token: 0x02000079 RID: 121
	[Serializable]
	public sealed class RuntimeFramework
	{
		// Token: 0x06000479 RID: 1145 RVA: 0x0000A52B File Offset: 0x0000872B
		public RuntimeFramework(RuntimeType runtime, Version version)
		{
			this.Runtime = runtime;
			if (version.Build < 0)
			{
				this.InitFromFrameworkVersion(version);
			}
			else
			{
				this.InitFromClrVersion(version);
			}
			this.DisplayName = RuntimeFramework.GetDefaultDisplayName(runtime, version);
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0000A560 File Offset: 0x00008760
		private void InitFromFrameworkVersion(Version version)
		{
			this.ClrVersion = version;
			this.FrameworkVersion = version;
			if (version.Major > 0)
			{
				switch (this.Runtime)
				{
				case RuntimeType.Any:
				case RuntimeType.Net:
				case RuntimeType.Mono:
					switch (version.Major)
					{
					case 1:
					{
						int minor = version.Minor;
						if (minor == 0)
						{
							this.ClrVersion = ((this.Runtime == RuntimeType.Mono) ? new Version(1, 1, 4322) : new Version(1, 0, 3705));
							return;
						}
						if (minor != 1)
						{
							RuntimeFramework.ThrowInvalidFrameworkVersion(version);
							return;
						}
						if (this.Runtime == RuntimeType.Mono)
						{
							this.FrameworkVersion = new Version(1, 0);
						}
						this.ClrVersion = new Version(1, 1, 4322);
						return;
					}
					case 2:
					case 3:
						this.ClrVersion = new Version(2, 0, 50727);
						return;
					case 4:
						this.ClrVersion = new Version(4, 0, 30319);
						return;
					default:
						RuntimeFramework.ThrowInvalidFrameworkVersion(version);
						return;
					}
					break;
				case RuntimeType.NetCF:
					if (version.Major == 3 && version.Minor == 5)
					{
						this.ClrVersion = new Version(3, 5, 7283);
					}
					break;
				case RuntimeType.SSCLI:
					break;
				case RuntimeType.Silverlight:
					this.ClrVersion = ((version.Major >= 4) ? new Version(4, 0, 60310) : new Version(2, 0, 50727));
					return;
				default:
					return;
				}
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0000A6B8 File Offset: 0x000088B8
		private static void ThrowInvalidFrameworkVersion(Version version)
		{
			throw new ArgumentException("Unknown framework version " + ((version != null) ? version.ToString() : null), "version");
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0000A6DB File Offset: 0x000088DB
		private void InitFromClrVersion(Version version)
		{
			this.FrameworkVersion = new Version(version.Major, version.Minor);
			this.ClrVersion = version;
			if (this.Runtime == RuntimeType.Mono && version.Major == 1)
			{
				this.FrameworkVersion = new Version(1, 0);
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x0000A71A File Offset: 0x0000891A
		public static RuntimeFramework CurrentFramework
		{
			get
			{
				return RuntimeFramework.currentFramework.Value;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x0000A726 File Offset: 0x00008926
		// (set) Token: 0x0600047F RID: 1151 RVA: 0x0000A72E File Offset: 0x0000892E
		public RuntimeType Runtime { get; private set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x0000A737 File Offset: 0x00008937
		// (set) Token: 0x06000481 RID: 1153 RVA: 0x0000A73F File Offset: 0x0000893F
		public Version FrameworkVersion { get; private set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x0000A748 File Offset: 0x00008948
		// (set) Token: 0x06000483 RID: 1155 RVA: 0x0000A750 File Offset: 0x00008950
		public Version ClrVersion { get; private set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x0000A759 File Offset: 0x00008959
		public bool AllowAnyVersion
		{
			get
			{
				return this.ClrVersion == RuntimeFramework.DefaultVersion;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x0000A76B File Offset: 0x0000896B
		// (set) Token: 0x06000486 RID: 1158 RVA: 0x0000A773 File Offset: 0x00008973
		public string DisplayName { get; private set; }

		// Token: 0x06000487 RID: 1159 RVA: 0x0000A77C File Offset: 0x0000897C
		public static RuntimeFramework Parse(string s)
		{
			RuntimeType runtimeType = RuntimeType.Any;
			Version version = RuntimeFramework.DefaultVersion;
			string[] array = s.Split(new char[] { '-' });
			if (array.Length == 2)
			{
				runtimeType = (RuntimeType)Enum.Parse(typeof(RuntimeType), array[0], true);
				string text = array[1];
				if (text != "")
				{
					version = new Version(text);
				}
			}
			else if (char.ToLower(s[0]) == 'v')
			{
				version = new Version(s.Substring(1));
			}
			else if (RuntimeFramework.IsRuntimeTypeName(s))
			{
				runtimeType = (RuntimeType)Enum.Parse(typeof(RuntimeType), s, true);
			}
			else
			{
				version = new Version(s);
			}
			return new RuntimeFramework(runtimeType, version);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0000A82C File Offset: 0x00008A2C
		public override string ToString()
		{
			if (this.AllowAnyVersion)
			{
				return this.Runtime.ToString().ToLower();
			}
			string text = this.FrameworkVersion.ToString();
			if (this.Runtime == RuntimeType.Any)
			{
				return "v" + text;
			}
			return this.Runtime.ToString().ToLower() + "-" + text;
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0000A8A0 File Offset: 0x00008AA0
		public bool Supports(RuntimeFramework target)
		{
			if (this.Runtime != RuntimeType.Any && target.Runtime != RuntimeType.Any && this.Runtime != target.Runtime)
			{
				return false;
			}
			if (this.AllowAnyVersion || target.AllowAnyVersion)
			{
				return true;
			}
			if (!RuntimeFramework.VersionsMatch(this.ClrVersion, target.ClrVersion))
			{
				return false;
			}
			if (this.Runtime != RuntimeType.Silverlight)
			{
				return this.FrameworkVersion.Major >= target.FrameworkVersion.Major && this.FrameworkVersion.Minor >= target.FrameworkVersion.Minor;
			}
			return this.FrameworkVersion.Major == target.FrameworkVersion.Major && this.FrameworkVersion.Minor == target.FrameworkVersion.Minor;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0000A965 File Offset: 0x00008B65
		private static bool IsNetCore()
		{
			return Environment.Version.Major >= 5;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0000A978 File Offset: 0x00008B78
		private static bool IsRuntimeTypeName(string name)
		{
			return TypeHelper.GetEnumNames(typeof(RuntimeType)).Any<string>((string item) => item.ToLower() == name.ToLower());
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0000A9B4 File Offset: 0x00008BB4
		private static string GetDefaultDisplayName(RuntimeType runtime, Version version)
		{
			if (version == RuntimeFramework.DefaultVersion)
			{
				return runtime.ToString();
			}
			if (runtime == RuntimeType.Any)
			{
				return "v" + ((version != null) ? version.ToString() : null);
			}
			return runtime.ToString() + " " + ((version != null) ? version.ToString() : null);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0000AA1C File Offset: 0x00008C1C
		private static bool VersionsMatch(Version v1, Version v2)
		{
			return v1.Major == v2.Major && v1.Minor == v2.Minor && (v1.Build < 0 || v2.Build < 0 || v1.Build == v2.Build) && (v1.Revision < 0 || v2.Revision < 0 || v1.Revision == v2.Revision);
		}

		// Token: 0x040000CF RID: 207
		public static readonly Version DefaultVersion = new Version(0, 0);

		// Token: 0x040000D0 RID: 208
		private static readonly Lazy<RuntimeFramework> currentFramework = new Lazy<RuntimeFramework>(delegate
		{
			Type type = Type.GetType("Mono.Runtime", false);
			bool flag = Type.GetType("MonoTouch.UIKit.UIApplicationDelegate,monotouch") != null;
			bool flag2 = type != null;
			bool flag3 = !flag2 && !flag && RuntimeFramework.IsNetCore();
			RuntimeType runtimeType = (flag ? RuntimeType.MonoTouch : (flag2 ? RuntimeType.Mono : ((Environment.OSVersion.Platform == PlatformID.WinCE) ? RuntimeType.NetCF : (flag3 ? RuntimeType.NetCore : RuntimeType.Net))));
			int num = Environment.Version.Major;
			int num2 = Environment.Version.Minor;
			if (flag2)
			{
				if (num != 1)
				{
					if (num == 2)
					{
						num = 3;
						num2 = 5;
					}
				}
				else
				{
					num2 = 0;
				}
			}
			else if (flag3)
			{
				num = 0;
				num2 = 0;
			}
			else
			{
				if (num == 2)
				{
					using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\.NETFramework"))
					{
						if (registryKey != null)
						{
							string text = registryKey.GetValue("InstallRoot") as string;
							if (text != null)
							{
								if (Directory.Exists(Path.Combine(text, "v3.5")))
								{
									num = 3;
									num2 = 5;
								}
								else if (Directory.Exists(Path.Combine(text, "v3.0")))
								{
									num = 3;
									num2 = 0;
								}
							}
						}
						goto IL_0131;
					}
				}
				if (num == 4 && Type.GetType("System.Reflection.AssemblyMetadataAttribute") != null)
				{
					num2 = 5;
				}
			}
			IL_0131:
			RuntimeFramework runtimeFramework = new RuntimeFramework(runtimeType, new Version(num, num2))
			{
				ClrVersion = Environment.Version
			};
			if (flag2)
			{
				MethodInfo method = type.GetMethod("GetDisplayName", BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.ExactBinding);
				if (method != null)
				{
					runtimeFramework.DisplayName = (string)method.Invoke(null, new object[0]);
				}
			}
			return runtimeFramework;
		});
	}
}
