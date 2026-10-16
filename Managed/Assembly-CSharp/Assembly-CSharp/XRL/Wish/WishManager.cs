using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Qud.API;
using UnityEngine;
using XRL.UI;
using XRL.World;
using XRL.World.Capabilities;

namespace XRL.Wish
{
	// Token: 0x0200056A RID: 1386
	[HasModSensitiveStaticCache]
	[HasWishCommand]
	public static class WishManager
	{
		// Token: 0x06003C3A RID: 15418 RVA: 0x001266E4 File Offset: 0x001248E4
		public static void UpdateCommandCollection()
		{
			WishManager.CommandCollection = new List<WishManager.Command>();
			foreach (MethodInfo methodInfo in ModManager.GetMethodsWithAttribute(typeof(WishCommand), typeof(HasWishCommand), true))
			{
				WishCommand[] array = methodInfo.GetCustomAttributes(typeof(WishCommand), false) as WishCommand[];
				int i = 0;
				while (i < array.Length)
				{
					WishCommand wishCommand = array[i];
					if (wishCommand.Command == null)
					{
						wishCommand.Command = methodInfo.Name.ToLower();
					}
					ParameterInfo[] parameters = methodInfo.GetParameters();
					Regex regex;
					if (wishCommand.Regex != null)
					{
						if (parameters.Count<ParameterInfo>() == 0 || (parameters.Count<ParameterInfo>() == 1 && parameters[0].ParameterType.IsAssignableFrom(typeof(Match))))
						{
							regex = new Regex(wishCommand.Regex, RegexOptions.IgnoreCase | RegexOptions.Compiled);
							goto IL_01DC;
						}
						MetricsManager.LogError(string.Concat(new string[]
						{
							"WishCommand ",
							methodInfo.DeclaringType.Name,
							"::",
							methodInfo.Name,
							" defined its own regex but method does not take (Match) or () parameters."
						}));
					}
					else
					{
						if (parameters.Count<ParameterInfo>() == 0)
						{
							regex = new Regex("^" + wishCommand.Command + "$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
							goto IL_01DC;
						}
						if (parameters.Count<ParameterInfo>() == 1 && (parameters[0].ParameterType == typeof(string) || parameters[0].ParameterType == typeof(Match)))
						{
							regex = new Regex("^" + wishCommand.Command + "(?::|\\s+)(.*)$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
							goto IL_01DC;
						}
						MetricsManager.LogError(string.Concat(new string[]
						{
							"WishCommand ",
							methodInfo.DeclaringType.Name,
							"::",
							methodInfo.Name,
							" must take either no arguments, or a single string, or a Match"
						}));
					}
					IL_0217:
					i++;
					continue;
					IL_01DC:
					WishManager.CommandCollection.Add(new WishManager.Command
					{
						wish = wishCommand,
						type = methodInfo.DeclaringType,
						method = methodInfo,
						regex = regex
					});
					goto IL_0217;
				}
			}
		}

		// Token: 0x06003C3B RID: 15419 RVA: 0x00126950 File Offset: 0x00124B50
		public static object GetInstanceForType(Type type)
		{
			if (WishManager.CommandObjects == null)
			{
				WishManager.CommandObjects = new Dictionary<Type, object>();
			}
			object obj;
			if (!WishManager.CommandObjects.TryGetValue(type, out obj))
			{
				obj = Activator.CreateInstance(type);
				WishManager.CommandObjects.Add(type, obj);
			}
			return obj;
		}

		// Token: 0x06003C3C RID: 15420 RVA: 0x00126991 File Offset: 0x00124B91
		public static void ClaimCommandInstance(Type type, object instance)
		{
			WishManager.CommandObjects[type] = instance;
		}

		// Token: 0x06003C3D RID: 15421 RVA: 0x001269A0 File Offset: 0x00124BA0
		[WishCommand(null, null, Command = "wish")]
		public static void AskWish()
		{
			string text = Popup.AskString("Your wish is my command!", "", null, null, null, 999, 0, false, true, new bool?(false));
			if (!text.IsNullOrEmpty())
			{
				try
				{
					MetricsManager.LogEvent("Wish:" + text);
					Wishing.HandleWish(The.Player, text);
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
				}
			}
		}

		// Token: 0x06003C3E RID: 15422 RVA: 0x00126A0C File Offset: 0x00124C0C
		[WishCommand(null, null)]
		public static void Rewish()
		{
			Loading.LoadTask("Loading wish commands", new Action(WishManager.UpdateCommandCollection), true);
		}

		// Token: 0x06003C3F RID: 15423 RVA: 0x00126A28 File Offset: 0x00124C28
		[WishCommand(null, null, Command = "wish")]
		public static bool HandleWish(string wish)
		{
			if (WishManager.CommandCollection == null)
			{
				WishManager.Rewish();
			}
			foreach (WishManager.Command command in WishManager.CommandCollection)
			{
				ParameterInfo[] parameters = command.method.GetParameters();
				Match match = command.regex.Match(wish);
				if (match.Success)
				{
					List<object> list = new List<object>();
					if (parameters.Count<ParameterInfo>() > 0)
					{
						if (parameters[0].ParameterType.IsAssignableFrom(typeof(Match)))
						{
							list.Add(match);
						}
						if (parameters[0].ParameterType == typeof(string))
						{
							list.Add(match.Groups[1].ToString());
						}
					}
					object obj = (command.method.IsStatic ? null : WishManager.GetInstanceForType(command.type));
					object obj2 = command.method.Invoke(obj, list.ToArray());
					if (command.method.ReturnType == typeof(void) || (bool)obj2)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06003C40 RID: 15424 RVA: 0x00126B6C File Offset: 0x00124D6C
		[WishCommand("rareliquids", null)]
		public static bool RareLiquids()
		{
			List<Cell> localAdjacentCells = The.PlayerCell.GetLocalAdjacentCells();
			foreach (string text in new string[] { "CloningPool", "WarmStaticPuddle", "BrainBrinePool", "FluxPhail" })
			{
				if (localAdjacentCells.Count < 1)
				{
					break;
				}
				Cell cell = localAdjacentCells[0];
				localAdjacentCells.RemoveAt(0);
				XRL.World.GameObject openLiquidVolume = cell.GetOpenLiquidVolume();
				if (openLiquidVolume != null)
				{
					openLiquidVolume.Destroy(null, false, false, null);
				}
				string result = WishSearcher.SearchForBlueprint(text).Result;
				cell.AddObject(GameObjectFactory.Factory.CreateObject(result, 0, 0, null, null, null, "Wish", null), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			}
			return true;
		}

		// Token: 0x06003C41 RID: 15425 RVA: 0x00126C2C File Offset: 0x00124E2C
		[WishCommand("revealandpulldown", null)]
		public static void RevealAndPullDown(string rest)
		{
			JournalMapNote mapNote = JournalAPI.GetMapNote(rest);
			if (mapNote != null)
			{
				if (!mapNote.Revealed)
				{
					JournalAPI.RevealMapNote(mapNote, false, null);
				}
				WishManager.PullDown(mapNote.ZoneID);
			}
		}

		// Token: 0x06003C42 RID: 15426 RVA: 0x00126C60 File Offset: 0x00124E60
		[WishCommand("pulldown", null)]
		public static void PullDown(string location)
		{
			Cell pullDownLocation = The.ZoneManager.GetZone(location).GetPullDownLocation(The.Player);
			The.Player.TeleportTo(pullDownLocation, new int?(0), true, false, false, false, "disappear", "appear");
		}

		// Token: 0x04001D92 RID: 7570
		[ModSensitiveStaticCache(false)]
		private static List<WishManager.Command> CommandCollection;

		// Token: 0x04001D93 RID: 7571
		[ModSensitiveStaticCache(false)]
		private static Dictionary<Type, object> CommandObjects;

		// Token: 0x0200197A RID: 6522
		private struct Command
		{
			// Token: 0x0600E3BC RID: 58300 RVA: 0x0052826F File Offset: 0x0052646F
			public override string ToString()
			{
				return string.Format("WishManager.Command: ${0} ${1} ${2}", this.regex, this.type, this.method.Name);
			}

			// Token: 0x040065E7 RID: 26087
			public WishCommand wish;

			// Token: 0x040065E8 RID: 26088
			public Type type;

			// Token: 0x040065E9 RID: 26089
			public MethodInfo method;

			// Token: 0x040065EA RID: 26090
			public Regex regex;
		}
	}
}
