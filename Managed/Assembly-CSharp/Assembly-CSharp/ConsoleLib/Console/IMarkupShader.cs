using System;
using System.Collections.Generic;
using System.Reflection;
using XRL;

namespace ConsoleLib.Console
{
	// Token: 0x0200153E RID: 5438
	public abstract class IMarkupShader : IComparable
	{
		// Token: 0x0600D3E6 RID: 54246 RVA: 0x004EBB7C File Offset: 0x004E9D7C
		public static IMarkupShader InstanceByType(string type, string name)
		{
			if (IMarkupShader.ShaderAssetFactoryMethods == null)
			{
				List<MethodInfo> list = new List<MethodInfo>(ModManager.GetMethodsWithAttribute(typeof(MarkupShaders.ShaderAssetFactoryMethod), typeof(MarkupShaders.ShaderAssetType), true));
				IMarkupShader.ShaderAssetFactoryMethods = new Dictionary<string, Func<string, IMarkupShader>>(list.Count);
				foreach (MethodInfo methodInfo in list)
				{
					foreach (MarkupShaders.ShaderAssetFactoryMethod shaderAssetFactoryMethod in methodInfo.GetCustomAttributes(typeof(MarkupShaders.ShaderAssetFactoryMethod), false) as MarkupShaders.ShaderAssetFactoryMethod[])
					{
						if (shaderAssetFactoryMethod.Type != null)
						{
							if (IMarkupShader.ShaderAssetFactoryMethods.ContainsKey(shaderAssetFactoryMethod.Type))
							{
								MetricsManager.LogError("duplicate shader asset type: " + shaderAssetFactoryMethod.Type);
							}
							else
							{
								IMarkupShader.ShaderAssetFactoryMethods[shaderAssetFactoryMethod.Type] = (Func<string, IMarkupShader>)Delegate.CreateDelegate(typeof(Func<string, IMarkupShader>), methodInfo);
							}
						}
					}
				}
			}
			Func<string, IMarkupShader> func;
			if (IMarkupShader.ShaderAssetFactoryMethods.TryGetValue(type, out func))
			{
				return func(name);
			}
			throw new Exception("unsupported shader type: " + type);
		}

		// Token: 0x0600D3E7 RID: 54247 RVA: 0x004EBCB4 File Offset: 0x004E9EB4
		public IMarkupShader()
		{
		}

		// Token: 0x0600D3E8 RID: 54248 RVA: 0x004EBCBC File Offset: 0x004E9EBC
		public IMarkupShader(string Name)
		{
			this.Name = Name;
			this.DisplayName = Name;
		}

		// Token: 0x0600D3E9 RID: 54249 RVA: 0x004EBCD2 File Offset: 0x004E9ED2
		public virtual string GetName()
		{
			return this.Name;
		}

		// Token: 0x0600D3EA RID: 54250 RVA: 0x004EBCDA File Offset: 0x004E9EDA
		public virtual void SetDisplayName(string name)
		{
			this.DisplayName = name;
		}

		// Token: 0x0600D3EB RID: 54251 RVA: 0x004EBCE3 File Offset: 0x004E9EE3
		public virtual string GetDisplayName()
		{
			return this.DisplayName;
		}

		// Token: 0x0600D3EC RID: 54252 RVA: 0x004EBCEB File Offset: 0x004E9EEB
		public virtual void SetSystemSymbol(bool flag)
		{
			this.SystemSymbol = flag;
		}

		// Token: 0x0600D3ED RID: 54253 RVA: 0x004EBCF4 File Offset: 0x004E9EF4
		public virtual bool IsSystemSymbol()
		{
			return this.SystemSymbol;
		}

		// Token: 0x0600D3EE RID: 54254 RVA: 0x004EBCFC File Offset: 0x004E9EFC
		public virtual void SetShowInPicker(bool flag)
		{
			this.ShowInPicker = flag;
		}

