using System;
using System.Collections;
using System.Collections.Specialized;

namespace LitJson
{
	// Token: 0x02000327 RID: 807
	public class JsonMockWrapper : IJsonWrapper, IList, ICollection, IEnumerable, IOrderedDictionary, IDictionary
	{
		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06001DDC RID: 7644 RVA: 0x00092734 File Offset: 0x00090934
		public bool IsArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06001DDD RID: 7645 RVA: 0x00092737 File Offset: 0x00090937
		public bool IsBoolean
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06001DDE RID: 7646 RVA: 0x0009273A File Offset: 0x0009093A
		public bool IsDouble
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06001DDF RID: 7647 RVA: 0x0009273D File Offset: 0x0009093D
		public bool IsInt
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06001DE0 RID: 7648 RVA: 0x00092740 File Offset: 0x00090940
		public bool IsLong
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06001DE1 RID: 7649 RVA: 0x00092743 File Offset: 0x00090943
		public bool IsObject
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06001DE2 RID: 7650 RVA: 0x00092746 File Offset: 0x00090946
		public bool IsString
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001DE3 RID: 7651 RVA: 0x00092749 File Offset: 0x00090949
		public bool GetBoolean()
		{
			return false;
		}

		// Token: 0x06001DE4 RID: 7652 RVA: 0x0009274C File Offset: 0x0009094C
		public double GetDouble()
		{
			return 0.0;
		}

		// Token: 0x06001DE5 RID: 7653 RVA: 0x00092757 File Offset: 0x00090957
		public int GetInt()
		{
			return 0;
		}

		// Token: 0x06001DE6 RID: 7654 RVA: 0x0009275A File Offset: 0x0009095A
		public JsonType GetJsonType()
		{
			return JsonType.None;
		}

		// Token: 0x06001DE7 RID: 7655 RVA: 0x0009275D File Offset: 0x0009095D
		public long GetLong()
		{
			return 0L;
		}

		// Token: 0x06001DE8 RID: 7656 RVA: 0x00092761 File Offset: 0x00090961
		public string GetString()
		{
			return "";
		}

		// Token: 0x06001DE9 RID: 7657 RVA: 0x00092768 File Offset: 0x00090968
		public void SetBoolean(bool val)
		{
		}

		// Token: 0x06001DEA RID: 7658 RVA: 0x0009276A File Offset: 0x0009096A
		public void SetDouble(double val)
		{
		}

		// Token: 0x06001DEB RID: 7659 RVA: 0x0009276C File Offset: 0x0009096C
		public void SetInt(int val)
		{
		}

		// Token: 0x06001DEC RID: 7660 RVA: 0x0009276E File Offset: 0x0009096E
		public void SetJsonType(JsonType type)
		{
		}

		// Token: 0x06001DED RID: 7661 RVA: 0x00092770 File Offset: 0x00090970
		public void SetLong(long val)
		{
		}

		// Token: 0x06001DEE RID: 7662 RVA: 0x00092772 File Offset: 0x00090972
		public void SetString(string val)
		{
		}

		// Token: 0x06001DEF RID: 7663 RVA: 0x00092774 File Offset: 0x00090974
		public string ToJson()
		{
			return "";
		}

		// Token: 0x06001DF0 RID: 7664 RVA: 0x0009277B File Offset: 0x0009097B
		public void ToJson(JsonWriter writer)
		{
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06001DF1 RID: 7665 RVA: 0x0009277D File Offset: 0x0009097D
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06001DF2 RID: 7666 RVA: 0x00092780 File Offset: 0x00090980
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000414 RID: 1044
		object IList.this[int index]
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x06001DF5 RID: 7669 RVA: 0x00092788 File Offset: 0x00090988
		int IList.Add(object value)
		{
			return 0;
		}

		// Token: 0x06001DF6 RID: 7670 RVA: 0x0009278B File Offset: 0x0009098B
		void IList.Clear()
		{
		}

		// Token: 0x06001DF7 RID: 7671 RVA: 0x0009278D File Offset: 0x0009098D
		bool IList.Contains(object value)
		{
			return false;
		}

		// Token: 0x06001DF8 RID: 7672 RVA: 0x00092790 File Offset: 0x00090990
		int IList.IndexOf(object value)
		{
			return -1;
		}

		// Token: 0x06001DF9 RID: 7673 RVA: 0x00092793 File Offset: 0x00090993
		void IList.Insert(int i, object v)
		{
		}

		// Token: 0x06001DFA RID: 7674 RVA: 0x00092795 File Offset: 0x00090995
		void IList.Remove(object value)
		{
		}

		// Token: 0x06001DFB RID: 7675 RVA: 0x00092797 File Offset: 0x00090997
		void IList.RemoveAt(int index)
		{
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06001DFC RID: 7676 RVA: 0x00092799 File Offset: 0x00090999
		int ICollection.Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06001DFD RID: 7677 RVA: 0x0009279C File Offset: 0x0009099C
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06001DFE RID: 7678 RVA: 0x0009279F File Offset: 0x0009099F
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06001DFF RID: 7679 RVA: 0x000927A2 File Offset: 0x000909A2
		void ICollection.CopyTo(Array array, int index)
		{
		}

		// Token: 0x06001E00 RID: 7680 RVA: 0x000927A4 File Offset: 0x000909A4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return null;
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06001E01 RID: 7681 RVA: 0x000927A7 File Offset: 0x000909A7
		bool IDictionary.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06001E02 RID: 7682 RVA: 0x000927AA File Offset: 0x000909AA
		bool IDictionary.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001E03 RID: 7683 RVA: 0x000927AD File Offset: 0x000909AD
		ICollection IDictionary.Keys
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06001E04 RID: 7684 RVA: 0x000927B0 File Offset: 0x000909B0
		ICollection IDictionary.Values
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700041C RID: 1052
		object IDictionary.this[object key]
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x06001E07 RID: 7687 RVA: 0x000927B8 File Offset: 0x000909B8
		void IDictionary.Add(object k, object v)
		{
		}

		// Token: 0x06001E08 RID: 7688 RVA: 0x000927BA File Offset: 0x000909BA
		void IDictionary.Clear()
		{
		}

		// Token: 0x06001E09 RID: 7689 RVA: 0x000927BC File Offset: 0x000909BC
		bool IDictionary.Contains(object key)
		{
			return false;
		}

		// Token: 0x06001E0A RID: 7690 RVA: 0x000927BF File Offset: 0x000909BF
		void IDictionary.Remove(object key)
		{
		}

		// Token: 0x06001E0B RID: 7691 RVA: 0x000927C1 File Offset: 0x000909C1
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return null;
		}

		// Token: 0x1700041D RID: 1053
		object IOrderedDictionary.this[int idx]
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x06001E0E RID: 7694 RVA: 0x000927C9 File Offset: 0x000909C9
		IDictionaryEnumerator IOrderedDictionary.GetEnumerator()
		{
			return null;
		}

		// Token: 0x06001E0F RID: 7695 RVA: 0x000927CC File Offset: 0x000909CC
		void IOrderedDictionary.Insert(int i, object k, object v)
		{
		}

		// Token: 0x06001E10 RID: 7696 RVA: 0x000927CE File Offset: 0x000909CE
		void IOrderedDictionary.RemoveAt(int i)
		{
		}
	}
}
