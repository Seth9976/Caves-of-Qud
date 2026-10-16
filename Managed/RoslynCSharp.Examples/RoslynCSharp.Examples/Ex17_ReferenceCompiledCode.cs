using System;
using System.IO;
using RoslynCSharp.Compiler;
using UnityEngine;

namespace RoslynCSharp.Example
{
	// Token: 0x02000013 RID: 19
	public class Ex17_ReferenceCompiledCode : MonoBehaviour
	{
		// Token: 0x06000023 RID: 35 RVA: 0x00002910 File Offset: 0x00000B10
		public void Start()
		{
			this.domain = ScriptDomain.CreateDomain("Example Domain", true, true, null);
			ScriptAssembly scriptAssembly = this.domain.CompileAndLoadSource("\r\n        using UnityEngine;\r\n        public class Example\r\n        {\r\n            public void LogToConsole(string arg)\r\n            {\r\n                Debug.Log(arg);\r\n            }\r\n        }", ScriptSecurityMode.UseSettings, null);
			this.domain.CompileAndLoadSource("\r\n        using UnityEngine;\r\n        public class ReferenceExample\r\n        {\r\n            public static void SayHello()\r\n            {\r\n                Example refClass = new Example();\r\n                refClass.LogToConsole(\"Hello World\");\r\n            }\r\n        }", ScriptSecurityMode.UseSettings, new IMetadataReferenceProvider[] { scriptAssembly }).MainType.SafeCallStatic("SayHello");
			AssemblyReference.FromAssembly(typeof(object).Assembly);
			AssemblyReference.FromImage(File.ReadAllBytes("C:/Assemblies/MyAssembly.dll"));
			AssemblyReference.FromStream(File.OpenRead("C:/Assemblies/MyAssembly.dll"));
			AssemblyReference.FromNameOrFile("mscorlib", null);
			AssemblyReference.FromNameOrFile("C:/Assemblies/MyAssembly.dll", null);
		}

		// Token: 0x0400001C RID: 28
		private ScriptDomain domain;

		// Token: 0x0400001D RID: 29
		private const string sourceCodeA = "\r\n        using UnityEngine;\r\n        public class Example\r\n        {\r\n            public void LogToConsole(string arg)\r\n            {\r\n                Debug.Log(arg);\r\n            }\r\n        }";

		// Token: 0x0400001E RID: 30
		private const string sourceCodeB = "\r\n        using UnityEngine;\r\n        public class ReferenceExample\r\n        {\r\n            public static void SayHello()\r\n            {\r\n                Example refClass = new Example();\r\n                refClass.LogToConsole(\"Hello World\");\r\n            }\r\n        }";
	}
}
