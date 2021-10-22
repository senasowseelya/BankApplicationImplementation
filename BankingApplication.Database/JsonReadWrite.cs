using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using BankingApplication.Models;



namespace BankingApplication.Database
{
     public class JsonReadWrite
     {
        String Content;
        
        public List<Bank> ReadData()
        {
            Content = File.ReadAllText($"{Path.Combine(Directory.GetCurrentDirectory())}/Data.json");
            return JsonConvert.DeserializeObject<List<Bank>>(Content);
        }
        public void WriteData(Bank NewBank)
        {
            var banks=ReadData();
            banks.Add(NewBank);
            Content = JsonConvert.SerializeObject(banks, Formatting.Indented);
            File.WriteAllText("~\\Data.json", Content);
        }
        public void WriteData()
        {
            var banks = ReadData();
            Content = JsonConvert.SerializeObject(banks, Formatting.Indented);
            File.WriteAllText("~\\Data.json", Content);
        }


    }
}
