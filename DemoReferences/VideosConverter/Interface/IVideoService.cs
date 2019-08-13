using System.Threading.Tasks;
using SpeechRecognition.Data.Entities;

namespace VideosConverter.Interface
{
    public interface IVideoService
    {
        Task<VideoUpload> SaveVideo(string url);
        Task<string> ConverterAudio(string pathFile);
    }
}
