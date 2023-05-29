using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using static System.Formats.Asn1.AsnWriter;

class Inicio
{
    public static void Main()
    {
        Certificate Certificado = new Certificate();
        Certificado.Verificar_Certificado();
    }

}

class Certificate
{

    public void Verificar_Certificado()
    {
        // INSTANCIA DE PARA CRIAR UM OBJETO PARA CLASSE DE CERTIFICADOS CHAMADA X509Store, NA QUAL VAI FAZER A CONEXÇAO COM A PASTA DE AUTORIDADE DE CERTIFICAÇÃO RAIZ
        X509Store Certificado = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
        //ABRINDO A CONEXÃO COM A PASTA E FAZENDO A LEITURA
        Certificado.Open(OpenFlags.ReadOnly);

        //PEGANDO TODOS OS CERTIFICADOS E ADICIONANDO EM UMA LISTA OU COLEÇÃO    
        // X509Certificate2Collection cert = Certificado.Certificates;

        /*foreach (X509Certificate2 x509 in cert)
        {
            Console.WriteLine(x509.IssuerName.Name);
           
        }*/

        var Certificates = Certificado.Certificates.Find(X509FindType.FindBySubjectName, "DigiCert", false);

        if (Certificates != null && Certificates.Count > 0)
        {
            Console.WriteLine("Certificado Existe");
        }
        // FECHANDO A CONEXÃO
        Certificado.Close();
        
    }
}

//DigiCert