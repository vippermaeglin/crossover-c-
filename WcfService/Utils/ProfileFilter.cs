using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfService.Persistence;

namespace WcfService.Utils
{
    public static class ProfileFilter
    {
        public static List<Profile> ByName(List<Profile> profiles, List<string> names)
        {
            List < Profile > result = new List<Profile>();
            foreach (Profile p in profiles)
            {
                foreach (string name in names)
                {
                    foreach (string n in name.Split(new char[] { ' ' }).ToList())
                    {
                        if (p.FirstName.ToUpper().TrimEnd().Contains(n.ToUpper()) || p.LastName.ToUpper().TrimEnd().Contains(n.ToUpper()))
                        {
                            result.Add(p);
                        }
                    }
                }
            }
            return result;
        }
        public static List<Profile> ByNationality(List<Profile> profiles, List<string> nationalities)
        {
            List<Profile> result = new List<Profile>();
            foreach (Profile p in profiles)
            {
                foreach (string n in nationalities)
                {
                    if (p.Nationality.ToUpper().TrimEnd()==n.ToUpper())
                    {
                        result.Add(p);
                    }
                }
            }
            return result;
        }
        public static List<Profile> ByGender(List<Profile> profiles, char gender)
        {
            List<Profile> result = new List<Profile>();
            foreach (Profile p in profiles)
            {
                if (p.Gender==gender)
                {
                    result.Add(p);
                }
            }
            return result;
        }

        public static List<Profile> ByAge(List<Profile> profiles, int minAge, int maxAge)
        {
            List<Profile> result = new List<Profile>();
            foreach (Profile p in profiles)
            {
                int age = GetAge(DateTime.Now, p.Birthday);
                if (age >= minAge && age <= maxAge)
                {
                    result.Add(p);
                }

            }
            return result;
        }

        public static List<Profile> ByHeight(List<Profile> profiles, decimal minHeight, decimal maxHeight)
        {
            List<Profile> result = new List<Profile>();
            foreach (Profile p in profiles)
            {
                if (p.Height >= minHeight && p.Height <= maxHeight)
                {
                    result.Add(p);
                }
                
            }
            return result;
        }

        public static List<Profile> ByWeight(List<Profile> profiles, decimal minWeight, decimal maxWeight)
        {
            List<Profile> result = new List<Profile>();
            foreach (Profile p in profiles)
            {
                if (p.Weight >= minWeight && p.Weight <= maxWeight)
                {
                    result.Add(p);
                }

            }
            return result;
        }
        public static int GetAge(DateTime reference, DateTime? birthday)
        {
            int age = reference.Year - birthday.Value.Year;
            if (reference < birthday.Value.AddYears(age)) age--;

            return age;
        }
    }
}