using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using UnityEngine.Bindings;
using UnityEngine.Diagnostics;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000101 RID: 257
	[NativeHeader("Runtime/Application/AdsIdHandler.h")]
	[NativeHeader("Runtime/Application/ApplicationInfo.h")]
	[NativeHeader("Runtime/Export/Application/Application.bindings.h")]
	[NativeHeader("Runtime/File/ApplicationSpecificPersistentDataPath.h")]
	[NativeHeader("Runtime/Input/GetInput.h")]
	[NativeHeader("Runtime/PreloadManager/LoadSceneOperation.h")]
	[NativeHeader("Runtime/Input/TargetFrameRate.h")]
	[NativeHeader("Runtime/BaseClasses/IsPlaying.h")]
	[NativeHeader("Runtime/Misc/BuildSettings.h")]
	[NativeHeader("Runtime/Utilities/URLUtility.h")]
	[NativeHeader("Runtime/Logging/LogSystem.h")]
	[NativeHeader("Runtime/PreloadManager/PreloadManager.h")]
	[NativeHeader("Runtime/Input/InputManager.h")]
	[NativeHeader("Runtime/Utilities/Argv.h")]
	[NativeHeader("Runtime/Misc/SystemInfo.h")]
	[NativeHeader("Runtime/Misc/PlayerSettings.h")]
	[NativeHeader("Runtime/Misc/Player.h")]
	[NativeHeader("Runtime/Network/NetworkUtility.h")]
	public class Application
	{
		// Token: 0x06000578 RID: 1400
		[FreeFunction("GetInputManager().QuitApplication")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Quit(int exitCode);

		// Token: 0x06000579 RID: 1401 RVA: 0x0000A0BC File Offset: 0x000082BC
		public static void Quit()
		{
			Application.Quit(0);
		}

		// Token: 0x0600057A RID: 1402
		[Obsolete("CancelQuit is deprecated. Use the wantsToQuit event instead.")]
		[FreeFunction("GetInputManager().CancelQuitApplication")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CancelQuit();

		// Token: 0x0600057B RID: 1403
		[FreeFunction("Application_Bindings::Unload")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Unload();

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600057C RID: 1404
		[Obsolete("This property is deprecated, please use LoadLevelAsync to detect if a specific scene is currently loading.")]
		public static extern bool isLoadingLevel
		{
			[FreeFunction("GetPreloadManager().IsLoadingOrQueued")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x0600057D RID: 1405
		[FreeFunction("UpdateMemoryUsage")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SimulateMemoryUsage(ApplicationMemoryUsage usage);

		// Token: 0x0600057E RID: 1406 RVA: 0x0000A0C8 File Offset: 0x000082C8
		[Obsolete("Streaming was a Unity Web Player feature, and is removed. This function is deprecated and always returns 1.0 for valid level indices.")]
		public static float GetStreamProgressForLevel(int levelIndex)
		{
			bool flag = levelIndex >= 0 && levelIndex < SceneManager.sceneCountInBuildSettings;
			float num;
			if (flag)
			{
				num = 1f;
			}
			else
			{
				num = 0f;
			}
			return num;
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0000A0FC File Offset: 0x000082FC
		[Obsolete("Streaming was a Unity Web Player feature, and is removed. This function is deprecated and always returns 1.0.")]
		public static float GetStreamProgressForLevel(string levelName)
		{
			return 1f;
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x0000A114 File Offset: 0x00008314
		[Obsolete("Streaming was a Unity Web Player feature, and is removed. This property is deprecated and always returns 0.")]
		public static int streamedBytes
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x0000A128 File Offset: 0x00008328
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Application.webSecurityEnabled is no longer supported, since the Unity Web Player is no longer supported by Unity", true)]
		public static bool webSecurityEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0000A13C File Offset: 0x0000833C
		public static bool CanStreamedLevelBeLoaded(int levelIndex)
		{
			return levelIndex >= 0 && levelIndex < SceneManager.sceneCountInBuildSettings;
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0000A160 File Offset: 0x00008360
		[FreeFunction("Application_Bindings::CanStreamedLevelBeLoaded")]
		public unsafe static bool CanStreamedLevelBeLoaded(string levelName)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(levelName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = levelName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = Application.CanStreamedLevelBeLoaded_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000584 RID: 1412
		public static extern bool isPlaying
		{
			[FreeFunction("IsWorldPlaying")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0000A1B8 File Offset: 0x000083B8
		[FreeFunction]
		public static bool IsPlaying([NotNull] Object obj)
		{
			if (obj == null)
			{
				ThrowHelper.ThrowArgumentNullException(obj, "obj");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Object>(obj);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(obj, "obj");
			}
			return Application.IsPlaying_Injected(intPtr);
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000586 RID: 1414
		public static extern bool isFocused
		{
			[FreeFunction("IsPlayerFocused")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000587 RID: 1415
		[FreeFunction("GetBuildSettings().GetBuildTags")]
		[Obsolete("Application.GetBuildTags is no longer supported and will be removed.", false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string[] GetBuildTags();

		// Token: 0x06000588 RID: 1416
		[FreeFunction("GetBuildSettings().SetBuildTags")]
		[Obsolete("Application.SetBuildTags is no longer supported and will be removed.", false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetBuildTags(string[] buildTags);

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x0000A1F0 File Offset: 0x000083F0
		public static string buildGUID
		{
			[FreeFunction("Application_Bindings::GetBuildGUID")]
			get
			{
				string stringAndDispose;
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					Application.get_buildGUID_Injected(out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600058A RID: 1418
		// (set) Token: 0x0600058B RID: 1419
		public static extern bool runInBackground
		{
			[FreeFunction("GetPlayerSettingsRunInBackground")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction("SetPlayerSettingsRunInBackground")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x0600058C RID: 1420
		[FreeFunction("GetBuildSettings().GetHasPROVersion")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool HasProLicense();

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600058D RID: 1421
		public static extern bool isBatchMode
		{
			[FreeFunction("::IsBatchmode")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600058E RID: 1422
		internal static extern bool isTestRun
		{
			[FreeFunction("::IsTestRun")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600058F RID: 1423
		internal static extern bool isBuildingEditorResources
		{
			[FreeFunction("::IsBuildingEditorResources")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000590 RID: 1424
		internal static extern bool isHumanControllingUs
		{
			[FreeFunction("::IsHumanControllingUs")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0000A220 File Offset: 0x00008420
		[FreeFunction("HasARGV")]
		internal unsafe static bool HasARGV(string name)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = Application.HasARGV_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0000A278 File Offset: 0x00008478
		[FreeFunction("GetFirstValueForARGV")]
		internal unsafe static string GetValueForARGV(string name)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				Application.GetValueForARGV_Injected(ref managedSpanWrapper, out managedSpanWrapper2);
			}
			finally
			{
				char* ptr = null;
				ManagedSpanWrapper managedSpanWrapper2;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper2);
			}
			return stringAndDispose;
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x0000A2D8 File Offset: 0x000084D8
		public static string dataPath
		{
			[FreeFunction("GetAppDataPath", IsThreadSafe = true)]
			get
			{
				string stringAndDispose;
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					Application.get_dataPath_Injected(out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x0000A308 File Offset: 0x00008508
		public static string streamingAssetsPath
		{
			[FreeFunction("GetStreamingAssetsPath", IsThreadSafe = true)]
			get
			{
				string stringAndDispose;
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					Application.get_streamingAssetsPath_Injected(out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x0000A338 File Offset: 0x00008538
		public static string persistentDataPath
		{
			[FreeFunction("GetPersistentDataPathApplicationSpecific")]
			get
			{
				string stringAndDispose;
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					Application.get_persistentDataPath_Injected(out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x0000A368 File Offset: 0x00008568
		public static string temporaryCachePath
		{
			[FreeFunction("GetTemporaryCachePathApplicationSpecific")]
			get
			{
				string stringAndDispose;
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					Application.get_temporaryCachePath_Injected(out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x0000A398 File Offset: 0x00008598
		public static string absoluteURL
		{
			[FreeFunction("GetPlayerSettings().GetAbsoluteURL")]
			get
			{
				string stringAndDispose;
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					Application.get_absoluteURL_Injected(out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0000A3C8 File Offset: 0x000085C8
		[Obsolete("Application.ExternalEval is deprecated. See https://docs.unity3d.com/Manual/webgl-interactingwithbrowserscripting.html for alternatives.")]
		public static void ExternalEval(string script)
		{
			bool flag = script.Length > 0 && script[script.Length - 1] != ';';
			if (flag)
			{
				script += ";";
			}
			Application.Internal_ExternalCall(script);
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0000A410 File Offset: 0x00008610
		[FreeFunction("Application_Bindings::ExternalCall")]
		private unsafe static void Internal_ExternalCall(string script)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(script, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = script.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Application.Internal_ExternalCall_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x0000A464 File Offset: 0x00008664
		public static string unityVersion
		{
			[FreeFunction("Application_Bindings::GetUnityVersion", IsThreadSafe = true)]
			get
			{
				string stringAndDispose;
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					Application.get_unityVersion_Injected(out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600059B RID: 1435
		internal static extern int unityVersionVer
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			[FreeFunction("Application_Bindings::GetUnityVersionVer", IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600059C RID: 1436
		internal static extern int unityVersionMaj
		{
			[FreeFunction("Application_Bindings::GetUnityVersionMaj", IsThreadSafe = true)]
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600059D RID: 1437
		internal static extern int unityVersionMin
		{
			[FreeFunction("Application_Bindings::GetUnityVersionMin", IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x0000A494 File Offset: 0x00008694
		public static string version
		{
			[FreeFunction("GetApplicationInfo().GetVersion")]
			get
			{
				string stringAndDispose;
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					Application.get_version_Injected(out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x0000A4C4 File Offset: 0x000086C4
		public static string installerName
		{
			[FreeFunction("GetApplicationInfo().GetInstallerName")]
			get
			{
				string stringAndDispose;
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					Application.get_installerName_Injected(out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x0000A4F4 File Offset: 0x000086F4
		public static string identifier
		{
			[FreeFunction("GetApplicationInfo().GetApplicationIdentifier")]
			get
			{
				string stringAndDispose;
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					Application.get_identifier_Injected(out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060005A1 RID: 1441
		public static extern ApplicationInstallMode installMode
		{
			[FreeFunction("GetApplicationInfo().GetInstallMode")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060005A2 RID: 1442
		public static extern ApplicationSandboxType sandboxType
		{
			[FreeFunction("GetApplicationInfo().GetSandboxType")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x0000A524 File Offset: 0x00008724
		public static string productName
		{
			[FreeFunction("GetPlayerSettings().GetProductName")]
			get
			{
				string stringAndDispose;
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					Application.get_productName_Injected(out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x0000A554 File Offset: 0x00008754
		public static string companyName
		{
			[FreeFunction("GetPlayerSettings().GetCompanyName")]
			get
			{
				string stringAndDispose;
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					Application.get_companyName_Injected(out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x0000A584 File Offset: 0x00008784
		public static string cloudProjectId
		{
			[FreeFunction("GetPlayerSettings().GetCloudProjectId")]
			get
			{
				string stringAndDispose;
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					Application.get_cloudProjectId_Injected(out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
		}

		// Token: 0x060005A6 RID: 1446
		[FreeFunction("GetAdsIdHandler().RequestAdsIdAsync")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool RequestAdvertisingIdentifierAsync(Application.AdvertisingIdentifierCallback delegateMethod);

		// Token: 0x060005A7 RID: 1447 RVA: 0x0000A5B4 File Offset: 0x000087B4
		[FreeFunction("OpenURL")]
		public unsafe static void OpenURL(string url)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(url, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = url.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Application.OpenURL_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0000A608 File Offset: 0x00008808
		[Obsolete("Use UnityEngine.Diagnostics.Utils.ForceCrash")]
		public static void ForceCrash(int mode)
		{
			Utils.ForceCrash((ForcedCrashCategory)mode);
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060005A9 RID: 1449
		// (set) Token: 0x060005AA RID: 1450
		public static extern int targetFrameRate
		{
			[FreeFunction("GetTargetFrameRate")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction("SetTargetFrameRate")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x060005AB RID: 1451
		[FreeFunction("Application_Bindings::SetLogCallbackDefined")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLogCallbackDefined(bool defined);

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060005AC RID: 1452
		// (set) Token: 0x060005AD RID: 1453
		[Obsolete("Use SetStackTraceLogType/GetStackTraceLogType instead")]
		public static extern StackTraceLogType stackTraceLogType
		{
			[FreeFunction("Application_Bindings::GetStackTraceLogType")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction("Application_Bindings::SetStackTraceLogType")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x060005AE RID: 1454
		[FreeFunction("GetStackTraceLogType")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern StackTraceLogType GetStackTraceLogType(LogType logType);

		// Token: 0x060005AF RID: 1455
		[FreeFunction("SetStackTraceLogType")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStackTraceLogType(LogType logType, StackTraceLogType stackTraceType);

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x0000A614 File Offset: 0x00008814
		public static string consoleLogPath
		{
			[FreeFunction("GetConsoleLogPath")]
			get
			{
				string stringAndDispose;
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					Application.get_consoleLogPath_Injected(out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060005B1 RID: 1457
		// (set) Token: 0x060005B2 RID: 1458
		public static extern ThreadPriority backgroundLoadingPriority
		{
			[FreeFunction("GetPreloadManager().GetThreadPriority")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction("GetPreloadManager().SetThreadPriority")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060005B3 RID: 1459
		public static extern bool genuine
		{
			[FreeFunction("IsApplicationGenuine")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060005B4 RID: 1460
		public static extern bool genuineCheckAvailable
		{
			[FreeFunction("IsApplicationGenuineAvailable")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0000A644 File Offset: 0x00008844
		[FreeFunction("Application_Bindings::RequestUserAuthorization")]
		public static AsyncOperation RequestUserAuthorization(UserAuthorization mode)
		{
			IntPtr intPtr = Application.RequestUserAuthorization_Injected(mode);
			return (intPtr == 0) ? null : AsyncOperation.BindingsMarshaller.ConvertToManaged(intPtr);
		}

		// Token: 0x060005B6 RID: 1462
		[FreeFunction("Application_Bindings::HasUserAuthorization")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool HasUserAuthorization(UserAuthorization mode);

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060005B7 RID: 1463
		internal static extern bool submitAnalytics
		{
			[FreeFunction("GetPlayerSettings().GetSubmitAnalytics")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x0000A668 File Offset: 0x00008868
		[Obsolete("This property is deprecated, please use SplashScreen.isFinished instead")]
		public static bool isShowingSplashScreen
		{
			get
			{
				return !SplashScreen.isFinished;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060005B9 RID: 1465
		public static extern RuntimePlatform platform
		{
			[FreeFunction("systeminfo::GetRuntimePlatform", IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x0000A684 File Offset: 0x00008884
		public static bool isMobilePlatform
		{
			get
			{
				RuntimePlatform platform = Application.platform;
				RuntimePlatform runtimePlatform = platform;
				if (runtimePlatform <= RuntimePlatform.Android)
				{
					if (runtimePlatform != RuntimePlatform.IPhonePlayer && runtimePlatform != RuntimePlatform.Android)
					{
						goto IL_003A;
					}
				}
				else
				{
					if (runtimePlatform - RuntimePlatform.MetroPlayerX86 <= 2)
					{
						return SystemInfo.deviceType == DeviceType.Handheld;
					}
					if (runtimePlatform != RuntimePlatform.VisionOS)
					{
						goto IL_003A;
					}
				}
				return true;
				IL_003A:
				return false;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x0000A6D0 File Offset: 0x000088D0
		public static bool isConsolePlatform
		{
			get
			{
				RuntimePlatform platform = Application.platform;
				return platform == RuntimePlatform.GameCoreXboxOne || platform == RuntimePlatform.GameCoreXboxSeries || platform == RuntimePlatform.PS4 || platform == RuntimePlatform.PS5 || platform == RuntimePlatform.Switch || platform == RuntimePlatform.Switch2 || platform == RuntimePlatform.XboxOne;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060005BC RID: 1468
		public static extern SystemLanguage systemLanguage
		{
			[FreeFunction("(SystemLanguage)systeminfo::GetSystemLanguage")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060005BD RID: 1469
		public static extern NetworkReachability internetReachability
		{
			[FreeFunction("GetInternetReachability")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060005BE RID: 1470 RVA: 0x0000A710 File Offset: 0x00008910
		// (remove) Token: 0x060005BF RID: 1471 RVA: 0x0000A744 File Offset: 0x00008944
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Application.LowMemoryCallback lowMemory;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060005C0 RID: 1472 RVA: 0x0000A778 File Offset: 0x00008978
		// (remove) Token: 0x060005C1 RID: 1473 RVA: 0x0000A7AC File Offset: 0x000089AC
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Application.MemoryUsageChangedCallback memoryUsageChanged;

		// Token: 0x060005C2 RID: 1474 RVA: 0x0000A7E0 File Offset: 0x000089E0
		[RequiredByNativeCode]
		internal static void CallLowMemory(ApplicationMemoryUsage usage)
		{
			Application.MemoryUsageChangedCallback memoryUsageChangedCallback = Application.memoryUsageChanged;
			bool flag = memoryUsageChangedCallback != null;
			if (flag)
			{
				ApplicationMemoryUsageChange applicationMemoryUsageChange = new ApplicationMemoryUsageChange(usage);
				memoryUsageChangedCallback(in applicationMemoryUsageChange);
			}
			if (usage > ApplicationMemoryUsage.High)
			{
				if (usage != ApplicationMemoryUsage.Critical)
				{
					throw new Exception(string.Format("Unknown application memory usage: {0}", usage));
				}
				Application.LowMemoryCallback lowMemoryCallback = Application.lowMemory;
				bool flag2 = lowMemoryCallback != null;
				if (flag2)
				{
					lowMemoryCallback();
				}
			}
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0000A858 File Offset: 0x00008A58
		[RequiredByNativeCode]
		internal static bool HasLogCallback()
		{
			return Application.s_LogCallbackHandler != null || Application.s_LogCallbackHandlerThreaded != null;
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060005C4 RID: 1476 RVA: 0x0000A87C File Offset: 0x00008A7C
		// (remove) Token: 0x060005C5 RID: 1477 RVA: 0x0000A89B File Offset: 0x00008A9B
		public static event Application.LogCallback logMessageReceived
		{
			add
			{
				Application.s_LogCallbackHandler = (Application.LogCallback)Delegate.Combine(Application.s_LogCallbackHandler, value);
				Application.SetLogCallbackDefined(true);
			}
			remove
			{
				Application.s_LogCallbackHandler = (Application.LogCallback)Delegate.Remove(Application.s_LogCallbackHandler, value);
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060005C6 RID: 1478 RVA: 0x0000A8B3 File Offset: 0x00008AB3
		// (remove) Token: 0x060005C7 RID: 1479 RVA: 0x0000A8D2 File Offset: 0x00008AD2
		public static event Application.LogCallback logMessageReceivedThreaded
		{
			add
			{
				Application.s_LogCallbackHandlerThreaded = (Application.LogCallback)Delegate.Combine(Application.s_LogCallbackHandlerThreaded, value);
				Application.SetLogCallbackDefined(true);
			}
			remove
			{
				Application.s_LogCallbackHandlerThreaded = (Application.LogCallback)Delegate.Remove(Application.s_LogCallbackHandlerThreaded, value);
			}
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0000A8EC File Offset: 0x00008AEC
		[RequiredByNativeCode]
		private static void CallLogCallback(string logString, string stackTrace, LogType type, bool invokedOnMainThread)
		{
			if (invokedOnMainThread)
			{
				Application.LogCallback logCallback = Application.s_LogCallbackHandler;
				bool flag = logCallback != null;
				if (flag)
				{
					logCallback(logString, stackTrace, type);
				}
			}
			Application.LogCallback logCallback2 = Application.s_LogCallbackHandlerThreaded;
			bool flag2 = logCallback2 != null;
			if (flag2)
			{
				logCallback2(logString, stackTrace, type);
			}
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0000A934 File Offset: 0x00008B34
		internal static void InvokeOnAdvertisingIdentifierCallback(string advertisingId, bool trackingEnabled)
		{
			bool flag = Application.OnAdvertisingIdentifierCallback != null;
			if (flag)
			{
				Application.OnAdvertisingIdentifierCallback(advertisingId, trackingEnabled, string.Empty);
			}
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0000A960 File Offset: 0x00008B60
		private static string ObjectToJSString(object o)
		{
			bool flag = o == null;
			string text;
			if (flag)
			{
				text = "null";
			}
			else
			{
				bool flag2 = o is string;
				if (flag2)
				{
					string text2 = o.ToString().Replace("\\", "\\\\");
					text2 = text2.Replace("\"", "\\\"");
					text2 = text2.Replace("\n", "\\n");
					text2 = text2.Replace("\r", "\\r");
					text2 = text2.Replace("\0", "");
					text2 = text2.Replace("\u2028", "");
					text2 = text2.Replace("\u2029", "");
					text = "\"" + text2 + "\"";
				}
				else
				{
					bool flag3 = o is int || o is short || o is uint || o is ushort || o is byte;
					if (flag3)
					{
						text = o.ToString();
					}
					else
					{
						bool flag4 = o is float;
						if (flag4)
						{
							NumberFormatInfo numberFormat = CultureInfo.InvariantCulture.NumberFormat;
							text = ((float)o).ToString(numberFormat);
						}
						else
						{
							bool flag5 = o is double;
							if (flag5)
							{
								NumberFormatInfo numberFormat2 = CultureInfo.InvariantCulture.NumberFormat;
								text = ((double)o).ToString(numberFormat2);
							}
							else
							{
								bool flag6 = o is char;
								if (flag6)
								{
									bool flag7 = (char)o == '"';
									if (flag7)
									{
										text = "\"\\\"\"";
									}
									else
									{
										text = "\"" + o.ToString() + "\"";
									}
								}
								else
								{
									bool flag8 = o is IList;
									if (flag8)
									{
										IList list = (IList)o;
										StringBuilder stringBuilder = new StringBuilder();
										stringBuilder.Append("new Array(");
										int count = list.Count;
										for (int i = 0; i < count; i++)
										{
											bool flag9 = i != 0;
											if (flag9)
											{
												stringBuilder.Append(", ");
											}
											stringBuilder.Append(Application.ObjectToJSString(list[i]));
										}
										stringBuilder.Append(")");
										text = stringBuilder.ToString();
									}
									else
									{
										text = Application.ObjectToJSString(o.ToString());
									}
								}
							}
						}
					}
				}
			}
			return text;
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0000ABB6 File Offset: 0x00008DB6
		[Obsolete("Application.ExternalCall is deprecated. See https://docs.unity3d.com/Manual/webgl-interactingwithbrowserscripting.html for alternatives.")]
		public static void ExternalCall(string functionName, params object[] args)
		{
			Application.Internal_ExternalCall(Application.BuildInvocationForArguments(functionName, args));
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0000ABC8 File Offset: 0x00008DC8
		private static string BuildInvocationForArguments(string functionName, params object[] args)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(functionName);
			stringBuilder.Append('(');
			int num = args.Length;
			for (int i = 0; i < num; i++)
			{
				bool flag = i != 0;
				if (flag)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(Application.ObjectToJSString(args[i]));
			}
			stringBuilder.Append(')');
			stringBuilder.Append(';');
			return stringBuilder.ToString();
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x0000AC48 File Offset: 0x00008E48
		[Obsolete("use Application.isEditor instead")]
		public static bool isPlayer
		{
			get
			{
				return !Application.isEditor;
			}
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0000AC64 File Offset: 0x00008E64
		[Obsolete("Use Object.DontDestroyOnLoad instead")]
		public static void DontDestroyOnLoad(Object o)
		{
			bool flag = o != null;
			if (flag)
			{
				Object.DontDestroyOnLoad(o);
			}
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x0000AC84 File Offset: 0x00008E84
		[Obsolete("Application.CaptureScreenshot is obsolete. Use ScreenCapture.CaptureScreenshot instead (UnityUpgradable) -> [UnityEngine] UnityEngine.ScreenCapture.CaptureScreenshot(*)", true)]
		public static void CaptureScreenshot(string filename, int superSize)
		{
			throw new NotSupportedException("Application.CaptureScreenshot is obsolete. Use ScreenCapture.CaptureScreenshot instead.");
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0000AC84 File Offset: 0x00008E84
		[Obsolete("Application.CaptureScreenshot is obsolete. Use ScreenCapture.CaptureScreenshot instead (UnityUpgradable) -> [UnityEngine] UnityEngine.ScreenCapture.CaptureScreenshot(*)", true)]
		public static void CaptureScreenshot(string filename)
		{
			throw new NotSupportedException("Application.CaptureScreenshot is obsolete. Use ScreenCapture.CaptureScreenshot instead.");
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060005D1 RID: 1489 RVA: 0x0000AC91 File Offset: 0x00008E91
		// (remove) Token: 0x060005D2 RID: 1490 RVA: 0x0000AC9B File Offset: 0x00008E9B
		public static event UnityAction onBeforeRender
		{
			add
			{
				BeforeRenderHelper.RegisterCallback(value);
			}
			remove
			{
				BeforeRenderHelper.UnregisterCallback(value);
			}
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060005D3 RID: 1491 RVA: 0x0000ACA8 File Offset: 0x00008EA8
		// (remove) Token: 0x060005D4 RID: 1492 RVA: 0x0000ACDC File Offset: 0x00008EDC
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<bool> focusChanged;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060005D5 RID: 1493 RVA: 0x0000AD10 File Offset: 0x00008F10
		// (remove) Token: 0x060005D6 RID: 1494 RVA: 0x0000AD44 File Offset: 0x00008F44
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<string> deepLinkActivated;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060005D7 RID: 1495 RVA: 0x0000AD78 File Offset: 0x00008F78
		// (remove) Token: 0x060005D8 RID: 1496 RVA: 0x0000ADAC File Offset: 0x00008FAC
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Func<bool> wantsToQuit;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060005D9 RID: 1497 RVA: 0x0000ADE0 File Offset: 0x00008FE0
		// (remove) Token: 0x060005DA RID: 1498 RVA: 0x0000AE14 File Offset: 0x00009014
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action quitting;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060005DB RID: 1499 RVA: 0x0000AE48 File Offset: 0x00009048
		// (remove) Token: 0x060005DC RID: 1500 RVA: 0x0000AE7C File Offset: 0x0000907C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action unloading;

		// Token: 0x060005DD RID: 1501 RVA: 0x0000AEB0 File Offset: 0x000090B0
		[RequiredByNativeCode]
		private static bool Internal_ApplicationWantsToQuit()
		{
			bool flag = Application.wantsToQuit != null;
			if (flag)
			{
				foreach (Func<bool> func in Application.wantsToQuit.GetInvocationList())
				{
					try
					{
						bool flag2 = !func();
						if (flag2)
						{
							return false;
						}
					}
					catch (Exception ex)
					{
						Debug.LogException(ex);
					}
				}
			}
			return true;
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x0000AF30 File Offset: 0x00009130
		public static CancellationToken exitCancellationToken
		{
			get
			{
				return Application.s_currentCancellationTokenSource.Token;
			}
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0000AF3C File Offset: 0x0000913C
		[RequiredByNativeCode]
		private static void Internal_InitializeExitCancellationToken()
		{
			bool flag = Application.s_currentCancellationTokenSource == null || Application.s_currentCancellationTokenSource.IsCancellationRequested;
			if (flag)
			{
				Application.s_currentCancellationTokenSource = new CancellationTokenSource();
			}
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0000AF6C File Offset: 0x0000916C
		[RequiredByNativeCode]
		private static void Internal_RaiseExitCancellationToken()
		{
			CancellationTokenSource cancellationTokenSource = Application.s_currentCancellationTokenSource;
			if (cancellationTokenSource != null)
			{
				cancellationTokenSource.Cancel();
			}
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0000AF80 File Offset: 0x00009180
		[RequiredByNativeCode]
		private static void Internal_ApplicationQuit()
		{
			bool flag = Application.quitting != null;
			if (flag)
			{
				Application.quitting();
			}
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0000AFA8 File Offset: 0x000091A8
		[RequiredByNativeCode]
		private static void Internal_ApplicationUnload()
		{
			bool flag = Application.unloading != null;
			if (flag)
			{
				Application.unloading();
			}
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0000AFCD File Offset: 0x000091CD
		[RequiredByNativeCode]
		internal static void InvokeOnBeforeRender()
		{
			BeforeRenderHelper.Invoke();
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0000AFD8 File Offset: 0x000091D8
		[RequiredByNativeCode]
		internal static void InvokeFocusChanged(bool focus)
		{
			bool flag = Application.focusChanged != null;
			if (flag)
			{
				Application.focusChanged(focus);
			}
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0000B000 File Offset: 0x00009200
		[RequiredByNativeCode]
		internal static void InvokeDeepLinkActivated(string url)
		{
			bool flag = Application.deepLinkActivated != null;
			if (flag)
			{
				Application.deepLinkActivated(url);
			}
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0000B026 File Offset: 0x00009226
		[Obsolete("Application.RegisterLogCallback is deprecated. Use Application.logMessageReceived instead.")]
		public static void RegisterLogCallback(Application.LogCallback handler)
		{
			Application.RegisterLogCallback(handler, false);
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0000B031 File Offset: 0x00009231
		[Obsolete("Application.RegisterLogCallbackThreaded is deprecated. Use Application.logMessageReceivedThreaded instead.")]
		public static void RegisterLogCallbackThreaded(Application.LogCallback handler)
		{
			Application.RegisterLogCallback(handler, true);
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0000B03C File Offset: 0x0000923C
		private static void RegisterLogCallback(Application.LogCallback handler, bool threaded)
		{
			bool flag = Application.s_RegisterLogCallbackDeprecated != null;
			if (flag)
			{
				Application.logMessageReceived -= Application.s_RegisterLogCallbackDeprecated;
				Application.logMessageReceivedThreaded -= Application.s_RegisterLogCallbackDeprecated;
			}
			Application.s_RegisterLogCallbackDeprecated = handler;
			bool flag2 = handler != null;
			if (flag2)
			{
				if (threaded)
				{
					Application.logMessageReceivedThreaded += handler;
				}
				else
				{
					Application.logMessageReceived += handler;
				}
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x0000B09C File Offset: 0x0000929C
		[Obsolete("Use SceneManager.sceneCountInBuildSettings")]
		public static int levelCount
		{
			get
			{
				return SceneManager.sceneCountInBuildSettings;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x0000B0B4 File Offset: 0x000092B4
		[Obsolete("Use SceneManager to determine what scenes have been loaded")]
		public static int loadedLevel
		{
			get
			{
				return SceneManager.GetActiveScene().buildIndex;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x0000B0D4 File Offset: 0x000092D4
		[Obsolete("Use SceneManager to determine what scenes have been loaded")]
		public static string loadedLevelName
		{
			get
			{
				return SceneManager.GetActiveScene().name;
			}
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0000B0F3 File Offset: 0x000092F3
		[Obsolete("Use SceneManager.LoadScene")]
		public static void LoadLevel(int index)
		{
			SceneManager.LoadScene(index, LoadSceneMode.Single);
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0000B0FE File Offset: 0x000092FE
		[Obsolete("Use SceneManager.LoadScene")]
		public static void LoadLevel(string name)
		{
			SceneManager.LoadScene(name, LoadSceneMode.Single);
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0000B109 File Offset: 0x00009309
		[Obsolete("Use SceneManager.LoadScene")]
		public static void LoadLevelAdditive(int index)
		{
			SceneManager.LoadScene(index, LoadSceneMode.Additive);
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0000B114 File Offset: 0x00009314
		[Obsolete("Use SceneManager.LoadScene")]
		public static void LoadLevelAdditive(string name)
		{
			SceneManager.LoadScene(name, LoadSceneMode.Additive);
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0000B120 File Offset: 0x00009320
		[Obsolete("Use SceneManager.LoadSceneAsync")]
		public static AsyncOperation LoadLevelAsync(int index)
		{
			return SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0000B13C File Offset: 0x0000933C
		[Obsolete("Use SceneManager.LoadSceneAsync")]
		public static AsyncOperation LoadLevelAsync(string levelName)
		{
			return SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0000B158 File Offset: 0x00009358
		[Obsolete("Use SceneManager.LoadSceneAsync")]
		public static AsyncOperation LoadLevelAdditiveAsync(int index)
		{
			return SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0000B174 File Offset: 0x00009374
		[Obsolete("Use SceneManager.LoadSceneAsync")]
		public static AsyncOperation LoadLevelAdditiveAsync(string levelName)
		{
			return SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0000B190 File Offset: 0x00009390
		[Obsolete("Use SceneManager.UnloadScene")]
		public static bool UnloadLevel(int index)
		{
			return SceneManager.UnloadScene(index);
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0000B1A8 File Offset: 0x000093A8
		[Obsolete("Use SceneManager.UnloadScene")]
		public static bool UnloadLevel(string scenePath)
		{
			return SceneManager.UnloadScene(scenePath);
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x0000B1C0 File Offset: 0x000093C0
		public static bool isEditor
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060005F9 RID: 1529
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CanStreamedLevelBeLoaded_Injected(ref ManagedSpanWrapper levelName);

		// Token: 0x060005FA RID: 1530
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsPlaying_Injected(IntPtr obj);

		// Token: 0x060005FB RID: 1531
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_buildGUID_Injected(out ManagedSpanWrapper ret);

		// Token: 0x060005FC RID: 1532
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasARGV_Injected(ref ManagedSpanWrapper name);

		// Token: 0x060005FD RID: 1533
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetValueForARGV_Injected(ref ManagedSpanWrapper name, out ManagedSpanWrapper ret);

		// Token: 0x060005FE RID: 1534
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_dataPath_Injected(out ManagedSpanWrapper ret);

		// Token: 0x060005FF RID: 1535
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_streamingAssetsPath_Injected(out ManagedSpanWrapper ret);

		// Token: 0x06000600 RID: 1536
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_persistentDataPath_Injected(out ManagedSpanWrapper ret);

		// Token: 0x06000601 RID: 1537
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_temporaryCachePath_Injected(out ManagedSpanWrapper ret);

		// Token: 0x06000602 RID: 1538
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_absoluteURL_Injected(out ManagedSpanWrapper ret);

		// Token: 0x06000603 RID: 1539
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_ExternalCall_Injected(ref ManagedSpanWrapper script);

		// Token: 0x06000604 RID: 1540
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_unityVersion_Injected(out ManagedSpanWrapper ret);

		// Token: 0x06000605 RID: 1541
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_version_Injected(out ManagedSpanWrapper ret);

		// Token: 0x06000606 RID: 1542
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_installerName_Injected(out ManagedSpanWrapper ret);

		// Token: 0x06000607 RID: 1543
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_identifier_Injected(out ManagedSpanWrapper ret);

		// Token: 0x06000608 RID: 1544
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_productName_Injected(out ManagedSpanWrapper ret);

		// Token: 0x06000609 RID: 1545
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_companyName_Injected(out ManagedSpanWrapper ret);

		// Token: 0x0600060A RID: 1546
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_cloudProjectId_Injected(out ManagedSpanWrapper ret);

		// Token: 0x0600060B RID: 1547
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void OpenURL_Injected(ref ManagedSpanWrapper url);

		// Token: 0x0600060C RID: 1548
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_consoleLogPath_Injected(out ManagedSpanWrapper ret);

		// Token: 0x0600060D RID: 1549
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr RequestUserAuthorization_Injected(UserAuthorization mode);

		// Token: 0x04000301 RID: 769
		private static Application.LogCallback s_LogCallbackHandler;

		// Token: 0x04000302 RID: 770
		private static Application.LogCallback s_LogCallbackHandlerThreaded;

		// Token: 0x04000303 RID: 771
		internal static Application.AdvertisingIdentifierCallback OnAdvertisingIdentifierCallback;

		// Token: 0x04000309 RID: 777
		private static CancellationTokenSource s_currentCancellationTokenSource = new CancellationTokenSource();

		// Token: 0x0400030A RID: 778
		private static volatile Application.LogCallback s_RegisterLogCallbackDeprecated;

		// Token: 0x02000102 RID: 258
		// (Invoke) Token: 0x0600060F RID: 1551
		public delegate void AdvertisingIdentifierCallback(string advertisingId, bool trackingEnabled, string errorMsg);

		// Token: 0x02000103 RID: 259
		// (Invoke) Token: 0x06000613 RID: 1555
		public delegate void LowMemoryCallback();

		// Token: 0x02000104 RID: 260
		// (Invoke) Token: 0x06000617 RID: 1559
		public delegate void MemoryUsageChangedCallback(in ApplicationMemoryUsageChange usage);

		// Token: 0x02000105 RID: 261
		// (Invoke) Token: 0x0600061B RID: 1563
		public delegate void LogCallback(string condition, string stackTrace, LogType type);
	}
}
