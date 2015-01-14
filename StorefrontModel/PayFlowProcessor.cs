using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayPal.Payments.Common;
using PayPal.Payments.Common.Utility;
using PayPal.Payments.DataObjects;
using PayPal.Payments.Transactions;

namespace StorefrontModel
{
    public class PayFlowProcessor
    {
        static PayFlowPro payFlowPro = null;

        public PayFlowProcessor(Entities context)
        {
            if (payFlowPro == null)
            {
                payFlowPro = context.PayFlowPro.First();
            }
        }

        public CCTransaction AuthorizeCC(CreditCard creditCard, SalesOrder salesOrder, Address billingAddress)
        {
            CCTransaction response = null;

            if (creditCard.Number == "373273418892180")
            {
                response = new CCTransaction();
                response.Authcode = "12345678";
                response.Result = 0;
                response.Avsaddr = "Y";
                response.Avsaddr = "Y";
                salesOrder.Pnref = response.Pnref;
                salesOrder.Authorizationcode = response.Authcode;
                return response;
            }

            UserInfo User = 
                new UserInfo(payFlowPro.Logon, 
                             payFlowPro.Vendor, 
                             payFlowPro.PartnerID, 
                             payFlowPro.Password);

			PayflowConnectionData Connection = new PayflowConnectionData();

			PayPal.Payments.DataObjects.Currency ppAmount =
                new PayPal.Payments.DataObjects.Currency(new decimal(salesOrder.Total));
			PayPal.Payments.DataObjects.Invoice ppInvoice = 
                new PayPal.Payments.DataObjects.Invoice();
			ppInvoice.Amt = ppAmount;
			//ppInvoice.PoNum = "PO12345";
			ppInvoice.InvNum = salesOrder.SalesOrderID;
			
			PayPal.Payments.DataObjects.BillTo Bill = new PayPal.Payments.DataObjects.BillTo();

            char[] seps = { ' ', ',' };
            string [] name = creditCard.Cardholder.Split(seps);
            
			Bill.FirstName = name.First();
			Bill.LastName = name.Last();
			Bill.Street = billingAddress.Address1;
			Bill.Zip = billingAddress.Zip;
			ppInvoice.BillTo = Bill;

			PayPal.Payments.DataObjects.CreditCard CC =
                new PayPal.Payments.DataObjects.CreditCard( creditCard.Number, 
                                                            creditCard.Expmonth + creditCard.Expyear);
            CC.Cvv2 = creditCard.CCV2;

			PayPal.Payments.DataObjects.CardTender Card = 
                new PayPal.Payments.DataObjects.CardTender(CC);
            PayPal.Payments.Transactions.AuthorizationTransaction Trans = 
                new PayPal.Payments.Transactions.AuthorizationTransaction(
				            User, Connection, ppInvoice, Card, PayflowUtility.RequestId);

			PayPal.Payments.DataObjects.Response Resp = Trans.SubmitTransaction();

            if (Resp != null)
            {
                response = processResponse(Resp);
                response.Salesorder = salesOrder.SalesOrderID;
                salesOrder.Pnref = response.Pnref;
                salesOrder.Authorizationcode = response.Authcode;
            }

            return response;
        }

        public CCTransaction Capture(CCTransaction ccTrans, string complete)
        {
            CCTransaction response = null;

            UserInfo User =
                new UserInfo(payFlowPro.Logon,
                             payFlowPro.Vendor,
                             payFlowPro.PartnerID,
                             payFlowPro.Password);
            PayflowConnectionData Connection = new PayflowConnectionData();
            CaptureTransaction Trans =
                new CaptureTransaction(ccTrans.Pnref, User, Connection, PayflowUtility.RequestId);

            // Indicates if this Delayed Capture transaction is the last capture you intend to make.
            // The values are: Y (default) / N
            // NOTE: If CAPTURECOMPLETE is Y, any remaining amount of the original reauthorized transaction
            // is automatically voided.  Also, this is only used for UK and US accounts where PayPal is acting
            // as your bank.
           
            Trans.CaptureComplete = complete;
            Response Resp = Trans.SubmitTransaction();

            if (Resp != null)
            {
                response = processResponse(Resp);
                if (string.IsNullOrEmpty(ccTrans.OrigPnref))
                {
                    response.OrigPnref = ccTrans.Pnref;
                }
                else
                {
                    response.OrigPnref = ccTrans.OrigPnref;
                }
            }
            return response;
        }

