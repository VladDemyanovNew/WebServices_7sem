//------------------------------------------------------------------------------
// <copyright file="WebDataService.svc.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System.Data.Services;
using System.Data.Services.Common;
using System.Data.Services.Providers;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;

using Lab6.WebServices.Database;

namespace Lab6.WebServices.Services
{
    public class WsDvr : EntityFrameworkDataService<WsDvrEntities>
    {
        // This method is called only once to initialize service-wide policies.
        public static void InitializeService(DataServiceConfiguration config)
        {
            // TODO: set rules to indicate which entity sets and service operations are visible, updatable, etc.
            // Examples:
            config.SetEntitySetAccessRule("*", EntitySetRights.AllRead | EntitySetRights.AllWrite);
            config.SetServiceOperationAccessRule("*", ServiceOperationRights.All);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
        }

        protected override void OnStartProcessingRequest(ProcessRequestArgs args)
        {
            base.OnStartProcessingRequest(args);
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
        }

        // [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "/AddStudent")]
        [WebGet]
        public Students AddStudent(string name)
        {
            var student = new Students { Name = name };
            this.CurrentDataSource.Students.Add(student);
            this.CurrentDataSource.SaveChanges();
            return student;
        }

        [WebGet]
        public Students UpdateStudent(int id, string name)
        {
            var student = this.CurrentDataSource.Students
                .AsEnumerable()
                .FirstOrDefault(x => x.Id == id);

            student.Name = name;
            this.CurrentDataSource.SaveChanges();

            return student;
        }
    }
}
