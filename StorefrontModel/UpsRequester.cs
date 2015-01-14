using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UPSXml;

namespace StorefrontModel
{
    public class UpsRequester : CarrierRequester
    {
        static private AccessRequest accReq = null;
        static private string url = null;
        static private string version = null;


        public UpsRequester(Entities context)
        {
            if (accReq == null)
            {
                Carrier ups = context.Carrier.Where("UPS");
                url = ups.URL;
                version = ups.Version;
                accReq = new AccessRequest();
                accReq.AccessLicenseNumber = ups.License;
                accReq.UserId = ups.UserId;
                accReq.Password = ups.PassWord;
            }
        }

        public AddressValidationResponse ValidateAddress(string city, string stateProvinceCode, string postalcode)
        {
            AddressValidationRequest av_req = new AddressValidationRequest();
            av_req.lang = "US";
            av_req.Request = new AddressValidationRequestRequest[1];
            av_req.Request[0] = new AddressValidationRequestRequest();
            av_req.Request[0].RequestAction = "AV";
            av_req.Request[0].TransactionReference = new AddressValidationRequestRequestTransactionReference[1];
            av_req.Request[0].TransactionReference[0] = new AddressValidationRequestRequestTransactionReference();
            av_req.Request[0].TransactionReference[0].CustomerContext = "Customer Data";
            av_req.Request[0].TransactionReference[0].XpciVersion = version;
            av_req.Address = new AddressValidationRequestAddress[1];
            av_req.Address[0] = new AddressValidationRequestAddress();
            av_req.Address[0].City = city;
            av_req.Address[0].StateProvinceCode = stateProvinceCode;
            av_req.Address[0].PostalCodeField = postalcode;

            return (AddressValidationResponse)Request(url, accReq, av_req, typeof(AddressValidationResponse));
        }

        public RatingServiceSelectionResponse RateRequest(  Company company,
                                                            Carrier carrier,
                                                            ShippingMethod shippingMethod,
                                                            Dictionary<string, ShippingPackage> packages,
                                                            Address shippingaddress )
        {
            RatingServiceSelectionRequest ratingServiceSelectionRequest = new RatingServiceSelectionRequest();
            ratingServiceSelectionRequest.Request = new RequestType();
            ratingServiceSelectionRequest.CustomerClassification = new CodeType();
            ratingServiceSelectionRequest.Request.TransactionReference = new TransactionReferenceType();
            ratingServiceSelectionRequest.Request.TransactionReference.XpciVersion = version;
            ratingServiceSelectionRequest.Request.TransactionReference.CustomerContext = "Rating and Service";
            ratingServiceSelectionRequest.Request.TransactionReference.ToolVersion = "";
            ratingServiceSelectionRequest.Request.RequestAction = "Rate";
            ratingServiceSelectionRequest.Request.RequestOption = "shop";

            ratingServiceSelectionRequest.PickupType = new CodeType();
            ratingServiceSelectionRequest.PickupType.Code = carrier.PickupType;

            ratingServiceSelectionRequest.Shipment = new ShipmentType();
            ratingServiceSelectionRequest.Shipment.Shipper = new ShipperType();
            ratingServiceSelectionRequest.Shipment.Shipper.Address = new AddressType();
            ratingServiceSelectionRequest.Shipment.Shipper.Address.PostalCode = company.Zip;
            ratingServiceSelectionRequest.Shipment.Shipper.Address.CountryCode = company.Country;

            ratingServiceSelectionRequest.Shipment.ShipFrom = new ShipFromType();
            ratingServiceSelectionRequest.Shipment.ShipFrom.Address = new AddressType();
            ratingServiceSelectionRequest.Shipment.ShipFrom.Address.PostalCode = company.Zip;
            ratingServiceSelectionRequest.Shipment.ShipFrom.Address.CountryCode = company.Country;

            ratingServiceSelectionRequest.Shipment.ShipTo = new ShipToType();
            ratingServiceSelectionRequest.Shipment.ShipTo.Address = new AddressType();
            ratingServiceSelectionRequest.Shipment.ShipTo.Address.AddressLine1 = shippingaddress.Address1;
            ratingServiceSelectionRequest.Shipment.ShipTo.Address.AddressLine2 = shippingaddress.Address2;
            ratingServiceSelectionRequest.Shipment.ShipTo.Address.AddressLine3 = shippingaddress.Address3;
            ratingServiceSelectionRequest.Shipment.ShipTo.Address.City = shippingaddress.City;
            ratingServiceSelectionRequest.Shipment.ShipTo.Address.StateProvinceCode = shippingaddress.State;
            ratingServiceSelectionRequest.Shipment.ShipTo.Address.PostalCode = shippingaddress.Zip;
            //ratingServiceSelectionRequest.Shipment.ShipTo.Address.ResidentialAddressIndicator = shippingaddress.;

            ratingServiceSelectionRequest.Shipment.Service = new CodeDescriptionType();
            ratingServiceSelectionRequest.Shipment.Service.Code = shippingMethod.ServiceCode;

            ratingServiceSelectionRequest.Shipment.Package = new PackageType[packages.Count];

            List<ShippingPackage> shippingPackages = packages.Values.ToList();

            for (int i = 0; i < packages.Count; i++)
            {
                ShippingPackage shippingPackage = shippingPackages[i];
                ratingServiceSelectionRequest.Shipment.Package[i] = new PackageType();
                ratingServiceSelectionRequest.Shipment.Package[i].PackagingType = new CodeDescriptionType();
                ratingServiceSelectionRequest.Shipment.Package[i].PackagingType.Code = "02";
                ratingServiceSelectionRequest.Shipment.Package[i].PackagingType.Description = "Package";
                //ratingServiceSelectionRequest.Shipment.Package[i]. = "Rate Shopping";

                /*
                ratingServiceSelectionRequest.Shipment.Package[i].Dimensions = new DimensionsType();
                ratingServiceSelectionRequest.Shipment.Package[i].Dimensions.UnitOfMeasurement = new UnitOfMeasurementType();
                ratingServiceSelectionRequest.Shipment.Package[i].Dimensions.UnitOfMeasurement.Code = "IN";
                ratingServiceSelectionRequest.Shipment.Package[i].Dimensions.Height = ;
                ratingServiceSelectionRequest.Shipment.Package[i].Dimensions.Length = ;
                ratingServiceSelectionRequest.Shipment.Package[i].Dimensions.Width = ;
                */

                ratingServiceSelectionRequest.Shipment.Package[i].PackageWeight = new WeightType();
                ratingServiceSelectionRequest.Shipment.Package[i].PackageWeight.UnitOfMeasurement =
                    new UnitOfMeasurementType();
                ratingServiceSelectionRequest.Shipment.Package[i].PackageWeight.UnitOfMeasurement.Code = "LBS";
                ratingServiceSelectionRequest.Shipment.Package[i].PackageWeight.Weight = shippingPackage.weight.ToString();
                ratingServiceSelectionRequest.Shipment.Package[i].PackageServiceOptions = new PackageServiceOptionsType();
                ratingServiceSelectionRequest.Shipment.Package[i].PackageServiceOptions.InsuredValue = new InsuredValueType();
                ratingServiceSelectionRequest.Shipment.Package[i].PackageServiceOptions.InsuredValue.CurrencyCode = "UDS";
                ratingServiceSelectionRequest.Shipment.Package[i].PackageServiceOptions.InsuredValue.MonetaryValue = shippingPackage.value.ToString();
            }
            return (RatingServiceSelectionResponse)Request(url, accReq, ratingServiceSelectionRequest, typeof(RatingServiceSelectionResponse));
        }
    }
}
