using System;
using System.Collections.Generic;
using System.Reflection;
using RoslynCSharp.Compiler;
using UnityEngine;

namespace RoslynCSharp.Example
{
	// Token: 0x02000006 RID: 6
	public class Ex04_CompilerReferences : MonoBehaviour
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002148 File Offset: 0x00000348
		public void Start()
		{
			this.domain = ScriptDomain.CreateDomain("Example Domain", true, true, null);
			if (!this.domain.IsCompilerServiceInitialized)
			{
				throw new InvalidOperationException("Compiler service is not initialized");
			}
			Assembly assembly = typeof(HashSet<>).Assembly;
			this.domain.RoslynCompilerService.ReferenceAssemblies.Add(AssemblyReference.FromAssembly(assembly));
			string text = "path/to/reference/assembly.dll";
			this.domain.RoslynCompilerService.ReferenceAssemblies.Add(AssemblyReference.FromNameOrFile(text, null));
		}

		// Token: 0x04000004 RID: 4
		private ScriptDomain domain;
	}
}
