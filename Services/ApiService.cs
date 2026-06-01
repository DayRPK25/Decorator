using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Decorator.Services
{
    internal class ApiService
    {
        private readonly HttpClient client;
        private readonly CookieContainer cookies;
        public static ApiService Instance = new ApiService();
        public string CurrentUser { get; set; }
        public string Token { get; set; }

        public int idUsuario { get; set; }

        public ApiService()
        {
            cookies = new CookieContainer();

            HttpClientHandler handler = new HttpClientHandler
            {
                CookieContainer = cookies,
                UseCookies = true
            };
            client = new HttpClient(handler);

            client.BaseAddress = new Uri("http://170.9.26.131:80");
        }

        public async Task<string> Login(
            string correo,
            string contrasena
            )
        {
            var data = new { correo = correo, contrasena = contrasena };
            string json = JsonSerializer.Serialize(data);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response =
                await client.PostAsync(
                    "api/auth/login",
                    content
                    );
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> Registro(
            string correo,
            string nombre,
            string nombreUsuario,
            string contrasena,
            string rol = "Estudiante"
            
            )
        {
            var data = new { correo = correo, nombre = nombre, nombreUsuario = nombreUsuario, contrasena = contrasena, rol = rol};
            string json = JsonSerializer.Serialize(data);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response =
                await client.PostAsync(
                    "api/auth/register",
                    content
                    );

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }


        public async Task<string> cargarCursos
            (
            int idUsuario

            )
        {
            var data = new {idUsuario = idUsuario};
            string json = JsonSerializer.Serialize(data);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response =
                await client.GetAsync(
                    "api/cursos"
                    );

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> cargarTareas
            (
            int idCurso

            )
        {
            //var data = new { idUsuario = idUsuario, };
            //string json = JsonSerializer.Serialize(data);

            //StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response =
                await client.GetAsync($"api/tareas?idCurso={idCurso}");

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> guardarArchivoP(string nombreArchivoP, string ruta, string contenido, string fechaCreacion, string fechaModificacion, string firma )
        {
            var data = new { nombreArchivoP = nombreArchivoP, ruta = ruta, contenido = contenido,  fechaCreacion = fechaCreacion, fechaModificacion = fechaModificacion, firma = firma };
            string json = JsonSerializer.Serialize(data);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response =
                await client.PostAsync(
                    "api/archivop/guardar",
                    content
                    );

            string body = await response.Content.ReadAsStringAsync();

            MessageBox.Show(body);

            return body;
        }

        public async Task<string> actualizarArchivoP(string nombreArchivoP, string ruta, string contenido, string fechaModificacionActual, string fechaCreacion, string fechaModificacionNueva, string firma)
        {
            var data = new { nombreArchivoP = nombreArchivoP, ruta = ruta, contenido = contenido, fechaModificacionNueva = fechaModificacionNueva, fechaCreacion = fechaCreacion, fechaModificacionActual = fechaModificacionActual, firma = firma };
            string json = JsonSerializer.Serialize(data);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response =
                await client.PostAsync(
                    "api/archivop/actualizar",
                    content
                    );

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        //

        public async Task<string> verificarArchivoP(string nombreArchivoP, string fechaCreacion, string fechaModificacion, string firma)
        {
            var data = new { nombreArchivoP = nombreArchivoP, fechaCreacion = fechaCreacion, fechaModificacion = fechaModificacion, firma = firma };
            string json = JsonSerializer.Serialize(data);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response =
                await client.PostAsync(
                    "api/archivop/verificar",
                    content
                    );

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> crearEntrega(int idArchivoP, DateTime fechaCreacion, string nombreArchivoP, string firma, string ruta, int idTarea,  string nota, string comentarioProfesor, int version = 1, string idGrupoTrabajo = "1")
        {
            var data = new { idArchivoP = idArchivoP, fechaCreacion = fechaCreacion,  nombreArchivoP = nombreArchivoP, firma = firma, ruta = ruta, idGrupoTrabajo = idGrupoTrabajo, idTarea = idTarea, nota = nota, comentarioProfesor = comentarioProfesor, version = version };
            string json = JsonSerializer.Serialize(data);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response =
                await client.PostAsync(
                    "api/entrega/crearEntrega",
                    content
                    );

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
