using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ScriptDecoratorApp
{
    public class SignedScript : ScriptDecorator
    {
        private string? _storedHash;
        private const string CsvPath = "signatures.csv"; //Guarda las firmas en un csv

        public SignedScript(IScript inner) : base(inner) { }

        // Generar firma y su persistencia
        public string sign()
        {
            _storedHash = computeHash(getText());
            SaveSignature();
            return _storedHash;
        }

        // Verificar contra una firma almacenada
        public bool verify()
        {
            if (_storedHash == null) loadSignature();
            if (_storedHash == null) return false;
            return verifyAgainst(_storedHash);
        }

        // verificar contra un hash dado
        public bool verifyAgainst(string? referenceHash)
        {
            if (referenceHash == null) return false;
            string current = computeHash(getText());
            return string.Equals(current, referenceHash,
                                 StringComparison.OrdinalIgnoreCase);
        }

        // Mostrar el hash guardado
        public string? getStoredHash()
        {
            if (_storedHash == null) loadSignature();
            return _storedHash;
        }

        // Volver a generar la firma
        public string resign() => sign();

        // SHA-256 (esto lo hizo la IA)
        private static string computeHash(string text)
        {
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(text));
            return Convert.ToHexString(bytes).ToLowerInvariant();
        }

        // Guardar el CSV en formato firma -> Hash
        private void saveSignature()
        {
            var lines = new System.Collections.Generic.List<string>();

            // Mantener entradas de otros archivos
            if (File.Exists(CsvPath))
            {
                foreach (var line in File.ReadAllLines(CsvPath))
                {
                    var parts = line.Split(',', 2);
                    if (parts.Length == 2 && parts[0] != getPath())
                        lines.Add(line);
                }
            }

            lines.Add($"{getPath()},{_storedHash}");
            File.WriteAllLines(CsvPath, lines);
        }

        private void loadSignature()
        {
            if (!File.Exists(CsvPath)) return;
            foreach (var line in File.ReadAllLines(CsvPath))
            {
                var parts = line.Split(',', 2);
                if (parts.Length == 2 && parts[0] == getPath())
                {
                    _storedHash = parts[1];
                    return;
                }
            }
        }
    }
}
