using Decorator.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;



namespace Decorator.Forms
{
    public partial class IDE : Form
    {
        private string directorioActual = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private int inicioComando = 0;
        private int idArchivo = 0;

        private string CalcularSHA256(string rutaArchivo)
        {
            using (SHA256 sha256 = SHA256.Create())
            using (FileStream stream = File.OpenRead(rutaArchivo))
            {
                byte[] hashBytes = sha256.ComputeHash(stream);
                return Convert.ToHexString(hashBytes);
            }
        }
        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            // Evitar borrar el prompt
            if ((e.KeyCode == Keys.Back || e.KeyCode == Keys.Left) &&
                richTextBox1.SelectionStart <= inicioComando)
            {
                e.SuppressKeyPress = true;
                return;
            }

            // Evitar escribir antes del prompt
            if (richTextBox1.SelectionStart < inicioComando)
            {
                richTextBox1.SelectionStart = richTextBox1.TextLength;
            }

            // Ejecutar comando
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                string texto = richTextBox1.Text;
                int index = texto.LastIndexOf(">>> ");

                string comando = texto.Substring(index + 4).Trim();

                EjecutarComandoTerminal(comando);
            }
        }

        private void MostrarPrompt()
        {
            richTextBox1.AppendText(directorioActual + " >>> ");
            inicioComando = richTextBox1.TextLength;
            richTextBox1.SelectionStart = richTextBox1.TextLength;
        }

        private void EjecutarComandoReal(string comando)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "cmd.exe";
                psi.Arguments = "/c " + comando;
                psi.WorkingDirectory = directorioActual;
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardError = true;
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;

                Process proceso = new Process();
                proceso.StartInfo = psi;
                proceso.Start();

                string salida = proceso.StandardOutput.ReadToEnd();
                string errores = proceso.StandardError.ReadToEnd();

                proceso.WaitForExit();

                richTextBox1.AppendText(salida);
                richTextBox1.AppendText(errores);
                MostrarPrompt();
            }
            catch (Exception ex)
            {
                richTextBox1.AppendText("Error: " + ex.Message);
                MostrarPrompt();
            }
        }

        private void CambiarDirectorio(string nuevaRuta)
        {
            nuevaRuta = nuevaRuta.Trim().Trim('"');

            string rutaFinal;

            if (Path.IsPathRooted(nuevaRuta))
            {
                rutaFinal = nuevaRuta;
            }
            else
            {
                rutaFinal = Path.Combine(directorioActual, nuevaRuta);
            }

            rutaFinal = Path.GetFullPath(rutaFinal);

            if (Directory.Exists(rutaFinal))
            {
                directorioActual = rutaFinal;
            }
            else
            {
                richTextBox1.AppendText("El sistema no puede encontrar la ruta especificada.\n");
            }
        }


        private void EjecutarComandoTerminal(string comando)
        {
            richTextBox1.AppendText("\n");

            if (comando.Trim() == "")
            {
                MostrarPrompt();
                return;
            }

            if (comando == "clear" || comando == "cls")
            {
                richTextBox1.Clear();
                MostrarPrompt();
                return;
            }

            if (comando.StartsWith("cd "))
            {
                CambiarDirectorio(comando.Substring(3).Trim());
                MostrarPrompt();
                return;
            }

            EjecutarComandoReal(comando);
        }


        private void fastColoredTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control &&
                (e.KeyCode == Keys.C || e.KeyCode == Keys.V || e.KeyCode == Keys.X))
            {
                e.SuppressKeyPress = true;
            }
        }


        bool guardado = false;
        string ruta_archivo = "";
        public IDE()
        {
            InitializeComponent();
            fastColoredTextBox1.ContextMenuStrip = null;
            fastColoredTextBox1.KeyDown += fastColoredTextBox1_KeyDown;
            fastColoredTextBox1.AllowDrop = false;
            fastColoredTextBox1.DragEnter += (s, e) =>
            {
                e.Effect = DragDropEffects.None;
            };
            richTextBox1.KeyDown += richTextBox1_KeyDown;
            richTextBox1.Text = directorioActual + " >>> ";
        }

        private void CargarCarpeta(string rutaCarpeta, TreeNode nodoPadre)
        {
            try
            {
                string[] carpetas = Directory.GetDirectories(rutaCarpeta);

                foreach (string carpeta in carpetas)
                {
                    TreeNode nodoCarpeta = new TreeNode(Path.GetFileName(carpeta));
                    nodoCarpeta.Tag = carpeta;

                    CargarCarpeta(carpeta, nodoCarpeta);

                    nodoPadre.Nodes.Add(nodoCarpeta);
                }

                string[] archivos = Directory.GetFiles(rutaCarpeta);

                foreach (string archivo in archivos)
                {

                    TreeNode nodoArchivo = new TreeNode(Path.GetFileName(archivo));
                    nodoArchivo.Tag = archivo;

                    nodoPadre.Nodes.Add(nodoArchivo);

                }
            }
            catch
            {
                // Evita errores por carpetas sin permisos
            }
        }

        private async void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (guardado == false)
                {
                    SaveFileDialog saveFile = new SaveFileDialog();

                    saveFile.Title = "Guardar";
                    saveFile.Filter = "Archivo Python (*.py)|*.py|Texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
                    saveFile.DefaultExt = ".py";
                    saveFile.FileName = "Default1.py";

                    if (saveFile.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllText(saveFile.FileName, fastColoredTextBox1.Text);

                        richTextBox1.AppendText(
                            "Archivo guardado en:\n" + saveFile.FileName + "\n"
                        );

                        guardado = true;
                        ruta_archivo = saveFile.FileName;

                        string nombreArchivo = Path.GetFileName(saveFile.FileName);

                        DateTime fechaCreacion =
                            File.GetCreationTime(saveFile.FileName);



                        string fechaCreacionF =
                            fechaCreacion.ToString("yyyy-MM-dd HH:mm:ss");



                        string firma = CalcularSHA256(saveFile.FileName);

                        string contenido =
                            File.ReadAllText(ruta_archivo);

                        string respuesta =
                            await ApiService.Instance.guardarArchivoP(
                                nombreArchivo,
                                saveFile.FileName,
                                contenido,
                                fechaCreacionF,
                                fechaCreacionF,
                                firma
                            );

                        MessageBox.Show(
                            respuesta,
                            "Respuesta backend"
                        );
                    }
                }

                richTextBox1.Clear();

                File.WriteAllText(ruta_archivo, fastColoredTextBox1.Text);

                ProcessStartInfo psi = new ProcessStartInfo();

                psi.FileName = "python";
                psi.Arguments = $"\"{ruta_archivo}\"";

                psi.RedirectStandardOutput = true;
                psi.RedirectStandardError = true;
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;

                Process proceso = new Process();

                proceso.StartInfo = psi;
                proceso.Start();

                string salida =
                    proceso.StandardOutput.ReadToEnd();

                string errores =
                    proceso.StandardError.ReadToEnd();

                proceso.WaitForExit();

                richTextBox1.AppendText(salida);
                richTextBox1.AppendText(errores);
                richTextBox1.AppendText("\n>>> ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.ToString(),
                    "Error completo"
                );
            }
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void fastColoredTextBox1_Load(object sender, EventArgs e)
        {

        }

        private async void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (guardado == false)
            {
                SaveFileDialog saveFile = new SaveFileDialog();

                saveFile.Title = "Guardar";
                saveFile.Filter = "Archivo Python (*.py)|*.py|Texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
                saveFile.DefaultExt = ".py";
                saveFile.FileName = "Default1.py";

                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFile.FileName, fastColoredTextBox1.Text);
                    richTextBox1.AppendText("Archivo guardado en:\n" + saveFile.FileName + "\n");
                    guardado = true;
                    ruta_archivo = saveFile.FileName;
                    string nombreArchivo = Path.GetFileName(saveFile.FileName);
                    DateTime fechaCreacion = File.GetCreationTime(saveFile.FileName);
                    DateTime fechaModificacion = File.GetLastWriteTime(saveFile.FileName);
                    string fechaCreacionF = fechaCreacion.ToString(("yyyy-MM-dd HH:mm:ss"));
                    string firma = CalcularSHA256(saveFile.FileName);
                    string contenido = File.ReadAllText(ruta_archivo);
                    await ApiService.Instance.guardarArchivoP(nombreArchivo, saveFile.FileName, contenido, fechaCreacionF, fechaCreacionF, firma);
                }
            }
            else
            {
                DateTime fechaModificacionActual = File.GetLastWriteTime(ruta_archivo);
                File.WriteAllText(ruta_archivo, fastColoredTextBox1.Text);
                string nombreArchivoP = Path.GetFileName(ruta_archivo);
                DateTime fechaCreacion = File.GetCreationTime(ruta_archivo);
                DateTime fechaModificacionNueva = File.GetLastWriteTime(ruta_archivo);
                string fechaCreacionF = fechaCreacion.ToString("yyyy-MM-dd HH:mm:ss");
                string fechaModificacionActualF = fechaModificacionActual.ToString("yyyy-MM-dd HH:mm:ss");
                string fechaModificacionNuevaF = fechaModificacionNueva.ToString("yyyy-MM-dd HH:mm:ss");
                string firma = CalcularSHA256(ruta_archivo);
                string contenido = File.ReadAllText(ruta_archivo);
                await ApiService.Instance.actualizarArchivoP(nombreArchivoP, ruta_archivo, contenido, fechaModificacionActualF, fechaCreacionF, fechaModificacionNuevaF, firma);
            }

        }

        private async void abrirArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Abrir archivo";
            openFileDialog.Filter = "Archivo Python (*.py)|*.py|Texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            string documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog.InitialDirectory = documents;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (Path.GetExtension(openFileDialog.FileName) == ".py")
                {
                    string firma = CalcularSHA256(openFileDialog.FileName);
                    string json = await ApiService.Instance.verificarArchivoP(
                         Path.GetFileName(openFileDialog.FileName), File.GetCreationTime(openFileDialog.FileName).ToString("yyyy-MM-dd HH:mm:ss"), File.GetLastWriteTime(openFileDialog.FileName).ToString("yyyy-MM-dd HH:mm:ss"), firma
                     );

                    JsonDocument doc = JsonDocument.Parse(json);
                    bool success = doc.RootElement.GetProperty("success").GetBoolean();
                    
                    if (!success)
                    {
                        string message = doc.RootElement.GetProperty("error").GetString();
                        MessageBox.Show("Este archivo fue editado fuera del IDE.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    fastColoredTextBox1.Text = File.ReadAllText(openFileDialog.FileName);
                    label1.Text = Path.GetFileName(openFileDialog.FileName);
                    guardado = true;
                    ruta_archivo = openFileDialog.FileName;

                }
                else
                {
                    MessageBox.Show("SOLO SE PUEDEN ABRIR ARCHIVOS .py", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }


            }
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void abrirCarpetaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            string documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            folderDialog.InitialDirectory = documents;
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                string carpeta = folderDialog.SelectedPath;

                treeView1.Nodes.Clear();

                TreeNode nodoRaiz = new TreeNode(Path.GetFileName(carpeta));
                nodoRaiz.Tag = carpeta;

                CargarCarpeta(carpeta, nodoRaiz);

                treeView1.Nodes.Add(nodoRaiz);
                nodoRaiz.Expand();
            }
        }

        private async void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            string ruta = e.Node.Tag.ToString();

            if (File.Exists(ruta))
            {
                if (Path.GetExtension(ruta) == ".py")
                {
                    string firma = CalcularSHA256(ruta);
                    string json = await ApiService.Instance.verificarArchivoP(
                        Path.GetFileName(ruta), File.GetCreationTime(ruta).ToString("yyyy-MM-dd HH:mm:ss"), File.GetLastWriteTime(ruta).ToString("yyyy-MM-dd HH:mm:ss"), firma

                     );

                    JsonDocument doc = JsonDocument.Parse(json);
                    bool success = doc.RootElement.GetProperty("success").GetBoolean();
                    string message = doc.RootElement.GetProperty("message").GetString();
                    if (!success)
                    {
                        MessageBox.Show("Este archivo fue editado fuera del IDE.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    fastColoredTextBox1.Text = File.ReadAllText(ruta);
                    label1.Text = Path.GetFileName(ruta);
                    guardado = true;
                    ruta_archivo = ruta;
                }
                else
                {
                    MessageBox.Show("SOLO SE PUEDEN ABRIR ARCHIVOS .py", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }

        private void toolStripLabel1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void archivoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
