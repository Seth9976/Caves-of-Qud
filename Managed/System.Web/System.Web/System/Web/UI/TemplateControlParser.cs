using System;
using System.Collections;
using System.IO;
using System.Security.Permissions;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web.UI
{
	/// <summary>Implements ASP.NET template parsing for template controls.</summary>
	// Token: 0x02000231 RID: 561
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class TemplateControlParser : BaseTemplateParser
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.TemplateControlParser" /> class.</summary>
		// Token: 0x060016F9 RID: 5881 RVA: 0x0003B9E9 File Offset: 0x00039BE9
		protected TemplateControlParser()
		{
			this.LoadConfigDefaults();
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x0003BA0C File Offset: 0x00039C0C
		internal override void LoadConfigDefaults()
		{
			base.LoadConfigDefaults();
			PagesSection pagesConfig = base.PagesConfig;
			this.autoEventWireup = pagesConfig.AutoEventWireup;
			this.enableViewState = pagesConfig.EnableViewState;
			this.compilationMode = pagesConfig.CompilationMode;
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x0003BA4C File Offset: 0x00039C4C
		internal override void ProcessMainAttributes(IDictionary atts)
		{
			this.autoEventWireup = base.GetBool(atts, "AutoEventWireup", this.autoEventWireup);
			this.enableViewState = base.GetBool(atts, "EnableViewState", this.enableViewState);
			string text = BaseParser.GetString(atts, "CompilationMode", this.compilationMode.ToString());
			if (!string.IsNullOrEmpty(text))
			{
				try
				{
					this.compilationMode = (CompilationMode)Enum.Parse(typeof(CompilationMode), text, true);
				}
				catch (Exception ex)
				{
					base.ThrowParseException("Invalid value of the CompilationMode attribute.", ex, Array.Empty<object>());
				}
			}
			atts.Remove("TargetSchema");
			text = BaseParser.GetString(atts, "ClientIDMode", null);
			if (!string.IsNullOrEmpty(text))
			{
				try
				{
					this.clientIDMode = new ClientIDMode?((ClientIDMode)Enum.Parse(typeof(ClientIDMode), text, true));
				}
				catch (Exception ex2)
				{
					base.ThrowParseException("Invalid value of the ClientIDMode attribute.", ex2, Array.Empty<object>());
				}
			}
			base.ProcessMainAttributes(atts);
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x0003BB5C File Offset: 0x00039D5C
		internal object GetCompiledInstance()
		{
			Type type = this.CompileIntoType();
			if (type == null)
			{
				return null;
			}
			object obj = Activator.CreateInstance(type);
			if (obj == null)
			{
				return null;
			}
			this.HandleOptions(obj);
			return obj;
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x0003BB90 File Offset: 0x00039D90
		internal override void AddDirective(string directive, IDictionary atts)
		{
			if (string.Compare("Register", directive, true, Helpers.InvariantCulture) == 0)
			{
				string @string = BaseParser.GetString(atts, "TagPrefix", null);
				if (@string == null || @string.Trim() == "")
				{
					base.ThrowParseException("No TagPrefix attribute found.", Array.Empty<object>());
				}
				string string2 = BaseParser.GetString(atts, "Namespace", null);
				string string3 = BaseParser.GetString(atts, "Assembly", null);
				if (string2 == null && string3 != null)
				{
					base.ThrowParseException("Need a Namespace attribute with Assembly.", Array.Empty<object>());
				}
				if (string2 != null)
				{
					if (atts.Count != 0)
					{
						base.ThrowParseException("Unknown attribute: " + TemplateParser.GetOneKey(atts), Array.Empty<object>());
					}
					base.RegisterNamespace(@string, string2, string3);
					return;
				}
				string string4 = BaseParser.GetString(atts, "TagName", null);
				string string5 = BaseParser.GetString(atts, "Src", null);
				if (string4 == null && string5 != null)
				{
					base.ThrowParseException("Need a TagName attribute with Src.", Array.Empty<object>());
				}
				if (string4 != null && string5 == null)
				{
					base.ThrowParseException("Need a Src attribute with TagName.", Array.Empty<object>());
				}
				base.RegisterCustomControl(@string, string4, string5);
				return;
			}
			else
			{
				if (string.Compare("Reference", directive, true, Helpers.InvariantCulture) == 0)
				{
					string text = null;
					string string6 = BaseParser.GetString(atts, "Page", null);
					bool flag = string6 != null;
					if (flag)
					{
						text = string6;
					}
					bool flag2 = false;
					string string7 = BaseParser.GetString(atts, "Control", null);
					if (string7 != null)
					{
						if (flag)
						{
							flag2 = true;
						}
						else
						{
							text = string7;
						}
					}
					string string8 = BaseParser.GetString(atts, "VirtualPath", null);
					if (string8 != null)
					{
						if (text != null)
						{
							flag2 = true;
						}
						else
						{
							text = string8;
						}
					}
					if (text == null)
					{
						base.ThrowParseException("Must provide one of the 'page', 'control' or 'virtualPath' attributes", Array.Empty<object>());
					}
					if (flag2)
					{
						base.ThrowParseException("Only one attribute can be specified.", Array.Empty<object>());
					}
					text = HostingEnvironment.VirtualPathProvider.CombineVirtualPaths(base.VirtualPath.Absolute, text);
					this.AddDependency(text, false);
					Type compiledType = BuildManager.GetCompiledType(text);
					this.AddAssembly(compiledType.Assembly, true);
					if (atts.Count != 0)
					{
						base.ThrowParseException("Unknown attribute: " + TemplateParser.GetOneKey(atts), Array.Empty<object>());
					}
					return;
				}
				base.AddDirective(directive, atts);
				return;
			}
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x0003BD9D File Offset: 0x00039F9D
		internal override void HandleOptions(object obj)
		{
			base.HandleOptions(obj);
			Control control = obj as Control;
			control.AutoEventWireup = this.autoEventWireup;
			control.EnableViewState = this.enableViewState;
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x060016FF RID: 5887 RVA: 0x0003BDC3 File Offset: 0x00039FC3
		internal bool AutoEventWireup
		{
			get
			{
				return this.autoEventWireup;
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06001700 RID: 5888 RVA: 0x0003BDCB File Offset: 0x00039FCB
		internal bool EnableViewState
		{
			get
			{
				return this.enableViewState;
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06001701 RID: 5889 RVA: 0x0003BDD3 File Offset: 0x00039FD3
		internal CompilationMode CompilationMode
		{
			get
			{
				return this.compilationMode;
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06001702 RID: 5890 RVA: 0x0003BDDB File Offset: 0x00039FDB
		internal ClientIDMode? ClientIDMode
		{
			get
			{
				return this.clientIDMode;
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06001703 RID: 5891 RVA: 0x0003BDE3 File Offset: 0x00039FE3
		// (set) Token: 0x06001704 RID: 5892 RVA: 0x0003BDEB File Offset: 0x00039FEB
		internal override TextReader Reader
		{
			get
			{
				return this.reader;
			}
			set
			{
				this.reader = value;
			}
		}

		// Token: 0x0400159D RID: 5533
		private bool autoEventWireup = true;

		// Token: 0x0400159E RID: 5534
		private bool enableViewState = true;

		// Token: 0x0400159F RID: 5535
		private CompilationMode compilationMode = CompilationMode.Always;

		// Token: 0x040015A0 RID: 5536
		private ClientIDMode? clientIDMode;

		// Token: 0x040015A1 RID: 5537
		private TextReader reader;
	}
}
