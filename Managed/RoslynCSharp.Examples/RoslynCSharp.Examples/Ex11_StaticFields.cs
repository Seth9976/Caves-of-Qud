using System;
using UnityEngine;

namespace RoslynCSharp.Example
{
	// Token: 0x0200000D RID: 13
	public class Ex11_StaticFields : MonoBehaviour
	{
		// Token: 0x06000017 RID: 23 RVA: 0x00002520 File Offset: 0x00000720
		public void Start()
		{
			this.domain = ScriptDomain.CreateDomain("Example Domain", true, true, null);
			ScriptType scriptType = this.domain.CompileAndLoadMainSource("\r\n        using UnityEngine;\r\n        class Example\r\n        {\r\n            static string exampleField = \"Hello World\";\r\n        }", ScriptSecurityMode.UseSettings, null);
			string text = (string)scriptType.FieldsStatic["exampleField"];
			Debug.Log(text == "Hello World");
			scriptType.FieldsStatic["exampleField"] = "Goodbye World";
			text = (string)scriptType.SafeFieldsStatic["exampleField"];
			scriptType.SafeFieldsStatic["exampleField"] = text;
		}

		// Token: 0x04000010 RID: 16
		private ScriptDomain domain;

		// Token: 0x04000011 RID: 17
		private const string sourceCode = "\r\n        using UnityEngine;\r\n        class Example\r\n        {\r\n            static string exampleField = \"Hello World\";\r\n        }";
	}
}
