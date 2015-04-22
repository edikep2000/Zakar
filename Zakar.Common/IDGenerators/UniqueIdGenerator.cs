using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zakar.Common.IDGenerators
{
  public   static class UniqueIdGenerator
  {

      public static String GenerateUniqueIdForZone(String name)
      {
         int hashCode = name.GetHashCode();
          return "Z" + hashCode.ToString();
      }

      public static String GenerateUniqueIdForGroup(string name)
      {
          return "G" + name.GetHashCode();
      }

      public static String GenerateUniqueIdForChapter(string name)
      {

          return "C" + name.GetHashCode();
      }

      public static String GenerateUniqueIdForPCF(String name)
      {
          return "P" + name.GetHashCode();
      }

      public static String GenerateUniqueIdForCell(String name)
      {
          return "C" + name.GetHashCode();
      }

      public static String GenerateUniqueIdForPartner(String fullName)
      {
          return "P" + fullName.GetHashCode();
      }
    }
}
