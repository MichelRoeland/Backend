using Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class BlockChainService
    {
        public List<BlockChainPatient> GetAllPatients()
        {
            //var allPatients = new List<BlockChainPatient>();

            //var bcr = new BlockChainApi.BlockChainApiClient();
            //var res = bcr.GetAllPatients();

            //foreach (var i in res.Patients)
            //{
            //    var patient = new BlockChainPatient
            //    {
            //        Address = i.Address,
            //        BsnNumber = i.BsnNumber,
            //        City = i.City,
            //        Country = i.Country,
            //        Initials = i.Initials,
            //        Streetname = i.Streetname,
            //        Housenumber = i.Housenumber,
            //        ItemList = i.ItemsList
            //    };

            //    allPatients.Add(patient);
            //}

            var allPatients = new List<BlockChainPatient>();

            //allPatients = ProcessService.GetPatientsDeserializeAsync();
            var itemList = new List<BlockChainApi.ContentItems>();
            var item1 = new BlockChainApi.ContentItems
            {
                ContentId = "1123123123-12456789-items",
                DataBlockCount = 3,
                DateTimeMutation = DateTime.Now,
            };
            itemList.Add(item1);


            var patient = new BlockChainPatient
            {
                Address = null,
                BsnNumber = "1123123123",
                City = "Zutphen",
                Country = "Nederland",
                Initials = "mr",
                Lastname = "Pasman",
                Streetname = "Bartokstraat",
                Housenumber = "13",
                ItemList = itemList.ToArray()


            };
            allPatients.Add(patient);

            return allPatients;
        }
    }
}
