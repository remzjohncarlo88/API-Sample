using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTest.Data
{
    public class ParseJson : IPraseJson
    {
        /// <summary>
        /// Process Data
        /// </summary>
        /// <param name="fileName">file name</param>
        /// <returns>List of persons.</returns>
        public JArray ProcessData(string fileName)
        {
            try
            {
                var model = default(JArray);
                if (!File.Exists(fileName)) return model;

                var jObject = JObject.Parse(File.ReadAllText(fileName));

                return (JArray)jObject["People"];
            }
            catch (Exception)
            {
                throw new Exception("Error parsing json file.");
            }
        }

        /// <summary>
        /// Process delete, update, and post actions
        /// </summary>
        /// <param name="fileName">json file name</param>
        /// <param name="action">action</param>
        /// <param name="peopleModel">People model</param>
        public void ProcessAction(string fileName, int action, PeopleModel peopleModel)
        {
            try
            {
                JObject peopleObj = JObject.Parse(File.ReadAllText(fileName));
                JArray peopleList = (JArray)peopleObj["People"];

                switch (action)
                {
                    case 1:
                        var detailsToDelete = peopleList.FirstOrDefault(obj => obj["Id"].Value<int>() == peopleModel.Id);
                        peopleList.Remove(detailsToDelete);

                        break;
                    case 2:
                        string newPerson = "{ 'Id': " + peopleModel.Id + ", 'Name': '" + peopleModel.Name + "', 'Age': " + peopleModel.Age + "}";
                        peopleList.Add(JObject.Parse(newPerson));
                        peopleObj["People"] = peopleList;

                        break;
                    case 3:
                        foreach (var person in peopleList.Where(obj => obj["Id"].Value<int>() == peopleModel.Id))
                        {
                            person["Name"] = !string.IsNullOrEmpty(peopleModel.Name) ? peopleModel.Name : "";
                            person["Age"] = peopleModel.Age;
                        }

                        peopleObj["People"] = peopleList;

                        break;
                    default:
                        break;
                }                

                string output = Newtonsoft.Json.JsonConvert.SerializeObject(peopleObj, Newtonsoft.Json.Formatting.Indented);
                System.IO.File.WriteAllText(fileName, output);
            }
            catch (Exception)
            {
                throw new Exception("Error processing json file.");
            }
        }
    }
}
