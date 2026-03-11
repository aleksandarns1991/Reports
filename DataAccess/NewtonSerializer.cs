using Newtonsoft.Json;
using Reports.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Reports.DataAccess
{
    public class NewtonSerializer : IDataPersistence<Guard>
    {
        private readonly string fileName = "data.json";

        public void Load(ICollection<Guard> data)
        {
            try
            {
                if (!File.Exists(fileName)) return;

                var content = File.ReadAllText(fileName);                
                var items = JsonConvert.DeserializeObject<ICollection<Guard>>(content)!;
                
                foreach (var item in items)
                {
                    data.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void Save(ICollection<Guard> data)
        {
            try
            {
                var items = JsonConvert.SerializeObject(data,Formatting.Indented);
                File.WriteAllText(fileName, items);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
