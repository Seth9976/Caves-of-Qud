using System;

namespace XRL.World
{
	// Token: 0x02000933 RID: 2355
	[Serializable]
	public class ImmutableEvent : Event
	{
		// Token: 0x06005AFA RID: 23290 RVA: 0x001B8AEC File Offset: 0x001B6CEC
		public ImmutableEvent(string _ID)
			: base(_ID)
		{
		}

		// Token: 0x06005AFB RID: 23291 RVA: 0x001B8AF5 File Offset: 0x001B6CF5
		public ImmutableEvent(string _ID, int ObjParams, int StrParams, int IntParams)
			: base(_ID, ObjParams, StrParams, IntParams)
		{
		}

		// Token: 0x06005AFC RID: 23292 RVA: 0x001B8B02 File Offset: 0x001B6D02
		public ImmutableEvent(string _ID, string ParameterName, string Parameter)
			: this(_ID, 0, 1, 0)
		{
			base.SetParameterInternal(ParameterName, Parameter);
		}

		// Token: 0x06005AFD RID: 23293 RVA: 0x001B8B17 File Offset: 0x001B6D17
		public ImmutableEvent(string _ID, string ParameterName, int Parameter)
			: this(_ID, 0, 0, 1)
		{
			base.SetParameterInternal(ParameterName, Parameter);
		}

		// Token: 0x06005AFE RID: 23294 RVA: 0x001B8B2C File Offset: 0x001B6D2C
		public ImmutableEvent(string _ID, string ParameterName, object Parameter)
			: this(_ID, 1, 0, 0)
		{
			base.SetParameterInternal(ParameterName, Parameter);
		}

		// Token: 0x06005AFF RID: 23295 RVA: 0x001B8B41 File Offset: 0x001B6D41
		public ImmutableEvent(string _ID, string ParameterName1, string Parameter1, string ParameterName2, object Parameter2)
			: this(_ID, 1, 1, 0)
		{
			base.SetParameterInternal(ParameterName1, Parameter1);
			base.SetParameterInternal(ParameterName2, Parameter2);
		}

		// Token: 0x06005B00 RID: 23296 RVA: 0x001B8B61 File Offset: 0x001B6D61
		public ImmutableEvent(string _ID, string ParameterName1, object Parameter1, string ParameterName2, string Parameter2)
			: this(_ID, 1, 1, 0)
		{
			base.SetParameterInternal(ParameterName1, Parameter1);
			base.SetParameterInternal(ParameterName2, Parameter2);
		}

		// Token: 0x06005B01 RID: 23297 RVA: 0x001B8B81 File Offset: 0x001B6D81
		public ImmutableEvent(string _ID, string ParameterName1, string Parameter1, string ParameterName2, string Parameter2)
			: this(_ID, 0, 2, 0)
		{
			base.SetParameterInternal(ParameterName1, Parameter1);
			base.SetParameterInternal(ParameterName2, Parameter2);
		}

		// Token: 0x06005B02 RID: 23298 RVA: 0x001B8BA1 File Offset: 0x001B6DA1
		public ImmutableEvent(string _ID, string ParameterName1, int Parameter1, string ParameterName2, string Parameter2)
			: this(_ID, 0, 1, 1)
		{
			base.SetParameterInternal(ParameterName1, Parameter1);
			base.SetParameterInternal(ParameterName2, Parameter2);
		}

		// Token: 0x06005B03 RID: 23299 RVA: 0x001B8BC1 File Offset: 0x001B6DC1
		public ImmutableEvent(string _ID, string ParameterName1, int Parameter1, string ParameterName2, object Parameter2)
			: this(_ID, 1, 0, 1)
		{
			base.SetParameterInternal(ParameterName1, Parameter1);
			base.SetParameterInternal(ParameterName2, Parameter2);
		}

		// Token: 0x06005B04 RID: 23300 RVA: 0x001B8BE1 File Offset: 0x001B6DE1
		public ImmutableEvent(string _ID, string ParameterName1, object Parameter1, string ParameterName2, int Parameter2)
			: this(_ID, 1, 0, 1)
		{
			base.SetParameterInternal(ParameterName1, Parameter1);
			base.SetParameterInternal(ParameterName2, Parameter2);
		}

		// Token: 0x06005B05 RID: 23301 RVA: 0x001B8C01 File Offset: 0x001B6E01
		public ImmutableEvent(string _ID, string ParameterName1, int Parameter1, string ParameterName2, int Parameter2)
			: this(_ID, 0, 0, 2)
		{
			base.SetParameterInternal(ParameterName1, Parameter1);
			base.SetParameterInternal(ParameterName2, Parameter2);
		}

		// Token: 0x06005B06 RID: 23302 RVA: 0x001B8C21 File Offset: 0x001B6E21
		public ImmutableEvent(string _ID, string ParameterName1, object Parameter1, string ParameterName2, object Parameter2)
			: this(_ID, 2, 0, 0)
		{
			base.SetParameterInternal(ParameterName1, Parameter1);
			base.SetParameterInternal(ParameterName2, Parameter2);
		}

		// Token: 0x06005B07 RID: 23303 RVA: 0x001B8C41 File Offset: 0x001B6E41
		public ImmutableEvent(string _ID, string ParameterName1, object Parameter1, string ParameterName2, string Parameter2, string ParameterName3, object Parameter3)
			: this(_ID, 2, 1, 0)
		{
			base.SetParameterInternal(ParameterName1, Parameter1);
			base.SetParameterInternal(ParameterName2, Parameter2);
			base.SetParameterInternal(ParameterName3, Parameter3);
		}

		// Token: 0x06005B08 RID: 23304 RVA: 0x001B8C6C File Offset: 0x001B6E6C
		public override void Clear()
		{
			throw new Exception("Cannot modify immutable event");
		}

		// Token: 0x06005B09 RID: 23305 RVA: 0x001B8C78 File Offset: 0x001B6E78
		public override Event SetParameter(string Name, object Value)
		{
			throw new Exception("Cannot modify immutable event");
		}

		// Token: 0x06005B0A RID: 23306 RVA: 0x001B8C84 File Offset: 0x001B6E84
		public override Event SetParameter(string Name, string Value)
		{
			throw new Exception("Cannot modify immutable event");
		}

		// Token: 0x06005B0B RID: 23307 RVA: 0x001B8C90 File Offset: 0x001B6E90
		public override Event SetParameter(string Name, int Value)
		{
			throw new Exception("Cannot modify immutable event");
		}

		// Token: 0x06005B0C RID: 23308 RVA: 0x001B8C9C File Offset: 0x001B6E9C
		public override Event AddParameter(string Name, string Value)
		{
			throw new Exception("Cannot modify immutable event");
		}

		// Token: 0x06005B0D RID: 23309 RVA: 0x001B8CA8 File Offset: 0x001B6EA8
		public override Event AddParameter(string Name, int Value)
		{
			throw new Exception("Cannot modify immutable event");
		}

		// Token: 0x06005B0E RID: 23310 RVA: 0x001B8CB4 File Offset: 0x001B6EB4
		public override Event AddParameter(string Name, object Value)
		{
			throw new Exception("Cannot modify immutable event");
		}

		// Token: 0x06005B0F RID: 23311 RVA: 0x001B8CC0 File Offset: 0x001B6EC0
		public override Event SetSilent(bool Silent)
		{
			throw new Exception("Cannot modify immutable event");
		}

		// Token: 0x06005B10 RID: 23312 RVA: 0x001B8CCC File Offset: 0x001B6ECC
		public override void RequestInterfaceExit()
		{
			throw new Exception("Cannot modify immutable event");
		}
	}
}
