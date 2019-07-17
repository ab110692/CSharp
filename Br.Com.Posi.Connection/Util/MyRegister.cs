using Br.Com.Posi.Connection.Enums;
using Br.Com.Posi.Connection.Model;
using Br.Com.Posi.Util;
using Microsoft.Win32;
using System;

namespace Br.Com.Posi.Connection.Util
{
    /// <summary>
    /// Sempre utilizar LocalMachine para ter acesso a chave.
    /// </summary>
    public class MyRegister
    {
        private static MyRegister _connection;
        private string _key;
        private string _subKey;
        private string _companyKey;
        private string _softwareKey;

        protected MyRegister(BancoDeDados dataBase)
        {
            _subKey = "SOFTWARE";
            _companyKey = "POSI";
            _softwareKey = dataBase.GetName();
            _key = _subKey + "\\" + _companyKey + "\\" + _softwareKey;
            CreatePattern();
        }

        public static MyRegister Instance(BancoDeDados dataBase)
        {
            if (_connection == null)
            {
                _connection = new MyRegister(dataBase);
            }
            return _connection;
        }

        private void CreatePattern()
        {
            RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(_key);
            registryKey.Close();
            registryKey.Dispose();
        }


        public void Writerconfiguraction(Configuracao configuration)
        {
            try
            {
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(_key, true);
                registryKey.SetValue("DataBaseName", configuration.BancoDeDados);
                registryKey.SetValue("ServerName", configuration.Servidor);
                registryKey.SetValue("ServerPort", configuration.Porta);
                registryKey.SetValue("UserName", Criptografia.Criptografar(configuration.Usuario));
                registryKey.SetValue("Password", Criptografia.Criptografar(configuration.Senha));
                registryKey.Flush();
                if (!registryKey.Handle.IsClosed)
                {
                    registryKey.Close();
                }
                registryKey.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Configuracao Readerconfiguraction()
        {
            try
            {
                Configuracao configuration = new Configuracao();
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(_key, true);
                configuration.BancoDeDados = registryKey.GetValue("DataBaseName", string.Empty).ToString();
                configuration.Servidor = registryKey.GetValue("ServerName", string.Empty).ToString();
                configuration.Porta = registryKey.GetValue("ServerPort", string.Empty).ToString();
                configuration.Usuario = Criptografia.Descriptografar(registryKey.GetValue("UserName", string.Empty).ToString());
                configuration.Senha = Criptografia.Descriptografar(registryKey.GetValue("Password", string.Empty).ToString());
                if (!registryKey.Handle.IsClosed)
                {
                    registryKey.Close();
                }
                registryKey.Dispose();
                return configuration;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
