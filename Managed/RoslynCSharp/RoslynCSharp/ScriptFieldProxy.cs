using System;
using System.Reflection;

namespace RoslynCSharp
{
	// Token: 0x0200000B RID: 11
	public sealed class ScriptFieldProxy : IScriptMemberProxy
	{
		// Token: 0x17000033 RID: 51
		public object this[string name]
		{
			get
			{
				try
				{
					FieldInfo fieldInfo = this.type.FindCachedField(name, this.isStatic);
					if (fieldInfo == null)
					{
						throw new TargetException(string.Format("Type '{0}' does not define a field called '{1}'", this.type, name));
					}
					object obj = ((this.owner != null) ? this.owner.Instance : null);
					return fieldInfo.GetValue(obj);
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
					FieldInfo fieldInfo = this.type.FindCachedField(name, this.isStatic);
					if (fieldInfo == null)
					{
						throw new TargetException(string.Format("Type '{0}' does not define a field called '{1}'", this.type, name));
					}
					object obj = ((this.owner != null) ? this.owner.Instance : null);
					fieldInfo.SetValue(obj, value);
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

		// Token: 0x06000092 RID: 146 RVA: 0x00003B08 File Offset: 0x00001D08
		internal ScriptFieldProxy(bool isStatic, ScriptType type, ScriptProxy owner = null)
		{
			this.isStatic = isStatic;
			this.type = type;
			this.owner = owner;
		}

		// Token: 0x0400003E RID: 62
		private bool isStatic;

		// Token: 0x0400003F RID: 63
		private ScriptType type;

		// Token: 0x04000040 RID: 64
		private ScriptProxy owner;

		// Token: 0x04000041 RID: 65
		internal bool throwOnError = true;
	}
}
