using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200028D RID: 653
	[NullableContext(1)]
	[Nullable(0)]
	internal class TreeDumper
	{
		// Token: 0x06001AEA RID: 6890 RVA: 0x0004FA2B File Offset: 0x0004DC2B
		protected TreeDumper()
		{
			this._sb = new StringBuilder();
		}

		// Token: 0x06001AEB RID: 6891 RVA: 0x0004FA3E File Offset: 0x0004DC3E
		public static string DumpCompact(TreeDumperNode root)
		{
			return new TreeDumper().DoDumpCompact(root);
		}

		// Token: 0x06001AEC RID: 6892 RVA: 0x0004FA4B File Offset: 0x0004DC4B
		protected string DoDumpCompact(TreeDumperNode root)
		{
			this.DoDumpCompact(root, string.Empty);
			return this._sb.ToString();
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x0004FA64 File Offset: 0x0004DC64
		private void DoDumpCompact(TreeDumperNode node, string indent)
		{
			this._sb.Append(node.Text);
			if (node.Value != null)
			{
				this._sb.AppendFormat(": {0}", this.DumperString(node.Value));
			}
			this._sb.AppendLine();
			List<TreeDumperNode> list = Enumerable.ToList<TreeDumperNode>(node.Children);
			for (int i = 0; i < list.Count; i++)
			{
				TreeDumperNode treeDumperNode = list[i];
				if (treeDumperNode != null)
				{
					this._sb.Append(indent);
					this._sb.Append((i == list.Count - 1) ? '└' : '├');
					this._sb.Append('─');
					this.DoDumpCompact(treeDumperNode, indent + ((i == list.Count - 1) ? "  " : "│ "));
				}
			}
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x0004FB41 File Offset: 0x0004DD41
		public static string DumpXML(TreeDumperNode root, [Nullable(2)] string indent = null)
		{
			TreeDumper treeDumper = new TreeDumper();
			treeDumper.DoDumpXML(root, string.Empty, indent ?? string.Empty);
			return treeDumper._sb.ToString();
		}

		// Token: 0x06001AEF RID: 6895 RVA: 0x0004FB68 File Offset: 0x0004DD68
		private void DoDumpXML(TreeDumperNode node, string indent, string relativeIndent)
		{
			if (Enumerable.All<TreeDumperNode>(node.Children, (TreeDumperNode child) => child == null))
			{
				this._sb.Append(indent);
				if (node.Value != null)
				{
					this._sb.AppendFormat("<{0}>{1}</{0}>", node.Text, this.DumperString(node.Value));
				}
				else
				{
					this._sb.AppendFormat("<{0} />", node.Text);
				}
				this._sb.AppendLine();
				return;
			}
			this._sb.Append(indent);
			this._sb.AppendFormat("<{0}>", node.Text);
			this._sb.AppendLine();
			if (node.Value != null)
			{
				this._sb.Append(indent);
				this._sb.AppendFormat("{0}", this.DumperString(node.Value));
				this._sb.AppendLine();
			}
			string text = indent + relativeIndent;
			foreach (TreeDumperNode treeDumperNode in node.Children)
			{
				if (treeDumperNode != null)
				{
					this.DoDumpXML(treeDumperNode, text, relativeIndent);
				}
			}
			this._sb.Append(indent);
			this._sb.AppendFormat("</{0}>", node.Text);
			this._sb.AppendLine();
		}

		// Token: 0x06001AF0 RID: 6896 RVA: 0x0004FCEC File Offset: 0x0004DEEC
		private static bool IsDefaultImmutableArray(object o)
		{
			TypeInfo typeInfo = IntrospectionExtensions.GetTypeInfo(o.GetType());
			if (typeInfo.IsGenericType && typeInfo.GetGenericTypeDefinition() == typeof(ImmutableArray<>))
			{
				object obj;
				if (typeInfo == null)
				{
					obj = null;
				}
				else
				{
					MethodInfo declaredMethod = typeInfo.GetDeclaredMethod("get_IsDefault");
					obj = ((declaredMethod != null) ? declaredMethod.Invoke(o, Array.Empty<object>()) : null);
				}
				object obj2 = obj;
				bool flag;
				bool flag2;
				if (obj2 is bool)
				{
					flag = (bool)obj2;
					flag2 = true;
				}
				else
				{
					flag2 = false;
				}
				return flag2 && flag;
			}
			return false;
		}

		// Token: 0x06001AF1 RID: 6897 RVA: 0x0004FD60 File Offset: 0x0004DF60
		protected virtual string DumperString(object o)
		{
			if (o == null)
			{
				return "(null)";
			}
			string text = o as string;
			if (text != null)
			{
				return text;
			}
			if (TreeDumper.IsDefaultImmutableArray(o))
			{
				return "(null)";
			}
			IEnumerable enumerable = o as IEnumerable;
			if (enumerable != null)
			{
				return string.Format("{{{0}}}", string.Join(", ", Enumerable.ToArray<string>(Enumerable.Select<object, string>(Enumerable.Cast<object>(enumerable), new Func<object, string>(this.DumperString)))));
			}
			ISymbol symbol = o as ISymbol;
			if (symbol != null)
			{
				return symbol.ToDisplayString(SymbolDisplayFormat.TestFormat);
			}
			return o.ToString() ?? "";
		}

		// Token: 0x04000A77 RID: 2679
		private readonly StringBuilder _sb;
	}
}
