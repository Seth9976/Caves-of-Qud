using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using ConsoleLib.Console;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.EventsModels;
using UnityEngine;
using XRL;
using XRL.Core;
using XRL.UI;

// Token: 0x0200015B RID: 347
public class MetricsManager
{
	// Token: 0x06000DB5 RID: 3509 RVA: 0x00058C88 File Offset: 0x00056E88
	public static void LogExceptionNow(Exception ex)
	{
		if (!MetricsManager.bInit)
		{
			return;
		}
		bool enableMetrics = Globals.EnableMetrics;
	}

	// Token: 0x06000DB6 RID: 3510 RVA: 0x00058C98 File Offset: 0x00056E98
	public static void LogExceptionNow(string ex)
	{
		if (!MetricsManager.bInit)
		{
			return;
		}
		bool enableMetrics = Globals.EnableMetrics;
	}

	// Token: 0x06000DB7 RID: 3511 RVA: 0x00058CA8 File Offset: 0x00056EA8
	public static void Init()
	{
		if (!Globals.EnableMetrics)
		{
			return;
		}
		if (MetricsManager.ManagerObject == null)
		{
			MetricsManager.ManagerObject = new GameObject();
			MetricsManager.ManagerObject.name = "PlayFab Dummy";
		}
		global::UnityEngine.Debug.Log("PlayFab player id: " + MetricsManager.GetAnonymousID());
		PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest
		{
			CustomId = MetricsManager.GetAnonymousID(),
			CreateAccount = new bool?(true),
			TitleId = "9C73C"
		}, new Action<LoginResult>(MetricsManager.OnLoginSuccess), new Action<PlayFabError>(MetricsManager.OnLoginFailure), null, null);
	}

	// Token: 0x06000DB8 RID: 3512 RVA: 0x00058D40 File Offset: 0x00056F40
	private static string GetAnonymousID()
	{
		string text = PlayerPrefs.GetString("_PlayfabID", null);
		if (string.IsNullOrEmpty(text))
		{
			text = Guid.NewGuid().ToString();
			PlayerPrefs.SetString("_PlayfabID", text);
		}
		return text;
	}

	// Token: 0x06000DB9 RID: 3513 RVA: 0x00058D84 File Offset: 0x00056F84
	private static void OnLoginSuccess(LoginResult result)
	{
		global::UnityEngine.Debug.Log("PlayFab login success.");
		MetricsManager.entityTokenResponse = result.EntityToken;
		string text = "PlayFab token expires ";
		DateTime? tokenExpiration = MetricsManager.entityTokenResponse.TokenExpiration;
		global::UnityEngine.Debug.Log(text + tokenExpiration.ToString());
	}

	// Token: 0x06000DBA RID: 3514 RVA: 0x00058DCD File Offset: 0x00056FCD
	private static void OnLoginFailure(PlayFabError error)
	{
		global::UnityEngine.Debug.LogError("PlayFab login failure:" + error.GenerateErrorReport());
	}

	// Token: 0x06000DBB RID: 3515 RVA: 0x00058DE4 File Offset: 0x00056FE4
	public static void Update()
	{
		if (MetricsManager.Events.Count > 0)
		{
			CleanQueue<MetricsManager.TelemetryEntry> events = MetricsManager.Events;
			lock (events)
			{
				CleanQueue<MetricsManager.TelemetryEntry> eventPool = MetricsManager.EventPool;
				lock (eventPool)
				{
					while (MetricsManager.Events.Count > 0)
					{
						MetricsManager.TelemetryEntry telemetryEntry = MetricsManager.Events.Dequeue();
						telemetryEntry.Send();
						telemetryEntry.Clear();
						MetricsManager.EventPool.Enqueue(telemetryEntry);
					}
				}
			}
		}
	}

	// Token: 0x06000DBC RID: 3516 RVA: 0x00058E88 File Offset: 0x00057088
	[Conditional("UNITY_EDITOR")]
	public static void LogEditorInfo(string Message)
	{
	}

	// Token: 0x06000DBD RID: 3517 RVA: 0x00058E8C File Offset: 0x0005708C
	public static void LogModInfo(ModInfo mod, object Message)
	{
		string text = ((mod != null) ? ("[" + mod.DisplayTitleStripped + "]") : "");
		if (mod != null)
		{
			ModSettings settings = mod.Settings;
			if (settings != null)
			{
				List<string> warnings = settings.Warnings;
				if (warnings != null)
				{
					warnings.Add(Message.ToString());
				}
			}
		}
		global::UnityEngine.Debug.Log("MODINFO " + text + " - " + ((Message != null) ? Message.ToString() : null));
	}

	// Token: 0x06000DBE RID: 3518 RVA: 0x00058EFF File Offset: 0x000570FF
	public static void LogAssemblyWarning(Type Type, object Message)
	{
		MetricsManager.LogAssemblyWarning((Type != null) ? Type.Assembly : null, Message);
	}

	// Token: 0x06000DBF RID: 3519 RVA: 0x00058F13 File Offset: 0x00057113
	public static void LogAssemblyWarning(MemberInfo Info, object Message)
	{
		Assembly assembly;
		if (Info == null)
		{
			assembly = null;
		}
		else
		{
			Type declaringType = Info.DeclaringType;
			assembly = ((declaringType != null) ? declaringType.Assembly : null);
		}
		MetricsManager.LogAssemblyWarning(assembly, Message);
	}

	// Token: 0x06000DC0 RID: 3520 RVA: 0x00058F34 File Offset: 0x00057134
	public static void LogAssemblyWarning(Assembly Assembly, object Message)
	{
		ModInfo mod = ModManager.GetMod(Assembly);
		if (mod != null)
		{
			mod.Warn(Message);
			return;
		}
		MetricsManager.LogWarning(Message);
	}

	// Token: 0x06000DC1 RID: 3521 RVA: 0x00058F5C File Offset: 0x0005715C
	public static void LogModWarning(ModInfo mod, object Message)
	{
		string text = ((mod != null) ? ("[" + mod.DisplayTitleStripped + "]") : "");
		if (mod != null)
		{
			ModSettings settings = mod.Settings;
			if (settings != null)
			{
				List<string> warnings = settings.Warnings;
				if (warnings != null)
				{
					warnings.Add(Message.ToString());
				}
			}
		}
		global::UnityEngine.Debug.LogWarning("MODWARN " + text + " - " + ((Message != null) ? Message.ToString() : null));
	}

	// Token: 0x06000DC2 RID: 3522 RVA: 0x00058FCF File Offset: 0x000571CF
	public static void LogWarning(string Message)
	{
		global::UnityEngine.Debug.LogWarning("WARN - " + Message);
	}

	// Token: 0x06000DC3 RID: 3523 RVA: 0x00058FE1 File Offset: 0x000571E1
	public static void LogWarning(object Message)
	{
		global::UnityEngine.Debug.LogWarning("WARN - " + Message.ToString());
	}

	// Token: 0x06000DC4 RID: 3524 RVA: 0x00058FF8 File Offset: 0x000571F8
	[Conditional("UNITY_EDITOR")]
	public static void LogEditorWarning(string Message)
	{
	}

	// Token: 0x06000DC5 RID: 3525 RVA: 0x00058FFC File Offset: 0x000571FC
	public static void LogFileWarning(DataFile File, object Message, IXmlLineInfo Line = null)
	{
		string text = ((Line != null) ? string.Format("File: {0}, Line: {1}:{2} ", File.Path, Line.LineNumber, Line.LinePosition) : ("File: " + File.Path + " "));
		if (File.IsMod)
		{
			MetricsManager.LogModWarning(File.Mod, text + ((Message != null) ? Message.ToString() : null));
			return;
		}
		MetricsManager.LogWarning(text + ((Message != null) ? Message.ToString() : null));
	}

	// Token: 0x06000DC6 RID: 3526 RVA: 0x00059087 File Offset: 0x00057287
	[Conditional("UNITY_EDITOR")]
	public static void LogEditorError(string Message)
	{
	}

	// Token: 0x06000DC7 RID: 3527 RVA: 0x00059089 File Offset: 0x00057289
	[Conditional("UNITY_EDITOR")]
	public static void LogEditorError(string Context, string Message)
	{
	}

	// Token: 0x06000DC8 RID: 3528 RVA: 0x0005908B File Offset: 0x0005728B
	public static void LogError(object Message)
	{
		MetricsManager.LogError(Message.ToString());
	}

	// Token: 0x06000DC9 RID: 3529 RVA: 0x00059098 File Offset: 0x00057298
	public static void LogError(object Context, object Message)
	{
		MetricsManager.LogError(Context.ToString(), Message.ToString());
	}

	// Token: 0x06000DCA RID: 3530 RVA: 0x000590AC File Offset: 0x000572AC
	public static void LogCallingModError(object Message)
	{
		ModInfo modInfo;
		StackFrame stackFrame;
		if (ModManager.TryGetCallingMod(out modInfo, out stackFrame))
		{
			MethodBase method = stackFrame.GetMethod();
			modInfo.Error(string.Concat(new string[]
			{
				(Message != null) ? Message.ToString() : null,
				" in ",
				method.DeclaringType.FullName,
				".",
				method.Name
			}));
			return;
		}
		MetricsManager.LogModError(null, Message);
	}

	// Token: 0x06000DCB RID: 3531 RVA: 0x0005911A File Offset: 0x0005731A
	public static void LogAssemblyError(Type Type, object Message)
	{
		MetricsManager.LogAssemblyError((Type != null) ? Type.Assembly : null, Message);
	}

	// Token: 0x06000DCC RID: 3532 RVA: 0x0005912E File Offset: 0x0005732E
	public static void LogAssemblyError(MemberInfo Info, object Message)
	{
		Assembly assembly;
		if (Info == null)
		{
			assembly = null;
		}
		else
		{
			Type declaringType = Info.DeclaringType;
			assembly = ((declaringType != null) ? declaringType.Assembly : null);
		}
		MetricsManager.LogAssemblyError(assembly, Message);
	}

	// Token: 0x06000DCD RID: 3533 RVA: 0x00059150 File Offset: 0x00057350
	public static void LogAssemblyError(Assembly Assembly, object Message)
	{
		ModInfo mod = ModManager.GetMod(Assembly);
		if (mod != null)
		{
			mod.Error(Message);
			return;
		}
		MetricsManager.LogError(Message);
	}

	// Token: 0x06000DCE RID: 3534 RVA: 0x00059175 File Offset: 0x00057375
	public static void LogPotentialModError(ModInfo Mod, object Message)
	{
		if (Mod != null)
		{
			MetricsManager.LogModError(Mod, Message);
			return;
		}
		MetricsManager.LogError(Message);
	}

	// Token: 0x06000DCF RID: 3535 RVA: 0x00059188 File Offset: 0x00057388
	public static void LogModError(ModInfo mod, object Message)
	{
		string text = ((mod != null) ? (" [" + mod.DisplayTitleStripped + "]") : "");
		if (mod != null)
		{
			ModSettings settings = mod.Settings;
			if (settings != null)
			{
				List<string> errors = settings.Errors;
				if (errors != null)
				{
					errors.Add(Message.ToString());
				}
			}
		}
		global::UnityEngine.Debug.LogError("MODERROR" + text + " - " + ((Message != null) ? Message.ToString() : null));
		if (Options.ShowErrorPopups)
		{
			string text2 = ConsoleLib.Console.ColorUtility.EscapeFormatting(Message.ToString());
			string text3 = (((mod != null) ? mod.DisplayTitle : null) ?? "{{R|Mod}}") + " {{R|Error}}";
			Popup.ShowBlockPrompt(text2, text3, "Sounds/UI/ui_notification", null, true, true, true, false, true);
		}
		if (Globals.EnableMetrics && mod != null)
		{
			string text4 = Message.ToString();
			if (MetricsManager.exceptionsSent.Contains(text4))
			{
				return;
			}
			MetricsManager.exceptionsSent.Add(text4);
			MetricsManager.SendTelemetry("mod_error", "diagnostics.moderror", new Dictionary<string, string>
			{
				{ "data", text4 },
				{ "mod", mod.ID }
			}, false, mod);
		}
	}

	// Token: 0x06000DD0 RID: 3536 RVA: 0x000592A0 File Offset: 0x000574A0
	public static void LogFileError(DataFile File, object Message, IXmlLineInfo Line = null)
	{
		string text = ((Line != null) ? string.Format("File: {0}, Line: {1}:{2} ", File.Path, Line.LineNumber, Line.LinePosition) : ("File: " + File.Path + " "));
		if (File.IsMod)
		{
			MetricsManager.LogModError(File.Mod, text + ((Message != null) ? Message.ToString() : null));
			return;
		}
		MetricsManager.LogError(text + ((Message != null) ? Message.ToString() : null));
	}

	// Token: 0x06000DD1 RID: 3537 RVA: 0x0005932B File Offset: 0x0005752B
	public static void LogKeybinding(GameCommand binding)
	{
		MetricsManager.LogKeybinding(binding.ID);
	}

	// Token: 0x06000DD2 RID: 3538 RVA: 0x00059338 File Offset: 0x00057538
	public static void LogKeybinding(string binding)
	{
		string text = CommandBindingManager.GetSerializedBindingsForCommand(binding).Aggregate("", delegate(string a, string b)
		{
			if (!string.IsNullOrEmpty(a))
			{
				return a + ", " + b;
			}
			return b;
		});
		MetricsManager.LogInfo("Changed Keybinding " + binding + " to " + text);
		if (Globals.EnableMetrics)
		{
			MetricsManager.SendTelemetry("keybind", "diagnostics.keybind", new Dictionary<string, string>
			{
				{ "command", binding },
				{ "binding", text }
			}, false, null);
		}
	}

	// Token: 0x06000DD3 RID: 3539 RVA: 0x000593C0 File Offset: 0x000575C0
	public static void LogError(string Message)
	{
		ModInfo modInfo;
		StackFrame stackFrame;
		if (ModManager.TryGetCallingMod(out modInfo, out stackFrame))
		{
			modInfo.Error(Message);
			return;
		}
		global::UnityEngine.Debug.LogError("ERROR  - " + Message);
		if (Options.ShowErrorPopups)
		{
			Popup.ShowBlockPrompt(Message + "\n" + new StackTrace(1).ToString(), "{{R|Error}}", "Sounds/UI/ui_notification", null, true, true, true, false, true);
		}
		if (Globals.EnableMetrics)
		{
			if (MetricsManager.exceptionsSent.Contains(Message))
			{
				return;
			}
			MetricsManager.exceptionsSent.Add(Message);
			MetricsManager.SendTelemetry("game_error", "diagnostics.exceptions", Message + "\n" + new StackTrace(1).ToString());
		}
	}

	// Token: 0x06000DD4 RID: 3540 RVA: 0x0005946C File Offset: 0x0005766C
	public static void LogError(string Context, string Message)
	{
		ModInfo modInfo;
		StackFrame stackFrame;
		if (ModManager.TryGetCallingMod(out modInfo, out stackFrame))
		{
			modInfo.Error(Context + "@" + Message);
			return;
		}
		global::UnityEngine.Debug.LogError("ERROR - " + Context + "@" + Message);
		if (Options.ShowErrorPopups)
		{
			Popup.ShowBlockPrompt(string.Concat(new string[]
			{
				Context,
				" - ",
				Message,
				"\n",
				new StackTrace(1).ToString()
			}), "{{R|Error}}", "Sounds/UI/ui_notification", null, true, true, true, false, true);
		}
		if (Globals.EnableMetrics)
		{
			string text = Context + " - " + Message;
			if (MetricsManager.exceptionsSent.Contains(text))
			{
				return;
			}
			MetricsManager.exceptionsSent.Add(text);
			MetricsManager.SendTelemetry("game_error", "diagnostics.exceptions", text);
		}
	}

	// Token: 0x06000DD5 RID: 3541 RVA: 0x00059539 File Offset: 0x00057739
	public static void LogInfo(string Message)
	{
		global::UnityEngine.Debug.Log("INFO - " + Message);
	}

	// Token: 0x06000DD6 RID: 3542 RVA: 0x0005954B File Offset: 0x0005774B
	public static void LogInfo(string Context, string Message)
	{
		global::UnityEngine.Debug.Log("INFO - at " + Context + ": " + Message);
	}

	// Token: 0x06000DD7 RID: 3543 RVA: 0x00059564 File Offset: 0x00057764
	public static void LogError(string Context, Exception x)
	{
		ModInfo modInfo;
		StackFrame stackFrame;
		if (ModManager.TryGetCallingMod(out modInfo, out stackFrame))
		{
			modInfo.Error(string.Format("{0}: {1}", Context, x));
			return;
		}
		global::UnityEngine.Debug.LogError("ERROR - " + Context + " :" + x.ToString());
		if (Options.ShowErrorPopups)
		{
			Popup.ShowBlockPrompt(Context + ":\n" + x.ToString(), "{{R|Error}}", "Sounds/UI/ui_notification", null, true, true, true, false, true);
		}
	}

	// Token: 0x06000DD8 RID: 3544 RVA: 0x000595D8 File Offset: 0x000577D8
	public static void SendTelemetryWithPayload(string name, string category = "unassigned", Dictionary<string, string> payload = null)
	{
		MetricsManager.SendTelemetry(name, category, payload, false, null);
	}

	// Token: 0x06000DD9 RID: 3545 RVA: 0x000595E4 File Offset: 0x000577E4
	public static void SendTelemetry(string name, string category = "unassigned", string payload = null)
	{
		MetricsManager.SendTelemetry(name, category, new Dictionary<string, string> { { "data", payload } }, false, null);
	}

	// Token: 0x06000DDA RID: 3546 RVA: 0x00059600 File Offset: 0x00057800
	public static void SendTelemetryJSON(string name, string category = "unassigned", string payload = "{}")
	{
		MetricsManager.SendTelemetry(name, category, payload, true, null);
	}

	// Token: 0x06000DDB RID: 3547 RVA: 0x0005961C File Offset: 0x0005781C
	public static void SendTelemetry(string name, string category, object payload, bool jsonPayload = false, ModInfo modInfo = null)
	{
		if (payload == null)
		{
			payload = new Dictionary<string, string> { { "data", "none" } };
		}
		if (Globals.EnableMetrics)
		{
			CleanQueue<MetricsManager.TelemetryEntry> events = MetricsManager.Events;
			lock (events)
			{
				CleanQueue<MetricsManager.TelemetryEntry> eventPool = MetricsManager.EventPool;
				lock (eventPool)
				{
					MetricsManager.TelemetryEntry telemetryEntry;
					if (MetricsManager.EventPool.Count > 0)
					{
						telemetryEntry = MetricsManager.EventPool.Dequeue();
					}
					else
					{
						telemetryEntry = new MetricsManager.TelemetryEntry();
					}
					telemetryEntry.name = name;
					telemetryEntry.category = category;
					telemetryEntry.data = payload;
					telemetryEntry.isJSON = jsonPayload;
					telemetryEntry.modInfo = modInfo;
					MetricsManager.Events.Enqueue(telemetryEntry);
				}
			}
		}
	}

	// Token: 0x06000DDC RID: 3548 RVA: 0x000596F8 File Offset: 0x000578F8
	private static void _SendTelemetry(string name, object payload, string category = "unassigned", ModInfo modInfo = null, bool jsonPayload = false)
	{
		try
		{
			if (payload == null)
			{
				payload = new Dictionary<string, string> { { "data", "none" } };
			}
			WriteEventsRequest writeEventsRequest = new WriteEventsRequest();
			EventContents eventContents = new EventContents();
			writeEventsRequest.Events = new List<EventContents> { eventContents };
			eventContents.Name = name;
			eventContents.EventNamespace = "custom.player." + category;
			eventContents.OriginalTimestamp = new DateTime?(DateTime.UtcNow);
			if (jsonPayload)
			{
				eventContents.Payload = null;
				eventContents.PayloadJSON = (string)payload;
			}
			else
			{
				eventContents.PayloadJSON = null;
				eventContents.Payload = payload;
			}
			if (MetricsManager.globalTags == null)
			{
				MetricsManager.globalTags = new Dictionary<string, string>();
				MetricsManager.globalTags.Add("version", XRLGame.CoreVersion.ToString());
			}
			eventContents.CustomTags = new Dictionary<string, string>(MetricsManager.globalTags);
			foreach (string text in MetricsManager.OptionsToIncludeInTelemetry)
			{
				eventContents.CustomTags.Add(text, Options.GetOption(text, ""));
			}
			StringBuilder modList = new StringBuilder();
			ModManager.ForEachMod(delegate(ModInfo mod)
			{
				modList.AppendFormat("{0}:{1};", mod.ID, mod.Manifest.Version.ToString());
			}, false);
			if (modList.Length > 0)
			{
				if (modList.Length > 128)
				{
					modList.Remove(119, modList.Length - 120);
					modList.Append("; (more)");
				}
				eventContents.CustomTags.Add("mods", modList.ToString());
			}
			if (MetricsManager.entityTokenResponse != null)
			{
				PlayFabEventsAPI.WriteTelemetryEvents(writeEventsRequest, new Action<WriteEventsResponse>(MetricsManager.OnWriteTelemetrySuccess), new Action<PlayFabError>(MetricsManager.OnWriteTelemetryFailure), null, null);
			}
		}
		catch (Exception ex)
		{
			global::UnityEngine.Debug.LogError("Telemetry exception: " + ex.ToString());
		}
	}

	// Token: 0x06000DDD RID: 3549 RVA: 0x00059910 File Offset: 0x00057B10
	private static void _SendTelemetryJSON(string name, string jsonpayload, string category = "unassigned", ModInfo modInfo = null)
	{
		MetricsManager._SendTelemetry(name, jsonpayload, category, modInfo, true);
	}

	// Token: 0x06000DDE RID: 3550 RVA: 0x0005991C File Offset: 0x00057B1C
	private static void OnWriteTelemetrySuccess(WriteEventsResponse request)
	{
	}

	// Token: 0x06000DDF RID: 3551 RVA: 0x0005991E File Offset: 0x00057B1E
	private static void OnWriteTelemetryFailure(PlayFabError error)
	{
		if (MetricsManager.telemetryErrorLogged > 0)
		{
			return;
		}
		MetricsManager.telemetryErrorLogged++;
		global::UnityEngine.Debug.LogError("Telemetry error: " + error.ToString());
	}

	// Token: 0x06000DE0 RID: 3552 RVA: 0x0005994A File Offset: 0x00057B4A
	public static void rngCheckpoint(string location)
	{
	}

	// Token: 0x06000DE1 RID: 3553 RVA: 0x0005994C File Offset: 0x00057B4C
	public static void LogException(string Context, Exception x, string category = "game_exception")
	{
		string text = Context + ": " + ((x != null) ? x.ToString() : null);
		ModInfo modInfo;
		StackFrame stackFrame;
		if (ModManager.TryGetStackMod(x, out modInfo, out stackFrame))
		{
			modInfo.Error(text);
			return;
		}
		global::UnityEngine.Debug.LogError(text);
		if (Options.ShowErrorPopups)
		{
			Popup.ShowBlockPrompt(text, "{{R|Error}}", "Sounds/UI/ui_notification", null, true, true, true, false, true);
		}
		try
		{
			if (Globals.EnableMetrics)
			{
				if (!MetricsManager.exceptionsSent.Contains(text))
				{
					MetricsManager.exceptionsSent.Add(text);
					MetricsManager.SendTelemetry(category, "diagnostics.exceptions", text);
				}
			}
		}
		catch (Exception ex)
		{
			global::UnityEngine.Debug.LogError("Metrics exception: " + ex.ToString());
		}
	}

	// Token: 0x06000DE2 RID: 3554 RVA: 0x00059A04 File Offset: 0x00057C04
	public static void LogEvent(string EventName)
	{
		try
		{
			bool flag = MetricsManager.bInit;
		}
		catch (Exception ex)
		{
			global::UnityEngine.Debug.LogError("Metrics exception: " + ex.ToString());
		}
	}

	// Token: 0x06000DE3 RID: 3555 RVA: 0x00059A44 File Offset: 0x00057C44
	public static void LogEvent(string EventName, float Value)
	{
		try
		{
			bool flag = MetricsManager.bInit;
		}
		catch (Exception ex)
		{
			global::UnityEngine.Debug.LogError("Metrics exception: " + ex.ToString());
		}
	}

	// Token: 0x0400098E RID: 2446
	public static List<string> OptionsToIncludeInTelemetry = new List<string> { "OptionsPrereleaseInputManager", "OptionOverlayUI", "OptionOverlayPrereleaseStage" };

	// Token: 0x0400098F RID: 2447
	public static bool bInit = false;

	// Token: 0x04000990 RID: 2448
	public static GameObject ManagerObject = null;

	// Token: 0x04000991 RID: 2449
	private static EntityTokenResponse entityTokenResponse = null;

	// Token: 0x04000992 RID: 2450
	private static CleanQueue<MetricsManager.TelemetryEntry> EventPool = new CleanQueue<MetricsManager.TelemetryEntry>();

	// Token: 0x04000993 RID: 2451
	private static CleanQueue<MetricsManager.TelemetryEntry> Events = new CleanQueue<MetricsManager.TelemetryEntry>();

	// Token: 0x04000994 RID: 2452
	public static Dictionary<string, string> globalTags = null;

	// Token: 0x04000995 RID: 2453
	private static int telemetryErrorLogged = 0;

	// Token: 0x04000996 RID: 2454
	public static FieldInfo RandomItertorField = null;

	// Token: 0x04000997 RID: 2455
	public static FieldInfo RandomItertorNextField = null;

	// Token: 0x04000998 RID: 2456
	public static Dictionary<string, int> lastCheckpoint = new Dictionary<string, int>();

	// Token: 0x04000999 RID: 2457
	public static HashSet<string> exceptionsSent = new HashSet<string>();

	// Token: 0x020015FF RID: 5631
	public class TelemetryEntry
	{
		// Token: 0x0600D688 RID: 54920 RVA: 0x004F88DE File Offset: 0x004F6ADE
		public TelemetryEntry()
		{
		}

		// Token: 0x0600D689 RID: 54921 RVA: 0x004F88E6 File Offset: 0x004F6AE6
		public TelemetryEntry(string name, object data = null, string category = "uncategorized", bool json = false, ModInfo modInfo = null)
			: this()
		{
			this.name = name;
			this.data = data;
			this.category = category;
			this.isJSON = json;
			this.modInfo = modInfo;
		}

		// Token: 0x0600D68A RID: 54922 RVA: 0x004F8914 File Offset: 0x004F6B14
		public void Send()
		{
			if (this.isJSON)
			{
				MetricsManager._SendTelemetryJSON(this.name, (string)this.data, this.category, this.modInfo);
				return;
			}
			MetricsManager._SendTelemetry(this.name, this.data, this.category, this.modInfo, false);
		}

		// Token: 0x0600D68B RID: 54923 RVA: 0x004F896A File Offset: 0x004F6B6A
		public void Clear()
		{
			this.name = null;
			this.isJSON = false;
			this.data = null;
			this.category = null;
		}

		// Token: 0x0400589C RID: 22684
		public bool isJSON;

		// Token: 0x0400589D RID: 22685
		public object data;

		// Token: 0x0400589E RID: 22686
		public string category;

		// Token: 0x0400589F RID: 22687
		public string name;

		// Token: 0x040058A0 RID: 22688
		public ModInfo modInfo;
	}
}
