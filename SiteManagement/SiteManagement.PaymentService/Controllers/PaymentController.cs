using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiteManagement.Base;
using SiteManagement.Data;
using SiteManagement.Data.Repository;
using SiteManagement.Schema;

namespace SiteManagement.PaymentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {

        private readonly SiteDbContext dbContext;

        public PaymentController(SiteDbContext dbContext)
        {

            this.dbContext = dbContext;


        }

        [HttpPost]
        public ApiResponse Payment(string credino, int billid)
        {
            if (credino.Length == 16)
            {

                var payment = dbContext.DuesBills.Where(x => x.Id == billid).SingleOrDefault();//fatura
                if (payment != null)
                {
                    var crediinfo = dbContext.Banks.Where(x => x.CreditCardNumber == credino).SingleOrDefault();//karttaki para
                    if (crediinfo != null)
                    {
                        if (payment.Status == false)
                        {
                            var total = payment.Electric + payment.NaturalGas + payment.Water + payment.Dues;
                            var balance = crediinfo.Balance;
                            if (balance > total)
                            {
                                balance = balance - total;
                                payment.Status = true;
                                crediinfo.Balance = balance;
                                //dbContext.Update(crediinfo);
                                dbContext.SaveChanges();
                                //return new ApiResponse(true, "Ödeme işlemi başarılı bir şekilde gerçekleştirildi.");
                                return new ApiResponse(true, "The payment transaction has been completed successfully.");

                            }
                            else
                            {
                                //return new ApiResponse(false, "Kartınızda yeterli bakiye yok");
                                return new ApiResponse(false, "There is not enough balance on your card");
                            }
                        }
                        else
                        {
                            //return new ApiResponse(false,"Bu ödeme zaten yapılmış");
                            return new ApiResponse(false, "This payment has already been made");
                        }
                    }
                    else
                    {
                        return new ApiResponse(false, "Check credit card number");
                    }
                }
                else
                {
                    return new ApiResponse(false, "No such payment information is available.");
                }
            }
            else { return new ApiResponse(false, "Credit card number must be 16 digits"); }
        }
    }
}
