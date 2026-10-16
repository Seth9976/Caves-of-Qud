using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x02000071 RID: 113
	internal class ExceptionPageTemplateFragment
	{
		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x00008EC9 File Offset: 0x000070C9
		// (set) Token: 0x0600045E RID: 1118 RVA: 0x00008ED1 File Offset: 0x000070D1
		public string Name { get; set; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x00008EDA File Offset: 0x000070DA
		// (set) Token: 0x06000460 RID: 1120 RVA: 0x00008EE2 File Offset: 0x000070E2
		public string FilePath { get; set; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x00008EEB File Offset: 0x000070EB
		// (set) Token: 0x06000462 RID: 1122 RVA: 0x00008EF3 File Offset: 0x000070F3
		public string ResourceName { get; set; }

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x00008EFC File Offset: 0x000070FC
		// (set) Token: 0x06000464 RID: 1124 RVA: 0x00008F04 File Offset: 0x00007104
		public string ResourceAssembly { get; set; }

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x00008F0D File Offset: 0x0000710D
		// (set) Token: 0x06000466 RID: 1126 RVA: 0x00008F15 File Offset: 0x00007115
		public List<string> MacroNames { get; set; }

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x00008F1E File Offset: 0x0000711E
		// (set) Token: 0x06000468 RID: 1128 RVA: 0x00008F26 File Offset: 0x00007126
		public List<string> RequiredMacros { get; set; }

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x00008F2F File Offset: 0x0000712F
		// (set) Token: 0x0600046A RID: 1130 RVA: 0x00008F37 File Offset: 0x00007137
		public string Value { get; set; }

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x00008F40 File Offset: 0x00007140
		// (set) Token: 0x0600046C RID: 1132 RVA: 0x00008F48 File Offset: 0x00007148
		public ExceptionPageTemplateType ValidForPageType { get; set; }

		// Token: 0x0600046D RID: 1133 RVA: 0x00008F51 File Offset: 0x00007151
		public ExceptionPageTemplateFragment()
		{
			this.ValidForPageType = ExceptionPageTemplateType.Any;
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00008F64 File Offset: 0x00007164
		public virtual void Init(ExceptionPageTemplateValues values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			string text = this.Value;
			if (text != null)
			{
				values.Add(this.Name, text);
				return;
			}
			text = this.FilePath;
			if (!string.IsNullOrEmpty(text))
			{
				values.Add(this.Name, this.LoadFile(text));
				return;
			}
			text = this.ResourceName;
			if (!string.IsNullOrEmpty(text))
			{
				values.Add(this.Name, this.LoadResource(text));
				return;
			}
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00008FE0 File Offset: 0x000071E0
		public virtual bool Visible(ExceptionPageTemplateValues values)
		{
			List<string> requiredMacros = this.RequiredMacros;
			if (requiredMacros == null || requiredMacros.Count == 0)
			{
				return true;
			}
			if (values == null || values.Count == 0)
			{
				return false;
			}
			foreach (string text in requiredMacros)
			{
				if (values.Get(text) == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00009058 File Offset: 0x00007258
		public string ReplaceMacros(string value, ExceptionPageTemplateValues values)
		{
			if (string.IsNullOrEmpty(value))
			{
				return value;
			}
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			List<string> macroNames = this.MacroNames;
			if (macroNames == null || macroNames.Count == 0)
			{
				return value;
			}
			StringBuilder stringBuilder = new StringBuilder(value);
			foreach (string text in macroNames)
			{
				if (!string.IsNullOrEmpty(text))
				{
					string text2 = values.Get(text);
					if (text2 == null)
					{
						text2 = string.Empty;
					}
					stringBuilder.Replace("@" + text + "@", text2);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00009110 File Offset: 0x00007310
		protected virtual string LoadFile(string path)
		{
			if (!File.Exists(path))
			{
				Console.Error.WriteLine("File '{0}' not found. Required for exception template.", path);
				return string.Empty;
			}
			string text;
			try
			{
				text = File.ReadAllText(path);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine("Error reading file '{0}'. Required for exception template. Exception {1} has been thrown: {2}", path, ex.GetType(), ex.Message);
				if (RuntimeHelpers.DebuggingEnabled)
				{
					Console.Error.WriteLine(ex.StackTrace);
				}
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00009194 File Offset: 0x00007394
		protected virtual string LoadResource(string resourceName)
		{
			string resourceAssembly = this.ResourceAssembly;
			Assembly assembly;
			if (string.IsNullOrEmpty(resourceAssembly))
			{
				assembly = base.GetType().Assembly;
			}
			else
			{
				try
				{
					assembly = Assembly.Load(resourceAssembly);
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine("Unable to load assembly '{0}' needed to retrieve an exception template resource '{1}'. Exception {2} has been thrown: {3}", new object[]
					{
						resourceAssembly,
						resourceName,
						ex.GetType(),
						ex.Message
					});
					if (RuntimeHelpers.DebuggingEnabled)
					{
						Console.Error.WriteLine(ex.StackTrace);
					}
					return string.Empty;
				}
			}
			string text;
			try
			{
				Stream manifestResourceStream = assembly.GetManifestResourceStream(resourceName);
				if (manifestResourceStream == null)
				{
					Console.Error.WriteLine("Manifest resource '{0}' required for exception template not found in assembly '{1}'.", resourceName, resourceAssembly);
					text = string.Empty;
				}
				else
				{
					using (StreamReader streamReader = new StreamReader(manifestResourceStream))
					{
						text = streamReader.ReadToEnd();
					}
				}
			}
			catch (Exception ex2)
			{
				Console.Error.WriteLine("Error reading manifest resource '{0}' from assembly '{1}', required for exception template. Exception {2} has been thrown: {3}", new object[]
				{
					resourceName,
					resourceAssembly,
					ex2.GetType(),
					ex2.Message
				});
				if (RuntimeHelpers.DebuggingEnabled)
				{
					Console.Error.WriteLine(ex2.StackTrace);
				}
				text = string.Empty;
			}
			return text;
		}
	}
}
