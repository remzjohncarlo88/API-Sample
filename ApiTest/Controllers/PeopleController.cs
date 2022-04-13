using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ApiTest.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ApiTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PeopleController : ControllerBase
    {
        string fileName = "data.json";

        /// <summary>
        /// Retrieve the list of persons from the file.
        /// </summary>
        /// <returns>List of individuals.</returns>
        [HttpGet("Get")]
        public IEnumerable<PeopleModel> Get()
        {
            try
            {
                ParseJson parseJson = new ParseJson();
                return (parseJson.ProcessData(fileName)).ToObject<List<PeopleModel>>();
            }
            catch (Exception)
            {
                throw new Exception("Error retrieving json file.");
            }
        }

        /// <summary>
        /// Create new person.
        /// </summary>
        /// <param name="person">People model</param>
        [HttpPost("Create")]
        public IEnumerable<PeopleModel> Create(PeopleModel person)
        {
            try
            {
                ParseJson parseJson = new ParseJson();
                parseJson.ProcessAction(fileName, 2, person);

                return (parseJson.ProcessData(fileName)).ToObject<List<PeopleModel>>();
            }
            catch (Exception)
            {
                throw new Exception("Error creating person.");
            }
        }

        /// <summary>
        /// Update the person's details.
        /// </summary>
        /// <param name="peopleModel">People model</param>
        [HttpPut("Update")]
        public IEnumerable<PeopleModel> Update(PeopleModel person)
        {
            try
            {
                ParseJson parseJson = new ParseJson();
                parseJson.ProcessAction(fileName, 3, person);

                return (parseJson.ProcessData(fileName)).ToObject<List<PeopleModel>>();
            }
            catch (Exception)
            {
                throw new Exception("Error updating person details.");
            }
        }

        /// <summary>
        /// Deletes a person by id.
        /// </summary>
        /// <param name="id">Person Id</param>
        [HttpDelete("Delete")]
        public void Delete(int id)
        {
            try
            {
                if (id >= 0)
                {
                    ParseJson parseJson = new ParseJson();
                    List<PeopleModel> people = (parseJson.ProcessData(fileName)).ToObject<List<PeopleModel>>();

                    var personToDelete = people.FirstOrDefault(obj => obj.Id == id);
                    parseJson.ProcessAction(fileName, 1, personToDelete);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error deleting person details.");
            }
        }

        /// <summary>
        /// Search person
        /// </summary>
        /// <param name="name">Person's name</param>
        /// <returns></returns>
        [HttpGet("Search")]
        public IEnumerable<PeopleModel> Search(string name)
        {
            try
            {
                ParseJson parseJson = new ParseJson();
                List<PeopleModel> people = (parseJson.ProcessData(fileName)).ToObject<List<PeopleModel>>();

                return people.FindAll(x => x.Name.Contains(name));
            }
            catch (Exception)
            {
                throw new Exception("Error searching.");
            }
        }
    }
}
