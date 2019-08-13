using System;

namespace SpeechRecognition.Data.Entities
{
    public class VideoUpload
    {
        public string FileName { get; set; }
        public DateTime DateUpload { get; set; }
        public string  Path { get; set; }
        public int State { get; set; }
        public  string PathAudio { get; set; }
    }
}