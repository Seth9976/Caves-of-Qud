using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using XRL;
using XRL.UI;

namespace Qud.UI
{
	// Token: 0x0200048E RID: 1166
	[HasDebugCommand]
	[UIView("DebugConsole", false, false, false, "Menu", "Console", false, 0, false, UICanvasHost = 1)]
	public static class DebugConsole
	{
		// Token: 0x06002CBF RID: 11455 RVA: 0x000F1CC4 File Offset: 0x000EFEC4
		public static void claimCommandInstance(Type type, object claimant)
		{
			if (DebugConsole.commandObjects.ContainsKey(type))
			{
				DebugConsole.commandObjects[type] = claimant;
				return;
			}
			DebugConsole.commandObjects.Add(type, claimant);
		}

		// Token: 0x06002CC0 RID: 11456 RVA: 0x000F1CEC File Offset: 0x000EFEEC
		public static void Initialize()
		{
			if (!DebugConsole.commands.IsNullOrEmpty<KeyValuePair<string, DebugConsole.Command>>())
			{
				return;
			}
			DebugConsole.WriteLine("Initializing debug console commands...");
			foreach (MethodInfo methodInfo in ModManager.GetMethodsWithAttribute(typeof(DebugCommand), typeof(HasDebugCommand), true))
			{
				DebugConsole.Command command = new DebugConsole.Command();
				command.name = methodInfo.Name.ToLower();
				command.method = methodInfo;
				if (DebugConsole.commands.ContainsKey(command.name))
				{
					MetricsManager.LogWarning("Multiple debug commands defined for: " + command.name);
				}
				else
				{
					DebugConsole.commands.Add(command.name, command);
				}
			}
		}

		// Token: 0x06002CC1 RID: 11457 RVA: 0x000F1DBC File Offset: 0x000EFFBC
		public static void Show()
		{
			DebugConsole.Initialize();
			SingletonWindowBase<ConsoleWindow>.instance.Show();
		}

		// Token: 0x06002CC2 RID: 11458 RVA: 0x000F1DCD File Offset: 0x000EFFCD
		[DebugCommand]
		public static void Hide()
		{
			SingletonWindowBase<ConsoleWindow>.instance.Hide();
		}

		// Token: 0x06002CC3 RID: 11459 RVA: 0x000F1DD9 File Offset: 0x000EFFD9
		[DebugCommand]
		public static void Clear()
		{
			DebugConsole.dirty = true;
		}

		// Token: 0x06002CC4 RID: 11460 RVA: 0x000F1DE1 File Offset: 0x000EFFE1
		[DebugCommand]
		public static void Write(string value)
		{
			DebugConsole.dirty = true;
		}

		// Token: 0x06002CC5 RID: 11461 RVA: 0x000F1DE9 File Offset: 0x000EFFE9
		[DebugCommand]
		public static void WriteLine(string value = "")
		{
			DebugConsole.Write(value);
			DebugConsole.Write("\n");
			DebugConsole.dirty = true;
		}

		// Token: 0x06002CC6 RID: 11462 RVA: 0x000F1E04 File Offset: 0x000F0004
		[DebugCommand]
		public static void Help()
		{
			DebugConsole.Initialize();
			foreach (KeyValuePair<string, DebugConsole.Command> keyValuePair in DebugConsole.commands)
			{
				DebugConsole.WriteLine(keyValuePair.Key);
			}
		}

		// Token: 0x06002CC7 RID: 11463 RVA: 0x000F1E60 File Offset: 0x000F0060
		public static List<string> ParseArguments(string command)
		{
			List<string> list = (from Match m in Regex.Matches(command, "[\\\"].+?[\\\"]|[^ ]+")
				select m.Value).ToList<string>();
			if (list.Count > 0)
			{
				list.RemoveAt(0);
			}
			return list;
		}

		// Token: 0x06002CC8 RID: 11464 RVA: 0x000F1EB8 File Offset: 0x000F00B8
		public static void Execute(string command)
		{
			DebugConsole.Initialize();
			DebugConsole.WriteLine("");
			DebugConsole.WriteLine("> " + command);
			DebugConsole.WriteLine("");
			List<string> list = DebugConsole.ParseArguments(command);
			command = command.Split(' ', StringSplitOptions.None)[0].ToLower();
			if (DebugConsole.commands.ContainsKey(command))
			{
				DebugConsole.commands[command.ToLower()].Invoke(list);
				return;
			}
			DebugConsole.Help();
			DebugConsole.WriteLine("unknown command: " + command);
		}

		// Token: 0x06002CC9 RID: 11465 RVA: 0x000F1F40 File Offset: 0x000F0140
		public static void ScrollToBottom()
		{
			SingletonWindowBase<ConsoleWindow>.instance.ScrollToBottom();
		}

