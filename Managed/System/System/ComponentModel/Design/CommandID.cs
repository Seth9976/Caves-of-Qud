using System;
using System.Globalization;

namespace System.ComponentModel.Design
{
	/// <summary>Represents a unique command identifier that consists of a numeric command ID and a GUID menu group identifier.</summary>
	// Token: 0x02000442 RID: 1090
	public class CommandID
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CommandID" /> class using the specified menu group GUID and command ID number.</summary>
		/// <param name="menuGroup">The GUID of the group that this menu command belongs to. </param>
		/// <param name="commandID">The numeric identifier of this menu command. </param>
		// Token: 0x0600238F RID: 9103 RVA: 0x0008100C File Offset: 0x0007F20C
		public CommandID(Guid menuGroup, int commandID)
		{
			this.Guid = menuGroup;
			this.ID = commandID;
		}

		/// <summary>Gets the numeric command ID.</summary>
		/// <returns>The command ID number.</returns>
		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06002390 RID: 9104 RVA: 0x00081022 File Offset: 0x0007F222
		public virtual int ID { get; }

		/// <summary>Determines whether two <see cref="T:System.ComponentModel.Design.CommandID" /> instances are equal.</summary>
		/// <returns>true if the specified object is equivalent to this one; otherwise, false.</returns>
		/// <param name="obj">The object to compare. </param>
		// Token: 0x06002391 RID: 9105 RVA: 0x0008102C File Offset: 0x0007F22C
		public override bool Equals(object obj)
		{
			if (!(obj is CommandID))
			{
				return false;
			}
			CommandID commandID = (CommandID)obj;
			return commandID.Guid.Equals(this.Guid) && commandID.ID == this.ID;
		}

		/// <returns>A hash code for the current object.</returns>
		// Token: 0x06002392 RID: 9106 RVA: 0x00081070 File Offset: 0x0007F270
		public override int GetHashCode()
		{
			return (this.Guid.GetHashCode() << 2) | this.ID;
		}

		/// <summary>Gets the GUID of the menu group that the menu command identified by this <see cref="T:System.ComponentModel.Design.CommandID" /> belongs to.</summary>
		/// <returns>The GUID of the command group for this command.</returns>
		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06002393 RID: 9107 RVA: 0x0008109A File Offset: 0x0007F29A
		public virtual Guid Guid { get; }

		/// <summary>Returns a <see cref="T:System.String" /> that represents the current object.</summary>
		/// <returns>A string that contains the command ID information, both the GUID and integer identifier. </returns>
		// Token: 0x06002394 RID: 9108 RVA: 0x000810A4 File Offset: 0x0007F2A4
		public override string ToString()
		{
			return this.Guid.ToString() + " : " + this.ID.ToString(CultureInfo.CurrentCulture);
		}
	}
}
