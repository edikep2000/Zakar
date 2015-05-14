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
	[Table(UpdateSchema = true)]
	[KeyGenerator(KeyGenerator.Autoinc)]
	public partial class IdentityUserClaim : INotifyPropertyChanging, INotifyPropertyChanged
	{
		private int _id;
		[Column(IsPrimaryKey = true)]
		[Storage("_id")]
		[System.ComponentModel.DataAnnotations.Required()]
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
		
		private int _userId;
		[Storage("_userId")]
		[System.ComponentModel.DataAnnotations.Required()]
		public virtual int UserId
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
		
		private string _claimType;
		[Storage("_claimType")]
		[System.ComponentModel.DataAnnotations.Required()]
		public virtual string ClaimType
		{
			get
			{
				return this._claimType;
			}
			set
			{
				if(this._claimType != value)
				{
					this.OnPropertyChanging("ClaimType");
					this._claimType = value;
					this.OnPropertyChanged("ClaimType");
				}
			}
		}
		
		private string _claimValue;
		[Storage("_claimValue")]
		[System.ComponentModel.DataAnnotations.Required()]
		public virtual string ClaimValue
		{
			get
			{
				return this._claimValue;
			}
			set
			{
				if(this._claimValue != value)
				{
					this.OnPropertyChanging("ClaimValue");
					this._claimValue = value;
					this.OnPropertyChanged("ClaimValue");
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
