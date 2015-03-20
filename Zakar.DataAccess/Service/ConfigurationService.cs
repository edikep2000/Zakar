using System;
using System.Linq;
using System.Linq.Expressions;
using Zakar.DataAccess.Utils;
using Zakar.Models;

namespace Zakar.DataAccess.Service
{
    public class ConfigurationService
    {
        private readonly IRepository<Setting> _repository;

        public ConfigurationService(IRepository<Setting> repository)
        {
            _repository = repository;
        }


        public void AddBooleanSetting(string settingsName, bool value)
        {
            Setting entity = SettingsFactory.BuildNewBooleanSetting();
            try
            {
                bool flag = value;
                entity.Name = settingsName;
                entity.Value = flag.ToString();
                Create(entity);
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public void AddIntegerSetting(string settingsName, object value)
        {
            Setting entity = SettingsFactory.BuildNewIntegerSetting();
            try
            {
                int num = Convert.ToInt32(value);
                entity.Name = settingsName;
                entity.Value = num.ToString();
                Create(entity);
            }
            catch (InvalidCastException exception)
            {
                throw;
            }
            catch (FormatException exception2)
            {
                throw;
            }
            catch (OverflowException exception3)
            {
                throw;
            }
        }

        public void AddStringSetting(string settingsName, string value)
        {
            Setting entity = SettingsFactory.BuildNewStringSetting();
            try
            {
                entity.Name = settingsName;
                entity.Value = value;
                Create(entity);
            }
            catch (Exception exception)
            {
                throw;
            }
        }


        public void Create(Setting entity)
        {
            if (entity != null)
            {
                _repository.Insert(entity);
            }
        }

        public IQueryable<Setting> GetAll()
        {
            return (from i in _repository.GetAll()
                orderby i.Id
                select i);
        }

        public Setting GetSettingByName(string settingsName)
        {
            return
                _repository.Find(i => i.Name == settingsName).FirstOrDefault();
        }

        public Setting GetSingle(int key)
        {
            return _repository.Find(i => i.Id == key).FirstOrDefault();
        }

       
    }
}