using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using IniParser;
using IniParser.Parser;

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
        X509Store Certificado = new X509Store(StoreName.Root, StoreLocation.CurrentUser);
        //ABRINDO A CONEXÃO COM A PASTA E FAZENDO A LEITURA
        Certificado.Open(OpenFlags.ReadOnly);
        //FILTRANDO BUSCA POR CERTIFICADO BASEADO NO NOME
        var Certificates = Certificado.Certificates.Find(X509FindType.FindBySubjectName, "*", false);
        // FECHANDO A CONEXÃO
        Certificado.Close();

        // VERIFICANDO SE O CERTIFICADO JÁ FOI INSTALADO
        if (Certificates != null && Certificates.Count > 0)
        {
            Console.WriteLine("Certificado já está instalado no Usuario Local");
        }
        else
        {
            InstalarCertificado();
        }
    }

    public void InstalarCertificado()
    {
        var Arquivo = new FileIniDataParser();
        var Dados = Arquivo.ReadFile(@"..\..\..\Config.ini");

        if (File.Exists(Dados["Certificado"]["Caminho"] + Dados["Certificado"]["NomedoArquivo"]))
        {
            // INICIANDO UMA CLASSE QUE RECEBE COMO PARAMETRO O CAMINHO DO CERTIFICADO PARA ADICIONAR
            X509Certificate2 CertificateFile = new X509Certificate2(Dados["Certificado"]["Caminho"] + Dados["Certificado"]["NomedoArquivo"]);

            // INSTANCIA DE PARA CRIAR UM OBJETO PARA CLASSE DE CERTIFICADOS CHAMADA X509Store, NA QUAL VAI FAZER A CONEXÇAO COM A PASTA DE AUTORIDADE DE CERTIFICAÇÃO RAIZ
            X509Store Certificado = new X509Store(StoreName.Root, StoreLocation.CurrentUser);

            //ABRINDO A CONEXÃO COM A PASTA E FAZENDO A LEITURA
            Certificado.Open(OpenFlags.ReadWrite);
            //ADICIONANDO CERTIFICADO
            Certificado.Add(CertificateFile);
            //FECHANDO CONEXÃO
            Certificado.Close();
        }
        else
        {
            Console.WriteLine("Certificado nao encontrado!");
        }
    }
}
