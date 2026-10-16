using System;
using System.Reflection;

namespace RoslynCSharp
{
	// Token: 0x0200000C RID: 12
	public sealed class ScriptPropertyProxy : IScriptMemberProxy
	{
		// Token: 0x17000034 RID: 52
		public object this[string name]
		{
			get
			{
				try
				{
					PropertyInfo propertyInfo = this.type.FindCachedProperty(name, this.isStatic);
					if (propertyInfo == null)
					{
						throw new TargetException(string.Format("Type '{0}' does not define a property called '{1}'", this.type, name));
					}
					if (!propertyInfo.CanRead)
					{
						throw new TargetException(string.Format("The property '{0}' was found but it does not define a get accessor", name));
					}
					object obj = ((this.owner != null) ? this.owner.Instance : null);
					return propertyInfo.GetValue(obj, null);
				}
				catch (Exception ex)
				{
					if (this.throwOnError)
					{
						throw ex;
					}
				}
				return null;
			}
			set
			{
				try
				{
					PropertyInfo propertyInfo = this.type.FindCachedProperty(name, this.isStatic);
					if (propertyInfo == null)
					{
						throw new TargetException(string.Format("Type '{0}' does not define a property called '{1}'", this.type, name));
					}
					if (!propertyInfo.CanWrite)
					{
						throw new TargetException(string.Format("The property '{0}' was found but it does not define a set accessor", name));
					}
					object obj = ((this.owner != null) ? this.owner.Instance : null);
					propertyInfo.SetValue(obj, value, null);
				}
				catch (Exception ex)
				{
					if (this.throwOnError)
					{
						throw ex;
					}
				}
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003C60 File Offset: 0x00001E60
		internal ScriptPropertyProxy(bool isStatic, ScriptType type, ScriptProxy owner = null)
		{
			this.isStatic = isStatic;
			this.type = type;
			this.owner = owner;
		}

		// Token: 0x04000042 RID: 66
		private bool isStatic;

		// Token: 0x04000043 RID: 67
		private ScriptType type;

		// Token: 0x04000044 RID: 68
		private ScriptProxy owner;

		// Token: 0x04000045 RID: 69
		internal bool throwOnError = true;
	}
}
