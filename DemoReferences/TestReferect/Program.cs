using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideosConverter.Implement;

namespace TestReferect
{
    class Program
    {
        async static Task Main(string[] args)
        {
            VideoService videoService = new VideoService();
            string url = @"http://v1.static.lendbox.vn/Videos//900/9c3/bac/9009c3bac73758026d60cd9a0373cfd9.mp4";
            var video = await videoService.SaveVideo(url);
        }
    }
}