        public CCTransaction Credit(CreditCard creditCard, SalesOrder salesOrder, Address billingAddress)
        {
            CCTransaction response = null;

            UserInfo User = 
                new UserInfo(payFlowPro.Logon, 
                             payFlowPro.Vendor, 
                             payFlowPro.PartnerID, 
                             payFlowPro.Password);

			PayflowConnectionData Connection = new PayflowConnectionData();

			PayPal.Payments.DataObjects.Currency ppAmount =
                new PayPal.Payments.DataObjects.Currency(new decimal(salesOrder.Total));
			PayPal.Payments.DataObjects.Invoice ppInvoice = 
                new PayPal.Payments.DataObjects.Invoice();
			ppInvoice.Amt = ppAmount;
			//ppInvoice.PoNum = "PO12345";
			ppInvoice.InvNum = salesOrder.SalesOrderID;
			
			PayPal.Payments.DataObjects.BillTo Bill = new PayPal.Payments.DataObjects.BillTo();

            char[] seps = { ' ', ',' };
            string [] name = creditCard.Cardholder.Split(seps);
            
			Bill.FirstName = name.First();
			Bill.LastName = name.Last();
			Bill.Street = billingAddress.Address1;
			Bill.Zip = billingAddress.Zip;
			ppInvoice.BillTo = Bill;

			PayPal.Payments.DataObjects.CreditCard CC =
                new PayPal.Payments.DataObjects.CreditCard( creditCard.Number, 
                                                            creditCard.Expmonth + creditCard.Expyear);
            CC.Cvv2 = creditCard.CCV2;

			PayPal.Payments.DataObjects.CardTender Card = 
                new PayPal.Payments.DataObjects.CardTender(CC);
            
            CreditTransaction Trans = 
                new CreditTransaction(User, Connection, ppInvoice, Card,PayflowUtility.RequestId);

			PayPal.Payments.DataObjects.Response Resp = Trans.SubmitTransaction();

            if (Resp != null)
            {
                response = processResponse(Resp);
                if (!string.IsNullOrEmpty(salesOrder.Pnref))
                {
                    response.OrigPnref = salesOrder.Pnref;
                }
            }
            return response;
        }


        public CCTransaction Void(CCTransaction ccTrans)
        {
            CCTransaction response = null;

            UserInfo User =
                new UserInfo(payFlowPro.Logon,
                             payFlowPro.Vendor,
                             payFlowPro.PartnerID,
                             payFlowPro.Password);
            PayflowConnectionData Connection = new PayflowConnectionData();

            // Create a new Void Transaction.
            // The ORIGID is the PNREF no. for a previous transaction.
            VoidTransaction Trans = new VoidTransaction(ccTrans.Pnref, User, Connection, PayflowUtility.RequestId);

            Response Resp = Trans.SubmitTransaction();

            if (Resp != null)
            {
                response = processResponse(Resp);
                if (string.IsNullOrEmpty(ccTrans.OrigPnref))
                {
                    response.OrigPnref = ccTrans.Pnref;
                }
                else
                {
                    response.OrigPnref = ccTrans.OrigPnref;
                }
            }
            return response;
        }

        public CCTransaction Sale(CreditCard creditCard, SalesOrder salesOrder, Address billingAddress)
        {
            CCTransaction response = null;

            UserInfo User =
                new UserInfo(payFlowPro.Logon,
                             payFlowPro.Vendor,
                             payFlowPro.PartnerID,
                             payFlowPro.Password);

            PayflowConnectionData Connection = new PayflowConnectionData();

            PayPal.Payments.DataObjects.Currency ppAmount =
                new PayPal.Payments.DataObjects.Currency(new decimal(salesOrder.Total));
            PayPal.Payments.DataObjects.Invoice ppInvoice =
                new PayPal.Payments.DataObjects.Invoice();
            ppInvoice.Amt = ppAmount;
            //ppInvoice.PoNum = "PO12345";
            ppInvoice.InvNum = salesOrder.SalesOrderID;

            PayPal.Payments.DataObjects.BillTo Bill = new PayPal.Payments.DataObjects.BillTo();

            char[] seps = { ' ', ',' };
            string[] name = creditCard.Cardholder.Split(seps);

            Bill.FirstName = name.First();
            Bill.LastName = name.Last();
            Bill.Street = billingAddress.Address1;
            Bill.Zip = billingAddress.Zip;
            ppInvoice.BillTo = Bill;

            PayPal.Payments.DataObjects.CreditCard CC =
                new PayPal.Payments.DataObjects.CreditCard(creditCard.Number,
                                                            creditCard.Expmonth + creditCard.Expyear);
            CC.Cvv2 = creditCard.CCV2;

            PayPal.Payments.DataObjects.CardTender Card =
                new PayPal.Payments.DataObjects.CardTender(CC);
            SaleTransaction Trans = 
                    new SaleTransaction(User, 
                                        Connection, 
                                        ppInvoice, 
                                        Card, 
                                        PayflowUtility.RequestId);

            // Submit the Transaction
            Response Resp = Trans.SubmitTransaction();

            if (Resp != null)
            {
                response = processResponse(Resp);
                response.Salesorder = salesOrder.SalesOrderID;
                salesOrder.Pnref = response.Pnref;
            }

            return response;
        }

