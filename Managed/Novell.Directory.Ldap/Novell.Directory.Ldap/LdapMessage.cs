using System;
using System.Reflection;
using Novell.Directory.Ldap.Rfc2251;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000026 RID: 38
	public class LdapMessage
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000192 RID: 402 RVA: 0x000080BB File Offset: 0x000062BB
		internal virtual LdapMessage RequestingMessage
		{
			get
			{
				return this.message.RequestingMessage;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000193 RID: 403 RVA: 0x000080C8 File Offset: 0x000062C8
		public virtual LdapControl[] Controls
		{
			get
			{
				LdapControl[] array = null;
				RfcControls controls = this.message.Controls;
				if (controls != null)
				{
					array = new LdapControl[controls.size()];
					for (int i = 0; i < controls.size(); i++)
					{
						RfcControl rfcControl = (RfcControl)controls.get_Renamed(i);
						string text = rfcControl.ControlType.stringValue();
						sbyte[] array2 = rfcControl.ControlValue.byteValue();
						bool flag = rfcControl.Criticality.booleanValue();
						array[i] = this.controlFactory(text, flag, array2);
					}
				}
				return array;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00008143 File Offset: 0x00006343
		public virtual int MessageID
		{
			get
			{
				if (this.imsgNum == -1)
				{
					this.imsgNum = this.message.MessageID;
				}
				return this.imsgNum;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00008165 File Offset: 0x00006365
		public virtual int Type
		{
			get
			{
				if (this.messageType == -1)
				{
					this.messageType = this.message.Type;
				}
				return this.messageType;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00008187 File Offset: 0x00006387
		public virtual bool Request
		{
			get
			{
				return this.message.isRequest();
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00008194 File Offset: 0x00006394
		internal virtual RfcLdapMessage Asn1Object
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000198 RID: 408 RVA: 0x0000819C File Offset: 0x0000639C
		private string Name
		{
			get
			{
				switch (this.Type)
				{
				case 0:
					return "LdapBindRequest";
				case 1:
					return "LdapBindResponse";
				case 2:
					return "LdapUnbindRequest";
				case 3:
					return "LdapSearchRequest";
				case 4:
					return "LdapSearchResponse";
				case 5:
					return "LdapSearchResult";
				case 6:
					return "LdapModifyRequest";
				case 7:
					return "LdapModifyResponse";
				case 8:
					return "LdapAddRequest";
				case 9:
					return "LdapAddResponse";
				case 10:
					return "LdapDelRequest";
				case 11:
					return "LdapDelResponse";
				case 12:
					return "LdapModifyRDNRequest";
				case 13:
					return "LdapModifyRDNResponse";
				case 14:
					return "LdapCompareRequest";
				case 15:
					return "LdapCompareResponse";
				case 16:
					return "LdapAbandonRequest";
				case 19:
					return "LdapSearchResultReference";
				case 23:
					return "LdapExtendedRequest";
				case 24:
					return "LdapExtendedResponse";
				case 25:
					return "LdapIntermediateResponse";
				}
				throw new SystemException("LdapMessage: Unknown Type " + this.Type.ToString());
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00008304 File Offset: 0x00006504
		// (set) Token: 0x0600019A RID: 410 RVA: 0x0000833C File Offset: 0x0000653C
		public virtual string Tag
		{
			get
			{
				if (this.stringTag != null)
				{
					return this.stringTag;
				}
				if (this.Request)
				{
					return null;
				}
				LdapMessage requestingMessage = this.RequestingMessage;
				if (requestingMessage == null)
				{
					return null;
				}
				return requestingMessage.stringTag;
			}
			set
			{
				this.stringTag = value;
			}
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00008345 File Offset: 0x00006545
		internal LdapMessage()
		{
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000835C File Offset: 0x0000655C
		internal LdapMessage(int type, RfcRequest op, LdapControl[] controls)
		{
			this.messageType = type;
			RfcControls rfcControls = null;
			if (controls != null)
			{
				rfcControls = new RfcControls();
				for (int i = 0; i < controls.Length; i++)
				{
					rfcControls.add(controls[i].Asn1Object);
				}
			}
			this.message = new RfcLdapMessage(op, rfcControls);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x000083B8 File Offset: 0x000065B8
		protected internal LdapMessage(RfcLdapMessage message)
		{
			this.message = message;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000083D5 File Offset: 0x000065D5
		internal LdapMessage Clone(string dn, string filter, bool reference)
		{
			return new LdapMessage((RfcLdapMessage)this.message.dupMessage(dn, filter, reference));
		}

		// Token: 0x0600019F RID: 415 RVA: 0x000083F0 File Offset: 0x000065F0
		private LdapControl controlFactory(string oid, bool critical, sbyte[] value_Renamed)
		{
			RespControlVector registeredControls = LdapControl.RegisteredControls;
			try
			{
				Type type = registeredControls.findResponseControl(oid);
				if (type == null)
				{
					return new LdapControl(oid, critical, value_Renamed);
				}
				Type[] array = new Type[]
				{
					typeof(string),
					typeof(bool),
					typeof(sbyte[])
				};
				object[] array2 = new object[] { oid, critical, value_Renamed };
				try
				{
					ConstructorInfo constructor = type.GetConstructor(array);
					try
					{
						return (LdapControl)constructor.Invoke(array2);
					}
					catch (UnauthorizedAccessException)
					{
					}
					catch (TargetInvocationException)
					{
					}
					catch (Exception)
					{
					}
				}
				catch (MethodAccessException)
				{
				}
			}
			catch (FieldAccessException)
			{
			}
			return new LdapControl(oid, critical, value_Renamed);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x000084E0 File Offset: 0x000066E0
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				this.Name,
				"(",
				this.MessageID.ToString(),
				"): ",
				this.message.ToString()
			});
		}

		// Token: 0x04000102 RID: 258
		public const int BIND_REQUEST = 0;

		// Token: 0x04000103 RID: 259
		public const int BIND_RESPONSE = 1;

		// Token: 0x04000104 RID: 260
		public const int UNBIND_REQUEST = 2;

		// Token: 0x04000105 RID: 261
		public const int SEARCH_REQUEST = 3;

		// Token: 0x04000106 RID: 262
		public const int SEARCH_RESPONSE = 4;

		// Token: 0x04000107 RID: 263
		public const int SEARCH_RESULT = 5;

		// Token: 0x04000108 RID: 264
		public const int MODIFY_REQUEST = 6;

		// Token: 0x04000109 RID: 265
		public const int MODIFY_RESPONSE = 7;

		// Token: 0x0400010A RID: 266
		public const int ADD_REQUEST = 8;

		// Token: 0x0400010B RID: 267
		public const int ADD_RESPONSE = 9;

		// Token: 0x0400010C RID: 268
		public const int DEL_REQUEST = 10;

		// Token: 0x0400010D RID: 269
		public const int DEL_RESPONSE = 11;

		// Token: 0x0400010E RID: 270
		public const int MODIFY_RDN_REQUEST = 12;

		// Token: 0x0400010F RID: 271
		public const int MODIFY_RDN_RESPONSE = 13;

		// Token: 0x04000110 RID: 272
		public const int COMPARE_REQUEST = 14;

		// Token: 0x04000111 RID: 273
		public const int COMPARE_RESPONSE = 15;

		// Token: 0x04000112 RID: 274
		public const int ABANDON_REQUEST = 16;

		// Token: 0x04000113 RID: 275
		public const int SEARCH_RESULT_REFERENCE = 19;

		// Token: 0x04000114 RID: 276
		public const int EXTENDED_REQUEST = 23;

		// Token: 0x04000115 RID: 277
		public const int EXTENDED_RESPONSE = 24;

		// Token: 0x04000116 RID: 278
		public const int INTERMEDIATE_RESPONSE = 25;

		// Token: 0x04000117 RID: 279
		protected internal RfcLdapMessage message;

		// Token: 0x04000118 RID: 280
		private int imsgNum = -1;

		// Token: 0x04000119 RID: 281
		private int messageType = -1;

		// Token: 0x0400011A RID: 282
		private string stringTag;
	}
}
