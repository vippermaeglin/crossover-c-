using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading;
using WcfService.Persistence;
using WcfService.Email;
using System.IO;
using WcfService.PDF;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CriminalService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CriminalService.svc or CriminalService.svc.cs at the Solution Explorer and start debugging.
    public class CriminalService : ICriminalService
    {
        public bool SearchProfiles(SearchFilter searchFilter)
        {
            try
            {
                if (searchFilter == null)
                {
                    throw new ArgumentNullException("Search parameters can't be null.");
                }
                ProfileDAO profileDAO = new ProfileDAO();
                List<Profile> profiles = profileDAO.SearchProfiles(searchFilter);

                if (profiles.Count > 0)
                {

                    Thread t = new Thread(delegate ()
                    {
                        try
                        {
                            List<MemoryStream> sProfiles = ConvertPDF(profiles);
                            EmailSender sender = new EmailSender(searchFilter.Email);
                            sender.SendProfiles(sProfiles);
                        }
                        catch (Exception ex)
                        {

                        }
                    });
                    t.IsBackground = true;
                    t.Start();
                }
                else return false;
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public List<MemoryStream> ConvertPDF(List<Profile> profiles)
        {
            List<MemoryStream> sProfiles = new List<MemoryStream>();
            try
            {
                foreach(Profile profile in profiles)
                {
                    PdfPrinter printer =
                        new PdfPrinter(profile);
                    MemoryStream memoryStream = new MemoryStream();
                    printer.Create(memoryStream);

                    sProfiles.Add(new MemoryStream(memoryStream.GetBuffer()));
                }
            }
            catch(Exception ex)
            {

            }
            return sProfiles;
        }
    }
}
