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
                    obj.FullName = data.FullName;
                    obj.IsActive = true;
                    obj.Subject = data.Subject;
                    obj.EmailAddress = data.EmailAddress;
                    obj.Body = data.Body;
                    obj.MobileNo = data.MobileNo;
                    obj.CreatedOn = DateTime.Now;
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

        public List<ContactU> List()
        {
            try
            {
                using (var contactData = new ContactUsRepository())
                {
                    return contactData.List();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ApiResponseMessage Delete(int contactId)
        {
            var response = new ApiResponseMessage();
            try
            {
                ContactU contact = new ContactU();
                using (var contactData = new ContactUsRepository())
                {
                    contact = contactData.Get(contactId);

                    if (contact != null && contact.Id > 0)
                    {
                        contact.IsActive = false;
                        contact.UpdatedOn = DateTime.Now;
                        contactData.Update(contact);

                        response.Status = HttpStatusCode.OK;
                        response.Message = "Delete successfully!";
                        response.Response = true;
                    }
                    else
                    { 
                        response.Status = HttpStatusCode.OK;
                        response.Message = "No Record Found!";
                        response.Response = false;
                }
                }
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
