using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using WcfService.Utils;

namespace WcfService.Persistence
{
    /// <summary>
    /// Data access object for Profile table 
    /// </summary> 
    public class ProfileDAO
    {

        /// <summary>
        /// Get the entire Profile table 
        /// </summary> 
        /// <returns> the Profile table</returns>
        public List<Profile> GetAllProfiles()
        {
            try {
                ProfilesDataContext dc = new ProfilesDataContext();
                return dc.GetTable<Profile>().ToList<Profile>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Search for a matched Profile 
        /// </summary> 
        /// <param name="names"> Name references</param>
        /// <param name="nationalities"> Nationality references</param>
        /// <param name="minAge"> Minimum age reference</param>
        /// <param name="maxAge"> Maximum age reference</param>
        /// <param name="minHeight"> Minimum height reference</param>
        /// <param name="maxHeight"> Maximum height reference</param>
        /// <param name="minWeight"> Minimum weight reference</param>
        /// <param name="maxWeight"> Maximum weight reference</param>
        /// <param name="maxResults"> Maximum number of results to produce</param>
        /// <returns> the matched Profile list</returns>
        public List<Profile> SearchProfiles(SearchFilter searchFilter)
        {
            List<Profile> result = new List<Profile>();
            try
            {
                result = GetAllProfiles();
                //Test if criteria is valid before filter, otherwise just ignore it
                if (searchFilter.Names.Count > 0)
                {
                    result = ProfileFilter.ByName(result, searchFilter.Names);
                }
                if (searchFilter.Nationalities.Count > 0)
                {
                    result = ProfileFilter.ByNationality(result, searchFilter.Nationalities);
                }
                if (searchFilter.Gender == 'M' || searchFilter.Gender == 'F')
                {
                    result = ProfileFilter.ByGender(result, searchFilter.Gender);
                }
                if (searchFilter.MaxAge != 0)
                {
                    result = ProfileFilter.ByAge(result, searchFilter.MinAge, searchFilter.MaxAge);
                }
                if (searchFilter.MaxHeight != 0)
                {
                    result = ProfileFilter.ByHeight(result, searchFilter.MinHeight, searchFilter.MaxHeight);
                }
                if (searchFilter.MaxWeight != 0)
                {
                    result = ProfileFilter.ByWeight(result, searchFilter.MinWeight, searchFilter.MaxWeight);
                }
                if (searchFilter.MaxResults > 0 && result.Count > searchFilter.MaxResults)
                    result = result.GetRange(0, searchFilter.MaxResults);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return result;
        }

    }
}