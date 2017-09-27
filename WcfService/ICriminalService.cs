using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ICriminalService
    {
        [OperationContract]
        bool SearchProfiles(SearchFilter searchFilter);
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class SearchFilter
    {
        private List<string> names;
        private List<string> nationalities;
        private char gender;
        private int minAge;
        private int maxAge;
        private decimal minHeight;
        private decimal maxHeight;
        private decimal minWeight;
        private decimal maxWeight;
        private int maxResults;
        private string email;

        [DataMember]
        public List<string> Names
        {
            get { return names; }
            set { names = value; }
        }

        [DataMember]
        public List<string> Nationalities
        {
            get { return nationalities; }
            set { nationalities = value; }
        }

        [DataMember]
        public char Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        [DataMember]
        public int MinAge
        {
            get { return minAge; }
            set { minAge = value; }
        }

        [DataMember]
        public int MaxAge
        {
            get { return maxAge; }
            set { maxAge = value; }
        }

        [DataMember]
        public decimal MinHeight
        {
            get { return minHeight; }
            set { minHeight = value; }
        }

        [DataMember]
        public decimal MaxHeight
        {
            get { return maxHeight; }
            set { maxHeight = value; }
        }

        [DataMember]
        public decimal MinWeight
        {
            get { return minWeight; }
            set { minWeight = value; }
        }

        [DataMember]
        public decimal MaxWeight
        {
            get { return maxWeight; }
            set { maxWeight = value; }
        }

        [DataMember]
        public int MaxResults
        {
            get { return maxResults; }
            set { maxResults = value; }
        }

        [DataMember]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

    }
}
