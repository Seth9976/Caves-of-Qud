using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200028A RID: 650
	[RequiredByNativeCode(GenerateProxy = true)]
	[NativeHeader("Runtime/Mono/AssemblyFullName.h")]
	internal struct AssemblyFullName
	{
		// Token: 0x06002046 RID: 8262 RVA: 0x0003E454 File Offset: 0x0003C654
		public override bool Equals(object other)
		{
			if (other is AssemblyFullName)
			{
				AssemblyFullName assemblyFullName = (AssemblyFullName)other;
				if (this.Name == assemblyFullName.Name && this.Version == assemblyFullName.Version && this.PublicKeyToken == assemblyFullName.PublicKeyToken)
				{
					return this.Culture == assemblyFullName.Culture;
				}
			}
			return false;
		}

		// Token: 0x06002047 RID: 8263 RVA: 0x0003E4C4 File Offset: 0x0003C6C4
		public override int GetHashCode()
		{
			return HashCode.Combine<string, AssemblyVersion, string, string>(this.Name, this.Version, this.PublicKeyToken, this.Culture);
		}

		// Token: 0x06002048 RID: 8264 RVA: 0x0003E4F4 File Offset: 0x0003C6F4
		public override string ToString()
		{
			return string.Format("{0}, Version={1}, Culture={2}, PublicKeyToken={3}", new object[]
			{
				this.Name,
				this.Version,
				string.IsNullOrEmpty(this.Culture) ? "neutral" : this.Culture,
				this.PublicKeyToken
			});
		}

		// Token: 0x0400099A RID: 2458
		[NativeName("name")]
		public string Name;

		// Token: 0x0400099B RID: 2459
		[NativeName("version")]
		public AssemblyVersion Version;

		// Token: 0x0400099C RID: 2460
		[NativeName("publicKeyToken")]
		public string PublicKeyToken;

		// Token: 0x0400099D RID: 2461
		[NativeName("culture")]
		public string Culture;
	}
}
