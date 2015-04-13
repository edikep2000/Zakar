using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zakar.Common.IDGenerators
{
  public   static class UniqueIdGenerator
    {
      public static String GenerateUniqueIdForZone()
      {
          const string prefix = "Z";
          var rnd = new Random(DateTime.Now.Millisecond);
          var ticks = rnd.Next(0, 3000);
          return prefix + ticks.ToString();
      }

      public static String GenerateUniqueIdForGroup()
      {
          const string prefix = "G";
          var rnd = new Random(DateTime.Now.Millisecond);
          var ticks = rnd.Next(0, 4000);
          return prefix + ticks.ToString();
      }

      public static String GenerateUniqueIdForChapter()
      {
          const string prefix = "C";
          var rnd = new Random(DateTime.Now.Millisecond);
          var ticks = rnd.Next(0, 3000);
          return prefix + ticks.ToString();
      }


      public static String GenerateUniqueIdForPCF()
      {
          const string prefix = "Z";
          var rnd = new Random(DateTime.Now.Millisecond);
          var ticks = rnd.Next(0, 5000);
          return prefix + ticks.ToString();
      }

      public static String GenerateUniqueIdForCell()
      {
          const string prefix = "Z";
          var rnd = new Random(DateTime.Now.Millisecond);
          var ticks = rnd.Next(0, 6000);
          return prefix + ticks.ToString();
      }

      public static String GenerateUniqueIdForPartner()
      {
          const string prefix = "P";
          var rnd = new Random(DateTime.Now.Millisecond);
          var ticks = rnd.Next(0, 10000);
          return prefix + ticks.ToString();
      }
    }
}
