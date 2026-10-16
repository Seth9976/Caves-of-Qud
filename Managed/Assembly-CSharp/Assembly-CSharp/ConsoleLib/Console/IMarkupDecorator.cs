using System;
using System.Collections.Generic;
using System.Reflection;
using XRL;

namespace ConsoleLib.Console
{
	// Token: 0x0200153F RID: 5439
	public abstract class IMarkupDecorator : IMarkupShader
	{
		// Token: 0x0600D3FC RID: 54268 RVA: 0x004EBDE4 File Offset: 0x004E9FE4
		public static IMarkupDecorator InstanceByType(string type, IMarkupShader shader)
		{
			if (IMarkupDecorator.ShaderDecoratorFactoryMethods == null)
			{
				List<MethodInfo> list = new List<MethodInfo>(ModManager.GetMethodsWithAttribute(typeof(MarkupShaders.ShaderDecoratorFactoryMethod), typeof(MarkupShaders.ShaderDecoratorType), true));
				IMarkupDecorator.ShaderDecoratorFactoryMethods = new Dictionary<string, Func<IMarkupShader, IMarkupDecorator>>(list.Count);
				foreach (MethodInfo methodInfo in list)
				{
					foreach (MarkupShaders.ShaderDecoratorFactoryMethod shaderDecoratorFactoryMethod in methodInfo.GetCustomAttributes(typeof(MarkupShaders.ShaderDecoratorFactoryMethod), false) as MarkupShaders.ShaderDecoratorFactoryMethod[])
					{
						if (shaderDecoratorFactoryMethod.Type != null)
						{
							if (IMarkupDecorator.ShaderDecoratorFactoryMethods.ContainsKey(shaderDecoratorFactoryMethod.Type))
							{
								MetricsManager.LogError("duplicate shader asset type: " + shaderDecoratorFactoryMethod.Type);
							}
							else
							{
								IMarkupDecorator.ShaderDecoratorFactoryMethods[shaderDecoratorFactoryMethod.Type] = (Func<IMarkupShader, IMarkupDecorator>)Delegate.CreateDelegate(typeof(Func<IMarkupShader, IMarkupDecorator>), methodInfo);
							}
						}
					}
				}
			}
			Func<IMarkupShader, IMarkupDecorator> func;
			if (IMarkupDecorator.ShaderDecoratorFactoryMethods.TryGetValue(type, out func))
			{
				return func(shader);
			}
			throw new Exception("unsupported shader decorator type: " + type);
		}

		// Token: 0x0600D3FD RID: 54269 RVA: 0x004EBF1C File Offset: 0x004EA11C
		public IMarkupDecorator()
		{
		}

		// Token: 0x0600D3FE RID: 54270 RVA: 0x004EBF24 File Offset: 0x004EA124
		public IMarkupDecorator(string Name)
			: base(Name)
		{
		}

		// Token: 0x0600D3FF RID: 54271 RVA: 0x004EBF2D File Offset: 0x004EA12D
		public override string GetName()
		{
			return this.Component.GetName();
		}

		// Token: 0x0600D400 RID: 54272 RVA: 0x004EBF3A File Offset: 0x004EA13A
		public override string GetDisplayName()
		{
			return this.Component.GetDisplayName();
		}

		// Token: 0x0600D401 RID: 54273 RVA: 0x004EBF47 File Offset: 0x004EA147
		public override bool IsSystemSymbol()
		{
			return this.Component.IsSystemSymbol();
		}

		// Token: 0x0600D402 RID: 54274 RVA: 0x004EBF54 File Offset: 0x004EA154
		public override bool GetShowInPicker()
		{
			return this.Component.GetShowInPicker();
		}

		// Token: 0x0600D403 RID: 54275 RVA: 0x004EBF61 File Offset: 0x004EA161
		public override IMarkupShader GetBasisShader()
		{
			return this.Component;
		}

		// Token: 0x0600D404 RID: 54276 RVA: 0x004EBF69 File Offset: 0x004EA169
		public virtual void ApplyDecoratorParameter(string param)
		{
			throw new Exception("decorator parameter " + param + " not supported on " + base.GetType().Name);
		}

		// Token: 0x04005507 RID: 21767
		protected IMarkupShader Component;

		// Token: 0x04005508 RID: 21768
		private static Dictionary<string, Func<IMarkupShader, IMarkupDecorator>> ShaderDecoratorFactoryMethods;
	}
}
