using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using VoiceNotes.Models;

namespace VoiceNotes.Services
{
    public class NotesStorageService
    {
        private readonly string _filePath;

        public NotesStorageService()
        {
            _filePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "notes.json");
        }

        public async Task SaveAsync(IEnumerable<Note> notes)
        {
            var json = JsonSerializer.Serialize(notes);
            await File.WriteAllTextAsync(_filePath, json);
        }

        public async Task<List<Note>> LoadAsync()
        {
            if (!File.Exists(_filePath))
                return new List<Note>();

            var json = await File.ReadAllTextAsync(_filePath);
            return JsonSerializer.Deserialize<List<Note>>(json)
                   ?? new List<Note>();
        }
    }
}
