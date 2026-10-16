using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Trivial.CodeSecurity;
using UnityEngine;

namespace RoslynCSharp
{
	// Token: 0x02000006 RID: 6
	public sealed class RoslynCSharp : ScriptableObject
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000263C File Offset: 0x0000083C
		public static RoslynCSharp Settings
		{
			get
			{
				if (RoslynCSharp.settings == null)
				{
					RoslynCSharp.LoadResources();
				}
				return RoslynCSharp.settings;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002655 File Offset: 0x00000855
		// (set) Token: 0x06000019 RID: 25 RVA: 0x0000265D File Offset: 0x0000085D
		public bool SecurityCheckCode
		{
			get
			{
				return this.securityCheckCode;
			}
			set
			{
				this.securityCheckCode = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002666 File Offset: 0x00000866
		public CodeSecurityRestrictions SecurityRestrictions
		{
			get
			{
				return this.securityRestrictions;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000266E File Offset: 0x0000086E
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002676 File Offset: 0x00000876
		public RoslynCSharp.LogDetail LogLevel
		{
			get
			{
				return this.logDetail;
			}
			set
			{
				this.logDetail = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001D RID: 29 RVA: 0x0000267F File Offset: 0x0000087F
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002687 File Offset: 0x00000887
		public bool AllowUnsafeCode
		{
			get
			{
				return this.allowUnsafeCode;
			}
			set
			{
				this.allowUnsafeCode = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002690 File Offset: 0x00000890
		// (set) Token: 0x06000020 RID: 32 RVA: 0x00002698 File Offset: 0x00000898
		public bool AllowOptimizeCode
		{
			get
			{
				return this.allowOptimizeCode;
			}
			set
			{
				this.allowOptimizeCode = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000026A1 File Offset: 0x000008A1
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000026A9 File Offset: 0x000008A9
		public bool AllowConcurrentCompile
		{
			get
			{
				return this.allowConcurrentCompile;
			}
			set
			{
				this.allowConcurrentCompile = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000026B2 File Offset: 0x000008B2
		// (set) Token: 0x06000024 RID: 36 RVA: 0x000026BA File Offset: 0x000008BA
		public bool GenerateInMemory
		{
			get
			{
				return this.generateInMemory;
			}
			set
			{
				this.generateInMemory = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000026C3 File Offset: 0x000008C3
		// (set) Token: 0x06000026 RID: 38 RVA: 0x000026CB File Offset: 0x000008CB
		public bool GenerateSymbols
		{
			get
			{
				return this.generateSymbols;
			}
			set
			{
				this.generateSymbols = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000026D4 File Offset: 0x000008D4
		// (set) Token: 0x06000028 RID: 40 RVA: 0x000026DC File Offset: 0x000008DC
		public int WarningLevel
		{
			get
			{
				return this.warningLevel;
			}
			set
			{
				this.warningLevel = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000026E5 File Offset: 0x000008E5
		// (set) Token: 0x0600002A RID: 42 RVA: 0x000026ED File Offset: 0x000008ED
		public LanguageVersion LanguageVersion
		{
			get
			{
				return this.languageVersion;
			}
			set
			{
				this.languageVersion = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000026F6 File Offset: 0x000008F6
		// (set) Token: 0x0600002C RID: 44 RVA: 0x000026FE File Offset: 0x000008FE
		public Platform TargetPlatform
		{
			get
			{
				return this.targetPlatform;
			}
			set
			{
				this.targetPlatform = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002707 File Offset: 0x00000907
		public IList<string> References
		{
			get
			{
				return this.refeferences;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600002E RID: 46 RVA: 0x0000270F File Offset: 0x0000090F
		public IList<string> DefineSymbols
		{
			get
			{
				return this.defineSymbols;
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002717 File Offset: 0x00000917
		public static void LoadResources()
		{
			RoslynCSharp.settings = Resources.Load<RoslynCSharp>("RoslynCSharpSettings");
			if (RoslynCSharp.settings == null)
			{
				RoslynCSharp.settings = ScriptableObject.CreateInstance<RoslynCSharp>();
				global::UnityEngine.Debug.LogWarningFormat("Failed to load settings asset '{0}' from resources. Default values will be used", new object[] { "RoslynCSharpSettings" });
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002757 File Offset: 0x00000957
		[Conditional("DEBUG")]
		public static void Log(string format, params object[] args)
		{
			if (RoslynCSharp.Settings.LogLevel >= RoslynCSharp.LogDetail.Info)
			{
				if (args.Length == 0)
				{
					global::UnityEngine.Debug.Log(format);
					return;
				}
				global::UnityEngine.Debug.LogFormat(format, args);
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002778 File Offset: 0x00000978
		[Conditional("DEBUG")]
		public static void LogWarning(string format, params object[] args)
		{
			if (RoslynCSharp.settings.LogLevel >= RoslynCSharp.LogDetail.Warnings)
			{
				if (args.Length == 0)
				{
					global::UnityEngine.Debug.LogWarning(format);
					return;
				}
				global::UnityEngine.Debug.LogWarningFormat(format, args);
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002799 File Offset: 0x00000999
		[Conditional("DEBUG")]
		public static void LogError(string format, params object[] args)
		{
			if (RoslynCSharp.settings.LogLevel >= RoslynCSharp.LogDetail.Errors)
			{
				if (args.Length == 0)
				{
					global::UnityEngine.Debug.LogError(format);
					return;
				}
				global::UnityEngine.Debug.LogErrorFormat(format, args);
			}
		}

		// Token: 0x04000016 RID: 22
		private static RoslynCSharp settings;

		// Token: 0x04000017 RID: 23
		[SerializeField]
		[HideInInspector]
		private bool securityCheckCode = true;

		// Token: 0x04000018 RID: 24
		[SerializeField]
		[HideInInspector]
		private CodeSecurityRestrictions securityRestrictions = new CodeSecurityRestrictions();

		// Token: 0x04000019 RID: 25
		[SerializeField]
		[HideInInspector]
		private RoslynCSharp.LogDetail logDetail = RoslynCSharp.LogDetail.Errors;

		// Token: 0x0400001A RID: 26
		[SerializeField]
		[HideInInspector]
		private bool allowUnsafeCode;

		// Token: 0x0400001B RID: 27
		[SerializeField]
		[HideInInspector]
		private bool allowOptimizeCode = true;

		// Token: 0x0400001C RID: 28
		[SerializeField]
		[HideInInspector]
		private bool allowConcurrentCompile = true;

		// Token: 0x0400001D RID: 29
		[SerializeField]
		[HideInInspector]
		private bool generateInMemory = true;

		// Token: 0x0400001E RID: 30
		[SerializeField]
		[HideInInspector]
		private bool generateSymbols = true;

		// Token: 0x0400001F RID: 31
		[SerializeField]
		[HideInInspector]
		private int warningLevel = 4;

		// Token: 0x04000020 RID: 32
		[SerializeField]
		[HideInInspector]
		private LanguageVersion languageVersion;

		// Token: 0x04000021 RID: 33
		[SerializeField]
		[HideInInspector]
		private Platform targetPlatform;

		// Token: 0x04000022 RID: 34
		[SerializeField]
		[HideInInspector]
		private List<string> refeferences = new List<string>();

		// Token: 0x04000023 RID: 35
		[SerializeField]
		[HideInInspector]
		private List<string> defineSymbols = new List<string>();

		// Token: 0x04000024 RID: 36
		public const string settingsName = "RoslynCSharpSettings";

		// Token: 0x02000014 RID: 20
		public enum LogDetail
		{
			// Token: 0x0400006C RID: 108
			None,
			// Token: 0x0400006D RID: 109
			Errors,
			// Token: 0x0400006E RID: 110
			Warnings,
			// Token: 0x0400006F RID: 111
			Info
		}
	}
}
