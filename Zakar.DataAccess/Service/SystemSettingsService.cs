using System;
using System.Collections.Generic;
using System.Linq;
using Zakar.Models;

namespace Zakar.DataAccess.Service
{
    public class SystemSettingsService
    {
      
        public IRepository<SystemSetting> SystemSettingsRepository { get; set; }

     

        public void CreateSetting(SystemSetting setting)
        {
            if (setting != null)
            {
                SystemSettingsRepository.Insert(setting);
            }
        }

        public void Delete(int id)
        {
            SystemSettingsRepository.Delete(GetSetting(id));
        }

        public void Delete(string name)
        {
            SystemSettingsRepository.Delete(GetSetting(name));
        }

        public IEnumerable<SystemSetting> GetAll()
        {
            return SystemSettingsRepository.GetAll().ToList();
        }

        public SystemSetting GetSetting(int id)
        {
            return
                SystemSettingsRepository.Find(i => i.Id == id)
                    .FirstOrDefault();
        }

        public SystemSetting GetSetting(string name)
        {
            return
                SystemSettingsRepository.Find(i => i.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
        }
    }
}