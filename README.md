# ZATCA-C#


**Step #1:**
```C#
//declaration for invoice data
var sellername = "Garage";
var taxno = "012345678987";
var date = "2021-11-28 02:27 PM";
var Grandtotal = "230";
var vat = "30";
```
**Step#2**
```C#
 //generate 5 TLVs(Type Length Value) Bytes Array
var TlV1 = GetTlV("1", sellername); //returns byte array
var TlV2 = GetTlV("2", taxno); //returns byte array
var TlV3 = GetTlV("3", date); //returns byte array
var TlV4 = GetTlV("4", Grandtotal); //returns byte array
var TlV5 = GetTlV("5", vat); //returns byte array
````
**Step#03**
//Concatinate All TLVs 
```C#
var sumTLV = CombineTwoArrays(CombineTwoArrays(CombineTwoArrays(CombineTwoArrays(TlV1, TlV2), TlV3), TlV4), TlV5); //returns byte array
````
**Step#04**
//Convert the sum of TLVs to Base64
```C#
Convert.ToBase64String(sumTLV, 0, sumTLV.Length);
````
FINAL RESULT:   AQZHYXJhZ2UCDzMwMjA0NTg4NjQwMDAwMwMTMjAyMS0xMS0yOCAwMjoyNyBQTQQDMjMwBQIzMA==  // Base64String 

Notes: The result will now convert to QR IMAGE
https://www.qr-code-generator.com/free-generator/

![image](https://user-images.githubusercontent.com/20103406/143774757-fbae48a7-3fd8-4559-9048-b094eb1f3cc4.png)

Result from ZATCA QR SCAN

![WhatsApp Image 2021-11-28 at 6 37 57 PM (1)](https://user-images.githubusercontent.com/20103406/143775119-fea25a68-9d7f-49f1-be14-b97c98774936.jpg)



**FUNCTIONS**
//Generate Single TLV
```C#
public byte[] GetTlV(string tagno, string val)
{
     byte[] arr1 = new byte[2] { Convert.ToByte(tagno), Convert.ToByte(val.Length) };
     byte[] arr2 = Encoding.ASCII.GetBytes(val);
     return CombineTwoArrays(arr1, arr2);
}

//General funtion for Concatinate 2 byte arrays
private static T[] CombineTwoArrays<T>(T[] a1, T[] a2)
{
    T[] arrayCombined = new T[a1.Length + a2.Length];
    Array.Copy(a1, 0, arrayCombined, 0, a1.Length);
    Array.Copy(a2, 0, arrayCombined, a1.Length, a2.Length);
    return arrayCombined;
}
