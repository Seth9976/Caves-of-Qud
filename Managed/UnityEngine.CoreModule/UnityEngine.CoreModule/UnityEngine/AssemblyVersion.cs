using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000289 RID: 649
	[NativeHeader("Runtime/Mono/AssemblyFullName.h")]
	[RequiredByNativeCode(GenerateProxy = true)]
	internal struct AssemblyVersion
	{
		// Token: 0x0600203E RID: 8254 RVA: 0x0003E180 File Offset: 0x0003C380
		public AssemblyVersion(ushort major, ushort minor, ushort build, ushort revision)
		{
			this.major = major;
			this.minor = minor;
			this.build = build;
			this.revision = revision;
		}

		// Token: 0x0600203F RID: 8255 RVA: 0x0003E1A0 File Offset: 0x0003C3A0
		public static bool operator ==(AssemblyVersion lhs, AssemblyVersion rhs)
		{
			return lhs.major == rhs.major && lhs.minor == rhs.minor && lhs.build == rhs.build && lhs.revision == rhs.revision;
		}

		// Token: 0x06002040 RID: 8256 RVA: 0x0003E1F0 File Offset: 0x0003C3F0
		public static bool operator !=(AssemblyVersion lhs, AssemblyVersion rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06002041 RID: 8257 RVA: 0x0003E20C File Offset: 0x0003C40C
		public static bool operator <(AssemblyVersion lhs, AssemblyVersion rhs)
		{
			bool flag = lhs.major != rhs.major;
			bool flag2;
			if (flag)
			{
				flag2 = lhs.major < rhs.major;
			}
			else
			{
				bool flag3 = lhs.minor != rhs.minor;
				if (flag3)
				{
					flag2 = lhs.minor < rhs.minor;
				}
				else
				{
					bool flag4 = lhs.build != rhs.build;
					if (flag4)
					{
						flag2 = lhs.build < rhs.build;
					}
					else
					{
						bool flag5 = lhs.revision != rhs.revision;
						flag2 = flag5 && lhs.revision < rhs.revision;
					}
				}
			}
			return flag2;
		}

		// Token: 0x06002042 RID: 8258 RVA: 0x0003E2BC File Offset: 0x0003C4BC
		public static bool operator >(AssemblyVersion lhs, AssemblyVersion rhs)
		{
			bool flag = lhs.major != rhs.major;
			bool flag2;
			if (flag)
			{
				flag2 = lhs.major > rhs.major;
			}
			else
			{
				bool flag3 = lhs.minor != rhs.minor;
				if (flag3)
				{
					flag2 = lhs.minor > rhs.minor;
				}
				else
				{
					bool flag4 = lhs.build != rhs.build;
					if (flag4)
					{
						flag2 = lhs.build > rhs.build;
					}
					else
					{
						bool flag5 = lhs.revision != rhs.revision;
						flag2 = flag5 && lhs.revision > rhs.revision;
					}
				}
			}
			return flag2;
		}

		// Token: 0x06002043 RID: 8259 RVA: 0x0003E36C File Offset: 0x0003C56C
		public override string ToString()
		{
			return string.Format("{0}.{1}.{2}.{3}", new object[] { this.major, this.minor, this.build, this.revision });
		}

		// Token: 0x06002044 RID: 8260 RVA: 0x0003E3C8 File Offset: 0x0003C5C8
		public override bool Equals(object other)
		{
			if (other is AssemblyVersion)
			{
				AssemblyVersion assemblyVersion = (AssemblyVersion)other;
				if (this.major == assemblyVersion.major && this.minor == assemblyVersion.minor && this.build == assemblyVersion.build)
				{
					return this.revision == assemblyVersion.revision;
				}
			}
			return false;
		}

		// Token: 0x06002045 RID: 8261 RVA: 0x0003E424 File Offset: 0x0003C624
		public override int GetHashCode()
		{
			return HashCode.Combine<ushort, ushort, ushort, ushort>(this.major, this.minor, this.build, this.revision);
		}

		// Token: 0x04000996 RID: 2454
		public ushort major;

		// Token: 0x04000997 RID: 2455
		public ushort minor;

		// Token: 0x04000998 RID: 2456
		public ushort build;

		// Token: 0x04000999 RID: 2457
		public ushort revision;
	}
}