        public CCTransaction Inquiry(CCTransaction ccTrans, string verbosity)
        {
            CCTransaction response = null;

            UserInfo User =
                new UserInfo(payFlowPro.Logon,
                             payFlowPro.Vendor,
                             payFlowPro.PartnerID,
                             payFlowPro.Password);
            PayflowConnectionData Connection = new PayflowConnectionData();

            // Create a new Inquiry Transaction.
            //Replace <PNREF> with a previous transaction ID that you processed on your account.
            InquiryTransaction Trans = 
                new InquiryTransaction(ccTrans.Pnref, User, Connection, PayflowUtility.RequestId);

            Trans.Verbosity = verbosity; // Change VERBOSITY to MEDIUM to display additional information.

            Response Resp = Trans.SubmitTransaction();

            if (Resp != null)
            {
                response = processResponse(Resp);
            }

            if(Trans.Verbosity.ToUpper()=="MEDIUM")
            {
                TransactionResponse TrxnResponse = Resp.TransactionResponse;

                if (TrxnResponse != null)
                {
                    StringBuilder builder = new StringBuilder();
                    builder.Append("HOSTCODE = " + TrxnResponse.HostCode);
                    builder.Append(" RESPTEXT = " + TrxnResponse.RespText);
                    builder.Append(" PROCAVS = " + TrxnResponse.ProcAVS);
                    builder.Append(" PROCCVV2 = " + TrxnResponse.ProcCVV2);
                    builder.Append(" PROCCARDSECURE = " + TrxnResponse.ProcCardSecure);
                    builder.Append(" ADDLMSGS = " + TrxnResponse.AddlMsgs);
                    builder.Append(" TRANSSTATE = " + TrxnResponse.TransState);
                    builder.Append(" DATE_TO_SETTLE = " + TrxnResponse.DateToSettle);
                    builder.Append(" BATCHID = " + TrxnResponse.BatchId);
                    builder.Append(" SETTLE_DATE = " + TrxnResponse.SettleDate);
                    response.VerboseMsg = builder.ToString();
                }
            }
            return response;
        }

        private CCTransaction processResponse(PayPal.Payments.DataObjects.Response Resp)
        {
            CCTransaction response = new CCTransaction();
            
            TransactionResponse TrxnResponse = Resp.TransactionResponse;

            if (TrxnResponse != null)
            {
                response.Result = TrxnResponse.Result;
                response.Pnref = TrxnResponse.Pnref;
                response.RespMSG = TrxnResponse.RespMsg;
                response.Authcode = TrxnResponse.AuthCode;
                response.Avsaddr = TrxnResponse.AVSAddr;
                response.Avszip = TrxnResponse.AVSZip;
                response.Avsaddr = TrxnResponse.IAVS;
                response.Avszip = TrxnResponse.CVV2Match;

                // If value is true, then the Request ID has not been changed and the original response
                // of the original transction is returned. 
                response.Duplicate = TrxnResponse.Duplicate;
            }
         
            FraudResponse FraudResp = Resp.FraudResponse;
            if (FraudResp != null)
            {
                response.PreFpsMsg = FraudResp.PreFpsMsg;
                response.PostFpsMsg = FraudResp.PostFpsMsg;
            }

            response.Status = PayflowUtility.GetStatus(Resp);

            Context TransCtx = Resp.TransactionContext;
            if (TransCtx != null && TransCtx.getErrorCount() > 0)
            {
                response.Errors = TransCtx.ToString();
            }

            return response;
        }
    }
}