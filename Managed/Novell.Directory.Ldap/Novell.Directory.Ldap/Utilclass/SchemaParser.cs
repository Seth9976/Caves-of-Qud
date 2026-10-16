using System;
using System.Collections;
using System.IO;
using System.Text;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x02000050 RID: 80
	public class SchemaParser
	{
		// Token: 0x060002F5 RID: 757 RVA: 0x0000ED6E File Offset: 0x0000CF6E
		private void InitBlock()
		{
			this.usage = 0;
			this.qualifiers = new ArrayList();
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000ED82 File Offset: 0x0000CF82
		// (set) Token: 0x060002F7 RID: 759 RVA: 0x0000ED8A File Offset: 0x0000CF8A
		public virtual string RawString
		{
			get
			{
				return this.rawString;
			}
			set
			{
				this.rawString = value;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0000ED93 File Offset: 0x0000CF93
		public virtual string[] Names
		{
			get
			{
				return this.names;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x0000ED9B File Offset: 0x0000CF9B
		public virtual IEnumerator Qualifiers
		{
			get
			{
				return this.qualifiers.GetEnumerator();
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060002FA RID: 762 RVA: 0x0000EDA8 File Offset: 0x0000CFA8
		public virtual string ID
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0000EDB0 File Offset: 0x0000CFB0
		public virtual string Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000EDB8 File Offset: 0x0000CFB8
		public virtual string Syntax
		{
			get
			{
				return this.syntax;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060002FD RID: 765 RVA: 0x0000EDC0 File Offset: 0x0000CFC0
		public virtual string Superior
		{
			get
			{
				return this.superior;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0000EDC8 File Offset: 0x0000CFC8
		public virtual bool Single
		{
			get
			{
				return this.single;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060002FF RID: 767 RVA: 0x0000EDD0 File Offset: 0x0000CFD0
		public virtual bool Obsolete
		{
			get
			{
				return this.obsolete;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0000EDD8 File Offset: 0x0000CFD8
		public virtual string Equality
		{
			get
			{
				return this.equality;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000301 RID: 769 RVA: 0x0000EDE0 File Offset: 0x0000CFE0
		public virtual string Ordering
		{
			get
			{
				return this.ordering;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0000EDE8 File Offset: 0x0000CFE8
		public virtual string Substring
		{
			get
			{
				return this.substring;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0000EDF0 File Offset: 0x0000CFF0
		public virtual bool Collective
		{
			get
			{
				return this.collective;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000304 RID: 772 RVA: 0x0000EDF8 File Offset: 0x0000CFF8
		public virtual bool UserMod
		{
			get
			{
				return this.userMod;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000305 RID: 773 RVA: 0x0000EE00 File Offset: 0x0000D000
		public virtual int Usage
		{
			get
			{
				return this.usage;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000306 RID: 774 RVA: 0x0000EE08 File Offset: 0x0000D008
		public virtual int Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000307 RID: 775 RVA: 0x0000EE10 File Offset: 0x0000D010
		public virtual string[] Superiors
		{
			get
			{
				return this.superiors;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000308 RID: 776 RVA: 0x0000EE18 File Offset: 0x0000D018
		public virtual string[] Required
		{
			get
			{
				return this.required;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000309 RID: 777 RVA: 0x0000EE20 File Offset: 0x0000D020
		public virtual string[] Optional
		{
			get
			{
				return this.optional;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600030A RID: 778 RVA: 0x0000EE28 File Offset: 0x0000D028
		public virtual string[] Auxiliary
		{
			get
			{
				return this.auxiliary;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0000EE30 File Offset: 0x0000D030
		public virtual string[] Precluded
		{
			get
			{
				return this.precluded;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600030C RID: 780 RVA: 0x0000EE38 File Offset: 0x0000D038
		public virtual string[] Applies
		{
			get
			{
				return this.applies;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600030D RID: 781 RVA: 0x0000EE40 File Offset: 0x0000D040
		public virtual string NameForm
		{
			get
			{
				return this.nameForm;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600030E RID: 782 RVA: 0x0000EE48 File Offset: 0x0000D048
		public virtual string ObjectClass
		{
			get
			{
				return this.nameForm;
			}
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000EE50 File Offset: 0x0000D050
		public SchemaParser(string aString)
		{
			this.InitBlock();
			int num;
			if ((num = aString.IndexOf('\\')) != -1)
			{
				StringBuilder stringBuilder = new StringBuilder(aString.Substring(0, num));
				for (int i = num; i < aString.Length; i++)
				{
					stringBuilder.Append(aString[i]);
					if (aString[i] == '\\')
					{
						stringBuilder.Append('\\');
					}
				}
				this.rawString = stringBuilder.ToString();
			}
			else
			{
				this.rawString = aString;
			}
			SchemaTokenCreator schemaTokenCreator = new SchemaTokenCreator(new StringReader(this.rawString));
			schemaTokenCreator.OrdinaryCharacter(46);
			schemaTokenCreator.OrdinaryCharacters(48, 57);
			schemaTokenCreator.OrdinaryCharacter(123);
			schemaTokenCreator.OrdinaryCharacter(125);
			schemaTokenCreator.OrdinaryCharacter(95);
			schemaTokenCreator.OrdinaryCharacter(59);
			schemaTokenCreator.WordCharacters(46, 57);
			schemaTokenCreator.WordCharacters(123, 125);
			schemaTokenCreator.WordCharacters(95, 95);
			schemaTokenCreator.WordCharacters(59, 59);
			try
			{
				if (-1 != schemaTokenCreator.nextToken() && schemaTokenCreator.lastttype == 40)
				{
					if (-3 == schemaTokenCreator.nextToken())
					{
						this.id = schemaTokenCreator.StringValue;
					}
					while (-1 != schemaTokenCreator.nextToken())
					{
						if (schemaTokenCreator.lastttype == -3)
						{
							if (schemaTokenCreator.StringValue.ToUpper().Equals("NAME".ToUpper()))
							{
								if (schemaTokenCreator.nextToken() == 39)
								{
									this.names = new string[1];
									this.names[0] = schemaTokenCreator.StringValue;
								}
								else if (schemaTokenCreator.lastttype == 40)
								{
									ArrayList arrayList = new ArrayList();
									while (schemaTokenCreator.nextToken() == 39)
									{
										if (schemaTokenCreator.StringValue != null)
										{
											arrayList.Add(schemaTokenCreator.StringValue);
										}
									}
									if (arrayList.Count > 0)
									{
										this.names = new string[arrayList.Count];
										ArrayList arrayList2 = arrayList;
										object[] array = this.names;
										SupportClass.ArrayListSupport.ToArray(arrayList2, array);
									}
								}
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("DESC".ToUpper()))
							{
								if (schemaTokenCreator.nextToken() == 39)
								{
									this.description = schemaTokenCreator.StringValue;
								}
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("SYNTAX".ToUpper()))
							{
								this.result = schemaTokenCreator.nextToken();
								if (this.result == -3 || this.result == 39)
								{
									this.syntax = schemaTokenCreator.StringValue;
								}
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("EQUALITY".ToUpper()))
							{
								if (schemaTokenCreator.nextToken() == -3)
								{
									this.equality = schemaTokenCreator.StringValue;
								}
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("ORDERING".ToUpper()))
							{
								if (schemaTokenCreator.nextToken() == -3)
								{
									this.ordering = schemaTokenCreator.StringValue;
								}
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("SUBSTR".ToUpper()))
							{
								if (schemaTokenCreator.nextToken() == -3)
								{
									this.substring = schemaTokenCreator.StringValue;
								}
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("FORM".ToUpper()))
							{
								if (schemaTokenCreator.nextToken() == -3)
								{
									this.nameForm = schemaTokenCreator.StringValue;
								}
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("OC".ToUpper()))
							{
								if (schemaTokenCreator.nextToken() == -3)
								{
									this.objectClass = schemaTokenCreator.StringValue;
								}
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("SUP".ToUpper()))
							{
								ArrayList arrayList3 = new ArrayList();
								schemaTokenCreator.nextToken();
								if (schemaTokenCreator.lastttype == 40)
								{
									schemaTokenCreator.nextToken();
									while (schemaTokenCreator.lastttype != 41)
									{
										if (schemaTokenCreator.lastttype != 36)
										{
											arrayList3.Add(schemaTokenCreator.StringValue);
										}
										schemaTokenCreator.nextToken();
									}
								}
								else
								{
									arrayList3.Add(schemaTokenCreator.StringValue);
									this.superior = schemaTokenCreator.StringValue;
								}
								if (arrayList3.Count > 0)
								{
									this.superiors = new string[arrayList3.Count];
									ArrayList arrayList4 = arrayList3;
									object[] array = this.superiors;
									SupportClass.ArrayListSupport.ToArray(arrayList4, array);
								}
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("SINGLE-VALUE".ToUpper()))
							{
								this.single = true;
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("OBSOLETE".ToUpper()))
							{
								this.obsolete = true;
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("COLLECTIVE".ToUpper()))
							{
								this.collective = true;
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("NO-USER-MODIFICATION".ToUpper()))
							{
								this.userMod = false;
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("MUST".ToUpper()))
							{
								ArrayList arrayList5 = new ArrayList();
								schemaTokenCreator.nextToken();
								if (schemaTokenCreator.lastttype == 40)
								{
									schemaTokenCreator.nextToken();
									while (schemaTokenCreator.lastttype != 41)
									{
										if (schemaTokenCreator.lastttype != 36)
										{
											arrayList5.Add(schemaTokenCreator.StringValue);
										}
										schemaTokenCreator.nextToken();
									}
								}
								else
								{
									arrayList5.Add(schemaTokenCreator.StringValue);
								}
								if (arrayList5.Count > 0)
								{
									this.required = new string[arrayList5.Count];
									ArrayList arrayList6 = arrayList5;
									object[] array = this.required;
									SupportClass.ArrayListSupport.ToArray(arrayList6, array);
								}
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("MAY".ToUpper()))
							{
								ArrayList arrayList7 = new ArrayList();
								schemaTokenCreator.nextToken();
								if (schemaTokenCreator.lastttype == 40)
								{
									schemaTokenCreator.nextToken();
									while (schemaTokenCreator.lastttype != 41)
									{
										if (schemaTokenCreator.lastttype != 36)
										{
											arrayList7.Add(schemaTokenCreator.StringValue);
										}
										schemaTokenCreator.nextToken();
									}
								}
								else
								{
									arrayList7.Add(schemaTokenCreator.StringValue);
								}
								if (arrayList7.Count > 0)
								{
									this.optional = new string[arrayList7.Count];
									ArrayList arrayList8 = arrayList7;
									object[] array = this.optional;
									SupportClass.ArrayListSupport.ToArray(arrayList8, array);
								}
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("NOT".ToUpper()))
							{
								ArrayList arrayList9 = new ArrayList();
								schemaTokenCreator.nextToken();
								if (schemaTokenCreator.lastttype == 40)
								{
									schemaTokenCreator.nextToken();
									while (schemaTokenCreator.lastttype != 41)
									{
										if (schemaTokenCreator.lastttype != 36)
										{
											arrayList9.Add(schemaTokenCreator.StringValue);
										}
										schemaTokenCreator.nextToken();
									}
								}
								else
								{
									arrayList9.Add(schemaTokenCreator.StringValue);
								}
								if (arrayList9.Count > 0)
								{
									this.precluded = new string[arrayList9.Count];
									ArrayList arrayList10 = arrayList9;
									object[] array = this.precluded;
									SupportClass.ArrayListSupport.ToArray(arrayList10, array);
								}
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("AUX".ToUpper()))
							{
								ArrayList arrayList11 = new ArrayList();
								schemaTokenCreator.nextToken();
								if (schemaTokenCreator.lastttype == 40)
								{
									schemaTokenCreator.nextToken();
									while (schemaTokenCreator.lastttype != 41)
									{
										if (schemaTokenCreator.lastttype != 36)
										{
											arrayList11.Add(schemaTokenCreator.StringValue);
										}
										schemaTokenCreator.nextToken();
									}
								}
								else
								{
									arrayList11.Add(schemaTokenCreator.StringValue);
								}
								if (arrayList11.Count > 0)
								{
									this.auxiliary = new string[arrayList11.Count];
									ArrayList arrayList12 = arrayList11;
									object[] array = this.auxiliary;
									SupportClass.ArrayListSupport.ToArray(arrayList12, array);
								}
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("ABSTRACT".ToUpper()))
							{
								this.type = 0;
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("STRUCTURAL".ToUpper()))
							{
								this.type = 1;
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("AUXILIARY".ToUpper()))
							{
								this.type = 2;
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("USAGE".ToUpper()))
							{
								if (schemaTokenCreator.nextToken() == -3)
								{
									string text = schemaTokenCreator.StringValue;
									if (text.ToUpper().Equals("directoryOperation".ToUpper()))
									{
										this.usage = 1;
									}
									else if (text.ToUpper().Equals("distributedOperation".ToUpper()))
									{
										this.usage = 2;
									}
									else if (text.ToUpper().Equals("dSAOperation".ToUpper()))
									{
										this.usage = 3;
									}
									else if (text.ToUpper().Equals("userApplications".ToUpper()))
									{
										this.usage = 0;
									}
								}
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("APPLIES".ToUpper()))
							{
								ArrayList arrayList13 = new ArrayList();
								schemaTokenCreator.nextToken();
								if (schemaTokenCreator.lastttype == 40)
								{
									schemaTokenCreator.nextToken();
									while (schemaTokenCreator.lastttype != 41)
									{
										if (schemaTokenCreator.lastttype != 36)
										{
											arrayList13.Add(schemaTokenCreator.StringValue);
										}
										schemaTokenCreator.nextToken();
									}
								}
								else
								{
									arrayList13.Add(schemaTokenCreator.StringValue);
								}
								if (arrayList13.Count > 0)
								{
									this.applies = new string[arrayList13.Count];
									ArrayList arrayList14 = arrayList13;
									object[] array = this.applies;
									SupportClass.ArrayListSupport.ToArray(arrayList14, array);
								}
							}
							else
							{
								string text = schemaTokenCreator.StringValue;
								AttributeQualifier attributeQualifier = this.parseQualifier(schemaTokenCreator, text);
								if (attributeQualifier != null)
								{
									this.qualifiers.Add(attributeQualifier);
								}
							}
						}
					}
				}
			}
			catch (IOException ex)
			{
				throw ex;
			}
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000F838 File Offset: 0x0000DA38
		private AttributeQualifier parseQualifier(SchemaTokenCreator st, string name)
		{
			ArrayList arrayList = new ArrayList(5);
			try
			{
				if (st.nextToken() == 39)
				{
					arrayList.Add(st.StringValue);
				}
				else if (st.lastttype == 40)
				{
					while (st.nextToken() == 39)
					{
						arrayList.Add(st.StringValue);
					}
				}
			}
			catch (IOException ex)
			{
				throw ex;
			}
			string[] array = new string[arrayList.Count];
			ArrayList arrayList2 = arrayList;
			object[] array2 = array;
			array = (string[])SupportClass.ArrayListSupport.ToArray(arrayList2, array2);
			return new AttributeQualifier(name, array);
		}

		// Token: 0x040001F9 RID: 505
		internal string rawString;

		// Token: 0x040001FA RID: 506
		internal string[] names;

		// Token: 0x040001FB RID: 507
		internal string id;

		// Token: 0x040001FC RID: 508
		internal string description;

		// Token: 0x040001FD RID: 509
		internal string syntax;

		// Token: 0x040001FE RID: 510
		internal string superior;

		// Token: 0x040001FF RID: 511
		internal string nameForm;

		// Token: 0x04000200 RID: 512
		internal string objectClass;

		// Token: 0x04000201 RID: 513
		internal string[] superiors;

		// Token: 0x04000202 RID: 514
		internal string[] required;

		// Token: 0x04000203 RID: 515
		internal string[] optional;

		// Token: 0x04000204 RID: 516
		internal string[] auxiliary;

		// Token: 0x04000205 RID: 517
		internal string[] precluded;

		// Token: 0x04000206 RID: 518
		internal string[] applies;

		// Token: 0x04000207 RID: 519
		internal bool single;

		// Token: 0x04000208 RID: 520
		internal bool obsolete;

		// Token: 0x04000209 RID: 521
		internal string equality;

		// Token: 0x0400020A RID: 522
		internal string ordering;

		// Token: 0x0400020B RID: 523
		internal string substring;

		// Token: 0x0400020C RID: 524
		internal bool collective;

		// Token: 0x0400020D RID: 525
		internal bool userMod = true;

		// Token: 0x0400020E RID: 526
		internal int usage;

		// Token: 0x0400020F RID: 527
		internal int type = -1;

		// Token: 0x04000210 RID: 528
		internal int result;

		// Token: 0x04000211 RID: 529
		internal ArrayList qualifiers;
	}
}
