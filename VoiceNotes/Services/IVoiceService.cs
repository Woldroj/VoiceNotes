using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VoiceNotes.Services
{
    public interface IVoiceService
    {
        Task<string> DictateAsync();
    }
}
