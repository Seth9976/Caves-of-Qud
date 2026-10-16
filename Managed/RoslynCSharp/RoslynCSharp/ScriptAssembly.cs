using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.CodeAnalysis;
using RoslynCSharp.Compiler;
using Trivial.CodeSecurity;
using UnityEngine;

namespace RoslynCSharp
{
	// Token: 0x02000007 RID: 7
	public sealed class ScriptAssembly : IMetadataReferenceProvider
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002821 File Offset: 0x00000A21
		public string Name
		{
			get
			{
				return this.rawAssembly.GetName().Name;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002833 File Offset: 0x00000A33
		// (set) Token: 0x06000036 RID: 54 RVA: 0x0000283B File Offset: 0x00000A3B
		public string AssemblyPath
		{
			get
			{
				return this.assemblyPath;
			}
			internal set
			{
				this.assemblyPath = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002844 File Offset: 0x00000A44
		// (set) Token: 0x06000038 RID: 56 RVA: 0x0000284C File Offset: 0x00000A4C
		public byte[] AssemblyImage
		{
			get
			{
				return this.rawAssemblyImage;
			}
			internal set
			{
				this.rawAssemblyImage = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002855 File Offset: 0x00000A55
		public Version Version
		{
			get
			{
				return this.rawAssembly.GetName().Version;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002867 File Offset: 0x00000A67
		public string FullName
		{
			get
			{
				return this.rawAssembly.FullName;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002874 File Offset: 0x00000A74
		public MetadataReference Reference
		{
			get
			{
				if (this.compileResult != null)
				{
					return this.compileResult.Reference;
				}
				return null;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600003C RID: 60 RVA: 0x0000288B File Offset: 0x00000A8B
		public DateTime RuntimeCompiledTime
		{
			get
			{
				return this.runtimeCompiledTime;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002893 File Offset: 0x00000A93
		public bool IsRuntimeCompiled
		{
			get
			{
				return this.isRuntimeCompiled;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600003E RID: 62 RVA: 0x0000289B File Offset: 0x00000A9B
		public CompilationResult CompileResult
		{
			get
			{
				return this.compileResult;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000028A3 File Offset: 0x00000AA3
		public bool IsSecurityValidated
		{
			get
			{
				return this.isSecurityValidated;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000028AB File Offset: 0x00000AAB
		public ScriptDomain Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000028B4 File Offset: 0x00000AB4
		public ScriptType MainType
		{
			get
			{
				if (this.scriptTypes.Count == 0)
				{
					return null;
				}
				ScriptType scriptType = null;
				using (Dictionary<string, ScriptType>.ValueCollection.Enumerator enumerator = this.scriptTypes.Values.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						scriptType = enumerator.Current;
					}
				}
				return scriptType;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000042 RID: 66 RVA: 0x0000291C File Offset: 0x00000B1C
		public Assembly RawAssembly
		{
			get
			{
				return this.rawAssembly;
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002924 File Offset: 0x00000B24
		internal ScriptAssembly(ScriptDomain domain, Assembly rawAssembly, CompilationResult compileResult = null)
		{
			this.domain = domain;
			this.rawAssembly = rawAssembly;
			this.compileResult = compileResult;
			foreach (Type type in rawAssembly.GetTypes())
			{
				if (!type.IsNested)
				{
					ScriptType scriptType = new ScriptType(this, null, type);
					this.scriptTypes.Add(type.FullName, scriptType);
				}
			}
			ScriptAssembly.matchedTypes.AddRange(this.scriptTypes.Values);
			foreach (ScriptType scriptType2 in ScriptAssembly.matchedTypes)
			{
				this.CreateNestedTypes(scriptType2);
			}
			ScriptAssembly.matchedTypes.Clear();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002A0C File Offset: 0x00000C0C
		public bool SecurityCheckAssembly(CodeSecurityRestrictions restrictions)
		{
			if (this.isSecurityValidated && restrictions.RestrictionsHash == this.securityValidatedHash)
			{
				return true;
			}
			CodeSecurityEngine codeSecurityEngine = this.CreateSecurityEngine();
			if (codeSecurityEngine == null)
			{
				return this.isSecurityValidated;
			}
			bool flag;
			using (codeSecurityEngine)
			{
				this.isSecurityValidated = codeSecurityEngine.SecurityCheckAssembly(restrictions, out this.securityReport);
				if (this.isSecurityValidated)
				{
					this.securityValidatedHash = restrictions.RestrictionsHash;
				}
				else
				{
					this.securityValidatedHash = -1;
				}
				flag = this.isSecurityValidated;
			}
			return flag;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002A98 File Offset: 0x00000C98
		public bool SecurityCheckAssembly(CodeSecurityRestrictions restrictions, out CodeSecurityReport report)
		{
			if (this.isSecurityValidated && restrictions.RestrictionsHash == this.securityValidatedHash)
			{
				report = this.securityReport;
				return true;
			}
			CodeSecurityEngine codeSecurityEngine = this.CreateSecurityEngine();
			if (codeSecurityEngine == null)
			{
				report = this.securityReport;
				return this.isSecurityValidated;
			}
			bool flag;
			using (codeSecurityEngine)
			{
				this.isSecurityValidated = codeSecurityEngine.SecurityCheckAssembly(restrictions, out this.securityReport);
				if (this.isSecurityValidated)
				{
					this.securityValidatedHash = restrictions.RestrictionsHash;
				}
				else
				{
					this.securityValidatedHash = -1;
				}
				report = this.securityReport;
				flag = this.isSecurityValidated;
			}
			return flag;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002B3C File Offset: 0x00000D3C
		private CodeSecurityEngine CreateSecurityEngine()
		{
			if (this.isRuntimeCompiled)
			{
				return this.CreateSecurityEngine(this.compileResult);
			}
			if (this.assemblyPath != null)
			{
				return new CodeSecurityEngine(this.assemblyPath);
			}
			if (this.rawAssemblyImage != null)
			{
				return new CodeSecurityEngine(this.rawAssemblyImage, null);
			}
			return null;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002B88 File Offset: 0x00000D88
		private CodeSecurityEngine CreateSecurityEngine(CompilationResult result)
		{
			if (!result.Success)
			{
				return null;
			}
			if (result.OutputAssemblyImage != null)
			{
				return new CodeSecurityEngine(result.OutputAssemblyImage, null);
			}
			if (result.OutputFile != null)
			{
				return new CodeSecurityEngine(result.OutputFile);
			}
			if (result.OutputAssembly != null)
			{
				return new CodeSecurityEngine(result.OutputAssembly.Location);
			}
			return null;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002BE8 File Offset: 0x00000DE8
		public bool HasType(string name)
		{
			return this.FindType(name) != null;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002BF4 File Offset: 0x00000DF4
		public bool HasSubTypeOf(Type subType)
		{
			return this.FindSubTypeOf(subType, true, true) != null;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002C02 File Offset: 0x00000E02
		public bool HasSubTypeOf(Type subType, string name)
		{
			return this.FindSubTypeOf(subType, name) != null;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002C0F File Offset: 0x00000E0F
		public bool HasSubTypeOf<T>()
		{
			return this.FindSubTypeOf<T>(true, true) != null;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002C1C File Offset: 0x00000E1C
		public bool HasSubTypeOf<T>(string name)
		{
			return this.FindSubTypeOf<T>(name, true) != null;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002C2C File Offset: 0x00000E2C
		public ScriptType FindType(string name)
		{
			Type type = this.rawAssembly.GetType(name, false, false);
			if (type == null)
			{
				return null;
			}
			return this.scriptTypes[type.FullName];
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002C64 File Offset: 0x00000E64
		public ScriptType FindSubTypeOf(Type subType, bool includeNonPublic = true, bool findNestedTypes = true)
		{
			foreach (ScriptType scriptType in this.scriptTypes.Values)
			{
				if ((includeNonPublic || scriptType.IsPublic) && (!scriptType.IsNestedType || findNestedTypes) && scriptType.IsSubTypeOf(subType))
				{
					return scriptType;
				}
			}
			return null;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002CDC File Offset: 0x00000EDC
		public ScriptType FindSubTypeOf(Type subType, string name)
		{
			ScriptType scriptType = this.FindType(name);
			if (scriptType == null)
			{
				return null;
			}
			if (scriptType.IsSubTypeOf(subType))
			{
				return scriptType;
			}
			return null;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002D02 File Offset: 0x00000F02
		public ScriptType FindSubTypeOf<T>(bool includeNonPublic = true, bool findNestedTypes = true)
		{
			return this.FindSubTypeOf(typeof(T), includeNonPublic, findNestedTypes);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002D16 File Offset: 0x00000F16
		public ScriptType FindSubTypeOf<T>(string name, bool findNestedTypes = true)
		{
			return this.FindSubTypeOf(typeof(T), name);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002D2C File Offset: 0x00000F2C
		public ScriptType[] FindAllSubTypesOf(Type subType, bool includeNonPublic = true, bool findNestedTypes = true)
		{
			ScriptAssembly.matchedTypes.Clear();
			foreach (ScriptType scriptType in this.scriptTypes.Values)
			{
				if ((includeNonPublic || scriptType.IsPublic) && (!scriptType.IsNestedType || findNestedTypes) && scriptType.IsSubTypeOf(subType))
				{
					ScriptAssembly.matchedTypes.Add(scriptType);
				}
			}
			return ScriptAssembly.matchedTypes.ToArray();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002DBC File Offset: 0x00000FBC
		public ScriptType[] FindAllSubTypesOf<T>(bool includeNonPublic = true, bool findNestedTypes = true)
		{
			return this.FindAllSubTypesOf(typeof(T), includeNonPublic, findNestedTypes);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002DD0 File Offset: 0x00000FD0
		public ScriptType[] FindAllTypes(bool includeNonPublic = true, bool findNestedTypes = true)
		{
			ScriptAssembly.matchedTypes.Clear();
			ScriptAssembly.matchedTypes.AddRange(this.scriptTypes.Values);
			if (!findNestedTypes)
			{
				ScriptAssembly.matchedTypes.RemoveAll((ScriptType t) => t.IsNestedType);
			}
			return ScriptAssembly.matchedTypes.ToArray();
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002E33 File Offset: 0x00001033
		public ScriptType[] FindAllUnityTypes(bool includeNonPublic = true, bool findNestedTypes = true)
		{
			return this.FindAllSubTypesOf<global::UnityEngine.Object>(includeNonPublic, findNestedTypes);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002E3D File Offset: 0x0000103D
		public ScriptType[] FindAllMonoBehaviourTypes(bool includeNonPublic = true, bool findNestedTypes = true)
		{
			return this.FindAllSubTypesOf<MonoBehaviour>(includeNonPublic, findNestedTypes);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002E47 File Offset: 0x00001047
		public ScriptType[] FindAllScriptableObjectTypes(bool includeNonPublic = true, bool findNestedTypes = true)
		{
			return this.FindAllSubTypesOf<ScriptableObject>(includeNonPublic, findNestedTypes);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002E51 File Offset: 0x00001051
		public IEnumerable<ScriptType> EnumerateAllSubTypesOf(Type subType, bool includeNonPublic = true, bool enumerateNestedTypes = true)
		{
			foreach (ScriptType scriptType in this.scriptTypes.Values)
			{
				if ((includeNonPublic || scriptType.IsPublic) && (!scriptType.IsNestedType || enumerateNestedTypes) && scriptType.IsSubTypeOf(subType))
				{
					yield return scriptType;
				}
			}
			Dictionary<string, ScriptType>.ValueCollection.Enumerator enumerator = default(Dictionary<string, ScriptType>.ValueCollection.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002E76 File Offset: 0x00001076
		public IEnumerable<ScriptType> EnumerateAllSubTypesOf<T>(bool includeNonPublic = true, bool enumerateNestedTypes = true)
		{
			return this.EnumerateAllSubTypesOf(typeof(T), includeNonPublic, enumerateNestedTypes);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002E8A File Offset: 0x0000108A
		public IEnumerable<ScriptType> EnumerateAllTypes(bool includeNonPublic = true, bool enumerateNestedTypes = true)
		{
			foreach (ScriptType scriptType in this.scriptTypes.Values)
			{
				if ((includeNonPublic || scriptType.IsPublic) && (!scriptType.IsNestedType || enumerateNestedTypes))
				{
					yield return scriptType;
				}
			}
			Dictionary<string, ScriptType>.ValueCollection.Enumerator enumerator = default(Dictionary<string, ScriptType>.ValueCollection.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002EA8 File Offset: 0x000010A8
		public IEnumerable<ScriptType> EnumerateAllUnityTypes(bool includeNonPublic = true, bool enumerateNestedTypes = true)
		{
			return this.EnumerateAllSubTypesOf<global::UnityEngine.Object>(includeNonPublic, enumerateNestedTypes);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002EB2 File Offset: 0x000010B2
		public IEnumerable<ScriptType> EnumerateAllMonoBehaviourTypes(bool includeNonPublic = true, bool enumerateNestedTypes = true)
		{
			return this.EnumerateAllSubTypesOf<MonoBehaviour>(includeNonPublic, enumerateNestedTypes);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002EBC File Offset: 0x000010BC
		public IEnumerable<ScriptType> EnumerateAllScriptableObjectTypes(bool includeNonPublic = true, bool enumerateNestedTypes = true)
		{
			return this.EnumerateAllSubTypesOf<ScriptableObject>(includeNonPublic, enumerateNestedTypes);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002EC6 File Offset: 0x000010C6
		internal void MarkAsRuntimeCompiled()
		{
			this.isRuntimeCompiled = true;
			this.runtimeCompiledTime = DateTime.Now;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002EDC File Offset: 0x000010DC
		private void CreateNestedTypes(ScriptType type)
		{
			foreach (Type type2 in type.RawType.GetNestedTypes(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				ScriptType scriptType = new ScriptType(this, type, type2);
				this.scriptTypes.Add(scriptType.FullName, scriptType);
				this.CreateNestedTypes(scriptType);
			}
		}

		// Token: 0x04000025 RID: 37
		private static List<ScriptType> matchedTypes = new List<ScriptType>();

		// Token: 0x04000026 RID: 38
		private ScriptDomain domain;

		// Token: 0x04000027 RID: 39
		private string assemblyPath;

		// Token: 0x04000028 RID: 40
		private byte[] rawAssemblyImage;

		// Token: 0x04000029 RID: 41
		private Assembly rawAssembly;

		// Token: 0x0400002A RID: 42
		private CodeSecurityReport securityReport;

		// Token: 0x0400002B RID: 43
		private CompilationResult compileResult;

		// Token: 0x0400002C RID: 44
		private Dictionary<string, ScriptType> scriptTypes = new Dictionary<string, ScriptType>();

		// Token: 0x0400002D RID: 45
		private DateTime runtimeCompiledTime = DateTime.MinValue;

		// Token: 0x0400002E RID: 46
		private bool isRuntimeCompiled;

		// Token: 0x0400002F RID: 47
		private bool isSecurityValidated;

		// Token: 0x04000030 RID: 48
		private int securityValidatedHash = -1;
	}
}
