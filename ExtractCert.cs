using System;
using System.Security.Cryptography.X509Certificates;
using System.IO;

class Program
{
    static void Main()
    {
        X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
        store.Open(OpenFlags.ReadOnly);

        try
        {
            foreach (X509Certificate2 cert in store.Certificates)
            {
                if (cert.HasPrivateKey)
                {
                    Console.WriteLine($"Found Certificate: {cert.Subject}");

                    string pfxFilePath = $"exported_cert_{Guid.NewGuid()}.pfx";
                    string pemFilePath = $"exported_cert_{Guid.NewGuid()}.pem";

                    Console.Write("Enter a password for the PFX file: ");
                    string password = Console.ReadLine();

                    byte[] pfxData = cert.Export(X509ContentType.Pfx, password);
                    File.WriteAllBytes(pfxFilePath, pfxData);
                    Console.WriteLine($"Certificate exported to: {pfxFilePath}");

                    byte[] certBytes = cert.Export(X509ContentType.Cert);
                    string base64 = Convert.ToBase64String(certBytes, Base64FormattingOptions.InsertLineBreaks);
                    string pem = $"-----BEGIN CERTIFICATE-----\n{base64}\n-----END CERTIFICATE-----\n";
                    File.WriteAllText(pemFilePath, pem);
                    Console.WriteLine($"Certificate exported to: {pemFilePath}");
                }
            }
        }
        finally
        {
            store.Close();
        }
    }
}