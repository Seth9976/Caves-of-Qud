using System;
using System.Threading;
using UnityEngine.Events;

namespace UnityEngine.Device
{
	// Token: 0x02000462 RID: 1122
	public static class Application
	{
		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06002B71 RID: 11121 RVA: 0x000530F8 File Offset: 0x000512F8
		public static string absoluteURL
		{
			get
			{
				return Application.absoluteURL;
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06002B72 RID: 11122 RVA: 0x000530FF File Offset: 0x000512FF
		// (set) Token: 0x06002B73 RID: 11123 RVA: 0x00053106 File Offset: 0x00051306
		public static ThreadPriority backgroundLoadingPriority
		{
			get
			{
				return Application.backgroundLoadingPriority;
			}
			set
			{
				Application.backgroundLoadingPriority = value;
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06002B74 RID: 11124 RVA: 0x0005310F File Offset: 0x0005130F
		public static string buildGUID
		{
			get
			{
				return Application.buildGUID;
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06002B75 RID: 11125 RVA: 0x00053116 File Offset: 0x00051316
		public static string cloudProjectId
		{
			get
			{
				return Application.cloudProjectId;
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06002B76 RID: 11126 RVA: 0x0005311D File Offset: 0x0005131D
		public static string companyName
		{
			get
			{
				return Application.companyName;
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06002B77 RID: 11127 RVA: 0x00053124 File Offset: 0x00051324
		public static string consoleLogPath
		{
			get
			{
				return Application.consoleLogPath;
			}
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06002B78 RID: 11128 RVA: 0x0005312B File Offset: 0x0005132B
		public static string dataPath
		{
			get
			{
				return Application.dataPath;
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06002B79 RID: 11129 RVA: 0x00053132 File Offset: 0x00051332
		public static bool genuine
		{
			get
			{
				return Application.genuine;
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06002B7A RID: 11130 RVA: 0x00053139 File Offset: 0x00051339
		public static bool genuineCheckAvailable
		{
			get
			{
				return Application.genuineCheckAvailable;
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06002B7B RID: 11131 RVA: 0x00053140 File Offset: 0x00051340
		public static string identifier
		{
			get
			{
				return Application.identifier;
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06002B7C RID: 11132 RVA: 0x00053147 File Offset: 0x00051347
		public static string installerName
		{
			get
			{
				return Application.installerName;
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06002B7D RID: 11133 RVA: 0x0005314E File Offset: 0x0005134E
		public static ApplicationInstallMode installMode
		{
			get
			{
				return Application.installMode;
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06002B7E RID: 11134 RVA: 0x00053155 File Offset: 0x00051355
		public static NetworkReachability internetReachability
		{
			get
			{
				return Application.internetReachability;
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06002B7F RID: 11135 RVA: 0x0005315C File Offset: 0x0005135C
		public static bool isBatchMode
		{
			get
			{
				return Application.isBatchMode;
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06002B80 RID: 11136 RVA: 0x00053163 File Offset: 0x00051363
		public static bool isConsolePlatform
		{
			get
			{
				return Application.isConsolePlatform;
			}
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06002B81 RID: 11137 RVA: 0x0005316A File Offset: 0x0005136A
		public static bool isEditor
		{
			get
			{
				return Application.isEditor;
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06002B82 RID: 11138 RVA: 0x00053171 File Offset: 0x00051371
		public static bool isFocused
		{
			get
			{
				return Application.isFocused;
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06002B83 RID: 11139 RVA: 0x00053178 File Offset: 0x00051378
		public static bool isMobilePlatform
		{
			get
			{
				return Application.isMobilePlatform;
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06002B84 RID: 11140 RVA: 0x0005317F File Offset: 0x0005137F
		public static bool isPlaying
		{
			get
			{
				return Application.isPlaying;
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06002B85 RID: 11141 RVA: 0x00053186 File Offset: 0x00051386
		public static string persistentDataPath
		{
			get
			{
				return Application.persistentDataPath;
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06002B86 RID: 11142 RVA: 0x0005318D File Offset: 0x0005138D
		public static RuntimePlatform platform
		{
			get
			{
				return Application.platform;
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06002B87 RID: 11143 RVA: 0x00053194 File Offset: 0x00051394
		public static string productName
		{
			get
			{
				return Application.productName;
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06002B88 RID: 11144 RVA: 0x0005319B File Offset: 0x0005139B
		// (set) Token: 0x06002B89 RID: 11145 RVA: 0x000531A2 File Offset: 0x000513A2
		public static bool runInBackground
		{
			get
			{
				return Application.runInBackground;
			}
			set
			{
				Application.runInBackground = value;
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06002B8A RID: 11146 RVA: 0x000531AB File Offset: 0x000513AB
		public static ApplicationSandboxType sandboxType
		{
			get
			{
				return Application.sandboxType;
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06002B8B RID: 11147 RVA: 0x000531B2 File Offset: 0x000513B2
		public static string streamingAssetsPath
		{
			get
			{
				return Application.streamingAssetsPath;
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06002B8C RID: 11148 RVA: 0x000531B9 File Offset: 0x000513B9
		public static SystemLanguage systemLanguage
		{
			get
			{
				return Application.systemLanguage;
			}
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06002B8D RID: 11149 RVA: 0x000531C0 File Offset: 0x000513C0
		// (set) Token: 0x06002B8E RID: 11150 RVA: 0x000531C7 File Offset: 0x000513C7
		public static int targetFrameRate
		{
			get
			{
				return Application.targetFrameRate;
			}
			set
			{
				Application.targetFrameRate = value;
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06002B8F RID: 11151 RVA: 0x000531D0 File Offset: 0x000513D0
		public static string temporaryCachePath
		{
			get
			{
				return Application.temporaryCachePath;
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06002B90 RID: 11152 RVA: 0x000531D7 File Offset: 0x000513D7
		public static string unityVersion
		{
			get
			{
				return Application.unityVersion;
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06002B91 RID: 11153 RVA: 0x000531DE File Offset: 0x000513DE
		public static string version
		{
			get
			{
				return Application.version;
			}
		}

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x06002B92 RID: 11154 RVA: 0x000531E5 File Offset: 0x000513E5
		// (remove) Token: 0x06002B93 RID: 11155 RVA: 0x000531EE File Offset: 0x000513EE
		public static event Action<string> deepLinkActivated
		{
			add
			{
				Application.deepLinkActivated += value;
			}
			remove
			{
				Application.deepLinkActivated -= value;
			}
		}

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x06002B94 RID: 11156 RVA: 0x000531F7 File Offset: 0x000513F7
		// (remove) Token: 0x06002B95 RID: 11157 RVA: 0x00053200 File Offset: 0x00051400
		public static event Action<bool> focusChanged
		{
			add
			{
				Application.focusChanged += value;
			}
			remove
			{
				Application.focusChanged -= value;
			}
		}

		// Token: 0x1400002F RID: 47
		// (add) Token: 0x06002B96 RID: 11158 RVA: 0x00053209 File Offset: 0x00051409
		// (remove) Token: 0x06002B97 RID: 11159 RVA: 0x00053212 File Offset: 0x00051412
		public static event Application.LogCallback logMessageReceived
		{
			add
			{
				Application.logMessageReceived += value;
			}
			remove
			{
				Application.logMessageReceived -= value;
			}
		}

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x06002B98 RID: 11160 RVA: 0x0005321B File Offset: 0x0005141B
		// (remove) Token: 0x06002B99 RID: 11161 RVA: 0x00053224 File Offset: 0x00051424
		public static event Application.LogCallback logMessageReceivedThreaded
		{
			add
			{
				Application.logMessageReceivedThreaded += value;
			}
			remove
			{
				Application.logMessageReceivedThreaded -= value;
			}
		}

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x06002B9A RID: 11162 RVA: 0x0005322D File Offset: 0x0005142D
		// (remove) Token: 0x06002B9B RID: 11163 RVA: 0x00053236 File Offset: 0x00051436
		public static event Application.LowMemoryCallback lowMemory
		{
			add
			{
				Application.lowMemory += value;
			}
			remove
			{
				Application.lowMemory -= value;
			}
		}

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x06002B9C RID: 11164 RVA: 0x0005323F File Offset: 0x0005143F
		// (remove) Token: 0x06002B9D RID: 11165 RVA: 0x00053248 File Offset: 0x00051448
		public static event Application.MemoryUsageChangedCallback memoryUsageChanged
		{
			add
			{
				Application.memoryUsageChanged += value;
			}
			remove
			{
				Application.memoryUsageChanged -= value;
			}
		}

		// Token: 0x14000033 RID: 51
		// (add) Token: 0x06002B9E RID: 11166 RVA: 0x00053251 File Offset: 0x00051451
		// (remove) Token: 0x06002B9F RID: 11167 RVA: 0x0005325A File Offset: 0x0005145A
		public static event UnityAction onBeforeRender
		{
			add
			{
				Application.onBeforeRender += value;
			}
			remove
			{
				Application.onBeforeRender -= value;
			}
		}

		// Token: 0x14000034 RID: 52
		// (add) Token: 0x06002BA0 RID: 11168 RVA: 0x00053263 File Offset: 0x00051463
		// (remove) Token: 0x06002BA1 RID: 11169 RVA: 0x0005326C File Offset: 0x0005146C
		public static event Action quitting
		{
			add
			{
				Application.quitting += value;
			}
			remove
			{
				Application.quitting -= value;
			}
		}

		// Token: 0x14000035 RID: 53
		// (add) Token: 0x06002BA2 RID: 11170 RVA: 0x00053275 File Offset: 0x00051475
		// (remove) Token: 0x06002BA3 RID: 11171 RVA: 0x0005327E File Offset: 0x0005147E
		public static event Func<bool> wantsToQuit
		{
			add
			{
				Application.wantsToQuit += value;
			}
			remove
			{
				Application.wantsToQuit -= value;
			}
		}

		// Token: 0x14000036 RID: 54
		// (add) Token: 0x06002BA4 RID: 11172 RVA: 0x00053287 File Offset: 0x00051487
		// (remove) Token: 0x06002BA5 RID: 11173 RVA: 0x00053290 File Offset: 0x00051490
		public static event Action unloading
		{
			add
			{
				Application.unloading += value;
			}
			remove
			{
				Application.unloading -= value;
			}
		}

		// Token: 0x06002BA6 RID: 11174 RVA: 0x0005329C File Offset: 0x0005149C
		public static bool CanStreamedLevelBeLoaded(int levelIndex)
		{
			return Application.CanStreamedLevelBeLoaded(levelIndex);
		}

		// Token: 0x06002BA7 RID: 11175 RVA: 0x000532B4 File Offset: 0x000514B4
		public static bool CanStreamedLevelBeLoaded(string levelName)
		{
			return Application.CanStreamedLevelBeLoaded(levelName);
		}

		// Token: 0x06002BA8 RID: 11176 RVA: 0x000532CC File Offset: 0x000514CC
		[Obsolete("Application.GetBuildTags is no longer supported and will be removed.", false)]
		public static string[] GetBuildTags()
		{
			return Application.GetBuildTags();
		}

		// Token: 0x06002BA9 RID: 11177 RVA: 0x000532E3 File Offset: 0x000514E3
		[Obsolete("Application.SetBuildTags is no longer supported and will be removed.", false)]
		public static void SetBuildTags(string[] buildTags)
		{
			Application.SetBuildTags(buildTags);
		}

		// Token: 0x06002BAA RID: 11178 RVA: 0x000532F0 File Offset: 0x000514F0
		public static StackTraceLogType GetStackTraceLogType(LogType logType)
		{
			return Application.GetStackTraceLogType(logType);
		}

		// Token: 0x06002BAB RID: 11179 RVA: 0x00053308 File Offset: 0x00051508
		public static bool HasProLicense()
		{
			return Application.HasProLicense();
		}

		// Token: 0x06002BAC RID: 11180 RVA: 0x00053320 File Offset: 0x00051520
		public static bool HasUserAuthorization(UserAuthorization mode)
		{
			return Application.HasUserAuthorization(mode);
		}

		// Token: 0x06002BAD RID: 11181 RVA: 0x00053338 File Offset: 0x00051538
		public static bool IsPlaying(Object obj)
		{
			return Application.IsPlaying(obj);
		}

		// Token: 0x06002BAE RID: 11182 RVA: 0x00053350 File Offset: 0x00051550
		public static void OpenURL(string url)
		{
			Application.OpenURL(url);
		}

		// Token: 0x06002BAF RID: 11183 RVA: 0x0005335A File Offset: 0x0005155A
		public static void Quit()
		{
			Application.Quit();
		}

		// Token: 0x06002BB0 RID: 11184 RVA: 0x00053363 File Offset: 0x00051563
		public static void Quit(int exitCode)
		{
			Application.Quit(exitCode);
		}

		// Token: 0x06002BB1 RID: 11185 RVA: 0x00053370 File Offset: 0x00051570
		public static bool RequestAdvertisingIdentifierAsync(Application.AdvertisingIdentifierCallback delegateMethod)
		{
			return Application.RequestAdvertisingIdentifierAsync(delegateMethod);
		}

		// Token: 0x06002BB2 RID: 11186 RVA: 0x00053388 File Offset: 0x00051588
		public static AsyncOperation RequestUserAuthorization(UserAuthorization mode)
		{
			return Application.RequestUserAuthorization(mode);
		}

		// Token: 0x06002BB3 RID: 11187 RVA: 0x000533A0 File Offset: 0x000515A0
		public static void SetStackTraceLogType(LogType logType, StackTraceLogType stackTraceType)
		{
			Application.SetStackTraceLogType(logType, stackTraceType);
		}

		// Token: 0x06002BB4 RID: 11188 RVA: 0x000533AB File Offset: 0x000515AB
		public static void Unload()
		{
			Application.Unload();
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06002BB5 RID: 11189 RVA: 0x000533B4 File Offset: 0x000515B4
		public static CancellationToken exitCancellationToken
		{
			get
			{
				return Application.exitCancellationToken;
			}
		}
	}
}
