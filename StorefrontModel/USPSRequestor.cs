using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StorefrontModel
{
    class USPSRequestor : CarrierRequester
    {
        //      // http://production.shippingapis.com/ShippingAPI.dll?API=IntlRate&XML=<IntlRateRequest USERID="237IDATA0575" PASSWORD="434XM12EF043"><Package ID="0"><Pounds>0</Pounds><Ounces>1</Ounces><MailType>Package</MailType><Country>Great Britain and Northern Ireland</Country></Package></IntlRateRequest>
        //        String prefix = "&API=IntlRate&XML=<IntlRateRequest USERID=\"" + carrier.getuser() +
        //            "\" PASSWORD=\"" + carrier.getpassword() +"\">";
        //        it = packages.values().iterator();
        //        String request = new String("");
        //        while (it.hasNext()) {
        //          ShippingPackage shippingPackage = (ShippingPackage) it.next();
        //          request += "<Package ID=\"0\"><Pounds>";
        //          if(shippingPackage.weight<2)
        //            shippingPackage.weight = 2;

        //          request += Integer.toString((int)shippingPackage.weight);
        //          request += "</Pounds><Ounces>0</Ounces><MailType>Package</MailType><Country>";
        //          request += ListsBean.GetCountryPostalCode(conn, salesorder.getshippingaddress().getcountry());
        //          request += "</Country></Package></IntlRateRequest>";
        //        }

        //        IXMLInputSerializer inserial = XMLSerializerFactory.
        //            getInputSerializer();
        //        XMLTransmitter transmitter = new XMLTransmitter();

        //        transmitter.setUrl(carrier.geturl());
        //        String ret = transmitter.doTransaction(prefix+request);
        //        inserial.setPackage("com.storefront.webuspsrate");
        //        IntlRateResponse response = (IntlRateResponse)inserial.get(ret);
        //        Iterator it2 = response.getPackageIterator();
        //        double postage = 0;
        //        while(it2.hasNext()) {
        //          com.storefront.webuspsrate.Package packageresp =
        //              (com.storefront.webuspsrate.Package)it2.next();
        //          Iterator it3 = packageresp.getServiceIterator();
        //          while(it3.hasNext()) {
        //            com.storefront.webuspsrate.Service service =
        //                (com.storefront.webuspsrate.Service)it3.next();

        //            if(service.getSvcDescription().getContentData().equals(salesorder.getshippingmethod().getcode())) {
        //              double d = Double.parseDouble(service.getPostage().getContentData());
        //              if(postage==0||postage>d)
        //                postage = d;
        //            }
        //          }
        //        }
        //        totalcharges += postage;
        //      }
    }
}
