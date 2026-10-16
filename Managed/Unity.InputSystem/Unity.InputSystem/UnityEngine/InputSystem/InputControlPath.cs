using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.Collections;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000034 RID: 52
	public static class InputControlPath
	{
		// Token: 0x0600044D RID: 1101 RVA: 0x000128E3 File Offset: 0x00010AE3
		internal static string CleanSlashes(this string pathComponent)
		{
			return pathComponent.Replace('/', ' ');
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x000128F0 File Offset: 0x00010AF0
		public static string Combine(InputControl parent, string path)
		{
			if (parent == null)
			{
				if (string.IsNullOrEmpty(path))
				{
					return string.Empty;
				}
				if (path[0] != '/')
				{
					return "/" + path;
				}
				return path;
			}
			else
			{
				if (string.IsNullOrEmpty(path))
				{
					return parent.path;
				}
				return parent.path + "/" + path;
			}
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00012948 File Offset: 0x00010B48
		public static string ToHumanReadableString(string path, InputControlPath.HumanReadableStringOptions options = InputControlPath.HumanReadableStringOptions.None, InputControl control = null)
		{
			string text;
			string text2;
			return InputControlPath.ToHumanReadableString(path, out text, out text2, options, control);
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00012964 File Offset: 0x00010B64
		public static string ToHumanReadableString(string path, out string deviceLayoutName, out string controlPath, InputControlPath.HumanReadableStringOptions options = InputControlPath.HumanReadableStringOptions.None, InputControl control = null)
		{
			deviceLayoutName = null;
			controlPath = null;
			if (string.IsNullOrEmpty(path))
			{
				return string.Empty;
			}
			if (control != null)
			{
				InputControl inputControl = InputControlPath.TryFindControl(control, path, 0) ?? (InputControlPath.Matches(path, control) ? control : null);
				if (inputControl != null)
				{
					string text = (((options & InputControlPath.HumanReadableStringOptions.UseShortNames) != InputControlPath.HumanReadableStringOptions.None && !string.IsNullOrEmpty(inputControl.shortDisplayName)) ? inputControl.shortDisplayName : inputControl.displayName);
					if ((options & InputControlPath.HumanReadableStringOptions.OmitDevice) == InputControlPath.HumanReadableStringOptions.None)
					{
						text = text + " [" + inputControl.device.displayName + "]";
					}
					deviceLayoutName = inputControl.device.layout;
					if (!(inputControl is InputDevice))
					{
						controlPath = inputControl.path.Substring(inputControl.device.path.Length + 1);
					}
					return text;
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			InputControlPath.PathParser pathParser = new InputControlPath.PathParser(path);
			string text4;
			using (InputControlLayout.CacheRef())
			{
				if (pathParser.MoveToNextComponent())
				{
					string text3;
					string text2 = pathParser.current.ToHumanReadableString(null, null, out text3, out text4, options);
					deviceLayoutName = text3;
					bool flag = true;
					while (pathParser.MoveToNextComponent())
					{
						if (!flag)
						{
							stringBuilder.Append('/');
						}
						stringBuilder.Append(pathParser.current.ToHumanReadableString(text3, controlPath, out text3, out controlPath, options));
						flag = false;
					}
					if ((options & InputControlPath.HumanReadableStringOptions.OmitDevice) == InputControlPath.HumanReadableStringOptions.None && !string.IsNullOrEmpty(text2))
					{
						stringBuilder.Append(" [");
						stringBuilder.Append(text2);
						stringBuilder.Append(']');
					}
				}
				if (stringBuilder.Length == 0)
				{
					text4 = path;
				}
				else
				{
					text4 = stringBuilder.ToString();
				}
			}
			return text4;
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00012AF8 File Offset: 0x00010CF8
		public static string[] TryGetDeviceUsages(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			InputControlPath.PathParser pathParser = new InputControlPath.PathParser(path);
			if (!pathParser.MoveToNextComponent())
			{
				return null;
			}
			if (pathParser.current.m_Usages.length > 0)
			{
				return pathParser.current.m_Usages.ToArray<string>((Substring x) => x.ToString());
			}
			return null;
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00012B6C File Offset: 0x00010D6C
		public static string TryGetDeviceLayout(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			InputControlPath.PathParser pathParser = new InputControlPath.PathParser(path);
			if (!pathParser.MoveToNextComponent())
			{
				return null;
			}
			if (pathParser.current.m_Layout.length > 0)
			{
				return pathParser.current.m_Layout.ToString().Unescape("ntr\\\"", "\n\t\r\\\"");
			}
			if (pathParser.current.isWildcard)
			{
				return "*";
			}
			return null;
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00012BEC File Offset: 0x00010DEC
		public static string TryGetControlLayout(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			int length = path.Length;
			int num = path.LastIndexOf('/');
			if (num == -1 || num == 0)
			{
				return null;
			}
			if (length > num + 2 && path[num + 1] == '<' && path[length - 1] == '>')
			{
				int num2 = num + 2;
				int num3 = length - num2 - 1;
				return path.Substring(num2, num3);
			}
			InputControlPath.PathParser pathParser = new InputControlPath.PathParser(path);
			if (!pathParser.MoveToNextComponent())
			{
				return null;
			}
			if (pathParser.current.isWildcard)
			{
				throw new NotImplementedException();
			}
			if (pathParser.current.m_Layout.length == 0)
			{
				return null;
			}
			string text = pathParser.current.m_Layout.ToString();
			if (!pathParser.MoveToNextComponent())
			{
				return null;
			}
			if (pathParser.current.isWildcard)
			{
				return "*";
			}
			return InputControlPath.FindControlLayoutRecursive(ref pathParser, text.Unescape("ntr\\\"", "\n\t\r\\\""));
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00012CE4 File Offset: 0x00010EE4
		private static string FindControlLayoutRecursive(ref InputControlPath.PathParser parser, string layoutName)
		{
			string text;
			using (InputControlLayout.CacheRef())
			{
				InputControlLayout inputControlLayout = InputControlLayout.cache.FindOrLoadLayout(new InternedString(layoutName), false);
				if (inputControlLayout == null)
				{
					text = null;
				}
				else
				{
					text = InputControlPath.FindControlLayoutRecursive(ref parser, inputControlLayout);
				}
			}
			return text;
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00012D40 File Offset: 0x00010F40
		private static string FindControlLayoutRecursive(ref InputControlPath.PathParser parser, InputControlLayout layout)
		{
			string text = null;
			int count = layout.controls.Count;
			for (int i = 0; i < count; i++)
			{
				if (InputControlPath.ControlLayoutMatchesPathComponent(ref layout.m_Controls[i], ref parser))
				{
					InternedString layout2 = layout.m_Controls[i].layout;
					if (!parser.isAtEnd)
					{
						InputControlPath.PathParser pathParser = parser;
						if (pathParser.MoveToNextComponent())
						{
							string text2 = InputControlPath.FindControlLayoutRecursive(ref pathParser, layout2);
							if (text2 != null)
							{
								if (text != null && text2 != text)
								{
									return null;
								}
								text = text2;
							}
						}
					}
					else
					{
						if (text != null && layout2 != text)
						{
							return null;
						}
						text = layout2.ToString();
					}
				}
			}
			return text;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00012DF8 File Offset: 0x00010FF8
		private static bool ControlLayoutMatchesPathComponent(ref InputControlLayout.ControlItem controlItem, ref InputControlPath.PathParser parser)
		{
			Substring layout = parser.current.m_Layout;
			if (layout.length > 0 && !InputControlPath.StringMatches(layout, controlItem.layout))
			{
				return false;
			}
			if (parser.current.m_Usages.length > 0)
			{
				for (int i = 0; i < parser.current.m_Usages.length; i++)
				{
					Substring substring = parser.current.m_Usages[i];
					if (substring.length > 0)
					{
						int count = controlItem.usages.Count;
						bool flag = false;
						for (int j = 0; j < count; j++)
						{
							if (InputControlPath.StringMatches(substring, controlItem.usages[j]))
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							return false;
						}
					}
				}
			}
			Substring name = parser.current.m_Name;
			return name.length <= 0 || InputControlPath.StringMatches(name, controlItem.name);
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00012EE8 File Offset: 0x000110E8
		private static bool StringMatches(Substring str, InternedString matchTo)
		{
			int length = str.length;
			int length2 = matchTo.length;
			string text = matchTo.ToLower();
			int num = 0;
			int num2 = 0;
			while (num2 < length && num < length2)
			{
				char c = str[num2];
				if (c == '\\' && num2 + 1 < length)
				{
					c = str[++num2];
				}
				if (c == '*')
				{
					while (num2 + 1 < length && str[num2 + 1] == '*')
					{
						num2++;
					}
					if (num2 == length - 1)
					{
						return true;
					}
					num2++;
					c = char.ToLowerInvariant(str[num2]);
					while (num < length2 && text[num] != c)
					{
						num++;
					}
					if (num == length2)
					{
						return false;
					}
				}
				else if (char.ToLowerInvariant(c) != text[num])
				{
					return false;
				}
				num++;
				num2++;
			}
			return num == length2 && num2 == length;
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00012FD0 File Offset: 0x000111D0
		public static InputControl TryFindControl(InputControl control, string path, int indexInPath = 0)
		{
			return InputControlPath.TryFindControl<InputControl>(control, path, indexInPath);
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00012FDC File Offset: 0x000111DC
		public static InputControl[] TryFindControls(InputControl control, string path, int indexInPath = 0)
		{
			InputControlList<InputControl> inputControlList = new InputControlList<InputControl>(Allocator.Temp, 0);
			InputControl[] array;
			try
			{
				InputControlPath.TryFindControls<InputControl>(control, path, indexInPath, ref inputControlList);
				array = inputControlList.ToArray(false);
			}
			finally
			{
				inputControlList.Dispose();
			}
			return array;
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00013024 File Offset: 0x00011224
		public static int TryFindControls(InputControl control, string path, ref InputControlList<InputControl> matches, int indexInPath = 0)
		{
			return InputControlPath.TryFindControls<InputControl>(control, path, indexInPath, ref matches);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00013030 File Offset: 0x00011230
		public static TControl TryFindControl<TControl>(InputControl control, string path, int indexInPath = 0) where TControl : InputControl
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (string.IsNullOrEmpty(path))
			{
				return default(TControl);
			}
			if (indexInPath == 0 && path[0] == '/')
			{
				indexInPath++;
			}
			InputControlList<TControl> inputControlList = default(InputControlList<TControl>);
			return InputControlPath.MatchControlsRecursive<TControl>(control, path, indexInPath, ref inputControlList, false);
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00013084 File Offset: 0x00011284
		public static int TryFindControls<TControl>(InputControl control, string path, int indexInPath, ref InputControlList<TControl> matches) where TControl : InputControl
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (indexInPath == 0 && path[0] == '/')
			{
				indexInPath++;
			}
			int count = matches.Count;
			InputControlPath.MatchControlsRecursive<TControl>(control, path, indexInPath, ref matches, true);
			return matches.Count - count;
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x000130DA File Offset: 0x000112DA
		public static InputControl TryFindChild(InputControl control, string path, int indexInPath = 0)
		{
			return InputControlPath.TryFindChild<InputControl>(control, path, indexInPath);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x000130E4 File Offset: 0x000112E4
		public static TControl TryFindChild<TControl>(InputControl control, string path, int indexInPath = 0) where TControl : InputControl
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			ReadOnlyArray<InputControl> children = control.children;
			int count = children.Count;
			for (int i = 0; i < count; i++)
			{
				TControl tcontrol = InputControlPath.TryFindControl<TControl>(children[i], path, indexInPath);
				if (tcontrol != null)
				{
					return tcontrol;
				}
			}
			return default(TControl);
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0001314C File Offset: 0x0001134C
		public static bool Matches(string expected, InputControl control)
		{
			if (string.IsNullOrEmpty(expected))
			{
				throw new ArgumentNullException("expected");
			}
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			InputControlPath.PathParser pathParser = new InputControlPath.PathParser(expected);
			return InputControlPath.MatchesRecursive(ref pathParser, control, false);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0001318C File Offset: 0x0001138C
		internal static bool MatchControlComponent(ref InputControlPath.ParsedPathComponent expectedControlComponent, ref InputControlLayout.ControlItem controlItem, bool matchAlias = false)
		{
			bool flag = false;
			bool flag2 = false;
			if (!expectedControlComponent.m_Name.isEmpty)
			{
				if (InputControlPath.StringMatches(expectedControlComponent.m_Name, controlItem.name))
				{
					flag = true;
				}
				else
				{
					if (!matchAlias)
					{
						return false;
					}
					ReadOnlyArray<InternedString> aliases = controlItem.aliases;
					for (int i = 0; i < aliases.Count; i++)
					{
						if (InputControlPath.StringMatches(expectedControlComponent.m_Name, aliases[i]))
						{
							flag = true;
							break;
						}
					}
				}
			}
			foreach (Substring substring in expectedControlComponent.m_Usages)
			{
				if (!substring.isEmpty)
				{
					int count = controlItem.usages.Count;
					for (int j = 0; j < count; j++)
					{
						if (InputControlPath.StringMatches(substring, controlItem.usages[j]))
						{
							flag2 = true;
							break;
						}
					}
				}
			}
			return flag || flag2;
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00013288 File Offset: 0x00011488
		public static bool MatchesPrefix(string expected, InputControl control)
		{
			if (string.IsNullOrEmpty(expected))
			{
				throw new ArgumentNullException("expected");
			}
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			InputControlPath.PathParser pathParser = new InputControlPath.PathParser(expected);
			return InputControlPath.MatchesRecursive(ref pathParser, control, true) && pathParser.isAtEnd;
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x000132D8 File Offset: 0x000114D8
		private static bool MatchesRecursive(ref InputControlPath.PathParser parser, InputControl currentControl, bool prefixOnly = false)
		{
			InputControl parent = currentControl.parent;
			if (parent != null && !InputControlPath.MatchesRecursive(ref parser, parent, prefixOnly))
			{
				return false;
			}
			if (!parser.MoveToNextComponent())
			{
				return prefixOnly;
			}
			return parser.current.Matches(currentControl);
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00013314 File Offset: 0x00011514
		private static TControl MatchControlsRecursive<TControl>(InputControl control, string path, int indexInPath, ref InputControlList<TControl> matches, bool matchMultiple) where TControl : InputControl
		{
			int length = path.Length;
			bool flag = true;
			if (path[indexInPath] == '<')
			{
				indexInPath++;
				flag = InputControlPath.MatchPathComponent(control.layout, path, ref indexInPath, InputControlPath.PathComponentType.Layout, 0);
				if (!flag)
				{
					InternedString layout = control.m_Layout;
					while (InputControlLayout.s_Layouts.baseLayoutTable.TryGetValue(layout, out layout))
					{
						flag = InputControlPath.MatchPathComponent(layout, path, ref indexInPath, InputControlPath.PathComponentType.Layout, 0);
						if (flag)
						{
							break;
						}
					}
				}
			}
			while (indexInPath < length && path[indexInPath] == '{' && flag)
			{
				indexInPath++;
				for (int i = 0; i < control.usages.Count; i++)
				{
					flag = InputControlPath.MatchPathComponent(control.usages[i], path, ref indexInPath, InputControlPath.PathComponentType.Usage, 0);
					if (flag)
					{
						break;
					}
				}
			}
			if (indexInPath < length - 1 && flag && path[indexInPath] == '#' && path[indexInPath + 1] == '(')
			{
				indexInPath += 2;
				flag = InputControlPath.MatchPathComponent(control.displayName, path, ref indexInPath, InputControlPath.PathComponentType.DisplayName, 0);
			}
			if (indexInPath < length && flag && path[indexInPath] != '/')
			{
				flag = InputControlPath.MatchPathComponent(control.name, path, ref indexInPath, InputControlPath.PathComponentType.Name, 0);
				if (!flag)
				{
					int num = 0;
					while (num < control.aliases.Count && !flag)
					{
						flag = InputControlPath.MatchPathComponent(control.aliases[num], path, ref indexInPath, InputControlPath.PathComponentType.Name, 0);
						num++;
					}
				}
			}
			if (flag)
			{
				if (indexInPath < length && path[indexInPath] == '*')
				{
					indexInPath++;
				}
				if (indexInPath == length)
				{
					TControl tcontrol = control as TControl;
					if (tcontrol == null)
					{
						return default(TControl);
					}
					if (matchMultiple)
					{
						matches.Add(tcontrol);
					}
					return tcontrol;
				}
				else if (path[indexInPath] == '/')
				{
					indexInPath++;
					if (indexInPath != length)
					{
						TControl tcontrol2;
						if (path[indexInPath] == '{')
						{
							tcontrol2 = InputControlPath.MatchByUsageAtDeviceRootRecursive<TControl>(control.device, path, indexInPath, ref matches, matchMultiple);
						}
						else
						{
							tcontrol2 = InputControlPath.MatchChildrenRecursive<TControl>(control, path, indexInPath, ref matches, matchMultiple);
						}
						return tcontrol2;
					}
					TControl tcontrol3 = control as TControl;
					if (tcontrol3 == null)
					{
						return default(TControl);
					}
					if (matchMultiple)
					{
						matches.Add(tcontrol3);
					}
					return tcontrol3;
				}
			}
			return default(TControl);
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00013548 File Offset: 0x00011748
		private static TControl MatchByUsageAtDeviceRootRecursive<TControl>(InputDevice device, string path, int indexInPath, ref InputControlList<TControl> matches, bool matchMultiple) where TControl : InputControl
		{
			InternedString[] usagesForEachControl = device.m_UsagesForEachControl;
			if (usagesForEachControl == null)
			{
				return default(TControl);
			}
			int num = device.m_UsageToControl.LengthSafe<InputControl>();
			int num2 = indexInPath + 1;
			bool flag = InputControlPath.PathComponentCanYieldMultipleMatches(path, indexInPath);
			int length = path.Length;
			indexInPath++;
			if (indexInPath == length)
			{
				throw new ArgumentException("Invalid path spec '" + path + "'; trailing '{'", "path");
			}
			TControl tcontrol = default(TControl);
			for (int i = 0; i < num; i++)
			{
				if (!InputControlPath.MatchPathComponent(usagesForEachControl[i], path, ref indexInPath, InputControlPath.PathComponentType.Usage, 0))
				{
					indexInPath = num2;
				}
				else
				{
					InputControl inputControl = device.m_UsageToControl[i];
					if (indexInPath < length && path[indexInPath] == '/')
					{
						tcontrol = InputControlPath.MatchChildrenRecursive<TControl>(inputControl, path, indexInPath + 1, ref matches, matchMultiple);
						if (tcontrol != null && !flag)
						{
							break;
						}
						if (tcontrol != null && !matchMultiple)
						{
							break;
						}
					}
					else
					{
						tcontrol = inputControl as TControl;
						if (tcontrol != null)
						{
							if (!matchMultiple)
							{
								break;
							}
							matches.Add(tcontrol);
						}
					}
				}
			}
			return tcontrol;
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00013658 File Offset: 0x00011858
		private static TControl MatchChildrenRecursive<TControl>(InputControl control, string path, int indexInPath, ref InputControlList<TControl> matches, bool matchMultiple) where TControl : InputControl
		{
			ReadOnlyArray<InputControl> children = control.children;
			int count = children.Count;
			TControl tcontrol = default(TControl);
			bool flag = InputControlPath.PathComponentCanYieldMultipleMatches(path, indexInPath);
			for (int i = 0; i < count; i++)
			{
				TControl tcontrol2 = InputControlPath.MatchControlsRecursive<TControl>(children[i], path, indexInPath, ref matches, matchMultiple);
				if (tcontrol2 != null)
				{
					if (!flag)
					{
						return tcontrol2;
					}
					if (!matchMultiple)
					{
						return tcontrol2;
					}
					tcontrol = tcontrol2;
				}
			}
			return tcontrol;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x000136C4 File Offset: 0x000118C4
		internal static bool MatchPathComponent(string component, string path, ref int indexInPath, InputControlPath.PathComponentType componentType, int startIndexInComponent = 0)
		{
			int length = component.Length;
			int length2 = path.Length;
			int num = indexInPath;
			int num2 = startIndexInComponent;
			while (indexInPath < length2)
			{
				char c = path[indexInPath];
				if (c == '\\' && indexInPath + 1 < length2)
				{
					indexInPath++;
					c = path[indexInPath];
				}
				else
				{
					if (c == '/' && componentType == InputControlPath.PathComponentType.Name)
					{
						break;
					}
					if ((c == '>' && componentType == InputControlPath.PathComponentType.Layout) || (c == '}' && componentType == InputControlPath.PathComponentType.Usage) || (c == ')' && componentType == InputControlPath.PathComponentType.DisplayName))
					{
						indexInPath++;
						break;
					}
					if (c == '*')
					{
						while (indexInPath + 1 < length2 && path[indexInPath + 1] == '*')
						{
							indexInPath++;
						}
						int num3 = indexInPath + 1;
						if (indexInPath < length2 - 1 && num2 < length && InputControlPath.MatchPathComponent(component, path, ref num3, componentType, num2))
						{
							indexInPath = num3;
							return true;
						}
						if (num2 < length)
						{
							num2++;
							continue;
						}
						return true;
					}
				}
				if (num2 == length)
				{
					indexInPath = num;
					return false;
				}
				char c2 = component[num2];
				if (c2 != c && char.ToLowerInvariant(c2) != char.ToLowerInvariant(c))
				{
					indexInPath = num;
					return false;
				}
				num2++;
				indexInPath++;
			}
			if (num2 == length)
			{
				return true;
			}
			indexInPath = num;
			return false;
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x000137EC File Offset: 0x000119EC
		private static bool PathComponentCanYieldMultipleMatches(string path, int indexInPath)
		{
			int num = path.IndexOf('/', indexInPath);
			if (num == -1)
			{
				return path.IndexOf('*', indexInPath) != -1 || path.IndexOf('<', indexInPath) != -1;
			}
			int num2 = num - indexInPath;
			return path.IndexOf('*', indexInPath, num2) != -1 || path.IndexOf('<', indexInPath, num2) != -1;
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00013848 File Offset: 0x00011A48
		public static IEnumerable<InputControlPath.ParsedPathComponent> Parse(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException("path");
			}
			InputControlPath.PathParser parser = new InputControlPath.PathParser(path);
			while (parser.MoveToNextComponent())
			{
				yield return parser.current;
			}
			yield break;
		}

		// Token: 0x04000149 RID: 329
		public const string Wildcard = "*";

		// Token: 0x0400014A RID: 330
		public const string DoubleWildcard = "**";

		// Token: 0x0400014B RID: 331
		public const char Separator = '/';

		// Token: 0x0400014C RID: 332
		internal const char SeparatorReplacement = ' ';

		// Token: 0x02000195 RID: 405
		[Flags]
		public enum HumanReadableStringOptions
		{
			// Token: 0x040008E1 RID: 2273
			None = 0,
			// Token: 0x040008E2 RID: 2274
			OmitDevice = 2,
			// Token: 0x040008E3 RID: 2275
			UseShortNames = 4
		}

		// Token: 0x02000196 RID: 406
		internal enum PathComponentType
		{
			// Token: 0x040008E5 RID: 2277
			Name,
			// Token: 0x040008E6 RID: 2278
			DisplayName,
			// Token: 0x040008E7 RID: 2279
			Usage,
			// Token: 0x040008E8 RID: 2280
			Layout
		}

		// Token: 0x02000197 RID: 407
		public struct ParsedPathComponent
		{
			// Token: 0x17000574 RID: 1396
			// (get) Token: 0x06001426 RID: 5158 RVA: 0x0005D8BC File Offset: 0x0005BABC
			public string layout
			{
				get
				{
					return this.m_Layout.ToString();
				}
			}

			// Token: 0x17000575 RID: 1397
			// (get) Token: 0x06001427 RID: 5159 RVA: 0x0005D8CF File Offset: 0x0005BACF
			public IEnumerable<string> usages
			{
				get
				{
					return this.m_Usages.Select<Substring, string>((Substring x) => x.ToString());
				}
			}

			// Token: 0x17000576 RID: 1398
			// (get) Token: 0x06001428 RID: 5160 RVA: 0x0005D900 File Offset: 0x0005BB00
			public string name
			{
				get
				{
					return this.m_Name.ToString();
				}
			}

			// Token: 0x17000577 RID: 1399
			// (get) Token: 0x06001429 RID: 5161 RVA: 0x0005D913 File Offset: 0x0005BB13
			public string displayName
			{
				get
				{
					return this.m_DisplayName.ToString();
				}
			}

			// Token: 0x17000578 RID: 1400
			// (get) Token: 0x0600142A RID: 5162 RVA: 0x0005D926 File Offset: 0x0005BB26
			internal bool isWildcard
			{
				get
				{
					return this.m_Name == "*";
				}
			}

			// Token: 0x17000579 RID: 1401
			// (get) Token: 0x0600142B RID: 5163 RVA: 0x0005D93D File Offset: 0x0005BB3D
			internal bool isDoubleWildcard
			{
				get
				{
					return this.m_Name == "**";
				}
			}

			// Token: 0x0600142C RID: 5164 RVA: 0x0005D954 File Offset: 0x0005BB54
			internal string ToHumanReadableString(string parentLayoutName, string parentControlPath, out string referencedLayoutName, out string controlPath, InputControlPath.HumanReadableStringOptions options)
			{
				referencedLayoutName = null;
				controlPath = null;
				string text = string.Empty;
				if (this.isWildcard)
				{
					text += "Any";
				}
				if (this.m_Usages.length > 0)
				{
					string text2 = string.Empty;
					for (int i = 0; i < this.m_Usages.length; i++)
					{
						if (!this.m_Usages[i].isEmpty)
						{
							if (text2 != string.Empty)
							{
								text2 = text2 + " & " + InputControlPath.ParsedPathComponent.ToHumanReadableString(this.m_Usages[i]);
							}
							else
							{
								text2 = InputControlPath.ParsedPathComponent.ToHumanReadableString(this.m_Usages[i]);
							}
						}
					}
					if (text2 != string.Empty)
					{
						if (text != string.Empty)
						{
							text = text + " " + text2;
						}
						else
						{
							text += text2;
						}
					}
				}
				if (!this.m_Layout.isEmpty)
				{
					referencedLayoutName = this.m_Layout.ToString();
					InputControlLayout inputControlLayout = InputControlLayout.cache.FindOrLoadLayout(referencedLayoutName, false);
					string text3;
					if (inputControlLayout != null && !string.IsNullOrEmpty(inputControlLayout.m_DisplayName))
					{
						text3 = inputControlLayout.m_DisplayName;
					}
					else
					{
						text3 = InputControlPath.ParsedPathComponent.ToHumanReadableString(this.m_Layout);
					}
					if (!string.IsNullOrEmpty(text))
					{
						text = text + " " + text3;
					}
					else
					{
						text += text3;
					}
				}
				if (!this.m_Name.isEmpty && !this.isWildcard)
				{
					string text4 = null;
					if (!string.IsNullOrEmpty(parentLayoutName))
					{
						InputControlLayout inputControlLayout2 = InputControlLayout.cache.FindOrLoadLayout(new InternedString(parentLayoutName), false);
						if (inputControlLayout2 != null)
						{
							InternedString internedString = new InternedString(this.m_Name.ToString());
							int num;
							InputControlLayout.ControlItem? controlItem = inputControlLayout2.FindControlIncludingArrayElements(internedString, out num);
							if (controlItem != null)
							{
								if (string.IsNullOrEmpty(parentControlPath))
								{
									if (num != -1)
									{
										controlPath = string.Format("{0}{1}", controlItem.Value.name, num);
									}
									else
									{
										controlPath = controlItem.Value.name;
									}
								}
								else if (num != -1)
								{
									controlPath = string.Format("{0}/{1}{2}", parentControlPath, controlItem.Value.name, num);
								}
								else
								{
									controlPath = string.Format("{0}/{1}", parentControlPath, controlItem.Value.name);
								}
								string text5 = (((options & InputControlPath.HumanReadableStringOptions.UseShortNames) != InputControlPath.HumanReadableStringOptions.None) ? controlItem.Value.shortDisplayName : null);
								string text6 = ((!string.IsNullOrEmpty(text5)) ? text5 : controlItem.Value.displayName);
								if (!string.IsNullOrEmpty(text6))
								{
									if (num != -1)
									{
										text4 = string.Format("{0} #{1}", text6, num);
									}
									else
									{
										text4 = text6;
									}
								}
								if (string.IsNullOrEmpty(referencedLayoutName))
								{
									referencedLayoutName = controlItem.Value.layout;
								}
							}
						}
					}
					if (text4 == null)
					{
						text4 = InputControlPath.ParsedPathComponent.ToHumanReadableString(this.m_Name);
					}
					if (!string.IsNullOrEmpty(text))
					{
						text = text + " " + text4;
					}
					else
					{
						text += text4;
					}
				}
				if (!this.m_DisplayName.isEmpty)
				{
					string text7 = "\"" + InputControlPath.ParsedPathComponent.ToHumanReadableString(this.m_DisplayName) + "\"";
					if (!string.IsNullOrEmpty(text))
					{
						text = text + " " + text7;
					}
					else
					{
						text += text7;
					}
				}
				return text;
			}

			// Token: 0x0600142D RID: 5165 RVA: 0x0005DCD2 File Offset: 0x0005BED2
			private static string ToHumanReadableString(Substring substring)
			{
				return substring.ToString().Unescape("/*{<", "/*{<");
			}

			// Token: 0x0600142E RID: 5166 RVA: 0x0005DCF0 File Offset: 0x0005BEF0
			public bool Matches(InputControl control)
			{
				if (!this.m_Layout.isEmpty)
				{
					bool flag = InputControlPath.ParsedPathComponent.ComparePathElementToString(this.m_Layout, control.layout);
					if (!flag)
					{
						InternedString layout = control.m_Layout;
						while (InputControlLayout.s_Layouts.baseLayoutTable.TryGetValue(layout, out layout) && !flag)
						{
							flag = InputControlPath.ParsedPathComponent.ComparePathElementToString(this.m_Layout, layout.ToString());
						}
					}
					if (!flag)
					{
						return false;
					}
				}
				if (this.m_Usages.length > 0)
				{
					for (int i = 0; i < this.m_Usages.length; i++)
					{
						if (!this.m_Usages[i].isEmpty)
						{
							ReadOnlyArray<InternedString> usages = control.usages;
							bool flag2 = false;
							for (int j = 0; j < usages.Count; j++)
							{
								if (InputControlPath.ParsedPathComponent.ComparePathElementToString(this.m_Usages[i], usages[j]))
								{
									flag2 = true;
									break;
								}
							}
							if (!flag2)
							{
								return false;
							}
						}
					}
				}
				return (this.m_Name.isEmpty || this.isWildcard || InputControlPath.ParsedPathComponent.ComparePathElementToString(this.m_Name, control.name)) && (this.m_DisplayName.isEmpty || InputControlPath.ParsedPathComponent.ComparePathElementToString(this.m_DisplayName, control.displayName));
			}

			// Token: 0x0600142F RID: 5167 RVA: 0x0005DE34 File Offset: 0x0005C034
			private static bool ComparePathElementToString(Substring pathElement, string element)
			{
				int length = pathElement.length;
				int length2 = element.Length;
				int num = 0;
				int num2 = 0;
				bool flag;
				bool flag2;
				for (;;)
				{
					flag = num == length;
					flag2 = num2 == length2;
					if (flag || flag2)
					{
						break;
					}
					char c = pathElement[num];
					if (c == '\\' && num + 1 < length)
					{
						c = pathElement[++num];
					}
					if (char.ToLowerInvariant(c) != char.ToLowerInvariant(element[num2]))
					{
						return false;
					}
					num++;
					num2++;
				}
				return flag == flag2;
			}

			// Token: 0x040008E9 RID: 2281
			internal Substring m_Layout;

			// Token: 0x040008EA RID: 2282
			internal InlinedArray<Substring> m_Usages;

			// Token: 0x040008EB RID: 2283
			internal Substring m_Name;

			// Token: 0x040008EC RID: 2284
			internal Substring m_DisplayName;
		}

		// Token: 0x02000198 RID: 408
		private struct PathParser
		{
			// Token: 0x1700057A RID: 1402
			// (get) Token: 0x06001430 RID: 5168 RVA: 0x0005DEB2 File Offset: 0x0005C0B2
			public bool isAtEnd
			{
				get
				{
					return this.rightIndexInPath == this.length;
				}
			}

			// Token: 0x06001431 RID: 5169 RVA: 0x0005DEC2 File Offset: 0x0005C0C2
			public PathParser(string path)
			{
				this.path = path;
				this.length = path.Length;
				this.leftIndexInPath = 0;
				this.rightIndexInPath = 0;
				this.current = default(InputControlPath.ParsedPathComponent);
			}

			// Token: 0x06001432 RID: 5170 RVA: 0x0005DEF4 File Offset: 0x0005C0F4
			public bool MoveToNextComponent()
			{
				if (this.rightIndexInPath == this.length)
				{
					return false;
				}
				this.leftIndexInPath = this.rightIndexInPath;
				if (this.path[this.leftIndexInPath] == '/')
				{
					this.leftIndexInPath++;
					this.rightIndexInPath = this.leftIndexInPath;
					if (this.leftIndexInPath == this.length)
					{
						return false;
					}
				}
				Substring substring = default(Substring);
				if (this.rightIndexInPath < this.length && this.path[this.rightIndexInPath] == '<')
				{
					substring = this.ParseComponentPart('>');
				}
				InlinedArray<Substring> inlinedArray = default(InlinedArray<Substring>);
				while (this.rightIndexInPath < this.length && this.path[this.rightIndexInPath] == '{')
				{
					inlinedArray.AppendWithCapacity(this.ParseComponentPart('}'), 10);
				}
				Substring substring2 = default(Substring);
				if (this.rightIndexInPath < this.length - 1 && this.path[this.rightIndexInPath] == '#' && this.path[this.rightIndexInPath + 1] == '(')
				{
					this.rightIndexInPath++;
					substring2 = this.ParseComponentPart(')');
				}
				Substring substring3 = default(Substring);
				if (this.rightIndexInPath < this.length && this.path[this.rightIndexInPath] != '/')
				{
					substring3 = this.ParseComponentPart('/');
				}
				this.current = new InputControlPath.ParsedPathComponent
				{
					m_Layout = substring,
					m_Usages = inlinedArray,
					m_Name = substring3,
					m_DisplayName = substring2
				};
				return this.leftIndexInPath != this.rightIndexInPath;
			}

			// Token: 0x06001433 RID: 5171 RVA: 0x0005E0A0 File Offset: 0x0005C2A0
			private Substring ParseComponentPart(char terminator)
			{
				if (terminator != '/')
				{
					this.rightIndexInPath++;
				}
				int num = this.rightIndexInPath;
				while (this.rightIndexInPath < this.length && this.path[this.rightIndexInPath] != terminator)
				{
					if (this.path[this.rightIndexInPath] == '\\' && this.rightIndexInPath + 1 < this.length)
					{
						this.rightIndexInPath++;
					}
					this.rightIndexInPath++;
				}
				int num2 = this.rightIndexInPath - num;
				if (this.rightIndexInPath < this.length && terminator != '/')
				{
					this.rightIndexInPath++;
				}
				return new Substring(this.path, num, num2);
			}

			// Token: 0x040008ED RID: 2285
			private string path;

			// Token: 0x040008EE RID: 2286
			private int length;

			// Token: 0x040008EF RID: 2287
			private int leftIndexInPath;

			// Token: 0x040008F0 RID: 2288
			private int rightIndexInPath;

			// Token: 0x040008F1 RID: 2289
			public InputControlPath.ParsedPathComponent current;
		}
	}
}
