using System;
using UnityEngine;

namespace RoslynCSharp.Example
{
	// Token: 0x0200000B RID: 11
	public class Ex09_FindTypes : MonoBehaviour
	{
		// Token: 0x06000013 RID: 19 RVA: 0x0000237C File Offset: 0x0000057C
		public void Start()
		{
			this.domain = ScriptDomain.CreateDomain("Example Domain", true, true, null);
			ScriptAssembly scriptAssembly = this.domain.CompileAndLoadSource("\r\n        using UnityEngine;\r\n        class Example : MonoBehaviour\r\n        {\r\n            static void ExampleMethod(string input)\r\n            {\r\n                Debug.Log(\"Hello \" + input);\r\n            }\r\n        }", ScriptSecurityMode.UseSettings, null);
			bool flag = true;
			foreach (ScriptType scriptType in scriptAssembly.FindAllTypes(flag, true))
			{
				Debug.Log("FindAllTypes: " + scriptType.FullName);
			}
			foreach (ScriptType scriptType2 in scriptAssembly.EnumerateAllTypes(flag, true))
			{
				Debug.Log("EnumerateAllTypes: " + scriptType2.FullName);
			}
			foreach (ScriptType scriptType3 in scriptAssembly.EnumerateAllSubTypesOf<MonoBehaviour>(flag, true))
			{
				Debug.Log("EnumerateSubTypesOf<MonoBehaviour>: " + scriptType3.FullName);
			}
			scriptAssembly.EnumerateAllMonoBehaviourTypes(flag, true);
			scriptAssembly.EnumerateAllScriptableObjectTypes(flag, true);
			scriptAssembly.EnumerateAllUnityTypes(flag, true);
		}

		// Token: 0x0400000C RID: 12
		private ScriptDomain domain;

		// Token: 0x0400000D RID: 13
		private const string sourceCode = "\r\n        using UnityEngine;\r\n        class Example : MonoBehaviour\r\n        {\r\n            static void ExampleMethod(string input)\r\n            {\r\n                Debug.Log(\"Hello \" + input);\r\n            }\r\n        }";
	}
}
