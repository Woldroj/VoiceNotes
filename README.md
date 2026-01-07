# VoiceNotes

## 📖 Descripción
**VoiceNotes** es una aplicación de notas basada en **voz**, desarrollada con **.NET MAUI** para Windows.  
Permite crear notas rápidamente mediante dictado de voz, mostrando la lista de notas en tiempo real.  
Se implementa el patrón **MVVM** y navegación entre páginas de manera sencilla y clara.

---

## 🛠️ Tecnologías utilizadas
- **.NET MAUI** (Multi-platform App UI)  
- **C# 10**  
- **XAML** para interfaces de usuario  
- **MVVM (Model-View-ViewModel)**  
- **Windows SpeechRecognizer** para dictado de voz  

---

## 🗣️ Interfaz natural
- **Dictado por voz (Speech-to-Text)**  
  - Permite crear notas hablando directamente al micrófono.  
  - Utiliza `Windows.Media.SpeechRecognition.SpeechRecognizer` para convertir la voz en texto en tiempo real.  
- Botón de **“🎤 Dictar”** en la página de detalle de la nota, que activa el dictado.

---

## 🚀 Cómo probar la aplicación (Windows)
1. **Clonar el repositorio**
   ```bash
   git clone https://github.com/Woldroj/VoiceNotes.git
2. **Abrir en visual studio 2026**
   - Abrir solucion VoiceNotes.sln.

3. **Activar reconocimimiento de voz en WINDOWS**
   - Configuración -> Privacidad y Seguridad -> Voz
   - Activar **Reconocimiento de voz en linea**

4. **Probar dictado**
   - Haz click en "nueva nota por voz"
   - Presiona "Dictar" habla y tu voz se convertira a texto automaticamente mientras te dice que le has dicho despues
   - Guarda la nota y aparecera en la pagina principal
