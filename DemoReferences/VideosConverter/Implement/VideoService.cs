using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MimeTypes.Core;
using SpeechRecognition.Data.Entities;
using VideosConverter.Helper;
using VideosConverter.Interface;
using Xabe.FFmpeg;
using Xabe.FFmpeg.Enums;

namespace VideosConverter.Implement
{
    public class VideoService : IVideoService
    {
        public async Task<VideoUpload> SaveVideo(string url)
        {
            if (url == null)
                return null;
            using (var client = new HttpClient())
            {
                try
                {
                    using (var result = await client.GetAsync(url))
                    {
                        if (!result.IsSuccessStatusCode) return null;
                        var upload = new VideoUpload();
                        var file = await result.Content.ReadAsByteArrayAsync();
                        var ext = MimeTypeMap.GetExtension(result.Content.Headers.ContentType.MediaType);
                        var folderUpload = SupportVideo.CreatePathFolder();
                        var pathToSave = SupportVideo.CreatePathToSave(folderUpload);
                        if (file == null) return null;
                        upload.State = 1;
                        upload.DateUpload = DateTime.Now;
                        upload.FileName = SupportVideo.RandomNameFile(ext);
                        upload.Path = Path.Combine(folderUpload, upload.FileName);
                        var fullPath = Path.Combine(pathToSave, upload.FileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                        {
                            stream.Write(file, 0, file.Length);
                        }
                        return upload;

                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }

        public async Task<string> ConverterAudio(string filename)
        {
            FFmpeg.ExecutablesPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "FFmpeg");
            await FFmpeg.GetLatestVersion().ConfigureAwait(false);
            var folderUpload = SupportVideo.CreatePathFolder();
            var pathToSave = SupportVideo.CreatePathToSave(folderUpload);
            var pathFile = Path.Combine(pathToSave, filename);
            if (!File.Exists(pathFile))
                return "";
            var fileConverter = new FileInfo(pathFile);
            var pathToConvert = new DirectoryInfo(pathToSave).GetFiles().FirstOrDefault(x => x.FullName == fileConverter.FullName);
            var outPutFileName = Path.ChangeExtension(fileConverter.FullName, ".wav");
            try
            {
                var mediaInfo = await MediaInfo.Get(pathToConvert).ConfigureAwait(false);
                var audioStream = mediaInfo.AudioStreams.First();
                var conversion = Conversion.New().AddStream(audioStream)
                    .SetOutput(outPutFileName)
                    .SetOverwriteOutput(true)
                    .SetPreset(ConversionPreset.UltraFast);
                await conversion.Start().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                return null;
            }
           
            return outPutFileName;
        }
    }
}
