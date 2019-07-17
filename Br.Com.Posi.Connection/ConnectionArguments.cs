using Microsoft.Win32;
using System;
using System.IO;
using System.Security;
using System.Xml;
using System.Xml.Serialization;

namespace Br.Com.Posi.Connection
{
    /// <summary>
    /// Sempre utilizar LocalMachine para ter acesso a chave.
    /// </summary>
    public class ConnectionArguments
    {
        private static ConnectionArguments _connection;
        private string _key;
        private string _subKey;
        private string _companyKey;
        private string _softwareKey;

        protected ConnectionArguments()
        {
            _subKey = "SOFTWARE";
            _companyKey = "POSI";
            _softwareKey = "Shelf";
            _key = _subKey + "\\" + _companyKey + "\\" + _softwareKey;
            CreatePattern();
        }
        public static ConnectionArguments Instance()
        {
            if (_connection == null)
            {
                _connection = new ConnectionArguments();
            }
            return _connection;
        }

        private void CreatePattern() 
        {
            try
            {
                RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(_key);
                registryKey.Close();
                registryKey.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Writerconfiguraction(Configuration configuraction)
        {
            try
            {
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(_key, true);
                registryKey.SetValue("DataBaseName", configuraction.DataBaseName);
                registryKey.SetValue("ServerName", configuraction.ServerName);
                registryKey.SetValue("ServerPort", configuraction.ServerPort);
                registryKey.SetValue("UserName", configuraction.UserName);
                registryKey.SetValue("Password", configuraction.Password);
                registryKey.Close();
                registryKey.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Configuration Readerconfiguraction()
        {
            try
            {
                Configuration configuraction = new Configuration();
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(_key, true);
                configuraction.DataBaseName = registryKey.GetValue("DataBaseName", string.Empty).ToString();
                configuraction.ServerName = registryKey.GetValue("ServerName", string.Empty).ToString();
                configuraction.ServerPort = registryKey.GetValue("ServerPort", string.Empty).ToString();
                configuraction.UserName = registryKey.GetValue("UserName", string.Empty).ToString();
                configuraction.Password = registryKey.GetValue("Password", string.Empty).ToString();
                registryKey.Close();
                registryKey.Dispose();
                return configuraction;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
