using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTest.Data
{
    public interface IPraseJson
    {
        /// <summary>
        /// Process Data
        /// </summary>
        /// <param name="fileName">file name</param>
        /// <returns>List of persons.</returns>
        JArray ProcessData(string fileName);

        /// <summary>
        /// Process delete, update, and post actions
        /// </summary>
        /// <param name="fileName">json file name</param>
        /// <param name="action">action</param>
        /// <param name="peopleModel">People model</param>
        void ProcessAction(string fileName, int action, PeopleModel peopleModel);

    }
}
