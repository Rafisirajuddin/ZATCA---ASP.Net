using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Zatca_Dotnet.Controllers
{
    public class QRController : Controller
    {
        // GET: QR
        public ActionResult Index()
        {
            var qrBase64String = ZatcaInvoiceQR();
            //sample result=  AQZHYXJhZ2UCDzMwMjA0NTg4NjQwMDAwMwMTMjAyMS0xMS0yOCAwMjoyNyBQTQQDMjMwBQIzMA==
            return View();
        }

        public string ZatcaInvoiceQR()
        {
            //declaration for invoice data
            var sellername = "Garage";
            var taxno = "302045886400003";
            var date = "2021-11-28 02:27 PM";
            var Grandtotal = "230";
            var vat = "30";

            //generate 5 TLVs Bytes Array
            var TlV1 = GetTlV("1", sellername);
            var TlV2 = GetTlV("2", taxno);
            var TlV3 = GetTlV("3", date);
            var TlV4 = GetTlV("4", Grandtotal);
            var TlV5 = GetTlV("5", vat);

            //Concatinate All TLVs
            var sumTLV = CombineTwoArrays(CombineTwoArrays(CombineTwoArrays(CombineTwoArrays(TlV1, TlV2), TlV3), TlV4), TlV5);

            //Convert the sum of TLVs to Base64
            return Convert.ToBase64String(sumTLV, 0, sumTLV.Length);

        }

        //Generate TLV
        public byte[] GetTlV(string tagno, string val)
        {
            byte[] tempByteArray = new byte[2] { Convert.ToByte(tagno), Convert.ToByte(val.Length) };
            byte[] tempByteArray1 = Encoding.ASCII.GetBytes(val);
            return CombineTwoArrays(tempByteArray, tempByteArray1);
        }

        //General funtion for Concatinate 2 byte arrays
        private static T[] CombineTwoArrays<T>(T[] a1, T[] a2)
        {
            T[] arrayCombined = new T[a1.Length + a2.Length];
            Array.Copy(a1, 0, arrayCombined, 0, a1.Length);
            Array.Copy(a2, 0, arrayCombined, a1.Length, a2.Length);
            return arrayCombined;
        }
    }
}