using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000154 RID: 340
	public struct SubsystemVersion : IEquatable<SubsystemVersion>
	{
		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000DAD RID: 3501 RVA: 0x00027644 File Offset: 0x00025844
		public readonly int Major { get; }

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000DAE RID: 3502 RVA: 0x0002764C File Offset: 0x0002584C
		public readonly int Minor { get; }

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000DAF RID: 3503 RVA: 0x00027654 File Offset: 0x00025854
		public static SubsystemVersion None
		{
			get
			{
				return default(SubsystemVersion);
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000DB0 RID: 3504 RVA: 0x0002766A File Offset: 0x0002586A
		public static SubsystemVersion Windows2000
		{
			get
			{
				return new SubsystemVersion(5, 0);
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000DB1 RID: 3505 RVA: 0x00027673 File Offset: 0x00025873
		public static SubsystemVersion WindowsXP
		{
			get
			{
				return new SubsystemVersion(5, 1);
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000DB2 RID: 3506 RVA: 0x0002767C File Offset: 0x0002587C
		public static SubsystemVersion WindowsVista
		{
			get
			{
				return new SubsystemVersion(6, 0);
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000DB3 RID: 3507 RVA: 0x00027685 File Offset: 0x00025885
		public static SubsystemVersion Windows7
		{
			get
			{
				return new SubsystemVersion(6, 1);
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000DB4 RID: 3508 RVA: 0x0002768E File Offset: 0x0002588E
		public static SubsystemVersion Windows8
		{
			get
			{
				return new SubsystemVersion(6, 2);
			}
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x00027697 File Offset: 0x00025897
		private SubsystemVersion(int major, int minor)
		{
			this.Major = major;
			this.Minor = minor;
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x000276A8 File Offset: 0x000258A8
		[NullableContext(1)]
		public static bool TryParse(string str, out SubsystemVersion version)
		{
			version = SubsystemVersion.None;
			if (string.IsNullOrWhiteSpace(str))
			{
				return false;
			}
			int num = str.IndexOf('.');
			string text;
			string text2;
			if (num >= 0)
			{
				if (str.Length == num + 1)
				{
					return false;
				}
				text = str.Substring(0, num);
				text2 = str.Substring(num + 1);
			}
			else
			{
				text = str;
				text2 = null;
			}
			int num2;
			if (text != text.Trim() || !int.TryParse(text, 0, CultureInfo.InvariantCulture, ref num2) || num2 >= 65356 || num2 < 0)
			{
				return false;
			}
			int num3 = 0;
			if (text2 != null && (text2 != text2.Trim() || !int.TryParse(text2, 0, CultureInfo.InvariantCulture, ref num3) || num3 >= 65356 || num3 < 0))
			{
				return false;
			}
			version = new SubsystemVersion(num2, num3);
			return true;
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x0002776F File Offset: 0x0002596F
		public static SubsystemVersion Create(int major, int minor)
		{
			return new SubsystemVersion(major, minor);
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x00027778 File Offset: 0x00025978
		internal static SubsystemVersion Default(OutputKind outputKind, Platform platform)
		{
			if (platform == Platform.Arm)
			{
				return SubsystemVersion.Windows8;
			}
			if (outputKind <= OutputKind.NetModule)
			{
				return new SubsystemVersion(4, 0);
			}
			if (outputKind - OutputKind.WindowsRuntimeMetadata > 1)
			{
				throw new ArgumentOutOfRangeException(CodeAnalysisResources.OutputKindNotSupported, "outputKind");
			}
			return SubsystemVersion.Windows8;
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000DB9 RID: 3513 RVA: 0x000277AD File Offset: 0x000259AD
		public bool IsValid
		{
			get
			{
				return this.Major >= 0 && this.Minor >= 0 && this.Major < 65536 && this.Minor < 65536;
			}
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x000277DD File Offset: 0x000259DD
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is SubsystemVersion && this.Equals((SubsystemVersion)obj);
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x000277F8 File Offset: 0x000259F8
		public override int GetHashCode()
		{
			return Hash.Combine(this.Minor.GetHashCode(), this.Major.GetHashCode());
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x00027826 File Offset: 0x00025A26
		public bool Equals(SubsystemVersion other)
		{
			return this.Major == other.Major && this.Minor == other.Minor;
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x00027848 File Offset: 0x00025A48
		[NullableContext(1)]
		public override string ToString()
		{
			return string.Format("{0}.{1:00}", this.Major, this.Minor);
		}
	}
}
