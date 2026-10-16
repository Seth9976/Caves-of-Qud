using System;
using UnityEngine;

namespace RoslynCSharp.Example
{
	// Token: 0x0200000F RID: 15
	public class Ex13_CreateInstance : MonoBehaviour
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00002660 File Offset: 0x00000860
		public void Start()
		{
			this.domain = ScriptDomain.CreateDomain("Example Domain", true, true, null);
			ScriptAssembly scriptAssembly = this.domain.CompileAndLoadSource("\r\n        using UnityEngine;\r\n        class Example\r\n        {\r\n            public Example() { }\r\n\r\n            public Example(string arg)\r\n            {\r\n                Debug.Log(\"Example_ctor: \" + arg);\r\n            }\r\n        }\r\n\r\n        class ExampleBehaviour : MonoBehaviour\r\n        {\r\n            void Start()\r\n            {\r\n                Debug.Log(\"ExampleBehaviour: Start\");\r\n            }\r\n        }\r\n\r\n        class ExampleScriptable : ScriptableObject\r\n        {\r\n            void OnEnable()\r\n            {\r\n                Debug.Log(\"ExampleScriptable: OnEnable\");\r\n            }\r\n        }", ScriptSecurityMode.UseSettings, null);
			ScriptType scriptType = scriptAssembly.FindType("Example");
			ScriptType scriptType2 = scriptAssembly.FindSubTypeOf<MonoBehaviour>("ExampleBehaviour", true);
			ScriptType scriptType3 = scriptAssembly.FindSubTypeOf<ScriptableObject>("ExampleScriptable", true);
			scriptType.CreateInstance(null);
			scriptType.CreateInstance(null, new object[] { "Hello World" });
			ScriptProxy scriptProxy = scriptType2.CreateInstance(base.gameObject);
			Debug.Log(scriptProxy.IsMonoBehaviour);
			Debug.Log(scriptProxy.IsUnityObject);
			scriptType3.CreateInstance(null);
			Debug.Log(scriptType3.IsScriptableObject);
			Debug.Log(scriptType3.IsUnityObject);
		}

		// Token: 0x04000014 RID: 20
		private ScriptDomain domain;

		// Token: 0x04000015 RID: 21
		private const string sourceCode = "\r\n        using UnityEngine;\r\n        class Example\r\n        {\r\n            public Example() { }\r\n\r\n            public Example(string arg)\r\n            {\r\n                Debug.Log(\"Example_ctor: \" + arg);\r\n            }\r\n        }\r\n\r\n        class ExampleBehaviour : MonoBehaviour\r\n        {\r\n            void Start()\r\n            {\r\n                Debug.Log(\"ExampleBehaviour: Start\");\r\n            }\r\n        }\r\n\r\n        class ExampleScriptable : ScriptableObject\r\n        {\r\n            void OnEnable()\r\n            {\r\n                Debug.Log(\"ExampleScriptable: OnEnable\");\r\n            }\r\n        }";
	}
}
