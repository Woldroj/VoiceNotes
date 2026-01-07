using Microsoft.Maui.ApplicationModel;
using System.Threading.Tasks;

namespace VoiceNotes.Services
{
    public static class VoiceService
    {
        public static async Task<string> DictateAsync()
        {
            try
            {
                // Revisar permisos de micrófono
                var status = await Permissions.CheckStatusAsync<Permissions.Microphone>();
                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.Microphone>();
                    if (status != PermissionStatus.Granted)
                        return string.Empty;
                }

                // 🔹 Simulación de dictado
                // Para clase funciona en todas las plataformas
                // En Android real se reemplaza por la implementación nativa
                await Task.Delay(500); // Simula tiempo de dictado
                return "Texto de prueba dictado";
            }
            catch (FeatureNotSupportedException)
            {
                return "Función no soportada en este dispositivo";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}

