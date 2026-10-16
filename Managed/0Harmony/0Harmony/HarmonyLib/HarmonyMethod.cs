using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HarmonyLib
{
	// Token: 0x02000069 RID: 105
	public class HarmonyMethod
	{
		// Token: 0x060001E9 RID: 489 RVA: 0x0000BDFA File Offset: 0x00009FFA
		public HarmonyMethod()
		{
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000BE0C File Offset: 0x0000A00C
		private void ImportMethod(MethodInfo theMethod)
		{
			this.method = theMethod;
			if (this.method != null)
			{
				List<HarmonyMethod> fromMethod = HarmonyMethodExtensions.GetFromMethod(this.method);
				if (fromMethod != null)
				{
					HarmonyMethod.Merge(fromMethod).CopyTo(this);
				}
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000BE43 File Offset: 0x0000A043
		public HarmonyMethod(MethodInfo method)
		{
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			this.ImportMethod(method);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000BE68 File Offset: 0x0000A068
		public HarmonyMethod(MethodInfo method, int priority = -1, string[] before = null, string[] after = null, bool? debug = null)
		{
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			this.ImportMethod(method);
			this.priority = priority;
			this.before = before;
			this.after = after;
			this.debug = debug;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000BEB8 File Offset: 0x0000A0B8
		public HarmonyMethod(Type methodType, string methodName, Type[] argumentTypes = null)
		{
			MethodInfo methodInfo = AccessTools.Method(methodType, methodName, argumentTypes, null);
			if (methodInfo == null)
			{
				throw new ArgumentException(string.Format("Cannot not find method for type {0} and name {1} and parameters {2}", methodType, methodName, (argumentTypes != null) ? argumentTypes.Description() : null));
			}
			this.ImportMethod(methodInfo);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000BF04 File Offset: 0x0000A104
		public static List<string> HarmonyFields()
		{
			return (from s in AccessTools.GetFieldNames(typeof(HarmonyMethod))
				where s != "method"
				select s).ToList<string>();
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000BF40 File Offset: 0x0000A140
		public static HarmonyMethod Merge(List<HarmonyMethod> attributes)
		{
			HarmonyMethod harmonyMethod = new HarmonyMethod();
			if (attributes == null)
			{
				return harmonyMethod;
			}
			Traverse resultTrv = Traverse.Create(harmonyMethod);
			attributes.ForEach(delegate(HarmonyMethod attribute)
			{
				Traverse trv = Traverse.Create(attribute);
				HarmonyMethod.HarmonyFields().ForEach(delegate(string f)
				{
					object value = trv.Field(f).GetValue();
					if (value != null && (f != "priority" || (int)value != -1))
					{
						HarmonyMethodExtensions.SetValue(resultTrv, f, value);
					}
				});
			});
			return harmonyMethod;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000BF80 File Offset: 0x0000A180
		public override string ToString()
		{
			string result = "";
			Traverse trv = Traverse.Create(this);
			HarmonyMethod.HarmonyFields().ForEach(delegate(string f)
			{
				if (result.Length > 0)
				{
					result += ", ";
				}
				result += string.Format("{0}={1}", f, trv.Field(f).GetValue());
			});
			return "HarmonyMethod[" + result + "]";
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000BFD8 File Offset: 0x0000A1D8
		internal string Description()
		{
			string text = ((this.declaringType != null) ? this.declaringType.FullName : "undefined");
			string text2 = this.methodName ?? "undefined";
			string text3 = ((this.methodType != null) ? this.methodType.Value.ToString() : "undefined");
			string text4 = ((this.argumentTypes != null) ? this.argumentTypes.Description() : "undefined");
			return string.Concat(new string[] { "(class=", text, ", methodname=", text2, ", type=", text3, ", args=", text4, ")" });
		}

		// Token: 0x04000134 RID: 308
		public MethodInfo method;

		// Token: 0x04000135 RID: 309
		public Type declaringType;

		// Token: 0x04000136 RID: 310
		public string methodName;

		// Token: 0x04000137 RID: 311
		public MethodType? methodType;

		// Token: 0x04000138 RID: 312
		public Type[] argumentTypes;

		// Token: 0x04000139 RID: 313
		public int priority = -1;

		// Token: 0x0400013A RID: 314
		public string[] before;

		// Token: 0x0400013B RID: 315
		public string[] after;

		// Token: 0x0400013C RID: 316
		public HarmonyReversePatchType? reversePatchType;

		// Token: 0x0400013D RID: 317
		public bool? debug;

		// Token: 0x0400013E RID: 318
		public bool nonVirtualDelegate;
	}
}
