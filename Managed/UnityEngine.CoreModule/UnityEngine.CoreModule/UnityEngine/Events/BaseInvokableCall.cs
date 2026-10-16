using System;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x02000341 RID: 833
	internal abstract class BaseInvokableCall
	{
		// Token: 0x060027D5 RID: 10197 RVA: 0x00030B93 File Offset: 0x0002ED93
		protected BaseInvokableCall()
		{
		}

		// Token: 0x060027D6 RID: 10198 RVA: 0x0004C628 File Offset: 0x0004A828
		protected BaseInvokableCall(object target, MethodInfo function)
		{
			bool flag = function == null;
			if (flag)
			{
				throw new ArgumentNullException("function");
			}
			bool isStatic = function.IsStatic;
			if (isStatic)
			{
				bool flag2 = target != null;
				if (flag2)
				{
					throw new ArgumentException("target must be null");
				}
			}
			else
			{
				bool flag3 = target == null;
				if (flag3)
				{
					throw new ArgumentNullException("target");
				}
			}
		}

		// Token: 0x060027D7 RID: 10199
		public abstract void Invoke(object[] args);

		// Token: 0x060027D8 RID: 10200 RVA: 0x0004C68C File Offset: 0x0004A88C
		protected static void ThrowOnInvalidArg<T>(object arg)
		{
			bool flag = arg != null && !(arg is T);
			if (flag)
			{
				throw new ArgumentException(UnityString.Format("Passed argument 'args[0]' is of the wrong type. Type:{0} Expected:{1}", new object[]
				{
					arg.GetType(),
					typeof(T)
				}));
			}
		}

		// Token: 0x060027D9 RID: 10201 RVA: 0x0004C6DC File Offset: 0x0004A8DC
		protected static bool AllowInvoke(Delegate @delegate)
		{
			object target = @delegate.Target;
			bool flag = target == null;
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				Object @object = target as Object;
				bool flag3 = @object != null;
				flag2 = !flag3 || @object != null;
			}
			return flag2;
		}

		// Token: 0x060027DA RID: 10202
		public abstract bool Find(object targetObj, MethodInfo method);
	}
}
