using System;
using RoslynCSharp.Compiler;
using UnityEngine;

namespace RoslynCSharp.Example
{
	// Token: 0x02000008 RID: 8
	public class Ex06_CompileFromSource : MonoBehaviour
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002234 File Offset: 0x00000434
		public void Start()
		{
			this.domain = ScriptDomain.CreateDomain("Example Domain", true, true, null);
			this.domain.CompileAndLoadSource("\r\n        using UnityEngine;\r\n        class Example\r\n        {\r\n            static void ExampleMethod()\r\n            {\r\n                Debug.Log(\"Hello World\");\r\n            }\r\n        }", ScriptSecurityMode.UseSettings, null);
			if (!this.domain.CompileResult.Success)
			{
				foreach (CompilationError compilationError in this.domain.CompileResult.Errors)
				{
					if (compilationError.IsError)
					{
						Debug.LogError(compilationError.ToString());
					}
					else if (compilationError.IsWarning)
					{
						Debug.LogWarning(compilationError.ToString());
					}
				}
			}
		}

		// Token: 0x04000006 RID: 6
		private ScriptDomain domain;

		// Token: 0x04000007 RID: 7
		private const string sourceCode = "\r\n        using UnityEngine;\r\n        class Example\r\n        {\r\n            static void ExampleMethod()\r\n            {\r\n                Debug.Log(\"Hello World\");\r\n            }\r\n        }";
	}
}
