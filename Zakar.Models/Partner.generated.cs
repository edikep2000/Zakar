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
	[Table("Partners", SchemaName = "dbo")]
	[ConcurrencyControl(OptimisticConcurrencyControlStrategy.Changed)]
	[KeyGenerator(KeyGenerator.Autoinc)]
	public partial class Partner : INotifyPropertyChanging, INotifyPropertyChanged
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
		
		private string _title;
		[Column("Title", OpenAccessType = OpenAccessType.UnicodeStringVariableLength, Length = 25, Scale = 0, SqlType = "nvarchar")]
		[Storage("_title")]
		[System.ComponentModel.DataAnnotations.StringLength(25)]
		[System.ComponentModel.DataAnnotations.Required()]
		public virtual string Title
		{
			get
			{
				return this._title;
			}
			set
			{
				if(this._title != value)
				{
					this.OnPropertyChanging("Title");
					this._title = value;
					this.OnPropertyChanged("Title");
				}
			}
		}
		
		private string _firstName;
		[Column("FirstName", OpenAccessType = OpenAccessType.UnicodeStringVariableLength, Length = 50, Scale = 0, SqlType = "nvarchar")]
		[Storage("_firstName")]
		[System.ComponentModel.DataAnnotations.StringLength(50)]
		[System.ComponentModel.DataAnnotations.Required()]
		public virtual string FirstName
		{
			get
			{
				return this._firstName;
			}
			set
			{
				if(this._firstName != value)
				{
					this.OnPropertyChanging("FirstName");
					this._firstName = value;
					this.OnPropertyChanged("FirstName");
				}
			}
		}
		
		private string _lastName;
		[Column("LastName", OpenAccessType = OpenAccessType.UnicodeStringVariableLength, Length = 50, Scale = 0, SqlType = "nvarchar")]
		[Storage("_lastName")]
		[System.ComponentModel.DataAnnotations.StringLength(50)]
		[System.ComponentModel.DataAnnotations.Required()]
		public virtual string LastName
		{
			get
			{
				return this._lastName;
			}
			set
			{
				if(this._lastName != value)
				{
					this.OnPropertyChanging("LastName");
					this._lastName = value;
					this.OnPropertyChanged("LastName");
				}
			}
		}
		
		private string _email;
		[Column("Email", OpenAccessType = OpenAccessType.UnicodeStringVariableLength, Length = 100, Scale = 0, SqlType = "nvarchar")]
		[Storage("_email")]
		[System.ComponentModel.DataAnnotations.StringLength(100)]
		[System.ComponentModel.DataAnnotations.Required()]
		public virtual string Email
		{
			get
			{
				return this._email;
			}
			set
			{
				if(this._email != value)
				{
					this.OnPropertyChanging("Email");
					this._email = value;
					this.OnPropertyChanged("Email");
				}
			}
		}
		
		private string _yookosId;
		[Column("YookosId", OpenAccessType = OpenAccessType.UnicodeStringVariableLength, IsNullable = true, Length = 30, Scale = 0, SqlType = "nvarchar")]
		[Storage("_yookosId")]
		[System.ComponentModel.DataAnnotations.StringLength(30)]
		public virtual string YookosId
		{
			get
			{
				return this._yookosId;
			}
			set
			{
				if(this._yookosId != value)
				{
					this.OnPropertyChanging("YookosId");
					this._yookosId = value;
					this.OnPropertyChanged("YookosId");
				}
			}
		}
		
		private string _phone;
		[Column("Phone", OpenAccessType = OpenAccessType.UnicodeStringVariableLength, Length = 50, Scale = 0, SqlType = "nvarchar")]
		[Storage("_phone")]
		[System.ComponentModel.DataAnnotations.StringLength(50)]
		[System.ComponentModel.DataAnnotations.Required()]
		public virtual string Phone
		{
			get
			{
				return this._phone;
			}
			set
			{
				if(this._phone != value)
				{
					this.OnPropertyChanging("Phone");
					this._phone = value;
					this.OnPropertyChanged("Phone");
				}
			}
		}
		
		private DateTime _dateCreated;
		[Column("DateCreated", OpenAccessType = OpenAccessType.DateTime, Length = 0, Scale = 0, SqlType = "datetime")]
		[Storage("_dateCreated")]
		[System.ComponentModel.DataAnnotations.DataType(System.ComponentModel.DataAnnotations.DataType.DateTime)]
		[System.ComponentModel.DataAnnotations.Required()]
		public virtual DateTime DateCreated
		{
			get
			{
				return this._dateCreated;
			}
			set
			{
				if(this._dateCreated != value)
				{
					this.OnPropertyChanging("DateCreated");
					this._dateCreated = value;
					this.OnPropertyChanged("DateCreated");
				}
			}
		}
		
		private int _churchId;
		[Column("ChurchId", OpenAccessType = OpenAccessType.Int32, Length = 0, Scale = 0, SqlType = "int")]
		[Storage("_churchId")]
		[System.ComponentModel.DataAnnotations.Required()]
		public virtual int ChurchId
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
		
		private bool _deleted;
		[Column("Deleted", OpenAccessType = OpenAccessType.Bit, Length = 0, Scale = 0, SqlType = "bit")]
		[Storage("_deleted")]
		[System.ComponentModel.DataAnnotations.Required()]
		public virtual bool Deleted
		{
			get
			{
				return this._deleted;
			}
			set
			{
				if(this._deleted != value)
				{
					this.OnPropertyChanging("Deleted");
					this._deleted = value;
					this.OnPropertyChanged("Deleted");
				}
			}
		}
		
		private DateTime? _dateDeleted;
		[Column("DateDeleted", OpenAccessType = OpenAccessType.DateTime, IsNullable = true, Length = 0, Scale = 0, SqlType = "datetime")]
		[Storage("_dateDeleted")]
		[System.ComponentModel.DataAnnotations.DataType(System.ComponentModel.DataAnnotations.DataType.DateTime)]
		public virtual DateTime? DateDeleted
		{
			get
			{
				return this._dateDeleted;
			}
			set
			{
				if(this._dateDeleted != value)
				{
					this.OnPropertyChanging("DateDeleted");
					this._dateDeleted = value;
					this.OnPropertyChanged("DateDeleted");
				}
			}
		}
		
		private Church _church;
		[ForeignKeyAssociation(ConstraintName = "FK_Partners_Churches", SharedFields = "ChurchId", TargetFields = "ChurchId")]
		[Storage("_church")]
		public virtual Church Church
		{
			get
			{
				return this._church;
			}
			set
			{
				if(this._church != value)
				{
					this.OnPropertyChanging("Church");
					this._church = value;
					this.OnPropertyChanged("Church");
				}
			}
		}
		
		private IList<Partnership> _partnerships = new List<Partnership>();
		[Collection(InverseProperty = "Partner")]
		[Storage("_partnerships")]
		public virtual IList<Partnership> Partnerships
		{
			get
			{
				return this._partnerships;
			}
		}
		
		private IList<NonValidatedPartnershipRecord> _nonValidatedPartnershipRecords = new List<NonValidatedPartnershipRecord>();
		[Collection(InverseProperty = "Partner1")]
		[Storage("_nonValidatedPartnershipRecords")]
		public virtual IList<NonValidatedPartnershipRecord> NonValidatedPartnershipRecords
		{
			get
			{
				return this._nonValidatedPartnershipRecords;
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
