using System;
using System.Collections;
using System.Collections.Specialized;

namespace LitJson
{
	// Token: 0x0200031A RID: 794
	public interface IJsonWrapper : IList, ICollection, IEnumerable, IOrderedDictionary, IDictionary
	{
		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06001D21 RID: 7457
		bool IsArray { get; }

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06001D22 RID: 7458
		bool IsBoolean { get; }

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06001D23 RID: 7459
		bool IsDouble { get; }

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06001D24 RID: 7460
		bool IsInt { get; }

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06001D25 RID: 7461
		bool IsLong { get; }

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06001D26 RID: 7462
		bool IsObject { get; }

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06001D27 RID: 7463
		bool IsString { get; }

		// Token: 0x06001D28 RID: 7464
		bool GetBoolean();

		// Token: 0x06001D29 RID: 7465
		double GetDouble();

		// Token: 0x06001D2A RID: 7466
		int GetInt();

		// Token: 0x06001D2B RID: 7467
		JsonType GetJsonType();

		// Token: 0x06001D2C RID: 7468
		long GetLong();

		// Token: 0x06001D2D RID: 7469
		string GetString();

		// Token: 0x06001D2E RID: 7470
		void SetBoolean(bool val);

		// Token: 0x06001D2F RID: 7471
		void SetDouble(double val);

		// Token: 0x06001D30 RID: 7472
		void SetInt(int val);

		// Token: 0x06001D31 RID: 7473
		void SetJsonType(JsonType type);

		// Token: 0x06001D32 RID: 7474
		void SetLong(long val);

		// Token: 0x06001D33 RID: 7475
		void SetString(string val);

		// Token: 0x06001D34 RID: 7476
		string ToJson();

		// Token: 0x06001D35 RID: 7477
		void ToJson(JsonWriter writer);
	}
}
