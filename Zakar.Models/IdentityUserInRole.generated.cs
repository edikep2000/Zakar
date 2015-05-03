#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the ClassGenerator.ttinclude code generation file.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Common;
using System.Collections.Generic;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Data.Common;
using Telerik.OpenAccess.Metadata.Fluent;
using Telerik.OpenAccess.Metadata.Fluent.Advanced;
using System.ComponentModel;
using Zakar.Models;

namespace Zakar.Models	
{
	[Table("IdentityUserInRole")]
	[KeyGenerator(KeyGenerator.Autoinc)]
	public partial class IdentityUserInRole : INotifyPropertyChanging, INotifyPropertyChanged
	{
		private int _id;
		[Column("Id", OpenAccessType = OpenAccessType.Int32, IsBackendCalculated = true, IsPrimaryKey = true, Length = 0, Scale = 0, SqlType = "int", Converter = "OpenAccessRuntime.Data.IntConverter")]
		[Storage("_id")]
		[System.ComponentModel.DataAnnotations.Required()]
		[System.ComponentModel.DataAnnotations.Key()]
		public virtual int Id
		{
			get
			{
				return this._id;
			}
			set
			{
				if(this._id != value)
				{
					this.OnPropertyChanging("Id");
					this._id = value;
					this.OnPropertyChanged("Id");
				}
			}
		}
		
		private string _userId;
		[Column("UserId", OpenAccessType = OpenAccessType.StringVariableLength, Length = 255, Scale = 0, SqlType = "varchar", Converter = "OpenAccessRuntime.Data.VariableLengthAnsiStringConverter")]
		[Storage("_userId")]
		[System.ComponentModel.DataAnnotations.StringLength(255)]
		[System.ComponentModel.DataAnnotations.Required()]
		public virtual string UserId
		{
			get
			{
				return this._userId;
			}
			set
			{
				if(this._userId != value)
				{
					this.OnPropertyChanging("UserId");
					this._userId = value;
					this.OnPropertyChanged("UserId");
				}
			}
		}
		
		private string _roleId;
		[Column("RoleId", OpenAccessType = OpenAccessType.StringVariableLength, Length = 255, Scale = 0, SqlType = "varchar", Converter = "OpenAccessRuntime.Data.VariableLengthAnsiStringConverter")]
		[Storage("_roleId")]
		[System.ComponentModel.DataAnnotations.StringLength(255)]
		[System.ComponentModel.DataAnnotations.Required()]
		public virtual string RoleId
		{
			get
			{
				return this._roleId;
			}
			set
			{
				if(this._roleId != value)
				{
					this.OnPropertyChanging("RoleId");
					this._roleId = value;
					this.OnPropertyChanged("RoleId");
				}
			}
		}
		
		private IdentityUser _identityUser;
		[ForeignKeyAssociation(SharedFields = "UserId", TargetFields = "Id", IsManaged = true)]
		[Storage("_identityUser")]
		public virtual IdentityUser IdentityUser
		{
			get
			{
				return this._identityUser;
			}
			set
			{
				if(this._identityUser != value)
				{
					this.OnPropertyChanging("IdentityUser");
					this._identityUser = value;
					this.OnPropertyChanged("IdentityUser");
				}
			}
		}
		
		private IdentityRole _identityRole;
		[ForeignKeyAssociation(SharedFields = "RoleId", TargetFields = "Id", IsManaged = true)]
		[Storage("_identityRole")]
		public virtual IdentityRole IdentityRole
		{
			get
			{
				return this._identityRole;
			}
			set
			{
				if(this._identityRole != value)
				{
					this.OnPropertyChanging("IdentityRole");
					this._identityRole = value;
					this.OnPropertyChanged("IdentityRole");
				}
			}
		}
		
		#region INotifyPropertyChanging members
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		protected virtual void OnPropertyChanging(string propertyName)
		{
			if(this.PropertyChanging != null)
			{
				this.PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
			}
		}
		
		#endregion
		
		#region INotifyPropertyChanged members
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void OnPropertyChanged(string propertyName)
		{
			if(this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		#endregion
		
	}
}
#pragma warning restore 1591
