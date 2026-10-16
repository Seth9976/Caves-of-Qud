using System;
using UnityEngine;

namespace RoslynCSharp.Example
{
	// Token: 0x02000011 RID: 17
	public class Ex15_InstanceFields : MonoBehaviour
	{
		// Token: 0x0600001F RID: 31 RVA: 0x000027B0 File Offset: 0x000009B0
		public void Start()
		{
			this.domain = ScriptDomain.CreateDomain("Example Domain", true, true, null);
			ScriptType scriptType = this.domain.CompileAndLoadMainSource("\r\n        using UnityEngine;\r\n        class Example\r\n        {\r\n            string exampleField = \"Hello World\";\r\n        }", ScriptSecurityMode.UseSettings, null);
			scriptType.CreateInstance(null);
			ScriptProxy scriptProxy = scriptType.CreateInstance(null);
			string text = (string)scriptProxy.Fields["exampleField"];
			Debug.Log(text == "Hello World");
			scriptProxy.Fields["exampleField"] = "Goodbye World";
			text = (string)scriptProxy.SafeFields["exampleField"];
			scriptProxy.SafeFields["exampleField"] = text;
		}

		// Token: 0x04000018 RID: 24
		private ScriptDomain domain;

		// Token: 0x04000019 RID: 25
		private const string sourceCode = "\r\n        using UnityEngine;\r\n        class Example\r\n        {\r\n            string exampleField = \"Hello World\";\r\n        }";
	}
}
