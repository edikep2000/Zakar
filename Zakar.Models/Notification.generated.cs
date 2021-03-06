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
	[Table("Notifications", SchemaName = "dbo")]
	[ConcurrencyControl(OptimisticConcurrencyControlStrategy.Changed)]
	[KeyGenerator(KeyGenerator.Autoinc)]
	public partial class Notification : INotifyPropertyChanging, INotifyPropertyChanged
	{
		private int _id;
		[Column("Id", OpenAccessType = OpenAccessType.Int32, IsBackendCalculated = true, IsPrimaryKey = true, Length = 0, Scale = 0, SqlType = "int")]
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
		
		private DateTime _dateSent;
		[Column("DateSent", OpenAccessType = OpenAccessType.DateTime, Length = 0, Scale = 0, SqlType = "datetime")]
		[Storage("_dateSent")]
		[System.ComponentModel.DataAnnotations.DataType(System.ComponentModel.DataAnnotations.DataType.DateTime)]
		[System.ComponentModel.DataAnnotations.Required()]
		public virtual DateTime DateSent
		{
			get
			{
				return this._dateSent;
			}
			set
			{
				if(this._dateSent != value)
				{
					this.OnPropertyChanging("DateSent");
					this._dateSent = value;
					this.OnPropertyChanged("DateSent");
				}
			}
		}
		
		private string _recipientAddress;
		[Column("RecipientAddress", OpenAccessType = OpenAccessType.UnicodeStringInfiniteLength, Length = 0, Scale = 0, SqlType = "nvarchar(max)")]
		[Storage("_recipientAddress")]
		[System.ComponentModel.DataAnnotations.Required()]
		public virtual string RecipientAddress
		{
			get
			{
				return this._recipientAddress;
			}
			set
			{
				if(this._recipientAddress != value)
				{
					this.OnPropertyChanging("RecipientAddress");
					this._recipientAddress = value;
					this.OnPropertyChanged("RecipientAddress");
				}
			}
		}
		
		private string _message;
		[Column("Message", OpenAccessType = OpenAccessType.UnicodeStringInfiniteLength, Length = 0, Scale = 0, SqlType = "nvarchar(max)")]
		[Storage("_message")]
		[System.ComponentModel.DataAnnotations.Required()]
		public virtual string Message
		{
			get
			{
				return this._message;
			}
			set
			{
				if(this._message != value)
				{
					this.OnPropertyChanging("Message");
					this._message = value;
					this.OnPropertyChanged("Message");
				}
			}
		}
		
		private bool _isSent;
		[Column("IsSent", OpenAccessType = OpenAccessType.Bit, Length = 0, Scale = 0, SqlType = "bit")]
		[Storage("_isSent")]
		[System.ComponentModel.DataAnnotations.Required()]
		public virtual bool IsSent
		{
			get
			{
				return this._isSent;
			}
			set
			{
				if(this._isSent != value)
				{
					this.OnPropertyChanging("IsSent");
					this._isSent = value;
					this.OnPropertyChanged("IsSent");
				}
			}
		}
		
		private int _notificationCateoryCategoryId;
		[Column("NotificationCateoryCategoryId", OpenAccessType = OpenAccessType.Int32, Length = 0, Scale = 0, SqlType = "int")]
		[Storage("_notificationCateoryCategoryId")]
		[System.ComponentModel.DataAnnotations.Required()]
		public virtual int NotificationCateoryCategoryId
		{
			get
			{
				return this._notificationCateoryCategoryId;
			}
			set
			{
				if(this._notificationCateoryCategoryId != value)
				{
					this.OnPropertyChanging("NotificationCateoryCategoryId");
					this._notificationCateoryCategoryId = value;
					this.OnPropertyChanged("NotificationCateoryCategoryId");
				}
			}
		}
		
		private int? _churchId;
		[Column("ChurchId", OpenAccessType = OpenAccessType.Int32, IsNullable = true, Length = 0, Scale = 0, SqlType = "int")]
		[Storage("_churchId")]
		public virtual int? ChurchId
		{
			get
			{
				return this._churchId;
			}
			set
			{
				if(this._churchId != value)
				{
					this.OnPropertyChanging("ChurchId");
					this._churchId = value;
					this.OnPropertyChanged("ChurchId");
				}
			}
		}
		
		private NotificationCategory _notificationCategory;
		[ForeignKeyAssociation(ConstraintName = "FK_NotificationCateoryNotifications", SharedFields = "NotificationCateoryCategoryId", TargetFields = "CategoryId")]
		[Storage("_notificationCategory")]
		public virtual NotificationCategory NotificationCategory
		{
			get
			{
				return this._notificationCategory;
			}
			set
			{
				if(this._notificationCategory != value)
				{
					this.OnPropertyChanging("NotificationCategory");
					this._notificationCategory = value;
					this.OnPropertyChanged("NotificationCategory");
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
