using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Roller.DContext;
using System;
using System.Collections.Generic;
using JsonSerializer = System.Text.Json.JsonSerializer;
using System.Net;
using System.Text;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Roller.Repository.Services
{
  public  class PaymentService: IPaymentService
    {
        private RollerDataContext _context;
        private readonly IConfiguration _configuration;
        public PaymentService(RollerDataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public TransactionModel PostTransaction(TransactionModel model)
        {
          
            //string URI = _configuration.GetValue<string>("URL:CyberPayLive");
           // string MyReturnUrl = _configuration.GetValue<string>("MyReturnUrl:CyberPayReturnUrl");
            string MyIntegrationKey = _configuration.GetValue<string>("PayService:MyIntegrationKey");

            string URI = "https://payment-api.cyberpay.ng/api/v1/payments";
            ////string MyReturnUrl = "https://localhost:5001/Payment/CompleteTrans";
            string MyReturnUrl = "http://mia.fcetasaba.edu.ng/Payment/CompleteTrans";

            var NewMerchantRef = GenerateMerchantRef();
            // string MyIntegrationKey = "cea4b4728cc7416ab555715190ea1ae0";
            List<SplitModel> splits = new List<SplitModel>();

            model.Split = JsonSerializer.Serialize(splits);
            var a = JsonSerializer.Deserialize<SplitModel[]>(model.Split);
            var payload = JsonSerializer.Serialize(new
                {
                    CustomerName = model.FullName,
                    CustomerEmail = model.Email,
                    Amount = model.Amount * 100,
                    CustomerMobile = model.Phone,
                    Currency = "NGN",
                    MerchantRef = NewMerchantRef,
                    Reference = model.PaymentReference,
                    IntegrationKey = MyIntegrationKey,
                    Splits = a.Select(x => { var e = x; e.Amount = e.Amount * 100; return e; }).ToArray(),
                    ReturnUrl = MyReturnUrl
                });
            //IntegrationKey = "229010b02d2648feba9622723e147dd5",

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, URI);

                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                var content = new StringContent(payload.ToString(), Encoding.UTF8, "text/json");
                var response = client.PostAsync(URI, content).Result;
                var data = response.Content.ReadAsStringAsync().Result;
                response.EnsureSuccessStatusCode();
                CyberpayResponse adviceResponse = JsonConvert.DeserializeObject<CyberpayResponse>(data);
                if (adviceResponse.Succeeded)
                {
                    var reference = adviceResponse.Data.transactionReference;
                    model.PaymentReference = reference;
                    model.UniqueKey = adviceResponse.Data.redirectUrl;
                   // _context.SaveChanges();
                    // if (Succeeded == false) throw new Exception(result.Message);
                }
                else throw new Exception(adviceResponse.code);

            }

            //using (HttpClient client = new HttpClient())
            //{
            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
            //    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, URI);
            //    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            //    var content = new StringContent(payload.ToString(), Encoding.UTF8, "text/json");
            //    var response = client.PostAsync(URI, content).Result;
            //    response.EnsureSuccessStatusCode();
            //    var data = response.Content.ReadAsStringAsync().Result;
            //    CyberpayResponse adviceResponse = jsonSerializer.Deserialize<CyberpayResponse>(data);
            //    if (adviceResponse.Succeeded)
            //    {
            //        var reference = adviceResponse.data.TransactionReference;
            //        model.PaymentReference = reference;
            //        model.Payload = payload;
            //        model.CreatedBy = model.FullName;
            //        model.UniqueKey = adviceResponse.data.RedirectUrl;
            //        //insert payment advice
            //        var entity = model.Create(model);
            //        _db.Add(entity);
            //        var result = _db.SaveChanges();
            //        if (result.Succeeded == false) throw new Exception(result.Message);
            //    }
            //    else throw new Exception(adviceResponse.data.message);

            //}


            return model;
            
        }
        private string GenerateMerchantRef()
        {
            var generatePayReference = new Random();
            string referencecode = generatePayReference.Next(12321232).ToString();
            return referencecode;
        }

        public TransactionModel GetTransaction(string reference)
        {

            string URI = "https://payment-api.cyberpay.ng/api/v1/payments/" + reference;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, URI);
                var result = client.GetAsync(new Uri(URI));
                var responseContent = result.Result.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<CyberpayResponse>(responseContent);
                if (response != null && response.Succeeded)
                {
                    var myCyberPayResponse = response.Data;

                    _context.SaveChanges();
                    //var getAmtPayTrans = _context.PaymentTransactions.Where(pay => pay.PaymentReference == reference).FirstOrDefault();
                    //if (getAmtPayTrans != null)
                    //{
                    //    getAmtPayTrans.PaymentStatus = myCyberPayResponse.Status;
                    //    getAmtPayTrans.PaymentDate = DateTime.Now;
                    //    getAmtPayTrans.LastDateUpdated = DateTime.Now;
                    //    getAmtPayTrans.IsActive = true;
                }
                //var chkPayReference = _context.CourseExamRegistrations.Where(c => c.PaymentReference == reference).ToList();
                //if (chkPayReference != null)
                //{
                //    foreach (var UpdateAllPayReference in chkPayReference)
                //    {
                //        // UpdateAllPayReference.PaymentReference = reference;
                //        UpdateAllPayReference.PaymentStatus = myCyberPayResponse.Status;
                //        UpdateAllPayReference.IsActive = true;

                //    }

                //    _context.SaveChanges();
                //}

                
                    else
                    {
                        throw new Exception("An Error Occured" + response.Data.message);
                    }

                return new TransactionModel
                {

                };


                    //return new CourseExamRegModel
                    //{
                    //    Amount = getAmtPayTrans.Amount.Value,
                    //    FullName = getAmtPayTrans.PayerName,
                    //    PaymentReference = response.Data.reference,
                    //    PaymentStatus = response.Data.Status,
                    //    Message = response.Data.message
                    //};

                }
              //  throw new Exception("An Error Occured" + response.Data.message);

            }
        //public ActionResult PostCyberpayV2(CyberPay model)
        //{
        //    CyberPayPaymentRequest request = new CyberPayPaymentRequest();


        //    int OperatorID = int.Parse(ConfigurationManager.AppSettings["OperatorID"].ToString());

        //    CyberPayV2Customer customer = new CyberPayV2Customer();
        //    var getAccount = _acct.GetAccount(model.i_account);
        //    request.Currency = "NGN";
        //    request.CustomerId = model.i_customer;
        //    request.MerchantRef = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
        //    if (getAccount.Status == StatusCode.Succeeded)
        //    {
        //        request.Description = model.ItemName + "-" + getAccount.Result.ID.Replace("@msisdn", "");

        //    }
        //    else
        //    {
        //        request.Description = model.ItemName + "-" + model.i_customer;

        //    }

        //    //request.Description = model.ItemName + "-" + model.i_customer;

        //    request.ReturnUrl = ConfigurationManager.AppSettings["CyberPayV2ReturnURL"].ToString();
        //    // request.Description = model.ItemName + "-" + model.i_customer;

        //    request.CustomerEmail = model.Email;

        //    request.Amount = (model.Value * 100);

        //    request.CustomerMobile = model.PhoneNumber;

        //    request.CustomerName = model.FullName;

        //    request.IntegrationKey = ConfigurationManager.AppSettings["CyberPayV2MerchantKey"].ToString();
        //    request.WebhookUrl = ConfigurationManager.AppSettings["Webhook"].ToString();

        //    var url = ConfigurationManager.AppSettings["CyberPayV2SetPaymentEndPoint"].ToString();

        //    _client.Headers.Add("Content-Type", "application/json");

        //    var serialisedRequest = JsonConvert.SerializeObject(request);
        //    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

        //    var responseString = _client.UploadString(url, "POST", serialisedRequest);

        //    var response = JsonConvert.DeserializeObject<CyberPayPaymentResponse>(responseString);

        //    if (response.succeeded == false)
        //    {
        //        return RedirectToAction("List", "Account");
        //    }
        //    //model.Value -= model.VAT;
        //    //Log to DB
        //    CyberPayV2LogModel requestlog = new CyberPayV2LogModel
        //    {
        //        amount = model.Value - model.VAT,
        //        CustomerDesc = model.CustomerDesc + " - Dealer ID",
        //        CustomerID = model.i_customer,
        //        FullName = request.CustomerName,
        //        Email = request.CustomerEmail,
        //        IsSuccessful = false,
        //        ItemCode = model.ItemCode,
        //        ItemName = model.ItemName,
        //        i_account = model.i_account,
        //        i_customer = model.i_customer,
        //        OperatorID = OperatorID,
        //        i_product = model.i_product,
        //        PhoneNumber = request.CustomerMobile,
        //        PortalCode = model.PortalCode,
        //        provider_id = model.provider_id,
        //        Session = model.Session,
        //        DateCreated = DateTime.Now,
        //        Value = model.Value - model.VAT,
        //        RedirectLink = response.data.redirectUrl,
        //        Request_Payload = serialisedRequest,
        //        TransRef = response.data.transactionReference,
        //        Transaction_status = "PENDING",
        //        // AllocateAll = model.AllocateAll,
        //        //AllocateAll = true,
        //        VAT = model.VAT,
        //        ProdVatPrice = model.amount,
        //        VatPercentage = model.VatPercentage,
        //        Source = "DEALERS-CYBERPAY"

        //    };

        //    var result = _payment.AddCyberPayV2RequestLog(requestlog);
        //    if (result.Status == StatusCode.Succeeded)
        //    {
        //        return Redirect(response.data.redirectUrl);

        //    }
        //    else
        //    {
        //        Error("Could not create Log");

        //        return RedirectToAction("List", "Account");
        //    }

        //    //return Redirect(response.data.redirectUrl);
        //}

    } 
}
