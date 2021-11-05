using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using BankingApplication.Models;



namespace BankingApplication.Database
{
     public class JsonReadWrite
     {
        String content;
        public static string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
        public static string filePath = projectDirectory + "\\BankingApplication.Database\\Data.json";
        public List<Bank> ReadData()
        {
            content = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<Bank>>(content) ?? new List<Bank>();
        }
       
       

        public void WriteData(List<Bank> banks)
        {
            content = JsonConvert.SerializeObject(banks, Formatting.Indented);
            File.WriteAllText(filePath, content);
        
        }

    }
}
