using System;

namespace UnityEngine
{
	// Token: 0x020002B8 RID: 696
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
	public class UnityAPICompatibilityVersionAttribute : Attribute
	{
		// Token: 0x060021E1 RID: 8673 RVA: 0x00042156 File Offset: 0x00040356
		[Obsolete("This overload of the attribute has been deprecated. Use the constructor that takes the version and a boolean", true)]
		public UnityAPICompatibilityVersionAttribute(string version)
		{
			this._version = version;
		}

		// Token: 0x060021E2 RID: 8674 RVA: 0x00042168 File Offset: 0x00040368
		public UnityAPICompatibilityVersionAttribute(string version, bool checkOnlyUnityVersion)
		{
			bool flag = !checkOnlyUnityVersion;
			if (flag)
			{
				throw new ArgumentException("You must pass 'true' to checkOnlyUnityVersion parameter.");
			}
			this._version = version;
		}

		// Token: 0x060021E3 RID: 8675 RVA: 0x00042197 File Offset: 0x00040397
		public UnityAPICompatibilityVersionAttribute(string version, string[] configurationAssembliesHashes)
		{
			this._version = version;
			this._configurationAssembliesHashes = configurationAssembliesHashes;
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x060021E4 RID: 8676 RVA: 0x000421B0 File Offset: 0x000403B0
		public string version
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x060021E5 RID: 8677 RVA: 0x000421C8 File Offset: 0x000403C8
		internal string[] configurationAssembliesHashes
		{
			get
			{
				return this._configurationAssembliesHashes;
			}
		}

		// Token: 0x040009E5 RID: 2533
		private string _version;

		// Token: 0x040009E6 RID: 2534
		private string[] _configurationAssembliesHashes;
	}
}
