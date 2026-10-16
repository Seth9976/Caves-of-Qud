using System;
using System.Collections.Generic;

namespace System.Web
{
	// Token: 0x02000074 RID: 116
	internal sealed class ExceptionPageTemplateValues
	{
		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x00009323 File Offset: 0x00007523
		private Dictionary<string, ExceptionPageTemplateFragmentValue> Values
		{
			get
			{
				if (this.values == null)
				{
					this.values = new Dictionary<string, ExceptionPageTemplateFragmentValue>(StringComparer.Ordinal);
				}
				return this.values;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x00009343 File Offset: 0x00007543
		public int Count
		{
			get
			{
				if (this.values != null)
				{
					return this.values.Count;
				}
				return 0;
			}
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0000935C File Offset: 0x0000755C
		public string Get(string name)
		{
			if (this.values == null || this.values.Count == 0 || string.IsNullOrEmpty(name))
			{
				return null;
			}
			ExceptionPageTemplateFragmentValue exceptionPageTemplateFragmentValue;
			if (this.values.TryGetValue(name, out exceptionPageTemplateFragmentValue))
			{
				return exceptionPageTemplateFragmentValue.Value;
			}
			return null;
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x000093A0 File Offset: 0x000075A0
		public void Add(string name, Func<string, string> valueProvider)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			if (valueProvider == null && this.values == null)
			{
				return;
			}
			if (this.Values.ContainsKey(name))
			{
				return;
			}
			this.Values[name] = new ExceptionPageTemplateFragmentValue(name, valueProvider);
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x000093F0 File Offset: 0x000075F0
		public void Add(string name, string value)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			if (value == null && this.values == null)
			{
				return;
			}
			if (this.Values.ContainsKey(name))
			{
				return;
			}
			this.Values[name] = new ExceptionPageTemplateFragmentValue(name, value);
		}

		// Token: 0x04000E8F RID: 3727
		private Dictionary<string, ExceptionPageTemplateFragmentValue> values;
	}
}