		// Token: 0x040018FA RID: 6394
		public static bool dirty = false;

		// Token: 0x040018FB RID: 6395
		public static Dictionary<string, DebugConsole.Command> commands = new Dictionary<string, DebugConsole.Command>();

		// Token: 0x040018FC RID: 6396
		public static Dictionary<Type, object> commandObjects = new Dictionary<Type, object>();

		// Token: 0x020018EE RID: 6382
		[HasDebugCommand]
		public class CallCommands
		{
			// Token: 0x0600E1D9 RID: 57817 RVA: 0x005204D8 File Offset: 0x0051E6D8
			[DebugCommand]
			public void select(string[] arguments)
			{
				if (arguments.Length == 0)
				{
					this.selectedType = null;
				}
				this.selectedType = ModManager.ResolveType(arguments[0], false, false, true);
				if (this.selectedType == null)
				{
					DebugConsole.WriteLine("unknown type, clearing selection");
					return;
				}
				DebugConsole.WriteLine("selected " + this.selectedType.Name);
			}

			// Token: 0x0600E1DA RID: 57818 RVA: 0x00520534 File Offset: 0x0051E734
			[DebugCommand]
			public void list(string[] arguments)
			{
				if (this.selectedType == null)
				{
					DebugConsole.WriteLine("nothing is selected");
				}
				DebugConsole.WriteLine("[methods]");
				foreach (MethodInfo methodInfo in this.selectedType.GetMethods())
				{
					if (methodInfo.IsStatic)
					{
						DebugConsole.WriteLine("static " + methodInfo.Name);
					}
				}
			}

			// Token: 0x0600E1DB RID: 57819 RVA: 0x005205A0 File Offset: 0x0051E7A0
			public static object ParseMethodParameter(string input, ParameterInfo param)
			{
				if (param.ParameterType == typeof(string))
				{
					return input;
				}
				if (param.ParameterType == typeof(int))
				{
					return int.Parse(input);
				}
				if (param.ParameterType == typeof(float))
				{
					return float.Parse(input);
				}
				if (param.ParameterType == typeof(double))
				{
					return double.Parse(input);
				}
				return input;
			}

			// Token: 0x0600E1DC RID: 57820 RVA: 0x00520630 File Offset: 0x0051E830
			[DebugCommand]
			public void call(params string[] arguments)
			{
				if (this.selectedType == null)
				{
					DebugConsole.WriteLine("nothing is selected");
					return;
				}
				MethodInfo method = this.selectedType.GetMethod(arguments[0]);
				List<object> list = new List<object>();
				List<ParameterInfo> list2 = method.GetParameters().ToList<ParameterInfo>();
				string text = "";
				for (int i = 0; i < list2.Count; i++)
				{
					if (i > 0)
					{
						text += ", ";
					}
					list.Add(DebugConsole.CallCommands.ParseMethodParameter(arguments[i + 1], list2[i]));
					text += arguments[i + 1];
				}
				if (method.IsStatic)
				{
					DebugConsole.WriteLine(string.Concat(new string[]
					{
						"calling static method ",
						this.selectedType.Name,
						"::",
						method.Name,
						"(",
						text,
						")"
					}));
					method.Invoke(null, list.ToArray());
				}
			}

			// Token: 0x04006427 RID: 25639
			private Type selectedType;
		}

		// Token: 0x020018EF RID: 6383
		public class Command
		{
			// Token: 0x0600E1DE RID: 57822 RVA: 0x00520734 File Offset: 0x0051E934
			public void Invoke(List<string> arguments)
			{
				object obj = null;
				if (!this.method.IsStatic)
				{
					if (!DebugConsole.commandObjects.ContainsKey(this.method.DeclaringType))
					{
						DebugConsole.commandObjects.Add(this.method.DeclaringType, Activator.CreateInstance(this.method.DeclaringType));
					}
					obj = DebugConsole.commandObjects[this.method.DeclaringType];
				}
				if (this.method.GetParameters().Length == 1 && this.method.GetParameters()[0].ParameterType == typeof(string[]))
				{
					this.method.Invoke(obj, new object[] { arguments.ToArray() });
					return;
				}
				if (this.method.GetParameters().Length == 1 && this.method.GetParameters()[0].ParameterType == typeof(List<string>))
				{
					this.method.Invoke(obj, new object[] { arguments });
					return;
				}
				MethodBase methodBase = this.method;
				object obj2 = obj;
				object[] array = arguments.ToArray();
				methodBase.Invoke(obj2, array);
			}

			// Token: 0x04006428 RID: 25640
			public string name;

			// Token: 0x04006429 RID: 25641
			public MethodInfo method;
		}
	}
}
