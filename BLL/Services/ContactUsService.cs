using BLL.Dtos;
using Utility.Commons;
using Data.Models;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Utility.Enumerations;
using AHDBLL.Dtos.Request;
using System.Data;

namespace BLL.Services
{
    public class ContactUsService
    {
        public ApiResponseMessage Save(ContactU data)
        {
            var response = new ApiResponseMessage();

            try
            {
                ContactU obj = new ContactU();
                using (var contactUsData = new ContactUsRepository())
                {
                    obj.FirstName = data.FirstName;
                    obj.LastName = data.LastName;
                    obj.Subject = data.Subject;
                    obj.EmailAddress = data.EmailAddress;
                    obj.Body = data.Body;
                    obj.MobileNo = data.MobileNo;
                    contactUsData.Add(obj);
                }
                response.Status = HttpStatusCode.OK;
                response.Message = "Saved successfully!";
                response.Response = obj;

            }
            catch (Exception ex)
            {
                response.Status = HttpStatusCode.InternalServerError;
                response.Message = "An Error has occured!";
                response.Response = false;
            }
            return response;
        }

    }
}
