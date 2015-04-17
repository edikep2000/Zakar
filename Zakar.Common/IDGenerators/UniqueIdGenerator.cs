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
          var characters = name.Trim().ToCharArray();
          return characters.Aggregate("Z", (current, character) => current + ((int) character));
      }

      public static String GenerateUniqueIdForGroup(string name)
      {
          var characters = name.Trim().ToCharArray();
          return characters.Aggregate("G", (current, character) => current + ((int) character));
      }

      public static String GenerateUniqueIdForChapter(string name)
      {
          var characters = name.Trim().ToCharArray();
          return characters.Aggregate("C", (current, character) => current + ((int)character));
      }


      public static String GenerateUniqueIdForPCF(String name)
      {
          var characters = name.Trim().ToCharArray();
          return characters.Aggregate("P", (current, character) => current + ((int)character));
      }

      public static String GenerateUniqueIdForCell(String name)
      {
          var characters = name.Trim().ToCharArray();
          return characters.Aggregate("C", (current, character) => current + ((int)character));
      }

      public static String GenerateUniqueIdForPartner(String fullName)
      {
          var characters = fullName.Trim().ToCharArray();
          return characters.Aggregate("P", (current, character) => current + ((int)character));
      }
    }
}
