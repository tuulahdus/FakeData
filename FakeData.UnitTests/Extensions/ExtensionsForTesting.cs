using System;
using Newtonsoft.Json;
using Xunit.Abstractions;

namespace FakeData.UnitTests.Extensions
{
    public static class ExtensionsForTesting
   {
      public static void Dump(this object obj)
      {
         Console.WriteLine(obj.DumpString());
      }

      public static string DumpString(this object obj, Type t = null)
      {
         var settings = new JsonSerializerSettings {
            Formatting = Formatting.Indented
         };
         return JsonConvert.SerializeObject(obj, t, settings);
      }

      public static void Dump(this ITestOutputHelper console, object obj)
      {
         console.WriteLine(obj.DumpString());
      }

      public static void Dump(this ITestOutputHelper console, object obj, Type type = null)
      {
         console.WriteLine(obj.DumpString(type));
      }
   }
}