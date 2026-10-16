using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200058A RID: 1418
	[Serializable]
	internal sealed class UxmlSerializableAdapter<T> : UxmlSerializableAdapterBase
	{
		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x06002A79 RID: 10873 RVA: 0x00098C20 File Offset: 0x00096E20
		// (set) Token: 0x06002A7A RID: 10874 RVA: 0x00098C2D File Offset: 0x00096E2D
		public override object dataBoxed
		{
			get
			{
				return this.data;
			}
			set
			{
				this.data = (T)((object)value);
			}
		}

		// Token: 0x06002A7B RID: 10875 RVA: 0x00098C3C File Offset: 0x00096E3C
		public T CloneInstance(T value)
		{
			UxmlSerializableAdapter<T> uxmlSerializableAdapter = null;
			try
			{
				IUxmlSerializedDataDeserializeReference uxmlSerializedDataDeserializeReference = value as IUxmlSerializedDataDeserializeReference;
				bool flag = uxmlSerializedDataDeserializeReference != null;
				if (flag)
				{
					return (T)((object)uxmlSerializedDataDeserializeReference.DeserializeReference());
				}
				this.data = value;
				string text = JsonUtility.ToJson(this);
				uxmlSerializableAdapter = JsonUtility.FromJson<UxmlSerializableAdapter<T>>(text);
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
			}
			finally
			{
				this.data = default(T);
			}
			return (uxmlSerializableAdapter != null) ? uxmlSerializableAdapter.data : default(T);
		}

		// Token: 0x06002A7C RID: 10876 RVA: 0x00098CE0 File Offset: 0x00096EE0
		public override object CloneInstanceBoxed(object value)
		{
			return this.CloneInstance((T)((object)value));
		}

		// Token: 0x0400130E RID: 4878
		public static readonly UxmlSerializableAdapter<T> SharedInstance = new UxmlSerializableAdapter<T>();

		// Token: 0x0400130F RID: 4879
		public T data;
	}
}
