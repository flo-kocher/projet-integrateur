using System.IO;
<<<<<<< HEAD
using System.Security.Authentication;
=======
>>>>>>> origin/alpha_merge
using UnityEngine;

namespace Mirror.SimpleWeb
{
<<<<<<< HEAD

    public class SslConfigLoader
=======
    internal class SslConfigLoader
>>>>>>> origin/alpha_merge
    {
        internal struct Cert
        {
            public string path;
            public string password;
        }
<<<<<<< HEAD
        public static SslConfig Load(bool sslEnabled, string sslCertJson, SslProtocols sslProtocols)
        {
            // don't need to load anything if ssl is not enabled
            if (!sslEnabled)
                return default;

            string certJsonPath = sslCertJson;
=======
        internal static SslConfig Load(SimpleWebTransport transport)
        {
            // don't need to load anything if ssl is not enabled
            if (!transport.sslEnabled)
                return default;

            string certJsonPath = transport.sslCertJson;
>>>>>>> origin/alpha_merge

            Cert cert = LoadCertJson(certJsonPath);

            return new SslConfig(
<<<<<<< HEAD
                enabled: sslEnabled,
                sslProtocols: sslProtocols,
=======
                enabled: transport.sslEnabled,
                sslProtocols: transport.sslProtocols,
>>>>>>> origin/alpha_merge
                certPath: cert.path,
                certPassword: cert.password
            );
        }

        internal static Cert LoadCertJson(string certJsonPath)
        {
            string json = File.ReadAllText(certJsonPath);
            Cert cert = JsonUtility.FromJson<Cert>(json);

<<<<<<< HEAD
            if (string.IsNullOrWhiteSpace(cert.path))
            {
                throw new InvalidDataException("Cert Json didn't not contain \"path\"");
            }
            // don't use IsNullOrWhiteSpace here because whitespace could be a valid password for a cert
=======
            if (string.IsNullOrEmpty(cert.path))
            {
                throw new InvalidDataException("Cert Json didn't not contain \"path\"");
            }
>>>>>>> origin/alpha_merge
            if (string.IsNullOrEmpty(cert.password))
            {
                // password can be empty
                cert.password = string.Empty;
            }

            return cert;
        }
    }
}
