using System;
using System.Collections;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000031 RID: 49
	public class LdapSchema : LdapEntry
	{
		// Token: 0x060001E8 RID: 488 RVA: 0x00009949 File Offset: 0x00007B49
		private void InitBlock()
		{
			this.nameTable = new Hashtable[8];
			this.idTable = new Hashtable[8];
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00009963 File Offset: 0x00007B63
		public virtual IEnumerator AttributeSchemas
		{
			get
			{
				return new EnumeratedIterator(this.idTable[0].Values.GetEnumerator());
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000997C File Offset: 0x00007B7C
		public virtual IEnumerator DITContentRuleSchemas
		{
			get
			{
				return new EnumeratedIterator(this.idTable[4].Values.GetEnumerator());
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00009995 File Offset: 0x00007B95
		public virtual IEnumerator DITStructureRuleSchemas
		{
			get
			{
				return new EnumeratedIterator(this.idTable[5].Values.GetEnumerator());
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001EC RID: 492 RVA: 0x000099AE File Offset: 0x00007BAE
		public virtual IEnumerator MatchingRuleSchemas
		{
			get
			{
				return new EnumeratedIterator(this.idTable[6].Values.GetEnumerator());
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001ED RID: 493 RVA: 0x000099C7 File Offset: 0x00007BC7
		public virtual IEnumerator MatchingRuleUseSchemas
		{
			get
			{
				return new EnumeratedIterator(this.idTable[7].Values.GetEnumerator());
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001EE RID: 494 RVA: 0x000099E0 File Offset: 0x00007BE0
		public virtual IEnumerator NameFormSchemas
		{
			get
			{
				return new EnumeratedIterator(this.idTable[3].Values.GetEnumerator());
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001EF RID: 495 RVA: 0x000099F9 File Offset: 0x00007BF9
		public virtual IEnumerator ObjectClassSchemas
		{
			get
			{
				return new EnumeratedIterator(this.idTable[1].Values.GetEnumerator());
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00009A12 File Offset: 0x00007C12
		public virtual IEnumerator SyntaxSchemas
		{
			get
			{
				return new EnumeratedIterator(this.idTable[2].Values.GetEnumerator());
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00009A2B File Offset: 0x00007C2B
		public virtual IEnumerator AttributeNames
		{
			get
			{
				return new EnumeratedIterator(new SupportClass.SetSupport(this.nameTable[0].Keys).GetEnumerator());
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x00009A49 File Offset: 0x00007C49
		public virtual IEnumerator DITContentRuleNames
		{
			get
			{
				return new EnumeratedIterator(new SupportClass.SetSupport(this.nameTable[4].Keys).GetEnumerator());
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00009A67 File Offset: 0x00007C67
		public virtual IEnumerator DITStructureRuleNames
		{
			get
			{
				return new EnumeratedIterator(new SupportClass.SetSupport(this.nameTable[5].Keys).GetEnumerator());
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00009A85 File Offset: 0x00007C85
		public virtual IEnumerator MatchingRuleNames
		{
			get
			{
				return new EnumeratedIterator(new SupportClass.SetSupport(this.nameTable[6].Keys).GetEnumerator());
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x00009AA3 File Offset: 0x00007CA3
		public virtual IEnumerator MatchingRuleUseNames
		{
			get
			{
				return new EnumeratedIterator(new SupportClass.SetSupport(this.nameTable[7].Keys).GetEnumerator());
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00009AC1 File Offset: 0x00007CC1
		public virtual IEnumerator NameFormNames
		{
			get
			{
				return new EnumeratedIterator(new SupportClass.SetSupport(this.nameTable[3].Keys).GetEnumerator());
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00009ADF File Offset: 0x00007CDF
		public virtual IEnumerator ObjectClassNames
		{
			get
			{
				return new EnumeratedIterator(new SupportClass.SetSupport(this.nameTable[1].Keys).GetEnumerator());
			}
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00009B00 File Offset: 0x00007D00
		public LdapSchema(LdapEntry ent)
			: base(ent.DN, ent.getAttributeSet())
		{
			this.InitBlock();
			for (int i = 0; i < LdapSchema.schemaTypeNames.Length; i++)
			{
				this.idTable[i] = new Hashtable();
				this.nameTable[i] = new Hashtable();
			}
			foreach (object obj in base.getAttributeSet())
			{
				LdapAttribute ldapAttribute = (LdapAttribute)obj;
				string name = ldapAttribute.Name;
				IEnumerator stringValues = ldapAttribute.StringValues;
				if (name.ToUpper().Equals(LdapSchema.schemaTypeNames[1].ToUpper()))
				{
					while (stringValues.MoveNext())
					{
						object obj2 = stringValues.Current;
						string text = (string)obj2;
						LdapObjectClassSchema ldapObjectClassSchema;
						try
						{
							ldapObjectClassSchema = new LdapObjectClassSchema(text);
						}
						catch (Exception)
						{
							continue;
						}
						this.addElement(1, ldapObjectClassSchema);
					}
				}
				else if (name.ToUpper().Equals(LdapSchema.schemaTypeNames[0].ToUpper()))
				{
					while (stringValues.MoveNext())
					{
						object obj3 = stringValues.Current;
						string text = (string)obj3;
						LdapAttributeSchema ldapAttributeSchema;
						try
						{
							ldapAttributeSchema = new LdapAttributeSchema(text);
						}
						catch (Exception)
						{
							continue;
						}
						this.addElement(0, ldapAttributeSchema);
					}
				}
				else if (name.ToUpper().Equals(LdapSchema.schemaTypeNames[2].ToUpper()))
				{
					while (stringValues.MoveNext())
					{
						object obj4 = stringValues.Current;
						string text = (string)obj4;
						LdapSyntaxSchema ldapSyntaxSchema = new LdapSyntaxSchema(text);
						this.addElement(2, ldapSyntaxSchema);
					}
				}
				else if (name.ToUpper().Equals(LdapSchema.schemaTypeNames[6].ToUpper()))
				{
					while (stringValues.MoveNext())
					{
						object obj5 = stringValues.Current;
						string text = (string)obj5;
						LdapMatchingRuleSchema ldapMatchingRuleSchema = new LdapMatchingRuleSchema(text, null);
						this.addElement(6, ldapMatchingRuleSchema);
					}
				}
				else if (name.ToUpper().Equals(LdapSchema.schemaTypeNames[7].ToUpper()))
				{
					while (stringValues.MoveNext())
					{
						object obj6 = stringValues.Current;
						string text = (string)obj6;
						LdapMatchingRuleUseSchema ldapMatchingRuleUseSchema = new LdapMatchingRuleUseSchema(text);
						this.addElement(7, ldapMatchingRuleUseSchema);
					}
				}
				else if (name.ToUpper().Equals(LdapSchema.schemaTypeNames[4].ToUpper()))
				{
					while (stringValues.MoveNext())
					{
						object obj7 = stringValues.Current;
						string text = (string)obj7;
						LdapDITContentRuleSchema ldapDITContentRuleSchema = new LdapDITContentRuleSchema(text);
						this.addElement(4, ldapDITContentRuleSchema);
					}
				}
				else if (name.ToUpper().Equals(LdapSchema.schemaTypeNames[5].ToUpper()))
				{
					while (stringValues.MoveNext())
					{
						object obj8 = stringValues.Current;
						string text = (string)obj8;
						LdapDITStructureRuleSchema ldapDITStructureRuleSchema = new LdapDITStructureRuleSchema(text);
						this.addElement(5, ldapDITStructureRuleSchema);
					}
				}
				else if (name.ToUpper().Equals(LdapSchema.schemaTypeNames[3].ToUpper()))
				{
					while (stringValues.MoveNext())
					{
						object obj9 = stringValues.Current;
						string text = (string)obj9;
						LdapNameFormSchema ldapNameFormSchema = new LdapNameFormSchema(text);
						this.addElement(3, ldapNameFormSchema);
					}
				}
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00009DE0 File Offset: 0x00007FE0
		private void addElement(int schemaType, LdapSchemaElement element)
		{
			SupportClass.PutElement(this.idTable[schemaType], element.ID, element);
			string[] names = element.Names;
			for (int i = 0; i < names.Length; i++)
			{
				SupportClass.PutElement(this.nameTable[schemaType], names[i].ToUpper(), element);
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00009E30 File Offset: 0x00008030
		private LdapSchemaElement getSchemaElement(int schemaType, string key)
		{
			if (key == null || key.ToUpper().Equals("".ToUpper()))
			{
				return null;
			}
			char c = key[0];
			if (c >= '0' && c <= '9')
			{
				return (LdapSchemaElement)this.idTable[schemaType][key];
			}
			return (LdapSchemaElement)this.nameTable[schemaType][key.ToUpper()];
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00009E97 File Offset: 0x00008097
		public virtual LdapAttributeSchema getAttributeSchema(string name)
		{
			return (LdapAttributeSchema)this.getSchemaElement(0, name);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00009EA6 File Offset: 0x000080A6
		public virtual LdapDITContentRuleSchema getDITContentRuleSchema(string name)
		{
			return (LdapDITContentRuleSchema)this.getSchemaElement(4, name);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00009EB5 File Offset: 0x000080B5
		public virtual LdapDITStructureRuleSchema getDITStructureRuleSchema(string name)
		{
			return (LdapDITStructureRuleSchema)this.getSchemaElement(5, name);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00009EC4 File Offset: 0x000080C4
		public virtual LdapDITStructureRuleSchema getDITStructureRuleSchema(int ID)
		{
			return (LdapDITStructureRuleSchema)this.idTable[5][ID];
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00009EEB File Offset: 0x000080EB
		public virtual LdapMatchingRuleSchema getMatchingRuleSchema(string name)
		{
			return (LdapMatchingRuleSchema)this.getSchemaElement(6, name);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00009EFA File Offset: 0x000080FA
		public virtual LdapMatchingRuleUseSchema getMatchingRuleUseSchema(string name)
		{
			return (LdapMatchingRuleUseSchema)this.getSchemaElement(7, name);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00009F09 File Offset: 0x00008109
		public virtual LdapNameFormSchema getNameFormSchema(string name)
		{
			return (LdapNameFormSchema)this.getSchemaElement(3, name);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00009F18 File Offset: 0x00008118
		public virtual LdapObjectClassSchema getObjectClassSchema(string name)
		{
			return (LdapObjectClassSchema)this.getSchemaElement(1, name);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00009F27 File Offset: 0x00008127
		public virtual LdapSyntaxSchema getSyntaxSchema(string oid)
		{
			return (LdapSyntaxSchema)this.getSchemaElement(2, oid);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00009F38 File Offset: 0x00008138
		private int getType(LdapSchemaElement element)
		{
			if (element is LdapAttributeSchema)
			{
				return 0;
			}
			if (element is LdapObjectClassSchema)
			{
				return 1;
			}
			if (element is LdapSyntaxSchema)
			{
				return 2;
			}
			if (element is LdapNameFormSchema)
			{
				return 3;
			}
			if (element is LdapMatchingRuleSchema)
			{
				return 6;
			}
			if (element is LdapMatchingRuleUseSchema)
			{
				return 7;
			}
			if (element is LdapDITContentRuleSchema)
			{
				return 4;
			}
			if (element is LdapDITStructureRuleSchema)
			{
				return 5;
			}
			throw new ArgumentException("The specified schema element type is not recognized");
		}

		// Token: 0x04000132 RID: 306
		private Hashtable[] idTable;

		// Token: 0x04000133 RID: 307
		private Hashtable[] nameTable;

		// Token: 0x04000134 RID: 308
		internal static readonly string[] schemaTypeNames = new string[] { "attributeTypes", "objectClasses", "ldapSyntaxes", "nameForms", "dITContentRules", "dITStructureRules", "matchingRules", "matchingRuleUse" };

		// Token: 0x04000135 RID: 309
		internal const int ATTRIBUTE = 0;

		// Token: 0x04000136 RID: 310
		internal const int OBJECT_CLASS = 1;

		// Token: 0x04000137 RID: 311
		internal const int SYNTAX = 2;

		// Token: 0x04000138 RID: 312
		internal const int NAME_FORM = 3;

		// Token: 0x04000139 RID: 313
		internal const int DITCONTENT = 4;

		// Token: 0x0400013A RID: 314
		internal const int DITSTRUCTURE = 5;

		// Token: 0x0400013B RID: 315
		internal const int MATCHING = 6;

		// Token: 0x0400013C RID: 316
		internal const int MATCHING_USE = 7;
	}
}
