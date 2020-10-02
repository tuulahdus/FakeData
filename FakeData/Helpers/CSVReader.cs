using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using CsvHelper;
using FakeData.Model;
using Microsoft.Extensions.FileProviders;

namespace FakeData.Helpers
{
    public class CSVReader
    {
        public static List<IAddress> ReadAddress(StreamReader reader, string country, string countryCode)
        {
            List<IAddress> records = new List<IAddress>();
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                while (csv.Read())
                {
                    var record = new Address
                    {
                        Street = csv.GetField(0),
                        StreetNumber = csv.GetField(1)
                    };
                    records.Add(record);
                }
            }
            reader.Dispose();
            return records;
        }

        public static List<T> ReadData<T>(string fileName, Func<StreamReader, List<T>> parser) where T : class
        {
            var embeddedProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly());
            using (var stream = embeddedProvider.GetFileInfo(fileName).CreateReadStream())
            {
                return parser.Invoke(new StreamReader(stream));
            }
        }
    }
}