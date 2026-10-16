using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cysharp.Threading.Tasks.Internal
{
	// Token: 0x0200010B RID: 267
	internal static class DiagnosticsExtensions
	{
		// Token: 0x06000615 RID: 1557 RVA: 0x0000D8E8 File Offset: 0x0000BAE8
		public static string CleanupAsyncStackTrace(this StackTrace stackTrace)
		{
			if (stackTrace == null)
			{
				return "";
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < stackTrace.FrameCount; i++)
			{
				StackFrame frame = stackTrace.GetFrame(i);
				MethodBase method = frame.GetMethod();
				if (!DiagnosticsExtensions.IgnoreLine(method))
				{
					if (DiagnosticsExtensions.IsAsync(method))
					{
						stringBuilder.Append("async ");
						Type type;
						DiagnosticsExtensions.TryResolveStateMachineMethod(ref method, out type);
					}
					MethodInfo methodInfo = method as MethodInfo;
					if (methodInfo != null)
					{
						stringBuilder.Append(DiagnosticsExtensions.BeautifyType(methodInfo.ReturnType, false));
						stringBuilder.Append(" ");
					}
					stringBuilder.Append(DiagnosticsExtensions.BeautifyType(method.DeclaringType, false));
					if (!method.IsConstructor)
					{
						stringBuilder.Append(".");
					}
					stringBuilder.Append(method.Name);
					if (method.IsGenericMethod)
					{
						stringBuilder.Append("<");
						foreach (Type type2 in method.GetGenericArguments())
						{
							stringBuilder.Append(DiagnosticsExtensions.BeautifyType(type2, true));
						}
						stringBuilder.Append(">");
					}
					stringBuilder.Append("(");
					stringBuilder.Append(string.Join(", ", from p in method.GetParameters()
						select DiagnosticsExtensions.BeautifyType(p.ParameterType, true) + " " + p.Name));
					stringBuilder.Append(")");
					if (DiagnosticsExtensions.displayFilenames && frame.GetILOffset() != -1)
					{
						string text = null;
						try
						{
							text = frame.GetFileName();
						}
						catch (NotSupportedException)
						{
							DiagnosticsExtensions.displayFilenames = false;
						}
						catch (SecurityException)
						{
							DiagnosticsExtensions.displayFilenames = false;
						}
						if (text != null)
						{
							stringBuilder.Append(' ');
							stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "(at {0})", DiagnosticsExtensions.AppendHyperLink(text, frame.GetFileLineNumber().ToString()));
						}
					}
					stringBuilder.AppendLine();
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x0000DAE4 File Offset: 0x0000BCE4
		private static bool IsAsync(MethodBase methodInfo)
		{
			Type declaringType = methodInfo.DeclaringType;
			return typeof(IAsyncStateMachine).IsAssignableFrom(declaringType);
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0000DB08 File Offset: 0x0000BD08
		private static bool TryResolveStateMachineMethod(ref MethodBase method, out Type declaringType)
		{
			declaringType = method.DeclaringType;
			Type declaringType2 = declaringType.DeclaringType;
			if (declaringType2 == null)
			{
				return false;
			}
			MethodInfo[] methods = declaringType2.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			if (methods == null)
			{
				return false;
			}
			foreach (MethodInfo methodInfo in methods)
			{
				IEnumerable<StateMachineAttribute> customAttributes = methodInfo.GetCustomAttributes<StateMachineAttribute>(false);
				if (customAttributes != null)
				{
					foreach (StateMachineAttribute stateMachineAttribute in customAttributes)
					{
						if (stateMachineAttribute.StateMachineType == declaringType)
						{
							method = methodInfo;
							declaringType = methodInfo.DeclaringType;
							return stateMachineAttribute is IteratorStateMachineAttribute;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0000DBCC File Offset: 0x0000BDCC
		private static string BeautifyType(Type t, bool shortName)
		{
			string text;
			if (DiagnosticsExtensions.builtInTypeNames.TryGetValue(t, out text))
			{
				return text;
			}
			if (t.IsGenericParameter)
			{
				return t.Name;
			}
			if (t.IsArray)
			{
				return DiagnosticsExtensions.BeautifyType(t.GetElementType(), shortName) + "[]";
			}
			string fullName = t.FullName;
			if (fullName != null && fullName.StartsWith("System.ValueTuple"))
			{
				return "(" + string.Join(", ", from x in t.GetGenericArguments()
					select DiagnosticsExtensions.BeautifyType(x, true)) + ")";
			}
			if (!t.IsGenericType)
			{
				string text2;
				if (!shortName)
				{
					if ((text2 = t.FullName.Replace("Cysharp.Threading.Tasks.Triggers.", "").Replace("Cysharp.Threading.Tasks.Internal.", "").Replace("Cysharp.Threading.Tasks.", "")) == null)
					{
						return t.Name;
					}
				}
				else
				{
					text2 = t.Name;
				}
				return text2;
			}
			string text3 = string.Join(", ", from x in t.GetGenericArguments()
				select DiagnosticsExtensions.BeautifyType(x, true));
			string text4 = t.GetGenericTypeDefinition().FullName;
			if (text4 == "System.Threading.Tasks.Task`1")
			{
				text4 = "Task";
			}
			return DiagnosticsExtensions.typeBeautifyRegex.Replace(text4, "").Replace("Cysharp.Threading.Tasks.Triggers.", "").Replace("Cysharp.Threading.Tasks.Internal.", "")
				.Replace("Cysharp.Threading.Tasks.", "") + "<" + text3 + ">";
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0000DD68 File Offset: 0x0000BF68
		private static bool IgnoreLine(MethodBase methodInfo)
		{
			string fullName = methodInfo.DeclaringType.FullName;
			return fullName == "System.Threading.ExecutionContext" || fullName.StartsWith("System.Runtime.CompilerServices") || fullName.StartsWith("Cysharp.Threading.Tasks.CompilerServices") || fullName == "System.Threading.Tasks.AwaitTaskContinuation" || fullName.StartsWith("System.Threading.Tasks.Task") || fullName.StartsWith("Cysharp.Threading.Tasks.UniTaskCompletionSourceCore") || fullName.StartsWith("Cysharp.Threading.Tasks.AwaiterActions");
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0000DDEC File Offset: 0x0000BFEC
		private static string AppendHyperLink(string path, string line)
		{
			FileInfo fileInfo = new FileInfo(path);
			if (fileInfo.Directory == null)
			{
				return fileInfo.Name;
			}
			string text = fileInfo.FullName.Replace(Path.DirectorySeparatorChar, '/').Replace(PlayerLoopHelper.ApplicationDataPath, "");
			string text2 = "Assets/" + text;
			return string.Concat(new string[] { "<a href=\"", text2, "\" line=\"", line, "\">", text2, ":", line, "</a>" });
		}

		// Token: 0x0400010A RID: 266
		private static bool displayFilenames = true;

		// Token: 0x0400010B RID: 267
		private static readonly Regex typeBeautifyRegex = new Regex("`.+$", RegexOptions.Compiled);

		// Token: 0x0400010C RID: 268
		private static readonly Dictionary<Type, string> builtInTypeNames = new Dictionary<Type, string>
		{
			{
				typeof(void),
				"void"
			},
			{
				typeof(bool),
				"bool"
			},
			{
				typeof(byte),
				"byte"
			},
			{
				typeof(char),
				"char"
			},
			{
				typeof(decimal),
				"decimal"
			},
			{
				typeof(double),
				"double"
			},
			{
				typeof(float),
				"float"
			},
			{
				typeof(int),
				"int"
			},
			{
				typeof(long),
				"long"
			},
			{
				typeof(object),
				"object"
			},
			{
				typeof(sbyte),
				"sbyte"
			},
			{
				typeof(short),
				"short"
			},
			{
				typeof(string),
				"string"
			},
			{
				typeof(uint),
				"uint"
			},
			{
				typeof(ulong),
				"ulong"
			},
			{
				typeof(ushort),
				"ushort"
			},
			{
				typeof(Task),
				"Task"
			},
			{
				typeof(UniTask),
				"UniTask"
			},
			{
				typeof(UniTaskVoid),
				"UniTaskVoid"
			}
		};
	}
}
