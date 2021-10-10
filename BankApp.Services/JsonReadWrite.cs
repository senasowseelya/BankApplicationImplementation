using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace BankApp.Database
{
     public class JsonReadWrite
    {
        String content;
        public void ReadData()
        {
            content = File.ReadAllText("C:\\Users\\DELL\\source\\repos\\BankApp.Database\\data.json");
            Data.Customers = JsonConvert.DeserializeObject <Dictionary < String, Customer >> (content);
        }
        public void WriteData(Customer newAcc)
        {
            Data.Customers.Add(newAcc.accno, newAcc);
            content = JsonConvert.SerializeObject(Data.Customers, Formatting.Indented);
            File.WriteAllText("C:\\Users\\DELL\\source\\repos\\BankApp.Database\\data.json", content);
        }
        public void WriteData()
        {
            content = JsonConvert.SerializeObject(Data.Customers, Formatting.Indented);
            File.WriteAllText("C:\\Users\\DELL\\source\\repos\\BankApp.Database\\data.json", content);
        }
   

    }
}
