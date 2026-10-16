using System;
using System.IO;

namespace SimpleJSON
{
	// Token: 0x0200035C RID: 860
	public class JSONData : JSONNode
	{
		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06002008 RID: 8200 RVA: 0x0009D74F File Offset: 0x0009B94F
		// (set) Token: 0x06002009 RID: 8201 RVA: 0x0009D757 File Offset: 0x0009B957
		public override string Value
		{
			get
			{
				return this.m_Data;
			}
			set
			{
				this.m_Data = value;
			}
		}

		// Token: 0x0600200A RID: 8202 RVA: 0x0009D760 File Offset: 0x0009B960
		public JSONData(string aData)
		{
			this.m_Data = aData;
		}

		// Token: 0x0600200B RID: 8203 RVA: 0x0009D76F File Offset: 0x0009B96F
		public JSONData(float aData)
		{
			this.AsFloat = aData;
		}

		// Token: 0x0600200C RID: 8204 RVA: 0x0009D77E File Offset: 0x0009B97E
		public JSONData(double aData)
		{
			this.AsDouble = aData;
		}

		// Token: 0x0600200D RID: 8205 RVA: 0x0009D78D File Offset: 0x0009B98D
		public JSONData(bool aData)
		{
			this.AsBool = aData;
		}

		// Token: 0x0600200E RID: 8206 RVA: 0x0009D79C File Offset: 0x0009B99C
		public JSONData(int aData)
		{
			this.AsInt = aData;
		}

		// Token: 0x0600200F RID: 8207 RVA: 0x0009D7AB File Offset: 0x0009B9AB
		public override string ToString()
		{
			return "\"" + JSONNode.Escape(this.m_Data) + "\"";
		}

		// Token: 0x06002010 RID: 8208 RVA: 0x0009D7C7 File Offset: 0x0009B9C7
		public override string ToString(string aPrefix)
		{
			return "\"" + JSONNode.Escape(this.m_Data) + "\"";
		}

		// Token: 0x06002011 RID: 8209 RVA: 0x0009D7E4 File Offset: 0x0009B9E4
		public override void Serialize(BinaryWriter aWriter)
		{
			JSONData jsondata = new JSONData("");
			jsondata.AsInt = this.AsInt;
			if (jsondata.m_Data == this.m_Data)
			{
				aWriter.Write(4);
				aWriter.Write(this.AsInt);
				return;
			}
			jsondata.AsFloat = this.AsFloat;
			if (jsondata.m_Data == this.m_Data)
			{
				aWriter.Write(7);
				aWriter.Write(this.AsFloat);
				return;
			}
			jsondata.AsDouble = this.AsDouble;
			if (jsondata.m_Data == this.m_Data)
			{
				aWriter.Write(5);
				aWriter.Write(this.AsDouble);
				return;
			}
			jsondata.AsBool = this.AsBool;
			if (jsondata.m_Data == this.m_Data)
			{
				aWriter.Write(6);
				aWriter.Write(this.AsBool);
				return;
			}
			aWriter.Write(3);
			aWriter.Write(this.m_Data);
		}

		// Token: 0x04001062 RID: 4194
		private string m_Data;
	}
}