		// Token: 0x0600D3EF RID: 54255 RVA: 0x004EBD05 File Offset: 0x004E9F05
		public virtual bool GetShowInPicker()
		{
			return this.ShowInPicker;
		}

		// Token: 0x0600D3F0 RID: 54256 RVA: 0x004EBD0D File Offset: 0x004E9F0D
		public void SetColors(char[] Colors)
		{
			this.Colors = Colors;
		}

		// Token: 0x0600D3F1 RID: 54257 RVA: 0x004EBD18 File Offset: 0x004E9F18
		public void SetColors(string[] colorSpecs)
		{
			this.Colors = new char[colorSpecs.Length];
			int i = 0;
			int num = colorSpecs.Length;
			while (i < num)
			{
				SolidColor solidColor = MarkupShaders.Get(colorSpecs[i]) as SolidColor;
				this.Colors[i] = ((solidColor == null) ? 'y' : (solidColor.Foreground ?? 'y'));
				i++;
			}
		}

		// Token: 0x0600D3F2 RID: 54258 RVA: 0x004EBD7B File Offset: 0x004E9F7B
		public void SetColors(string colorSpec)
		{
			this.SetColors(colorSpec.Split('-', StringSplitOptions.None));
		}

		// Token: 0x0600D3F3 RID: 54259 RVA: 0x004EBD8C File Offset: 0x004E9F8C
		public virtual char? GetForegroundColor(char ch, int localPos, int localLen, int totalPos, int totalLen)
		{
			return new char?('y');
		}

		// Token: 0x0600D3F4 RID: 54260 RVA: 0x004EBD95 File Offset: 0x004E9F95
		public virtual char? GetBackgroundColor(char ch, int localPos, int localLen, int totalPos, int totalLen)
		{
			return new char?('k');
		}

		// Token: 0x0600D3F5 RID: 54261 RVA: 0x004EBD9E File Offset: 0x004E9F9E
		public virtual bool IsPattern()
		{
			return false;
		}

		// Token: 0x0600D3F6 RID: 54262 RVA: 0x004EBDA1 File Offset: 0x004E9FA1
		public virtual IMarkupShader GetInstanceHandler()
		{
			return this;
		}

		// Token: 0x0600D3F7 RID: 54263 RVA: 0x004EBDA4 File Offset: 0x004E9FA4
		public virtual IMarkupShader GetMatchHandler(string action)
		{
			return null;
		}

		// Token: 0x0600D3F8 RID: 54264 RVA: 0x004EBDA7 File Offset: 0x004E9FA7
		public virtual int GetPatternPriority()
		{
			return 100;
		}

		// Token: 0x0600D3F9 RID: 54265 RVA: 0x004EBDAB File Offset: 0x004E9FAB
		public virtual IMarkupShader GetBasisShader()
		{
			return this;
		}

		// Token: 0x0600D3FA RID: 54266 RVA: 0x004EBDAE File Offset: 0x004E9FAE
		public int CompareTo(object obj)
		{
			return this.CompareTo(obj as IMarkupShader);
		}

		// Token: 0x0600D3FB RID: 54267 RVA: 0x004EBDBC File Offset: 0x004E9FBC
		public int CompareTo(IMarkupShader o)
		{
			if (o == null)
			{
				return -1;
			}
			return o.GetPatternPriority().CompareTo(this.GetPatternPriority());
		}

		// Token: 0x04005501 RID: 21761
		private string Name;

		// Token: 0x04005502 RID: 21762
		private string DisplayName;

		// Token: 0x04005503 RID: 21763
		private bool SystemSymbol;

		// Token: 0x04005504 RID: 21764
		private bool ShowInPicker;

		// Token: 0x04005505 RID: 21765
		protected char[] Colors;

		// Token: 0x04005506 RID: 21766
		private static Dictionary<string, Func<string, IMarkupShader>> ShaderAssetFactoryMethods;
	}
}
