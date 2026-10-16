using System;
using System.Reflection;

namespace RoslynCSharp
{
	// Token: 0x02000004 RID: 4
	public sealed class AsyncLoadOperation : AsyncOperation
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002354 File Offset: 0x00000554
		public ScriptDomain LoadDomain
		{
			get
			{
				ScriptDomain scriptDomain = this.loadDomain;
				ScriptDomain scriptDomain2;
				lock (scriptDomain)
				{
					scriptDomain2 = this.loadDomain;
				}
				return scriptDomain2;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002398 File Offset: 0x00000598
		public ScriptAssembly LoadedAssembly
		{
			get
			{
				object obj = this.assemblyAccessLock;
				ScriptAssembly scriptAssembly;
				lock (obj)
				{
					scriptAssembly = this.loadResult;
				}
				return scriptAssembly;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000023DC File Offset: 0x000005DC
		public bool IsSecurityVerified
		{
			get
			{
				return this.isSecurityVerified;
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000023E4 File Offset: 0x000005E4
		internal AsyncLoadOperation(ScriptDomain domain, AssemblyName assemblyName, ScriptSecurityMode securityMode)
		{
			this.loadDomain = domain;
			this.asmName = assemblyName;
			this.loadType = AsyncLoadOperation.AssemblyLoadType.LoadByName;
			this.securityMode = securityMode;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002413 File Offset: 0x00000613
		internal AsyncLoadOperation(ScriptDomain domain, string assemblyPath, ScriptSecurityMode securityMode)
		{
			this.loadDomain = domain;
			this.asmPath = assemblyPath;
			this.loadType = AsyncLoadOperation.AssemblyLoadType.LoadByPath;
			this.securityMode = securityMode;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002442 File Offset: 0x00000642
		internal AsyncLoadOperation(ScriptDomain domain, byte[] assemblyBytes, ScriptSecurityMode securityMode)
		{
			this.loadDomain = domain;
			this.asmBytes = assemblyBytes;
			this.loadType = AsyncLoadOperation.AssemblyLoadType.LoadFromBytes;
			this.securityMode = securityMode;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002474 File Offset: 0x00000674
		protected override void RunAsyncOperation()
		{
			ScriptAssembly scriptAssembly = null;
			ScriptDomain scriptDomain = this.loadDomain;
			lock (scriptDomain)
			{
				switch (this.loadType)
				{
				case AsyncLoadOperation.AssemblyLoadType.LoadByName:
					scriptAssembly = this.loadDomain.LoadAssembly(this.asmName, this.securityMode);
					break;
				case AsyncLoadOperation.AssemblyLoadType.LoadByPath:
					scriptAssembly = this.loadDomain.LoadAssembly(this.asmPath, this.securityMode);
					break;
				case AsyncLoadOperation.AssemblyLoadType.LoadFromBytes:
					scriptAssembly = this.loadDomain.LoadAssembly(this.asmBytes, this.securityMode);
					break;
				}
				object obj = this.assemblyAccessLock;
				lock (obj)
				{
					this.loadResult = scriptAssembly;
				}
			}
			this.isSuccessful = scriptAssembly != null;
			this.isSecurityVerified = this.loadDomain.SecurityResult != null && this.loadDomain.SecurityResult.IsSecurityVerified;
		}

		// Token: 0x04000009 RID: 9
		private object assemblyAccessLock = new object();

		// Token: 0x0400000A RID: 10
		private ScriptDomain loadDomain;

		// Token: 0x0400000B RID: 11
		private ScriptAssembly loadResult;

		// Token: 0x0400000C RID: 12
		private ScriptSecurityMode securityMode;

		// Token: 0x0400000D RID: 13
		private bool isSecurityVerified;

		// Token: 0x0400000E RID: 14
		private AsyncLoadOperation.AssemblyLoadType loadType;

		// Token: 0x0400000F RID: 15
		private AssemblyName asmName;

		// Token: 0x04000010 RID: 16
		private string asmPath;

		// Token: 0x04000011 RID: 17
		private byte[] asmBytes;

		// Token: 0x02000013 RID: 19
		private enum AssemblyLoadType
		{
			// Token: 0x04000068 RID: 104
			LoadByName,
			// Token: 0x04000069 RID: 105
			LoadByPath,
			// Token: 0x0400006A RID: 106
			LoadFromBytes
		}
	}
}
