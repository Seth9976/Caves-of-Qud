using System;
using System.Globalization;
using System.Text;

namespace System.Web.Services.Diagnostics
{
	// Token: 0x020000BC RID: 188
	internal class TraceMethod
	{
		// Token: 0x060004F7 RID: 1271 RVA: 0x000170F3 File Offset: 0x000152F3
		internal TraceMethod(object target, string name, params object[] args)
		{
			this.target = target;
			this.name = name;
			this.args = args;
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00017110 File Offset: 0x00015310
		public override string ToString()
		{
			if (this.call == null)
			{
				this.call = TraceMethod.CallString(this.target, this.name, this.args);
			}
			return this.call;
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00017140 File Offset: 0x00015340
		internal static string CallString(object target, string method, params object[] args)
		{
			StringBuilder stringBuilder = new StringBuilder();
			TraceMethod.WriteObjectId(stringBuilder, target);
			stringBuilder.Append(':');
			stringBuilder.Append(':');
			stringBuilder.Append(method);
			stringBuilder.Append('(');
			for (int i = 0; i < args.Length; i++)
			{
				object obj = args[i];
				TraceMethod.WriteObjectId(stringBuilder, obj);
				if (obj != null)
				{
					stringBuilder.Append('=');
					TraceMethod.WriteValue(stringBuilder, obj);
				}
				if (i + 1 < args.Length)
				{
					stringBuilder.Append(',');
					stringBuilder.Append(' ');
				}
			}
			stringBuilder.Append(')');
			return stringBuilder.ToString();
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x000171D2 File Offset: 0x000153D2
		internal static string MethodId(object target, string method)
		{
			StringBuilder stringBuilder = new StringBuilder();
			TraceMethod.WriteObjectId(stringBuilder, target);
			stringBuilder.Append(':');
			stringBuilder.Append(':');
			stringBuilder.Append(method);
			return stringBuilder.ToString();
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00017200 File Offset: 0x00015400
		private static void WriteObjectId(StringBuilder sb, object o)
		{
			if (o == null)
			{
				sb.Append("(null)");
				return;
			}
			if (o is Type)
			{
				Type type = (Type)o;
				sb.Append(type.FullName);
				if (!type.IsAbstract || !type.IsSealed)
				{
					sb.Append('#');
					sb.Append(TraceMethod.HashString(o));
					return;
				}
			}
			else
			{
				sb.Append(o.GetType().FullName);
				sb.Append('#');
				sb.Append(TraceMethod.HashString(o));
			}
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00017288 File Offset: 0x00015488
		private static void WriteValue(StringBuilder sb, object o)
		{
			if (o == null)
			{
				return;
			}
			if (o is string)
			{
				sb.Append('"');
				sb.Append(o);
				sb.Append('"');
				return;
			}
			Type type = o.GetType();
			if (type.IsArray)
			{
				sb.Append('[');
				sb.Append(((Array)o).Length);
				sb.Append(']');
				return;
			}
			string text = o.ToString();
			if (type.FullName == text)
			{
				sb.Append('.');
				sb.Append('.');
				return;
			}
			sb.Append(text);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00017320 File Offset: 0x00015520
		private static string HashString(object objectValue)
		{
			if (objectValue == null)
			{
				return "(null)";
			}
			return objectValue.GetHashCode().ToString(NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x0400036F RID: 879
		private object target;

		// Token: 0x04000370 RID: 880
		private string name;

		// Token: 0x04000371 RID: 881
		private object[] args;

		// Token: 0x04000372 RID: 882
		private string call;
	}
}
