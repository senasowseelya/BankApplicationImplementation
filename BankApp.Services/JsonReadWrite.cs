using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using BankApp.Models;

namespace BankApp.Database
{
     public class JsonReadWrite
     {
        String content;
        public void ReadData()
        {
            content = File.ReadAllText("C:\\Users\\DELL\\source\\repos\\BankApp.Database\\data.json");
            Data.Banks = JsonConvert.DeserializeObject<List<Bank>>(content);
        }
        public void WriteData(Bank NewBank)
        {
            Data.Banks.Add(NewBank);
            content = JsonConvert.SerializeObject(Data.Banks, Formatting.Indented);
            File.WriteAllText("C:\\Users\\DELL\\source\\repos\\BankApp.Database\\data.json", content);
        }
        public void WriteData()
        {
            content = JsonConvert.SerializeObject(Data.Banks, Formatting.Indented);
            File.WriteAllText("C:\\Users\\DELL\\source\\repos\\BankApp.Database\\data.json", content);
        }


    }
}
