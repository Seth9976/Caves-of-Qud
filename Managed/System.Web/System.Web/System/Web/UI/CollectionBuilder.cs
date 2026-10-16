using System;
using System.Collections;
using System.Reflection;
using System.Text;

namespace System.Web.UI
{
	// Token: 0x020001AC RID: 428
	internal sealed class CollectionBuilder : ControlBuilder
	{
		// Token: 0x0600107C RID: 4220 RVA: 0x000287AA File Offset: 0x000269AA
		internal CollectionBuilder()
		{
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x0002B67C File Offset: 0x0002987C
		public override void AppendLiteralString(string s)
		{
			if (s != null && s.Trim().Length > 0)
			{
				string text = "Literal content not allowed for ";
				Type controlType = base.ControlType;
				throw new HttpException(text + ((controlType != null) ? controlType.ToString() : null));
			}
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x0002B6B4 File Offset: 0x000298B4
		public override Type GetChildControlType(string tagName, IDictionary attribs)
		{
			Type childControlType = base.Root.GetChildControlType(tagName, attribs);
			if (this.possibleElementTypes != null)
			{
				bool flag = false;
				int num = 0;
				while (num < this.possibleElementTypes.Length && !flag)
				{
					flag = this.possibleElementTypes[num].IsAssignableFrom(childControlType);
					num++;
				}
				if (!flag)
				{
					StringBuilder stringBuilder = new StringBuilder();
					for (int i = 0; i < this.possibleElementTypes.Length; i++)
					{
						if (i != 0)
						{
							stringBuilder.Append(", ");
						}
						stringBuilder.Append(this.possibleElementTypes[i]);
					}
					string text = "Cannot add a ";
					Type type = childControlType;
					string text2 = ((type != null) ? type.ToString() : null);
					string text3 = " to ";
					StringBuilder stringBuilder2 = stringBuilder;
					throw new HttpException(text + text2 + text3 + ((stringBuilder2 != null) ? stringBuilder2.ToString() : null));
				}
			}
			return childControlType;
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x0002B774 File Offset: 0x00029974
		public override void Init(TemplateParser parser, ControlBuilder parentBuilder, Type type, string tagName, string id, IDictionary attribs)
		{
			base.Init(parser, parentBuilder, type, tagName, id, attribs);
			PropertyInfo property = parentBuilder.ControlType.GetProperty(tagName, ControlBuilder.FlagsNoCase);
			base.SetControlType(property.PropertyType);
			MemberInfo[] member = base.ControlType.GetMember("Item", MemberTypes.Property, ControlBuilder.FlagsNoCase & ~BindingFlags.IgnoreCase);
			if (member.Length != 0)
			{
				this.possibleElementTypes = new Type[member.Length];
				for (int i = 0; i < member.Length; i++)
				{
					this.possibleElementTypes[i] = ((PropertyInfo)member[i]).PropertyType;
				}
				return;
			}
			string text = "Collection of type '";
			Type controlType = base.ControlType;
			throw new HttpException(text + ((controlType != null) ? controlType.ToString() : null) + "' does not have an indexer.");
		}

		// Token: 0x0400138F RID: 5007
		private Type[] possibleElementTypes;
	}
}
